using System;
using System.Text;
using System.Runtime.InteropServices;

namespace WhenFullScreen
{
    public class WindowHandle
    {
        private enum GetAncestorFlags
        {
            /// <summary>
            /// Retrieves the parent window. This does not include the owner, as it does with the GetParent function. 
            /// </summary>
            GetParent = 1,
            /// <summary>
            /// Retrieves the root window by walking the chain of parent windows.
            /// </summary>
            GetRoot = 2,
            /// <summary>
            /// Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent. 
            /// </summary>
            GetRootOwner = 3
        }

        private const int MONITOR_DEFAULTTONULL = 0;
        private const int MONITOR_DEFAULTTOPRIMARY = 1;
        private const int MONITOR_DEFAULTTONEAREST = 2;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

            public int X
            {
                get { return Left; }
                set { Right -= (Left - value); Left = value; }
            }

            public int Y
            {
                get { return Top; }
                set { Bottom -= (Top - value); Top = value; }
            }

            public int Height
            {
                get { return Bottom - Top; }
                set { Bottom = value + Top; }
            }

            public int Width
            {
                get { return Right - Left; }
                set { Right = value + Left; }
            }

            public System.Drawing.Point Location
            {
                get { return new System.Drawing.Point(Left, Top); }
                set { X = value.X; Y = value.Y; }
            }

            public System.Drawing.Size Size
            {
                get { return new System.Drawing.Size(Width, Height); }
                set { Width = value.Width; Height = value.Height; }
            }

            public static implicit operator System.Drawing.Rectangle(RECT r)
            {
                return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
            }

            public static implicit operator RECT(System.Drawing.Rectangle r)
            {
                return new RECT(r);
            }

            public static bool operator ==(RECT r1, RECT r2)
            {
                return r1.Equals(r2);
            }

            public static bool operator !=(RECT r1, RECT r2)
            {
                return !r1.Equals(r2);
            }

            public bool Equals(RECT r)
            {
                return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            }

            public override bool Equals(object obj)
            {
                if (obj is RECT)
                    return Equals((RECT)obj);
                else if (obj is System.Drawing.Rectangle)
                    return Equals(new RECT((System.Drawing.Rectangle)obj));
                return false;
            }

            public override int GetHashCode()
            {
                return ((System.Drawing.Rectangle)this).GetHashCode();
            }

            public override string ToString()
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow(); // 최상위 윈도우의 핸들을 얻어옵니다.

        [DllImport("user32.dll", ExactSpelling = true)]
        static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestorFlags flags); // hWnd를 포함하는 최상위 핸들을 얻어옵니다.

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount); // hWnd의 클래스명을 얻어옵니다.

        [DllImport("user32.dll")]
        static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags); // hWnd가 속하는 모니터의 핸들을 얻어옵니다.

        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi); // 모니터의 정보를 얻어옵니다.

        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow(); // 화면의 해상도

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect); // hWnd의 사각영역을 얻어옵니다.

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect); // hWnd의 작업영역을 얻어옵니다.

        /*---------------------------------------------------------------------------------*/

        public WindowHandle()
        {
            // 핸들을 설정하지 않으면 이 시점에서의 최상위 윈도우로 설정됩니다.
            m_hWnd = GetForegroundWindow();
        }

        public WindowHandle(IntPtr hWnd)
        {
            m_hWnd = hWnd;
        }

        /*---------------------------------------------------------------------------------*/

        IntPtr m_hWnd = IntPtr.Zero;

        /*---------------------------------------------------------------------------------*/

        public bool IsFullScreen
        {
            get
            {
                if (m_hWnd == IntPtr.Zero)
                    return false;
                
                // 컨트롤의 최상위 핸들을 얻어옵니다.
                IntPtr hWnd = GetAncestor(m_hWnd, GetAncestorFlags.GetRoot);
                if (hWnd == IntPtr.Zero)
                    return false;
                
                // 클래스이름을 얻어옵니다.
                StringBuilder className = new StringBuilder(256);
                if (GetClassName(hWnd, className, className.Capacity) == 0)
                    return false;
                string classNameStr = className.ToString();
                
                // 바탕화면, 시작화면은 제외
                if (classNameStr == "WorkerW"/*배경화면*/
                    ||
                    classNameStr == "ProgMan"
                    ||
                    classNameStr == "ImmersiveLauncher"/*Win8 시작화면*/)
                    return false;
                
                // 현재 컨트롤이 속하는 모니터의 사각영역을 얻어옵니다.
                RECT desktop;
                IntPtr monitor = MonitorFromWindow(hWnd, MONITOR_DEFAULTTONEAREST);
                if (monitor == IntPtr.Zero)
                {
                    // 모니터를 찾을 수 없으면 현재 윈도우 화면의 핸들로 설정한다.
                    IntPtr desktopWnd = GetDesktopWindow();
                    if (desktopWnd == IntPtr.Zero)
                        return false;

                    if (GetWindowRect(desktopWnd, out desktop) == false)
                        return false;
                }
                else
                {
                    MONITORINFO info = new MONITORINFO();
                    info.cbSize = Marshal.SizeOf(info);
                    if (GetMonitorInfo(monitor, ref info) == false)
                        return false;

                    desktop = info.rcMonitor;
                }
                
                // 컨트롤의 작업영역을 알아낸다.
                RECT client;
                if (GetClientRect(hWnd, out client) == false)
                    return false;
                
                // 영역을 크기로 변환한다.
                int cx = client.Right - client.Left;
                int cy = client.Bottom - client.Top;

                int dx = desktop.Right - desktop.Left;
                int dy = desktop.Bottom - desktop.Top;
                
                // 컨트롤의 크기가 모니터 크기보다 작으면 전체화면이 아니다.
                if (cx < dx || cy < dy)
                    return false;
                
                // 여기까지 도달했으면 전체화면이다.
                return true;
            }
        }
    }
}

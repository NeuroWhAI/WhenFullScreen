using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Win32;

namespace WhenFullScreen
{
    public partial class Form_Main : Form, IStringReceiver
    {
        public Form_Main()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Minimized;

            m_fullscreenEvent.WhenRisingEdge = this.WhenEnterFullscreen;
            m_fullscreenEvent.WhenFallingEdge = this.WhenLeaveFullscreen;
        }

        /*---------------------------------------------------------------------------------*/

        // 화면 상태가 변함에따라 하강에지, 상승에지 이벤트를 호출해주는 객체
        private EdgeEvent m_fullscreenEvent = new EdgeEvent(false);
        
        // 종료한 프로세스 정보 목록
        private List<ProcessInfo> m_killedProgramList = new List<ProcessInfo>();

        // 전체화면이더라도 무시할 클래스명 목록
        private string[] m_classNamesExcluded =
        {
            "WorkerW",                              // 배경화면
            "ProgMan",
            "ImmersiveLauncher",                    // Win8 시작화면
        };

        // 프로세스 목록에 변화가 있는지 여부
        private bool m_bProcessListChanged = false;

        /*---------------------------------------------------------------------------------*/

        private void Form_Main_Load(object sender, EventArgs e)
        {
            if (Environment.Is64BitProcess == false
                &&
                Environment.Is64BitOperatingSystem)
            {
                MessageBox.Show("64비트 실행파일로 실행해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();

                return;
            }

            // 시작프로그램에 등록되어 있는지 확인
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey
                 ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            string valName = "WhatBox_WhenFullScreen";

            if (rkey.GetValue(valName) == null)
            {
                this.ToolStripMenuItem_startupSet.Text = "Windows 시작시 실행하게";
            }
            else
            {
                this.ToolStripMenuItem_startupSet.Text = "Windows 시작시 실행하지않게";
            }

            // 프로세스 목록 불러오기
            ListboxFileInterface.ReadFile("processes.txt", this.listBox_program);

            HideForm();

            this.timer_update.Start();

            // 프로그램 실행 알림
            this.notifyIcon_tray.ShowBalloonTip(2000, "When FullScreen", "I\'m HERE!", ToolTipIcon.Info);
        }

        private void button_hide_Click(object sender, EventArgs e)
        {
            HideForm();
        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.timer_update.Stop();

            this.notifyIcon_tray.Visible = false;
        }

        private void ToolStripMenuItem_open_Click(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void ToolStripMenuItem_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void notifyIcon_tray_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void timer_update_Tick(object sender, EventArgs e)
        {
            WindowHandle wnd = new WindowHandle();

            m_fullscreenEvent.SetSignal(wnd.IsFullScreen(m_classNamesExcluded));
        }

        private void ToolStripMenuItem_addProc_Click(object sender, EventArgs e)
        {
            Form_InputProcess inputForm = new Form_InputProcess(this);
            inputForm.ShowDialog();
        }

        private void ToolStripMenuItem_removeProc_Click(object sender, EventArgs e)
        {
            if (this.listBox_program.SelectedItem != null)
            {
                this.listBox_program.Items.Remove(this.listBox_program.SelectedItem);

                // 목록 변경 알림
                m_bProcessListChanged = true;
            }
        }

        private void contextMenuStrip_programList_Opening(object sender, CancelEventArgs e)
        {
            // 목록에 선택된 항목의 유무에따라 삭제옵션의 활성화 여부를 설정
            this.ToolStripMenuItem_removeProc.Enabled = (this.listBox_program.SelectedItem != null);
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 프로세스 목록이 변했으면 
            if (m_bProcessListChanged)
            {
                // 목록 저장
                ListboxFileInterface.WriteFile("processes.txt", this.listBox_program);
            }
        }

        private void ToolStripMenuItem_startupSet_Click(object sender, EventArgs e)
        {
            RegistryKey rkey = Registry.CurrentUser.OpenSubKey
                 ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            string valName = "WhatBox_WhenFullScreen";

            if (rkey.GetValue(valName) == null)
            {
                rkey.SetValue(valName, Application.ExecutablePath.ToString());

                this.ToolStripMenuItem_startupSet.Text = "Windows 시작시 실행하지않게";

                MessageBox.Show("시작프로그램으로 등록되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                rkey.DeleteValue(valName, false);

                this.ToolStripMenuItem_startupSet.Text = "Windows 시작시 실행하게";

                MessageBox.Show("시작프로그램에서 제거되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /*---------------------------------------------------------------------------------*/

        private void HideForm()
        {
            this.notifyIcon_tray.Visible = true;
            this.Hide();
        }

        private void ShowForm()
        {
            this.notifyIcon_tray.Visible = false;
            this.Show();
            if (this.WindowState != FormWindowState.Normal)
                this.WindowState = FormWindowState.Normal;
        }

        /*---------------------------------------------------------------------------------*/

        private void WhenEnterFullscreen()
        {
            foreach (var item in this.listBox_program.Items)
            {
                var processes = Process.GetProcessesByName(item.ToString());

                if (processes.Length > 0)
                {
                    m_killedProgramList.Add(new ProcessInfo(processes[0].MainModule.FileName,
                        processes[0].ProcessName));

                    foreach (Process proc in processes)
                    {
                        proc.Kill();
                    }
                }
            }
        }

        private void WhenLeaveFullscreen()
        {
            foreach (ProcessInfo proc in m_killedProgramList)
            {
                if (Process.GetProcessesByName(proc.processName).Length <= 0)
                {
                    Process.Start(proc.fileName);
                }
            }

            m_killedProgramList.Clear();
        }

        /*---------------------------------------------------------------------------------*/

        public void ReceiveString(string str)
        {
            // 중복검사
            foreach (var item in this.listBox_program.Items)
            {
                if (item.ToString() == str)
                {
                    MessageBox.Show("중복된 이름입니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            // 중복이 아니므로 추가
            this.listBox_program.Items.Add(str);

            // 목록 변경 알림
            m_bProcessListChanged = true;
        }
    }
}

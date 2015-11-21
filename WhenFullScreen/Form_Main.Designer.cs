namespace WhenFullScreen
{
    partial class Form_Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.button_hide = new System.Windows.Forms.Button();
            this.listBox_program = new System.Windows.Forms.ListBox();
            this.contextMenuStrip_programList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_addProc = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_removeProc = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon_tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_tray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_open = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_option = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.timer_update = new System.Windows.Forms.Timer(this.components);
            this.ToolStripMenuItem_startupSet = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_programList.SuspendLayout();
            this.contextMenuStrip_tray.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_hide
            // 
            this.button_hide.Location = new System.Drawing.Point(12, 155);
            this.button_hide.Name = "button_hide";
            this.button_hide.Size = new System.Drawing.Size(305, 41);
            this.button_hide.TabIndex = 0;
            this.button_hide.Text = "숨기기";
            this.button_hide.UseVisualStyleBackColor = true;
            this.button_hide.Click += new System.EventHandler(this.button_hide_Click);
            // 
            // listBox_program
            // 
            this.listBox_program.ContextMenuStrip = this.contextMenuStrip_programList;
            this.listBox_program.FormattingEnabled = true;
            this.listBox_program.HorizontalScrollbar = true;
            this.listBox_program.ItemHeight = 15;
            this.listBox_program.Location = new System.Drawing.Point(12, 12);
            this.listBox_program.Name = "listBox_program";
            this.listBox_program.Size = new System.Drawing.Size(305, 139);
            this.listBox_program.TabIndex = 1;
            // 
            // contextMenuStrip_programList
            // 
            this.contextMenuStrip_programList.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_programList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_addProc,
            this.ToolStripMenuItem_removeProc});
            this.contextMenuStrip_programList.Name = "contextMenuStrip_programList";
            this.contextMenuStrip_programList.Size = new System.Drawing.Size(115, 56);
            this.contextMenuStrip_programList.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_programList_Opening);
            // 
            // ToolStripMenuItem_addProc
            // 
            this.ToolStripMenuItem_addProc.Name = "ToolStripMenuItem_addProc";
            this.ToolStripMenuItem_addProc.Size = new System.Drawing.Size(114, 26);
            this.ToolStripMenuItem_addProc.Text = "추가";
            this.ToolStripMenuItem_addProc.Click += new System.EventHandler(this.ToolStripMenuItem_addProc_Click);
            // 
            // ToolStripMenuItem_removeProc
            // 
            this.ToolStripMenuItem_removeProc.Name = "ToolStripMenuItem_removeProc";
            this.ToolStripMenuItem_removeProc.Size = new System.Drawing.Size(114, 26);
            this.ToolStripMenuItem_removeProc.Text = "삭제";
            this.ToolStripMenuItem_removeProc.Click += new System.EventHandler(this.ToolStripMenuItem_removeProc_Click);
            // 
            // notifyIcon_tray
            // 
            this.notifyIcon_tray.ContextMenuStrip = this.contextMenuStrip_tray;
            this.notifyIcon_tray.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_tray.Icon")));
            this.notifyIcon_tray.Text = "When FullScreen";
            this.notifyIcon_tray.DoubleClick += new System.EventHandler(this.notifyIcon_tray_DoubleClick);
            // 
            // contextMenuStrip_tray
            // 
            this.contextMenuStrip_tray.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip_tray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_open,
            this.ToolStripMenuItem_option,
            this.ToolStripMenuItem_exit});
            this.contextMenuStrip_tray.Name = "contextMenuStrip_tray";
            this.contextMenuStrip_tray.Size = new System.Drawing.Size(115, 82);
            // 
            // ToolStripMenuItem_open
            // 
            this.ToolStripMenuItem_open.Name = "ToolStripMenuItem_open";
            this.ToolStripMenuItem_open.Size = new System.Drawing.Size(114, 26);
            this.ToolStripMenuItem_open.Text = "열기";
            this.ToolStripMenuItem_open.Click += new System.EventHandler(this.ToolStripMenuItem_open_Click);
            // 
            // ToolStripMenuItem_option
            // 
            this.ToolStripMenuItem_option.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_startupSet});
            this.ToolStripMenuItem_option.Name = "ToolStripMenuItem_option";
            this.ToolStripMenuItem_option.Size = new System.Drawing.Size(114, 26);
            this.ToolStripMenuItem_option.Text = "설정";
            // 
            // ToolStripMenuItem_exit
            // 
            this.ToolStripMenuItem_exit.Name = "ToolStripMenuItem_exit";
            this.ToolStripMenuItem_exit.Size = new System.Drawing.Size(114, 26);
            this.ToolStripMenuItem_exit.Text = "종료";
            this.ToolStripMenuItem_exit.Click += new System.EventHandler(this.ToolStripMenuItem_exit_Click);
            // 
            // timer_update
            // 
            this.timer_update.Interval = 3000;
            this.timer_update.Tick += new System.EventHandler(this.timer_update_Tick);
            // 
            // ToolStripMenuItem_startupSet
            // 
            this.ToolStripMenuItem_startupSet.Name = "ToolStripMenuItem_startupSet";
            this.ToolStripMenuItem_startupSet.Size = new System.Drawing.Size(231, 26);
            this.ToolStripMenuItem_startupSet.Text = "Windows 시작시 실행";
            this.ToolStripMenuItem_startupSet.Click += new System.EventHandler(this.ToolStripMenuItem_startupSet_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 208);
            this.Controls.Add(this.listBox_program);
            this.Controls.Add(this.button_hide);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_Main";
            this.Text = "When FullScreen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Main_FormClosed);
            this.Load += new System.EventHandler(this.Form_Main_Load);
            this.contextMenuStrip_programList.ResumeLayout(false);
            this.contextMenuStrip_tray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_hide;
        private System.Windows.Forms.ListBox listBox_program;
        private System.Windows.Forms.NotifyIcon notifyIcon_tray;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_tray;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_open;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_exit;
        private System.Windows.Forms.Timer timer_update;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_programList;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_addProc;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_removeProc;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_option;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_startupSet;
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhenFullScreen
{
    public partial class Form_InputProcess : Form
    {
        public Form_InputProcess(IStringReceiver strReceiver = null)
        {
            InitializeComponent();

            m_strReceiver = strReceiver;
        }

        /*---------------------------------------------------------------------------------*/

        IStringReceiver m_strReceiver;

        /*---------------------------------------------------------------------------------*/

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            if (this.textBox_processName.Text.Length > 0)
            {
                if(m_strReceiver != null)
                    m_strReceiver.ReceiveString(this.textBox_processName.Text);

                this.Close();
            }
            else
            {
                MessageBox.Show("프로세스 이름을 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                this.textBox_processName.Focus();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreManagement
{
    /// <summary>
    /// 信息提示
    /// </summary>
    public partial class MessUI : Form
    {
        public MessUI()
        {
            InitializeComponent();
        }
        private string strMessage = "";

        public string StrMessage
        {
            get { return strMessage; }
            set { strMessage = value; }
        }

        private void Mess_Load(object sender, EventArgs e)
        {
            this.Mess.Text = StrMessage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

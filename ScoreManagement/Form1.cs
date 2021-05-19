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
    // 登陆窗体
    public partial class Form1 : Form
    {
        private string UserNameNotes = "请输入账号";
        private string PwdNotes = "请输入密码";

        //登陆主画面
        public Form1()
        {
            InitializeComponent();
        }

        //忘记密码按钮
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ResetPass rp = new ResetPass();
            rp.Show();
            this.Hide();
        }

        //登陆按钮事件
        private void button1_Click(object sender, EventArgs e)
        {
            CheckLoginStatus();
        }

        private void userpwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)//回车进行check
            {
                CheckLoginStatus();
            }
            
        }

        private void CheckLoginStatus()
        {
            #region 校验参数

            if (string.IsNullOrWhiteSpace(this.username.Text))
            {
                MessageBox.Show("请输入用户名！");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.userpwd.Text))
            {
                MessageBox.Show("请输入密码！");
                return;
            }

            #endregion

            var sql = $"select * from AccountInfo where UserNo = {this.username.Text} and Password = {this.userpwd.Text}";
            DbHandle db = new DbHandle();
            var res = db.GetLoginDataBySql(sql);
            if (res.Tables[0]?.Rows == null || res.Tables[0].Rows.Count == 0)
            {
                //登陆失败
                MessUI messUI = new MessUI();
                messUI.StrMessage = "账号或者密码错误！";
                messUI.ShowDialog();

            }
            else
            {
                //登陆成功，进入主窗口
                MasterUI masterUI = new MasterUI();
                masterUI.Show();
                this.Dispose(false);
            }
        }

        #region Placeholder Demo
        private void username_Leave(object sender, EventArgs e)
        {
            //  退出失去焦点，重新显示
            if (string.IsNullOrEmpty(this.username.Text))
            {
                this.username.ForeColor = Color.DarkGray;
                this.username.Text = UserNameNotes;
            }
        }

        private void username_Enter(object sender, EventArgs e)
        {
            //  进入获得焦点，清空
            if (this.username.Text == UserNameNotes)
            {
                this.username.ForeColor = Color.Black;
                this.username.Text = "";
            }
        }
        #endregion


    }
}

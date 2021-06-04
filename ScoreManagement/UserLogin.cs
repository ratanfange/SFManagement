using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreManagement
{
    // 登陆窗体
    public partial class UserLogin : Form
    {
        private string UserNameNotes = "请输入账号";
        private string PwdNotes = "请输入密码";

        //登陆主画面
        public UserLogin()
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

            this.backgroundWorker1.RunWorkerAsync();
            ProcessingForm processing = new ProcessingForm(this.backgroundWorker1,1);
            processing.ShowDialog(this);
            processing.Close();

            //CheckLoginStatus();
        }

        private void userpwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)//回车进行check
            {
                this.backgroundWorker1.RunWorkerAsync();
                ProcessingForm processing = new ProcessingForm(this.backgroundWorker1,1);
                processing.ShowDialog(this);
                processing.Close();

                //CheckLoginStatus();
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

            var sql = $"select * from AccountInfo where UserNo = '{this.username.Text}' and Password = '{this.userpwd.Text}'";
            DbHandle db = new DbHandle();
            var res = db.GetDataBySql(sql);
            if (res.Tables[0]?.Rows == null || res.Tables[0].Rows.Count == 0)
            {
                //登陆失败
                MessUI messUI = new MessUI();
                messUI.StrMessage = "账号或者密码错误！";
                messUI.ShowDialog();

            }
            else
            {
                // 登陆成功，进入主窗口
                Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cfa.AppSettings.Settings["UserName"].Value = this.username.Text;
                cfa.AppSettings.Settings["UserNo"].Value = res.Tables[0].Rows[0].ItemArray[0].ToString();
                cfa.AppSettings.Settings["Admin"].Value = res.Tables[0].Rows[0].ItemArray[2].ToString();
                cfa.Save();
                ConfigurationManager.RefreshSection("appSettings");
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

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// 进度条处理逻辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(10);
                
                if (worker.CancellationPending)  // 如果用户取消则跳出处理数据代码 
                {
                    e.Cancel = true;
                    break;
                }

                worker.ReportProgress(i);
            }

            

        }

        /// <summary>
        /// 进度条框完成后执行，可能是异常，取消，成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                //MessageBox.Show("取消！");
            }
            else
            {
                CheckLoginStatus();
                //MessageBox.Show("sucess");
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            this.button1.BackColor = Color.AliceBlue;
        }
    }
}

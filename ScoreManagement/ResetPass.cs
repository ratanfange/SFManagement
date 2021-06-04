using ScoreManagement.Utils;
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
    public partial class ResetPass : Form
    {
        private DbHandle _dbHandle = new DbHandle();

        public ResetPass()
        {
            InitializeComponent();
        }

        //确认修改完成按钮事件
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.IsNull() || this.textBox2.Text.IsNull())
            {
                MessageBox.Show("请输入值");
            }
            else
            {
                if(this.textBox1.Text.HasSpecial() || this.textBox2.Text.HasSpecial())
                {
                    MessageBox.Show("检测出含有特殊字符！");
                    return;
                }

                var sql = $"select * from AccountInfo where UserNo = '{this.textBox1.Text}'";
                var res = _dbHandle.GetDataBySql(sql);

                if (res.Tables[0]?.Rows == null || res.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("未找到该账户信息！");
                }
                else
                {
                    //update
                    _dbHandle.UpdateLoginInfo(this.textBox1.Text, this.textBox2.Text);

                    //修改成功，返回登陆界面
                    UserLogin form1 = new UserLogin();
                    form1.Show();
                    this.Hide();
                }


            }
            
        }
    }
}

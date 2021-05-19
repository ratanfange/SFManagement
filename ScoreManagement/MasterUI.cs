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
    public partial class MasterUI : Form
    {
        public MasterUI()
        {
            InitializeComponent();
        }

        private DbHandle dbHandle = new DbHandle();
        private DataSet dataSet = new DataSet();

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            //Form1 form1 = new Form1();
            //form1.Show();
            //this.Close();
            Application.Exit();
        }

        /// <summary>
        /// 时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
           this.SysTime.Text  = DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒");
        }

        /// <summary>
        /// 新增学生信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            AddUserUI addUserUI = new AddUserUI();
            addUserUI.Show();
            this.Hide();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var sql = @"select UserNo,UserName,UserClass,Tel,CreateDate from UserInfo";
            if (!string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                sql += $" where UserNo Like '%{this.textBox1.Text}%'";
            }
            dataSet = dbHandle.GetUserInfoData(sql);
            if(dataSet == null || dataSet.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("数据为空!");
            }
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "ds";
        }

        /// <summary>
        /// 初始化载入表格数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Master_Load(object sender, EventArgs e)
        {
            var sql = @"select top 100 UserNo,UserName,UserClass,Tel,CreateDate from UserInfo";
            dataSet = dbHandle.GetUserInfoData(sql);

            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "ds";
        }

        /// <summary>
        /// 编辑按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if(-1 == this.dataGridView1.CurrentCell.RowIndex)
            {
                MessageBox.Show("请选择编辑行", "提示");
            }
            else if (this.dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("请选择单行进行编辑", "提示");
            }
            else
            {
                var selectRow = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex];
                AddUserUI addUserUI = new AddUserUI(selectRow.Cells["UserNo"].Value.ToString(), 
                    selectRow.Cells["UserName"].Value.ToString(), selectRow.Cells["UserClass"].Value.ToString(), selectRow.Cells["Tel"].Value.ToString());
                addUserUI.Show();
                this.Hide();
            }
        }

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            var index = this.dataGridView1.CurrentCell.RowIndex;
            if(index == -1)
            {
                MessageBox.Show("请选择要删除的行");
            }

            var dialogResult = MessageBox.Show("确认要删除嘛？", "记事本", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.OK)
            {
                for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
                {
                    dbHandle.DeletUserInfoData(this.dataGridView1.SelectedRows[i].Cells[1].Value.ToString());
                }

                button1_Click(null, null);
                MessageBox.Show("删除成功！");

            }
           
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                button1_Click(null, null);
            }
        }

        /// <summary>
        /// 双击cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button3_Click(sender, null);
        }
    }
}

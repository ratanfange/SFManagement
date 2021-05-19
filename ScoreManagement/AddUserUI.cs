using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScoreManagement
{
    public partial class AddUserUI : Form
    {
        public AddUserUI(string u1 = null,string u2 = null,string u3 = null,string u4 = null)
        {
            InitializeComponent();
            _userNo = u1;
            _userName = u2;
            _userClass = u3;
            _tel = u4;

        }

        private string _userNo = "";
        private string _userName = "";
        private string _userClass = "";
        private string _tel = "";
        private bool isAdd = true;
        private static readonly string HasSpecialChar = "{0}不允许包含特殊字符";

        /// <summary>
        /// 新增更新画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddUserUI_Load(object sender, EventArgs e)
        {
            this.label5.Text = "新增信息";
            if (!string.IsNullOrWhiteSpace(_userNo))
            {
                this.UserNo.ReadOnly = true;
                this.label5.Text = "更新信息";
                isAdd = false;
            }
            this.UserNo.Text = _userNo;
            this.UserName.Text = _userName;
            this.UserClass.Text = _userClass;
            this.Tel.Text = _tel;
        }

        /// <summary>
        /// 新增更新画面-确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //保存
            DbHandle dbHandle = new DbHandle();

            if (isAdd)
            {
                if (IsSpecialChar(this.UserNo.Text))
                {
                    MessageBox.Show(string.Format(HasSpecialChar,"学号"));
                    return;
                }

                var sql = $"select UserNo,UserName,UserClass,Tel from UserInfo where UserNo = {this.UserNo.Text}";
                var res = dbHandle.GetUserInfoData(sql);
                if (res.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("该学号已存在！");
                    return;
                }
            }

            #region 校验参数

            if (!string.IsNullOrWhiteSpace(this.UserName.Text) && IsSpecialChar(this.UserName.Text))
            {
                MessageBox.Show(string.Format(HasSpecialChar, "姓名"));
                return;
            }

            if (!string.IsNullOrWhiteSpace(this.UserClass.Text) && IsSpecialChar(this.UserClass.Text))
            {
                MessageBox.Show(string.Format(HasSpecialChar, "班级"));
                return;
            }

            if (!string.IsNullOrWhiteSpace(this.Tel.Text) && IsSpecialChar(this.Tel.Text))
            {
                MessageBox.Show(string.Format(HasSpecialChar, "电话"));
                return;
            }

            #endregion


            dbHandle.AddOrUpdateUserInfoData(isAdd,new UserModel 
            {
                UserNo = this.UserNo.Text,
                UserName = this.UserName.Text,
                UserClass = this.UserClass.Text,
                Tel = this.Tel.Text

            });

            MasterUI masterUI = new MasterUI();
            masterUI.Show();
            this.Close();

        }

        /// <summary>
        /// 新增更新画面-返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            MasterUI masterUI = new MasterUI();
            masterUI.Show();
            this.Close();
        }

        /// <summary>
        /// 判断特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsSpecialChar(string str)
        {
            Regex regExp = new Regex("[ \\[ \\] \\^ \\-_*×――(^)$%~!＠@＃#$…&%￥—+=<>《》!！??？:：•`·、。，；,.;/\'\"{}（）‘’“”-]");
            if (regExp.IsMatch(str))
            {
                return true;
            }
            return false;
        }
    }
}

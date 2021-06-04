using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreManagement
{
    public class DbHandle
    {
        private string connectionString = ConfigurationManager.AppSettings["connectionstring"];
        private SqlConnection conn = null;

        public DbHandle()
        {
           conn = new SqlConnection(connectionString);
        }

        /// <summary>
        /// 查询登陆信息
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public DataSet GetDataBySql(string sql) 
        {
            conn.Open();

            try
            {
                //SqlDataReader dr = cmd.ExecuteReader();
                //定义一个数据库适配器对象
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);

                //定义一个数据集对象
                DataSet dataSet = new DataSet();
                //将查询结果填充数据集对象，并用一个表的别名"ds"标记
                sda.Fill(dataSet, "ds");

                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();//关闭数据库连接
            }
            
        }

        /// <summary>
        /// 添加更新数据
        /// </summary>
        /// <param name="isAdd"></param>
        /// <param name="userModel"></param>
        public void UpdateLoginInfo(string username, string password)
        {
            string addOrUpdateSql = $"update AccountInfo set Password = N'{password}' " +
                                $" where UserNo = N'{username}'";

            ExecuteSql(addOrUpdateSql);

        }

        /// <summary>
        /// 取得数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetUserInfoData(string sql)
        {
            conn.Open();
            //定义一个数据库适配器对象
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);

            //定义一个数据集对象
            DataSet dataSet = new DataSet();

            //将查询结果填充数据集对象，并用一个表的别名"ds"标记
            sda.Fill(dataSet, "ds");
            conn.Close();

            return dataSet;
        }

        /// <summary>
        /// 添加更新数据
        /// </summary>
        /// <param name="isAdd"></param>
        /// <param name="userModel"></param>
        public void AddOrUpdateUserInfoData(bool isAdd,UserModel userModel)
        {
            string addOrUpdateSql = "";

            if (isAdd)
            {
                //add
                addOrUpdateSql = $"insert into UserInfo(UserNo,UserName,UserClass,Tel) values ('{userModel.UserNo}',N'{userModel.UserName}',N'{userModel.UserClass}','{userModel.Tel}')";

            }
            else
            {
                //update
                addOrUpdateSql = $"update UserInfo set UserName = N'{userModel.UserName}',UserClass = N'{userModel.UserClass}',Tel = '{userModel.Tel}',ModifyDate = '{DateTime.Now.ToString()}' " +
                                $" where UserNo = '{userModel.UserNo}'";

            }

            ExecuteSql(addOrUpdateSql);

        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="userNo"></param>
        public void DeletUserInfoData(string userNo)
        {
            var sql = $"delete from UserInfo where UserNo = '{userNo}'";
            ExecuteSql(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        private void ExecuteSql(string sql)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }
}

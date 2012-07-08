using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
/// <summary>
/// 数据库操作的基本类，主要用来服务于DAO
/// author 张浩春
/// time 201-3-24
/// </summary>
/// 
namespace DAO
{
    public class DatabaseAccess
    {

        private SqlConnection connection;//数据库链接

        /// <summary>
        /// 构造方法，在创建对象时就连接数据库
        /// </summary>
        public DatabaseAccess()
        {
            ReadXml.init();//初始化数据库链接路径
            String url = ReadXml.Connection;
            try
            {
                connection = new SqlConnection(url);
                connection.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); 
            }

        }

        /// <summary>
        /// 执行数据库的更新、插入、删除
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public Boolean executeUpdate(String sql)
        {
            SqlCommand com = new SqlCommand(sql, connection);
            int res = -1;
            try
            {
                res = com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); 
            }
            return res > 0 ? true : false;
        }

        /// <summary>
        /// 数据库的查询并返回结果
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public SqlDataReader executeQuery(String sql)
        {
            SqlCommand com = new SqlCommand(sql, connection);
            SqlDataReader result = null;
            try
            {
                result = com.ExecuteReader();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); 
            }
            return result;
        }

        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        public void close()
        {
            connection.Close();
        }

    }
}
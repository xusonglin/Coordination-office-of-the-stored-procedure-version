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
using System.Data.Sql;
using System.Data.SqlTypes;
using DAO;

/// <summary>
///DataBase 数据库操作
/// </summary>
public class DataBase
{

     #region 字段申明
    //数据库的连接
    private SqlConnection conn = null;
    #endregion
    //初始化

	public DataBase()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    
    /// <summary>
    /// 数据库的连接操作
    /// </summary>
    public void DateBaseConnection()
    {
        //数据库的连接路径
       
        String SQLDriver =ReadXml.Connection;
        try
        {
            //连接数据库
            conn = new SqlConnection(SQLDriver);
            //打开连接
            conn.Open();
        }
        catch (Exception e)
        {
            //输出异常
            throw new Exception(e.ToString());
        }
    }

    /// <summary>
    /// 关闭数据库的连接
    /// </summary>
    public void DateBaseClose()
    {
        //关闭连接
        conn.Close();
    }

    /// <summary>
    /// 数据库的增，删，改 操作。返回boolean 值
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public Boolean DateExecuteSQL(String sql)
    {
        Boolean b = false;
        try
        {
            //打开数据库连接
            DateBaseConnection();
            //执行数据库的操作
            SqlCommand cmd = new SqlCommand(sql, conn);
            //数据库的操作
            cmd.ExecuteNonQuery();
            b = true;
            //关闭数据库连接
            DateBaseClose();
        }
        catch (Exception e)
        {
            //输出异常
            throw new Exception(e.ToString());
        }
        return b;
    }

    /// <summary>
    /// 数据库的查询操作，返回数据集合
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public DataSet DateQuerySQL(String sql)
    {
        //打开数据库连接
        DateBaseConnection();
        //数据在内存中的缓存
        DataSet set = new DataSet();
        //数据库的执行操作
        SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
        //存放查询的返回结果集
        adapter.Fill(set);
        //关闭数据库连接
        DateBaseClose();
        //返回结果集
        return set;
    }

    /// <summary>
    /// 查询符合sql语句条件的条数
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public int countNum(String sql)
    {
        //打开数据库连接
        DateBaseConnection();
        //数据库的执行操作
        //SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

        SqlCommand com = new SqlCommand(sql, conn);
        SqlDataReader dr = com.ExecuteReader();
        //计算sql的查询结果条数
        int num = Convert.ToInt32(dr.Read());
        //关闭数据库连接
        DateBaseClose();
        return num;
    }
}
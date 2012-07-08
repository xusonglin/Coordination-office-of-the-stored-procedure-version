using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using JiaoShiXinXiTongJi;

using Maticsoft.DBUtility;

namespace JiaoShiXinXiTongJi.Task
{
    public partial class TaskDesing : BasePage
    {
        #region 定义变量
        protected string strtable;
        protected string usernam;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlParameter[] parameters= { new SqlParameter("@t", SqlDbType.Int) };
            parameters[0].Value = 1;
            DataSet ds = DbHelperSQL.RunProcedure("sp_sysobjectsSelect_name", parameters, "ds");
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                strtable += ds.Tables[0].Rows[0]["name"].ToString() + ",";   //避免重复表名的出现
            }//优化。需要把strtable窜的最后一个字符","去掉。如strtable = strtable.Substring(0, strtable.Length - 1);//去掉最后一个的","
            usernam = Session["ZGXM"].ToString();

            Common common = new Common();
            SqlConnection con = common.coon();

            if (Page.IsPostBack)
            {
                string userid = Session["UserID"].ToString();
                string username = Session["ZGXM"].ToString();
                DateTime strTime = System.DateTime.Now;
                con.Open();
                if (Request.Form["list"] != null && Request.Form["list_text"] != null)
                {
                    string list_create = "";
                    string list = Request.Form["list"];
                    string list_textt = Request.Form["list_text"];
                    if (list.Length == 0 || list_textt.Length == 0)
                    {
                        Response.Write("<script>alert('任务创建失败！请重试！')</script>");
                        Response.Write("<script>document.location=document.location;</script>");

                    }

                    else
                    {
                        #region 将标题和字段信息插入到固定表中
                        string newid = "";
                        string listt = list.Substring(1, list.Length - 1);
                        string[] arr = list.Split(',');
                        SqlParameter[] parametersnewid = { new SqlParameter("@t", SqlDbType.Int) };
                        parametersnewid[0].Value = 1;
                        DataSet dt = DbHelperSQL.RunProcedure("sp_newidSelect", parametersnewid, "dt");
                        newid = dt.Tables[0].Rows[0]["id"].ToString();//获取newid

                        #region 定义参数
                        int rowsAffected;
                        SqlParameter[] parametersinsert = {
                        new SqlParameter("@taskid", SqlDbType.NVarChar,4000),
                        new SqlParameter("@titlename", SqlDbType.NVarChar,4000),
                        new SqlParameter("@titlefontfamily", SqlDbType.NVarChar,4000),
                        new SqlParameter("@titlefontsize", SqlDbType.NVarChar,4000),
                        new SqlParameter("@titlefontcolor", SqlDbType.NVarChar,4000),
                        new SqlParameter("@isfold", SqlDbType.NVarChar,4000)
                        };
                        parametersinsert[0].Value = newid;
                        parametersinsert[1].Value = arr[0];
                        parametersinsert[2].Value = arr[1];
                        parametersinsert[3].Value = arr[2];
                        parametersinsert[4].Value = arr[3];
                        parametersinsert[5].Value = arr[4];
                        #endregion
                        DbHelperSQL.RunProcedure("sp_BapTitleInsert_Task", parametersinsert, out rowsAffected);
                        string TableName = arr[0];
                        string list_text = list_textt.Substring(0, list_textt.Length - 1);
                        string[] arr_text = list_text.Split('_');
                        for (int i = 0; i < arr_text.Length; i++)
                        {
                            string[] arra_text = arr_text[i].Split(',');
                            #region 定义参数
                            int rowsAffected1;
                            SqlParameter[] parametersinsert1 = {
                            new SqlParameter("@taskid", SqlDbType.NVarChar,4000),
                            new SqlParameter("@textname", SqlDbType.NVarChar,4000),
                            new SqlParameter("@textfontfamily", SqlDbType.NVarChar,4000),
                            new SqlParameter("@textfontsize", SqlDbType.NVarChar,4000),
                            new SqlParameter("@textfontcolor", SqlDbType.NVarChar,4000),
                            new SqlParameter("@type", SqlDbType.NVarChar,4000)
                            };
                            parametersinsert1[0].Value = newid;
                            parametersinsert1[1].Value = arra_text[0];
                            parametersinsert1[2].Value = arra_text[1];
                            parametersinsert1[3].Value = arra_text[2];
                            parametersinsert1[4].Value = arra_text[3];
                            parametersinsert1[5].Value = arra_text[4];
                            #endregion
                            DbHelperSQL.RunProcedure("sp_BapTextInsert_Task", parametersinsert1, out rowsAffected1);
                            list_create += arra_text[0] + ",";

                        }

                        #endregion
                        #region 创建临时数据库表
                        //创建表

                        #region 定义参数
                        int rowsAffected2;
                        SqlParameter[] parametersinsert2 = {
                        new SqlParameter("@taskid", SqlDbType.UniqueIdentifier),
                        new SqlParameter("@userid", SqlDbType.UniqueIdentifier),
                        new SqlParameter("@taskname", SqlDbType.NVarChar,2000),
                        new SqlParameter("@taskmaker", SqlDbType.NVarChar,2000),
                        new SqlParameter("@datetime", SqlDbType.DateTime),
                        new SqlParameter("@state", SqlDbType.NVarChar,2000)
                        };
                        Guid newid1 = new Guid(newid);
                        Guid userid1 = new Guid(userid);
                        parametersinsert2[0].Value = newid1;
                        parametersinsert2[1].Value = userid1;
                        parametersinsert2[2].Value = TableName;
                        parametersinsert2[3].Value = username;
                        parametersinsert2[4].Value = strTime;
                        parametersinsert2[5].Value = "未发布";
                        #endregion
                        DbHelperSQL.RunProcedure("sp_BapTaskInsert_Task", parametersinsert2, out rowsAffected2);


                        #region 定义参数
                        int rowsAffected3;
                        SqlParameter[] parametersinsert3 = {
                        new SqlParameter("@tasknameandmaker", SqlDbType.NVarChar,4000)
                        };
                        parametersinsert3[0].Value = TableName+"【" + username + "】";
                        int rowsAffected4;
                        SqlParameter[] parametersinsert4 = {
                        new SqlParameter("@tasknameandmaker", SqlDbType.NVarChar,4000)
                        };
                        parametersinsert4[0].Value = TableName + "【" + username + "】";
                        #endregion
                        DataSet dr = DbHelperSQL.RunProcedure("sp_IsUserTable", parametersinsert3, "dr");
                        if (dr.Tables[0].Rows[0]["IsTable"].ToString() == "1")
                        {
                            DbHelperSQL.RunProcedure("sp_IsExists_Drop", parametersinsert4, out rowsAffected4);
                        }
                        #region 省略
                        //using (SqlConnection coon = common.coon())
                        //{
                        //    string li = list_create;
                        //    string lis = list_create.Substring(0, list_create.Length - 1);
                        //    string[] list1 = lis.Split(',');
                        //    string strlist = "";
                        //    for (int i = 0; i < list1.Length; i++)
                        //    {
                        //        strlist += (list1[i] + " " + "varchar(50)") + ",";
                        //        string strlist1 = list1[0];
                        //    }
                        //    string cmdText = @" create table [" + TableName + "【" + username + "】](UserID UNIQUEIDENTIFIER, " + strlist + " px int NOT NULL  IDENTITY(1,1) , )   ";//2012-1-7增加UserID处理信息填写时修改
                        //    coon.Open();
                        //    SqlCommand coom = new SqlCommand(cmdText, coon);
                        //    coom.ExecuteScalar();
                        //    coon.Close();

                        //    Response.Write("<script>alert('创建数据表成功！')</script>");
                        //    Response.Write("<script>document.location=document.location;</script>");

                        //}
                        #endregion
               
                        string li = list_create;
                        string lis = list_create.Substring(0, list_create.Length - 1);
                        string[] list1 = lis.Split(',');
                        string strlist = "";
                        for (int i = 0; i < list1.Length; i++)
                        {
                            strlist += (list1[i] + " " + "varchar(50)") + ",";
                            string strlist1 = list1[0];
                        }
                        #region 定义参数
                        int rowsAffected5;
                        SqlParameter[] parametersinsert5 = {
                        new SqlParameter("@tasknameandmaker", SqlDbType.NVarChar,500),
                        new SqlParameter("@strlist", SqlDbType.NVarChar,3500)
                        };
                        parametersinsert5[0].Value = TableName + "【" + username + "】";
                        parametersinsert5[1].Value = strlist;
                        #endregion
                        DbHelperSQL.RunProcedure("sp_NoExists_Create", parametersinsert5, out rowsAffected5);

                        Response.Write("<script>alert('创建数据表成功！')</script>");
                        Response.Write("<script>document.location=document.location;</script>");

                    
                        #endregion

                    }
                }

            }
        }

    }
}

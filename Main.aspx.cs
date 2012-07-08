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


namespace JiaoShiXinXiTongJi
{
    public partial class _Default : BasePage
    {
        #region 定义变量
        protected string role;
        protected string functionids;
        protected string Funs;
        protected string sysFuns;
        protected string sysFunsname;
        protected string userid;
        protected string username = "";
        protected string taskcount = "";
        #endregion
     protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            labRealName.Text = Session["ZGXM"].ToString();
            username = Session["ZGXM"].ToString();


            int rowsAffected;
            string userid1 = Session["UserID"].ToString();
            Guid userid = new Guid(userid1);
            SqlParameter[] parameters = { new SqlParameter("@userid", SqlDbType.UniqueIdentifier) };
            parameters[0].Value = userid;
            int result = DbHelperSQL.RunProcedure("sp_BapTaskUserSelect_Count", parameters, out rowsAffected);
            #region
            //string selcount = "SELECT COUNT(*) FROM dbo.Bap_Task_User WHERE UserID='" + userid + "' AND State='未完成'";
            // taskcount = DbHelperSQL.GetSingle(selcount) + "";
            //if (taskcount == "")
            //{
            //    taskcount = "0";
            //}
            #endregion
            taskcount = result.ToString();


            #region 取得角色权限
            string role1 = Session["Role"].ToString();
            Guid role= new Guid(role1);
            SqlParameter[] parametersrole = { new SqlParameter("@id", SqlDbType.UniqueIdentifier) };
            parametersrole[0].Value = role;
            DataSet ds = DbHelperSQL.RunProcedure("sp_BapRoleSelect_FunctionIDs", parametersrole, "ds");
            if (ds.Tables[0].Rows.Count == 0)
            {
                functionids = "";
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                functionids = ds.Tables[0].Rows[0]["FunctionIDs"].ToString();
            }
            //string sel = "select FunctionIDs from bap_role where id='" + role + "'";
            //functionids = DbHelperSQL.GetSingle(sel).ToString();

            string[] arrids = functionids.Split(',');
            for (int i = 0; i < arrids.Length; i++)
            {
                string Fun = "";
                string functionid = arrids[i];
                Guid functionid1 = new Guid(functionid);
                SqlParameter[] parametersfunctionid = { new SqlParameter("@functionid", SqlDbType.UniqueIdentifier) };
                parametersfunctionid[0].Value = functionid1;
                DataSet dt = DbHelperSQL.RunProcedure("sp_BapFunctionSelect_UrlandParentID", parametersfunctionid, "dt");
                string FunctionName = dt.Tables[0].Rows[0]["FunctionName"].ToString();
                string Url = dt.Tables[0].Rows[0]["Url"].ToString();
                string ParentID = dt.Tables[0].Rows[0]["ParentID"].ToString().ToUpper();
                Fun = FunctionName + "%#" + Url + "%#" + ParentID;
                #region
                //string selfunction = "select * from bap_function where functionid='" + functionid + "' order by px";
                //SqlDataReader drr = DbHelperSQL.ExecuteReader(selfunction);
                //while (drr.Read())
                //{
                //    string FunctionName = drr["FunctionName"].ToString();
                //    string Url = drr["Url"].ToString();
                //    string ParentID = drr["ParentID"].ToString().ToUpper();
                //    Fun = FunctionName + "%#" + Url + "%#" + ParentID;
                //}
                #endregion
                Funs += Fun + "&#";
            }
            #endregion
            #region 取得功能
            //Common common = new Common();
            //SqlConnection con = common.coon();
            //con.Open();
            //string sysfuncion = "select * from bap_function where ParentID='00000000-0000-0000-0000-000000000000'";
            // SqlDataAdapter da_text = new SqlDataAdapter(sysfuncion, con);
            //DataSet ds_text = new DataSet();
            //da_text.Fill(ds_text);
            //string[] arr_text = new string[100];
            //for (int j = 0; j < ds_text.Tables[0].Rows.Count; j++)
            //{
            //    sysFuns += ds_text.Tables[0].Rows[j][0].ToString().ToUpper() + ","; //拼接 FunctionID名称
            //    sysFunsname += ds_text.Tables[0].Rows[j][1].ToString() + ","; //拼接 FunctionID名称
            //}
            //sysFuns = sysFuns.Substring(0, sysFuns.Length - 1);//去掉最后一个的","
            //sysFunsname = sysFunsname.Substring(0, sysFunsname.Length - 1);
            //con.Close();
            #endregion
            SqlParameter[] parametersfunctionid1 = { new SqlParameter("@t", SqlDbType.Int) };
            parametersfunctionid1[0].Value = 1;
            DataSet ds_text = DbHelperSQL.RunProcedure("sp_BapFunctionSelect_Admin", parametersfunctionid1, "ds_text");
            string[] arr_text = new string[100];
            for (int j = 0; j < ds_text.Tables[0].Rows.Count; j++)
            {
                sysFuns += ds_text.Tables[0].Rows[j][0].ToString().ToUpper() + ","; //拼接 FunctionID名称
                sysFunsname += ds_text.Tables[0].Rows[j][1].ToString() + ","; //拼接 FunctionID名称
            }
            sysFuns = sysFuns.Substring(0, sysFuns.Length - 1);//去掉最后一个的","
            sysFunsname = sysFunsname.Substring(0, sysFunsname.Length - 1);   
        }

         if (Page.IsPostBack)
         {
         Session.Clear();
         Session.Abandon();
         Response.Redirect("~/logon.aspx");
         
         }
    }
    }
}

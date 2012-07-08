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
    public partial class ViewTask : BasePage
    {
        #region 定义变量
        protected string str;
        protected string str_text;
        protected string arr_text_first;
        protected string testlist = "";
        protected string TaskID = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            TaskID = Request.QueryString["TaskID"].ToString();
            string UserName = Session["ZGXM"].ToString();
            string select = "select TaskName from bap_task where TaskID='" + TaskID + "'";
            string TaskName = DbHelperSQL.GetSingle(select).ToString();
            string TableName = TaskName + "【" + UserName + "】";
            if (!Page.IsPostBack)
            {
                InitData(TableName);
            }
        }

        private void InitData(string TableName)
        {
            #region 取得任务的字段名称
            Common common = new Common();
            SqlConnection con = common.coon();
            con.Open();
            string sel_text = "select * from Bap_Text where TaskID='" + TaskID + "'";  //从表Bap_Text中选取 字段属性，用来在Excel中显示列
            SqlDataAdapter da_text = new SqlDataAdapter(sel_text, con);
            DataSet ds_text = new DataSet();
            da_text.Fill(ds_text);
            string[] arr_text = new string[100];
            for (int j = 0; j < ds_text.Tables[0].Rows.Count; j++)
            {
                for (int i = 1; i < ds_text.Tables[0].Columns.Count; i++)
                {
                    arr_text[j] += ds_text.Tables[0].Rows[j][i].ToString() + ",";
                }
                arr_text_first += ds_text.Tables[0].Rows[j][2].ToString() + ",";
                str_text += arr_text[j] + "_";                 //所有行的加到一起
            }
            con.Close();
            string li_text = str_text.Substring(0, str_text.Length - 1);
            string[] arr_textt = li_text.Split('_');
            string title_text = "";
            string title_te = "";
            for (int t = 0; t < arr_textt.Length; t++)
            {
                string[] arr_text_ = arr_textt[t].Split(',');
                string[] arr_length = new string[100];
                for (int k = 0; k < arr_text_.Length; k++)
                {
                    title_te = arr_text_[0];   // 字段的名称
                }
                title_text += title_te + ",";
            }
            #endregion
            #region 绑定控件
            testlist = title_text;
            testlist = testlist.Substring(0, testlist.Length - 1);
            con.Open();
            string select = "select "+testlist+" from [" + TableName + "]";
            SqlDataAdapter mydata = new SqlDataAdapter(select, con);
            DataSet myds = new DataSet();
            mydata.Fill(myds, TableName);
            viewtaskID.DataSource = myds;
            viewtaskID.DataBind();
            #endregion



        }
    }
}

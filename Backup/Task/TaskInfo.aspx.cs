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
    public partial class TaskInfo : BasePage
    {
        #region 定义变量
        public string strID = "";
        public string strname = "";
        public string desc = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            string TaskID = Request.QueryString["id"].ToString();  // 接收前一个页面传过来的ID,然后把这个任务的信息显示出来
            strID = TaskID;
            string select = " select * from  bap_task where TaskID ='" + TaskID + "'";
            SqlDataReader dr = DbHelperSQL.ExecuteReader(select);
            while (dr.Read())
            {
                title.Text = dr["TaskName"].ToString();
                time.Text = dr["DateTime"].ToString();
                editer.Text = dr["TaskMaker"].ToString();
                Session["TaskMaker"] = editer.Text;
                strname = title.Text;
            }
            string select_ = " select TaskDes from  bap_task_user where TaskID ='" + TaskID + "'";
            if (DbHelperSQL.GetSingle(select_).ToString() != "" && DbHelperSQL.GetSingle(select_).ToString() != null)
            {
                this.content.InnerHtml = DbHelperSQL.GetSingle(select_).ToString();
            }
            else
                this.content.InnerHtml = "无";

        }


    }
}

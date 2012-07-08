using System;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

using System.Web.Security;

using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using JiaoShiXinXiTongJi;
using Maticsoft.DBUtility;


namespace JiaoShiXinXiTongJi.USER
{
    public partial class _Department : BasePage
    { 
        #region 定义变量
        protected string userid = "";
        protected string YxID = "";
        protected string XyID = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            userid = Session["UserID"].ToString();
            YxID = Session["XY"].ToString();
            XyID = Session["XX"].ToString();
          
        }

      
    }
}

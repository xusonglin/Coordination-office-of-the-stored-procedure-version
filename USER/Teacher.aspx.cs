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
    public partial class _Teacher : BasePage
    {

        protected string userid = "";
        protected string XX = "";
        protected string XY = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            userid = Session["UserID"].ToString();
            XY = Session["XY"].ToString();
            XX = Session["XX"].ToString();
         
        }


    }
}


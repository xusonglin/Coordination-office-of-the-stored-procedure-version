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

namespace JiaoShiXinXiTongJi.RoleManager
{
    public partial class Role : BasePage
    {
        protected string userid = "";
        protected string roleid;
        protected string XX = "";
        protected string XY = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                roleid = Session["Role"].ToString();
                userid = Session["UserID"].ToString();
                XY = Session["XY"].ToString();
                XX = Session["XX"].ToString();
            }
           
        }
    }
}

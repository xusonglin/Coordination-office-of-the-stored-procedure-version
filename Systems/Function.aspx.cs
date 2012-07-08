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

namespace JiaoShiXinXiTongJi.Systems
{
    public partial class Function : BasePage
    {
        protected string XX = "";
        protected string XY = "";
        protected string roleid;
        protected void Page_Load(object sender, EventArgs e)
        {
            XY = Session["XY"].ToString();
            XX = Session["XX"].ToString();
            roleid = Session["Role"].ToString();
        }
    }
}

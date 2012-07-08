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

namespace JiaoShiXinXiTongJi.Task
{
    public partial class myTaskYear : BasePage
    {
        public string userid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            userid = Session["UserID"].ToString();
        }
    }
}
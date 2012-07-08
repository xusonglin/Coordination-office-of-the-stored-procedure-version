using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JiaoShiXinXiTongJi.USER
{
    public partial class _School : BasePage
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

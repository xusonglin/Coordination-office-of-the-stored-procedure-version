using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace JiaoShiXinXiTongJi
{
    public class BasePage : System.Web.UI.Page
    {

        public BasePage()
        {
            //   
            //TODO: 在此处添加构造函数逻辑   
            //   
        }
        #region 处理Session 过期
        protected override void OnInit(EventArgs e)
        {
            if (Session["ZGBH"] == null || Session["ZGXM"] == null || Session["UserID"] == null || Session["Role"] == null || Session["XX"] == null || Session["XY"] == null)
            {
               Response.Redirect("~/Error.aspx");
                Response.End();
            }
        }
        #endregion
    }
}

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

namespace JiaoShiXinXiTongJi.treatise
{
    public partial class myTreatise : BasePage
    {
        #region 定义变量

        protected string ZGXM="";
        protected string ZGBH = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            ZGBH = Session["ZGBH"].ToString();
            ZGXM = Session["ZGXM"].ToString();
        }
    }
}

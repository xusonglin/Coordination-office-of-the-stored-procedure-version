using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DAO;
using pojo;

namespace JiaoShiXinXiTongJi.treatise
{
    public partial class onePath : BasePage
    {
        protected string spath = "";//该项目的虚拟路径，形如：http://www.baidu.com
        protected string notice = "";//提示信息
        protected string rid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            spath = HttpUtility.HtmlEncode("http://" + Request.Headers["host"] + Request.ApplicationPath);
            //addHeader(xpathHead);
             rid = Request["Id"];
            notice = Request["notice"];
            if (notice != null)
            {
                Response.Output.Write("<script type='text/javascript'>$.messager.alert('提示','" + notice + "','info');</script>");
            }
            XPathDAO xpathDAO = new XPathDAO();
            Xpath path = xpathDAO.getInfo(new Guid(rid));
            id.Value = path.Id.ToString();
            //websitenameLa.Text = path.Websitename;
            websitename.Text = path.Websitename;
            //weblanguageLa.Text = path.Weblanguage;
            weblanguage.Text = path.Weblanguage;
            baseurl.Text = path.Baseurl;
            baseurlLa.Text = path.Baseurl;
            cntitle.Text = path.Cntitle;
            cntitleLa.Text = path.Cntitle;
            entitle.Text = path.Entitle;
            entitleLa.Text = path.Entitle;
            types.Text = path.Types;
            typesLa.Text = path.Types;
            unit.Text = path.Unit;
            unitLa.Text = path.Unit;
            author.Text = path.Author;
            authorLa.Text = path.Author;
            articletype.Text = path.Articletype;
            articletypeLa.Text = path.Articletype;
            source.Text = path.Source;
            sourceLa.Text = path.Source;
            language.Text = path.Language;
            laguageLa.Text = path.Language;
            journalname.Text = path.Journalname;
            journalnameLa.Text = path.Journalname;
            publishtime.Text = path.Publishtime;
            publishtimeLa.Text = path.Publishtime;
            levels.Text = path.Levels;
            levelsLa.Text = path.Levels;
            recordtype.Text = path.Recordtype;
            recordtypeLa.Text = path.Recordtype;
            forbid.SelectedValue = path.Forbid + "";
         //   forbidLa.Text = path.Forbid == 0 ? "禁用" : "启用";
        }

        /// <summary>
        /// 动态的添加js、css，解决了在head中使用<%%>这样的代码时<被转换为&lt;问题
        /// </summary>
        /// <param name="ctrl"></param>
        private void addHeader(HtmlControl ctrl)
        {
            HtmlLink link = new HtmlLink();
            link.Href = spath + "/themes/default/easyui.css";
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            ctrl.Controls.Add(link);
            HtmlLink link2 = new HtmlLink();
            link2.Href = spath + "/themes/icon.css";
            link2.Attributes.Add("type", "text/css");
            link2.Attributes.Add("rel", "stylesheet");
            ctrl.Controls.Add(link2);
            HtmlLink link3 = new HtmlLink();
            link3.Attributes.Add("type", "text/css");
            link3.Attributes.Add("rel", "stylesheet");
            link3.Href = spath + "/css/xpath.css";
            ctrl.Controls.Add(link3);
            HtmlGenericControl script1 = new HtmlGenericControl();
            script1.TagName = "script";
            script1.Attributes.Add("type", "text/javascript");
            script1.Attributes.Add("src", spath + "/js/jquery/jquery-1.4.4.min.js");
            ctrl.Controls.Add(script1);
            HtmlGenericControl script2 = new HtmlGenericControl();
            script2.TagName = "script";
            script2.Attributes.Add("type", "text/javascript");
            script2.Attributes.Add("src", spath + "/js/jquery/jquery.easyui.min.js");
            ctrl.Controls.Add(script2);
            HtmlGenericControl script3 = new HtmlGenericControl();
            script3.TagName = "script";
            script3.Attributes.Add("type", "text/javascript");
            script3.Attributes.Add("src", spath + "/js/easyui-lang-zh_CN.js");
            ctrl.Controls.Add(script3);
            HtmlGenericControl script4 = new HtmlGenericControl();
            script4.TagName = "script";
            script4.Attributes.Add("type", "text/javascript");
            script4.Attributes.Add("src", spath + "/js/jss/xpath.js");
            ctrl.Controls.Add(script4);
        }
    }
}

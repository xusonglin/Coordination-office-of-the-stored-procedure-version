using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO;
using pojo;

namespace JiaoShiXinXiTongJi.treatise
{
    public partial class oneTreatise : BasePage
    {
        protected string title;
        protected void Page_Load(object sender, EventArgs e)
        {
            string auth = "";
            if (Session != null)
            {
                auth = Session["ZGXM"].ToString();
            }
            string[] divide = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
            string Id = Request["Id"];
            TreatiseDAO treatiseDAO = new TreatiseDAO();
            Treatise treatise = treatiseDAO.details(new Guid(Id));
            title = treatise.Cntitle;
            sourceWebSite.Text = treatise.Websitename;
            sourceWebSite.NavigateUrl = treatise.Baseurl;
            sourceUrl.NavigateUrl = treatise.Articleurl;
            sourceUrl.ToolTip = treatise.Cntitle;
            sourceUrl.Text = treatise.Articleurl;
            cnTitle.Text = treatise.Cntitle;
            enTitle.Text = treatise.Entitle;
            types.Text = treatise.Types;
            author.Text = treatise.Author;
            articleType.Text = treatise.Articletype;
            source.Text = treatise.Source;
            language.Text = treatise.Language;
            journalname.Text = treatise.Journalname;
            publishTime.Text = treatise.Publishtime;
            levels.Text = treatise.Levels;
            recordType.Text = treatise.Recordtype;
            if (treatise.Author != null && treatise.Author.Length > 0)
            {
                string[] auths = treatise.Author.Split('，');
                int pos = getPos(auths, auth);
                if (pos >= 0)
                {
                    divideType.Text = "第" + divide[pos] + "作者";
                }
                else
                {
                    divideType.Text = "无";
                }
            }
        }

        /// <summary>
        /// 获得字符串在数据中的位置
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private int getPos(string[] arr, string text)
        {
            int pos = -1;
            if (arr != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].Equals(text))
                    {
                        pos = i;
                        break;
                    }
                }
            }
            return pos;
        }
    }
}

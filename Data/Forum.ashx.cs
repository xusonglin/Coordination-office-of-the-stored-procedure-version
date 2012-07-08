using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Maticsoft.DBUtility;
using System.Data;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography;// 密码加密用到
using System.IO;// 密码加密用到

namespace JiaoShiXinXiTongJi.Data
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Forum : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string action = context.Request.Params["action"];
            string CountID = context.Request["CountID"];
            string Content = context.Request["Content"];
            string userid = context.Request["userid"];

            string HfID = context.Request["HfID"];
            string strResult = "";
          
            switch (action)
            {
                case "tijao": strResult = tijiao(userid, CountID, Content); break;    //提交回复;
                case "deletehf": strResult = deletehf(HfID); break;    //删除回复;
            }
            context.Response.Write(strResult);
        }

        private string tijiao(string userid, string CountID, string Content)
        {
             //userid = "B0060EF8-7DFB-446A-96C1-B7707B786053";
            DateTime HfDate = DateTime.Now;
            string ins = "insert into bap_forum_hf select newid(),'" + CountID + "','" + Content + "','" + HfDate + "','" + userid + "'";
           
            return DbHelperSQL.ExecuteSql(ins) > 0 ? "true" : "false";
        }

        private string deletehf(string HfID)
        {
          
            string del = "delete from bap_forum_hf where hfid='" + HfID + "'";
            return DbHelperSQL.ExecuteSql(del) > 0 ? "true" : "false";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

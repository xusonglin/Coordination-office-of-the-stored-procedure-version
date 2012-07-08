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
    public class HandlerCom : IHttpHandler
    {
        string type = "";
        string PID = "";
        string userid = "";
        string XX = "";
        string XY = "";
        public void ProcessRequest(HttpContext context)
        {
            type = context.Request["type"];
            PID = context.Request["PID"];
            userid = context.Request["userid"];
            XX = context.Request["XX"];
            XY = context.Request["XY"];

            string result = "";

            result = "[";
            foreach (System.Data.DataRow dr in GetAllData().Rows)
            {

                result += "{\"id\":\"" + dr["ID"].ToString() + "\",\"text\":\"" + dr["name"].ToString() + "\"";
                result += "},";

            }
            result = result.TrimEnd(',');
            result += "]";

            context.Response.Write(result);

        }


        public System.Data.DataTable GetAllData()
        {
            string strSelect = "";
            if (type == "School")
            {
                if (userid == "00000000-0000-0000-0000-000000000000")
                {
                    strSelect = "SELECT ID AS id ,MC AS name, ParentID AS PID FROM bap_school where ParentID='00000000-0000-0000-0000-000000000000'";
                }
                else
                    strSelect = "SELECT ID AS id ,MC AS name, ParentID AS PID FROM bap_school where id in (select XX from bap_user where userid='"+userid+"')";

            }
            else if (type == "Dpt" && PID != "" && PID != null)
            {

                if (userid == "00000000-0000-0000-0000-000000000000"||XY=="00000000-0000-0000-0000-000000000000")
                {

                 strSelect = "SELECT ID AS id ,MC AS name, ParentID AS PID FROM bap_school where ParentID='" + PID + "'";
                
                }
                else
                strSelect = "SELECT ID AS id ,MC AS name, ParentID AS PID FROM bap_school where ParentID='" + PID + "' and  id in (select XY from bap_user where userid='" + userid + "')";

            }
            else
                strSelect = "SELECT ID AS id ,MC AS name, ParentID AS PID FROM bap_school where ParentID<>'00000000-0000-0000-0000-000000000000' and id in (select XY from bap_user where userid='" + userid + "')";


            return DbHelperSQL.Query(strSelect).Tables[0];
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

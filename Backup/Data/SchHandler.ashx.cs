using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Maticsoft.DBUtility;
using System.Text;
using System.Web.Services;

namespace jsxxtjxt
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class SchHandler : IHttpHandler
    {
        private string SchParentId="00000000-0000-0000-0000-000000000000";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            #region 接收参数
            string action = context.Request.Params["action"];
            string strBH = context.Request["BH"];
            string strMC = context.Request["MC"];
            string strDesc = context.Request["Desc"];
            string strID=context.Request["ID"];
            string XXID = context.Request["XXID"];
            string XYID = context.Request["XYID"];
            string search = context.Request["search"];
            string strResult = "false";
            #endregion
            switch (action)
            {
                case "Add": strResult = Add(strBH, strMC, strDesc); break;
                case "Delete": strResult = Delete(strID); break;
                case "Query": strResult = Select(XXID, XYID, search); break;
                case "Edit":strResult=Edit(strBH, strMC, strDesc, strID);break;
            }
            context.Response.Write(strResult);
        }

        private string Edit(string strBH, string strMC, string strDesc, string strID)
        {
            var edit = "update bap_School set BH='" + strBH + "',MC='" + strMC + "',[Desc]='" + strDesc + "' where ID='" + strID + "'";
            return DbHelperSQL.ExecuteSql(edit) > 0 ? "true" : "false";
        }
        private string Add(string strBH, string strMC, string strDesc)
        {
            var add = "insert into bap_School select newid(),'" + strBH + "','" + strMC + "','" + strDesc + "','"+SchParentId+"'";
            return DbHelperSQL.ExecuteSql(add) > 0 ? "true" : "false";
        }
        private string Delete(string strID)
        {
            var delete = "delete from bap_School where ID='" + strID + "'";
            return DbHelperSQL.ExecuteSql(delete) > 0 ? "true" : "false";
        }
        private string Select(string strXXID, string strXYID, string search)
        {
            string select = "";
            if (search != null && search != "")
            {
                if (strXXID == "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000") //系统管理员
                {
                    select = "select * from bap_school where ParentID='00000000-0000-0000-0000-000000000000'and MC like '%"+search+"%' ";

                }
                else if (strXXID != "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000") //学校管理员
                {
                    select = "select * from bap_school where ID='" + strXXID + "' and MC like '%" + search + "%' ";
                }
                else                                                                                                             //系管理员
                {
                    select = "select * from bap_school where ID='" + strXXID + "'and MC like '%" + search + "%' ";
                }
            }

            else
            {


                if (strXXID == "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000") //系统管理员
                {
                    select = "select * from bap_school where ParentID='00000000-0000-0000-0000-000000000000' ";

                }
                else if (strXXID != "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000") //学校管理员
                {
                    select = "select * from bap_school where ID='" + strXXID + "'  ";
                }
                else                                                                                                             //系管理员
                {
                    select = "select * from bap_school where ID='" + strXXID + "'";
                }
            
            
            
            }

            DataTable dt = DbHelperSQL.Query(select).Tables[0];
            return GetJSONString(dt);

        }

        private string GetJSONString(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"total\":");
            jsonBuilder.Append(dt.Rows.Count);
            jsonBuilder.Append(",\"rows\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
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

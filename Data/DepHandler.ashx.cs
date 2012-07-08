using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Maticsoft.DBUtility;
using System.Data;
using System.Text;

namespace jsxxtjxt
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class DepHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            #region 取得参数

            string action = context.Request.Params["action"];
            string ID = context.Request["ID"];   
            string strBH = context.Request["BH"];   
            string strMC = context.Request["MC"];
            string strDesc = context.Request["Desc"];
            string strParentID=context.Request["ParentID"];
            string XXID = context.Request["XXID"];
            string XYID = context.Request["XYID"];
            string search = context.Request["search"];
            #endregion

            string strResult = "false";

            switch (action)
            {
                case "Add": strResult = Add(strBH, strMC, strDesc, strParentID); break;
                case "Delete": strResult = Delete(ID); break;
                case "Query": strResult = Select(XXID,XYID,search); break;
                case "Edite": strResult = Edite(strBH, strMC, strDesc, strParentID, ID); break;
            }
            context.Response.Write(strResult);
        }
        

        private string Add(string strBH, string strMC, string strDesc, string strParentID)
        {
            var add = "insert into bap_School select newid(),'" + strBH + "','" + strMC + "','" + strDesc + "','" + strParentID+"'";
            return DbHelperSQL.ExecuteSql(add) > 0 ? "true" : "false";
        }

        private string Edite(string strBH, string strMC, string strDesc, string strParentID,string ID)
        {
            var edite = "update bap_School set BH='" + strBH + "',MC='" + strMC + "',[Desc]='" + strDesc + "',ParentID='" + strParentID + "' where ID='" + ID + "'";
            return DbHelperSQL.ExecuteSql(edite) > 0 ? "true" : "false";
        }

        private string Delete(string strNum)
        {
            var delete = "delete from Bap_School where ID='" + strNum + "'";
            return DbHelperSQL.ExecuteSql(delete) > 0 ? "true" : "false";
        }

        private string Select(string strXXID, string strXYID,string search)
        {   
            
            string select = "";
            if (search != null && search != "")
            {
                if (strXXID == "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000") //系统管理员
                {
                    select = "select bs.*,bss.MC as pmc from bap_school bs left join bap_school bss on bs.ParentID=bss.ID  where bs.ParentID!='00000000-0000-0000-0000-000000000000' and bs.MC like '%" + search + "%'  ";
                }
                else if (strXXID != "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000") //学校管理员
                {
                    select = "select bs.*,bss.MC as pmc from bap_school bs left join bap_school bss on bs.ParentID=bss.ID  where bs.ParentID='" + strXXID + "' and bs.MC like '%" + search + "%'  ";
                }
                else                                                                                                             //系管理员
                {
                    select = "select bs.*,bss.MC as pmc from bap_school bs left join bap_school bss on bs.ParentID=bss.ID  where bs.ID='" + strXYID + "'and bs.MC like '%" + search + "%'  ";
                }
            }
            else
            {

                if (strXXID == "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000") //系统管理员
                {
                    select = "select bs.*,bss.MC as pmc from bap_school bs left join bap_school bss on bs.ParentID=bss.ID  where bs.ParentID!='00000000-0000-0000-0000-000000000000'  ";
                }
                else if (strXXID != "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000") //学校管理员
                {
                    select = "select bs.*,bss.MC as pmc from bap_school bs left join bap_school bss on bs.ParentID=bss.ID  where bs.ParentID='" + strXXID + "'  ";
                }
                else                                                                                                             //系管理员
                {
                    select = "select bs.*,bss.MC as pmc from bap_school bs left join bap_school bss on bs.ParentID=bss.ID  where bs.ID='" + strXYID + "'";
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
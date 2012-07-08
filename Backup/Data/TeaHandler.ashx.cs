using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Maticsoft.DBUtility;
using System.Data;
using System.Text;
using System.Security.Cryptography;// 密码加密用到
using System.IO;// 密码加密用到
using JiaoShiXinXiTongJi;

namespace JiaoShiXinXiTongJi.Data
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TeaHandler : IHttpHandler
    {


        Common common = new Common();


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            #region 接收参数
            string action = context.Request.Params["action"];
            string strYxID=context.Request["XY"];
            string strXXID = context.Request["XX"];
            string strNum = context.Request["ZGBH"];
            string strName = context.Request["ZGXM"];
            string strPass = context.Request["Pass"];
            string strXb = context.Request["XB"];
            string strBirthday = context.Request["CSRQ"];
            string strAge = context.Request["NL"];
            string strSfzh = context.Request["CID"];
            string strJiguan = context.Request["JG"];
            string strMinzu = context.Request["MZ"];
            string strXy = context.Request["XY"];
            string strYx = context.Request["YX"];
            string strXl = context.Request["XL"];
            string strZw = context.Request["ZW"];
            string strZcjb = context.Request["ZCJB"];
            string strDzjz = context.Request["DZJZ"];
            string strRzsj = context.Request["RZSJ"];
            string strTel = context.Request["Tel"];
            string strEmail = context.Request["Email"];
            string strRole = context.Request["Role"];
            string roleids = context.Request["roleids"];
            string userid = context.Request["userid"];
            string xxID = context.Request["xxID"];
            string xyID = context.Request["xyID"];
            string search = context.Request["search"];
            string ids = context.Request["ids"];
             //获取每页总记录数和当前页数
            int rows = 10;
            int page = 1;
            if (context.Request["rows"] != null && context.Request["rows"] != "")
            {
             rows = Int32.Parse(context.Request["rows"].ToString());
             page = Int32.Parse(context.Request["page"].ToString());
            }
            //获取客户端排序字段名 ,目前实现的客户端排序,故如下的代码注释掉
            //  string sort = context.Request["sort"].ToString();
            string sort = "mc";
            //升序asc还是降序desc
            // string order = context.Request["order"].ToString();
            string order = "desc";
            string strResult = "false";
            #endregion
            switch (action)
            {

                case "Add": strResult = Add(strNum, strName, strPass, xxID, xyID); break;
                case "Edite": strResult = Edite(strNum, strName, strPass, xxID, xyID,userid); break;
                case "Delete": strResult = Delete(ids); break;
                case "Query": strResult = Select(strName, rows, page, sort, order == "asc" ? true : false, strXXID, strYxID,search); break;
                case "Xzyx":strResult=SelectTec(strYxID);break;
                case "UpdateRole": strResult = UpdateRole(strNum, roleids); break;
                case "replace": strResult = replace(strNum); break;
            }
            context.Response.Write(strResult);
        }

        private string SelectTec(string strYxID)
        {
            var select = "";
            select = "select * from Bap_User where YX='" + strYxID + "'";
            DataTable dt = DbHelperSQL.Query(select).Tables[0];
            return GetJSONString(dt);
        }


        private string Add(string zgbh,string zgxm, string Pass, string xxid,string xyid)
        {
        
            if (xyid.ToString() == "0" || xyid.ToString() == "" || xyid.ToString() == null)
                xyid = "00000000-0000-0000-0000-000000000000";

            string passs = common.Encrypting(Pass);
            var add = "insert into bap_user(UserID,ZGBH,ZGXM,Pass,XX,XY,Role) select newid(),'" + zgbh + "','" + zgxm + "','" + passs + "','" + xxid + "','" + xyid + "','C45339F4-36F1-43DB-ACD9-92FC343D4CE7'";
            return DbHelperSQL.ExecuteSql(add) > 0 ? "true" : "false";
        }


        private string Edite(string zgbh, string zgxm, string Pass, string xxid, string xyid,string userid)
        {

            if (xyid.ToString() == "0" || xyid.ToString() == "" | xyid.ToString() == null)
                xyid = "00000000-0000-0000-0000-000000000000";

            string passs = common.Encrypting(Pass);
            var edite = "update bap_user set  ZGBH='" + zgbh + "',ZGXM='" + zgxm + "',Pass='" + passs + "',XX='" + xxid + "',XY='" + xyid + "' where UserID='" + userid + "'";
            return DbHelperSQL.ExecuteSql(edite) > 0 ? "true" : "false";
        }



        private string Delete(string userid)
        {
            var delete = "delete from Bap_User where UserID in (" + userid + ")";
            return DbHelperSQL.ExecuteSql(delete) > 0 ? "true" : "false";
        }
        private string replace(string zgbh)
        {
            var update = "update Bap_User set Pass='On8U4+dy1Rs=' where ZGBH='" + zgbh + "'";
            return DbHelperSQL.ExecuteSql(update) > 0 ? "true" : "false";
        }

        private string UpdateRole(string zgbh,string roleid)
        {
            var update = "update bap_user set Role='" + roleid + "' where ZGBH='" + zgbh + "'";
            return DbHelperSQL.ExecuteSql(update) > 0 ? "true" : "false";
        }
        private string Select(string strMC, int rows, int page, string sortField, bool order, string strXXID, string strXYID, string search)
        {
            var select = "";
            string selTotal = "";
            page = (page - 1) * rows;
            if (search != null && search != "")
            {
                if (strXXID == "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000")
                {

                    select = "SELECT TOP " + rows + " bu.*,br.ROLE AS role_,bs.MC AS XY_,bss.MC AS YX_ from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where bu.ZGXM like '%" + search + "%'and bu.UserID NOT IN (SELECT TOP " + page + " bu.UserID from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where bu.ZGXM like '%" + search + "%'  )";
                    selTotal = "SELECT Count(*) from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where bu.ZGXM like '%" + search + "%' ";
                  
                }
                else if (strXXID != "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000")
                {
                    select = "SELECT TOP " + rows + " bu.*,br.ROLE AS role_,bs.MC AS XY_,bss.MC AS YX_ from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XX='" + strXXID + "' and bu.ZGXM like '%" + search + "%' AND bu.UserID NOT IN (SELECT TOP " + page + " bu.UserID from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XX='" + strXXID + "'and bu.ZGXM like '%" + search + "%' )";
                    selTotal = "SELECT Count(*) from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XX='" + strXXID + "'and bu.ZGXM like '%" + search + "%' ";

                } 

                else
                {
                    select = "SELECT TOP " + rows + " bu.*,br.ROLE AS role_,bs.MC AS XY_,bss.MC AS YX_ from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XY='" + strXYID + "' and bu.ZGXM like '%" + search + "%' AND bu.UserID NOT IN (SELECT TOP " + page + " bu.UserID from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XY='" + strXYID + "' and bu.ZGXM like '%" + search + "%' )";
                    selTotal = "SELECT Count(*) from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XY='" + strXYID + "' and bu.ZGXM like '%" + search + "%' ";

                  
                }
            }


            else
            {

                if (strXXID == "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000")
                {

                    select = "SELECT TOP " + rows + " bu.*,br.ROLE AS role_,bs.MC AS XY_,bss.MC AS YX_ from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where bu.UserID NOT IN (SELECT TOP " + page + " bu.UserID from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID  )";
                    selTotal = "SELECT Count(*) from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID  ";

                }
                else if (strXXID != "00000000-0000-0000-0000-000000000000" && strXYID == "00000000-0000-0000-0000-000000000000")
                {
                    
                    select = "SELECT TOP " + rows + " bu.*,br.ROLE AS role_,bs.MC AS XY_,bss.MC AS YX_ from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XX='" + strXXID + "' AND bu.UserID NOT IN (SELECT TOP " + page + " bu.UserID from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XX='" + strXXID + "' )";
                    selTotal = "SELECT Count(*) from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XX='" + strXXID + "' ";
                  
                }

                else
                {

                    select = "SELECT TOP " + rows + " bu.*,br.ROLE AS role_,bs.MC AS XY_,bss.MC AS YX_ from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XY='" + strXYID + "' AND bu.UserID NOT IN (SELECT TOP " + page + " bu.UserID from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XY='" + strXYID + "' )";
                    selTotal = "SELECT Count(*) from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID where XY='" + strXYID + "' ";
                }
            
            
            
            }



            DataTable dt = DbHelperSQL.Query(select).Tables[0];



             
            int Total = (Convert.ToInt32(DbHelperSQL.GetSingle(selTotal).ToString()));

            return JiaoShiXinXiTongJi.Function.GetJSONString(dt, Total);


        }
        
       

        private string GetJSONString(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"total\":");
            jsonBuilder.Append(dt.Rows.Count);
            jsonBuilder.Append(",\"rows\":[");
            //  jsonBuilder.Append(dt.TableName);
            //   jsonBuilder.Append("\":[");
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

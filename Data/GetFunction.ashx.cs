using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data;
using Maticsoft.DBUtility;

namespace JiaoShiXinXiTongJi.Data
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetFunction : IHttpHandler
    {
        #region 定义变量
        public List<Bap_Function> lists = null;
        private string result = string.Empty;
        private string temp = string.Empty;
        private string strParent = "00000000-0000-0000-0000-000000000000";
        static string XX = "";
        static string XY = "";
        static string roleid = "";
        #endregion
        public void ProcessRequest(HttpContext context)
        {
            #region 取得参数
            context.Response.ContentType = "text/plain";
            XX = context.Request["XX"].ToUpper();
            XY = context.Request["XY"].ToUpper();
            roleid = context.Request["roleid"].ToUpper();
            #endregion
            Bap_Function sh = new Bap_Function();
            lists = sh.GetList();
            if (lists.Count > 0)
            {
                result = "[";
                int count = lists.Where(n => n.ParentID == strParent).ToList().Count;
                foreach (Bap_Function school in lists.Where(n => n.ParentID == strParent))
                {
                    temp = string.Empty;
                    result += "{\"id\":\"" + school.FunctionID + "\",\"text\":\"" + school.FunctionName + "\"";
                    if (lists.Where(n => n.ParentID == school.FunctionID).ToList().Count > 0)
                        result += ",\"children\":" + GetObject(school.FunctionID, strParent);
                    result += "},";
                }
                result = result.TrimEnd(',');
                result += "]";
            }

            context.Response.Write(result);

        }

        private string GetObject(string ParentId, string Array)
        {

            temp += "[";
            foreach (Bap_Function school in lists.Where(n => n.ParentID == ParentId))
            {
                string  rolecheck = "false";
                string FunctionIDs = FunRole(roleid);
                if (FunctionIDs != "" && FunctionIDs != null)
                {
                    if (FunctionIDs.IndexOf(school.FunctionID) != -1)
                    { rolecheck = "true"; }
                }
                temp += "{\"id\":\"" + school.FunctionID + "\",\"checked\":" + rolecheck + ",\"text\":\"" + school.FunctionName + "\"";

                temp += "},";
            }
            temp = temp.TrimEnd(',');
            temp += "]";
            return temp;
        }
        public partial class Bap_Function
        {
            public Bap_Function()
            { }
            public System.Data.DataTable GetAllData()
            {
                string strSelect = "";
                if (XX == "00000000-0000-0000-0000-000000000000" && XY == "00000000-0000-0000-0000-000000000000") //系统管理员
                {
                    strSelect = "select * from Bap_Function ";
                }
                else
                {
                    strSelect = "select * from Bap_Function  where functionid not in ('C9E4B680-4605-41C1-9478-CC8DBB3D70FD','2EC13159-911E-4ACA-A349-56033876D3FF')";
                }
                return DbHelperSQL.Query(strSelect).Tables[0];
            }
            public List<Bap_Function> GetList()
            {
                List<Bap_Function> Entities = new List<Bap_Function>();

                foreach (System.Data.DataRow dr in GetAllData().Rows)
                {
                    Entities.Add(new Bap_Function()
                    {
                        FunctionID = dr["FunctionID"].ToString(),
                        FunctionName = dr["FunctionName"].ToString(),
                        ParentID = dr["ParentID"].ToString()
                    });
                }
                return Entities;
            }
            private string _functionid;
            private string _functionnameh;

            private string _parentid;
            public string FunctionID
            {
                set { _functionid = value; }
                get { return _functionid; }
            }
            public string FunctionName
            {
                set { _functionnameh = value; }
                get { return _functionnameh; }
            }
            public string ParentID
            {
                set { _parentid = value; }
                get { return _parentid; }
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        public string FunRole(string roleid)
        {
            
            string strSelect = "";
              strSelect = "select FunctionIDs from Bap_Role  where ID='"+roleid+"'";
              if (DbHelperSQL.GetSingle(strSelect) != null && DbHelperSQL.GetSingle(strSelect) != "")
              {
                  return DbHelperSQL.GetSingle(strSelect).ToString();
              }
              else
                  return "";
        }


    }
}

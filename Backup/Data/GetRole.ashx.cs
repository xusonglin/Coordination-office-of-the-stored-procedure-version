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
    public class GetRole : IHttpHandler
    {
        #region 定义变量
        public List<Bap_Task> lists = null;
        private string result = string.Empty;
        private string temp = string.Empty;
        private string departmentURL = "Department.aspx";
        static string userid = "";
        static string roleid = "";
        #endregion
        public void ProcessRequest(HttpContext context)
        {
           context.Response.ContentType = "text/plain";
           userid = context.Request["userid"];
           roleid = context.Request["roleid"];
            Bap_Task sh = new Bap_Task();
            lists = sh.GetList();
            if (lists.Count > 0)
            {
                result = "[";
                int count = lists.ToList().Count;
                foreach (Bap_Task task in lists)
                {

                    string rolecheck = "false";
                    if (roleid == task.ID)
                    { rolecheck = "true"; }
                   
                    temp = string.Empty;
                    result += "{\"id\":\"" + task.ID + "\",\"checked\":" + rolecheck + ",\"text\":\"" + task.Role + "\"";
                    result += "},";

                }
                result = result.TrimEnd(',');
                result += "]";
            }
            context.Response.Write(result);
        }

        public partial class Bap_Task
        {

            public Bap_Task()
            { }
            public System.Data.DataTable GetAllData()
            {
                string strSelect = "select * from Bap_Role where UserID='"+userid+"' ";
                return DbHelperSQL.Query(strSelect).Tables[0];
            }
            public List<Bap_Task> GetList()
            {
                List<Bap_Task> Entities = new List<Bap_Task>();

                foreach (System.Data.DataRow dr in GetAllData().Rows)
                {
                    Entities.Add(new Bap_Task()
                    {
                        ID = dr["ID"].ToString(),
                        Role = dr["Role"].ToString()

                    });
                }
                return Entities;
            }
            private string _id;
            private string _role;

            public string ID
            {
                set { _id = value; }
                get { return _id; }
            }
            public string Role
            {
                set { _role = value; }
                get { return _role; }
            }

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
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

    public class GetTask_ : IHttpHandler
    {
        #region 定义变量
        public List<Bap_Task> lists = null;
        private string result = string.Empty;
        private string temp = string.Empty;
        private string departmentURL = "Department.aspx";
        static string userid = "";
        #endregion
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            userid = context.Request["userid"];
            Bap_Task sh = new Bap_Task();
            lists = sh.GetList(); 
            if (lists.Count > 0)
            {
                result = "[";
                int count = lists.ToList().Count;
                foreach (Bap_Task task in lists.Where(n => n.UserID == userid))
                    {
                        temp = string.Empty;
                        result += "{\"id\":\"" + task.TaskID + "\",\"text\":\"" + task.TaskName + "\",\"attributes\":{\"url\":\"" + departmentURL + "\"}";
                        result += "},";

                    }
                    result = result.TrimEnd(',');
                    result += "]";
                context.Response.Write(result);
            }
        }

        public partial class Bap_Task
        {

            public Bap_Task()
            { }
            public System.Data.DataTable GetAllData()
            {   
                string strSelect = "select * from Bap_Task" ;
                return DbHelperSQL.Query(strSelect).Tables[0];
            }
            public List<Bap_Task> GetList()
            {
                List<Bap_Task> Entities = new List<Bap_Task>();

                foreach (System.Data.DataRow dr in GetAllData().Rows)
                {
                    Entities.Add(new Bap_Task()
                    {
                        TaskID = dr["TaskID"].ToString(),
                        TaskName = dr["TaskName"].ToString(),
                        UserID = dr["UserID"].ToString(),
                      
                    });
                }
                return Entities;
            }
            private string _taskid;
            private string _taskname;
            private string _userid;
            public string TaskID
            {
                set { _taskid = value; }
                get { return _taskid; }
            }
            public string UserID
            {
                set { _userid = value; }
                get { return _userid; }
            }

            public string TaskName
            {
                set { _taskname = value; }
                get { return _taskname; }
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
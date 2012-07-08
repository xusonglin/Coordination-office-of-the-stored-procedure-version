using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data;
using Maticsoft.DBUtility;

namespace jsxxtjxt
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetSchool : IHttpHandler
    {
        #region 定义变量
        public List<Bap_School> lists = null;
        public List<Bap_user> users = null;
        private string result = string.Empty;
        private string temp = string.Empty;
        private string strParent = "00000000-0000-0000-0000-000000000000";
        private string schoolURL = "School.aspx";
        private string departmentURL = "Department.aspx";
        static string XX = "";
        static string XY = "";
        #endregion
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            XX = context.Request["XX"].ToLower();
            XY = context.Request["XY"].ToLower();
            Bap_School sh = new Bap_School();
            Bap_user bu = new Bap_user();       
            lists = sh.GetList();
            users = bu.GetList();
            if (lists.Count > 0)
            {
                if (XX == "00000000-0000-0000-0000-000000000000")  //系统管理员
                {
                    result = "[";
                    int count = lists.Where(n => n.ParentID == strParent).ToList().Count;
               
                    foreach (Bap_School school in lists.Where(n => n.ParentID == strParent))
                    {

                        int countt = lists.Where(n => n.ParentID == school.ID).ToList().Count;
                        string state = "open";
                        if (countt > 0)
                        {
                            state = "open";
                        }

                        temp = string.Empty;
                  
                        result += "{\"id\":\"" + school.ID + "\",\"state\":\"" + state + "\",\"text\":\"" + school.MC + "\",\"attributes\":{\"url\":\"" + schoolURL + "\"}";
                        if (lists.Where(n => n.ParentID == school.ID).ToList().Count > 0)
                            result += ",\"children\":" + GetObject(school.ID, strParent);
                        result += "},";
                    }
                    result = result.TrimEnd(',');
                    result += "]";
                }
                else
                {
                    result = "[";
                    int count = lists.Where(n => n.ID == XX).ToList().Count;
             
                    foreach (Bap_School school in lists.Where(n => n.ID == XX))
                    {

                        int countt = lists.Where(n => n.ParentID == school.ID).ToList().Count;
                        string state = "open";
                        if (countt > 0)
                        {
                            state = "open";
                        }
                        temp = string.Empty;
                        result += "{\"id\":\"" + school.ID + "\",\"state\":\"" + state + "\",\"text\":\"" + school.MC + "\",\"attributes\":{\"url\":\"" + schoolURL + "\"}";
                        if (lists.Where(n => n.ParentID == school.ID).ToList().Count > 0)
                            result += ",\"children\":" + GetObject(school.ID, strParent);
                        result += "},";
                    }
                    result = result.TrimEnd(',');
                    result += "]";
                
                }

            }
            context.Response.Write(result);

        }
  
        private string GetObject(string ParentId, string Array)
        {

            if (XY == "00000000-0000-0000-0000-000000000000")
            {
              
                temp += "[";
                foreach (Bap_School school in lists.Where(n => n.ParentID == ParentId ))
                {
                    int countt = users.Where(n => n.XY == school.ID).ToList().Count;
                    string state = "open";
                    if (countt > 0)
                    {
                        state = "closed";
                    }
                    temp += "{\"id\":\"" + school.ID + "\",\"state\":\"" + state + "\",\"text\":\"" + school.MC + "\",\"attributes\":{\"url\":\"" + departmentURL + "\"}";
                    if (users.Where(n => n.XY == school.ID).ToList().Count > 0)
                    {
                        temp = temp + ",\"children\":";
                        string abc = Getuser(school.ID);
                        temp = abc;
                    }
                    temp += "},";
                }
                temp = temp.TrimEnd(',');
                temp += "]";
            }


            else {
               
                temp += "[";
                foreach (Bap_School school in lists.Where(n => (n.ParentID == XX && n.ID == XY)))
                {
                    int countt = users.Where(n => n.XY == school.ID).ToList().Count;
                    string state = "open";
                    if (countt > 0)
                    {
                        state = "closed";
                    }
                    temp += "{\"id\":\"" + school.ID + "\",\"state\":\"" + state + "\",\"text\":\"" + school.MC + "\",\"attributes\":{\"url\":\"" + departmentURL + "\"}";
                    if (users.Where(n => n.XY == school.ID).ToList().Count > 0)
                    {
                        temp = temp + ",\"children\":";
                        string abc = Getuser(school.ID);
                        temp = abc;
                    }
                    temp += "},";
                }
                temp = temp.TrimEnd(',');
                temp += "]";
            
            
            }
            return temp;
        }


        private string Getuser(string yxid)
        {

            temp += "[";
            foreach (Bap_user school in users.Where(n => n.XY == yxid))
            {
                temp += "{\"id\":\"" + school.UserID + "\",\"text\":\"" + school.ZGXM + "\",\"attributes\":{\"url\":\"" + departmentURL + "\"}";
                temp += "},";
            }
            temp = temp.TrimEnd(',');
            temp += "]";
            return temp;
        }


        public partial class Bap_School
        {
            public Bap_School()
            { }
            public System.Data.DataTable GetAllData()
            {
                string strSelect = "select * from Bap_School ";
                return DbHelperSQL.Query(strSelect).Tables[0];
            }
            public List<Bap_School> GetList()
            {
                List<Bap_School> Entities = new List<Bap_School>();

                foreach (System.Data.DataRow dr in GetAllData().Rows)
                {
                    Entities.Add(new Bap_School()
                    {
                        ID = dr["ID"].ToString(),
                        BH = dr["BH"].ToString(),
                        MC = dr["MC"].ToString(),
                        Desc = dr["Desc"].ToString(),
                        ParentID = dr["ParentID"].ToString()
                    });
                }
                return Entities;
            }
            private string _id;
            private string _bh;
            private string _mc;
            private string _desc;
            private string _parentid;
            public string ID
            {
                set { _id = value; }
                get { return _id; }
            }
            public string BH
            {
                set { _bh = value; }
                get { return _bh; }
            }
            public string MC
            {
                set { _mc = value; }
                get { return _mc; }
            }
            public string Desc
            {
                set { _desc = value; }
                get { return _desc; }
            }
            public string ParentID
            {
                set { _parentid = value; }
                get { return _parentid; }
            }

        }



        public partial class Bap_user
        {
            public Bap_user()
            { }
            public System.Data.DataTable GetAllData()
            {
                string strSelect = "select * from Bap_user";
                return DbHelperSQL.Query(strSelect).Tables[0];
            }
            public List<Bap_user> GetList()
            {
                List<Bap_user> Entities = new List<Bap_user>();

                foreach (System.Data.DataRow dr in GetAllData().Rows)
                {
                    Entities.Add(new Bap_user()
                    {
                        UserID = dr["UserID"].ToString(),
                        ZGXM = dr["ZGXM"].ToString(),
                        XY = dr["XY"].ToString()
                   
                    });
                }
                return Entities;
            }
            private string _userid;
            private string _zgxm;
            private string _xx;

            public string UserID
            {
                set { _userid = value; }
                get { return _userid; }
            }
            public string ZGXM
            {
                set { _zgxm = value; }
                get { return _zgxm; }
            }
            public string XY
            {
                set { _xx = value; }
                get { return _xx; }
            }

        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        public Boolean bChild(string pid)
        {
            string sqlexe = "select Count(*) From Bap_School where ParentID=" + pid.ToString() + " ";
            int  dt = Convert.ToInt32(DbHelperSQL.GetSingle(sqlexe).ToString());
            bool test = false;
            if (dt > 0)
            {
                test = true;
            }
            return test;
        }

       
    }
}

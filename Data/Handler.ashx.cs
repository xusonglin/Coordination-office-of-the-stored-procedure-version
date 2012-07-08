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

namespace JiaoShiXinXiTongJi
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>                                                                                                          
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            #region 接收参数
            string action = context.Request.Params["action"];
            string username = context.Request["username"];
            string TaskID = context.Request["TaskID"];
            string TaskName = context.Request["TaskName"];
            string TaskMaker = context.Request["TaskMaker"];
            string userid = context.Request["userid"];
            string roleID = context.Request["roleID"];
            string functions = context.Request["functions"];
            string MC = context.Request["MC"];
            string DESC = context.Request["Desc"];
            string funname = context.Request["funname"];
            string fundesc = context.Request["fundesc"];
            string funurl = context.Request["funurl"];
            string funid = context.Request["funid"];
            string functionid = context.Request["functionid"];
            string parentid = context.Request["parentid"];
            string search = context.Request["search"];
            string ypass = context.Request["ypass"];
            string xpass = context.Request["xpass"];
            string strResult = "false";
            int rows = 10;
            int page = 1;
            if (context.Request["rows"] != null && context.Request["rows"] != "")
            {
                rows = Int32.Parse(context.Request["rows"].ToString());
                page = Int32.Parse(context.Request["page"].ToString());
            }
            #endregion

            switch (action)
            {
                case "Editrole": strResult = Editrole(roleID, MC, DESC); break;    // 修改角色
                case "Addrole": strResult = Addrole(MC, DESC,userid); break;       //添加角色
                case "Delete": strResult = Delete(TaskID,TaskName,username); break;   // 删除任务
                case "Deleterole": strResult = Deleterole(roleID); break;               //删除角色
                case "CancelPass": strResult = CancelPass(TaskID); break;             //撤销发布
                case "Query": strResult = Select(userid); break;                     //显示任务
                case "myQuery": strResult = mySelect(userid, search,rows,page); break;                  //显示个人任务
                case "myQueryyear": strResult = mySelectyear(userid, search); break;           //显示年度任务
                case "roleQuery": strResult = roleSelect(userid); break;             //显示角色
                case "UpdateRole": strResult = UpdateRole(roleID, functions); break;    //角色授权   
                case "Addfun": strResult = Addfun(funname, fundesc,funurl,funid); break;  // 添加功能
                case "Editfun": strResult = Editfun(funname, fundesc, funurl, funid); break;  // 修改功能
                case "DeleteFun": strResult = DeleteFun(functionid); break;                   //删除功能
                case "myQueryfun": strResult = myQueryfun(parentid); break;                   //系统配置里显示所有功能
                case "myDelete": strResult = myDelete(TaskID, userid); break;   // 删除我的任务
                case "myyearDelete": strResult = myyearDelete(TaskID, userid); break;   // 删除我的任务
                case "ChangePass": strResult = ChangePass(ypass, xpass, userid); break;   // 修改密码
            }
            context.Response.Write(strResult);
        }


        private string ChangePass(string ypass, string xpass,string userid)
        {
            Common common = new Common();
            var test = false;
            var sele = "select pass from bap_user where UserID='" + userid + "'";
            string pass = DbHelperSQL.GetSingle(sele).ToString();
            if (common.Decrypting(pass) == ypass)
            {
                var edit = "update bap_user  set pass='" + common.Encrypting(xpass) + "' where UserID='" + userid + "'";
                if (DbHelperSQL.ExecuteSql(edit) > 0)
                { test = true; }
            }

            return test.ToString();  //Query、GetSingle、ExecuteSql（三个执行语句）
        }


        private string Editrole(string roleid, string mc, string DESC)
        {
            var edit = "update bap_role set [Role]='" + mc + "',[Desc]='" + DESC + "' where ID='" + roleid + "'";
            return DbHelperSQL.ExecuteSql(edit) > 0 ? "true" : "false";  //Query、GetSingle、ExecuteSql（三个执行语句）
        }

        private string UpdateRole(string roleid, string functions)
        {
            var edit = "update Bap_Role set FunctionIDs='" + functions + "' where ID='" + roleid + "' ";
            return DbHelperSQL.ExecuteSql(edit) > 0 ? "true" : "false";  //Query、GetSingle、ExecuteSql（三个执行语句）
        }

        private string Addfun(string funname, string fundesc, string funurl,string funid)
        {

           string px = DbHelperSQL.GetMaxID("px", "Bap_Function").ToString();
           if (funid == "")
           { funid = "00000000-0000-0000-0000-000000000000"; }
            string  newid=DbHelperSQL.GetSingle("select newid()").ToString();
            var add = "insert into Bap_Function(FunctionID,FunctionName,FunctionDesc,Url,ParentID,px) select '"+newid+"','"+funname+"','"+fundesc+"','"+funurl+"','"+funid+"','"+px+"'";
            var updaterolefunction = "update bap_role set functionids=functionids + ','+'" + newid + "'";
            DbHelperSQL.ExecuteSql(updaterolefunction);
            return DbHelperSQL.ExecuteSql(add) > 0 ? "true" : "false";
        }
        private string Editfun(string funname, string fundesc, string funurl, string funid)
        {

            var edit = "update Bap_Function set FunctionName='" + funname + "',FunctionDesc='" + fundesc + "',Url='" + funurl + "' where FunctionID='" + funid + "'";
            return DbHelperSQL.ExecuteSql(edit) > 0 ? "true" : "false";
        }
        private string Addrole( string MC, string DESC ,string userid)
        {

            string px = DbHelperSQL.GetMaxID("px", "Bap_Role").ToString ();

            var add = "insert into bap_role(ID,[UserID],[Role],[Desc],px) select newid(),'"+userid+"','"+MC+"','"+DESC+"','"+px+"'";
            return DbHelperSQL.ExecuteSql(add) > 0 ? "true" : "false";
        }

        private string Delete(string TaskID,string taskname,string username)
        {
            var result = false;
 
            var delete = "delete from bap_task where taskid='"+ TaskID + "'";         
            string del_ = "delete from bap_task_user where taskid='" + TaskID + "'";// 同时删除bap_task_user 中的这个任务和用户的关系，这样，用户登录后就看不到任务
            string deltitle = "delete from bap_title where taskid='" + TaskID + "'";// 
            string deltext = "delete from bap_text where taskid='" + TaskID + "'";// 
            string cmddel = @" drop table [" + taskname + "【" + username + "】]   ";//删除数据库中的表
            DbHelperSQL.ExecuteSql(del_);

            if (DbHelperSQL.ExecuteSql(delete) > 0)
            {
                result = true;
                DbHelperSQL.ExecuteSql(deltitle) ;
                DbHelperSQL.ExecuteSql(deltext) ;
                DbHelperSQL.ExecuteSql(cmddel);
            }
            return result.ToString();

        }

        private string myDelete(string TaskID, string userid)
        {
            var delete = "delete from bap_task_user where taskid='" + TaskID + "' and Userid='"+userid+"'";
            return DbHelperSQL.ExecuteSql(delete) > 0 ? "true" : "false";
        }

        private string myyearDelete(string TaskID, string userid)
        {
            var delete = "delete from bap_task_user where taskid='" + TaskID + "' and Userid='" + userid + "'";
            return DbHelperSQL.ExecuteSql(delete) > 0 ? "true" : "false";

        }
        private string Deleterole(string roleid)
        {
            var delete = "delete from bap_role where ID='" + roleid + "'";
            return DbHelperSQL.ExecuteSql(delete) > 0 ? "true" : "false";
        }

        private string DeleteFun(string functionid)
        {
            var delete = "delete from bap_function where FunctionID='" + functionid + "'";
            return DbHelperSQL.ExecuteSql(delete) > 0 ? "true" : "false";
        }


        private string CancelPass(string taskid)
        {
            var   result = false;
            var update = "update bap_task set state='未发布' where taskid='" + taskid + "'"; //更改bap_task中的任务状态为未发布
            var delete = "delete from bap_task_user where taskid='" + taskid + "'";// 同时删除bap_task_user 中的这个任务和用户的关系，这样，用户登录后就看不到任务
            if (DbHelperSQL.ExecuteSql(update) > 0 &&  DbHelperSQL.ExecuteSql(delete)>0)
            {
                result = true;
            }
            return  result .ToString ();

        }

        private string Select(string userid)
        {
            var select = "";
              select = "select * from bap_task where UserID='" + userid + "'";
                DataTable dt = DbHelperSQL.Query(select).Tables[0];
                return GetJSONString(dt);
            
        }

        private string mySelect(string userid, string search,int rows,int page)
        {
            var select = "";
            string selecttotal="";
            if (search != null && search != "")
            {
                //select = "SELECT bt.TaskID,bt.StartTime,bt.EndTime,b.*,bt.State AS State_ FROM dbo.Bap_Task_User bt LEFT JOIN dbo.Bap_Task b ON bt.TaskID = b.TaskID where bt.UserID='" + userid + "'and bt.LeiXin='1' and b.TaskName like '%" + search + "%' ORDER BY  b.DateTime DESC";
                select = "SELECT bt.TaskID,bt.StartTime,bt.EndTime,b.*,bt.State AS State_ FROM dbo.Bap_Task_User bt LEFT JOIN dbo.Bap_Task b ON bt.TaskID = b.TaskID where bt.UserID='" + userid + "'and bt.LeiXin='1' and b.TaskName like '%" + search + "%' ORDER BY  b.DateTime DESC";
                selecttotal = "SELECT count(*) FROM dbo.Bap_Task_User bt LEFT JOIN dbo.Bap_Task b ON bt.TaskID = b.TaskID where bt.UserID='" + userid + "'and bt.LeiXin='1' and b.TaskName like '%" + search + "%' ";

            }
            else 
            {

                //select = "SELECT bt.TaskID,bt.StartTime,bt.EndTime,b.*,bt.State AS State_ FROM dbo.Bap_Task_User bt LEFT JOIN dbo.Bap_Task b ON bt.TaskID = b.TaskID where bt.UserID='" + userid + "'and bt.LeiXin='1' ORDER BY  b.DateTime DESC  ";
                select = "SELECT top(" + rows + ")bt.TaskID,bt.StartTime,bt.EndTime,b.*,bt.State AS State_ FROM dbo.Bap_Task_User bt LEFT JOIN dbo.Bap_Task b ON bt.TaskID = b.TaskID where bt.UserID='" + userid + "'and bt.LeiXin='1' and bt.taskid not in(SELECT top(" + rows * (page - 1) + ")bt.TaskID FROM dbo.Bap_Task_User bt LEFT JOIN dbo.Bap_Task b ON bt.TaskID = b.TaskID where bt.UserID='" + userid + "'and bt.LeiXin='1' ORDER BY  b.DateTime DESC  )ORDER BY  b.DateTime DESC  ";

                selecttotal="SELECT count(*) FROM dbo.Bap_Task_User bt LEFT JOIN dbo.Bap_Task b ON bt.TaskID = b.TaskID where bt.UserID='" + userid + "'and bt.LeiXin='1'";

            }



            DataTable dt = DbHelperSQL.Query(select).Tables[0];
            //return GetJSONString(dt);
            int Total = (Convert.ToInt32(DbHelperSQL.GetSingle(selecttotal).ToString()));
          return GetJSONStrings(dt, Total);

        }


        private string mySelectyear(string userid,string search)
        {
            var select = "";
            if (search != null && search != "")
            {
                select = "SELECT bt.TaskID,bt.StartTime,bt.EndTime,b.*,bt.State AS State_ FROM dbo.Bap_Task_User bt LEFT JOIN dbo.Bap_Task b ON bt.TaskID = b.TaskID where bt.UserID='" + userid + "' and bt.LeiXin='2' and b.TaskName like '%" + search + "%' ORDER BY  b.DateTime DESC";
            }
            else
            {
                select = "SELECT bt.TaskID,bt.StartTime,bt.EndTime,b.*,bt.State AS State_ FROM dbo.Bap_Task_User bt LEFT JOIN dbo.Bap_Task b ON bt.TaskID = b.TaskID where bt.UserID='" + userid + "' and bt.LeiXin='2' ORDER BY  b.DateTime DESC";
            
            
            }
            DataTable dt = DbHelperSQL.Query(select).Tables[0];
            return GetJSONString(dt);

        }
        private string roleSelect(string userid)
        {
            var select = "";
            select = "SELECT * FROM dbo.Bap_Role where ID<>'682EA172-A66B-4319-9B28-A17C589376EC' and UserId='"+userid+"' order by px ";
            DataTable dt = DbHelperSQL.Query(select).Tables[0];
            return GetJSONString(dt);

        }

        private string myQueryfun(string parentid)
        {
            var select = "";
            if (parentid == null || parentid == "")
            {
                select = "SELECT * FROM dbo.Bap_Function where ParentID='00000000-0000-0000-0000-000000000000' order by px ";
            }

            else
            {
                select = "SELECT * FROM dbo.Bap_Function where ParentID='" + parentid + "' order by px ";
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
        private string GetJSONStrings(DataTable dt,int totalcount)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"total\":");
            jsonBuilder.Append(totalcount);
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

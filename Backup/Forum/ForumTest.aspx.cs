using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using JiaoShiXinXiTongJi;
using Maticsoft.DBUtility;

namespace JiaoShiXinXiTongJi.Forum
{
    public partial class ForumTest : BasePage
    {
        #region 定义变量
        protected string strs="";
        protected int  Count=0;
        protected string userid = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
         userid = Session["UserID"].ToString();
         string type=  Request.Form["type"]+"";
         if(type=="add")
         {
             #region 参数
             string liuyanconten = Request.Form["Content"].ToString();         
             DateTime LyDate = DateTime.Now;
             Guid userid1 = new Guid(userid);
             int rowsAffected1;
             SqlParameter[] parametersinsert1 = {
             new SqlParameter("@content", SqlDbType.NVarChar,4000),
             new SqlParameter("@lydate", SqlDbType.DateTime),
             new SqlParameter("@userid", SqlDbType.UniqueIdentifier)
             };
             parametersinsert1[0].Value = liuyanconten;
             parametersinsert1[1].Value = LyDate;
             parametersinsert1[2].Value = userid1;
             #endregion
             DbHelperSQL.RunProcedure("sp_BapForumInsert_BBS", parametersinsert1, out rowsAffected1);
             //string ins = "insert into bap_forum select newid(),'" + liuyanconten + "','" + LyDate + "','" + userid + "'";
             //DbHelperSQL.ExecuteSql(ins);
                
         
         }

         if (type == "deleteliuyan")
         {
             #region 参数
             string CountID = Request.Form["CountID"].ToString();
             Guid countid1 = new Guid(CountID);
             int rowsAffected2;
             SqlParameter[] parametersdelete2 = {
             new SqlParameter("@countid", SqlDbType.UniqueIdentifier )
             };
             parametersdelete2[0].Value = countid1;
             int rowsAffected3;
             SqlParameter[] parametersdelete3 = {
             new SqlParameter("@countid", SqlDbType.UniqueIdentifier )
             };
             parametersdelete3[0].Value = countid1;
             #endregion
             DbHelperSQL.RunProcedure("sp_BapForumHfDelete_BBS", parametersdelete3, out rowsAffected3);
             DbHelperSQL.RunProcedure("sp_BapForumDelete_BBS", parametersdelete2, out rowsAffected2);
             #region 删除操作有一点逻辑错误。应先删除子表的内容，然后在删除与各关联的父表的内容
             //string CountID = Request.Form["CountID"].ToString();
             //string dele = "delete from bap_forum where CountID='" + CountID + "'";
             //string delehf = "delete from bap_forum_hf where CountID='" + CountID + "'";
             //DbHelperSQL.ExecuteSql(dele);
             //DbHelperSQL.ExecuteSql(delehf);
             #endregion

         }
         //存储过程
         SqlParameter[] parameters = { new SqlParameter("@t", SqlDbType.Int) };
         parameters[0].Value = 1;
         //将表Bap_User、Bap_Forum关联取用户信息值，和BBS信息值
         DataSet dt = DbHelperSQL.RunProcedure("sp_BapForumAndUserSelect_BBS", parameters, "dt");

        //string se = "select bf.*,bu.ZGXM from bap_forum bf left join bap_user bu on bf.userid=bu.userid order by bf.Lydate desc";
        //DataSet dt = DbHelperSQL.Query(se);
        string[] arrlist = new string[1000];
        if (dt.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
            {

                arrlist[i] = dt.Tables[0].Rows[i]["CountID"].ToString() + "_" + dt.Tables[0].Rows[i]["Content"].ToString() + "_" + dt.Tables[0].Rows[i]["LyDate"].ToString() + "_" + dt.Tables[0].Rows[i]["UserID"].ToString() + "_" + dt.Tables[0].Rows[i]["ZGXM"].ToString();
                strs += arrlist[i] + "&";
            }

            strs = strs.Substring(0, strs.Length - 1);
            Count = dt.Tables[0].Rows.Count;

        }
 
        }

   

    }
}


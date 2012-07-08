
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
using System.Security.Cryptography;// 密码加密用到
using System.IO;// 密码加密用到
using System.Text;
using JiaoShiXinXiTongJi;
using Maticsoft.DBUtility;

namespace JiaoShiXinXiTongJi
{
    public partial class logon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack)
            {
                string username = Request.Form["username"].ToString();
                string pass = Request.Form["password"].ToString();
           

                Common common = new Common();

                //存储过程
                SqlParameter[] parameters = { new SqlParameter("@username",SqlDbType.NVarChar,2000)};
                parameters[0].Value = username;
                DataSet ds = DbHelperSQL.RunProcedure("sp_bapuserselect_UidandPwd", parameters, "ds");
                //判断用户名是否存在
                if (ds.Tables[0].Rows.Count == 0)
                {
                    Response.Write("<script> alert('用户名不存在！')</script>");
                    Response.Write("<script>document.location=document.location;</script>");
                }
                //判断密码是否正确
                if (ds.Tables[0].Rows.Count> 0)
                {
                    if ((ds.Tables[0].Rows[0]["Pass"].ToString().Trim() == common.Encrypting(pass.Trim())))
                    {
                        Session["ZGBH"] = username;
                        Session["ZGXM"] = ds.Tables[0].Rows[0]["ZGXM"].ToString();
                        Session["UserID"] = ds.Tables[0].Rows[0]["UserID"].ToString();
                        Session["XX"] = ds.Tables[0].Rows[0]["XX"].ToString();
                        Session["XY"] = ds.Tables[0].Rows[0]["XY"].ToString();
                        Session["Role"] = ds.Tables[0].Rows[0]["Role"].ToString();
                        Response.Redirect("~/Main.aspx");
                    }
                    else
                    {
                        Response.Write("<script> alert('密码错误！')</script>");
                        Response.Write("<script>document.location=document.location;</script>");
                    }
                }
            }

        }
    }
}

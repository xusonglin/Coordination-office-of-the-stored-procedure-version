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
namespace JiaoShiXinXiTongJi.USER
{
    public partial class myinfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 显示信息
            if (!Page.IsPostBack)
            {



                string userid1 = Session["UserID"].ToString();
                Guid userid = new Guid(userid1);

                //存储过程
                SqlParameter[] parameters = { new SqlParameter("@userid", SqlDbType.UniqueIdentifier) };
                parameters[0].Value = userid;
                //将表Bap_User、Bap_Role、Bap_School(2个)、Tab_ZCBM关联取用户信息值
                DataSet ds = DbHelperSQL.RunProcedure("sp_bapUserRoleselect_Myinfo", parameters, "ds");
                zgbh.Text = ds.Tables[0].Rows[0]["ZGBH"].ToString();

                zgxm.Text = ds.Tables[0].Rows[0]["ZGXM"].ToString();
                csrq.Text = ds.Tables[0].Rows[0]["CSRQ"].ToString();
                nl.Text = ds.Tables[0].Rows[0]["NL"].ToString();
                mz.Text = ds.Tables[0].Rows[0]["MZ"].ToString();
                jg.Text = ds.Tables[0].Rows[0]["JG"].ToString();
                jg.Text = ds.Tables[0].Rows[0]["JG"].ToString();
                zw.Text = ds.Tables[0].Rows[0]["ZW"].ToString();
                dzjz.Text = ds.Tables[0].Rows[0]["DZJZ"].ToString();
                cid.Text = ds.Tables[0].Rows[0]["CID"].ToString();
                rzsj.Text = ds.Tables[0].Rows[0]["RZSJ"].ToString();
                lxdh.Text = ds.Tables[0].Rows[0]["Tel"].ToString();
                email.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                Session["xb"] = ds.Tables[0].Rows[0]["XB"].ToString();
                if (ds.Tables[0].Rows[0]["xl"].ToString() == "" || ds.Tables[0].Rows[0]["xl"].ToString() == null)
                {
                    Session["xl"] = "8";

                }
                else
                {
                    Session["xl"] = ds.Tables[0].Rows[0]["xl"].ToString();
                }
                if (ds.Tables[0].Rows[0]["zc"].ToString() == "" || ds.Tables[0].Rows[0]["zc"].ToString() == null)
                {
                    Session["zc"] = "8";
                }
                else
                {
                    Session["zc"] = ds.Tables[0].Rows[0]["zc"].ToString();
                }
                role.Text = ds.Tables[0].Rows[0]["Role_"].ToString();
                Session["role"] = ds.Tables[0].Rows[0]["Role"].ToString();
                #region 另一种SOL语句读取用户个人信息方式
                //string userid = Session["UserID"].ToString();
                //string selectuser = "select bu.*,br.ROLE AS role_,bs.MC AS XY_,bss.MC AS YX_ ,xl.BM as xl,zc.BM as zc from Bap_User bu LEFT JOIN Bap_Role br ON bu.ROLE=br.id LEFT JOIN dbo.Bap_School bs ON bu.XX=bs.ID LEFT JOIN dbo.Bap_School bss ON bu.XY=bss.ID left join Tab_XLBM xl on bu.xl=xl.BM left join Tab_ZCBM zc on bu.zcjb=zc.BM  where bu.UserID='" + userid + "'";
                //SqlDataReader dr = DbHelperSQL.ExecuteReader(selectuser);
                //while (dr.Read())
                //{
                //    zgbh.Text = dr["ZGBH"].ToString();

                //    zgxm.Text = dr["ZGXM"].ToString();
                //    csrq.Text = dr["CSRQ"].ToString();
                //    nl.Text = dr["NL"].ToString();
                //    mz.Text = dr["MZ"].ToString();
                //    jg.Text = dr["JG"].ToString();
                //    jg.Text = dr["JG"].ToString();
                //    zw.Text = dr["ZW"].ToString();
                //    dzjz.Text = dr["DZJZ"].ToString();
                //    cid.Text = dr["CID"].ToString();
                //    rzsj.Text = dr["RZSJ"].ToString();
                //    lxdh.Text = dr["Tel"].ToString();
                //    email.Text = dr["Email"].ToString();
                //    Session["xb"] = dr["XB"].ToString();
                //    if (dr["xl"].ToString() == "" || dr["xl"].ToString() == null)
                //    {
                //        Session["xl"] = "8";

                //    }
                //    else
                //    {
                //        Session["xl"] = dr["xl"].ToString();
                //    }
                //    if (dr["zc"].ToString() == "" || dr["zc"].ToString() == null)
                //    {
                //        Session["zc"] = "8";
                //    }
                //    else
                //    {
                //        Session["zc"] = dr["zc"].ToString();
                //    }
                //    role.Text = dr["Role_"].ToString();
                //    Session["role"] = dr["Role"].ToString();

                //}

                //dr.Close();
                #endregion
                bindxb();
                Bindxueli();
                Bindzhicheng();
                Bindxuexiao();
                Bindxueyuan();

            }
        }
            #endregion
        #region 数据绑定
        private void Bindxueyuan()
        {
            #region 此处只能绑定，但是不可改，无其他可改的。转为存储过程的一样不能改。应参考Bindzhicheng()方法的编写。
            //Common common = new Common();
            //SqlConnection Conn = common.coon();
            //string strSql = "select * from Bap_School where ID='" + Session["XY"].ToString() + "' ";
            //SqlDataAdapter adp = new SqlDataAdapter(strSql, Conn);
            //Conn.Open();
            //DataSet dt = new DataSet();
            //adp.Fill(dt, "MyTable");
            //DropDownList_xy.AppendDataBoundItems = true;
            //DropDownList_xy.DataSource = dt.Tables["MyTable"].DefaultView;
            //DropDownList_xy.DataTextField = "MC";
            //DropDownList_xy.DataValueField = "ID";
            //DropDownList_xy.DataBind();
            //Conn.Close();
            #endregion
            string id1 = Session["XY"].ToString();
            Guid id = new Guid(id1);
            //存储过程
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.UniqueIdentifier) };
            parameters[0].Value = id;
            DataSet dt = DbHelperSQL.RunProcedure("sp_BapSchoolSelect_Bind", parameters, "dt");
            DropDownList_xy.AppendDataBoundItems = true;
            DropDownList_xy.DataSource = dt.Tables["dt"].DefaultView;
            DropDownList_xy.DataTextField = "MC";
            DropDownList_xy.DataValueField = "ID";
            DropDownList_xy.DataBind();
        }

        private void Bindxuexiao()
        {
            #region 同上。需根据前面所选学院，动态生成该学校所有的专业。
            //Common common = new Common();
            //SqlConnection Conn = common.coon();
            //string strSql = "select * from Bap_School where ID='" + Session["XX"].ToString() + "' ";
            //SqlDataAdapter adp = new SqlDataAdapter(strSql, Conn);
            //Conn.Open();
            //DataSet dt = new DataSet();
            //adp.Fill(dt, "MyTable");
            //DropDownList_xx.AppendDataBoundItems = true;
            //DropDownList_xx.DataSource = dt.Tables["MyTable"].DefaultView;
            //DropDownList_xx.DataTextField = "MC";
            //DropDownList_xx.DataValueField = "ID";

            //DropDownList_xx.DataBind();

            //Conn.Close();
            #endregion
            string id1 = Session["XX"].ToString();
            Guid id = new Guid(id1);
            //存储过程
            SqlParameter[] parameters = { new SqlParameter("@id", SqlDbType.UniqueIdentifier) };
            parameters[0].Value = id;
            DataSet dt1 = DbHelperSQL.RunProcedure("sp_BapSchoolSelect_Bind", parameters, "dt1");
            DropDownList_xx.AppendDataBoundItems = true;
            DropDownList_xx.DataSource = dt1.Tables["dt1"].DefaultView;
            DropDownList_xx.DataTextField = "MC";
            DropDownList_xx.DataValueField = "ID";
            DropDownList_xx.DataBind();
        }

        private void Bindzhicheng()
        {
            #region 存储过程最好无参数，可以减少代码量
            //Common common = new Common();
            //SqlConnection Conn = common.coon();
            //string strSql = "select * from Tab_ZCBM ";
            //SqlDataAdapter adp = new SqlDataAdapter(strSql, Conn);
            //Conn.Open();
            //DataSet dt = new DataSet();
            //adp.Fill(dt, "MyTable");
            //DropDownList_zc.SelectedValue = Session["zc"].ToString();
            //DropDownList_zc.AppendDataBoundItems = true;
            //DropDownList_zc.DataSource = dt.Tables["MyTable"].DefaultView;
            //DropDownList_zc.DataTextField = "MC";
            //DropDownList_zc.DataValueField = "BM";
            //DropDownList_zc.DataBind();
            //Conn.Close();
            #endregion
            //存储过程
            SqlParameter[] parameters = { new SqlParameter("@t", SqlDbType.Int) };
            parameters[0].Value = 1;
            DataSet dt = DbHelperSQL.RunProcedure("sp_TabZCBMSelect_Bind", parameters, "dt");
            DropDownList_zc.SelectedValue = Session["zc"].ToString();
            DropDownList_zc.AppendDataBoundItems = true;
            DropDownList_zc.DataSource = dt.Tables["dt"].DefaultView;
            DropDownList_zc.DataTextField = "MC";
            DropDownList_zc.DataValueField = "BM";
            DropDownList_zc.DataBind();
        }

        private void Bindxueli()
        {
            #region 存储过程最好无参数，可以减少代码量
            //Common common = new Common();
            //SqlConnection Conn = common.coon();
            //string strSql = "select * from Tab_XLBM ";
            //SqlDataAdapter adp = new SqlDataAdapter(strSql, Conn);
            //Conn.Open();
            //DataSet dt = new DataSet();
            //adp.Fill(dt, "MyTable");
            //DropDownList_xl.SelectedValue = Session["xl"].ToString();
            //DropDownList_xl.AppendDataBoundItems = true;
            //DropDownList_xl.DataSource = dt.Tables["MyTable"].DefaultView;
            //DropDownList_xl.DataTextField = "MC";
            //DropDownList_xl.DataValueField = "BM";
            //DropDownList_xl.DataBind();
            //Conn.Close();
            //存储过程
            #endregion
            SqlParameter[] parameters = { new SqlParameter("@t", SqlDbType.Int) };
            parameters[0].Value = 1;
            DataSet dt = DbHelperSQL.RunProcedure("sp_TabXLBMSelect_Bind", parameters, "dt");
            DropDownList_xl.SelectedValue = Session["xl"].ToString();
            DropDownList_xl.AppendDataBoundItems = true;
            DropDownList_xl.DataSource = dt.Tables["dt"].DefaultView;
            DropDownList_xl.DataTextField = "MC";
            DropDownList_xl.DataValueField = "BM";
            DropDownList_xl.DataBind();
        }

        private void bindxb()
        {
            xb.Text = Session["xb"].ToString();
        }
        #endregion

        #region 提交数据
        protected void submit_Click(object sender, EventArgs e)
        {
            #region 省略
            //string userid = Session["UserID"].ToString();
            //string zgbh_ = zgbh.Text.ToString();
            //string zgxm_ = zgxm.Text.ToString();
            //string xb_ = xb.SelectedValue.ToString();
            //string csrq_ = csrq.Text.ToString();
            //string nl_ = nl.Text.ToString();
            //string mz_ = mz.Text.ToString();
            //string jg_ = jg.Text.ToString();
            //string xx = DropDownList_xx.SelectedValue.ToString();
            //if (xx == "" || xx == null)
            //{
            //    xx = "00000000-0000-0000-0000-000000000000";
            //}
            //string xy = DropDownList_xy.SelectedValue.ToString();
            //if (xy == "" || xy == null)
            //{
            //    xy = "00000000-0000-0000-0000-000000000000";
            //}
            //string xl_ = DropDownList_xl.SelectedValue.ToString();
            //string zw_ = zw.Text.ToString();
            //string zcjb_ = DropDownList_zc.SelectedValue.ToString();
            //string dzjz_ = dzjz.Text.ToString();
            //string cid_ = cid.Text.ToString();
            //string rzsj_ = rzsj.Text.ToString();
            //string lxdh_ = lxdh.Text.ToString();
            //string email_ = email.Text.ToString();
            //string role_ = Session["role"].ToString();
            //string update = "update bap_user set ZGXM='" + zgxm_ + "',XB='" + xb_ + "',CSRQ='" + csrq_ + "',NL='" + nl_ + "',MZ='" + mz_ + "',JG='" + jg_ + "',XX='" + xx + "',XY='" + xy + "',XL='" + xl_ + "',ZW='" + zw_ + "',ZCJB='" + zcjb_ + "',DZJZ='" + dzjz_ + "',CID='" + cid_ + "',RZSJ='" + rzsj_ + "',Tel='" + lxdh_ + "',Email='" + email_ + "' where UserID='" + userid + "'";
            //DbHelperSQL.ExecuteSql(update);
            #endregion
            #region 定义参数
            int rowsAffected;
            SqlParameter[] parameters = {
            new SqlParameter("@zgxm", SqlDbType.NVarChar,2000),
            new SqlParameter("@xb", SqlDbType.NVarChar,2000),
            new SqlParameter("@csrq", SqlDbType.NVarChar,2000),
            new SqlParameter("@nl", SqlDbType.Int),
            new SqlParameter("@mz", SqlDbType.NVarChar,2000),
            new SqlParameter("@jg", SqlDbType.NVarChar,2000),
            new SqlParameter("@xx", SqlDbType.NVarChar,2000),
            new SqlParameter("@xy", SqlDbType.NVarChar,2000),
            new SqlParameter("@xl", SqlDbType.NVarChar,2000),
            new SqlParameter("@zw", SqlDbType.NVarChar,2000),
            new SqlParameter("@zcjb", SqlDbType.NVarChar,2000),
            new SqlParameter("@dzjz", SqlDbType.NVarChar,2000),
            new SqlParameter("@cid", SqlDbType.NVarChar,2000),
            new SqlParameter("@rzsj", SqlDbType.NVarChar,2000),
            new SqlParameter("@tel", SqlDbType.NVarChar,2000),
            new SqlParameter("@email", SqlDbType.NVarChar,2000),
            new SqlParameter("@userid", SqlDbType.UniqueIdentifier)
            }; 
            string id1 = Session["UserID"].ToString();
            Guid userid = new Guid(id1);
            string zgbh_ = zgbh.Text.ToString();
            string zgxm_ = zgxm.Text.ToString();
            string xb_ = xb.SelectedValue.ToString();
            string csrq_ = csrq.Text.ToString();
            string nl_ = nl.Text.ToString();
            string mz_ = mz.Text.ToString();
            string jg_ = jg.Text.ToString();
            string xx = DropDownList_xx.SelectedValue.ToString();
            if (xx == "" || xx == null)
            {
                xx = "00000000-0000-0000-0000-000000000000";
            }
            string xy = DropDownList_xy.SelectedValue.ToString();
            if (xy == "" || xy == null)
            {
                xy = "00000000-0000-0000-0000-000000000000";
            }
            string xl_ = DropDownList_xl.SelectedValue.ToString();
            string zw_ = zw.Text.ToString();
            string zcjb_ = DropDownList_zc.SelectedValue.ToString();
            string dzjz_ = dzjz.Text.ToString();
            string cid_ = cid.Text.ToString();
            string rzsj_ = rzsj.Text.ToString();
            string lxdh_ = lxdh.Text.ToString();
            string email_ = email.Text.ToString();
            string role_ = Session["role"].ToString();
            parameters[0].Value = zgxm_;
            parameters[1].Value = xb_;
            parameters[2].Value = csrq_;
            parameters[3].Value = nl_;
            parameters[4].Value = mz_;
            parameters[5].Value = jg_;
            parameters[6].Value = xx;
            parameters[7].Value = xy;
            parameters[8].Value = xl_;
            parameters[9].Value = zw_;
            parameters[10].Value = zcjb_;
            parameters[11].Value = dzjz_;
            parameters[12].Value = cid_;
            parameters[13].Value = rzsj_;
            parameters[14].Value = lxdh_;
            parameters[15].Value = email_;
            parameters[16].Value = userid;
            #endregion
            DbHelperSQL.RunProcedure("sp_BapUserUpdate_myinfo", parameters, out rowsAffected);
            Response.Write("<script> alert('修改信息成功！')</script>");
            Response.Write("<script>document.location=document.location;</script>");
        }
        #endregion
    }
}

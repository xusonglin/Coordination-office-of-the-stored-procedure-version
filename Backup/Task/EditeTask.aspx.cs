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
namespace JiaoShiXinXiTongJi.Task
{
    public partial class EditeTask : BasePage
    {
        #region 定义变量
        protected string str;
        protected string str_text;
        protected string arr_text_first;
        protected string GeRenXi;
        protected string GeRenXiInfo;
        protected string stringdata;
        protected   string testt;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            string TaskMaker = Session["TaskMaker"].ToString(); ///注意区别创建者
            string TaskID = Request.QueryString["Id"].ToString(); //接收前一个页面传来的任务ID,然后把这个任务的内容显示在页面上
            string userid = Session["UserID"].ToString();// 取得用户ID
             
            taskmaker.Text=DbHelperSQL.GetSingle("select TaskMaker from bap_task where TaskID='" + TaskID + "'").ToString();
            
            #region 取得个人信息在前台填充数据
            string selectuser = "select bu.*,bs.MC as xuexiao,bss.MC as xueyuan,tx.MC as xueli,tz.MC as zcjb from bap_user bu left join bap_school bs on bu.XX=bs.ID left join  bap_school bss on bu.XY=bss.ID left join Tab_XLBM tx on bu.XL=tx.BM left join Tab_ZCBM tz on bu.ZCJB=tz.BM  where userid='" + userid + "'";
            SqlDataReader drs = DbHelperSQL.ExecuteReader(selectuser);
            while (drs.Read())
            {

                string zgbh = drs["ZGBH"].ToString();
                string zgxm = drs["ZGXM"].ToString();
                string xb = drs["XB"].ToString();
                string csri = drs["CSRQ"].ToString();
                string nl = drs["NL"].ToString();
                string cid = drs["CID"].ToString();
                string jg = drs["JG"].ToString();
                string mz = drs["MZ"].ToString();
                string xy = drs["xuexiao"].ToString();
                string yx = drs["xueyuan"].ToString();
                string xl = drs["xueli"].ToString();
                string zw = drs["ZW"].ToString();
                string zcjb = drs["zcjb"].ToString();
                string dzjz = drs["DZJZ"].ToString();
                string rzsj = drs["RZSJ"].ToString();

                GeRenXiInfo = zgbh + "&" + zgxm + "&" + xb + "&" + csri + "&" + nl + "&" + cid + "&" + jg + "&" + mz + "&" + xy + "&" + yx + "&" + xl + "&" + zw + "&" + zcjb + "&" + dzjz + "&" + rzsj;

                GeRenXi = "职工编号" + "&" + "姓名" + "&" + "性别" + "&" + "出生日期" + "&" + "年龄" + "&" + "身份证号" + "&" + "籍贯" + "&" + "民族" + "&" + "学院" + "&" + "系别" + "&" + "学历" + "&" + "职务" + "&" + "职称级别" + "&" + "党政兼职" + "&" + "任职时间";

            }
            #endregion


            #region 取得任务标题以及字段信息
            string selecttaskname = "select TaskName from bap_task where TaskID='" + TaskID + "'";
            string TaskName = DbHelperSQL.GetSingle(selecttaskname).ToString();
             Common conmon = new Common();
             SqlConnection con = conmon.coon();

                // 页面首次加载时显示信息
                con.Open();
                string sel = "select * from Bap_Title where TaskID='" + TaskID + "'";
                SqlDataAdapter da = new SqlDataAdapter(sel, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 1; i < ds.Tables[0].Columns.Count; i++) //列
                {
                    str += ds.Tables[0].Rows[0][i].ToString() + ",";// 将标题的信息拼接起来
                }
                string sel_text = "select * from Bap_Text where TaskID='" + TaskID + "'";
                SqlDataAdapter da_text = new SqlDataAdapter(sel_text, con);
                DataSet ds_text = new DataSet();
                da_text.Fill(ds_text);
                string[] arr_text = new string[100];
                for (int j = 0; j < ds_text.Tables[0].Rows.Count; j++)
                {
                    for (int i = 1; i < ds_text.Tables[0].Columns.Count; i++)
                    {
                        arr_text[j] += ds_text.Tables[0].Rows[j][i].ToString() + ","; // 每个字段的信息拼接
                    }
                    arr_text_first += ds_text.Tables[0].Rows[j][1].ToString() + ","; //拼接 字段名称，在创建动态数据表的时候用
                    str_text += arr_text[j] + "_";  // 将字段的所有信息拼接起来
                }
                con.Close();
            #endregion 


            #region 取得已经填写的任务信息
                testt = "UserID,"+arr_text_first.Substring(0, arr_text_first.Length - 1)+",px";
                con.Open();
                string selectdata = "select " + testt + " from  [" + TaskName + "【" + TaskMaker + "】] where UserID='" + userid + "'order by px";

                SqlDataAdapter data = new SqlDataAdapter(selectdata, con);
                DataSet data_text = new DataSet();
                data.Fill(data_text);
                for (int j = 0; j < data_text.Tables[0].Rows.Count; j++) //行
                {
                    string datatext = "";
                    for (int i = 1; i < data_text.Tables[0].Columns.Count; i++) //列
                    {
                        datatext += data_text.Tables[0].Rows[j][i] + "#";
                    }
                    stringdata += datatext + "&";
                }
                con.Close();
                #endregion 

            #region 数据提交
           if (Page.IsPostBack)     // 提交数据时
            {
                if (Request.Form["Text_valuelist"] != null)
                {
                    string Text_valuelist = Request.Form["Text_valuelist"];
                    if (Text_valuelist.Length == 0)
                    {
                        Response.Write("<script>alert('任务创建失败！请重试！')</script>");
                        Response.Write("<script>document.location=document.location;</script>");

                    }

                    else
                  {
                        if (Request.Form["type"] == "add") //添加操作
                        {
                         string valueslist = Text_valuelist.Substring(0, Text_valuelist.Length - 1);
                         string[] arrlists = valueslist.Split('&');
                         for (int w = 0; w < arrlists.Length; w++)
                            {

                                string[] arrlist = arrlists[w].Split('_');
                                string listvalue = "";
                                string listval = "";
                                for (int i = 0; i < arrlist.Length-1; i++)
                                {
                                    listvalue += '"' + arrlist[i] + '"' + ",";    //待解决问题，字符串的问题

                                }
                                listval = listvalue.Substring(0, listvalue.Length - 1).ToString();
                                listval = listval.Replace("\"", "'");
                                string columnval = "UserID," + arr_text_first.Substring(0, arr_text_first.Length - 1);
                                string name = TaskName + "【" + TaskMaker + "】";
                                string insert = " insert into [" + TaskName + "【" + TaskMaker + "】](" + columnval + ") values('" + userid + "', " + listval + " )";
                                DbHelperSQL.ExecuteSql(insert);
                            
                            }
                            string update = "update bap_task_user set State='已完成' where TaskID='" + TaskID + "' and UserID='" + userid + "'";
                            DbHelperSQL.ExecuteSql(update);
                            Response.Write("<script>alert('数据提交成功！')</script>");
                            Response.Write("<script>document.location=document.location;</script>");
                        }


                        if (Request.Form["type"] == "edite") //修改
                        {
                            string arr = Text_valuelist.Substring(0, Text_valuelist.Length - 1);
                            string dataid = Request.Form["dataid"].ToString();
                            string[] arrlist = arr.Split('&');
                            con.Open();
                            string sel_text_ = "select * from Bap_Text where TaskID='" + TaskID + "'";
                            SqlDataAdapter da_text_ = new SqlDataAdapter(sel_text_, con);
                            DataSet ds_text_ = new DataSet();
                            da_text.Fill(ds_text_);
                            string stringtext = "";
                            string[] arr_text_ = new string[100];

                            for (int j = 0; j < ds_text_.Tables[0].Rows.Count; j++)
                            {
                                stringtext += ds_text_.Tables[0].Rows[j][1].ToString() + "='" + arrlist[j] + "',"; //拼接 字段名称，在创建动态数据表的时候用
                            }
                            con.Close();
                            stringtext = stringtext.Substring(0, stringtext.Length - 1);
                            string updatedata = "update [" + TaskName + "【" + TaskMaker + "】] set " + stringtext + " where UserID='" + userid + "' and px='" + dataid + "'   ";
                            DbHelperSQL.ExecuteSql(updatedata);
                            Response.Write("<script>alert('数据修改成功！')</script>");
                            Response.Write("<script>document.location=document.location;</script>");
                        }

                        if (Request.Form["type"] == "delete")
                        {
                            string dataid = Request.Form["dataid"].ToString();
                            string updatedata = "delete from  [" + TaskName + "【" + TaskMaker + "】]  where UserID='" + userid + "' and px='" + dataid + "'   ";
                            DbHelperSQL.ExecuteSql(updatedata);
                            Response.Write("<script>alert('数据删除成功！')</script>");
                            Response.Write("<script>document.location=document.location;</script>");
                        }

                    }
                }


            }
                #endregion 
        }
    }
}

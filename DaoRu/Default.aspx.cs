using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using pojo;
using System.Threading;
using C_R;
using Maticsoft.DBUtility;

namespace JiaoShiXinXiTongJi.DaoRu
{
    public partial class Default : BasePage
    {



        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            Session["filename"] = "";
            if (FileUpload1.HasFile == false)//HasFile用来检查FileUpload是否有指定文件
            {
                Response.Write("<script>alert('请您选择Excel文件')</script> ");
                return;//当无文件时,返回
            }
            string IsXls = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();
            //System.IO.Path.GetExtension获得文件的扩展名
            if (IsXls != ".xls")
            {
                Response.Write("<script>alert('只可以选择Excel文件')</script>");
                return;//当选择的不是Excel文件时,返回
            }

            string filename = ""; filename = FileUpload1.FileName;
            Session["filename"] = filename;
            //获取Execle文件名  DateTime日期函数
            // Label2.Text = filename;
            //Server.MapPath 获得虚拟服务器相对路径
            string savePath = Server.MapPath(("~\\DaoRu\\files\\") + filename);
            //SaveAs 将上传的文件内容保存在服务器上
            FileUpload1.SaveAs(savePath);
            //调用自定义方法
            DataSet ds = new ExcelDateSet().ExcelDs(savePath);
            //定义一个DataRow数组
            DataRow[] dr = ds.Tables[0].Select();
            int rowsnum = ds.Tables[0].Rows.Count;
            if (rowsnum == 0)
            {
                Response.Write("<script>alert('Excel表为空表,无数据!')</script>");   //当Excel表为空时,对用户进行提示
            }
            else
            {
                trselet.Visible = false;
                queding.Visible = true;
                fanhui.Visible = true;
                tishitr.Visible = true;


               // Response.Write("<table width='1380px;' style=' border : yellow 2px solid;'>");
                //存放拆分之后的所有课表
                LinkedList<LinkedList<String>> lastlist = new LinkedList<LinkedList<String>>();
                //LinkedList<Object> lastlist = new LinkedList<Object>();
                //读取execl表中的每一行，放在一个list中
                for (int i = 0; i < dr.Length; i++)
                {
                    LinkedList<String> List = new LinkedList<String>();

                    List.AddFirst(dr[i][0].ToString());
                    List.AddLast(dr[i][1].ToString());
                    List.AddLast(dr[i][2].ToString());
                    List.AddLast(dr[i][3].ToString());
                    List.AddLast(dr[i][4].ToString());
                    List.AddLast(dr[i][5].ToString());
                    List.AddLast(dr[i][6].ToString());
                    List.AddLast(dr[i][7].ToString());

                    //调用自己写方法，将execl表中的数据划分好。返回的数据是一个list中包含的list成员的链表
                    LinkedList<LinkedList<String>> templist = new ExcelDateSet().ListCutter(List);
                    //将返回的list放在最后的集合中
                    foreach (LinkedList<String> obj in templist)
                    {
                        lastlist.AddLast(obj);
                    }
                }


                //将分割好的课程表打印出来
              //  Response.Write("</table>");
              //  Response.Write("<br/>");
                //拆分最后的分解好的execl表，将其写入到数据库中
                Response.Write("<table width='1380px;' style=' border : yellow 2px solid;'>");
                foreach (LinkedList<String> linklist in lastlist)
                {
                    Response.Write("<tr>");

                    //里层的每个list为一个老师的某一次课程
                    int i = 0;
                    String[] tempstr = new String[20];
                    foreach (String str in linklist)
                    {
                        tempstr[i++] = str;
                        //继续打印出来每个课程的相关信息
                        Response.Write("<td style=' border : yellow 2px solid;'>" + str + "</td>");

                    }


                    Response.Write("</tr>");
                }


                //数据的拆分，写入数据库工作完成。
                Response.Write("</table>");


            }

        }

        protected void queding_Click(object sender, EventArgs e)
        {

            int iii = 0;
            string filename = Session["filename"].ToString();
            //获取Execle文件名  DateTime日期函数
            // Label2.Text = filename;
            //Server.MapPath 获得虚拟服务器相对路径
            string savePath = Server.MapPath(("~\\DaoRu\\files\\") + filename);
            //SaveAs 将上传的文件内容保存在服务器上

            //调用自定义方法
            DataSet ds = new ExcelDateSet().ExcelDs(savePath);
            //定义一个DataRow数组
            DataRow[] dr = ds.Tables[0].Select();
            int rowsnum = ds.Tables[0].Rows.Count;

           // Response.Write("<table width='1380px;' style=' border : yellow 2px solid;'>");
            //存放拆分之后的所有课表
            LinkedList<LinkedList<String>> lastlist = new LinkedList<LinkedList<String>>();
            //LinkedList<Object> lastlist = new LinkedList<Object>();
            //读取execl表中的每一行，放在一个list中
            for (int i = 0; i < dr.Length; i++)
            {
                LinkedList<String> List = new LinkedList<String>();

                List.AddFirst(dr[i][0].ToString());
                List.AddLast(dr[i][1].ToString());
                List.AddLast(dr[i][2].ToString());
                List.AddLast(dr[i][3].ToString());
                List.AddLast(dr[i][4].ToString());
                List.AddLast(dr[i][5].ToString());
                List.AddLast(dr[i][6].ToString());
                List.AddLast(dr[i][7].ToString());

                //调用自己写方法，将execl表中的数据划分好。返回的数据是一个list中包含的list成员的链表
                LinkedList<LinkedList<String>> templist = new ExcelDateSet().ListCutter(List);
                //将返回的list放在最后的集合中
                foreach (LinkedList<String> obj in templist)
                {
                    lastlist.AddLast(obj);
                }

                foreach (LinkedList<String> linklist in lastlist)
                {

                    int j = 0;

                    String[] tempstr = new String[20];
                    foreach (String str in linklist)
                    {
                        tempstr[j++] = str;

                    }


                    Bap_Course classbean = new Bap_Course();

                    classbean.Course_Name = tempstr[0];
                    classbean.Staff_num = tempstr[1];
                    classbean.Teacher_Name = tempstr[2];
                    classbean.Department = tempstr[3];
                    classbean.Hours = tempstr[4];
                    classbean.Class_Addr = tempstr[5];
                    classbean.Classes = tempstr[6];
                    // classbean.Class_Time = tempstr[7];
                    classbean.Class_Time = tempstr[8];
                    classbean.Class_Week = tempstr[9];
                    // classbean.Startweek = tempstr[10];
                    classbean.Total_Week = tempstr[11];
                    classbean.Is_Week = tempstr[12];
                    #region 定义参数 
                    int rowsAffected1;
                    SqlParameter[] parametersinsert1 = {
                    new SqlParameter("@course_name", SqlDbType.VarChar,50),
                    new SqlParameter("@staff_num", SqlDbType.VarChar,50),
                    new SqlParameter("@teacher_name", SqlDbType.VarChar,50),
                    new SqlParameter("@department", SqlDbType.VarChar,50),
                    new SqlParameter("@hours", SqlDbType.Int),
                    new SqlParameter("@class_addr", SqlDbType.VarChar,50),
                    new SqlParameter("@classes", SqlDbType.VarChar,50),
                    new SqlParameter("@class_time", SqlDbType.VarChar,50),
                    new SqlParameter("@class_week", SqlDbType.VarChar,12),
                    new SqlParameter("@total_week", SqlDbType.VarChar,12),
                    new SqlParameter("@is_week", SqlDbType.VarChar,12)
                    };
                    parametersinsert1[0].Value = classbean.Course_Name;
                    parametersinsert1[1].Value = classbean.Staff_num;
                    parametersinsert1[2].Value = classbean.Teacher_Name;
                    parametersinsert1[3].Value = classbean.Department;
                    parametersinsert1[4].Value =Convert.ToInt32(classbean.Hours);
                    parametersinsert1[5].Value = classbean.Class_Addr;
                    parametersinsert1[6].Value = classbean.Classes;
                    parametersinsert1[7].Value = classbean.Class_Time;
                    parametersinsert1[8].Value = classbean.Class_Week;
                    parametersinsert1[9].Value = classbean.Total_Week;
                    parametersinsert1[10].Value = classbean.Is_Week;
                    #endregion

                    //try中的操作是将课程安排写到数据库中，暂时没有完成。
                    try
                    {
                       // DataBase db = new DataBase();
                        int result = DbHelperSQL.RunProcedure("sp_BapCourseInsert_DaoRu", parametersinsert1, out rowsAffected1);
                        #region 忽略
                        //String insertstr = @"insert into Bap_Course(Course_Name ,Staff_num,Teacher_Name,Department,Hours,Class_Addr,Classes,Class_Time,Class_Week,Total_Week,Is_Week)
                        //values ('" + classbean.Course_Name + "','" + classbean.Staff_num + "','" + classbean.Teacher_Name + "','" + classbean.Department + "','" + classbean.Hours + "','" + classbean.Class_Addr + "','" + classbean.Classes + "','" + classbean.Class_Time + "','" + classbean.Class_Week + "','" + classbean.Total_Week + "','" + classbean.Is_Week + "' ) ";
                        //Boolean b = db.DateExecuteSQL(insertstr);
                        #endregion
                        if (result>0)
                        {
                            iii++;
                        }
                    }
                    catch (Exception ex)       //捕捉异常
                    {
                        Response.Write("<script>alert('导入内容:" + ex.Message + "')</script>");
                    }


                }
                Response.Write("<script>alert('" + iii + "条数据导入成功!');</script>");
                trselet.Visible = true;
                queding.Visible = false;
                fanhui.Visible = false;
                Session["filename"] = "";
                //Response.Redirect("~/DaoRu/Default.aspx");
                Response.Write("<script>document.location=document.location;</script>");
                //数据的拆分，写入数据库工作完成。

            }
        }

        protected void fanhui_Click(object sender, EventArgs e)
        {
            Session["filename"] = "";
            Response.Write("<script>document.location=document.location;</script>");
        }

    }

        /// <summary>
        /// 信息提醒的方法
        /// 每隔十分钟扫描一次数据库，将当前时间和查询得到得时间相比较，如果时间的间隔小于等于半个小时则发送短信提醒
        /// 先判断登录用户是否设置了短信提醒
        /// </summary>
        /// <param name="workernum"></param>


        /// <summary>
        /// 线程运行的方法
        /// </summary>
  
    }


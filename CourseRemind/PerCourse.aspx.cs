using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C_R;
using System.Web.Script.Serialization;

namespace JiaoShiXinXiTongJi.CourseRemind
{
    public partial class PerCourse : BasePage
    {
        public string ZGXM = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // String S_name = Session["ZGBH"];
            String S_name = "15047";//测试
            ZGXM = Session["ZGXM"].ToString();
            //获取查询条件
            String name = HttpContext.Current.Request["name"];
            String value = HttpContext.Current.Request["value"];

            //默认得到当前根据职工号登陆的教师的个人课程表
            if (name == null || value == null)
            {
                name = "Staff_num";
                value = S_name;
            }

            CourseModel CourseModel = new CourseModel();

            //根据条件获得所有课程信息
            List<Bap_Course> list = CourseModel.getCourse(name, value);

            //循环遍历 输出课程表
            foreach (Bap_Course bc in list)
            {
                Label1.Text = "职工号：" + bc.Staff_num + "&nbsp;&nbsp;&nbsp;&nbsp;教师姓名：" + bc.Teacher_Name;
                Label2.Text = bc.Staff_num;
                Label3.Text = bc.Teacher_Name;
                Label4.Text = bc.Teacher_Name;
                if (bc.Class_Time == "周一")
                {
                    if (bc.Class_Week == "1,2")
                    {
                        Label1_1.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "3,4")
                    {
                        Label2_1.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "5,6")
                    {
                        Label3_1.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "7,8")
                    {
                        Label4_1.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                }
                else if (bc.Class_Time == "周二")
                {
                    if (bc.Class_Week == "1,2")
                    {
                        Label1_2.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "3,4")
                    {
                        Label2_2.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "5,6")
                    {
                        Label3_2.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "7,8")
                    {
                        Label4_2.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                }
                else if (bc.Class_Time == "周三")
                {
                    if (bc.Class_Week == "1,2")
                    {
                        Label1_3.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "3,4")
                    {
                        Label2_3.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "5,6")
                    {
                        Label3_3.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "7,8")
                    {
                        Label4_3.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                }
                else if (bc.Class_Time == "周四")
                {
                    if (bc.Class_Week == "1,2")
                    {
                        Label1_4.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "3,4")
                    {
                        Label2_4.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "5,6")
                    {
                        Label3_4.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "7,8")
                    {
                        Label4_4.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                }
                else if (bc.Class_Time == "周五")
                {
                    if (bc.Class_Week == "1,2")
                    {
                        Label1_5.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "3,4")
                    {
                        Label2_5.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "5,6")
                    {
                        Label3_5.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                    else if (bc.Class_Week == "7,8")
                    {
                        Label4_5.Text = bc.Course_Name + "<br />" + bc.Total_Week + "/" + bc.Is_Week + "<br />" + bc.Class_Addr;
                    }
                }

            }
        }
    }
}

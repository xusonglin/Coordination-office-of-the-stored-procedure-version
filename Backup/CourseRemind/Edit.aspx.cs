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
using C_R;
namespace JiaoShiXinXiTongJi.CourseRemind
{
    public partial class Edit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request["Id"];
            Id_Edit.Text = id;
            CourseModel Course_Model = new CourseModel();
            Bap_Course Bap_Course = Course_Model.getOne(id);
            Course_Name.Text = Bap_Course.Course_Name;
            Staff_num.Text = Bap_Course.Staff_num;
            Teacher_Name.Text = Bap_Course.Teacher_Name;
            Department.Text = Bap_Course.Department;
            Hours.Text = Bap_Course.Hours;
            Class_Time.Text = Bap_Course.Class_Time;
            Class_Week.Text = Bap_Course.Class_Week;
            Total_Week.Text = Bap_Course.Total_Week;
            Is_Week.Text = Bap_Course.Is_Week;
            Class_Addr.Text = Bap_Course.Class_Addr;
            Classes.Text = Bap_Course.Classes;

        }
        protected void Save_Click(object sender, EventArgs e)
        {

            CourseModel Course_Model = new CourseModel();
            Bap_Course Bap_Course = new Bap_Course();
            Bap_Course.Id = Id_Edit.Text;

            Bap_Course.Course_Name = Course_Name_Edit.Text;
            Bap_Course.Staff_num = Staff_num_Edit.Text;
            Bap_Course.Teacher_Name = Teacher_Name_Edit.Text;
            Bap_Course.Department = Department_Edit.Text;
            Bap_Course.Hours = Hours_Edit.Text;
            Bap_Course.Class_Time = Class_Time_Edit.SelectedValue;
            Bap_Course.Class_Week = Class_Week_Edit.SelectedValue;
            Bap_Course.Total_Week = Total_Week_Edit1.Text + "-" + Total_Week_Edit2.Text + "周";
            Bap_Course.Is_Week = Is_Week_Edit.SelectedValue; ;
            Bap_Course.Class_Addr = Class_Addr_Edit.Text;
            Bap_Course.Classes = Classes_Edit.Text;

            bool is_Success = Course_Model.SaveById(Bap_Course);
            if (is_Success)
            {
                Response.Redirect("List.aspx");
            }
            else
            {
                SaveInfo.Text = "修改失败...";
            }


        }
    }
}

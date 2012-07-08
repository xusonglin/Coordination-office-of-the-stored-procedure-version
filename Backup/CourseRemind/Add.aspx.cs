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
using Maticsoft.DBUtility;

namespace JiaoShiXinXiTongJi.CourseRemind
{
    public partial class Add : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string type = Request.QueryString["type"] + "";

            if (type == "edite")
            {
                string id = Request.QueryString["id"] + "";
                string sele = "select * from Bap_Course where id='"+id+"'";
                DataSet dt = DbHelperSQL.Query(sele);
                Course_Name_Edit.Text = dt.Tables[0].Rows[0]["Course_Name"] + "";

               // Course_Name_Edit.Text = dt.Tables[0].Rows[0]["Course_Name"] + "";

              //  Course_Name_Edit.Text = dt.Tables[0].Rows[0]["Course_Name"] + "";
               // Course_Name_Edit.Text = dt.Rows["0"]["Course_Name"] + "";
            
            }

        }
        //添加一条课程信息
        protected void Save_Click(object sender, EventArgs e)
        {
            CourseModel Course_Model = new CourseModel();
            Bap_Course Bap_Course = new Bap_Course();

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

            bool is_Success = Course_Model.Add(Bap_Course);
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

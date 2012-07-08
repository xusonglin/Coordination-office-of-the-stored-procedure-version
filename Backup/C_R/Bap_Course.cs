using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Bap_Course 的摘要说明
/// </summary>
/// 
namespace C_R
{
    public class Bap_Course
    {
        private String id;
        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        private String course_Name;
        public String Course_Name
        {
            get { return course_Name; }
            set { course_Name = value; }
        }

        private String staff_num;
        public String Staff_num
        {
            get { return staff_num; }
            set { staff_num = value; }
        }

        private String teacher_Name;
        public String Teacher_Name
        {
            get { return teacher_Name; }
            set { teacher_Name = value; }
        }

        private String department;
        public String Department
        {
            get { return department; }
            set { department = value; }
        }

        private String hours;
        public String Hours
        {
            get { return hours; }
            set { hours = value; }
        }

        private String class_Time;
        public String Class_Time
        {
            get { return class_Time; }
            set { class_Time = value; }
        }

        private String class_Week;
        public String Class_Week
        {
            get { return class_Week; }
            set { class_Week = value; }
        }

        private String total_Week;
        public String Total_Week
        {
            get { return total_Week; }
            set { total_Week = value; }
        }

        private String is_Week;
        public String Is_Week
        {
            get { return is_Week; }
            set { is_Week = value; }
        }

        private String class_Addr;
        public String Class_Addr
        {
            get { return class_Addr; }
            set { class_Addr = value; }
        }

        private String classes;
        public String Classes
        {
            get { return classes; }
            set { classes = value; }
        }
    }
}
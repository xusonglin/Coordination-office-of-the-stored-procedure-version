using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C_R
{
    public class Contact
    {
        private String id;
        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        private String staff_num;
        public String Staff_Num
        {
            get { return staff_num; }
            set { staff_num = value; }
        }

        private String teacher_name;
        public String Teacher_Name
        {
            get { return teacher_name; }
            set { teacher_name = value; }
        }

        private String phone_num;
        public String Phone_Num
        {
            get { return phone_num; }
            set { phone_num = value; }
        }
    }
}
using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using C_R;
using System.IO;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using Maticsoft.DBUtility;

namespace JiaoShiXinXiTongJi.CourseRemind
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class CourseRemindHandler_ : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            String sig = context.Request["sig"];
            //根据请求选择不同的方法
            switch (sig)
            {
                case "getList":
                    getList(context);
                    break;
                case "getSearch":
                    getSearch(context);
                    break;
                case "getClass":
                    getClass(context);
                    break;
                case "getSend":
                    getSend(context);
                    break;
                case "deletedata":
                    string strResult=deletedata(context);
                     context.Response.Write(strResult);

                    break;
                default:
                    context.Response.Write("Erorr 404, Not Found ! ");
                    break;
            }
        }
        //删除数据记录
        private string  deletedata(HttpContext context)
        {
            string ids = context.Request["ids"];
            string dele="delete from Bap_Course where id in ("+ids+") ";
            return DbHelperSQL.ExecuteSql(dele) > 0 ? "true" : "false";


        }
        //获取所有课程信息
        public void getList(HttpContext context)
        {
            HttpResponse response = context.Response;
            int page = Int32.Parse(context.Request["page"]);
            int size = Int32.Parse(context.Request["rows"]);
            String json = "";
            CourseModel coursemodel = new CourseModel();
            List<Bap_Course> list = coursemodel.list(page, size);
            int total = coursemodel.count();
            response.ContentEncoding = UTF8Encoding.UTF8;
            response.ContentType = "text/plain";
            json = "{\"total\":" + total + ",\"rows\":" + new JavaScriptSerializer().Serialize(list) + "}";
            response.Output.Write(json);
        }

        //根据条件获取搜索结果
        public void getSearch(HttpContext context)
        {
            int page = Int32.Parse(context.Request["page"]);
            int size = Int32.Parse(context.Request["rows"]);
            String name = context.Request["name"];
            String value = context.Request["value"];
            CourseModel coursemodel = new CourseModel();
            List<Bap_Course> list = coursemodel.Search(name, value,page,size);
            int total = coursemodel.SearchCount(name, value);
            context.Response.ContentEncoding = UTF8Encoding.UTF8;
            context.Response.ContentType = "text/json";
            String json = "{\"total\":" + total + ",\"rows\":" + new JavaScriptSerializer().Serialize(list) + "}";
            context.Response.Output.Write(json);
        }
        public void getClass(HttpContext context)
        {
            
            //int page = Int32.Parse(context.Request["page"]);
            //int size = Int32.Parse(context.Request["rows"]);
         //   String name = "李光敏";
            String name = context.Request["ZGXM"];
            String value = context.Request["Staff_Num"];
            CourseModel coursemodel = new CourseModel();
            List<Bap_Course> list = coursemodel.Search(name, value,1,10);
            String json = "[";
            foreach (Bap_Course bc in list)
            {
                json += "{ \"Id\":\"" + bc.Id + "\",\"Class_Time\":\"" + bc.Class_Time + " " + bc.Course_Name + " " + bc.Class_Week + "节课 " + bc.Class_Addr + "\"},";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            context.Response.Output.Write(json);
        }
        //发送短信
        public void getSend(HttpContext context)
        {
            //webchinese.cn 网站用户名  需要注册
            String uid = "ouyangyun";

            //webchinese.cn 网站 借口安全码
            String key = "a0e3256c578e41e5986f";

            String Staff_Num = context.Request["Staff_Num"];
            CourseModel coursemodel = new CourseModel();
            List<Contact> PhoneNum = coursemodel.getPhoneNum(Staff_Num);
            //联系人手机号码

            string selnum = "select tel from  bap_user where ZGBH='" + Staff_Num + "' ";
            string num = DbHelperSQL.GetSingle(selnum)+"";
            //String Tel_Num = "15629597701";
            String Tel_Num = num;
            String r_msg = "短信发送失败！";
            if (Tel_Num == "")
            { r_msg = "对不起，用户未设置手机号！"; }
            else
            {
                //短信内容
                //String Sms_Cont = context.Request["sms"];

                //String url = "http://utf8.sms.webchinese.cn/?Uid=" + uid + "&Key=" + key + "&smsMob=" + Tel_Num + "&smsText=" + Sms_Cont;
                //String result = GetHtmlFromUrl(url);
               
                //if (Convert.ToInt32(result) > 0)
                //{
                //    r_msg = "短信发送成功！";
                //}
            }
            context.Response.Output.Write(r_msg);

        }

        //模拟HTTP请求 实现短信发送接口
        public string GetHtmlFromUrl(string url)
        {
            string strRet = null;
            if (url == null || url.Trim().ToString() == "")
            {
                return strRet;
            }
            string targeturl = url.Trim().ToString();
            try
            {
                System.Net.HttpWebRequest hr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(targeturl);
                hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                hr.Method = "GET";
                hr.Timeout = 30 * 60 * 1000;
                System.Net.WebResponse hs = hr.GetResponse();
                Stream sr = hs.GetResponseStream();
                StreamReader ser = new StreamReader(sr, Encoding.Default);
                strRet = ser.ReadToEnd();
            }
            catch (Exception ex)
            {
                strRet = null;
            }
            return strRet;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

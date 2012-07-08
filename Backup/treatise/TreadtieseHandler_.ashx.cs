using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using DAO;
using System.Collections.Generic;
using pojo;
using System.Web.Script.Serialization;
using System.Text;
using System.Web.SessionState;
using System.IO;
using Maticsoft.DBUtility;

namespace JiaoShiXinXiTongJi.treatise
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TreadtieseHandler_ : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string sig = context.Request.Params["sig"];//获得提交的标记参数，以便程序知道调用哪个方法
            string ZGBH = context.Request.Params["ZGBH"];
            string ZGXM = context.Request.Params["ZGXM"];
          


            switch (sig)
            {
                case "websiteList":
                    getWebSite(context);
                    break;
                case "searchTreatise":
                    searchTreatise(context);
                    break;
                case "insert":
                    insert(context);
                    break;
                case "exist":
                    checkTreatise(context);
                    break;
                case "insertXPath":
                    insertXPath(context);
                    break;
                case "mytreatise":
                    myTreatise(context, ZGXM, ZGBH);
                    break;
                case "pathList":
                    pathList(context);
                    break;
                case "forbid":
                    forbidXPath(context);
                    break;
                case "updateXPath":
                    updateXPath(context);
                    break;
                case "Delete":
                    string strResult = Delete(context);
                    context.Response.Write(strResult);
                    break;
                default:
                    context.Response.Write("The request url as aot available!Please make sure that you request url is available.");
                    break;
            }
        }
        /// <summary>
        /// 获得所有网站链接
        /// </summary>
        /// <param name="context"></param>
        private void getWebSite(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.ContentEncoding = UTF8Encoding.UTF8;
            response.ContentType = "text/json";
            XPathDAO pathDAO = new XPathDAO();
            List<Xpath> webList = pathDAO.webSiteList();
            response.Output.Write(new JavaScriptSerializer().Serialize(webList));
        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="context"></param>
        private string  Delete(HttpContext context)
        {
          string ids = context.Request.Params["ids"];
          string dele = "delete from treatise where id in (" + ids + ")";
          return DbHelperSQL.ExecuteSql(dele) > 0 ? "true" : "false";
          

        }



        /// <summary>
        /// 自动搜索文章
        /// </summary>
        /// <param name="context"></param>
        private void searchTreatise(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            response.ContentEncoding = UTF8Encoding.UTF8;
            response.ContentType = "text/json";
            XPathDAO pathDAO = new XPathDAO();
            string id = request.Params["id"];
            string url = request.Params["treatiseURL"];
            TreatiseDAO treatiseDAO = new TreatiseDAO();
            if (treatiseDAO.exist(new Guid(id), url))
            {//判断用户搜索的文章在数据库中是否存在，若存在，则不再进行搜索，并提醒用户，以减少系统不必要的开销
                response.ContentType = "text/html";
                response.Output.Write("EXIST:您搜索的文章在我们的数据中已经存在，请直接返回到搜索功能区进行搜索！");
                return;
            }
            Xpath path = pathDAO.getInfo(new Guid(id));
           // if (url.Contains(path.Baseurl))
           // {//如果文章的链接和网站的基本链接不匹配，则向客户输入错误信息
            //    response.ContentType = "text/html";
           //     response.Output.Write("error");
            //    return;
           // }
            Treatise treatise = new Util().captureTreatise(path, url);
            response.Output.Write(new JavaScriptSerializer().Serialize(treatise));//new JavaScriptSerializer().Serialize(path)
        }

        /// <summary>
        /// 插入一条数据到treatise数据表
        /// </summary>
        private void insert(HttpContext context)
        {
            Treatise treatise = new Treatise();
            HttpRequest request = context.Request;
            Util util = new Util();
            util.loadParams(context, treatise);//获得表单提交的数据
            treatise.Sourceid =new Guid(request.Params["webId"]);
            treatise.Articleurl = request.Params["weburl"];
            XPathDAO pathDAO = new XPathDAO();
            Xpath path = pathDAO.getInfo(treatise.Sourceid);
            treatise.Language = treatise.Language.Length == 0 ? path.Weblanguage : treatise.Language;
            TreatiseDAO treatiseDAO = new TreatiseDAO();
            String notice = treatiseDAO.insert(treatise) ? "论文添加成功" : "论文添加失败";
            context.Response.Write(notice);
        }

        /// <summary>
        /// 查检是否存在相同标题和作者的文章
        /// </summary>
        /// <param name="context"></param>
        private void checkTreatise(HttpContext context)
        {
            HttpRequest request = context.Request;
            TreatiseDAO treatiseDAO = new TreatiseDAO();
            string cntitle = request.Params["cntitle"];
            string entitle = request.Params["entitle"];
            string author = request.Params["author"];
            if (treatiseDAO.exist(cntitle, author) || treatiseDAO.exist(entitle, author))
            {
                context.Response.Output.Write("EXIST");
            }
            else
            {
                context.Response.Output.Write("NOTEXIST");
            }
        }

        /// <summary>
        /// 插入一条网站的抓取规则到数据库
        /// </summary>
        /// <param name="context"></param>
        private void insertXPath(HttpContext context)
        {
            Xpath path = new Xpath();
            Util util = new Util();
            util.loadParams(context, path);//获取实体类
            XPathDAO pathDAO = new XPathDAO();
            string notice = pathDAO.insert(path) ? "添加成功" : "添加失败";
            context.Response.Output.Write(notice);
        }

        /// <summary>
        /// 获得相关用户的所有论文
        /// </summary>
        /// <param name="contect"></param>
        private void myTreatise(HttpContext context, string zgxm, string zgbh)
        {
            //string zgxm = context.Session["ZGXM"].ToString().Trim();
            //string zgbh = context.Session["ZGBH"].ToString().Trim();
            if (zgbh != null)
            {
                if (zgbh.Contains("admin"))
                {//如果为管理员，则将zgxm变为空，这样就能拿到所有的数据
                    zgxm = "";
                }
                TreatiseDAO treatiseDAO = new TreatiseDAO();
                int page = Int32.Parse(context.Request["page"]);
                int size = Int32.Parse(context.Request["rows"]);
                List<Treatise> list = treatiseDAO.list(page, size, zgxm);
                int total = treatiseDAO.count(zgxm);
                HttpResponse response = context.Response;
                response.ContentEncoding = UTF8Encoding.UTF8;
                response.ContentType = "text/json";
                string json = "{\"total\":" + total + ",\"rows\":" + new JavaScriptSerializer().Serialize(list) + "}";//将数据拼装成jquery easy-ui规定的JSON格式
                response.Output.Write(json);
            }
        }

        /// <summary>
        /// 分页获得所有的抓取规则列表
        /// </summary>
        /// <param name="context"></param>
        private void pathList(HttpContext context)
        {
            XPathDAO pathDAO = new XPathDAO();
            string page = context.Request["page"];
            string size = context.Request["size"];
            if (page == null)
            {
                page = "1";
            }
            if (size == null)
            {
                size = "20";
            }
            List<Xpath> pathList = pathDAO.pathList(Int32.Parse(page), Int32.Parse(size));
            long total = pathDAO.count();
            HttpResponse response = context.Response;
            response.ContentEncoding = UTF8Encoding.UTF8;
            response.ContentType = "text/json";
            string json = "{\"total\":" + total + ",\"rows\":" + new JavaScriptSerializer().Serialize(pathList) + "}";//将数据拼装成jquery easy-ui规定的JSON格式
            response.Output.Write(json);

        }

        /// <summary>
        /// 禁用网站规则
        /// 说明：此处接收JSON格式字符串转换为list时得到结果不正确
        /// @version:2012-3-20 19:30
        /// </summary>
        /// <param name="context"></param>
        private void forbidXPath(HttpContext context)
        {
            string idsJson = context.Request["ids"];//获得页面提交过来的JSON格式的字符串
            string isaccept = context.Request["isaccept"];
            int accept = 0;
            switch (isaccept)
            {
                case "accept":
                    accept = 1;
                    break;
                case "forbid":
                    accept = 0;
                    break;
                default:
                    accept = 0;
                    break;
            }
            string temp = accept == 0 ? "禁用" : "启用";
            List<Ids> idList = new JavaScriptSerializer().Deserialize<List<Ids>>(idsJson);//反序列化JSON格式的字符串为指定类型
            string ids = new Util().listToString(idList);//将list中的ID转换为string型的字符串
            Boolean update = new XPathDAO().update(ids, accept);
            string notice = update ? temp + idList.Count + "条规则成功！" : temp + idList.Count + "条规则失败！";
            context.Response.Output.Write(notice);
        }

        /// <summary>
        /// 更新xpath的数据
        /// </summary>
        /// <param name="context"></param>
        public void updateXPath(HttpContext context)
        {
            Util util = new Util();
            Xpath path = new Xpath();
            util.loadParams(context, path);//获得表单的数据
            string notice = "-";
            if (path.Id != null)
            {
                XPathDAO pathDAO = new XPathDAO();
                Boolean update = pathDAO.update(path);
                notice = update ? "更新成功" : "更新失败";
            }
            context.Server.Transfer("onePath.aspx?Id=" + path.Id + "&notice=" + notice, true);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pojo;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
//using System.Runtime.Serialization.Json;
using System.Reflection;
/// <summary>
/// 论文部分用到的工具类
/// author 张浩春
/// time 2012-3-24
/// </summary>
public class Util
{
	public Util()
	{
	
	}

    public Treatise captureTreatise(Xpath path, string url)
    {
        Treatise treatise = new Treatise();
        HtmlDocument document = new HtmlDocument();
        WebRequest request = WebRequest.Create(url);
        WebResponse response = request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
        document.Load(reader);
        HtmlNode navNode = document.DocumentNode;
        HtmlNodeCollection nodeList = null;
        int sig = 0;
        if (path.Cntitle != null && path.Cntitle.Length > 0)
        {//中文标题
            nodeList = navNode.SelectNodes(path.Cntitle);
            sig = 0;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    sig++;
                    treatise.Cntitle = treatise.Cntitle + (sig > 1 ? "，" : "") + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        if (path.Entitle != null && path.Entitle.Length > 0)
        {//英文标题
            nodeList = navNode.SelectNodes(path.Entitle);
            sig = 0;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    sig++;
                    treatise.Entitle = treatise.Entitle + (sig > 1 ? "，" : "") + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        if (path.Unit != null && path.Unit.Length > 0)
        {//单位名称
            nodeList = navNode.SelectNodes(path.Unit);
            sig = 0;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    sig++;
                    treatise.Unitname = treatise.Unitname + (sig > 1 ? "，" : "") + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        if (path.Types != null && path.Types.Length > 0)
        {//文理科类型
            nodeList = navNode.SelectNodes(path.Types);
            sig = 0;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    sig++;
                    treatise.Types = treatise.Types + (sig > 1 ? "，" : "") + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        if (path.Author != null && path.Author.Length > 0)
        {//作者
            nodeList = navNode.SelectNodes(path.Author);
            sig = 0;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    sig++;
                    treatise.Author = treatise.Author + (sig > 1 ? "，" : "") + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        if (path.Articletype != null && path.Articletype.Length > 0)
        {//论文类型
            nodeList = navNode.SelectNodes(path.Articletype);
            sig = 0;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    sig++;
                    treatise.Articletype = treatise.Articletype + (sig > 1 ? "，" : "") + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        if (path.Source != null && path.Source.Length > 0)
        {//课题来源
            nodeList = navNode.SelectNodes(path.Source);
            sig = 0;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    sig++;
                    treatise.Source = treatise.Source + (sig > 1 ? "，" : "") + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        if (path.Language != null && path.Language.Length > 0)
        {//论文语言类型
            nodeList = navNode.SelectNodes(path.Language);
            sig = 0;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    sig++;
                    treatise.Language = treatise.Language + (sig > 1 ? "，" : "") + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        if (path.Journalname != null && path.Journalname.Length > 0)
        {//期刊名称
            nodeList = navNode.SelectNodes(path.Journalname);
            sig = 0;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    sig++;
                    treatise.Journalname = treatise.Journalname + (sig > 1 ? "，" : "") + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        if (path.Publishtime != null && path.Publishtime.Length > 0)
        {//论文发表时间
            nodeList = navNode.SelectNodes(path.Publishtime);
            sig++;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    treatise.Publishtime = treatise.Publishtime + (sig > 1 ? "，" : "") + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        if (path.Levels != null && path.Levels.Length > 0)
        {//论文级别
            nodeList = navNode.SelectNodes(path.Levels);
            sig = 0;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    sig++;
                    treatise.Levels = treatise.Levels + " " + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        if (path.Recordtype != null && path.Recordtype.Length > 0)
        {//收录类型
            nodeList = navNode.SelectNodes(path.Recordtype);
            sig = 0;
            if (nodeList != null)
            {
                foreach (HtmlNode node in nodeList)
                {
                    sig++;
                    treatise.Recordtype = treatise.Recordtype + " " + htmlReplace(node.InnerText);
                }
            }
            nodeList = null;
        }
        reader.Close();//关闭流
        return treatise;
    }

    /// <summary>
    /// 替换字符串中特殊的字符
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string htmlReplace(string text)
    {
        text = replaceAll(text, "\r", "");
        text = replaceAll(text, "\n", "");
        text = replaceAll(text, "；", " ");//替换掉所有的中文分号
        text = replaceAll(text, "，", " ");//替换掉所有的中文分号
        text = replaceAll(text, "&nbsp;", " ");
        return Regex.Replace(text.Trim(), "\\s+", " ");//替换一个或多个空格为一个空格
    }

    /// <summary>
    /// 替换掉XPath中不符合W3C标准的标签，例如：tbody
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string replaceNotExistLable(string text)
    {
        text = replaceAll(text, "tbody/", "");
        text = replaceAll(text, "thead/", "");
        return text;
    }

    /// <summary>
    /// 替换字符串中指定所有的字符串为目标字符串
    /// </summary>
    /// <param name="src">需要替换的字符串</param>
    /// <param name="oldString">需要替换的字符</param>
    /// <param name="targetString">替换目标字符</param>
    /// <returns></returns>
    public string replaceAll(string src, string oldString, string targetString)
    {
        while (src.Contains(oldString))
        {
            src = src.Replace(oldString, targetString);
        }
        return src;
    }

    /// <summary>
    /// 通过反射来实现自动提取表单内容并存入实体类
    /// </summary>
    /// <param name="context"></param>
    /// <param name="bean"></param>
    public void loadParams(HttpContext context, object bean)
    {
        HttpRequest request = context.Request;
        NameValueCollection paramCollection = request.Form;
        Type cls = bean.GetType();
        PropertyInfo property = null;
        string proName = "", proTypeName = "";
        foreach (string paramName in paramCollection)
        {
            if (paramName != "")
            {
                proName = paramName.Substring(0, 1).ToUpper() + paramName.Substring(1);//获得实体类中的属性名称
                property = cls.GetProperty(proName);
                if (property != null)
                {
                    proTypeName = property.PropertyType.Name.ToLower();
                    string value = replaceNotExistLable(request.Params[paramName]);
                    switch (proTypeName)
                    {//将值转换为该属性的类型，暂时这样处理，应由反射来实现
                        case "int":
                            if (value != null && value.Length > 0)
                                property.SetValue(bean, int.Parse(value), null);
                            break;
                        case "int32":
                            if (value != null && value.Length > 0)
                                property.SetValue(bean, int.Parse(value), null);
                            break;
                        case "double":
                            if (value != null && value.Length > 0)
                                property.SetValue(bean, double.Parse(value), null);
                            break;
                        case "bool":
                            if (value != null && value.Length > 0)
                                property.SetValue(bean, bool.Parse(value), null);
                            break;
                        case "float":
                            if (value != null && value.Length > 0)
                                property.SetValue(bean, float.Parse(value), null);
                            break;
                        case "datetime":
                            if (value != null && value.Length > 0)
                                property.SetValue(bean, DateTime.Parse(value), null);
                            break;
                        case "guid":
                            if (value != null && value.Length > 0)
                                property.SetValue(bean, new Guid(value), null);
                            break;
                        default:
                            if (value != null && value.Length > 0)
                                property.SetValue(bean, value, null);
                            break;

                    }
                    property = null;
                }
            }

        }
    }

    /// <summary>
    /// 将list 中的ID转换为一个字符串类型，便于批量操作，形如：1,2,3
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public string listToString(List<Ids> list)
    {
        string ids = "";
        int sig = 0;
        List<Ids>.Enumerator e = list.GetEnumerator();
        while (e.MoveNext())
        {
            if (sig < list.Count - 1)
            {
                ids += "'" + e.Current.Id + "',";
            }
            else
            {
                ids += "'" + e.Current.Id + "'";
            }
            sig++;
        }
        return ids;
    }
   
}
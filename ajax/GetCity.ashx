<%@ WebHandler Language="C#" Class="GetCity" %>

using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;

public class GetCity : IHttpHandler
{
    private List<CityInfo> listCity = null;

    private string result = string.Empty;

    private string temp = string.Empty;

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        CityInfo ci = new CityInfo();

        listCity = ci.GetAll();
        if (listCity.Count > 0)
        {
            result = "[";
            int count = listCity.Where(n => n.ParentId == string.Empty).ToList().Count;
            foreach (CityInfo city in listCity.Where(n => n.ParentId == string.Empty))
            {
                temp = string.Empty;
                result += "{\"id\":\"" + city.CityId + "\",\"text\":\"" + city.CityName + "\",\"attributes\":{\"url\":\"" + city.Url + "\"}";
                if (listCity.Where(n => n.ParentId == city.CityId).ToList().Count > 0)
                    result += ",\"children\":" + GetObject(city.CityId, string.Empty);
                result += "},";
            }
            result = result.TrimEnd(',');
            result += "]";
        }
        context.Response.Write(result);
    }

    /// <summary>
    /// 递归获取子级
    /// </summary>
    private string GetObject(string ParentId, string Array)
    {
        temp += "[";
        foreach (CityInfo city in listCity.Where(n => n.ParentId == ParentId))
        {
            temp += "{\"id\":\"" + city.CityId + "\",\"text\":\"" + city.CityName + "\",\"attributes\":{\"url\":\"" + city.Url + "\"}";
            if (listCity.Where(n => n.ParentId == city.CityId).ToList().Count > 0)
            {
                temp = temp + ",\"children\":";
                string abc = GetObject(city.CityId, temp);
                temp = abc;
            }
            temp += "},";
        }
        temp = temp.TrimEnd(',');
        temp += "]";
        return temp;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

/// <summary>
///CityInfo 的摘要说明
/// </summary>
public class CityInfo
{
    public CityInfo()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public string CityId { get; set; }

    public string CityName { get; set; }

    public string ParentId { get; set; }

    public string Url { get; set; }


    /// <summary>
    /// 创建数据集
    /// </summary>
    public List<CityInfo> GetAll()
    {
        List<CityInfo> listCity = new List<CityInfo>();

        // 创建一级菜单
        CityInfo city1 = new CityInfo()
        {
            CityId = "01",
            CityName = "湖南省",
            ParentId = string.Empty,
            Url = "http://www.yahoo.com"
        };
        listCity.Add(city1);
        // 创建二级菜单
        CityInfo city1_1 = new CityInfo()
        {
            CityId = "0101",
            CityName = "长沙市",
            ParentId = "01",
            Url = "http://www.51.com"
        };
        listCity.Add(city1_1);
        CityInfo city1_2 = new CityInfo()
        {
            CityId = "0102",
            CityName = "湘潭市",
            ParentId = "01",
            Url = "http://www.baidu.com"
        };
        listCity.Add(city1_2);
        CityInfo city1_3 = new CityInfo()
        {
            CityId = "0103",
            CityName = "株洲市",
            ParentId = "01",
            Url = "http://www.baidu.com"
        };
        listCity.Add(city1_3);
        CityInfo city1_4 = new CityInfo()
        {
            CityId = "0104",
            CityName = "怀化市",
            ParentId = "01",
            Url = "http://www.baidu.com"
        };
        listCity.Add(city1_4);
        // 创建三级菜单
        CityInfo city1_4_1 = new CityInfo()
        {
            CityId = "010401",
            CityName = "辰溪县",
            ParentId = "0104",
            Url = "http://www.baidu.com"
        };
        listCity.Add(city1_4_1);
        // 创建四级菜单
        CityInfo city1_4_1_1 = new CityInfo()
        {
            CityId = "01040101",
            CityName = "黄溪口镇",
            ParentId = "010401",
            Url = "http://www.baidu.com"
        };
        listCity.Add(city1_4_1_1);
        // 创建五级菜单
        CityInfo city1_4_1_1_1 = new CityInfo()
        {
            CityId = "0104010101",
            CityName = "苏木溪瑶族乡",
            ParentId = "01040101",
            Url = "http://www.cnwzh.com"
        };
        listCity.Add(city1_4_1_1_1);
        CityInfo city1_4_1_1_2 = new CityInfo()
        {
            CityId = "0104010102",
            CityName = "罗子山瑶族乡",
            ParentId = "01040101",
            Url = "http://www.baidu.com"
        };
        listCity.Add(city1_4_1_1_2);
        CityInfo city1_4_2 = new CityInfo()
        {
            CityId = "010402",
            CityName = "溆浦县",
            ParentId = "0104",
            Url = "http://www.baidu.com"
        };
        listCity.Add(city1_4_2);
        // 创建一级菜单
        CityInfo city2 = new CityInfo()
        {
            CityId = "02",
            CityName = "安徽省",
            ParentId = string.Empty,
            Url = "http://www.baidu.com"
        };
        listCity.Add(city2);
        // 创建二级菜单
        CityInfo city2_1 = new CityInfo()
        {
            CityId = "0201",
            CityName = "合肥市",
            ParentId = "02",
            Url = "http://www.baidu.com"
        };
        listCity.Add(city2_1);
        CityInfo city2_2 = new CityInfo()
        {
            CityId = "0202",
            CityName = "六安市",
            ParentId = "02",
            Url = "http://www.baidu.com"
        };
        listCity.Add(city2_2);
        return listCity;
    }
}
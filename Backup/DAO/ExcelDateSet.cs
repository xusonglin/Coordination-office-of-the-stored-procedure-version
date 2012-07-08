using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using System.Net;
using System.IO;
using System.Text;

/// <summary>
///ExcelDateSet 的摘要说明
/// </summary>
public class ExcelDateSet
{
    public ExcelDateSet()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 读取excel表中的数据放在dataset中,只读取第一个工作表中的数据
    /// </summary>
    /// <param name="Path"></param>
    /// <returns></returns>
    public DataSet ExcelDs(string Path)
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
        OleDbConnection conn = new OleDbConnection(strConn);
        conn.Open();
        string strExcel = "";
        OleDbDataAdapter myCommand = null;
        DataSet ds = null;
        strExcel = "select * from [sheet1$]";
        myCommand = new OleDbDataAdapter(strExcel, strConn);
        ds = new DataSet();
        myCommand.Fill(ds, "table1");
        return ds;
    }
    /// <summary>
    /// 将excel表中的一行，当做一个list传过来，然后返回分割好的详细的课程安排
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public LinkedList<LinkedList<String>> ListCutter(LinkedList<String> list)
    {
        LinkedList<LinkedList<String>> lastlist = new LinkedList<LinkedList<String>>();

        String classtime = list.Last();
        list.RemoveLast();
        String classes = list.Last();
        list.RemoveLast();
        String adds = list.Last();
        list.RemoveLast();
        //表示只有一个班级的课
        if (!classtime.Contains(";"))
        {
            list.AddLast(adds);
            list.AddLast(classes);
            list.AddLast(classtime);
            //当上课时间部位空时需要分割
            if (classtime.Contains("第"))
            {
                //上课时间还要划分
                LinkedList<String> tl = CutClassTime(classtime);
                foreach (String str in tl)
                {
                    list.AddLast(str);
                }
            }
            lastlist.AddLast(list);
            return lastlist;
        }
        else
        {
            //一行存在多个上课时间的时候
            //将classtime拆分
            String[] ct = new String[10] { "", "", "", "", "", "", "", "", "", "" };
            if (classtime.Contains(";"))
            {
                int i = 0;
                for (i = 0; classtime.Contains(";"); i++)
                {
                    //获取最后一个；后面的字符串
                    ct[i] = classtime.Substring(classtime.LastIndexOf(";") + 1);
                    //获得最后一个；前面的字符串,从第0个开始，向后数n个
                    classtime = classtime.Substring(0, classtime.LastIndexOf(";"));
                }
                ct[i] = classtime;
            }
            else
            {
                ct[0] = classtime;
            }
            //将classes拆分
            String[] cs = new String[10] { "", "", "", "", "", "", "", "", "", "" };
            if (classes.Contains(","))
            {
                //将classes拆分
                int j = 0;
                for (j = 0; classes.Contains(","); j++)
                {
                    cs[j] = classes.Substring(classes.LastIndexOf(",") + 1);
                    classes = classes.Substring(0, classes.LastIndexOf(","));
                }
                cs[j] = classes;
            }
            else
            {
                cs[0] = classes; cs[1] = classes; cs[2] = classes; cs[3] = classes; cs[4] = classes;
            }
            //将adds进行拆分
            String[] ad = new String[10] { "", "", "", "", "", "", "", "", "", "" };
            if (adds.Contains(";"))
            {
                int k = 0;
                for (k = 0; adds.Contains(";"); k++)
                {
                    ad[k] = adds.Substring(adds.LastIndexOf(";") + 1);
                    adds = adds.Substring(0, adds.LastIndexOf(";"));
                }
                ad[k] = adds;
            }
            else
            {
                ad[0] = adds; ad[1] = adds; ad[2] = adds; ad[3] = adds; ad[4] = adds;
            }
            //将拆开的结果放在lastlist中
            int n = 0;
            for (n = 0; n < 8; n++)
            {
                if (!"".Equals(ct[n]))
                {
                    //防止list的值在操作时发生变化，重新定义list并进行赋值
                    LinkedList<String> li = new LinkedList<string>();
                    foreach (String tp in list)
                    {
                        li.AddLast(tp);
                    }
                    //如果ct中有值时，表示其是一条有效数据，将相应的cs，ad放在一起
                    li.AddLast(ad[n]);
                    li.AddLast(cs[n]);
                    li.AddLast(ct[n]);
                    //这里的ct如果不为空（上课时间还应该做一定的分割）
                    if (ct[n].Contains("第"))
                    {
                        LinkedList<String> tl = CutClassTime(ct[n]);
                        foreach (String str in tl)
                        {
                            li.AddLast(str);
                        }
                    }
                    //将list放在最后返回的数组中
                    lastlist.AddLast(li);
                }
                else
                {
                    //ct中没有值，表示后面的数据不存在，可以返回了；
                    break;
                }
            }
        }
        return lastlist;
    }
    /// <summary>
    /// 将传过来的classtime进行分割，分割为周几，第几次课，第几周，单双周
    /// </summary>
    /// <param name="ct">参数为上课的时间安排</param>
    /// <returns></returns>
    public LinkedList<String> CutClassTime(String ct)
    {
        LinkedList<String> list = new LinkedList<String>();
        //前两个字符为周几——三，五___________周四第1,2节{第1-19周|单周}
        String weekday = ct.Substring(0, 2);
        //改变ct的值，————实现：第1,2节{第1-19周|单周}
        ct = ct.Substring(2);
        //第二个元素为第几次课——1，2  or 3，4
        String classes = ct.Substring(1, 3);
        //weektemp暂时存放第几周到第几周_____第1-19周|每周
        ct = ct.Substring(ct.LastIndexOf("{") + 1, (ct.LastIndexOf("}") - ct.LastIndexOf("{")));
        //单周（0），双周（2），每周（1）
        String doubleweek = "每周";
        //给单双周赋值,并将剩余的weektemp的值，修改为__________第1-19周
        if (ct.Contains("单"))
        {
            doubleweek = "单周";
            ct = ct.Substring(0, ct.LastIndexOf("|"));
        }
        else if (ct.Contains("双"))
        {
            doubleweek = "双周";
            ct = ct.Substring(0, ct.LastIndexOf("|"));
        }
        //修改后ct的值为_________第1-19周
        //第几周开始
        String startweek = ct.Substring(1, ct.LastIndexOf("-") - 1);
        //第几周结束
        String endweek = startweek +"-"+ ct.Substring(ct.LastIndexOf("-") + 1, ct.LastIndexOf("周") - ct.LastIndexOf("-") - 1)+"周";

        list.AddLast(weekday);
        list.AddLast(classes);
        list.AddLast(startweek);
        list.AddLast(endweek);
        list.AddLast(doubleweek);

        return list;
    }

    /// <summary>
    /// 需要发送短信时调用，调用时只需要把拼成的URL传给该函数即可。判断返回值即可
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
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
            HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
            hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
            hr.Method = "GET";
            hr.Timeout = 30 * 60 * 1000;
            WebResponse hs = hr.GetResponse();
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

}
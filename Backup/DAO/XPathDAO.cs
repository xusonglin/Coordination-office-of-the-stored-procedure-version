using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DAO;
using pojo;
using Maticsoft.DBUtility;
/// <summary>
/// 添加xpath用到的DAO层
/// author 张浩春
/// time 201-3-24
/// </summary>

namespace DAO {
    public class XPathDAO {
        /// <summary>
        /// 插入一条数据到数据库
        /// </summary>
        /// <param name="path">Xpath的对象</param>
        /// <returns></returns>
        public Boolean insert(Xpath path) {
            Boolean insert = false;
            DatabaseAccess database = new DatabaseAccess();
            String sql = "insert into xpath(id,websitename,weblanguage,baseurl,cntitle,entitle,unit,types,author,articletype,source,language,journalname,publishtime,levels,recordtype,forbid) values(NewId(),'"
                + path.Websitename + "','" + path.Weblanguage + "','" + path.Baseurl + "','" + path.Cntitle + "','" + path.Entitle + "','" + path.Unit + "','" + path.Types + "','" + path.Author + "','" + path.Articletype + "','" + path.Source + "','" + path.Language + "','" + path.Journalname + "','" + path.Publishtime + "','" + path.Levels + "','" + path.Recordtype + "',0)";
            try {
                insert = database.executeUpdate(sql);
            } catch (Exception e) {
                throw new Exception(e.Message);
            } finally {
                database.close();
            }
            return insert;
        }

        /// <summary>
        /// 获得某一个xpath的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Xpath getInfo(Guid id) {
            Xpath path = new Xpath();
            DatabaseAccess database = new DatabaseAccess();
            String sql = "select * from xpath where id='" + id + "'";
            try {
                SqlDataReader data = database.executeQuery(sql);
                if (data.Read()) {
                    path.Id = data.GetGuid(0);
                    path.Websitename = data.GetString(1);
                    path.Weblanguage = data.GetString(2);
                    path.Baseurl = data.GetString(3);
                    path.Cntitle = data.GetString(4);
                    path.Entitle = data.GetString(5);
                    path.Unit = data.GetString(6);
                    path.Types = data.GetString(7);
                    path.Author = data.GetString(8);
                    path.Articletype = data.GetString(9);
                    path.Source = data.GetString(10);
                    path.Language = data.GetString(11);
                    path.Journalname = data.GetString(12);
                    path.Publishtime = data.GetString(13);
                    path.Levels = data.GetString(14);
                    path.Recordtype = data.GetString(15);
                    path.Forbid = (int)data["forbid"];
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            } finally {
                database.close();
            }
            return path;
        }

        /// <summary>
        /// 获得所有已经添加到数据库的论文网站
        /// </summary>
        /// <returns></returns>
        public List<Xpath> webSiteList() {
            List<Xpath> webList = new List<Xpath>();
            DatabaseAccess database = new DatabaseAccess();
            String sql = "select id,websitename,baseurl from xpath where forbid=1";
            try {
                SqlDataReader result = database.executeQuery(sql);
                while (result.Read()) {
                    Xpath path = new Xpath();
                    path.Id = result.GetGuid(0);
                    path.Baseurl = result.GetString(2);
                    path.Websitename = result.GetString(1) + "(" + path.Baseurl + ")";
                    webList.Add(path);
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            } finally {
                database.close();
            }
            return webList;
        }

        /// <summary>
        /// 获得抓取规则的所有数据
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="size">每页显示的数据</param>
        /// <returns></returns>
        public List<Xpath> pathList(int page,int size) {
            List<Xpath> pathList = new List<Xpath>();
            DatabaseAccess database = new DatabaseAccess();
            String sql = "select top(" +  size + ") * from xpath where id not in(select top( " + (page - 1) * size + ") id from xpath)";
            try {
                SqlDataReader result = database.executeQuery(sql);
                while (result.Read()) {
                    Xpath path = new Xpath();
                    path.Id = (Guid)result["id"];
                    path.Baseurl = result["baseurl"].ToString();
                    path.Websitename = result["websitename"].ToString();
                    path.Weblanguage = result["weblanguage"].ToString();
                    path.Cntitle = result["cntitle"].ToString();
                    path.Entitle = result["entitle"].ToString();
                    path.Unit = result["unit"].ToString();
                    path.Types = result["types"].ToString();
                    path.Author = result["author"].ToString();
                    path.Articletype = result["articletype"].ToString();
                    path.Source = result["source"].ToString();
                    path.Language = result["language"].ToString();
                    path.Journalname = result["journalname"].ToString();
                    path.Publishtime = result["publishtime"].ToString();
                    path.Levels = result["levels"].ToString();
                    path.Recordtype = result["recordtype"].ToString();
                    path.Forbid = (int)result["forbid"];
                    pathList.Add(path);
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            } finally {
                database.close();
            }
            return pathList;
        }

        /// <summary>
        /// 获得xpath中的数据总数
        /// </summary>
        /// <returns></returns>
        public long count() {
            long total = 0;
            DatabaseAccess database = new DatabaseAccess();
            string sql = "select count(*) from xpath";
            try {
                SqlDataReader result = database.executeQuery(sql);
                if (result.Read()) {
                    total = result.GetInt32(0);
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            } finally {
                database.close();
            }
            return total;
        }

        /// <summary>
        /// 更新规则是否启用
        /// </summary>
        /// <param name="ids">id的批量字符串表示形式</param>
        /// <param name="forbid">是否启用1表示启用， 0表示禁止</param>
        /// <returns></returns>
        public Boolean update(string ids,int forbid) {
            Boolean update = false;
            DatabaseAccess database = new DatabaseAccess();
            string sql = "update xpath set forbid=" + forbid + " where id in (" + ids + ")";
            try {
                update = database.executeUpdate(sql);
            } catch (Exception e) {
                throw new Exception(e.Message);
            } finally {
                database.close();
            }
            return update;
        }


        /// <summary>
        /// 更新整个数据的内容
        /// </summary>
        /// <param name="path">xpath的实例</param>
        /// <returns></returns>
        public Boolean update(Xpath path) {
            Boolean update = false;
            DatabaseAccess database = new DatabaseAccess();
            string sql = "update xpath set websitename='" + path.Websitename + "',weblanguage='" + path.Weblanguage + "',baseurl='" + path.Baseurl + "',cntitle='" + path.Cntitle + "',entitle='" + path.Entitle + "',unit='" + path.Unit + "',author='" + path.Author + "',types='" + path.Types + "',articletype='" + path.Articletype + "',source='" + path.Source + "',language='" + path.Language + "',journalname='" + path.Journalname + "',publishtime='" + path.Publishtime + "',levels='" + path.Levels + "',recordtype='" + path.Recordtype + "',forbid=" + path.Forbid + " where id='" + path.Id+"'";
            try {
                update = database.executeUpdate(sql);
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
            return update;
        }
    }
}
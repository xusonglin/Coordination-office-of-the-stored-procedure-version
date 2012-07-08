using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using pojo;
/// <summary>
///添加论文时用到的DAO层
/// author 张浩春
/// time 201-3-24
/// </summary>
/// 
namespace DAO
{
    public class TreatiseDAO
    {
        public TreatiseDAO()
        {
            
        }

        /// <summary>
        /// 插入一条新的论文记录到数据库
        /// </summary>
        /// <param name="treatise"></param>
        /// <returns></returns>
        public Boolean insert(Treatise treatise) {
            Boolean insert = false;
            DatabaseAccess database = new DatabaseAccess();
            String sql = "insert into treatise (id,articleurl,sourceid,cntitle,entitle,unitname,types,author,articletype,source,language,journalname,publishtime,levels,recordtype) values(NewId(),'"+
               treatise.Articleurl+"','"+treatise.Sourceid+"','"+ treatise.Cntitle + "','" + treatise.Entitle + "','" + treatise.Unitname + "','" + treatise.Types + "','" + treatise.Author + "','" + treatise.Articletype + "','" + treatise.Source + "','" + treatise.Language + "','" + treatise.Journalname + "','" + treatise.Publishtime + "','" + treatise.Levels + "','" + treatise.Recordtype + "')";
            try{
                insert = database.executeUpdate(sql);
            }
            catch (Exception e){
                throw new Exception(e.Message);
            } finally {
                database.close();
            }
            return insert;
        }

        /// <summary>
        /// 根据文章来源网站在xpath中的ID和文章来源判断文章是否存在，存在返回true，不存在返回false
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public Boolean exist(Guid id,string url) {
            Boolean exist = false;
            DatabaseAccess database = new DatabaseAccess();
            String sql = "select id from treatise where articleurl='"+url+"' and sourceid='"+id+"'";
            try {
                SqlDataReader result = database.executeQuery(sql);
                if (result.Read()) {
                    exist = true;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            } finally {
                database.close();
            }
            return exist;
        }

        /// <summary>
        /// 检测添加的论文是否存在
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public Boolean exist(string title,string author) {
            Boolean exist = false;
            DatabaseAccess database = new DatabaseAccess();
            String sql = "select id from treatise where cntitle='" + title + "' or entitle='"+title+"' and author='" + author + "'";
            try {
                SqlDataReader result = database.executeQuery(sql);
                if (result.Read()) {
                    exist = true;
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            } finally {
                database.close();
            }
            return exist;
        }

        /// <summary>
        /// 分页获得用户的所有论文
        /// </summary>
        /// <param name="page">分页时第几页</param>
        /// <param name="size">每页显示的数据量</param>
        /// <param name="zgxm">职业的姓名</param>
        /// <returns></returns>
        public List<Treatise> list(int page,int size,string zgxm) {
            List<Treatise> list = new List<Treatise>();
            DatabaseAccess database = new DatabaseAccess();
            String sql = "select top(" + size + ") * from treatise where author like '%" + zgxm + "%' and id not in(select top(" + (page - 1) * size + ") id from treatise where author like '%" + zgxm + "%' )";
            try {
                SqlDataReader result = database.executeQuery(sql);
                while (result.Read()) {
                    Treatise treatise = new Treatise();
                    treatise.Id = result.GetGuid(0);
                    treatise.Articleurl = result.GetString(1);
                    treatise.Sourceid = result.GetGuid(2);
                    treatise.Cntitle = result.GetString(3);
                    treatise.Entitle = result.GetString(4);
                    treatise.Unitname = result.GetString(5);
                    treatise.Types = result.GetString(6);
                    treatise.Author = result.GetString(7);
                    treatise.Articletype = result.GetString(8);
                    treatise.Source = result.GetString(9);
                    treatise.Language = result.GetString(10);
                    treatise.Journalname = result.GetString(11);
                    treatise.Publishtime = result.GetString(12);
                    treatise.Levels = result.GetString(13);
                    treatise.Recordtype = result.GetString(14);
                    list.Add(treatise);
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            } finally {
                database.close();
            }
            return list;
        }

        /// <summary>
        /// 获得教师的论文总数
        /// </summary>
        /// <param name="zgxm">职业姓名</param>
        /// <returns></returns>
        public int count(string zgxm) {
            int total = 0;
            DatabaseAccess database = new DatabaseAccess();
            String sql = "select count(*) as total from treatise where author like '%" + zgxm + "%'";
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
        /// 获得一条论文的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Treatise details(Guid id) {
            Treatise treatise = new Treatise();
            DatabaseAccess database = new DatabaseAccess();
            String sql = "select treatise.*,xpath.websitename,xpath.baseurl from treatise,xpath where treatise.id='" + id + "' and treatise.sourceid=xpath.id";
            try {
                SqlDataReader result = database.executeQuery(sql);
                while (result.Read()) {
                    treatise.Id = result.GetGuid(0);
                    treatise.Articleurl = result.GetString(1);
                    treatise.Sourceid = result.GetGuid(2);
                    treatise.Cntitle = result.GetString(3);
                    treatise.Entitle = result.GetString(4);
                    treatise.Unitname = result.GetString(5);
                    treatise.Types = result.GetString(6);
                    treatise.Author = result.GetString(7);
                    treatise.Articletype = result.GetString(8);
                    treatise.Source = result.GetString(9);
                    treatise.Language = result.GetString(10);
                    treatise.Journalname = result.GetString(11);
                    treatise.Publishtime = result.GetString(12);
                    treatise.Levels = result.GetString(13);
                    treatise.Recordtype = result.GetString(14);
                    treatise.Websitename = result.GetString(15);
                    treatise.Baseurl = result.GetString(16);
                }
            } catch (Exception e) {
                throw new Exception(e.Message);
            } finally {
                database.close();
            }
            return treatise;
        }
    }
}
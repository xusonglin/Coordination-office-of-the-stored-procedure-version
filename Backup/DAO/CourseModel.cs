using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pojo;
using DAO;
using C_R;
using System.Data.SqlClient;

/// <summary>
///CourseModel 的摘要说明
/// </summary>
public class CourseModel
{
    public CourseModel()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    //获取整个课程表内容
    public List<Bap_Course> list(int page, int size)
    {

        List<Bap_Course> list = new List<Bap_Course>();
        DatabaseAccess database = new DatabaseAccess();
        String sql = "select top(" + size + ") * from Bap_Course where id not in(select top(" + (page - 1) * size + ") id from Bap_Course)";
        try
        {
            SqlDataReader result = database.executeQuery(sql);
            while (result.Read())
            {
                Bap_Course course = new Bap_Course();
                course.Id = result["Id"].ToString();
                course.Course_Name = result.GetString(1);
                course.Staff_num = result.GetString(2);
                course.Teacher_Name = result.GetString(3);
                course.Department = result.GetString(4);
                course.Hours = result["Hours"].ToString();
                course.Class_Time = result.GetString(6);
                course.Class_Week = result.GetString(7);
                course.Total_Week = result.GetString(8);
                course.Is_Week = result.GetString(9);
                course.Class_Addr = result.GetString(10);
                course.Classes = result.GetString(11);
                list.Add(course);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            database.close();
        }
        return list;
    }

    //统计课程表总条数
    public int count()
    {
        int total = 0;
        DatabaseAccess database = new DatabaseAccess();
        String sql = "select count(*) as total from Bap_Course";
        try
        {
            SqlDataReader result = database.executeQuery(sql);
            if (result.Read())
            {
                total = result.GetInt32(0);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            database.close();
        }
        return total;
    }

    //根据ID获得某一条信息
    public Bap_Course getOne(string id)
    {
        Bap_Course Bap_Course = new Bap_Course();
        DatabaseAccess database = new DatabaseAccess();
        string sql = "select * from Bap_Course where id = " + Int32.Parse(id);
        try
        {
            SqlDataReader result = database.executeQuery(sql);
            while (result.Read())
            {
                // Bap_Course.Id = result["Id"].ToString();
                Bap_Course.Course_Name = result["Course_Name"].ToString();
                Bap_Course.Staff_num = result["Staff_num"].ToString();
                Bap_Course.Teacher_Name = result["Teacher_Name"].ToString();
                Bap_Course.Department = result["Department"].ToString();
                Bap_Course.Hours = result["Hours"].ToString();
                Bap_Course.Class_Time = result["Class_Time"].ToString();
                Bap_Course.Class_Week = result["Class_Week"].ToString();
                Bap_Course.Total_Week = result["Total_Week"].ToString();
                Bap_Course.Is_Week = result["Is_Week"].ToString();
                Bap_Course.Class_Addr = result["Class_Addr"].ToString();
                Bap_Course.Classes = result["Classes"].ToString();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            database.close();
        }
        return Bap_Course;
    }

    //根据ID保存修改后的课程信息
    public bool SaveById(Bap_Course Bap_Course)
    {
        DatabaseAccess database = new DatabaseAccess();

        String sql = String.Format(@"update Bap_Course set 
                        Course_Name='{0}',
                        Staff_num='{1}',
                        Teacher_Name='{2}',
                        Department='{3}',
                        Hours='{4}',
                        Class_Time='{5}',
                        Class_Week='{6}',
                        Total_Week='{7}',
                        Is_Week='{8}',
                        Class_Addr='{9}',
                        Classes='{10}' where id='" + Convert.ToInt32(Bap_Course.Id) + "'",
                                                   Bap_Course.Course_Name,
                                                   Bap_Course.Staff_num,
                                                   Bap_Course.Teacher_Name,
                                                    Bap_Course.Department,
                                                    Bap_Course.Hours,
                                                    Bap_Course.Class_Time,
                                                     Bap_Course.Class_Week,
                                                     Bap_Course.Total_Week,
                                                     Bap_Course.Is_Week,
                                                     Bap_Course.Class_Addr,
                                                     Bap_Course.Classes);

        return database.executeUpdate(sql);

    }
    //获取查询结果
    public List<Bap_Course> Search(String name, String value, int page, int size)
    {
        List<Bap_Course> list = new List<Bap_Course>();
        DatabaseAccess database = new DatabaseAccess();
       // String sql = "select * from Bap_Course where 1=1";
        String sql = "select top(" + size + ") * from Bap_Course where id not in(select top(" + (page - 1) * size + ") id from Bap_Course where 1=1)";
        if(name!="")
        {
        sql +=" and Teacher_Name like '%"+name +"%'";
        }
        if (value  != "")
        {
            sql += " and Staff_num like '%" + value + "%'";
        }
       // String sql = "select * from Bap_Course where " + name + " like '" + value + "%'";
        try
        {
            SqlDataReader result = database.executeQuery(sql);
            while (result.Read())
            {
                Bap_Course course = new Bap_Course();
                course.Id = result["Id"].ToString();
                course.Course_Name = result.GetString(1);
                course.Staff_num = result.GetString(2);
                course.Teacher_Name = result.GetString(3);
                course.Department = result.GetString(4);
                course.Hours = result["Hours"].ToString();
                course.Class_Time = result.GetString(6);
                course.Class_Week = result.GetString(7);
                course.Total_Week = result.GetString(8);
                course.Is_Week = result.GetString(9);
                course.Class_Addr = result.GetString(10);
                course.Classes = result.GetString(11);
                list.Add(course);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            database.close();
        }
        return list;

    }

    //获取查询结果条数
    public int SearchCount(String name, String value)
    {
        int total = 0;
        DatabaseAccess database = new DatabaseAccess();

        String sql = "select count(*) from Bap_Course where 1=1";

        if (name != "")
        {
            sql += " and Teacher_Name like '%" + name + "%'";
        }
        if (value != "")
        {
            sql += " and Staff_num like '%" + value + "%'";
        }
       // String sql = "select count(*) from Bap_Course where " + name + " like '" + value + "%'";
        try
        {
            SqlDataReader result = database.executeQuery(sql);
            if (result.Read())
            {
                total = result.GetInt32(0);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            database.close();
        }
        return total;
    }

    //添加一条课程信息
    public bool Add(Bap_Course Bap_Course)
    {
        DatabaseAccess database = new DatabaseAccess();

        String sql = String.Format(@"insert into Bap_Course(Course_Name,Staff_num,Teacher_Name,Department,Hours,Class_Time,Class_Week,Total_Week,Is_Week,Class_Addr,Classes) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
              Bap_Course.Course_Name, Bap_Course.Staff_num, Bap_Course.Teacher_Name, Bap_Course.Department, Bap_Course.Hours,
              Bap_Course.Class_Time, Bap_Course.Class_Week, Bap_Course.Total_Week, Bap_Course.Is_Week, Bap_Course.Class_Addr, Bap_Course.Classes);

        return database.executeUpdate(sql);

    }

    //获得个人课表
    public List<Bap_Course> getCourse(String name, String value)
    {
        List<Bap_Course> list = new List<Bap_Course>();
        DatabaseAccess database = new DatabaseAccess();
        string sql = "select * from Bap_Course where " + name + " = '" + value + "'";
        try
        {
            SqlDataReader result = database.executeQuery(sql);
            while (result.Read())
            {
                Bap_Course course = new Bap_Course();
                course.Id = result["Id"].ToString();
                course.Course_Name = result.GetString(1);
                course.Staff_num = result.GetString(2);
                course.Teacher_Name = result.GetString(3);
                course.Department = result.GetString(4);
                course.Hours = result["Hours"].ToString();
                course.Class_Time = result.GetString(6);
                course.Class_Week = result.GetString(7);
                course.Total_Week = result.GetString(8);
                course.Is_Week = result.GetString(9);
                course.Class_Addr = result.GetString(10);
                course.Classes = result.GetString(11);
                list.Add(course);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            database.close();
        }
        return list;
    }

    //获得手机号码
    public List<Contact> getPhoneNum(String Staff_Num)
    {
        List<Contact> list = new List<Contact>();
        DatabaseAccess database = new DatabaseAccess();
        String sql = "select Phone_Num from Bap_SmsContact where Staff_Num=" + Staff_Num;
        try
        {
            SqlDataReader result = database.executeQuery(sql);
            while (result.Read())
            {
                Contact SmsContact = new Contact();
                SmsContact.Id            =  result["Id"].ToString();
                SmsContact.Staff_Num     =  result["Staff_Num"].ToString();
                SmsContact.Teacher_Name  =  result["Teacher_Name"].ToString();
                SmsContact.Phone_Num     =  result["Phone_Num"].ToString();
                list.Add(SmsContact);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            database.close();
        }
        return list;
    }
}
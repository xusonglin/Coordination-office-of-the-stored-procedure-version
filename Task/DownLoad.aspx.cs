using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.SqlClient;
using Aspose.Cells;
using JiaoShiXinXiTongJi;
using Maticsoft.DBUtility;
namespace JiaoShiXinXiTongJi.Task
{
    public partial class DownLoad : BasePage
    {
        protected string str;
        protected string str_text;
        protected string arr_text_first;
        protected string testlist="";
        protected void Page_Load(object sender, EventArgs e)
        {

            string taskid = Request.QueryString["TaskID"].ToString(); //接收前面传来的任务ID

            string select = "select TaskName from bap_task where TaskID='" + taskid + "'";
            string taskname = DbHelperSQL.GetSingle(select).ToString(); //取得任务名称
  
            Common common = new Common();
            SqlConnection con = common.coon();
            con.Open();
            string username = Session["ZGXM"].ToString(); // 获取当前用户名
          
            if (taskid != "" && taskname != "")
            {
                #region 取得任务信息
                string sel = "select * from Bap_Title where TaskID='" + taskid + "'";  //从Bap_Title中取出这个表的标题属性，然后导出Excel时用来写标题
                SqlDataAdapter da = new SqlDataAdapter(sel, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                string[] arr = new string[100];
                for (int i = 1; i < ds.Tables[0].Columns.Count; i++) //列
                {
                    str += ds.Tables[0].Rows[0][i].ToString() + ",";  // 将标题属性拼接起来

                }

                string sel_text = "select * from Bap_Text where TaskID='" + taskid + "'";  //从表Bap_Text中选取 字段属性，用来在Excel中显示列
                SqlDataAdapter da_text = new SqlDataAdapter(sel_text, con);
                DataSet ds_text = new DataSet();
                da_text.Fill(ds_text);
                string[] arr_text = new string[100];

                for (int j = 0; j < ds_text.Tables[0].Rows.Count; j++)
                {
                    for (int i = 1; i < ds_text.Tables[0].Columns.Count; i++)
                    {

                        arr_text[j] += ds_text.Tables[0].Rows[j][i].ToString() + ",";

                    }

                    arr_text_first += ds_text.Tables[0].Rows[j][2].ToString() + ",";
                    str_text += arr_text[j] + "_";                 //所有行的加到一起
                }



                string li = str.Substring(0, str.Length - 1);
                string[] arr_ = li.Split(',');
                string title = "";
                string Font_Names;
                string Font_Size;
                string ForeColor = "";
                string ISBold = "";
                for (int t = 0; t < li.Length; t++)
                {

                    title = arr_[0].ToString();      //标题的属性
                    Font_Names = arr_[1].ToString();
                    Font_Size = arr_[2].ToString();
                    ForeColor = arr_[3].ToString();
                    ISBold = arr_[4].ToString();

                }



                string li_text = str_text.Substring(0, str_text.Length - 1);

                string[] arr_textt = li_text.Split('_');

                string title_text = "";
                string Text_Font_Names;
                string Text_Font_Size;
                string Text_ForeColor;
                string title_te = "";
                int lengt = 0;
                string length = "";
                for (int t = 0; t < arr_textt.Length; t++)
                {

                    string[] arr_text_ = arr_textt[t].Split(',');
                    string[] arr_length = new string[100];

                    for (int k = 0; k < arr_text_.Length; k++)
                    {
                        title_te = arr_text_[0];   // 字段的名称
                        Text_Font_Names = arr_text_[1];
                        Text_Font_Size = arr_text_[2];
                        Text_ForeColor = arr_text_[3];
                        lengt = arr_text_[0].Length;   // 统计每个字段的长度，用来动态设计表格的宽度
                    }

                    length += lengt + ",";
                    title_text += title_te + ",";



                }


                testlist = title_text;

                int count = arr_textt.Length;  // 统计字段个数，用来标题合并单元格用





                string seletcout = "select count(*) from [" + taskname + "【" + username + "】]";
                int taskcount = Int32.Parse(DbHelperSQL.GetSingle(seletcout).ToString());

                con.Close();
            #endregion
                CreateExcelTable(title, title_text, taskname, count, length, ISBold, ForeColor, taskcount);   // 调用生成EXCEL函数


                Response.Write("<script>window.opener=null;window.close();</script>");// 不会弹出询问


            }

            else
            {
                Response.Write("<script>alert('操作失败！请重试！')</script>");
                Response.Write("<script>document.location=document.location;</script>");
            }

        }




        #region 取动态表中的数据
        private DataSet GetData(string taskname)
        {
            string username = Session["ZGXM"].ToString(); // 获取当前用户名
            Common common = new Common();
            SqlConnection conn = common.coon();
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
            }
            testlist = testlist.Substring(0, testlist.Length - 1);
            string cmdText = "select " + testlist + " from [" + taskname + "【" + username + "】]";  //  从动态生成的那个表中去数据
            SqlDataAdapter da = new SqlDataAdapter(cmdText, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            return ds;
        }
        #endregion


        #region 生成Excel
        private void CreateExcelTable(string title, string text, string taskname, int count, string length11, string ISBold, string ForeColor, int taskcount)
        {
            string username = Session["ZGXM"].ToString();
            try
            {
                int rowIndex = 1;
                int colIndex = 0;
                DataSet ds = GetData(taskname); //调用上面的函数
                System.Data.DataTable table = ds.Tables[0];
                Aspose.Cells.License licExcel = new License();  //Aspose.Cells申明
                if (File.Exists(Server.MapPath("~/Excel/Bin/cellLic.lic")))
                    licExcel.SetLicense(Server.MapPath("~/Excel/Bin/cellLic.lic"));
                else if (File.Exists(Server.MapPath("~/Excel/Bin/cellLic.lic")))
                    licExcel.SetLicense(Server.MapPath("~/Excel/Bin/cellLic.lic"));
                Workbook workbook = new Workbook();
                Worksheet worksheet = workbook.Worksheets[0];
                int sheet2 = workbook.Worksheets.AddCopy(0);
                Worksheet worksheet2 = workbook.Worksheets[sheet2];

                Styles styles = workbook.Styles;
                int styleIndex = styles.Add();
                Aspose.Cells.Style borderStyle = styles[styleIndex];
                //Style borderStyle = styles[styleIndex];
                borderStyle.Borders.SetStyle(CellBorderType.Thin);
                borderStyle.Borders.DiagonalStyle = CellBorderType.None;
                borderStyle.HorizontalAlignment = TextAlignmentType.Center;//文字居中 
                Cells cells = worksheet.Cells;
                worksheet.Name = title + "【" + username + "】";  //sheet1的名字
                string title_text = text.Substring(0, text.Length - 1);
                string[] arr = title_text.Split(',');
                int j = 10; // 设定表格初始宽度
                string lengthlist = length11.Substring(0, length11.Length - 1);
                string[] arr_length = lengthlist.Split(',');
                for (int i = 0; i < arr.Length; i++)
                {
                    int k = Int32.Parse(arr_length[i]);//判断每个字段的长度，然后在Excel中设定表格宽度

                    if (k <= 2)
                        j = 10;
                    if (k > 2 && k <= 4)
                        j = 20;


                    cells[1, i].PutValue(arr[i]); // 填写列名
                    cells[1, i].Style.HorizontalAlignment = TextAlignmentType.Center; ;
                    cells.SetColumnWidth(i, j); //设置宽度
                    for (int w = 1; w <= taskcount + 1; w++)
                    {
                        cells[w, i].Style = borderStyle;
                    }
                }
                string nowdate = System.DateTime.Today.ToString("yyyy/MM/dd");

                cells[taskcount + 4, arr.Length - 2].PutValue("制表人：");

                cells[taskcount + 4, arr.Length - 1].PutValue(username);

                cells[taskcount + 5, arr.Length - 2].PutValue("制表日期：");

                cells[taskcount + 5, arr.Length - 1].PutValue(nowdate);

                cells[0, 1].PutValue(title); // 填写标题


                cells.Merge(0, 0, 1, count);// 1,3 行 2.4 列  合并 ，标题合并

                cells[Byte.Parse(0.ToString())].Style.Font.Size = 25;//字体大小

                cells[Byte.Parse(0.ToString())].Style.Font.Color = System.Drawing.Color.Black; //字体颜色
                if (ISBold == "Yes")
                {
                    cells[Byte.Parse(0.ToString())].Style.Font.IsBold = true; //加粗
                }
                //cells.Rows[Byte.Parse(0.ToString())].Style.Name = "华文行楷";

                cells[Byte.Parse(0.ToString())].Style.HorizontalAlignment = TextAlignmentType.Center;

                cells[Byte.Parse(0.ToString())].Style.VerticalAlignment = TextAlignmentType.Bottom;
                cells.SetRowHeight(0, 30);  //设置标题高度

                for (int c = 1; c <= taskcount; c++)
                {
                    cells.SetRowHeight(c, 20);// 设置下面数据的高度

                }
                cells.SetRowHeight(taskcount + 1, 20);
                foreach (DataRow row in table.Rows)     // 填写数据库表中的数据
                {
                    //添加数据
                    rowIndex++;
                    colIndex = 0;
                    foreach (DataColumn col in table.Columns)
                    {
                        colIndex++;
                        cells[rowIndex, colIndex - 1].PutValue(row[col.ColumnName].ToString()); //调整第一列开始写 colIndex-1
                    }
                }

                workbook.Save(Server.MapPath("~/Excel/" + title + "【" + username + "】.xls"));

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            //Response.Write("<script>window.location.href='~/Excel/" + title + "【" + username + "】.xls'</script>");
            //Response.Redirect(Server.MapPath("~/Excel/" + title + "【" + username + "】.xls"));
            string NewFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            //打开要下载的文件，并把该文件存放在FileStream中
            NewFileName = Server.MapPath("~/Excel/" + title + "【" + username + "】.xls");
            System.IO.FileStream Reader = System.IO.File.OpenRead(NewFileName);
            //文件传送的剩余字节数：初始值为文件的总大小
            long Length = Reader.Length;
            Response.Buffer = false;
            Response.AddHeader("Connection", "Keep-Alive");
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(title + "【" + username + "】.xls"));
            Response.AddHeader("Content-Length", Length.ToString());
            //存放欲发送数据的缓冲区
            byte[] Buffer = new Byte[10000];
            //每次实际读取的字节数
            int ByteToRead;
            while (Length > 0)
            {
                //剩余字节数不为零，继续传送
                if (Response.IsClientConnected)
                {
                    //客户端浏览器还打开着，继续传送
                    //往缓冲区读入数据
                    ByteToRead = Reader.Read(Buffer, 0, 10000);
                    //把缓冲区的数据写入客户端浏览器
                    Response.OutputStream.Write(Buffer, 0, ByteToRead);
                    //立即写入客户端
                    Response.Flush();
                    //剩余字节数减少
                    Length -= ByteToRead;
                }
                else
                {
                    //客户端浏览器已经断开，阻止继续循环
                    Length = -1;
                }
            }
            //关闭该文件
            Reader.Close();
            //删除该Excel文件
            File.Delete(NewFileName);
         

        }
        #endregion
    }
}

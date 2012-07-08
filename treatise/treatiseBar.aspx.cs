using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using ZedGraph;
using ZedGraph.Web;
using Maticsoft.DBUtility;
using System.Data.SqlClient;
namespace JiaoShiXinXiTongJi.treatise
{
    public partial class treatiseStatistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ZedGraphWeb1.RenderGraph += new ZedGraph.Web.ZedGraphWebControlEventHandler(this.OnRenderGraph);

        }
        private void OnRenderGraph(ZedGraphWeb zgw, Graphics g, MasterPane masterPane)
        {
            // Get the GraphPane so we can work with it
            GraphPane myPane = masterPane[0];

            myPane.Title.Text = "科研论文统计图";
            myPane.XAxis.Title.Text = "作者姓名";
            myPane.YAxis.Title.Text = "总篇数";

            PointPairList list = new PointPairList();
         
         //   int totalCount = DbHelperSQL.RunProcedure ("GetSplitResults",parameters,"ds").Tables [0].Rows .Count ;
              int totalCount = DbHelperSQL.Query ("GetSplitResults").Tables [0].Rows .Count;
             myPane.YAxis.Scale.MajorStep =1;
            for (int x = 0; x < totalCount; x++)
            {
                int y = Convert.ToInt32(DbHelperSQL.Query("GetSplitResults").Tables[0].Rows[x]["篇数"].ToString());
                list.Add(x, y);
             
            }

            BarItem myCurve = myPane.AddBar("论文数", list, Color.Blue);
            myCurve.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue);
          

            myPane.XAxis.MajorTic.IsBetweenLabels = true;
            string[] labels = new string[totalCount];
            for (int i = 0; i < totalCount; i++) 
            {
                labels[i] = DbHelperSQL.Query("GetSplitResults").Tables[0].Rows[i]["作者名"].ToString();
            }

            myPane.XAxis.Scale.TextLabels = labels;
            myPane.XAxis.Type = AxisType.Text;
            myPane.XAxis.Scale.FontSpec.Angle = 90;
            myPane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255), 45.0f);
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);

            masterPane.AxisChange(g);
        }
    }
}
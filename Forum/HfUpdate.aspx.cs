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
using System.Data.SqlClient;
using JiaoShiXinXiTongJi;
using Maticsoft.DBUtility;

namespace JiaoShiXinXiTongJi.Forum
{
    public partial class HfUpdate : BasePage
    {
        protected string strs="";
        protected void Page_Load(object sender, EventArgs e)
        {

           
             string    CountIDD = Request.Params["CountIDD"].ToString();
             string sel = "select bfh.*,bu.zgxm,bf.userid as liuyanuserid from bap_forum_hf bfh left join bap_user bu on bfh.hfuserid=bu.userid left join bap_forum bf on bf.countid=bfh.countid where bfh.countid='" + CountIDD + "' order by bfh.hfdate desc";
            DataSet dt = DbHelperSQL.Query(sel);
            if (dt.Tables[0].Rows.Count > 0)
            {
                string[] arrlist = new string[1000];
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {

                    arrlist[i] = dt.Tables[0].Rows[i]["HfID"].ToString() + "_" + dt.Tables[0].Rows[i]["HFContent"].ToString() + "_" + dt.Tables[0].Rows[i]["HfDate"].ToString() + "_" + dt.Tables[0].Rows[i]["HfUserID"].ToString() + "_" + dt.Tables[0].Rows[i]["ZGXM"].ToString() + "_" + dt.Tables[0].Rows[i]["liuyanuserid"].ToString();
                    strs += arrlist[i] + "&";
                }

                strs = strs.Substring(0, strs.Length - 1);

            }
          

        }
    }
}

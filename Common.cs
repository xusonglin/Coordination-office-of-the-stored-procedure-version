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
using System.Security.Cryptography;// 密码加密用到
using System.IO;// 密码加密用到
using System.Text;


namespace JiaoShiXinXiTongJi
{
    class Common
    {
        public SqlConnection coon()
        {
            //return new SqlConnection("server=PC-B\\SQLEXPRESS;database=demo;integrated security=true");
            return new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
        }
        private SqlConnection con;  //创建连接对象
        #region   打开数据库连接
        /// <summary>
        /// 打开数据库连接.
        /// </summary>
        private void Open()
        {
            // 打开数据库连接
            if (con == null)
            {
                con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
            }
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

        }
        #endregion

        #region  关闭连接
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if (con != null)
                con.Close();
        }
        #endregion
        #region   数据加密
        /// <summary>
        ///数据加密

        public string Encrypting(string str)
        {
            byte[] bytIn = System.Text.Encoding.Default.GetBytes(str);
            byte[] iv = { 102, 16, 93, 156, 78, 4, 218, 32 };
            byte[] key = { 55, 103, 246, 79, 36, 99, 167, 3 };
            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            dsp.Key = iv;
            dsp.IV = key;
            ICryptoTransform ict = dsp.CreateEncryptor();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());

        }
        #endregion
        #region   数据解密
        /// <summary>
        ///数据解密
        public string Decrypting(string str)
        {
            byte[] bytIn = System.Convert.FromBase64String(str);

            byte[] iv = { 102, 16, 93, 156, 78, 4, 218, 32 };
            byte[] key = { 55, 103, 246, 79, 36, 99, 167, 3 };

            DESCryptoServiceProvider dsp = new DESCryptoServiceProvider();
            dsp.Key = iv;
            dsp.IV = key;
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            ICryptoTransform ict = dsp.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, ict, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs, Encoding.Default);
            return sr.ReadToEnd();
        }
        #endregion
        internal void open()
        {
            throw new NotImplementedException();
        }
    }
}

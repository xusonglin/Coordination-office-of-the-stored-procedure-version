using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using System.Text;

/// <summary>
/// 读取数据库的配置文件
/// author 张浩春
/// time 2012-3-24
/// </summary>
namespace DAO {
    public class ReadXml {
        private static string connection = null;

        /// <summary>
        /// 读取数据库的路径 
        /// </summary>
        public static void init() {
            if (connection == null) {//如果数据库的链接路径为空，则读取，否则不做任何操作
                string basePath = System.AppDomain.CurrentDomain.BaseDirectory + "Web.config";
                XmlDocument doc = new XmlDocument();
                StreamReader reader = new StreamReader(basePath,UTF8Encoding.UTF8,false);
                doc.Load(reader);
                XmlNode node = doc.SelectSingleNode("//add[@key=\"ConnectionString\"]");
                XmlAttributeCollection attrList = node.Attributes;
                node = attrList.GetNamedItem("value");
                connection = node.InnerText;
            }
        }

        public static string Connection {
            get { return ReadXml.connection; }
        }


        public ReadXml() {

        }
    }
}
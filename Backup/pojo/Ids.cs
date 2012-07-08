using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Ids 的摘要说明
///在数据库中实际上不存在与之相对应的表
///为了方便JSON格式数据传输，因此建立此类
/// </summary>
///
namespace pojo {
    public class Ids {
        private string id;
        public string Id {
            get { return id; }
            set { id = value; }
        }

    }
}
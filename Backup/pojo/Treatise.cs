using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Treatise 的摘要说明
/// </summary>
namespace pojo {
    public class Treatise {
        private Guid id;
        public Guid Id {
            get { return id; }
            set { id = value; }
        }

        private string articleurl;
        public string Articleurl {
            get { return articleurl; }
            set { articleurl = value; }
        }

        private Guid sourceid;
        public Guid Sourceid {
            get { return sourceid; }
            set { sourceid = value; }
        }

        private string cntitle;
        public string Cntitle {
            get { return cntitle; }
            set { cntitle = value; }
        }

        private string entitle;
        public string Entitle {
            get { return entitle; }
            set { entitle = value; }
        }

        private string unitname;
        public string Unitname {
            get { return unitname; }
            set { unitname = value; }
        }

        private string types;
        public string Types {
            get { return types; }
            set { types = value; }
        }

        private string author;
        public string Author {
            get { return author; }
            set { author = value; }
        }

        private string articletype;
        public string Articletype {
            get { return articletype; }
            set { articletype = value; }
        }

        private string source;
        public string Source {
            get { return source; }
            set { source = value; }
        }

        private string language;
        public string Language {
            get { return language; }
            set { language = value; }
        }

        private string journalname;
        public string Journalname {
            get { return journalname; }
            set { journalname = value; }
        }

        private string publishtime;
        public string Publishtime {
            get { return publishtime; }
            set { publishtime = value; }
        }

        private string levels;
        public string Levels {
            get { return levels; }
            set { levels = value; }
        }

        private string recordtype;
        public string Recordtype {
            get { return recordtype; }
            set { recordtype = value; }
        }

        private string websitename;//文章来源网站名称，在treatise表中实际不存在该字段
        public string Websitename {
            get { return websitename; }
            set { websitename = value; }
        }

        private string baseurl;//文章来源网站链接，在treatise表中实际不存在该字段
        public string Baseurl {
            get { return baseurl; }
            set { baseurl = value; }
        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Xpath 的摘要说明
/// </summary>
namespace pojo {
    public class Xpath {
        private Guid id;
        public Guid Id {
            get { return id; }
            set { id = value; }
        }

        private string websitename;
        public string Websitename {
            get { return websitename; }
            set { websitename = value; }
        }

        private string weblanguage;
        public string Weblanguage {
            get { return weblanguage; }
            set { weblanguage = value; }
        }

        private string baseurl;

        public string Baseurl {
            get { return baseurl; }
            set { baseurl = value; }
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

        private string unit;
        public string Unit {
            get { return unit; }
            set { unit = value; }
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
        public string Publishtime
        {
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

        private int forbid;
        public int Forbid {
            get { return forbid; }
            set { forbid = value; }
        }
    }
}
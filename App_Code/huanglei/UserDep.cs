using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Xml.Linq;
using System.Data;

namespace UserDep
{
    public class User
    {
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string yhid;
        public string Yhid
        {
            get { return yhid; }
            set { yhid = value; }
        }
    }
    /// <summary>
    ///Dep 的摘要说明
    /// </summary>
    public class Dep
    {
        public Dep()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public XDocument getDep()
        {
            DataTable dt = Sqlhelper.Serach("select yxdm,yxmc from dm_yuanxi");
            string xmlString = "<成都技师学院></成都技师学院>";
            XDocument temp = XDocument.Parse(xmlString);
            XElement tempXE = null;
            foreach (DataRow x in dt.Rows)
            {
                tempXE = new XElement("院系");
                tempXE.SetAttributeValue("text", x["yxmc"].ToString());
                tempXE.SetAttributeValue("value", x["yxdm"].ToString());
                temp.Element("成都技师学院").Add(tempXE);
            }
            return temp;
            //return new Dictionary<string, string> { { "0001", "机械工程系" }, { "0002", "电子信息工程处" } };
        }
        public XDocument getDepzhu()
        {
            DataTable dt = Sqlhelper.Serach("select zid,zmc from zhuqx order by px");
            string xmlString = "<成都技师学院></成都技师学院>";
            XDocument temp = XDocument.Parse(xmlString);
            XElement tempXE = null;
            foreach (DataRow x in dt.Rows)
            {
                tempXE = new XElement("院系");
                tempXE.SetAttributeValue("text", x["zmc"].ToString());
                tempXE.SetAttributeValue("value", x["zid"].ToString());
                temp.Element("成都技师学院").Add(tempXE);
            }
            return temp;
            //return new Dictionary<string, string> { { "0001", "机械工程系" }, { "0002", "电子信息工程处" } };
        }
    }
}
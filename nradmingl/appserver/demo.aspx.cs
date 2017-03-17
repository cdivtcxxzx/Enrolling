using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;


public partial class nradmingl_appserver_demo : System.Web.UI.Page
{
    public class MyJson
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public Object Value { get; set; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string cs = Request.QueryString["cs"];        
        if (cs!=null && cs.Trim().Equals("demo"))
        {
            List<MyJson> data = new List<MyJson>();
            List<string> lst = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                lst.Add("hello " + i.ToString().Trim()+" " + DateTime.Now);
                MyJson obj = new MyJson();
                obj.Code = i.ToString();
                obj.Msg = "msg" + i.ToString();
                obj.Value = lst;
                data.Add(obj);
            }
            string jg = toJson(data);
            Response.Write(jg);
            return;
        }
    }

    public string toJson(object obj)
    {
        string result = null;
        MyJson data = new MyJson();
        try
        {
            data.Code = "success";
            data.Msg = "success！";
            data.Value = obj;
        }
        catch (Exception ex)
        {

            data.Code = "error";
            data.Msg = "error！";
            data.Value = null;
        }
        result = JsonConvert.SerializeObject(data);
        return result;
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class _404 : System.Web.UI.Page
{
   // public static string zrsql = "Data Source=bds250590547.my3w.com;Initial Catalog=bds250590547_db; User ID=bds250590547;Password=cdfhsql2016";//用来记录注入的SQL连接 
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Response.Write(HttpContext.Current.Session["username"].ToString());
            if (Request.QueryString["id"] != null)
        {
        //SQL注入检测
        //basic.clearsqljc("gzyuan");
            //d:\\soft\\gzy
            string errxx = HttpUtility.HtmlDecode(Request["id"].ToString().Replace("@@", "&").Replace("￥￥", "#"));
           // Response.Write(errxx);
            this.errshow.InnerHtml = errxx.Replace("|", "<br>").Replace("|", "<br>").Replace("|", "<br>").Replace("|", "<br>").Replace("|", "<br>").Replace("|", "<br>").Replace("d:\\soft\\gzy\\", "网站中:").Replace("e:\\", "网站中:").Replace("c:\\", "网站中:").Replace("d:\\", "网站中:");
            if (errxx.Contains("请勿非法操作网站"))
            {
                //统计该IP是否非法访问
                //获得当前IP
                
               


            }
            else
            {
               

            }
           
               
        }
        }catch(Exception e2)
        {
            this.errshow.InnerHtml += e2.Message;
        }
        
    }
 
}
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
    /// <summary>
    /// 获取客户端IP地址（无视代理）
    /// </summary>
    /// <returns>若失败则返回回送地址</returns>
    public static string GetHostAddress()
    {
        string userHostAddress = HttpContext.Current.Request.UserHostAddress;

        if (string.IsNullOrEmpty(userHostAddress))
        {
            userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
        if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
        {
            return userHostAddress;
        }
        return "127.0.0.1";
    }

    /// <summary>
    /// 检查IP地址格式
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool IsIP(string ip)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Write((HttpContext.Current.Request.UserAgent + "").ToLower().Trim() + "@" + GetHostAddress());
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
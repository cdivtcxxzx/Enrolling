using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_Default : System.Web.UI.Page
{
    #region 页面初始化参数
    public string menuok;
    public string id;
    private string pagelm1 = "系统后台管理中心";

    private string pageqx1 = "浏览";
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "hkxxxt.aspx";//页面值
    #endregion
  
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("defaultgl.aspx");
        Response.Write("<font color=red>未授权访问!</font>");
        Response.End();
        //登陆验证,权限验证,日志
        new c_login().tongyiyz(pagelm1, pageqx1, "进入"+pagelm1+"页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);

        if (Request["webUserName"] != null)
        {
            //if (Request["loginok"].ToString() == "auot" && Request["loginok"] != null)
            //{

            //    //Response.Write("单点登陆");
            //    //明文传送
            //        if (Request["webUserName"] != null && Request["webPwd"] != null)
            //        {
            //            string webpwd = Request["webPwd"].ToString();
            //            if (Request["jm"] != null)
            //            {
            //                if (Request["jm"].ToString() == "des" && Request["webPwd"] != null)
            //                {
            //                    //加密传送-解密
            //                    mypass.RC4Crypto jm = new mypass.RC4Crypto();

            //                    webpwd = jm.Decrypt((String)Request["webPwd"].ToString(), "cdivtc");
            //                }
            //            }

            //            string URL = "xingstj.aspx";
            //            if (Request["tourl"] != null) URL = Request["tourl"].ToString();

            //            //Response.Write(URL);
            //            //System.Web.UI.Page page = new System.Web.UI.Page();
            //            c_login myLogin = new c_login();
            //            if (Request["webUserName"].ToString() == webpwd)
            //            if (myLogin.login(Request["webUserName"], true, false))
            //            {

            //                Response.Write("<script language=javascript>location.href='" + URL + "'</script>");
            //                //Response.End();
            //            }
            //            else
            //            {
            //                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script language=javascript>;main.location.href='/default.aspx'</script>");
            //                //Response.End();
            //            }
                  
            //        //显
            //        //Response.Redirect("xingstj.aspx");
            //    }
            //}
           string URL="default.aspx";
           if (Request["tourl"] != null) URL = Request["tourl"].ToString();
            string webUserName=Request["webUserName"].ToString();
            string yhid="";
            mypass.RC4Crypto jm = new mypass.RC4Crypto();
            webUserName = HttpUtility.UrlDecode(webUserName);
            yhid = jm.Decrypt(webUserName, "cdivtc");
                    c_login myLogin = new c_login();
                        if (myLogin.login(yhid, true, false))
                        {

                            Response.Write("<script language=javascript>location.href='" + URL + "'</script>");
                            //Response.End();
                        }
                        else
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script language=javascript>;main.location.href='/default.aspx'</script>");
                            //Response.End();
                        }

                    //显
                    //Response.Redirect("xingstj.aspx");
                }


        else 
        {



            //if (agent == "" ||
            //    agent.IndexOf("mobile") != -1 ||
            //    agent.IndexOf("mobi") != -1 ||
            //    agent.IndexOf("nokia") != -1 ||
            //    agent.IndexOf("samsung") != -1 ||
            //    agent.IndexOf("sonyericsson") != -1 ||
            //    agent.IndexOf("mot") != -1 ||
            //    agent.IndexOf("blackberry") != -1 ||
            //    agent.IndexOf("lg") != -1 ||
            //    agent.IndexOf("htc") != -1 ||
            //    agent.IndexOf("j2me") != -1 ||
            //    agent.IndexOf("ucweb") != -1 ||
            //    agent.IndexOf("opera mini") != -1 ||
            //    agent.IndexOf("mobi") != -1)
            //{
            //    //终端可能是手机


            //    HttpContext.Current.Response.Redirect("~/admin/xingsbdsj.aspx");
            //}
        }


       
        //接受WEBSERS
   // http://www.cdivtc.com:10000/admin/default.aspx?webUserName=zhangming1&webPwd=111111&loginok=auot&tourl=xingstj.aspx
       

        //判断登陆(此页面默认所有登陆用户都能访问)
     //   Response.Write(Session["Yhqx"]);
        c_login indexLogin = new c_login();
        indexLogin.Loginyanzheng();
        if (Session["Name"] != null && Session["UserName"] != null)
        {
            this.txtUsername.Text = Session["Name"].ToString();
            //用户组获取
            try
            {
                if (Session["Lsz"].ToString() != "")
                {
                    string[] strLszs = Session["Lsz"].ToString().Split(',');
                    foreach (string strLsz in strLszs)
                    {
                        string sqlSerachByYhid = "SELECT zid,zmc FROM zhuqx WHERE zid=@zid";
                        DataTable dt1 = Sqlhelper.Serach(sqlSerachByYhid, new SqlParameter("zid", strLsz));

                        if (dt1.Rows.Count > 0)
                        {
                            this.txtUsergroup.ToolTip = this.txtUsergroup.ToolTip + "," + dt1.Rows[0]["zmc"].ToString();
                            this.txtUsergroup.Text = dt1.Rows[0]["zmc"].ToString();
                            if (this.txtUsergroup.Text.Length > 6) this.txtUsergroup.Text = this.txtUsergroup.Text.Substring(0, 6);
                           

                        }
                    }

                }
            }
            catch
            {

            }
            
        }
        else
        {
            this.txtUsername.Text = "匿名用户";
            this.txtUsergroup.Text = "普通用户";
        }
        //网站配置表中读取
        c_index indexLr = new c_index(Session["UserName"].ToString());
        this.Title = indexLr.title;
        this.MetaKeywords = indexLr.metaKeyworkds;
        this.MetaDescription = indexLr.metaDescription;
        this.webcss.Href =indexLr.webcss_Href; //网站css文件
     //   this.logo.Src = indexLr.log_scr;//网站LOGO
        this.leftdh.InnerHtml = indexLr.leftdh_InnerHtml; //左侧导航默认页,menu.aspx从数据库中读取
        this.nrdh.InnerHtml = indexLr.nrdh_InnerHtml;//右侧内容框架默认页,shortcut.html从库中读取
        //头部导航-数据库中读取
        this.topdh.InnerHtml = indexLr.topdh_InnerHtml;        
        //菜单显示 循环输出需要显示的菜单
        // href=\"shortcut.html\" 为内容页显示的网址 title为左边导航的页面读取值
        //this.menu.InnerHtml="<a href=\"shortcut.html\" title=\"menu.aspx?order=1000\">快速导航</a>";
        this.menu.InnerHtml = indexLr.menu_InnerHtml + "";
        //版权-数据库中获取
        this.copyright.InnerHtml = "<p>明先生新闻管理系统 V1.0  联系QQ：530121438</p>";   
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nradmingl_Defaultgl : System.Web.UI.Page
{
    #region 页面初始化参数
    private string xwdith = "1366";//屏宽
    private string xheight = "768";//屏高
    private string pagelm1 = "后台管理";//请与系统栏目管理设置为一致便于权限管理

    private string pageqx1 = "浏览";//权限名称，根据页面的权限控制命名，与栏目管理中权限一致，最大设置为５个
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "";//当前页面值
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 软件授权验证
        string btitle = pagelm1;
        if (basic.sqyz() == "no")
        {
            Response.Redirect("/404/?username=system&id=|提示：对不起您的系统还未授权||解决：请联系QQ：4615914获取授权||");
            btitle = "对不起您的系统还未授权,请联系QQ：4615914获取授权";
            Response.End();
        }
        else
        {
            if (basic.sqyz() == "sy")
            {
                btitle = "___迎新系统试用版";
            }
        }
        #endregion
        #region 页面基本配置及标题标识
        try
        {

            webpage = Request.Url.GetLeftPart(UriPartial.Query).ToString().Replace(Request.Url.Port.ToString(), Sqlhelper.serverport); 
               // Response.Write(webpage);
               // Response.End();
         
        }
        catch { }

       
        
        System.Data.DataTable wangzxx = Sqlhelper.Serach("SELECT TOP 100 *  FROM [wangzxx] order by xxid asc");
        if (wangzxx.Rows.Count > 0)
        {
            for (int i = 0; i < wangzxx.Rows.Count; i++)
            {
                //网站开关
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "isopen")
                {
                    if (wangzxx.Rows[i]["xxnr"].ToString() == "0")
                    {
                        btitle = "网站正在维护，请稍后再访问！" + btitle;
                        try
                        {
                            this.tsxx.Style.Add("display", "");
                            this.tsxx.InnerHtml = "<font color=red>系统维护中</font>";
                        }
                        catch { }
                       // Response.End();
                    }
                   
                }
                //网站标题及META
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "title") this.Title = wangzxx.Rows[i]["xxnr"].ToString() + btitle;
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "MetaKeywords") this.MetaKeywords = wangzxx.Rows[i]["xxnr"].ToString() + btitle;
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "description") this.MetaDescription = wangzxx.Rows[i]["xxnr"].ToString() + btitle;

            }
        }
        
        #endregion
        #region 当前页浏览权限验证
        new c_login().tongyiyz(webpage, pagelm1, pageqx1, "进入" + pagelm1 + "页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
        //默认如权限１，若单独验证某个权限，如下方式
        //new c_login().powerYanzheng(Session["username"].ToString(), pagelm1, pageqx2, "2");//验证当前栏目关键字中的权限２,通常在按钮中需验证权限时使用

        #endregion
        #region 显示用户和组
        if (Session["Name"] != null && Session["UserName"] != null)
        {
            this.username.InnerHtml = Session["Name"].ToString();
            //用户组获取
            //try
            //{
            //    if (Session["Lsz"].ToString() != "")
            //    {
            //        string[] strLszs = Session["Lsz"].ToString().Split(',');
            //        foreach (string strLsz in strLszs)
            //        {
            //            string sqlSerachByYhid = "SELECT zid,zmc FROM zhuqx WHERE zid=@zid";
            //            DataTable dt1 = Sqlhelper.Serach(sqlSerachByYhid, new SqlParameter("zid", strLsz));

            //            if (dt1.Rows.Count > 0)
            //            {
            //                this.usernamex.InnerHtml = "<p>" + Session["Name"].ToString() + "</p><a title=" + dt1.Rows[0]["zmc"].ToString() + "><i class=\"fa fa-circle text-success\"></i>" + dt1.Rows[0]["zmc"].ToString() + "</a>";



            //            }
            //        }

            //    }
            //}
            //catch
            //{

            //}

        }
        else
        {

            this.username.InnerHtml = "匿名用户";
            //this.usernamex.InnerHtml = "<p>匿名用户</p><a><i class=\"fa fa-circle text-success\"></i>普通用户</a>";

        }
        #endregion
    }
}
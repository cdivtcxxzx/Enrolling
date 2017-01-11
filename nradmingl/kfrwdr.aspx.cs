using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nradmingl_export : System.Web.UI.Page
{
    #region 功能模块说明及页面基本信息说明
    //所属模块：开发任务管理
    //任务名称：开发任务导入
    //完成功能描述：演示数据导入标准写法
    //编写人：张明
    //创建日期：2016年11月26日
    //更新日期：2016年11月28日
    //版本记录：第一版,编写导入标准规范
    #endregion
    #region 页面初始化参数
    private string xwdith = "1366";//屏宽
    private string xheight = "768";//屏高
    private string pagelm1 = "项目开发管理";//请与系统栏目管理中栏目关键字设置为一致便于权限管理

    private string pageqx1 = "导入";//权限名称，根据页面的权限控制命名，与栏目管理中权限一致，最大设置为５个
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "";//当前页面值，在加载时会自动获取
    private string btitle = "";//附属标题
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            #region 页面基本配置及标题标识
            btitle = pagelm1;
            try
            {
                //读取cookies中的当前网址信息,如果没有使用服务器获取
                if (Request.Cookies["xurl"] != null)
                {
                    HttpCookie cookiesurl = Request.Cookies["xurl"];
                    webpage = cookiesurl.Value.ToString().Replace("%3A", ":").Replace("%3F", "?").Replace("%3D", "=").Replace("%26", "&");
                }
                else
                {
                    webpage = Request.Url.GetLeftPart(UriPartial.Query).ToString().Replace(Request.Url.Port.ToString(), Sqlhelper.serverport);
                }

            }
            catch (Exception e1) { Response.Write(e1.Message); }

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

                            // Response.End();前端用户启用,后台根据情况启用
                        }

                    }
                    //网站标题及META关键字设置
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
            #region 控制导入步骤各环节的显示
            if (Request["setp"] != null&&Request["mb"] != null)
            {
                if (Request["setp"].ToString() == "1")
                {
                    setpdown.Style.Add("display", "");
                    setpup.Style.Add("display", "none");
                    setpdown.HRef = "?setp=2&mb=" + Request["mb"].ToString();

                    setp1cz.Style.Add("display", "");
                    setp2cz.Style.Add("display", "none");
                    setp3cz.Style.Add("display", "none");
                }
                if (Request["setp"].ToString() == "2")
                {
                    setp2.Attributes.Add("class","active");
                    
                    setpdown.Style.Add("display", "");
                    setpup.Style.Add("display", "");
                    setpup.HRef = "?setp=1&mb=" + Request["mb"].ToString();
                    setpdown.HRef = "?setp=3&mb=" + Request["mb"].ToString();
                    setp1cz.Style.Add("display", "none");
                    setp2cz.Style.Add("display", "");
                    setp3cz.Style.Add("display", "none");
                }
                if (Request["setp"].ToString() == "3")
                {
                    setp2.Attributes.Add("class", "active");
                    setp3.Attributes.Add("class", "active");
                    setpdown.Style.Add("display", "none");
                    setpup.Style.Add("display", "");
                    setpup.HRef = "?setp=2&mb=" + Request["mb"].ToString();

                    setp1cz.Style.Add("display", "none");
                    setp2cz.Style.Add("display", "none");
                    setp3cz.Style.Add("display", "");

                }
            #endregion
                #region 根据参数提供第一步的模板下载(mb=auto:使用配置的数据库语句自动生成EXCEL,mb=文件名路径)
                if (Request["mb"] != null)
                {
                }
                else
                {
                    setp1cz.Style.Add("display", "");
                    setp2cz.Style.Add("display", "none");
                    setp3cz.Style.Add("display", "none");
                    setp1ts.Text = "<font color=red>程序员很懒,该页的导入模板参数未提供,请上报错误![出错地址:" + webpage + "]</font>";
                    this.setpdown.Style.Add("display", "none");

                }
                #endregion
                //Response.Write("第" + Request["setp"].ToString() + "步");
            }
        }
        catch (Exception err)
        {
            #region 记录页面日志方便后期分析
            if (Session["username"] != null)
            {
                new c_log().logAdd(pagelm1, pageqx1, err.Message, "2", Session["username"].ToString());//记录错误日志
            }
            else
            {
                new c_log().logAdd(pagelm1, pageqx1, err.Message, "2", "未知用户");//记录错误日志
            }
            #endregion
        }

    }
}
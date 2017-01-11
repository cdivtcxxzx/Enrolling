using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_help : System.Web.UI.Page
{
    #region 页面初始化参数

    private string pagelm1 = "问题建议";

    private string pageqx1 = "浏览";
    private string pageqx2 = "添加";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "help.aspx";//页面值

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 登陆验证及权限
        try
        {

            //将标题设置为栏目+权限
            this.Title = this.pagelm1 + "--" + pageqx1 + "-" + pageqx2 + "-" + pageqx3 + "-" + pageqx4 + "-" + pageqx5;
            //获取登陆状态
            if (!new c_login().Loginyanzheng())
            {
                //登陆验证为Flase
                //basic.MsgBox(this.Page, "登陆已超时，请重新登陆！", "~/Default.aspx");
                Response.Write("<script>top.location.href='/Default.aspx';</script>");
            }

            if (!IsPostBack)
            {
                //第一次访问这个页面时
                //获取模块权限

                if (!new c_login().powerYanzheng(Session["UserName"].ToString(), pagelm1, pageqx1, "2"))
                {
                    Response.Write("<script>alert('" + Session["Name"].ToString() + ":对不起,您无权浏览\"" + this.Title.ToString() + "\"的内容！网站已经停止"+pagelm1+"的访问!');history.go(-1)</script>");
                    Response.End();
                }
               
            }


        }
        catch (Exception ex)
        {
            new c_log().logAdd(pagelm1, pageqx1, webpage + "在PAGE_LOAD登陆验证、权限验证时出错，" + ex.StackTrace.ToString() + ",详情：" + ex.InnerException + ex.Message);
        }
        #endregion

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            //获取模块权限
            //获取登陆状态
            if (!new c_login().Loginyanzheng())
            {
                //登陆验证为Flase
                //basic.MsgBox(this.Page, "登陆已超时，请重新登陆！", "~/Default.aspx");
                Response.Write("<script>top.location.href='/Default.aspx';</script>");
            }
            if (!new c_login().powerYanzheng(Session["UserName"].ToString(), pagelm1, pageqx2, "2"))
            {
                Response.Write("<script>alert('" + Session["Name"].ToString() + ":对不起,您无权" + pageqx2 + "\"" + this.Title.ToString() + "\"的内容！!');history.go(-1)</script>");
                Response.End();
            }
            else
            {
                //日志
                new c_log().logAdd(pagelm1, pageqx2, "[页面]:"+this.TextBox7.Text+"[问题建议]:"+this.TextBox8.Text);
                basic.MsgBox(this.Page, "你的问题和建议已经提交,我们会尽快处理,如果觉得太慢,你可以致电028-64458881","-1");


            }
        }
        catch (Exception ex)
        {
            new c_log().logAdd(pagelm1, pageqx2, webpage + "在提交问题建议时出错，" + ex.StackTrace.ToString() + ",详情：" + ex.InnerException + ex.Message);
        }

    }
}
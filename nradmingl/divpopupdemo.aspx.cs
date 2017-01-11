using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_divpopupdemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.fade.Style.Add("display", "block");//大黑压框
        this.xxts.Style.Add("display", "block");//提示框
        this.xxtstitle.InnerText = "演示后台控制";//消息提示标题
        //this.xxtsnr.InnerHtml = "<font color=red>对不起，你无权操作该内容，请重试!</font>";//也可预先内置好内容!

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        this.fade.Style.Add("display", "none");//大黑压框
        this.xxts.Style.Add("display", "none");//提示框
       //隐藏掉两个DIV
    }
}
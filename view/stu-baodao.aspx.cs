using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_stu_baodao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["pk_sno"] != null && Session["pk_sno"].ToString() != "")
        {
            //检查是否确认
            if (organizationService.isStuConfrim(Session["pk_sno"].ToString()) && !organizationService.getStuConfirm(Session["pk_sno"].ToString()))
            {
                //this.server_msg.Value = "您的信息已经确认,无需再次确认";
                //todo..跳转
                //btn_submit.Visible = false;               

                Response.Write("<script>window.location.href='../../nradmingl/defaultxs.aspx';</script>"); 
            }        
        }
        
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        //获取相关参数
        if (Session["pk_sno"] != null && Session["pk_sno"].ToString() != "")
        {
            bool isCheck = checkCofirm.Checked;
            if (!isCheck)
            {
                server_msg.Value = "请勾选“已阅读”！";
                btnConfirm.Text = "重 试";
                btnConfirm.Enabled = true;
                btnConfirm.CssClass = "btnConfirm";
                return;
            }
            Response.Redirect("xsxx-confirm.aspx");
        }
        else
        {
            server_msg.Value = "参数不正确！";
            btnConfirm.Text = "重 试";
            btnConfirm.Enabled = true;
            btnConfirm.CssClass = "btnConfirm";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceReference1;  //引用时的名称
using System.Xml.Linq;
public partial class admin_changpwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //验证是否登陆
        //c_login indexLogin = new c_login();
        //indexLogin.Loginyanzheng();
        //this.Label1.Text = Session["Name"].ToString();
        //this.Image1.ImageUrl = "http://lyncex.cdivtc.com:8001/Photo/UploadHead.aspx?operation=get&user=" + Session["UserName"].ToString();

        //this.Label5.Text = Session["Usergroup"].ToString(); 
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //用户修改密码
        string successMessage = "修改成功";
        //string failMessage = "";
        
        if (Session["UserName"] != null)
        {
            string str2 = Session["UserName"].ToString();
            string oldpwd = ((TextBox)formView1.Controls[0].FindControl("TextBox7")).Text.Trim();
            string newpwd1 = ((TextBox)formView1.Controls[0].FindControl("TextBox6")).Text.Trim();
            string newpwd2 = ((TextBox)formView1.Controls[0].FindControl("TextBox5")).Text.Trim();
            
            if (oldpwd != "" && newpwd1 != "" && newpwd2 != "")
            {
                if (newpwd1 != newpwd2 || newpwd1.Length < 6)
                {
                    //failMessage = "对不起你的新密码两次输入不一样或新密码位数少于6位，请重新输入";
                    basic.MsgBox(this.Page, "对不起你的新密码两次输入不一样或新密码位数少于6位，请重新输入", "changpwd.aspx");
                    //Response.Write("<script>alert('对不起你的新密码两次输入不一样或新密码位数少于6位，请重新输入')</script>");
                }
                else
                {
                    if (oldpwd.Length > 5)
                    {

                        
                        int result = new Account().ChangePwd(str2, oldpwd, newpwd1);
                        if (result == 1)
                        {
                            successMessage = successMessage + ",新密码为：***" + newpwd1.Substring(4, newpwd1.Length - 4) + ",请重新登陆！";

                            string lxdh = ((TextBox)formView1.Controls[0].FindControl("Label4")).Text.Trim();
                            string yhdh = ((TextBox)formView1.Controls[0].FindControl("TextBox4")).Text.Trim();
                            try
                            {
                                Sqlhelper.ExcuteNonQuery("update yonghqx set yhdh='" + yhdh + "',lxdh='"+lxdh+"' where yhid='" + str2 + "'");
                                 successMessage = successMessage + "电话信息已更新";
                                //basic.MsgBox(this.Page, successMessage, "-1");
                            }
                            catch (Exception ex)
                            {

                                new c_log().logAdd("修改密码", "修改", "用户为" + str2 + "修改电话:" + yhdh + ex.Message);
                            }
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script language=javascript>alert('" + successMessage + "');top.location.href='" + "logout.aspx" + "';</script>");
                            //Response.RedirectLocation = "top";
                            //Response.Redirect("", true);
                            //basic.MsgBox(this.Page, successMessage, "logout.aspx");
                        }
                        else if (result == -1) { basic.MsgBox(this.Page, "修改密码失败！请检查你原来的密码是否正确", "changpwd.aspx"); }
                        else { basic.MsgBox(this.Page, "修改密码失败！请重试", "changpwd.aspx"); }
                    }
                    else
                    {
                        //failMessage = "对不起你原来的密码位数少于6位，请重新输入";
                        basic.MsgBox(this.Page, "对不起你原来的密码位数少于6位，请重新输入", "changpwd.aspx");
                        // Response.Write("<script>alert('对不起你原来的密码位数少于6位，请重新输入')</script>");
                    }
                }
                
                
            }
            else if (oldpwd == "" && newpwd1 == "" && newpwd2 == "")
            {
                //修改联系电话
                string lxdh = ((TextBox)formView1.Controls[0].FindControl("Label4")).Text.Trim();
                string yhdh = ((TextBox)formView1.Controls[0].FindControl("TextBox4")).Text.Trim();
                try
                {
                    Sqlhelper.ExcuteNonQuery("update yonghqx set yhdh='" + yhdh + "',lxdh='" + lxdh + "' where yhid='" + str2 + "'");
                    successMessage = successMessage + ",电话信息已更新";
                    basic.MsgBox(this.Page, successMessage, "changpwd.aspx");
                }
                catch (Exception ex)
                {

                    new c_log().logAdd("修改密码", "修改", "用户为" + str2 + "修改电话:" + yhdh + ex.Message);
                }
                
            }
            basic.MsgBox(this.Page, "请正确填写数据", "changpwd.aspx");
        }
        else basic.MsgBox(this.Page, "登陆已超时，请重新登陆", "logout.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //根据当前用户登陆ID转到用户权限更新及分配页面
        Response.Redirect("showPower.aspx");
    }
}
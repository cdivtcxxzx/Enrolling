using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_LoginOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Response.Cookies["LoginUser"] != null)
            {
                HttpCookie killCookie = new HttpCookie("LoginUser");
                killCookie.Value = DateTime.Now.ToString();
                killCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(killCookie);
            }
            if (Session["UserName"] != null && Session["Name"] != null)
            {
                Session["Name"] = null;
                Session["UserName"] = null;
                Session.Clear();
                Session.Abandon(); 
                Response.Buffer = true;
                Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
                Response.Cache.SetNoStore();
            }
            Response.Redirect("~/Default.aspx");
        }
    }
}
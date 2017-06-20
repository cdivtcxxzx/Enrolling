using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class loginsf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }
    protected void Button1_Click2(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }
    protected void Button1_Click3(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx?sf=czy&url=/nradmingl/defaultczy.aspx");
    }
    protected void Button1_Click4(object sender, EventArgs e)
    {
        Response.Redirect("login.aspx");
    }
}
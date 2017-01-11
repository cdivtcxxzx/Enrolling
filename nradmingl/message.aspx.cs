using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_message : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string mode = Request.QueryString["mode"];
        string id = Request.QueryString["id"];
        string url = Request.QueryString["url"];
        new Message().Readed(id);
        if (mode == "normal") { url += "?id=" + id; }
        Response.Redirect(url);

    }
}
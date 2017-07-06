using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nradmingl_bzr_confirm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            throw new Exception("没登陆");
        }
        this.pk_staff_no.Value = Session["username"].ToString().Trim();
    }
}
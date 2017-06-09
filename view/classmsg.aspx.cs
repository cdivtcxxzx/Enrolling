using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_classmsg : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        string pk_sno =null ;//获取学号
        if (Session["username"] == null)
        {
            this.server_msg.Value = "参数错误";
            return;
        }
        this.pk_sno.Value = Session["username"].ToString().Trim();

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class nradmingl_yktphoto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["url"] != null)
        {
            DataTable ph = Sqlhelper.yktSerach("SELECT [photo_name],[photo_path] FROM [dt_photo] where user_serial='" + Request["url"].ToString() + "'");
            if (ph.Rows.Count > 0)
            {
                this.tp.InnerHtml="<img src=\"" + ph.Rows[0][1].ToString().Replace("../", "") + "/" + ph.Rows[0][0].ToString() + "\"  >";
            }
        }
    }
}
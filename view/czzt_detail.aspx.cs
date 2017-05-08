using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_czzt_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cs = Request.QueryString["cs"];
        this.cs.Value = cs;
        string pk_batch_no = Request.QueryString["pk_batch_no"];
        this.pk_batch_no.Value = pk_batch_no;
        string pk_collage_no = Request.QueryString["pk_collage_no"];
        this.pk_collage_no.Value = pk_collage_no;

    }
}
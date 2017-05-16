using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_bzr_xsjbxx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string pk_sno = Request.QueryString["pk_sno"];//获取学号
        if (pk_sno == null || pk_sno.Trim().Length == 0)
        {
            this.server_msg.Value = "参数错误";
            return;
        }

        this.hidden_pk_sno.Value = pk_sno;
        if (!IsPostBack)
        {

           
            

        }

    }
}
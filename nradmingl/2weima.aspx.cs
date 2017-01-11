using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nradmingl_2weima : System.Web.UI.Page
{
    protected string myurl = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["url"] != null)
        {
            //Response.Write(Request["url"].ToString().Replace('|','&'));
           myurl = Request["url"].ToString().Replace('|', '&');
          //this.jsdiv.InnerHtml="<script>              function ok(){$('#code').qrcode('" + myurl + "'); }</scirpt>";
               
              
            
        }
    }
}
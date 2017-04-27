using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_ssfp_zp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取寝室照片信息
        string zp = "../images/xsgysmall.jpg";
        string zpbig = "../images/xsgysmall.jpg";
        if(Request["img"]!=null)
        {
            zpbig = Request["img"].ToString();
        }


        
        this.shuseImg.Src = zpbig;
        //this.shuseImg.Attributes.Add("onclick", "location.href='ssfp_zp.aspx?img=" + zpbig + "'");
    }
}
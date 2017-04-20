using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_xsxx_confirm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //get post获取学号
        //string pk_sno = Request["pk_sno"].ToString();
        hidden_pk_sno.Value = "2";

        //检查是否确认
        if (organizationService.isStuConfrim("2"))
        {
            hidden_alert_msg.Value = "您的信息已经确认！";
            //todo..跳转
        }

    }
}
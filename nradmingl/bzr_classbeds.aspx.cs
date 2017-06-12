using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_bzr_classbeds : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //string pk_staff_no = Request.QueryString["pk_staff_no"];//获取员工编号
        //if (pk_staff_no == null || pk_staff_no.Trim().Length == 0)
        //{
        //    throw new Exception("参数错误");
        //}

        //Session["pk_staff_no"] = pk_staff_no;

        if (Session["username"] == null)
        {
            throw new Exception("没登陆");
        }
        this.pk_staff_no.Value = Session["username"].ToString().Trim();
    }
}
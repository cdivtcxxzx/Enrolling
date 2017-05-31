using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nradmingl_Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string pk_batch_no = Request.QueryString["pk_batch_no"];//获取迎新批次
        //if (pk_batch_no == null || pk_batch_no.Trim().Length == 0)
        //{
        //    throw new Exception("参数错误");
        //}
        //Session["pk_batch_no"] = pk_batch_no;

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
        //this.pk_batch_no.Value = Session["pk_batch_no"].ToString().Trim();
        this.pk_staff_no.Value = Session["username"].ToString().Trim();
    }
}
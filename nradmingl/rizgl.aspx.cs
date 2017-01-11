using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class admin_rizgl : System.Web.UI.Page
{
    #region 页面初始化参数

    private string pagelm1 = "日志管理";

    private string pageqx1 = "浏览";
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "rizgl.aspx";//页面值
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        new c_login().tongyiyz(pagelm1, pageqx1, "进入日志管理页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
        //  c_login indexLogin = new c_login();
        //  indexLogin.Loginyanzheng();
        if (!IsPostBack)
        {
            ViewState["sql"] = "SELECT rizlog.id, rizlog.time, lanm.lmmc, rizlog.userid, rizlog.ip, rizlog.cznr, quanxdm.qxmc, zhuqx.ZMC FROM rizlog INNER JOIN lanm ON rizlog.lm = lanm.lmid INNER JOIN quanxdm ON rizlog.lx = quanxdm.qxid INNER JOIN zhuqx ON rizlog.usergroup = zhuqx.ZID order by time desc";//viewstate筛选出来的sql字串
            bind();
            gvbind(ViewState["sql"].ToString ());
            // GridView1.DataSource = new uum().getAll();
            // GridView1.DataBind();
        }
    }
    #region 设置页面显示条数事件
    protected void PageSize_Go(object sender, EventArgs e)
    {
        //this.DropDownList2.Items.Insert(0, new ListItem("全部"));

        TextBox ps = (TextBox)this.GridView1.BottomPagerRow.FindControl("PageSize_Set");
        if (!string.IsNullOrEmpty(ps.Text))
        {

            int _PageSize = 0;

            if ((Int32.TryParse(ps.Text, out _PageSize) == true) && _PageSize > 0)
            {

                GridView1.PageSize = _PageSize;
                gvbind(ViewState["sql"].ToString());
             /*   if (ViewState["sql"].ToString() == "")
                { gvbind(); }
                else
                {
                    string sql;
                    sql = ViewState["sql"].ToString();

                    DataTable dt = new DataTable();
                    dt = Sqlhelper.Serach(sql);
                    GridView1.DataSource = dt.DefaultView;
                    GridView1.DataBind();
                }*/

            }

        }
    }
    #endregion
    #region 分页事件


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        sortgvbind(ViewState["sql"].ToString());

       
       

    }

    #endregion


    #region 定向转到

    protected void LinkButtonGo_Click(object sender, EventArgs e)
    {

        LinkButton lbtn_go = (LinkButton)this.GridView1.BottomPagerRow.FindControl("LinkButtonGo");

        TextBox txt_go = (TextBox)this.GridView1.BottomPagerRow.FindControl("txt_go");

        if (!string.IsNullOrEmpty(txt_go.Text))
        {

            int PageToGo = 0;

            if ((Int32.TryParse(txt_go.Text, out PageToGo) == true) && PageToGo > 0)
            {

                lbtn_go.CommandName = "Page";

                lbtn_go.CommandArgument = PageToGo.ToString();

            }

        }

    }

    #endregion
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridView1.AllowPaging = false;//取消分页,使GridView显示全部数据.
        
        gvbind (ViewState["sql"].ToString ());//重新绑定.
        Response.Clear();
        Response.Charset = "GB2312";
        Response.AppendHeader("Content-Disposition", "attachment;filename= " + Server.UrlEncode(DateTime.Now.ToShortDateString()+"导出.xls"));
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件.
        this.EnableViewState = false;
        System.Globalization.CultureInfo myCItrad = new System.Globalization.CultureInfo("ZH-CN", true);
        System.IO.StringWriter stringWrite = new System.IO.StringWriter(myCItrad);
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        GridView1.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.Write(@"<style> .text { mso-number-format:\@; } </script> ");
        Response.End();
        GridView1.AllowPaging = true;
        //将查询结果导出到EXCEL
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sql;
        sql = "SELECT rizlog.id, rizlog.time, lanm.lmmc, rizlog.userid, rizlog.ip, rizlog.cznr, quanxdm.qxmc, zhuqx.ZMC FROM rizlog INNER JOIN lanm ON rizlog.lm = lanm.lmid INNER JOIN quanxdm ON rizlog.lx = quanxdm.qxid INNER JOIN zhuqx ON rizlog.usergroup = zhuqx.ZID where 1=1";
        if (!((TextBox1.Text == "") || (TextBox1.Text == "登陆名")))
        {
            sql = sql+"and userid like" + "'" + TextBox1.Text + "%'";
        }
            if (DropDownList2.SelectedValue.ToString()!="")
            { sql = sql+"and zmc="+"'"+DropDownList2.SelectedValue.ToString()+"'"; }
            if (DropDownList3.SelectedValue.ToString()!="")
            { sql = sql + "and lmmc=" + "'" + DropDownList3.SelectedValue.ToString() + "'"; }
            if (DropDownList4.SelectedValue.ToString() != "")
            { sql = sql + "and qxmc=" + "'" + DropDownList4.SelectedValue.ToString() + "'"; }
            if ((TextBox3.Text != "") && (TextBox4.Text != ""))
            { sql = sql + "and time between" + "'" + TextBox3.Text + "'" + "and" + "'" + TextBox4.Text + "'"; }


            ViewState["sql"] = sql + "order by time desc";
        gvbind(ViewState["sql"].ToString());
        //根据条件查询,将结果输出到下gridview表
        //要求：栏目、操作、用户组，在绑定数据前面要加一个空值。
        //如<select id=yhz ><option value="">用户组选择</option><option value="1">超级管理员</option><option value="2">普通管理员</option></select>


    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string[] chkIds = null;
   
        string xtje = Request["hdfWPBH"].ToString();
        string batchRegroup = xtje.TrimEnd(',');//通过这种方式来获得前台隐藏域的内容  
        if (batchRegroup.Length != 0)
        {
            chkIds = batchRegroup.Split(',');
        }
        //string sql = "";
        for (int i = 0; i < chkIds.Length; i++)
        {
            //将传过来的ID记录状态改为删除
            //sql = "UPDATE T_WPXX_CK SET SPR='" + userrealName + "' WHERE ID='" + chkIds[i] + "'";
           // wpck.auditOrDelete(sql);//传入SQL语句并执行  
           Response.Write("<script>alert('本页不允许删除记录！');</script>");
                  
        }
       // ClientScript.RegisterStartupScript(this.GetType(), "pass", "alert('审核通过!');", true);
        //GridViewShow_CK();//GridView绑定数据显示方法  
    }

    protected void bind()
    {
        string sql;
        string sql2;
        string sql3;
        sql = "select * from zhuqx";
        sql2 = "select * from lanm";
        sql3 = "select * from quanxdm";
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        dt = Sqlhelper.Serach(sql);
        dt2 = Sqlhelper.Serach(sql2);
        dt3 = Sqlhelper.Serach(sql3);
        DropDownList2.DataSource = dt.DefaultView;
        DropDownList3.DataSource = dt2.DefaultView;
        DropDownList4.DataSource = dt3.DefaultView;
        //DropDownList1.DataTextField = "xb";

        DropDownList2.DataValueField = "zmc";
        DropDownList2.DataBind();
        DropDownList3.DataValueField = "lmmc";
        DropDownList3.DataBind();
        DropDownList4.DataValueField = "qxmc";
        DropDownList4.DataBind();

        DropDownList2.Items.Insert(0, new ListItem("选择用户组", ""));
        DropDownList3.Items.Insert(0, new ListItem("栏目选择", ""));
        DropDownList4.Items.Insert(0, new ListItem("权限选择", ""));
       
    }
    /// <summary>
    ///重新邦定数据库
    /// </summary>
    protected void gvbind(string sql)  //gridview绑定数据
    {
       // string sql = "SELECT rizlog.id, rizlog.time, lanm.lmmc, rizlog.userid, rizlog.ip, rizlog.cznr, quanxdm.qxmc, zhuqx.ZMC FROM rizlog INNER JOIN lanm ON rizlog.lm = lanm.lmid INNER JOIN quanxdm ON rizlog.lx = quanxdm.qxid INNER JOIN zhuqx ON rizlog.usergroup = zhuqx.ZID order by time desc";
        DataTable dt = new DataTable();
        dt=Sqlhelper.Serach(sql);
        Session["total"] = dt.Rows.Count;
        GridView1.DataSource = dt.DefaultView;
        GridView1.DataBind();
        
        
        
    }
    protected void sortgvbind(string sql)
    {
        string SortExpression = this.GridView1.Attributes["SortExpression"];
        string SortDirection = this.GridView1.Attributes["SortDirection"];
        DataTable dt = new DataTable();
        dt = Sqlhelper.Serach(sql);
        Session["total"] = dt.Rows.Count;
        GridView1.DataSource = dt.DefaultView;
        if ((!string.IsNullOrEmpty(SortExpression)) && (!string.IsNullOrEmpty(SortDirection)))
        {
            dt.DefaultView.Sort = string.Format("{0} {1}", SortExpression, SortDirection);


        }

        GridView1.DataBind();
    }
   
    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string SortExpression;
        string SortDirection = "ASC";
        SortExpression = e.SortExpression.ToString();
        if (SortDirection == this.GridView1.Attributes["SortDirection"])//判断排序方向
        { SortDirection = (this.GridView1.Attributes["SortDirection"].ToString() == SortDirection ? "DESC" : "ASC"); }
        this.GridView1.Attributes["SortExpression"] = SortExpression;
        this.GridView1.Attributes["SortDirection"] = SortDirection;
       
       
        // Session["xingscx_orderBy"] = SortExpression + " " + SortDirection;
        // ViewState["sqltemp"] = ViewState["sql"].ToString().Replace("order by xuesjbsj.xsid", "order by xuesjbsj." + Session["xingscx_orderBy"].ToString());
        // Response.Write(ViewState["sql"].ToString());      
        sortgvbind(ViewState["sql"].ToString());

    }
}
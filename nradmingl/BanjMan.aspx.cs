using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_BanjMan : System.Web.UI.Page
{
    #region 页面初始化参数

    private string pagelm1 = "班级管理";

    private string pageqx1 = "浏览";
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "BanjMan.aspx";//页面值

    protected int rownum;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        //登陆验证,权限验证,日志
        new c_login().tongyiyz(pagelm1, pageqx1, "进入班级管理页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
        if (!IsPostBack)
        {
            //ViewState["sortOrder"] = "ASC";
            //ViewState["sortBy"] = "yhid";
            //DataTable dt = new Account().getAllUser();
            //foreach (DataRow x in dt.Rows)
            //{  
            //    x[2]=new Power().getZhuMCs(x.ItemArray[2].ToString());

            //}
            //GridView1.DataSource = dt;

            //ViewState["GridView1_DataSource"] = GridView1.DataSource;
            //Bind();
            string school_id=new Banj().getSchool(Session["UserName"].ToString());
            if(school_id!="")
            {
            nav_1.NavigateUrl = "BanjUpdate.aspx?mode=add&school="+school_id;
            }
            else { basic.MsgBox(this.Page, "不是学校帐号不能管理班级", "-1"); }
        }
    }
    #region 设置页面显示条数事件
    protected void PageSize_Go(object sender, EventArgs e)
    {

        TextBox ps = (TextBox)this.GridView1.BottomPagerRow.FindControl("PageSize_Set");
        if (!string.IsNullOrEmpty(ps.Text))
        {

            int _PageSize = 0;

            if ((Int32.TryParse(ps.Text, out _PageSize) == true) && _PageSize > 0)
            {

                GridView1.PageSize = _PageSize;

                //Bind();
            }

        }
    }
    #endregion
    #region 分页事件



    //protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    //{

    //        ViewState["sortBy"] = e.SortExpression;
    //        if (e.SortDirection.ToString() != ViewState["sortOrder"].ToString())
    //        {
    //            switch(ViewState["sortOrder"].ToString())
    //            {
    //                case "DESC":
    //                ViewState["sortOrder"] = "ASC";
    //                break;
    //                case "ASC":
    //                ViewState["sortOrder"] = "DESC";
    //                break;
    //            }
    //        }
    //        Bind();

    //}


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //Bind();
    }

    #endregion

    protected string GetPower1(string lmyyqxs)
    {
        string xtje = "";
        if (lmyyqxs.Length <= 36)
        {
            xtje = lmyyqxs;
        }
        else
        {
            xtje = "<a href='#' txttop='txttop' title='" + lmyyqxs + "'>" + lmyyqxs.Substring(21, 14) + "</a>";
        }
        return xtje;
    }
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

    protected void Search_Onclick(object sender, EventArgs e)
    {
        //TextBox tb = (TextBox)this.form1.FindControl("searchtext");
        //DataTable dt = new  Account().getByKey(tb.Text);
        GridView1.DataSourceID = "accountSearch";
        //GridView1.DataSource = dt;
        //ViewState["GridView1_DataSource"] = GridView1.DataSource;
        //Bind();
    }

    //protected void Bind()
    //{
    //    DataTable dt = ViewState["GridView1_DataSource"] as DataTable;
    //    DataView dv = new DataView(dt);
    //    dv.Sort = ViewState["sortBy"].ToString() + " " + ViewState["sortOrder"].ToString();
    //    GridView1.DataSource = dv;
    //    GridView1.DataBind();
    //}


    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        //   this.Label1.Text = "xxxx" + "<br>";
        GridView _gridView = (GridView)sender;
        string id;
        //获得行索引

        id = e.CommandArgument.ToString();

        if (e.CommandName == "删除")
        {       //string xx = textbox.Text;
            if (Sqlhelper.ExcuteNonQuery("delete from ZY_BANJ  where banj_id='" + id + "'") > 0)
            {
                Label1.Text = "<font color=green>删除" + id + "成功！</font>";
            }
            else
            {
                Label1.Text = "<font color=red>删除" + id + "失败</font>";
            }

        }

        //Sql_yh.SelectCommand = ViewState["SqlDataSource1.SelectCommand"].ToString();
        _gridView.DataBind();
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {

    }
    protected void srcAccount_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        DataTable dt = e.ReturnValue as DataTable;
        if (dt == null)
        {
            rownum = 0;
        }
        else
        {
            rownum = dt.Rows.Count;
        }
    }
    protected void accountSearch_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        DataTable dt = e.ReturnValue as DataTable;
        if (dt == null)
        {
            rownum = 0;
        }
        else
        {
            rownum = dt.Rows.Count;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_cs : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {


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
}
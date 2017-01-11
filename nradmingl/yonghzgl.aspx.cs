using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_yonghzgl : System.Web.UI.Page
{
    protected int num = 0;
    #region 页面初始化参数

    private string pagelm1 = "用户组管理";

    private string pageqx1 = "浏览";
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "yonghzgl.aspx";//页面值
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        new c_login().tongyiyz(pagelm1, pageqx1, "进入用户组管理页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
        if (!IsPostBack)
        {
            

                Session["YHZTotalRows"] = new Power().ZhuPower().Rows.Count;
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

            }

        } 
    }
    #endregion
    #region 分页事件


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
       
        
    }

    #endregion
    protected string GetZMembers(string zid)
    {
        num = 0;
        string members = "";
        DataTable dt = Sqlhelper.Serach("select xm,yhid from yonghqx where lsz like @zid+',%' or lsz like '%,'+@zid+',%'",new SqlParameter("zid",zid));
        foreach (DataRow x in dt.Rows)
        {
            members += x["xm"]+",";
            num++;
        }
        return members.TrimEnd(',');
    }
    protected string GetPower1(string lmyyqxs)
    {
        string xtje = "";
        if (lmyyqxs.Length <= 96)
        {
            xtje = lmyyqxs;
        }
        else
        {
            xtje = "<a href='#' txttop='txttop' title='" + lmyyqxs + "'>" + lmyyqxs.Substring(21, 64) + "</a>";
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
    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        //   this.Label1.Text = "xxxx" + "<br>";
        GridView _gridView = (GridView)sender;
        string id;
        //获得行索引

        id = e.CommandArgument.ToString();

        if (e.CommandName == "删除")
        {       //string xx = textbox.Text;
            if (Sqlhelper.ExcuteNonQuery("delete from [zhuqx]  where zid='" + id + "'") > 0)
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

}
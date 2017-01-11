using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_xwglmy : System.Web.UI.Page
{
    #region 页面初始化参数

    private string pagelm1 = "我的信息管理";

    private string pageqx1 = "浏览";
    private string pageqx2 = "修改";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";


    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        new c_login().tongyiyz(pagelm1, pageqx1, "进入" + pagelm1 + "页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
        if (!IsPostBack)
        {
            ViewState["gridsql"] = SqlDataSource1.SelectCommand;
        }
        else
        {

            SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
        }
        this.SqlDataSource3.FilterExpression = new Power().Getlanm("glqx");
        this.SqlDataSource2.FilterExpression = new Power().Getlanm("glqx");
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
                //this.SqlDataSource1.SelectCommand = ViewState["SqlDataSource1.SelectCommand"].ToString();
                //GridView1.DataBind();
                //GV_DataBind();
            }

        }
    }
    #endregion
    #region 分页事件总页数

    protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ViewState["count"] = e.AffectedRows;
        //ViewState["countbd"] = getbds();
        //int s=GridView1.Rows
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
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
        //this.SqlDataSource1.SelectCommand = ViewState["SqlDataSource1.SelectCommand"].ToString();
        //GridView1.DataBind();

    }

    #endregion
    #region 始终显示下部控制区
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        if (this.GridView1.Rows.Count != 0)
        {
            Control table = this.GridView1.Controls[0];
            int count = table.Controls.Count;
            table.Controls[count - 1].Visible = true;
        }
    }
    #endregion

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        GridView _gridView = (GridView)sender;
        string id, sql;

        id = e.CommandArgument.ToString();
       



        if (e.CommandName == "删除")
        {
            //string xx = textbox.Text;
           
            if (Sqlhelper.ExcuteNonQuery("DELETE FROM xw_neirong  where id=" + id + " ") > 0)
            {
                this.Label1.Text = "<font color=green> &nbsp;&nbsp;&nbsp;&nbsp;新闻删除成功!</font>";
            }
            else
            {
                this.Label1.Text = "<font color=red> &nbsp;&nbsp;&nbsp;&nbsp;新闻删除失败,请重试!!</font>";
            }
        }
        ViewState["gridsql"] = SqlDataSource1.SelectCommand;
        SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
        _gridView.DataBind();
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
        try
        {
            int cg = 0;
            int sb = 0;
            string sbjl = "";

            for (int i = 0; i < chkIds.Length; i++)
            {
                string xm1 = "";
                string zt1 = "";
                //将传过来的ID记录状态改为删除
                //sql = "UPDATE T_WPXX_CK SET SPR='" + userrealName + "' WHERE ID='" + chkIds[i] + "'";
                // wpck.auditOrDelete(sql);//传入SQL语句并执行  
               // DataTable xm = Sqlhelper.Serach("select 姓名,领取状态 from byz where id=" + chkIds[i] + "");
                if (Sqlhelper.ExcuteNonQuery("DELETE FROM xw_neirong  where id=" + chkIds[i] + " ") > 0)
                {
                   // this.Label1.Text = "<font color=green>新闻删除成功!</font>";
                    cg = cg + 1;
                }
                else
                {
                    sb = sb + 1;
                    sbjl = " &nbsp;&nbsp;&nbsp;&nbsp;有" + sb + "条新闻删除失败";
                }
                //Response.Write("<script>alert('" + chkIds[i] + "');</script>");

            }
            this.Label1.Text = "<font color=green> &nbsp;&nbsp;&nbsp;&nbsp;共删除" + cg + "条新闻!</font><font color=red>" + sbjl + "</font>";
        }
        catch
        {
            this.Label1.Text = "<font color=red> &nbsp;&nbsp;&nbsp;&nbsp;批量删除出错！</font>";
        }


    }


    protected void Search_Onclick(object sender, ImageClickEventArgs e)
    {
        if (this.DropDownList2.SelectedValue.Length > 0)
        {
            //有子栏目
            this.LB_top.Text = "新闻管理&gt;&gt;&nbsp;" + this.DropDownList1.SelectedItem.Text + "&gt;&gt;&nbsp;" + this.DropDownList2.SelectedItem.Text;
            this.SqlDataSource1.SelectCommand = "SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc, xw_neirong.isyn,xw_neirong.title, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.id FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where (xw_neirong.lmid='" + this.DropDownList2.SelectedValue + "' and xw_neirong.title like '%" + this.searchtext.Text + "%') and xw_neirong.fabuyhid='"+Session["UserName"].ToString()+"'";
            ViewState["gridsql"] = SqlDataSource1.SelectCommand;
            SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
            GridView1.DataBind();
        }
        else
        {
            //无子栏目
            this.LB_top.Text = "新闻管理&gt;&gt;&nbsp;" + this.DropDownList1.SelectedItem.Text;
            this.SqlDataSource1.SelectCommand = "SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc,xw_neirong.isyn, xw_neirong.title, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.id FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where (xw_neirong.lmid='" + this.DropDownList1.SelectedValue + "' and xw_neirong.title like '%" + this.searchtext.Text + "%') and xw_neirong.fabuyhid='" + Session["UserName"].ToString() + "'";
            ViewState["gridsql"] = SqlDataSource1.SelectCommand;
            SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
            GridView1.DataBind();
        }
    }
    protected string imagestu(string images)
    {
        if (images.Length > 0)
        {
            return "[图]";
        }
        return "";
    }

    protected string xwzt(string isyn)
    {
        if (isyn=="0")
        {
            return "<font color=red>未审核</font>";
        }
        if (isyn == "1")
        {
            return "<font color=green>已审核</font>";
        }
        if (isyn == "2")
        {
            return "<font color=red>被打回</font>";
        }
        return "未审核";
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.DropDownList2.SelectedValue.Length > 0)
        {
            //有子栏目
            this.LB_top.Text = "新闻管理&gt;&gt;&nbsp;" + this.DropDownList1.SelectedItem.Text + "&gt;&gt;&nbsp;" + this.DropDownList2.SelectedItem.Text;
            this.SqlDataSource1.SelectCommand = "SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc,xw_neirong.isyn, xw_neirong.title, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.id FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where (xw_neirong.lmid='" + this.DropDownList2.SelectedValue + "') and xw_neirong.fabuyhid='" + Session["UserName"].ToString() + "'";
            ViewState["gridsql"] = SqlDataSource1.SelectCommand;
            SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
            GridView1.DataBind();
        }
        else
        {
            this.LB_top.Text = "新闻管理&gt;&gt;&nbsp;" + this.DropDownList1.SelectedItem.Text;
            //无子栏目
            this.SqlDataSource1.SelectCommand = "SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc, xw_neirong.title,xw_neirong.isyn, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.id FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where (xw_neirong.lmid='" + this.DropDownList1.SelectedValue + "') and xw_neirong.fabuyhid='" + Session["UserName"].ToString() + "'";
            ViewState["gridsql"] = SqlDataSource1.SelectCommand;
            SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
            GridView1.DataBind();
        }
    }

    protected void DropDownList2_DataBound(object sender, EventArgs e)
    {
      // this.Label2.Text=DropDownList2.Items.Count.ToString();
       try
       {
           if (DropDownList2.Items.Count > 0)
           {
               this.DropDownList2.Style.Add("display","");
           }
           else
           {
               this.DropDownList2.Style.Add("display", "none");
           }
       }
       catch (Exception ex) {  }
    }
}
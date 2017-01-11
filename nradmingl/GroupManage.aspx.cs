using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_GroupManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        lb1.Text = GetZmc();
    }
    protected string GetZmc()
    {
        string zid = Request.QueryString["zid"];

        try
        {
            return Sqlhelper.Serach("select zmc from zhuqx where zid='" + zid + "'").Rows[0]["zmc"].ToString();
        }
        catch (Exception)
        {

            return "";
        }
    }
    protected void Button_Search_Click(object sender, EventArgs e)
    {

        //SqlDataSource1.SelectCommand = "SELECT yhid,xm,uumzw from yonghqx where yhid like '%" + TextBox1.Text + "%' or xm like '%" + TextBox1.Text + "%' or uumzw like '%" + TextBox1.Text + "%'";
        ////SqlDataSource1.FilterExpression = new Power().GetFilterExpression("yxdm");
        //GridView1.EmptyDataText = "未找到相应用户，请确认搜索条件";
        //GridView1.DataBind();
        //this.Label2.Visible = false;
    }
        #region 设置页面显示条数事件
    protected void PageSize_Go(object sender, EventArgs e)
    {
        //this.DropDownList2.Items.Insert(0, new ListItem("全部"));

        TextBox ps = (TextBox)this.GV_xs.BottomPagerRow.FindControl("PageSize_Set");
        if (!string.IsNullOrEmpty(ps.Text))
        {

            int _PageSize = 0;

            if ((Int32.TryParse(ps.Text, out _PageSize) == true) && _PageSize > 0)
            {

                GV_xs.PageSize = _PageSize;

            }

        }
    }
    #endregion
    #region 分页事件总页数

    protected void Sql_xs_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        int count = e.AffectedRows;
        ViewState["count"] = count;
        //if (count > 10) { Sql_xs.SelectCommand = ""; GV_xs.EmptyDataText = "符合条件学生过多!请输入更准确的数据重新搜索";Sql_xs.FilterExpression="yxdm = '111111'"; GV_xs.DataBind(); }
    }
    protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        int count = e.AffectedRows;
        ViewState["search_count"] = count;
        //if (count > 10) { SqlDataSource1.SelectCommand = ""; GridView1.EmptyDataText = "符合条件用户过多!请输入更准确的数据重新搜索"; SqlDataSource1.FilterExpression = "yxdm = '111111'"; GridView1.DataBind(); }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_xs.PageIndex = e.NewPageIndex;



    }

    #endregion


    #region 定向转到

    protected void LinkButtonGo_Click(object sender, EventArgs e)
    {

        LinkButton lbtn_go = (LinkButton)this.GV_xs.BottomPagerRow.FindControl("LinkButtonGo");

        TextBox txt_go = (TextBox)this.GV_xs.BottomPagerRow.FindControl("txt_go");

        if (!string.IsNullOrEmpty(txt_go.Text))
        {

            int PageToGo = 0;

            if ((Int32.TryParse(txt_go.Text, out PageToGo) == true) && PageToGo > 0)
            {

                lbtn_go.CommandName = "Page";

                lbtn_go.CommandArgument = PageToGo.ToString();

            }

        }
        //this.Sql_xs.SelectCommand = ViewState["Sql_xs.SelectCommand"].ToString();
        //GV_xs.DataBind();

    }

    #endregion
    /// <summary>
    /// 点击页面中的按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region 删除成员
        if (e.CommandName == "shanchu")
        {
            string zid = Request.QueryString["zid"];
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GV_xs.Rows[index];
            TableCell yhid = selectedRow.Cells[0];
            new Power().RemoveLsz(yhid.Text,zid);
            GV_xs.DataBind();
        }
        #endregion
        #region 添加成员
        if (e.CommandName == "tianjia")
        {
            string zid = Request.QueryString["zid"];
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView1.Rows[index];
            TableCell yhid = selectedRow.Cells[0];
            new Power().AddLsz(yhid.Text, zid);
            GridView1.DataBind();
            GV_xs.DataBind();
        }
        #endregion
        //    #region 转系
    //    if (e.CommandName == "toAnotherDep")
    //    {
    //        this.panel_zx.Visible = true;
    //        this.hzyk.Visible = false;
    //        int index = Convert.ToInt32(e.CommandArgument);
    //        GridViewRow selectedRow = GV_xs.Rows[index];
    //        TableCell xsid = selectedRow.Cells[0];
    //        TableCell xm = selectedRow.Cells[1];
    //        TableCell sfz = selectedRow.Cells[2];
    //        TableCell yx = selectedRow.Cells[3];
    //        TableCell zy = selectedRow.Cells[4];
    //        Label_zx_xsid.Text = xsid.Text;
    //        Label_zx_xm.Text = xm.Text;
    //        Label_zx_sfz.Text = sfz.Text;
    //        Label_zx_yx.Text = yx.Text;
    //        Label_zx_zy.Text = zy.Text;
    //        //ClientScriptManager clm = Page.ClientScript;
    //        //clm.RegisterClientScriptBlock(Page.GetType(),"show_dialog", "$('#dialog').dialog({resizable: false,width: 433,modal: true,buttons: {'取消': function () {$(this).dialog('close');}}});",true);
    //        //clm.RegisterStartupScript(Page.GetType(), "show_dialog", "$('#dialog').dialog({resizable: false,width: 433,modal: true,buttons: {'取消': function () {$(this).dialog('close');}}});", true);
    //        //HttpContext.Current.Response.
    //        //Response.Write("<script type='text/javascript'> $('#dialog').dialog({resizable: false,width: 433,modal: true,buttons: {'取消': function () {$(this).dialog('close');}}});</script>");
    //        //int index = Convert.ToInt32(e.CommandArgument);
    //        //GridViewRow selectedRow = GV_xs.Rows[index];
    //        //TableCell yhid = selectedRow.Cells[3];
    //        //string guid = Sqlhelper.Serach("select guid from yonghqx where yhid='" + yhid.Text + "'").Rows[0][0].ToString();

    //        //try
    //        //{
    //        //    Sqlhelper.ExcuteNonQuery("delete from banzr where guid=@guid", new SqlParameter("guid", guid));
    //        //    //GridView1.DataSourceID = "uumSearch";
    //        //    GV_xs.DataBind();
    //        //    basic.MsgBox(this.Page, "删除成功", "");
    //        //    //Response.Write("<script>alert('添加成功！');</script>");
    //        //}
    //        //catch (Exception)
    //        //{

    //        //    basic.MsgBox(this.Page, "添加失败", "");
    //        //}
    //    } 
    //#endregion
    //    #region 换专业
    //    if (e.CommandName == "change")
    //    {
    //        this.panel_zx.Visible = false;
    //        this.hzyk.Visible = true;
    //        int index = Convert.ToInt32(e.CommandArgument);
    //        GridViewRow selectedRow = GV_xs.Rows[index];
    //        TableCell xsid = selectedRow.Cells[0];
    //        TableCell xm = selectedRow.Cells[1];
    //        TableCell sfz = selectedRow.Cells[2];
    //        TableCell yx = selectedRow.Cells[3];
    //        TableCell zy = selectedRow.Cells[4];
    //        Label_xsid.Text = xsid.Text;
    //        Label_xm.Text = xm.Text;
    //        Label_sfz.Text= sfz.Text;
    //        Label_yx.Text= yx.Text;
    //        Label_zy.Text= zy.Text;
    //        //ClientScriptManager clm = Page.ClientScript;
    //        //clm.RegisterClientScriptBlock(Page.GetType(),"show_dialog", "$('#dialog').dialog({resizable: false,width: 433,modal: true,buttons: {'取消': function () {$(this).dialog('close');}}});",true);
    //        //clm.RegisterStartupScript(Page.GetType(), "show_dialog", "$('#dialog').dialog({resizable: false,width: 433,modal: true,buttons: {'取消': function () {$(this).dialog('close');}}});", true);
    //        //HttpContext.Current.Response.
    //        //Response.Write("<script type='text/javascript'> $('#dialog').dialog({resizable: false,width: 433,modal: true,buttons: {'取消': function () {$(this).dialog('close');}}});</script>");
    //        //int index = Convert.ToInt32(e.CommandArgument);
    //        //GridViewRow selectedRow = GV_xs.Rows[index];
    //        //TableCell yhid = selectedRow.Cells[3];
    //        //string guid = Sqlhelper.Serach("select guid from yonghqx where yhid='" + yhid.Text + "'").Rows[0][0].ToString();

    //        //try
    //        //{
    //        //    Sqlhelper.ExcuteNonQuery("delete from banzr where guid=@guid", new SqlParameter("guid", guid));
    //        //    //GridView1.DataSourceID = "uumSearch";
    //        //    GV_xs.DataBind();
    //        //    basic.MsgBox(this.Page, "删除成功", "");
    //        //    //Response.Write("<script>alert('添加成功！');</script>");
    //        //}
    //        //catch (Exception)
    //        //{

    //        //    basic.MsgBox(this.Page, "添加失败", "");
    //        //}
    //    } 
    //#endregion
    }
}
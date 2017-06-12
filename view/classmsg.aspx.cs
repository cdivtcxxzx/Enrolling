using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using model;

public partial class view_classmsg : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(DateTime.Now.ToString());
        string pk_sno ="" ;//获取学号
        if (Session["username"] == null)
        {
            this.server_msg.Value = "参数错误";
            return;
        }
        pk_sno = Session["username"].ToString().Trim();

        //查找班级 以及消息 查询
        Base_STU stu = organizationService.getStu(pk_sno);
        if(stu!=null && stu.FK_Class_NO!=null && stu.FK_Class_NO != "")
        {
            DataTable dtMsg = messageService.getDtMsgsByClassNO(stu.FK_Class_NO);
            if (dtMsg != null && dtMsg.Rows.Count > 0)
            {
                Session["rowsCount"] = dtMsg.Rows.Count;
                GridView1.DataSource = dtMsg; 
                GridView1.DataBind();
            }
            else
            {
                Session["rowsCount"] = "0";
            }
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

    #region 分页事件总页数


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
    # region 显示总记录数
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        DataTable dt = (DataTable)e.ReturnValue;
        if (dt == null)
        {
            Session["rowsCount"] = "0";
        }
        else
        {
            Session["rowsCount"] = dt.Rows.Count.ToString();
        }
    }
    #endregion

    public string show_disable(string pk_no)
    {
        string pk_sno = "";
        string result = "";
        if (Session["username"] == null)
        {
            return result;
        }
        pk_sno = Session["username"].ToString();
        if (messageService.isStuReadMsg(pk_no, pk_sno))
            result = "<span style='color:blue'>已阅读</span>";
        else
            result = "<span style='color:red'>未阅读</span>";
        return result;
    }
}
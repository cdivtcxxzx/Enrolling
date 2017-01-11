using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_uumyh : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        if (!IsPostBack)
        {
           // ViewState["sortOrder"] = "ASC";
            //ViewState["sortBy"] = "yhid";
           //GridView1.DataSource = new uum().getAll();

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
               // Bind();
            }

        } 
    }
    #endregion
    #region 分页事件

    
//    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
//{
//        ViewState["sortBy"] = e.SortExpression;
//            if (e.SortDirection.ToString() != ViewState["sortOrder"].ToString())
//            {
//                switch(ViewState["sortOrder"].ToString())
//                {
//                    case "DESC":
//                    ViewState["sortOrder"] = "ASC";
//                    break;
//                    case "ASC":
//                    ViewState["sortOrder"] = "DESC";
//                    break;
//                }
//            }
//          //  Bind();
//}


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //DataTable dt = ViewState["GridView1_DataSource"] as DataTable;
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
       // Bind();
        
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
    
    #region 写入本地数据库
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable tempDt = new uum().getAll();
        DataTable dt = new uum().yonghqx(tempDt,true);
        dt.TableName = "yonghqx";
        ds.Tables.Add(dt);

        try
        {
            Sqlhelper.UpdateDataSet("select guid,yhid,xm,uumzw,lxdh from yonghqx", ds, "yonghqx");
            basic.MsgBox(this.Page, "导入成功", "-1");
        }
        catch (Exception)
        {

            basic.MsgBox(this.Page, "导入失败", "-1");
        }
        
    }
    #endregion
    
    /// <summary>
    /// 点击页面中的按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DaoRu")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView1.Rows[index];
            TableCell name = selectedRow.Cells[0];
            TableCell yhid = selectedRow.Cells[2];
            DataTable tempDt = new uum().getByName(name.Text,yhid.Text);
           
            DataTable dt = new uum().yonghqx(tempDt,true);//转换成需要写入数据库的表
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                Sqlhelper.UpdateDataSet("select guid,yhid,xm,uumzw,lxdh from yonghqx where yhid='" + yhid.Text + "'", ds, "yonghqx");
                basic.MsgBox(this.Page,"导入成功","-1");
                //Response.Write("<script>alert('导入成功！');</script>");
            }
            catch (Exception)
            {

                basic.MsgBox(this.Page, "导入失败", "-1");
            }
        }


    }
    protected void Search_Onclick(object sender, EventArgs e)
    {
        GridView1.DataSourceID = "uumSearch";
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class admin_listmodel : System.Web.UI.Page
{
    #region 静态常用方法
   

    #endregion

    #region 页面初始化参数

    private string pagename1 = "";
    private string pagename2 = "";
    private string pagename3 = "";
    private string pageqx1 = "浏览";
    private string pageqx2 = "查询";
    private string pageqx3 = "删除";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "";//页面值
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
       //获取页面参数
        //basic.MsgBox(this.Page, "提示测试", "yhupdate.aspx");
        if (!String.IsNullOrEmpty(Request.QueryString["webpage"]))
        {
        webpage = Request["webpage"].ToString().Trim();
        }
        else
        {
             webpage="main";
        }
        if (!IsPostBack)
        {
       
            ViewState["sortOrder"] = "ASC";
            ViewState["sortBy"] = "yhid";
            DataTable dt1 = new Account().getAllUser();
            GridView1.DataSource = dt1;
              GridView1.DataKeyNames = new string[] { "yhid"};

            ViewState["GridView1_DataSource"] = GridView1.DataSource;
            //设置gridview

            //添加包复选框的模板列
            TemplateField tf = new TemplateField();
            tf.ItemTemplate = new MyTemplate("", DataControlRowType.DataRow);
            //tf.HeaderText = "选择";
            tf.HeaderTemplate = new MyTemplate("", DataControlRowType.Header);
            tf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            GridView1.Columns.Add(tf);


            for (int i = 0; i < dt1.Columns.Count; i++)
            {
                BoundField bc = new BoundField();
                bc.SortExpression = dt1.Columns[i].ColumnName.ToString();
                
                bc.DataField = dt1.Columns[i].ColumnName.ToString();

                string headerText = dt1.Columns[i].Caption.ToString();
                if (headerText == "yhid")
                {
                    headerText="用户名";
                }
                       if (headerText == "xm")
                {
                    headerText = "真实姓名";
                }

                //替换表头文字
                //DataSet hDs = common.RunQuery("select top 1 * from tb_listfield where cname='" + tbName + "' and cfield='" + headerText + "'");
                
                bc.HeaderText = headerText;

                bc.ItemStyle.HorizontalAlign = HorizontalAlign.Left;//居中对齐
                bc.ItemStyle.Height=25;
                bc.ItemStyle.VerticalAlign=  VerticalAlign.Middle;//上下对齐
                
                GridView1.Columns.Add(bc);
            }

            //添加编辑列
            HyperLinkField cf = new HyperLinkField();
            //CommandField cf = new CommandField();//命令字段
            //cf.ButtonType = ButtonType.Link;//超链接样式的按钮
            //cf.ShowEditButton = true;//显示编辑按钮
           // cf.CausesValidation = false;//引发数据验证为false
            //string gjz="yhid";
            //string[] gjz1 = "yhid";
            cf.DataNavigateUrlFields = GridView1.DataKeyNames;
           
            cf.DataNavigateUrlFormatString = "editmodel.aspx?webpage=" + webpage + "&yhid={0}";
                
            //cf.NavigateUrl = "editmodel.aspx?" + gjz + "=" + GridView1.DataKeyNames. +"";
            cf.Text = "编辑";
           
            cf.HeaderText = "编辑";
            cf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            GridView1.Columns.Add(cf);//添加编辑按钮到gridview
            //<asp:HyperLinkField DataNavigateUrlFormatString="yhupdate.aspx?yhid={0}"   HeaderStyle-Width="25" DataNavigateUrlFields="yhid" Text="修改" />


            //添加删除列
            CommandField cf2 = new CommandField();
            cf2.ButtonType = ButtonType.Link;
            cf2.ShowDeleteButton = true;//显示删除按钮
            cf2.CausesValidation = false;
            cf2.HeaderText = "删除";
            cf2.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            GridView1.Columns.Add(cf2);

           




            //gvshow.DataBind();

            Bind();

            //GridView1.DataBind();
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
                //DataTable dt = ViewState["GridView1_DataSource"] as DataTable;
                //GridView1.DataSource = dt;
                //GridView1.DataBind();
                Bind();
            }

        }
    }
    #endregion
    #region 分页事件


    //private string ConvertSortDirectionToSql(SortDirection sortDirection)
    //{
    //    int x = _sortCount % 2;
    //    string newSortdirection = string.Empty;
    //    switch (x)
    //    {
    //        case 0:
    //            newSortdirection = "ASC";
    //            break;
    //        case 1:
    //            newSortdirection = "DESC";
    //            break;
    //    }
    //    _sortCount++;
    //    return newSortdirection;
    //}

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        //DataTable dt = ViewState["GridView1_DataSource"] as DataTable;
        //DataView dv = dt.DefaultView;
        //if (dt != null)
        //{
        ViewState["sortBy"] = e.SortExpression;
        if (e.SortDirection.ToString() != ViewState["sortOrder"].ToString())
        {
            switch (ViewState["sortOrder"].ToString())
            {
                case "DESC":
                    ViewState["sortOrder"] = "ASC";
                    break;
                case "ASC":
                    ViewState["sortOrder"] = "DESC";
                    break;
            }
        }
        Bind();
        //dv.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
        //GridView1.DataSource = dv;
        //GridView1.DataBind();
        // }
    }


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //DataTable dt = ViewState["GridView1_DataSource"] as DataTable;
        //GridView1.DataSource = dt;
        //GridView1.DataBind();
        Bind();
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

    protected void Search_Onclick(object sender, EventArgs e)
    {
        //搜索按钮事件
        TextBox tb = (TextBox)this.form1.FindControl("searchtext");
        DataTable dt = new Account().getByKey(tb.Text);

        GridView1.DataSource = dt;
        //GridView1.DataSourceID = null;
        //GridView1.DataBind();
        ViewState["GridView1_DataSource"] = GridView1.DataSource;
        Bind();
    }

    protected void Bind()
    {
        //绑定数据到gridview
        DataTable dt = ViewState["GridView1_DataSource"] as DataTable;
        DataView dv = new DataView(dt);
        dv.Sort = ViewState["sortBy"].ToString() + " " + ViewState["sortOrder"].ToString();
        GridView1.DataSource = dv;
        GridView1.DataBind();
    }
    protected void plcz_Click(object sender, EventArgs e)
    {
        //批量操作按钮事件
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
            basic.MsgBox(this.Page, "传进来的记录为：" + batchRegroup,"");

        }
        // ClientScript.RegisterStartupScript(this.GetType(), "pass", "alert('审核通过!');", true);
        //GridViewShow_CK();//GridView绑定数据显示方法  
    }
}
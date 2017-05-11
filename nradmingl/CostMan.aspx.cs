using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nradmingl_CostMan : System.Web.UI.Page
{
    #region 功能模块说明及页面基本信息说明
    //所属模块：收费标准管理
    //编写人：黄磊
    //创建日期：2017年5月7日
    //更新日期：2017年5月7日
    //版本记录：v1.0.0
    #endregion
    #region 页面初始化参数
    private string xwdith = "1366";//屏宽
    private string xheight = "768";//屏高
    private string pagelm1 = "收费标准管理";//请与系统栏目管理中栏目关键字设置为一致便于权限管理

    private string pageqx1 = "浏览";//权限名称，根据页面的权限控制命名，与栏目管理中权限一致，最大设置为５个
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "";//当前页面值，在加载时会自动获取
    private string btitle = "";//附属标题
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            #region 获取屏幕可用高度和宽度做相应页面设置
            //高度小于７２８列表记录只显示１０条最合适　，当宽度小于一定程度时隐藏一些列
            try
            {
                HttpCookie cookiesw = Request.Cookies["xwidth"];
                HttpCookie cookiesh = Request.Cookies["xheight"];
                xwdith = cookiesw.Value.ToString();
                xheight = cookiesh.Value.ToString();
                if (Convert.ToInt32(xheight) < 860)
                {
                    GridView1.PageSize = 10;
                }
                else
                {
                    GridView1.PageSize = 15;
                }


            }
            catch { }
            #endregion
            #region 页面基本配置及标题标识
            btitle = pagelm1;
            try
            {
                //读取cookies中的当前网址信息,如果没有使用服务器获取
                if (Request.Cookies["xurl"] != null)
                {
                    HttpCookie cookiesurl = Request.Cookies["xurl"];
                    webpage = cookiesurl.Value.ToString().Replace("%3A", ":").Replace("%3F", "?").Replace("%3D", "=").Replace("%26", "&");
                }
                else
                {
                    webpage = Request.Url.GetLeftPart(UriPartial.Query).ToString().Replace(Request.Url.Port.ToString(), Sqlhelper.serverport);
                }

            }
            catch (Exception e1) { Response.Write(e1.Message); }

            System.Data.DataTable wangzxx = Sqlhelper.Serach("SELECT TOP 100 *  FROM [wangzxx] order by xxid asc");
            if (wangzxx.Rows.Count > 0)
            {
                for (int i = 0; i < wangzxx.Rows.Count; i++)
                {
                    //网站开关
                    if (wangzxx.Rows[i]["xxgjz"].ToString() == "isopen")
                    {
                        if (wangzxx.Rows[i]["xxnr"].ToString() == "0")
                        {
                            btitle = "网站正在维护，请稍后再访问！" + btitle;

                            // Response.End();前端用户启用,后台根据情况启用
                        }

                    }
                    //网站标题及META关键字设置
                    if (wangzxx.Rows[i]["xxgjz"].ToString() == "title") this.Title = wangzxx.Rows[i]["xxnr"].ToString() + btitle;
                    if (wangzxx.Rows[i]["xxgjz"].ToString() == "MetaKeywords") this.MetaKeywords = wangzxx.Rows[i]["xxnr"].ToString() + btitle;
                    if (wangzxx.Rows[i]["xxgjz"].ToString() == "description") this.MetaDescription = wangzxx.Rows[i]["xxnr"].ToString() + btitle;

                }
            }

            #endregion
            #region 当前页浏览权限验证
            new c_login().tongyiyz(webpage, pagelm1, pageqx1, "进入" + pagelm1 + "页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
            //默认如权限１，若单独验证某个权限，如下方式
            //new c_login().powerYanzheng(Session["username"].ToString(), pagelm1, pageqx2, "2");//验证当前栏目关键字中的权限２,通常在按钮中需验证权限时使用

            #endregion
            #region 数据筛选及ＳＱＬ数据源设置
            if (!IsPostBack)
            {
                
            }

            try
            {



                //管理筛选
                //this.SqlDataSource1.FilterExpression = new Power().Getlanm("glqx");//glqx对应院系代码当前查询中的字段名

                if (!IsPostBack)
                {
                    //ViewState["gridsql"] = SqlDataSource1.SelectCommand;//绑定数据源的查询语句
                    //根据屏幕高度设置ＧＲＩＤＶＩＥＷ的ＰＡＧＥ显示条数
                    if (Convert.ToInt32(xheight) <= 728) this.GridView1.PageSize = 10;

                }
                else
                {

                    //SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
                }
            }
            catch
            {
            }
            #endregion
        }
        catch (Exception err)
        {
            #region 记录页面日志方便后期分析
            if (Session["username"] != null)
            {
                new c_log().logAdd(pagelm1, pageqx1, err.Message, "2", Session["username"].ToString());//记录错误日志

            }
            else
            {
                new c_log().logAdd(pagelm1, pageqx1, err.Message, "2", "未知用户");//记录错误日志
            }
            #endregion
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
    //行选择事件回调
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //string id = e.Row.ID.ToString();
        //try
        //{
        //    this.g_ts.Text = "你选择了第" + (Convert.ToInt32(id) + 1).ToString() + "行1，要操作的事，和提示写在这qt！";
        //}
        //catch { 
        //}
        //e.Row.Attributes.Add("onclick", "javascript:__doPostBack('GridView1','Select$" + e.Row.RowIndex + "')");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //gridview行操作举例
        GridView _gridView = (GridView)sender;
        string id, sql;

        id = e.CommandArgument.ToString();



        try
        {

            if (e.CommandName == "删除")
            {
                //string xx = textbox.Text;

                if (Sqlhelper.ExcuteNonQuery("DELETE FROM xw_neirong  where id=" + id + " ") > 0)
                {
                    this.tsxx.Value = "<font color=green> &nbsp;&nbsp;&nbsp;&nbsp;新闻删除成功!</font>";

                }
                else
                {
                    this.tsxx.Value = "<font color=red> &nbsp;&nbsp;&nbsp;&nbsp;新闻删除失败,请重试!!</font>";
                }
            }
        }
        catch (Exception err1) { this.tsxx.Value = "出错了:" + err1.Message; }
        //ViewState["gridsql"] = SqlDataSource1.SelectCommand;
        //SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
        //_gridView.DataBind();
    }


    protected void Search_Onclick(object sender, ImageClickEventArgs e)
    {
        //搜索按钮举例
        //if (this.DropDownList2.SelectedValue.Length > 0)
        //{
        //    //有子栏目
        //    this.LB_top.Text = "新闻管理&gt;&gt;&nbsp;" + this.DropDownList1.SelectedItem.Text + "&gt;&gt;&nbsp;" + this.DropDownList2.SelectedItem.Text;
        //    if (CheckBox1.Checked)
        //    {
        //        this.SqlDataSource1.SelectCommand = "SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc, xw_neirong.isyn,xw_neirong.title, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.id,xw_lanm.glqx FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where  xw_neirong.title like '%" + this.searchtext.Text + "%'";
        //    }
        //    else
        //    {
        //        this.SqlDataSource1.SelectCommand = "SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc, xw_neirong.isyn,xw_neirong.title, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.id,xw_lanm.glqx FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where xw_neirong.lmid='" + this.DropDownList2.SelectedValue + "' and xw_neirong.title like '%" + this.searchtext.Text + "%'";
        //    }
        //    ViewState["gridsql"] = SqlDataSource1.SelectCommand;
        //    SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
        //    GridView1.DataBind();
        //}
        //else
        //{
        //    //无子栏目
        //    this.LB_top.Text = "新闻管理&gt;&gt;&nbsp;" + this.DropDownList1.SelectedItem.Text;
        //    if (CheckBox1.Checked)
        //    {
        //        this.SqlDataSource1.SelectCommand = "SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc,xw_neirong.isyn, xw_neirong.title, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.id,xw_lanm.glqx FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where  xw_neirong.title like '%" + this.searchtext.Text + "%'";

        //    }
        //    else
        //    {
        //        this.SqlDataSource1.SelectCommand = "SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc,xw_neirong.isyn, xw_neirong.title, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.id,xw_lanm.glqx FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where xw_neirong.lmid='" + this.DropDownList1.SelectedValue + "' and xw_neirong.title like '%" + this.searchtext.Text + "%'";
        //    }
        //        ViewState["gridsql"] = SqlDataSource1.SelectCommand;
        //    SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
        //    GridView1.DataBind();
        //}
    }
   
    

    protected void DropDownList2_DataBound(object sender, EventArgs e)
    {
        //通过下拉列表加载判断值隐藏二级下拉举例
        //try
        //{
        //    if (DropDownList2.Items.Count > 0)
        //    {
        //        this.DropDownList2.Style.Add("display","");
        //    }
        //    else
        //    {
        //        this.DropDownList2.Style.Add("display", "none");
        //    }
        //}
        //catch (Exception ex) {  }
    }
    protected void exportexcel(object sender, EventArgs e)
    {
        //准备导出的DATATABLE,为了输出时列名为中文,请在写SQL语句时重定义一下列名
        //例:SELECT [int] 序号  FROM [taskmanager] order by [int] desc 
        System.Data.DataTable dt = null;
        #region 导出
        //引用EXCEL导出类
        toexcel xzfile = new toexcel();
        string filen = xzfile.DatatableToExcel(dt, "寝室预分配数据");
        //Response.Write("文件名" + filen);
        if (filen.Length > 4)
        {
            this.tsbox.Value = "<span style=\"font-size:Large;\"> <font color=green>导出成功,请<a href=" + filen + " target=_blank >点此下载</a></font></span>";
            //this.Label1.Text = "<font color=green>生成导入模板成功,请<a href=" + filen + " target=_blank >点此下载模板</a></font>";

        }
        else
        {
            this.tsbox.Value = "<span style=\"font-size:Large;\"><font color=red>导出<b>失败</b>,请重试!</font></span>";

        }
        #endregion
    }
    protected void ClearData(object sender, EventArgs e)
    {
        string pk_fee = DDL_cost.SelectedValue;
        int result= Sqlhelper.ConExcuteNonQuery(Sqlhelper.conStr_cost, "delete from Fee_Item where FK_Fee=@pk_fee", new SqlParameter("pk_fee", pk_fee));
        if (result > 0)
        {
            this.tsbox.Value = "<span style=\"font-size:Large;\"> <font color=green>已删除"+result+"条数据</font></span>";
            //this.Label1.Text = "<font color=green>生成导入模板成功,请<a href=" + filen + " target=_blank >点此下载模板</a></font>";

        }
        else
        {
            this.tsbox.Value = "<span style=\"font-size:Large;\"><font color=red>删除<b>失败</b>,请重试!</font></span>";

        }
    }

    
}
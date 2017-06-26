using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using model;

public partial class nradmingl_xsxx_fb : System.Web.UI.Page
{


    #region 页面初始化参数
    private string xwdith = "1366";//屏宽
    private string xheight = "768";//屏高
    private string pagelm1 = "学生分班管理";//请与系统栏目管理中栏目关键字设置为一致便于权限管理

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
            //new c_login().tongyiyz(webpage, pagelm1, pageqx1, "进入" + pagelm1 + "页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
            //默认如权限１，若单独验证某个权限，如下方式
            //new c_login().powerYanzheng(Session["username"].ToString(), pagelm1, pageqx2, "2");//验证当前栏目关键字中的权限２,通常在按钮中需验证权限时使用
            //记录该批次是否受导入时间限制 true为可导入 false不可导入
            hid_daoru_flag.Value = organizationService.isInEnableBatch(DateTime.Now) ? "true" : "false";
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
    }//end Page_Load

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
            Session["fb_rowsCount"] = "0";
        }
        else
        {
            Session["fb_rowsCount"] = dt.Rows.Count.ToString();
        }
    }
    #endregion
    //学院列处理
    public string show_xy(string colleage)
    {
        if (colleage != "-1" && colleage != "")
        {
            Base_College c = organizationService.getColleageByCode(colleage);
            if (c != null)
            {
                return c.Name;
            }
            else
            {
                return "";
            }
        }
        return "";
    }

    //导出数据
    protected void exportexcel(object sender, EventArgs e)
    {
        string sel_batch = batch.SelectedValue.ToString().Trim();
        string sel_colleage = xueyuan.SelectedValue.ToString().Trim();
        DataTable dt = organizationService.getStuByBatchCol(sel_batch, sel_colleage);
        if (dt == null || dt.Rows.Count <= 0) { return; }
        dt.Columns.Remove("Fresh_bath");
        dt.Columns.Remove("Colleage");
        dt.Columns.Remove("SPE_PK");
        dt.Columns["PK_SNO"].ColumnName = "学号";
        dt.Columns["Name"].ColumnName = "姓名";
        dt.Columns["Gender"].ColumnName = "性别";
        dt.Columns["ID_NO"].ColumnName = "身份证";
        dt.Columns["SPE_Name"].ColumnName = "专业";
        dt.Columns["Xz"].ColumnName = "学制";
        dt.Columns["Year"].ColumnName = "年级";
        dt.Columns["Class_Name"].ColumnName = "班级名称";
        dt.Columns["Note"].ColumnName = "备注";
        #region 导出
        //引用EXCEL导出类
        toexcel xzfile = new toexcel();
        string filen = xzfile.DatatableToExcel(dt, "学生分班信息");


        if (filen.Length > 4)
        {
            this.tsxx.Value = "<span style=\"font-size:Large;\"> <font color=green>导出成功,请<a href=" + filen + " target=_blank >点此下载</a></font></span>";
            //this.g_ts.Text = "<font color=green>生成导入模板成功,请<a href=" + filen + " target=_blank >点此下载模板</a></font>";

        }
        else
        {
            this.tsxx.Value = "<span style=\"font-size:Large;\"><font color=red>导出<b>失败</b>,请重试!</font></span>";

        }
        #endregion
    }

    //批次选择，记录session
    protected void batch_SelectedIndexChanged(object sender, EventArgs e)
    {
        string batch_fb = batch.SelectedValue.ToString();
        if (batch_fb != "" && batch_fb != "-1")
        {
            Session["batch_fb"] = batch_fb;
        }
        else
        {
            Session["batch_fb"] = "-1";
        }
    }
    //学院选择，记录session
    protected void xueyuan_SelectedIndexChanged(object sender, EventArgs e)
    {
        string colleage_fb = xueyuan.SelectedValue.ToString();
        if (colleage_fb != "" && colleage_fb != "-1")
        {
            Session["colleage_fb"] = colleage_fb;
        }
        else
        {
            Session["colleage_fb"] = "-1";
        }
    }
}
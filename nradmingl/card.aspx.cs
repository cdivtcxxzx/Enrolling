using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class nradmingl_card : System.Web.UI.Page
{
    #region 功能模块说明及页面基本信息说明
    //所属模块：开发演示
    //任务名称：layui前端列表\导入导出功能演示及后台编写标准
    //完成功能描述：演示前端后台功能编写规范参考
    //编写人：张明
    //创建日期：2016年11月26日
    //更新日期：2016年11月28日
    //版本记录：第一版,编写后台页面编写规范
    #endregion
    #region 页面初始化参数
    private string xwdith = "1366";//屏宽
    private string xheight = "768";//屏高
    private string pagelm1 = "一卡通读取测试";//请与系统栏目管理中栏目关键字设置为一致便于权限管理

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
          
            #region 数据筛选及ＳＱＬ数据源设置

            try
            {



                //管理筛选
                //this.SqlDataSource1.FilterExpression = new Power().Getlanm("glqx");//glqx对应院系代码当前查询中的字段名

                if (!IsPostBack)
                {
                    ViewState["gridsql"] = SqlDataSource1.SelectCommand;//绑定数据源的查询语句
                    //根据屏幕高度设置ＧＲＩＤＶＩＥＷ的ＰＡＧＥ显示条数
                    if (Convert.ToInt32(xheight) <= 728) this.GridView1.PageSize = 10;
                }
                else
                {

                    SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (textbox2.Text.Trim().Length > 0)
            {
                this.textbox1.Text = Convert.ToString(Convert.ToInt64(textbox2.Text.ToString()), 16);
                //String strA =textbox2.Text.ToString("x8");
                this.textbox2.Text = "";
                string sql = "SELECT TOP 500 row_number() over (order by  user_lname)  AS 序号,[user_serial],[user_no] 编号,[user_lname] 姓名,[user_fname] 登陆名,[user_depname] 部门,[user_card] 绑定卡号,[user_id] 身份证  FROM [dt_user]  where user_card='" + textbox1.Text.Trim() + "' order by user_lname ";
                DataTable cx = Sqlhelper.yktSerach(sql);
                //Response.Write(sql);
                if (cx.Rows.Count > 0)
                {
                    this.Label1.Text = "<font color=green>采集到卡号:" + this.textbox1.Text + "该用户已经采集了信息[" + cx.Rows[0]["姓名"].ToString() + "]</font>";

                    ViewState["gridsql"] = sql;
                    SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
                }
                else
                {
                    string sql2 = "SELECT TOP 10 [cardid]      ,[CreateTime],[name],[user_type],[user_id],[user_role],[khsj]FROM [v_account]  where cardid='" + this.textbox1.Text + "' order by CreateTime desc";
                    DataTable cx2 = Sqlhelper.ykt218Serach(sql2);
                    if (cx2.Rows.Count > 0)
                    {
                        this.Label1.Text = "<font color=red>读取到卡号和用户信息:" + this.textbox1.Text + "[" + cx2.Rows[0]["name"].ToString() + "],但该用户还没有绑定该卡号!</font>";
                        sql = "SELECT TOP 500 row_number() over (order by  user_lname)  AS 序号,[user_serial],[user_no] 编号,[user_lname] 姓名,[user_fname] 登陆名,[user_depname] 部门,[user_card] 绑定卡号,[user_id] 身份证  FROM [dt_user]  where user_lname='" + cx2.Rows[0]["name"].ToString() + "' order by user_lname ";

                        ViewState["gridsql"] = sql;
                        SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();

                    }
                    else
                    {
                        this.Label1.Text = "<font color=red>读取到卡号:" + this.textbox1.Text + ",但系统中未找到该卡的信息!</font>";
                    }
                }
            }
            else
            {
                this.Label1.Text = "<font color=red>信息为空,请重新刷卡!</font>";

            }

        }
        catch (Exception ex1) { this.Label1.Text = "<font color=red>"+ex1.Message+"</font>"; }
        this.textbox2.Focus();
        //SELECT TOP 1000 [user_serial],[user_no],[user_lname],[user_fname],[user_depname],[user_card],[user_id]  FROM [scm_main].[dbo].[dt_user]
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
        ViewState["gridsql"] = SqlDataSource1.SelectCommand;
        SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
        _gridView.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //批量操作举例
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
            this.tsxx.Value = "<font color=yellow> &nbsp;&nbsp;&nbsp;&nbsp;共删除" + cg + "条新闻!</font><font color=red>" + sbjl + "</font>";
        }
        catch (Exception err2)
        {
            this.tsxx.Value = "<font color=red> 批量删除出错！" + err2.Message + "</font>";
        }


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
    protected string imagestu(string images)
    {
        DataTable ph = Sqlhelper.yktSerach("SELECT [photo_name],[photo_path] FROM [dt_photo] where user_serial='" + images + "'");
        if (ph.Rows.Count > 0)
        {
            return "<img src=\"" + ph.Rows[0][1].ToString().Replace("../","") + "/" + ph.Rows[0][0].ToString() + "\" style=\"width:20px;height:25px\" >";
        }

        return "无照片";
    }

    protected string xwzt(string isyn)
    {
        //自定义页面状态举例
        if (isyn == "0")
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
        System.Data.DataTable dt = Sqlhelper.Serach("SELECT *  FROM [taskmanager] order by [int] desc ");
        #region 导出
        //引用EXCEL导出类
        toexcel xzfile = new toexcel();
        string filen = xzfile.DatatableToExcel(dt, "项目任务详情");
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
    protected void Button3_Click1(object sender, EventArgs e)
    {
        string sql = "SELECT TOP 500 row_number() over (order by  user_lname)  AS 序号,[user_serial],[user_no] 编号,[user_lname] 姓名,[user_fname] 登陆名,[user_depname] 部门,[user_card] 绑定卡号,[user_id] 身份证  FROM [dt_user]  where  user_lname like '%" + textbox3.Text.Trim() + "%'  order by user_lname ";
        DataTable cx = Sqlhelper.yktSerach(sql);
        //Response.Write(sql);
        if (cx.Rows.Count > 0)
        {
            this.Label1.Text = "<font color=green>找到用户信息</font>";
            ViewState["gridsql"] = sql;
            SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
        }
        else
        {
            this.Label1.Text = "<font color=red>一卡通系统中未查询到该用户!</font>";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class nradmingl_cx_xsxx : System.Web.UI.Page
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
    private string pagelm1 = "新生信息查询院系";//请与系统栏目管理中栏目关键字设置为一致便于权限管理

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

            try
            {
                if (!IsPostBack)
                {

                    #region 页面基本配置及标题标识
                    btitle = pagelm1;
                    try
                    {
                        //读取cookies中的当前网址信息,如果没有使用服务器获取
                        webpage = Request.Url.GetLeftPart(UriPartial.Query).ToString().Replace(Request.Url.Port.ToString(), Sqlhelper.serverport);


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
                }
            }
            catch { }

            #region 数据筛选及ＳＱＬ数据源设置
            try
            {



                //管理筛选

                //this.SqlDataSource1.FilterExpression = new Power().Getlanm("glqx");//glqx对应院系代码当前查询中的字段名

                //string yxdm = "";
                //if (this.DropDownList1.SelectedValue.Length > 0)
                //{
                //    yxdm = this.DropDownList1.SelectedValue;

                //}
                //else
                //{
                //    DataTable yh = Sqlhelper.Serach("SELECT     TOP (1) Fresh_Class.PK_Class_NO, Fresh_Class.Name FROM         Fresh_Class right OUTER JOIN                      Fresh_Counseller ON Fresh_Class.PK_Class_NO = Fresh_Counseller.FK_Class_NO WHERE     (Fresh_Counseller.FK_Staff_NO = '" + Session["username"].ToString() + "')");
                //    if (yh.Rows.Count > 0) yxdm = yh.Rows[0]["PK_Class_NO"].ToString();
                //}

                //string sql2 = "";
                //if (TextBox1.Text.Length > 0)
                //{
                //    sql2 = "and ( xm like '%" + TextBox1.Text + "%' or sfzjh like '%" + TextBox1.Text + "%' or gkbmh like '%" + TextBox1.Text + "%') ";
                //}
                //sql2 = "SELECT TOP 200 row_number() over (order by  xm )  AS 序号,[gkbmh] 高考报名号,[xm] 姓名,[gender_code] 性别,[sfzjh] 身份证号,[zc_zt] 网上注册,[ol_zt] 网上缴费,[bd_zt] 报到状态  FROM [TJ].[dbo].[Fresh_STU] where bjdm='" + yxdm + "' " + sql2 + " order by xm";

                ////Response.Write(sql2+"为空");
                //ViewState["gridsql"] = sql2;
                //SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
                //Response.Write(SqlDataSource1.SelectCommand);
                //GridView1.DataBind();





            }
            catch (Exception e1)
            {
                Response.Write(e1.Message);
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
    protected string getxsname(string yhid)
    {
        string[] xm = yhid.Split(',');
        string sql = "";
        string yhxm = "";
        for (int i = 0; i < xm.Length; i++)
        {

            sql += " or id='" + xm[i] + "' ";

        }
        DataTable yh = Sqlhelper.Serach("SELECT [xm]  FROM [dsbm_xsxx] where 1=2 " + sql);
        if (yh.Rows.Count > 0)
        {
            for (int i = 0; i < yh.Rows.Count; i++)
            {
                if (i == 0)
                {
                    yhxm = yh.Rows[i]["xm"].ToString();
                }
                else
                {
                    yhxm += "," + yh.Rows[i]["xm"].ToString();
                }
            }
        }
        return yhxm;
    }
    protected string getjsname(string yhid)
    {
        string[] xm = yhid.Split(',');
        string sql = "";
        string yhxm = "";
        for (int i = 0; i < xm.Length; i++)
        {

            sql += " or id='" + xm[i] + "' ";

        }
        DataTable yh = Sqlhelper.Serach("SELECT [xm]  FROM [dsbm_zdjs] where 1=2 " + sql);
        if (yh.Rows.Count > 0)
        {
            for (int i = 0; i < yh.Rows.Count; i++)
            {
                if (i == 0)
                {
                    yhxm = yh.Rows[i]["xm"].ToString();
                }
                else
                {
                    yhxm += "," + yh.Rows[i]["xm"].ToString();
                }
            }
        }
        return yhxm;
    }
    #region 数据筛选
    protected string datatablesaixuan(string gjz)
    {
        #region 管理数据筛选返回or数据串
        string qx = "";
        string sx = "";

        Power qxhq = new Power();
        qx = qxhq.Getonebmdm("Fresh_SPE.FK_College_Code");
        try
        {
            qx = qx.Substring(0, qx.Length - 1);
        }
        catch { }
        // Response.Write("操作的数据： "+qx);


        if (qx.Split(',').Length > 0)
        {

            for (int i = 0; i < qx.Split(',').Length; i++)
            {

                string sqlcx = "SELECT TOP 1 [YXMC] FROM  [DM_YUANXI] where yxdm='" + qx.Split(',')[i].ToString() + "'";
                DataTable qxd = Sqlhelper.Serach(sqlcx);
                //this.ztts.Text +=sqlcx;
                if (qxd.Rows.Count > 0)
                {
                    //查询有多少条预分配数据
                    // SELECT     count(Fresh_Bed_Class_Log.PK_Bed_Class_Log) FROM         Fresh_SPE RIGHT OUTER JOIN                      Fresh_Class ON Fresh_SPE.PK_SPE = Fresh_Class.FK_SPE_NO RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO
                    sx += " or " + gjz + "='" + qx.Split(',')[i].ToString() + "'";


                }


            }

        }

        #endregion
        return sx;
    }
    protected void datasaixuan(string gjz, SqlDataSource SqlDataSourcez1)
    {
        #region 管理数据筛选 datasaixuan("区市县代码",this.SqlDataSource1)
        string qx = "";
        string sx = "";
        #region 获取该操作员能操作的系数据
        Power qxhq = new Power();
        qx = qxhq.Getonebmdm("Fresh_SPE.FK_College_Code");
        try
        {
            qx = qx.Substring(0, qx.Length - 1);
        }
        catch { }
        // Response.Write("操作的数据： "+qx);

        #endregion
        if (qx.Split(',').Length > 0)
        {

            for (int i = 0; i < qx.Split(',').Length; i++)
            {

                string sqlcx = "SELECT TOP 1 [YXMC] FROM  [DM_YUANXI] where yxdm='" + qx.Split(',')[i].ToString() + "'";
                DataTable qxd = Sqlhelper.Serach(sqlcx);
                //this.ztts.Text +=sqlcx;
                if (qxd.Rows.Count > 0)
                {
                    //查询有多少条预分配数据
                    // SELECT     count(Fresh_Bed_Class_Log.PK_Bed_Class_Log) FROM         Fresh_SPE RIGHT OUTER JOIN                      Fresh_Class ON Fresh_SPE.PK_SPE = Fresh_Class.FK_SPE_NO RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO
                    sx += " or " + gjz + "='" + qx.Split(',')[i].ToString() + "'";


                }

        #endregion
            }
            if (sx.Length > 0)
            {
                sx = "  " + gjz + "='0' " + sx + "";
            }
        }
        ///Response.Write(this.SqlDataSource1.SelectCommand);
        //Response.Write("操作的数据： "+qx+"@sx:"+sx);

        //SqlDataSourcez1.FilterExpression = sx;//管理数据筛选
        //ViewState["count"] = ((DataView)SqlDataSourcez1.Select(DataSourceSelectArguments.Empty)).Count;

    }

    #endregion



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
                Response.Write("页数：" + _PageSize.ToString());
                GridView1.PageSize = _PageSize;
                //this.SqlDataSource1.SelectCommand = ViewState["SqlDataSource1.SelectCommand"].ToString();
                // GridView1.DataBind();
                //GV_DataBind();
            }

        }
    }
    #endregion
    #region 分页事件总页数

    protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ViewState["count"] = e.AffectedRows;
        //ViewState["count"] = ((DataView)this.SqlDataSource1.Select(DataSourceSelectArguments.Empty)).Count;
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
        //if (this.GridView1.Rows.Count != 0)
        //{
        //    Control table = this.GridView1.Controls[0];
        //    int count = table.Controls.Count;
        //    table.Controls[count - 1].Visible = true;
        //}
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
            string shbz = "";
            DataTable cxbz = Sqlhelper.Serach("select [shbz] from ds_bm where id=" + id);
            if (cxbz.Rows.Count > 0)
            {
                shbz = cxbz.Rows[0][0].ToString();
            }

            if (e.CommandName == "确认报到")
            {
                //string xx = textbox.Text;insert into Fresh_Affair_Log (PK_Affair_Log,FK_SNO,FK_Affair_NO,Log_Status,Creater,Create_DT,Updater,Update_DT) values (
                // Response.Write(id);
                //Response.End();
                if (Sqlhelper.ExcuteNonQuery("UPDATE [Base_STU]   SET [Status_Code] ='已报到' WHERE [PK_SNO] ='" + id + "'") > 0)
                {
                    string guid = Guid.NewGuid().ToString();//自动生成主键和公寓编号
                    if (Sqlhelper.ExcuteNonQuery("insert into Fresh_Affair_Log (PK_Affair_Log,FK_SNO,FK_Affair_NO,Log_Status,Creater,Create_DT,Updater,Update_DT) values ('" + guid + "','" + id + "','1','已完成','" + Session["username"].ToString() + ":" + Session["username"].ToString() + "','" + DateTime.Now.ToString() + "','" + Session["username"].ToString() + ":" + Session["username"].ToString() + "','" + DateTime.Now.ToString() + "')") > 0)
                    {
                        // this.tsxx.Value = "<font color=yellow> &nbsp;&nbsp;&nbsp;&nbsp;确认成功!</font>";
                        this.g_ts.Text = "<font color=blue>学号：" + e.CommandArgument.ToString() + ",报到成功</font>";
                    }
                    else
                    {
                        //  this.tsxx.Value = "<font color=yellow> &nbsp;&nbsp;&nbsp;&nbsp;确认成功!但写日志未成功，请重点一下！</font>";
                        this.g_ts.Text = "<font color=blue>学号：" + e.CommandArgument.ToString() + ",报到成功！但写日志未成功，请重点一下！</font>";
                    }
                }
                else
                {
                    // this.tsxx.Value = "<font color=red> &nbsp;&nbsp;&nbsp;&nbsp报到失败,请重试!!</font>";
                    this.g_ts.Text = "<font color=red>学号：" + id + ",报到失败,请重试!!</font>";
                }
            }
            if (e.CommandName == "取消")
            {
                //string xx = textbox.Text;
                // Response.Write(id);
                //Response.End();
                if (Sqlhelper.ExcuteNonQuery("UPDATE [Base_STU]   SET [Status_Code] ='未报到' WHERE [PK_SNO] ='" + id + "'") > 0)
                {


                    if (Sqlhelper.ExcuteNonQuery("delete FROM [yxxt_data].[dbo].[Fresh_Affair_Log] where  FK_SNO='" + id + "' and  FK_Affair_NO='1'") > 0)
                    {
                        //  this.tsxx.Value = "<font color=yellow> &nbsp;&nbsp;&nbsp;&nbsp;取消成功!</font>";
                        this.g_ts.Text = "<font color=blue>学号：" + e.CommandArgument.ToString() + ",取消成功</font>";
                    }
                    else
                    {
                        // this.tsxx.Value = "<font color=yellow> &nbsp;&nbsp;&nbsp;&nbsp;取消成功!但删除日志未成功，请重点一下！</font>";
                        this.g_ts.Text = "<font color=blue>学号：" + e.CommandArgument.ToString() + ",取消成功!但删除日志未成功，请重点一下！</font>";
                    }




                    // this.tsxx.Value = "<font color=yellow> &nbsp;&nbsp;&nbsp;&nbsp;取消成功!</font>";
                    this.g_ts.Text = "<font color=blue>高考报名号：" + id + ",取消报到成功!</font>";

                }
                else
                {
                    //   this.tsxx.Value = "<font color=red> &nbsp;&nbsp;&nbsp;&nbsp取消失败,请重试!!</font>";
                    this.g_ts.Text = "<font color=red>高考报名号：" + id + ",取消失败,请重试!!</font>";
                }
            }

        }
        catch (Exception err1) { this.tsxx.Value = "出错了:" + err1.Message; }
        //ViewState["gridsql"] = SqlDataSource1.SelectCommand;
        //SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
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
    protected string imagestu2(string images)
    {
        if (images == "01")
        {
            return "男";
        }
        return "女";
    }
    protected string imagestu3(string images)
    {
        if (images == "已办理")
        {
            return "<font color=blue>已办理</font>";
        }
        return images;
    }
    protected string imagestu(string images)
    {
        if (images == "已注册")
        {
            return "<font color=blue>已注册</font>";
        }
        return "<font color=red>未注册</font>";
    }
    protected string imagejs(string images)
    {
        if (images == "已缴费")
        {
            return "<font color=blue>已缴费</font>";
        }
        return "<font color=red>未缴费</font>";
    }
    protected string imagejs4(string images)
    {
        if (images.Length>0)
        {
            return "<font color=blue>" +images.Split('.')[0] + "</font>";
        }
        return "<font color=red>未缴费</font>";
    }
    protected string imagezt(string images)
    {
        if (images == "已报到")
        {
            return "<font color=blue>已报到</font>";
        }
        else
        {
            return "<font color=red>" + images + "</font>";
        }
        return "";
    }
    protected string show1(string images)
    {
        if (images == "未提交")
        {
            return "";
        }
        else
        {
            return "display:none";
        }
        return "";
    }
    protected string show2(string images)
    {
        if (images == "未提交")
        {
            return "display:none";
        }
        else
        {
            return "";
        }
        return "";
    }
    protected string yfpbj(string isyn)
    {
        return dormitory.serch_yfpbj(isyn.Trim()).ToString();
    }
    protected string sycw(string isyn)
    {
        //剩余床位获取

        return dormitory.serch_sycw(isyn.Trim()).ToString();
        //return "";
    }
    protected string fpcw(string isyn)
    {
        //分配床位统计
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
    /// <summary>
    /// 执行查询
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <param name="parameters">查询参数</param>
    /// <returns></returns>
    public static DataTable Serachtj(string sql, params SqlParameter[] parameters)
    {
        //建立连接
        using (SqlConnection conn = new SqlConnection("Data Source=10.35.10.83;Initial Catalog=TJ;User ID=data_tj;Password=tj@cdivtc;"))
        {

            //打开连接
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataSet st = new DataSet();


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(st);
                    return st.Tables[0];
                }
            }
            catch (Exception e)
            {
                return new DataTable();
            }
        }
    }

    /// <summary>
    /// 将网格数据导出到Excel
    /// </summary>
    /// <param name="ctrl">网格名称(如GridView1)</param>
    /// <param name="FileType">要导出的文件类型(Excel:application/ms-excel)</param>
    /// <param name="FileName">要保存的文件名</param>
    public static void GridViewToExcel(Control ctrl, string FileType, string FileName)
    {
        HttpContext.Current.Response.Charset = "GB2312";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;//注意编码
        HttpContext.Current.Response.AppendHeader("Content-Disposition",
            "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
        HttpContext.Current.Response.ContentType = FileType;//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword 
        ctrl.Page.EnableViewState = false;

        string strStyle = "<style>td{mso-number-format:\"\\@\";}</style>";



        System.IO.StringWriter tw = new System.IO.StringWriter();
        tw.WriteLine(strStyle);
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        ctrl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);//解决GRIDVIEW输出EXCEL出错
    }
    protected void exportexcel(object sender, EventArgs e)
    {
        //g_ts.Text = "<font color=red>对不起，助学贷款和收费数据有点问题，暂停导出，正在修复！</font>";
        //return;

        string sql = "SELECT  row_number() over (order by  班级名称 )  AS 序号,院系名称,班级名称,学号,高考报名号,姓名,性别,身份证号,网上注册,网上缴费,[学费],[住宿费],[床上用品],[基本医疗保险],[军训服装费],寝室,床位,报到状态,QQ,联系电话,家庭地址,高考时电话,父亲电话,母亲电话,户籍地址,学生性质 from (select a.yxmc 院系名称,bjmc 班级名称,a.xh 学号,[gkbmh] 高考报名号,[xm] 姓名,case when a.[gender_code]='01' then '男' else '女' end 性别,[sfzjh] 身份证号,case when [zc_zt]='1' then '已注册' else '未注册' end  网上注册,case when [ol_zt]='1' then '已缴费' else '未在网上缴费' end 网上缴费,je,sfxmmc,qsxx.qsh 寝室,qsxx.cwxx 床位,b.[Status_Code] 报到状态,b.[QQ]      ,b.[Phone] 联系电话   ,b.[Home_add] 家庭地址 ,b.[Phone_dr] 高考时电话,b.[Phone_fa] 父亲电话      ,b.[Phone_ma] 母亲电话      ,b.[Huji_add] 户籍地址      ,b.[Note] 学生性质  FROM [TJ].[dbo].[Fresh_STU] a left join enrollment.yxxt_data.dbo.base_stu b on a.xh=b.pk_sno left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='01') sfxm on sfxm.xh=a.xh  left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='02') sfxmzs on sfxmzs.xh=a.xh left join (SELECT     Fresh_Bed_Log.FK_SNO xh, Fresh_Room.Room_NO qsh,Fresh_Bed.Bed_Name cwxx FROM         enrollment.yxxt_data.dbo.Fresh_Bed_Log LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Bed ON Fresh_Bed_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO) qsxx on qsxx.xh=a.xh left join (select sum(je) je,t.xh,t.sfxmmc from (select isnull(SUM(sfje),0)-isnull(SUM(tfje),0) je,xh,sfxmmc from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and len(xh)=15  group by xh,sfxmmc union select SUM(sfje) je,xh,sfxmmc FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is null and sfnd=2017 and zfzt=2 and len(xh)=15 group by xh,sfxmmc) t  where je>0 group by t.xh,t.sfxmmc ) ol_all on a.xh=ol_all.xh where a.yxmc='" + this.yx.SelectedItem.Text + "') tp PIVOT (sum(je) for sfxmmc in ([学费],[住宿费],[床上用品],[基本医疗保险],[军训服装费])) tp1 order by 班级名称";

        string yx1 = "";
        if (yx.SelectedItem.Text != "全部院系")
        {
            sql = "SELECT  row_number() over (order by  班级名称 )  AS 序号,院系名称,班级名称,学号,高考报名号,姓名,性别,身份证号,网上注册,网上缴费,[学费],[住宿费],[床上用品],[基本医疗保险],[军训服装费],寝室,床位,报到状态,QQ,联系电话,家庭地址,高考时电话,父亲电话,母亲电话,户籍地址,学生性质 from (select a.yxmc 院系名称,bjmc 班级名称,a.xh 学号,[gkbmh] 高考报名号,[xm] 姓名,case when a.[gender_code]='01' then '男' else '女' end 性别,[sfzjh] 身份证号,case when [zc_zt]='1' then '已注册' else '未注册' end  网上注册,case when [ol_zt]='1' then '已缴费' else '未在网上缴费' end 网上缴费,je,sfxmmc,qsxx.qsh 寝室,qsxx.cwxx 床位,b.[Status_Code] 报到状态,b.[QQ]      ,b.[Phone] 联系电话   ,b.[Home_add] 家庭地址 ,b.[Phone_dr] 高考时电话,b.[Phone_fa] 父亲电话      ,b.[Phone_ma] 母亲电话      ,b.[Huji_add] 户籍地址      ,b.[Note] 学生性质  FROM [TJ].[dbo].[Fresh_STU] a left join enrollment.yxxt_data.dbo.base_stu b on a.xh=b.pk_sno left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='01') sfxm on sfxm.xh=a.xh  left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='02') sfxmzs on sfxmzs.xh=a.xh left join (SELECT     Fresh_Bed_Log.FK_SNO xh, Fresh_Room.Room_NO qsh,Fresh_Bed.Bed_Name cwxx FROM         enrollment.yxxt_data.dbo.Fresh_Bed_Log LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Bed ON Fresh_Bed_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO) qsxx on qsxx.xh=a.xh left join (select sum(je) je,t.xh,t.sfxmmc from (select isnull(SUM(sfje),0)-isnull(SUM(tfje),0) je,xh,sfxmmc from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and len(xh)=15  group by xh,sfxmmc union select SUM(sfje) je,xh,sfxmmc FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is null and sfnd=2017 and zfzt=2 and len(xh)=15 group by xh,sfxmmc) t  where je>0 group by t.xh,t.sfxmmc ) ol_all on a.xh=ol_all.xh where a.yxmc='" + this.yx.SelectedItem.Text + "') tp PIVOT (sum(je) for sfxmmc in ([学费],[住宿费],[床上用品],[基本医疗保险],[军训服装费])) tp1 order by 班级名称";

        }
        else
        {
            sql = "SELECT  row_number() over (order by  班级名称 )  AS 序号,院系名称,班级名称,学号,高考报名号,姓名,性别,身份证号,网上注册,网上缴费,[学费],[住宿费],[床上用品],[基本医疗保险],[军训服装费],寝室,床位,报到状态,QQ,联系电话,家庭地址,高考时电话,父亲电话,母亲电话,户籍地址,学生性质 from (select a.yxmc 院系名称,bjmc 班级名称,a.xh 学号,[gkbmh] 高考报名号,[xm] 姓名,case when a.[gender_code]='01' then '男' else '女' end 性别,[sfzjh] 身份证号,case when [zc_zt]='1' then '已注册' else '未注册' end  网上注册,case when [ol_zt]='1' then '已缴费' else '未在网上缴费' end 网上缴费,je,sfxmmc,qsxx.qsh 寝室,qsxx.cwxx 床位,b.[Status_Code] 报到状态,b.[QQ]      ,b.[Phone] 联系电话   ,b.[Home_add] 家庭地址 ,b.[Phone_dr] 高考时电话,b.[Phone_fa] 父亲电话      ,b.[Phone_ma] 母亲电话      ,b.[Huji_add] 户籍地址      ,b.[Note] 学生性质  FROM [TJ].[dbo].[Fresh_STU] a left join enrollment.yxxt_data.dbo.base_stu b on a.xh=b.pk_sno left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='01') sfxm on sfxm.xh=a.xh  left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='02') sfxmzs on sfxmzs.xh=a.xh left join (SELECT     Fresh_Bed_Log.FK_SNO xh, Fresh_Room.Room_NO qsh,Fresh_Bed.Bed_Name cwxx FROM         enrollment.yxxt_data.dbo.Fresh_Bed_Log LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Bed ON Fresh_Bed_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO) qsxx on qsxx.xh=a.xh left join (select sum(je) je,t.xh,t.sfxmmc from (select isnull(SUM(sfje),0)-isnull(SUM(tfje),0) je,xh,sfxmmc from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and len(xh)=15  group by xh,sfxmmc union select SUM(sfje) je,xh,sfxmmc FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is null and sfnd=2017 and zfzt=2 and len(xh)=15 group by xh,sfxmmc) t  where je>0 group by t.xh,t.sfxmmc ) ol_all on a.xh=ol_all.xh ) tp PIVOT (sum(je) for sfxmmc in ([学费],[住宿费],[床上用品],[基本医疗保险],[军训服装费])) tp1 order by 班级名称";

            //if(!Session["lsz"].ToString().Contains(""))
            //{
            //g_ts.Text = "<font color=red>对不起，只能单个院系导出！</font>";
            //return;
            //}
        }

        //string sql = "SELECT  row_number() over (order by  班级名称 )  AS 序号,院系名称,班级名称,学号,高考报名号,姓名,性别,身份证号,网上注册,网上缴费,[学费],[住宿费],[床上用品],[基本医疗保险],[军训服装费],寝室,床位,报到状态,QQ,联系电话,家庭地址,高考时电话,父亲电话,母亲电话,户籍地址,学生性质 from (select a.yxmc 院系名称,bjmc 班级名称,a.xh 学号,[gkbmh] 高考报名号,[xm] 姓名,case when a.[gender_code]='01' then '男' else '女' end 性别,[sfzjh] 身份证号,case when [zc_zt]='1' then '已注册' else '未注册' end  网上注册,case when [ol_zt]='1' then '已缴费' else '未在网上缴费' end 网上缴费,je,sfxmmc,qsxx.qsh 寝室,qsxx.cwxx 床位,b.[Status_Code] 报到状态,b.[QQ]      ,b.[Phone] 联系电话   ,b.[Home_add] 家庭地址 ,b.[Phone_dr] 高考时电话,b.[Phone_fa] 父亲电话      ,b.[Phone_ma] 母亲电话      ,b.[Huji_add] 户籍地址      ,b.[Note] 学生性质  FROM [TJ].[dbo].[Fresh_STU] a left join enrollment.yxxt_data.dbo.base_stu b on a.xh=b.pk_sno left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='01') sfxm on sfxm.xh=a.xh  left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='02') sfxmzs on sfxmzs.xh=a.xh left join (SELECT     Fresh_Bed_Log.FK_SNO xh, Fresh_Room.Room_NO qsh,Fresh_Bed.Bed_Name cwxx FROM         enrollment.yxxt_data.dbo.Fresh_Bed_Log LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Bed ON Fresh_Bed_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO) qsxx on qsxx.xh=a.xh left join (select sum(je) je,t.xh,t.sfxmmc from (select isnull(SUM(sfje),0)-isnull(SUM(tfje),0) je,xh,sfxmmc from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and len(xh)=15  group by xh,sfxmmc union select SUM(sfje) je,xh,sfxmmc FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is null and sfnd=2017 and zfzt=2 and len(xh)=15 group by xh,sfxmmc) t  where je>0 group by t.xh,t.sfxmmc ) ol_all on a.xh=ol_all.xh where a.yxmc='" + this.yx.SelectedItem.Text + "') tp PIVOT (sum(je) for sfxmmc in ([学费],[住宿费],[床上用品],[基本医疗保险],[军训服装费])) tp1 order by 班级名称";

        //准备导出的DATATABLE,为了输出时列名为中文,请在写SQL语句时重定义一下列名
        //例:SELECT [int] 序号  FROM [taskmanager] order by [int] desc 
        System.Data.DataTable dt = Serachtj(sql);

        #region 导出
        //引用EXCEL导出类
        toexcel xzfile = new toexcel();
        string filen = xzfile.DatatableToExcel(dt, this.yx.SelectedItem.Text + "学生详细数据");
        //Response.Write("文件名" + filen);
        if (filen.Length > 4)
        {
            this.tsbox.Value = "<span style=\"font-size:Large;\"> <font color=green>导出成功,请</font><a href=" + filen + " target=_blank ><b><font color=red>点此下载</font></b></a></span>";
            //this.Label1.Text = "<font color=green>生成导入模板成功,请<a href=" + filen + " target=_blank >点此下载模板</a></font>";

        }
        else
        {
            this.tsbox.Value = "<span style=\"font-size:Large;\"><font color=red>导出<b>失败</b>,请重试!</font></span>";

        }
        #endregion







        //SqlDataSource3.SelectCommand="SELECT TOP 200 row_number() over (order by  xm )  AS 序号,bjmc 班级名称,xh 学号,[gkbmh] 高考报名号,[xm] 姓名,a.[gender_code] 性别,[sfzjh] 身份证号,[zc_zt] 网上注册,[ol_zt] 网上缴费,[ol_je] 缴费金额,b.[Status_Code] 报到状态,b.[QQ]      ,b.[Phone] 联系电话   ,b.[Home_add] 家庭地址 ,b.[Phone_dr] 高考时电话,b.[Phone_fa] 父亲电话      ,b.[Phone_ma] 母亲电话      ,b.[Huji_add] 户籍地址      ,b.[Note] 学生性质  FROM [TJ].[dbo].[Fresh_STU] a left join enrollment.yxxt_data.dbo.base_stu b on a.xh=b.pk_sno where bjdm='"+this.DropDownList1.SelectedValue+"'    order by xm";





        //DateTime dt = System.DateTime.Now;
        //string str = dt.ToString("yyyyMMddhhmmss");

        //string xxx = "";
        //try
        //{
        //    xxx = DropDownList1.SelectedItem.Text;
        //}
        //catch
        //{
        //}

        //str = xxx+"学生详细数据" + str + ".xls";

        //this.GridView2.AllowPaging = false;
        //GridView2.DataBind();
        //this.GridView2.AllowPaging = false;
        //GridViewToExcel(GridView2, "application/ms-excel", str);
    }
    protected void xq_SelectedIndexChanged(object sender, EventArgs e)
    {
        gzt();
    }
    protected void dorm_SelectedIndexChanged(object sender, EventArgs e)
    {
        gzt();
    }
    protected void floor_SelectedIndexChanged(object sender, EventArgs e)
    {
        gzt();
    }
    protected void bj_SelectedIndexChanged(object sender, EventArgs e)
    {
        gzt();
    }
    protected void gzt()
    {
        string sql2 = "";
        if (yx.SelectedItem.Text != "全部院系")
        {
            sql2 += " and a.yxmc='" + this.yx.SelectedItem.Text + "' ";
        }

        if (DropDownList1.SelectedItem.Text != "全部班级")
        {
            sql2 += " and a.bjdm='" + this.DropDownList1.SelectedValue + "' ";
        }
        if (TextBox1.Text.Length > 0)
        {
            sql2 += "and ( a.xm like '%" + TextBox1.Text + "%' or a.sfzjh like '%" + TextBox1.Text + "%' or a.gkbmh like '%" + TextBox1.Text + "%') ";
        }

        string sql = "SELECT  row_number() over (order by  班级名称 )  AS 序号,院系名称,班级名称,学号,高考报名号,姓名,性别,身份证号,网上注册,网上缴费,[学费],[住宿费],[床上用品],[基本医疗保险],[军训服装费],寝室,床位,报到状态,QQ,联系电话,家庭地址,高考时电话,父亲电话,母亲电话,户籍地址,学生性质 from (select a.yxmc 院系名称,bjmc 班级名称,a.xh 学号,[gkbmh] 高考报名号,[xm] 姓名,case when a.[gender_code]='01' then '男' else '女' end 性别,[sfzjh] 身份证号,case when [zc_zt]='1' then '已注册' else '未注册' end  网上注册,case when [ol_zt]='1' then '已缴费' else '未在网上缴费' end 网上缴费,je,sfxmmc,qsxx.qsh 寝室,qsxx.cwxx 床位,b.[Status_Code] 报到状态,b.[QQ]      ,b.[Phone] 联系电话   ,b.[Home_add] 家庭地址 ,b.[Phone_dr] 高考时电话,b.[Phone_fa] 父亲电话      ,b.[Phone_ma] 母亲电话      ,b.[Huji_add] 户籍地址      ,b.[Note] 学生性质  FROM [TJ].[dbo].[Fresh_STU] a left join enrollment.yxxt_data.dbo.base_stu b on a.xh=b.pk_sno left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='01') sfxm on sfxm.xh=a.xh  left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='02') sfxmzs on sfxmzs.xh=a.xh left join (SELECT     Fresh_Bed_Log.FK_SNO xh, Fresh_Room.Room_NO qsh,Fresh_Bed.Bed_Name cwxx FROM         enrollment.yxxt_data.dbo.Fresh_Bed_Log LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Bed ON Fresh_Bed_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO) qsxx on qsxx.xh=a.xh left join (select sum(je) je,t.xh,t.sfxmmc from (select isnull(SUM(sfje),0)-isnull(SUM(tfje),0) je,xh,sfxmmc from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and len(xh)=15  group by xh,sfxmmc union select SUM(sfje) je,xh,sfxmmc FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is null and sfnd=2017 and zfzt=2 and len(xh)=15 group by xh,sfxmmc) t  where je>0 group by t.xh,t.sfxmmc ) ol_all on a.xh=ol_all.xh where 1=1 " + sql2 + ") tp PIVOT (sum(je) for sfxmmc in ([学费],[住宿费],[床上用品],[基本医疗保险],[军训服装费])) tp1 order by 班级名称 ";

        ViewState["gridsql"] = sql;
        SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
        //Response.Write(SqlDataSource1.SelectCommand);
        GridView1.DataBind();


    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    protected void clearyfp(object sender, EventArgs e)
    {
        //清空预分配数据
    }
    protected void yx_SelectedIndexChanged(object sender, EventArgs e)
    {
        gzt();
        //提示统计信息
        //if (yx.SelectedValue == "全部院系" || yx.SelectedValue == "0")
        //{
        //    g_ts.Text = dormitory.serch_yfptj("0", "all", "");
        //}
        //else
        //{
        //    g_ts.Text = dormitory.serch_yfptj(yx.SelectedValue, "0", yx.SelectedItem.Text);
        //}
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ViewState["gridsql"] = "SELECT TOP 200 [gkbmh] 高考报名号,[xm] 姓名,[gender_code] 性别,[sfzjh] 身份证号,[zc_zt] 网上注册,[ol_zt] 网上缴费,[bd_zt] 报到状态  FROM [TJ].[dbo].[Fresh_STU] where bjdm='" + this.DropDownList1.SelectedValue + "' order by xm";
        // this.g_ts.Text= ViewState["gridsql"].ToString();

        SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
        ViewState["count"] = ((DataView)this.SqlDataSource1.Select(DataSourceSelectArguments.Empty)).Count;
        // datasaixuan("区市县代码", this.SqlDataSource1);//数据筛选
        //Response.Write(SqlDataSource1.SelectCommand);
        GridView1.DataBind();
    }



    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //更新赛项
        gzt();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        //查询
        gzt();
    }
}
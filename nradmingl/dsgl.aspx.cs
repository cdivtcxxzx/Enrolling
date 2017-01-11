using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class admin_dsgl : System.Web.UI.Page
{
    #region 页面初始化参数

    private string pagelm1 = "高职班级管理";

    private string pageqx1 = "浏览";
    private string pageqx2 = "修改";
    private string pageqx3 = "删除";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "banzrgl.aspx";//页面值

    #endregion
    private int pwidth = 1024;
    private int pheight = 768;
    protected string getbzr(string bzr)
    {
        string bzr2 = "";
        DataTable bzr1 = Sqlhelper.Serach("SELECT *  FROM [yonghqx] where guid='" + bzr + "'");
        if (bzr1.Rows.Count > 0)
        {
            bzr2 = bzr1.Rows[0]["xm"].ToString();
        }
        if (bzr.Length > 0)
        {
            return bzr2;
        }
        else
        {
            return "<font color=red>未分配</a>";
        }
    }
    protected string getbzrid(string bzr)
    {
        string bzr2 = "";
        DataTable bzr1 = Sqlhelper.Serach("SELECT *  FROM [yonghqx] where guid='" + bzr + "'");
        if (bzr1.Rows.Count > 0)
        {
            bzr2 = bzr1.Rows[0]["yhid"].ToString();
        }
        if (bzr.Length > 0)
        {
            return bzr2;
        }
        else
        {
            return "<font color=red>未分配</a>";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //判断浏览器
        try
        {
            if (Session["fbl"] != null)
            {
                string[] widhts = Session["fbl"].ToString().Split('*');
                if (widhts.Length > 1)
                {
                    pwidth = Convert.ToInt32(widhts[0]);
                    pheight = Convert.ToInt32(widhts[1]);
                }

                if (pheight >= 900)
                {
                    //this.GridView1.Columns[6].Visible = false;
                    this.GridView1.PageSize = 15;
                }

            }
        }
        catch
        {
        }
        //登陆验证,权限验证,日志
        new c_login().tongyiyz(pagelm1, pageqx1, "进入管理页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);

        if (!IsPostBack)
        {
            string yhid = Session["UserName"].ToString();
            ViewState["SqlDataSource1.SelectCommand"] = SqlDataSource1.SelectCommand;
           // Session["banzrgl_searchString"] = "SELECT    row_number() over (order by BANJXX.id) as uid, BANJXX.id, BANJXX.bjdm, BANJXX.njdm, BANJXX.bjmc, BANJXX.xz, BANJXX.bjbm, BANJXX.yxdm yxdm, BANJXX.zydm, BANJXX.bzrid, BANJXX.jbny, BANJXX.xqdm,                    BANJXX.csid, BANJXX.xjrs, BANJXX.xjzx, BANJXX.xjyz, DM_ZY.zycc, DM_XIAOQU.xqmc FROM         BANJXX LEFT OUTER JOIN                      DM_ZY ON BANJXX.zydm = DM_ZY.zydm  LEFT OUTER JOIN    DM_XIAOQU ON BANJXX.xqdm = DM_XIAOQU.xqdm  where 1=1 and DM_ZY.zycc = '高职' and banjxx.njdm='" + DropDownList5.SelectedValue + "' ";
            //Session["banzrgl_yxdm"] = "%%";
            //Session["banzrgl_orderBy"] = "banjxx.bjdm";
            //Session["textSearch"] = "";
            //string ex = "1=2";
            //string ex_yx = "1=2";
            //foreach (var x in new Power().GetYxdmsByYhid(yhid))
            //{
            //    ex += " or szbmid='" + x.ToString() + "'";
            //    ex_yx += " or yxdm='" + x.ToString() + "'";
            //}
            //string ex = new Power().GetFilterExpression("yxdm");
            //string ex_yx = new Power().GetFilterExpression("yxdm");
            //SqlDataSource1.FilterExpression = ex;
            //ViewState["SqlDataSource1.FilterExpression"] = ex;
            //ViewState["SqlDataSource4.FilterExpression"] = ex_yx;
            ////Response.Write(ex);
        }
        //SqlDataSource4.FilterExpression = ViewState["SqlDataSource4.FilterExpression"].ToString();


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
                GVBind();
            }

        }
    }
    #endregion
    #region 分页事件


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;

        GVBind();
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
    public override void VerifyRenderingInServerForm(Control control) { }
    /// <summary>
    /// 将所有学生数据导出到EXCEL
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dt = Sqlhelper.Serach("SELECT    row_number() over (order by id desc) as 序号, id 赛事编号,name 赛事名称,nd 年度,zbdw 主办单位,starttime 开始时间,overtime 结束时间,bz 备注 from ds_saishi order by id desc ");


        #region 导出
        toexcel xzfile = new toexcel();
        string filen = xzfile.DatatableToExcel(dt, "赛事数据");
        //Response.Write("文件名：" + filen);
        if (filen.Length > 4)
        {
            this.alertMessage.Value = "<span style=\"font-size:Large;\"> <font color=green>导出成功,请<a href=" + filen + " target=_blank >点此下载</a></font></span>";
            // Response.Write(this.alertMessage2.Value);

            //this.Label1.Text = "<font color=green>生成导入模板成功,请<a href=" + filen + " target=_blank >点此下载模板</a></font>";
            //this.alertMessage2.Value = "导出失败";
        }
        else
        {
            this.alertMessage.Value = "<span style=\"font-size:Large;\"><font color=red>导出<b>失败</b>,请重试!</font></span>";

        }
        #endregion


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
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
        for (int i = 0; i < chkIds.Length; i++)
        {
            //将传过来的ID记录状态改为删除
            //sql = "UPDATE T_WPXX_CK SET SPR='" + userrealName + "' WHERE ID='" + chkIds[i] + "'";
            // wpck.auditOrDelete(sql);//传入SQL语句并执行  
            Response.Write("<script>alert('本页不允许删除记录！');</script>");

        }
        // ClientScript.RegisterStartupScript(this.GetType(), "pass", "alert('审核通过!');", true);
        //GridViewShow_CK();//GridView绑定数据显示方法  
    }
    protected void Button3_Click1(object sender, EventArgs e)
    {
        HiddenField hf = (HiddenField)this.Page.FindControl("hdfWPBH");
        string[] temp = hf.Value.ToString().TrimEnd(',').Split(',');
        foreach (string x in temp)
        {
            try
            {
                Sqlhelper.ExcuteNonQuery("delete from banzr where guid='" + x + "'");
            }
            catch (Exception ex)
            {

                new c_log().logAdd(pagelm1, pageqx2, x + "错误代码为:" + ex.Message);
            }
        }
        GVBind();

    }

    /// <summary>
    /// 通过院系代码得到名称
    /// </summary>
    /// <param name="dm">院系代码</param>
    /// <returns>院系名称</returns>
  
    /// <summary>
    /// 通过班主任guid得到管理班级
    /// </summary>
    /// <param name="dm">guid</param>
    /// <returns>管理班级</returns>
    protected string GetBanjByBanzrid(string guid)
    {
        DataTable banj = Sqlhelper.Serach("select banjxx.bjmc from banjxx where bzrguid='" + guid + "'");
        string banjs = string.Empty;
        foreach (DataRow dr in banj.Rows)
        {
            banjs += dr.ItemArray[0].ToString() + ",";
        }
        return banjs;
    }


    /// <summary>
    /// 获得总记录数
    /// </summary>
    /// <returns></returns>
    protected int GetCount()
    {
        DataTable xs = Sqlhelper.Serach("select id from xuesjbsj");
        return xs.Rows.Count;
    }
    /// <summary>
    /// 读取存在viewState里的selectCommand,进行数据绑定
    /// </summary>
    protected void GVBind()
    {

        SqlDataSource1.SelectCommand = ViewState["SqlDataSource1.SelectCommand"].ToString();
        //SqlDataSource1.FilterExpression = ViewState["SqlDataSource1.FilterExpression"].ToString();
        GridView1.DataBind();
    }
    /// <summary>
    /// 读取存在viewState里的selectCommand,进行专业下拉框数据绑定
    /// </summary>
    protected void DDL_ZY_Bind()
    {

       


    }
    /// <summary>
    /// 自定义排序(测试)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void sorting(object sender, EventArgs e)
    {
        switch (GridView1.SortDirection)
        {
            case SortDirection.Ascending:
                GridView1.Sort("xqdm", SortDirection.Descending);
                break;
            case SortDirection.Descending:
                GridView1.Sort("xqdm", SortDirection.Ascending);
                break;
        }
    }

    //protected void XiaoQuChange(object sender, EventArgs e)
    //{
    //    string appendString = DropDownList2.SelectedValue;
    //    string selectCommand = "SELECT [zydm], [zym] FROM [zy] where nd=2013"+" and xibu='财经商贸系'";
    //    SqlDataSource4.SelectCommand = selectCommand;
    //    DropDownList4.DataBind();
    //}

    /// <summary>
    /// 自定义排序
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        //ViewState["sortBy"] = e.SortExpression;
        //if (e.SortDirection.ToString() != ViewState["sortOrder"].ToString())
        //{
        //    switch (ViewState["sortOrder"].ToString())
        //    {
        //        case "DESC":
        //            ViewState["sortOrder"] = "ASC";
        //            break;
        //        case "ASC":
        //            ViewState["sortOrder"] = "DESC";
        //            break;
        //    }
        //}
        if (e.SortDirection.ToString() == "Descending")
            Session["banzrgl_orderBy"] = e.SortExpression + " " + "DESC";
        else Session["banzrgl_orderBy"] = e.SortExpression + " " + "ASC";
        GVBind();
    }
    protected void SqlDataSource1_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ViewState["count"] = e.AffectedRows;
    }

    /// <summary>
    /// 权限验证,通过url判断栏目
    /// </summary>
    /// <param name="operation">待验证权限</param>
    /// <param name="type">1:只查询用户权限表,2:全部查询</param>
    /// <returns></returns>
    public bool powerYanzheng(string operation, string type)
    {

        string x1 = Request.PhysicalPath;

        string url = Path.GetFileName(x1);
        try
        {
            //查询权限id
            string strSqlQxidSerach = "SELECT qxid FROM quanxdm WHERE qxmc=@qxmc";
            string strQxid = "";
            DataTable dtQxid = Sqlhelper.Serach(strSqlQxidSerach, new SqlParameter("qxmc", operation));
            if (dtQxid.Rows.Count > 0)
            {
                strQxid = dtQxid.Rows[0][0].ToString();
            }
            //查询是否验证
            string strSqlLmidSerach = "SELECT sfqxyz,lmmc FROM lanm WHERE url=@url";
            string strSfqxyz = "";
            DataTable dtLmid = Sqlhelper.Serach(strSqlLmidSerach, new SqlParameter("url", url));
            if (dtLmid.Rows.Count > 0)
            {
                foreach (DataRow row in dtLmid.Rows)
                {
                    strSfqxyz = row["sfqxyz"].ToString();
                }
            }
            //如果不需要验证，则直接通过验证
            if (!strSfqxyz.Contains(strQxid))
            {
                return true;
            }
            string lmmc = dtLmid.Rows[0]["lmmc"].ToString();
            switch (type)
            {
                //用户权限表中验证
                case "1":
                    //string strLsz = "";
                    string SqlString = "SELECT * FROM yonghqx WHERE yhid=@yhid";
                    DataTable dtQx = Sqlhelper.Serach(SqlString, new SqlParameter("yhid", Session["UserName"].ToString()));
                    if (dtQx.Rows.Count > 0)
                    {
                        DataRow row = dtQx.Rows[0];
                        string strYhqx = row["yhqx"].ToString();
                        XDocument xml = XDocument.Parse(strYhqx);
                        if (xml.Element("Root").Element(lmmc).Element(operation) != null)
                            return true;
                        else
                        {
                            Response.Write("<script>alert('没有权限');history.go(-1)</script>");
                            return false;
                        }
                    }
                    else
                        Response.Write("<script>alert('没有权限');history.go(-1)</script>");
                    return false;
                case "2":
                    if (Session["Yhqx"] != null)
                    {
                        string xx = Session["Yhqx"].ToString();
                        XDocument xml = XDocument.Parse(Session["Yhqx"].ToString());
                        if (xml.Element("Root").Element(lmmc).Element(operation) != null)
                            return true;
                    }
                    Response.Write("<script>alert('没有权限');history.go(-1)</script>");
                    return false;

                default: Response.Write("<script>alert('没有权限');history.go(-1)</script>"); return false;
            }
        }
        catch
        {

            Response.Write("<script>alert('没有权限');history.go(-1)</script>");
            return false;
        }
    }
    /// <summary>
    /// 点击页面中的按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "修改")
        {
            string bjdm = e.CommandArgument.ToString();
            this.alertMessage2.Value = "dsgladd.aspx?type=edit&id=" + bjdm;

        }
        if (e.CommandName == "删除")
        {
            // if (!new Power().powerYanzheng("删除", "2")) { return; }
            //int index = Convert.ToInt32(e.CommandArgument);

            //登陆验证,权限验证,日志
            //new c_login().tongyiyz(pagelm1, pageqx3, "班主任管理页-删除班主任[" + yhid.Text + "]", false, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
            string bjdm = e.CommandArgument.ToString();

           
            try
            {
                if (Sqlhelper.ExcuteNonQuery("delete from ds_saishi where id='" + bjdm + "'  ") > 0)
                {
                    GridView1.DataBind();
                    this.alertMessage.Value = "<font color=green>删除成功！</font>";
                }
                else
                {
                    this.alertMessage.Value = "<font color=red>删除失败！ </font>";
                }


                //Response.Write("<script>alert('添加成功！');</script>");
            }
            catch (Exception ex)
            {

                this.alertMessage.Value = "<font color=red>删除失败！+" + ex.Message + "</font>";
            }
        }
    }
    protected void Button2_Click3(object sender, EventArgs e)
    {
        this.alertMessage2.Value = "dsgladd.aspx?type=add";
    }
    protected void Button2_Click4(object sender, EventArgs e)
    {
        this.GridView1.DataBind();
    }
    protected void Button2_Click31(object sender, EventArgs e)
    {
        Response.Redirect("dsgl.aspx");
    }
}
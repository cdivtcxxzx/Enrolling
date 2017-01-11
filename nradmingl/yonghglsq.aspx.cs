using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class admin_yonghglsq : System.Web.UI.Page
{

    #region 页面初始化参数

    private string pagelm1 = "教师管理";

    private string pageqx1 = "浏览";
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "yonghglsq.aspx";//页面值

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //登陆验证,权限验证,日志
        new c_login().tongyiyz(pagelm1, pageqx1, "进入教师管理显示页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);

       // Response.Write(new Power().GetFilterExpression("yxdm"));
        //Response.End();
        if (!IsPostBack)
        {
            ViewState["SqlDataSource1.SelectCommand"] = Sql_yh.SelectCommand;
            Sql_yh.SelectCommand = ViewState["SqlDataSource1.SelectCommand"].ToString();
            Sql_yh.FilterExpression = new Power().GetFilterExpression("pjjxbmdm");
        
            //Response.Write(Sql_yh.SelectCommand);
           // Sql_yh.SelectCommand = "";
           // Response.Write(" where " + new Power().GetFilterExpression("yxdm"));
            GridView1.DataBind();
            //Sql_yh.DataBind();

        }
        Sql_yh.SelectCommand = ViewState["SqlDataSource1.SelectCommand"].ToString();
        Sql_yh.FilterExpression = new Power().GetFilterExpression("pjjxbmdm");
        //Response.Write(new Power().GetFilterExpression("pjjxbmdm"));        
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
    #region 批量按钮
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
                //if (Sqlhelper.ExcuteNonQuery("DELETE FROM xw_neirong  where id=" + chkIds[i] + " ") > 0)
                try
                {
                    string yxdm = "";
                    try
                    {
                        yxdm = new Power().Getonebmdm("yxdm");
                    }
                    catch { }

                    DataTable yh = Sqlhelper.Serach("select pjjxbmdm,pjbmdm from yonghqx where yhid='" + chkIds[i] + "'");
                    if (yh.Rows.Count > 0)
                    {
                        string sql = "";


                        string bmdm = yh.Rows[0][0].ToString();
                        string pjbmdm = yh.Rows[0][1].ToString();
                        if (yxdm.Split(',').Length > 0)
                        {
                            for (int xx = 0; xx < yxdm.Split(',').Length; xx++)
                            {
                                if (yxdm.Split(',')[xx].Length > 0)
                                {
                                    bmdm = bmdm.Replace(yxdm.Split(',')[xx] + ",", "");
                                    pjbmdm = pjbmdm.Replace(yxdm.Split(',')[xx], "");


                                    //if (yh.Rows[q][1].ToString() == yxdm.Split(',')[xx]) sql = ",pjbmdm=''";
                                }
                                //this.Label1.Text += yxdm.Split(',')[xx]+"<br>"+bmdm+"部门代码<br>";
                            }
                        }
                        if (Sqlhelper.ExcuteNonQuery("update yonghqx set pjjxbmdm='" + bmdm + "',pjbmdm='" + pjbmdm + "'" + sql + " where yhid='" + chkIds[i] + "'") > 0)
                        {
                            cg = cg + 1;
                        }
                        else
                        {
                            sbjl += "清除"+yh.Rows[0][2].ToString()+"失败！";
                        }
                        //this.Label1.Text += bmdm;
                    }
                    //Response.Write("<script>alert('导入成功！');</script>");
                }
                catch (Exception)
                {

                    basic.MsgBox(this.Page, "移出失败", "-1");
                }

                //Response.Write("<script>alert('" + chkIds[i] + "');</script>");

            }
            this.Label1.Text = "<font color=green> &nbsp;&nbsp;&nbsp;&nbsp;共清除" + cg + "教师!</font><font color=red>" + sbjl + "</font>";
        }
        catch
        {
            this.Label1.Text = "<font color=red> &nbsp;&nbsp;&nbsp;&nbsp;批量清除出错！</font>";
        }


    }

    #endregion
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Search_Onclick(object sender, EventArgs e)
    {
        string search=searchtext.Text;
        //Sql_yh.FilterExpression = new Power().GetFilteryxmc("dm_yuanxi.yxmc");
        ViewState["SqlDataSource1.SelectCommand"] = "SELECT pjjxbmdm,yhid,xm,yhqx,yonghqx.yxdm,yonghqx.pjbmdm,lsz,dltime,fwcs,guid,dm_yuanxi.yxmc from yonghqx inner join dm_yuanxi on yonghqx.yxdm=dm_yuanxi.yxdm where yonghqx.xm like '%" + search + "%' or yonghqx.yhid like '%" + search + "%' and (" + new Power().GetFilterExpression("pjjxbmdm")+" )";
        Sql_yh.SelectCommand = ViewState["SqlDataSource1.SelectCommand"].ToString();
       // Response.Write(ViewState["SqlDataSource1.SelectCommand"].ToString());
        Sql_yh.FilterExpression = new Power().GetFilterExpression("pjjxbmdm");
        GridView1.DataBind();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        this.Label1.Text = "xxxx" + "<br>";
        GridView _gridView = (GridView)sender;
        string id;

        id = e.CommandArgument.ToString();
        
        if (e.CommandName == "清除")
        {
            //string xx = textbox.Text;

            try
            {
                string yxdm = "";
                try
                {
                    yxdm = new Power().Getonebmdm("yxdm");
                }
                catch { }
                
                DataTable yh = Sqlhelper.Serach("select pjjxbmdm,pjbmdm from yonghqx where yhid='" + id + "'");
                if (yh.Rows.Count > 0)
                {
                    string sql = "";


                    string bmdm = yh.Rows[0][0].ToString();
                    if (yxdm.Split(',').Length > 0)
                    {
                        for (int xx = 0; xx < yxdm.Split(',').Length;xx++ )
                        {
                            if (yxdm.Split(',')[xx].Length>0)
                            {
                                bmdm = bmdm.Replace(yxdm.Split(',')[xx] + ",", "");

                                if (yh.Rows[0][1].ToString() == yxdm.Split(',')[xx]) sql = ",pjbmdm=''";
                            }
                            //this.Label1.Text += yxdm.Split(',')[xx]+"<br>"+bmdm+"部门代码<br>";
                        }
                    }
                    
                    if (Sqlhelper.ExcuteNonQuery("update yonghqx set pjjxbmdm='" + bmdm + "'" + sql + " where yhid='" + id + "'") > 0)
                    {
                        Label1.Text = "<font color=green>将" + id + "移出本系成功！</font>";
                    }
                    else
                    {
                        Label1.Text = "<font color=red>将" + id + "移出本系失败</font>";
                    }
                    //this.Label1.Text += bmdm;
                }
                //Response.Write("<script>alert('导入成功！');</script>");
            }
            catch (Exception)
            {

                basic.MsgBox(this.Page, "移出失败", "-1");
            }
          
        }
        
        Sql_yh.SelectCommand = ViewState["SqlDataSource1.SelectCommand"].ToString();
        _gridView.DataBind();
    }
    protected string GetPower1(string lmyyqxs)
    {
        string xtje = "";
        if (lmyyqxs.Length <= 36)
        {
            xtje = lmyyqxs;
        }
        else
        {
            xtje = "<a href='#' txttop='txttop' title='"+lmyyqxs+"'>"+lmyyqxs.Substring(21, 14)+"</a>";
        }
        return xtje;
    }

    protected void delall(object sender, EventArgs e)
    {
        try
        {
            string yxdm = "";
            try
            {
                yxdm = new Power().Getonebmdm("yxdm");
            }
            catch { }

            DataTable yh = Sqlhelper.Serach("select pjjxbmdm,pjbmdm,yhid from yonghqx where len(pjjxbmdm)>1");
            if (yh.Rows.Count > 0)
            {
                string sql = "";
                int cgjl = 0;
                for (int q = 0; q < yh.Rows.Count; q++)
                {
                    string bmdm = yh.Rows[q][0].ToString();
                    string pjbmdm = yh.Rows[q][1].ToString();
                    if (yxdm.Split(',').Length > 0)
                    {
                        for (int xx = 0; xx < yxdm.Split(',').Length; xx++)
                        {
                            if (yxdm.Split(',')[xx].Length > 0)
                            {
                                bmdm = bmdm.Replace(yxdm.Split(',')[xx] + ",", "");
                                pjbmdm=pjbmdm.Replace(yxdm.Split(',')[xx], "");
                                

                                //if (yh.Rows[q][1].ToString() == yxdm.Split(',')[xx]) sql = ",pjbmdm=''";
                            }
                            //this.Label1.Text += yxdm.Split(',')[xx]+"<br>"+bmdm+"部门代码<br>";
                        }
                    }

                    if (Sqlhelper.ExcuteNonQuery("update yonghqx set pjjxbmdm='" + bmdm + "',pjbmdm='"+pjbmdm +"'" + sql + " where yhid='" + yh.Rows[q][2].ToString() + "'") > 0)
                    {
                        cgjl = cgjl + 1;
                       }
                }
                Label1.Text = "<font color=green>共从本系移出"+cgjl.ToString()+"位教师！</font>";
                    
                //this.Label1.Text += bmdm;
            }
            //Response.Write("<script>alert('导入成功！');</script>");
        }
        catch (Exception)
        {

            basic.MsgBox(this.Page, "移出失败", "-1");
        }

    }

    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        //this.Label1.Text = "xxxx" + "<br>";
        GridView _gridView = (GridView)sender;
        string id;

        id = e.CommandArgument.ToString();

        if (e.CommandName == "清除")
        {
            //string xx = textbox.Text;

            try
            {
                string yxdm = "";
                try
                {
                    yxdm = new Power().Getonebmdm("yxdm");
                }
                catch { }

                DataTable yh = Sqlhelper.Serach("select pjjxbmdm,pjbmdm from yonghqx where yhid='" + id + "'");
                if (yh.Rows.Count > 0)
                {
                    string sql = "";


                    string bmdm = yh.Rows[0][0].ToString();
                    string pjbmdm = yh.Rows[0][1].ToString();
                    if (yxdm.Split(',').Length > 0)
                    {
                        for (int xx = 0; xx < yxdm.Split(',').Length; xx++)
                        {
                            if (yxdm.Split(',')[xx].Length > 0)
                            {
                                bmdm = bmdm.Replace(yxdm.Split(',')[xx] + ",", "");
                                pjbmdm = pjbmdm.Replace(yxdm.Split(',')[xx], "");


                                //if (yh.Rows[q][1].ToString() == yxdm.Split(',')[xx]) sql = ",pjbmdm=''";
                            }
                            //this.Label1.Text += yxdm.Split(',')[xx]+"<br>"+bmdm+"部门代码<br>";
                        }
                    }
                    if (Sqlhelper.ExcuteNonQuery("update yonghqx set pjjxbmdm='" + bmdm + "',pjbmdm='"+pjbmdm+"'" + sql + " where yhid='" + id + "'") > 0)
                    {
                        Label1.Text = "<font color=green>将" + id + "移出本系成功！</font>";
                    }
                    else
                    {
                        Label1.Text = "<font color=red>将" + id + "移出本系失败</font>";
                    }
                    //this.Label1.Text += bmdm;
                }
                //Response.Write("<script>alert('导入成功！');</script>");
            }
            catch (Exception)
            {

                basic.MsgBox(this.Page, "移出失败", "-1");
            }

        }

        Sql_yh.SelectCommand = ViewState["SqlDataSource1.SelectCommand"].ToString();
        _gridView.DataBind();
    }
}
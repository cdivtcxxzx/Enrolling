using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_yonghsqadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            ViewState["sql"] = Sql_yh.SelectCommand;
            Sql_yh.SelectCommand = ViewState["sql"].ToString();
            GridView1.DataBind();
        }
        Sql_yh.SelectCommand = ViewState["sql"].ToString();
       // GridView1.DataBind();
        
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
                  // string yhid = e.CommandArgument.ToString();
            //string xm= selectedRow.Cells[1].Text;
            try
            {
                string yxdm = "";
                try
                {
                    yxdm=new Power().Getonebmdm("yxdm").Split(',')[0];
                }
                catch { }
              DataTable yh=Sqlhelper.Serach("select pjjxbmdm,pjbmdm,xm from yonghqx where yhid='"+chkIds[i]+"'");
                  if (yh.Rows.Count>0)
                  {
                      string sql = "";
                      if(yh.Rows[0][1].ToString().Length==0)sql=",pjbmdm='"+yxdm+"'";
                      string bmdm = yh.Rows[0][0].ToString().Replace(yxdm + ",","") + yxdm + ",";
                      if (Sqlhelper.ExcuteNonQuery("update yonghqx set pjjxbmdm='" + bmdm + "'"+sql+" where yhid='" + chkIds[i] + "'") > 0)
                      {
                    
                            cg = cg + 1;
                        }
                        else
                        {
                            sbjl += "导入"+yh.Rows[0][2].ToString()+"失败！";
                        }
                       
                  }
                //Response.Write("<script>alert('导入成功！');</script>");
            }
            catch (Exception)
            {

                basic.MsgBox(this.Page, "导入失败", "-1");
            }
        
                //Response.Write("<script>alert('" + chkIds[i] + "');</script>");

        }
            this.add_tip.Text = "<font color=green> &nbsp;&nbsp;&nbsp;&nbsp;共导入" + cg + "教师!</font><font color=red>" + sbjl + "</font>";
        }
        catch
        {
            this.add_tip.Text = "<font color=red> &nbsp;&nbsp;&nbsp;&nbsp;批量清除出错！</font>";
        }


    }

    #endregion
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    /// <summary>
    /// 点击页面中的按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string x = e.CommandName;
        if (e.CommandName == "DaoRu")
        {
            //int index = Convert.ToInt32(e.CommandArgument);
            //GridViewRow selectedRow = GridView1.Rows[index];

            string yhid = e.CommandArgument.ToString();
            //string xm= selectedRow.Cells[1].Text;
            try
            {
                string yxdm = "";
                try
                {
                    yxdm=new Power().Getonebmdm("yxdm").Split(',')[0];
                }
                catch { }
              DataTable yh=Sqlhelper.Serach("select pjjxbmdm,pjbmdm,xm from yonghqx where yhid='"+yhid+"'");
                  if (yh.Rows.Count>0)
                  {
                      string sql = "";
                      if(yh.Rows[0][1].ToString().Length==0)sql=",pjbmdm='"+yxdm+"'";
                      string bmdm = yh.Rows[0][0].ToString().Replace(yxdm + ",","") + yxdm + ",";
                      if (Sqlhelper.ExcuteNonQuery("update yonghqx set pjjxbmdm='" + bmdm + "'"+sql+" where yhid='" + yhid + "'") > 0)
                      {
                          add_tip.Text = "<font color=green>将" + yh.Rows[0][2].ToString() + "加入本系成功！</font>";
                      }
                      else
                      {
                          add_tip.Text = "<font color=red>将" + yh.Rows[0][2].ToString() + "加入本系失败</font>";
                      }
                  }
                //Response.Write("<script>alert('导入成功！');</script>");
            }
            catch (Exception)
            {

                basic.MsgBox(this.Page, "导入失败", "-1");
            }
        }
        GridView1.DataBind();

    }
    protected void Search_Onclick(object sender, EventArgs e)
    {
        ViewState["sql"] = "SELECT pjjxbmdm,yhid,xm,yhqx,lsz,dltime,fwcs,guid,dm_yuanxi.yxmc,yonghqx.yxdm from yonghqx inner join dm_yuanxi on yonghqx.yxdm=dm_yuanxi.yxdm where (yhid like '%" + this.SearchBox.Text + "%' or yxmc like '%" + this.SearchBox.Text + "%' or xm like '%" + this.SearchBox.Text + "%')";
        Sql_yh.SelectCommand = ViewState["sql"].ToString();
        //Response.Write(ViewState["sql"].ToString());
        GridView1.DataBind();
        Label2.Visible = false;
    }

}
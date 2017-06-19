using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class nradmingl_ssgl_clear : System.Web.UI.Page
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
    private string pagelm1 = "宿舍预分配清空";//请与系统栏目管理中栏目关键字设置为一致便于权限管理

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
            //Response.Write(qx);
            if (qx.Length > 0)
            {
                this.ztts.Text = "你能管理的数据有：";
            }
            else { this.ztts.Text = "您暂时没有能管理的数据，请联系迎新管理员"; }
            #endregion
            if (qx.Split(',').Length > 0)
            {

                for (int i = 0; i < qx.Split(',').Length; i++)
                {
                    #region 清除本年度预分配数据
                    string sqlcx = "SELECT TOP 1 [YXMC] FROM  [DM_YUANXI] where yxdm='" + qx.Split(',')[i].ToString() + "'";
                    DataTable qxd = Sqlhelper.Serach(sqlcx);
                    //this.ztts.Text +=sqlcx;
                    if (qxd.Rows.Count > 0)
                    {
                        //查询有多少条预分配数据
                       // SELECT     count(Fresh_Bed_Class_Log.PK_Bed_Class_Log) FROM         Fresh_SPE RIGHT OUTER JOIN                      Fresh_Class ON Fresh_SPE.PK_SPE = Fresh_Class.FK_SPE_NO RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO
                        sx += " or PK_College='" + qx.Split(',')[i].ToString()+"'";


                        this.ztts.Text += qxd.Rows[0][0].ToString() + ",";
                    }
                   
                    #endregion
                }
                if(sx.Length>0)
                {
                    sx = " 1=2 " + sx + "";
                }
            }
           // Response.Write(sx);
            SqlDataSource2.FilterExpression = sx;





      
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


   


   

    
    protected void clearyfp(object sender, EventArgs e)
    {
        string qx = "";
        string sqlyfp = "";
        try
        {
    
                    #region 清除本年度班级预分配数据
                   
                    if (c_bedyfp.Checked)
                    {
                         //sqlyfp = "delete Fresh_Bed_Class_Log FROM         Fresh_Bed_Class_Log LEFT OUTER JOIN                      Fresh_Class ON Fresh_Bed_Class_Log.FK_Class_NO = Fresh_Class.PK_Class_NO LEFT OUTER JOIN                      Fresh_SPE ON Fresh_Class.FK_SPE_NO = Fresh_SPE.PK_SPE WHERE     (Fresh_SPE.FK_College_Code = '" + yxdm.SelectedValue + "') and Fresh_SPE.Year='" + this.DropDownList1.SelectedValue + "'";
                        //查询出该操作员能够操作的预分配数据
                        sqlyfp=" SELECT     Fresh_Bed_Class_Log.PK_Bed_Class_Log AS 床位主键, Fresh_Bed_Class_Log.FK_Bed_NO AS 床位编号, Fresh_Bed_Class_Log.FK_Class_NO AS 班级代码, Fresh_Bed_Class_Log.College_NO AS 院系代码, Fresh_Bed_Log.FK_SNO AS 学号, Fresh_Dorm.Year AS 年度 FROM         Fresh_Bed LEFT OUTER JOIN         Fresh_Room LEFT OUTER JOIN                      Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO RIGHT OUTER JOIN                      Fresh_Bed_Log RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed_Log.FK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO  where Fresh_Dorm.Year='"+year.SelectedValue+"' and Fresh_Bed_Class_Log.College_NO='"+yxdm.SelectedValue+"'";
                        //Response.Write(sqlyfp + "<br>");
                        
                        //1、查出原有多少条数据，已经被选择的有多少条
                        DataTable countcw = Sqlhelper.Serach(sqlyfp);
                        int yxcw = countcw.Rows.Count;//记录能操作的床位数
                        int ybxcw = 0;//记录已经被选的床位
                        if(yxcw>0)
                        {
                            for(int i=0;i<yxcw;i++)
                            {
                                if (countcw.Rows[i]["学号"].ToString().Length > 0) ybxcw++;
                            }
                        }


                    //2、统计设置成功的数据
                        string sqlcz = "update [Fresh_Bed_Class_Log] set [FK_Class_NO]=null from Fresh_Bed LEFT OUTER JOIN        Fresh_Room LEFT OUTER JOIN                      Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO RIGHT OUTER JOIN                      Fresh_Bed_Log RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed_Log.FK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO  where Fresh_Dorm.Year='" + year.SelectedValue + "' and Fresh_Bed_Class_Log.College_NO='" + yxdm.SelectedValue + "' and Fresh_Bed_Log.FK_SNO is null";
                       // Response.Write(sqlcz + "<br>");
                        int czcg = Sqlhelper.ExcuteNonQuery(sqlcz);
                        ztts.Text = "<font color=blue>清空班级预分配操作提示:" +year.SelectedValue  + "年"+yxdm.SelectedItem.Text+"共分配床位："+yxcw.ToString()+",学生已选:"+ybxcw.ToString()+",本次清除："+czcg.ToString()+"【如果有学生选择床位将无法清除分配数据】</font><br>";

                    
                    
                    
                    
                    }
                    #endregion
                    #region 清除本年度院系预分配数据

                    if (c_bedyfpyx.Checked)
                    {
                        //sqlyfp = "delete Fresh_Bed_Class_Log FROM         Fresh_Bed_Class_Log LEFT OUTER JOIN                      Fresh_Class ON Fresh_Bed_Class_Log.FK_Class_NO = Fresh_Class.PK_Class_NO LEFT OUTER JOIN                      Fresh_SPE ON Fresh_Class.FK_SPE_NO = Fresh_SPE.PK_SPE WHERE     (Fresh_SPE.FK_College_Code = '" + yxdm.SelectedValue + "') and Fresh_SPE.Year='" + this.DropDownList1.SelectedValue + "'";
                        //查询出该操作员能够操作的预分配数据
                        sqlyfp = " SELECT     Fresh_Bed_Class_Log.PK_Bed_Class_Log AS 床位主键, Fresh_Bed_Class_Log.FK_Bed_NO AS 床位编号, Fresh_Bed_Class_Log.FK_Class_NO AS 班级代码, Fresh_Bed_Class_Log.College_NO AS 院系代码, Fresh_Bed_Log.FK_SNO AS 学号, Fresh_Dorm.Year AS 年度 FROM         Fresh_Bed LEFT OUTER JOIN         Fresh_Room LEFT OUTER JOIN                      Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO RIGHT OUTER JOIN                      Fresh_Bed_Log RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed_Log.FK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO  where Fresh_Dorm.Year='" + year.SelectedValue + "' and Fresh_Bed_Class_Log.College_NO='" + yxdm.SelectedValue + "'";
                        //Response.Write(sqlyfp + "<br>");

                        //1、查出原有多少条数据，已经被选择的有多少条
                        DataTable countcw = Sqlhelper.Serach(sqlyfp);
                        int yxcw = countcw.Rows.Count;//记录能操作的床位数
                        int ybxcw = 0;//记录已经被选的床位
                        if (yxcw > 0)
                        {
                            for (int i = 0; i < yxcw; i++)
                            {
                                if (countcw.Rows[i]["学号"].ToString().Length > 0) ybxcw++;
                            }
                        }


                        //2、统计设置成功的数据
                        string sqlcz = "delete [Fresh_Bed_Class_Log]  from Fresh_Bed LEFT OUTER JOIN        Fresh_Room LEFT OUTER JOIN                      Fresh_Dorm ON Fresh_Room.FK_Dorm_NO = Fresh_Dorm.PK_Dorm_NO ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO RIGHT OUTER JOIN                      Fresh_Bed_Log RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Bed_Log.FK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO  where Fresh_Dorm.Year='" + year.SelectedValue + "' and Fresh_Bed_Class_Log.College_NO='" + yxdm.SelectedValue + "' and Fresh_Bed_Log.FK_SNO is null";
                        //Response.Write(sqlcz + "<br>");
                        int czcg = Sqlhelper.ExcuteNonQuery(sqlcz);
                        int czcg2 = Sqlhelper.ExcuteNonQuery("update Fresh_Bed set college_no=null FROM         Fresh_Dorm RIGHT OUTER JOIN     Fresh_Room ON Fresh_Dorm.PK_Dorm_NO = Fresh_Room.FK_Dorm_NO RIGHT OUTER JOIN    Fresh_Bed ON Fresh_Room.PK_Room_NO = Fresh_Bed.FK_Room_NO where college_no='"+yxdm.SelectedValue+"' and Year='"+year.SelectedValue+"'");
                        ztts.Text += "<font color=blue>清空预分配到院操作提示:" + year.SelectedValue + "年" + yxdm.SelectedItem.Text + "共分配床位：" + yxcw.ToString() + ",学生已选:" + ybxcw.ToString() + ",本次删除分配床位数：" + czcg.ToString() + ",清空分配到系床位数："+czcg2.ToString()+"【如有学生选寝将无法删除】</font><br>";





                    }
                    #endregion
            #region 清除床位信息

            #endregion
        }
        catch (Exception e1)
        {
            ztts.Text = "<font color=red>操作出错:" + e1.Message+"</font>";
        }
    }

    protected void c_roomtype_CheckedChanged(object sender, EventArgs e)
    {
        if(c_roomtype.Checked)
        {
            c_dorm.Checked = true;
            c_room.Checked = true;
            c_bed.Checked = true;
            c_bedyfpyx.Checked = true;
            c_bedyfp.Checked = true;

            //变灰
            c_dorm.Enabled = false;
            c_room.Enabled = false;
            c_bed.Enabled = false;
            c_bedyfpyx.Enabled = false;
            c_bedyfp.Enabled = false;
        }
        else
        {
            c_dorm.Enabled = true;
            c_room.Enabled = true;
            c_bed.Enabled = true;
            c_bedyfpyx.Enabled = true;
            c_bedyfp.Enabled = true;
        }
    }
    protected void c_dorm_CheckedChanged(object sender, EventArgs e)
    {
        if(c_dorm.Checked)
        {
            c_room.Checked = true;
            c_bed.Checked = true;
            c_bedyfpyx.Checked = true;
            c_bedyfp.Checked = true;
            //变灰
           
            c_room.Enabled = false;
            c_bed.Enabled = false;
            c_bedyfpyx.Enabled = false;
            c_bedyfp.Enabled = false;
        }
        else
        {
            c_room.Enabled = true;
            c_bed.Enabled = true;
            c_bedyfpyx.Enabled = true;
            c_bedyfp.Enabled = true;

        }
    }
    protected void c_room_CheckedChanged(object sender, EventArgs e)
    {
        if(c_room.Checked)
        {
            c_bed.Checked = true;
            c_bedyfpyx.Checked = true;
            c_bedyfp.Checked = true;
            //变灰

           
            c_bed.Enabled = false;
            c_bedyfpyx.Enabled = false;
            c_bedyfp.Enabled = false;
        }
        else
        {
            c_bed.Enabled = true;
            c_bedyfpyx.Enabled = true;
            c_bedyfp.Enabled = true;
        }
    }
    protected void c_bed_CheckedChanged(object sender, EventArgs e)
    {
        if(c_bed.Checked)
        {
            c_bedyfpyx.Checked = true;
            c_bedyfp.Checked = true;
            //变灰

            c_bedyfpyx.Enabled = false;
            c_bedyfp.Enabled = false;
        }
        else
        {
           
            c_bedyfpyx.Enabled = true;
            c_bedyfp.Enabled = true;
        }
    }
    protected void c_bedyfpyx_CheckedChanged(object sender, EventArgs e)
    {
        if(c_bedyfpyx.Checked)
        {
            c_bedyfp.Checked = true;
            //变灰

            c_bedyfp.Enabled = false;
        }
        else
        {

          
            c_bedyfp.Enabled = true;
        }
    }
}

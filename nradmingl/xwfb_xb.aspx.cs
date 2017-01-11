using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_xwfb : System.Web.UI.Page
{
       #region 页面初始化参数

    private string pagelm1 = "发布专题信息";

    private string pageqx1 = "浏览";
    private string pageqx2 = "修改";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(SqlDataSource3.SelectCommand);
       // Response.Write(SqlDataSource2.SelectCommand);
        new c_login().tongyiyz(pagelm1, pageqx1, "进入"+pagelm1+"页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
        if (!IsPostBack)
        {
            if (Request["id"] != null)
            {
                //编辑新闻
                this.btn_Action.Text = "确认修改";
                DataTable xwneirong = Sqlhelper.Serach("select * from xibu_neirong where id=" + Request["id"].ToString());
                if (xwneirong.Rows.Count > 0)
                {
                    DataTable fid = Sqlhelper.Serach("select fid,ztid from xibu_lanm where lmid=" + xwneirong.Rows[0]["lmid"].ToString());
                    if (fid.Rows.Count > 0)
                    {
                        if (fid.Rows[0][0].ToString() == "-1")
                        {
                            
                            DropDownList1.DataBind();
                            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(xwneirong.Rows[0]["lmid"].ToString()));
                        }
                        else
                        {
                            DropDownList1.DataBind();
                            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(fid.Rows[0]["fid"].ToString()));
                            DropDownList2.DataBind();
                            DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(xwneirong.Rows[0]["lmid"].ToString()));
                            DropDownList3.DataBind();
                            DropDownList3.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(fid.Rows[0]["ztid"].ToString()));
                        }
                    }
                    else
                    {
                        this.btn_Action.Text = "确认发布";
                        return;
                    }

                    this.btn_Action.Text = "确认修改";
                    this.TB_title.Text = xwneirong.Rows[0]["title"].ToString();
                    this.txtnewsContent.Text = xwneirong.Rows[0]["Content"].ToString();
                    if (xwneirong.Rows[0]["images"].ToString().Length > 0)
                    {
                        this.CheckBox1.Checked = true;
                        this.image.Text = xwneirong.Rows[0]["images"].ToString();
                        Session["image"] = xwneirong.Rows[0]["images"].ToString();
                        this.image.Visible = true;
                    }
                    //ListBox1.DataBind();
                    //阅读权限,选中
                    //int i = 0;
                    //Response.Write(xwneirong.Rows[0]["readpower"].ToString());
                    //foreach (ListItem x in ListBox1.Items)
                    //{
                    //    if (xwneirong.Rows[0]["readpower"].ToString().Contains(x.Value))
                    //    //{ ListBox1.SelectedIndex=i; }
                    //    { x.Selected = true; }
                    //    i = i + 1;
                    //}
                }
                else
                {
                    this.btn_Action.Text = "确认发布";
                }
            }
        }

    }
    protected void btn_Action_Click(object sender, EventArgs e)
    {
        tsxx.Text = "";
        string xbdm1 = this.DropDownList3.SelectedValue;
        //判断标题
        string title = this.TB_title.Text;
        if (title.Length <= 4)
        {
            this.tsxx.Text = "<font color=red>标题不能少于4个字符！</font>";
        }


        //获取新闻栏目ID
        string xwlmid = "";
        if (DropDownList2.SelectedValue.Length > 0)
        {
            xwlmid = DropDownList2.SelectedValue;
        }
        else
        {
            xwlmid = DropDownList1.SelectedValue;
        }
        if (xwlmid.Length == 0)
        {
            this.tsxx.Text += "<font color=red>新闻栏目为空！</font>";
        }
        //阅读权限
        string qx = "";
        foreach (ListItem x in ListBox1.Items)
        {
            if (x.Selected) { qx += x.Value + "|"; }
        }
        Response.Write(qx);
        //获取内容
        if (this.txtnewsContent.Text.Length <= 4)
        {
            this.tsxx.Text += "<font color=red>新闻内容太短！</font>";
        }
        //有错误信息，返回
        if (this.tsxx.Text.Length > 0) return;
        //幻灯
        string hdimage = "";
        if (this.image.Text.Length > 4) hdimage = this.image.Text;

     
        string sqlcx = "";
        if (Request["id"] != null)
        {
            sqlcx = "select id,bz from xibu_neirong where id='" + Request["id"].ToString()+ "'";
        }
        else
        {
            sqlcx = "select id,bz from xibu_neirong where lmid='" + xwlmid + "' and title='" + title + "'";
        }
        //[title],[author],[CopyFrom],[LMID],[shenpr],[Content],[fabutime],[shenpitime],[hits],[isyn],[isFlash] ,[images],[px],[shenpyhid],[fabuyhid],[readpower] FROM [xibu_neirong]
        DataTable xw = Sqlhelper.Serach(sqlcx);
        if (xw.Rows.Count > 0)
        {
            //更新新闻
            string sql="";
            if (hdimage.Length > 0)
            {
                sql += ",isFlash='1',images='" + hdimage + "'";
            }
            else
            {
                sql += ",isFlash='0',images=''";
           
            }
            if (qx.Length > 0)
            {
                if (qx=="00|")
                {
                    sql += ",readpower='" + qx + "',isyn='0'";
                }
                else
                {
                    sql += ",readpower='" + qx + "',isyn='1'";
                }
            }
            else
            {
                sql += ",readpower='00|',isyn='0'";
            
            }

           // Response.Write("UPDATE xibu_neirong  SET author ='" + Session["Name"].ToString() + "',title=@title,CopyFrom ='" + pagelm1 + "',Content=@Content,fabutime='" + DateTime.Now.ToString() + "',isyn='1',fabuyhid='" + Session["UserName"].ToString() + "'" + sql + " where id='" + xw.Rows[0][0].ToString() + "',bz='"+xw.Rows[0][1].ToString()+"|"+Session["UserName"].ToString()+"在"+DateTime.Now.ToString()+"修改新闻'");

            if (Sqlhelper.ExcuteNonQuery("UPDATE xibu_neirong  SET xbdm='"+xbdm1+"',author ='" + Session["Name"].ToString() + "',title=@title,CopyFrom ='" + pagelm1 + "',Content=@Content,fabutime='" + DateTime.Now.ToString() + "',fabuyhid='" + Session["UserName"].ToString() + "'" + sql + ",bz='" + xw.Rows[0][1].ToString() + "|" + Session["UserName"].ToString() + "在" + DateTime.Now.ToString() + "修改新闻'  where id='" + xw.Rows[0][0].ToString() + "' ",
                new SqlParameter("title", title), new SqlParameter("Content", txtnewsContent.Text.Replace("font-family:", "font-family:微软雅黑,宋体,").Replace("-width:0.5pt;", "-width:1pt;"))) > 0)
            {
                Response.Write("<script>alert('更新信息成功！');location.href='xwfb.aspx?id=" + xw.Rows[0][0].ToString() + "';</script>");
            }
            else
            {
                tsxx.Text="<font color=red>更新信息失败，请重试</font>";
            }
     
        }
        else
        {
            //插入新闻
            //INSERT INTO xibu_neirong([title],[author],[CopyFrom],[LMID],[shenpr],[Content],[fabutime],[shenpitime],[hits],[isyn],[isFlash],[images],[px],[shenpyhid] ,[fabuyhid],[readpower])VALUES()
            string sql = "";
            if (hdimage.Length > 0)
            {
                sql += ",'1','" + hdimage + "'";
            }
            else
            {
                sql += ",'0',''";
            }
            if (qx.Length > 0)
            {
                if (qx=="00|")
                {
                    sql += ",'" + qx + "','0'";
                }
                else
                {
                    sql += ",'" + qx + "','1'";
                }
            }
            else
            {
                sql += ",'00|','0'";
            }
            //Response.Write("INSERT INTO xibu_neirong([title],[author],[CopyFrom],[LMID],[Content],[fabutime],[hits],[isyn],[isFlash],[images],[readpower],[px],[fabuyhid],[bz])VALUES(@title,'" + Session["Name"].ToString() + "','" + pagelm1 + "','" + xwlmid + "',@Content,'" + DateTime.Now.ToString() + "','1','1'" + sql + ",'2','" + Session["UserName"].ToString() + "','" + Session["Name"].ToString() + DateTime.Now.ToString() + "添加新闻')");
            //Response.End();
            if (Sqlhelper.ExcuteNonQuery("INSERT INTO xibu_neirong([xbdm],[title],[author],[CopyFrom],[LMID],[Content],[fabutime],[hits],[isFlash],[images],[readpower],[isyn],[px],[fabuyhid],[bz])VALUES('"+xbdm1+"',@title,'" + Session["Name"].ToString() + "','" + pagelm1 + "','" + xwlmid + "',@Content,'" + DateTime.Now.ToString() + "','1'" + sql + ",'2','" + Session["UserName"].ToString() + "','" + Session["Name"].ToString() + DateTime.Now.ToString() + "添加新闻')", new SqlParameter("title", title), new SqlParameter("Content", txtnewsContent.Text.Replace("-width:0.5pt;", "-width:1pt;").Replace("font-family:", "font-family:微软雅黑,宋体,"))) > 0)
            {
                Response.Write("<script>alert('发布信息成功！');location.href='xwfb.aspx';</script>");
            }
            else
            {
                tsxx.Text = "<font color=red>发布信息失败，请重试</font>";
            }


        }

    }
    protected void reset_OnClick(object sender, EventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            image.Visible = true;
            try
            {
                image.Text = Session["image"].ToString();
            }
            catch
            {
            }
        }
        else
        {
            image.Text = "";
 
            image.Visible = false;
        }
    }
    protected void DropDownList2_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList2.Items.Count > 0)
            {
                this.DropDownList2.Style.Add("display", "");
            }
            else
            {
                this.DropDownList2.Style.Add("display", "none");
            }
        }
        catch (Exception ex) { }
    }
    protected void ListBox1_DataBound(object sender, EventArgs e)
    {
        if (Request["id"] != null)
        {
            DataTable xwneirong = Sqlhelper.Serach("select * from xibu_neirong where id=" + Request["id"].ToString());
            int i = 0;
           // Response.Write(xwneirong.Rows[0]["readpower"].ToString());
            foreach (ListItem x in ListBox1.Items)
            {
                if (xwneirong.Rows[0]["readpower"].ToString().Contains(x.Value))
                //{ ListBox1.SelectedIndex=i; }
                { x.Selected = true; }
                i = i + 1;
            }
           
        }
    }
}
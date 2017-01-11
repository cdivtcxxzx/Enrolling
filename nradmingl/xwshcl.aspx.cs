using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


public partial class admin_xwshcl : System.Web.UI.Page
{
       #region 页面初始化参数

    private string pagelm1 = "发布审核处理";

    private string pageqx1 = "浏览";
    private string pageqx2 = "修改";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        new c_login().tongyiyz(pagelm1, pageqx1, "进入"+pagelm1+"页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
        if (!IsPostBack)
        {
            if (Request["id"] != null)
            {
                //编辑新闻
                this.btn_Action.Text = "审核通过";
                DataTable xwneirong = Sqlhelper.Serach("select * from xw_neirong where id=" + Request["id"].ToString());
                if (xwneirong.Rows.Count > 0)
                {
                    DataTable fid = Sqlhelper.Serach("select fid from xw_lanm where lmid=" + xwneirong.Rows[0]["lmid"].ToString());
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
                        }
                    }
                    else
                    {
                        this.btn_Action.Text = "确认发布";
                        return;
                    }

                    this.btn_Action.Text = "审核通过";
                    this.TB_title.Text = xwneirong.Rows[0]["title"].ToString();
                    this.txtnewsContent.Text = xwneirong.Rows[0]["Content"].ToString();
                    if (xwneirong.Rows[0]["images"].ToString().Trim().Length > 0)
                    {
                        this.CheckBox1.Checked = true;
                        this.image.Text = xwneirong.Rows[0]["images"].ToString();
                        //Session["image"] = xwneirong.Rows[0]["images"].ToString();
                        this.image.Visible = true;
                    }
                    
                    if (this.txtnewsContent.Text.Length > 0)
                    {
                        image.Visible = true;
                        Image1.Visible = true;
                        DropDownList3.Visible = true;
                        DropDownList3.Items.Clear();
                       
                        Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
                        MatchCollection matches = regImg.Matches(this.txtnewsContent.Text);
                        //tsxx.Text = "内容：" + regImg.ToString();
                        string tp = "新闻图片";
                        int i = 1;
                        foreach (Match match in matches)
                        {
                            ListItem x = new ListItem(tp + i.ToString(), match.Groups["imgUrl"].Value);
                            //Response.Write(match.Groups["imgUrl"].Value);
                            // Response.Write(match.Groups["sr"].Value);

                            DropDownList3.Items.Add(x);
                            i++;
                        }
                        //image.Text = DropDownList3.SelectedValue;
                        if (DropDownList3.SelectedValue == "")
                        {
                            this.Label1.Text = "<font color=red>当前新闻内容中没有图片,可手动填写一个网络地址或再上传一张图片</font>";
                            DropDownList3.Visible = false;
                            Image1.Visible = false;
                        }
                        Image1.ImageUrl = DropDownList3.SelectedValue;

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

        //阅读权限
        string qx="";
        foreach (ListItem x in ListBox1.Items)
        {
            if (x.Selected) { qx += x.Value+"|"; }
        }
        string sqlcx = "";
        if (Request["id"] != null)
        {
            sqlcx = "select id,bz from xw_neirong where id='" + Request["id"].ToString()+ "'";
        }
        else
        {
            sqlcx = "select id,bz from xw_neirong where lmid='" + xwlmid + "' and title='" + title + "'";
        }
        //[title],[author],[CopyFrom],[LMID],[shenpr],[Content],[fabutime],[shenpitime],[hits],[isyn],[isFlash] ,[images],[px],[shenpyhid],[fabuyhid],[readpower] FROM [xw_neirong]
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
                    sql += ",readpower='" + qx + "',isyn='1'";
                }
                else
                {
                    sql += ",readpower='" + qx + "',isyn='1'";
                }
            }
            else
            {
                sql += ",readpower='00|',isyn='1'";
            
            }

           // Response.Write("UPDATE xw_neirong  SET author ='" + Session["Name"].ToString() + "',title=@title,CopyFrom ='" + pagelm1 + "',Content=@Content,fabutime='" + DateTime.Now.ToString() + "',isyn='1',fabuyhid='" + Session["UserName"].ToString() + "'" + sql + " where id='" + xw.Rows[0][0].ToString() + "',bz='"+xw.Rows[0][1].ToString()+"|"+Session["UserName"].ToString()+"在"+DateTime.Now.ToString()+"修改新闻'");

            if (Sqlhelper.ExcuteNonQuery("UPDATE xw_neirong  SET spyj='" + this.spyj.Text + "',shenpr=@shenpr,title=@title,CopyFrom ='" + pagelm1 + "',Content=@Content,shenpitime='" + DateTime.Now.ToString() + "'" + sql + ",bz='" + xw.Rows[0][1].ToString() + "|" + Session["UserName"].ToString() + "在" + DateTime.Now.ToString() + "审批新闻'  where id='" + xw.Rows[0][0].ToString() + "' ",
                new SqlParameter("shenpr", Session["username"].ToString()), new SqlParameter("title", title), new SqlParameter("Content", txtnewsContent.Text)) > 0)
            {
                Response.Write("<script>alert('审核新闻成功！');location.href='xwsh.aspx';</script>");
            }
            else
            {
                tsxx.Text="<font color=red>审核新闻失败，请重试</font>";
            }
     
        }
        else
        {
            //插入新闻
            //INSERT INTO xw_neirong([title],[author],[CopyFrom],[LMID],[shenpr],[Content],[fabutime],[shenpitime],[hits],[isyn],[isFlash],[images],[px],[shenpyhid] ,[fabuyhid],[readpower])VALUES()
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
                if (qx.Contains("00"))
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
                sql += ",'00','0'";
            }
            //Response.Write("INSERT INTO xw_neirong([title],[author],[CopyFrom],[LMID],[Content],[fabutime],[hits],[isyn],[isFlash],[images],[readpower],[px],[fabuyhid],[bz])VALUES(@title,'" + Session["Name"].ToString() + "','" + pagelm1 + "','" + xwlmid + "',@Content,'" + DateTime.Now.ToString() + "','1','1'" + sql + ",'2','" + Session["UserName"].ToString() + "','" + Session["Name"].ToString() + DateTime.Now.ToString() + "添加新闻')");
            //Response.End();
            if (Sqlhelper.ExcuteNonQuery("INSERT INTO xw_neirong([title],[author],[CopyFrom],[LMID],[Content],[fabutime],[hits],[isFlash],[images],[readpower],[isyn],[px],[fabuyhid],[bz])VALUES(@title,'" + Session["Name"].ToString() + "','" + pagelm1 + "','" + xwlmid + "',@Content,'" + DateTime.Now.ToString() + "','1','1'" + sql + ",'2','" + Session["UserName"].ToString() + "','"+Session["Name"].ToString()+DateTime.Now.ToString()+"添加新闻')",new SqlParameter("title", title), new SqlParameter("Content", txtnewsContent.Text)) > 0)
            {
                Response.Write("<script>alert('发布新闻成功！');location.href='xwfb.aspx';</script>");
            }
            else
            {
                tsxx.Text = "<font color=red>发布新闻失败，请重试</font>";
            }


        }

    }
    protected void reset_OnClick(object sender, EventArgs e)
    {

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (CheckBox1.Checked)
            {
                if (this.DropDownList3.SelectedValue.Length > 0)
                {
                    image.Text = this.DropDownList3.SelectedValue;
                    Image1.ImageUrl = this.DropDownList3.SelectedValue;
                }
                else
                {
                    if (this.txtnewsContent.Text.Length > 0)
                    {
                        image.Visible = true;
                        
                        DropDownList3.Items.Clear();
                        image.Text = "";
                        Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
                        MatchCollection matches = regImg.Matches(this.txtnewsContent.Text);
                        //tsxx.Text = "内容：" + regImg.ToString();
                        string tp = "新闻图片";
                        int i = 1;
                        foreach (Match match in matches)
                        {
                            ListItem x = new ListItem(tp + i.ToString(), match.Groups["imgUrl"].Value);
                            //Response.Write(match.Groups["imgUrl"].Value);
                            // Response.Write(match.Groups["sr"].Value);

                            DropDownList3.Items.Add(x);
                            i++;
                        }
                        if (DropDownList3.SelectedValue != "")
                        {
                            Image1.Visible = true;
                            DropDownList3.Visible = true;
                            image.Text = DropDownList3.SelectedValue;
                            Image1.ImageUrl = DropDownList3.SelectedValue;
                        }

                    }

                }
            }
            else
            {
                image.Text = "";

            }
        }
        catch { }
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
            DataTable xwneirong = Sqlhelper.Serach("select * from xw_neirong where id=" + Request["id"].ToString());
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
    protected void btn_Action_Clickno(object sender, EventArgs e)
    {
        tsxx.Text = "";
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

        //阅读权限
        string qx = "";
        foreach (ListItem x in ListBox1.Items)
        {
            if (x.Selected) { qx += x.Value + "|"; }
        }
        string sqlcx = "";
        if (Request["id"] != null)
        {
            sqlcx = "select id,bz from xw_neirong where id='" + Request["id"].ToString() + "'";
        }
        else
        {
            sqlcx = "select id,bz from xw_neirong where lmid='" + xwlmid + "' and title='" + title + "'";
        }
        //[title],[author],[CopyFrom],[LMID],[shenpr],[Content],[fabutime],[shenpitime],[hits],[isyn],[isFlash] ,[images],[px],[shenpyhid],[fabuyhid],[readpower] FROM [xw_neirong]
        DataTable xw = Sqlhelper.Serach(sqlcx);
        if (xw.Rows.Count > 0)
        {
            //更新新闻
            string sql = "";
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
                    sql += ",readpower='" + qx + "',isyn='2'";
                }
                else
                {
                    sql += ",readpower='" + qx + "',isyn='2'";
                }
            }
            else
            {
                sql += ",readpower='00',isyn='2'";

            }

            // Response.Write("UPDATE xw_neirong  SET author ='" + Session["Name"].ToString() + "',title=@title,CopyFrom ='" + pagelm1 + "',Content=@Content,fabutime='" + DateTime.Now.ToString() + "',isyn='1',fabuyhid='" + Session["UserName"].ToString() + "'" + sql + " where id='" + xw.Rows[0][0].ToString() + "',bz='"+xw.Rows[0][1].ToString()+"|"+Session["UserName"].ToString()+"在"+DateTime.Now.ToString()+"修改新闻'");

            if (Sqlhelper.ExcuteNonQuery("UPDATE xw_neirong  SET spyj='" + this.spyj.Text + "',author ='" + Session["Name"].ToString() + "',title=@title,CopyFrom ='" + pagelm1 + "',Content=@Content,fabutime='" + DateTime.Now.ToString() + "',fabuyhid='" + Session["UserName"].ToString() + "'" + sql + ",bz='" + xw.Rows[0][1].ToString() + "|" + Session["UserName"].ToString() + "在" + DateTime.Now.ToString() + "修改新闻'  where id='" + xw.Rows[0][0].ToString() + "' ",
                new SqlParameter("title", title), new SqlParameter("Content", txtnewsContent.Text)) > 0)
            {
                Response.Write("<script>alert('审核新闻为打回状态成功！');location.href='xwsh.aspx';</script>");
            }
            else
            {
                tsxx.Text = "<font color=red>审核新闻失败，请重试</font>";
            }

        }
        else
        {
            //插入新闻
            //INSERT INTO xw_neirong([title],[author],[CopyFrom],[LMID],[shenpr],[Content],[fabutime],[shenpitime],[hits],[isyn],[isFlash],[images],[px],[shenpyhid] ,[fabuyhid],[readpower])VALUES()
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
                if (qx.Contains("00"))
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
                sql += ",'00','0'";
            }
            //Response.Write("INSERT INTO xw_neirong([title],[author],[CopyFrom],[LMID],[Content],[fabutime],[hits],[isyn],[isFlash],[images],[readpower],[px],[fabuyhid],[bz])VALUES(@title,'" + Session["Name"].ToString() + "','" + pagelm1 + "','" + xwlmid + "',@Content,'" + DateTime.Now.ToString() + "','1','1'" + sql + ",'2','" + Session["UserName"].ToString() + "','" + Session["Name"].ToString() + DateTime.Now.ToString() + "添加新闻')");
            //Response.End();
            if (Sqlhelper.ExcuteNonQuery("INSERT INTO xw_neirong([title],[author],[CopyFrom],[LMID],[Content],[fabutime],[hits],[isFlash],[images],[readpower],[isyn],[px],[fabuyhid],[bz])VALUES(@title,'" + Session["Name"].ToString() + "','" + pagelm1 + "','" + xwlmid + "',@Content,'" + DateTime.Now.ToString() + "','1','1'" + sql + ",'2','" + Session["UserName"].ToString() + "','" + Session["Name"].ToString() + DateTime.Now.ToString() + "添加新闻')", new SqlParameter("title", title), new SqlParameter("Content", txtnewsContent.Text)) > 0)
            {
                Response.Write("<script>alert('发布新闻成功！');location.href='xwfb.aspx';</script>");
            }
            else
            {
                tsxx.Text = "<font color=red>发布新闻失败，请重试</font>";
            }


        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DropDownList3.SelectedValue.Length > 0)
        {
            image.Text = this.DropDownList3.SelectedValue;
            Image1.ImageUrl = this.DropDownList3.SelectedValue;
        }
    }
}
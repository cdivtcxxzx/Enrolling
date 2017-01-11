using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

public partial class admin_xwfb : System.Web.UI.Page
{
    #region 页面初始化参数

    private string pagelm1 = "发布反馈信息";

    private string pageqx1 = "浏览";
    private string pageqx2 = "修改";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        new c_login().tongyiyz(pagelm1, pageqx1, "进入" + pagelm1 + "页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
        //管理筛选
        this.SqlDataSource2.FilterExpression = new Power().Getlanm("glqx");
        this.SqlDataSource3.FilterExpression = new Power().Getlanm("glqx");
       // Response.Write(new Power().Getlanm("glqx"));
        if (!IsPostBack)
        {
            this.TextBox1.Text = DateTime.Now.ToString();


            if (Request["id"] != null)
            {
                //编辑新闻
                this.btn_Action.Text = "确认修改";
                //this.LinkButton1.Visible = true;
                this.xwyl.HRef = "/xw.aspx?xwid=" + Request["id"].ToString() + "&sh=0";
                DataTable xwneirong = Sqlhelper.Serach("select * from xw_neirong where id=" + Request["id"].ToString());
                if (xwneirong.Rows.Count > 0)
                {
                    DataTable fid = Sqlhelper.Serach("select fid,sfsh from xw_lanm where lmid=" + xwneirong.Rows[0]["lmid"].ToString());
                    if (fid.Rows.Count > 0)
                    {
                        if (fid.Rows[0][0].ToString() == "-1")
                        {
                            DropDownList1.DataBind();
                            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(xwneirong.Rows[0]["lmid"].ToString()));
                       //获取该栏目情况
                            if (fid.Rows[0]["sfsh"].ToString() == "1")
                            {
                                this.Label1.Text = "该栏目只有一级栏目，<font color=red>开启了新闻审核，编辑新闻后需审核</font>";
                                this.CheckBox2.Checked = true;
                            }
                            else
                            {
                                this.CheckBox2.Checked = false;
                                this.Label1.Text = "该栏目只有一级栏目，发布新闻的不需要审核";
                            }
                        }
                        else
                        {
                            DropDownList1.DataBind();
                            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(fid.Rows[0]["fid"].ToString()));
                            DropDownList2.DataBind();
                            DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(xwneirong.Rows[0]["lmid"].ToString()));
                            //获取该栏目情况
                            if (fid.Rows[0]["sfsh"].ToString() == "1")
                            {
                                this.CheckBox2.Checked = true;
                                this.Label1.Text = "该栏目有二级栏目，<font color=red>开启了新闻审核，编辑新闻后需审核</font>";
                            }
                            else
                            {
                                this.CheckBox2.Checked = false;
                                this.Label1.Text = "该栏目有二级栏目，发布新闻的不需要审核";
                            }
                        }
                        CheckImgState();
                    }
                    else
                    {
                        this.btn_Action.Text = "确认发布";
                        return;
                    }

                    this.btn_Action.Text = "确认修改";
                    try
                    {
                        ViewState["xw_img"] = xwneirong.Rows[0]["xw_img"].ToString();
                        xwimg.ImageUrl = xwneirong.Rows[0]["xw_img"].ToString().Split('.')[0] + "-small.jpg";
                    }
                    catch (Exception)
                    {


                    }
                    if (xwneirong.Rows[0]["spyj"].ToString().Length > 1)
                    {
                        this.spyj.Text ="审批意见："+xwneirong.Rows[0]["spyj"].ToString();
                    }
                    this.TB_title.Text = xwneirong.Rows[0]["title"].ToString();
                    this.txtnewsContent.Text = xwneirong.Rows[0]["Content"].ToString();
                    this.wburl.Text = xwneirong.Rows[0]["wburl"].ToString();
                    this.TextBox1.Text = xwneirong.Rows[0]["fabutime"].ToString();
                    this.isZhiDing_true.Checked = xwneirong.Rows[0]["isZhiDing"].ToString() == "1" ? true : false;
                    this.isZhiDing_false.Checked = !this.isZhiDing_true.Checked;
                    if (xwneirong.Rows[0]["images"].ToString().Trim().Length > 0)
                    {
                        //this.CheckBox1.Checked = true;
                        this.image.Text = xwneirong.Rows[0]["images"].ToString();
                        //Session["image"] = xwneirong.Rows[0]["images"].ToString();
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
                    this.TextBox1.Text = DateTime.Now.ToString();

                    this.btn_Action.Text = "确认发布";
                }
            }
            else
            {
                this.xwyl.Style.Add("display","none");
            }
        }
        

    }
    /// <summary>
    /// 检查栏目类型，设置展示图片标准
    /// </summary>
    protected void CheckImgState()
    {
        string lmid1 = DropDownList1.SelectedValue;
        string lmid2 = DropDownList2.SelectedValue;
        string lx = "";
        lx = Sqlhelper.Serach("select lx,sfsh from xw_lanm where lmid='" + lmid1 + "'").Rows[0]["lx"].ToString();
        if (lmid2 != "-99" && lmid2 != "")
        {
            lx = Sqlhelper.Serach("select lx,sfsh from xw_lanm where lmid='" + lmid2 + "'").Rows[0]["lx"].ToString();
        }
        string sh = Sqlhelper.Serach("select lx,sfsh from xw_lanm where lmid='" + lmid1 + "'").Rows[0]["sfsh"].ToString();
        if (sh == "1")
        {
            this.Label1.Text = "该栏目<font color=red>开启了新闻审核，编辑新闻后需审核</font>";
            this.CheckBox2.Checked = true;
        }
        else
        {
            this.Label1.Text = "该栏目发布新闻的不需要审核";
            this.CheckBox2.Checked = false;
        }
        if (lmid2.Length > 0)
        {
            try
            {
                sh = Sqlhelper.Serach("select lx,sfsh from xw_lanm where lmid='" + lmid2 + "'").Rows[0]["sfsh"].ToString();
                if (sh == "1")
                {
                    this.CheckBox2.Checked = true;
                    this.Label1.Text = "该栏目<font color=red>开启了新闻审核，编辑新闻后需审核</font>";
                }
                else
                {
                    this.CheckBox2.Checked = false;
                    this.Label1.Text = "该栏目发布新闻的不需要审核";
                }
            }
            catch { }
        }
        xwimg.Visible = true;
        FileUpload1.Enabled = true;
        upload.Enabled = true;
        tips.InnerText = "该栏目必须上传展示图片";
        tips.Style.Add("color", "Red");
        if (lx == "图文")
        {
            ViewState["xw_lx"] = "true";
            ViewState["img_w"] = 500; ViewState["img_h"] = 320;
            
        }
        else if (lx == "图标"||lx == "视频")
        {
            ViewState["xw_lx"] = "true";
            ViewState["img_w"] = 300; ViewState["img_h"] = 120;
            
        }
        else
        {
            ViewState["xw_lx"] = "false";
            xwimg.Visible = false;
            FileUpload1.Enabled = false;
            upload.Enabled = false;
            tips.InnerText = "该栏目不用上传展示图片";
            tips.Style.Add("color", "Green");
        }
    }

    protected void btn_Action_Click(object sender, EventArgs e)
    {
        string xw_img = "";
        if (ViewState["xw_lx"] != null && ViewState["xw_lx"].ToString() == "true")
        {
            if (ViewState["xw_img"] != null)
            {
                xw_img = ViewState["xw_img"].ToString();
            }
            else { tsxx.Text = "<font color=red>发布信息失败，该栏目必须上传图片</font>"; return; }
        }
        tsxx.Text = "";
        //判断时间正确性
        

            DateTime dt1;

            if (DateTime.TryParse(this.TextBox1.Text, out dt1))
            {

            }
            else
            {
                this.tsxx.Text = "<font color=red>发布时间类型错误！</font>";
            }
            string wburl1 = "";
            if (this.wburl.Text.Trim().Length > 0)
            {
                wburl1 = wburl.Text.Trim();
                this.txtnewsContent.Text = "该新闻为外部链接新闻,若要取消外部链接跳转,请删除链接地址";
            }
        //判断标题
        string title = this.TB_title.Text;
        if (title.Length <= 3)
        {
            this.tsxx.Text = "<font color=red>标题不能少于4个字符！</font>";
        }
        //展示图片
        
        //获取新闻栏目ID
        string xwlmid = "";
        if (DropDownList2.SelectedValue != "" && DropDownList2.SelectedValue != "-99")
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
        //Response.Write(qx);
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
            sqlcx = "select id,bz from xw_neirong where id='" + Request["id"].ToString() + "'";
        }
        else
        {
            sqlcx = "select id,bz from xw_neirong where lmid='" + xwlmid + "' and title='" + title + "'";
        }
        //[title],[author],[CopyFrom],[LMID],[shenpr],[Content],[fabutime],[shenpitime],[hits],[isyn],[isFlash] ,[images],[px],[shenpyhid],[fabuyhid],[readpower] FROM [xw_neirong]
       

        DataTable xw = Sqlhelper.Serach(sqlcx);
        string isZhiDing = isZhiDing_true.Checked ? "1" : "0";
        string sh = "1";
        if (this.CheckBox2.Checked) { sh = "0"; }
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
                if (qx == "00|")
                {
                    sql += ",readpower='" + qx + "',isyn='"+sh+"'";
                }
                else
                {
                    sql += ",readpower='" + qx + "',isyn='" + sh + "'";
                }
            }
            else
            {
                sql += ",readpower='00|',isyn='" + sh + "'";

            }
            // Response.Write("UPDATE xw_neirong  SET author ='" + Session["Name"].ToString() + "',title=@title,CopyFrom ='" + pagelm1 + "',Content=@Content,fabutime='" + DateTime.Now.ToString() + "',isyn='1',fabuyhid='" + Session["UserName"].ToString() + "'" + sql + " where id='" + xw.Rows[0][0].ToString() + "',bz='"+xw.Rows[0][1].ToString()+"|"+Session["UserName"].ToString()+"在"+DateTime.Now.ToString()+"修改新闻'");

            if (Sqlhelper.ExcuteNonQuery("UPDATE xw_neirong  SET isZhiDing=@isZhiDing, [LMID]='"+xwlmid+"',title=@title,CopyFrom ='" + pagelm1 + "',wburl=@wburl,Content=@Content,xw_img='" + xw_img + "'" + sql + ",bz='" + xw.Rows[0][1].ToString() + "|" + Session["UserName"].ToString() + "在" + DateTime.Now.ToString() + "修改新闻'  where id='" + xw.Rows[0][0].ToString() + "' ",
                new SqlParameter("isZhiDing", isZhiDing), new SqlParameter("title", title), new SqlParameter("wburl", wburl1), new SqlParameter("Content", txtnewsContent.Text)) > 0)
            {
                Response.Write("<script>alert('更新信息成功！');location.href='xwfb.aspx?id=" + xw.Rows[0][0].ToString() + "';</script>");
            }
            else
            {
                tsxx.Text = "<font color=red>更新信息失败，请重试</font>";
            }

        }
        else
        {
            //插入新闻
            //INSERT INTO xw_neirong([title],[author],[CopyFrom],[LMID],[shenpr],[Content],[fabutime],[shenpitime],[hits],[isyn],[isFlash],[images],[px],[shenpyhid] ,[fabuyhid],[readpower])VALUES()
            string sql = "";
            if (hdimage.Length > 0)
            {
                sql += ",'" + sh + "','" + hdimage + "'";
            }
            else
            {
                sql += ",'0',''";
            }
            if (qx.Length > 0)
            {
                if (qx == "00|")
                {
                    sql += ",'" + qx + "','" + sh + "'";
                }
                else
                {
                    sql += ",'" + qx + "','" + sh + "'";
                }
            }
            else
            {
                sql += ",'00|','" + sh + "'";
            }

            //Response.Write("INSERT INTO xw_neirong([title],[author],[CopyFrom],[LMID],[Content],[fabutime],[hits],[isyn],[isFlash],[images],[readpower],[px],[fabuyhid],[bz])VALUES(@title,'" + Session["Name"].ToString() + "','" + pagelm1 + "','" + xwlmid + "',@Content,'" + DateTime.Now.ToString() + "','1','1'" + sql + ",'2','" + Session["UserName"].ToString() + "','" + Session["Name"].ToString() + DateTime.Now.ToString() + "添加新闻')");
            //Response.End();
            if (Sqlhelper.ExcuteNonQuery("INSERT INTO xw_neirong([title],[author],[CopyFrom],[LMID],[Content],[fabutime],[xw_img],[hits],[isFlash],[images],[readpower],[isyn],[px],[fabuyhid],[bz],[isZhiDing],[wburl])VALUES(@title,'" + Session["Name"].ToString() + "','" + pagelm1 + "','" + xwlmid + "',@Content,'" + this.TextBox1.Text + "','" + xw_img + "','1'" + sql + ",'2','" + Session["UserName"].ToString() + "','" + Session["Name"].ToString() + DateTime.Now.ToString() + "添加新闻',@isZhiDing,@wburl)", new SqlParameter("title", title), new SqlParameter("Content", txtnewsContent.Text), new SqlParameter("isZhiDing", isZhiDing), new SqlParameter("wburl", wburl1)) > 0)
            {
                Response.Write("<script>alert('发布信息成功！');location.href='xwfb.aspx';</script>");
                tsxx.Text = "<font color=red>发布信息成功，你可以再次发布！</font>";
                this.TB_title.Text = "";
                this.txtnewsContent.Text = "";
                this.TextBox1.Text = "";
                this.image.Text = "";
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
    protected void upload_Click(object sender, EventArgs e)
    {
        Boolean fileOK = false;
        string path = Server.MapPath("~/upload/xwimg/");
        string fileExt = "";
        if (FileUpload1.HasFile)
        {
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
            string[] allowedExt = { ".jpg", ".jpeg", ".gif", ".png",".bmp" };
            for (int i = 0; i < allowedExt.Length; i++)
            {
                if (fileExt == allowedExt[i])
                {
                    fileOK = true;
                }
            }
        }
        if (fileOK)
        {
            try
            {
                string file_guid = Guid.NewGuid().ToString();
                string fileName = file_guid + fileExt;
                //path += fileName;
                FileUpload1.SaveAs(path + fileName);
                Bitmap bmp = new Bitmap(path + fileName);
                int img_w = Convert.ToInt16(ViewState["img_w"]);
                int img_h = Convert.ToInt16(ViewState["img_h"]);
                if (bmp.Width == img_w && bmp.Height == img_h)
                {
                    //改变大小
                    var thumbnail = bmp.GetThumbnailImage(200, 200 * img_h / img_w, null, IntPtr.Zero);
                    //存诸
                    thumbnail.Save(path + file_guid + "-small" + ".jpg", ImageFormat.Jpeg);
                    ViewState["xw_img"] = "/upload/xwimg/" + fileName;
                    tips.InnerText = "上传成功";
                    xwimg.ImageUrl = "/upload/xwimg/" + file_guid + "-small" + ".jpg";
                    xwimg.DataBind();
                }
                else
                {
                    tips.InnerText = "图片大小必须是宽"+img_w+"，高"+img_h;
                }
            }
            catch (Exception ex)
            {
                tips.InnerText = "上传失败";
            }
        }
        else
        {
            tips.InnerText = "上传的图片格式只能为jpg,jpeg,png,gif,bmp之一";
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        //if (CheckBox1.Checked)
        //{
        //      if (this.txtnewsContent.Text.Length>0)
        //{
        //    image.Visible = true;
        //    image.Text = "";
        //    Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
        //    MatchCollection matches = regImg.Matches(this.txtnewsContent.Text);
        //    tsxx.Text="内容：" + regImg.ToString();
        //    string tp = "新闻图片";
        //    int i = 1;
        //    foreach (Match match in matches)
        //    {
        //        ListItem x = new ListItem(tp + i.ToString(), match.Groups["imgUrl"].Value);
        //        Response.Write(match.Groups["imgUrl"].Value);
        //       // Response.Write(match.Groups["sr"].Value);

        //        DropDownList3.Items.Add(x);
        //        i++;
        //    }
        //    image.Text = DropDownList3.SelectedItem.Value;
        //    Image1.ImageUrl = DropDownList3.SelectedItem.Value;
          
        //}
        //else
        //{
        //    image.Visible = true;
        //    image.Text = "";
        //    if(Session["image"]!=null)
        //    {
        //    try
        //    {
        //        image.Visible = true;
        //        image.Text = Session["image"].ToString();
        //        string tp = "新闻图片";
        //        int i = 1;

        //        ListItem x = new ListItem(tp + i.ToString(), image.Text);

        //        DropDownList3.Items.Add(x);
        //        image.Text = DropDownList3.SelectedItem.Text;
        //        Image1.ImageUrl = DropDownList3.SelectedItem.Text;
        //    }
        //    catch
        //    {
        //    }
        //    }
        //      }
        
            
       
          
        //   // image.Visible = false;
        //}
        //    else
        //    {
        //        tsxx.Text = "<font color=red>你的新闻内容中没有图片信息，请先上传一张图片</font>";
        //    image.Visible = false;
        //    image.Text = "";
        //    }
    }
    protected void DropDownList2_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList2.Items.Count > 1)
            {
                this.DropDownList2.Style.Add("display", "");
            }
            else
            {
                this.DropDownList2.Style.Add("display", "none");
                
            }
        }
        catch (Exception ex) { }
        CheckImgState();
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckImgState();
    }
    protected void DropDownList1_DataBound(object sender, EventArgs e)
    {
        if (Request["lb"] != null)
        {
            if (Request["lb"] == "cb")
            {
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText("催办信息"));
            }
        }
        CheckImgState();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckImgState();
    }
    protected void btn_Action0_Click(object sender, EventArgs e)
    {
        if (Request["id"] != null)
        {
            //this.form1.Target = "_blank";
            Response.Redirect("/xw.aspx?xwid=" + Request["id"].ToString() + "&sh=0");
        }
        else
        {
            Response.Write("无参数");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //this.image.Text = this.txtnewsContent.Text;
        
            if (this.txtnewsContent.Text.Length > 0)
            {
                image.Visible = true;
                Image1.Visible = true;
                DropDownList3.Visible = true;
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
                image.Text = DropDownList3.SelectedItem.Value;
                Image1.ImageUrl = DropDownList3.SelectedItem.Value;

            }
            else
            {
                Image1.Visible = true;
                image.Visible = true;
                DropDownList3.Visible = true;
                DropDownList3.Items.Clear();
                image.Text = "";
                if (Session["image"] != null)
                {
                    try
                    {
                        image.Visible = true;
                        image.Text = Session["image"].ToString();
                        string tp = "新闻图片";
                        int i = 1;

                        ListItem x = new ListItem(tp + i.ToString(), image.Text);

                        DropDownList3.Items.Add(x);
                        image.Text = DropDownList3.SelectedItem.Text;
                        Image1.ImageUrl = DropDownList3.SelectedItem.Text;
                    }
                    catch
                    {
                    }
                }
                else
                {
                    Image1.Visible = false;
                    DropDownList3.Visible = false;
                    DropDownList3.Items.Clear();
                    tsxx.Text = "<font color=red>你的新闻内容中没有图片信息，请先上传一张图片</font>";
                    tsxxtp.Text = "<font color=red>你的新闻内容中没有图片信息，请先上传一张图片</font>";
                    image.Visible = false;
                    image.Text = "";
                }
            }


            if (image.Text.Length > 0) image.Visible = true;

            // image.Visible = false;
       
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
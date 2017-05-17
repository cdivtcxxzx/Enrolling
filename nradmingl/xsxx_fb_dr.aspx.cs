using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Web;
using model;//单文件上传类

public partial class nradmingl_xsxx_fb_dr : System.Web.UI.Page
{
    #region 页面初始化参数
    private string xwdith = "1366";//屏宽
    private string xheight = "768";//屏高
    private string pagelm1 = "学生分班管理";//请与系统栏目管理中栏目关键字设置为一致便于权限管理
    private string upfile = "xsxx_upload";//导入上传的临时文件名称
    //导入模板的字段
    private string zd = "学号,姓名,性别,身份证,专业,学制,年级,班级名称";

    private string pageqx1 = "导入";//权限名称，根据页面的权限控制命名，与栏目管理中权限一致，最大设置为５个
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "";//当前页面值，在加载时会自动获取
    private string btitle = "";//附属标题
    #endregion
    //班级缓存
    protected List<Fresh_Class> classes = organizationService.getAllClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
            #region 控制导入步骤各环节的显示
            if (Request["setp"] != null && Request["mb"] != null)
            {
                if (Request["setp"].ToString() == "1")
                {
                    setpdown.Style.Add("display", "");
                    setpup.Style.Add("display", "none");
                    setpdown.HRef = "?setp=2&mb=" + Request["mb"].ToString();

                    setp1cz.Style.Add("display", "");
                    setp2cz.Style.Add("display", "none");
                    setp3cz.Style.Add("display", "none");
                }
                if (Request["setp"].ToString() == "2")
                {
                    setp2.Attributes.Add("class", "active");

                    setpdown.Style.Add("display", "");
                    setpup.Style.Add("display", "");
                    setpup.HRef = "?setp=1&mb=" + Request["mb"].ToString();
                    setpdown.HRef = "?setp=3&mb=" + Request["mb"].ToString();
                    setp1cz.Style.Add("display", "none");
                    setp2cz.Style.Add("display", "");
                    setp3cz.Style.Add("display", "none");
                }
                if (Request["setp"].ToString() == "3")
                {
                    setp2.Attributes.Add("class", "active");
                    setp3.Attributes.Add("class", "active");
                    setpdown.Style.Add("display", "none");
                    setpup.Style.Add("display", "");
                    setpup.HRef = "?setp=2&mb=" + Request["mb"].ToString();

                    setp1cz.Style.Add("display", "none");
                    setp2cz.Style.Add("display", "none");
                    setp3cz.Style.Add("display", "");

                }

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
    protected void batch_import_Click(object sender, EventArgs e)
    {
        //导入逻辑
        #region 文件上传,初始数据准备

        

        //批次检查
        if (Session["batch_fb"] == null || Session["batch_fb"].ToString() == "" || Session["batch_fb"].ToString() == "-1")
        {
            //如果上传出错，显示错误提示
            ztxx.Text = "<font color=red>请在第上一步中选择批次！</font>";
            return;
        }

        //初始变量，总记录数(成功记录数)
        int zs = 0;
        //用于后期删除行的记录信息变量
        string delrow = "";

        //上传文件位置设置
        String path = Server.MapPath("~/" + Sqlhelper.gldir + "/upload/");//管理目录的上传文件夹
        //上传文件


        var Upload = new UploadFile();
        Upload.Save("FileUpload1", this.upfile, Session["Name"].ToString());

        //上传的控件名，存储名，谁上传的
        #endregion
        if (Upload.Error)
        {
            //如果上传出错，显示错误提示
            ztxx.Text = "<font color=red>" + Upload.Message + "</font>";
        }
        else
        {
            //ztxx.Text="上传成功,下面列出了不成功有误的数据请核对!</font>";

            //开始显示数据，将数据存入datatable中
            DataTable x = new DataTable();
            //将EXCEL表中所有数据存入x表格中
            x = ReadXLSByExcel(HttpContext.Current.Server.MapPath(Upload.FileInfo["filepath"]), zd);

            //循环输出，判断
            string[] zds = zd.Split(',');
            //读取所有行


            if (x.Rows.Count > 0)
            {
                //获取总记录数
                zs = x.Rows.Count;
                //添加错误提示
                x.Columns.Add("错误提示");
                x.Columns.Add("班级代码");



                //循环读取并判断所有字段
                for (int ii = 0; ii < x.Rows.Count; ii++)
                {
                    //设置默认错误提示为空
                    x.Rows[ii]["错误提示"] = "";
                    //判断逻辑为：（1)先判断每一列数据是否有误，无误后，(2)提交数据，提交成功的记录行号，(3)后面进行删除记录显示
                    try
                    {
                        #region (1)判断当前行数据有效性

                        //1 学号验证
                        Base_STU stu = organizationService.getStu(x.Rows[ii]["学号"].ToString().Trim().Replace("'", ""));

                        if (stu == null)
                        {
                            x.Rows[ii]["错误提示"] += "学号不存在";
                        }
                        else
                        {
                            //2.1 身份证验证
                            if (stu.ID_NO != x.Rows[ii]["身份证"].ToString().Trim().Replace("'", ""))
                            {
                                x.Rows[ii]["错误提示"] += " 身份证有误";
                            }
                            //2.2 姓名验证
                            if (stu.Name != x.Rows[ii]["姓名"].ToString().Trim().Replace("'", ""))
                            {
                                x.Rows[ii]["错误提示"] += " 姓名有误";
                            }
                        }
                           
                        



                        //3 班级代码 
                        Fresh_Class sel_class = classes.Where(c => c.Name == x.Rows[ii]["班级名称"].ToString().Trim()).SingleOrDefault();
                        if (sel_class == null)
                        {
                            x.Rows[ii]["错误提示"] += " 班级名称有误";
                        }
                        else
                        {
                            //4 专业和班级是否匹配
                            if (sel_class.FK_SPE_NO != stu.FK_SPE_Code)
                            {
                                x.Rows[ii]["错误提示"] += " 当前专业和班级不匹配";
                            }
                            else
                            {
                                x.Rows[ii]["班级代码"] = sel_class.PK_Class_NO;
                            }
                            
                        }
                        
                        

                        #endregion

                        //删除正确的行数据，并向数据库提交记录 
                        #region (2)修改提交数据
                        if (x.Rows[ii]["错误提示"].ToString().Length < 1 && stu != null)
                        {
                            //记录成功记录
                            int cgjj = 0;
                            try
                            {
                                //cgjj = Sqlhelper.ExcuteNonQuery("");

                                stu.FK_Class_NO = x.Rows[ii]["班级代码"].ToString();

                                if (organizationService.stuUpdate(stu.PK_SNO,stu))
                                {
                                    cgjj = 1;
                                }
                                else
                                {
                                    x.Rows[ii]["错误提示"] += " 班级修改失败";
                                }
                            }
                            catch (Exception ex)
                            {
                                x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + ex.Message + "";
                            }

                            #region 删除号逻辑，不用更改
                            if (cgjj > 0)
                            {
                                //记录要删除的行号
                                if (delrow == "")
                                {
                                    delrow = delrow + ii.ToString();

                                }
                                else
                                {
                                    delrow = delrow + "," + ii.ToString();
                                }
                                //记录删除的行号结束
                            }
                            else
                            {
                                x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + "写入数据库失败!";
                            }
                            #endregion

                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        //某行数据出错
                        x.Rows[ii]["错误提示"] = " 该学生数据有误请检查:" + ex.Message;
                    }

                    //精细化错误提示
                    if (x.Rows[ii]["错误提示"].ToString().Length > 3)
                    {
                        x.Rows[ii]["错误提示"] = "第" + (ii + 1).ToString() + "行:" + x.Rows[ii]["错误提示"].ToString();
                    }

                }
                #region (3)删除已经提交成功的行，仅显示错误提示,此逻辑不用修改

                string[] delok = delrow.Split(',');
                if (delok.Length > 0 && delrow != "")
                {//删除指定行

                    this.ztxx.Text = "<font color=red>总共" + x.Rows.Count + "条记录，已导入" + delok.Length.ToString() + "条记录，有" + (x.Rows.Count - delok.Length) + "条错误记录在下表显示!!</font>";
                    for (int ok = delok.Length - 1; ok >= 0; ok--)
                    {
                        try
                        {
                            x.Rows.RemoveAt(Convert.ToInt32(delok[ok]));
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                x.Rows[Convert.ToInt32(delok[ok])]["错误提示"] = "修改失败:" + ex.Message;
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                else
                {
                    this.ztxx.Text = "<font color=red>总共" + x.Rows.Count + "条记录，已导入0条记录，错误记录在下表显示!!</font>";
                }
                #endregion
            }

            #region (4)隐藏不需要提示的列,绑定gridview呈现错误数据
            x.Columns.Remove("班级代码");
            GridView1.DataSource = x;
            GridView1.DataBind();

            #endregion
            

        }

    }
    public DataTable ReadXLSByExcel(string fileFullPath, string zd)
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=;Data Source=" + fileFullPath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"";//只适合xls后缀

        string tableName = "";

        using (OleDbConnection oleConn = new OleDbConnection(strConn))
        {
            try
            {
                oleConn.Open();
            }
            catch (Exception ex)
            {
                this.ztxx.Text = "<font color=red>文件格式不对，请把需上传的文件另存为2003版XLS</font>" + ex.Message;
            }

            try
            {
                DataTable sheetNames = oleConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                //DataTable sheetNames = oleConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);

                tableName = sheetNames.Rows[0][2].ToString().Trim();//获取 Excel 的表名，默认值是sheet1

                if (tableName.Length <= 0)
                {
                    this.ztxx.Text = "<font color=red>EXCEL中的表不正确</font>";
                    return new DataTable();

                }


                string sql = "select " + zd + " from [" + tableName + "] ";


                DataSet ds = new DataSet();
                OleDbCommand objCmd = new OleDbCommand(sql, oleConn);
                OleDbDataAdapter myData = new OleDbDataAdapter(sql, oleConn);
                myData.Fill(ds, tableName);//填充数据

                return ds.Tables[0];

            }

            catch (Exception ex)
            {
                this.ztxx.Text = "<font color=red>文件格式不对，请下载和重新准备数据，注意列的对应！</font>" + ex.Message;
                return new DataTable();

            }
        }

    }
    //批次选择事件，记录于session中
    protected void DropDownListBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int batch_index = DropDownListBatch.SelectedIndex;
        string batch = DropDownListBatch.SelectedValue.ToString();
        if (batch_index > 0)
        {
            Session["batch_fb"] = batch;
        }
        else
        {
            Session["batch_fb"] = "-1";
        }
    }
    #region 设置页面显示条数事件
    protected void PageSize_Go(object sender, EventArgs e)
    {

        TextBox ps = (TextBox)this.GridView2.BottomPagerRow.FindControl("PageSize_Set");
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
        if (this.GridView2.Rows.Count != 0)
        {
            Control table = this.GridView2.Controls[0];
            int count = table.Controls.Count;
            table.Controls[count - 1].Visible = true;
        }
    }
    #endregion
    # region 显示总记录数
    protected void ObjectDataSource2_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        DataTable dt = (DataTable)e.ReturnValue;
        if (dt == null)
        {
            Session["fbDrRowsCount"] = "0";
        }
        else
        {
            Session["fbDrRowsCount"] = dt.Rows.Count.ToString();
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

    protected void DropDownListBatch_DataBound(object sender, EventArgs e)
    {
        //初次下拉加载数据后设置批次选中
        if (Session["batch_fb"] != null && Session["batch_fb"] != "")
        {
            DropDownListBatch.Items.FindByValue(Session["batch_fb"].ToString()).Selected = true;
        }
    }
}
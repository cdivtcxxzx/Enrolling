using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using Web;//单文件上传类

public partial class nradmingl_ssgl_dr : System.Web.UI.Page
{
    #region 功能模块说明及页面基本信息说明
    //所属模块：宿舍管理
    //任务名称：预分配寝室导入
    //完成功能描述：模块化导入演示及预分配寝室数据导入（以班为单位）
    //编写人：张明
    //创建日期：2017年4月28日
    //更新日期：2017年4月28日
    //版本记录：第一版,编写导入标准规范
    #endregion
    #region 页面初始化参数
    private string xwdith = "1366";//屏宽
    private string xheight = "768";//屏高
    private string pagelm1 = "预分配寝室管理";//请与系统栏目管理中栏目关键字设置为一致便于权限管理
    private string upfile = "预分配寝室导入";//导入上传的临时文件名称
    //导入模板的字段
    private string zd = "序号,年度,校区,公寓名称,楼层,房间编号,房间类型,房间人数,床位编号,床位位置说明,班级名称";

    private string pageqx1 = "导入";//权限名称，根据页面的权限控制命名，与栏目管理中权限一致，最大设置为５个
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
            #endregion
                #region 根据参数提供第一步的模板下载(mb=auto:使用配置的数据库语句自动生成EXCEL,mb=文件名路径)
                if (Request["mb"] != null)
                {
                    if (Request["mb"].ToString() == "auto")
                    {
                        //自定义生产模板文件，请参考“导出”项生成一个Excel下载地址，本项不需要

                    }
                    else
                    {
                        mbfile.HRef = Request["mb"].ToString();
                    }
                }
                else
                {
                    setp1cz.Style.Add("display", "");
                    setp2cz.Style.Add("display", "none");
                    setp3cz.Style.Add("display", "none");
                    setp1ts.Text = "<font color=red>程序员很懒,该页的导入模板参数未提供,请上报错误![出错地址:" + webpage + "]</font>";
                    this.setpdown.Style.Add("display", "none");

                }
                #endregion
                //Response.Write("第" + Request["setp"].ToString() + "步");
            }
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
        //初始变量，总记录数
        int zs = 0;
        //用于后期删除行的记录信息变量
        string delrow = "";
        
        //上传文件位置设置
        String path = Server.MapPath("~/" + Sqlhelper.gldir + "/upload/");//管理目录的上传文件夹
        //上传文件
        

        var Upload = new UploadFile();
        Upload.Save("FileUpload1", "寝室预分配数据上传", Session["Name"].ToString());
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
                //循环读取并判断所有字段
                for (int ii = 0; ii < x.Rows.Count; ii++)
                {
                    //设置默认错误提示为空
                    x.Rows[ii]["错误提示"] = "";
//判断逻辑为：（1)先判断每一列数据是否有误，无误后，(2)提交数据，提交成功的记录行号，(3)后面进行删除记录显示
                    try
                    {
#region (1)判断当前行数据有效性
                        //每列数据判断，第一列
                        if (x.Rows[ii]["序号"].ToString().Replace("'", "").Length > 0)
                        {
                            //条件满足，可进一步判断

                           


                        }
                        else
                        {
                            //不满足，抛出错误提示
                            x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + "序号不能为空！";

                        }
#endregion

                        //删除正确的行数据，并向数据库提交记录
 #region (2)修改提交数据
                        if (x.Rows[ii]["错误提示"].ToString().Length < 1)
                        {
                            //记录成功记录
                                int cgjj = 0;
                                try
                                {
                                    cgjj = Sqlhelper.ExcuteNonQuery("");
                                }
                                catch (Exception ex)
                                {
                                    x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + ex.Message + "";
                                }
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

                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        //某行数据出错
                        x.Rows[ii]["错误提示"] = "该学生数据有误请检查" + ex.Message;
                    }
                 
                    //精细化错误提示
                    if (x.Rows[ii]["错误提示"].ToString().Length > 3)
                    {
                        x.Rows[ii]["错误提示"] = "第" + (ii + 1).ToString() + "行:" + x.Rows[ii]["错误提示"].ToString();
                    }

                }
#region (3)删除已经提交成功的行，仅显示错误提示,此逻辑不用修改

                string[] delok = delrow.Split(',');
                if (delok.Length > 0)
                {//删除指定行

                    this.ztxx.Text = "<font color=red>成功" + delok.Length.ToString() + "条记录!!</font>";
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
                    this.ztxx.Text = "<font color=red>上传失败!失败的记录列在下面,请修改后,重新上传!</font>";
                }
                #endregion
            }

#region (4)隐藏不需要提示的列,绑定gridview呈现错误数据

            try
            {
                x.Columns.Remove("身份证号");
            }
            catch (Exception ex)
            {
                if (ex.Message == "列“身份证号”不属于表 。")
                {
                    this.ztxx.Text += "<font color=red>你上传的数据中的“身份证号”列取名不正确，请检查!</font>";
                }
                else
                {
                    this.ztxx.Text += "<font color=red>" + ex.Message + "!</font>";
                }
            }

            GridView1.DataSource = x;
            //GridView1.
            GridView1.DataBind();
            #endregion
        }
       
    }
    public DataTable ReadXLSByExcel(string fileFullPath, string zd)
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=;Data Source=" + fileFullPath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"";//只适合xls后缀
        // "Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=;Extended properties=Excel 5.0;Data Source="&Server.MapPath("/zsgl/upload/"+request("filename"))
        // string  strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileFullPath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
        // Response.Write(strConn);
        //    参数HDR的值：HDR=Yes，这代表第一行是标题，不做为数据使用 ，如果用HDR=NO，则表示第一行不是标题，做为数据来使用。系统默认的是YES  。
        //参数IMEX值：具体什么意思我也不清楚哈，不过如果没有设置=1的话，在单元格里面的中文有时不能正确读取到，会读到null。
        // 官方的解释：
        // IMEX ( IMport EXport mode )设置
        //当 IMEX=0 时为“汇出模式”，这个模式开启的 Excel 档案只能用来做“写入”用途。
        //当 IMEX=1 时为“汇入模式”，这个模式开启的 Excel 档案只能用来做“读取”用途。
        //当 IMEX=2 时为“连結模式”，这个模式开启的 Excel 档案可同时支援“读取”与“写入”用途。
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
                this.ztxx.Text = "<font color=red>文件格式不对，请下载模版重新准备数据，不要改变列的名字</font>" + ex.Message;
                return new DataTable();

            }
        }
       
    }
}
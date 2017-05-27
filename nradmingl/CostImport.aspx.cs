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

public partial class nradmingl_CostImport : System.Web.UI.Page
{
    #region 功能模块说明及页面基本信息说明
    //所属模块：费用标准管理
    //任务名称：费用标准导入
    //完成功能描述：费用标准数据导入
    //编写人：黄磊
    //创建日期：2017年5月8日
    //更新日期：2017年5月8日
    //版本记录：v1.0.0
    #endregion
    #region 页面初始化参数
    private string xwdith = "1366";//屏宽
    private string xheight = "768";//屏高
    private string pagelm1 = "费用标准管理";//请与系统栏目管理中栏目关键字设置为一致便于权限管理
    private string upfile = "费用标准导入";//导入上传的临时文件名称
    //导入模板的字段
    private string zd = "费用批次编号,专业代码,费用名称,金额,收费项目代码,收费项目名称,收费类型,是否必收,是否生成网上订单";
    //错误提示时，要隐藏的字段
    private string removeok = "年度,校区,公寓名称,楼层,房间类型,房间人数,床位位置说明";
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
            if (!IsPostBack)
            {
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
                #region 根据参数提供第一步的模板下载(mb=auto:使用配置的数据库语句自动生成EXCEL,mb=文件名路径)
                if (Request["mb"] != null)
                {
                    if (Request["mb"].ToString() == "auto")
                    {
                        //自定义生产模板文件，请参考“导出”项生成一个Excel下载地址，本项不需要

                    }
                    else
                    {
                        mbfile.HRef = "mb\\zzjwxjdr.xls";
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

            #region 开始显示数据，将数据存入datatable中,并判断列是否正确
            DataTable x = new DataTable();
            //将EXCEL表中所有数据存入x表格中,clok用于后面存所有记录，x记录错误记录
            toexcel todatatable = new toexcel();
            x = todatatable.ExcelfileToDatatalbe(HttpContext.Current.Server.MapPath(Upload.FileInfo["filepath"]), true);
            DataTable clok = new DataTable();
            //读取所有行
            //判断各列名是否正确
            string err = "";
            int colzs = zd.Split(',').Length;
            for (int i = 0; i < colzs; i++)
            {
                if (x.Columns.Contains(zd.Split(',')[i].ToString()))
                {
                    //存在

                }
                else
                {
                    //不存在
                    if (err == "")
                    {
                        err += zd.Split(',')[i].ToString();
                    }
                    else
                    {
                        err += "," + zd.Split(',')[i].ToString();
                    }
                }

            }
            if (err.Length > 0)
            {
                this.ztxx.Text = "<font color=red>模板有" + colzs.ToString() + "列【" + zd + "】<br>【" + err + "】列未找到，请确认数据准备正确!</font>";
                GridView1.DataSource = x;
                GridView1.DataBind();

                return;
            }
            #endregion

            if (x.Rows.Count > 0)
            {
                //获取总记录数
                zs = x.Rows.Count;
                //添加错误提示
                x.Columns.Add("错误提示");
                //循环读取并判断所有字段
                int updatetype = 0;//类型更新只写一次
                int updatedorm = 0;//宿舍更新只写一次e
                int updateroom = 0;//房间备注只写一次
                int updatebed = 0;//只更新一次床位备注
                for (int ii = 0; ii < x.Rows.Count; ii++)
                {
                    //设置默认错误提示为空
                    x.Rows[ii]["错误提示"] = "";
                    //判断逻辑为：（1)先判断每一列数据是否有误，无误后，(2)提交数据，提交成功的记录行号，(3)后面进行删除记录显示
                    try
                    {
                        #region (1)判断当前行数据有效性
                        //每列数据判断，第一列,验证举例
                        //"序号,年度,校区,公寓名称,楼层,房间编号,房间类型,房间人数,床位编号,床位位置说明,班级名称";

                        //校区,公寓名称,楼层,房间编号,房间类型,房间人数,床位编号,床位位置说明,班级名称";
                        string nd = x.Rows[ii]["年度"].ToString().Replace("'", "");
                        string xq = x.Rows[ii]["校区"].ToString().Replace("'", "");
                        string dormname = x.Rows[ii]["公寓名称"].ToString().Replace("'", "");
                        string floor = x.Rows[ii]["楼层"].ToString().Replace("'", "");
                        string roomid = x.Rows[ii]["房间编号"].ToString().Replace("'", "");
                        string roomtype = x.Rows[ii]["房间类型"].ToString().Replace("'", "");
                        string xb = x.Rows[ii]["性别"].ToString().Replace("'", "");
                        string roomrs = x.Rows[ii]["房间人数"].ToString().Replace("'", "");
                        string cwbh = x.Rows[ii]["床位编号"].ToString().Replace("'", "");
                        string cwms = x.Rows[ii]["床位位置说明"].ToString().Replace("'", "");
                        string bjmc = x.Rows[ii]["班级名称"].ToString().Replace("'", "");
                        int nuber;
                        #region 验证年度


                        if (nd.Length == 4)
                        {

                            if (!int.TryParse(nd, out nuber))
                            {
                                //非数字
                                x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + "年度列：" + nd + "不符要求，年度只能为4位数字！";
                            }

                        }
                        else
                        {
                            //不满足，抛出错误提示
                            if (nd.Length > 0)
                            {
                                x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + "年度列：" + "年度位数不对，只能为4位数字！";
                            }
                            else
                            {
                                x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + "年度不能为空！";
                            }

                        }
                        #endregion
                        #region 验证楼层


                        if (floor.Length <= 2)
                        {

                            if (!int.TryParse(floor, out nuber))
                            {
                                //非数字
                                x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + "楼层列：" + floor + "不符要求，楼层只能为2位以内数字！";
                            }

                            if (floor.Length == 0)
                            {
                                x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + "楼层不能为空！";
                            }

                        }
                        else
                        {
                            //不满足，抛出错误提示

                            x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + "楼层位数不对，只能为2位内数字！";


                        }
                        #endregion
                        #region 验证校区
                        int xqyz = 0;
                        string xqstr = "";
                        DataTable xqsql = Sqlhelper.Serach("SELECT TOP 20 [PK_Campus] 校区主键,[Campus_NO] 校区编号,[Campus_Name] 校区名称  FROM [Base_Campus] where [Enabled]='true'");
                        if (xqsql.Rows.Count > 0)
                        {
                            for (int i = 0; i < xqsql.Rows.Count; i++)
                            {
                                if (xq == xqsql.Rows[i]["校区名称"].ToString())
                                {
                                    xqyz = 1;
                                }
                                else
                                {
                                    if (i == 0)
                                    {
                                        xqstr += xqsql.Rows[i]["校区名称"].ToString();
                                    }
                                    else
                                    {
                                        xqstr += "," + xqsql.Rows[i]["校区名称"].ToString();
                                    }

                                }
                            }
                        }
                        if (xqyz == 0)
                        {
                            if (xqstr.Replace(",", "").Length > 0)
                            {
                                x.Rows[ii]["错误提示"] += "未找到该校区信息，系统中有的校区为：";
                            }
                            else
                            {
                                x.Rows[ii]["错误提示"] += "系统中无校区信息，请联系管理员！";
                            }
                        }
                        #endregion
                        int dormyz = 0;//记录是否已经有该公寓了
                        #region 验证公寓名称

                        //验证是否有该公寓
                        DataTable dormsql = Sqlhelper.Serach("SELECT TOP 10 [PK_Dorm_NO],[Dorm_NO],[Year],[Name] 公寓名称,[Campus_NO]  FROM [Fresh_Dorm] where Name='" + dormname + "' order by dorm_NO ");
                        if (dormsql.Rows.Count > 0)
                        {
                            dormyz = dormsql.Rows.Count;
                        }
                        if (dormyz > 1)
                        {
                            x.Rows[ii]["错误提示"] += dormname + "公寓名有重复！";
                        }
                        #endregion
                        int roomyz = 0;//记录是否已经有该房间了
                        #region 验证房间编号
                        DataTable roomsql = Sqlhelper.Serach("SELECT TOP 10 [Room_NO]  FROM [Fresh_Room] where Room_NO='" + roomid + "'");
                        if (roomsql.Rows.Count > 0)
                        {
                            roomyz = roomsql.Rows.Count;
                        }
                        if (roomyz > 1)
                        {
                            x.Rows[ii]["错误提示"] += roomid + "房间有重复，请处理！";
                        }
                        #endregion
                        int roomtypeyz = 0;//记录是否已经有该房间了
                        #region 验证房间类型
                        DataTable roomtypesql = Sqlhelper.Serach("SELECT TOP 10 [PK_Room_Type],[Type_NO],[Year],[Type_Name]  FROM [Fresh_Room_Type] where Type_Name='" + roomtype + "' and YEAR='" + nd + "'");
                        if (roomtypesql.Rows.Count > 0)
                        {
                            roomtypeyz = roomtypesql.Rows.Count;
                        }
                        if (roomtypeyz > 1)
                        {
                            x.Rows[ii]["错误提示"] += roomid + "房间类型有重复！";
                        }
                        #endregion

                        #region 验证房间人数是否为数字
                        if (roomrs.Length > 0)
                        {

                            if (!int.TryParse(roomrs, out nuber))
                            {
                                //非数字
                                x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + "房间人数列：" + roomrs + "不符要求，年度只能为数字！";
                            }
                            else
                            {
                                //验证数据库中是否有重复不同的房间数

                            }

                        }
                        else
                        {
                            //不满足，抛出错误提示

                            x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + "房间人数不能为空！";


                        }
                        #endregion
                        int bedyz = 0;//记录是否已经有该床位了
                        #region 床位编号验证
                        DataTable bedsql = Sqlhelper.Serach("SELECT TOP 10 [PK_Bed_NO],[Bed_NO],[Bed_Name]  FROM [Fresh_Bed] where bed_No='" + cwbh + "' and FK_Room_NO='" + roomid + "'");
                        if (bedsql.Rows.Count > 0)
                        {
                            bedyz = bedsql.Rows.Count;
                        }
                        if (bedyz > 1)
                        {
                            x.Rows[ii]["错误提示"] += roomid + "该房间有重复床位[" + cwbh + "]，请处理！";
                        }
                        #endregion
                        int bjmcyz = 0;//验证班级数量
                        #region 班级名称验证
                        //SELECT TOP 10 [PK_Class_NO],[FK_Campus_NO],[FK_SPE_NO],[Name]  FROM [Fresh_Class] where name=''
                        DataTable bjmcsql = Sqlhelper.Serach("SELECT TOP 10 [PK_Class_NO],[FK_Campus_NO],[FK_SPE_NO],[Name]  FROM [Fresh_Class] where name='" + bjmc + "'");
                        if (bjmcsql.Rows.Count > 0)
                        {
                            bjmcyz = bjmcsql.Rows.Count;
                        }
                        else
                        {
                            x.Rows[ii]["错误提示"] += bjmc + "系统中无该班级名称，请确认教务是否创建了班级或已从教务同步班级！";
                        }
                        if (bjmcyz > 1)
                        {
                            x.Rows[ii]["错误提示"] += bjmc + "班级有重复名称，请联系相关管理员处理！";
                        }
                        #endregion

                        #endregion
                        //Response.Write("上传文件:" + x.Rows[ii]["错误提示"].ToString());
                        //删除正确的行数据，并向数据库提交记录
                        #region (2)修改提交数据
                        if (x.Rows[ii]["错误提示"].ToString().Length < 1)
                        {
                            //记录成功记录
                            string cgjj = "";
                            try
                            {
                                if (updateroom_c.Checked)
                                {
                                    #region 导入房间类型
                                    string sqlupdate = "";
                                    if (roomtypeyz > 0)
                                    {
                                        //更新
                                        if (updatetype == 0)
                                        {

                                            sqlupdate = dormitory.update_Roomtype("", nd, roomtype, Session["username"].ToString(), 0);
                                        }
                                        else
                                        {
                                            sqlupdate = dormitory.update_Roomtype("", nd, roomtype, Session["username"].ToString(), 1);
                                        }
                                    }
                                    else
                                    {
                                        //写入
                                        sqlupdate = dormitory.update_Roomtype("", nd, roomtype, Session["username"].ToString(), 0);
                                    }
                                    cgjj = sqlupdate.Split('@')[0].ToString();
                                    if (cgjj == "1")
                                    {
                                        updatetype = 1;
                                    }
                                    else
                                    {
                                        x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + sqlupdate.Split('@')[1].ToString() + "";
                                    }
                                    #endregion
                                    #region 导入公寓宿舍信息

                                    if (dormyz > 0)
                                    {
                                        //更新
                                        if (updatedorm == 0)
                                        {

                                            sqlupdate = dormitory.update_Fresh_Dorm(nd, dormname, xq, Session["username"].ToString(), 0);
                                        }
                                        else
                                        {
                                            sqlupdate = dormitory.update_Fresh_Dorm(nd, dormname, xq, Session["username"].ToString(), 1);
                                        }
                                    }
                                    else
                                    {
                                        //写入
                                        sqlupdate = dormitory.update_Fresh_Dorm(nd, dormname, xq, Session["username"].ToString(), 1);
                                    }
                                    cgjj = sqlupdate.Split('@')[0].ToString();
                                    if (cgjj == "1")
                                    {
                                        updatedorm = 1;
                                    }
                                    else
                                    {
                                        x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + sqlupdate.Split('@')[1].ToString() + "";
                                    }
                                    #endregion
                                    //宿舍

                                    #region 导入房间信息

                                    if (roomyz > 0)
                                    {
                                        //更新
                                        if (updateroom == 0)
                                        {

                                            sqlupdate = dormitory.update_Fresh_room(roomid, dormname, floor, roomtype, xb, Session["username"].ToString(), 0);
                                        }
                                        else
                                        {
                                            sqlupdate = dormitory.update_Fresh_room(roomid, dormname, floor, roomtype, xb, Session["username"].ToString(), 1);
                                        }
                                    }
                                    else
                                    {
                                        //写入
                                        sqlupdate = dormitory.update_Fresh_room(roomid, dormname, floor, roomtype, xb, Session["username"].ToString(), 1);
                                    }
                                    cgjj = sqlupdate.Split('@')[0].ToString();
                                    if (cgjj == "1")
                                    {
                                        updateroom = 1;
                                    }
                                    else
                                    {
                                        x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + sqlupdate.Split('@')[1].ToString() + "";
                                    }
                                    #endregion
                                    //房间
                                    //床位
                                    //Response.Write("导入床位信息");
                                    #region 导入床位信息

                                    if (bedyz > 0)
                                    {
                                        //更新

                                        sqlupdate = dormitory.update_Fresh_bed(cwbh, cwms, roomid, Session["username"].ToString());

                                    }
                                    else
                                    {
                                        //写入
                                        sqlupdate = dormitory.update_Fresh_bed(cwbh, cwms, roomid, Session["username"].ToString());
                                    }

                                    // Response.Write("导入结果：" + sqlupdate);
                                    cgjj = sqlupdate.Split('@')[0].ToString();
                                    if (cgjj == "1")
                                    {
                                        updatebed = 1;
                                    }
                                    else
                                    {
                                        x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + sqlupdate.Split('@')[1].ToString() + "";
                                    }
                                    #endregion
                                }
                                //班级预分配床位
                                #region 班级预分配床位
                                string sqlupdat = "";
                                if (bjmcyz > 0)
                                {
                                    sqlupdat = dormitory.update_Fresh_bedyfp(cwbh, roomid, bjmc, Session["username"].ToString());
                                }
                                cgjj = sqlupdat.Split('@')[0].ToString();
                                if (cgjj != "1")
                                {
                                    x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + sqlupdat.Split('@')[1].ToString() + "";
                                }
                                #endregion


                            }
                            catch (Exception ex)
                            {
                                x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"] + ex.Message + "";
                            }





                            #region 删除号逻辑，不用更改
                            if (cgjj == "1")
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
                                x.Rows[ii]["错误提示"] = x.Rows[ii]["错误提示"].ToString();
                            }
                            #endregion

                        }

                        #endregion

                    }
                    catch (Exception ex)
                    {
                        //某行数据出错
                        x.Rows[ii]["错误提示"] = "该学生数据有误请检查" + ex.Message;
                    }

                    //精细化错误提示
                    if (x.Rows[ii]["错误提示"].ToString().Length > 1)
                    {
                        x.Rows[ii]["错误提示"] = "第" + (ii + 1).ToString() + "行:" + x.Rows[ii]["错误提示"].ToString();
                    }
                    else
                    {
                        x.Rows[ii]["错误提示"] = "导入成功";
                    }

                }
                #region (3)删除已经提交成功的行，仅显示错误提示,此逻辑不用修改
                //记录全部错误和正确记录
                clok = x;
                if (clok.Rows.Count > 0)
                {
                    //Response.Write(clok.Rows.Count.ToString());
                    for (int re = 0; re < removeok.Split(',').Length; re++)
                    {
                        try
                        {
                            //x.Columns.Remove(removeok.Split(',')[re].ToString());
                            clok.Columns.Remove(removeok.Split(',')[re].ToString());
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message == "列“" + removeok.Split(',')[re].ToString() + "”不属于表 。")
                            {
                                this.ztxx.Text += "<font color=red>你上传的数据中的“" + removeok.Split(',')[re].ToString() + "”列取名不正确，请检查!</font>";
                            }
                            else
                            {
                                this.ztxx.Text += "<font color=red>" + ex.Message + "!</font>";
                            }
                        }
                    }
                    GridView2.DataSource = clok;

                    GridView2.DataBind();
                }

                //clok.Columns["错误提示"].ColumnName = "操作提示";  

                //Response.Write("需删除的行："+delrow+"!");
                string[] delok = delrow.Split(',');
                if (delok.Length > 1)
                {//删除指定行
                    int zhs = delok.Length;
                    if (!delrow.Contains(","))
                    {
                        zhs = 0;



                    }
                    if (clok.Rows.Count == zhs)
                    {
                        //如果导入的记录全正确，显示全部正确记录
                        //Response.Write("如果导入的记录全正确，显示全部正确记录");
                        GridView2.Visible = true;
                        GridView1.Visible = false;
                        CheckBox1.Checked = true;
                    }
                    this.ztxx.Text = "<font color=red>成功" + zhs.ToString() + "条记录!!</font>";
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
            else
            {
                return;
            }

            #region (4)隐藏不需要提示的列,绑定gridview呈现错误数据

            for (int re = 0; re < removeok.Split(',').Length; re++)
            {
                try
                {
                    x.Columns.Remove(removeok.Split(',')[re].ToString());
                    //clok.Columns.Remove(removeok.Split(',')[re].ToString());
                }
                catch (Exception ex)
                {
                    //if (ex.Message == "列“" + removeok.Split(',')[re].ToString() + "”不属于表 。")
                    //{
                    //    this.ztxx.Text += "<font color=red>x你上传的数据中的“" + removeok.Split(',')[re].ToString() + "”列取名不正确，请检查!</font>";
                    //}
                    //else
                    //{
                    //    this.ztxx.Text += "<font color=red>" + ex.Message + "!</font>";
                    //}
                }
            }


            GridView1.DataSource = x;
            //GridView2.DataSource = clok;
            //GridView1.
            //隐藏错误提示列，仅显示自定义错误提示列
            //GridView1.Columns[1].Visible = false;
            GridView1.DataBind();
            //GridView2.DataBind();

            #endregion
            #region 跳到第三步，显示导入结果
            setp2.Attributes.Add("class", "active");
            setp3.Attributes.Add("class", "active");
            setpdown.Style.Add("display", "none");
            setpup.Style.Add("display", "");
            setpup.HRef = "?setp=2&mb=" + Request["mb"].ToString();

            setp1cz.Style.Add("display", "none");
            setp2cz.Style.Add("display", "none");
            setp3cz.Style.Add("display", "");




            #endregion
        }

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)//如果不是分页列
        {
            //将GRIDVIEW的第一列隐藏
            e.Row.Cells[6].Attributes.Add("style", "display:none;");
            e.Row.Cells[0].Attributes.Add("style", "text-align:left;width:40%;text-indent:-30px;padding-left:40px");
            e.Row.Cells[1].Attributes.Add("style", "width:50px;");
            //e.Row.Cells[2].Attributes.Add("style", "width:60px;");
            //e.Row.Cells[3].Attributes.Add("style", "width:40px;");
            //e.Row.Cells[4].Attributes.Add("style", "width:60px;");
            //e.Row.Cells[5].Attributes.Add("style", "width:100px;");
        }
    }
    protected string cwts(string ts)
    {
        if (ts.Contains("导入成功"))
        {
            return "<font color=green>" + ts + "</font>";
        }
        else
        {
            return "<font color=red><b>" + ts + "</b></font>";
        }
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            GridView1.Visible = false;
            GridView2.Visible = true;
        }
        else
        {
            GridView2.Visible = false;
            GridView1.Visible = true;
        }
    }
}
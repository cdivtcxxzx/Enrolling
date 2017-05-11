using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nradmingl_Class_Counseller : System.Web.UI.Page
{
    #region 页面初始化参数
    private string xwdith = "1366";//屏宽
    private string xheight = "768";//屏高
    private string pagelm1 = "班主任设置";//请与系统栏目管理中栏目关键字设置为一致便于权限管理

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
                if (Convert.ToInt32(xheight) < 860)
                {
                    GridView1.PageSize = 10;
                }
                else
                {
                    GridView1.PageSize = 15;
                }


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
            #region 数据筛选及ＳＱＬ数据源设置

            try
            {



                //管理筛选
                //this.SqlDataSource1.FilterExpression = new Power().Getlanm("glqx");//glqx对应院系代码当前查询中的字段名

                if (!IsPostBack)
                {
                    ViewState["gridsql"] = SqlDataSource1.SelectCommand;//绑定数据源的查询语句
                    //根据屏幕高度设置ＧＲＩＤＶＩＥＷ的ＰＡＧＥ显示条数
                    if (Convert.ToInt32(xheight) <= 728) this.GridView1.PageSize = 10;
                }
                else
                {

                    SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
                }
            }
            catch
            {
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
        if (!IsPostBack)
        {
            string PK_Class_NO="";
            try
            {
                PK_Class_NO = Request["id"].ToString();
            }
            catch (Exception)
            {

                //throw;
            }
            LB_Class_NO.Text = PK_Class_NO;
            LB_Class.Text = new GZJW().GetClassName(PK_Class_NO);
            DataTable dt=new GZJW().GetClass(PK_Class_NO);
            if(dt.Rows.Count>0)
            {
                div_ss.Visible = false;
                div_info.Visible = true;
                LB_yhid.Text = dt.Rows[0]["FK_Staff_NO"].ToString();
                LB_coun.Text = dt.Rows[0]["xm"].ToString();
                TB_phone.Text = dt.Rows[0]["Phone"].ToString();
                TB_qq.Text=dt.Rows[0]["QQ"].ToString();
            }
        }
    }
    /// <summary>
    /// 点击页面中的按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        #region 添加成员
        if (e.CommandName == "tianjia")
        {
            
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GridView1.Rows[index];
            TableCell yhid = selectedRow.Cells[0];
            TableCell name = selectedRow.Cells[1];
            //new Power().AddLsz(yhid.Text, zid);
            LB_yhid.Text = yhid.Text;
            LB_coun.Text=name.Text;
            GridView1.DataBind();
            div_ss.Visible = false;
            div_info.Visible = true;
        }
        #endregion
    }
    protected void BT_reset_Click(object sender, EventArgs e)
    {
        div_ss.Visible = true;
        div_info.Visible = false;
    }
    protected void BT_ok_Click(object sender, EventArgs e)
    {
        string PK_Class_NO=LB_Class_NO.Text;
        string yhid = LB_yhid.Text;
        string phone = TB_phone.Text.Trim();
        string qq = TB_qq.Text.Trim();
        if(new GZJW().SetCounseller(PK_Class_NO, yhid, phone, qq))
        {
            this.tsbox.Value = "<span style=\"font-size:Large;\"> <font color=green>设置成功</font></span>";
        }
        else
        {
            this.tsbox.Value = "<span style=\"font-size:Large;\"> <font color=green>设置失败，请联系管理员</font></span>";
        }
    }
}
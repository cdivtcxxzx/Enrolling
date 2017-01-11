using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_menu : System.Web.UI.Page
{
    #region 页面初始化参数

    private string pagelm1 = "";

    private string pageqx1 = "浏览";
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "menu.aspx";//页面值

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!String.IsNullOrEmpty(Request.QueryString["order"]))
            {

                string orderValue = Request.QueryString["order"];
                //获取当前菜单的值
                string sqlSerachByDhcdh1 = "SELECT gjz,dhcdh,sfcdxs FROM lanm WHERE dhcdh=@dhcdh and sfcdxs=1 order by px asc";
                DataTable dt1 = Sqlhelper.Serach(sqlSerachByDhcdh1, new SqlParameter("dhcdh", orderValue));
                if (dt1.Rows.Count > 0)
                {
                    #region 登陆验证及权限
                    try
                    {
                        pagelm1 = dt1.Rows[0]["gjz"].ToString();

                        //将标题设置为栏目+权限
                        this.Title = this.pagelm1 + "--" + pageqx1 + "-" + pageqx2 + "-" + pageqx3 + "-" + pageqx4 + "-" + pageqx5;
                        //获取登陆状态
                        if (!new c_login().Loginyanzheng())
                        {
                            //登陆验证为Flase
                            //basic.MsgBox(this.Page, "登陆已超时，请重新登陆！", "~/Default.aspx");
                            Response.Write("<script>top.location.href='/Default.aspx';</script>");
                        }


                            //第一次访问这个页面时
                            //获取模块权限

                            if (!new c_login().powerYanzheng(Session["UserName"].ToString(), pagelm1, pageqx1, "2"))
                            {
                                Response.Write("<script>alert('" + Session["Name"].ToString() + ":对不起,您无权" + pageqx1 + "\"" + this.Title.ToString() + "\"的内容！如果你确定要操作该内容,请联系您的上级部门!');history.go(-1)</script>");
                                Response.End();
                            }
                            else
                            {
                                //日志
                                new c_log().logAdd(pagelm1, pageqx1, "进入" + pagelm1 + "菜单");


                            }
                        


                    }
                    catch (Exception ex)
                    {
                        new c_log().logAdd(pagelm1, pageqx1, webpage + "在PAGE_LOAD登陆验证、权限验证时出错，" + ex.StackTrace.ToString() + ",详情：" + ex.InnerException + ex.Message);
                    }
                    #endregion
                }




                string sqlSerachByDhcdh = "SELECT * FROM lanm WHERE dhcdh=@dhcdh and sfdhxs=1 order by px asc";
                DataTable dt = Sqlhelper.Serach(sqlSerachByDhcdh, new SqlParameter("dhcdh", orderValue));
                if (dt.Rows.Count > 0)
                {
                    this.menu.InnerHtml = "<script type=\"text/javascript\"> <!-- \r d = new dTree('d');";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //判断是否有权限
                        if (new c_login().powerYanzheng(Session["UserName"].ToString(), dt.Rows[i]["gjz"].ToString(), pageqx1, "2"))
                        {

                            DataRow row = dt.Rows[i];
                            if ("-1" != row["fid"].ToString())
                            {
                                this.menu.InnerHtml += "d.add(" +
                                    row["lmid"].ToString() + "," +
                                    row["fid"].ToString() + ",'" +
                                    row["lmmc"].ToString() + "','" +
                                    row["url"].ToString() + "','','" +
                                    row["dkfs"].ToString() + "','" +
                                    row["lmtp"].ToString() + "','" +
                                    row["lmtp"].ToString() + "');";
                            }
                            else
                            {
                                this.menu.InnerHtml += "d.add(" +
                                    row["lmid"].ToString() + "," +
                                    row["fid"].ToString() + ",'" +
                                    row["lmmc"].ToString() + "');";
                            }
                        }
                    }
                    this.menu.InnerHtml += "document.write(d);//--></script>";

                }
                else
                {
                    DefaultMenu();
                }
            }
            else
            {
                DefaultMenu();
            }
        }
        catch
        {
            Response.Write("网络连接失败,请稍后重试");
        }
    }


    public void DefaultMenu()
    {
        string sqlSerachDefault = "SELECT * FROM lanm WHERE  sfdhxs=1 and  (dhcdh=(SELECT MIN(dhcdh) FROM lanm)) ORDER BY fid ";
        DataTable dtDefault = Sqlhelper.Serach(sqlSerachDefault);
        if (dtDefault.Rows.Count > 0)
        {
            this.menu.InnerHtml = "<script type=\"text/javascript\"> <!-- \r d = new dTree('d');";
            for (int i = 0; i < dtDefault.Rows.Count; i++)
            { //判断是否有权限
                if (new c_login().powerYanzheng(Session["UserName"].ToString(), dtDefault.Rows[i]["gjz"].ToString(), "浏览", "2"))
                {
                    DataRow row = dtDefault.Rows[i];
                    if ("-1" != row["fid"].ToString())
                    {
                        this.menu.InnerHtml += "d.add(" +
                            row["lmid"].ToString() + "," +
                            row["fid"].ToString() + ",'" +
                            row["lmmc"].ToString() + "','" +
                            row["url"].ToString() + "','','" +
                            row["dkfs"].ToString() + "','" +
                            row["lmtp"].ToString() + "','" +
                            row["lmtp"].ToString() + "');";
                    }
                    else
                    {
                        this.menu.InnerHtml += "d.add(" +
                            row["lmid"].ToString() + "," +
                            row["fid"].ToString() + ",'" +
                            row["lmmc"].ToString() + "');";
                    }
                }
            }
            this.menu.InnerHtml += "document.write(d);//--></script>";
        }
        else
        {
            this.menu.InnerHtml = "数据库连接失败！";
        }
    }
}
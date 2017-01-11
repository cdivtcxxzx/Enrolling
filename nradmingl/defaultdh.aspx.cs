using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_defaultdh : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取浏览器信息



//        <!--服务端-->
//Request["winHeight"] //这样可以得到客户端高
//Request["winWidth"] //这样可以得到客户端宽
        //if (Session["fbl"] != null) this.llq.InnerHtml = Session["fbl"].ToString();








        //获取登陆状态
        if (!new c_login().Loginyanzheng())
        {
            //登陆验证为Flase
            //basic.MsgBox(this.Page, "登陆已超时，请重新登陆！", "~/Default.aspx");
            Response.Write("<script>top.location.href='/Default.aspx';</script>");
        }

        //最近操作
        string fwcz = "";
        string cySqlString = "SELECT gjz,url,lmid FROM lanm WHERE lmid=@lmid";
        string lmSqlString = "SELECT distinct top 8 lm,userid,id,time FROM rizlog WHERE userid=@userid order by id desc";
        string[] zjcz8 = new string[8]{"","","","","","","",""};
       // zjcz8[0] = "";
        try
        {
            //获取栏目ID
            DataTable lmDt = Sqlhelper.Serach(lmSqlString, new SqlParameter("userid", Session["UserName"]));
            if (lmDt.Rows.Count > 0)
            {

                for (int i = 0; i < lmDt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                       // this.zhfw.InnerHtml = lmDt.Rows[i]["time"].ToString();
                    }
                    // Response.Write(lmDt.Rows[i]["lm"] + "<br>");
                    DataTable cyDt = Sqlhelper.Serach(cySqlString, new SqlParameter("lmid", lmDt.Rows[i]["lm"]));
                    if (cyDt.Rows.Count > 0)
                    {

                        if (zjcz8[0] != cyDt.Rows[0]["gjz"].ToString() && zjcz8[1] != cyDt.Rows[0]["gjz"].ToString() && zjcz8[2] != cyDt.Rows[0]["gjz"].ToString() && zjcz8[3] != cyDt.Rows[0]["gjz"].ToString() && zjcz8[4] != cyDt.Rows[0]["gjz"].ToString() && zjcz8[5] != cyDt.Rows[0]["gjz"].ToString() && zjcz8[6] != cyDt.Rows[0]["gjz"].ToString() && zjcz8[7] != cyDt.Rows[0]["gjz"].ToString())
                        {
                            //Response.Write(cyDt.Rows[0]["gjz"] + "<br>");
                            this.zjcz.InnerHtml = this.zjcz.InnerHtml + "<a href=\"" + cyDt.Rows[0]["url"].ToString() + "\">" + cyDt.Rows[0]["gjz"].ToString() + "</a>&nbsp;&nbsp;&nbsp;&nbsp;";

                        }
                        zjcz8[i] = cyDt.Rows[0]["gjz"].ToString();
                        //fwcz = cyDt.Rows[0]["gjz"].ToString();
                    }

                }

            }
        }
        catch(Exception ex)
        {
            this.zjcz.InnerHtml = "未获取到最近的操作记录!";
        }

        //最近访问
        string[] zjfw8 = new string[10] { "", "", "", "", "", "", "", "","","" };
        string fwxm = "";
        string yhSqlString = "SELECT TOP (10) yonghqx.xm FROM quanxdm INNER JOIN rizlog ON quanxdm.qxid = rizlog.lx INNER JOIN lanm ON rizlog.lm = lanm.lmid INNER JOIN yonghqx ON rizlog.userid = yonghqx.yhid WHERE (lanm.gjz = '用户管理') AND (quanxdm.qxmc = '登陆') ORDER BY rizlog.id DESC";

        try
        {

            DataTable yhDt = Sqlhelper.Serach(yhSqlString);

            if (yhDt.Rows.Count > 0)
            {
                zjfw8[0] ="";
                //this.zjdl.InnerHtml = fwxm;

                for (int i = 0; i < yhDt.Rows.Count; i++)
                {
                    // Response.Write(lmDt.Rows[i]["lm"] + "<br>");


                        //Response.Write(cyDt.Rows[0]["gjz"] + "<br>");

                    if (zjfw8[0] != yhDt.Rows[i][0].ToString() && zjfw8[1] != yhDt.Rows[i][0].ToString() && zjfw8[2] != yhDt.Rows[i][0].ToString() && zjfw8[3] != yhDt.Rows[i][0].ToString() && zjfw8[4] != yhDt.Rows[i][0].ToString() && zjfw8[5] != yhDt.Rows[i][0].ToString() && zjfw8[6] != yhDt.Rows[i][0].ToString() && zjfw8[7] != yhDt.Rows[i][0].ToString() && zjfw8[8] != yhDt.Rows[i][0].ToString() && zjfw8[9] != yhDt.Rows[i][0].ToString())
                        {
                            //Response.Write(cyDt.Rows[0]["gjz"] + "<br>");
                           // this.zjdl.InnerHtml = this.zjdl.InnerHtml + "&nbsp;&nbsp;" + yhDt.Rows[i][0].ToString();
                        }
                        zjfw8[i] = yhDt.Rows[i][0].ToString();
                       


                }

            }
        }
        catch(Exception ex)
        {
           // this.zjdl.InnerHtml = "未获取到最近的访问记录!"+ex.Message;
        }
           
        this.quanxfz.InnerHtml="<a href=\"showPower.aspx?yhid="+Session["UserName"].ToString()+"\">查看我的权限和分组详情</a>";
           
    }
    protected string gettz(string gjz)
    {
       
            DataTable xy = Sqlhelper.Serach("select xxnr from wangzxx where xxgjz='" + gjz + "'");

            if (xy.Rows.Count > 0)
            {
                return xy.Rows[0]["xxnr"].ToString();
            }
            else
            {

                return "";
            }
       
    }
}
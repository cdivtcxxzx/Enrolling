using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using Newtonsoft.Json;


public partial class admin_apptjgz : System.Web.UI.Page
{
    public string width1 = "1%";
    public string width2 = "99%";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //统计班主任访问该页的次数

        }

        string agent = (Request.UserAgent + "").ToLower().Trim();
        string mob = "0";

        if (agent == "" ||
            agent.IndexOf("mobile") != -1 ||
            agent.IndexOf("mobi") != -1 ||
            agent.IndexOf("nokia") != -1 ||
            agent.IndexOf("samsung") != -1 ||
            agent.IndexOf("sonyericsson") != -1 ||
            agent.IndexOf("mot") != -1 ||
            agent.IndexOf("blackberry") != -1 ||
            agent.IndexOf("lg") != -1 ||
            agent.IndexOf("htc") != -1 ||
            agent.IndexOf("j2me") != -1 ||
            agent.IndexOf("ucweb") != -1 ||
            agent.IndexOf("opera mini") != -1 ||
            agent.IndexOf("mobi") != -1)
        {
            //终端可能是手机
            mob = "1";

           
        }
        string nd = basic.dqnd();
        //实时更新
        string sql1 = "select yxmc,yxdm from dm_yuanxi where zt=1 and yxdm<>'0115'";
        string lb = "高职";
        if (Request["id"] != null)
        {
            if (Request["id"].ToString() == "1")
            {//中职
                lb = "中职";
                title1.InnerHtml = "<b>"+nd+"年招生信息统计【中职】</b>";
                int s1 = 0;
                int s2 = 0;
                string zzzsrs1 = "";
                string zzbdrs1 = "";
                string zzbdl1 = "";
                DataTable zzzsrs = Sqlhelper.Serach("SELECT  sum([招生计划])  FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系代码='0115' ");
                // DataTable zzydrs = Sqlhelper.Serach("SELECT COUNT(id)  FROM [XSJCSJ] where lb='中职'  and (XSZT>=2) and NJ='"+basic.dqnd()+"' and zgz='无'   ");
                DataTable zzbdrs = Sqlhelper.Serach("SELECT COUNT(id)  FROM [XSJCSJ] where lb='中职'  and (XSZT>=2) and NJ='"+basic.dqnd()+"' and zgz='无'  ");
                if (zzzsrs.Rows.Count > 0) zzzsrs1 = zzzsrs.Rows[0][0].ToString() + "人"; s1 = Convert.ToInt32(zzzsrs.Rows[0][0].ToString());
                if (zzbdrs.Rows.Count > 0) zzbdrs1 = zzbdrs.Rows[0][0].ToString() + "人"; s2 = Convert.ToInt32(zzbdrs.Rows[0][0].ToString());
                string result = "0.0%";
                if (s1 != 0)
                {
                    double percent = Convert.ToDouble(s2) / Convert.ToDouble(s1);
                    //string result = percent.ToString("0%");//得到6%
                    double percent2 = 1 - percent;
                    result = percent.ToString("0.00%");//得到5.882%
                    width1 = percent.ToString("0.00%");
                    width2 = percent2.ToString("0.00%");
                    zzbdl1 = result;
                }
                //头部
                this.zs.InnerHtml = "<div class=\"progress-group\"><span class=\"progress-text\">总计（招生数/计划数）</span><span class=\"progress-number\"><b>" + s2 + "</b>/" + s1 + "&nbsp;&nbsp;&nbsp;&nbsp;<font color=green><b>" + result + "</b></font></span>";
                this.zs.InnerHtml += "<div class=\"progress sm\"><div class=\"progress-bar progress-bar-yellow progress-bar-striped\" style=\"width: " + result + "\"></div></div>";
                sql1 = "select 专业名称 yxmc,专业代码 yxdm from [zsgl_zsjh] where 院系代码='0115' and 年度='" + nd + "'";

            }
            else
            {//高职
			lb = "高职";
                title1.InnerHtml = "<b>" + nd + "年招生信息统计【高职】</b>";
  int s1 = 0;
                int s2 = 0;
                string zzzsrs1 = "0";
                string zzbdrs1 = "0";
                string zzbdl1 = "0";
                DataTable zzzsrs = Sqlhelper.Serach("SELECT  sum([招生计划])  FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系代码<>'0115' ");
                // DataTable zzydrs = Sqlhelper.Serach("SELECT COUNT(id)  FROM [XSJCSJ] where lb='中职'  and (XSZT>=2) and NJ='"+basic.dqnd()+"' and zgz='无'   ");
                DataTable zzbdrs = Sqlhelper.Serach("SELECT COUNT(id)  FROM [XSJCSJ] where lb='高职'  and (XSZT>=2) and NJ='"+basic.dqnd()+"' and zgz='无'  ");
                try
                {
                    if (zzzsrs.Rows.Count > 0) zzzsrs1 = zzzsrs.Rows[0][0].ToString() + "人"; s1 = Convert.ToInt32(zzzsrs.Rows[0][0].ToString());
                    if (zzbdrs.Rows.Count > 0) zzbdrs1 = zzbdrs.Rows[0][0].ToString() + "人"; s2 = Convert.ToInt32(zzbdrs.Rows[0][0].ToString());
                }
                catch { }
                string result = "0.0%";
                if (s1 != 0)
                {
                    double percent = Convert.ToDouble(s2) / Convert.ToDouble(s1);
                    //string result = percent.ToString("0%");//得到6%
                    double percent2 = 1 - percent;
                    result = percent.ToString("0.00%");//得到5.882%
                    width1 = percent.ToString("0.00%");
                    width2 = percent2.ToString("0.00%");
                    zzbdl1 = result;
                }
                //头部
                this.zs.InnerHtml = "<div class=\"progress-group\"><span class=\"progress-text\">总计</span><span class=\"progress-number\"><b>" + s2 + "</b>/" + s1 + "&nbsp;&nbsp;&nbsp;&nbsp;<font color=green><b>" + result + "</b></font></span>";
                this.zs.InnerHtml += "<div class=\"progress sm\"><div class=\"progress-bar progress-bar-yellow progress-bar-striped\" style=\"width: " + result + "\"></div></div>";
				
                sql1 = "select  distinct 院系名称 yxmc,院系代码 yxdm from [zsgl_zsjh] where 院系代码<>'0115' and 年度='" + nd + "' order by 院系名称,院系代码";


            }

        }
       
       string ts = "";
        DataTable yxdm = Sqlhelper.Serach(sql1);
       // DataTable zydm = Sqlhelper.Serach("select 专业名称 yxmc,专业代码 yxdm from [zsgl_zsjh] where yxdm='0115' and 年度='"+nd+"'");
        this.yxshow.InnerHtml = "";
        if (yxdm.Rows.Count > 0)
        {
           
            //显示各系班级的报到情况
            string tjtime = DateTime.Now.ToString();
            //DataTable bdqkq = Sqlhelper.xjptSerach("select * from xsgl_bdtj where 系或班级='全校'  ");
            

            for (int i = 0; i < yxdm.Rows.Count; i++)
            {
                
                string y1 = "progress-bar progress-bar-aqua progress-bar-striped";
                string y2 = "progress-bar progress-bar-green progress-bar-striped";
                string y3 = "progress-bar progress-bar-red progress-bar-striped";
                string y4 = "progress-bar progress-bar-light-blue progress-bar-striped";
                string y5 = "progress-bar progress-bar-red2 progress-bar-striped";
                string y6 = "progress-bar progress-bar-red3 progress-bar-striped";
                string ys = y6;
                if (i == 1)ys = y1 ;
                if (i == 2) ys = y2;
                if (i == 3) ys = y3;
                if (i == 4) ys = y4;
                if (i == 5) ys = y5;
                if (i == 6) ys = y6;
                if (i == 7) ys = y1;
                if (i == 8) ys = y2;
                if (i == 9) ys = y3;
                if (i == 10) ys = y4;
                if (i == 11) ys = y5;
                if (i == 12) ys = y6;
                //
                string yd = "0";
                string sd = "0";
                string bdl = "0.00%";
                
                //string y7 = "";
                string sql3 = "SELECT  sum([招生计划])  FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系代码='"+ yxdm.Rows[i][1].ToString() +"' ";
				//Response.Write(sql2);
                string sql2 = "select count(id) from xsjcsj where lb='" + lb + "' and yxdm='" + yxdm.Rows[i][1].ToString() + "'  and (XSZT>=2) and NJ='"+nd+"'  ";
				
                if (lb == "中职")
                {
                    sql2 = "select count(id) from xsjcsj where lb='" + lb + "' and zydm='" + yxdm.Rows[i][1].ToString() + "'  and (XSZT>=2) and NJ='" + nd + "'  ";
                    sql3 = "SELECT  sum([招生计划])  FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系代码='0115' and 专业代码='" + yxdm.Rows[i][1].ToString() + "' ";
                }
                


                DataTable zsrs = Sqlhelper.Serach(sql3);
                DataTable bdrs = Sqlhelper.Serach(sql2);
				if (zsrs.Rows.Count > 0)
				{
             // Response.Write(sql3);
			  }
			  if (bdrs.Rows.Count > 0)
				{
              
			  }
			  else
			  {
			  //Response.Write(sql2);
			  }
                if (zsrs.Rows.Count > 0&&bdrs.Rows.Count>0)
                {
				//Response.Write("you shu ju");
                    yd = zsrs.Rows[0][0].ToString();
                    sd = bdrs.Rows[0][0].ToString();
                     if (yd != "0")
            {
                double percent = Convert.ToDouble(sd) / Convert.ToDouble(yd);
                //string result = percent.ToString("0%");//得到6%
                double percent2 = 1 - percent;
                bdl = percent.ToString("0.00%");//得到5.882%


                // Response.Write((s2 / s1).ToString());
            }
                     //bdl = result;

                   // tjtime = bdqk.Rows[0][4].ToString();
                }

                    //Response.Write(yxdm.Rows[i][1].ToString());
					 if (lb == "中职")
                {
				 this.yxshow.InnerHtml += "<div   class=\"progress-group\"><span class=\"progress-text\"><a href='#'>" + yxdm.Rows[i][0].ToString() + "</a></span><span class=\"progress-number\"><b>" + sd + "</b>/" + yd + "&nbsp;&nbsp;&nbsp;&nbsp;<font color=green><b>" + bdl + "</b></font></span>";
				}
				else
				{
                    this.yxshow.InnerHtml += "<div   class=\"progress-group\"><span class=\"progress-text\"><a href='apptjyx.aspx?yxdm=" + yxdm.Rows[i][1].ToString() + "'>" + yxdm.Rows[i][0].ToString() + "</a></span><span class=\"progress-number\"><b>" + sd + "</b>/" + yd + "&nbsp;&nbsp;&nbsp;&nbsp;<font color=green><b>" + bdl + "</b></font></span>";
					}
                    //Response.Write("apptjyxgz.aspx?yxdm=" + yxdm.Rows[i][1].ToString() + "'");
                    this.yxshow.InnerHtml += "<div class=\"progress sm\"><div class=\""+ys+"\" style=\"width: " + bdl + "\"></div></div>";
                
                


            }
           
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
      //  new Statistics().Totalupdate("2015", "1");
      //  Response.Redirect("apptj.aspx");
    }
}
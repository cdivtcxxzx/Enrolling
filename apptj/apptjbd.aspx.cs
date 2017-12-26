using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using Newtonsoft.Json;


public partial class apptj_apptjbd : System.Web.UI.Page
{
    public string width1 = "1%";
    public string width2 = "99%";
    /// <summary>
    /// 执行查询
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <param name="parameters">查询参数</param>
    /// <returns></returns>
    public static DataTable Serachtj(string sql, params SqlParameter[] parameters)
    {
        //建立连接
        using (SqlConnection conn = new SqlConnection("Data Source=10.35.10.83;Initial Catalog=TJ;User ID=data_tj;Password=tj@cdivtc;"))
        {

            //打开连接
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataSet st = new DataSet();


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(st);
                    return st.Tables[0];
                }
            }
            catch (Exception e)
            {
                return new DataTable();
            }
        }
    }
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
        //高职
        lb = "高职";
        //title1.InnerHtml = "<b>" + nd + "年迎新数据统计【高职】</b>";
        int s1 = 0;
        int s2 = 0;
        string zzzsrs1 = "0";
        string zzbdrs1 = "0";
        string zzbdl1 = "0";
        string gzbdl = "0.00%";
        string result = "0.00%";
        DataTable yxtj = Serachtj("SELECT plan_num 招生计划,lq_num 录取人数,zc_num 网上注册,ol_num 网上缴费,bd_num 现场报到,up_dt 更新时间 FROM (select sum(num) plan_num from Fresh_Plan) a left join (select count(*) lq_num from Fresh_STU where nd='2017' and len(zydm)=6) b on 1=1 left join (select count(*) zc_num from Fresh_STU where nd='2017' and zc_zt='1' and len(zydm)=6) c on 1=1 left join (select count(*) ol_num from Fresh_STU where nd='2017' and ol_zt='1' and len(zydm)=6) d on 1=1 left join (select count(*) bd_num from Fresh_STU where nd='2017' and bd_zt='已报到' and len(zydm)=6) e on 1=1 left join (select top 1 up_dt from event_log where event='fresh_tj') f on 1=1");

        if (yxtj.Rows.Count > 0)
        {
            //迎新统计

            zzzsrs1 = yxtj.Rows[0]["录取人数"].ToString();
            zzbdrs1 = yxtj.Rows[0]["现场报到"].ToString();

            s1 = Convert.ToInt32(zzzsrs1);
            s2 = Convert.ToInt32(zzbdrs1);
            if (s1 != 0)
            {
                double percent = Convert.ToDouble(s2) / Convert.ToDouble(s1);
                //string result = percent.ToString("0%");//得到6%
                double percent2 = 1 - percent;
                result = percent.ToString("0.00%");//得到5.882%
                width1 = percent.ToString("0.00%");
                width2 = percent2.ToString("0.00%");
                gzbdl = result;
            }


        }






        //头部
        this.zs.InnerHtml = "<div class=\"progress-group\"><span class=\"progress-text\">总计</span><span class=\"progress-number\">报到:<b>" + s2 + "</b>/录取:" + s1 + "&nbsp;&nbsp;&nbsp;&nbsp;<font color=green><b>" + result + "</b></font></span>";
        this.zs.InnerHtml += "<div class=\"progress sm\"><div class=\"progress-bar progress-bar-yellow progress-bar-striped\" style=\"width: " + result + "\"></div></div>";





        //各系的显示

        string ts = "";
        DataTable yxdm = Serachtj("SELECT a.yxmc 学院名称,isnull(lq_num,0) 录取人数,isnull(ol_num,0) 来校报到,a.yxdm FROM (SELECT     yxmc, SUM(num) AS plan_num, yxdm FROM         Fresh_Plan GROUP BY yxmc, yxdm) a left join (SELECT     yxmc, COUNT(*) AS lq_num, yxdm FROM         Fresh_STU WHERE     (nd = '2017') AND (LEN(zydm) = 6) GROUP BY yxmc, yxdm) b on a.yxmc=b.yxmc left join (select yxmc,count(*) zc_num,yxdm from Fresh_STU where nd='2017' and zc_zt='1' and len(zydm)=6 group by yxmc,yxdm) c on a.yxmc=c.yxmc left join (select yxmc,count(*) ol_num from Fresh_STU where nd='2017' and bd_zt='已报到' and len(zydm)=6 group by yxmc) d on a.yxmc=d.yxmc ");
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
                if (i == 1) ys = y1;
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


                //Response.Write("you shu ju");
                yd = yxdm.Rows[i][1].ToString();
                sd = yxdm.Rows[i][2].ToString();
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


                //Response.Write(yxdm.Rows[i][1].ToString());
                if (lb == "中职")
                {
                    this.yxshow.InnerHtml += "<div   class=\"progress-group\"><span class=\"progress-text\"><a href=''>" + yxdm.Rows[i][0].ToString() + "</a></span><span class=\"progress-number\"><b>" + sd + "</b>/" + yd + "&nbsp;&nbsp;&nbsp;&nbsp;<font color=green><b>" + bdl + "</b></font></span>";
                }
                else
                {
                    this.yxshow.InnerHtml += "<div   class=\"progress-group\"><span class=\"progress-text\"><a href='/nradmingl/tj_yx.aspx?id="+yxdm.Rows[i]["yxdm"].ToString()+ "'>" + yxdm.Rows[i][0].ToString() + "</a></span><span class=\"progress-number\"><b>" + sd + "</b>/" + yd + "&nbsp;&nbsp;&nbsp;&nbsp;<font color=green><b>" + bdl + "</b></font></span>";
                }
                //Response.Write("apptjyxgz.aspx?yxdm=" + yxdm.Rows[i][1].ToString() + "'");
                this.yxshow.InnerHtml += "<div class=\"progress sm\"><div class=\"" + ys + "\" style=\"width: " + bdl + "\"></div></div>";




            }

        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //  new Statistics().Totalupdate("2015", "1");
        //  Response.Redirect("apptj.aspx");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class admin_appbaodao2 : System.Web.UI.Page
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

        string yx_zsjh = "0";
        string yx_zslq = "0";
        string yx_wszc = "0";
        string yx_wsjf = "0";
        string yx_xcbd = "0";
        string yxtjtime = "0";
        string gzbdl = "【2017年8月17日0时开始】";
        string gzbdl1 = "【2017年8月17日0时开始】";
        string gzbdl2 = "【2017年9月1日12时开始】";
        DataTable yxtj = Serachtj("SELECT plan_num 招生计划,lq_num 录取人数,zc_num 网上注册,ol_num 网上缴费,bd_num 现场报到,up_dt 更新时间 FROM (select sum(num) plan_num from Fresh_Plan) a left join (select count(*) lq_num from Fresh_STU where nd='2017' and len(zydm)=6) b on 1=1 left join (select count(*) zc_num from Fresh_STU where nd='2017' and zc_zt='1' and len(zydm)=6) c on 1=1 left join (select count(*) ol_num from Fresh_STU where nd='2017' and ol_zt='1' and len(zydm)=6) d on 1=1 left join (select count(*) bd_num from Fresh_STU where nd='2017' and bd_zt='已报到' and len(zydm)=6) e on 1=1 left join (select top 1 up_dt from event_log where event='fresh_tj') f on 1=1");
        DataTable tj = Serachtj("select base_stu.yxmc 学院名称,isnull(base_stu.学生数,0) 录取学生数,isnull(all_stu.[缴费],0) 已缴学费,isnull(ol_stu.[网上缴费],0) 网上缴费,isnull(off_stu.缴费-dz_stu.[对账],0) 现场缴费,isnull(zxdk.[助学贷款],0) 申请助学贷款 from  (select yxmc,isnull(count(*),0) 学生数 from fresh_stu where len(zydm)=6 group by yxmc) base_stu left join (select yxmc,count(*) 缴费 from fresh_stu stu left join (select sum(je) je,xh from (select isnull(SUM(sfje),0)-isnull(SUM(tfje),0) je,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and sfxmdm='01' group by xh union select SUM(sfje) je,xh FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is null and sfnd=2017 and zfzt=2 and sfxmdm='01' group by xh) t where je>0  and len(xh)>12 group by xh ) ol_all on stu.xh=ol_all.xh where ol_all.xh is not null group by stu.yxmc) all_stu on base_stu.yxmc=all_stu.yxmc left join (select yxmc,isnull(count(*),0) 网上缴费 from fresh_stu stu left join (select SUM(sfje) je,xh FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfnd=2017 and zfzt=2 and sfxmdm='01' group by xh) ol_all on stu.xh=ol_all.xh where ol_all.xh is not null group by stu.yxmc) ol_stu on base_stu.yxmc=ol_stu.yxmc left join (select yxmc,isnull(count(*),0) 缴费 from fresh_stu stu left join (select xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and sfxmdm='01') off_all on stu.xh=off_all.xh where off_all.xh is not null group by stu.yxmc) off_stu on base_stu.yxmc=off_stu.yxmc left join (select yxmc,isnull(count(*),0) 对账 from fresh_stu stu left join (SELECT SUM(sfje) je,xh FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is not null and sfnd=2017 and sfxmdm='01' group by xh) dz_all on stu.xh=dz_all.xh where dz_all.xh is not null group by stu.yxmc) dz_stu on base_stu.yxmc=dz_stu.yxmc left join (select yxmc,isnull(count(*),0) 助学贷款 from fresh_stu stu left join (select pay_list.sno xh from FreshMan.dbo.Pay_List left join FreshMan.dbo.Pay_List_Detail on pay_list.pk_pay_list=pay_list_detail.fk_pay_list left join FreshMan.dbo.Fee_Item on pay_list_detail.fk_fee_item=fee_item.pk_fee_item where pay_list.fk_fee=1 and fee_item.fee_code='01' and pay_list_detail.is_in_olorder=0) zxdk_all on stu.xh=zxdk_all.xh where zxdk_all.xh is not null group by stu.yxmc) zxdk on base_stu.yxmc=zxdk.yxmc");

      
   

       string xcjf = Convert.ToString(tj.Compute("Sum(现场缴费)", "")); ;
      string zxdk = Convert.ToString(tj.Compute("Sum(申请助学贷款)", ""));

        if (yxtj.Rows.Count > 0)
        {
            //迎新统计
           yx_zsjh = yxtj.Rows[0]["招生计划"].ToString();
            yx_zslq = yxtj.Rows[0]["录取人数"].ToString();
            yx_wszc = yxtj.Rows[0]["网上注册"].ToString();
            yx_wsjf = (Convert.ToInt32(yxtj.Rows[0]["网上缴费"].ToString())+Convert.ToInt32(xcjf)).ToString();
            yx_xcbd = yxtj.Rows[0]["现场报到"].ToString();
            yxtjtime = yxtj.Rows[0]["更新时间"].ToString();
            int s1 = Convert.ToInt32(yx_zslq);
           int s2 = Convert.ToInt32(yx_wszc);//网上注册
           int s3 = Convert.ToInt32(yx_wsjf);//网上缴费
           int s4 = Convert.ToInt32(yx_xcbd);//来校报到
            if (s1 != 0)
            {
                double percent = Convert.ToDouble(s2) / Convert.ToDouble(s1);
                //string result = percent.ToString("0%");//得到6%
                double percent2 = 1 - percent;
                string result = percent.ToString("0.00%");//得到5.882%
                width1 = percent.ToString("0.00%");
                width2 = percent2.ToString("0.00%");
                gzbdl = result;
            }
            if (s1 != 0)
            {
                double percent = Convert.ToDouble(s3) / Convert.ToDouble(s1);
                //string result = percent.ToString("0%");//得到6%
                double percent2 = 1 - percent;
                string result = percent.ToString("0.00%");//得到5.882%
                width1 = percent.ToString("0.00%");
                width2 = percent2.ToString("0.00%");
                gzbdl1 = result;
            }
            if (s1 != 0)
            {
                double percent = Convert.ToDouble(s4) / Convert.ToDouble(s1);
                //string result = percent.ToString("0%");//得到6%
                double percent2 = 1 - percent;
                string result = percent.ToString("0.00%");//得到5.882%
                width1 = percent.ToString("0.00%");
                width2 = percent2.ToString("0.00%");
                gzbdl2 = result;
            }

            if (gzbdl == "0.00%")
            {
                gzbdl = "【2017年8月17日0时开始】";
            }
            else
            {
                gzbdl = "" + gzbdl;
            }
            if (gzbdl1 == "0.00%")
            {
                gzbdl1 = "【2017年8月17日0时开始】";
            }
            else
            {
                gzbdl1 = "" + gzbdl1;
            }
            if (gzbdl2 == "0.00%")
            {
                gzbdl2 = "【2017年8月17日0时开始】";
            }
            else
            {
                gzbdl2 = "" + gzbdl2;
            }

            this.gzshow.InnerHtml = "<SPAN class=progress-text  style=\"font-size:18px;\">&nbsp;&nbsp;完成进度：" + gzbdl + "</SPAN>  <DIV class=\"progress sm active\"><DIV style=\"WIDTH: " + gzbdl + "\" class=\"progress-bar progress-bar-light-blue progress-bar-striped\"></DIV></DIV><SPAN class=progress-number><B>网上注册人数：" + yx_wszc + "</B> / 招生录取人数：" + yx_zslq + "</SPAN>";
            this.jf.InnerHtml = "<SPAN class=progress-text  style=\"font-size:18px;\">&nbsp;&nbsp;完成进度：" + gzbdl1 + "</SPAN>  <DIV class=\"progress sm active\"><DIV style=\"WIDTH: " + gzbdl1 + "\" class=\"progress-bar progress-bar-green progress-bar-striped\"></DIV></DIV><SPAN class=progress-number><B>已缴费学生数：" + yx_wsjf + "</B> / 申请助学贷款：" + zxdk + "</SPAN>";
            this.bd.InnerHtml = "<SPAN class=progress-text  style=\"font-size:18px;\">&nbsp;&nbsp;完成进度：" + gzbdl2 + "</SPAN>  <DIV class=\"progress sm active\"><DIV style=\"WIDTH: " + gzbdl2 + "\" class=\"progress-bar progress-bar-red2 progress-bar-striped\"></DIV></DIV><SPAN class=progress-number><B>来校报到人数：" + yx_xcbd + "</B> / 招生录取人数：" + yx_zslq + "</SPAN>";

        }


       // int s1=0;
       // int s2=0;
       // string zzzsrs1="";
       // string zzbdrs1="";
       // string zzbdl1="";
       // DataTable zzzsrs = Sqlhelper.Serach("SELECT  sum([招生计划])  FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系代码='0115' ");
       //// DataTable zzydrs = Sqlhelper.Serach("SELECT COUNT(id)  FROM [XSJCSJ] where lb='中职'  and (XSZT>=2) and NJ='"+basic.dqnd()+"' and zgz='无'   ");
       // DataTable zzbdrs = Sqlhelper.Serach("SELECT COUNT(id)  FROM [XSJCSJ] where lb='中职'  and (XSZT>=2) and NJ='"+basic.dqnd()+"' and zgz='无'  ");
       // if (zzzsrs.Rows.Count > 0)zzzsrs1 = zzzsrs.Rows[0][0].ToString() + "人";s1= Convert.ToInt32( zzzsrs.Rows[0][0].ToString());
       // if (zzbdrs.Rows.Count > 0)zzbdrs1 = zzbdrs.Rows[0][0].ToString() + "人";s2= Convert.ToInt32( zzbdrs.Rows[0][0].ToString());
       //if(s1!=0)
       //{
       //    double percent = Convert.ToDouble(s2) / Convert.ToDouble(s1);
       //    //string result = percent.ToString("0%");//得到6%
       //    double percent2 = 1 - percent;
       //    string result = percent.ToString("0.00%");//得到5.882%
       //    width1 = percent.ToString("0.00%");
       //    width2 = percent2.ToString("0.00%");
       //   zzbdl1= result;
       // }
       // //<SPAN class=progress-text  style="font-size:20px;">报到率 91.66%</SPAN>  <br /><br /><DIV class="progress sm active"><DIV style="WIDTH: 91.66%" class="progress-bar progress-bar-aqua progress-bar-striped"></DIV></DIV><SPAN class=progress-number><B>报到人数：2572</B> / 招生人数：2806</SPAN>

       // this.zzshow.InnerHtml="<SPAN class=progress-text  style=\"font-size:20px;\">招生进度:"+zzbdl1+"</SPAN>  <br /><br /><DIV class=\"progress sm active\"><DIV style=\"WIDTH: "+zzbdl1+"\" class=\"progress-bar progress-bar-aqua progress-bar-striped\"></DIV></DIV><SPAN class=progress-number><B>招生人数："+zzbdrs1+"</B> / 计划人数："+zzzsrs1+"</SPAN>";

       //int s3 = 0;
       //int s4 = 0;
       // string gzzsrs1="0";
       // string gzbdrs1="0";
       // string gzbdl1="未开始";
       // DataTable gzzsrs = Sqlhelper.Serach("SELECT  sum([招生计划])  FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系代码<>'0115'");
       // //DataTable gzydrs = Sqlhelper.Serach("SELECT COUNT(id)  FROM [XSJCSJ] where lb='高职'  and (XSZT>=2) and NJ='"+basic.dqnd()+"'   ");
       // DataTable gzbdrs = Sqlhelper.Serach("SELECT COUNT(id)  FROM [XSJCSJ] where lb='高职'  and (XSZT>=2) and NJ='"+basic.dqnd()+"'   ");
       // if (gzzsrs.Rows.Count > 0)
       // {
       //     try
       //     {
       //         gzzsrs1 = gzzsrs.Rows[0][0].ToString() + "人"; s3 = Convert.ToInt32(gzzsrs.Rows[0][0].ToString());
       //     }
       //     catch { }
       // }
       // if (gzbdrs.Rows.Count > 0)
       // {
       //     try
       //     {
       //         gzbdrs1 = gzbdrs.Rows[0][0].ToString() + "人"; s4 = Convert.ToInt32(gzbdrs.Rows[0][0].ToString());
       //     }
       //     catch { }
       // }
       //if (s3 != 0)
       //{
       //    double percent3 = Convert.ToDouble(s4) / Convert.ToDouble(s3);
       //    //string result = percent.ToString("0%");//得到6%
       //    double percent4 = 1 - percent3;
       //    string result2 = percent3.ToString("0.00%");//得到5.882%
       //    //width1 = percent3.ToString("0.00%");
       //   // width2 = percent4.ToString("0.00%");
       //    gzbdl1 = result2;


       //    //this.Label4.Text = (s4 / s3 * 100).ToString("0.00") + "%";
       //}
        //DataTable zstj = zrSerach("SELECT [ID]  FROM [XSJCSJ] where lb='中职' and NJ='2017' and XSZT>=2");
        //if(zstj.Rows.Count>0)
        //{
        //    this.zzshow.InnerHtml = "<SPAN class=progress-text  style=\"font-size:20px;\">完成进度:未开始</SPAN>  <br /><br /><DIV class=\"progress sm active\"><DIV style=\"WIDTH:0%\" class=\"progress-bar progress-bar-yellow progress-bar-striped\"></DIV></DIV><SPAN class=progress-number><B>报到人数：0</B> / 招生人数：" + zstj.Rows.Count.ToString() + "</SPAN>";

        //}
       



    }
    public static string uumsql = "Data Source=10.35.10.83;Initial Catalog=xssjpt; User ID=data_zsgl;Password=1a2b3c4d5e";
    public static System.Data.DataTable zrSerach(string sql, params System.Data.SqlClient.SqlParameter[] parameters)
    {
        //建立连接
        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(uumsql))
        {
            //打开连接
            try
            {
                conn.Open();
                using (System.Data.SqlClient.SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (System.Data.SqlClient.SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    System.Data.DataSet st = new System.Data.DataSet();
                    System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    adapter.Fill(st);
                    return st.Tables[0];
                }

            }
            catch (Exception e)
            {
                return new System.Data.DataTable();
            }
        }
    }
    }

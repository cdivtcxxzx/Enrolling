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
    protected void Page_Load(object sender, EventArgs e)
    {
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
        DataTable zstj = zrSerach("SELECT [ID]  FROM [XSJCSJ] where lb='中职' and NJ='2017' and XSZT>=2");
        if(zstj.Rows.Count>0)
        {
            this.zzshow.InnerHtml = "<SPAN class=progress-text  style=\"font-size:20px;\">完成进度:未开始</SPAN>  <br /><br /><DIV class=\"progress sm active\"><DIV style=\"WIDTH:0%\" class=\"progress-bar progress-bar-yellow progress-bar-striped\"></DIV></DIV><SPAN class=progress-number><B>报到人数：0</B> / 招生人数：" + zstj.Rows.Count.ToString() + "</SPAN>";

        }
       



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

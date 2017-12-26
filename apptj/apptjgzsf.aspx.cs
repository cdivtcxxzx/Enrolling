using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class apptj_apptjgzsf : System.Web.UI.Page
{
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

    public string l1 = "0";
    public string l2 = "0";
    public string l3 = "0";
    public string l4 = "0";
    public string l5 = "0";
    public string l6 = "0";
    public string l7 = "0";
    public string b1 = "0%";
    public string b2 = "0%";
    public string b3 = "0%";
    public string d1 = "";
    public string d2= "";
    public string d3="";
    public string jfzs = "";

    protected void Page_Load(object sender, EventArgs e)
    {


        DataTable tj = Serachtj("select base_stu.yxmc 学院名称,isnull(base_stu.学生数,0) 录取学生数,isnull(all_stu.[缴费],0) 已缴学费,isnull(ol_stu.[网上缴费],0) 网上缴费,isnull(off_stu.缴费-dz_stu.[对账],0) 现场缴费,isnull(zxdk.[助学贷款],0) 申请助学贷款 from  (select yxmc,isnull(count(*),0) 学生数 from fresh_stu where len(zydm)=6 group by yxmc) base_stu left join (select yxmc,count(*) 缴费 from fresh_stu stu left join (select sum(je) je,xh from (select isnull(SUM(sfje),0)-isnull(SUM(tfje),0) je,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and sfxmdm='01' group by xh union select SUM(sfje) je,xh FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is null and sfnd=2017 and zfzt=2 and sfxmdm='01' group by xh) t where je>0  and len(xh)>12 group by xh ) ol_all on stu.xh=ol_all.xh where ol_all.xh is not null group by stu.yxmc) all_stu on base_stu.yxmc=all_stu.yxmc left join (select yxmc,isnull(count(*),0) 网上缴费 from fresh_stu stu left join (select SUM(sfje) je,xh FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfnd=2017 and zfzt=2 and sfxmdm='01' group by xh) ol_all on stu.xh=ol_all.xh where ol_all.xh is not null group by stu.yxmc) ol_stu on base_stu.yxmc=ol_stu.yxmc left join (select yxmc,isnull(count(*),0) 缴费 from fresh_stu stu left join (select xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and sfxmdm='01') off_all on stu.xh=off_all.xh where off_all.xh is not null group by stu.yxmc) off_stu on base_stu.yxmc=off_stu.yxmc left join (select yxmc,isnull(count(*),0) 对账 from fresh_stu stu left join (SELECT SUM(sfje) je,xh FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is not null and sfnd=2017 and sfxmdm='01' group by xh) dz_all on stu.xh=dz_all.xh where dz_all.xh is not null group by stu.yxmc) dz_stu on base_stu.yxmc=dz_stu.yxmc left join (select yxmc,isnull(count(*),0) 助学贷款 from fresh_stu stu left join (select pay_list.sno xh from FreshMan.dbo.Pay_List left join FreshMan.dbo.Pay_List_Detail on pay_list.pk_pay_list=pay_list_detail.fk_pay_list left join FreshMan.dbo.Fee_Item on pay_list_detail.fk_fee_item=fee_item.pk_fee_item where pay_list.fk_fee=1 and fee_item.fee_code='01' and pay_list_detail.is_in_olorder=0) zxdk_all on stu.xh=zxdk_all.xh where zxdk_all.xh is not null group by stu.yxmc) zxdk on base_stu.yxmc=zxdk.yxmc");

        for (int x = 0; x < tj.Rows.Count; x++)
        {
            if(x==0)
            {
                d2 = "{name: '" + tj.Rows[x][0].ToString() + "',y: " + tj.Rows[x][2].ToString() + ",drilldown: '" + tj.Rows[x][0].ToString() + "'}";
            }
            else
            {
                d2 += ",{name: '" + tj.Rows[x][0].ToString() + "',y: " + tj.Rows[x][2].ToString() + ",drilldown: '" + tj.Rows[x][0].ToString() + "'}";
          
            }
        }

        l1 = Convert.ToString(tj.Compute("Sum(录取学生数)", "")); ;
        l4 = Convert.ToString(tj.Compute("Sum(已缴学费)", "")); ;
        b1 = Convert.ToString(tj.Compute("Sum(已缴学费)", ""));

        l5 = Convert.ToString(tj.Compute("Sum(网上缴费)", "")); ;
        l6 = Convert.ToString(tj.Compute("Sum(现场缴费)", "")); ;
        l7 = Convert.ToString(tj.Compute("Sum(申请助学贷款)", ""));
        b3 = l7;
        
        DataTable yxtj = Serachtj("SELECT plan_num 招生计划人数,lq_num 录取人数,zc_num 网上注册人数,ol_num 网上缴费人数,bd_num 现场报到人数,up_dt 更新时间 FROM (select sum(num) plan_num from Fresh_Plan) a left join (select count(*) lq_num from Fresh_STU where nd='2017' and len(zydm)=6) b on 1=1 left join (select count(*) zc_num from Fresh_STU where nd='2017' and zc_zt='1' and len(zydm)=6) c on 1=1 left join (select count(*) ol_num from Fresh_STU where nd='2017' and ol_zt='1' and len(zydm)=6) d on 1=1 left join (select count(*) bd_num from Fresh_STU where nd='2017' and bd_zt='已报到' and len(zydm)=6) e on 1=1 left join (select top 1 up_dt from event_log where event='fresh_tj') f on 1=1");

        if (yxtj.Rows.Count > 0)
        {
            //迎新统计
  
            l2 = yxtj.Rows[0]["网上注册人数"].ToString();
            l3 = yxtj.Rows[0]["网上缴费人数"].ToString();
            b2 = (Convert.ToInt32(l3) + Convert.ToInt32(l6) - Convert.ToInt32(b1)).ToString();
            jfzs = (Convert.ToInt32(l3) + Convert.ToInt32(l6)).ToString();
        }

        d1="{name: '录取人数',y: "+l1+",drilldown: '录取人数'}";
        d1 += ", {name: '网上注册',y: " + l2 + ",drilldown: '网上注册'}";
        //d1 += ", {name: '网上缴费',y: " + l3 + ",drilldown: '网上缴费'}";
        d1 += ", {name: '缴费学生总数',y: " + jfzs + ",drilldown: '缴费学生总数'}";
        d1 += ", {name: '网上缴学费',y: " + l5 + ",drilldown: '网上缴学费'}";
        d1 += ", {name: '网上缴其它费',y: " + b2 + ",drilldown: '网上缴其它费'}";
        d1 += ", { name: '现场缴费',y: " + l6 + ", drilldown: '现场缴费'}";
        d1 += ", {name: '申请助学贷款',y: " + l7 + ",drilldown: '申请助学贷款'}";


        d3 = "['网上缴费', " + l5 + "],['现场缴费', " + l6 + "]";


    }
}
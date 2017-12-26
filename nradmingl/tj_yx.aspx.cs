using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class nradmingl_tj_yx : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        string yx = "00";
        if (Request["url"] != null)
        {
            ljurl.HRef = "/apptj/apptjgz.aspx";
        }
        if (Request["id"] != null)
        {
            yx = Request["id"].ToString();
        }
        string sql = "SELECT     a.bjmc AS 班级名称, ISNULL(b.lq_num, 0) AS 录取,ISNULL(c.zc_num, 0) AS 注册,isnull(d.ol_num+(f.jf1-g.[对账]),0) 缴费, ISNULL(e.bd_num, 0) AS 报到 FROM         (SELECT     yxdm, count(bjdm) AS plan_num, bjdm,bjmc,zydm                       FROM          Fresh_STU                       GROUP BY yxdm, bjdm,bjmc,zydm) AS a LEFT OUTER JOIN                          (SELECT     yxdm, COUNT(*) AS lq_num, bjdm                            FROM          Fresh_STU                            WHERE      (nd = '2017') AND (LEN(zydm) = 6)                            GROUP BY yxdm, bjdm) AS b ON a.bjdm = b.bjdm LEFT OUTER JOIN                          (SELECT     yxdm, COUNT(*) AS zc_num, bjdm                            FROM          Fresh_STU                            WHERE      (nd = '2017') AND (zc_zt = '1') AND (LEN(zydm) = 6)                            GROUP BY yxdm, bjdm) AS c ON a.bjdm = c.bjdm LEFT OUTER JOIN                          (SELECT     yxdm, COUNT(*) AS ol_num,bjdm                            FROM          Fresh_STU                            WHERE      (nd = '2017') AND (ol_zt='1') AND (LEN(zydm) = 6)                            GROUP BY yxdm,bjdm) AS d ON a.bjdm= d.bjdm                             LEFT OUTER JOIN                          (select yxdm,COUNT(*) as jf1,bjdm from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and sfxmdm='01' GROUP BY yxdm,bjdm) AS f ON a.bjdm= f.bjdm                            LEFT OUTER JOIN                    (select yxdm,isnull(count(*),0) 对账,bjdm from fresh_stu stu left join (SELECT SUM(sfje) je,xh FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is not null and sfnd=2017 and sfxmdm='01' group by xh) dz_all on stu.xh=dz_all.xh where dz_all.xh is not null group by stu.yxdm,stu.bjdm) AS g ON a.bjdm= g.bjdm           LEFT OUTER JOIN                     (SELECT     yxdm, COUNT(*) AS bd_num,bjdm                            FROM          Fresh_STU                            WHERE      (nd = '2017') AND (bd_zt = '已报到') AND (LEN(zydm) = 6)                            GROUP BY yxdm,bjdm) AS e ON a.bjdm= e.bjdm where  len(a.zydm)=6 and a.yxdm='" + yx + "'";

        DataTable tj = Serachtj(sql);
        try
        {
            DataRow newrow = tj.NewRow();

            newrow["班级名称"] = "合计";
            newrow["录取"] = Convert.ToString(tj.Compute("Sum(录取)", "")); ;
            newrow["注册"] = Convert.ToString(tj.Compute("Sum(注册)", "")); ;
            newrow["缴费"] = Convert.ToString(tj.Compute("Sum(缴费)", "")); ;
            newrow["报到"] = Convert.ToString(tj.Compute("Sum(报到)", "")); ;
            //newrow["申请助学贷款"] = Convert.ToString(tj.Compute("Sum(申请助学贷款)", "")); ;


            tj.Rows.Add(newrow);
        }
        catch { }
        GridView1.DataSource = tj;
        GridView1.DataBind();
    }
}
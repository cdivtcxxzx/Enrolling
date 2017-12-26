using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class nradmingl_jk_wsbd : System.Web.UI.Page
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
        DataTable tj = Serachtj("SELECT a.yxmc 学院名称,isnull(lq_num,0) 录取人数,isnull(zc_num,0) 网上注册,isnull(ol_num,0) 网上缴费,isnull(qs_num,0) 选寝人数,isnull(xx_num,0) 完善信息,isnull(note_num,0) 未读通知 FROM (select yxmc,sum(num) plan_num from Fresh_Plan group by yxmc) a left join (select yxmc,count(*) lq_num from Fresh_STU where nd='2017' and len(zydm)=6 group by yxmc) b on a.yxmc=b.yxmc left join (select yxmc,count(*) zc_num from Fresh_STU where nd='2017' and zc_zt='1' and len(zydm)=6 group by yxmc) c on a.yxmc=c.yxmc left join (select yxmc,count(*) ol_num from Fresh_STU where nd='2017' and ol_zt='1' and len(zydm)=6 group by yxmc) d on a.yxmc=d.yxmc left join (select yxmc,count(*) qs_num from Fresh_STU where nd='2017' and qs_zt='1' and len(zydm)=6 group by yxmc) e on a.yxmc=e.yxmc left join (select yxmc,count(*) xx_num from Fresh_STU where nd='2017' and xx_zt='1' and len(zydm)=6 group by yxmc) f on a.yxmc=f.yxmc left join (select yxmc,sum(note_unread) note_num from Fresh_STU where nd='2017' and len(zydm)=6 group by yxmc) g on a.yxmc=g.yxmc ");

        DataRow newrow = tj.NewRow();

        newrow["学院名称"] ="全院合计";
        newrow["录取人数"] = Convert.ToString(tj.Compute("Sum(录取人数)", "")); ;
        newrow["网上注册"] = Convert.ToString(tj.Compute("Sum(网上注册)", "")); ;
        newrow["网上缴费"] = Convert.ToString(tj.Compute("Sum(网上缴费)", "")); ;
        newrow["选寝人数"] = Convert.ToString(tj.Compute("Sum(选寝人数)", "")); ;
        newrow["完善信息"] = Convert.ToString(tj.Compute("Sum(完善信息)", "")); ;
        newrow["未读通知"] = Convert.ToString(tj.Compute("Sum(未读通知)", "")); ;

        tj.Rows.Add(newrow);
        GridView1.DataSource = tj;
        GridView1.DataBind();
    }
    protected void exportexcel(object sender, EventArgs e)
    {
        //准备导出的DATATABLE,为了输出时列名为中文,请在写SQL语句时重定义一下列名
        //例:SELECT [int] 序号  FROM [taskmanager] order by [int] desc 
        System.Data.DataTable dt = Serachtj("SELECT a.yxmc 学院名称,isnull(lq_num,0) 录取人数,isnull(zc_num,0) 网上注册,isnull(ol_num,0) 网上缴费,isnull(qs_num,0) 选寝人数,isnull(xx_num,0) 完善信息,isnull(note_num,0) 未读通知 FROM (select yxmc,sum(num) plan_num from Fresh_Plan group by yxmc) a left join (select yxmc,count(*) lq_num from Fresh_STU where nd='2017' and len(zydm)=6 group by yxmc) b on a.yxmc=b.yxmc left join (select yxmc,count(*) zc_num from Fresh_STU where nd='2017' and zc_zt='1' and len(zydm)=6 group by yxmc) c on a.yxmc=c.yxmc left join (select yxmc,count(*) ol_num from Fresh_STU where nd='2017' and ol_zt='1' and len(zydm)=6 group by yxmc) d on a.yxmc=d.yxmc left join (select yxmc,count(*) qs_num from Fresh_STU where nd='2017' and qs_zt='1' and len(zydm)=6 group by yxmc) e on a.yxmc=e.yxmc left join (select yxmc,count(*) xx_num from Fresh_STU where nd='2017' and xx_zt='1' and len(zydm)=6 group by yxmc) f on a.yxmc=f.yxmc left join (select yxmc,sum(note_unread) note_num from Fresh_STU where nd='2017' and len(zydm)=6 group by yxmc) g on a.yxmc=g.yxmc ");
        #region 导出
        //引用EXCEL导出类
        toexcel xzfile = new toexcel();
        string filen = xzfile.DatatableToExcel(dt, "网上报到统计数据");
        //Response.Write("文件名" + filen);
        if (filen.Length > 4)
        {
            this.tsbox.Value = "<span style=\"font-size:Large;\"> <font color=green>导出成功,请</font><a href=" + filen + " target=_blank ><b><font color=red>点此下载</font></b></a></span>";
            //this.Label1.Text = "<font color=green>生成导入模板成功,请<a href=" + filen + " target=_blank >点此下载模板</a></font>";

        }
        else
        {
            this.tsbox.Value = "<span style=\"font-size:Large;\"><font color=red>导出<b>失败</b>,请重试!</font></span>";

        }
        #endregion
    }
    protected void xq_SelectedIndexChanged(object sender, EventArgs e)
    {
        gzt();
    }
    protected void dorm_SelectedIndexChanged(object sender, EventArgs e)
    {
        gzt();
    }
    protected void floor_SelectedIndexChanged(object sender, EventArgs e)
    {
        gzt();
    }
    protected void bj_SelectedIndexChanged(object sender, EventArgs e)
    {
        gzt();
    }
    protected void gzt()
    {




    }
    protected void clearyfp(object sender, EventArgs e)
    {
        //清空预分配数据
    }
    protected void yx_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class nradmingl_tj_xtsy : System.Web.UI.Page
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

        string sql = "";
        if (this.yx.SelectedItem.Text == "全部院系")
        {
            sql = "SELECT     DM_YUANXI.YXMC AS 院系名称, Fresh_Class.Year AS 年度, Fresh_Class.PK_Class_NO AS 班级编号, Fresh_Class.Name AS 班级名称,    yonghqx.xm AS 辅导员, Fresh_Counseller.Phone AS 联系电话, Fresh_Counseller.QQ AS qq号码, yonghqx.fwcs AS 访问次数 FROM         Fresh_SPE INNER JOIN                      Fresh_Class ON Fresh_SPE.PK_SPE = Fresh_Class.FK_SPE_NO LEFT OUTER JOIN                      DM_YUANXI ON Fresh_SPE.FK_College_Code = DM_YUANXI.YXDM LEFT OUTER JOIN                      yonghqx RIGHT OUTER JOIN                      Fresh_Counseller ON yonghqx.yhid = Fresh_Counseller.FK_Staff_NO ON Fresh_Class.PK_Class_NO = Fresh_Counseller.FK_Class_NO ";

        }
        else
        {

            sql = "SELECT     DM_YUANXI.YXMC AS 院系名称, Fresh_Class.Year AS 年度, Fresh_Class.PK_Class_NO AS 班级编号, Fresh_Class.Name AS 班级名称,    yonghqx.xm AS 辅导员, Fresh_Counseller.Phone AS 联系电话, Fresh_Counseller.QQ AS qq号码, yonghqx.fwcs AS 访问次数 FROM         Fresh_SPE INNER JOIN                      Fresh_Class ON Fresh_SPE.PK_SPE = Fresh_Class.FK_SPE_NO LEFT OUTER JOIN                      DM_YUANXI ON Fresh_SPE.FK_College_Code = DM_YUANXI.YXDM LEFT OUTER JOIN                      yonghqx RIGHT OUTER JOIN                      Fresh_Counseller ON yonghqx.yhid = Fresh_Counseller.FK_Staff_NO ON Fresh_Class.PK_Class_NO = Fresh_Counseller.FK_Class_NO where DM_YUANXI.YXMC='" + this.yx.SelectedItem.Text + "'";

        }
        DataTable yx1 =Sqlhelper.Serach(sql);
       
            g_ts.Text = "<font color=red>共有班级："+yx1.Rows.Count.ToString()+"个，设置辅导员："+yx1.Select("len(辅导员)>0").Count()+"</font>";
       
        GridView1.DataSource = yx1;
        GridView1.DataBind();
    
    }
    protected void exportexcel(object sender, EventArgs e)
    {

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
      
        //提示统计信息
        if (yx.SelectedValue == "全部院系" || yx.SelectedValue == "0")
        {
            //g_ts.Text = dormitory.serch_yfptj("0", "all", "");
        }
        else
        {
            //g_ts.Text = dormitory.serch_yfptj(yx.SelectedValue, "0", yx.SelectedItem.Text);
        }
    }
}
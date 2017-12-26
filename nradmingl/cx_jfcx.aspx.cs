using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class nradmingl_cx_jfcx : System.Web.UI.Page
{
    #region 合并单元格 合并某一列所有行
    /// <summary>  
    /// 合并GridView中某列相同信息的行（单元格）  
    /// </summary>  
    /// <param name="GridView1"></param>  
    /// <param name="cellNum"></param>  
    public static void GroupCol(GridView GridView1, int cols)
    {
        if (GridView1.Rows.Count < 1 || cols > GridView1.Rows[0].Cells.Count - 1)
        {
            return;
        }
        TableCell oldTc = GridView1.Rows[0].Cells[cols];
        for (int i = 1; i < GridView1.Rows.Count; i++)
        {
            TableCell tc = GridView1.Rows[i].Cells[cols];
            if (oldTc.Text == tc.Text)
            {
                tc.Visible = false;
                if (oldTc.RowSpan == 0)
                {
                    oldTc.RowSpan = 1;
                }
                oldTc.RowSpan++;
                oldTc.VerticalAlign = VerticalAlign.Middle;
            }
            else
            {
                oldTc = tc;
            }
        }
    }
    #endregion 
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

       // string sql = "";
       // if (this.yx.SelectedItem.Text == "全部院系")
       // {
       //     sql = "SELECT     DM_YUANXI.YXMC AS 院系名称, Fresh_Class.Year AS 年度, Fresh_Class.PK_Class_NO AS 班级编号, Fresh_Class.Name AS 班级名称,    yonghqx.xm AS 辅导员, Fresh_Counseller.Phone AS 联系电话, Fresh_Counseller.QQ AS qq号码, yonghqx.fwcs AS 访问次数 FROM         Fresh_SPE INNER JOIN                      Fresh_Class ON Fresh_SPE.PK_SPE = Fresh_Class.FK_SPE_NO LEFT OUTER JOIN                      DM_YUANXI ON Fresh_SPE.FK_College_Code = DM_YUANXI.YXDM LEFT OUTER JOIN                      yonghqx RIGHT OUTER JOIN                      Fresh_Counseller ON yonghqx.yhid = Fresh_Counseller.FK_Staff_NO ON Fresh_Class.PK_Class_NO = Fresh_Counseller.FK_Class_NO ";

       // }
       // else
       // {

       //     sql = "SELECT     DM_YUANXI.YXMC AS 院系名称, Fresh_Class.Year AS 年度, Fresh_Class.PK_Class_NO AS 班级编号, Fresh_Class.Name AS 班级名称,    yonghqx.xm AS 辅导员, Fresh_Counseller.Phone AS 联系电话, Fresh_Counseller.QQ AS qq号码, yonghqx.fwcs AS 访问次数 FROM         Fresh_SPE INNER JOIN                      Fresh_Class ON Fresh_SPE.PK_SPE = Fresh_Class.FK_SPE_NO LEFT OUTER JOIN                      DM_YUANXI ON Fresh_SPE.FK_College_Code = DM_YUANXI.YXDM LEFT OUTER JOIN                      yonghqx RIGHT OUTER JOIN                      Fresh_Counseller ON yonghqx.yhid = Fresh_Counseller.FK_Staff_NO ON Fresh_Class.PK_Class_NO = Fresh_Counseller.FK_Class_NO where DM_YUANXI.YXMC='" + this.yx.SelectedItem.Text + "'";

       // }
       // sql="select a.xh,a.gkbmh,a.xm,a.bjmc,sfxmmc,sfje from fresh_stu a left join [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] b on a.xh=b.xh where sfnd=2017 and zfzt=2 and (a.gkbmh like '%602032%' or a.xm like '%602032%' or a.sfzjh like '%602032%')";
       // DataTable yx1 = Sqlhelper.Serach(sql);

       //// g_ts.Text = "<font color=red>共有班级：" + yx1.Rows.Count.ToString() + "个，设置辅导员：" + yx1.Select("len(辅导员)>0").Count() + "</font>";

       // GridView1.DataSource = yx1;
       // GridView1.DataBind();

    }
    protected void exportexcel(object sender, EventArgs e)
    {
        gzt();

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

        string sql = "";
        if (this.yx.SelectedItem.Text != "全部院系")
        {
            sql += " and a.yxdm='" + this.yx.SelectedValue + "' ";

        }
        if (this.bj.SelectedItem.Text != "全部班级")
        {


            sql += " and a.bjdm='" + this.bj.SelectedValue + "' ";
        }
        sql = "select top 300 a.yxmc 院系名称,a.bjmc 班级名称,a.xh 学号,a.gkbmh 高考报名号,a.xm 姓名,sfxmmc 收费项目名称,sfje 收费金额 from fresh_stu a left join [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] b on a.xh=b.xh where sfnd=2017 and zfzt=2 " + sql + " and (a.gkbmh like '%" + this.TextBox1.Text + "%' or a.xm like '%" + this.TextBox1.Text + "%' or a.sfzjh like '%" + this.TextBox1.Text + "%')";
        DataTable yx1 = Serachtj(sql);
        if (yx1.Rows.Count > 0)
        {
            //g_ts.Text = yx1.Rows.Count.ToString()+"!!!!";
        }
        //g_ts.Text+= sql;

        GridView1.DataSource = yx1;
        GridView1.DataBind();

        //GroupCol(GridView1, 0);
        //GroupCol(GridView1, 1);
        GroupCol(GridView1, 2);
        GroupCol(GridView1, 3);
        GroupCol(GridView1, 4);


    }
    protected void clearyfp(object sender, EventArgs e)
    {
        //清空预分配数据
    }
    protected void yx_SelectedIndexChanged(object sender, EventArgs e)
    {
        gzt();
    }
}
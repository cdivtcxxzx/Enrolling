using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

public partial class nradmingl_tj_ssqk : System.Web.UI.Page
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

    protected string sycw(string images)
    {
        try
        {
            if ( Convert.ToInt32( images)>10)
            {
                return images;
            }
            if (Convert.ToInt32(images) > 5)
            {
                return "<font color=blue>" + images + "</font>";
            }
            else
            {
                return "<font color=red><b>" + images + "</b></font>";
            }
        }
        catch { }
        return images;
    }
    protected string cwyj(string images,string ycw,string yxcw,string lq,string jf)
    {
        try
        {
            if (ycw == "0")
            {
                return "未准备床位";
            }
            if (ycw == "1"&&images=="1")
            {
                return "床位充足";
            }

            if (Convert.ToInt32(ycw) - Convert.ToInt32(yxcw) == 0)
            {
                if (Convert.ToInt32(jf) - Convert.ToInt32(yxcw) > Convert.ToInt32(images))
                {
                    return "<font color=red><b>缴费数超过寝室数</b></font>";
                }
                else
                {
                    if (Convert.ToInt32(lq) - Convert.ToInt32(yxcw) > Convert.ToInt32(images))
                    {
                        return "<font color=red><b>准备寝室已不足</b></font>";
                    }
                    else
                    {
                        return "<font color=blue><b>全部用完</b></font>";
                    }
                }
            }



            if (Convert.ToInt32(images) > 10)
            {
                return "床位充足";
            }
            if (Convert.ToInt32(images) > 5)
            {
                if (Convert.ToInt32(ycw) >= 10)
                {
                    return "<font color=blue>床位低于10个</font>";
                }
                else
                {
                    return "床位充足";
                }
            }
            else
            {

                if (Convert.ToInt32(ycw) >= 5)
                {
                    return "<font color=red><b>床位低于5个</b></font>";
                }
                else
                {
                    if (Convert.ToInt32(ycw) <= 2)
                    {
                        if (Convert.ToInt32(images) > 1)
                        {
                            return "床位充足";
                        }
                        else
                        {
                            return "<font color=red><b>床位已不足</b></font>";
                        }
                    }
                    else
                    {

                        if (Convert.ToInt32(images) >= 2)
                        {
                            return "床位充足";
                        }
                        else
                        {
                            return "<font color=red><b>床位已不足</b></font>";
                        }
                    }
                }

                
            }
        }
        catch { }
        return images;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql32 = "";
        string sql32a = "";
        string sql32b = "";
        if (CheckBox1.Checked)
        {
            sql32 = "";
            sql32a = "";
            sql32b = "";
        }
        else
        {
            sql32b = " and len([zydm])=6 ";
            sql32 = " and len([SPE_Code])=6 ";
            sql32a = " where len([SPE_Code])=6 ";
        }



        try
        {
            string sqlz = "SELECT a.yxdm AS 院系代码, ISNULL(a.lqrs, 0) AS 录取人数, ISNULL(b.qss, 0) AS 准备床位, ISNULL(c.xqs, 0) AS 已选床位, DM_YUANXI.YXMC AS 院系名称 FROM (SELECT College_NO AS yxdm, COUNT(College_NO) AS lqrs FROM vw_student_base " + sql32a + " GROUP BY College_NO) AS a LEFT OUTER JOIN DM_YUANXI ON a.yxdm = DM_YUANXI.YXDM LEFT OUTER JOIN(SELECT College_NO AS yxdm, COUNT(College_NO) AS qss FROM vw_class_beds GROUP BY College_NO) AS b ON a.yxdm = b.yxdm LEFT OUTER JOIN(SELECT vw_fresh_student_base.College_NO AS yxdm, COUNT(vw_fresh_student_base.College_NO) AS xqs FROM Fresh_Bed_Log LEFT OUTER JOIN vw_fresh_student_base ON Fresh_Bed_Log.FK_SNO = vw_fresh_student_base.PK_SNO GROUP BY vw_fresh_student_base.College_NO) AS c ON a.yxdm = c.yxdm";
            string sqln = "SELECT a.yxdm AS 院系代码, ISNULL(a.lqrs, 0) AS 录取人数, ISNULL(b.qss, 0) AS 准备床位, ISNULL(c.xqs, 0) AS 已选床位, DM_YUANXI.YXMC AS 院系名称 FROM (SELECT College_NO AS yxdm, COUNT(College_NO) AS lqrs FROM vw_student_base where Gender_Code='01' " + sql32 + " GROUP BY College_NO) AS a LEFT OUTER JOIN DM_YUANXI ON a.yxdm = DM_YUANXI.YXDM LEFT OUTER JOIN(SELECT College_NO AS yxdm, COUNT(College_NO) AS qss FROM vw_class_beds  where Gender='男' GROUP BY College_NO) AS b ON a.yxdm = b.yxdm LEFT OUTER JOIN(SELECT vw_fresh_student_base.College_NO AS yxdm, COUNT(vw_fresh_student_base.College_NO) AS xqs FROM Fresh_Bed_Log LEFT OUTER JOIN vw_fresh_student_base ON Fresh_Bed_Log.FK_SNO = vw_fresh_student_base.PK_SNO where vw_fresh_student_base.Gender_Code='01' GROUP BY vw_fresh_student_base.College_NO) AS c ON a.yxdm = c.yxdm";
            string sqlv = "SELECT a.yxdm AS 院系代码, ISNULL(a.lqrs, 0) AS 录取人数, ISNULL(b.qss, 0) AS 准备床位, ISNULL(c.xqs, 0) AS 已选床位, DM_YUANXI.YXMC AS 院系名称 FROM (SELECT College_NO AS yxdm, COUNT(College_NO) AS lqrs FROM vw_student_base where Gender_Code='02' " + sql32 + " GROUP BY College_NO) AS a LEFT OUTER JOIN DM_YUANXI ON a.yxdm = DM_YUANXI.YXDM LEFT OUTER JOIN(SELECT College_NO AS yxdm, COUNT(College_NO) AS qss FROM vw_class_beds  where Gender='女' GROUP BY College_NO) AS b ON a.yxdm = b.yxdm LEFT OUTER JOIN(SELECT vw_fresh_student_base.College_NO AS yxdm, COUNT(vw_fresh_student_base.College_NO) AS xqs FROM Fresh_Bed_Log LEFT OUTER JOIN vw_fresh_student_base ON Fresh_Bed_Log.FK_SNO = vw_fresh_student_base.PK_SNO where vw_fresh_student_base.Gender_Code='02' GROUP BY vw_fresh_student_base.College_NO) AS c ON a.yxdm = c.yxdm";
            if (this.yx.SelectedItem.Text != "全部院系")
            {
                sqlz = "SELECT a.yxdm AS 院系代码, ISNULL(a.lqrs, 0) AS 录取人数, ISNULL(b.qss, 0) AS 准备床位, ISNULL(c.xqs, 0) AS 已选床位, DM_YUANXI.YXMC AS 院系名称 FROM (SELECT College_NO AS yxdm, COUNT(College_NO) AS lqrs FROM vw_student_base where College_NO='" + this.yx.SelectedValue + "' " + sql32 + "  GROUP BY College_NO) AS a LEFT OUTER JOIN DM_YUANXI ON a.yxdm = DM_YUANXI.YXDM LEFT OUTER JOIN(SELECT College_NO AS yxdm, COUNT(College_NO) AS qss FROM vw_class_beds  where College_NO='" + this.yx.SelectedValue + "'  GROUP BY College_NO) AS b ON a.yxdm = b.yxdm LEFT OUTER JOIN(SELECT vw_fresh_student_base.College_NO AS yxdm, COUNT(vw_fresh_student_base.College_NO) AS xqs FROM Fresh_Bed_Log LEFT OUTER JOIN vw_fresh_student_base ON Fresh_Bed_Log.FK_SNO = vw_fresh_student_base.PK_SNO  where vw_fresh_student_base.College_NO='" + this.yx.SelectedValue + "'  GROUP BY vw_fresh_student_base.College_NO) AS c ON a.yxdm = c.yxdm"; ;
                sqln = "SELECT a.yxdm AS 院系代码, ISNULL(a.lqrs, 0) AS 录取人数, ISNULL(b.qss, 0) AS 准备床位, ISNULL(c.xqs, 0) AS 已选床位, DM_YUANXI.YXMC AS 院系名称 FROM (SELECT College_NO AS yxdm, COUNT(College_NO) AS lqrs FROM vw_student_base where Gender_Code='01' and College_NO='" + this.yx.SelectedValue + "' " + sql32 + "  GROUP BY College_NO) AS a LEFT OUTER JOIN DM_YUANXI ON a.yxdm = DM_YUANXI.YXDM LEFT OUTER JOIN(SELECT College_NO AS yxdm, COUNT(College_NO) AS qss FROM vw_class_beds  where Gender='男' and  College_NO='" + this.yx.SelectedValue + "'  GROUP BY College_NO) AS b ON a.yxdm = b.yxdm LEFT OUTER JOIN(SELECT vw_fresh_student_base.College_NO AS yxdm, COUNT(vw_fresh_student_base.College_NO) AS xqs FROM Fresh_Bed_Log LEFT OUTER JOIN vw_fresh_student_base ON Fresh_Bed_Log.FK_SNO = vw_fresh_student_base.PK_SNO where vw_fresh_student_base.Gender_Code='01' and vw_fresh_student_base.College_NO='" + this.yx.SelectedValue + "'  GROUP BY vw_fresh_student_base.College_NO) AS c ON a.yxdm = c.yxdm";
                sqlv = "SELECT a.yxdm AS 院系代码, ISNULL(a.lqrs, 0) AS 录取人数, ISNULL(b.qss, 0) AS 准备床位, ISNULL(c.xqs, 0) AS 已选床位, DM_YUANXI.YXMC AS 院系名称 FROM (SELECT College_NO AS yxdm, COUNT(College_NO) AS lqrs FROM vw_student_base where Gender_Code='02' and College_NO='" + this.yx.SelectedValue + "' " + sql32 + "  GROUP BY College_NO) AS a LEFT OUTER JOIN DM_YUANXI ON a.yxdm = DM_YUANXI.YXDM LEFT OUTER JOIN(SELECT College_NO AS yxdm, COUNT(College_NO) AS qss FROM vw_class_beds  where Gender='女' and  College_NO='" + this.yx.SelectedValue + "'  GROUP BY College_NO) AS b ON a.yxdm = b.yxdm LEFT OUTER JOIN(SELECT vw_fresh_student_base.College_NO AS yxdm, COUNT(vw_fresh_student_base.College_NO) AS xqs FROM Fresh_Bed_Log LEFT OUTER JOIN vw_fresh_student_base ON Fresh_Bed_Log.FK_SNO = vw_fresh_student_base.PK_SNO where vw_fresh_student_base.Gender_Code='02' and vw_fresh_student_base.College_NO='" + this.yx.SelectedValue + "'  GROUP BY vw_fresh_student_base.College_NO) AS c ON a.yxdm = c.yxdm";

            }

            //院系总计
            DataTable yxzj = Sqlhelper.Serach(sqlz);

            yxzj.Columns.Add("缴费学生数");
            for (int i = 0; i < yxzj.Rows.Count; i++)
            {
                DataTable yxzj1 = Serachtj("SELECT isnull(count([yxdm]),0)  FROM [Fresh_STU] where ol_zt=1 " + sql32b + " and yxdm='" + yxzj.Rows[i]["院系代码"].ToString() + "'");
                if (yxzj1.Rows.Count > 0)
                {
                    yxzj.Rows[i]["缴费学生数"] = yxzj1.Rows[0][0].ToString();
                }
            }



            //院系男
            DataTable yxzjn = Sqlhelper.Serach(sqln);
            yxzjn.Columns.Add("缴费学生数");
            for (int i = 0; i < yxzjn.Rows.Count; i++)
            {
                DataTable yxzjn1 = Serachtj("SELECT isnull(count([yxdm]),0)  FROM [Fresh_STU] where ol_zt=1 " + sql32b + " and [gender_code]='01' and  yxdm='" + yxzj.Rows[i]["院系代码"].ToString() + "'");
                if (yxzjn1.Rows.Count > 0)
                {
                    yxzjn.Rows[i]["缴费学生数"] = yxzjn1.Rows[0][0].ToString();
                }
            }
            //院系女
            DataTable yxzjv = Sqlhelper.Serach(sqlv);
            yxzjv.Columns.Add("缴费学生数");
            for (int i = 0; i < yxzjv.Rows.Count; i++)
            {
                DataTable yxzjv1 = Serachtj("SELECT isnull(count([yxdm]),0)  FROM [Fresh_STU] where ol_zt=1 " + sql32b + " and [gender_code]='02' and  yxdm='" + yxzj.Rows[i]["院系代码"].ToString() + "'");
                if (yxzjv1.Rows.Count > 0)
                {
                    yxzjv.Rows[i]["缴费学生数"] = yxzjv1.Rows[0][0].ToString();
                }
            }

            //合并三个表格
            DataTable zj = new DataTable();
            zj.Columns.Add("序号");
            zj.Columns.Add("院系名称");
            zj.Columns.Add("性别");
            zj.Columns.Add("录取人数");
            zj.Columns.Add("缴费学生数");
            zj.Columns.Add("准备床位");
            zj.Columns.Add("已选床位");
            zj.Columns.Add("剩余床位");
            try
            {
                for (int i = 0; i < yxzj.Rows.Count; i++)
                {
                    DataRow newrow = zj.NewRow();
                    newrow["序号"] = (i + 1).ToString();
                    newrow["性别"] = "男";
                    newrow["录取人数"] = yxzjn.Rows[i][1].ToString();
                    newrow["准备床位"] = yxzjn.Rows[i][2].ToString();
                    newrow["已选床位"] = yxzjn.Rows[i][3].ToString();
                    newrow["院系名称"] = yxzjn.Rows[i][4].ToString();
                    newrow["缴费学生数"] = yxzjn.Rows[i][5].ToString();
                    newrow["剩余床位"] = (Convert.ToInt32(yxzjn.Rows[i][2].ToString()) - Convert.ToInt32(yxzjn.Rows[i][3].ToString())).ToString();
                    zj.Rows.Add(newrow);
                    DataRow newrow1 = zj.NewRow();
                    newrow1["序号"] = (i + 1).ToString();
                    newrow1["性别"] = "女";
                    newrow1["录取人数"] = yxzjv.Rows[i][1].ToString();
                    newrow1["准备床位"] = yxzjv.Rows[i][2].ToString();
                    newrow1["已选床位"] = yxzjv.Rows[i][3].ToString();
                    newrow1["院系名称"] = yxzjv.Rows[i][4].ToString();
                    newrow1["缴费学生数"] = yxzjv.Rows[i][5].ToString();
                    newrow1["剩余床位"] = (Convert.ToInt32(yxzjv.Rows[i][2].ToString()) - Convert.ToInt32(yxzjv.Rows[i][3].ToString())).ToString();
                    zj.Rows.Add(newrow1);

                    DataRow newrow2 = zj.NewRow();
                    newrow2["序号"] = (i + 1).ToString();
                    newrow2["性别"] = "合计";
                    newrow2["录取人数"] = yxzj.Rows[i][1].ToString();
                    newrow2["准备床位"] = yxzj.Rows[i][2].ToString();
                    newrow2["已选床位"] = yxzj.Rows[i][3].ToString();
                    newrow2["院系名称"] = yxzj.Rows[i][4].ToString();
                    newrow2["缴费学生数"] = yxzj.Rows[i][5].ToString();
                    newrow2["剩余床位"] = (Convert.ToInt32(yxzj.Rows[i][2].ToString()) - Convert.ToInt32(yxzj.Rows[i][3].ToString())).ToString();
                    zj.Rows.Add(newrow2);
                }
            }
            catch (Exception ex1) { g_ts.Text += ex1.Message + "@"; }
            GridView1.DataSource = zj;

            GridView1.DataBind();
            GroupCol(GridView1, 0);
            GroupCol(GridView1, 1);
        }
        catch { }

        //学校统计

        try
        {

            string sqlz = "SELECT a.yxdm AS 院系代码, ISNULL(a.lqrs, 0) AS 录取人数, ISNULL(b.qss, 0) AS 准备床位, ISNULL(c.xqs, 0) AS 已选床位,Fresh_Class.Name AS 院系名称 FROM (SELECT [FK_Class_NO] AS yxdm, COUNT([FK_Class_NO]) AS lqrs FROM vw_student_base where College_NO='" + this.yx.SelectedValue + "'  " + sql32 + "GROUP BY [FK_Class_NO]) AS a LEFT OUTER JOIN Fresh_Class ON a.yxdm = Fresh_Class.PK_Class_NO  LEFT OUTER JOIN(SELECT [PK_Class_NO] AS yxdm, COUNT([PK_Class_NO]) AS qss FROM vw_class_beds  where College_NO='" + this.yx.SelectedValue + "'  GROUP BY [PK_Class_NO]) AS b ON a.yxdm = b.yxdm LEFT OUTER JOIN(SELECT vw_fresh_student_base.FK_Class_NO AS yxdm, COUNT(vw_fresh_student_base.FK_Class_NO) AS xqs FROM Fresh_Bed_Log LEFT OUTER JOIN vw_fresh_student_base ON Fresh_Bed_Log.FK_SNO = vw_fresh_student_base.PK_SNO  where vw_fresh_student_base.College_NO='" + this.yx.SelectedValue + "'  GROUP BY vw_fresh_student_base.FK_Class_NO) AS c ON a.yxdm = c.yxdm"; ;
            string sqln = "SELECT a.yxdm AS 院系代码, ISNULL(a.lqrs, 0) AS 录取人数, ISNULL(b.qss, 0) AS 准备床位, ISNULL(c.xqs, 0) AS 已选床位,Fresh_Class.Name AS 院系名称 FROM (SELECT [FK_Class_NO] AS yxdm, COUNT([FK_Class_NO]) AS lqrs FROM vw_student_base where Gender_Code='01' and  College_NO='" + this.yx.SelectedValue + "' " + sql32 + " GROUP BY [FK_Class_NO]) AS a LEFT OUTER JOIN Fresh_Class ON a.yxdm = Fresh_Class.PK_Class_NO  LEFT OUTER JOIN(SELECT [PK_Class_NO] AS yxdm, COUNT([PK_Class_NO]) AS qss FROM vw_class_beds  where Gender='男' and   College_NO='" + this.yx.SelectedValue + "'  GROUP BY [PK_Class_NO]) AS b ON a.yxdm = b.yxdm LEFT OUTER JOIN(SELECT vw_fresh_student_base.FK_Class_NO AS yxdm, COUNT(vw_fresh_student_base.FK_Class_NO) AS xqs FROM Fresh_Bed_Log LEFT OUTER JOIN vw_fresh_student_base ON Fresh_Bed_Log.FK_SNO = vw_fresh_student_base.PK_SNO  where vw_fresh_student_base.Gender_Code='01' and  vw_fresh_student_base.College_NO='" + this.yx.SelectedValue + "'  GROUP BY vw_fresh_student_base.FK_Class_NO) AS c ON a.yxdm = c.yxdm"; ;
            string sqlv = "SELECT a.yxdm AS 院系代码, ISNULL(a.lqrs, 0) AS 录取人数, ISNULL(b.qss, 0) AS 准备床位, ISNULL(c.xqs, 0) AS 已选床位,Fresh_Class.Name AS 院系名称 FROM (SELECT [FK_Class_NO] AS yxdm, COUNT([FK_Class_NO]) AS lqrs FROM vw_student_base where Gender_Code='02' and  College_NO='" + this.yx.SelectedValue + "' " + sql32 + "  GROUP BY [FK_Class_NO]) AS a LEFT OUTER JOIN Fresh_Class ON a.yxdm = Fresh_Class.PK_Class_NO  LEFT OUTER JOIN(SELECT [PK_Class_NO] AS yxdm, COUNT([PK_Class_NO]) AS qss FROM vw_class_beds  where Gender='女' and   College_NO='" + this.yx.SelectedValue + "'  GROUP BY [PK_Class_NO]) AS b ON a.yxdm = b.yxdm LEFT OUTER JOIN(SELECT vw_fresh_student_base.FK_Class_NO AS yxdm, COUNT(vw_fresh_student_base.FK_Class_NO) AS xqs FROM Fresh_Bed_Log LEFT OUTER JOIN vw_fresh_student_base ON Fresh_Bed_Log.FK_SNO = vw_fresh_student_base.PK_SNO  where vw_fresh_student_base.Gender_Code='02' and  vw_fresh_student_base.College_NO='" + this.yx.SelectedValue + "'  GROUP BY vw_fresh_student_base.FK_Class_NO) AS c ON a.yxdm = c.yxdm"; ;
           
         
           

            //院系总计
            DataTable yxzj = Sqlhelper.Serach(sqlz);

            yxzj.Columns.Add("缴费学生数");
            for (int i = 0; i < yxzj.Rows.Count; i++)
            {
                DataTable yxzj1 = Serachtj("SELECT isnull(count([yxdm]),0)  FROM [Fresh_STU] where ol_zt=1 " + sql32b + " and [bjdm]='" + yxzj.Rows[i]["院系代码"].ToString() + "'");
                if (yxzj1.Rows.Count > 0)
                {
                    yxzj.Rows[i]["缴费学生数"] = yxzj1.Rows[0][0].ToString();
                }
            }



            //院系男
            DataTable yxzjn = Sqlhelper.Serach(sqln);
            yxzjn.Columns.Add("缴费学生数");
            for (int i = 0; i < yxzjn.Rows.Count; i++)
            {
                DataTable yxzjn1 = Serachtj("SELECT isnull(count([yxdm]),0)  FROM [Fresh_STU] where ol_zt=1 " + sql32b + " and [gender_code]='01' and  [bjdm]='" + yxzj.Rows[i]["院系代码"].ToString() + "'");
                if (yxzjn1.Rows.Count > 0)
                {
                    yxzjn.Rows[i]["缴费学生数"] = yxzjn1.Rows[0][0].ToString();
                }
            }
            //院系女
            DataTable yxzjv = Sqlhelper.Serach(sqlv);
            yxzjv.Columns.Add("缴费学生数");
            for (int i = 0; i < yxzjv.Rows.Count; i++)
            {
                DataTable yxzjv1 = Serachtj("SELECT isnull(count([yxdm]),0)  FROM [Fresh_STU] where ol_zt=1 " + sql32b + " and [gender_code]='02' and  [bjdm]='" + yxzj.Rows[i]["院系代码"].ToString() + "'");
                if (yxzjv1.Rows.Count > 0)
                {
                    yxzjv.Rows[i]["缴费学生数"] = yxzjv1.Rows[0][0].ToString();
                }
            }

            //合并三个表格
            DataTable zj = new DataTable();
            zj.Columns.Add("序号");
            zj.Columns.Add("班级名称");
            zj.Columns.Add("性别");
            zj.Columns.Add("录取人数");
            zj.Columns.Add("缴费学生数");
            zj.Columns.Add("准备床位");
            zj.Columns.Add("已选床位");
            zj.Columns.Add("剩余床位");
            try
            {
                for (int i = 0; i < yxzj.Rows.Count; i++)
                {
                    try
                    {
                        DataRow newrow = zj.NewRow();
                        newrow["序号"] = (i + 1).ToString();
                        newrow["性别"] = "男";
                        newrow["录取人数"] = yxzjn.Rows[i][1].ToString();
                        newrow["准备床位"] = yxzjn.Rows[i][2].ToString();
                        newrow["已选床位"] = yxzjn.Rows[i][3].ToString();
                        newrow["班级名称"] = yxzjn.Rows[i][4].ToString();
                        newrow["缴费学生数"] = yxzjn.Rows[i][5].ToString();
                        newrow["剩余床位"] = (Convert.ToInt32(yxzjn.Rows[i][2].ToString()) - Convert.ToInt32(yxzjn.Rows[i][3].ToString())).ToString();
                        zj.Rows.Add(newrow);
                    }
                    catch { }
                    try
                    {
                    DataRow newrow1 = zj.NewRow();
                    newrow1["序号"] = (i + 1).ToString();
                    newrow1["性别"] = "女";
                    newrow1["录取人数"] = yxzjv.Rows[i][1].ToString();
                    newrow1["准备床位"] = yxzjv.Rows[i][2].ToString();
                    newrow1["已选床位"] = yxzjv.Rows[i][3].ToString();
                    newrow1["班级名称"] = yxzjv.Rows[i][4].ToString();
                    newrow1["缴费学生数"] = yxzjv.Rows[i][5].ToString();
                    newrow1["剩余床位"] = (Convert.ToInt32(yxzjv.Rows[i][2].ToString()) - Convert.ToInt32(yxzjv.Rows[i][3].ToString())).ToString();
                    zj.Rows.Add(newrow1);
                    }
                    catch { }
                    try
                    {
                    DataRow newrow2 = zj.NewRow();
                    newrow2["序号"] = (i + 1).ToString();
                    newrow2["性别"] = "合计";
                    newrow2["录取人数"] = yxzj.Rows[i][1].ToString();
                    newrow2["准备床位"] = yxzj.Rows[i][2].ToString();
                    newrow2["已选床位"] = yxzj.Rows[i][3].ToString();
                    newrow2["班级名称"] = yxzj.Rows[i][4].ToString();
                    newrow2["缴费学生数"] = yxzj.Rows[i][5].ToString();
                    newrow2["剩余床位"] = (Convert.ToInt32(yxzj.Rows[i][2].ToString()) - Convert.ToInt32(yxzj.Rows[i][3].ToString())).ToString();
                    zj.Rows.Add(newrow2);
                    }
                    catch { }
                }
            }
            catch (Exception ex1) { g_ts.Text += ex1.Message + "#"; }
            GridView2.DataSource = zj;

            GridView2.DataBind();
            GroupCol(GridView2, 0);
            GroupCol(GridView2, 1);
        }
        catch { }


    }

    /// <summary>
    /// 将网格数据导出到Excel
    /// </summary>
    /// <param name="ctrl">网格名称(如GridView1)</param>
    /// <param name="FileType">要导出的文件类型(Excel:application/ms-excel)</param>
    /// <param name="FileName">要保存的文件名</param>
    public static void GridViewToExcel(Control ctrl, string FileType, string FileName)
    {
        HttpContext.Current.Response.Charset = "GB2312";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;//注意编码
        HttpContext.Current.Response.AppendHeader("Content-Disposition",
            "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8).ToString());
        HttpContext.Current.Response.ContentType = FileType;//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword 
        ctrl.Page.EnableViewState = false;
        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        ctrl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);//解决GRIDVIEW输出EXCEL出错
    }
    protected void exportexcel(object sender, EventArgs e)
    {
        DateTime dt = System.DateTime.Now;
        string str = dt.ToString("yyyyMMddhhmmss");
        str ="网上报到之寝室统计数据(" + this.yx.SelectedItem.Text + ")" + str + ".xls";

        if (yx.SelectedItem.Text == "全部院系")
        {

            this.GridView1.AllowPaging = false;
            GridView1.DataBind();
            GroupCol(GridView1, 0);
            GroupCol(GridView1, 1);
            GridViewToExcel(GridView1, "application/ms-excel", str);
            // Export(gvRecord, "application/ms-excel", str);
        }
        else
        {
            this.GridView2.AllowPaging = false;
            GridView2.DataBind();
            GroupCol(GridView2, 0);
            GroupCol(GridView2, 1);
            GridViewToExcel(GridView2, "application/ms-excel", str);
        }


        ////准备导出的DATATABLE,为了输出时列名为中文,请在写SQL语句时重定义一下列名
        ////例:SELECT [int] 序号  FROM [taskmanager] order by [int] desc 
        //System.Data.DataTable dt = Sqlhelper.Serach("SELECT     row_number() over (order by  ds_dalei.name, ds_bsxiangmu.name )  AS 序号,ds_bm.id AS 报名编号, ds_saishi.name AS 赛事名称, ds_dalei.name AS 比赛大类, ds_bsxiangmu.name AS 参赛项目, ds_bm.sxlx AS 赛项类型,ds_bm.xuexiao AS 参赛学校, ds_bm.xsxm AS 参赛选手, ds_bm.jsxm AS 指导教师, ds_bm.shzt AS 报名状态 FROM         ds_bm LEFT OUTER JOIN                      yonghqx ON ds_bm.xuexiao = yonghqx.xuexiao LEFT OUTER JOIN                      ds_saishi ON ds_bm.saishiid = ds_saishi.id LEFT OUTER JOIN          ds_bsxiangmu ON ds_bm.saixiangbm = ds_bsxiangmu.dm LEFT OUTER JOIN             ds_dalei ON ds_bm.daleibm = ds_dalei.dm WHERE     (ds_bm.shzt = '未提交' OR            ds_bm.shzt = '已上报' or (ds_bm.shzt = '被打回')) AND (ds_bm.saishiid ='" + this.DropDownList1.SelectedValue + "') AND (yonghqx.yhid = '" + Session["username"].ToString() + "') ORDER BY 比赛大类, 参赛项目");

        //#region 导出
        ////引用EXCEL导出类
        //toexcel xzfile = new toexcel();
        //string filen = xzfile.DatatableToExcel(dt, "报名数据");
        ////Response.Write("文件名" + filen);
        //if (filen.Length > 4)
        //{
        //    this.tsbox.Value = "<span style=\"font-size:Large;\"> <font color=green>导出成功,请</font><a href=" + filen + " target=_blank ><b><font color=red>点此下载</font></b></a></span>";
        //    //this.Label1.Text = "<font color=green>生成导入模板成功,请<a href=" + filen + " target=_blank >点此下载模板</a></font>";

        //}
        //else
        //{
        //    this.tsbox.Value = "<span style=\"font-size:Large;\"><font color=red>导出<b>失败</b>,请重试!</font></span>";

        //}
        //#endregion
    }
    
    protected void exportexcel2(object sender, EventArgs e)
    {
        DateTime dt1 = System.DateTime.Now;
        string str = dt1.ToString("yyyyMMddhhmmss");
        str = "寝室数据(" + this.yx.SelectedItem.Text + ")" + str;
        string sql = "";

        if (yx.SelectedItem.Text == "全部院系")
        {
            sql = "SELECT     Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号, Fresh_Bed.Bed_Name AS 床位位置描述, Fresh_Room.Gender AS 性别,                       Base_College.Name AS 已分配院系, Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名,                       Base_STU.Test_NO AS 高考报名号, Base_STU.Phone AS 联系电话 FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Bed_Log LEFT OUTER JOIN                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN                      Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON                       Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO ORDER BY 已分配院系, 已分配班级, 房间编号, 床位编号";
        
        }
        else
        {
            sql = "SELECT     Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号, Fresh_Bed.Bed_Name AS 床位位置描述, Fresh_Room.Gender AS 性别,                       Base_College.Name AS 已分配院系, Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名,                       Base_STU.Test_NO AS 高考报名号, Base_STU.Phone AS 联系电话 FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Bed_Log LEFT OUTER JOIN                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN                      Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON                       Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO where Base_College.Name='" + yx.SelectedItem.Text + "' ORDER BY 已分配院系, 已分配班级, 房间编号, 床位编号";
        }


        //sql = "SELECT TOP 1200 row_number() over (order by  a.bjmc )  AS 序号,a.yxmc 院系名称,bjmc 班级名称,a.xh 学号,[gkbmh] 高考报名号,[xm] 姓名,case when a.[gender_code]='01' then '男' else '女' end 性别,[sfzjh] 身份证号,case when [zc_zt]='1' then '已注册' else '未注册' end  网上注册,case when [ol_zt]='1' then '已缴费' else '未在网上缴费' end 网上缴费,[ol_je] 网上缴费金额,sfxm.qfje 现场缴学费,sfxmzs.qfje 现场缴住宿费,case when zxdk.Fee_Name='助学贷款' then '已办理' else '未办理' end 助学贷款,qsxx.qsh 寝室,qsxx.cwxx 床位,b.[Status_Code] 报到状态,b.[QQ]      ,b.[Phone] 联系电话   ,b.[Home_add] 家庭地址 ,b.[Phone_dr] 高考时电话,b.[Phone_fa] 父亲电话      ,b.[Phone_ma] 母亲电话      ,b.[Huji_add] 户籍地址      ,b.[Note] 学生性质  FROM [TJ].[dbo].[Fresh_STU] a left join enrollment.yxxt_data.dbo.base_stu b on a.xh=b.pk_sno left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='01') sfxm on sfxm.xh=a.xh  left join  (select qfje,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje>0 and sfnd=2017 and sfxmdm='02') sfxmzs on sfxmzs.xh=a.xh left join (SELECT     Fresh_Bed_Log.FK_SNO xh, Fresh_Room.Room_NO qsh,Fresh_Bed.Bed_Name cwxx FROM         enrollment.yxxt_data.dbo.Fresh_Bed_Log LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Bed ON Fresh_Bed_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO LEFT OUTER JOIN                      enrollment.yxxt_data.dbo.Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO) qsxx on qsxx.xh=a.xh left join (select pay_list.sno xh,Fee_name from FreshMan.dbo.Pay_List left join FreshMan.dbo.Pay_List_Detail on pay_list.pk_pay_list=pay_list_detail.fk_pay_list left join FreshMan.dbo.Fee_Item on pay_list_detail.fk_fee_item=fee_item.pk_fee_item where pay_list.fk_fee=1 and fee_item.fee_code='01' and pay_list_detail.is_in_olorder=0) zxdk on zxdk.xh=a.xh where a.yxmc='轨道交通学院' order by a.bjmc ";

        //准备导出的DATATABLE,为了输出时列名为中文,请在写SQL语句时重定义一下列名
        //例:SELECT [int] 序号  FROM [taskmanager] order by [int] desc 
        //System.Data.DataTable dt = Serachtj(sql);
        System.Data.DataTable dt = Sqlhelper.Serach(sql);

        #region 导出
        //引用EXCEL导出类
        toexcel xzfile = new toexcel();
        string filen = xzfile.DatatableToExcel(dt, "寝室数据");
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
        gzt();
        //提示统计信息
        if (yx.SelectedValue == "全部院系" || yx.SelectedValue == "0")
        {
            //g_ts.Text = dormitory.serch_yfptj("0", "all", "");
            bjlist.Style.Add("display", "none");
        }
        else
        {
            bjlist.Style.Add("display", "");
            //g_ts.Text = dormitory.serch_yfptj(yx.SelectedValue, "0", yx.SelectedItem.Text);
        }
    }
}
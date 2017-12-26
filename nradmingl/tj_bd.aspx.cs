using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class nradmingl_tj_bd : System.Web.UI.Page
{



    protected string bdl(string bd,string lq)
    {
        int s1 = Convert.ToInt32(lq);
       int s2 = Convert.ToInt32(bd);
        if (s1 != 0)
        {
            double percent = Convert.ToDouble(s2) / Convert.ToDouble(s1);
            //string result = percent.ToString("0%");//得到6%
            double percent2 = 1 - percent;
            return percent.ToString("0.00%");//得到5.882%
        }
        return "0.00%";
    }
    protected string zxdk(string zxdk1, string jfzs,string wsjf,string xcjf)
    {
        int s1 = Convert.ToInt32(jfzs);
        int s2 = Convert.ToInt32(wsjf);
        int s3 = Convert.ToInt32(xcjf);
        int s4 = s1 - s2 - s3;
        
        return zxdk1+"(<b>"+s4.ToString()+"</b>)";
    }
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
        DataTable tj = Serachtj("select base_stu.yxmc 学院名称,isnull(base_stu.学生数,0) 录取人数,isnull(zc_num,0) 网上注册,isnull(d.ol_num+isnull(off_stu.缴费-dz_stu.[对账],0),0) 缴费总数,isnull(ol_stu.[网上缴费],0) 网上缴学费            ,isnull(off_stu.缴费-dz_stu.[对账],0) 现场缴费,isnull(zxdk.[助学贷款],0) 申请助学贷款,isnull(qs_num,0) 选寝人数,isnull(xx_num,0) 完善信息,isnull(bd_num,0) 来校报到,base_stu.yxdm from  (select yxmc,isnull(count(*),0) 学生数,yxdm from fresh_stu where len(zydm)=6 group by yxmc,yxdm) base_stu left join (select yxmc,count(*) zc_num from Fresh_STU where nd='2017' and zc_zt='1' and len(zydm)=6 group by yxmc) c on base_stu.yxmc=c.yxmc  left join (select yxmc,count(*) ol_num,yxdm from Fresh_STU where nd='2017' and ol_zt='1' and len(zydm)=6 group by yxmc,yxdm) d on base_stu.yxmc=d.yxmc left join (select yxmc,count(*) qs_num from Fresh_STU where nd='2017' and qs_zt='1' and len(zydm)=6 group by yxmc) e on base_stu.yxmc=e.yxmc left join (select yxmc,count(*) xx_num from Fresh_STU where nd='2017' and xx_zt='1' and len(zydm)=6 group by yxmc) f on base_stu.yxmc=f.yxmc left join (select yxmc,count(*) bd_num from Fresh_STU where nd='2017' and len(zydm)=6 and bd_zt!='未报到' group by yxmc) g on base_stu.yxmc=g.yxmc left join (select yxmc,count(*) 缴费 from fresh_stu stu left join (select sum(je) je,xh from (select isnull(SUM(sfje),0)-isnull(SUM(tfje),0) je,xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and sfxmdm='01' group by xh union select SUM(sfje) je,xh FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is null and sfnd=2017 and zfzt=2 and sfxmdm='01' group by xh) t where je>0  and len(xh)>12 group by xh ) ol_all on stu.xh=ol_all.xh where ol_all.xh is not null group by stu.yxmc) all_stu on base_stu.yxmc=all_stu.yxmc left join (select yxmc,isnull(count(*),0) 网上缴费 from fresh_stu stu left join (select SUM(sfje) je,xh FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfnd=2017 and zfzt=2 and sfxmdm='01' group by xh) ol_all on stu.xh=ol_all.xh where ol_all.xh is not null group by stu.yxmc) ol_stu on base_stu.yxmc=ol_stu.yxmc left join (select yxmc,isnull(count(*),0) 缴费 from fresh_stu stu left join (select xh from [SF].[cdgyzy].[dbo].v_jk_jfxx where qfje<=0 and sfnd=2017 and sfxmdm='01') off_all on stu.xh=off_all.xh where off_all.xh is not null group by stu.yxmc) off_stu on base_stu.yxmc=off_stu.yxmc left join (select yxmc,isnull(count(*),0) 对账 from fresh_stu stu left join (SELECT SUM(sfje) je,xh FROM [SF].[cdgyzy].[dbo].[SCS_JK_ORDER] where sfdh is not null and sfnd=2017 and sfxmdm='01' group by xh) dz_all on stu.xh=dz_all.xh where dz_all.xh is not null group by stu.yxmc) dz_stu on base_stu.yxmc=dz_stu.yxmc left join (select yxmc,isnull(count(*),0) 助学贷款 from fresh_stu stu left join (select pay_list.sno xh from FreshMan.dbo.Pay_List left join FreshMan.dbo.Pay_List_Detail on pay_list.pk_pay_list=pay_list_detail.fk_pay_list left join FreshMan.dbo.Fee_Item on pay_list_detail.fk_fee_item=fee_item.pk_fee_item where pay_list.fk_fee=1 and fee_item.fee_code='01' and pay_list_detail.is_in_olorder=0) zxdk_all on stu.xh=zxdk_all.xh where zxdk_all.xh is not null group by stu.yxmc) zxdk on base_stu.yxmc=zxdk.yxmc");

        DataRow newrow = tj.NewRow();

        newrow["学院名称"] = "全院合计";
        newrow["录取人数"] = Convert.ToString(tj.Compute("Sum(录取人数)", ""));
        newrow["网上注册"] = Convert.ToString(tj.Compute("Sum(网上注册)", ""));
       
        newrow["缴费总数"] = Convert.ToString(tj.Compute("Sum(缴费总数)", ""));
        newrow["网上缴学费"] = Convert.ToString(tj.Compute("Sum(网上缴学费)", ""));
         newrow["现场缴费"] = Convert.ToString(tj.Compute("Sum(现场缴费)", ""));
         newrow["申请助学贷款"] = Convert.ToString(tj.Compute("Sum(申请助学贷款)", ""));
        newrow["选寝人数"] = Convert.ToString(tj.Compute("Sum(选寝人数)", ""));
        newrow["完善信息"] = Convert.ToString(tj.Compute("Sum(完善信息)", ""));
        newrow["来校报到"] = Convert.ToString(tj.Compute("Sum(来校报到)", ""));

        tj.Rows.Add(newrow);
        GridView1.DataSource = tj;
        GridView1.DataBind();
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
        System.IO.StringWriter tw = new System.IO.StringWriter();
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
        str =  "迎新统计数据" + str + ".xls";

        this.GridView1.AllowPaging = false;
        GridView1.DataBind();
        this.GridView1.Columns[11].Visible = false;
        GridViewToExcel(GridView1, "application/ms-excel", str);
        // Export(gvRecord, "application/ms-excel", str);



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
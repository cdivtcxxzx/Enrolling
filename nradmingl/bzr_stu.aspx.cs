using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nradmingl_Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //string pk_staff_no = Request.QueryString["pk_staff_no"];//获取员工编号
        //if (pk_staff_no == null || pk_staff_no.Trim().Length == 0)
        //{
        //    throw new Exception("参数错误");
        //}

        //Session["pk_staff_no"] = pk_staff_no;

        if (Session["username"] == null)
        {
            throw new Exception("没登陆");
        }
        this.pk_staff_no.Value = Session["username"].ToString().Trim();
    }


    protected void btn_down_Click(object sender, EventArgs e)
    {
        if (hid_class_no.Value != null && hid_class_no.Value.Trim().Length > 0)
        {
            batch batch_logic = new batch();
            DataTable dt = batch_logic.get_classstudent(hid_class_no.Value.Trim());
            if (dt == null || dt.Rows.Count <= 0) { this.tsxx.Value = "<span style=\"font-size:Large;\"><font color=red>导出<b>失败</b>,请重试!</font></span>"; return; }
            //dt.Columns.Remove("year");
            //dt.Columns.Remove("collage");
            //dt.Columns.Remove("spe_name");
            //dt.Columns.Remove("Status_code");
            dt.Columns.Remove("register");
            dt.Columns.Remove("TuitionType");
            dt.Columns["pk_sno"].ColumnName = "学号";
            dt.Columns["name"].ColumnName = "姓名";
            dt.Columns["gender"].ColumnName = "性别";
            dt.Columns["id_no"].ColumnName = "身份证号";
            dt.Columns["test_no"].ColumnName = "高考报名号";
            dt.Columns["phone"].ColumnName = "联系电话";

            dt.Columns["year"].ColumnName = "年级";
            dt.Columns["collage"].ColumnName = "学院";
            dt.Columns["spe_name"].ColumnName = "专业";
            dt.Columns["Status_code"].ColumnName = "状态";
            #region 导出
            //引用EXCEL导出类
            toexcel xzfile = new toexcel();
            string filen = xzfile.DatatableToExcel(dt, "学生信息");


            if (filen.Length > 4)
            {
                this.tsxx.Value = "<span style=\"font-size:Large;\"> <font color=green>导出成功,请<a href=" + filen + " target=_blank >点此下载</a></font></span>";
                //this.g_ts.Text = "<font color=green>生成导入模板成功,请<a href=" + filen + " target=_blank >点此下载模板</a></font>";

            }
            else
            {
                this.tsxx.Value = "<span style=\"font-size:Large;\"><font color=red>导出<b>失败</b>,请重试!</font></span>";
            }
            #endregion
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_bzr_classbeds : System.Web.UI.Page
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
    //导出数据
    protected void btn_down_Click(object sender, EventArgs e)
    {
        if (hid_class_no.Value != null && hid_class_no.Value.Trim().Length > 0 )
        {
            batch batch_logic = new batch();
            System.Data.DataTable dt = batch_logic.get_classbedstudent(hid_class_no.Value.Trim());
            if (dt == null || dt.Rows.Count <= 0) { this.tsxx.Value = "<span style=\"font-size:Large;\"><font color=red>导出<b>失败</b>,请重试!</font></span>"; return; }
            dt.Columns.Remove("PK_Dorm_NO");
            dt.Columns.Remove("Dorm_NO");
            dt.Columns.Remove("Year");
            dt.Columns.Remove("Campus_NO");
            dt.Columns.Remove("PK_Room_NO");
            dt.Columns.Remove("FK_Room_Type");
            dt.Columns.Remove("PK_Bed_NO");
            dt.Columns.Remove("College_NO");
            dt.Columns.Remove("SPE_Code");
            dt.Columns.Remove("SpeYear");
            dt.Columns.Remove("Class_Campus_NO");
            dt.Columns.Remove("Class_Campus_Name");
            dt.Columns.Remove("PK_Class_NO");
            dt.Columns.Remove("FK_SNO");
            dt.Columns.Remove("PK_College");

            dt.Columns["DormName"].ColumnName = "宿舍";
            dt.Columns["Campus_name"].ColumnName = "校区";
            dt.Columns["Room_NO"].ColumnName = "房间号";
            dt.Columns["Floor"].ColumnName = "楼层";
            dt.Columns["Type_Name"].ColumnName = "房间类型";
            dt.Columns["Bed_NO"].ColumnName = "床号";
            dt.Columns["Bed_name"].ColumnName = "位置";
            dt.Columns["CollegeName"].ColumnName = "学院";
            dt.Columns["SPE_Name"].ColumnName = "专业";
            dt.Columns["ClassName"].ColumnName = "班级";
            dt.Columns["studentname"].ColumnName = "姓名";
            dt.Columns["Gender"].ColumnName = "性别";
            #region 导出
            //引用EXCEL导出类
            toexcel xzfile = new toexcel();
            string filen = xzfile.DatatableToExcel(dt, "学生寝室信息");


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
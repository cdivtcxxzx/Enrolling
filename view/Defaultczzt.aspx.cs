using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_Defaultczzt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("<div style='width:80%;margin-left:10%;'><font color=red><b><br>为了提高访问速度，管理首页统计显示已经关闭！<br><br>（1）为了不引起选寝冲突，学生网上选寝室功能已于2017年9月1日12时关闭！请各级学院及辅导员到“迎新统计”-》“寝室使用情况”选择院系后，点击右上角“导出寝室数据”按钮，导出本院系寝室数据，并打印，方便现场分配寝室！<br><br>（2）学生网上缴费详情可点击左侧导航菜单“网上缴费查询”进行缴费详情查询！<br><br>（3）学生来校报到后，请辅导员及时在左侧导航菜单“现场报到确认”中确认学生报到信息！</b></font></div>");
        Response.End();
    }
}
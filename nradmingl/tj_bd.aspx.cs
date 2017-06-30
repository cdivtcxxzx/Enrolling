using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nradmingl_tj_bd : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

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
        gzt();
        //提示统计信息
        //if (yx.SelectedValue == "全部院系" || yx.SelectedValue == "0")
        //{
        //    //g_ts.Text = dormitory.serch_yfptj("0", "all", "");

        //}
        //else
        //{

        //    //g_ts.Text = dormitory.serch_yfptj(yx.SelectedValue, "0", yx.SelectedItem.Text);
        //}
    }
}
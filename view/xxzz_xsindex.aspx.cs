using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class view_xxzz_xsindex : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //设置链接
        //查看详情链接
        string xh="";
        //验证是否传递学号
        if (Session["username"]!=null)
        {
           
            xh=Session["username"].ToString();



#region 获取班级，班主任，班主任电话信息
            DataTable bjxx = dormitory.serch_xsxx(xh);
            if (bjxx.Rows.Count > 0)
            {
                this.xsxx_bj.Text = "班级：" + bjxx.Rows[0]["班级名称"].ToString();
                this.xsxx_bzr.Text = "班主任：" + bjxx.Rows[0]["班主任姓名"].ToString();
                this.xsxx_bzrdh.Text = "联系电话：" + bjxx.Rows[0]["班主任电话"].ToString();
            }
            else
            {
                this.xsxx_bj.Text = "班级：无";
                this.xsxx_bzr.Text = "班主任：无";
                this.xsxx_bzrdh.Text = "联系电话：无";
            }
#endregion
            #region 获取操作状态更改图标状态
            //基本状态
            //写法未完成
            //<font color=red>未完成</font>  
            //写法已完成
            //<font color=green>已完成</font>

            this.xszt_bdxz.InnerText = "";//报到须知  
            this.xszt_wsjf.InnerText = "";//网上缴费
            this.xszt_czqs.InnerText = "";//选择寝室
            this.xszt_xxws.InnerText = "";//信息完善

            //详细情况

            this.xsztxq_bdxz.InnerText = "";//报到须知
            this.xsztxq_wsjh.InnerText = "";//网上缴费
            this.xsztxq_xzqs.InnerText = "";//选择寝室
            this.xsztxq_xxws.InnerText = "";//信息完善

            //图片状态更改说明
            zttp1.Src = "../images/xszt/1.png";//状态已完成：1.png 未完成1-1.png
            zttp2.Src = "../images/xszt/2.png";//状态已完成：2.png 未完成2-2.png
            zttp3.Src = "../images/xszt/3-3.png";//状态已完成：3.png 未完成3-3.png
            zttp4.Src = "../images/xszt/w4-4.png";//状态已完成：w4.png 未完成w4-4.png
            #endregion

#region 链接跳转
            //顶部学生信息详情
            this.xsxxurl.HRef = "xsjbxx.aspx?pk_affair_no=11&pk_sno=" + Session["username"].ToString() + "";
            this.xscz_bdxz.HRef = "";//报到须知  
            this.xscz_wsjf.HRef = "xswsjf.aspx";//网上缴费
            this.xscz_xzqs.HRef = "ssfp-yfp.aspx?pk_affair_no=3&pk_sno="+Session["username"].ToString();//选择寝室
            this.xscz_xxws.HRef = "xsxx-extend.aspx?pk_affair_no=12&pk_sno=" + Session["username"].ToString();//信息完善
#endregion

        }
        else
        {
            Response.Write("<script>top.location.href='/login.aspx?sf=xs'</script>");
        }
       
    }
}
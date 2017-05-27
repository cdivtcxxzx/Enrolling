using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class view_xswsjf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //设置链接
        //查看详情链接
        string xh = "";
        //验证是否传递学号
        if (Session["username"] != null)
        {

            xh = Session["username"].ToString();



           
            #region 获取操作状态
            //基本状态
            //写法未完成
            //<font color=red>未完成</font>  
            //写法已完成
            //<font color=green>已完成</font>
            this.xszt_jf1.InnerHtml = "<font color=red>未完成</font>";//缴费项目确认  
            this.xszt_jf2.InnerHtml = "<font color=red>未完成</font>";//网上缴费
           

            //详细情况
            this.xsztxq_jf1.InnerHtml = "";//缴费项目确认  
            this.xsztxq_jf2.InnerHtml = "";//网上缴费

            batch batch_logic = new batch();
            List<fresh_affair_log> affairlog_list = batch_logic.get_studentaffairlog_list(xh);//事务日志列表
            if (affairlog_list != null)
            {
                for (int j = 0; affairlog_list != null && j < affairlog_list.Count; j++)
                {
                    if (affairlog_list[j].FK_Affair_NO.Trim().Equals("7"))//缴费项目确认
                    {
                        this.xszt_jf1.InnerHtml = affairlog_list[j].Log_Status.Trim();//网上缴费
                    }
                    if (affairlog_list[j].FK_Affair_NO.Trim().Equals("10"))//网上缴费
                    {
                        this.xszt_jf2.InnerHtml = affairlog_list[j].Log_Status.Trim();//网上缴费
                    }
                }
            }            


            #endregion

            #region 链接跳转


            this.xscz_jf1.HRef = "xsbjf.aspx?pk_affair_no=7&pk_sno=" + Session["username"].ToString();//缴费项目选择
            this.xscz_jf2.HRef = "xsbjf_order.aspx?pk_affair_no=10&pk_sno=" + Session["username"].ToString();//确认网上缴费
            #endregion

        }
        else
        {
            Response.Write("<script>top.location.href='/login.aspx?sf=xs'</script>");
        }
    }
}
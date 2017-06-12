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
            this.xszt_bdzc.InnerHtml = "<font color=green>已完成</font>";//报到注册  
            this.xszt_bdxz.InnerHtml = "<font color=green>已完成</font>";//报到须知  
            this.xszt_wsjf.InnerHtml = "<font color=red>未完成</font>";//网上缴费
            this.xszt_czqs.InnerHtml = "<font color=red>未完成</font>";//选择寝室
            this.xszt_xxws.InnerHtml = "<font color=red>未完成</font>";//信息完善
            this.xszt_tzgg.InnerHtml = "<font color=red>未完成</font>";//通知公告

            //详细情况
            this.xsztxq_bdzc.InnerHtml = "";//报到注册
            this.xsztxq_bdxz.InnerHtml = "";//报到须知
            this.xsztxq_wsjh.InnerHtml = "";//网上缴费
            this.xsztxq_xzqs.InnerHtml = "";//选择寝室
            this.xsztxq_xxws.InnerHtml = "";//信息完善
            this.xsztxq_tzgg.InnerHtml = "";//通知公告

            //图片状态更改说明
            zttp1.Src = "../images/xszt/1.png";//状态已完成：1.png 未完成1-1.png
            zttp2.Src = "../images/xszt/2.png";//状态已完成：2.png 未完成2-2.png
            zttp3.Src = "../images/xszt/3-3.png";//状态已完成：3.png 未完成3-3.png
            zttp4.Src = "../images/xszt/4-4.png";//状态已完成：4.png 未完成4-4.png
            zttp5.Src = "../images/xszt/5-5.png";//状态已完成：5.png 未完成5-5.png
            zttp6.Src = "../images/xszt/w6-6.png";//状态已完成：w6.png 未完成w6-6.png


            batch batch_logic = new batch();
            System.Data.DataTable dtmsg = batch_logic.get_noreadmsg(xh);//获取发给学生的未读通知
            if (dtmsg != null && dtmsg.Rows.Count > 0)
            {
                zttp6.Src = "../images/xszt/w6.png";
                this.xszt_tzgg.InnerHtml = "<font color=red>"+dtmsg.Rows.Count.ToString().Trim()+"条通知未读</font>";//通知公告
                this.xsztxq_tzgg.InnerHtml = "";//通知公告
            }
            else
            {
                zttp6.Src = "../images/xszt/w6.png";
                this.xszt_tzgg.InnerHtml = "<font color=green></font>";//通知公告
                this.xsztxq_tzgg.InnerHtml = "";//通知公告
            }

            List<fresh_affair_log> affairlog_list = batch_logic.get_studentaffairlog_list(xh);//事务日志列表
            if (affairlog_list != null)
            {
                bool has_must = false;//有必交费用项目
                string has_must_log = null;
                bool has_order = false;//有未提交的订单
                string has_order_log = null;

                for (int j = 0; affairlog_list != null && j < affairlog_list.Count; j++)
                {
                    if (affairlog_list[j].FK_Affair_NO.Trim().Equals("9"))//报到须知
                    {
                        if (!affairlog_list[j].Log_Status.Trim().Equals("未确认"))
                        {
                            this.xszt_bdzc.InnerHtml = "<font color=green>已完成</font>";
                            zttp1.Src = "../images/xszt/1.png";//状态已完成：1.png 未完成1-1.png
                            this.xszt_bdxz.InnerHtml = "<font color=green>已完成</font>";
                            zttp2.Src = "../images/xszt/2.png";//状态已完成：1.png 未完成1-1.png
                        }
                    }
                    if (affairlog_list[j].FK_Affair_NO.Trim().Equals("7"))//缴费项目确认
                    {
                        if (!affairlog_list[j].Log_Status.Trim().Equals("未选必交费"))
                        {
                            has_must = true;//有必交费用项目
                        }
                        has_must_log = affairlog_list[j].Log_Status.Trim();

                    }
                    if (affairlog_list[j].FK_Affair_NO.Trim().Equals("10"))//网上缴费
                    {
                        if (!affairlog_list[j].Log_Status.Trim().Equals("待缴订单数量:0"))
                        {
                            has_order = true;//还有没有交费的订单
                        }
                        has_order_log = affairlog_list[j].Log_Status.Trim();
                    }
                    if (affairlog_list[j].FK_Affair_NO.Trim().Equals("3"))//选择寝室
                    {
                        if (!affairlog_list[j].Log_Status.Trim().Equals("未选择"))
                        {
                            this.xszt_czqs.InnerHtml = "<font color=green>已完成</font>";
                            zttp4.Src = "../images/xszt/4.png";//状态已完成：4.png 未完成4-4.png
                        }
                    }
                    if (affairlog_list[j].FK_Affair_NO.Trim().Equals("12"))//信息完善
                    {
                        if (!affairlog_list[j].Log_Status.Trim().Equals("未完善"))
                        {
                            this.xszt_xxws.InnerHtml = "<font color=green>已完成</font>";
                            zttp5.Src = "../images/xszt/5.png";//状态已完成：5.png 未完成5-5.png
                        }
                    }
                }
                if (has_must && !has_order)
                {
                    this.xszt_wsjf.InnerHtml = "<font color=green>已完成</font>";
                    zttp3.Src = "../images/xszt/3.png";//状态已完成：3.png 未完成3-3.png
                }
                else
                {
                    if (!has_must)
                    {
                        this.xsztxq_wsjh.InnerHtml = has_must_log;//网上缴费
                    }
                    else
                    {
                        this.xsztxq_wsjh.InnerHtml = has_order_log;//网上缴费
                    }
                }
            }         
            #endregion

#region 链接跳转
            //顶部学生信息详情
            this.xsxxurl.HRef = "xsjbxx.aspx?pk_affair_no=11&pk_sno=" + Session["username"].ToString() + "";
            this.xscz_bdxz.HRef = "guide.html";//报到须知  
            this.xscz_wsjf.HRef = "xswsjf.aspx";//网上缴费
            this.xscz_xzqs.HRef = "ssfp-yfp.aspx?pk_affair_no=3&pk_sno="+Session["username"].ToString();//选择寝室
            this.xscz_xxws.HRef = "xsxx-extend.aspx?pk_affair_no=12&pk_sno=" + Session["username"].ToString();//信息完善
            this.xscz_tzgg.HRef = "classmsg.aspx";//通知
#endregion

        }
        else
        {
            Response.Write("<script>top.location.href='/login.aspx?sf=xs'</script>");
        }
       
    }
}
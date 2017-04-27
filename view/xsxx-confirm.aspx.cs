using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_xsxx_confirm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 检查操作权限
        string pk_sno = Request.QueryString["pk_sno"];//获取学号
        if (pk_sno == null || pk_sno.Trim().Length == 0)
        {
            this.server_msg.Value = "参数错误";
            return;
        }

        string pk_affair_no = Request.QueryString["pk_affair_no"];//获取事务主键
        if (pk_affair_no == null || pk_affair_no.Trim().Length == 0)
        {
            this.server_msg.Value = "参数错误";
            return;
        }
        string pk_staff_no = Request.QueryString["pk_staff_no"];//获取员工编号

        string session_pk_sno = null;
        string session_pk_staff_no = null;
        if (Session["pk_sno"] != null)
        {
            session_pk_sno = Session["pk_sno"].ToString();
        }
        if (Session["pk_staff_no"] != null)
        {
            session_pk_staff_no = Session["pk_staff_no"].ToString();
        }

        batch batch_logic = new batch();
        affair_operate_auth_msg jg = batch_logic.affair_operate_auth(pk_affair_no, pk_sno, session_pk_sno, pk_staff_no, session_pk_staff_no, "cdivtc_xsxxqr_xabh");
        if (!jg.isauth)
        {
            this.server_msg.Value = jg.msg; ;
            return;
        }
        #endregion


        //get post获取学号
        //string pk_sno = Request["pk_sno"].ToString();
        
        this.hidden_pk_sno.Value = pk_sno.Trim();
        this.pk_affair_no.Value = pk_affair_no;//其值应由xszz-index.aspx或defaultczy.aspx传参过来
        this.pk_staff_no.Value = pk_staff_no;

        List<fresh_affair> data = batch_logic.get_freshstudent_affair_list(this.hidden_pk_sno.Value);
        if (data == null || data.Count == 0)
        {
            throw new Exception("获取学生迎新事务数据错误");
        }
        this.pk_batch_no.Value = data[0].FK_Batch_NO;


        //检查是否确认
        if (organizationService.isStuConfrim(pk_sno))
        {
            server_msg.Value = "您的信息已经确认！点击确定后回到首页！";
            //todo..跳转
            btn_submit.Visible = false;
            Response.Write("<script>setTimeout('history.go(-1);',300);</script>"); 
        }

    }
}
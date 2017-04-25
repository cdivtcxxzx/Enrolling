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


    }
}
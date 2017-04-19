using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_xsbjf : System.Web.UI.Page
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

        batch batch_logic = new batch();

        bool flag = batch_logic.is_affair_operate_appKey(pk_affair_no, "cdivtc_jf_ab087");//本应用验证码是否正确
        if (!flag)
        {
            this.server_msg.Value = "非授权访问";
            return;
        }

        string pk_staff_no = Request.QueryString["pk_staff_no"];//获取员工编号
        if (pk_staff_no == null || pk_staff_no.Trim().Length == 0)
        {
            pk_staff_no = "";
            //是学生自主登陆，验证pk_sno是是否与当前学生登陆帐户一致
            if (Session["pk_sno"] == null || !Session["pk_sno"].ToString().Trim().Equals(pk_sno.Trim()))
            {
                this.server_msg.Value = "非授权访问";
                return;
            }
        }
        else
        {
            //验证现场迎新操作员是否登陆
            if (Session["pk_staff_no"] == null || !Session["pk_staff_no"].ToString().Trim().Equals(pk_staff_no.Trim()))
            {
                this.server_msg.Value = "非授权访问";
                return;
            }
            //验证迎新管理元是否具备操作权限
            flag= batch_logic.check_operator_object(pk_staff_no, pk_affair_no, pk_sno);
            if (!flag)
            {
                this.server_msg.Value = "非授权访问";
                return;
            }
        }

        //验证学生是否具备该事务操作权限
        flag = batch_logic.check_student_affair_condition(pk_sno, pk_affair_no);//验证该生本事务操作前置条件是否满足
        if (!flag)
        {
            fresh_affair data1 = batch_logic.get_affair(pk_affair_no);
            if (data1 != null)
            {
                this.server_msg.Value = data1.precondition1Message + "," + data1.precondition2Message;
                return;
            }
            else
            {
                this.server_msg.Value = "获取事务前置条件时错误";
                return;
            }
        }
        #endregion

        #region 检查操作权限
        this.pk_sno.Value = pk_sno;//其值应由会话中来，在学生登陆成功后被赋予初值
        this.pk_affair_no.Value = pk_affair_no;//其值应由xszz-index.aspx或defaultczy.aspx传参过来
        this.pk_staff_no.Value = pk_staff_no;

        List<fresh_affair> data=batch_logic.get_freshstudent_affair_list(this.pk_sno.Value);
        if (data == null || data.Count == 0)
        {
            throw new Exception("获取学生迎新事务数据错误");
        }
        this.pk_batch_no.Value = data[0].FK_Batch_NO;

        #endregion
    }
}
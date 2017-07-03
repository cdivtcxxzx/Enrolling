using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_xsjbxx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PK_Affair_NO  事务编号
        //PK_SNO        学生编号
        //PK_Staff_NO   操作员编号

        #region 检查操作权限
        string pk_sno = Request.QueryString["pk_sno"];//获取学号
        if (pk_sno == null || pk_sno.Trim().Length == 0)
        {
            this.server_msg.Value = "参数错误";
            return;
        }

        //string pk_affair_no = Request.QueryString["pk_affair_no"];//获取事务主键
        //if (pk_affair_no == null || pk_affair_no.Trim().Length == 0)
        //{
        //    this.server_msg.Value = "参数错误";
        //    return;
        //}
        string pk_staff_no = Request.QueryString["pk_staff_no"];//获取员工编号

        //string session_pk_sno = null;
        //string session_pk_staff_no = null;
        //if (Session["pk_sno"] != null)
        //{
        //    session_pk_sno = Session["pk_sno"].ToString();
        //}
        //if (Session["pk_staff_no"] != null)
        //{
        //    session_pk_staff_no = Session["pk_staff_no"].ToString();
        //}

        //batch batch_logic = new batch();
        //affair_operate_auth_msg jg = batch_logic.affair_operate_auth(pk_affair_no, pk_sno, session_pk_sno, pk_staff_no, session_pk_staff_no, "cdivtc_xsjbxx_9086");
        //if (!jg.isauth)
        //{
        //    this.server_msg.Value = jg.msg; ;
        //    return;
        //}
        #endregion

        this.hidden_pk_sno.Value = pk_sno;
        this.pk_staff_no.Value = pk_staff_no;
        if (!IsPostBack)
        {
            #region 信息清空    
            //照片地址
            xszpxx.ImageUrl = "";
            //学号
            xsxx_xh.Text = pk_sno;
            //姓名
            xsxx_xm.Text = "";
            //性别
            xsxx_xb.Text = "";
            //身份证
            xsxx_sfzh.Text = "";
            //学历层次
            xsxx_xlcc.Text = "";
            //学院
            xsxx_xy.Text = "";
            //专业 
            xsxx_zy.Text = "";
            //年级
            xsxx_nj.Text = "";
            //班级名称
            xsxx_bjmc.Text = "";
            //班主任
            xsxx_bzr.Text = "";
            //班主任电话
            xsxx_bzrdh.Text = "";
            //班主任QQ
            xsxx_bzrqq.Text = "";
            //报名号
            xsxx_bmh.Text = "";
            #endregion
            
            

        }

    }
}
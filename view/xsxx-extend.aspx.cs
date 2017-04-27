using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_xsxx_extend : System.Web.UI.Page
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
        hidden_pk_sno.Value = pk_sno.Trim();


        //获取民族
        xsxx_mz.DataSource = organizationService.getCodesItem("003");
        xsxx_mz.DataTextField = "Item_Name";
        xsxx_mz.DataValueField = "PK_Item";
        xsxx_mz.DataBind();
        xsxx_mz.Items.Insert(0, new ListItem("请选择", "-1"));

        ////设置政治面貌
        xsxx_zzmm.DataSource = organizationService.getCodesItem("004");
        xsxx_zzmm.DataTextField = "Item_Name";
        xsxx_zzmm.DataValueField = "PK_Item";
        xsxx_zzmm.DataBind();
        xsxx_zzmm.Items.Insert(0, new ListItem("请选择", "-1"));


        xsxx_sg.Items.Clear();
        xsxx_tz.Items.Clear();
        xsxx_sg.Items.Insert(0, new ListItem("120以下", "0"));
        xsxx_tz.Items.Insert(0, new ListItem("20以下", "0"));
        //设置身高、体重
        for (int i = 1; i < 80; i++)
        {
            int sg = i+120;
            int tz = i + 20;
            xsxx_sg.Items.Insert(i, new ListItem(sg.ToString(), sg.ToString()));
            xsxx_tz.Items.Insert(i, new ListItem(tz.ToString(), tz.ToString()));
        }
        xsxx_sg.Items.Add( new ListItem("200以上", "999"));
        xsxx_tz.Items.Add( new ListItem("100以上", "999"));
        xsxx_sg.Items.Insert(0, new ListItem("请选择", "-1"));
        xsxx_tz.Items.Insert(0, new ListItem("请选择", "-1"));
    }
}
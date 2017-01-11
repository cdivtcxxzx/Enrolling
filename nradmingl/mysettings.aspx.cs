using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_mysettings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox TB_xl = formView1.FindControl("TB_xl") as TextBox;
            TextBox TB_xb = formView1.FindControl("TB_xb") as TextBox;
            TextBox TB_mz = formView1.FindControl("TB_mz") as TextBox;
            //TextBox TB_yx = formView1.FindControl("TB_yx") as TextBox;
            TextBox TB_zzmm = formView1.FindControl("TB_zzmm") as TextBox;
            DropDownList DDL_xl = formView1.FindControl("DDL_xl") as DropDownList;
            DropDownList DDL_xb = formView1.FindControl("DDL_xb") as DropDownList;
            DropDownList DDL_mz = formView1.FindControl("DDL_mz") as DropDownList;
            //DropDownList DDL_yx = formView1.FindControl("DDL_yx") as DropDownList;
            DropDownList DDL_zzmm = formView1.FindControl("DDL_zzmm") as DropDownList;
            DDL_xl.SelectedIndex = DDL_xl.Items.IndexOf(DDL_xl.Items.FindByText(TB_xl.Text));
            DDL_xb.SelectedIndex = DDL_xb.Items.IndexOf(DDL_xb.Items.FindByText(TB_xb.Text));
            DDL_mz.SelectedIndex = DDL_mz.Items.IndexOf(DDL_mz.Items.FindByText(TB_mz.Text));
            //DDL_yx.SelectedIndex = DDL_yx.Items.IndexOf(DDL_yx.Items.FindByValue(TB_yx.Text));
            DDL_zzmm.SelectedIndex = DDL_zzmm.Items.IndexOf(DDL_zzmm.Items.FindByText(TB_zzmm.Text));
        }
    }
    /// <summary>
    /// 更新修改后数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LB_update_Click(object sender, EventArgs e)
    {
        TextBox TB_xm = formView1.FindControl("TB_xm") as TextBox;
        string xm = TB_xm.Text.Trim();
        TextBox TB_xmpy = formView1.FindControl("TB_xmpy") as TextBox;
        string xmpy = TB_xmpy.Text.Trim();
        DropDownList DDL_xb = formView1.FindControl("DDL_xb") as DropDownList;
        string xb = DDL_xb.SelectedItem.Text;
        DropDownList DDL_mz = formView1.FindControl("DDL_mz") as DropDownList;
        string mz = DDL_mz.SelectedItem.Text;
        TextBox TB_sfzjh = formView1.FindControl("TB_sfzjh") as TextBox;
        string sfzjh = TB_sfzjh.Text.Trim();
        TextBox TB_csrq = formView1.FindControl("TB_csrq") as TextBox;
        string csrq = TB_csrq.Text.Trim();
        //DropDownList ddl_yx = formView1.FindControl("ddl_yx") as DropDownList;
        //string yxdm = DDL_xb.SelectedValue;
        DropDownList DDL_zzmm = formView1.FindControl("DDL_zzmm") as DropDownList;
        string zzmm = DDL_zzmm.SelectedValue;
        TextBox TB_lxdh = formView1.FindControl("TB_lxdh") as TextBox;
        string lxdh = TB_lxdh.Text.Trim();
        TextBox TB_email = formView1.FindControl("TB_email") as TextBox;
        string email = TB_email.Text.Trim();
        DropDownList DDL_xl = formView1.FindControl("DDL_xl") as DropDownList;
        string xl = DDL_xl.SelectedValue;
        TextBox TB_xw = formView1.FindControl("TB_xw") as TextBox;
        string xw = TB_xw.Text.Trim();
        TextBox TB_byyx = formView1.FindControl("TB_byyx") as TextBox;
        string byyx = TB_byyx.Text.Trim();
        TextBox TB_byzy = formView1.FindControl("TB_byzy") as TextBox;
        string byzy = TB_byzy.Text.Trim();
        TextBox TB_lxny = formView1.FindControl("TB_lxny") as TextBox;
        string lxny = TB_lxny.Text.Trim();
        TextBox TB_cjqsny = formView1.FindControl("TB_cjqsny") as TextBox;
        string cjqsny = TB_cjqsny.Text.Trim();
        TextBox TB_zw = formView1.FindControl("TB_zw") as TextBox;
        string zw = TB_zw.Text.Trim();
        TextBox TB_jszgzs = formView1.FindControl("TB_jszgzs") as TextBox;
        string jszgzs = TB_jszgzs.Text.Trim();
        TextBox TB_jszc = formView1.FindControl("TB_jszc") as TextBox;
        string jszc = TB_jszc.Text.Trim();
        TextBox TB_zczy = formView1.FindControl("TB_zczy") as TextBox;
        string zczy = TB_zczy.Text.Trim();
        TextBox TB_zcsj = formView1.FindControl("TB_zcsj") as TextBox;
        string zcsj = TB_zcsj.Text.Trim();
        TextBox TB_jndj = formView1.FindControl("TB_jndj") as TextBox;
        string jndj = TB_jndj.Text.Trim();
        TextBox TB_jndjgz = formView1.FindControl("TB_jndjgz") as TextBox;
        string jndjgz = TB_jndjgz.Text.Trim();
        TextBox TB_jndjsj = formView1.FindControl("TB_jndjsj") as TextBox;
        string jndjsj = TB_jndjsj.Text.Trim();
        TextBox TB_llsx = formView1.FindControl("TB_llsx") as TextBox;
        string llsx = TB_llsx.Text.Trim();
        TextBox TB_jczy = formView1.FindControl("TB_jczy") as TextBox;
        string jczy = TB_jczy.Text.Trim();
        TextBox TB_jyzmc = formView1.FindControl("TB_jyzmc") as TextBox;
        string jyzmc = TB_jyzmc.Text.Trim();
        TextBox TB_jxkyzyfx = formView1.FindControl("TB_jxkyzyfx") as TextBox;
        string jxkyzyfx = TB_zcsj.Text.Trim();
        string yhid = Session["UserName"].ToString();
        string sql = "update userdata set xmpy='" + xmpy + "',xb='" + xb + "',mz='" + mz + "',sfzjh='" + sfzjh + "',csrq='" + csrq + "',zzmm='" + zzmm + "',xl='" + xl + "',xw='" + xw + "',byyx='" + byyx + "',byzy='" + byzy + "',lxny='" + lxny + "',cjqsny='"+cjqsny
        + "',zw='" + zw + "',jszgzs='" + jszgzs + "',jszc='" + jszc + "',zczy='" + zczy + "',zcsj='" + zcsj + "',jndj='" + jndj + "',jndjgz='" + jndjgz + "',jndjsj='" + jndjsj + "',llsx='" + llsx + "',jczy='" + jczy + "',jyzdm='" + jyzmc + "',jxkyzyfx='" + jxkyzyfx + "' where yhid='" + yhid + "'";
        try
        {
            if (Sqlhelper.ExcuteNonQuery(sql) == 0) { throw new Exception("更新出错"); }
            Sqlhelper.ExcuteNonQuery("update yonghqx set xm='" + xm + "',lxdh='" + lxdh + "',email='" + email + "' where yhid='"+yhid+"'");
            basic.MsgBox(this.Page, "修改成功", "-1");
            //basic.MsgBox(this.Page, "修改成功", "xingsupdate.aspx?xsid=" + xsid);
        }
        catch (Exception ex)
        {
            throw ex;
            basic.MsgBox(this.Page, "修改失败，如果多次出现，请联系管理员", "-1");
            //basic.MsgBox(this.Page, "修改失败，如果多次出现，请联系管理员", "xingsupdate.aspx?xsid=" + xsid);
            new c_log().logAdd("个人设置", "修改", "登录名=" +yhid + ex.Message);
        }
    }
}
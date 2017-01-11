using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_DistrictUpdate : System.Web.UI.Page
{
    string mode;
    string code;
    string yhid;
    string guid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                mode = Request["mode"].ToString();
            }
            catch (Exception)
            {

                basic.MsgBox(this.Page, "非法访问", "-1");
            }
            if (mode == "edit")
            {
                try
                {
                    code = Request["code"].ToString();
                    DataTable dt = Sqlhelper.Serach("select s.name,y.yhid,y.guid from ZY_DISTRICT s left join yonghqx y on s.userguid=y.guid");
                    TB_xm.Text = dt.Rows[0]["name"].ToString();
                    TB_code.Text = code;
                    TB_code.Enabled = false;
                    yhid = dt.Rows[0]["yhid"].ToString();
                    TB_yhid.Text = yhid;
                    TB_yhid.Enabled = false;
                    guid = dt.Rows[0]["guid"].ToString();
                    LB_insert.Visible = false;
                    LB_up.Visible = true;
                }
                catch (Exception)
                {

                    basic.MsgBox(this.Page, "非法访问", "-1");
                }
                toplink.Text = "当前位置&gt;&gt;&nbsp;修改区县信息";
            }
        }
    }
    /// <summary>
    /// 插入新数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LB_insert_Click(object sender, EventArgs e)
    {
        string guid_new = Guid.NewGuid().ToString();
        string yhid = string.Empty;
        string xm = string.Empty;
        string mm = string.Empty;
        code=TB_code.Text;
        yhid = TB_yhid.Text;
        xm = TB_xm.Text;
        mm = TB_pwd.Text;
        string md5_mm = md5.MD5Encrypt(mm, md5.GetKey());
        string insertDistrict = "insert into ZY_DISTRICT (code,name,userguid) values (@code,@name,@userguid)";
        string insertYonghqx = "insert into yonghqx (guid,yhid,xm,mm) values (@guid,@yhid,@xm,@mm)";
        try
        {
            Sqlhelper.ExcuteNonQuery(insertDistrict, new SqlParameter("code", code), new SqlParameter("name", xm), new SqlParameter("userguid", guid_new));
            Sqlhelper.ExcuteNonQuery(insertYonghqx, new SqlParameter("guid", guid_new), new SqlParameter("yhid", yhid), new SqlParameter("xm", xm), new SqlParameter("mm", md5_mm));
            basic.MsgBox(this.Page, "添加成功", "-1");
        }
        catch (Exception ex)
        {
            basic.MsgBox(this.Page,"添加失败，如果多次出现，请联系管理员", "-1");

        }
    }
    /// <summary>
    /// 修改数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LB_up_Click(object sender, EventArgs e)
    {
        string xm = string.Empty;
        string mm = string.Empty;
        
        xm = TB_xm.Text;
        mm = TB_pwd.Text;
        string md5_mm = md5.MD5Encrypt(mm, md5.GetKey());
        string insertDistrict = "update ZY_DISTRICT set name=@name where code=@code";
        string insertYonghqx = "update yonghqx set xm=@xm,mm=@mm where guid=@guid";
        try
        {
            Sqlhelper.ExcuteNonQuery(insertDistrict, new SqlParameter("code", code), new SqlParameter("name", xm));
            Sqlhelper.ExcuteNonQuery(insertYonghqx, new SqlParameter("guid", guid), new SqlParameter("xm", xm), new SqlParameter("mm", md5_mm));
            basic.MsgBox(this.Page, "修改成功", "-1");
        }
        catch (Exception ex)
        {
            basic.MsgBox(this.Page, "修改失败，如果多次出现，请联系管理员", "-1");

        }
    }
    protected void TB_code_TextChanged(object sender, EventArgs e)
    {
        code = TB_code.Text.Trim();
        try
        {
            DataTable dt = Sqlhelper.Serach("select code from ZY_DISTRICT where code='" + code + "'");
            if (dt.Rows.Count > 0)
            {
                code_tip.Text = "该代码已使用";
                LB_insert.Enabled = false;
            }
            else { code_tip.Text = "该代码可以使用"; yhid_tip.ForeColor = System.Drawing.Color.Green; LB_insert.Enabled = true; }
        }
        catch (Exception)
        {
            code_tip.Text = "数据库错误，请稍后再试";
            LB_insert.Enabled = false;
            
        }
    }
    protected void TB_yhid_TextChanged(object sender, EventArgs e)
    {
        string yhid = TB_yhid.Text.Trim();
        try
        {
            DataTable dt = Sqlhelper.Serach("select yhid from yonghqx where yhid='" + yhid + "'");
            if (dt.Rows.Count > 0)
            {
                yhid_tip.Text = "该用户名已存在";
                LB_insert.Enabled = false;
            }
            else { yhid_tip.Text = "该用户名可以使用"; yhid_tip.ForeColor = System.Drawing.Color.Green; LB_insert.Enabled = true; }
        }
        catch (Exception)
        {
            yhid_tip.Text = "数据库错误，请稍后再试";
            LB_insert.Enabled = false;
            
        }
    }
}
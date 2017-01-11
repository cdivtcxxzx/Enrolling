using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_BanjUpdate : System.Web.UI.Page
{
    string mode;
    string id;
    string yhid;
    string guid;
    string code;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                mode = Request["mode"].ToString();
                code = Request["school"].ToString();
                id = Guid.NewGuid().ToString();
                TB_code.Text = id;
            }
            catch (Exception)
            {

                basic.MsgBox(this.Page, "非法访问", "-1");
            }
            if (mode == "edit")
            {
                try
                {
                    id = Request["id"].ToString();
                    DataTable dt = Sqlhelper.Serach("select s.name,y.yhid,y.guid from zy_banj s left join yonghqx y on s.userguid=y.guid");
                    TB_xm.Text = dt.Rows[0]["name"].ToString();
                    TB_code.Text = id;
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
                toplink.Text = "当前位置&gt;&gt;&nbsp;修改班级信息";
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
        id = TB_code.Text;
        yhid = TB_yhid.Text;
        xm = TB_xm.Text;
        mm = TB_pwd.Text;
        string md5_mm = md5.MD5Encrypt(mm, md5.GetKey());
        string insertSchool = "insert into ZY_BANJ (banj_id,name,school_id,userguid) values (@id,@name,@code,@userguid)";
        string insertYonghqx = "insert into yonghqx (guid,yhid,xm,mm) values (@guid,@yhid,@xm,@mm)";
        try
        {
            Sqlhelper.ExcuteNonQuery(insertSchool, new SqlParameter("id", id), new SqlParameter("name", xm), new SqlParameter("code", code), new SqlParameter("userguid", guid_new));
            Sqlhelper.ExcuteNonQuery(insertYonghqx, new SqlParameter("guid", guid_new), new SqlParameter("yhid", yhid), new SqlParameter("xm", xm), new SqlParameter("mm", md5_mm));
            basic.MsgBox(this.Page, "添加成功", "-1");
        }
        catch (Exception ex)
        {
            basic.MsgBox(this.Page, "添加失败，如果多次出现，请联系管理员", "-1");

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
        string insertSchool = "update ZY_BANJ set name=@name where banj_id=@id";
        string insertYonghqx = "update yonghqx set xm=@xm,mm=@mm where guid=@guid";
        try
        {
            Sqlhelper.ExcuteNonQuery(insertSchool, new SqlParameter("id", id), new SqlParameter("name", xm));
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
        id = TB_code.Text.Trim();
        try
        {
            DataTable dt = Sqlhelper.Serach("select school_id from ZY_SCHOOL where school_id='" + id + "'");
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
        string yhid_new = TB_yhid.Text.Trim();
        if (yhid_new == yhid) { return; }
        try
        {
            DataTable dt = Sqlhelper.Serach("select yhid from yonghqx where yhid='" + yhid_new + "'");
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
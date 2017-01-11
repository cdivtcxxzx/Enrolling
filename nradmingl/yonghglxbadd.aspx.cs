using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class admin_yonghglxbadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //管理筛选
        this.SqlDataSource3.FilterExpression = new Power().GetFilterExpression("yxdm");
    }
    /// <summary>
    /// 插入新数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LB_insert_Click(object sender, EventArgs e)
    {
        string guid = Guid.NewGuid().ToString();
        string yhid = string.Empty;
        string xm = string.Empty;
        string mm = string.Empty;
        string yhdh = string.Empty;
        string yxdm = string.Empty;
        string zjh = string.Empty;
        string qq = string.Empty;
        yhid = TB_yhid.Text;
        xm = TB_xm.Text;
        mm = TB_pwd.Text;
        string md5_mm = md5.MD5Encrypt(mm, md5.GetKey());
        yhdh = TB_yhdh.Text;
        zjh = TB_zjh.Text;
        yxdm = DropDownList3.SelectedValue;
        string insertYonghqx = "insert into yonghqx (guid,yhid,xm,mm,lxdh,yhdh,yxdm) values (@guid,@yhid,@xm,@mm,@lxdh,@yhdh,@yxdm)";
        try
        {
            Sqlhelper.ExcuteNonQuery(insertYonghqx, new SqlParameter("guid", guid), new SqlParameter("yhid", yhid), new SqlParameter("xm", xm), new SqlParameter("mm", md5_mm), new SqlParameter("lxdh", zjh), new SqlParameter("yhdh", yhdh), new SqlParameter("yxdm", yxdm));
            basic.MsgBox(this.Page, "添加成功", "-1");
        }
        catch (Exception ex)
        {
            basic.MsgBox(this.Page, "添加失败，如果多次出现，请联系管理员", "-1");

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
            throw;
        }
    }
}
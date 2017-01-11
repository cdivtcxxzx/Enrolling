using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections;

public partial class admin_yhupdate : System.Web.UI.Page
{
    string yhid;
    DataTable qx;
    protected void Page_Load(object sender, EventArgs e)
    {
        qx = Sqlhelper.Serach("select qxid,qxmc from quanxdm");
        yhid = Request.QueryString["yhid"];
        if (!IsPostBack)
        {

            RepeaterBind();
            checkZhuPower(yhid);
            CheckYxmc(yhid);
            CheckYhqx(yhid);
        }

    }
    /// <summary>
    /// 重置uum密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void resetPwd_OnClick(object sender, EventArgs e)
    {
        TextBox textbox = this.formView1.FindControl("TB_PWD") as TextBox;
        if (new Account().ResetPwd(Request.QueryString["yhid"], textbox.Text)==1)
        {
            basic.MsgBox(this.Page, "重置成功","-1");
        }
        else basic.MsgBox(this.Page, "重置失败", "-1");
    }
    /// <summary>
    /// 通过权限id获得权限名
    /// </summary>
    /// <param name="qxdm">权限代码</param>
    /// <returns></returns>
    protected string GetQxmc(string qxdm)
    {
        DataRow[] drs = qx.Select("qxid='" + qxdm + "'");
        if (drs.Count() > 0)
        {
            string qxmc = drs[0]["qxmc"].ToString();
            return qxmc;
        }
        else if (qxdm == "0") { return "不需要分配权限"; } else { return "未知权限名"; }
    }
    /// <summary>
    /// 产生相应栏目的权限数据源
    /// </summary>
    /// <param name="sfqxyz">相应栏目的sfqxyz字段内容</param>
    /// <returns></returns>
    protected DataTable GetRep(string sfqxyz)
    {
        if (sfqxyz == "0") { return null; }
        string[] qxs = sfqxyz.TrimEnd(',').Split(',');
        DataTable dt = new DataTable();
        dt.Columns.Add("qxdm", typeof(string));
        //dt.Columns.Add("qxid", typeof(string));
        foreach (string x in qxs)
        {
            DataRow dr = dt.NewRow();
            dr[0] = x;
            //dr[1] = x;
            dt.Rows.Add(dr);
        }
        return dt;
    }
    /// <summary>
    /// 组和管理数据权限绑定数据
    /// </summary>
    protected void RepeaterBind()
    {
        DataTable dt = new Power().ZhuPower();
        DataTable yxmc = Sqlhelper.Serach("select * from dm_yuanxi where  yxdm<>'9999' order by px");
        YHZ.DataSource = dt;
        YHZ.DataBind();
        glyx.DataSource = yxmc;
        glyx.DataBind();
    }
    /// <summary>
    /// 更新用户权限
    /// </summary>
    protected void yhqxUpdate_Click(object sender, EventArgs e)
    {
        string xmlString = "<Root></Root>";
        XDocument temp = XDocument.Parse(xmlString);
        XElement tempXE = null;
        //用于判断是否一个权限都没
        int k = 0;
        //ArrayList lanmCheckBoxID = Session["lanmCheckBoxID"] as ArrayList;
        //foreach (var y in lanmCheckBoxID)
        //{

        //    CheckBox cb = Panel1.FindControl(y.ToString()) as CheckBox;
        //    if (cb.Checked)
        //    {

        //        string lmid = y.ToString().Split('_')[0];
        //        string lmmc = y.ToString().Split('_')[1];
        //        if (temp.Element("Root").Element(lmmc) == null)
        //        {
        //            tempXE = new XElement(lmmc);
        //            tempXE.SetAttributeValue("ID", lmid);
        //            temp.Element("Root").Add(tempXE);
        //        }
        //        temp.Element("Root").Element(lmmc).Add(new XElement(cb.Text));
        //        k++;

        //    }

        //}

        foreach (GridViewRow x in GV_qx.Rows)
        {
            Repeater rep = x.FindControl("Rep") as Repeater;
            string lmid = x.Cells[0].Text;
            string gjz = x.Cells[2].Text;

            for (int i = 0; i < rep.Items.Count; i++)
            {
                CheckBox CB_qx = rep.Items[i].FindControl("CB_qx") as CheckBox;

                if (CB_qx.Checked)
                {
                    if (temp.Element("Root").Element(gjz) == null)
                    {
                        tempXE = new XElement(gjz);
                        tempXE.SetAttributeValue("ID", lmid);
                        temp.Element("Root").Add(tempXE);
                    }
                    temp.Element("Root").Element(gjz).Add(new XElement(CB_qx.Text));
                    k++;
                }

            }
        }
        //循环添加管理数据的系部
        temp.Element("Root").Add(new XElement("能操作数据的系部"));
        for (int i = 0; i < glyx.Controls.Count; i++)
        {
            CheckBox glyxCB = glyx.Items[i].FindControl("yx") as CheckBox;

            if (glyxCB.Checked)
            {
                temp.Element("Root").Element("能操作数据的系部").Add(new XElement(glyxCB.Text));
                k++;
            }

        }
        if (k == 0) { temp = new XDocument(); }
        string lsz = string.Empty;
        for (int i = 0; i < YHZ.Controls.Count; i++)
        {
            CheckBox lszCB = YHZ.Items[i].FindControl("lsz") as CheckBox;
            if (lszCB.Checked)
            {
                lsz = lsz + lszCB.Text + ",";
            }
        }
        lsz = new Power().getZhuIDs(lsz);
        if (lsz == null) lsz = "";
        //更新用户权限
        if (Sqlhelper.ExcuteNonQuery("update yonghqx set yhqx=@yhqx,lsz=@lsz where yhid=@yhid", new SqlParameter("yhqx", temp.ToString()), new SqlParameter("lsz", lsz), new SqlParameter("yhid", yhid)) > 0)
        {
            basic.MsgBox(this.Page, "更新成功", "-1");
        }
        else basic.MsgBox(this.Page, "更新失败", "-1");
    }
    /// <summary>
    /// 检查隶属组，并把相应checkbox打钩
    /// </summary>
    /// <param name="_yhid">用户登录名</param>
    protected void checkZhuPower(string _yhid)
    {
        DataTable dt = Sqlhelper.Serach("select lsz from yonghqx where yhid=@yhid", new SqlParameter("yhid", _yhid));
        string lsz = dt.Rows[0][0].ToString();
        if (lsz != "")
        {
            lsz = new Power().getZhuMCs(lsz);
            string[] lszs = new Power().getFromQx(lsz);
            for (int i = 0; i < YHZ.Controls.Count; i++)
            {
                CheckBox lszCB = YHZ.Items[i].FindControl("lsz") as CheckBox;
                if (lszs != null)
                {
                    foreach (string x in lszs)
                    {
                        if (x == lszCB.Text) { lszCB.Checked = true; }
                    }
                }
            }
        }
    }
    /// <summary>
    /// 检查用户权限，并把相应的CheckBox打钩
    /// </summary>
    /// <param name="_yhid"></param>
    protected void CheckYhqx(string _yhid)
    {
        foreach (GridViewRow x in GV_qx.Rows)
        {
            Repeater rep = x.FindControl("Rep") as Repeater;
            string lmid = x.Cells[0].Text;
            string gjz = x.Cells[2].Text;

            for (int i = 0; i < rep.Items.Count; i++)
            {
                CheckBox CB_qx = rep.Items[i].FindControl("CB_qx") as CheckBox;

                if (new Power().powerYanzheng(_yhid, gjz, CB_qx.Text, "1"))
                {
                    CB_qx.Checked = true;
                }

            }
        }
    }
    /// <summary>
    /// 检查能管理数据的系部，并把相应checkbox打钩
    /// </summary>
    /// <param name="_yhid">用户登录名</param>
    protected void CheckYxmc(string _yhid)
    {
        DataTable dt = Sqlhelper.Serach("select yhqx from yonghqx where yhid=@yhid", new SqlParameter("yhid", _yhid));
        ArrayList al = new Power().GetYxmcsByYhid(_yhid);
        if (al.Count > 0)
        {
            for (int i = 0; i < glyx.Controls.Count; i++)
            {
                CheckBox glyxCB = glyx.Items[i].FindControl("yx") as CheckBox;

                foreach (string x in al)
                {
                    if (x == glyxCB.Text) { glyxCB.Checked = true; }
                }

            }
        }
    }
}
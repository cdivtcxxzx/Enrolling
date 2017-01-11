using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Xml.Linq;

public partial class admin_showPower : System.Web.UI.Page
{
    string yhid;
    DataTable qx;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!new c_login().Loginyanzheng()) { Response.Redirect("/Default.aspx", true); return; }
        qx = Sqlhelper.Serach("select qxid,qxmc from quanxdm");
        yhid = Session["UserName"].ToString();
        if (!IsPostBack)
        {
            string yhqx=Session["Yhqx"].ToString();
            string fe = "2=3";
            XDocument xml_yhqx=XDocument.Parse(yhqx);
            XElement lms=xml_yhqx.Element("Root");
            foreach (XElement x in lms.Elements())
            {
                fe += " or gjz='" + x.Name+"'";
            }
            Sql_qx.FilterExpression = fe;
            RepeaterBind();
            //checkZhuPower(yhid);
            //CheckYxmc(yhid);
            //CheckYhqx(yhid);
        }

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
    protected DataTable GetRep(string sfqxyz,string gjz)
    {
        string yhqx = Session["Yhqx"].ToString();
        XDocument xml_yhqx = XDocument.Parse(yhqx);
        if (sfqxyz == "0") { return null; }
        string[] qxs = sfqxyz.TrimEnd(',').Split(',');
        DataTable dt = new DataTable();
        dt.Columns.Add("qxdm", typeof(string));
        //dt.Columns.Add("qxid", typeof(string));
        foreach (string x in qxs)
        {
            if (!xml_yhqx.Element("Root").Element(gjz).ToString().Contains(GetQxmc(x))) { continue; }
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
        DataTable yxmc = Sqlhelper.Serach("select * from dm_yuanxi where zt=1");
        string dt_ex = "1=2";
        if (Session["lsz"].ToString() != "")
        {
            string[] lszs = Session["lsz"].ToString().Trim(',').Split(',');

            foreach (string lsz in lszs)
            {
                dt_ex += " or zid=" + lsz;
            }
        }
        if (dt.Select(dt_ex).Count() > 0)
        {
            YHZ.DataSource = dt.Select(dt_ex).CopyToDataTable();
        }
        else { YHZ.DataSource = null; }
        YHZ.DataBind();
        if (yxmc.Select(new Power().GetFilterExpression("yxdm")).Count()>0)
        {
        glyx.DataSource = yxmc.Select(new Power().GetFilterExpression("yxdm")).CopyToDataTable();}
        else{glyx.DataSource=null;}
        glyx.DataBind();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

public partial class admin_yonghsq : System.Web.UI.Page
{
    string yhid;
    DataTable qx;

    #region 页面初始化参数

    private string pagelm1 = "教师管理";

    private string pageqx1 = "浏览";
    private string pageqx2 = "重置密码";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "yonghsq.aspx";//页面值

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //登陆验证,权限验证,日志
        new c_login().tongyiyz(pagelm1, pageqx1, "进入用户管理权限分配页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
        qx = Sqlhelper.Serach("select qxid,qxmc from quanxdm");
        yhid = Request.QueryString["yhid"]; 
        if (!IsPostBack)
        {
            string yhqx = Session["Yhqx"].ToString();
            string fe = "2=3";
            XDocument xml_yhqx = XDocument.Parse(yhqx);
            XElement lms = xml_yhqx.Element("Root");
            foreach (XElement x in lms.Elements())
            {
                fe += " or gjz='" + x.Name + "'";
            }
            Sql_qx.FilterExpression = fe;
            RepeaterBind();
            checkZhuPower(yhid);
            CheckYxmc(yhid);
            CheckYhqx(yhid);
            checkjxxb(yhid);
        }
    }

    //protected void CheckQx(string _yhid)
    //{
    //    foreach (TreeNode x in Tree_qx.Nodes[0].ChildNodes)
    //    {
    //        x.chil
    //        new Power().powerYanzheng(_yhid, "", "", "1");
    //    }
    //}
    /// <summary>
    /// 重置uum密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void resetPwd_OnClick(object sender, EventArgs e)
    {
        //登陆验证,权限验证,日志
        new c_login().tongyiyz(pagelm1, pageqx2, "重置教师密码", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
       
        TextBox textbox = this.formView1.FindControl("TB_PWD") as TextBox;
        if (new Account().ResetPwd(Request.QueryString["yhid"], textbox.Text)==1)
        {
            string xx = "<script>alert('重置成功');windows.navigate('yhupdate.aspx');</script>";
            Response.Write(xx);
        }
        else Response.Write("<script>alert('修改失败');</script>");
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
    protected DataTable GetRep(string sfqxyz, string gjz)
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
        DataTable yxmc = Sqlhelper.Serach("select * from dm_yuanxi");
        string dt_ex = "1=2";
       
        if (Session["lsz"].ToString() != "")
        {
            string[] lszs = Session["lsz"].ToString().TrimEnd(',').Split(',');

            foreach (string lsz in lszs)
            {
                dt_ex += " or zid=" + lsz;
            }
        }
        YHZ.DataSource = dt;//绑定全部组


        //if (dt.Select(dt_ex).Count() > 0)
        //{
        //    YHZ.DataSource = dt.Select(dt_ex).CopyToDataTable();
        //}
        //else { YHZ.DataSource = null; }
        YHZ.DataBind();
        if (dt.Select(dt_ex).Count() > 0)
        {
            DataTable yhzl = dt.Select(dt_ex).ParseToDataTable();
            if (yhzl.Rows.Count > 0)
            {
                for (int i = 0; i < YHZ.Controls.Count; i++)
                {
                    CheckBox lszCB = YHZ.Items[i].FindControl("lsz") as CheckBox;
                
                        for (int x = 0; x < yhzl.Rows.Count; x++)
                        {
                            if (yhzl.Rows[x]["zmc"].ToString() == lszCB.Text) { lszCB.Enabled = true; }
                        }

                    
                }

            }
        }
        else { YHZ.DataSource = null; }
        glyx.DataSource = Sqlhelper.Serach("select * from dm_yuanxi");
        glyx.DataBind();
        //使管理数据变亮
        if (yxmc.Select(new Power().GetFilterExpression("yxdm")).Count() > 0)
        {


            DataTable glyxhs = yxmc.Select(new Power().GetFilterExpression("yxdm")).ParseToDataTable();
            for (int i = 0; i < glyx.Controls.Count; i++)
            {
                CheckBox lszCB = glyx.Items[i].FindControl("yx") as CheckBox;
                if (glyxhs.Rows.Count > 0)
                {
                    for (int x = 0; x < glyxhs.Rows.Count; x++)
                    {
                        if (glyxhs.Rows[x]["yxmc"].ToString() == lszCB.Text) { lszCB.Enabled = true; }
                    }

                }
            }
        }
       
        DataTable yxmcjx = Sqlhelper.Serach("select * from dm_yuanxi where isjx='1'");
        jxxbz.DataSource = yxmcjx;
        jxxbz.DataBind();
        //使checkbox变亮


        if (yxmcjx.Select(new Power().GetFilterExpression("yxdm")).Count() > 0)
        {
            DataTable czyxmcjx = yxmcjx.Select(new Power().GetFilterExpression("yxdm")).ParseToDataTable();
            
                for (int i = 0; i < jxxbz.Controls.Count; i++)
                {
                    CheckBox lszCB = jxxbz.Items[i].FindControl("jxxb") as CheckBox;
                    if (czyxmcjx.Rows.Count > 0)
                    {
                        for (int x = 0; x < czyxmcjx.Rows.Count; x++)
                        {
                            if (czyxmcjx.Rows[x]["yxmc"].ToString()== lszCB.Text) { lszCB.Enabled = true; }
                        }
                       
                    }
                }
                
        }

        
        
        //jxxbz.DataBind();
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
    /// 检查上课的系部，并把相应checkbox打钩
    /// </summary>
    /// <param name="_yhid">用户登录名</param>
    protected void checkjxxb(string _yhid)
    {
        DataTable dt = Sqlhelper.Serach("select pjjxbmdm from yonghqx where yhid=@yhid", new SqlParameter("yhid", _yhid));
        string lsz = dt.Rows[0][0].ToString();
        if (lsz != "")
        {
            lsz = new Power().getjxMCs(lsz);
            string[] lszs = new Power().getFromQx(lsz);
            for (int i = 0; i < jxxbz.Controls.Count; i++)
            {
                CheckBox lszCB = jxxbz.Items[i].FindControl("jxxb") as CheckBox;
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
        string jxxb = string.Empty;
        for (int i = 0; i < jxxbz.Controls.Count; i++)
        {
            CheckBox lszCB = jxxbz.Items[i].FindControl("jxxb") as CheckBox;
            if (lszCB.Checked)
            {
                jxxb = jxxb + lszCB.Text + ",";
            }
        }
        jxxb = new Power().getjxids(jxxb);
        if (jxxb == null) jxxb = "";
  
        //更新用户权限
        if (Sqlhelper.ExcuteNonQuery("update yonghqx set yhqx=@yhqx,pjjxbmdm=@jxxb,lsz=@lsz where yhid=@yhid", new SqlParameter("yhqx", temp.ToString()),new SqlParameter("jxxb",jxxb), new SqlParameter("lsz", lsz), new SqlParameter("yhid", yhid)) > 0)
        {
            basic.MsgBox(this.Page, "更新成功", "-1");
        }
        else basic.MsgBox(this.Page, "更新失败", "-1");
    }



}
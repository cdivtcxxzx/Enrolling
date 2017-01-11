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

public partial class admin_yonghzUpdate : System.Web.UI.Page
{
    protected string zid;
    DataTable qx;
    protected void Page_Load(object sender, EventArgs e)
    {
        zid = Request.QueryString["zid"];
        qx = Sqlhelper.Serach("select qxid,qxmc from quanxdm");
        if (zid == null) { Response.Write("请使用正规方法访问"); Response.Redirect("~/default.aspx"); }
        if (!IsPostBack)
        {
            
            RepeaterBind();
            CheckYxmc(zid);
            CheckLmqx(zid);
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
        DataTable yxmc = Sqlhelper.Serach("select * from dm_yuanxi where zt=1 and yxdm<>'9999' order by px");
        glyx.DataSource = yxmc;
        glyx.DataBind();
    }

    /// <summary>
    /// formview绑定数据
    /// </summary>
    protected void FormBind()
    {
        DataTable dt = ViewState["formView1"] as DataTable;
        formView1.DataSource = dt;
        formView1.DataBind();
    }


    /// <summary>
    /// 动态添加栏目权限表(废除)
    /// </summary>>
    /// <param name="_zid">组id</param>
    protected void lanmqxTable(string _zid)
    {
        Table tempTable = new Power().LanmPowerTable(_zid,false);//得到栏目权限列表
        TableRow spanRow = new TableRow();
        TableCell spanCell = new TableCell();
        spanCell.RowSpan = tempTable.Rows.Count + 2;
        spanCell.Text = "用户权限&nbsp;";
        spanCell.Width = 120;
        spanRow.Cells.Add(spanCell);
        tempTable.Rows.AddAt(0, spanRow);//添加左边一栏
        TableRow trLast = new TableRow();
        TableCell tempTC = new TableCell();
        Button bt = new Button();
        bt.ID = "yonghzUpdate";
        bt.Text = "确认添加";
        bt.CssClass = "button";
        bt.Click += new EventHandler(yonghzUpdate_Click);//设置button的点击事件
        tempTC.Controls.Add(bt);
        trLast.Cells.Add(tempTC);
        trLast.Cells.Add(new TableCell());
        trLast.Cells[1].Text = "<input type=\"reset\" class=\"button\" value=\"重置\" />";
        trLast.Cells.Add(new TableCell());
        trLast.Cells[2].Text = "&nbsp;";
        //trLast.Cells.Add(new TableCell());
        //trLast.Cells[3].Text = "&nbsp;";
        tempTable.Rows.Add(trLast);//添加最后一行
        Panel1.Controls.Add(tempTable);//栏目权限表添加到panel1控件
    }
    /// <summary>
    /// 更新用户组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void yonghzUpdate_Click(object sender, EventArgs e)
    {
        TextBox cb1 = formView1.FindControl("TB_ZhuName") as TextBox;
        string zmc = cb1.Text;
        TextBox cb2 = formView1.FindControl("TB_ZhuSM") as TextBox;
        string zsm = cb1.Text;
        string zqx = null;
        string xmlString = "<Root></Root>";
        XDocument temp = XDocument.Parse(xmlString);
        XElement tempXE = null;
        //用于判断是否一个权限都没
        int k = 0;
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
        zqx = temp.ToString();
        //更新用户组
        if (Sqlhelper.ExcuteNonQuery("update zhuqx set zmc=@zmc,zsm=@zsm,zqx=@zqx where zid=@zid", new SqlParameter("zmc", zmc), new SqlParameter("zsm", zsm), new SqlParameter("zqx", zqx), new SqlParameter("zid", zid)) > 0)
        {
            basic.MsgBox(this.Page, "添加成功", "-1");
        }
        else basic.MsgBox(this.Page, "添加失败", "-1");
    }

    /// <summary>
    /// 检查用户权限，并把相应的CheckBox打钩
    /// </summary>
    /// <param name="_zid"></param>
    protected void CheckLmqx(string _zid)
    {
        foreach (GridViewRow x in GV_qx.Rows)
        {
            Repeater rep = x.FindControl("Rep") as Repeater;
            string lmid = x.Cells[0].Text;
            string gjz = x.Cells[2].Text;

            for (int i = 0; i < rep.Items.Count; i++)
            {
                CheckBox CB_qx = rep.Items[i].FindControl("CB_qx") as CheckBox;

                if (new Power().powerYanzheng(_zid, gjz, CB_qx.Text))
                {
                    CB_qx.Checked = true;
                }

            }
        }
    }
    /// <summary>
    /// 检查能管理数据的系部，并把相应checkbox打钩
    /// </summary>
    /// <param name="_zid">组id</param>
    protected void CheckYxmc(string _zid)
    {
        DataTable dt = Sqlhelper.Serach("select zqx from zhuqx where zid=@zid", new SqlParameter("zid", _zid));
        ArrayList al = new Power().GetYxmcsByZid(_zid);
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
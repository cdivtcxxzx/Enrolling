using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

public partial class admin_yonghzadd : System.Web.UI.Page
{
    DataTable qx;
    protected void Page_Load(object sender, EventArgs e)
    {
        qx = Sqlhelper.Serach("select qxid,qxmc from quanxdm");
        if (!IsPostBack)
        {
            
            RepeaterBind();
            
        }
        //lanmqxTable();

    }
    /// <summary>
    /// 管理数据绑定数据
    /// </summary>
    protected void RepeaterBind()
    {
        DataTable yxmc = Sqlhelper.Serach("select * from dm_yuanxi");
        glyx.DataSource = yxmc;
        glyx.DataBind();
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
    /// 动态添加栏目权限表
    /// </summary>>
    protected void lanmqxTable()
    {
        Table tempTable = new Power().LanmPowerTable();//得到栏目权限列表
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
        bt.ID = "yonghzAdd";
        bt.Text = "确认添加";
        bt.CssClass = "button";
        bt.Click += new EventHandler(yonghzAdd_Click);//设置button的点击事件
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
    /// 添加新用户组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void yonghzAdd_Click(object sender, EventArgs e)
    {
        TextBox cb1 = form1.FindControl("TB_ZhuName") as TextBox;
        string zmc=cb1.Text;
        TextBox cb2 = form1.FindControl("TB_ZhuSM") as TextBox;
        string zsm = cb1.Text;
        string zqx=null;
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
        //添加新用户组
        if (Sqlhelper.ExcuteNonQuery("insert into zhuqx (zmc,zsm,zqx) values (@zmc,@zsm,@zqx)", new SqlParameter("zmc", zmc), new SqlParameter("zsm", zsm), new SqlParameter("zqx", zqx)) > 0)
        {
            basic.MsgBox(this.Page, "添加成功", "-1");
        }
        else basic.MsgBox(this.Page, "添加失败", "-1");
    }
}
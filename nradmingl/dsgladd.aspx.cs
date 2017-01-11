using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_dsgladd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["type"] != null)
            {
                if (Request["type"].ToString() == "edit")
                {
                    this.tijiao.Text = "确认修改";
                    DataTable x = Sqlhelper.Serach("SELECT    row_number() over (order by id) as uid, id,name,nd,zbdw,starttime,overtime,bz from ds_saishi where id=" + Request["id"].ToString() + " order by nd desc ");
                    if (x.Rows.Count > 0)
                    {
                        this.ssname.Text = x.Rows[0]["name"].ToString();
                        this.zbdw.Text = x.Rows[0]["zbdw"].ToString();
                        this.starttime.Text = x.Rows[0]["starttime"].ToString();
                        this.overtime.Text = x.Rows[0]["overtime"].ToString();
                        this.Label1.Text = x.Rows[0]["id"].ToString();

                    }
                }
            }
        }
    }
    protected void LB_insert_Click(object sender, EventArgs e)
    {
        if (this.tijiao.Text!= "确认修改")
        {
            //添加
            if (Sqlhelper.ExcuteNonQuery("insert into ds_saishi (name,nd,zbdw,starttime,overtime)values('" + this.ssname.Text + "','" + this.DropDownList4.SelectedValue + "','" + this.zbdw.Text + "','" + this.starttime.Text + "','" + this.overtime.Text + "')") > 0)
            {
                this.alertMessage.Value = "<font color=green>添加成功，请返回或继续添加！" + " </font>";
            }
            else
            {
                this.alertMessage.Value = "<font color=red>添加失败，请重试！" + " </font>";
            }
        }
        else
        {
            //修改
            if (Sqlhelper.ExcuteNonQuery("update ds_saishi set name='"+this.ssname.Text+"',nd='"+DropDownList4.SelectedValue+"',zbdw='"+this.zbdw.Text+"' where id="+Request["id"].ToString()+"") > 0)
            {
                Response.Write("update ds_saishi set name='"+this.ssname.Text+"',nd='"+DropDownList4.SelectedValue+"',zbdw='"+this.zbdw.Text+"' where id="+Request["id"].ToString());
                this.alertMessage.Value = "<font color=green>修改成功，请返回或继续添加！" + " </font>";
            }
            else
            {
                this.alertMessage.Value = "<font color=red>修改失败，请重试！" + " </font>";
            }

        }
    }
}
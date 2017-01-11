using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_niandxq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //年度
        if (!IsPostBack)
        {
            int year1 = DateTime.Now.Year;
            this.DropDownList1.Items.Insert(0, year1.ToString());
            this.DropDownList1.Items.Add(new ListItem((year1 + 1).ToString(), (year1 + 1).ToString()));
            this.DropDownList1.Items.Add(new ListItem((year1 - 1).ToString(), (year1 - 1).ToString()));
            this.DropDownList1.Items.Add(new ListItem((year1 - 2).ToString(), (year1 - 2).ToString()));
            this.DropDownList1.Items.Add(new ListItem((year1 - 3).ToString(), (year1 - 3).ToString()));
            searchtext0.Text = new Degree().getXueQi();
        }
        
    }
    protected void Search_Onclick(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text != ""&&this.searchtext0.Text != "")//判断前三个文本框不能为空
        {
            string sqlstring = "SELECT * FROM DM_ndxueqi WHERE nd=" + "'" + DropDownList1.SelectedItem.Text.Trim() + "' and xueqi='" + searchtext0.Text.Trim() + "' and sys='教学评价系统'";
            DataTable dt = new DataTable();
            dt = Sqlhelper.Serach(sqlstring);
            if (dt.Rows.Count == 0)//判断关键字重复
            {
                string Strsql;
                Strsql = "insert into DM_ndxueqi(nd,xueqi,sys) values(" + "'" + DropDownList1.SelectedItem.Text.Trim() + "'," + "'" + searchtext0.Text.Trim() + "','教学评价系统')";
                SqlConnection zjconn = new SqlConnection(Sqlhelper.conStr);
                SqlCommand zjcmd = new SqlCommand(Strsql, zjconn);
                zjconn.Open();
                zjcmd.ExecuteNonQuery();
                zjconn.Close();
                this.alertMessage.Value = "<span style=\"font-size:Large;\"><font color=green>添加成功！</font></span>";
                //Response.Write("<Script Language='JavaScript'>alert('添加成功');</Script>");
            }
            else
            {
                this.alertMessage.Value = "<span style=\"font-size:Large;\"><font color=red>年度和学期重复!！</font></span>";
                // Response.Write("<Script Language='JavaScript'>alert('年度和学期重复!');</Script>");
            }
            GridView1.DataBind();
           
        }
            
        //Sqlhelper.conStr;//向网站信息配置表写记录//向网站信息配置表写记录

        else
        { 
            Response.Write("<script>alert('年度和学期不能为空');window.navigate('niandxq.aspx')</script>"); }

    }
    
}
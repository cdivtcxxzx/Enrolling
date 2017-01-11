using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using myControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_zhandgl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void Button1_Click(object sender, EventArgs e)
    {
       
        if ((TextBox4.Text != "") && (TextBox3.Text != "") && (TextBox5.Text != ""))//判断前三个文本框不能为空
        {
            string sqlstring = "SELECT * FROM wangzxx WHERE xxgjz=" + "'" + TextBox3.Text.Trim() + "'";
            DataTable dt = new DataTable();
            dt = Sqlhelper.Serach(sqlstring);
            if (dt.Rows.Count == 0)//判断关键字重复
            {
                string Strsql;
                Strsql = "insert into wangzxx(title,xxgjz,xxnr,xxpx,xxbz) values(" + "'" + TextBox4.Text.Trim() + "'," + "'" + TextBox3.Text.Trim() + "'," + "'" + TextBox5.Text.Trim() + "'," + Convert.ToInt32(TextBox6.Text.Trim()) + "," + "'" + TextBox7.Text.Trim() + "')";
                SqlConnection zjconn = new SqlConnection(Sqlhelper.conStr);
                SqlCommand zjcmd = new SqlCommand(Strsql, zjconn);
                zjconn.Open();
                zjcmd.ExecuteNonQuery();
                zjconn.Close();
                Response.Write("<Script Language='JavaScript'>alert('添加记录成功');window.navigate('zhandgl.aspx');</Script>");
            }
            else
            { Response.Write("<Script Language='JavaScript'>alert('配置关键字重复');window.navigate('zhandgl.aspx');</Script>"); }
           
        }
        //Sqlhelper.conStr;//向网站信息配置表写记录//向网站信息配置表写记录

        else
        { Response.Write("<script>alert('配置标题、关键字和信息内容不能为空');window.navigate('zhandgl.aspx')</script>"); }

    }
}
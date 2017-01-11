using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_yonghbmgl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Search_Onclick(object sender, EventArgs e)
    {
        if (searchtext.Text != ""&&this.searchtext0.Text != "")//判断前三个文本框不能为空
        {
            string sqlstring = "SELECT * FROM dm_yuanxi WHERE yxmc=" + "'" + searchtext.Text.Trim() + "' or yxdm='"+searchtext0.Text.Trim()+"'";
            DataTable dt = new DataTable();
            dt = Sqlhelper.Serach(sqlstring);
            if (dt.Rows.Count == 0)//判断关键字重复
            {
                string Strsql;
                Strsql = "insert into dm_yuanxi(yxmc,yxdm) values(" + "'" + searchtext.Text.Trim() + "'," + "'" + searchtext0.Text.Trim() + "')";
                SqlConnection zjconn = new SqlConnection(Sqlhelper.conStr);
                SqlCommand zjcmd = new SqlCommand(Strsql, zjconn);
                zjconn.Open();
                zjcmd.ExecuteNonQuery();
                zjconn.Close();
                Response.Write("<Script Language='JavaScript'>alert('添加成功');window.navigate('yonghbmgl.aspx');</Script>");
            }
            else
            { Response.Write("<Script Language='JavaScript'>alert('部门名称或代码重复!');window.navigate('yonghbmgl.aspx');</Script>"); }
           
        }
        //Sqlhelper.conStr;//向网站信息配置表写记录//向网站信息配置表写记录

        else
        { Response.Write("<script>alert('代码和部门名称不能为空');window.navigate('yonghbmgl.aspx')</script>"); }

    }
    
}
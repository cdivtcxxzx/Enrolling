using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class zmtest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

            
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
if (dormitory.isbillet(xh.Text.Trim()))
            {
                Response.Write("已分配寝室！");
            }
            else
            {
                Response.Write("未分配寝室！");
            }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable bit = dormitory.billetdata(xh.Text.Trim());
        if (bit.Rows.Count > 0)
        {
            for (int i = 0; i < bit.Columns.Count;i++ )
                Response.Write(bit.Rows[0][i].ToString()+"<br>");
        }
        else
        {
            Response.Write("获取数据为空！");
        }
    }
}
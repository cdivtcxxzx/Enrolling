using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class admin_ptset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //管理筛选
        //this.SqlDataSource3.FilterExpression = new Power().GetFilterExpression("yxdm");
    }
    /// <summary>
    /// 插入新数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void LB_insert_Click(object sender, EventArgs e)
    {
      
    }
    protected void TB_yhid_TextChanged(object sender, EventArgs e)
    {
      
    }
}
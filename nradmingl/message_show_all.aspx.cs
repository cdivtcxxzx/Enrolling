using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class admin_message_show_all : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Search_Onclick(object sender, ImageClickEventArgs e)
    {
        GV_new.DataSourceID = "messageSearch";
    }
    /// <summary>
    /// 点击页面中的按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region 删除成员
        if (e.CommandName == "shanchu")
        {
            
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow selectedRow = GV_new.Rows[index];
            TableCell tcid = selectedRow.Cells[1];
            string id = tcid.Text;
            Sqlhelper.ExcuteNonQuery("delete from message where  id=@id", new SqlParameter("id", id));
            GV_new.DataBind();
        }
        #endregion
    }
}
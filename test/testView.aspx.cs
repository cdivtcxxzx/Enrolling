using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using model;

public partial class test_testView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Fresh_Room_Type> listgetroomtype = dormitory.listgetroomtype("1");
        Response.Write(listgetroomtype);
        testDd.DataSource = listgetroomtype;
        testDd.DataTextField = "Type_Name";
        testDd.DataValueField = "Type_NO";
        testDd.DataBind();
        testDd.Items.Insert(0, new ListItem(listgetroomtype[0].Type_NO, "-1"));
    }
}
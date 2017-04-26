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
        //测试下拉列表填充
        //List<Fresh_Room_Type> listgetroomtype = dormitory.listgetroomtype("1");
        //Response.Write(listgetroomtype);
        //testDd.DataSource = listgetroomtype;
        //testDd.DataTextField = "Type_Name";
        //testDd.DataValueField = "Type_NO";
        //testDd.DataBind();
        //testDd.Items.Insert(0, new ListItem(listgetroomtype[0].Type_NO, "-1"));

        //测试学生信息确认
        //Response.Write(organizationService.addStuConfirm("2",false));
        //Response.Write(organizationService.isStuConfrim("2"));

        //测试学生信息修改
        //Base_STU b = organizationService.getStu("1");
        //b.QQ = "";
        //Response.Write(organizationService.stuUpdate("1", b));

        //测试代码获取
        Base_Code_Item item = organizationService.getCodeItem("001","01");
        if (item != null)
        {
            Response.Write(item.Item_Name);
        }
    }
}
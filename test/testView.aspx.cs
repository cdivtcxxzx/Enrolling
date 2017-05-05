using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using model;
using Newtonsoft.Json;

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
        //Base_Code_Item item = organizationService.getCodeItem("001","01");
        //if (item != null)
        //{
        //    Response.Write(item.Item_Name);
        //}

        //测试代码集合获取
        //List<Base_Code_Item> code_items = organizationService.getCodesItem("003");
        //if (code_items.Count > 0)
        //{
            //for (var i = 0; i < code_items.Count; i++)
            //{
            //    Response.Write(code_items[i].Item_Name+"<br/>");
            //}
            //Response.Write(JsonConvert.SerializeObject(code_items));
        //}

        //测试实体反射出属性
        //organizationModelDataContext oDC = new organizationModelDataContext();
        //Base_STU stu_info = oDC.Base_STUs.Where(s => s.PK_SNO == "2").SingleOrDefault();
        //PropertyInfo[] propertys = stu_info.GetType().GetProperties();
        //foreach (PropertyInfo property in propertys)
        //{
        //    Response.Write(property.Name +","+ property.GetValue(stu_info, null) + "<br/> ");
        //}

        //Response.Write(DateTime.Now.ToShortDateString());

        //测试多实体连查
        //organizationModelDataContext oDC = new organizationModelDataContext();
        //var stu = from s in oDC.Base_STUs
        //          join zy in oDC.Fresh_SPEs on s.FK_SPE_Code equals zy.PK_SPE
        //          join f in oDC.Fresh_STUs on s.PK_SNO equals f.PK_SNO
        //          where f.FK_Fresh_Batch == "1"
        //          orderby s.Name
        //          select new
        //          {
        //              PK_SNO = s.PK_SNO,
        //              Test_NO = s.Test_NO,
        //              Name = s.Name,
        //              Gender = s.Gender_Code=="" ? "" : s.Gender_Code == "01" ? "男" : "女",
        //              Nation_code = s.Nation_Code,
        //              SPE_Name = zy.SPE_Name,
        //              Xz = zy.Xznx,
        //              Year = s.Year
        //          };

        //GridView1.DataSource = stu;
        //GridView1.DataBind();


        //测试getStuByBatch
        //GridView1.DataSource = organizationService.getStuByBatch("");
        //GridView1.DataBind();
        
    }
}
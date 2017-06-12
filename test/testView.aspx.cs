using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        //Base_Code_Item item = organizationService.getCodeItem("003", "05");
        //if (item != null)
        //{
        //    Response.Write(item.Item_Name);
        //}

        //测试代码集合获取
        //List<Base_Code_Item> code_items = organizationService.getCodesItem("003");
        //if (code_items.Count > 0)
        //{
        //    for (var i = 0; i < code_items.Count; i++)
        //    {
        //        Response.Write(code_items[i].Item_NO + "<br/>");
        //    }
        //    //Response.Write(JsonConvert.SerializeObject(code_items));
        //}

        //测试实体反射出属性
        //organizationModelDataContext oDC = new organizationModelDataContext();
        //Base_STU stu_info = oDC.Base_STUs.Where(s => s.PK_SNO == "2").SingleOrDefault();
        //PropertyInfo[] propertys = stu_info.GetType().GetProperties();
        //foreach (PropertyInfo property in propertys)
        //{
        //    Response.Write(property.Name + "," + property.GetValue(stu_info, null) + "<br/> ");
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


        //民族|性别
        //List<Base_Code_Item> mz = organizationService.getCodesItem("003");
        //List<Base_Code_Item> xb = organizationService.getCodesItem("002");

        //Base_Code_Item AMz =  mz.Where(m => m.Item_Name == "汉族").SingleOrDefault();
        //Response.Write(organizationService.getSpe("9") == null);

        //测试字符串指定位数输出
        //int aNum = 1232;
        //Response.Write(aNum.ToString("000"));

        //测试学生数量查询
        //Response.Write(organizationService.getStuCount("4","2017"));
        //生成学号 createNum
        //Response.Write(organizationService.createNum("2017","1","03"));
        
        //测试权限获取
        //List<Base_College> yx = organizationService.getYxByYhid("chenzhiqiu",Session["Lsz"].ToString());
        //foreach (var item in yx)
        //{
        //    Response.Write(item.Name);
        //    Response.Write("<br/>");

        //测试批次时间判断
        //DateTime tsts = Convert.ToDateTime("2017-6-6 00:12:00");
        //Response.Write(organizationService.isInEnableBatch(tsts));

        //测试信息获取
        //List<ClassMsgObj> dt = messageService.getMsgsByClassNO("2017010022");

        //foreach (var item in dt)
        //{
        //    Response.Write(item.PK_NO + "<br/>");
        //}

        var dt = messageService.getListMsgsByClassNO("2017010022");
        foreach (var item in dt)
        {
            Response.Write(item);
        }

        //测试信息插入
        //string iseeee = messageService.addStuReadMsg("1", "201756010203010");
        //Response.Write(iseeee);
        
    }
    //测试导出
    protected void Button1_Click(object sender, EventArgs e)
    {
        //Array s = organizationService.getStuByBatch("1");
        toexcel xzfile = new toexcel();
        //DataTable tb = new DataTable();
        //tb.Columns.Add("学号");
        //tb.Columns.Add("报名号");
        //tb.Columns.Add("姓名");
        //tb.Columns.Add("性别");
        //tb.Columns.Add("民族代码");
        //tb.Columns.Add("专业");
        //tb.Columns.Add("学制");
        //tb.Columns.Add("年级");
        //foreach (var obj in s)
        //{

        //    Response.Write(obj.GetType());
        //    Response.Write("<br/>");
        //}

        DataTable dt =  organizationService.getStuByBatch("1");
        dt.Columns.Remove("Nation_code");
        dt.Columns.Remove("Fresh_bath");
        dt.Columns["PK_SNO"].ColumnName = "学号";
        dt.Columns["Test_NO"].ColumnName = "报名号";
        dt.Columns["Name"].ColumnName = "姓名";
        dt.Columns["Gender"].ColumnName = "性别";
        dt.Columns["SPE_Name"].ColumnName = "专业";
        dt.Columns["Xz"].ColumnName = "学制";
        dt.Columns["Year"].ColumnName = "年级";               
        string filen = xzfile.DatatableToExcel(dt, "学生数据");
        Response.Write(filen);
    }


}

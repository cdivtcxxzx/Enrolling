using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;

public partial class nradmingl_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        //DataTable x = dormitory.billetdata("1");
        //for (int i = 0; x != null && i < x.Rows.Count; i++)
        //{
        //    // Console.WriteLine(row.FK_SNO);
        //    Response.Write("获取到datatable数据：学号：" + x.Rows[i]["FK_SNO"].ToString() +"@"+ x.Rows[i]["FK_Bed_No"].ToString() + "@" + x.Rows[i]["Updater"].ToString() + "@" + x.Rows[i]["Update_DT"].ToString()+ "<br>");
        //}
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        List<Fresh_Bed_Log> data = dormitory.listbilletdata(this.TextBox1.Text);
        // List<Fresh_Bed_Log> data = ModelConvertHelper<Fresh_Bed_Log>.ConvertToModel(dormitory.billetdata("2001"));


        for (int i = 0; data != null && i < data.Count; i++)
        {
            Fresh_Bed_Log row = data[i];
            // Console.WriteLine(row.FK_SNO);
            Response.Write("list数据共" + data.Count.ToString() + "行数据<br>");
            Response.Write("获取到list数据第" + (i + 1).ToString() + "行：<br>");
            //Fresh_Bed_Log cl1 = new Fresh_Bed_Log();
            System.Reflection.PropertyInfo[] propertys = row.GetType().GetProperties();
            string s = string.Empty;
            foreach (System.Reflection.PropertyInfo info in propertys)
            {
                s += info.Name + ":";
                s += info.GetValue(row, null) + "<br>";
            }
            Response.Write(s + "-----------------------------------<br>");
        }
    }
    protected void Button1_Click2(object sender, EventArgs e)
    {
        List<Fresh_Bed> data = dormitory.listgetbed(this.TextBox2.Text);
        // List<Fresh_Bed_Log> data = ModelConvertHelper<Fresh_Bed_Log>.ConvertToModel(dormitory.billetdata("2001"));


        for (int i = 0; data != null && i < data.Count; i++)
        {
            Fresh_Bed row = data[i];
            // Console.WriteLine(row.FK_SNO);
            Response.Write("list数据共" + data.Count.ToString() + "行数据<br>");
            Response.Write("获取到list数据第" + (i + 1).ToString() + "行：<br>");
            //Fresh_Bed_Log cl1 = new Fresh_Bed_Log();
            //遍历所有类的属性名及值
            System.Reflection.PropertyInfo[] propertys = row.GetType().GetProperties();
            string s = string.Empty;
            foreach (System.Reflection.PropertyInfo info in propertys)
            {
                s += info.Name + ":";
                s += info.GetValue(row, null) + "<br>";
            }
            Response.Write(s + "-----------------------------------<br>");
        }
    }
    protected void Button1_Click3(object sender, EventArgs e)
    {
        List<Fresh_Room> data = dormitory.listgetroom(this.TextBox3.Text);
        // List<Fresh_Bed_Log> data = ModelConvertHelper<Fresh_Bed_Log>.ConvertToModel(dormitory.billetdata("2001"));


        for (int i = 0; data != null && i < data.Count; i++)
        {
            Fresh_Room row = data[i];
            // Console.WriteLine(row.FK_SNO);
            Response.Write("list数据共" + data.Count.ToString() + "行数据<br>");
            Response.Write("获取到list数据第" + (i + 1).ToString() + "行：<br>");
            //Fresh_Bed_Log cl1 = new Fresh_Bed_Log();
            //遍历所有类的属性名及值
            System.Reflection.PropertyInfo[] propertys = row.GetType().GetProperties();
            string s = string.Empty;
            foreach (System.Reflection.PropertyInfo info in propertys)
            {
                s += info.Name + ":";
                s += info.GetValue(row, null) + "<br>";
            }
            Response.Write(s + "-----------------------------------<br>");
        }
    }
}
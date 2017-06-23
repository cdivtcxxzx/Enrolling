using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using InterfaceWebServiceykt;

public partial class zmtest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     //   //ServiceReferenceykt.
        //InterfaceWebServiceSoapClient service = new InterfaceWebServiceSoapClient();
        InterfaceWebService service = new InterfaceWebService();
        // InterfaceWebServiceSoapChannel service = new InterfaceWebServiceSoapChannel();
        CredentialSoapHeader header = new CredentialSoapHeader();//Create  SOAP 

        header.UserID = "weds_3";//Set SOAP header user name information
        header.PassWord = "NT6+StekQz86of4T//CuwzzJkja5soj//TtYsN0fQAlah0BLf3I9pHlFApIiIcuyxm23XYGI0AWZl9iQ+87IH6ZesAf89WiQXhgY26IEP3WahuIKN7Z1/izZ2GVfi04tmZ3apeJZBfHULLYwvFQkwWfUxG2TT54FBa+SDGcOREc=";//Set SOAP header user password information
        service.CredentialSoapHeaderValue = header;
        string token = service.ToLogin();
        this.xh.Text = token;





        
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
    protected void Button3_Click(object sender, EventArgs e)
    {
        //^(?![\d]+$)(?![a-zA-Z]+$)(?![^\da-zA-Z]+$).{6,20}$
        //能匹配的组合为：数字+字母，数字+特殊字符，字母+特殊字符，数字+字母+特殊字符组合，而且不能是纯数字，纯字母，纯特殊字符
        //上面的正则里所说的特殊字符是除了数字，字母之外的所有字符
        //如果要限定特殊字符，例如，特殊字符的范围为 !#$%^&*  ，那么可以这么改
        //^(?![\d]+$)(?![a-zA-Z]+$)(?![!#$%^&*]+$)[\da-zA-Z!#$%^&*]{6,20}$
        if (xh.Text != IscNumber(xh.Text))
        {
            Response.Write(IscNumber(xh.Text));
        }
        else
        {
            if (!IsNatural_Number(this.xh.Text, 8, 16))
            {
                Response.Write("密码强度不够，请使用8-16位的数字+字母，数字+特殊字符，字母+特殊字符，数字+字母+特殊字符组合，而且不能是纯数字，纯字母，纯特殊字符设置自己的密码。<br><br>");
                if (xh.Text.Length < 8)
                {
                    Response.Write("密码位数不足8位！<br>");
                }
                if (xh.Text.Length > 16)
                {
                    Response.Write("密码位数超过16位！<br>");
                }


            }
            else
            {
                Response.Write("验证通过:" + xh.Text);
            }
        }

    }
    public bool IsNatural_Number(string str,int min,int max)
    {
        //^(?![\d]+$)(?![a-zA-Z]+$)(?![^\da-zA-Z]+$).{6,20}$
        //能匹配的组合为：数字+字母，数字+特殊字符，字母+特殊字符，数字+字母+特殊字符组合，而且不能是纯数字，纯字母，纯特殊字符
        //上面的正则里所说的特殊字符是除了数字，字母之外的所有字符
        //如果要限定特殊字符，例如，特殊字符的范围为 !#$%^&*  ，那么可以这么改
        //^(?![\d]+$)(?![a-zA-Z]+$)(?![!#$%^&*]+$)[\da-zA-Z!#$%^&*]{6,20}$
        System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^(?![\d]+$)(?![a-zA-Z]+$)(?![^\da-zA-Z]+$).{"+min+","+max+"}$");
        return reg1.IsMatch(str);
    }
    public string IscNumber(string str)
    {
        //^(?![\d]+$)(?![a-zA-Z]+$)(?![^\da-zA-Z]+$).{6,20}$ 纯数字

        System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9]*$");
        if(reg1.IsMatch(str))
        {
            return "密码不能为纯数字！";
        }
        System.Text.RegularExpressions.Regex reg2 = new System.Text.RegularExpressions.Regex(@"^[A-Za-z]+$");
        if (reg2.IsMatch(str))
        {
            return "密码不能为纯字母！";
        }
        System.Text.RegularExpressions.Regex reg3 = new System.Text.RegularExpressions.Regex(@"^[^\w\s]+$");
        if (reg3.IsMatch(str))
        {
            return "密码不能为纯符号！";
        }
        System.Text.RegularExpressions.Regex reg4 = new System.Text.RegularExpressions.Regex(@"^[\u4E00-\u9FA5]+$");
        for (int x = 0; x < str.Length;x++ )
        {
            if (reg4.IsMatch(str.Substring(x,1)))
            {
                return "密码中不能使用汉字！[" + str.Substring(x,1)+"]";
            }
        }
           
        
        return str;
    }
}
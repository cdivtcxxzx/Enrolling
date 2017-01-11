<%@ Page Language="C#" AutoEventWireup="true" CodeFile="webapiys.aspx.cs" Inherits="nradmingl_webapiys" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Webapi接口地址:<asp:TextBox ID="TextBox1" runat="server" Width="560px">http://10.35.10.4:7555/SmstClassService/SearchSmstClassStudent</asp:TextBox>
&nbsp; 页面编码<asp:TextBox ID="TextBox5" runat="server" Width="54px">utf-8</asp:TextBox>
        gb2312<br />
        <br />
        用户名:<asp:TextBox ID="TextBox2" runat="server">zhouxiangyang</asp:TextBox>
        密码:<asp:TextBox ID="TextBox3" runat="server">64451568</asp:TextBox>
&nbsp;&nbsp;
        <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="头部BASE64加密" />
        <asp:TextBox ID="TextBox4" runat="server" Width="228px"></asp:TextBox>
        <asp:TextBox ID="TextBox6" runat="server" Width="38px">json</asp:TextBox>
        <br />
        <br />
        参数1:<asp:TextBox ID="cs1" runat="server">BH</asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi1" runat="server">16070202</asp:TextBox>
        <br />
        参数2:<asp:TextBox ID="cs2" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi2" runat="server">15030103</asp:TextBox>
        <br />
        参数3:<asp:TextBox ID="cs3" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi3" runat="server"></asp:TextBox>
        <br />
        参数4:<asp:TextBox ID="cs4" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi4" runat="server"></asp:TextBox>
        <br />
        参数5:<asp:TextBox ID="cs5" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi5" runat="server"></asp:TextBox>
        <br />
        参数6:<asp:TextBox ID="cs6" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi6" runat="server"></asp:TextBox>
        <br />
        参数7:<asp:TextBox ID="cs7" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi7" runat="server"></asp:TextBox>
        <br />
        参数8:<asp:TextBox ID="cs8" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi8" runat="server"></asp:TextBox>
        <br />
        参数9:<asp:TextBox ID="cs9" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi9" runat="server"></asp:TextBox>
        <br />
        参数10:<asp:TextBox ID="cs10" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi10" runat="server"></asp:TextBox>
        <br />
        参数11:<asp:TextBox ID="cs11" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi11" runat="server"></asp:TextBox>
        <br />
        参数12:<asp:TextBox ID="cs12" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi12" runat="server"></asp:TextBox>
        <br />
        参数13:<asp:TextBox ID="cs13" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi13" runat="server"></asp:TextBox>
        <br />
        参数14:<asp:TextBox ID="cs14" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi14" runat="server"></asp:TextBox>
        <br />
        参数15:<asp:TextBox ID="cs15" runat="server"></asp:TextBox>
&nbsp;&nbsp; 值:<asp:TextBox ID="zhi15" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="传递参数测试" />
        <br />
        <br />
        <asp:Label ID="tsxx" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>

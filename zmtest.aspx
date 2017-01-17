<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zmtest.aspx.cs" Inherits="zmtest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        输入学号：<asp:TextBox ID="xh" runat="server">学号</asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="学生是否已分配宿舍（学号）" />
&nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="学生已分配床位(学号)" />
        <br />
    
    </div>
    </form>
</body>
</html>

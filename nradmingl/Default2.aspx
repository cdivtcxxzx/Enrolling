<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="nradmingl_Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server">1</asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="测试21床位分配" OnClick="Button1_Click" />
    &nbsp;</div>
        <p>
        <asp:TextBox ID="TextBox2" runat="server">1</asp:TextBox>
            <asp:Button ID="Button2" runat="server" Text="测试22床位" OnClick="Button1_Click2" />
        </p>
        <p>
        <asp:TextBox ID="TextBox3" runat="server">1</asp:TextBox>
            <asp:Button ID="Button3" runat="server" Text="测试23房间" OnClick="Button1_Click3" />
        </p>
    </form>
</body>
</html>

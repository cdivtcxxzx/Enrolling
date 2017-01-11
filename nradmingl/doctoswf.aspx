<%@ Page Language="C#" AutoEventWireup="true" CodeFile="doctoswf.aspx.cs" Inherits="admin_doctoswf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        需转换的文件夹名<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        (起始于网站根目录)<asp:TextBox ID="TextBox3" runat="server" Width="83px">.doc</asp:TextBox>
        <asp:TextBox ID="TextBox4" runat="server" Width="83px">.DOC</asp:TextBox>
        <asp:TextBox ID="TextBox5" runat="server" Width="83px">.xls</asp:TextBox>
        <asp:TextBox ID="TextBox6" runat="server" Width="83px">.XLS</asp:TextBox>
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;是否验证已存在(1为要):<asp:TextBox 
            ID="TextBox12" runat="server">0</asp:TextBox>
        &nbsp;&nbsp;<asp:TextBox ID="TextBox7" runat="server" Width="83px">.ppt</asp:TextBox>
        <asp:TextBox ID="TextBox8" runat="server" Width="85px">.PPT</asp:TextBox>
        <asp:TextBox ID="TextBox9" runat="server" Width="83px">.pdf</asp:TextBox>
        <asp:TextBox ID="TextBox10" runat="server" Width="83px">PDF</asp:TextBox>
        <br />
        <asp:Label ID="Label1" runat="server" Text="点击开始转换,进行批量转换操作"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="开始转换" />
    
        <asp:Button ID="Button3" runat="server"  Text="仅验证" onclick="Button3_Click" />
    
        <asp:TextBox ID="TextBox2" runat="server" Height="186px" TextMode="MultiLine" 
            Width="878px"></asp:TextBox>
        <asp:Button ID="Button2" runat="server"  
            Text="验证文档是否存在" onclick="Button2_Click" />
    
        <asp:TextBox ID="TextBox11" runat="server" Height="146px" TextMode="MultiLine" 
            Width="798px"></asp:TextBox>
    
    </div>
    </form>
</body>
</html>

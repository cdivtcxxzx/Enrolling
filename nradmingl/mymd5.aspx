<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mymd5.aspx.cs"  validateRequest="false"  Inherits="nradmingl_mymd5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        输入域名:<asp:TextBox ID="TextBox1" runat="server" Width="178px"></asp:TextBox>
        密钥:<asp:TextBox ID="TextBox2" runat="server" Width="94px"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="生成正式授权" 
            Width="87px" />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="生成试用授权" 
            Width="90px" />
        <asp:Label ID="Label1" runat="server" Font-Size="Large"></asp:Label>
        <br />
        <br />
        输入值:<asp:TextBox ID="TextBox3" runat="server" Width="178px"></asp:TextBox>
        密钥:<asp:TextBox ID="TextBox4" runat="server" Width="161px"></asp:TextBox>
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="验证授权" />
        <asp:Label ID="Label2" runat="server" Font-Size="Large"></asp:Label>
    
        <br />
        <br />
        <asp:TextBox ID="TextBox6" runat="server" Width="161px"></asp:TextBox>
        影画工职加密密钥<asp:TextBox ID="TextBox7" runat="server" Width="330px"><5"vtJ?sOGLK.]38H@*D!;=~VZ-^q$Pn&0CY4(kQ</asp:TextBox>
        <asp:Button ID="Button5" runat="server" onclick="Button12_Click" Text="MD5加密" />
        <asp:Button ID="Button6" runat="server" onclick="Button133_Click" Text="影画工职加密" />
        <br />
        <br />
        <asp:TextBox ID="TextBox5" runat="server" Width="550px"></asp:TextBox>
        <asp:Button ID="Button4" runat="server" onclick="Button41_Click" Text="写入当前授权" 
            Width="87px" />
    
        <br />
    
        <br />
        跳转:<asp:TextBox ID="TextBox9" runat="server" Width="550px"></asp:TextBox>
        登陆退出:<asp:TextBox ID="TextBox10" runat="server" Width="27px">1</asp:TextBox>
        为空或1登陆，2注销<br />
        网址:<asp:TextBox ID="TextBox8" runat="server" Width="550px">http://kb.cdivtc.com/sso.aspx</asp:TextBox>
        <asp:Button ID="Button8" runat="server" onclick="Button41_Clickurl" Text="生成单点登录测试" 
            Width="115px" />
    
        <br />
    
        <br />
        <asp:Button ID="Button7" runat="server" onclick="Button41t_Click" Text="影画工职用户同步" 
            Width="116px" />
    
        <asp:Label ID="Label4" runat="server"></asp:Label>
    
        <br />
        <asp:Label ID="Label3" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>

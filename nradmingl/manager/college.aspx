<%@ Page Language="C#" AutoEventWireup="true" CodeFile="college.aspx.cs" Inherits="nradmingl_manager_college" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>学院数据</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="学院主键" DataSourceID="ds_college">
            <Columns>
                <asp:BoundField DataField="学院主键" HeaderText="学院主键" ReadOnly="True" SortExpression="学院主键" />
                <asp:BoundField DataField="学院名称" HeaderText="学院名称" SortExpression="学院名称" />
                <asp:BoundField DataField="有效标志" HeaderText="有效标志" SortExpression="有效标志" />
                <asp:BoundField DataField="学院编码" HeaderText="学院编码" SortExpression="学院编码" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ds_college" runat="server" ConnectionString="<%$ ConnectionStrings:yxxt_dataConnectionString1 %>" SelectCommand="SELECT PK_College AS 学院主键, Name AS 学院名称, Enabled AS 有效标志, College_NO AS 学院编码 FROM Base_College"></asp:SqlDataSource>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="operate.aspx.cs" Inherits="nradmingl_manager_staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>迎新操作</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="主键" DataSourceID="ds_operate">
            <Columns>
                <asp:BoundField DataField="主键" HeaderText="主键" ReadOnly="True" SortExpression="主键" />
                <asp:BoundField DataField="操作名称" HeaderText="操作名称" SortExpression="操作名称" />
                <asp:BoundField DataField="操作url地址" HeaderText="操作url地址" SortExpression="操作url地址" />
                <asp:BoundField DataField="操作类型" HeaderText="操作类型" SortExpression="操作类型" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ds_operate" runat="server" ConnectionString="<%$ ConnectionStrings:yxxt_dataConnectionString1 %>" SelectCommand="SELECT PK_OPER_NO AS 主键, OPER_Name AS 操作名称, OPER_URL AS 操作url地址, OPER_Type AS 操作类型 FROM Fresh_OPER"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

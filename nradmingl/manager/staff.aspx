<%@ Page Language="C#" AutoEventWireup="true" CodeFile="staff.aspx.cs" Inherits="nradmingl_manager_staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>迎新员工数据</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="员工编号" DataSourceID="ds_staff">
            <Columns>
                <asp:BoundField DataField="员工编号" HeaderText="员工编号" ReadOnly="True" SortExpression="员工编号" />
                <asp:BoundField DataField="电话" HeaderText="电话" SortExpression="电话" />
                <asp:BoundField DataField="姓名" HeaderText="姓名" SortExpression="姓名" />
                <asp:BoundField DataField="密码" HeaderText="密码" SortExpression="密码" />
                <asp:BoundField DataField="性别" HeaderText="性别" SortExpression="性别" />
                <asp:BoundField DataField="所属学院" HeaderText="所属学院" SortExpression="所属学院" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ds_staff" runat="server" ConnectionString="<%$ ConnectionStrings:yxxt_dataConnectionString1 %>" SelectCommand="SELECT a.PK_Staff_NO AS 员工编号, a.Phone AS 电话, a.Name AS 姓名, a.Password AS 密码, b.Item_Name AS 性别, c.Name AS 所属学院 FROM Base_Staff AS a INNER JOIN Base_Code_Item AS b ON a.Gender = b.Item_NO INNER JOIN Base_College AS c ON a.FK_College_NO = c.PK_College WHERE (b.FK_Code = '002')"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

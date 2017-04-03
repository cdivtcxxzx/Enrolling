<%@ Page Language="C#" AutoEventWireup="true" CodeFile="class.aspx.cs" Inherits="nradmingl_manager_college" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>班级数据</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="班级编码" DataSourceID="ds_class">
            <Columns>
                <asp:BoundField DataField="班级编码" HeaderText="班级编码" ReadOnly="True" SortExpression="班级编码" />
                <asp:BoundField DataField="班级名称" HeaderText="班级名称" SortExpression="班级名称" />
                <asp:BoundField DataField="年级" HeaderText="年级" SortExpression="年级" />
                <asp:BoundField DataField="专业名称" HeaderText="专业名称" SortExpression="专业名称" />
                <asp:BoundField DataField="班级所在校区" HeaderText="班级所在校区" SortExpression="班级所在校区" />
                <asp:BoundField DataField="有效校区标志" HeaderText="有效校区标志" SortExpression="有效校区标志" />
                <asp:BoundField DataField="学历层次" HeaderText="学历层次" SortExpression="学历层次" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ds_class" runat="server" ConnectionString="<%$ ConnectionStrings:yxxt_dataConnectionString1 %>" SelectCommand="SELECT a.PK_Class_NO AS 班级编码, a.Name AS 班级名称, b.Year AS 年级, b.SPE_Name AS 专业名称, c.Campus_Name AS 班级所在校区, c.Enabled AS 有效校区标志, d.Item_Name AS 学历层次 FROM Fresh_Class AS a INNER JOIN Fresh_SPE AS b ON a.FK_SPE_NO = b.PK_SPE INNER JOIN Base_Campus AS c ON a.FK_Campus_NO = c.PK_Campus INNER JOIN Base_Code_Item AS d ON b.EDU_Level_Code = d.Item_NO WHERE (d.FK_Code = '001')"></asp:SqlDataSource>
    </form>
</body>
</html>

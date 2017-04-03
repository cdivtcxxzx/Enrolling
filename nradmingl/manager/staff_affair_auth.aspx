<%@ Page Language="C#" AutoEventWireup="true" CodeFile="staff_affair_auth.aspx.cs" Inherits="nradmingl_manager_staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>迎新员工迎新事务授权数据</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="员工编号" DataSourceID="ds_staff_affair_auth">
            <Columns>
                <asp:BoundField DataField="所属学院" HeaderText="所属学院" SortExpression="所属学院" />
                <asp:BoundField DataField="员工编号" HeaderText="员工编号" SortExpression="员工编号" ReadOnly="True" />
                <asp:BoundField DataField="员工电话" HeaderText="员工电话" SortExpression="员工电话" />
                <asp:BoundField DataField="员工姓名" HeaderText="员工姓名" SortExpression="员工姓名" />
                <asp:BoundField DataField="性别" HeaderText="性别" SortExpression="性别" />
                <asp:BoundField DataField="迎新批次授权标志" HeaderText="迎新批次授权标志" SortExpression="迎新批次授权标志" />
                <asp:BoundField DataField="迎新批次" HeaderText="迎新批次" SortExpression="迎新批次" />
                <asp:BoundField DataField="迎新年" HeaderText="迎新年" SortExpression="迎新年" />
                <asp:BoundField DataField="迎新学生类型" HeaderText="迎新学生类型" SortExpression="迎新学生类型" />
                <asp:BoundField DataField="事务名称" HeaderText="事务名称" SortExpression="事务名称" />
                <asp:BoundField DataField="事务类型" HeaderText="事务类型" SortExpression="事务类型" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ds_staff_affair_auth" runat="server" ConnectionString="<%$ ConnectionStrings:yxxt_dataConnectionString1 %>" SelectCommand="SELECT f.Name AS 所属学院, a.PK_Staff_NO AS 员工编号, a.Phone AS 员工电话, a.Name AS 员工姓名, g.Item_Name AS 性别, b.Enabled AS 迎新批次授权标志, c.Batch_Name AS 迎新批次, c.Year AS 迎新年, h.Item_Name AS 迎新学生类型, e.Affair_Name AS 事务名称, e.Affair_Type AS 事务类型 FROM Base_Staff AS a INNER JOIN Fresh_Operator_AUTH AS b ON a.PK_Staff_NO = b.FK_Staff_NO INNER JOIN Fresh_Batch AS c ON b.FK_Fresh_Batch = c.PK_Batch_NO INNER JOIN Fresh_Affair_AUTH AS d ON b.PK_Operator_AUTH = d.FK_Operator_AUTH INNER JOIN Fresh_Affair AS e ON d.FK_Affair_NO = e.PK_Affair_NO INNER JOIN Base_College AS f ON a.FK_College_NO = f.PK_College INNER JOIN Base_Code_Item AS g ON a.Gender = g.Item_NO INNER JOIN Base_Code_Item AS h ON c.STU_Type = h.Item_NO WHERE (g.FK_Code = '002') AND (h.FK_Code = '001') ORDER BY 迎新年, 迎新批次, 员工编号, 事务名称"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="counseller.aspx.cs" Inherits="nradmingl_manager_college" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>班主任数据</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ds_counseller">
            <Columns>
                <asp:BoundField DataField="班级编号" HeaderText="班级编号" SortExpression="班级编号" />
                <asp:BoundField DataField="班级名称" HeaderText="班级名称" SortExpression="班级名称" />
                <asp:BoundField DataField="员工编号" HeaderText="员工编号" SortExpression="员工编号" />
                <asp:BoundField DataField="员工电话" HeaderText="员工电话" SortExpression="员工电话" />
                <asp:BoundField DataField="员工姓名" HeaderText="员工姓名" SortExpression="员工姓名" />
                <asp:BoundField DataField="班主任电话" HeaderText="班主任电话" SortExpression="班主任电话" />
                <asp:BoundField DataField="性别" HeaderText="性别" SortExpression="性别" />
                <asp:BoundField DataField="专业名称" HeaderText="专业名称" SortExpression="专业名称" />
                <asp:BoundField DataField="班级所在校区" HeaderText="班级所在校区" SortExpression="班级所在校区" />
                <asp:BoundField DataField="校区有效标志" HeaderText="校区有效标志" SortExpression="校区有效标志" />
                <asp:BoundField DataField="专业学历层次" HeaderText="专业学历层次" SortExpression="专业学历层次" />
                <asp:BoundField DataField="年级" HeaderText="年级" SortExpression="年级" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ds_counseller" runat="server" ConnectionString="<%$ ConnectionStrings:yxxt_dataConnectionString1 %>" SelectCommand="SELECT a.PK_Class_NO AS 班级编号, a.Name AS 班级名称, e.PK_Staff_NO AS 员工编号, e.Phone AS 员工电话, e.Name AS 员工姓名, e.counsellerphone AS 班主任电话, e.Item_Name AS 性别, b.SPE_Name AS 专业名称, c.Campus_Name AS 班级所在校区, c.Enabled AS 校区有效标志, d.Item_Name AS 专业学历层次, b.Year AS 年级 FROM Fresh_Class AS a LEFT OUTER JOIN (SELECT t2.PK_Staff_NO, t2.Phone, t2.FK_College_NO, t2.Name, t2.Gender, t2.Password, t1.FK_Class_NO, t1.Phone AS counsellerphone, t3.Item_Name FROM Fresh_Counseller AS t1 INNER JOIN Base_Staff AS t2 ON t1.FK_Staff_NO = t2.PK_Staff_NO INNER JOIN Base_Code_Item AS t3 ON t2.Gender = t3.Item_NO WHERE (t3.FK_Code = '002')) AS e ON a.PK_Class_NO = e.FK_Class_NO INNER JOIN Fresh_SPE AS b ON a.FK_SPE_NO = b.PK_SPE INNER JOIN Base_Campus AS c ON a.FK_Campus_NO = c.PK_Campus INNER JOIN Base_Code_Item AS d ON b.EDU_Level_Code = d.Item_NO WHERE (d.FK_Code = '001') ORDER BY 年级"></asp:SqlDataSource>
    </form>
</body>
</html>

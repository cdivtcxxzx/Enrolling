<%@ Page Language="C#" AutoEventWireup="true" CodeFile="staff_affair_auth_scope.aspx.cs" Inherits="nradmingl_manager_staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
            <style type="text/css">
        td {word-break:break-all; word-wrap:break-word;}
    </style>
    <title>迎新员工迎新事务授权操作范围数据</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="员工编号" DataSourceID="ds_staff_affair_auth_scope">
            <Columns>
                <asp:BoundField DataField="所属学院" HeaderText="所属学院" SortExpression="所属学院" />
                <asp:BoundField DataField="性别" HeaderText="性别" SortExpression="性别" />
                <asp:BoundField DataField="迎新学生类型" HeaderText="迎新学生类型" SortExpression="迎新学生类型" />
                                <asp:BoundField DataField="员工编号" HeaderText="员工编号" SortExpression="员工编号" ReadOnly="True" />
                <asp:BoundField DataField="员工姓名" HeaderText="员工姓名" SortExpression="员工姓名" />
                <asp:BoundField DataField="迎新批次" HeaderText="迎新批次" SortExpression="迎新批次" />
                                <asp:BoundField DataField="迎新年" HeaderText="迎新年" SortExpression="迎新年" />
                <asp:BoundField DataField="迎新批次授权标志" HeaderText="迎新批次授权标志" SortExpression="迎新批次授权标志" />
                <asp:BoundField DataField="事务名称" HeaderText="事务名称" SortExpression="事务名称" />
                <asp:BoundField DataField="事务类型" HeaderText="事务类型" SortExpression="事务类型" />
                <asp:BoundField DataField="可操作学院编码集合" HeaderText="可操作学院编码集合" SortExpression="可操作学院编码集合" />
                <asp:BoundField DataField="可操作学院名称集合" HeaderText="可操作学院名称集合" SortExpression="可操作学院名称集合" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ds_staff_affair_auth_scope" runat="server" ConnectionString="<%$ ConnectionStrings:yxxt_dataConnectionString1 %>" SelectCommand="SELECT collegename AS 所属学院, gender AS 性别, welcome_type AS 迎新学生类型, PK_Staff_NO AS 员工编号, Name AS 员工姓名, Batch_Name AS 迎新批次, Year AS 迎新年, Enabled AS 迎新批次授权标志, Affair_Name AS 事务名称, Affair_Type AS 事务类型, FK_College_NO_STR AS 可操作学院编码集合, FK_College_NAME_STR AS 可操作学院名称集合 FROM vw_staff_affair_auth_scope ORDER BY 迎新年, 迎新批次, 员工编号, 事务名称"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="base_stu.aspx.cs" Inherits="nradmingl_manager_staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>学生基本信息</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="学号" DataSourceID="ds_stu">
            <Columns>
                <asp:BoundField DataField="年级" HeaderText="年级" SortExpression="年级" />
                <asp:BoundField DataField="专业名称" HeaderText="专业名称" SortExpression="专业名称" />
                <asp:BoundField DataField="所属学院" HeaderText="所属学院" SortExpression="所属学院" />
                <asp:BoundField DataField="学号" HeaderText="学号" ReadOnly="True" SortExpression="学号" />
                <asp:BoundField DataField="高考报名号" HeaderText="高考报名号" SortExpression="高考报名号" />
                <asp:BoundField DataField="身份证" HeaderText="身份证" SortExpression="身份证" />
                <asp:BoundField DataField="姓名" HeaderText="姓名" SortExpression="姓名" />
                <asp:BoundField DataField="状态" HeaderText="状态" SortExpression="状态" />
                <asp:BoundField DataField="导入时间" HeaderText="导入时间" SortExpression="导入时间" />
                <asp:BoundField DataField="班级名称" HeaderText="班级名称" SortExpression="班级名称" />
                <asp:BoundField DataField="所在校区" HeaderText="所在校区" SortExpression="所在校区" />
                <asp:BoundField DataField="性别" HeaderText="性别" SortExpression="性别" />
                <asp:BoundField DataField="批次名称" HeaderText="批次名称" SortExpression="批次名称" />
                <asp:BoundField DataField="迎新年" HeaderText="迎新年" SortExpression="迎新年" />
                <asp:BoundField DataField="学历层次" HeaderText="学历层次" SortExpression="学历层次" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ds_stu" runat="server" ConnectionString="<%$ ConnectionStrings:yxxt_dataConnectionString1 %>" SelectCommand="SELECT a.year AS 年级, a.SPE_Name AS 专业名称, a.Collage AS 所属学院, a.PK_SNO AS 学号, a.Test_NO AS 高考报名号, a.ID_NO AS 身份证, a.Name AS 姓名, a.Status_Code AS 状态, a.DT_Initial AS 导入时间, b.Name AS 班级名称, b.Campus_Name AS 所在校区, c.Item_Name AS 性别, e.Batch_Name AS 批次名称, e.Year AS 迎新年, f.Item_Name AS 学历层次 FROM vw_student_base AS a LEFT OUTER JOIN (SELECT t1.PK_Class_NO, t1.FK_Campus_NO, t1.FK_SPE_NO, t1.Name, t2.PK_Campus, t2.Campus_NO, t2.Campus_Name, t2.Enabled FROM Fresh_Class AS t1 INNER JOIN Base_Campus AS t2 ON t1.FK_Campus_NO = t2.PK_Campus) AS b ON a.FK_Class_NO = b.PK_Class_NO LEFT OUTER JOIN Fresh_STU AS d ON a.PK_SNO = d.PK_SNO INNER JOIN Base_Code_Item AS c ON a.Gender_Code = c.Item_NO INNER JOIN Fresh_Batch AS e ON d.FK_Fresh_Batch = e.PK_Batch_NO INNER JOIN Base_Code_Item AS f ON a.EDU_Level_Code = f.Item_NO WHERE (c.FK_Code = '002') AND (f.FK_Code = '001') ORDER BY 迎新年">
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

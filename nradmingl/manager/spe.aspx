<%@ Page Language="C#" AutoEventWireup="true" CodeFile="spe.aspx.cs" Inherits="nradmingl_manager_college" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>专业数据</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ds_spe" DataKeyNames="专业主键">
            <Columns>
                <asp:BoundField DataField="专业主键" HeaderText="专业主键" SortExpression="专业主键" ReadOnly="True" />
                <asp:BoundField DataField="专业代码" HeaderText="专业代码" SortExpression="专业代码" />
                <asp:BoundField DataField="年级" HeaderText="年级" SortExpression="年级" />
                <asp:BoundField DataField="专业名称" HeaderText="专业名称" SortExpression="专业名称" />
                <asp:BoundField DataField="学院名称" HeaderText="学院名称" SortExpression="学院名称" />
                <asp:BoundField DataField="学院有效标志" HeaderText="学院有效标志" SortExpression="学院有效标志" />
                <asp:BoundField DataField="学历层次" HeaderText="学历层次" SortExpression="学历层次" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ds_spe" runat="server" ConnectionString="<%$ ConnectionStrings:yxxt_dataConnectionString1 %>" SelectCommand="SELECT a.PK_SPE AS 专业主键, a.SPE_Code AS 专业代码, a.Year AS 年级, a.SPE_Name AS 专业名称, b.Name AS 学院名称, b.Enabled AS 学院有效标志, c.Item_Name AS 学历层次 FROM Fresh_SPE AS a INNER JOIN Base_College AS b ON a.FK_College_Code = b.PK_College INNER JOIN Base_Code_Item AS c ON a.EDU_Level_Code = c.Item_NO WHERE (c.FK_Code = '001')"></asp:SqlDataSource>
    </form>
</body>
</html>

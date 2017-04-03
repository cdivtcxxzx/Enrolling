<%@ Page Language="C#" AutoEventWireup="true" CodeFile="batch.aspx.cs" Inherits="nradmingl_manager_staff" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>迎新批次数据</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="主键" DataSourceID="ds_batch">
            <Columns>
                <asp:BoundField DataField="主键" HeaderText="主键" ReadOnly="True" SortExpression="主键" />
                <asp:BoundField DataField="批次名称" HeaderText="批次名称" SortExpression="批次名称" />
                <asp:BoundField DataField="迎新年" HeaderText="迎新年" SortExpression="迎新年" />
                <asp:BoundField DataField="迎新工作开始时间" HeaderText="迎新工作开始时间" SortExpression="迎新工作开始时间" />
                <asp:BoundField DataField="迎新工作结束时间" HeaderText="迎新工作结束时间" SortExpression="迎新工作结束时间" />
                <asp:BoundField DataField="服务开始时间" HeaderText="服务开始时间" SortExpression="服务开始时间" />
                <asp:BoundField DataField="服务结束时间" HeaderText="服务结束时间" SortExpression="服务结束时间" />
                <asp:BoundField DataField="服务启停" HeaderText="服务启停" SortExpression="服务启停" />
                <asp:BoundField DataField="学生类型" HeaderText="学生类型" SortExpression="学生类型" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ds_batch" runat="server" ConnectionString="<%$ ConnectionStrings:yxxt_dataConnectionString1 %>" SelectCommand="SELECT a.PK_Batch_NO AS 主键, a.Batch_Name AS 批次名称, a.Year AS 迎新年,a.Welcome_Begin AS 迎新工作开始时间, a.Welcome_End AS 迎新工作结束时间, a.Service_Begin AS 服务开始时间, a.Service_End AS 服务结束时间, a.Enabled AS 服务启停, b.Item_Name AS 学生类型 FROM Fresh_Batch AS a INNER JOIN Base_Code_Item AS b ON a.STU_Type = b.Item_NO WHERE (b.FK_Code = '001')"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

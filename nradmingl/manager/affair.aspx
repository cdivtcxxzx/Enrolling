<%@ Page Language="C#" AutoEventWireup="true" CodeFile="affair.aspx.cs" Inherits="nradmingl_manager_college" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <style type="text/css">
        td {word-break:break-all; word-wrap:break-word;}
    </style>


    <title>迎新事务数据</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ds_affair" DataKeyNames="主键">
            <Columns>
                <asp:BoundField DataField="主键" HeaderText="主键" ReadOnly="True" SortExpression="主键" />
                <asp:BoundField DataField="内部索引号" HeaderText="内部索引号" SortExpression="内部索引号" />
                <asp:BoundField DataField="事务名称" HeaderText="事务名称" SortExpression="事务名称" />
                <asp:BoundField DataField="事务类型" HeaderText="事务类型" SortExpression="事务类型" />
                <asp:BoundField DataField="前置条件1" HeaderText="前置条件1" SortExpression="前置条件1" />
                <asp:BoundField DataField="前置条件2" HeaderText="前置条件2" SortExpression="前置条件2" />
                <asp:BoundField DataField="事务状态回调函数" HeaderText="事务状态回调函数" SortExpression="事务状态回调函数" />
                <asp:BoundField DataField="事务性质" HeaderText="事务性质" SortExpression="事务性质" />
                <asp:BoundField DataField="操作参数" HeaderText="操作参数" SortExpression="操作参数" />
                <asp:BoundField DataField="批次名称" HeaderText="批次名称" SortExpression="批次名称" />
                <asp:BoundField DataField="迎新年" HeaderText="迎新年" SortExpression="迎新年" />
                <asp:BoundField DataField="操作名称" HeaderText="操作名称" SortExpression="操作名称" />
                <asp:BoundField DataField="操作地址" HeaderText="操作地址" SortExpression="操作地址" />
                <asp:BoundField DataField="操作类型" HeaderText="操作类型" SortExpression="操作类型" />
                <asp:BoundField DataField="学历层次" HeaderText="学历层次" SortExpression="学历层次" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ds_affair" runat="server" ConnectionString="<%$ ConnectionStrings:yxxt_dataConnectionString1 %>" SelectCommand="SELECT a.PK_Affair_NO AS 主键, a.Affair_Index AS 内部索引号, a.Affair_Name AS 事务名称, a.Affair_Type AS 事务类型, a.Precondition1 AS 前置条件1, a.Precondition2 AS 前置条件2, a.Call_Function AS 事务状态回调函数, a.Affair_CHAR AS 事务性质, a.Parameters AS 操作参数, b.Batch_Name AS 批次名称, b.Year AS 迎新年, c.OPER_Name AS 操作名称, c.OPER_URL AS 操作地址, c.OPER_Type AS 操作类型, d.Item_Name AS 学历层次 FROM Fresh_Affair AS a INNER JOIN Fresh_Batch AS b ON a.FK_Batch_NO = b.PK_Batch_NO INNER JOIN Fresh_OPER AS c ON a.FK_OPER_NO = c.PK_OPER_NO INNER JOIN Base_Code_Item AS d ON b.STU_Type = d.Item_NO WHERE (d.FK_Code = '001') ORDER BY 迎新年, b.PK_Batch_NO, 内部索引号"></asp:SqlDataSource>
    </form>
</body>
</html>

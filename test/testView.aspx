<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testView.aspx.cs" Inherits="test_testView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <%--<div>

        <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="LinqDataSource1" DataTextField="Batch_Name" DataValueField="PK_Batch_NO">
            <asp:ListItem Selected="True" Value="">--请选择批次--</asp:ListItem>
        </asp:DropDownList>

        <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="model.organizationModelDataContext" EntityTypeName="" Select="new (PK_Batch_NO, Batch_Name)" TableName="Fresh_Batches">
        </asp:LinqDataSource>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" EmptyDataText="请选择相应批次" AllowPaging="True"></asp:GridView>
    </div>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getStuByBatch" TypeName="organizationService">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" DefaultValue="“0”" Name="batch" PropertyName="SelectedValue" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />--%>
        <img src="../nradmingl/yanzhengma.aspx" alt="点击刷新" style="width:100px;height:38px" />
    </form>
</body>

</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uumyhcx.aspx.cs" Inherits="admin_uumyhcx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin:0 0 0 0">
    <form id="form1" runat="server">
    <div>
  
    <asp:GridView ID="GridView1" runat="server"  EmptyDataText="在人事系统中未找到该用户,请确认姓名和身份证后再试一次!"
            AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:BoundField DataField="yhid" HeaderText="登录名" />
            <asp:BoundField DataField="sfz" HeaderText="身份证" />
            <asp:BoundField DataField="xm" HeaderText="姓名" />
            <asp:BoundField DataField="bmmc" HeaderText="部门" />
        </Columns>
        </asp:GridView>
        

        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="getBySearch" TypeName="uum">
            <SelectParameters>
                <asp:QueryStringParameter Name="key" QueryStringField="sfz" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>

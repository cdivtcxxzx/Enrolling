<%@ Page Language="C#" AutoEventWireup="true" CodeFile="djks.aspx.cs" Inherits="admin_djks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="编号" SortExpression="id" />
                <asp:BoundField DataField="lx" HeaderText="等级类型" SortExpression="lx" />
 

                 <asp:TemplateField  HeaderText="题目问题"  SortExpression="tm">
               
                <ItemTemplate>
                    <%# mypassok(Eval("tm").ToString())%>
                </ItemTemplate>
               
        </asp:TemplateField> 
        <asp:TemplateField  HeaderText="选项1"  SortExpression="qs1">
               <ItemTemplate><%# mypassok(Eval("qs1").ToString())%></ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField  HeaderText="选项2"  SortExpression="qs2">
               <ItemTemplate><%# mypassok(Eval("qs2").ToString())%></ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField  HeaderText="选项3"  SortExpression="qs3">
               <ItemTemplate><%# mypassok(Eval("qs3").ToString())%></ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField  HeaderText="选项4"  SortExpression="qs4">
               <ItemTemplate><%# mypassok(Eval("qs4").ToString())%></ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField  HeaderText="选项5"  SortExpression="qs5">
               <ItemTemplate><%# mypassok(Eval("qs5").ToString())%></ItemTemplate>
        </asp:TemplateField>
                 <asp:TemplateField  HeaderText="答案"  SortExpression="da">
               <ItemTemplate><%# mypassok(Eval("da").ToString())%></ItemTemplate>
        </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:djksConnectionString %>" 
            SelectCommand="SELECT [id], [lx], [tm], [qs1], [qs2], [qs3], [qs4], [qs5], [da] FROM [jctk] ORDER BY [lx], [id]">
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

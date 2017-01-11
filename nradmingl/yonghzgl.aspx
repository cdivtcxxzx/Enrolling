<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yonghzgl.aspx.cs" Inherits="admin_yonghzgl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>用户组管理</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
</head>
<body id="C_User">
    <form id="form1" runat="server">
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;用户组管理</a>
			<a href="yonghzadd.aspx">添加新用户组</a>
            <asp:Label ID="Label1" runat="server"></asp:Label>
		    
			
		</td>
		
		<td style="text-align:right">
        </td>
	</tr>
  </thead>
</table>
 
  <br />
 
<div id="PanelDefault">

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1"  CssClass="table"  OnRowCommand="GridView1_RowCommand1"
        AllowPaging="True" AllowSorting="True" PageSize="20" 
        OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="ZID" 
      >
        <Columns>
            
            <asp:BoundField DataField="ZID" HeaderText="编号" HeaderStyle-Width="50"  
                SortExpression="ZID" InsertVisible="False" ReadOnly="True" >
            
            </asp:BoundField>
            <asp:BoundField DataField="ZMC" HeaderText="组名称"  SortExpression="ZMC"  
                HeaderStyle-Width="120" />
          <%--  <asp:BoundField DataField="ZQX" HeaderText="组权限" SortExpression="ZQX" />--%>

              <asp:TemplateField  HeaderText="用户列表"  >
            <HeaderStyle />
            <ItemTemplate><asp:Label runat="server" ID="qxlb" Text='<%#GetZMembers(Eval("ZID").ToString())%>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="总计" HeaderStyle-Width="40" >
            <HeaderStyle />
            <ItemTemplate><asp:Label runat="server" ID="total" Text='<%#this.num%>'></asp:Label></ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="ZSM" HeaderText="组说明"  SortExpression="ZSM"  HeaderStyle-Width="120"/>
               <%-- <asp:ButtonField HeaderText="管理" Text="查看"  HeaderStyle-Width="25" />--%>
            <asp:HyperLinkField DataNavigateUrlFormatString="yonghzUpdate.aspx?zid={0}"   HeaderStyle-Width="25" DataNavigateUrlFields="ZID" Text="修改"  HeaderText="修改" />
                        <asp:TemplateField  HeaderText="删除"  >
            
            <ItemTemplate>
<asp:LinkButton ID="Button3" runat="server" CommandName="删除"  CommandArgument='<%#Eval("ZID")%>'    Text='删除'>   </asp:LinkButton>
       </ItemTemplate></asp:TemplateField>
            
             <asp:HyperLinkField DataNavigateUrlFormatString="GroupManage.aspx?zid={0}"   HeaderStyle-Width="50" DataNavigateUrlFields="ZID" Text="成员管理"  HeaderText="成员管理" />
            
            <%--<asp:ButtonField Text="删除"  HeaderStyle-Width="25" />--%>
        </Columns>
         <PagerTemplate>
<span style="float:left;">
            每页<asp:Label ID="LabelPageSize" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageSize %>"></asp:Label>
            条 当前<asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex+1 %>"></asp:Label>
            /<asp:Label ID="Label3" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
            页&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First"
                CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=0 %>">首页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=0 %>">上一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">下一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">尾页</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;转到<asp:TextBox ID="txt_go" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>

            <asp:LinkButton ID="LinkButtonGo" runat="server"  CssClass="click" Text="跳转" OnClick="LinkButtonGo_Click" />
            </span><span style="float:right;"><b>总记录：<%# Session["YHZTotalRows"].ToString()%></b>&nbsp;&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /> </span>
        </PagerTemplate>

      
        
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        SelectCommand="SELECT [ZID], [ZMC], [ZQX], [ZSM] FROM [zhuqx] order by px">
    </asp:SqlDataSource>
   
    
    

    </div>
    </form>
</body>
</html>
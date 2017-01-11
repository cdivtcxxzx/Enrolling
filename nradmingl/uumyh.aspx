<%@ Page Language="C#" AutoEventWireup="true" CodeFile="uumyh.aspx.cs" Inherits="admin_uumyh" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>UUM</title>
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
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;UUM获取到的用户</a>&nbsp;
			
		    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">全部写入数据库</asp:LinkButton>
			
			<a href="yonghgl.aspx">返回用户管理</a></td>
            <td style="text-align:right">
            
            
                <asp:TextBox ID="SearchBox" runat="server"></asp:TextBox>
                <asp:Button ID="SearchButton" runat="server" Text="搜索用户" onclick="Search_Onclick" CssClass="click"/>
            
            
            </td>
		
	</tr>
  </thead>
</table>
 
  <br />
 
<div id="PanelDefault">
<asp:ScriptManager ID="sm1" runat="server" />
<asp:UpdatePanel ID="up1" runat="server" >
<Triggers>
<asp:PostBackTrigger ControlID="GridView1" />
</Triggers>
<ContentTemplate >
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
          CssClass="table"  OnRowCommand="GridView1_RowCommand"  DataSourceID="srcUum"
        AllowPaging="True" AllowSorting="True"   EmptyDataText="无相关数据！"
        OnPageIndexChanging="GridView1_PageIndexChanging"  onselectedindexchanged="GridView1_SelectedIndexChanged"

      >
        <Columns>
            <asp:BoundField DataField="xm" HeaderText="真实姓名" HeaderStyle-Width="70"   InsertVisible="False" 
                ReadOnly="True" SortExpression="xm"  >
         
            </asp:BoundField>
            <asp:BoundField DataField="gh" HeaderText="工号" HeaderStyle-Width="40"  SortExpression="gh" >
            
            </asp:BoundField>
            <asp:BoundField DataField="yhid" HeaderText="登陆名"  SortExpression="yhid"  HeaderStyle-Width="80" />
            <asp:BoundField DataField="sfz" HeaderText="身份证号" SortExpression="sfz" HeaderStyle-Width="80"/>
            <asp:BoundField DataField="bmmc" HeaderText="部门"  SortExpression="bmmc"  HeaderStyle-Width="80" />
            <asp:BoundField DataField="zw" HeaderText="职务" 
                SortExpression="zw"  HeaderStyle-Width="80" />
            <asp:BoundField DataField="dh" HeaderText="电话" SortExpression="dh"  HeaderStyle-Width="108" />
            <asp:ButtonField  HeaderText="管理" Text="导入"  CommandName="DaoRu"   HeaderStyle-Width="25" />
           
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
            </span><span style="float:right;"><b>总记录：<%# Session["UumTotalRows"].ToString() %></b>&nbsp;&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /> </span>
        </PagerTemplate>
        
    </asp:GridView>
    </ContentTemplate></asp:UpdatePanel>

    <asp:ObjectDataSource 
         ID="srcUum"
         TypeName="uum"
         SelectMethod="getAll"
         runat="server"
    >
    
    </asp:ObjectDataSource> 
    <asp:ObjectDataSource
     ID="uumSearch"
      runat="server" SelectMethod="getBySearch" TypeName="uum">
        <SelectParameters>
            <asp:ControlParameter ControlID="SearchBox" Name="key" PropertyName="Text" 
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
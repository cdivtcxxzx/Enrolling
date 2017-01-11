<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BanjMan.aspx.cs" Inherits="admin_BanjMan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>班级管理</title>
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
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;班级管理</a>
			<%--<a href="yonghadd.aspx">添加用户</a>--%>
            <asp:HyperLink runat="server" ID="nav_1" NavigateUrl="#" Text="手动添加"></asp:HyperLink>
           
			
			
		</td>
		
		<td style="text-align:right"><asp:TextBox ID="searchtext" 
                runat="server" name="1" txttop="txttop" title="搜索"></asp:TextBox>
           <asp:Button 
                ID="Button1" runat="server" Text="搜索" CssClass="click" onclick="Search_Onclick" />
        </td>
	</tr>
  </thead>
</table>
 
  <br />
 
<div id="PanelDefault">
<asp:ScriptManager ID="sm1" runat="server" />
<asp:UpdatePanel ID="up1" runat="server" ><ContentTemplate >
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <asp:GridView ID="GridView1" runat="server"    CssClass="table"  
            CellPadding="2" CellSpacing="1" GridLines="None" AlternatingRowStyle-CssClass="dgAlter"     AutoGenerateColumns="False" 
        EmptyDataText="无相关数据！"   DataSourceID="srcAccount"
        AllowPaging="True" AllowSorting="True"  OnRowCommand="GridView1_RowCommand1" 
        OnPageIndexChanging="GridView1_PageIndexChanging" OnDataBound="GridView1_DataBound" 
      >
        <Columns>
         <asp:TemplateField HeaderText="">  
         <HeaderTemplate>  
          &nbsp;
         </HeaderTemplate>  
         <ItemTemplate>  
            &nbsp;
         </ItemTemplate>  
         <ItemStyle Height="25px" HorizontalAlign="Center" />  
         <HeaderStyle Width="1" Height="28px" HorizontalAlign="Center" />  
       </asp:TemplateField>  
            
            <asp:BoundField DataField="banj_id" HeaderText="班级id"  HeaderStyle-Width="70"  SortExpression="banj_id" >
            
            <HeaderStyle Width="70px" />
            
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="学校名称"  SortExpression="name"  
                HeaderStyle-Width="280" >
            <HeaderStyle Width="280px" />
            </asp:BoundField>
            <%--<asp:BoundField DataField="yhqx" HeaderText="权限列表" SortExpression="yhqx" />--%>

             <%--<asp:TemplateField  HeaderText="权限列表"  >
            <HeaderStyle  />
            <ItemTemplate><asp:Label runat="server" ID="qxlb" Text='<%#GetPower1(Eval("yhqx").ToString())%>'></asp:Label></ItemTemplate>
            </asp:TemplateField>--%>




            
<%--            <asp:ButtonField HeaderText="管理" Text="查看"  HeaderStyle-Width="25" >
            <HeaderStyle Width="25px" />
            </asp:ButtonField>--%>
            <asp:HyperLinkField DataNavigateUrlFormatString="BanjUpdate.aspx?mode=edit&id={0}&school={1}"   HeaderStyle-Width="25" DataNavigateUrlFields="banj_id,school_id" Text="修改" HeaderText="管理" />

             <asp:TemplateField  HeaderText="删除"  >
            
            <ItemTemplate>
<asp:LinkButton ID="Button3" runat="server" CommandName="删除"  CommandArgument='<%#Eval("banj_id")%>'  OnClientClick="return confirm('是否删除')"   Text='删除'>   </asp:LinkButton>
                
       </ItemTemplate></asp:TemplateField>
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
                 <%//多页翻页
                     if (GridView1.PageCount >= 10 && GridView1.PageCount - GridView1.PageIndex >= 10)
                  {
                     
                       %>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+2 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+2 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+3 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+3 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+4 %></asp:LinkButton>
               <asp:LinkButton ID="LinkButton5" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+5 %>"
                CommandName="Page"><%#((GridView)Container.NamingContainer).PageIndex + 5%></asp:LinkButton> 
                <asp:LinkButton ID="LinkButton6" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+6%>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 6%></asp:LinkButton>
                <asp:LinkButton ID="LinkButton7" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+7 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 7%></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton9" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+8 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 8%></asp:LinkButton>
                 <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+9 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 9%></asp:LinkButton>
                 <asp:LinkButton ID="LinkButton12" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+10 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 10%></asp:LinkButton>
                <%}
                    else if(GridView1.PageCount >= 8 && GridView1.PageCount - GridView1.PageIndex >= 5){ %>
                    <asp:LinkButton ID="LinkButton8" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+2%>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 2%></asp:LinkButton>
                <asp:LinkButton ID="LinkButton10" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+3 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 3%></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton11" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 4%></asp:LinkButton>
                    <%}%>
            <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">下一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">尾页</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;转到<asp:TextBox ID="txt_go" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>

            <asp:LinkButton ID="LinkButtonGo" runat="server"  CssClass="click" Text="跳转" OnClick="LinkButtonGo_Click" />
            </span><span style="float:right;"><b>总记录：<%# rownum %></b>&nbsp;&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /> </span>
        </PagerTemplate>

      
        
    </asp:GridView>
    </ContentTemplate></asp:UpdatePanel>
    <asp:ObjectDataSource 
         ID="srcAccount"
         TypeName="Banj"
         SelectMethod="getAllByUser"
         runat="server" OnSelected="srcAccount_Selected" OldValuesParameterFormatString="original_{0}"
    >
        <SelectParameters>
            <asp:SessionParameter Name="UserName" SessionField="UserName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource
         ID="accountSearch"
          runat="server" SelectMethod="getByKeyByUser" TypeName="Banj" OnSelected="accountSearch_Selected"
    >
        <SelectParameters>
            <asp:ControlParameter ControlID="searchtext" Name="key" PropertyName="Text" 
                Type="String" />
            <asp:SessionParameter Name="UserName" SessionField="UserName" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    </div>
    </form>
</body>
</html>

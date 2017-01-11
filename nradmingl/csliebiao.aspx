<%@ Page Language="C#" AutoEventWireup="true" CodeFile="csliebiao.aspx.cs" Inherits="admin_cs" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>优化后的用户管理界面</title>
<link runat="server" id="webcss" type="text/css" href="styleqt.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>

 
  


</head>
<body id="C_User">
    <form id="form1" runat="server">
    <%--<div style="color:#333;font-family: 宋体,Verdana,Arial,sans-serif; font-weight:bold; border-bottom:2px solid #C0C0C0; height:25px; line-height:25px; font-size:14px; cellpadding:0;cellspacing:0 "><span style='color:#D84009;'>当前位置>>用户管理>>列表显示</span></div>
--%>
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td><a href="?action=manage">当前位置&gt;&gt;&nbsp;用户管理</a>
			<a href="yonghadd.aspx">添加用户</a>
            
           
						
		</td>
		
		<td style="text-align:right;width:180px;">

        
        <asp:TextBox ID="searchtext" 
                runat="server" name="1" txttop="txttop" title="根据用户姓名或登陆ID搜索用户" style=" margin-right:0px; height:18px;width:100px; border:1px solid #DBDDDE;float:left; "></asp:TextBox>
           
           <asp:ImageButton ID="ImageButton1" runat="server" 
                ImageUrl="images/sous.gif" onclick="Search_Onclick" ToolTip="搜索查找用户" style=" margin-left:0px; border:0px;float:left;" />
&nbsp;</td>
        
	</tr>
  </thead>
</table>
 

 
<div id="PanelDefault">
<asp:ScriptManager ID="sm1" runat="server" />
<asp:UpdatePanel ID="up1" runat="server" ><ContentTemplate >
    <asp:GridView ID="GridView1" runat="server"    CssClass="table"  
            CellPadding="2" CellSpacing="1" GridLines="None" AlternatingRowStyle-CssClass="dgAlter"     AutoGenerateColumns="False" 
        EmptyDataText="无相关数据！"   DataSourceID="srcAccount"
        AllowPaging="True" AllowSorting="True" 
        OnPageIndexChanging="GridView1_PageIndexChanging" 
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
         <HeaderStyle Width="1" Height="28px"  HorizontalAlign="Center" />  
       </asp:TemplateField>  
            <asp:TemplateField HeaderText="">  
         <HeaderTemplate>  
           
         </HeaderTemplate>  
         <ItemTemplate>  
           <div><img height="22" titop="titop" width="22" alt='http://lyncex.cdivtc.com:8001/Photo/UploadHead.aspx?operation=get&user=<%#(Convert.ToString(Eval("yhid")))%>' src='http://lyncex.cdivtc.com:8001/Photo/UploadHead.aspx?operation=get&user=<%#(Convert.ToString(Eval("yhid")))%>' /> </div> 
         </ItemTemplate>  
         <ItemStyle Height="24px" HorizontalAlign="Center" />  
         <HeaderStyle Width="3%" Height="24px" HorizontalAlign="Center" />  
       </asp:TemplateField>  
            <asp:BoundField DataField="xm" HeaderText="真实姓名"  HeaderStyle-Width="70"  SortExpression="xm" >
            
            <HeaderStyle Width="70px" />
            
            </asp:BoundField>
            <asp:BoundField DataField="yhid" HeaderText="登陆名"  SortExpression="yhid"  
                HeaderStyle-Width="80" >
            <HeaderStyle Width="80px" />
            </asp:BoundField>
            <%--<asp:BoundField DataField="yhqx" HeaderText="权限列表" SortExpression="yhqx" />--%>

             <asp:TemplateField  HeaderText="权限列表"  >
            <HeaderStyle Width="80px" />
            <ItemTemplate><asp:Label runat="server" ID="qxlb" Text='<%#GetPower1(Eval("yhqx").ToString())%>'></asp:Label></ItemTemplate>
            </asp:TemplateField>




            <asp:BoundField DataField="lsz" HeaderText="所在用户组"  SortExpression="lsz"  
                HeaderStyle-Width="80" >
            <HeaderStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="dltime" HeaderText="最后访问时间" 
                SortExpression="dltime"  HeaderStyle-Width="120" >
            <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="fwcs" HeaderText="访问次数" SortExpression="fwcs"  
                HeaderStyle-Width="108" >
            <HeaderStyle Width="35px" />
            </asp:BoundField>
<%--            <asp:ButtonField HeaderText="管理" Text="查看"  HeaderStyle-Width="25" >
            <HeaderStyle Width="25px" />
            </asp:ButtonField>--%>
            <asp:HyperLinkField DataNavigateUrlFormatString="yhupdate.aspx?yhid={0}"   HeaderStyle-Width="25" DataNavigateUrlFields="yhid" Text="修改" HeaderText="管理" />
<%--            <asp:ButtonField Text="删除"  HeaderStyle-Width="25" >
            <HeaderStyle Width="25px" />
            </asp:ButtonField>--%>
        </Columns>
        
         <PagerTemplate>
<span style="  float:left;">
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
                <asp:LinkButton ID="LinkButton1"  runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+2 %>"
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
            </span><span style="float:right;"><b>总记录：<%# Session["YhqxTotalRows"].ToString() %></b>&nbsp;&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /> </span>
       </div>
        </PagerTemplate>

      
        
    </asp:GridView>
    </ContentTemplate></asp:UpdatePanel>
    <asp:ObjectDataSource 
         ID="srcAccount"
         TypeName="Account"
         SelectMethod="getAllUser"
         runat="server"
    />
    <asp:ObjectDataSource
         ID="accountSearch"
          runat="server" SelectMethod="getByKey" TypeName="Account"
    >
        <SelectParameters>
            <asp:ControlParameter ControlID="searchtext" Name="key" PropertyName="Text" 
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    </div>
    </form>
</body>
</html>

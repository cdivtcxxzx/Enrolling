<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupManage.aspx.cs" Inherits="admin_GroupManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>新生换专业</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<link rel="stylesheet" href="sqtime/jquery-ui.css" /> 
 <script type="text/javascript"  src="sqtime/jquery-ui.js"></script> 
 <script type="text/javascript"  src="sqtime/jquery.ui.datepicker-zh-CN.js"></script> 
 <script type="text/javascript">
     function banjidialog() {
         $("#dialog").dialog({
             resizable: false,
             width: 433,
             modal: true,
             buttons: {
                 "取消": function () {
                     $(this).dialog("close");
                 }
             }
         });
     }
 </script>
  <%--<style type="text/css">#dialog{display:none;}</style> --%>
</head>
<body>
    <form id="form1" runat="server">
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;组成员管理</a>
			
			
			
		</td>
		
<%--		<td style="text-align:right">
            输入用户的登录名、姓名或者部门名搜索
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button_Search" runat="server" CssClass="click" Text="搜索" 
            onclick="Button_Search_Click"/></td>--%>
	</tr>
  </thead>
</table>
 
  <br />
  
<div id="PanelDefault"  style="float:left;width:50%;">
       <table class="table" style="margin-top:8px;">
<thead>
<tr>
    
	<td style="text-align:left">
          <asp:Label ID="lb1" runat="server" ></asp:Label>  组现有成员</td>
</tr>
</thead>
</table>
        <asp:GridView ID="GV_xs" runat="server" AutoGenerateColumns="False"  AllowPaging="true" AllowSorting="true"
            DataSourceID="Sql_xs"  CssClass="table" OnRowCommand="GridView1_RowCommand" PageSize="10" EmptyDataText="该组没有成员!" >
            
<Columns>
<asp:BoundField   DataField="yhid"  HeaderStyle-Width="80" HeaderText="登录名" 
        SortExpression="yhid">
<HeaderStyle Width="80"></HeaderStyle>
    </asp:BoundField>
<asp:BoundField   DataField="xm"  HeaderStyle-Width="70" HeaderText="姓名" 
        SortExpression="xm">
<HeaderStyle Width="70"></HeaderStyle>
    </asp:BoundField>
<%--<asp:BoundField   DataField="sfzjh"   HeaderText="身份证号" 
        SortExpression="sfzjh">

    </asp:BoundField>--%>
    <asp:BoundField   DataField="yxmc"   HeaderText="所属部门" 
        SortExpression="yxmc">
    </asp:BoundField>

    <asp:ButtonField  Text="删除"  CommandName="shanchu" ButtonType="Button" HeaderStyle-Width="15" Visible="true" HeaderText="管理"/>
   <%-- <asp:ButtonField HeaderText="" Text="转系"  CommandName="toAnotherDep" ButtonType="Button" HeaderStyle-Width="15"/>--%>
    <%--<asp:TemplateField><ItemTemplate><asp:Button Text="换专业" OnClientClick="" /></ItemTemplate></asp:TemplateField>--%>
    
</Columns>
 <PagerTemplate>
<span style="float:left;">


            每页<asp:Label ID="LabelPageSize" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageSize %>"></asp:Label>
            人 当前<asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex+1 %>"></asp:Label>
            /<asp:Label ID="Label3" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
            页&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First"
                CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=0 %>">首页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=0 %>">上一页</asp:LinkButton>
                <%if (GV_xs.PageCount >= 8 && GV_xs.PageCount - GV_xs.PageIndex >= 8)
                  {
                     
                       %>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+2 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+2 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+3 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+3 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+4 %></asp:LinkButton>
               <asp:LinkButton ID="LinkButton5" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+5 %>"
                CommandName="Page"><%#  ((GridView)Container.NamingContainer).PageIndex+5 %></asp:LinkButton> 
                <asp:LinkButton ID="LinkButton6" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+6 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+6 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton7" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+7 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+7 %></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton9" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+8 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+8 %></asp:LinkButton>

                <%}
                  else if (GV_xs.PageCount >= 8 && GV_xs.PageCount - GV_xs.PageIndex >= 5)
                  { %>
                    <asp:LinkButton ID="LinkButton8" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+2%>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+2 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton10" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+3 %>" CommandName="Page"> <%#  ((GridView)Container.NamingContainer).PageIndex+3 %></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton11" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+4 %></asp:LinkButton>
                    <%}%>

            <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">下一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">尾页</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;转到<asp:TextBox ID="txt_go" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>

            <asp:LinkButton ID="LinkButtonGo" runat="server"  Text="跳转" OnClick="LinkButtonGo_Click" />
            </span><span style="float:right;"><b>成员数:<%#ViewState["count"].ToString()%>人</b>&nbsp;&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>人<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /> </span>
        </PagerTemplate>
</asp:GridView>
<asp:SqlDataSource 
        ID="Sql_xs" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
           onselected="Sql_xs_Selected"
        
            SelectCommand="select *,yxmc from yonghqx a left join dm_yuanxi b on a.yxdm=b.yxdm  where lsz like @zid+',%' or lsz like '%,'+@zid+',%'">
    <SelectParameters>
        <asp:QueryStringParameter Name="zid" QueryStringField="zid" />
    </SelectParameters>
            </asp:SqlDataSource>
        
    </div>
    <%--<asp:Label runat="server" ID="Label2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请先查找学生,才能进行换专业操作,并点击换专业按钮!</asp:Label>--%>

<div   style="float:right;width:50%;">
<table class="table" style="margin-top:8px;">
<thead>
<tr>
	<td style="text-align:left">
            添加成员</td>
</tr>
<tr>
	<td style="text-align:left">
            输入用户的登录名、姓名或者部门名搜索
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button_Search" runat="server" CssClass="click" Text="搜索" 
            onclick="Button_Search_Click"/></td>
</tr>
</thead>
</table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  AllowPaging="true" AllowSorting="true"
            DataSourceID="SqlDataSource1"  CssClass="table" OnRowCommand="GridView1_RowCommand" PageSize="10" EmptyDataText="请搜索添加用户!" >
            
<Columns>
<asp:BoundField   DataField="yhid"  HeaderStyle-Width="80" HeaderText="登录名" 
        SortExpression="yhid">
<HeaderStyle Width="80"></HeaderStyle>
    </asp:BoundField>
<asp:BoundField   DataField="xm"  HeaderStyle-Width="70" HeaderText="姓名" 
        SortExpression="xm">
<HeaderStyle Width="70"></HeaderStyle>
    </asp:BoundField>
<%--<asp:BoundField   DataField="sfzjh"   HeaderText="身份证号" 
        SortExpression="sfzjh">

    </asp:BoundField>--%>
    <asp:BoundField   DataField="uumzw"   HeaderText="所属部门" 
        SortExpression="uumzw">
    </asp:BoundField>

    <asp:ButtonField  Text="添加"  CommandName="tianjia" ButtonType="Button" HeaderStyle-Width="15" Visible="true" HeaderText="管理"/>
   <%-- <asp:ButtonField HeaderText="" Text="转系"  CommandName="toAnotherDep" ButtonType="Button" HeaderStyle-Width="15"/>--%>
    <%--<asp:TemplateField><ItemTemplate><asp:Button Text="换专业" OnClientClick="" /></ItemTemplate></asp:TemplateField>--%>
    
</Columns>
</asp:GridView>
<asp:SqlDataSource 
        ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
           onselected="SqlDataSource1_Selected"
        
            SelectCommand="SELECT yhid,xm,uumzw from yonghqx where yhid like '%' + @text + '%' or xm like '%' + @text + '%' or uumzw like '%' + @text + '%'">
    <SelectParameters>
        <asp:ControlParameter ControlID="TextBox1" Name="text" PropertyName="Text" />
    </SelectParameters>
            </asp:SqlDataSource>
<table class="table"><tr><td><asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td></tr><asp:Label ID="action_tip" runat="server" ForeColor="Red" ></asp:Label></table></div>
    </form>
</body>
</html>

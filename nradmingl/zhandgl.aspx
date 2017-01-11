<%@ Page Language="C#" AutoEventWireup="true"  validateRequest="false" CodeFile="zhandgl.aspx.cs" Inherits="admin_zhandgl" %>
<%@Register TagPrefix="custom" Namespace="myControls"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>站点配置页</title>
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
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;网站信息配置</a>

		</td>
		
		<td style="text-align:right">
        </td>
	</tr>
  </thead>
</table>
 
  <br />
 
<div id="PanelDefault">

<table class="table" cellspacing="0" rules="all" width="100%" border="1" id="Table1">
		<tr>
			<th><a href="">配置标题</a><th><a href="">配置关键字</a></th><th><a href="">配置信息内容</a></th><th><a href="">排序</a></th><th><a href="">配置说明</a></th><th>管理选项</th>
		</tr>
        <tr>
			<td scope="col">
                <asp:TextBox ID="TextBox4" 
                    runat="server" Width="90%"></asp:TextBox>
                </td><td  >
                <asp:TextBox 
                    ID="TextBox3" runat="server" Width="90%"></asp:TextBox>
                </td><td >
                <asp:TextBox 
                    ID="TextBox5" runat="server" TextMode="MultiLine" Width="90%"></asp:TextBox>
                </td><td>
                <asp:TextBox 
                    ID="TextBox6" runat="server" OnKeyPress="if((event.keyCode>=48)&&(event.keyCode <=57)) {event.returnValue=true;} else{event.returnValue=false;}" Width="40px"></asp:TextBox>
                </td><td scope="col">
                <asp:TextBox 
                    ID="TextBox7" runat="server" TextMode="MultiLine"  Width="90%"></asp:TextBox>
                </td><td scope="col">
                <asp:Button ID="Button1" runat="server" CssClass="click" Text="添加配置" 
                    onclick="Button1_Click" />
                
            </td>
		</tr>
</table>
<br />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1"  CssClass="table" 
        AllowPaging="True" AllowSorting="True" PageSize="100" DataKeyNames="xxid" >
        <Columns>
            <asp:BoundField DataField="xxid" HeaderText="编号" HeaderStyle-Width="40"   InsertVisible="False" 
                ReadOnly="True" SortExpression="xxid" >
         
<HeaderStyle Width="40px"></HeaderStyle>
         
            </asp:BoundField>
            <asp:BoundField DataField="title" HeaderText="配置标题" HeaderStyle-Width="140"  
                SortExpression="title" >
            
<HeaderStyle Width="140px"></HeaderStyle>
            
            </asp:BoundField>
            <asp:BoundField DataField="xxgjz" HeaderText="配置关键字"  SortExpression="xxgjz"  
                HeaderStyle-Width="120" >
<HeaderStyle Width="80px"></HeaderStyle>
            </asp:BoundField>
                <custom:LongTextField DataField="xxnr" HeaderText="配置信息内容" HeaderStyle-Width="120" SortExpression="xxnr"  />
            
            <asp:BoundField DataField="xxpx" HeaderText="排序"  SortExpression="xxpx"  
                HeaderStyle-Width="30" >
<HeaderStyle Width="80px"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="xxbz" HeaderText="配置说明" 
                SortExpression="xxbz"  >
<HeaderStyle Width="120px"></HeaderStyle>
            </asp:BoundField>
            <asp:CommandField HeaderText="管理选项"   ShowDeleteButton="true"  ShowEditButton="True" HeaderStyle-Width="45"  ShowHeader="True">
            <ControlStyle CssClass="manage" />
            </asp:CommandField>
            
       
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

            <asp:Button ID="LinkButtonGo" runat="server"  CssClass="click" Text="跳转" />
            </span><span style="float:right;"><b>总记录：条</b>&nbsp;&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="TextBox2" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:Button ID="buttion2" runat="server"  Text="设置"   CssClass="click" /> </span>
        </PagerTemplate>

    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        
        
        SelectCommand="SELECT * FROM [wangzxx]" 
        
        UpdateCommand ="UPDATE wangzxx SET title = @title, xxgjz = @xxgjz, xxnr = @xxnr, xxpx = @xxpx, xxbz = @xxbz WHERE (xxid = @xxid)" 
        DeleteCommand="DELETE FROM wangzxx WHERE (xxid = @xxid) and xxgjz<>'isopen' and  xxgjz<>'系统授权'">
        <UpdateParameters>
            <asp:Parameter Name="title" />
            <asp:Parameter Name="xxid" />
        </UpdateParameters>
    </asp:SqlDataSource>
    
    

    </div>
    </form>
</body>
</html>
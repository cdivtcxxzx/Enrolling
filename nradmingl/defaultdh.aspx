<%@ Page Language="C#" AutoEventWireup="true" CodeFile="defaultdh.aspx.cs" Inherits="admin_defaultdh" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<style type="text/css">
th {width:140px;}
    .style1
    {
        width: 71px;
    }
    .style2
    {
        width: 110px;
    }
</style>
<!--客户端-->

<body>

<form id="form1" runat="server">

 <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;通知公告信息</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
		
		
	</tr>
  </thead>
</table>
<br />
<asp:FormView ID="FV_detail" runat="server" DataSourceID="SqlDataSource1" CssClass="table"
            EmptyDataText="暂时没有通知公告信息！">
            <ItemTemplate>
                <div class="content">
                    <h2>
                        标题:
                        <%#Eval("title") %></h2>
                    <br />
                    <label>
                        &nbsp;&nbsp;<font color=red><b><%#Eval("xxnr") %></label></b></font><br />
                   
                </div>
                
            </ItemTemplate>
        </asp:FormView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnString %>"
            SelectCommand="select title,xxnr  from wangzxx  where xxgjz='系统通知'">
           
        </asp:SqlDataSource>
<br />

<table class="table">
<thead>
<tr class="noeffect">
	<td colspan="2">用户信息</td>
</tr>
</thead>
<tr>
	<td>权限及分组</td>
	<td id="quanxfz" runat="server"><a href="showPower.aspx">查看我的权限和分组详情</a></td>
</tr>

<tr>
	<td>常用操作</td>
	<td><a href="changpwd.aspx">修改个人信息</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="yonghgl.aspx">用户管理</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="logout.aspx">注销登陆系统</a>&nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>

<tr>
	<td>最近操作</td>
	<td id="zjcz" runat="server"></td>
</tr>


<tr>
	<td>客户端信息</td>
	<td>登陆IP：<%=basic.GetIPAddress() %>&nbsp;浏览器：<%=Request.Browser.Browser.ToString() %> <%=Request.Browser.Version.ToString() %> （推荐使用IE浏览器）&nbsp;
</td>
</tr>

</table>
</form>
</body>
</html>

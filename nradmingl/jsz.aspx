<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jsz.aspx.cs" Inherits="admin_jsz" %>


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
    </style>
<!--客户端-->

<body>

<form id="form1" runat="server">

 <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;功能未开放</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
		
		
	</tr>
  </thead>
</table>
<br />
<asp:FormView ID="FV_detail" runat="server" DataSourceID="SqlDataSource1" CssClass="table"
            EmptyDataText="功能暂时没有开通！">
            <ItemTemplate>
                <div class="content">
                    <h2>
                        标题:
                        <%#Eval("title") %></h2>
                    <br />
                    <label>
                        &nbsp;&nbsp;<font color=red><b><%#Eval("xxnr") %></label></b></font><br /></div>
                
            </ItemTemplate>
        </asp:FormView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnString %>"
            SelectCommand="select title,xxnr  from wangzxx  where xxgjz='系统通知'">
           
        </asp:SqlDataSource>
<br />

</form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="shujkgl.aspx.cs" Inherits="admin_shujkgl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>数据库管理</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<link rel="stylesheet" href="sqtime/jquery-ui.css" /> 
 
 <script type="text/javascript"  src="sqtime/jquery-ui.js"></script> 
 <script type="text/javascript"  src="sqtime/jquery.ui.datepicker-zh-CN.js"></script> 
 


 


    <style type="text/css">
        .style1
        {
            width: 103px;
        }
    </style>

 


    </head>
<body id="C_User">
    <form id="form1" runat="server">
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;数据库管理</a>
			
			
			
		</td>
		
		<td style="text-align:right">
        
	    </td>
	</tr>
  </thead>
</table>
 

 
<div id="PanelDefault">
    
    <table class="table" style="margin-top:8px;">
<thead>
<tr>
	<td colspan="2"><a href="#" class="helpall">[?]&nbsp;数据库操作</a></td>
</tr>
</thead>
 
<tr id="manage_ParentID">
	<td class="style1" >数据库详情&nbsp;<a href="#" class="help">[?]</a></td>
	<td >
	<span class="note">数据库详细信息。</span>

	    &nbsp;&nbsp;&nbsp; 
	    当前数据库类型 Microsoft SQL Server 2008
    </td>
</tr>
<tr id="Tr2">
	<td class="style1" >数据库备份&nbsp;<a href="#" class="help">[?]</a></td>
	<td >
	<span class="note">数据库备份及恢复。</span>

	    &nbsp;&nbsp;<br />
&nbsp; &nbsp; 
	    <asp:Button ID="Button6" runat="server" CssClass="button" Text="备份数据库" />
&nbsp;
        <asp:Button ID="Button7" runat="server" CssClass="button" Text="恢复数据库" />
&nbsp;
        <asp:Button ID="Button8" runat="server" CssClass="button" Text="下载数据库" />
    </td>
</tr>
<tr id="Tr3">
	<td class="style1" >数据库日志&nbsp;<a href="#" class="help">[?]</a></td>
	<td >
	<span class="note">对数据库日志进行清理。</span>

	    &nbsp;&nbsp;<br />
&nbsp; &nbsp; 
	    <asp:Button ID="Button1" runat="server" CssClass="button" Text="清除日志" />
    </td>
</tr>
 <tr id="Tr1">
	<td class="style1" >执行数据库查询&nbsp;<a href="#" class="help">[?]</a></td>
	<td >
	   <span class="note">执行数据库查询语句。（高级）</span> &nbsp;<br />
        &nbsp;&nbsp;
        <asp:TextBox ID="TextBox7" runat="server" Width="416px"></asp:TextBox>
        <asp:Button ID="Button4" runat="server" CssClass="button" Text="执行" />
     </td>
</tr>
 
</table>

    
    </div>
    <script type="text/javascript">
        function onclicksel() {
            var chkobj = document.getElementById("BoxIdAll");
            if (chkobj.checked == true) {
                selAll();
            }
            else {
                removeAll();
            }
        }
        function selAll() {
            var selobj = document.getElementsByName("BoxId");
            for (var i = 0; i < selobj.length; i++) {
                if (!selobj[i].disabled) {
                    selobj[i].checked = true;
                }
            }
        }

        function removeAll() {
            var selobj = document.getElementsByName("BoxId");
            for (var i = 0; i < selobj.length; i++) {
                selobj[i].checked = false;
            }
        }
        function batchAudit(id) {
            var AuditVal = "";
            var bid = document.getElementsByName("BoxId");
            for (var i = 0; i < bid.length; i++) {
                if (bid[i].checked == true) {
                    AuditVal = AuditVal + bid[i].value + ",";
                }
            }
            if (AuditVal.length <= 0) {
                alert("请先选择要删除的记录");
                return false;
            }
            else {
                if (id == "btnDelete") {
                    if (confirm("您确认要批量删除记录吗？")) {
                        document.getElementById("hdfWPBH").value = AuditVal;
                        //alert(document.getElementById("hdfWPBH").value);
                        return true;
                    }
                    return false;
                }
            }
        }  
    </script>
    </form>
    </body>
</html>
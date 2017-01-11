<%@ Page Language="C#" AutoEventWireup="true" CodeFile="help.aspx.cs" Inherits="admin_help" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<meta name="keywords" content="" />
<meta name="description" content="" />

<title>建议或帮助</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
    <style type="text/css">
        .style1
        {
            width: 111px;
        }
        .style2
        {
            width: 401px;
        }
    </style>
</head>
<body id="C_User">
    <form id="form1" runat="server">
       <div id="PanelUpdate">
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;用户提供建议或寻求帮助</a><a href="help.ppt">系统帮助演示</a>

		</td>
		
		<td style="text-align:right">
        </td>
	</tr>
  </thead>
</table>
 
  <br />
 
	
<table class="table">
<thead>
<tr class="noeffect">
	<td colspan="2"><a href="#" class="helpall">[?]&nbsp;请填写你的建议或需要帮助的内容</a></td>
</tr>
</thead>
		<tr>
			<td >需改进的页面&nbsp;<a href="#" class="help">[?]</a></td>
			<td >
			<span class="note">用户在使用时觉得不好的页面名称,或需要改进的页面名称。</span>
                <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
				
			
			
			</td>
		</tr>	
        <tr>
			<td  >问题或建议&nbsp;<a href="#" class="help">[?]</a></td>
			<td >
			<span class="note">请详描你需要帮助的内容,或需改进的内容。</span>
                <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" Height="258px" 
                    Width="548px"></asp:TextBox>
				
			
			
			    </td>
		</tr>	
<tr class="noeffect">
	<td ></td>
	<td>
        <asp:Button ID="Button1" runat="server" CssClass="button" 
            onclick="Button1_Click" Text="确认提交" />
        <input type="reset" class="button" value='重新填写' /></td>
</tr>
</table>
</div>
</form>
</body>
</html>

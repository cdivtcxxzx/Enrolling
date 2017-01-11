<%@ Page Language="C#" AutoEventWireup="true" CodeFile="divpopupdemo.aspx.cs" Inherits="admin_divpopupdemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<style> 
.black_overlay{ display: none; position: absolute; top: 0%; left: 0%; width: 100%; height: 100%;
background-color: black; z-index:1001; -moz-opacity: 0.8; opacity:.60; filter: alpha(opacity=60); }
.white_content { display: none; position: absolute; top: 25%; left: 25%; width: 50%; height: 50%;
padding: 5px;border: 5px solid #999999;  background-color: white; z-index:1002; overflow: auto; }
</style> 

<link runat="server" id="webcss" type="text/css" href="artDialog/skins/default.css" rel="stylesheet" rev="stylesheet" media="all" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <a href="javascript:void(0)" onclick="document.getElementById
('xxts').style.display='block';document.getElementById('fade').style.display='block'">打开</a> 
    <asp:Button ID="Button1" runat="server" Text="后台打开" onclick="Button1_Click" />

<div id="fade" runat="server" txt="用于全屏黑掉" class="black_overlay"> 
</div>

    <div id="xxts" runat="server" class="white_content">
    <table width=100%><tr><td width="100%"><div class="aui_titleBar"> 
	<div id="xxtstitle" runat="server" class="aui_title">提示标题</div> 
										 <a class="aui_close" href="javascript:void(0)" onclick="document.getElementById
('xxts').style.display='none';document.getElementById('fade').style.display='none'">X</a> 
									</div> </td></tr>
                                    <tr><td width=100%><div id="xxtsnr"  style="float:left" runat="server">内容显示区，.white_content { display: none; position: absolute; top: 25%; left: 25%; width: 50%; height: 50%;
padding: 5px;}控制大小，<br /><asp:Button ID="Button2" runat="server" Text="后台关闭"  onclick="Button1_Click1" /></div> </td></tr></table>
    
	
    </div>

    </form>
</body>
</html>

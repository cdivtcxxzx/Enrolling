<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ptset.aspx.cs" Inherits="admin_ptset" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>添加用户</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
    <style type="text/css">
        .style1
        {
            width: 139px;
        }
        </style>
</head>
<body id="C_User">
    <form id="form1" runat="server">
       <div id="PanelUpdate">
       <table class="table">
	<thead>
	<tr>
		<td colspan="2"  style="padding:14px;">
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;系统参数设置</a>

		</td>
		

	</tr>
  </thead>
  </table>
  <br>
  
    <table class="table">
	
   <tr>
			<td style="padding:14px;" class="style1">比赛年度设置&nbsp;<a href="#" class="help">[?]</a></td>
			<td  >
			<span class="note">设置当前比赛年度</span>&nbsp;
                <asp:DropDownList ID="DropDownList4" runat="server" Font-Size="Medium">
                    <asp:ListItem Selected="True">2016年</asp:ListItem>
                    <asp:ListItem>2015年</asp:ListItem>
                </asp:DropDownList>
			</td>
		</tr>	
        <tr>
			<td  class="style1"  style="padding:14px;">报名时间设置&nbsp;<a href="#" class="help">[?]</a></td>
			<td  >
			<span class="note">设置报名的开始时间和截止时间</span>&nbsp;
                开始时间：<asp:TextBox ID="TB_yhid0" runat="server" 
                    ontextchanged="TB_yhid_TextChanged" AutoPostBack="true" Width="164px" 
                    Font-Size="Medium">2016-09-23 08:00:00</asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 截止时间：<asp:TextBox ID="TB_yhid1" runat="server" 
                    ontextchanged="TB_yhid_TextChanged" AutoPostBack="true" Width="158px" 
                    Font-Size="Medium">2016-10-23 08:00:00</asp:TextBox>
            </td>
		</tr>	
        	<tr>
			<td  class="style1"  style="padding:14px;">查询对象设置&nbsp;<a href="#" class="help">[?]</a></td>
			<td >
			<span class="note">查询对象设置</span>&nbsp;
                <asp:CheckBox ID="CheckBox3" runat="server" Text="开放" />
                <asp:CheckBox ID="CheckBox1" runat="server" Text="选手" />
                <asp:CheckBox ID="CheckBox2" runat="server" Text="学校" />
                <asp:CheckBox ID="CheckBox4" runat="server" Text="区县教育局" />
                <asp:CheckBox ID="CheckBox5" runat="server" Text="市教育局" />
                </td>
		</tr>
        


<tr class="noeffect">
	<td class="style1"  style="padding:14px;"></td>
	<td >
            <br />
            <asp:Button ID="LB_insert" runat="server"  
            onclick="LB_insert_Click" Text="确认更改" Font-Size="Medium" />
            <br />
            <%--<input type="button"  class="button" ID="LB_insert" runat="server" onclick="LB_insert_Click"  value='确认添加' />--%>
            
        &nbsp;</td>
</tr>
</table>
 
  <br />
 
	

</div>
</form>
</body>
</html>

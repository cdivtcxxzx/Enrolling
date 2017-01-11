<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BanjUpdate.aspx.cs" Inherits="admin_BanjUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>管理班级</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
    <style type="text/css">
        .style1
        {
            width: 139px;
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
    <table class="table">
	<thead>
	<tr>
		<td colspan="2">
			<asp:HyperLink runat="server" ID="toplink" NavigateUrl="#" Text="当前位置&gt;&gt;&nbsp;添加班级"></asp:HyperLink><a href="DistrictMan.aspx">返回</a>

		</td>
		

	</tr>
  </thead>
   <tr>
			<td  class="style1">班级名称&nbsp;<a href="#" class="help">[?]</a></td>
			<td  >
			<span class="note">班级名称</span>&nbsp;
                <asp:TextBox ID="TB_xm" runat="server"  Text=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TB_xm" ErrorMessage="*必填" Font-Size="Medium"
            SetFocusOnError="true"  ForeColor="Red" />
			</td>
		</tr>	
       
        	<tr>
			<td  class="style1">班级id&nbsp;<a href="#" class="help">[?]</a></td>
			<td >
			<span class="note">班级id,不能重复</span>&nbsp;
                <asp:TextBox ID="TB_code" runat="server" ontextchanged="TB_code_TextChanged" AutoPostBack="true" Enabled="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TB_code" ErrorMessage="*必填" Font-Size="Medium"
            SetFocusOnError="true"  ForeColor="Red" />
                <asp:Label ID="code_tip" runat="server" ForeColor="Red"></asp:Label>
                </td>
		</tr>
         
        <tr>
			<td  class="style1">班级帐号&nbsp;<a href="#" class="help">[?]</a></td>
			<td >
			<span class="note">登录系统帐号,不能重复</span>&nbsp;
                 <asp:TextBox ID="TB_yhid" runat="server" ontextchanged="TB_yhid_TextChanged" AutoPostBack="true"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TB_yhid" ErrorMessage="*必填" Font-Size="Medium"
            SetFocusOnError="true"  ForeColor="Red" />
                <asp:Label ID="yhid_tip" runat="server" ForeColor="Red"></asp:Label>
                </td>
		</tr>
        <tr>
			<td  class="style1">设置密码&nbsp;<a href="#" class="help">[?]</a></td>
			<td >
			<span class="note">设置初始密码。</span>&nbsp;
               
			    密码：<asp:TextBox ID="TB_pwd" runat="server" TextMode="Password"></asp:TextBox>

			    确认密码:<asp:TextBox ID="TB_pwd_confirm" runat="server" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TB_pwd" ControlToCompare="TB_pwd_confirm" ErrorMessage="两次输入的密码不一致" ForeColor="Red"/>
                </td>
		</tr>
		
		


<tr class="noeffect">
	<td class="style1"></td>
	<td >
            <asp:Button ID="LB_insert" runat="server"  
            onclick="LB_insert_Click" Text="确认添加" />
        <asp:Button ID="LB_up" runat="server"  
            onclick="LB_up_Click" Text="确认修改" Visible="false"/>
            <%--<input type="button"  class="button" ID="LB_insert" runat="server" onclick="LB_insert_Click"  value='确认添加' />--%>
            
        <input type="reset"  value='重新填写' />
               
				
			
			   </td>
</tr>
</table>
 
  <br />
 
	

</div>
</form>
</body>
</html>
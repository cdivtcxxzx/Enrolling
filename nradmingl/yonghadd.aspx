<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yonghadd.aspx.cs" Inherits="admin_yonghadd" %>

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
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;添加新用户</a>

		</td>
		

	</tr>
  </thead>
   <tr>
			<td  class="style1">姓名&nbsp;<a href="#" class="help">[?]</a></td>
			<td  >
			<span class="note">用户真实姓名</span>&nbsp;
                <asp:TextBox ID="TB_xm" runat="server"  Text=""></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TB_xm" ErrorMessage="*必填" Font-Size="Medium"
            SetFocusOnError="true"  ForeColor="Red" />
			</td>
		</tr>	
        <tr>
			<td  class="style1">分配院系&nbsp;<a href="#" class="help">[?]</a></td>
			<td  >
			<span class="note">分配用户所属系部</span>&nbsp;
                <asp:DropDownList ID="DropDownList3" runat="server" DataTextField="yxmc" DataValueField="yxdm" AppendDataBoundItems="true" DataSourceID="SqlDataSource3">
                    <asp:ListItem Value="">部门选择</asp:ListItem>
                </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        SelectCommand="SELECT * FROM [dm_yuanxi]"></asp:SqlDataSource>
            </td>
		</tr>	
        	<tr>
			<td  class="style1">用户登陆名&nbsp;<a href="#" class="help">[?]</a></td>
			<td >
			<span class="note">用户登陆名,不能重复</span>&nbsp;
                <asp:TextBox ID="TB_yhid" runat="server" ontextchanged="TB_yhid_TextChanged" AutoPostBack="true"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TB_yhid" ErrorMessage="*必填" Font-Size="Medium"
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
		
		<tr>
			<td  class="style1">座机电话&nbsp;<a href="#" class="help">[?]</a></td>
			<td >
			<span class="note">办公室电话</span>&nbsp;
                <asp:TextBox ID="TB_zjh" runat="server"></asp:TextBox>
            </td>
		</tr>
        		<tr>
			<td  class="style1">手机号码&nbsp;<a href="#" class="help">[?]</a></td>
			<td >
			<span class="note">最新的手机号。</span>&nbsp;
                <asp:TextBox ID="TB_yhdh" runat="server"></asp:TextBox>
				
			
			        </td>
		</tr>


<tr class="noeffect">
	<td class="style1"></td>
	<td >
            <asp:Button ID="LB_insert" runat="server"  
            onclick="LB_insert_Click" Text="确认添加" />
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

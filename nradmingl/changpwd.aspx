<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changpwd.aspx.cs" Inherits="admin_changpwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>用户信息修改</title>
 <link runat="server" id="webcss" type="text/css" href="styleqt.css" rel="stylesheet" rev="stylesheet" media="all" />
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
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<asp:LinkButton runat="server" ID="LB_top" Text="个人设置&gt;&gt;&nbsp;修改密码"></asp:LinkButton>
           
		   <a href="showpower.aspx">查看权限</a>
           <a href="changpwd.aspx">修改密码</a></td>
	</tr>
  </thead>
</table>
 
  <br />
 
	
<asp:FormView id="formView1" runat="server"  CssClass="table"  DataSourceID="Source1"  >
<ItemTemplate> 

		<tr>
			<td style="width:20%;" class="style1">真实姓名&nbsp;<a href="#" class="help">[?]</a></td>
			<td style="width:60%;">
			<span class="note">用户真实姓名(如果姓名错误，只有在人事管理系统中才能更改。</span>&nbsp;
                <asp:Label ID="Label1" runat="server" Text='<%#Eval("xm")%>'></asp:Label>
				
			
			</td>
			<td  rowspan="3"  style="width:20%;">
			    <asp:Image ID="Image1" runat="server"   ImageUrl='<%#string.Format("http://lyncex.cdivtc.com:8001/Photo/UploadHead.aspx?operation=get&user={0}",Convert.ToString(Eval("yhid")))%>' />
				
			
			</td>
		</tr>	
        <tr>
			<td  class="style1">用户组&nbsp;<a href="#" class="help">[?]</a></td>
			<td class="style2">
			<span class="note">用户所在分组。</span>&nbsp;
                <asp:Label ID="Label5" runat="server" Text='<%#new Power().getZhuMCs(Eval("lsz").ToString())%>'></asp:Label>
				
			
			    </td>
		</tr>	
        <tr>
			<td  class="style1">用户部门&nbsp;<a href="#" class="help">[?]</a></td>
			<td class="style2">
			<span class="note">用户所在部门，来源于人事管理系统。</span>&nbsp;
                <asp:Label ID="Label6" runat="server" Text='<%#Eval("uumzw")%>'></asp:Label>
				
			
			    </td>
		</tr>	
        	<tr>
			<td  class="style1">用户原密码&nbsp;<a href="#" class="help">[?]</a></td>
			<td colspan="2">
			<span class="note">必须原密码正确才能修改密码。</span>&nbsp;
                <asp:TextBox ID="TextBox7" runat="server" TextMode="Password"></asp:TextBox>
				
			
			
			</td>
		</tr>
		<tr>
			<td  class="style1">用户新密码&nbsp;<a href="#" class="help">[?]</a></td>
			<td colspan="2">
			<span class="note">遗失密码将无法找回，请妥善保管好密码。如果密码为空则不修改用户密码，只修改其他信息。</span>&nbsp;
                <asp:TextBox ID="TextBox6" runat="server" TextMode="Password"></asp:TextBox>
				
			
			
			</td>
		</tr>
		<tr>
			<td  class="style1">再次输入新密码&nbsp;<a href="#" class="help">[?]</a></td>
			<td colspan="2">
			<span class="note">遗失密码将无法找回，请妥善保管好密码。如果密码为空则不修改用户密码，只修改其他信息。</span>&nbsp;
                <asp:TextBox ID="TextBox5" runat="server" 
                    TextMode="Password"></asp:TextBox>
				
			
			</td>
		</tr>
		<tr>
			<td colspan="3" height="10"></td>
		</tr>	
		
		<tr>
			<td  class="style1">最后访问时间&nbsp;<a href="#" class="help">[?]</a></td>
			<td colspan="2">
			<span class="note">最后访问时间</span>&nbsp;
                <asp:Label ID="Label2" runat="server" Text='<%#Eval("dltime")%>'></asp:Label>
				
			
			</td>
		</tr>
		
		<tr>
			<td  class="style1">访问次数&nbsp;<a href="#" class="help">[?]</a></td>
			<td colspan="2">
			<span class="note">用户访问次数。</span>&nbsp;
                <asp:Label ID="Label3" runat="server" Text='<%#Eval("fwcs")%>'></asp:Label>
				
			
			</td>
		</tr>
		
		<tr>
			<td class="style1">座机号码&nbsp;<a href="#" class="help">[?]</a></td>
			<td colspan="2">
			<span class="note">座机号码</span>&nbsp;
                <asp:TextBox ID="Label4" runat="server" Text='<%#Eval("lxdh")%>'></asp:TextBox>
				
			
			</td>
		</tr>
		
		<tr>
			<td  class="style1">手机号码&nbsp;<a href="#" class="help">[?]</a></td>
			<td colspan="2">
			<span class="note">手机号码。</span>&nbsp;
                <asp:TextBox ID="TextBox4" runat="server" Text='<%#Eval("yhdh")%>'></asp:TextBox>
				
			</td>
		</tr>
<tr class="noeffect">
	<td class="style1"></td>
	<td colspan="2">
        <asp:Button ID="Button1" runat="server" CssClass="button" 
            onclick="Button1_Click" Text="确认修改" />
        <input type="reset" class="button" value='重新填写' /></td>
</tr>
</ItemTemplate>
</asp:FormView>
<%-- <asp: ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        SelectCommand = "select * from xuesjbsj where xsid=@xsid"
        
        >
    <SelectParameters>
        <asp:QueryStringParameter Name="xsid" QueryStringField="xsid" />
    </SelectParameters>
    </asp:SqlDataSource>--%>
    <asp:ObjectDataSource ID="Source1" runat="server"
                          TypeName="Account"
                          SelectMethod="getById"  
               OldValuesParameterFormatString="original_{0}">
        <SelectParameters><asp:SessionParameter  Name="yhid" SessionField="UserName" 
                Type="String"/></SelectParameters>
    </asp:ObjectDataSource>
</div>
</form>
</body>
</html>


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yonghzadd.aspx.cs" Inherits="admin_yonghzadd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>用户组管理</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<script type="text/javascript">
    $(function () {
        if ($("#C_UserGroup").length != 0) {
            $("#C_UserGroup a[href$='?action=delete&deletetable=c_user&deletefield=usergroup&id=1']").hide();
            $("#C_UserGroup a[href$='?action=delete&deletetable=c_user&deletefield=usergroup&id=2']").hide();
        }
        $("#TB_Name").myValidator();
        $("#TB_OrderNum").myValidator({ ismust: false, type: "int" });


        $("#seleteall").click(function () {
            $(this).parent().parent().parent().parent().parent().find("input").attr("checked", $("#seleteall").prop("checked"));
        })
    })

</script>
</head>
<body id="C_User">
    <form id="form1" runat="server">
    
        <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;用户组添加</a>
			<a href="yonghzgl.aspx">返回用户组管理</a>
			
			
		</td>
		
		<td style="text-align:right">
        </td>
	</tr>
  </thead>
</table>
 
  <br />
    <div id="PanelManage">
	
<table class="table" style="margin-top:8px;">
<thead>
<tr>
	<td colspan="2">用户组管理数据&nbsp;<a href="#" class="helpall">[?]</a></td>
</tr>
</thead>
 <tr id="manage_UserName">
	<td style="width:120px;">用户组名称&nbsp;<a href="#" class="help">[?]</a></td>
	<td>
	<span class="note">用户组名称。</span>
	<asp:TextBox ID="TB_ZhuName" runat="server"></asp:TextBox>
	    &nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>
<tr id="manage_Name">
	<td style="width:120px;">用户组说明&nbsp;<a href="#" class="help">[?]</a></td>
	<td>
	<span class="note">用户组的详细说明。</span>
	<asp:TextBox ID="TB_ZhuSM" runat="server" TextMode="MultiLine"></asp:TextBox>
	</td>
</tr>
<tr id="manage_glyx">
	<td style="width:120px;">选择管理数据&nbsp;<a href="#" class="help">[?]</a></td>
	<td>
	<span class="note">选择用户能管理哪些系部的数据</span>
        <asp:Repeater id="glyx" runat="server"  >
        <ItemTemplate>
        <asp:CheckBox id="yx"  name="lsz" runat="server" Text=<%# Eval("yxmc") %> />
 
           </ItemTemplate>
           </asp:Repeater>
	</td>
</tr>
<%--  <tr id="Tr1">
	<td style="width:120px;">排序&nbsp;<a href="#" class="help">[?]</a></td>
	<td>
	<span class="note">排序,以数字表示,越小排在越前。</span>
	<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
	    &nbsp;&nbsp;&nbsp;&nbsp;</td>
</tr>--%>
 
<asp:Panel ID="Panel1" runat="server">
    <asp:GridView ID="GV_qx" runat="server" AutoGenerateColumns="false" DataSourceID="ObjectDataSource1" CssClass="table">
    <Columns>
    <asp:BoundField DataField="lmid" HeaderText="栏目ID" HeaderStyle-Width="5%"/>
    <asp:BoundField DataField="lmmc" HeaderText="栏目名称" HeaderStyle-Width="20%" />
    <asp:BoundField DataField="gjz" HeaderText="栏目关键字" HeaderStyle-Width="20%" />
    <asp:TemplateField HeaderText="相关权限">
    <ItemTemplate>
    <asp:Repeater ID="Rep" runat="server" DataSource='<%#GetRep(Eval("sfqxyz").ToString()) %>' >
    <ItemTemplate>
    <asp:CheckBox ID="CB_qx" Text='<%#GetQxmc(Eval("qxdm").ToString())%>' runat="server"/>
    </ItemTemplate>
    </asp:Repeater>
    </ItemTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView>
   <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetLanmStruct" TypeName="Power">
                <SelectParameters>
                    <asp:Parameter DefaultValue="-1" Name="lmid" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
    <asp:Table ID="Table1" CssClass="table" runat="server">
  <asp:TableRow>
  <asp:TableCell Width="5%">
  
  </asp:TableCell>
    <asp:TableCell Width="20%">
  
  </asp:TableCell>
  <asp:TableCell Width="20%">
  <asp:Button ID="BT_update" CssClass="button" OnClick="yonghzAdd_Click" runat="server" Text="确定" />
  <input type="reset" class="button" value="重置" />
  </asp:TableCell>
    <asp:TableCell HorizontalAlign="Left">
  <input id="seleteall" type="checkbox" name="seleteall" /><label for="seleteall">全选</label>
  </asp:TableCell>
  <asp:TableCell></asp:TableCell>
  </asp:TableRow>
  </asp:Table>
    </asp:Panel>
</table>
 
</div>
 
 
 
 
 
    </form>
</body>
</html>

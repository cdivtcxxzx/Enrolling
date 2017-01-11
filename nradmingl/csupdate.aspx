<%@ Page Language="C#" AutoEventWireup="true" CodeFile="csupdate.aspx.cs" Inherits="admin_csupdate" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>用户设置及权限分配</title>
<link runat="server" id="webcss" type="text/css" href="styleqt.css" rel="stylesheet" rev="stylesheet" media="all" />
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
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;权限分配页</a> <a href="yonghgl.aspx">返回用户管理</a>
			
			
		</td>
		
		<td style="text-align:right">
            <asp:TextBox ID="searchtext" runat="server"></asp:TextBox>
           <asp:Button 
                ID="Button1" runat="server" Text="搜索用户" CssClass="click" />
            </td>
	</tr>
  </thead>
</table>
  
 
 
<div id="PanelManage">
	
<table class="table" style="margin-top:8px;">
<thead>
<tr>
	<td colspan="2">用户管理数据&nbsp;<a href="#" class="helpall">[?]</a></td>
</tr>
</thead>
</table>
<asp:FormView id="formView1" runat="server"  CssClass="table" DataSourceID="Sql_yh">
<ItemTemplate>
 <tr id="manage_UserName">
	<td style="width:120px;">登录名密码&nbsp;<a href="#" class="help">[?]</a></td>
	<td>
	<span class="note">UUM统一登陆用户名，密码为重置慎用。</span>
	<asp:Label ID="TB_UserName" Text='<%# Eval("yhid") %>' ReadOnly="true" runat="server"></asp:Label>
	&nbsp;&nbsp;&nbsp;&nbsp;新密码：<asp:TextBox ID="TB_PWD" runat="server" 
            TextMode="Password"></asp:TextBox>&nbsp;确认新密码:<asp:TextBox ID="TB_PWD2" 
            runat="server" TextMode="Password"></asp:TextBox>（不更改请不填）&nbsp;&nbsp; 
        <asp:Button ID="resetPwd" runat="server" onclick="resetPwd_OnClick" Text="重置密码" />
     </td>
</tr>
<tr id="manage_Name">
	<td style="width:120px;">真实姓名&nbsp;<a href="#" class="help">[?]</a></td>
	<td>
	<span class="note">用户姓名。</span>
	<asp:Label ID="TextBox1" ReadOnly="true" Text=<%# Eval("xm") %> runat="server"></asp:Label>
	</td>
</tr>
 <tr id="manage_Email">
	<td style="width:120px;">处室系部&nbsp;<a href="#" class="help">[?]</a></td>
	<td>
	<span class="note">用户的部门信息。</span>
	<asp:Label ID="TextBox2" ReadOnly="true" Text=<%# Eval("uumzw") %> runat="server"></asp:Label>
	</td>
</tr>
<tr id="manage_Tel">
	<td style="width:120px;">电话&nbsp;<a href="#" class="help">[?]</a></td>
	<td>
	<span class="note">电话、手机。</span>
	座机号码:<asp:Label ID="TextBox3"  Text=<%# Eval("lxdh") %> runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;手机号码:<asp:Label ID="TextBox4" Text=<%# Eval("yhdh") %>  runat="server"></asp:Label>
	</td>
</tr>



</ItemTemplate>
</asp:FormView>

    <asp:SqlDataSource ID="Sql_yh" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        SelectCommand="SELECT [yhid], [xm], [uumzw], [lxdh], [yhdh] FROM [yonghqx] WHERE ([yhid] = @yhid)">
        <SelectParameters>
            <asp:QueryStringParameter Name="yhid" QueryStringField="yhid" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

<table class="table"> 
<tr id="manage_UserGroup">
	<td style="width:120px;">所在用户组&nbsp;<a href="#" class="help">[?]</a></td>
	<td>
	<span class="note">选择用户所在的用户组，那么该用户将具备所选用户组的所有权限。</span>
        <asp:Repeater id="YHZ" runat="server"  >
        <ItemTemplate>
        <asp:CheckBox id="lsz"  name="lsz" runat="server" Text=<%# Eval("zmc") %> />
 
<%--        <asp:CheckBox id="CBL_Content_17"  name="CBL_Content_6" runat="server" 
            Text="学生处管理员" />
 
        <asp:CheckBox id="CBL_Content_18"  name="CBL_Content_6" runat="server" 
            Text="汽车系学生科" />
 
        <asp:CheckBox id="CBL_Content_19"  name="CBL_Content_6" runat="server" 
            Text="计算机系学生科" />
 
        <asp:CheckBox id="CBL_Content_20"  name="CBL_Content_6" runat="server" 
            Text="信息中心管理员" />
 
        <asp:CheckBox id="CBL_Content_21"  name="CBL_Content_6" runat="server" 
            Text="校领导" />
 
        <asp:CheckBox id="CBL_Content_22"  name="CBL_Content_6" runat="server" 
            Text="班主任" />--%>
           </ItemTemplate>
           </asp:Repeater>
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

</table>
 
</div>
<asp:FormView ID="formView2" runat="server" CssClass="table">
<ItemTemplate>
<tr id="manage_Content" runat="server">
	<td style="width:120px;">用户权限&nbsp;<a href="#" class="help">[?]</a></td>

</tr>
</ItemTemplate></asp:FormView>

 
 

 
 
    <asp:Panel ID="Panel1" runat="server">
    <asp:GridView ID="GV_qx" runat="server" AutoGenerateColumns="false" DataSourceID="Sql_qx" CssClass="table">
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
    <asp:SqlDataSource ID="Sql_qx" runat="server" SelectCommand="select b.lmid,'   |__   |__'+b.lmmc as lmmc,b.gjz,b.sfqxyz,a.id+STR(b.lmid) as id,b.fid from [lanm] b 
  left join 
  (  SELECT lmid,'|__'+lmmc as lmmc,gjz,sfqxyz,str(fid)+STR(lmid) as id,fid
  FROM [lanm] 
  where fid in (select lmid from lanm  where fid='-1')
  union (select lmid,lmmc,gjz,sfqxyz,str(lmid) as lmid,fid from lanm where fid='-1') ) as a on a.lmid=b.fid
  where b.fid in (SELECT lmid from [lanm] where fid in (select lmid from lanm  where fid='-1'))
  union
  (SELECT lmid,'|__'+lmmc as lmmc,gjz,sfqxyz,str(fid)+STR(lmid) as id,fid
  FROM [lanm] 
  where fid in (select lmid from lanm  where fid='-1') 
  union (select lmid,lmmc,gjz,sfqxyz,str(lmid) as lmid,fid from lanm where fid='-1') )  
  order by id" ConnectionString="<%$ ConnectionStrings:SqlConnString %>"></asp:SqlDataSource>
  <asp:Table ID="Table1" CssClass="table" runat="server">
  <asp:TableRow>
  <asp:TableCell Width="5%">
  
  </asp:TableCell>
    <asp:TableCell Width="20%">
  
  </asp:TableCell>
  <asp:TableCell Width="20%">
  <asp:Button ID="BT_update" CssClass="button" OnClick="yhqxUpdate_Click" runat="server" Text="更新" />
  <input type="reset" class="button" value="重置" />
  </asp:TableCell>
    <asp:TableCell HorizontalAlign="Left">
  <input id="seleteall" type="checkbox" name="seleteall" /><label for="seleteall">全选</label>
  </asp:TableCell>
  <asp:TableCell></asp:TableCell>
  </asp:TableRow>
  </asp:Table>
    </asp:Panel>

 

 
 
 </form>
</body>
</html>
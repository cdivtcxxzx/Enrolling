<%@ Page Language="C#" AutoEventWireup="true" CodeFile="showPower.aspx.cs" Inherits="admin_showPower" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>用户设置及权限分配</title>
 <link runat="server" id="webcss" type="text/css" href="styleqt.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>

</head>
<body id="C_User">
 <form id="form1" runat="server">

<table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<asp:LinkButton runat="server" ID="LB_top" Text="个人设置&gt;&gt;&nbsp;查看权限"></asp:LinkButton>
          
		   <a href="showpower.aspx">查看权限</a>
           <a href="changpwd.aspx">修改密码</a></td>
	</tr>
  </thead>
</table>
  
 
 
<div id="PanelManage">
	


<table class="table"> 
<tr id="manage_UserGroup">
	<td style="width:120px;">所在用户组&nbsp;<a href="#" class="help">[?]</a></td>
	<td>
	<span class="note">用户所在的用户组，那么该用户将具备所选用户组的所有权限。</span>
        <asp:Repeater id="YHZ" runat="server"  >
        <ItemTemplate>
        <asp:CheckBox id="lsz"  name="lsz" runat="server" Text=<%# Eval("zmc") %>  Checked="true"  />
 
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
	<td style="width:120px;">管理数据&nbsp;<a href="#" class="help">[?]</a></td>
	<td>
	<span class="note">用户能管理哪些系部的数据</span>
        <asp:Repeater id="glyx" runat="server"  >
        <ItemTemplate>
        <asp:CheckBox id="yx"  name="lsz" runat="server" Text=<%# Eval("yxmc") %> Checked="true" />
 
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
    <asp:Repeater ID="Rep" runat="server" DataSource='<%#GetRep(Eval("sfqxyz").ToString(),Eval("gjz").ToString()) %>' >
    <ItemTemplate>
    <asp:CheckBox ID="CB_qx" Text='<%#GetQxmc(Eval("qxdm").ToString())%>' runat="server" Checked="true"/>
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

    </asp:Panel>

 

 
 
 </form>
</body>
</html>

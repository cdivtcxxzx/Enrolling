<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yonghbmgl.aspx.cs" Inherits="admin_yonghbmgl" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>优化后的部门管理界面</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>

 

  


</head>
<body id="C_User">
    <form id="form1" runat="server">
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;部门维护管理</a>&nbsp;
			
			
		</td>
		
		<td style="text-align:right">输入部门名称:<asp:TextBox ID="searchtext" 
                runat="server" name="1" txttop="txttop" title="根据输入的部门名称添加"></asp:TextBox>
           &nbsp;代码<asp:TextBox ID="searchtext0" 
                runat="server" name="1" txttop="txttop" title="根据输入的部门名称添加" Width="63px"></asp:TextBox>
           <asp:Button 
                ID="Button1" runat="server" Text="添加部门" CssClass="click" 
                onclick="Search_Onclick" />
        </td>
	</tr>
  </thead>
</table>
 
  <br />
 
<div id="PanelDefault">
<asp:ScriptManager ID="sm1" runat="server" />
<asp:UpdatePanel ID="up1" runat="server" ><ContentTemplate >



    <asp:GridView ID="GridView1" CssClass="table" runat="server"  DataKeyNames="id" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="序号" InsertVisible="False" 
                ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="YXDM" HeaderText="部门代码" SortExpression="YXDM" />
            <asp:BoundField DataField="YXMC" HeaderText="部门名称" SortExpression="YXMC" />

            <asp:CheckBoxField DataField="zt" HeaderText="启用" SortExpression="isjx" />
            <asp:CommandField HeaderText="管理操作" ShowDeleteButton="True" 
                ShowEditButton="True" />
        </Columns>
    </asp:GridView>



    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        SelectCommand="SELECT [YXDM], [id], [YXMC], [px], [fid], [zt] FROM [DM_YUANXI] ORDER BY [px]"

                
        
        UpdateCommand ="UPDATE dm_yuanxi SET yxdm = @yxdm, yxmc = @yxmc,zt=@zt WHERE (id = @id)" 
        DeleteCommand="DELETE FROM dm_yuanxi WHERE (id = @id)" >
        <UpdateParameters>
            <asp:Parameter Name="yxmc" />
            <asp:Parameter Name="yxdm" />
            <asp:Parameter Name="id" />
            <asp:Parameter Name="zt" />
        </UpdateParameters>
    </asp:SqlDataSource>



    </ContentTemplate></asp:UpdatePanel>
   </div>
    </form>
</body>
</html>
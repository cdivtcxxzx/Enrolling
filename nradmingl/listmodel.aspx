<%@ Page Language="C#" AutoEventWireup="true" CodeFile="listmodel.aspx.cs" Inherits="admin_listmodel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=7" />
<title>数据浏览模板</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
</head>
<body id="C_User">

    <form id="form1" runat="server">
    <table class="table subsubmenu" id="toptool" sm="顶部工具栏">
	<thead>
	<tr>
		<td id="toolbuttion" runat="server" sm="顶部位置及导航按钮">
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;用户管理系统</a>
			<a href="uumyh.aspx">从UUM获取用户</a>
			
			
		</td>
		
		<td id="searchbuttion" runat="server" style="text-align:right"  sm="顶部搜索选项及搜索按钮"><asp:TextBox ID="searchtext" 
                runat="server" name="1" txttop="txttop"  title="根据用户姓名或登陆ID搜索用户"></asp:TextBox>
           <asp:Button 
                ID="Button1" runat="server" Text="搜索用户" CssClass="click" onclick="Search_Onclick" />
        </td>
	</tr>
  </thead>
</table>
 
  <br />
 
<div id="PanelDefault">
<asp:ScriptManager ID="sm1" runat="server" />
<asp:UpdatePanel ID="up1" runat="server" ><ContentTemplate >
    <asp:GridView ID="GridView1" runat="server"    CssClass="table"  
            CellPadding="2" CellSpacing="1" GridLines="None" AlternatingRowStyle-CssClass="dgAlter" DataKeyNames="yhid"    AutoGenerateColumns="False" 
        EmptyDataText="对不起,未找到相关数据！"  
        AllowPaging="True" AllowSorting="True" 
        OnPageIndexChanging="GridView1_PageIndexChanging" 
        OnSorting="GridView1_Sorting"
      >
        <Columns>

       <%-- <asp:TemplateField HeaderText="设置高度">  
         <HeaderTemplate>  
          &nbsp;
         </HeaderTemplate>  
         <ItemTemplate>  
            &nbsp;
         </ItemTemplate>  
         <ItemStyle Height="24px" HorizontalAlign="Center" />  
         <HeaderStyle Width="1" Height="28px" BackColor="#80B4CF" HorizontalAlign="Center" />  
       </asp:TemplateField>  

         <asp:TemplateField HeaderText="编号">  
         <HeaderTemplate>  
           
         </HeaderTemplate>  
         <ItemTemplate>  
           <div><img height="24" titop="titop" width="24" alt='http://lyncex.cdivtc.com:8001/Photo/UploadHead.aspx?operation=get&user=<%#(Convert.ToString(Eval("yhid")))%>' src='http://lyncex.cdivtc.com:8001/Photo/UploadHead.aspx?operation=get&user=<%#(Convert.ToString(Eval("yhid")))%>' /> </div> 
         </ItemTemplate>  
         <ItemStyle Height="25px" HorizontalAlign="Center" />  
         <HeaderStyle Width="3%" Height="24px" BackColor="#80B4CF" HorizontalAlign="Center" />  
       </asp:TemplateField>  
            <asp:BoundField DataField="xm" HeaderText="真实姓名"  HeaderStyle-Width="70"  SortExpression="xm" >
            
            <HeaderStyle Width="70px" />
            
            </asp:BoundField>
            <asp:BoundField DataField="yhid" HeaderText="登陆名"  SortExpression="yhid"  
                HeaderStyle-Width="80" >
            <HeaderStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="yhqx" HeaderText="权限列表" SortExpression="yhqx" />
            <asp:BoundField DataField="lsz" HeaderText="所在用户组"  SortExpression="lsz"  
                HeaderStyle-Width="80" >
            <HeaderStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="dltime" HeaderText="最后访问时间" 
                SortExpression="dltime"  HeaderStyle-Width="120" >
            <HeaderStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="fwcs" HeaderText="访问次数" SortExpression="fwcs"  
                HeaderStyle-Width="108" >
            <HeaderStyle Width="108px" />
            </asp:BoundField>
            <asp:ButtonField HeaderText="管理" Text="查看"  HeaderStyle-Width="25" >
            <HeaderStyle Width="25px" />
            </asp:ButtonField>
            <asp:HyperLinkField DataNavigateUrlFormatString="yhupdate.aspx?yhid={0}"   HeaderStyle-Width="25" DataNavigateUrlFields="yhid" Text="修改" />
            <asp:ButtonField Text="删除"  HeaderStyle-Width="25" >
            <HeaderStyle Width="25px" />
            </asp:ButtonField>--%>
        </Columns>
         <PagerTemplate>
<span style="float:left;">
<a onclick="return batchAudit(this.id);" id="btnDelete" href="javascript:__doPostBack('btnDelete','')"><span id="plcz" runat="server">批量删除</span></a>&nbsp;&nbsp;&nbsp;&nbsp;
            每页<asp:Label ID="LabelPageSize" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageSize %>"></asp:Label>
            条 当前<asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex+1 %>"></asp:Label>
            /<asp:Label ID="Label3" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
            页&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First"
                CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=0 %>">首页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=0 %>">上一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">下一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">尾页</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;转到<asp:TextBox ID="txt_go" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>

            <asp:LinkButton ID="LinkButtonGo" runat="server"  CssClass="click" Text="跳转" OnClick="LinkButtonGo_Click" />
            </span><span style="float:right;"><b>总记录：<%# Session["YhqxTotalRows"].ToString() %></asp:Label></b>&nbsp;&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /> </span>
        </PagerTemplate>

      
        
    </asp:GridView>
    </ContentTemplate></asp:UpdatePanel>
    <asp:ObjectDataSource 
         ID="srcAccount"
         TypeName="Account"
         SelectMethod="getAllUser"
         runat="server"
    />
    
    <!--
    批量操作代码JS
    -->
    <asp:LinkButton ID="btnDelete" onclick="plcz_Click"  OnClientClick='return batchAudit(this.id)' runat="server"></asp:LinkButton>
    <script type="text/javascript">
        function onclicksel() {
            var chkobj = document.getElementById("BoxIdAll");
            if (chkobj.checked == true) {
                selAll();
            }
            else {
                removeAll();
            }
        }
        function selAll() {
            var selobj = document.getElementsByName("BoxId");
            for (var i = 0; i < selobj.length; i++) {
                if (!selobj[i].disabled) {
                    selobj[i].checked = true;
                }
            }
        }

        function removeAll() {
            var selobj = document.getElementsByName("BoxId");
            for (var i = 0; i < selobj.length; i++) {
                selobj[i].checked = false;
            }
        }
        function batchAudit(id) {
            var AuditVal = "";
            var bid = document.getElementsByName("BoxId");
            for (var i = 0; i < bid.length; i++) {
                if (bid[i].checked == true) {
                    AuditVal = AuditVal + bid[i].value + ",";
                }
            }
            if (AuditVal.length <= 0) {
                alert("请先选择要删除的记录");
                return false;
            }
            else {
                if (id == "btnDelete") {
                    if (confirm("您确认要批量删除这" + String(AuditVal.length / 2) + "条记录吗？")) {
                        document.getElementById("hdfWPBH").value = AuditVal;
                        //alert(document.getElementById("hdfWPBH").value);
                        return true;
                    }
                    return false;
                }
            }
        }  
    </script>
        <!--
    批量操作代码结束
    -->
    </div>
    </form>
</body>
</html>

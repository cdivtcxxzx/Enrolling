<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yonghsqadd.aspx.cs" EnableEventValidation="false"  Inherits="admin_yonghsqadd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>用户添加</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
    </head>
<body id="C_User">
    <form id="form1" runat="server">
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td ><a href="?action=manage">当前位置&gt;&gt;&nbsp;教师添加</a><a href="yonghglsq.aspx">返回教师管理</a><a href="yonghglxbadd.aspx">手动添加教师</a>
            </td>

            <td style="text-align:right">
            
            
                可输入姓名、系名称查询：<asp:TextBox ID="SearchBox" runat="server" Width="211px" ></asp:TextBox><asp:Button ID="SearchButton" runat="server" Text="搜索用户" onclick="Search_Onclick" CssClass="click"/>
            
            
            </td>
		
	</tr>
  </thead>
</table>

<div id="PanelDefault">
<asp:ScriptManager ID="sm1" runat="server" />
<asp:UpdatePanel ID="up1" runat="server" >
<Triggers>
<asp:PostBackTrigger ControlID="GridView1" />
</Triggers>
<ContentTemplate >
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="add_tip" runat="server" ForeColor="Red" 
        Font-Size="Large"></asp:Label>
 <asp:HiddenField ID="hdfWPBH" runat="server" />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
          CssClass="table"  OnRowCommand="GridView1_RowCommand"  DataSourceID="Sql_yh"
        AllowPaging="True" AllowSorting="True"   EmptyDataText="暂无相关数据！"
        OnPageIndexChanging="GridView1_PageIndexChanging" >
        <Columns>
         <asp:TemplateField>
                <HeaderTemplate>
                      <input type="checkbox" name="BoxIdAll" id="BoxIdAll" onclick="onclicksel();" />  
                </HeaderTemplate>
                <ItemTemplate>
                     <input id="BoxId" name="BoxId" value='<%#(Convert.ToString(Eval("yhid")))%>' type="checkbox" /> 
                </ItemTemplate>
                <ItemStyle Height="21px" HorizontalAlign="Center" />
                <HeaderStyle Width="2%"  HorizontalAlign="Center" />
            </asp:TemplateField>

       
                        <asp:BoundField DataField="xm" HeaderText="真实姓名"  HeaderStyle-Width="70"  SortExpression="xm" >
            
            <HeaderStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="yhid" HeaderText="登陆名"  SortExpression="yhid"  
                HeaderStyle-Width="80" >
            <HeaderStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="yxmc" HeaderText="所属部门"  SortExpression="yxmc"  
               >
        
            </asp:BoundField>
            
           <%-- <asp:BoundField DataField="yhqx" HeaderText="权限列表" SortExpression="yhqx" />--%>

            <asp:TemplateField  HeaderText="当前上课系部"  >
     
            <ItemTemplate><asp:Label runat="server" ID="qxlb" Text='<%#new Power().getjxMCs(Eval("pjjxbmdm").ToString())%>'></asp:Label></ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField  HeaderText="所在用户组"  >
           
            <ItemTemplate><asp:Label runat="server" ID="Label_lsz" Text='<%#new Power().getZhuMCs(Eval("lsz").ToString())%>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
           <%-- <asp:ButtonField  HeaderText="管理" Text="导入本系"  CommandName="DaoRu"    HeaderStyle-Width="25" />
           --%>  <asp:TemplateField HeaderText="">
 <HeaderTemplate><asp:Button CssClass="abutton" name="btnDelete" onclick="Button3_Click"  txttop="txttop" ToolTip="先选择教师后，批量导入！" ID="btnDelete" runat="server" CommandName="批量导入"  CommandArgument=''  OnClientClick="return batchAudit('btnDelete');"   Text='批量导入教师' ></asp:Button>

                <%--<a onclick="return batchAudit(this.id);" id="btnDelete" href="javascript:__doPostBack('btnDelete','')"><span id="plcz" runat="server">点此批量发放毕业证</span></a>--%>
                </HeaderTemplate>
               
                <ItemTemplate>
               <asp:LinkButton ID="LinkButtonx2"  CssClass="abutton" runat="server" CommandName="DaoRu"  CommandArgument='<%#Eval("yhid")%>'    Text='导入本系' ></asp:LinkButton>
            </ItemTemplate>
                
                </asp:TemplateField>

        </Columns>
     <PagerTemplate>
<span style="float:left;">
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
            </span><span style="float:right;"><b></b>&nbsp;&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /> </span>
        </PagerTemplate>
        
    </asp:GridView>
    </ContentTemplate></asp:UpdatePanel>


       <asp:SqlDataSource  onselected="SqlDataSource1_Selected"  ID="Sql_yh" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        SelectCommand="SELECT pjjxbmdm,yhid,xm,yhqx,lsz,dltime,fwcs,guid,dm_yuanxi.yxmc,yonghqx.yxdm from yonghqx inner join dm_yuanxi on yonghqx.yxdm=dm_yuanxi.yxdm"
          
        ></asp:SqlDataSource>
    </div>
    <asp:Label runat="server" ID="Label2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;请先输入系部名称，教师姓名或工号查找，点击导入本系按钮添加用户，然后返回教师管理设置相应权限!</asp:Label>
    </form>

    
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
                 alert("请先选择要导入的老师,在姓名前打勾!");
                 return false;
             }
             else {
                 if (id == "btnDelete") {
                     if (confirm("您确认要批量导入这" + String(AuditVal.length / 5) + "教师吗？")) {
                         document.getElementById("hdfWPBH").value = AuditVal;
                         //alert(AuditVal);
                         //alert(document.getElementById("hdfWPBH").value);
                         return true;
                     }
                     return false;
                 }
             }
         }  
    </script>
</body>
</html>

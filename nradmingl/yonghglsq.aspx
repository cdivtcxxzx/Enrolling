<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yonghglsq.aspx.cs" Inherits="admin_yonghglsq" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>教师管理</title>
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
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;教师维护</a>
			
			<a href="yonghsqadd.aspx">添加教师</a>
			
		</td>
		
		<td style="text-align:right"><asp:TextBox ID="searchtext" 
                runat="server" name="1" txttop="txttop" title="根据用户姓名或登陆ID搜索用户"></asp:TextBox>
           <asp:Button 
                ID="Button1" runat="server" Text="搜索教师" CssClass="click" onclick="Search_Onclick" />
        &nbsp;
           <asp:Button 
                ID="Button2" runat="server" Text="清除本系所有教师" CssClass="click" 
                onclick="delall" />
        </td>
	</tr>
  </thead>
</table>
 

 
<div id="PanelDefault">
<asp:ScriptManager ID="sm1" runat="server" />
<asp:UpdatePanel ID="up1" runat="server" ><ContentTemplate >
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <asp:HiddenField ID="hdfWPBH" runat="server" />
    <asp:GridView ID="GridView1" runat="server"    CssClass="table"  
            CellPadding="2" CellSpacing="1" GridLines="None"    
        AlternatingRowStyle-CssClass="dgAlter"    AutoGenerateColumns="False" 
        EmptyDataText="无相关教师数据！"   DataSourceID="Sql_yh"
        AllowPaging="True" AllowSorting="True" 
        OnPageIndexChanging="GridView1_PageIndexChanging"  OnRowCommand="GridView1_RowCommand1" >
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
     

            <asp:TemplateField  HeaderText="上课系部"  SortExpression="pjjxbmdm" >
            <HeaderStyle Width="80px" />
            <ItemTemplate><asp:Label runat="server" ID="qxlb" Text='<%#new Power().getjxMCs(Eval("pjjxbmdm").ToString())%>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="等级认定部门" SortExpression="pjbmdm" >
            <HeaderStyle Width="80px" />
            <ItemTemplate><asp:Label runat="server" ID="qxlb1" Text='<%#new Power().getjxMCs(Eval("pjbmdm").ToString()).Replace(",","")%>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField  HeaderText="所在用户组" SortExpression="lsz" >
            <HeaderStyle Width="80px" />
            <ItemTemplate><asp:Label runat="server" ID="Label_lsz" Text='<%#new Power().getZhuMCs(Eval("lsz").ToString())%>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="dltime" HeaderText="最后访问时间" SortExpression="dltime"  HeaderStyle-Width="90" >
            <HeaderStyle Width="90px" />
            </asp:BoundField>
            <asp:BoundField DataField="fwcs" HeaderText="访问" SortExpression="fwcs"  
                HeaderStyle-Width="25" >
            <HeaderStyle Width="25px" />
            </asp:BoundField>


             <asp:TemplateField HeaderText="" HeaderStyle-Width="70">
 <HeaderTemplate><asp:LinkButton name="btnDelete" onclick="Button3_Click"  txttop="txttop" ToolTip="先选择教师后，批量删除！" ID="btnDelete" runat="server" CommandName="批量清除"  CommandArgument=''  OnClientClick="return batchAudit('btnDelete');"   Text='批量清除教师' ></asp:LinkButton>

                <%--<a onclick="return batchAudit(this.id);" id="btnDelete" href="javascript:__doPostBack('btnDelete','')"><span id="plcz" runat="server">点此批量发放毕业证</span></a>--%>
                </HeaderTemplate>
               
                <ItemTemplate>
             <a href="yonghsq.aspx?yhid=<%# Eval("yhid").ToString() %>"  txttop="txttop"  title="管理教师">管理</a> &nbsp;&nbsp;&nbsp;&nbsp;
             <asp:LinkButton ID="LinkButton2" runat="server" CommandName="清除"  CommandArgument='<%#Eval("yhid")%>'    Text='清除' >
              </asp:LinkButton>
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
                 <%//多页翻页
                     if (GridView1.PageCount >= 10 && GridView1.PageCount - GridView1.PageIndex >= 10)
                  {
                     
                       %>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+2 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+2 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+3 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+3 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+4 %></asp:LinkButton>
               <asp:LinkButton ID="LinkButton5" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+5 %>"
                CommandName="Page"><%#((GridView)Container.NamingContainer).PageIndex + 5%></asp:LinkButton> 
                <asp:LinkButton ID="LinkButton6" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+6%>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 6%></asp:LinkButton>
                <asp:LinkButton ID="LinkButton7" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+7 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 7%></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton9" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+8 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 8%></asp:LinkButton>
                 <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+9 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 9%></asp:LinkButton>
                 <asp:LinkButton ID="LinkButton12" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+10 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 10%></asp:LinkButton>
                <%}
                    else if(GridView1.PageCount >= 8 && GridView1.PageCount - GridView1.PageIndex >= 5){ %>
                    <asp:LinkButton ID="LinkButton8" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+2%>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 2%></asp:LinkButton>
                <asp:LinkButton ID="LinkButton10" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+3 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 3%></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton11" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page" ><%#((GridView)Container.NamingContainer).PageIndex + 4%></asp:LinkButton>
                    <%}%>
            <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">下一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">尾页</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;转到<asp:TextBox ID="txt_go" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>

            <asp:LinkButton ID="LinkButtonGo" runat="server"  CssClass="click" Text="跳转" OnClick="LinkButtonGo_Click" />
            </span><span style="float:right;"><b>总记录：</b><%#ViewState["count"].ToString()%>&nbsp;&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /> </span>
        </PagerTemplate>

      
        
    </asp:GridView>
        
    </ContentTemplate></asp:UpdatePanel>

    <asp:SqlDataSource  onselected="SqlDataSource1_Selected"  ID="Sql_yh" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        SelectCommand="SELECT pjjxbmdm,yhid,xm,yhqx,lsz,dltime,fwcs,guid,dm_yuanxi.yxmc,yonghqx.pjbmdm,yonghqx.yxdm from yonghqx inner join dm_yuanxi on yonghqx.yxdm=dm_yuanxi.yxdm"
          
        ></asp:SqlDataSource>


    </div>
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
                 alert("请先选择要清除的老师,在姓名前打勾!");
                 return false;
             }
             else {
                 if (id == "btnDelete") {
                     if (confirm("您确认要批量清除这" + String(AuditVal.length / 5) + "教师吗？")) {
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


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xwgl.aspx.cs" Inherits="admin_xwgl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />


<script src="../layer/jquery1.83.min.js" type="text/javascript"></script>
<script src="../layer/layer.min.js"  type="text/javascript"></script>


</head>
<body>

    <form id="form1" runat="server">
    <script >
function xw(nid,title){
$.layer({
            type : 2,
            title: ''+title,
            shadeClose: true,
            maxmin: true,
   fix: true,  
            area: ['995px', 515],                     
            iframe: {
                src : '/xw.aspx?xwid='+nid+''
            } ,
        });
}
</script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
        <asp:LinkButton runat="server" ID="LB_top" Text="信息管理&gt;&gt;&nbsp;全部新闻信息"></asp:LinkButton></td><td style="text-align:right">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                    DataSourceID="SqlDataSource2" DataTextField="lmmc" DataValueField="lmid" 
                    Height="25px" Width="122px" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
                            >
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                    DataSourceID="SqlDataSource3" DataTextField="lmmc" DataValueField="lmid" 
                    Height="25px" Width="122px" 
                            onselectedindexchanged="DropDownList2_SelectedIndexChanged" 
                            ondatabound="DropDownList2_DataBound">
                </asp:DropDownList>
                        
                </ContentTemplate>
                </asp:UpdatePanel></td><td style="text-align:left">
            <asp:Button ID="Button1" runat="server" Text="栏目跳转" CssClass="click" 
                onclick="Button1_Click" Font-Size="Medium" />
		&nbsp;&nbsp;&nbsp;
		</td>
		
		
		
		<td style="text-align:right;width:200px;">

        <asp:CheckBox ID="CheckBox1" runat="server" Text="全部" />
        <asp:TextBox ID="searchtext" 
                runat="server" name="1" txttop="txttop" title="根据发送人或标题搜索消息" style=" margin-right:0px; height:18px;width:80px; border:1px solid #DBDDDE;float:left; "></asp:TextBox>
           
            
           
           <asp:ImageButton ID="ImageButton1" runat="server" 
                ImageUrl="images/sous.gif" onclick="Search_Onclick" ToolTip="搜索查找消息" style=" margin-left:0px; border:0px;float:left;" />
&nbsp;</td>
        
	</tr>
  </thead>
</table>
    <div><asp:HiddenField ID="hdfWPBH" runat="server" />
      <asp:Label ID="Label1" runat="server"></asp:Label>
    <asp:GridView  OnRowCommand="GridView1_RowCommand"  ID="GridView1"  OnDataBound="GridView1_DataBound"  runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" CssClass="table" 
            EmptyDataText="该栏目下没有可管理的新闻!" 
            AllowPaging="True" AllowSorting="True">
    <Columns>
    <asp:TemplateField>
                <HeaderTemplate>
                      <input type="checkbox" name="BoxIdAll" id="BoxIdAll" onclick="onclicksel();" />  
                </HeaderTemplate>
                <ItemTemplate>
                     <input id="BoxId" name="BoxId" value='<%#(Convert.ToString(Eval("id")))%>' type="checkbox" /> 
                </ItemTemplate>
                <ItemStyle Height="21px" HorizontalAlign="Center" />
                <HeaderStyle Width="2%"  HorizontalAlign="Center" />
            </asp:TemplateField>
    <asp:BoundField DataField="序号" HeaderText="序号" SortExpression="序号"/>
    <asp:BoundField DataField="lmmc" HeaderText="栏目" SortExpression="lmmc"/>
     <asp:TemplateField HeaderText="标题"  SortExpression="title">
        
            <ItemTemplate>
            <a href="/xw.aspx?xwid=<%# Eval("id").ToString() %>"  target="_blank"  title="点击查看新闻信息"><%# Eval("title").ToString() %><%# imagestu(Eval("images").ToString()) %></a>
            </ItemTemplate>

            <ItemStyle Height="28px" />
            </asp:TemplateField>
   <asp:TemplateField HeaderText="状态"  SortExpression="title">
        
            <ItemTemplate>
            <%# xwzt(Eval("isyn").ToString()) %></a>
            </ItemTemplate>

            <ItemStyle Height="28px" />
            </asp:TemplateField>
  
    <asp:BoundField DataField="author" HeaderText="发布人" SortExpression="author" />
    <%--<asp:BoundField DataField="titleurl" HeaderText="标题"  HtmlEncode="false" HtmlEncodeFormatString="false" />--%>
        <asp:BoundField DataField="fabutime" HeaderText="发布时间" 
            SortExpression="fabutime" />
        
        <asp:TemplateField HeaderText="">
                 
                <HeaderTemplate><asp:LinkButton name="btnDelete" onclick="Button3_Click"  txttop="txttop" ToolTip="先选择新闻后，批量删除！" ID="btnDelete" runat="server" CommandName="批量发放毕业证"  CommandArgument=''  OnClientClick="return batchAudit('btnDelete');"   Text='批量删除新闻' ></asp:LinkButton>

                <%--<a onclick="return batchAudit(this.id);" id="btnDelete" href="javascript:__doPostBack('btnDelete','')"><span id="plcz" runat="server">点此批量发放毕业证</span></a>--%>
                </HeaderTemplate>
                <ItemTemplate>
             <a href="xwfb.aspx?id=<%# Eval("id").ToString() %>"  txttop="txttop"  title="编辑新闻">编辑</a> &nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CommandName="删除"  CommandArgument='<%#Eval("id")%>'    Text='删除' >      
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
                <%if (GridView1.PageCount >= 8 && GridView1.PageCount - GridView1.PageIndex >= 8)
                  {
                     
                       %>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+2 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+2 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+3 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+3 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+4 %></asp:LinkButton>
               <asp:LinkButton ID="LinkButton5" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+5 %>"
                CommandName="Page"><%#  ((GridView)Container.NamingContainer).PageIndex+6 %></asp:LinkButton> 
                <asp:LinkButton ID="LinkButton6" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+6 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+6 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton7" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+7 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+7 %></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton9" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+8 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+8 %></asp:LinkButton>

                <%}
                  else if (GridView1.PageCount >= 8 && GridView1.PageCount - GridView1.PageIndex >= 5)
                  { %>
                    <asp:LinkButton ID="LinkButton8" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+2%>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+2 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton10" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+3 %>" CommandName="Page"> <%#  ((GridView)Container.NamingContainer).PageIndex+3 %></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton11" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+4 %></asp:LinkButton>
                    <%}%>

            <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">下一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">尾页</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;转到<asp:TextBox ID="txt_go" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>

            <asp:LinkButton ID="LinkButtonGo" runat="server"  Text="跳转" OnClick="LinkButtonGo_Click" /></span><span style="float:right;"><b>新闻数:<%#ViewState["count"].ToString()%>条</b>&nbsp;</b></font>&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /></span>
        </PagerTemplate>
    </asp:GridView>
        <asp:SqlDataSource onselected="SqlDataSource1_Selected"   ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
            SelectCommand="SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc, xw_neirong.title, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.isyn,xw_neirong.id,xw_lanm.glqx FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid">
        </asp:SqlDataSource>
    </div>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    
                            
        SelectCommand="SELECT [lmid], [lmmc],[glqx] FROM [xw_lanm] WHERE ([fid] = @fid) ORDER BY [px]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList1" Name="fid" 
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    
                            
        SelectCommand="SELECT [lmid], [lmmc], [px],[glqx] FROM [xw_lanm] WHERE (([fid] = -1) AND (url ='' or url='#' or url is null)) order by px">
                </asp:SqlDataSource>                    
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
                      alert("请先选择要删除的新闻,在新闻前打勾!");
                      return false;
                  }
                  else {
                      if (id == "btnDelete") {
                          if (confirm("您确认要批量删除这" + String(AuditVal.length / 3) + "条新闻吗？")) {
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
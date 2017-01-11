<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dsgl.aspx.cs" Inherits="admin_dsgl" %>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<HTML><HEAD><TITLE>大赛管理页</TITLE>
<META content="text/html; charset=utf-8" http-equiv=Content-Type>
<META name=author content=www.cdivtc.com>
<META name=viewport 
content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
<META name=apple-mobile-web-app-capable content=yes>
<META name=apple-mobile-web-app-status-bar-style content=black>
<META name=format-detection content=telephone=no>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<LINK rel=stylesheet 
type=text/css href="index.css" charset=gbk>

<META name=GENERATOR content="MSHTML 8.00.6001.19403"></HEAD>
<body id="C_User">
    <form id="form1" runat="server">
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="#">当前位置&gt;&gt;&nbsp;大赛赛事管理</a>
            
                <asp:Button 
                ID="Button4" runat="server" Text="添加新赛事" CssClass="click" 
                onclick="Button2_Click3" />
                <asp:Button 
                ID="Button5" runat="server" Text="&nbsp;刷新&nbsp;" CssClass="click" 
                onclick="Button2_Click31" />
			
			&nbsp;&nbsp;
			
              
			
			
			
		</td>

		<td style="text-align:right">
        使用大赛名称搜索：
            <asp:TextBox 
                ID="SearchBox" runat="server" Width="80px" Text=""></asp:TextBox>
            <asp:DropDownList ID="DropDownList5" runat="server" AppendDataBoundItems="True" 
                onselectedindexchanged="Button1_Click" AutoPostBack="True">
                <asp:ListItem>2016</asp:ListItem>
                <asp:ListItem>2015</asp:ListItem>
                
            </asp:DropDownList> 

        <asp:Button 
                ID="Button1" runat="server" Text=" 查询 " CssClass="click" 
                onclick="Button1_Click" />&nbsp;
                <asp:Button 
                ID="Button2" runat="server" Text=" 导出 " CssClass="click" 
                onclick="Button2_Click" /></td>
	</tr>
  </thead>
</table>
 
    <br />
    展示说明：此页展示了系统管理员在后台的设置和添加、修改、删除、查询、导出等相关功能。<br />
 

<div id="Div1">
<asp:HiddenField ID="hdfWPBH" runat="server" />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1"  CssClass="table" EmptyDataText="无相关数据！"
        AllowPaging="True" AllowSorting="True" DataKeyNames="id" OnRowCommand="GridView1_RowCommand"
        OnPageIndexChanging="GridView1_PageIndexChanging"  onsorting="GridView1_Sorting" PageSize="2"
      >
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

            <asp:BoundField   DataField="uid" HeaderText="序号" HeaderStyle-Width="40" 
                     ReadOnly="True" SortExpression="uid">
            </asp:BoundField>
            <asp:BoundField DataField="id" HeaderText="id" HeaderStyle-Width="40" 
                SortExpression="id" InsertVisible="False"  Visible=false ReadOnly="True" >
            </asp:BoundField>
            <asp:BoundField DataField="nd" HeaderText="年度" HeaderStyle-Width="50" 
                SortExpression="nd" >
            </asp:BoundField>
                        <asp:BoundField DataField="name" HeaderText="大赛名称" HeaderStyle-Width="200" 
                SortExpression="name" ReadOnly="True" >
            </asp:BoundField>
            
            <asp:BoundField DataField="zbdw" HeaderText="主办单位" 
                SortExpression="zbdw" >
            </asp:BoundField>
            <asp:BoundField DataField="starttime" HeaderText="开始时间" HeaderStyle-Width="150" 
                SortExpression="starttime" >
            </asp:BoundField>
            
            
                    <%-- <asp:HyperLinkField DataNavigateUrlFormatString="banzrupdate.aspx?mode=add&guid={0}&uid={1}"   HeaderStyle-Width="25" DataNavigateUrlFields="guid,uid"   Text="详情" HeaderText="管理操作" />--%>
                      
         <%--<asp:CommandField HeaderText=""   ShowDeleteButton="true"   HeaderStyle-Width="45"  ShowHeader="False">
            <ControlStyle CssClass="manage" />
            
            </asp:CommandField>--%>
            
               
                 <asp:BoundField DataField="overtime" HeaderText="结束时间" SortExpression="overtime" />
               <asp:TemplateField >
 <HeaderTemplate>管理操作

                <%--<a onclick="return batchAudit(this.id);" id="btnDelete" href="javascript:__doPostBack('btnDelete','')"><span id="plcz" runat="server">点此批量发放毕业证</span></a>--%>
                </HeaderTemplate>
               
                <ItemTemplate>
                <asp:Button ID="Button1"  runat="server"  CommandArgument='<%#Eval("id").ToString()%>' Font-Size="Medium"  CommandName="修改" Text="修改" />
              &nbsp;&nbsp;
             <asp:Button ID="Button3"  runat="server"  CommandArgument='<%#Eval("id").ToString()%>' Font-Size="Medium"  CommandName="删除" Text="删除" />
              
            </ItemTemplate>
                
                </asp:TemplateField>
        </Columns>
         <PagerTemplate>
<span style="float:left;">

<asp:LinkButton runat="server" Text="批量删除" OnClientClick="return batchAudit(this.id);" ID="multiDelete" onclick="Button3_Click1" ></asp:LinkButton>
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

            <asp:LinkButton ID="LinkButtonGo" runat="server"  Text="跳转" OnClick="LinkButtonGo_Click" />
            </span><span style="float:right;"><b>总记录:<%#ViewState["count"].ToString()%></b>&nbsp;&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /> </span>
        </PagerTemplate>

      
        
    </asp:GridView>
 
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        
        SelectCommand="SELECT    row_number() over (order by id desc) as uid, id,name,nd,zbdw,starttime,overtime,bz from ds_saishi order by id desc " 
        
        onselected="SqlDataSource1_Selected" >
    </asp:SqlDataSource>
    
    
    


    
    </div>
     <input id="alertMessage" runat="server"  style="font-size:Large;" type="hidden" />
    <input id="alertMessage2" runat="server"  style="font-size:Large;" type="hidden" />
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
                if (id == "GridView1_multiDelete") {
                    if (confirm("您确认要批量删除记录吗？")) {
                        document.getElementById("hdfWPBH").value = AuditVal;
                        //alert(document.getElementById("hdfWPBH").value);
                        return true;
                    }
                    return false;
                }
            }
        }  
    </script>
      
</form>



   <!--提示信息段-->
    
   
    <script>
        // skin demo
        (function () {
            var _skin, _jQuery;
            var _search = window.location.search;
            if (_search) {
                _skin = _search.split('demoSkin=')[1];
                _jQuery = _search.indexOf('jQuery=true') !== -1;
                if (_jQuery) document.write('<scr' + 'ipt src="artDialog/jquery-1.6.2.min.js"></sc' + 'ript>');
            };

            document.write('<scr' + 'ipt src="artDialog/artDialog.source.js?skin=' + (_skin || 'blue') + '"></sc' + 'ript>');
            window._isDemoSkin = !!_skin;
            window.onload = function () {
                _skin && art.dialog({
                    content: '欢迎使用"artDialog"对话框组件!',
                    icon: 'succeed',
                    fixed: true
                }, function () {
                    this.title('2秒后自动关闭').lock().time(0);
                    return false;
                });
            };
        })();
</script>

<script  type="text/javascript" src="artDialog/plugins/iframeTools.source.js"></script>

<script type="text/javascript">
    //var ckeditor;
    //$(function(){
    //    ckeditor = CKEDITOR.replace("=txtBoxNote.ClientID ");
    // });
    if (document.all("alertMessage").value != "") {
        showts(document.all("alertMessage").value);
        document.all("alertMessage").value = "";
    }
    if (document.all("alertMessage2").value != "") {
        showurl(document.all("alertMessage2").value);
        document.all("alertMessage2").value = "";
    }
    function showurl(url) {
        art.dialog.open(url, { title: '提示', left: '50px', width: '1000px', height: '100%', fixed: true });
    }
    function showts(content) {
        var timer;
        art.dialog({
            id: 'xxtszm',
            content: content,
            init: function () {
                var that = this, i = 20;
                var fn = function () {
                    that.title('信息提示!' + i + '秒后自动关闭');
                    !i && that.close();
                    i--;
                };
                timer = setInterval(fn, 1000);
                fn();
            },
            close: function () {
                clearInterval(timer);
            }
        }).show();
    }

    function Image10_onclick() {

    }

</script>
<!--提示信息结束，后台调用，this.alertMessage="<span style=\"font-size:Large;\"><font color=red>对不起，你的结束时间格式不正确，请检查！</font></span>";-->
  
    </body>
</html>


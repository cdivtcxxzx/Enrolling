<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false"  CodeFile="rizgl.aspx.cs" Inherits="admin_rizgl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=7" />
<title>日志管理</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<link rel="stylesheet" href="sqtime/jquery-ui.css" /> 
 
 <script type="text/javascript"  src="sqtime/jquery-ui.js"></script> 
 <script type="text/javascript"  src="sqtime/jquery.ui.datepicker-zh-CN.js"></script> 
 
 <script type="text/javascript" >
     $(function () {
         $("#TextBox3").datepicker($.datepicker.regional["zh-CN"]);
         $("#TextBox4").datepicker($.datepicker.regional["zh-CN"]);
         
     });  
 </script>

 


</head>
<body id="C_User">
    <form id="form1" runat="server">
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;日志管理</a>
			
			
			
		</td>
		
		<td style="text-align:right">
            <asp:TextBox 
                ID="TextBox1" runat="server" Width="50px" Text="登陆名"></asp:TextBox>
            <asp:DropDownList ID="DropDownList2" runat="server" DataTextField="ZMC" 
                DataValueField="ZID">
                <asp:ListItem Value="全部">全部</asp:ListItem>
            </asp:DropDownList> <asp:DropDownList ID="DropDownList3" runat="server" 
                DataTextField="lmmc" DataValueField="lmid">
                <asp:ListItem>全部</asp:ListItem>
            </asp:DropDownList> <asp:DropDownList ID="DropDownList4" runat="server" 
                DataTextField="qxmc" DataValueField="qxid">
                <asp:ListItem>全部</asp:ListItem>
            </asp:DropDownList>时间:<asp:TextBox ID="TextBox3" runat="server" Width="60px" 
                AutoPostBack="True" ></asp:TextBox>&nbsp;~<asp:TextBox ID="TextBox4" 
                runat="server" Width="60px"></asp:TextBox>
        <asp:Button 
                ID="Button1" runat="server" Text=" 查询 " CssClass="click" 
                onclick="Button1_Click" />&nbsp;<asp:Button 
                ID="Button2" runat="server" Text=" 导出 " CssClass="click" 
                onclick="Button2_Click" /></td>
	</tr>
  </thead>
</table>
 
  <br />
 
<div id="PanelDefault">
<asp:HiddenField ID="hdfWPBH" runat="server" />  
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  CssClass="table" 
        AllowPaging="True" AllowSorting="True"   PageSize="10"
        OnPageIndexChanging="GridView1_PageIndexChanging" DataKeyNames="id" onsorting="GridView1_Sorting" 
      >
        <Columns>
         <asp:TemplateField HeaderText="编号">  
         <HeaderTemplate>  
           <input type="checkbox" name="BoxIdAll" id="BoxIdAll" onclick="onclicksel();" />  
         </HeaderTemplate>  
         <ItemTemplate>  
           <input id="BoxId" name="BoxId" value='<%#(Convert.ToString(Eval("id")))%>' type="checkbox" />  
         </ItemTemplate>  
         <ItemStyle Height="22px" HorizontalAlign="Center" />  
         <HeaderStyle Width="3%" Height="28px" BackColor="#80B4CF" HorizontalAlign="Center" />  
       </asp:TemplateField>  
            <asp:BoundField DataField="id" HeaderText="编号" HeaderStyle-Width="40"  
                SortExpression="id" InsertVisible="False" ReadOnly="True" >
            
            <HeaderStyle Width="40px" />
            
            </asp:BoundField>
            <asp:BoundField DataField="userid" HeaderText="登陆名"  SortExpression="userid"  
                HeaderStyle-Width="80" >
            <HeaderStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="ZMC" HeaderText="用户组" 
                SortExpression="ZMC" HeaderStyle-Width="80" >
            <HeaderStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="lmmc" HeaderStyle-Width="80" HeaderText="栏目模块"  SortExpression="lmmc"  
                 >
                <HeaderStyle Width="80px" />
            </asp:BoundField>
                <asp:BoundField DataField="qxmc" HeaderStyle-Width="60" HeaderText="操作类型" 
                SortExpression="qxmc" >
            <HeaderStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="ip" HeaderText="ip地址" HeaderStyle-Width="100" 
                SortExpression="ip" >
            <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="time" HeaderText="操作时间" HeaderStyle-Width="110" 
                SortExpression="time" >
            <HeaderStyle Width="110px" />
            </asp:BoundField>
            <asp:BoundField DataField="cznr" HeaderText="操作详情" SortExpression="cznr" />
        </Columns>
         <PagerTemplate>
<span style="float:left;">
&nbsp;&nbsp;&nbsp;&nbsp;

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

            <asp:LinkButton ID="LinkButtonGo" runat="server"  Text="跳转" OnClick="LinkButtonGo_Click" />
            </span><span style="float:right;"><b>总记录：<%#Session["total"].ToString()%></b>&nbsp;&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /> </span>
        </PagerTemplate>

      
        
    </asp:GridView>
    
    
    
     <asp:LinkButton ID="btnDelete" onclick="Button3_Click"  OnClientClick='return batchAudit(this.id)' runat="server"></asp:LinkButton>
    </div>
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
                    if (confirm("您确认要批量删除这" + String(AuditVal.length/2) + "条记录吗？")) {
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
    </body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lanmgl_xb.aspx.cs" Inherits="admin_lanmgl_xb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta http-equiv="X-UA-Compatible" content="IE=7" />

<title>栏目管理</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<script src="images/jquery-1.9.1.js"></script>
  <script src="images/jquery-ui.js"></script>
</head>
<body>
<form id="form1" runat="server">
<table class="table subsubmenu">
	        <thead>
	        <tr>
		       <td>
			   <a href="#">当前位置&gt;&gt;&nbsp;新闻栏目管理系统</a>
			   <a href="lanmadd_xb.aspx?action=add&yxdm=<%=Request["yxdm"].ToString() %>">添加新闻栏目</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Label ID="Label2" runat="server" Font-Size="Medium" Text="当前系部处室:"></asp:Label>
                   <asp:DropDownList ID="DropDownList1" runat="server" 
                       DataSourceID="SqlDataSource1" DataTextField="YXMC" DataValueField="YXDM" 
                       Font-Size="Medium">
                   </asp:DropDownList>
                   <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                       ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                       SelectCommand="SELECT [YXDM], [YXMC] FROM [DM_YUANXI] WHERE ([YXDM] = @YXDM) ORDER BY [YXDM]">
                       <SelectParameters>
                           <asp:QueryStringParameter Name="YXDM" QueryStringField="yxdm" Type="String" />
                       </SelectParameters>
                   </asp:SqlDataSource>
		       </td>
	        </tr>
            </thead>
        </table>
<div id="PanelDefault">
<div>
<asp:ScriptManager ID="sm1" runat="server" />
<asp:UpdatePanel ID="up1" runat="server" ><ContentTemplate >
<asp:ListView 
    ID="lvLanm" 
    runat="server"
    AutoGenerateColumns="false" 
    EmptyDataText="无相关数据!"
    DataSourceID="srcLanm1"
    OnItemDataBound="lvLanm_ItemDataBound"
    EnableViewState="false"  
    DataKeyNames="lmid">
    <LayoutTemplate>
        <table class="table" style="margin-top:8px;">
        <thead>
	        <tr>		
		        <td width="5%">编号</td>
		
		        <td width="20%">名称</td>
		
		        <td width="18%">菜单显示</td>
		
		        <td width="18%">导航显示</td>
		
		        <td width="18%">首页显示</td>
		
		        <%--<td width="8%">记录日志</td>--%>
		
<%--		        <td width="8%">权限控制</td>
		
		        <td width="20%">权限列表(只显示前5个)</td>--%>
		
		        <td width="5%">排序</td>
		
		        <td width="15%">管理操作</td>
            </tr>
        </thead>
            <asp:PlaceHolder ID="ItemPlaceholder" runat="server" />
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr id='<%#(Convert.ToString(Eval("lmid")))%>'>
            <td><asp:Label ID="Label1" runat="server" Text='<%# GetNumLanm1() %>' /></td>
            <td><img src='<%#(Convert.ToString(Eval("lmtp")))%>' />&nbsp;<strong><a href='lanmadd_xw.aspx?id=<%#(Convert.ToString(Eval("lmid")))%>&action=edit'><%#(Convert.ToString(Eval("lmmc")))%></a></strong></td>
            <td><a href="#" id='<%# "sfcdxs"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfcdxs")))%>','sfcdxs')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfcdxs"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfcdxs")))%></font></a></td>
            <td><a href="#" id='<%# "sfdhxs"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfdhxs")))%>','sfdhxs')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfdhxs"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfdhxs")))%></font></a></td>
            <td><a href="#" id='<%# "sftop"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sftop")))%>','sftop')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sftop"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sftop")))%></font></a></td>
            <%--<td><a href="#" id='<%# "sfjlrz"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfjlrz")))%>','sfjlrz')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfjlrz"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfjlrz")))%></font></a></td>--%>
           <%-- <td><a href="#" id='<%# "sfqxyz"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfqxyz")))%>','sfqxyz')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfqxyz"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfqxyz")))%></font></a></td>
            <td class="manage"><%#GetPower(Convert.ToString(Eval("lmyyqx")))%></td>--%>
            <td class="manage"><%# Eval("px") %></td>
            <td class="manage"><a href="javascript:Button1_onclick('lanmadd_xb.aspx?id=<%#(Convert.ToString(Eval("lmid")))%>&action=edit&yxdm=<%#(Convert.ToString(Eval("xbdm")))%>')"  class="copy">修改</a>
                               <a href="#" onclick="ajaxDelete('<%#(Convert.ToString(Eval("lmid")))%>')" class="copy">删除</a></td>
        </tr>
        <asp:ListView ID="lvLanm2"
            DataSourceID="srcLanm2" 
            runat="server"
            EnableViewState="false"
            AutoGenerateColumns="false"
            DataKeyNames="lmid" 
            OnItemDataBound="lvLanm2_ItemDataBound"  >
            <ItemTemplate>
                <tr id='<%#(Convert.ToString(Eval("lmid")))%>'>
                    <td>&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# GetNumLanm2() %>' /></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='<%#(Convert.ToString(Eval("lmtp")))%>' />&nbsp;<strong><a href='lanmadd_xw.aspx?id=<%# Convert.ToString(Eval("lmid"))%>&action=edit'><%#(Convert.ToString(Eval("lmmc")))%></a></strong></td>
                    <td><a href="#" id='<%# "sfcdxs"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfcdxs")))%>','sfcdxs')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfcdxs"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfcdxs")))%></font></a></td>
                    <td><a href="#" id='<%# "sfdhxs"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfdhxs")))%>','sfdhxs')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfdhxs"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfdhxs")))%></font></a></td>
                    <td><a href="#" id='<%# "sftop"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sftop")))%>','sftop')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sftop"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sftop")))%></font></a></td>
                    <%--<td><a href="#" id='<%# "sfjlrz"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfjlrz")))%>','sfjlrz')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfjlrz"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfjlrz")))%></font></a></td>--%>
<%--                    <td><a href="#" id='<%# "sfqxyz"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfqxyz")))%>','sfqxyz')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfqxyz"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfqxyz")))%></font></a></td>
                    <td class="manage"><%#GetPower(Convert.ToString(Eval("lmyyqx")))%></td>--%>
                    <td class="manage"><%# Eval("px") %></td>
                    <td class="manage"><a href="javascript:Button1_onclick('lanmadd_xb.aspx?id=<%#(Convert.ToString(Eval("lmid")))%>&action=edit&yxdm=<%#(Convert.ToString(Eval("xbdm")))%>')"  class="copy">修改</a>
                                       <a href="#" onclick="ajaxDelete('<%#(Convert.ToString(Eval("lmid")))%>')" class="copy">删除</a></td>
                </tr>
                <asp:ListView ID="lvLanm3" runat="server" DataSourceID="srcLanm3" EnableViewState="false" AutoGenerateColumns="false">
                    <ItemTemplate>
                         <tr id='<%#(Convert.ToString(Eval("lmid")))%>'>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# GetNumLanm3() %>' /></td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='<%#(Convert.ToString(Eval("lmtp")))%>' />&nbsp;<strong><a href='lanmadd.aspx?id=<%# Convert.ToString(Eval("lmid"))%>&action=edit'><%#(Convert.ToString(Eval("lmmc")))%></a></strong></td>
                    <td><a href="#" id='<%# "sfcdxs"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfcdxs")))%>','sfcdxs')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfcdxs"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfcdxs")))%></font></a></td>
                    <td><a href="#" id='<%# "sfdhxs"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfdhxs")))%>','sfdhxs')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfdhxs"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfdhxs")))%></font></a></td>
                    <td><a href="#" id='<%# "sftop"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sftop")))%>','sftop')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sftop"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sftop")))%></font></a></td>
                    <td><a href="#" id='<%# "sfjlrz"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfjlrz")))%>','sfjlrz')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfjlrz"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfjlrz")))%></font></a></td>
<%--                    <td><a href="#" id='<%# "sfqxyz"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfqxyz")))%>','sfqxyz')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfqxyz"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfqxyz")))%></font></a></td>
                    <td class="manage"><%#GetPower(Convert.ToString(Eval("lmyyqx")))%></td>--%>
                    <td class="manage"><%# Eval("px") %></td>
                    <td class="manage"><a href="javascript:Button1_onclick('lanmadd_xb.aspx?id=<%#(Convert.ToString(Eval("lmid")))%>&action=edit&yxdm=<%#(Convert.ToString(Eval("xbdm")))%>')"  class="copy">修改</a>
                                       <a href="#" onclick="ajaxDelete('<%#(Convert.ToString(Eval("lmid")))%>')" class="copy">删除</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <asp:SqlDataSource 
                        ID="srcLanm3"                         
                        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                        SelectCommand="SELECT * FROM xibu_lanm WHERE fid=@secondfid  order by px" runat="server">
                        <SelectParameters>
                            <asp:Parameter Name="secondfid" />
                        </SelectParameters>
                </asp:SqlDataSource>               
            </ItemTemplate>
                </asp:ListView>
                        <asp:SqlDataSource 
                            ID="srcLanm2" 
                            ConnectionString="<%$ ConnectionStrings:SqlConnString %>"
                            SelectCommand="SELECT * FROM xibu_lanm WHERE fid=@getfid  order by px" runat="server">
                            <SelectParameters>
                                <asp:Parameter Name="getfid"/>
                            </SelectParameters>
                        </asp:SqlDataSource>
    </ItemTemplate>
        </asp:ListView> 
        <asp:SqlDataSource 
            ID="srcLanm1" 
            runat="server" 
            ConnectionString="<%$ ConnectionStrings:SqlConnString %>"
            SelectCommand="SELECT * FROM xibu_lanm WHERE fid=@fid and xbdm=@yxdm order by px">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="fid" Type="String" />
               
                           <asp:QueryStringParameter Name="YXDM" QueryStringField="yxdm" Type="String" />
                 <%--<asp:Parameter DefaultValue="0" Name="fid2" Type="String" />--%>
            </SelectParameters>
        </asp:SqlDataSource>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>
 <!--提示信息段-->
    <input id="alertMessage" runat="server"  style="font-size:Large;" type="hidden" />
</form>
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

<script  type="text/javascript" src="/admin/artDialog/plugins/iframeTools.source.js"></script>
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function Button1_onclick(url) {
            art.dialog.open(url, { title: '栏目管理－修改栏目', width: '100%', height: '100%', fixed: true });
        }

// ]]>
    </script>
<script type="text/javascript">

    function ajaxProcess(lmid, sfxs, item) {
        var color = $("#" + item + lmid + " font").css("color");
        if (color == "black" || color == "rgb(0,0,0)" || color == "#000000")
            color = "red";
        else
            color = "black";
        var datas = { "lmid": lmid, "sfxs": sfxs, "item": item };
        $.ajax({
            type: "POST",
            url: "ajaxHolder/ajaxLanmXBHandler.ashx",
            data: datas,
            success: function (msg) {
                if (msg == "1") {
                    $("#" + item + lmid + " font").html($("#" + item + lmid + " font").html() == "ㄨ" ? "√" : "ㄨ");
                    $("#" + item + lmid + " font").css("color", color);
                }
                else {
                    alert("更新失败！");
                }
            }
        });
    }

    function ajaxDelete(lmid) {
        if (confirm('确定删除该栏目?')) {
            var datas = { "lmid": lmid, "action": "Delete" };
            $.ajax({
                type: "POST",
                url: "ajaxHolder/ajaxLanmXBHandler.ashx",
                data: datas,
                success: function (msg) {
                    if (msg == "success") {

                        $("#" + lmid).hide();

                        alert("删除成功！");
                    }
                    else {
                        alert("更新失败！");
                    }
                }
            });
        }
    }
</script>
</body>
</html>

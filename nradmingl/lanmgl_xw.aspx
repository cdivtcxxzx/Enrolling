<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lanmgl_xw.aspx.cs" Inherits="admin_lanmgl_xw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />


<title>栏目管理</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
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
            url: "ajaxHolder/ajaxLanmXWHandler.ashx",
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
                url: "ajaxHolder/ajaxLanmXWHandler.ashx",
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
        //window.scrollTo(0, 0);      
    }
</script>
</head>
<body>
<form id="form1" runat="server">
<table class="table subsubmenu">
	        <thead>
	        <tr>
		       <td>
			   <a href="#">当前位置&gt;&gt;&nbsp;新闻栏目管理系统</a>
			   <a href="lanmadd_xw.aspx?action=add">添加新闻栏目</a>
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
		
		        <td width="10%">菜单显示</td>
		
		        <td width="10%">置顶新闻</td>
		
		        <td width="10%">首页显示</td>
		
		        <td width="10%">开启审核</td>
		
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
            <td><asp:Label runat="server" Text='<%# GetNumLanm1() %>' /></td>
            <td><img src='<%#(Convert.ToString(Eval("lmtp")))%>' />&nbsp;<strong><a href='lanmadd_xw.aspx?id=<%#(Convert.ToString(Eval("lmid")))%>&action=edit'><%#(Convert.ToString(Eval("lmmc")))%></a></strong></td>
            <td><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfcdxs"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfcdxs")))%></font></td>
            <td><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfdhxs"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfdhxs")))%></font></td>
            <td><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sftop"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sftop")))%></font></td>
          <td><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfsh"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfsh")))%></font></td>
          
            <%--<td><a href="#" id='<%# "sfjlrz"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfjlrz")))%>','sfjlrz')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfjlrz"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfjlrz")))%></font></a></td>--%>
           <%-- <td><a href="#" id='<%# "sfqxyz"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfqxyz")))%>','sfqxyz')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfqxyz"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfqxyz")))%></font></a></td>
            <td class="manage"><%#GetPower(Convert.ToString(Eval("lmyyqx")))%></td>--%>
            <td class="manage"><%# Eval("px") %></td>
            <td class="manage"><a href="lanmadd_xw.aspx?id=<%#(Convert.ToString(Eval("lmid")))%>&action=edit" class="copy">修改</a>
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
                    <td><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfcdxs"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfcdxs")))%></font></td>
                    <td><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfdhxs"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfdhxs")))%></font></td>
                    <td><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sftop"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sftop")))%></font></td>
                   <td><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfsh"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfsh")))%></font></td>
                  
                    <%--<td><a href="#" id='<%# "sfjlrz"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfjlrz")))%>','sfjlrz')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfjlrz"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfjlrz")))%></font></a></td>--%>
<%--                    <td><a href="#" id='<%# "sfqxyz"+(Convert.ToString(Eval("lmid")))%>' onclick="ajaxProcess('<%#(Convert.ToString(Eval("lmid")))%>','<%#(Convert.ToString(Eval("sfqxyz")))%>','sfqxyz')"><font color='<%#getColorLanmStatus(Convert.ToString(Eval("sfqxyz"))) %>' ><%#displayLanmStatus(Convert.ToString(Eval("sfqxyz")))%></font></a></td>
                    <td class="manage"><%#GetPower(Convert.ToString(Eval("lmyyqx")))%></td>--%>
                    <td class="manage"><%# Eval("px") %></td>
                    <td class="manage"><a href="lanmadd_xw.aspx?id=<%#(Convert.ToString(Eval("lmid")))%>&action=edit" class="copy">修改</a>
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
                    <td class="manage"><a href="lanmadd_xw.aspx?id=<%#(Convert.ToString(Eval("lmid")))%>&action=edit" class="copy">修改</a>
                                       <a href="#" onclick="ajaxDelete('<%#(Convert.ToString(Eval("lmid")))%>')" class="copy">删除</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <asp:SqlDataSource 
                        ID="srcLanm3"                         
                        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                        SelectCommand="SELECT * FROM xw_lanm WHERE fid=@secondfid  order by px" runat="server">
                        <SelectParameters>
                            <asp:Parameter Name="secondfid" />
                        </SelectParameters>
                </asp:SqlDataSource>               
            </ItemTemplate>
                </asp:ListView>
                        <asp:SqlDataSource 
                            ID="srcLanm2" 
                            ConnectionString="<%$ ConnectionStrings:SqlConnString %>"
                            SelectCommand="SELECT * FROM xw_lanm WHERE fid=@getfid  order by px" runat="server">
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
            SelectCommand="SELECT * FROM xw_lanm WHERE fid=@fid order by px">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="fid" Type="String" />
                <%--<asp:Parameter DefaultValue="0" Name="fid2" Type="String" />--%>
            </SelectParameters>
        </asp:SqlDataSource>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>
</form>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ds_tj.aspx.cs" Inherits="admin_ds_tj" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>赛事统计</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<LINK rel=stylesheet 
type=text/css href="index.css" charset=gbk>
    <style type="text/css">
        .style1
        {
            width: 13px;
        }
    </style>
</head>
<body id="C_User">
    <form id="form1" runat="server">
       <div id="PanelUpdate">
       <table class="table">
	<thead>
	<tr>
		<td  style="padding:14px;">
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;赛事分类统计</a>

		</td><td style="text-align:right">
            年度、大赛选择：
            <asp:DropDownList ID="DropDownList5" runat="server" AppendDataBoundItems="True" 
                AutoPostBack="True" 
                Font-Size="Medium">
                <asp:ListItem Value="2016">2016年</asp:ListItem>
                <asp:ListItem Value="2015">2015年</asp:ListItem>
                
            </asp:DropDownList> 

            <asp:DropDownList ID="DropDownList6" runat="server" 
                DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="name" 
                Font-Size="Medium">
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList7" runat="server" AppendDataBoundItems="True" 
                onselectedindexchanged="Button1_Click" AutoPostBack="True" 
                Font-Size="Medium">
                <asp:ListItem Value="区县筛选">区县筛选</asp:ListItem>
                
            </asp:DropDownList> 

            <asp:DropDownList ID="DropDownList8" runat="server" AppendDataBoundItems="True" 
                onselectedindexchanged="Button1_Click" AutoPostBack="True" 
                Font-Size="Medium">
                <asp:ListItem Value="赛项筛选">赛项筛选</asp:ListItem>
                
            </asp:DropDownList> 

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                SelectCommand="SELECT [name] FROM [ds_saishi] WHERE ([nd] = @nd)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="DropDownList5" Name="nd" 
                        PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>

        <asp:Button 
                ID="Button1" runat="server" Text=" 统计 " CssClass="click" 
                onclick="Button1_Click" Font-Size="Medium" />&nbsp;
                <asp:Button 
                ID="Button2" runat="server" Text="导出结果" CssClass="click" 
                onclick="Button2_Click" Font-Size="Medium" /></td>
		

	</tr>
  </thead>
  </table>
  <br>
  
    <table class="table">
	
   <tr>
			<td class="style1">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/admin/images/tj1.png" />
            </td>
</tr>
</table>
 
  <br />
  <input id="alertMessage" runat="server"  style="font-size:Large;" type="hidden" />
    <input id="alertMessage2" runat="server"  style="font-size:Large;" type="hidden" />
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
  

</div>
</form>
</body>
</html>



<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dsgladd.aspx.cs" Inherits="admin_dsgladd" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>添加赛事</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
    <style type="text/css">
        .style1
        {
            width: 139px;
        }
        </style>
</head>
<body id="C_User">
    <form id="form1" runat="server">
       <div id="PanelUpdate">
       <table class="table">
	<thead>
	<tr>
		<td colspan="2"  style="padding:14px;">
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;赛事添加及修改</a>

		</td>
		

	</tr>
  </thead>
  </table>
  <br>
  
    <table class="table">
	
   <tr>
			<td style="padding:14px;" class="style1">比赛年度：</td>
			<td  >
			&nbsp;<asp:DropDownList ID="DropDownList4" runat="server" Font-Size="Medium">
                    <asp:ListItem Selected="True" Value="2016">2016年</asp:ListItem>
                    <asp:ListItem Value="2015">2015年</asp:ListItem>
                    <asp:ListItem Value="2014">2014年</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
			</td>
		</tr>	
        <tr>
			<td  class="style1"  style="padding:14px;">赛事名称：</td>
			<td  >
			
                
                <asp:TextBox ID="ssname" runat="server" Font-Size="Medium" Width="335px"></asp:TextBox>
			
                
            </td>
		</tr>	
         <tr>
			<td  class="style1"  style="padding:14px;">主办单位：</td>
			<td  >
			
                
                <asp:TextBox ID="zbdw" runat="server" Font-Size="Medium" Width="335px"></asp:TextBox>
			
                
            </td>
		</tr>
        	<tr>
			<td  class="style1"  style="padding:14px;">比赛时间段：</td>
			<td >
			    开始时间：<asp:TextBox ID="starttime" runat="server" 
                    AutoPostBack="true" Width="164px" 
                    Font-Size="Medium">2016-09-23 08:00:00</asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 截止时间：<asp:TextBox ID="overtime" runat="server" 
                    AutoPostBack="true" Width="158px" 
                    Font-Size="Medium">2016-10-23 08:00:00</asp:TextBox></td>
		</tr>
        


<tr class="noeffect">
	<td class="style1"  style="padding:14px;"></td>
	<td >
            <br />
            <asp:Button ID="tijiao" runat="server"  
            onclick="LB_insert_Click" Text="确认添加" Font-Size="Medium" />
            <br />
            <%--<input type="button"  class="button" ID="LB_insert" runat="server" onclick="LB_insert_Click"  value='确认添加' />--%>
            
        &nbsp;</td>
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


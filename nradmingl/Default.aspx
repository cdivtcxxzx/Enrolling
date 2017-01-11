<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" %>
<%
  
    %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<meta name="keywords" content="" />
<meta name="description" content="" />

<title></title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<style type="text/css">
html {
	
	/*overflow:hidden;*/
	background:#fff;	
    padding:115px 0px 30px 0px;/*---ie6---*/
}
body {
	
	/*overflow:hidden;*/
	background:#fff;
}
<style type="text/css">
html {height:100%;}
body {height:100%;}
</style>
<!--[if lt IE 7]>     
 <style type="text/css">
html {height:100%;}
body {height:100%;}
</style> <![endif]-->
<!--[if IE 7]>         
<style type="text/css">
html {height:100%;}
body {height:100%;}
</style> <![endif]-->
<script type="text/javascript">
    var isIe = true;
    if (!document.all) {
        isIe = false;
    }
    function downToResize(obj, e) {
        obj.style.cursor = 'col-resize';
        var e = isIe ? window.event : e;
        //记录开始准备移动的起始位置    
        obj.mouseDownX = e.clientX;
        //父级左边框架的宽度    
        obj.parentLeftFW = document.getElementById('leftdh').width;
        //父级右边框架的宽度    
        obj.parentRightFW = document.getElementById('main').parentNode.clientWidth;
        obj.parentBox = document.getElementById('box');
        if (isIe) {
            obj.setCapture();
        } else {
            alert('不支持火狐..');
            obj.mouseDownX = 0;
        }
    }
    function moveToResize(obj, e) {
        var e = isIe ? window.event : e;
        if (!obj.mouseDownX) return false;
        obj.removed = 1;
        obj.parentBox.style.display = 'inline';
        obj.parentBox.style.width = obj.offsetWidth;
        obj.parentBox.style.height = obj.offsetHeight;
        obj.parentBox.style.left = e.clientX;
        obj.parentBox.style.top = getPosTop(obj.parentBox);
    }
    function getPosLeft(elm) {
        var left = elm.offsetLeft;
        while ((elm = elm.offsetParent) != null) {
            left += elm.offsetLeft;
        }
        return left;
    }
    function getPosTop(elm) {
        var top = elm.offsetTop;
        while ((elm = elm.offsetParent) != null) {
            top += elm.offsetTop;
        }
        return top;
    }

 
    function upToResize(obj, e) {
        var e = isIe ? window.event : e;
        //实际的移动边框的大小   
        var changeW = e.clientX * 1 - obj.mouseDownX;
        if (changeW != 0 && obj.removed) {
            var newLeftW = obj.parentLeftFW * 1 + changeW;
            var newRightW = obj.parentRightFW * 1 - changeW;
            if (newLeftW <= 50) {    //如果左边宽度小于150时，左边宽度就是150 
                newLeftW = 150;
            }
            if (newRightW <= 200) {    //如果右边宽度小于50时，左边宽度就是150
                newLeftW = 150;
            }
            var leftObj = document.getElementById('leftdh').parentNode;
            leftObj.width = newLeftW;
            leftObj.firstChild.style.width = newLeftW + 'px';
        }
        if (isIe) {
            obj.releaseCapture();
        } else {

        }
        obj.mouseDownX = 0;
        obj.removed = 0;
        obj.style.cursor = 'default';
        obj.parentBox.style.display = 'none';
    }
    function setCookie(name, value) {
        var Days = 30;
        var exp = new Date();
        exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
    }
    setCookie(xwidth, '800');
    setCookie(xheight, '500');
  </script>

</head>
<body style="margin:0; padding:0;" id="pagemain">



    <form id="form1" runat="server">

<div class="top">
<!--
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td style="background:url(images/top_bg.gif) repeat-x left -20px; height:20px;"></td>
			</tr>
		</table>
-->
		<table width="100%" border="0" cellspacing="0" style="background-image:url(images/bg.jpg); text-align:right;" cellpadding="0">
			<tr>
				<td id="logo" width="20" height="80">
					&nbsp;<%--<img runat="server" id="logo" src="" alt="logo" />--%>

				</td><td>&nbsp;</td>
				<td width="420" align="right">
					<table border="0" align="center" cellpadding="0" cellspacing="0">
						<tr>
							<td  class="nav" id="toptool1" title="" runat="server">用户&nbsp;:&nbsp;<asp:Label ID="txtUsername" runat="server"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;用户组&nbsp;:&nbsp;<asp:Label ID="txtUsergroup" 
                                    runat="server"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;时间&nbsp;:&nbsp;<span class="time"></span></td>
						</tr>
						<tr>
							<td height="8"></td>
						</tr>
						<tr>
							<td runat="server" id="topdh" class="submenu"></td>
						</tr>
					</table>
				</td>
			</tr>
            <tr>
				<td class="menu"  colspan="2" runat="server" id="menu">
				</td>
			</tr>
		</table>

         
	</div>
   
	<div class="side" runat="server"  id="leftdh">
		
	</div>
	<div class="tool"> 
		<table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0">
		  <tr>
			<td id="toolhide" style="width:11px; border-left:1px solid #ccc; border-right:1px solid #ccc; background:#f1f1f1;"><img alt="退出系统" src="images/ico_close.gif"/></td>
		  </tr>
		</table>
	</div>
	<div class="main"  runat="server"  id="nrdh">
		
	</div>
	<div  class="bottom" style="display:none;font-size:11px; padding:0 10px;" id="copyright" runat="server">
		
	</div>
    </form>
</body>
</html>

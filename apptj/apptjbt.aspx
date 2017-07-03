<%@ Page Language="C#" AutoEventWireup="true" CodeFile="apptjbt.aspx.cs" Inherits="admin_apptjbt" %>


<%
    
    string agent = (Request.UserAgent + "").ToLower().Trim();
   

    if (agent == "" ||
        agent.IndexOf("mobile") != -1 ||
        agent.IndexOf("mobi") != -1 ||
        agent.IndexOf("nokia") != -1 ||
        agent.IndexOf("samsung") != -1 ||
        agent.IndexOf("sonyericsson") != -1 ||
        agent.IndexOf("mot") != -1 ||
        agent.IndexOf("blackberry") != -1 ||
        agent.IndexOf("lg") != -1 ||
        agent.IndexOf("htc") != -1 ||
        agent.IndexOf("j2me") != -1 ||
        agent.IndexOf("ucweb") != -1 ||
        agent.IndexOf("opera mini") != -1 ||
        agent.IndexOf("mobi") != -1)
    {
        //终端可能是手机
       

        Response.Write("<?xml version='1.0'?><!DOCTYPE html PUBLIC '-//WAPFORUM//DTD XHTML Mobile 1.0//EN' 'http://www.wapforum.org/DTD/xhtml-mobile10.dtd'><HTML xmlns='http://www.w3.org/1999/xhtml'>");

    }
    else
    {
        Response.Write(" <!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\">");
    }
     %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   
<title>报名信息统计</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<script src="../files/common/jquery-1.9.1.js"></script>
<script src="../files/common/jquery-ui.js"></script>  
	<script type='text/javascript' src='js2/highcharts.js'></script>
	<script type="text/javascript" src="js2/exporting.js"> </script>
    </head>
<body>
    <form id="form1" runat="server">
    <div id="jsshow" runat="server" style="display:none" ></div>
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="apptj.aspx"><<</a>
            
			
			
			
		</td>
        <td style="width:80px" ><a href="apptjtb.aspx">柱状图</a></td><td style="width:80px" ><a href="?action=manage">饼图</a></td><td style="width:80px" ><a href="apptjll.aspx">流量图</a></td>
		
	</tr>
  </thead>
</table>
 
  <br />
    <div id="PanelManage"  style="display:none" runat="server">
       
<asp:Label ID="tjtime" runat="server"></asp:Label>
    </div>
    <div id="container" style="width:96%;height:480px;margin:0px; margin-left:10px; text-align:center">
     <div style="display:none" id="Div1" runat="server">
       

    </div>
</form>
</body>

</html>

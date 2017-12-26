<%@ Page Language="C#" AutoEventWireup="true" CodeFile="appbaodao2.aspx.cs" Inherits="admin_appbaodao2" %>

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
   
<title>迎新报到统计</title>
<META charset=UTF-8>
<META name=viewport 
content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
<LINK rel=stylesheet type=text/css 
href="../css/bootstrap.css">


<LINK rel=stylesheet 
type=text/css href="../css/AdminLTE.css">

<style>
    .skin-blue-light .main-header .navbar {
	BACKGROUND-COLOR: #3c8dbc
}
.main-footer
{
FONT-SIZE: 11px !important;
}
.box-header {
    
    PADDING-TOP: 0px;
}
.box-body {

    PADDING-TOP: 0px;
}
.box {

    border-left: #00a65a 1px solid;
     border-right: #00a65a 1px solid;
      border-bottom: #00a65a 1px solid;
}
.progress {
    MARGIN-BOTTOM: 10px;
    margin-top: 10px;
}
</style><!--[if lt IE 9]>

<![endif]-->
<META name=GENERATOR content="MSHTML 8.00.7601.17514">



<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<script src="../files/common/jquery-1.9.1.js"></script>
<script src="../files/common/jquery-ui.js"></script>  
	<script type='text/javascript' src='js2/highcharts.js'></script>
	<script type="text/javascript" src="js2/exporting.js"> </script>
</head>
<BODY class="skin-blue-light layout-top-nav">
<DIV class=wrapper><SECTION class=content>




    <div style="width:100%; text-align:center; font-size:20px;"><b>2017年高职迎新情况统计</b>
    
          
        
                </div>
                <br />
    
                
<DIV style="CLEAR: both" class=clear></DIV>
<DIV class=col-md-4 onmousedown="location.href='apptjbd.aspx?id=2'">
<DIV class="box box-success">
<DIV class="box-header with-border" >
<br />
<H3 class=box-title>【来校报到】</H3>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<SPAN class=progress-number>点击查看详情</SPAN>
<DIV class="box-tools pull-right"> </DIV></DIV>
<DIV class=box-body><br />
<DIV class=progress-group id="bd" runat="server"><SPAN class=progress-text style="font-size:20px;">完成进度:2017年8月17日0时开始网上注册</SPAN> <br /><br />

<DIV class="progress progress-sm active">
<DIV style="WIDTH: 0%" 
class="progress-bar progress-bar-yellow progress-bar-striped" 
role=progressbar></DIV></DIV>
<SPAN class=progress-number><B>网上注册人数：0人</B> / 招生人数：0人</SPAN> 
</DIV></DIV></DIV></DIV>
<DIV class=col-md-4 onmousedown="location.href='apptjgz.aspx?id=2'">
<DIV class="box box-success">
<DIV class="box-header with-border" >
<br />
<H3 class=box-title>【网上注册】</H3>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<SPAN class=progress-number>点击查看详情</SPAN>
<DIV class="box-tools pull-right"> </DIV></DIV>
<DIV class=box-body><br />
<DIV class=progress-group id="gzshow" runat="server"><SPAN class=progress-text style="font-size:20px;">完成进度:2017年8月17日0时开始网上注册</SPAN> <br /><br />

<DIV class="progress progress-sm active">
<DIV style="WIDTH: 0%" 
class="progress-bar progress-bar-yellow progress-bar-striped" 
role=progressbar></DIV></DIV>
<SPAN class=progress-number><B>网上注册人数：0人</B> / 招生人数：0人</SPAN> 
</DIV></DIV></DIV></DIV>
<DIV class=col-md-4 onmousedown="location.href='apptjgzsf.aspx?id=2'">
<DIV class="box box-success">
<DIV class="box-header with-border" >
<br />
<H3 class=box-title>【缴费情况】</H3>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<SPAN class=progress-number>点击查看详情</SPAN>
<DIV class="box-tools pull-right"> </DIV></DIV>
<DIV class=box-body><br />
<DIV class=progress-group id="jf" runat="server"><SPAN class=progress-text style="font-size:20px;">完成进度:2017年8月17日0时开始网上注册</SPAN> <br /><br />

<DIV class="progress progress-sm active">
<DIV style="WIDTH: 0%" 
class="progress-bar progress-bar-yellow progress-bar-striped" 
role=progressbar></DIV></DIV>
<SPAN class=progress-number><B>网上注册人数：0人</B> / 招生人数：0人</SPAN> 
</DIV></DIV></DIV></DIV>



</SECTION><!-- Main Footer --></DIV>

</BODY></HTML>

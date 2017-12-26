<%@ Page Language="C#" AutoEventWireup="true" CodeFile="apptjbd.aspx.cs" Inherits="apptj_apptjbd" %>

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
   
<title>来校情况统计</title>
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
    <form id="form1" runat="server">
   
    <div id="PanelManage" runat="server">

    <style>
    .layui-elem-quote {
    margin-bottom: 5px;
    padding: 8px 15px 8px 15px;
    line-height: 32px;
    border-left: 5px solid #005CA3;
    border-radius: 0 5px 5px 0;
    background-color: #f2f2f2;
    margin-left:15px;
    margin-right:15px;
    color: #333333;
    font-family: "Open Sans", sans-serif;
   
    font-size: 13px;
    direction: ltr;
}
.layui-btn-small {
    height: 30px;
    line-height: 30px;
    padding: 0 10px;
    font-size: 12px;
}

.layui-btn {
    display: inline-block;
    height: 30px;
    line-height: 30px;
    padding: 0 18px;
    background-color: #005CA3;
    color: #fff;
    white-space: nowrap;
    text-align: center;
    border: none;
    border-radius: 0px;
    cursor: pointer;
    opacity: .9;
    filter: alpha(opacity=90);
}
.layui-elem-quote {
    margin-bottom: 5px;
    padding: 8px 15px 8px 15px;
    line-height: 32px;
    border-left: 5px solid #005CA3;
    border-radius: 0 5px 5px 0;
    background-color: #f2f2f2;
}
    </style>

    <br />

         <blockquote class="layui-elem-quote" style="margin-bottom: 15px;">&nbsp;<span >
          >高职迎新统计>来校报到情况</span>
           <span style="float:right">

            <!--调用C#原生按钮设置样式举例OVER-->
 <%--               <a href="#" class="layui-btn layui-btn-small hidden-xs">
					<i class="layui-icon">&#xe630;</i> 一卡通更新
				</a>
             --%><a href="appbaodao2.aspx" class="layui-btn layui-btn-small">
					返回
				</a>

               

		  </span>       
      </blockquote>

<DIV style="CLEAR: both" class=clear></DIV>

<div class="col-md-12" style="font-size:16px;">
          <div class="col-md-6">
            <div class="box box-danger">

              <div class="box-header with-border" id="zs" runat="server">
                <div class="progress-group"><span class="progress-text">橙色</span><span class="progress-number"><b>160</b>/200</span>
                  <div class="progress sm"><div class="progress-bar progress-bar-yellow progress-bar-striped" style="width: 78.125%"></div></div>
                </div>
              </div>
              <div class="box-body" id="yxshow" runat="server">
                <div class="progress-group">
                  <span class="progress-text">蓝色</span>
                  <span class="progress-number"><b>160</b>/200</span>
                  <div class="progress sm">
                   <div class="progress-bar progress-bar-aqua progress-bar-striped" style="width: 73.323170731707%"></div>
                  </div>
                </div>
                <div class="progress-group">
                  <span class="progress-text">绿色</span>
                  <span class="progress-number"><b>310</b>/400</span>
          
                  <div class="progress sm active">
                    
                    <div class="progress-bar progress-bar-green progress-bar-striped" style="width: 74.204946996466%"></div>
                  </div>
                </div>
                <div class="progress-group">
                  <span class="progress-text">红色</span>
                  <span class="progress-number"><b>480</b>/800</span>
                  <div class="progress sm">
                    <div class="progress-bar progress-bar-red progress-bar-striped" style="width: 76.824817518248%"></div>
                  </div>
                </div>
                <div class="progress-group">
                  <span class="progress-text">深蓝色</span>
                  <span class="progress-number"><b>250</b>/500</span>
                  <div class="progress sm">
                   <div class="progress-bar progress-bar-light-blue progress-bar-striped" style="width: 78.406466512702%"></div>
                  </div>
                </div>
                <div class="progress-group">
                  <span class="progress-text">浅绿色</span>
                  <span class="progress-number"><b>480</b>/800</span>
                  <div class="progress sm">
                    <div class="progress-bar progress-bar-red2 progress-bar-striped" style="width: 76.824817518248%"></div>
                  </div>
                </div>
                <div class="progress-group">
                  <span class="progress-text">紫色</span>
                  <span class="progress-number"><b>250</b>/500</span>
                  <div class="progress sm">
                   <div class="progress-bar progress-bar-red3 progress-bar-striped" style="width: 76.824817518248%"></div>
                       </div>
                </div>
              </div>
            </div>
          </div>

        </div>

        
    </div>



</form>
</body>

</html>


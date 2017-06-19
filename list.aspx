<%@ page language="C#" autoeventwireup="true"  CodeFile="list.aspx.cs" Inherits="wblue_list"  %>
<!DOCTYPE html>
<html>
<head runat="server">
<meta name="viewport" content="width=device-width,user-scalable=0,initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0"/>
<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
<title></title>

    <link rel="stylesheet" href="./font-awesome/css/font-awesome.min.css">
<link type="text/css" href="b_css/_system/system.css" rel="stylesheet"/>
       <LINK href="b_css/tpl2/system.css" type="text/css" rel="stylesheet"> 
       <LINK href="b_css/tpl2/default/default.css" type="text/css" rel="stylesheet"> 
<link type="text/css" href="b_js/_portletPlugs/simpleNews/css/simplenews.css" rel="stylesheet" />
<link type="text/css" href="b_js/_portletPlugs/datepicker/css/datepicker.css" rel="stylesheet" />
<link type="text/css" href="b_js/_portletPlugs/sudyNavi/css/sudyNav.css" rel="stylesheet" />

<script language="javascript" src="b_js/jquery.min.js"></script>
<script language="javascript" src="b_js/jquery.sudy.wp.visitcount.js"></script>
<script type="text/javascript" src="b_js/_portletPlugs/datepicker/js/jquery.datepicker.js"></script>
<script type="text/javascript" src="b_js/_portletPlugs/datepicker/js/datepicker_lang_HK.js"></script>
<script type="text/javascript" src="b_js/_portletPlugs/sudyNavi/jquery.sudyNav.js"></script>
<link href="b_css/base.css" rel="stylesheet">
<link href="b_css/media.css" rel="stylesheet">
<script type="text/javascript" src="extends/extends.js"></script>
<script type="text/javascript" src="b_js/jquery.cookie.js"></script>
<script type="text/javascript" src="b_js/json2.js"></script>
   
<link rel="stylesheet" href="extends/extends.css" type="text/css" media="all" />
 <link href="/html5play/video-js.css" rel="stylesheet" type="text/css"><script src="/html5play/video.js"></script> 
 <script>     videojs.options.flash.swf = "/html5play/video-js.swf";</script>
<!--[if lt IE 9]>
	<script src="extends/libs/html5.js"></script>
	<link href="b_css/ie.css" rel="stylesheet">
<![endif]-->

<!--[if lt IE 7]>
	<script src="extends/libs/pngfix.js"></script>
	<script type="text/javascript">
		DD_belatedPNG.fix('.site-logo,.search-submit')
	</script>
<![endif]-->
</head>
<body  class="wp-main-page"  style="   >
    <form id="form1" runat="server">

    
<style>
    #fix_header {
    position: fixed;
    width: 100%;
    height: 45px;
    z-index: 9999;
    top: 0;
    left: 0;
}
.zyin_tbdh {
    background: #0073CC;
    height: 45px;
    position: fixed;
    top: 0;
    width: 100%;
}
.fr {
    float: right;
    display: inline;
}

.head-bd .person a {
    float: left;
    display: block;
    color: #fff;
    height: 45px;
    line-height: 45px;
    padding: 0 10px;
    font-size: 14px;
}
.person ul li {
    float: left;
    position: relative;
    list-style: none
}
.head-bd .person li:hover a.option_01 {
    background: #005CA3 url(../images/menu_option.png) no-repeat 1px 13px;
}
.head-bd .person li:hover a.option_02 {
    background: #005CA3 url(../images/menu_option.png) no-repeat -29px 13px;
}.head-bd .person li:hover a.option_03 {
    background: #005CA3 url(../images/menu_option.png) no-repeat -58px 13px;
}
.head-bd .person a {
    float: left;
    display: block;
    color: #fff;
    height: 45px;
    line-height: 45px;
    padding: 0 10px;
    font-size: 14px;
}
.zyin_dh {
    width: 1100px;
    margin: 0 auto;
    line-height: 45px;
    font-size: 18px;
    font-family: "Microsoft YaHei";
}
.zyin_dh .zyin_dhR {
    float: right;
    font-size: 14px;
}
.zyin_dh .zyin_dhR {
    float: right;
    font-size: 15px;
}
.zyin_dhR {
    width: 200px;
}
.person {
    float: right;
    position: relative;
    z-index: 999;
}



				.header_con a.menu_zy span {
    background-position: -98px 2px;
}
.header_con a.menu_jy span {
    background-position: -193px 2px;
}
.header_con a.menu_space span {
    background-position: -467px 2px;
}

				.zyin_dh .zyin_dhL {
    float: left;
    display: inline;
    font-size: 16px;
}
.clearfix:before, .clearfix:after {
    content: "";
    display: table;
}
.header_con {
    height: 45px;
    font-size: 14px;
    line-height: 45px;
	font: 12px/1.5 microsoft yahei ,Helvetica, Arial, sans-serif;
}
.x_link li {
    float: left;
    margin-right: 4px;
	    list-style: none;
}
.x_link a:hover, .x_link li.selected a {
    background-color: #005CA3;
    text-decoration: none;
}
.header_con a:hover, .header_con a.active {
    background: #005ca3;
    text-decoration: none;
}
.header_con a {
    color: #fff;
    height: 45px;
    display: block;
    padding: 0 17px;
    cursor: pointer;
}
.x_link a {
    display: block;
    padding: 0 20px;
}

.header_con a.menu_home span {
    background-position: -6px 2px;
}

.header_con a span {
    background: url(images/menu_bg.png) no-repeat;
    display: inline-block;
    padding-left: 25px;
}
.x_link a span {
    color: #fff;
    font-size:14px;
    display: block;
    height: 44px;
    line-height: 44px;
}

				</style>








   <!--头部开始-->
    <div class="wp-wrapper wp-header">
    	<div class="wp-inner clearfix"  style="background: url(images/logobgyx.png);    height: 109px;">
    		<!--logo开始-->
    		<div class="wp-panel logo-panel panel-1">
    			<a class="navi-aside-toggle"></a>
	    		<div class="wp-window logo-window window-1">
	    			<a href="/"></a>
	    		</div>
    		</div>
    		<!--//logo结束-->
			<!--搜索开始-->
			
<style>
    .main ul.main-nav > li {
    display: inline-block;
    margin: 0 15px;
}
@media (max-width: 1080px)
.main {
    padding: 0 20px;
}
.main 
{
    margin-top: 35px;
    padding: 0 60px;
    display: table;
    height: 90px;
    width: 100%;
    border-bottom: 1px solid #dee0df;
}
.container {
    max-width: 1080px;
    margin: 0 auto;
    margin-top: 35px;
}


.main a.brand {
    color: #e74430;
    font-size: 21px;
    float: left;
    margin-right: 30px;
}

.main a.brand img {
    position: relative;
    margin-right: 15px;
}
img {
    border: 0;
}
.responsive-sidebar-nav {
    display: none;
    float: right;
    margin-top: 25px;
    margin-left: 25px;
}


.main .switcher {
    position: relative;
    float: right;
    margin-top: 25px;
    margin-left: 25px;
}


.dropup, .dropdown {
    position: relative;
}


.main ul.main-nav {
    list-style: none;
    display: inline-block;
    margin: 0;
    padding: 0;
    float: right;
}







[class^="icon-"], [class*=" icon-"] {
    font-family: 'icomoon';
    speak: none;
    font-style: normal;
    font-weight: normal;
    font-variant: normal;
    text-transform: none;
    line-height: 1;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
}
.aw-search-box #global_search_btns {
    position: absolute;
    right: 5px;
    top: 5px;
    font-size: 18px;
    color: #999;
}
.icon-search:before {
    content: "\e604";
}
.aw-search-box #global_search_btns {
    position: absolute;
    right: 5px;
    top: 5px;
    font-size: 18px;
    color: #999;
}
.aw-search-box {
    margin: 40px 40px;
}
.aw-logo, .aw-search-box, .aw-top-nav {
    position: relative;
    float: right;
}
.aw-search-box input {
    width: 200px;
    height: 20px;
    padding-right: 30px;
    
}
.form-control {
    padding: 6px;
    resize: none;
    box-shadow: none;
    border-color: #ccc;
}
.form-control {
    display: block;
    width: 100%;
    height: 34px;
    padding: 6px 12px;
    font-size: 14px;
    line-height: 1.42857143;
    color: #555;
    background-color: #fff;
    background-image: none;
    border: 1px solid #ccc;
    border-radius: 4px;
    -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
    -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
    -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
    transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
}
</style>

			</div>
			<!--//搜索结束-->

    	</div>
    </div>
<!--//头部结束-->

<!--导航开始-->
    <nav class="wp-wrapper wp-navi">
    	<div class="wp-inner clearfix">
    		<div class="wp-panel main-nav-panel panel-5" frag="面板75">
    			<div class="wp-window main-nav-window window-5" frag="窗口75">
    				<div id="wp_nav_w75"> 

    				<div class="navi-slide-head">
		    			<h3 class="navi-slide-title">站点导航</h3>
		    			<a class="navi-slide-arrow"></a>
		    		</div>
    				
		            <ul class="wp-menu clearfix" data-nav-aside='{"title":"站点导航","index":0}' id="dhshow" runat="server">
		            	
		              
		            
		                
		            </ul>
                        <ul class="wp-menu clearfix" data-nav-aside='{"title":"服务导航","index":1}' id="downdhshow" runat="server">

                          

                            



                        </ul>
	          		 
 </div>
	          	</div>
	        </div>
    	</div>
    </nav>

    <!--aside导航-->
    <div class="wp-navi-aside" id="wp-navi-aside">
    	<div class="aside-inner">
    		<div class="navi-aside-wrap"></div>
    	</div>
    	<div class="navi-aside-mask"></div>
    </div>
<!--//导航结束-->

<div class="wp-wrapper" id="naver">
	 <div class="wp-inner">
        <div class="navbar" frag="面板14">
    	       <div class="navbox" frag="窗口14">
      	              <div id="wp_nav_w14">

                         
                          
    <ul class="wp_nav" id="menushow" runat="server" data-nav-config="{drop_v: 'down', drop_w: 'right', dir: 'y', opacity_main: '-1', opacity_sub: '-1', dWidth: '0'}">

        <li class="nav-item i1"><a href="#" target="_self" class=""><span class="item-name"></span></a><i class="mark"></i>
            <ul class="sub-nav" style="width: 0px; height: 0px; top: 46px; left: 0px; visibility: hidden;">

                <li class="nav-item i3-1 " style="display: block; width: 100%;"><a href="#" target="_self" style="display: block; width: auto;"><span class="item-name"></span></a><i class="mark"></i></li>

               

            </ul>
            

        </li>

       

    </ul>


</div>
               </div>
        </div>
        <div class="navlist">

        </div>
     </div>
</div>

<!--大图开始-->

<!--//大图结束-->
    <div class="wp-wrapper" id="container-1">
        <div class="wp-inner" frag="面板91">
            <div frag="窗口91" class="l-banner" portletmode="simpleColumnAttri">

                <!--<img src="/_upload/tpl/01/36/310/template310/images/list-bg.jpg" style="width:100%; height:100%;">-->
                <img border='0' id="lmlogo" runat="server" src='images/jsbg1.jpg' />

            </div>
        </div>
    </div>
<!--主体开始-->
<div class="wp-wrapper" id="container" style="display:none">
	 <div class="wp-inner clearfix">
          <div class="cont-2 clearfix">
                <div class="cont-2-l" frag="面板9">
                      <div class="post" frag="窗口9">
        	                <div class="tt">
          	                     <h3 class="tit"><span class="title"></span></h3>
                                 <div class="more_btn"><a href="#" class="w9_more" target="_blank"><span class="more_text"><img src="images/more.png" width="18" height="29"/></span></a></div>
                            </div>
							<div class="con">
                                <div id="wp_news_w9">
                                  
                                </div>


                                <div class="news_list zdy-2 xnxw clearfix">
                                    <div class="sudy-scroll" id="scroll-1458179148213" style="width: 766px; height: 475px;">
                                        <div class="sudy-scroll-wrap" style="width: 766px; height: 475px;">
                                        
                                        </div>
                                    </div>
                                </div>
                                                <!--<li class="news n{序号值} clearfix">
                                                      <div class="slt">{缩略图}</div>
                                                      <div class="bt">{标题}</div>
													  <div class="time">{发布时间}</div>
                                                      <div class="nr">{简介}</div>
                                                </li>-->  
                                  
                            </div>
                      </div>
                </div>
				
                <div class="cont-2-r" frag="面板8">
                      <div class="post post-8" frag="窗口8">
        	                <div class="tt">
          	                     <h3 class="tit"><span class="title" frag="标题"></span></h3>
                                 <div class="more_btn" frag="按钮" type="更多"><a href="/20/list.htm" class="w8_more"><span class="more_text" frag="按钮内容"><img src="images/more.png" width="18" height="29"/></span></a></div>
                            </div>
                             <div class="con">
                                 <div id="wp_news_w8"> 

                              
                                 </div> 

                            </div>
                      </div>
                </div>
               
          </div>
		  <div class="clear"></div>
		  
    <br />

     </div>
</div>
<!--//主体结束-->
    <div class="wp-wrapper wp-container list-container">
        <div class="wp-inner clearfix" style="background-color:#edeadf;">
            <div class="wp-column-menu">
                <div class="column-head" frag="面板86">
                    <div class="column-anchor clearfix" frag="窗口86" portletmode="simpleColumnAnchor">
                        <h3 class="anchor-title"><span class='Column_Anchor' id="lmmc" runat="server"></span></h3>
                        <a class="column-switch"></a>
                    </div>
                </div>
                <div class="column-body" frag="面板87">
                    <div class="column-list" frag="窗口87" portletmode="simpleColumnList">
                        <div id="wp_listcolumn_w87" runat="server">


                       


                        </div>
                    </div>
                </div>
                <div class="acon acon1" frag="面板85">
                    <div class="sudy-select" frag="窗口851">
                        <div id="wp_nav_w851" style="display:none;">

                            <span class="select-name select-open"></span>

                          

                        </div>
                    </div>
                </div>
            </div>

            <div class="wp-column-news">
                <div class="column-news-box">
                    <div class="list-head" frag="面板88">
                        <div class="list-meta clearfix" frag="窗口88" portletmode="simpleColumnAttri">

                            <h2 class="column-title" id="lmmc2" runat="server">无数据</h2>
                            <div class="column-path" id="lmdh" runat="server"><span class="path-name">当前位置：</span><a href="/" target="_self">首页</a><span class='possplit'>&nbsp;&nbsp;</span><a href="#" target="_self">未找到栏目数据</a><span class='possplit'>&nbsp;&nbsp;</span><a href="#" target="_self"></a></div>

                        </div>
                    </div>

                    <div class="column-news-con" frag="面板89">
                        <div class="column-news-list clearfix" frag="窗口89" portletmode="simpleList">

                            <div class="wp_single wp_column_article" id="wp_column_article">
                                <div class="wp_entry">
                                     <style>
                    .title{clear:both; height:30px;font-size:24px;color:#900; font-weight:bold; text-align:center; line-height:36px; border-bottom:#ddd solid 2px; margin:0 40px; padding:20px 0 10px 0;overflow:hidden;}
.table {width:98%; margin-top:10px; border:0px solid #C0C0C0; border-collapse:collapse;padding:20px; font-size:16px;color:#828282}
.table td{ border-bottom:1px solid #ccc;padding:4px;border-top:1px solid #f8f8f9;border-left:1px solid #f8f8f9;border-right:1px solid #f8f8f9; }
.table th{ border-bottom:1px solid #ccc;padding:4px;border-top:1px solid #f8f8f9;border-left:1px solid #f8f8f9;border-right:1px solid #f8f8f9; }

.table .hover {background:#E0F0FF;}
.table .click {background:#FBE9EA;}
.table .focus {background:#D3EAFF; line-height:40px; color:#ccc;}
.paging_content img
{
    padding-top:20px;
    padding-bottom:5px;
    display:block;
    margin:0 auto;
    max-width:860px;
}
.paging_content object
{
    padding-top:20px;
    padding-bottom:5px;
    padding-right:30px;
    display:block;
    margin:0 auto;
    max-width:860px;
}
@media only screen and (max-width:500px)
{
.noshowok
{
    display:none;
}
}
.borderSolid1CCC
{
    height:16px;
    width:32px;
}
.leftok
{
    text-align:left;
}

                </style>
                                    <div id="wp_content_w89_0" class="paging_content" style="width:92%;display:" runat="server">
                                        <p style="text-align:justify;text-indent:37px;font:12px/28px simsun;letter-spacing:normal;color:#4b4b4b;word-spacing:0px;font-size-adjust:none;font-stretch:normal;-webkit-text-stroke-width:0px">
                                        </p>
                                    </div>


                                    <style>
                                                           
div,h3, p {
    border: 0px;
    padding: 0px;
    /* font-size: 100%; */
    margin: 0px;
    font-family: inherit;
    -webkit-font-smoothing: subpixel-antialiased;
    font-weight: normal;
}
.cover p,.cover h3{transition: height 0.3s, transform 0.3s; -moz-transition: height 0.3s, -moz-transform 0.3s;-webkit-transition: height 0.3s, -webkit-transform 0.3s; -o-transition:  height 0.3s,-o-transform 0.3s;}

.cover p,.cover h3{height: 0;overflow: hidden;}

/*.conR dd:hover .cover,.ztw li:hover .cover,.fw_wap li:hover .cover{display: block;}*/
/*.conR dd:hover .cover p,.ztw li:hover .cover p,.conR dd:hover .cover h3,.ztw li:hover .cover h3,.fw_wap li:hover .cover p,.fw_wap li:hover .cover h3{height: 28px;}*/
.conR dd:hover .cover p,.ztw li:hover .cover p,.conR dd:hover .cover h3,.ztw li:hover .cover h3,.fw_wap li:hover .cover p,.fw_wap li:hover .cover h3{height: 28px;}

.cover a { width: 130px; text-align: center; }


.conR{width: 402px;float: left;margin:5px 0 0 30px;overflow: hidden;}
.conR dl{overflow: hidden;margin: 0px 0;background: #f4f4f4;*margin: 0px 0;-webkit-margin-before: 0em;
    -webkit-margin-after: 0em;}
.conR dt,.ztw dt{width: 94px;border: 1px solid #c9c9c9;border-right: none;float: left;height: 125px;background: #f4f4f4;text-align: center;padding-top: 48px;}
.conR dt span,.ztw dt span{display: block;width: 100%;font-size: 15px;padding-top: 6px;}
.conR dd{/*border-top:2px solid #8c0000;*/height: 173px;float: left;width: 307px;position: relative;overflow: hidden;}
.conR dd a h3{display:none;} /*For V9.1*/
.conR dd .img,.fw_wap li .img{width: 100%;}
.conR dl.last dd img{margin-top:15px;margin-left:15px;}
.conR dd p,.ztw dd p{position: absolute;/*height: 28px;*/background: #000;bottom: 0;width: 100%;opacity: 0.5;filter:alpha(opacity=50);z-index: 1;left: 0;}
.conR dd h3,.ztw dd h3{position: absolute;bottom: 0; width:100%;/*height: 28px;*/line-height: 28px;text-align: center;z-index: 2;left: 0;font-size: 14px;color: #fff;}



 .ztw{width: 100%;overflow: hidden;padding-top: 0px;}
.ztw dl{background: #f4f4f4;overflow: hidden;width: 100%;}
.ztw dt{padding-top: 21px;}
.ztw dd{/*border-top:2px solid #8c0000;*/float: left;width: 100%;}
.ztw dd ul{overflow: hidden;}
.ztw dd li{float: left;position: relative;overflow: hidden;display: inline-block; width:263px;margin-left:10px;margin-bottom:10px;}
.ztw dd li img{width:263px;}
.conR dd h3 a,.ztw dd h3 a{color: #fff;display: block;height:28px;line-height: 28px;}
.ztw dd a h3{display:none;} /*For V9.1*/
dd,ul{
-webkit-margin-start: 0px;
-webkit-padding-start: 0px;
}
img
{
    border:0px;
    image-border:0px;
}
    </style>
    <style>
        .inner_page_title {
    position: relative;
    height: 40px;
    background-color: #f2f2f4;
}
        .page_path {
    text-align: right;
    position: absolute;
    z-index: 1;
    top: 15px;
    right: 0px;
        font-size: 14px;
}
.inner_page_title_text {
    display: inline-block;
    position: absolute;
    z-index: 1;
    bottom: 0px;
    left: 0px;
    padding-right: 2px;
    
}
.inner_page_title_text span {
    display: inline-block;
    color: #555555;
    font-size: 16px;
    font-family:'Microsoft YaHei';
    line-height: 38px;
    /* padding: 0 2px; */
    padding: 0 30px 0 0;
    border-bottom: 3px solid #3a87d3;
}
.inner_page_title_border {
    width: 100%;
    height: 1px;
    border-bottom: 3px solid #003d7d;
    position: absolute;
    z-index: 0;
    bottom: 0px;
    right: 0px;
}
    </style>










                                    <div id="wp_content_w89_1" class="paging_content" style="display:none" runat="server">
                                    <asp:HiddenField ID="hdfWPBH" runat="server" />
      <asp:Label ID="Label1" runat="server"></asp:Label>
                                        <asp:GridView  OnRowCommand="GridView1_RowCommand"  ID="GridView1"  OnDataBound="GridView1_DataBound"  runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" CssClass="table"  PageSize="15"
            EmptyDataText="该栏目下还没有新闻!" 
            AllowPaging="True" AllowSorting="True">
    <Columns>
   
  <asp:TemplateField HeaderText=""  SortExpression="title">
        
            <ItemTemplate>
            <span style="background:url(images/arrow.gif) no-repeat center 6px;display:inline-block;width:10px;height:18px;margin-top:1px;font-size:11px;float:left;text-indent:-99em;overflow:hidden;margin-right:5px;">1</sapn>
            </ItemTemplate>

            <ItemStyle Height="28px" Width="2%" />
            </asp:TemplateField>
     <asp:TemplateField HeaderText="标题"  HeaderStyle-CssClass="leftok"  SortExpression="title">
        
            <ItemTemplate>
            <a href="xw.aspx?xwid=<%# Eval("id").ToString() %>"  target="_blank"  title="点击查看新闻内容信息:<%# xwtitle(Eval("title").ToString(),"all") %>"><%# xwtitle(Eval("title").ToString(),"") %><%# imagestu(Eval("images").ToString()) %></a>
            </ItemTemplate>
             <ItemStyle Height="28px" Width="78%" />
           
            </asp:TemplateField>
   
  
 
    <%--<asp:BoundField DataField="titleurl" HeaderText="标题"  HtmlEncode="false" HtmlEncodeFormatString="false" />--%>
        <asp:BoundField DataField="fabutime"   HeaderStyle-CssClass="noshowok" ItemStyle-CssClass="noshowok"  HeaderText="发布时间" DataFormatString="{0:yyyy年M月dd日}" 
            SortExpression="fabutime" />
        
     


    </Columns>
    <PagerTemplate>
<span style="float:left;height:40px;margin-top:10px;">


            每页<asp:Label ID="LabelPageSize" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageSize %>"></asp:Label>
            条 当前<asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex+1 %>"></asp:Label>
            /<asp:Label ID="Label3" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
            页&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First"
                CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=0 %>">首页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=0 %>">上一页</asp:LinkButton>
                <%if (GridView1.PageCount >= 8 && GridView1.PageCount - GridView1.PageIndex >= 8)
                  {
                     
                       %>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+2 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+2 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+3 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+3 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+4 %></asp:LinkButton>
               <asp:LinkButton ID="LinkButton5" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+5 %>"
                CommandName="Page"><%#  ((GridView)Container.NamingContainer).PageIndex+6 %></asp:LinkButton> 
                <asp:LinkButton ID="LinkButton6" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+6 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+6 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton7" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+7 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+7 %></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton9" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+8 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+8 %></asp:LinkButton>

                <%}
                  else if (GridView1.PageCount >= 8 && GridView1.PageCount - GridView1.PageIndex >= 5)
                  { %>
                    <asp:LinkButton ID="LinkButton8" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+2%>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+2 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton10" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+3 %>" CommandName="Page"> <%#  ((GridView)Container.NamingContainer).PageIndex+3 %></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton11" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+4 %></asp:LinkButton>
                    <%}%>

            <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">下一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">尾页</asp:LinkButton>
           </span><span class="noshowok" style="float:right;height:40px;margin-top:10px;">转到<asp:TextBox ID="txt_go" runat="server" Height="16px" Width="32px" CssClass="borderSolid1CCC"></asp:TextBox>

            <asp:LinkButton ID="LinkButtonGo" runat="server"  Text="跳转" OnClick="LinkButtonGo_Click" />&nbsp;&nbsp;&nbsp;<b>新闻数:<%#ViewState["count"].ToString()%>条</b>&nbsp;</b></font>&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass="borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /></span>
        </PagerTemplate>
    </asp:GridView>
        <asp:SqlDataSource onselected="SqlDataSource1_Selected"   ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
            
                                            SelectCommand="SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc, xw_neirong.title, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.isyn,xw_neirong.id,xw_lanm.glqx FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where  isyn='1' and xw_lanm.lmid=@lmid order by fabutime desc">
            <SelectParameters>
                <asp:QueryStringParameter Name="lmid" QueryStringField="id" />
            </SelectParameters>
        </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="clear"></div>

<!--底部开始-->
 
    <style>
        #footer > .inner {
	padding: 0px;
}
#footer > .inner {
	padding: 15px;
    display: inline-block;
    /*width:900px;*/
}

        #footer {
	color:#555555; font-size: 11px; position: relative; background-color: #EFEEEE;
}
#footer h2 {
	visibility: hidden;
}
        #footer a:hover {
	color: #555555; text-decoration: underline;
}
#footer ul.menu {
	list-style: none; margin: 4px 0px 0px; padding: 0px;
}
#footer ul.menu li {
	list-style: none; margin: 0px 8px 0px 0px; padding: 0px 8px 0px 0px; line-height: 1.2em; border-right-color: rgb(255, 255, 255); border-right-width: 1px; border-right-style: solid; float: left;
}
#footer ul.menu li:first-child {
	padding-left: 0px;
}
#footer ul.menu li:last-child {
	border: currentColor; border-image: none;
}
#footer p {
	margin: 8px 0px 0px; line-height: 20px;color:#555555;
}
#footer a {
	color:#555555;
}
#footer p.give-to-mit {
	right: 0px; bottom: 15px;top:15px; display: inline-block; position: absolute;
}
#footer p.address {
	float: left;
}
#footer p.follow-us {
	float: left;
}
#footer p.follow-us {
	width: 200px; padding-left: 2px;
}
#footer p span.text-block {
	padding: 0px 8px 0px 6px; color:#555555; border-right-width: 1px; 
}
#footer p span.first.text-block {
	padding-left: 0px;
}
#footer p span.last.text-block {
	border: currentColor; border-image: none;
}
#footer p.follow-us .text-block {
	padding-right: 2px; float: left;
}


    </style>
    <div id="footer-wrapper" class="wp-foot-top">
        <div id="footer">
            <div class="inner">

                <ul class="menu" id="downshow"  runat="server">

                   
                </ul>
                <!--[if lt IE 7]>      <br /> <![endif]-->
                <!--[if IE 7]>         <br /> <![endif]-->
                <!--[if IE 8]>         <br /> <![endif]-->
                <h2>联系说明</h2>

                <p class="address clearfix" id="downlx" runat="server">
                    
                </p>

               
            </div>
        </div>
    </div>
<!--//底部结束-->
    </form>
       <script type="text/javascript" src="_js/app.js"></script>
<script type="text/javascript">
    $(function () {
        // 初始化SDAPP
        new SDAPP({
            "menu": {
                type: "aside,slide"
            }
        });
    });
</script>
</body>
   
</html>

<%@ page language="C#" autoeventwireup="true"  CodeFile="xw.aspx.cs" Inherits="wblue_xw"  %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
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


<link href="/html5play/video-js.css" rel="stylesheet" type="text/css"><script src="/html5play/video.js"></script> 
 <script> videojs.options.flash.swf = "/html5play/video-js.swf";</script>

</head>
<body  class="wp-main-page"  >
    <form id="form1" runat="server">
    





  



  <!--头部开始-->
    <div class="wp-wrapper wp-header">
    	<div class="wp-inner clearfix" style="background: url(images/logobgyx.png);    height: 110px;">
    		<!--logo开始-->
    		<div class="wp-panel logo-panel panel-1">
    			<a class="navi-aside-toggle"></a>
	    		<div class="wp-window logo-window window-1">
	    			<a href="/"></a>
	    		</div>
    		</div>
    		<!--//logo结束-->
			<!--搜索开始-->
			<div class="wp-panel search-panel panel-3" frag="面板3">
			   <div class="aw-search-box  hidden-xs hidden-sm">
				
					<input class="form-control search-query" type="text" placeholder="搜索新闻，输入关键字" autocomplete="off" name="word" id="word">
					<span title="搜索" id="global_search_btns" onClick="location.href='/serch.aspx?word='+ document.getElementById('word').value"><i class="fa fa-search"></i></span>
					<div class="aw-dropdown" style="display: none;">
						<div class="mod-body">
							<p class="title">输入关键字进行搜索</p>
							<ul class="aw-dropdown-list hide"></ul>
							<p class="search" style="display: block;"><span>搜索:</span><a onClick="location.href='/serch.aspx?word='+ document.getElementById('word').value"></a></p>
						</div>
						
					</div>
				
			</div>
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
    width: 20px;
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

                           


                        </div>
                    </div>
                </div>
            </div>

            <div class="wp-column-news">
                <div class="column-news-box">
                    <div class="list-head" frag="面板88">
                        <div class="list-meta clearfix" frag="窗口88" portletmode="simpleColumnAttri">

                            <h2 class="column-title" id="lmmc2" runat="server"></h2>
                            <div class="column-path" id="lmdh" runat="server"><span class="path-name">当前位置：</span><a href="/" target="_self">首页</a><span class='possplit'>&nbsp;&nbsp;</span><a href="#" target="_self">未找到数据</a><span class='possplit'>&nbsp;&nbsp;</span><a href="#" target="_self"></a></div>

                        </div>
                    </div>

                    <div class="column-news-con" frag="面板89">
                        <div class="column-news-list clearfix" frag="窗口89" portletmode="simpleList">

                            <div class="wp_single wp_column_article" id="wp_column_article">
                                <div class="wp_entry">
                                     <style>
                    .title{clear:both; /*height:30px;*/font-size:22px;color:#900; font-weight:bold; text-align:center; line-height:36px; border-bottom:#ddd solid 2px; /*margin:0 40px;*/ padding:20px 0 10px 0;/*overflow:hidden;*/}
.labler{height:36px; padding:5px 30px; line-height:36px;text-align:right;}
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
@media screen and (max-width: 467px)
{
    .paging_content img
{
    max-width:300px;
    max-height:200px;
}
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
.paging_content p div{
    margin-left:0px;
    margin:0 auto;
}
                </style>
                                    <div id="wp_content_w89_0" class="paging_content" style="display:;width:92%;" runat="server">
                          <div class="labler"><span> </span></div>              
<p style="text-align:justify;text-indent:37px;font:12px/28px simsun;letter-spacing:normal;color:#4b4b4b;word-spacing:0px;font-size-adjust:none;font-stretch:normal;-webkit-text-stroke-width:0px">
                                        </p>
                                    </div>
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
     <asp:TemplateField HeaderText="标题"  SortExpression="title">
        
            <ItemTemplate>
            <a href="xw.aspx?xwid=<%# Eval("id").ToString() %>"  target="_blank"  title="点击查看新闻内容信息:<%# xwtitle(Eval("title").ToString(),"all") %>"><%# xwtitle(Eval("title").ToString(),"") %><%# imagestu(Eval("images").ToString()) %></a>
            </ItemTemplate>
             <ItemStyle Height="28px" Width="78%" />
           
            </asp:TemplateField>
   
  
 
    <%--<asp:BoundField DataField="titleurl" HeaderText="标题"  HtmlEncode="false" HtmlEncodeFormatString="false" />--%>
        <asp:BoundField DataField="fabutime" HeaderText="发布时间" 
            SortExpression="fabutime" />
        
     


    </Columns>
    <PagerTemplate>
<span style="float:left;">


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
            &nbsp;&nbsp;&nbsp;&nbsp;转到<asp:TextBox ID="txt_go" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>

            <asp:LinkButton ID="LinkButtonGo" runat="server"  Text="跳转" OnClick="LinkButtonGo_Click" /></span><span style="float:right;"><b>新闻数:<%#ViewState["count"].ToString()%>条</b>&nbsp;</b></font>&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  Text="设置"   OnClick="PageSize_Go" /></span>
        </PagerTemplate>
    </asp:GridView>
        <asp:SqlDataSource onselected="SqlDataSource1_Selected"   ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
            
                                            SelectCommand="SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc, xw_neirong.title, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.isyn,xw_neirong.id,xw_lanm.glqx FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where xw_lanm.lmid=@lmid">
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


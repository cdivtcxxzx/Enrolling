<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html lang="zh-CN"><head id="headt" runat=server><meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
   
    <title></title>
    <meta name="description" content="">
    <meta name="keywords" content="">


    <meta name="HandheldFriendly" content="True">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <link rel="shortcut icon" href="###assets/favicon.ico">

    <link rel="stylesheet" href="./bootstrap/3.3.4/css/bootstrap.min.css">
    <link rel="stylesheet" href="./font-awesome/css/font-awesome.min.css">
    <link rel="stylesheet" href="./b_css/vs.min.css">
    <link rel="stylesheet" type="text/css" href="./b_css/screen.css">
	 <link rel="stylesheet" href="./bootstrap/jquery.slideBox.css">
     <link rel="stylesheet" href="b_css/login.css">
	   <script type="text/javascript" src="//cdn.bootcss.com/respond.js/1.4.2/respond.min.js"></script>
    <script>
        var _hmt = _hmt || [];
    </script>

    <link rel="canonical" href="###">
    <meta name="referrer" content="origin">
    <link rel="next" href="###page/2/">
   
    <meta name="generator" content="Ghost 0.7">
    
<style id="fit-vids-style">.fluid-width-video-wrapper{width:100%;position:relative;padding:0;}.fluid-width-video-wrapper iframe,.fluid-width-video-wrapper object,.fluid-width-video-wrapper embed {position:absolute;top:0;left:0;width:100%;height:100%;}</style></head>
<body runat="server" class="home-template"  >


<style>
    #logook
    {
        background:url(images/logobgyx.png);height:100px
    }
    @media (max-width: 550px) 
    {
        .main-navigation{ display: none;}
       
       #logook
    {
        background:url(images/logobgyxs550.png)!important;height:100px
    }  
    }
    @media (max-width: 400px) 
    {
       #logook
    {
        background:url(images/logobgyxs.png)!important;height:100px
    }  
    }
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







    <!-- start header -->
   
    <div class="main">
        <div class="container" id="logook" style="">
          <a href="#" class="brand"></a>
          

            <div class="switcher">
                <div class="dropdown">
                   
                </div>
            </div>

           
<style>
    .main ul.main-nav > li {
    display: inline-block;
    margin: 0 15px;
}

    @media (max-width: 767px)
    {
.main-navigation {
    display:none;
}

    }


    @media (max-width: 500px)
    {
        #demo1{width:100%!important;}
        #demo1 img{width:100%!important;}
        #demo1 li{width:100%!important;}


    }

    
@media (max-width: 1080px)
.main {
    padding: 0 20px;
}
.main {
    padding: 0 30px;
    display: table;
    height: 90px;
    width: 100%;
    border-bottom: 1px solid #dee0df;
    <%--margin-top: 35px;--%>
}
.container {
    max-width: 1080px;
    margin: 0 auto;
    
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
    margin: 40px 0;
}
.aw-logo, .aw-search-box, .aw-top-nav {
    position: relative;
    float: right;
}
.aw-search-box input {
    width: 260px;
    height: 32px;
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
    </div>

    <!-- end header -->

    <!-- start navigation -->
    <div class="main-navigation">
        <div class="container">
            <div class="row">
                <div class="col-sm-12">
                    <div class="navbar-header">
                        <span class="nav-toggle-button collapsed" data-toggle="collapse" data-target="#main-menu" aria-expanded="false">
                        <span class="sr-only"></span>
                        <i class="fa fa-bars"></i>
                        </span><div class="aw-search-box  hidden-xe" style="margin: 10px 0;">
				<form class="navbar-search" action="/search/" id="global_search_form" method="post">
					<input class="form-control search-query" type="text" placeholder="搜索新闻，输入关键字" autocomplete="off" name="aw-search-query" id="aw-search-query">
					<span title="搜索" id="global_search_btns" onClick="location.href='/serch.aspx?word='+ document.getElementById('aw-search-query').value"><c class="fa fa-search"></c></span>
					<div class="aw-dropdown" style="display: none;">
						<div class="mod-body">
							<p class="title">输入关键字进行搜索</p>
							<ul class="aw-dropdown-list hide"></ul>
							<p class="search" style="display: block;"><span>搜索:</span><a onClick="location.href='/serch.aspx?word='+ document.getElementById('word').value"></a></p>
						</div>
						
					</div>
				</form>
			</div>
                    </div>
                    <div class="navbar-collapse collapse" id="main-menu" aria-expanded="false" style="height: 1px;">
                        <ul class="menu" id="menushow"  runat="server">
       
</ul>   
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end navigation -->
	 <script src="./b_js/jquery.min2.js"></script>
<script src="./b_js/jquery.slideBox.min.js" type="text/javascript"></script>
   <script src="./bootstrap/3.3.4/js/bootstrap.min.js"></script>

	<script>
	    jQuery(function ($) {
	        $('#demo1').slideBox(
    {
        duration: 0.5, //滚动持续时间，单位：秒
        easing: 'linear', //swing,linear//滚动特效
        delay: 15, //滚动延迟时间，单位：秒
        hideClickBar: false, //不自动隐藏点选按键
        startIndex: 0//初始焦点顺序
    }
    );

	    });
</script>

    <!-- start site's main content area -->
    <div class="content-wrap" style="background: #f5f5f5;">
        <div class="container">
            <div class="row">

                <main class="col-md-8 main-content">
                  <article id="70" class="post">
                    
                    <div class="featured-media"><!-- 轮播处理 -->
<div id="demo1" class="slideBox"  style="width:100%; height: 100%;border:0px solid #ccc">
  <ul class="items" id="tpshow" runat="server">
   <li  style="width: 100%; height: 100%;" onclick="location.href='/login.aspx?url=/nradmingl/defaultxs.aspx&sf=xs'"><a href="/login.aspx?url=/nradmingl/defaultxs.aspx&sf=xs" title="新生自助报到"><img style="width:auto;height:auto;"  src="img/img1.PNG" /></a></li>
   <li  style="width: 100%;height: 100%;" onclick="location.href='/login.aspx?url=/nradmingl/defaultxs.aspx&sf=xs'"><a href="/login.aspx?url=/nradmingl/defaultxs.aspx&sf=xs" title="新生资助政策"><img style="width:auto;height:auto;"   src="img/img2.PNG"   /></a></li>
   <li  style="width: 100%;height: 100%;" onclick="location.href='/login.aspx?url=/nradmingl/defaultxs.aspx&sf=xs'"><a href="/login.aspx?url=/nradmingl/defaultxs.aspx&sf=xs" title="到校交通路线查询"><img style="width:auto;height:auto;"   src="img/img3.PNG"  /></a></li>
  </ul>
</div>
<!-- 轮播处理结束 --></div>
                    <div class="post-content">
                      <p id="rdxw" runat="server"><b><a href="/login.aspx?url=/nradmingl/defaultxs.aspx&sf=xs">【新生网上报到说明】</a></b><b>此处放置简要报到流程</b><br /><br /><br /><br /><br /><br /><br /></p><p style="text-align:right"></p>
                    </div>
                    <div class="post-permalink" id="read1" style="float: right;    margin-bottom: 10px;    margin-right: 10px;" runat="server"> <a href="#" class="btn btn-default">查看详细报到流程</a> </div>
                  </article>
              </main>

                <aside class="col-md-4 sidebar">
                <!-- start widget -->
<!-- end widget -->	

<!-- start tag cloud widget -->
<div class="widget" >
	<h4 class="title" id="tztitle" runat="server"><span style="float:right;" ><p><a href="/list.aspx?id=12">更多</a></p></span>通知公告</h4>
	<div class="content community" id="tzlist" runat="server">
		<p>暂无内容</p>
		
		
	</div>
</div>
<!-- end tag cloud widget -->	




<!-- start tag cloud widget -->
<div class="widget">
	<h4 class="title" id="mydivtitle" runat="server"><span style="float:right;font-size:15px"><a href="/list.aspx?id=88">>></a></span>新生网上报到登陆</h4>
	<div class="content tag-cloud" id="mydiv" style="overflow:hidden; height:220px;" runat="server" >
	<div style=" text-align:center;">
              <div class="login_area">
              <div id="login-main-form">
                  <div class="loginbox c_b">
                    <!-- 输入框 -->
                    <div class="lgn_inputbg c_b" style="text-align:left">
                        <!--验证用户名--><div class="single_imgarea" id="account-info">
                        <div class="na-img-area" id="account-avator" style="display:none">
                          <div class="na-img-bg-area" id="account-avator-con"></div>
                        </div>
                 <br />
                           
                        <p class="us_id"></p>
                      </div>
                      <label id="region-code" class="labelbox login_user c_b" for="">
                        <div class="turn_area"><a class="btn_turn" id="manual_code" href="javascript:void(0);" title="关闭国家码"></a></div>
                        <div class="country_list">
                          <div class="animation countrycode_selector" id="countrycode_selector">
                            <span class="country_code"><tt class="countrycode-value" id="countrycode_value"></tt><i class="icon_arrow_down"></i></span>
                          </div>
                        </div>
                        <input name="txt_name" type="text" id="txt_name" class="item_account" autocomplete="off" placeholder="请输入高考报名号">
                      </label>
                      <div class="country-container" id="countrycode_container" style="display: none;">
                        <div class="country_container_con" id="countrycode_container_con"></div>
                      </div>
                      <label class="labelbox pwd_panel c_b">
                        <div class="eye_panel pwd-visiable">
                          <i class="eye pwd-eye">
                          <svg width="100%" height="100%" version="1.1" xmlns="http://www.w3.org/2000/svg">
                            <path class="eye_outer" d="M0 8 C6 0,14 0,20 8, 14 16,6 16, 0 8 z"></path>
                            <circle class="eye_inner" cx="10" cy="8" r="3"></circle>
                          </svg>
                          </i>
                        </div>
                        <input name="txt_pwd" type="password" id="txt_pwd" placeholder="默认密码为身份证后六位" autocomplete="off">

                        &nbsp;<input type="text" placeholder="请输入密码" autocomplete="off" id="visiablePwd" style="display:none">
                      </label>
                    </div>
                    <div class="security_Controller" style="display: none;">
                      <label class="checkbox_area"><input type="checkbox" id="trustSecurityController" class="checkbox">使用安全控件</label>
                    </div>
                    <div class="lgncode" id="captcha">
                    </div>
                    <!-- 错误信息 -->
                    <div class="err_tip" id="error-outcon">
                      <div class="dis_box"><em class="icon_error"></em><span class="error-con"></span></div>
                    </div>
                    <!-- 登录频繁 -->
                    <div id="error-forbidden" class="err_forbidden">您的操作频率过快，请稍后再试。</div>
                    <div class="btns_bg">
                      <input type="submit" name="Button1" value="立即登录" id="Button1" class="btnadpt btn_orange" style="border-radius: 5px;">
                      <span id="custom_display_8" class="sns-default-container sns_default_container" style="display: none;">
                      </span>
                      <span id="Label1" style="color:#CC0000;font-size:Medium;text-decoration:none;"></span>
                        <a class="outer-link" href="view/xsxx_pwd_reset.aspx">忘记密码？</a>
                    </div>
                    <!-- 其他登录方式 s -->
                    <div style="display: none;" class="other_login_type sns-login-container" id="custom_display_16">
                      <fieldset class="oth_type_tit">
                        <legend align="center" class="oth_type_txt">其他方式登录</legend>
                      </fieldset>
                      <div class="oth_type_links">
                        <a class="icon_type btn_qq sns-login-link" data-type="qq" href="#" title="QQ登录" target="_blank"><i class="btn_sns_icontype icon_default_qq"></i></a>
                       
                        <a class="icon_type btn_alipay sns-login-link" data-type="alipay" href="#" title="支付宝登录" target="_blank"><i class="btn_sns_icontype icon_default_alipay"></i></a>
                        <a class="icon_type btn_weixin sns-login-link" data-type="weixin" href="#" title="微信登录" style="display: "><i class="btn_sns_icontype icon_default_weixin"></i></a>
                      </div>
                    </div>
                  </div>
               
              </div>
              </div>
            </div>

	
	</div>
	<style>.login_area {
    width: 358px;
    margin: 0 auto;
    padding-bottom: 20px;
}
.err_tip {
    margin-bottom: 5px;
    line-height: 20px;
    color: #f56700;
    display: none;
}
input[type="text"]{
	        border: 0px solid #ebebeb;
	    }
.login_area{width:268px;}
.labelbox input {width:266px;height:43px;}

	</style>
</div>
<!-- end tag cloud widget -->	


<!-- start widget -->
<!-- end widget -->	

<!-- start widget -->
<!-- end widget -->                </aside>

          </div>
        </div>
    </div>

    
    <div class="copyright">
	
	
	
	
        <div class="container">
		
		
	
		<style>

.zyin_flink div {
    display: inline;
    float: left;
    width: 890px;
    margin-left: 30px;
}
.zyin_flink div strong {
    font-weight: normal;
    margin-right: 10px;
    display: inline;
    float: left;
    font-size: 14px;
    color: #256ccb;
    paddin
	g-top: 4px;
}
.zyin_flink dd span a {
    color: #787878;
    white-space: nowrap;
}
.zyin_link p a {
    color: #787878;
    display: inline-block;
    font-size: 12px;
    margin-right: 0px;
    padding-bottom: 10px;
	text-decoration:none
}
.zyin_link p a:hover {
    color: #256ccb;

	text-decoration:none
}
.zyin_flink div span {
    display: inline;
    float: left;
    margin-right: 25px;
}
.zyin_link span {
    color: #077cc1;
float: left;
text-align:left;
}
.zyin_link a img {
    display: inline;
    float: left;
    margin-right: 16px;
}
</style>

<div style="display: inline;    float: left;    width: 100%;    margin-left: 30px;" id="footshow" runat="server">
					<div class="zyin_link">
						<strong style="font-weight: normal;margin-right: 10px;display: inline;float: left;    font-size: 14px;    color:#256ccb; padding-top: 4px;">友情链接：</strong>
						<p style="    display: block;float: left;width: 90%;line-height: 30px; word-break: break-all;text-align:left;">
							</p>
						<div class="clear"></div>
					</div>
					<div class="zyin_link" style="margin-top:10px;">
						<strong  style="font-weight: normal;margin-right: 10px;display: inline;float: left;    font-size: 14px;    color:#256ccb; padding-top: 4px;">导航链接：</strong>
						<p  style="    display: block;float: left;width: 90%;line-height: 30px; word-break: break-all;text-decoration:none">
							
						</p>
						<div class="clear"></div>
					</div>
				</div>
	
		
		
		
            <div class="row">
                <div class="col-sm-12" id="copyrights" runat="server">
                    </div>
            </div>
        </div>
    </div>

    <a href="####" id="back-to-top" style="display: none;"><i class="fa fa-angle-up"></i></a>

   
   
    

   

  



</body></html>
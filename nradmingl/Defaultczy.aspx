<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Defaultczy.aspx.cs" Inherits="nradmingl_Defaultczy" %>

<!DOCTYPE html>

<html>
<head runat="server">   
    <meta charset="utf-8">
		<title>后台管理</title>
		<meta name="renderer" content="webkit">
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
		<meta name="apple-mobile-web-app-status-bar-style" content="black">
		<meta name="apple-mobile-web-app-capable" content="yes">
		<meta name="format-detection" content="telephone=no">

		<link rel="stylesheet" href="plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="plugins/global.css" media="all">
		<link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
</head>
<body>
   <form id="form1" runat="server">
    <div class="layui-layout layui-layout-admin">
			<div class="layui-header header header-demo">
				<div class="layui-main">
					<div class="admin-login-box">
						<a class="logo"  href="/">
							<span style="font-size: 22px;">迎新管理系统</span>
						</a>
						<div title="左侧导航菜单显示/隐藏" class="admin-side-toggle  hidden-xs">
							<i class="fa fa-chevron-left" aria-hidden="true"></i>
						</div>
                        
					</div>
                     
					<ul class="layui-nav">
                    <li class="layui-nav-item" id="tsxx" runat="server"> </li>
						
						<li class="layui-nav-item  hidden-xs">
							<a href="/default.aspx"><i class="fa fa fa-home" aria-hidden="true"  style="margin-right:2px;"></i>返回首页</a>
						</li>
                        
						
						<li class="layui-nav-item">
							<a href="javascript:;" class="admin-header-user">
								<img src="../img/user2-160x160.jpg" style="width: 40px; height: 40px; border-radius: 100%;" />
                                
								<span  id="username" runat="server">用户名</span>
                               
							</a>
							<dl class="layui-nav-child dda">
								<dd>
									<a href="/zyzx/grset.aspx"><i class="fa fa-user-circle" aria-hidden="true"></i> 个人信息</a>
								</dd>
								<dd>
									<a href="/zyzx/grset.aspx"><i class="fa fa-gear" aria-hidden="true"></i> 个人设置</a>
								</dd>
								<dd>
									<a href="/logout.aspx"><i class="fa fa-sign-out" aria-hidden="true"></i> 注销退出</a>
								</dd>
                                
							</dl>
						</li>
                        <li class="layui-nav-item   hidden-xs"><a href="javascript:" class="admin-side-full" style="margin-right: -10px;margin-left: -10px;" title="全屏"  >
					<i class="fa fa-arrows-alt" aria-hidden="true"></i>
				</a></li>
					</ul>
                     
				</div>
			</div>
			<div class="layui-side layui-bg-black" id="admin-side">
				<div class="layui-side-scroll" id="admin-navbar-side" lay-filter="side">
                    <ul class="layui-nav layui-nav-tree">
                        <li class="layui-nav-item"><a href="#">导航1</a></li>
                        

                    </ul>
				</div>
			</div>
			<div class="layui-body" style="bottom: 0;" id="admin-body">
                <div style="width:500px;height:100%;float:left; margin-left:10px;margin-right:10px;">
                    <blockquote class="layui-elem-quote">
                        学生学号： <div class="layui-input-inline"><asp:TextBox ID="find_xh" placeholder="请输入学生学号" cssClass="layui-input" runat="server"></asp:TextBox></div>
                        <input type="button" value="查询" class="layui-btn layui-btn-small"  onclick="find()" />
                    </blockquote>
                    <br />
                    <fieldset class="layui-elem-field">
  <legend>操作员状态</legend>
  <div class="layui-field-box">
      <asp:HiddenField ID="pk_batch_no" Value="" runat="server" />
      <asp:HiddenField ID="pk_staff_no" Value="" runat="server" />
      <table><tr><td style="width:50%;">迎新学年：<asp:Label ID="batch_year" runat="server" Text="2016-2017学年"></asp:Label></td><td>迎新批次：<asp:Label ID="batch_name" runat="server" Text="秋季统招迎新"></asp:Label></td>
                       </tr>
          <tr><td style="width:50%;"></td><td></td></tr>
           <tr><td colspan="2" style="width:50%;">迎新服务时间：<asp:Label ID="batch_service_time" runat="server" Text="2017年7月1日~2017年9月2日"></asp:Label></td></tr>
           <tr><td style="width:50%;">操作员姓名：<asp:Label ID="staff_name" runat="server" Text="张明"></asp:Label></td><td>登陆时间：<asp:Label ID="login_time" runat="server" Text="2017年3月15日"></asp:Label></td></tr>
           <tr><td  colspan="2" style="width:50%;">当前事务操作：<asp:Label ID="affair_name" runat="server" Text="迎新系统首页"></asp:Label></td></tr>
          <tr><td style="width:50%;">预操作总人数：<asp:Label ID="affair_willcount" runat="server" Text="100"></asp:Label></td><td>已操作总人数：<asp:Label ID="affair_havecount" runat="server" Text="100"></asp:Label></td></tr>
      </table>
    
      
      
      
      
      
      
      
  </div>
</fieldset>
<br />

                        <fieldset class="layui-elem-field">
  <legend>学生状态</legend>
  <div class="layui-field-box">
    <table style="width:100%;font-size:16px;line-height:28px"><tr><td style="width:50%;border-right:1px solid #005CA3;padding-left:10px;padding-right:10px;">
        学号：<asp:Label ID="xs_xh" runat="server" Text=""></asp:Label>
        <br />
        姓名：<asp:Label ID="xs_xm" runat="server" Text=""></asp:Label>
        <br />
        性别：<asp:Label ID="xs_sb" runat="server" Text=""></asp:Label>
        <br />
        身份证号：<asp:Label ID="xs_sfz" runat="server" Text=""></asp:Label>
        <br />
        学历层次：<asp:Label ID="xs_xl" runat="server" Text=""></asp:Label>
        <br />
        学院：<asp:Label ID="xs_xy" runat="server" Text=""></asp:Label>
        <br />
        专业：<asp:Label ID="xs_zy" runat="server" Text=""></asp:Label><br />
        年级：<asp:Label ID="xs_nj" runat="server" Text=""></asp:Label><br />
        班级名称：<asp:Label ID="xs_bj" runat="server" Text=""></asp:Label><br />
        班主任：<asp:Label ID="xs_bzr" runat="server" Text=""></asp:Label><br />
        班主任电话：<asp:Label ID="xs_bzrdhhm" runat="server" Text=""></asp:Label><br />
                                  </td>
        <td style="width:50%;padding-left:10px;padding-right:10px; vertical-align:top;"  id="affair_list">
            新生报到：<asp:Label ID="zt_xsbd" runat="server" Text=""></asp:Label>
            <br />
        缴纳学费：<asp:Label ID="zt_jnxf" runat="server" Text=""></asp:Label>
            <br />
        分配宿舍：<asp:Label ID="zt_fpss" runat="server" Text=""></asp:Label>
            <br />
        选择床上用品：<asp:Label ID="zt_xzcsyp" runat="server" Text=""></asp:Label>
            <br />
        领取床上用品：<asp:Label ID="zt_yqcsyp" runat="server" Text=""></asp:Label>
            <br />
        领取一卡通：<asp:Label ID="zt_lqykt" runat="server" Text=""></asp:Label>
            <br />
        选择宿舍：<asp:Label ID="zt_xcss" runat="server" Text=""></asp:Label><br />
        宿舍入住：<asp:Label ID="zt_ssrz" runat="server" Text=""></asp:Label><br />       
        </td></tr></table>
  </div>
</fieldset>
                </div>
				<div class="layui-tab admin-nav-card layui-tab-brief" style="border-left:1px solid  #005CA3" lay-filter="admin-tab">
               
					<ul class="layui-tab-title">
						<li class="layui-this">
							<i class="fa fa-dashboard" aria-hidden="true"></i>
							<cite>学生自助首页</cite>
						</li>
            
					</ul>
                   
                  
					<div class="layui-tab-content" style="min-height: 150px; padding: 5px 0 0 0;">
						<div class="layui-tab-item layui-show"  id="czmain" runat="server" >
							<iframe id="iframeId"  src="/view/xszz-index.aspx"></iframe>
						</div>
					</div>
				</div>
			</div>
			
			<div class="site-tree-mobile layui-hide">
				<i class="layui-icon">&#xe602;</i>
			</div>
			<div class="site-mobile-shade"></div>
            <!--前端框架ＪＳ及弹出层ＪＳ-->
			<script type="text/javascript" src="plugins/layui/layui.js"></script>           
       
             <!--前端框架ＪＳ及弹出层ＪＳＯＶＥＲ-->
             <!--页面自动生成二维码ＪＳ　-->
			
            <script type="text/javascript" src="plugins/jquery.qrcode.min.js"></script>
             <!--页面自动生成二维码ＪＳＯＶＥＲ-->
			<script>
			    //忽略所有JS错误
			    function killerrors() { return true; }
			    window.onerror = killerrors;

			    //忽略错误结束,加载页面JS执行


			    layui.config({
			        base: 'js/'
			    }).use(['element', 'layer', 'navbar'], function () {
			        var element = layui.element(),
						$ = layui.jquery,
						layer = layui.layer,
						navbar = layui.navbar();

			        //navbar.render();
			        //iframe自适应
			        $(window).on('resize', function () {
			            var $content = $('.admin-nav-card .layui-tab-content');
			            $content.height($(this).height() - 117);
			            $content.find('iframe').each(function () {
			                $(this).height($content.height());
			            });
			        }).resize();
			        //设置navbar
			        navbar.set({
			            elem: '#admin-navbar-side',
			            url: 'js/czymenu.txt'
			        });
			        //渲染navbar
			        navbar.render();
			        var $body = $('.admin-nav-card');
			        var $tabs = $body.children('ul.layui-tab-title');
			        var $contents = $body.children('div.layui-tab-content');
			        var tabFilter = 'admin-tab';
			        //监听按钮事件
			        var btnSearch = $('#button2').on('click', function () {
			           // console.log('s');

			            //查询学生信息，更新

                        //



			            $('#iframeId').attr('src', 'SchoolMan1.aspx');
			            

			        });
			        //监听点击事件
			        navbar.on('click(side)', function (data) {
			            var href = data.field.href;
			            if (href === undefined || href === '') {
			                return;
			            }
			            var iframe = '<iframe id="iframeId" src="' + href + '"></iframe>';
			            var html = data.elem.html();
			            var count = 0;
			            var tabIndex;

			            console.log($tabs);
			            
			            $tabs.find('li').each(function (i, e) {
			                element.tabDelete(tabFilter, $(this).index()).init();

			                var $cite = $(this).children('cite');
			                if ($cite.text() === data.elem.find('cite').text()) {
			                    //count++;
			                    tabIndex = i;
			                };
                            
			            });
			            //tab不存在
			            if (count === 0) {
			                //添加删除样式
			                //html += '<i class="layui-icon layui-unselect layui-tab-close">&#x1006;</i>';
			                //添加tab
			                element.tabAdd(tabFilter, {
			                    title: html,
			                    content: iframe
			                });
			                //iframe 自适应
			                var $content = $('.admin-nav-card .layui-tab-content');
			                $content.find('iframe').each(function () {
			                    $(this).height($content.height());
			                });
			                //绑定关闭事件
			                $tabs = $body.children('ul.layui-tab-title');
			                var $li = $tabs.find('li');
			                $li.eq($li.length - 1).children('i.layui-tab-close').on('click', function () {
			                    element.tabDelete(tabFilter, $(this).parent('li').index()).init();
			                });
			                //获取焦点
			                element.tabChange(tabFilter, $li.length - 1);

			            } else {
			                //切换tab
			                element.tabChange(tabFilter, tabIndex);
			            }
			        });
			        //菜单的隐藏显示
			        $('.admin-side-toggle').on('click', function () {
			            console.log($('#admin-side').width());
			            console.log($('#admin-body').css('left'));
			            var sideWidth = $('#admin-side').width();
			            if (sideWidth === 180) {
			                $('#admin-side').width(0);
			                $('#admin-body').css('left', '0');
			                $(".admin-side-toggle").html("<i class='fa fa-chevron-right' aria-hidden='true'></i>");

			            } else {
			                $('#admin-side').width(180);
			                $('#admin-body').css('left', '180px');
			                $(".admin-side-toggle").html("<i class='fa fa-chevron-left' aria-hidden='true'></i>");
			            }

			        });



			        //全屏控制
			        $('.admin-side-full').on('click', function () {
			            if (!$(this).attr('fullscreen')) {
			                $(this).attr('fullscreen', 'true');
			                //全屏操作
			                var de = document.documentElement;
			                if (de.requestFullscreen) {
			                    de.requestFullscreen();
			                } else if (de.mozRequestFullScreen) {
			                    de.mozRequestFullScreen();
			                } else if (de.webkitRequestFullScreen) {
			                    de.webkitRequestFullScreen();
			                }
			            } else {
			                $(this).removeAttr('fullscreen')
			                //退出全屏
			                var de = document;
			                if (de.exitFullscreen) {
			                    de.exitFullscreen();
			                } else if (de.mozCancelFullScreen) {
			                    de.mozCancelFullScreen();
			                } else if (de.webkitCancelFullScreen) {
			                    de.webkitCancelFullScreen();
			                }
			            }
			        });
			        //手机设备的简单适配
			        var treeMobile = $('.site-tree-mobile'),
						shadeMobile = $('.site-mobile-shade');
			        treeMobile.on('click', function () {
			            $('body').addClass('site-mobile');
			        });
			        shadeMobile.on('click', function () {
			            $('body').removeClass('site-mobile');
			        });
			    });



			    function setCookie(name, value) {
			        var Days = 30;
			        var exp = new Date();
			        exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
			        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
			    }

			    //将当前页的ＵＲＬ宽高存入ＣＯＯＫＩＥ

			    setCookie('xwidth', screen.availWidth);
			    setCookie('xheight', screen.availHeight);
			    setCookie('xurl', window.location.href);
			    //生成当前页二维码ＣＯＤＥ
			    //$('#code').qrcode(window.location.href);

                //运行管理员页面业务代码
			   // load();

			</script>
       <script type="text/javascript" src="http://libs.baidu.com/jquery/1.9.1/jquery.min.js?v=20160917"></script>
        			<script type="text/javascript" src="../b_js/app/manager.js"></script>
        <script> load();</script>
		</div>
  
</form>  
</body>
</html>

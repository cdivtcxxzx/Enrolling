<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stdentlogin.aspx.cs" Inherits="view_stdentlogin" %>



<!DOCTYPE html>
<html lang="zh-cn">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" content="编码" />
        <meta name="renderer" content="webkit">
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
		<meta name="apple-mobile-web-app-status-bar-style" content="black">
		<meta name="apple-mobile-web-app-capable" content="yes">
		<meta name="format-detection" content="telephone=no">

    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
        <link rel="stylesheet" href="../nradmingl/plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="../nradmingl/plugins/global.css" media="all" />
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="../nradmingl/plugins/font-awesome/css/font-awesome.min.css" />
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="../nradmingl/plugins/table.css" />
    
     <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
   
   
</head>
<body> 
<!--展示响应式CSS块-->
 <style>
         
.responsive-utilities-test .col-xs-6 {
    margin-bottom: 10px;
}
.responsive-utilities-test .col-xs-6 {
    margin-bottom: 10px;
}
.col-xs-6 {
    width: 50%;
    float:left;
}
.visible-on .col-xs-6 .hidden-xs, .visible-on .col-xs-6 .hidden-sm, .visible-on .col-xs-6 .hidden-md, .visible-on .col-xs-6 .hidden-lg, .hidden-on .col-xs-6 .hidden-xs, .hidden-on .col-xs-6 .hidden-sm, .hidden-on .col-xs-6 .hidden-md, .hidden-on .col-xs-6 .hidden-lg {
    color: #999;
    border: 1px solid #ddd;
}
     .visible-on .col-xs-6 .visible-xs-block, .visible-on .col-xs-6 .visible-sm-block, .visible-on .col-xs-6 .visible-md-block, .visible-on .col-xs-6 .visible-lg-block, .hidden-on .col-xs-6 .visible-xs-block, .hidden-on .col-xs-6 .visible-sm-block, .hidden-on .col-xs-6 .visible-md-block, .hidden-on .col-xs-6 .visible-lg-block {
    color: #468847;
    background-color: #dff0d8;
    border: 1px solid #d6e9c6;
}
.responsive-utilities-test span {
    display: block;
    padding: 15px 10px;
    font-size: 14px;
    font-weight: 700;
    line-height: 1.1;
    text-align: center;
    border-radius: 4px;
}
     </style>
     <!--展示响应式CSS块over-->
     <!--页面开始全范围框架-->
     <div class="admin-main">
     <!--顶部提示及导航-->
    		<blockquote class="layui-elem-quote">
          
            <i class="layui-icon">&#xe602;</i>迎新管理 <i class="layui-icon">&#xe602;</i>学生登陆
            <span style="float:right">
            
				
                 <a href="javascript:window.location.go(-1);" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe603;</i>
				</a>
               </span>
				
			</blockquote>
 <!--顶部提示及导航OVER-->

  <!--标签框架-->
         <style>
             .xszp img{
                 width:100%;
                 max-width:220px;
                 max-height:360px;
             }
             .xsxx1{float:left;margin-right:10px;width:45%;}
             .xsxx2{float:left;margin-right:10px;width:45%;}
             .xsxx3{float:left;width:32%;}
             @media (max-width: 930px) {
                 .xsxx1 {float: none;margin-right:10px;
                         width:100%;
                 }
                 .xsxx2 {
                 width:100%;}
                
             }
             @media (max-width: 550px) {
                                  .xsxx2 {
                 width:100%;float: none;}
                 .xsxx3 {
                 width:100%;float: none;}
             }
         </style>
         <div style="margin-top:15px;">
	        
       
             
<form class="layui-form layui-form-pane" runat="server" action="">
    <div class="xsxx1"><div class="layui-form-item" pane="">
          <label class="layui-form-label" style="height:94%;display:none">注意事项：</label>
          <div class="layui-input-block" style="margin-left: 10px!important">
           <div class="layui-form-mid layui-word-aux-ts xszp" style="margin-left:10px;float:none!important" id="tsxx" runat="server">注意事项：</div></div>
        </div></div>
     <div class="xsxx2"  >   <div class="layui-form-item">
          <label class="layui-form-label">报名号：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:0px;    padding: 0px 0;">
               <asp:TextBox ID="username" autocomplete="off" placeholder="请输入报名号" CssClass ="layui-input" runat="server"></asp:TextBox>
           </div>


             




          </div>
        </div>
         <div class="layui-form-item" >
          <label class="layui-form-label">密码：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:0px;    padding: 0px 0;">
                <asp:TextBox ID="password" lay-verify="required" autocomplete="off" placeholder="默认为身份证后六位" CssClass ="layui-input" runat="server" TextMode="Password"></asp:TextBox>

           </div></div>
        </div>
         <div class="layui-form-item" pane="">
          <label class="layui-form-label">验证码：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;">
               <asp:Image ID="Image1" runat="server" /></div></div>
        </div>
         <div class="layui-form-item" style="text-align:center">
          <button class="layui-btn" onclick="javascript:">登陆</button>&nbsp;&nbsp;&nbsp;&nbsp;<button class="layui-btn" onclick="javascript:">忘记密码</button>
        </div></div>
        
        </div>
  
     

      </form>
</div>        

    
  <!--标签框架over-->

        </div>


        		<script type="text/javascript" src="../nradmingl/plugins/layui/layui.js"></script>
		<script>
		    layui.use('element', function () {
		        var $ = layui.jquery,
					element = layui.element(); //Tab的切换功能，切换事件监听等，需要依赖element模块

		        //触发事件
		        var active = {
		            tabAdd: function () {
		                //新增一个Tab项
		                element.tabAdd('demo', {
		                    title: '新选项' + (Math.random() * 1000 | 0) //用于演示
								,
		                    content: '内容' + (Math.random() * 1000 | 0)
		                })
		            },
		            tabDelete: function () {
		                //删除指定Tab项
		                element.tabDelete('demo', 2); //删除第3项（注意序号是从0开始计算）
		            },
		            tabChange: function () {
		                //切换到指定Tab项
		                element.tabChange('demo', 1); //切换到第2项（注意序号是从0开始计算）
		            }
		        };

		        $('.site-demo-active').on('click', function () {
		            var type = $(this).data('type');
		            active[type] ? active[type].call(this) : '';
		        });
		    });


		</script>
        	<script>
        	    layui.use(['form', 'layedit', 'laydate'], function () {
        	        var form = layui.form(),
					layer = layui.layer,
					layedit = layui.layedit,
					laydate = layui.laydate;

        	        //创建一个编辑器
        	        var editIndex = layedit.build('LAY_demo_editor');
        	        //自定义验证规则
        	        form.verify({
        	            title: function (value) {
        	                if (value.length < 5) {
        	                    return '标题至少得5个字符啊';
        	                }
        	            },
        	            pass: [/(.+){6,12}$/, '密码必须6到12位'],
        	            content: function (value) {
        	                layedit.sync(editIndex);
        	            }
        	        });

        	        //监听提交
        	        form.on('submit(demo1)', function (data) {
        	            layer.alert(JSON.stringify(data.field), {
        	                title: '最终的提交信息'
        	            })
        	            return false;
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
		</script>



</body>
</html>

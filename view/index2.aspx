<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index2.aspx.cs" Inherits="view_index2" %>
 
 
 
<!DOCTYPE html>
<html lang="zh-cn">
<head id="Head1"><title>
	学生自助首页
</title><meta charset="UTF-8" content="编码" /><meta name="renderer" content="webkit" /><meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" /><meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" /><meta name="apple-mobile-web-app-status-bar-style" content="black" /><meta name="apple-mobile-web-app-capable" content="yes" /><meta name="format-detection" content="telephone=no" />
 
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
        <link rel="stylesheet" href="../nradmingl/plugins/layui/css/layui.css" media="all" /><link rel="stylesheet" href="../nradmingl/plugins/global.css" media="all" />
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
          
            <i class="layui-icon">&#xe602;</i>迎新管理&gt;&gt;学生网上报到首页
            <span style="float:right">
            
				
                 <a href="/" class="layui-btn layui-btn-small">
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
             .xsxx1{float:left;margin-right:10px;width:32%;}
             .xsxx2{float:left;margin-right:10px;width:32%;}
             .xsxx3{float:left;width:32%;}
            
                 .xsxx1 {float: none;margin-right:10px;
                         width:100%;
                 }
                 .xsxx2 {
                 width:48%;}
                 .xsxx3 {
                 width:48%;}
            
             @media (max-width: 550px) {
                                  .xsxx2 {
                 width:100%;float: none;}
                 .xsxx3 {
                 width:100%;float: none;}
             }
         </style>
         <div style="margin-top:15px;">
	        
       
             
<form class="layui-form layui-form-pane" action="">
   
     <div class="xsxx2"  >   <div class="layui-form-item" pane="" style="min-height: 56px">
          <label class="layui-form-label" style="width:120px;"><a class="layui-btn"  onClick="javascript:location.href='xszz-index.aspx';">阅读报到须知</a></label>
          <div class="layui-input-block" style="margin-left:140px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:40px;padding: 18px 0;">
               <span id="xsxx_xh"><font color=green><b>已于2017年4月18日阅读报到须知</b></font></span> </div></div>
        </div>
		
		<div class="layui-form-item" pane="" style="min-height: 56px">
          <label class="layui-form-label" style="width:120px;"><a class="layui-btn"  onClick="javascript:location.href='xszz-index.aspx';">录取信息确认</a></label>
          <div class="layui-input-block" style="margin-left:140px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:40px;padding: 18px 0;">
               <span id="xsxx_xh"><font color=green><b>已确认录取信息</b></font></span> </div></div>
        </div>
		
				<div class="layui-form-item" pane="" style="min-height: 56px">
          <label class="layui-form-label" style="width:120px;"><a class="layui-btn"  onClick="javascript:location.href='xszz-index.aspx';">基础信息登记</a></label>
          <div class="layui-input-block" style="margin-left:140px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:40px;padding: 18px 0;">
               <span id="xsxx_xh"><font color=red><b>已登记基础信息，完成率80%</b></font></span> </div></div>
        </div>
		
		</div>
     <div  class="xsxx3"  >   
	 
				<div class="layui-form-item" pane="" style="min-height: 56px">
          <label class="layui-form-label" style="width:120px;"><a class="layui-btn"  onClick="javascript:location.href='xszz-index.aspx';">网上缴费</a></label>
          <div class="layui-input-block" style="margin-left:140px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:40px;padding: 18px 0;">
               <span id="xsxx_xh"><font color=green><b>完成操作</b></font></span> </div></div>
        </div>
				<div class="layui-form-item" pane="" style="min-height: 56px">
          <label class="layui-form-label" style="width:120px;"><a class="layui-btn"  onClick="javascript:location.href='xszz-index.aspx';">寝室选择</a></label>
          <div class="layui-input-block" style="margin-left:140px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:40px;padding: 18px 0;">
               <span id="xsxx_xh"><font color=green><b>已选择1号学生公寓306寝室</b></font></span> </div></div>
        </div>
		
		<div class="layui-form-item" pane="" style="min-height: 56px">
          <label class="layui-form-label" style="width:120px;"><a class="layui-btn"  onClick="javascript:location.href='xszz-index.aspx';">消息提醒</a></label>
          <div class="layui-input-block" style="margin-left:140px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:40px;padding: 18px 0;">
               <span id="xsxx_xh">有<font color=red><b>3</b></font>条消息未读</span> </div></div>
        </div>
	 
	 
        </div>
		<!--
        <div class="layui-form-item" style="text-align:center">
          <button class="layui-btn" onClick="javascript:">返回操作首页</button>
        </div>-->
 
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


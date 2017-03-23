<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xsjbxx.aspx.cs" Inherits="view_xsjbxx" %>


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
          
            迎新管理>>学生基本信息
            <span style="float:right">
            
				
                 <a href="javascript:window.location.go(-1);" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe603;</i>
				</a>
               </span>
				
			</blockquote>
 <!--顶部提示及导航OVER-->

  <!--标签框架-->
		<div style="width:50%;float:left">
            <asp:Image ID="Image1" runat="server" /></div>
         
         <div>
             <form class="layui-form layui-form-pane" action="">
        <div class="layui-form-item">
          <label class="layui-form-label">输入框</label>
          <div class="layui-input-block">
            <input type="text" name="title" required="" lay-verify="required" placeholder="请输入标题" autocomplete="off" class="layui-input">
          </div>
        </div>
        <div class="layui-form-item">
          <label class="layui-form-label">密码框</label>
          <div class="layui-input-inline">
            <input type="password" name="password" required="" lay-verify="required" placeholder="请输入密码" autocomplete="off" class="layui-input">
          </div>
          <div class="layui-form-mid layui-word-aux">辅助文字</div>
        </div>
        <div class="layui-form-item">
          <label class="layui-form-label">选择框</label>
          <div class="layui-input-block">
            <select name="city" lay-verify="required">
              <option value=""></option>
              <option value="0">北京</option>
              <option value="1">上海</option>
              <option value="2">广州</option>
              <option value="3">深圳</option>
              <option value="4">杭州</option>
            </select><div class="layui-unselect layui-form-select"><div class="layui-select-title"><input type="text" placeholder="请选择" value="" readonly="" class="layui-input layui-unselect"><i class="layui-edge"></i></div><dl class="layui-anim layui-anim-upbit"><dd lay-value="0" class="">北京</dd><dd lay-value="1" class="">上海</dd><dd lay-value="2" class="">广州</dd><dd lay-value="3" class="">深圳</dd><dd lay-value="4" class="">杭州</dd></dl></div>
          </div>
        </div>
        <div class="layui-form-item" pane="">
          <label class="layui-form-label">开关</label>
          <div class="layui-input-block">
            <input type="checkbox" name="switch" lay-skin="switch"><div class="layui-unselect layui-form-switch" lay-skin="_switch"><em></em><i></i></div>
          </div>
        </div>
        <div class="layui-form-item" pane="">
          <label class="layui-form-label">单选框</label>
          <div class="layui-input-block">
            <input type="radio" name="sex" value="男" title="男"><div class="layui-unselect layui-form-radio"><i class="layui-anim layui-icon"></i><span>男</span></div>
            <input type="radio" name="sex" value="女" title="女" checked=""><div class="layui-unselect layui-form-radio layui-form-radioed"><i class="layui-anim layui-icon"></i><span>女</span></div>
          </div>
        </div>
        <div class="layui-form-item layui-form-text">
          <label class="layui-form-label">文本域</label>
          <div class="layui-input-block">
            <textarea placeholder="请输入内容" class="layui-textarea"></textarea>
          </div>
        </div>
        <div class="layui-form-item">
          <button class="layui-btn" lay-submit="" lay-filter="formDemoPane">立即提交</button>
        </div>
      </form>

         </div>


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

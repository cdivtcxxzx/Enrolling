<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xys.aspx.cs" Inherits="nradmingl_xys" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" content="编码" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
        <link rel="stylesheet" href="plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="plugins/global.css" media="all" />
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css" />
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="plugins/table.css" />
    
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
          
            基本展示
            <span style="float:right">
            
				<a href="kfgl.aspx" id="A1" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe62d;</i>表格演示
				</a>
                 <a href="" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#x1002;</i>
				</a>
                <a href="javascript:" onclick="layer.open({  id:1,type: 2,  title: '当前页二维码',  shadeClose: true,  shade: 0.8,  area: ['285px', '318px'],time: 60000,  content: '2weima.aspx?url='+window.location.href.replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|')});"><img src="images/2weima.png" titop="images/2weima.png" alt="images/2weima.png" style="height:28px;width:28px" /></a>
				</span>
				
			</blockquote>
 <!--顶部提示及导航OVER-->

  <!--标签框架-->
		<div class="layui-tab">

        <!--标签顶部切换用LI,与后面标签内容配合演示为4个LI-->
			<ul class="layui-tab-title">
				<li class="layui-this">在...可见</li>
				<li>在...隐藏</li>
				<li>表单演示</li>
				<li>其它工具演示</li>
			</ul>
        <!--标签顶部切换用LIST,与后面标签内容配合over-->
        <!--标签内容框架与标签顶部LI配合-->
			<div class="layui-tab-content">
             <!--标签内容,演示为4个内容DIV-->

				<div class="layui-tab-item layui-show">
					<div class="row responsive-utilities-test visible-on">
    <div class="col-xs-6">
      <span class="hidden-xs">超小屏幕</span>
      <span class="visible-xs-block">✔ 在超小屏幕上可见</span>
    </div>
    <div class="col-xs-6">
      <span class="hidden-sm">小屏幕</span>
      <span class="visible-sm-block">✔ 在小屏幕上可见</span>
    </div>
    <div class="clearfix visible-xs-block"></div>
    <div class="col-xs-6">
      <span class="hidden-md">中等屏幕</span>
      <span class="visible-md-block">✔ 在中等屏幕上可见</span>
    </div>
    <div class="col-xs-6">
      <span class="hidden-lg">大屏幕</span>
      <span class="visible-lg-block">✔ 在大屏幕上可见</span>
    </div>
  </div>
  <div class="row responsive-utilities-test visible-on">
    <div class="col-xs-6 col-sm-6">
      <span class="hidden-xs hidden-sm">超小屏幕和小屏幕</span>
      <span class="visible-xs-block visible-sm-block">✔ 在超小屏幕和小屏幕上可见</span>
    </div>
    <div class="col-xs-6 col-sm-6">
      <span class="hidden-md hidden-lg">中等屏幕和大屏幕</span>
      <span class="visible-md-block visible-lg-block">✔ 在中等屏幕和大屏幕上可见</span>
    </div>
    <div class="clearfix visible-xs-block"></div>
    <div class="col-xs-6 col-sm-6">
      <span class="hidden-xs hidden-md">超小屏幕和中等屏幕</span>
      <span class="visible-xs-block visible-md-block">✔ 在超小屏幕和中等屏幕上可见</span>
    </div>
    <div class="col-xs-6 col-sm-6">
      <span class="hidden-sm hidden-lg">小屏幕和大屏幕</span>
      <span class="visible-sm-block visible-lg-block">✔ 在小屏幕和大屏幕上可见</span>
    </div>
    <div class="clearfix visible-xs-block"></div>
    <div class="col-xs-6 col-sm-6">
      <span class="hidden-xs hidden-lg">超小屏幕和大屏幕</span>
      <span class="visible-xs-block visible-lg-block">✔ 在超小屏幕和大屏幕上可见</span>
    </div>
    <div class="col-xs-6 col-sm-6">
      <span class="hidden-sm hidden-md">小屏幕和中等屏幕</span>
      <span class="visible-sm-block visible-md-block">✔ 在小屏幕和中等屏幕上可见</span>
    </div>
  </div>
				</div>
				<div class="layui-tab-item">
                
                 <div class="row responsive-utilities-test hidden-on">
    <div class="col-xs-6 col-sm-3">
      <span class="hidden-xs">超小屏幕</span>
      <span class="visible-xs-block">✔ 在超小屏幕上隐藏</span>
    </div>
    <div class="col-xs-6 col-sm-3">
      <span class="hidden-sm">小屏幕</span>
      <span class="visible-sm-block">✔ 在小屏幕上隐藏</span>
    </div>
    <div class="clearfix visible-xs-block"></div>
    <div class="col-xs-6 col-sm-3">
      <span class="hidden-md">中等屏幕</span>
      <span class="visible-md-block">✔ 在中等屏幕上隐藏</span>
    </div>
    <div class="col-xs-6 col-sm-3">
      <span class="hidden-lg">大屏幕</span>
      <span class="visible-lg-block">✔ 在大屏幕上隐藏</span>
    </div>
  </div>
  <div class="row responsive-utilities-test hidden-on">
    <div class="col-xs-6">
      <span class="hidden-xs hidden-sm">超小屏幕与小屏幕</span>
      <span class="visible-xs-block visible-sm-block">✔ 在超小屏幕和小屏幕上隐藏</span>
    </div>
    <div class="col-xs-6">
      <span class="hidden-md hidden-lg">中等屏幕和大屏幕</span>
      <span class="visible-md-block visible-lg-block">✔ 在 medium 和 large 上隐藏</span>
    </div>
    <div class="clearfix visible-xs-block"></div>
    <div class="col-xs-6">
      <span class="hidden-xs hidden-md">超小屏幕和中等屏幕</span>
      <span class="visible-xs-block visible-md-block">✔ 在超小屏幕和中等屏幕上隐藏</span>
    </div>
    <div class="col-xs-6">
      <span class="hidden-sm hidden-lg">小屏幕和大屏幕</span>
      <span class="visible-sm-block visible-lg-block">✔ 在小屏幕和大屏幕上隐藏</span>
    </div>
    <div class="clearfix visible-xs-block"></div>
    <div class="col-xs-6">
      <span class="hidden-xs hidden-lg">超小屏幕和大屏幕</span>
      <span class="visible-xs-block visible-lg-block">✔ 在超小屏幕和大屏幕上隐藏</span>
    </div>
    <div class="col-xs-6">
      <span class="hidden-sm hidden-md">小屏幕和中等屏幕</span>
      <span class="visible-sm-block visible-md-block">✔ 在小屏幕和中等屏幕上隐藏</span>
    </div>
  </div>
                
                
                
                </div>
				<div class="layui-tab-item">		
                <div style="margin: 15px;">
			<fieldset class="layui-elem-field layui-field-title" style="margin-top: 20px;">
				<legend>响应式的表单集合</legend>
			</fieldset>

			<form class="layui-form" action="">
				<div class="layui-form-item">
					<label class="layui-form-label">单行输入框</label>
					<div class="layui-input-block">
						<input type="text" name="title" lay-verify="title" autocomplete="off" placeholder="请输入标题" class="layui-input">
					</div>
				</div>












				<div class="layui-form-item">
					<label class="layui-form-label">验证必填项</label>
					<div class="layui-input-block">
						<input type="text" name="username" lay-verify="required" placeholder="请输入" autocomplete="off" class="layui-input">
					</div>
				</div>
				<div class="layui-form-item">
					<label class="layui-form-label">验证手机</label>
					<div class="layui-input-inline">
						<input type="tel" name="phone" lay-verify="phone" autocomplete="off" class="layui-input">
					</div>
				</div>
				<div class="layui-form-item">
					<label class="layui-form-label">验证邮箱</label>
					<div class="layui-input-inline">
						<input type="text" name="email" lay-verify="email" autocomplete="off" class="layui-input">
					</div>
				</div>

				<div class="layui-form-item">
					<div class="layui-inline">
						<label class="layui-form-label">验证数字</label>
						<div class="layui-input-inline">
							<input type="number" name="number" lay-verify="number" autocomplete="off" class="layui-input">
						</div>
					</div>
					<div class="layui-inline">
						<label class="layui-form-label">验证日期</label>
						<div class="layui-input-block">
							<input type="text" name="date" id="date" lay-verify="date" placeholder="yyyy-mm-dd" autocomplete="off" class="layui-input" onclick="layui.laydate({elem: this})">
						</div>
					</div>
					<div class="layui-inline">
						<label class="layui-form-label">验证链接</label>
						<div class="layui-input-block">
							<input type="tel" name="url" lay-verify="url" autocomplete="off" class="layui-input">
						</div>
					</div>
				</div>
				<div class="layui-form-item">
					<label class="layui-form-label">验证身份证</label>
					<div class="layui-input-block">
						<input type="text" name="identity" lay-verify="identity" placeholder="" autocomplete="off" class="layui-input">
					</div>
				</div>
				<div class="layui-form-item">
					<label class="layui-form-label">自定义验证</label>
					<div class="layui-input-inline">
						<input type="password" name="password" lay-verify="pass" placeholder="请输入密码" autocomplete="off" class="layui-input">
					</div>
					<div class="layui-form-mid layui-word-aux">请填写6到12位密码</div>
				</div>

				<div class="layui-form-item">
					<div class="layui-inline">
						<label class="layui-form-label">范围</label>
						<div class="layui-input-inline" style="width: 100px;">
							<input type="text" name="price_min" placeholder="￥" autocomplete="off" class="layui-input">
						</div>
						<div class="layui-form-mid">-</div>
						<div class="layui-input-inline" style="width: 100px;">
							<input type="text" name="price_max" placeholder="￥" autocomplete="off" class="layui-input">
						</div>
					</div>
				</div>

				<div class="layui-form-item">
					<label class="layui-form-label">单行选择框</label>
					<div class="layui-input-block">
						<select name="interest" lay-filter="aihao">
							<option value=""></option>
							<option value="0">写作</option>
							<option value="1" selected="">阅读</option>
							<option value="2">游戏</option>
							<option value="3">音乐</option>
							<option value="4">旅行</option>
						</select>
					</div>
				</div>

				<div class="layui-form-item">
					<label class="layui-form-label">分组选择框</label>
					<div class="layui-input-inline">
						<select name="quiz">
							<option value="">请选择问题</option>
							<optgroup label="城市记忆">
								<option value="你工作的第一个城市">你工作的第一个城市</option>
							</optgroup>
							<optgroup label="学生时代">
								<option value="你的工号">你的工号</option>
								<option value="你最喜欢的老师">你最喜欢的老师</option>
							</optgroup>
						</select>
					</div>
				</div>

				<div class="layui-form-item">
					<label class="layui-form-label">行内选择框</label>
					<div class="layui-input-inline">
						<select name="quiz1">
							<option value="">请选择省</option>
							<option value="浙江" selected="">浙江省</option>
							<option value="你的工号">江西省</option>
							<option value="你最喜欢的老师">福建省</option>
						</select>
					</div>
					<div class="layui-input-inline">
						<select name="quiz2">
							<option value="">请选择市</option>
							<option value="杭州">杭州</option>
							<option value="宁波" disabled="">宁波</option>
							<option value="温州">温州</option>
							<option value="温州">台州</option>
							<option value="温州">绍兴</option>
						</select>
					</div>
					<div class="layui-input-inline">
						<select name="quiz3">
							<option value="">请选择县/区</option>
							<option value="西湖区">西湖区</option>
							<option value="余杭区">余杭区</option>
							<option value="拱墅区">临安市</option>
						</select>
					</div>
				</div>

				<div class="layui-form-item">
					<label class="layui-form-label">复选框</label>
					<div class="layui-input-block">
						<input type="checkbox" name="like[write]" title="写作">
						<input type="checkbox" name="like[read]" title="阅读" checked="">
						<input type="checkbox" name="like[game]" title="游戏">
					</div>
				</div>
				<div class="layui-form-item">
					<label class="layui-form-label">开关-关</label>
					<div class="layui-input-block">
						<input type="checkbox" name="close" lay-skin="switch" title="开关">
					</div>
				</div>
				<div class="layui-form-item">
					<label class="layui-form-label">开关-开</label>
					<div class="layui-input-block">
						<input type="checkbox" checked="" name="open" lay-skin="switch" lay-filter="switchTest" title="开关">
					</div>
				</div>
				<div class="layui-form-item">
					<label class="layui-form-label">单选框</label>
					<div class="layui-input-block">
						<input type="radio" name="sex" value="男" title="男" checked="">
						<input type="radio" name="sex" value="女" title="女">
						<input type="radio" name="sex" value="禁" title="禁用" disabled="">
					</div>
				</div>
				<div class="layui-form-item layui-form-text">
					<label class="layui-form-label">普通文本域</label>
					<div class="layui-input-block">
						<textarea placeholder="请输入内容" class="layui-textarea"></textarea>
					</div>
				</div>
				<div class="layui-form-item layui-form-text">
					<label class="layui-form-label">编辑器</label>
					<div class="layui-input-block">
						<textarea class="layui-textarea layui-hide" name="content" lay-verify="content" id="LAY_demo_editor"></textarea>
					</div>
				</div>
				<div class="layui-form-item">
					<div class="layui-input-block">
						<button class="layui-btn" lay-submit="" lay-filter="demo1">立即提交</button>
						<button type="reset" class="layui-btn layui-btn-primary">重置</button>
					</div>
				</div>
			</form>
		</div>
</div>
                <div class="layui-tab-item">
         
              空标签,可加任意内容



                </div>
		      <!--标签内容over-->
			</div>
        <!--标签内容框架与标签顶部LI配合OVER-->
		</div>


        </div>


        		<script type="text/javascript" src="plugins/layui/layui.js"></script>
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
        	    });
		</script>



</body>
</html>

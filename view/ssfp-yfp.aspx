<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssfp-yfp.aspx.cs" Inherits="view_ssfp_yfp" %>


<!DOCTYPE html>
<html lang="zh-cn">
<head runat="server">
    <title>宿舍分配-预分配</title>
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
   
    <link href="../bootstrap/3.3.4/css/bootstrap.min.css" rel="stylesheet" />
   
</head>
<body>
    <form id="form1" class="layui-form layui-form-pane" runat="server">
         <div class="admin-main">
     <!--顶部提示及导航-->
    		<blockquote class="layui-elem-quote">
          
            <i class="layui-icon">&#xe602;</i>迎新管理>>预分配宿舍
            <span style="float:right">
            
				
                 <a href="javascript:window.location.go(-1);" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe603;</i>
				</a>
               </span>
				
			</blockquote>
 <!--顶部提示及导航OVER-->
        <div class="container">
            <div class="col-xs-12 col-sm-4" style="margin-top:15px;border:1px solid #eee;text-align:center;" >
               <style>.noshow{display:none}</style> <p style="margin-top:20px;"><span>校区：</span><span id="xiaoqu" runat="server"><asp:Label ID="xqbh" runat="server" CssClass="noshow" Text="01"></asp:Label><asp:Label ID="xqmc" runat="server" Text="天府新区"></asp:Label></span></p>
               <%-- <p><span>类型：</span><span id="shuse" runat="server">男宿舍</span></p>--%>
                <p style="margin-top:10px;margin-bottom:10px;">
                    <img src="../images/xsgysmall.jpg" alt="宿舍照片" class="xsgytp" style="margin-top: 18px; width: 90%; height: 90%" id="shuseImg" runat="server" />
                </p>
            </div>
            <div class="col-xs-12 col-sm-8" style="margin-top:15px;">
                
                <div class="layui-form-item" pane="">
          <label class="layui-form-label" style="width:120px;">学号：</label>
          <div class="layui-input-block" style="margin-left:120px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;">
               <asp:Label ID="xsxx_xh" runat="server" Text="20170001"></asp:Label>

           </div>

          </div>
        </div>
                 <div class="layui-form-item" pane="">
          <label class="layui-form-label" style="width:120px;">姓名：</label>
          <div class="layui-input-block" style="margin-left:120px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;">
               <asp:Label ID="Label1" runat="server" Text="张三"></asp:Label></div></div>
        </div>
                 <div class="layui-form-item" pane="">
          <label class="layui-form-label" style="width:120px;">班级：</label>
          <div class="layui-input-block" style="margin-left:120px;">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;">
               <asp:Label ID="Label2" runat="server" Text="汽修1701班"></asp:Label></div></div>
        </div>

                 <div class="layui-form-item">
                    <label class="layui-form-label"  style="width:120px;">房间类型选择</label>
                <div class="layui-input-block"  style="margin-left:120px;">

                    <asp:DropDownList ID="DropDownList1"  lay-filter="aihao" runat="server" DataSourceID="SqlDataSource1" DataTextField="Type_Name" DataValueField="Type_NO"></asp:DropDownList>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnString %>" SelectCommand="SELECT [Type_Name], [Type_NO] FROM [Fresh_Room_Type]"></asp:SqlDataSource>


                </div>
                 </div>
                       <div class="layui-form-item">
                    <label class="layui-form-label"  style="width:120px;">宿舍楼栋选择</label>
                <div class="layui-input-block"  style="margin-left:120px;">
                 <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="Dorm_NO">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnString %>" SelectCommand="SELECT [Dorm_NO], [Name] FROM [Fresh_Dorm] WHERE ([Campus_NO] = @Campus_NO)">
            <SelectParameters>
                <asp:ControlParameter ControlID="xqbh" Name="Campus_NO" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
                </div>
                 </div>
                 <div class="layui-form-item">
                    <label class="layui-form-label"  style="width:120px;">楼层选择</label>
                <div class="layui-input-block"  style="margin-left:120px;">
                 <select name="interest" lay-filter="aihao">
                     <option value=""></option>
                     <option value="0">一楼</option>
                     <option value="1" >二楼</option>
                     <option value="2" selected="">三楼</option>
                     <option value="3">四楼</option>
                     <option value="3">五楼</option>
                     <option value="3">六楼</option>
                         
                    </select>
                </div>
                 </div>
               
                <style>
                    #cwts{margin:10px;}
                    .layui-elem-field legend {
                        margin-left: 20px;
                        padding: 0 10px;
                        font-size: 16px;
                        font-weight: 300;
                    }
                    legend {
    display: block;
    padding: 0;
    margin-bottom: 0px;
    font-size: 21px;
    line-height: inherit;
    color: #333;
    border: 0;
    border-bottom: 0px solid #e5e5e5;
    width: inherit;
}
                </style>

  <fieldset class="layui-elem-field">
  <legend>房间选择</legend>
  <div class="layui-field-box" style="    padding: 10px 5px;">
     <div class="layui-input-block" style="    margin-left:2px;">
         <input type="radio" name="fh" value="301" title="301" disabled="">
         <input type="radio" name="fh" value="302" title="302" disabled="">
         <input type="radio" name="fh" value="303" title="303" disabled="">
         <input type="radio" name="fh" value="304" title="304" disabled="">
         <input type="radio" name="fh" value="305" title="305" disabled="">
        
      <input type="radio" name="fh" value="306" title="306" checked="">
      <input type="radio" name="fh" value="307" title="307">
      <input type="radio" name="fh" value="308" title="308" disabled="">
         <input type="radio" name="fh" value="309" title="309" disabled="">
         <input type="radio" name="fh" value="310" title="310" disabled="">
         <input type="radio" name="fh" value="311" title="311" disabled="">
         <input type="radio" name="fh" value="312" title="312">
        
          <input type="radio" name="fh" value="313" title="313">
          <input type="radio" name="fh" value="314" title="314">
          <input type="radio" name="fh" value="315" title="315">
          <input type="radio" name="fh" value="316" title="316">
          <input type="radio" name="fh" value="317" title="317">
        
          <input type="radio" name="fh" value="318" title="318">
           <input type="radio" name="fh" value="319" title="319"> 
         <input type="radio" name="fh" value="320" title="320">
          <input type="radio" name="fh" value="32" title="321" disabled="">
         <input type="radio" name="fh" value="32" title="322" disabled="">
         <input type="radio" name="fh" value="32" title="323" disabled="">
         <input type="radio" name="fh" value="32" title="324" disabled="">
         <input type="radio" name="fh" value="32" title="325" disabled="">
         
    </div>
  </div>
</fieldset>

                 <fieldset class="layui-elem-field">
  <legend>床位选择</legend>
  <div class="layui-field-box" style="    padding: 10px 5px;">
     <div class="layui-input-block" style="    margin-left:2px;">
         <input type="radio" name="cw" value="01床" title="01床" disabled="">
         <input type="radio" name="cw" value="02床" title="02床" disabled="">
         <input type="radio" name="cw" value="03床" title="03床" disabled="">
         <input type="radio" name="cw" value="04床" title="04床" checked="">
         <input type="radio" name="cw" value="05床" title="05床" >
        
      <input type="radio" name="cw" value="06床" title="06床" > 
               <asp:Label ID="cwts"  runat="server" Text="下铺靠窗"></asp:Label>

           
      
         
    </div>
  </div>
</fieldset>
  <fieldset class="layui-elem-field">
  <legend>提示信息</legend>
  <div class="layui-field-box">
    已选择一号学生公寓3楼306寝室，该寝室已有3人选择，剩于3个床位
  </div>
</fieldset>

<div class="layui-form-item" style="text-align:center">
          <button class="layui-btn" onclick="javascript:">确认寝室选择</button>&nbsp;&nbsp;&nbsp;&nbsp;<button class="layui-btn" onclick="javascript:">返回操作首页</button>
        </div>


            </div>

        </div>
        <asp:HiddenField ID="hidenClassNo" runat="server" />
        <asp:HiddenField ID="hidenGender" runat="server" />

             </div>

        <asp:HiddenField ID="hiddenSno" runat="server" />

    </form>
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

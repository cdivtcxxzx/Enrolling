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
          
            <i class="layui-icon">&#xe602;</i>学生网上自助报到>>学生基本信息
            <span style="float:right"  id="btnback">
            
				
                 <a href="javascript:history.go(-1);" class="layui-btn layui-btn-small">
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
             @media (max-width: 930px) {
                 .xsxx1 {float: none;margin-right:10px;
                         width:100%;
                 }
                 .xsxx2 {
                 width:48%;}
                 .xsxx3 {
                 width:48%;}
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
    <asp:HiddenField ID="hidden_pk_sno" runat="server" />
    <asp:HiddenField ID="server_msg" runat="server" />
    <asp:HiddenField ID="pk_staff_no" Value="" runat="server" />
    <div class="xsxx1"><div class="layui-form-item" pane="">
          <label class="layui-form-label" style="height:94%;display:none">照片：</label>
          <div class="layui-input-block" style="margin-left: 10px!important">
           <div class="layui-form-mid layui-word-aux-ts xszp" style="margin-left:10px;text-align:center;float:none!important"><asp:Image ID="xszpxx" ImageUrl="../images/xstp/test.jpg" runat="server" /></div></div>
        </div></div>
     <div class="xsxx2"  >   <div class="layui-form-item" pane="">
          <label class="layui-form-label">学号：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;">
               <asp:Label ID="xsxx_xh" runat="server" Text=""></asp:Label></div></div>
        </div>
         <div class="layui-form-item" pane="">
          <label class="layui-form-label">姓名：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_xm" runat="server" Text=""></asp:Label></div></div>
        </div>
         <div class="layui-form-item" pane="">
          <label class="layui-form-label">性别：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_xb" runat="server" Text=""></asp:Label></div></div>
        </div>
         <div class="layui-form-item" pane="">
          <label class="layui-form-label">身份证号：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_sfzh" runat="server" Text=""></asp:Label></div></div>
        </div>
        
         <div class="layui-form-item" pane="">
          <label class="layui-form-label">学历层次：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_xlcc" runat="server" Text=""></asp:Label></div></div>
        </div>
         <div class="layui-form-item" pane="">
          <label class="layui-form-label">学院：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_xy" runat="server" Text=""></asp:Label></div></div>
        </div></div>
     <div  class="xsxx3"  >         <div class="layui-form-item" pane="">
          <label class="layui-form-label">专业：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_zy" runat="server" Text=""></asp:Label></div></div>
        </div>
         <div class="layui-form-item" pane="">
          <label class="layui-form-label">年级：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_nj" runat="server" Text=""></asp:Label></div></div>
        </div>
     <div class="layui-form-item" pane="">
          <label class="layui-form-label">班级名称：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_bjmc" runat="server" Text=""></asp:Label></div></div>
        </div>
         <div class="layui-form-item" pane="">
          <label class="layui-form-label">班主任：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_bzr" runat="server" Text=""></asp:Label></div></div>
        </div>
         <div class="layui-form-item" pane="">
          <label class="layui-form-label">班主任电话：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_bzrdh" runat="server" Text=""></asp:Label></div></div>
        </div>
         <div class="layui-form-item" pane="">
          <label class="layui-form-label">班主任QQ：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_bzrqq" runat="server" Text=""></asp:Label></div></div>
        </div>
        <div class="layui-form-item" style="text-align:center">
          <button class="layui-btn" id="backmain" onclick="javascript:history.go(-1);">返回操作首页</button>
        </div>

     </div>
        
     

      </form>
</div>        

    
  <!--标签框架over-->

        </div>


    <script type="text/javascript" src="../nradmingl/plugins/layui/layui.js"></script>
    <script src="../b_js/app/xsjbxx.js"></script>



</body>
</html>

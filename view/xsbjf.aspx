<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xsbjf.aspx.cs" Inherits="view_xsbjf" %>


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
          
            <i class="layui-icon">&#xe602;</i>选择费用
            <span style="float:right;" id="btnback">            				
                 <a href="xswsjf.aspx" class="layui-btn layui-btn-small">
					返回
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
	        
       
             
<form class="layui-form layui-form-pane" runat="server">
      <asp:HiddenField ID="pk_sno" Value="" runat="server" />
      <asp:HiddenField ID="pk_batch_no" Value="" runat="server" />
      <asp:HiddenField ID="pk_affair_no" Value="" runat="server" />
      <asp:HiddenField ID="pk_staff_no" Value="" runat="server" />

     <asp:HiddenField ID="server_msg" Value="" runat="server" />
                   <asp:HiddenField ID="tmpdata" Value="" runat="server" />


     <div  id="contents" >   
         <div class="layui-form-item" pane="" style="margin-bottom:0px;">
         </div>

         <div class="layui-form-item" pane="" style="margin-bottom:0px;">
          <label class="layui-form-label">姓名：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_xm" runat="server" Text=""></asp:Label></div></div>
        </div>

         <div class="layui-form-item" pane="" style="margin-bottom:0px;">
          <label class="layui-form-label">身份证号：</label>
          <div class="layui-input-block">
           <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_sfzh" runat="server" Text=""></asp:Label></div>
           </div>
        </div>

         <div class="layui-form-item" pane="" style="margin-bottom:0px;">
               <label class="layui-form-label">专业名称：</label>
               <div class="layui-input-block">
                <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><asp:Label ID="xsxx_zymc" runat="server" Text=""></asp:Label></div>
                </div>
         </div>

         <div class="layui-form-item" pane="" style="margin-top:6px;margin-bottom:0px;">
             <label class="layui-form-label">款项</label>
               <div class="layui-input-block">
                <div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;">
                    <asp:Label ID="Label1" runat="server" Text="金额(单位:元)"></asp:Label></div>
                </div>
         </div>

 <%--        <div class="layui-form-item" pane="" style="margin-bottom: 0px; height: 36px">
             <div style="float: left; height: 36px; margin-top: 3px; width: 109px">
                 <input style="text-align: center;" type="checkbox" name="like1[write]" lay-skin="primary" title="教材费" checked="">
             </div>
             <div class="layui-input-inline">
                 <select name="modules" lay-verify="required" lay-search="">
                     <option value="">请选择</option>
                     <option value="1">layer</option>
                     <option value="2">form</option>
                     <option value="3">layim</option>

                 </select><div class="layui-unselect layui-form-select">
                     <div class="layui-select-title">
                         <input type="text" placeholder="请选择" value="" class="layui-input layui-unselect"><i class="layui-edge"></i></div>
                     <dl class="layui-anim layui-anim-upbit">
                         <dd lay-value="1" class="">layer</dd>
                         <dd lay-value="2" class="">form</dd>
                         <dd lay-value="3" class="">layim</dd>
                     </dl>
                 </div>
             </div>


         </div>--%>

     </div>

    <div style="clear:both;"></div>
    <div class="xsxx2" style="text-align:center;margin-top:10px;">
        <a href="javascript:void(0);" onclick="sure();" style="width:100px;" class="layui-btn layui-btn-small" id="sure" style="display:none;">确定</a>
        <a href="javascript:void(0);" onclick="cancel();" style="width:100px;"  class="layui-btn layui-btn-small" style="margin-left:18px;" id="cancel">返回</a>
    </div>
    </form>
</div>        

    
  <!--标签框架over-->

        </div>
        <script type="text/javascript" src="../b_js/jquery.min2.js"></script>
        <script type="text/javascript" src="../b_js/app/xsbjf.js"></script>

        <script type="text/javascript" src="../nradmingl/plugins/layui/layui.js"></script>

        <script>
            load();
		</script>
        <div id="TuitionClass" ref-data="none" style="display:none;margin-left:10px;margin-right:10px;">
            <div style="margin-top:10px;color:red;"><label>需要支付的金额总计：</label><label id="fee_sum">0</label><label>元</label></div>
        <div style="margin-top:10px;">
        <label><input id="xf_normal" name="TuitionClass" type="radio" value="" onclick="changesum();" />正常缴费 </label> 
        <label><input id="xf_green" name="TuitionClass" type="radio" value="" onclick="msg('提示', '绿色通道需要到校现场申请办理'); changesum();" />绿色通道 </label> 
        <label><input id="xf_loan" name="TuitionClass" type="radio" value="" onclick="msg('提示', '助学贷款需要到校现场申请办理'); changesum();" />助学贷款 </label> 
        </div>
        <div style="margin-top:10px;">            
            <div >
                <label style="color:red;font:bold;">提示：</label><br />
                <label style="color:orangered;font:bold;">“绿色通道”和“助学贷款”申请需要到校现场申请办理</label>
            </div>
        </div>
    </div>
</body>
</html>

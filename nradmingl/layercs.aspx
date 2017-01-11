<%@ Page Language="C#" AutoEventWireup="true" CodeFile="layercs.aspx.cs" Inherits="nradmingl_layercs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->

        <link rel="stylesheet" href="plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="plugins/global.css" media="all">
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
  
<button id="test1">小小提示层</button>

你必须先引入jQuery1.8或以上版本

  <script type="text/javascript" src="plugins/layui/layui.js"></script>
<script>

    //一般直接写在一个js文件中
    layui.use(['layer', 'form'], function () {
        var layer = layui.layer
  , form = layui.form();

        layer.msg('Hello World');
        layer.open({ type: 2, title: 'layer mobile页', shadeClose: true, shade: 0.8, area: ['380px', '90%'], content: 'http://layer.layui.com/mobile/' });
    });
</script> 
<a href="javascript:" onclick=" layer.open({  type: 1,  area: ['600px', '360px'],  shadeClose: true,   content: '自定义内容'  });">当然</a>，你也可以写在外部的js文件

    </div>
    </form>
</body>
</html>

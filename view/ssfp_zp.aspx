<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssfp_zp.aspx.cs" Inherits="view_ssfp_zp" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head runat="server">
    <title>宿舍照片显示</title>
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
          
            <i class="layui-icon">&#xe602;</i>预分配宿舍>>宿舍照片
            <span style="float:right">
            
				
                 <a href="javascript:history.go(-1);" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe603;</i>
				</a>
               </span>
				<div style="display:none">
                    <asp:Label ID="xh" runat="server" Text=""></asp:Label></div>
			</blockquote>
                
                <style>
                    .layui-form select{height:37px;width:100%;}
                    .layui-form input[type=checkbox], .layui-form input[type=radio], .layui-form select {display:inherit
                    }
                    label{
                        vertical-align: inherit;
                    }
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

 <!--顶部提示及导航OVER-->
        <div class="container">
                      

                <p style="margin-top:10px;margin-bottom:10px;">
                    <img src="../images/xsgysmall.jpg" alt="宿舍照片" class="xsgytp" style="margin-top: 18px; width: 90%; height: 90%" id="shuseImg" runat="server" />
                </p>
             </div>

    </form>
    
</body>
</html>

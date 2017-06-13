<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xsxx_pwd_reset.aspx.cs" Inherits="view_xsxx_pwd_reset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>学生管理</title>
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
    <link rel="stylesheet" href="../nradmingl/plugins/layui/css/layui-qiu.css" media="all" />
    <link rel="stylesheet" href="../nradmingl/plugins/global.css" media="all" />
    <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
    <link rel="stylesheet" href="../nradmingl/plugins/font-awesome/css/font-awesome.min.css" />
    <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
    <link rel="stylesheet" href="../nradmingl/plugins/table.css" />
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
    <style>
        .container {
            margin:10px auto;
            padding:10px;
        }
        .tishi{
            color:red;
            margin-bottom:5px;
            display:inline-block;
        }
    </style>
</head>
<body>
    <form id="form1" class="layui-form layui-form-pane" runat="server">    
        <asp:HiddenField ID="tsxx" runat="server" />   
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
         
        <div class="container">
            <asp:label runat="server" ID="ts" CssClass="tishi" Text="" >请输入高考报名号和身份证号，密码将重置为身份证号后八位。</asp:label>
            <div class="layui-form-item">
                <label class="layui-form-label">报名号：</label>
                <div class="layui-input-block">
                    <input type="text" name="xsxx_Test_NO" id="xsxx_Test_NO" value=""  autocomplete="off" class="layui-input" />
                </div>
            </div>

            <div class="layui-form-item">
                <label class="layui-form-label">身份证号：</label>
                <div class="layui-input-block">
                    <input type="text" name="xsxx_ID_NO" id="xsxx_ID_NO" value=""  autocomplete="off" class="layui-input"  />
                </div>
            </div>

            <div class="layui-form-item">
                <div class="layui-input-block">
                    <asp:button ID="btn_confirm" runat="server" cssClass="layui-btn layui-btn-normal" Text="提交"/>
                    &nbsp;&nbsp;
                    <input type="button" id="btn_back" class="layui-btn layui-btn-normal" value="返回" style="display:none" />
                </div>
            </div>
        </div>
        <!--引发ＬＡＹＵＩ前端必须ＪＳ-->
    <script type="text/javascript" src="../nradmingl/plugins/layui/layui.js"></script>
  
    <!--引发ＬＡＹＵＩ前端必须ＪＳ　ＯＶＥＲ-->
        <script>
            layui.use(['layer', 'form', 'jquery'], function () {
                var layer = layui.layer
      , form = layui.form();
                var $ = layui.jquery;
                if ($("#tsxx").val() != "") {
                    parent.layer.open({ content: $("#tsxx").val(), title: '提示信息(30秒后自动关闭)', btn: ['关闭'], time: 30000 });
                    $("#tsxx").value = "";
                }
                $('#btn_back').on('click', function () {
                    window.history.go(-1);
                });
            });
            layui.config({
                base: 'plugins/layui/modules/'
            });
        </script>
    </form>
</body>
</html>

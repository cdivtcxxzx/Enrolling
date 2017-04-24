<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xsxx-extend.aspx.cs" Inherits="view_xsxx_extend" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" content="编码" />
    <meta name="renderer" content="webkit" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="format-detection" content="telephone=no" />
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
    <link rel="stylesheet" href="../nradmingl/plugins/layui/css/layui.css" media="all" />
    <link rel="stylesheet" href="../nradmingl/plugins/font-awesome/css/font-awesome.min.css" />
    <style>
        .admin-main {
            margin: 5px 15px 10px 15px;
        }

        .p30 {
            width: 30%;
        }

        .p50 {
            width: 50%;
        }

        .p70 {
            width: 70%;
        }
    </style>
</head>
<body>
    <div class="admin-main">
        <blockquote class="layui-elem-quote">
            <i class="layui-icon">&#xe602;</i>迎新管理<i class="layui-icon">&#xe602;</i>完善个人信息
            <span style="float: right">
                <a href="javascript:window.location.go(-1);" class="layui-btn layui-btn-small">
                    <i class="layui-icon">&#xe603;</i>
                </a>
            </span>
        </blockquote>
        <form id="form1" class="layui-form layui-form-pane" runat="server">
            <asp:HiddenField ID="hidden_pk_sno" runat="server" />
            <asp:HiddenField ID="hidden_alert_msg" runat="server" />
            <div class="xsxx-wrapper">
                <div class="layui-form-item">
                    <label class="layui-form-label">姓名：</label>
                    <div class="layui-input-block">
                        <asp:Label ID="xsxx_xm" CssClass="layui-input" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="layui-form-item">
                    <label class="layui-form-label">性别：</label>
                    <div class="layui-input-block">
                        <asp:Label ID="xsxx_xb" CssClass="layui-input" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="layui-form-item">
                    <label class="layui-form-label">身份证号：</label>
                    <div class="layui-input-block">
                        <asp:Label ID="xsxx_sfzh" CssClass="layui-input" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="layui-form-item">

                    <label class="layui-form-label">联系电话：</label>
                    <div class="layui-input-block">
                        <asp:Label ID="xsxx_lxdh" CssClass="layui-input" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="layui-form-item">
                    <label class="layui-form-label">报名号：</label>
                    <div class="layui-input-block">
                        <asp:Label ID="xsxx_test" CssClass="layui-input" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <script src="../nradmingl/plugins/layui/layui.js"></script>
    <script src="../b_js/app/xsxx-extend.js"></script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="classmsg_detail.aspx.cs" Inherits="view_classmsg_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    <%--<link rel="stylesheet" href="../nradmingl/plugins/global.css" media="all" />--%>
    <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
    <link rel="stylesheet" href="../nradmingl/plugins/font-awesome/css/font-awesome.min.css" />
    <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
    <%--<link rel="stylesheet" href="../nradmingl/plugins/table.css" />--%>
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
    <style>
        .admin-main {
            margin: 5px 15px 10px 15px;
        }
    </style>
</head>
<body>
    <div class="admin-main">
        <form id="form1" runat="server">
            <div>
                <asp:Label runat="server" ID="msg_label" />
            </div>
        </form>
    </div>
</body>
</html>

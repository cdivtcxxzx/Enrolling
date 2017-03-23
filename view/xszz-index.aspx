<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xszz-index.aspx.cs" Inherits="view_xszz_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <title>学生自助操作首页</title>
    <link href="../bootstrap/3.3.4/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            margin-top: 10px;
            font-size: 14px;
        }

        .glyphicon {
            margin: 0 10px;
        }

        .col-xs-12 {
            min-height: 34px;
            background-color: #5FB878;
            margin-top: 12px;
            overflow: hidden;
            line-height: 34px;
            color: #ffffff;
        }

        a {
            color: #ffffff;
            text-decoration: none;
        }

        a:hover,a:active {
            text-decoration: none;
            color: #d4cfcf;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h4 class="text-center">学生事务操作首页导航</h4>
            <div class="col-xs-12 col-sm-6">
                <div class="=row">
                    <div class="col-xs-8"><i class="glyphicon glyphicon-tags"></i><a href="#">学生操作1</a></div>
                    <div class="col-xs-4"><span>已完成</span></div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-8"><span class="glyphicon glyphicon-tags"></span><a href="#">学生操作2</a></div>
                <div class="col-xs-4"><span>已完成</span></div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-8"><span class="glyphicon glyphicon-tags"></span><a href="#">学生操作3</a></div>
                <div class="col-xs-4"><span>已完成</span></div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-8"><span class="glyphicon glyphicon-tags"></span><a href="#">学生操作4</a></div>
                <div class="col-xs-4"><span>已完成</span></div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-8"><span class="glyphicon glyphicon-tags"></span><a href="#">学生操作5</a></div>
                <div class="col-xs-4"><span>已完成</span></div>
            </div>
            <div class="col-xs-12 col-sm-6">
                <div class="col-xs-8"><span class="glyphicon glyphicon-tags"></span><a href="#">学生操作6</a></div>
                <div class="col-xs-4"><span>已完成</span></div>
            </div>
        </div>
        <script src="../b_js/jquery.min2.js"></script>
        <script src="../bootstrap/3.3.4/js/bootstrap.min.js"></script>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="change_password.aspx.cs" Inherits="view_change_password" %>

<!DOCTYPE html>

<html lang="zh-cn">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" content="编码" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
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
    <form id="form1" runat="server" class="layui-form">
        <div class="admin-main">
<%--            <blockquote class="layui-elem-quote">
                <i class="layui-icon">&#xe602;</i>修改密码           
            </blockquote>--%>

           <blockquote class="layui-elem-quote">
            <i class="layui-icon">&#xe602;</i>自助报到<i class="layui-icon">&#xe602;</i>修改密码
            <span style="float: right" id="btnback">
                 <a href="xxzz_xsindex.aspx" class="layui-btn layui-btn-small">
                    <i class="layui-icon">&#xe603;</i>
                </a>
            </span>
        </blockquote>

            <div>
                <div class="layui-form-item">
                    <label class="layui-form-label">旧密码：</label>
                    <div class="layui-input-inline">
                        <input type="password" id="oldpwd" name="username" lay-verify="oldpass" placeholder="请输入旧密码" autocomplete="off" class="layui-input">
                    </div>
                </div>
                <div class="layui-form-item">
                    <label class="layui-form-label">新密码：</label>
                    <div class="layui-input-inline">
                        <input id="pwd1" type="password" name="password" lay-verify="pass" placeholder="8-10位字母数字特殊字符" autocomplete="off" class="layui-input">
                    </div>
                </div>
                <div class="layui-form-item">
                    <label class="layui-form-label">新密码：</label>
                    <div class="layui-input-inline">
                        <input id="pwd2" type="password" name="password" lay-verify="same" placeholder="重复输入新密码" autocomplete="off" class="layui-input">
                    </div>
                </div>
                <div style="text-align: center; float: left; margin-top: 10px; margin-left: 150px">
                    <span>
                        <a href="#" onclick="pwd();" class="layui-btn layui-btn-small" lay-submit="" lay-filter="demo1" id="">
                            <i class="layui-icon">&#xe605;</i> 提交
                        </a>
                    </span>
                </div>
                <div style="text-align: center; float: left; margin-top: 10px; margin-left: 5px">
                <span>
                    <a href="" class="layui-btn layui-btn-small">
                        <i class="layui-icon">&#x1006;</i> 重置
                    </a>
                </span>
            </div>
            </div>
        </div>
    </form>
            <script type="text/javascript" src="../b_js/jquery.min2.js"></script>
        <script type="text/javascript" src="../b_js/app/change_password.js"></script>

    <script type="text/javascript" src="../nradmingl/plugins/layui/layui.js"></script>
    <script>


        //layui.use(['form', 'layedit', 'laydate'], function () {
        //    var form = layui.form()
        //    , layer = layui.layer
        //    , layedit = layui.layedit
        //    , laydate = layui.laydate;
        //    //创建一个编辑器
        //    var editIndex = layedit.build('LAY_demo_editor');
        //    //自定义验证规则
        //    form.verify({
        //        oldpass: function (value) {
        //            if (value.length==0) {
        //                return '必须填写旧密码';
        //            }
        //        },
        //        same: function(){
        //            var pwd1 = document.getElementById("pwd1").value;
        //            var pwd2 = document.getElementById("pwd2").value;
        //            if (pwd1!= pwd2) {
        //                return '两次密码输入不一致';
        //            }
        //        },
        //        pass: [/^[0-9A-Za-z]{6,10}$/, '密码必须为字母或数字6到10位']
        //      , content: function (value) {
        //          layedit.sync(editIndex);
        //      }
        //    }); 
        //    监听提交
        //    form.on('submit(demo1)', function (data) {
        //        layer.alert(JSON.stringify(data.field), {
        //            title: '最终的提交信息'
        //        })
        //        return false;
        //    });

        //});


    </script>
</body>
</html>

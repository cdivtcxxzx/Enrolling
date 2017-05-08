<%@ Page Language="C#" AutoEventWireup="true" CodeFile="yxxcglsq.aspx.cs" Inherits="nradmingl_yxxcglsq" %>


<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->

    <link rel="stylesheet" href="plugins/layui/css/layui.css" media="all" />
    <link rel="stylesheet" href="plugins/global.css" media="all">
    <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
    <link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
    <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
    <link rel="stylesheet" href="plugins/table.css" />

    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->

</head>
<body style="background: #fff">
    <form id="form1" class="layui-form">

        <!--页面开始-->
        <div class="admin-main">

            <div class="layui-form-item">
                <label class="layui-form-label">迎新批次</label>
                <div class="layui-input-block">
                    <select name="freshbatch" id="freshbatch">
                    </select>
                </div>
            </div>
            <div>
                <table class="layui-table" id="affairlist">
                    <thead>
                        <tr>
                            <th>事务名称</th>
                            <th>类型</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>

            <div>
                <table class="layui-table" id="stafflist">
                    <thead>
                        <tr>
                            <th>事务名称</th>
                            <th>学生</th>
                            <th>操作员编号</th>
                            <th>操作员姓名</th>
                            <th>迎新年</th>
                            <th>授权操作学院</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>

            <blockquote class="layui-elem-quote">
            </blockquote>





            <br />
            &nbsp;<!--与后台配合的提示信息隐藏域--><input id="tsxx" type="text" runat="server" value="" style="display: none" />
            <input id="tsbox" type="text" runat="server" value="" style="display: none" />
            <!--与后台配合的提示信息隐藏域OVER-->

            <div class="admin-table-page" style="display: none">
                <div id="page" class="page">
                </div>
            </div>
        </div>

        <script type="text/javascript" src="../b_js/jquery.min2.js"></script>
        <script type="text/javascript" src="../b_js/app/yxxcglsq.js"></script>

        <!--引发ＬＡＹＵＩ前端必须ＪＳ-->
        <script type="text/javascript" src="plugins/layui/layui.js"></script>

        <!--引发ＬＡＹＵＩ前端必须ＪＳ　ＯＶＥＲ-->




        <script>
            // $('#code').qrcode(window.location.href);
            //鼠标滑过图片及文字提示时显示titop样式
            //	    $(document).tooltip({
            //	        items: "img, [titop], [title]", content: function () {
            //	            var element = $(this);
            //	            if (element.is("[titop]")) {	                var text = element.attr("alt");	                return "<img class='map'  src='" + text + "'>";	            }
            //	            if (element.is("[txttop]")) {return element.attr("title");             }

            //	        }  });
            //一般直接写在一个js文件中
            layui.use(['layer', 'form'], function () {
                var layer = layui.layer
                    , form = layui.form();

                //layer.msg('Hello World');
                //layer.open({ type: 2, title: 'layer mobile页', shadeClose: true, shade: 0.8, area: ['380px', '90%'], content: 'http://layer.layui.com/mobile/' });
            });


            layui.config({
                base: 'plugins/layui/modules/'
            });

            layui.use(['icheck', 'laypage'], function () {
                var $ = layui.jquery,
                    laypage = layui.laypage;
                $('input').iCheck({
                    checkboxClass: 'icheckbox_square-blue'
                });

                //page
                laypage({
                    cont: 'page',
                    pages: 10 //总页数
                    ,
                    groups: 5 //连续显示分页数
                    ,
                    jump: function (obj, first) {
                        //得到了当前页，用于向服务端请求对应数据
                        var curr = obj.curr;
                        if (!first) {
                            //layer.msg('第 '+ obj.curr +' 页');
                        }
                    }
                });

                $('#search').on('click', function () {
                    parent.layer.alert('你点击了搜索按钮')
                });


                $('.site-table tbody tr').on('click', function (event) {
                    var $this = $(this);
                    var $input = $this.children('td').eq(0).find('input');
                    $input.on('ifChecked', function (e) {
                        $this.css('background-color', '#EEEEEE');
                    });
                    $input.on('ifUnchecked', function (e) {
                        $this.removeAttr('style');
                    });
                    $input.iCheck('toggle');
                }).find('input').each(function () {
                    var $this = $(this);
                    $this.on('ifChecked', function (e) {
                        $this.parents('tr').css('background-color', '#EEEEEE');
                    });
                    $this.on('ifUnchecked', function (e) {
                        $this.parents('tr').removeAttr('style');
                    });
                });
                $('#selected-all').on('ifChanged', function (event) {
                    var $input = $('.site-table tbody tr td').find('input');
                    $input.iCheck(event.currentTarget.checked ? 'check' : 'uncheck');
                });

            });
        </script>

        <script type="text/javascript">
            //后台操作提示配合
            if (document.all("tsxx").value != "") {
                parent.layer.msg(document.all("tsxx").value);
                document.all("tsxx").value = "";
            }
            //tsxx纯为提示,tsbox 需点关闭或延时1分钟关闭
            if (document.all("tsbox").value != "") {
                parent.layer.open({ content: document.all("tsbox").value, title: '提示信息', btn: ['关闭'], time: 60000 });
                document.all("tsbox").value = "";
            }
            //批量操作

            function batchAudit(id) {
                var AuditVal = "";
                var bid = document.getElementsByName("BoxId");
                for (var i = 0; i < bid.length; i++) {
                    if (bid[i].checked == true) {
                        AuditVal = AuditVal + bid[i].value + ",";
                    }
                }
                if (AuditVal.length <= 0) {
                    parent.layer.msg("请先选择一条记录,在记录前打勾!");

                    return false;
                }
                else {
                    if (id == "btnDelete") {
                        document.getElementById("hdfWPBH").value = AuditVal;
                        return true;
                        //                             layer.open({ content: '您确认要批量删除这' + String(AuditVal.length / 4) + '条记录吗？'
                        //                                      , btn: ['确认', '取消']
                        //                                      , yes: function (index, layero) {
                        //                                          document.getElementById("hdfWPBH").value = AuditVal;
                        //                                          //此处写传给删除页面的参数
                        //                                          return true;
                        //                                          //使用AJAX回调删除
                        //                                          layer.close(index);

                        //                                      }, btn2: function (index, layero) {
                        //                                          return false;
                        //                                      }
                        //                                      , cancel: function () {
                        //                                          return false;
                        //                                      }
                        //                             });
                        return false;
                    }
                }
            }

        </script>

        <script>
            load();
        </script>


    </form>

    <div id="addstaff" style="display: none;">
            <div>
                <label>操作员姓名</label>
                <input type="text" id="name" value="" />
                <input type="button" id="find" value="查询" onclick="finduser()" />
            </div>
            <div>
                <select name="namelist" id="namelist" size="20" style="width: 95%;">
                </select>
            </div>
    </div>
    <div id="addstaff_content" style="display: none;">
        <table align="center">
            <tr>
                <td>姓名:<label id="username"></label></td>
                <td></td>
                <td >
                   
                </td>
            </tr>
          
            <tr>
                <td>可选学院</td>
                <td></td>
                <td>已选学院</td>
            </tr>
            <tr>
                <td>
                <select name="collegelist" id="collegelist" size="8">
                    <option>abc</option>
                    <option>abc</option>
                </select>
                </td>
                <td>
                  <input type="button" id="add" value=">>" onclick="additem()" />
                    <br /><br /><br />
                  <input type="button" id="remove" value="<<" onclick="removeitem()" />
                </td>
                <td>
                <select name="selectedcollegelist" id="selectedcollegelist" size="8" >
                    <option>abc</option>
                    <option>abc</option>
                </select>
                </td>
            </tr>
        </table>
    </div>
</body>

</html>

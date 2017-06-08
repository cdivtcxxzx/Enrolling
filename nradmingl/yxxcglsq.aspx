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

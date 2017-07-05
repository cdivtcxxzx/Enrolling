<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bzr_classmsg.aspx.cs" Inherits="bzr_classmsg" %>

<!DOCTYPE html>

<html lang="zh-cn">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" content="编码" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
     <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
        <link rel="stylesheet" href="plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="plugins/global.css" media="all" />
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css" />
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="plugins/table.css" />    
        <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
 
   
</head>
<body>
    <form id="form1" runat="server" class="layui-form">
              <asp:HiddenField ID="pk_staff_no" Value="" runat="server" />
            <asp:HiddenField ID="staff_name" Value="" runat="server" />
<%--              <asp:HiddenField ID="pk_batch_no" Value="" runat="server" />--%>
        <div class="admin-main">
            <blockquote class="layui-elem-quote">
                <i class="layui-icon">&#xe602;</i>班主任<i class="layui-icon">&#xe602;</i>通知           
            </blockquote>
            <div>                
                <div class="layui-form-item">
                    <!--迎新批次下拉列表-->
                    <div class="layui-inline">
<%--                        <label class="layui-form-label">批次：</label>--%>
                        <div class="layui-input-inline" style="width:400px;">
                            <table>
                                <tr>
                                    <td style="width:200px;">
                                        <select name="batchlist" id="batchlist">

                                        </select>
                                    </td>
                                    <td style="width:100px;padding-left:10px;">
                                        <input type="button" value="新建通知" onclick="addmsg();" class="layui-input"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                   
                    <table class="site-table table-hover" cellspacing="0" rules="all" border="1" id="msglist" style="border-collapse: collapse;">
                        <thead>
                            <tr>        
                                <th scope="col">标题</th>                        
                                <th scope="col">日期</th>
                                <th scope="col">作者</th>
                                <th scope="col">班级</th>
                                <th scope="col"></th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
                    </table>

                </div>
            </div>
        </div>

        <div id="newmsg" style="display: none;">
            <div class="layui-form-item">
                <label class="layui-form-label" style="padding:0px 0px;">标题</label>
<%--                <div class="layui-input-inline" style="width:400px;">--%>
                    <div class="layui-input-inline" style="width:80%;">
                    <input type="text" id="new_title" value=""  class="layui-input" />
                </div>
            </div>
            <div  class="layui-form-item">
                <label class="layui-form-label" style="padding:0px 0px;">内容</label>
<%--                <div class="layui-input-inline" style="width:400px;">--%>
                    <div class="layui-input-inline" style="width:80%;">
                    <textarea rows="10" col="300" id="new_content" class="layui-textarea"></textarea>
                </div>
            </div>
        </div>                    
    </form>
        <script type="text/javascript" src="../b_js/jquery.min2.js"></script>
        <script type="text/javascript" src="../b_js/app/bzr_classmsg.js"></script>
        <script type="text/javascript" src="plugins/layui/layui.js"></script>

    <script>
        load();
    </script>

    <div id="classlist_win" style="display: none;">
        <div  id="classlist" style="text-align:center;margin-top:10px;margin-bottom:10px;">
        </div>
    </div>
</body>
</html>

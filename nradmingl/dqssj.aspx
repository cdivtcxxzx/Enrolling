<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dqssj.aspx.cs" Inherits="nradmingl_Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入寝室数据</title>
    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->

        <link rel="stylesheet" href="plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="plugins/global.css" media="all">
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="plugins/table.css" />
    
     <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
    

</head>
<body>
    <style>
        .layui-form-radio i:hover, .layui-form-radioed i {
            color: #196BAB;
        }
        .cuowu{
            color:#ff0000;
        }
        .zhengque{
            color:#148f35;
        }
    </style>
    <form id="form1" class="layui-form" runat="server">
        <div class="admin-main">
            <blockquote class="layui-elem-quote">导入数据结果</blockquote>
            <div class="layui-form-item" style="float: right">

                <input type="radio" name="sex" value="男" title="查看错误项" checked="">
                <input type="radio" name="sex" value="女" title="查看所有信息">
            </div>

            <div>
                <table class="site-table table-hover" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse: collapse;">
                    <tbody>
                        <tr>
                            <th scope="col">序号</th>
                            <th scope="col">房间编号</th>
                            <th class="hidden-xs" scope="col">性别</th>
                            <th scope="col" class="hidden-xs">房间类型</th>
                            <th class="hidden-xs" scope="col">房间人数</th>
                            <th class="hidden-xs" scope="col">床位唯一编号</th>
                            <th class="hidden-xs" scope="col">床位位置说明</th>
                            <th class="hidden-xs" scope="col">班级名称</th>
                            <th scope="col">数据导入状态</th>
                            <th scope="col">说明</th>
                        </tr>
                        <tr>
                            <td>1</td>
                            <td>一宿101</td>
                            <td class="hidden-xs">女</td>
                            <td class="hidden-xs">普通6人间</td>
                            <td class="hidden-xs">6</td>
                            <td class="hidden-xs">10101</td>
                            <td class="hidden-xs">下铺靠窗</td>
                            <td class="hidden-xs">信号1701班</td>
                            <td class="layui-icon" style="font-size: 22px; color: #ff0000;">&#x1006;</td>
                            <td class="cuowu">楼层错误</td>
                        </tr>
                        <tr>
                            <td>2</td>
                            <td>一宿101</td>
                            <td class="hidden-xs">女</td>
                            <td class="hidden-xs">普通6人间</td>
                            <td class="hidden-xs">10</td>
                            <td class="hidden-xs">10102</td>
                            <td class="hidden-xs">上铺靠窗</td>
                            <td class="hidden-xs">物流1701班</td>
                            <td class="layui-icon" style="font-size: 22px; color: #ff0000;">&#x1006;</td>
                            <td class="cuowu">房间人数错误</td>
                        </tr>
                        <tr>
                            <td>3</td>
                            <td>二宿211</td>
                            <td class="hidden-xs">男</td>
                            <td class="hidden-xs">普通6人间</td>
                            <td class="hidden-xs">6</td>
                            <td class="hidden-xs">20111</td>
                            <td class="hidden-xs">上铺靠门</td>
                            <td class="hidden-xs">运输1703班</td>
                            <td class="layui-icon" style="font-size: 22px; color: #ff0000;">&#x1006;</td>
                            <td class="cuowu">楼栋错误</td>
                        </tr>
                        <tr>
                            <td>4</td>
                            <td>三宿605</td>
                            <td class="hidden-xs">女</td>
                            <td class="hidden-xs">普通6人间</td>
                            <td class="hidden-xs">6</td>
                            <td class="hidden-xs">60605</td>
                            <td class="hidden-xs"></td>
                            <td class="hidden-xs">信号1704班</td>
                            <td class="layui-icon" style="font-size: 22px; color: #ff0000;">&#x1006;</td>
                            <td class="cuowu">床铺位置错误</td>
                        </tr>
                        <tr>
                            <td>5</td>
                            <td>三宿105</td>
                            <td class="hidden-xs">女</td>
                            <td class="hidden-xs">普通6人间</td>
                            <td class="hidden-xs">6</td>
                            <td class="hidden-xs">30105</td>
                            <td class="hidden-xs">下铺靠门</td>
                            <td class="hidden-xs">物流1702班</td>
                            <td class="layui-icon" style="font-size: 22px; color: #148f35;">&#xe605;</td>
                            <td class="zhengque">正确</td>
                        </tr>
                    </tbody>
                </table>


            </div>

        </div>
    </form>
    

    <!--引发ＬＡＹＵＩ前端必须ＪＳ-->
    <script type="text/javascript" src="plugins/layui/layui.js"></script>
  
    <!--引发ＬＡＹＵＩ前端必须ＪＳ　ＯＶＥＲ-->
    
	<script>
	    layui.use('form', function () {
	        var form = layui.form(); //只有执行了这一步，部分表单元素才会修饰成功

	        //……
	    });
</script>

             

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssgl.aspx.cs" Inherits="nradmingl_Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>宿舍管理</title>
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
        .layui-form-select dl dd.layui-this {
            background-color: #196BAB;
            color: #fff;
        }

    </style>
    <form id="form1" class="layui-form" runat="server">
    <div class="admin-main">
      <blockquote class="layui-elem-quote">
          <i class="layui-icon">&#xe602;</i>学生宿舍管理<i class="layui-icon">&#xe602;</i>宿舍信息
           <span style="float:right">
				<a href=""  class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe60e;</i><i class="layui-icon">一卡通更新</i>
				</a>
                 <a href="" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe61d;</i><i class="layui-icon">模板下载</i>
				</a>
                <a href="" class="layui-btn layui-btn-small">
			        <i class="layui-icon">&#xe61e;</i><i class="layui-icon">导入寝室数据</i>
				</a>
		  </span>       
      </blockquote>
        <div>
            <div class="layui-form-item">
                <div class="layui-input-inline">
                    <select name="quiz1">
                        <option value="">请选择校区</option>
                        <option value="" selected="">天府校区</option>
                    </select><div class="layui-unselect layui-form-select">
                        <div class="layui-select-title">
                            <input type="text" placeholder="" value="" readonly="" class="layui-input layui-unselect"><i class="layui-edge"></i></div>
                        <dl class="layui-anim layui-anim-upbit">
                            <dd lay-value="" class="layui-this"></dd>
                            <dd lay-value="" class=""></dd>
                            <dd lay-value="" class=""></dd>
                        </dl>
                    </div>
                </div>
                <div class="layui-input-inline">
                    <select name="quiz2">
                        <option value="">请选择楼号</option>
                        <option value="1栋">1栋</option>
                        <option value="2栋">2栋</option>
                        <option value="3栋">3栋</option>
                        <option value="4栋">4栋</option>
                    </select><div class="layui-unselect layui-form-select">
                        <div class="layui-select-title">
                            <input type="text" placeholder="请选择楼号" value="" readonly="" class="layui-input layui-unselect"><i class="layui-edge"></i></div>
                        <dl class="layui-anim layui-anim-upbit">
                            <dd lay-value="1栋" class="">1栋</dd>
                            <dd lay-value="2栋" class=" layui-disabled">2栋</dd>
                            <dd lay-value="3栋" class="">3栋</dd>
                            <dd lay-value="4栋" class="">4栋</dd>
                        </dl>
                    </div>
                </div>
                <div class="layui-input-inline">
                    <select name="quiz3">
                        <option value="">请选择楼层</option>
                        <option value="1楼">1楼</option>
                        <option value="2楼">2楼</option>
                        <option value="3楼">3楼</option>
                        <option value="4楼">4楼</option>
                        <option value="5楼">5楼</option>
                        <option value="6楼">6楼</option>
                    </select><div class="layui-unselect layui-form-select">
                        <div class="layui-select-title">
                            <input type="text" placeholder="请选择楼层" value="" readonly="" class="layui-input layui-unselect"><i class="layui-edge"></i></div>
                        <dl class="layui-anim layui-anim-upbit">
                            <dd lay-value="1楼" class="">1楼</dd>
                            <dd lay-value="2楼" class="">2楼</dd>
                            <dd lay-value="3楼" class="">3楼</dd>
                            <dd lay-value="4楼" class="">4楼</dd>
                            <dd lay-value="5楼" class="">5楼</dd>
                            <dd lay-value="6楼" class="">6楼</dd>
                        </dl>
                    </div>
                </div>
            </div>
        </div>    
  <div>   
	<table class="site-table table-hover" cellspacing="0" rules="all" border="1" id="GridView1" style="border-collapse:collapse;">
		<tbody><tr>
			<th align="center" scope="col" style="width:2%;">
                      <div class="icheckbox_square-blue" style="position: relative;"><input type="checkbox" id="selected-all" name="selected-all" onclick="onclicksel();" style="position: absolute; opacity: 0;"><ins class="iCheck-helper" style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;"></ins></div>  
                </th><th scope="col" ><a href="javascript:__doPostBack('GridView1','Sort$序号')">序号</a></th><th class="hidden-xs" scope="col"><a href="javascript:__doPostBack('GridView1','Sort$lmmc')">公寓楼名称</a></th>
                <th scope="col"><a href="javascript:__doPostBack('GridView1','Sort$title')">楼层</a></th><th class="hidden-xs" scope="col"><a href="javascript:__doPostBack('GridView1','Sort$title')">房间名称</a></th>
                <th scope="col"><a href="javascript:__doPostBack('GridView1','Sort$author')">房间类型</a></th><th class="hidden-xs" scope="col"><a href="javascript:__doPostBack('GridView1','Sort$fabutime')">性别</a></th><th scope="col">管理操作</th>                
		</tr><tr>
			<td align="center">
            <div class="icheckbox_square-blue" style="position: relative;"><input id="BoxId" name="BoxId" class="icheck" value="497" type="checkbox" style="position: absolute; opacity: 0;"><ins class="iCheck-helper" style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;"></ins></div> 
            </td><td>1</td><td class="hidden-xs">1栋</td><td>1楼</td>
            <td >1101</td><td class="hidden-xs">六人间</td><td class="hidden-xs">女</td>
            <td>
            <a href="javascript:" onclick="parent.layer.open({  type: 2,  title: '宿舍信息－寝室详情',  shadeClose: true,  shade: 0.8,  area: ['100%', '90%'],  content: 'qsxq.aspx?id=497'});" txttop="txttop" class="layui-btn layui-btn-mini" title="寝室详情">查看详情</a>
            </td>
		</tr><tr>
			<td align="center">
            <div class="icheckbox_square-blue" style="position: relative;"><input id="BoxId" name="BoxId" class="icheck" value="497" type="checkbox" style="position: absolute; opacity: 0;"><ins class="iCheck-helper" style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;"></ins></div> 
            </td><td>2</td><td class="hidden-xs">2栋</td><td>5楼</td>
            <td >2507</td><td class="hidden-xs">六人间</td><td class="hidden-xs">男</td>
            <td>
            <a href="javascript:" onclick="parent.layer.open({  type: 2,  title: '宿舍信息－寝室详情',  shadeClose: true,  shade: 0.8,  area: ['100%', '90%'],  content: 'qsxq.aspx?id=497'});" txttop="txttop" class="layui-btn layui-btn-mini" title="寝室详情">查看详情</a>
            </td>            
		</tr><tr>
			<td align="center">
            <div class="icheckbox_square-blue" style="position: relative;"><input id="BoxId" name="BoxId" class="icheck" value="497" type="checkbox" style="position: absolute; opacity: 0;"><ins class="iCheck-helper" style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;"></ins></div> 
            </td><td>3</td><td class="hidden-xs">2栋</td><td>6楼</td>
            <td >2614</td><td class="hidden-xs">六人间</td><td class="hidden-xs">男</td>
            <td>
            <a href="javascript:" onclick="parent.layer.open({  type: 2,  title: '宿舍信息－寝室详情',  shadeClose: true,  shade: 0.8,  area: ['100%', '90%'],  content: 'qsxq.aspx?id=497'});" txttop="txttop" class="layui-btn layui-btn-mini" title="寝室详情">查看详情</a>
            </td>            
		</tr><tr>
			<td align="center">
            <div class="icheckbox_square-blue" style="position: relative;"><input id="BoxId" name="BoxId" class="icheck" value="497" type="checkbox" style="position: absolute; opacity: 0;"><ins class="iCheck-helper" style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;"></ins></div> 
            </td><td>4</td><td class="hidden-xs">3栋</td><td>4楼</td>
            <td >3409</td><td class="hidden-xs">六人间</td><td class="hidden-xs">女</td>
            <td>
            <a href="javascript:" onclick="parent.layer.open({  type: 2,  title: '宿舍信息－寝室详情',  shadeClose: true,  shade: 0.8,  area: ['100%', '90%'],  content: 'qsxq.aspx?id=497'});" txttop="txttop" class="layui-btn layui-btn-mini" title="寝室详情">查看详情</a>
            </td>            
		</tr><tr>
            <td colspan="8">
                <span style="float: left; padding-bottom: 8px; padding-top: 8px;" class="hidden-xs">每页<span id="GridView1_LabelPageSize">10</span>
                    条 &nbsp;&nbsp;</span><span style="float: left; padding-bottom: 8px; padding-top: 8px;">当前<span id="GridView1_LabelCurrentPage">1</span>
                        /<span id="GridView1_Label3">1</span>
                        页&nbsp;&nbsp;&nbsp;&nbsp;<a id="GridView1_LinkButtonFirstPage" class="aspNetDisabled">首页</a>
                        <a id="GridView1_LinkButtonPreviousPage" class="aspNetDisabled">上一页</a>
                        <a id="GridView1_LinkButtonNextPage" class="aspNetDisabled">下一页</a>
                        <a id="GridView1_LinkButtonLastPage" class="aspNetDisabled">尾页</a>
                        &nbsp;&nbsp;&nbsp;&nbsp;<input name="GridView1$ctl06$txt_go" type="text" id="GridView1_txt_go" class=" borderSolid1CCC" style="height: 16px; width: 32px;">
                        <a id="GridView1_LinkButtonGo" class="layui-btn layui-btn-mini" href="javascript:__doPostBack('GridView1$ctl06$LinkButtonGo','')">跳转</a></span><span class="hidden-xs" style="float: right; padding-bottom: 8px; padding-top: 8px;">&nbsp;&nbsp;&nbsp;每页显示<input name="GridView1$ctl06$PageSize_Set" type="text" id="GridView1_PageSize_Set" class=" borderSolid1CCC" style="height: 16px; width: 32px;">条<a id="GridView1_buttion2" class="layui-btn layui-btn-mini" href="javascript:__doPostBack('GridView1$ctl06$buttion2','')">设置</a></span><span style="float: right; padding-bottom: 8px; padding-top: 8px;"><b>总记录:4条</b>&nbsp;</span>
            </td>
		</tr>
	</tbody></table>


</div>
 
  </div> 
    </form>
    

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

</body>
</html>

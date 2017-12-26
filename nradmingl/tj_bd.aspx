<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation = "false"  CodeFile="tj_bd.aspx.cs" Inherits="nradmingl_tj_bd" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>报到统计</title>
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
        select
        {
            display:inline-block;
            height: 37px;
        }

    </style>
    <form id="form1"  runat="server">
    <div class="admin-main">
      <blockquote class="layui-elem-quote">&nbsp;<span class=" hidden-xs">
          <i class="layui-icon">&#xe602;</i>报到统计<i class="layui-icon">&#xe602;</i>按院系查看</span>
           <span style="float:right">

            <!--调用C#原生按钮设置样式举例OVER-->
 <%--               <a href="#" class="layui-btn layui-btn-small hidden-xs">
					<i class="layui-icon">&#xe630;</i> 一卡通更新
				</a>
             --%><a href="tj_bd.aspx" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#x1002;</i> 刷新
				</a>

               
                <asp:LinkButton CssClass="layui-btn layui-btn-small" name="exportexcel1" onclick="exportexcel"  txttop="txttop" ToolTip="数据导出" ID="LinkButton13" runat="server"    Text='' ><i class="layui-icon">&#xe61e;</i>导出<span class=" hidden-xs">数据</span></asp:LinkButton>

		  </span>       
      </blockquote>

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
        
        <div>
            <div class="layui-form-item">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                  <ContentTemplate>
                                      <asp:Label ID="g_ts" runat="server"  Font-Size="Larger"></asp:Label>
                    </ContentTemplate></asp:UpdatePanel>

            </div>
        </div>    
  <div>   
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>
                      
  <asp:HiddenField ID="hdfWPBH" runat="server" />
    <asp:GridView ID="GridView1"  CssClass="site-table table-hover"   AutoGenerateColumns="False"   runat="server">
    <Columns>
    <asp:BoundField DataField="学院名称" HeaderText="学院名称" SortExpression="学院名称" 
            InsertVisible="False" ReadOnly="True"/>
            <asp:BoundField DataField="录取人数"  HeaderText="录取人数" SortExpression="录取人数" 
            InsertVisible="False" ReadOnly="True"/>
            <asp:BoundField DataField="网上注册"  
            ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"  HeaderText="网上注册"  
           SortExpression="网上注册"/>
    
    <asp:BoundField DataField="缴费总数"  
            ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"  HeaderText="缴费总数"  
           SortExpression="缴费总数"/>
    
             <asp:BoundField DataField="网上缴学费" HeaderText="网上缴学费"  
            ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs" SortExpression="网上缴学费"/>
             <asp:BoundField DataField="现场缴费" HeaderText="现场缴费"  
            ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs" SortExpression="现场缴费"/>

            <asp:TemplateField ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs" HeaderText="申请助学贷款">
        
            <ItemTemplate>
            <%# zxdk(Eval("申请助学贷款").ToString(), Eval("缴费总数").ToString(), Eval("网上缴学费").ToString(), Eval("现场缴费").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>


       <%--    <asp:BoundField DataField="缴费总数" HeaderText="缴费总数"  
            ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs" SortExpression="缴费总数"/>--%>
           
    <asp:BoundField DataField="选寝人数"  
            ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"  HeaderText="选寝人数"  
           SortExpression="选寝人数"/>
             <asp:BoundField DataField="完善信息"  
            ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"  HeaderText="完善信息"  
           SortExpression="完善信息"/>
            <asp:BoundField DataField="来校报到" HeaderText="来校报到"  
           SortExpression="来校报到"/>
             <asp:TemplateField HeaderText="报到率">
        
            <ItemTemplate>
            <font color=blue><b><%# bdl(Eval("来校报到").ToString(), Eval("录取人数").ToString())%></b></font>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>

            

              <asp:TemplateField  HeaderText="查看班级详情"  >
            
            <ItemTemplate>
         <a href="javascript: " onclick="parent.layer.open({  type: 2,  title: '',  shadeClose: true,  shade: 0.8,  area: ['80%', '98%'],  content: 'tj_yx.aspx?id=<%# Eval("yxdm").ToString() %>&view=qxsh',cancel: function(index, layero){ location.reload(true)  }});"  txttop="txttop" class="layui-btn layui-btn-mini"  title="查看班级详情">查看详情</a>  
    
       </ItemTemplate></asp:TemplateField></Columns>

    </asp:GridView>

    
    <div style="width:90%">
    提示：<br />
    缴费总数=网上缴学费+助学贷款学生缴其它费+现场缴费;<br />
    申请助学贷款后括号中为在网上缴纳其它费用的人数;
    

    </div>
           


   </ContentTemplate></asp:UpdatePanel>
   

      
             








      <!--与后台配合的提示信息隐藏域-->
            <input id="tsxx" type="text" runat="server" value="" style="display:none" />
            <input id="tsbox" type="text" runat="server" value="" style="display:none" />
			<!--与后台配合的提示信息隐藏域OVER-->
            
			<div class="admin-table-page" style="display:none">
				<div id="page" class="page">
				</div>
			</div>
		</div>
     
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
    </form>
</body>
</html>

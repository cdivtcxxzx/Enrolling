<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Class_Counseller.aspx.cs" Inherits="nradmingl_Class_Counseller" %>

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
    <style>
         .inde{
            border:0px!important;

        }
         .layui-form-item label{
              width:100px;
         }
        

    </style>
</head>
<body>
    <div class="admin-main">
    <form id="form1" runat="server" class="layui-form">
    
    <div runat="server" id="div1">
        <div class="layui-form-item">
                <label class="layui-form-label">班级：</label>
                <div class="layui-input-inline">
                    <asp:Label runat="server" Visible="false" ID="LB_Class_NO"></asp:Label><asp:Label runat="server" ID="LB_Class" CssClass="layui-input inde" ></asp:Label>
                </div>
            </div>
        <%--<asp:DropDownList runat="server" ID="DDL_class" DataSourceID="ObjectDataSource1" DataTextField="Name" DataValueField="PK_Class_NO"></asp:DropDownList>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetClassDT" TypeName="GZJW"></asp:ObjectDataSource>--%>
        <%--班级：
        <br />--%>
    </div>
        <div runat="server" id="div_info" visible="false">
            <div class="layui-form-item">
                    <label class="layui-form-label">辅导员帐号：</label>
                    <div class="layui-input-inline">
                        <asp:Label runat="server" ID="LB_yhid" CssClass="layui-input inde"></asp:Label>
                    </div>
                </div>
            <div class="layui-form-item">
                    <label class="layui-form-label">姓名：</label>
                    <div class="layui-input-inline">
                        <asp:Label runat="server" ID="LB_coun" CssClass="layui-input inde"></asp:Label>
                    </div>
                </div>
            <div class="layui-form-item">
                    <label class="layui-form-label">电话：</label>
                    <div class="layui-input-inline">
                        <asp:TextBox runat="server" ID="TB_phone" CssClass="layui-input"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="rev_phone" runat="server" ValidationExpression="\d{3}-\d{8}|\d{4}-\{7,8}" ControlToValidate="TB_phone" ErrorMessage="请输入正确的手机号"></asp:RegularExpressionValidator>--%>
                    </div>
                </div>
            <div class="layui-form-item">
                    <label class="layui-form-label">QQ：</label>
                    <div class="layui-input-inline">
                        <asp:TextBox runat="server" ID="TB_qq" CssClass="layui-input"></asp:TextBox>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="[1-9][0-9]{4,}" ControlToValidate="TB_qq" ErrorMessage="请输入正确的QQ号"></asp:RegularExpressionValidator>--%>
                    </div>
                </div>
            <div style="margin-left:180px">
                <div style="text-align: center; float: left; margin-top: 10px; margin-left: 5px">
                    <span>
                        <asp:Button runat="server" ID="BT_ok" Text="确定" OnClick="BT_ok_Click" CssClass="layui-btn layui-btn-small" />
                    </span>
                </div>
                <div style="text-align: center; float: left; margin-top: 10px; margin-left: 5px">
                    <span>
                        <asp:Button runat="server" ID="BT_reset" Text="重选" OnClick="BT_reset_Click" CssClass="layui-btn layui-btn-small" />
                    </span>
                </div>
            </div>
            <div class="layui-form-item">
               <div style="margin-left:180px;margin-top:5px;">
                        <asp:Label runat="server" ID="LB_tips" CssClass="layui-input-inline"></asp:Label>
               </div>
             </div>
           <%-- 辅导员帐号：<asp:Label runat="server" ID="LB_yhid"></asp:Label>
        <br />
            姓名：<asp:Label runat="server" ID="LB_coun"></asp:Label>
        <br />电话：<asp:TextBox runat="server" ID="TB_phone"></asp:TextBox>
        <br />QQ：<asp:TextBox runat="server" ID="TB_qq"></asp:TextBox>
        <br /><asp:Button runat="server" ID="BT_ok" Text="确定" OnClick="BT_ok_Click" /> <asp:Button runat="server" ID="BT_reset" Text="重选" OnClick="BT_reset_Click"/>
        <br /><asp:Label runat="server" ID="LB_tips"></asp:Label>--%>
        </div>
        <div runat="server" id="div_ss" class="layui-form-item">
                <label class="layui-form-label">班主任：</label>
                <div class="layui-input-inline">
                    <asp:TextBox runat="server" ID="TB_key" CssClass="layui-input" ToolTip="输入账号、姓名或部门等信息"></asp:TextBox>
                </div>
                <div  class="layui-inline" style="line-height:38px;">
                    <asp:LinkButton runat="server" ID="Button_Search" CssClass="layui-btn layui-btn-small" Text="搜索"><i class="layui-icon">&#xe615;</i></asp:LinkButton>
                </div>
            
        <%--<div runat="server" id="div_ss">
        <asp:TextBox runat="server" ID="TB_key"></asp:TextBox><asp:Button ID="Button_Search" runat="server" CssClass="click" Text="搜索" 
            />
        <br />--%>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  AllowPaging="true" AllowSorting="true"
            DataSourceID="SqlDataSource1"  CssClass="layui-table table-hover" OnRowCommand="GridView1_RowCommand" PageSize="10" EmptyDataText="请通过搜索设置辅导员!" >
            
<Columns>
<asp:BoundField   DataField="yhid"   HeaderText="帐号" 
        SortExpression="yhid">
<HeaderStyle></HeaderStyle>
    </asp:BoundField>
<asp:BoundField   DataField="xm"  HeaderText="姓名" 
        SortExpression="xm">
<HeaderStyle ></HeaderStyle>
    </asp:BoundField>
<%--<asp:BoundField   DataField="sfzjh"   HeaderText="身份证号" 
        SortExpression="sfzjh">

    </asp:BoundField>--%>
    <asp:BoundField   DataField="uumzw"   HeaderText="所属部门" 
        SortExpression="uumzw">
    </asp:BoundField>

    <asp:ButtonField  Text="选择"  CommandName="tianjia" ButtonType="Button"  ControlStyle-CssClass="layui-btn layui-btn-mini" HeaderStyle-Width="15" Visible="true" HeaderText="操作"/>
   <%-- <asp:ButtonField HeaderText="" Text="转系"  CommandName="toAnotherDep" ButtonType="Button" HeaderStyle-Width="15"/>--%>
    <%--<asp:TemplateField><ItemTemplate><asp:Button Text="换专业" OnClientClick="" /></ItemTemplate></asp:TemplateField>--%>
    
</Columns>
</asp:GridView>
<asp:SqlDataSource 
        ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
           
        
            SelectCommand="SELECT yhid,xm,uumzw from yonghqx where yhid like '%' + @text + '%' or xm like '%' + @text + '%' or uumzw like '%' + @text + '%'">
    <SelectParameters>
        <asp:ControlParameter ControlID="TB_key" Name="text" PropertyName="Text" />
    </SelectParameters>
            </asp:SqlDataSource>
    </div>
    
    </form>
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
                  parent.layer.open({ content: document.all("tsbox").value ,title:'提示信息',btn: ['关闭'],time:60000});
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

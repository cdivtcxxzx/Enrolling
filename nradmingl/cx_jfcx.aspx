<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cx_jfcx.aspx.cs" Inherits="nradmingl_cx_jfcx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>学生缴费情况查询</title>
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
        .layui-input, .layui-textarea {
    display: inline;
    width: 50%;
        }

    </style>
    <form id="form1"  runat="server">
    <div class="admin-main">
      <blockquote class="layui-elem-quote">&nbsp;<span class=" hidden-xs">
          <i class="layui-icon">&#xe602;</i>学生缴费情况查询<i class="layui-icon">&#xe602;</i>按院系班级</span>
           <span style="float:right">

            <!--调用C#原生按钮设置样式举例OVER-->
 <%--               <a href="#" class="layui-btn layui-btn-small hidden-xs">
					<i class="layui-icon">&#xe630;</i> 一卡通更新
				</a>
             --%><a href="cx_jfcx.aspx" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#x1002;</i> 刷新
				</a>

               
                <asp:LinkButton CssClass="layui-btn layui-btn-small" name="exportexcel1" 
              onclick="exportexcel"  txttop="txttop" ToolTip="数据导出" ID="LinkButton13" 
              runat="server"    Text='' Visible="False" ></asp:LinkButton>

		  </span>       
      </blockquote>

          
        <div>
            <div class="layui-form-item">
                      
                
                    
                      <asp:DropDownList ID="yx"  Font-Size="Medium" runat="server" DataSourceID="SqlDataSource6" DataTextField="yxmc" DataValueField="yxdm" AutoPostBack="True" OnSelectedIndexChanged="yx_SelectedIndexChanged">
                <asp:ListItem Selected="True">全部院系</asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnString %>" SelectCommand="select '0' yxdm,'全部院系' yxmc union(SELECT [YXDM], [YXMC] FROM [DM_YUANXI] WHERE (([isjx] = @isjx) AND ([zt] = @zt))) ORDER BY [YXDM]">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="isjx" Type="Boolean" />
                    <asp:Parameter DefaultValue="true" Name="zt" Type="Boolean" />
                </SelectParameters>
            </asp:SqlDataSource>

                      <asp:DropDownList ID="bj" runat="server" DataSourceID="SqlDataSource5" 
                        DataTextField="Name" DataValueField="PK_Class_NO" AutoPostBack="True" 
                        onselectedindexchanged="bj_SelectedIndexChanged" Font-Size="Medium">
                        <asp:ListItem Selected="True" Value=" ">全部班级</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                        SelectCommand="select ' ' PK_Class_NO,'全部班级' Name union ( SELECT DISTINCT Fresh_Class.PK_Class_NO, Fresh_Class.Name FROM         Fresh_Class LEFT OUTER JOIN                      Fresh_SPE ON Fresh_Class.FK_SPE_NO = Fresh_SPE.PK_SPE  where Fresh_SPE.FK_College_Code=@yxdm)  ORDER BY [PK_Class_NO]">
                     <SelectParameters>
                            <asp:ControlParameter ControlID="yx" Name="yxdm" 
                                PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource><asp:TextBox ID="TextBox1"  CssClass="layui-input"  placeholder="输入姓名、高考报名号、身份证号"   runat="server"></asp:TextBox>
                      <span >
                      <asp:LinkButton ID="cx" runat="server" CssClass="layui-btn " 
                          name="exportexcel1" onclick="exportexcel" Text="缴费查询" ToolTip="数据查询" 
                          txttop="txttop"></asp:LinkButton>
                      </span>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="g_ts" runat="server" Font-Size="Larger"></asp:Label>
                  

            </div>
        </div>    
  <div>   
                    
  <asp:HiddenField ID="hdfWPBH" runat="server" />
    <asp:GridView ID="GridView1" CssClass="site-table table-hover"  runat="server">
    </asp:GridView>



      
             








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

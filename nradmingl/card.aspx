<%@ Page Language="C#" AutoEventWireup="true" CodeFile="card.aspx.cs" Inherits="nradmingl_card" %>


<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
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
<body style="background:#fff">
    <form id="form1" runat="server">
     <!--页面开始-->
     	<div class="admin-main">
         
		
          <blockquote class="layui-elem-quote">
          
          
          
<asp:textbox runat="server" ID="textbox2"   Height="40px"  CssClass="layui-input displayi " placeholder="请刷卡查询" Font-Size="Large"  
            Width="169px"></asp:textbox>
    
<asp:textbox runat="server" ID="textbox1"    Height="40px"  CssClass="layui-input displayi " Font-Size="Large" 
            Width="165px"></asp:textbox>
        
             <asp:Button ID="Button4"  CssClass="layui-btn" onclick="Button1_Click"   runat="server" Text="查询绑定信息" />
           
    <span style="float:right">  <a href="" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#x1002;</i>
				</a></span>
			</blockquote>
			
					<blockquote class="layui-elem-quote">
          
    <style>.displayi{display: inline;}</style>
       <asp:textbox runat="server" 
                    ID="textbox3" Font-Size="Large"  Width="169px"  Height="40px"  CssClass="layui-input displayi " placeholder="输入姓名查询"></asp:textbox>
       
      
    
    
       
      
                <asp:Button CssClass="layui-btn"  ID="Button3" nclick="Button1_Click3"  runat="server" 
                            Text="查询编号" onclick="Button3_Click1" />
           <asp:Label ID="Label1" runat="server"></asp:Label>
    
    
       
      
   </blockquote>



<asp:HiddenField ID="hdfWPBH" runat="server" />
      
    <asp:GridView  OnRowCommand="GridView1_RowCommand"  ID="GridView1"  OnDataBound="GridView1_DataBound"  runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" CssClass="site-table table-hover"
            EmptyDataText="未获取到数据!" 
            AllowPaging="True" AllowSorting="True">
    <Columns>
    <asp:TemplateField>
                <HeaderTemplate>
                      <input type="checkbox"  id="selected-all" name="selected-all" onclick="onclicksel();" />  
                </HeaderTemplate>
                <ItemTemplate>
                     <input id="BoxId" name="BoxId"  class="icheck" value='<%#(Convert.ToString(Eval("user_serial")))%>' type="checkbox" /> 
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="2%"  HorizontalAlign="Center" />
            </asp:TemplateField>
    <asp:BoundField DataField="序号" HeaderText="序号" SortExpression="序号"/>
    <asp:BoundField DataField="编号" HeaderText="编号"  SortExpression="编号"/>
    <asp:BoundField DataField="姓名" HeaderText="姓名"  SortExpression="姓名"/>
     
    <asp:BoundField DataField="登陆名" HeaderText="登陆名"   ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs" SortExpression="登陆名" />
    <asp:BoundField DataField="部门" HeaderText="部门"   SortExpression="部门" />
    <asp:BoundField DataField="绑定卡号" HeaderText="绑定卡号"   SortExpression="绑定卡号" />

    <%--<asp:BoundField DataField="titleurl" HeaderText="标题"  HtmlEncode="false" HtmlEncodeFormatString="false" />--%>
        <asp:BoundField DataField="身份证"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs" HeaderText="身份证" 
            SortExpression="身份证" />
        <asp:TemplateField HeaderText="照片"  SortExpression="title">
        
            <ItemTemplate>
            <a href="javascript:;" onclick="parent.layer.open({  id:1,type: 2,  title: '用户照片',  shadeClose: true,  shade: 0.8,  area: ['285px', '318px'],time: 60000,  content: 'yktphoto.aspx?url=<%# Eval("user_serial").ToString()%>'});""><%# imagestu(Eval("user_serial").ToString())%></a>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
        <asp:TemplateField HeaderText="" >
                 
                <HeaderTemplate>操作

                <%--<a onclick="return batchAudit(this.id);" id="btnDelete" href="javascript:__doPostBack('btnDelete','')"><span id="plcz" runat="server">点此批量发放毕业证</span></a>--%>
                </HeaderTemplate>
                <ItemTemplate>
             <a href="javascript: " onclick="parent.layer.open({  type: 2,  title: '查看用户信息详情－<%# Eval("姓名").ToString() %>',  shadeClose: true,  shade: 0.8,  area: ['100%', '90%'],  content: 'cardxq.aspx?userid=<%# Eval("user_serial").ToString() %>'});"  txttop="txttop" class="layui-btn layui-btn-mini"  title="查看详情">详情</a>
            </ItemTemplate>
                
                </asp:TemplateField>


    </Columns>
    <PagerTemplate>
<span style="float:left;padding-bottom: 8px;padding-top: 8px;" class="hidden-xs" >


            每页<asp:Label ID="LabelPageSize" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageSize %>"></asp:Label>
            条 &nbsp;&nbsp;</span><span style="float:left;padding-bottom: 8px;padding-top: 8px;"  >当前<asp:Label ID="LabelCurrentPage" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageIndex+1 %>"></asp:Label>
            /<asp:Label ID="Label3" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageCount %>"></asp:Label>
            页&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButtonFirstPage" runat="server" CommandArgument="First"
                CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=0 %>">首页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonPreviousPage" runat="server" CommandArgument="Prev"
                CommandName="Page" Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=0 %>">上一页</asp:LinkButton>
                <%if (GridView1.PageCount >= 8 && GridView1.PageCount - GridView1.PageIndex >= 8)
                  {
                     
                       %>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+2 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+2 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+3 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+3 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument="<%# ((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page"><%# ((GridView)Container.NamingContainer).PageIndex+4 %></asp:LinkButton>
               <asp:LinkButton ID="LinkButton5" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+5 %>"
                CommandName="Page"><%#  ((GridView)Container.NamingContainer).PageIndex+6 %></asp:LinkButton> 
                <asp:LinkButton ID="LinkButton6" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+6 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+6 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton7" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+7 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+7 %></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton9" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+8 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+8 %></asp:LinkButton>

                <%}
                  else if (GridView1.PageCount >= 8 && GridView1.PageCount - GridView1.PageIndex >= 5)
                  { %>
                    <asp:LinkButton ID="LinkButton8" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+2%>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+2 %></asp:LinkButton>
                <asp:LinkButton ID="LinkButton10" runat="server" CommandArgument="<%#((GridView)Container.NamingContainer).PageIndex+3 %>" CommandName="Page"> <%#  ((GridView)Container.NamingContainer).PageIndex+3 %></asp:LinkButton>
                  <asp:LinkButton ID="LinkButton11" runat="server" CommandArgument="<%#  ((GridView)Container.NamingContainer).PageIndex+4 %>"
                CommandName="Page" ><%#  ((GridView)Container.NamingContainer).PageIndex+4 %></asp:LinkButton>
                    <%}%>

            <asp:LinkButton ID="LinkButtonNextPage" runat="server" CommandArgument="Next" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">下一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonLastPage" runat="server" CommandArgument="Last" CommandName="Page"
                Enabled="<%# ((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">尾页</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txt_go" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>

            <asp:LinkButton ID="LinkButtonGo" runat="server" class="layui-btn layui-btn-mini" Text="跳转" OnClick="LinkButtonGo_Click" /></span><span class="hidden-xs" style="float:right;padding-bottom: 8px;padding-top: 8px;">&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  class="layui-btn layui-btn-mini" Text="设置"   OnClick="PageSize_Go" /></span><span style="float:right;padding-bottom: 8px;padding-top: 8px;"><b>总记录:<%#ViewState["count"].ToString()%>条</b>&nbsp;</b></font></span>
        </PagerTemplate>
    </asp:GridView>
        <asp:SqlDataSource onselected="SqlDataSource1_Selected"   ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:yktSqlConnString %>" 
            SelectCommand="SELECT TOP 500 row_number() over (order by  user_lname)  AS 序号,[user_serial],[user_no] 编号,[user_lname] 姓名,[user_fname] 登陆名,[user_depname] 部门,[user_card] 绑定卡号,[user_id] 身份证  FROM [dt_user] order by user_lname ">
        </asp:SqlDataSource>
   
   

      













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



      <script type="text/javascript">

        function keyDown() {
       var keycode = event.keyCode;
       var realkey = String.fromCharCode(event.keyCode);
       //alert("按键码:" + keycode + "字符:" + realkey);
       if (keycode == 13) {
           document.getElementById("textbox2").focus();
       }
   }
   document.onkeydown = keyDown;
    </script>
<!--
    keydown事件会在键盘按下时触发.
    keyup事件会在按键释放时触发,也就是你按下键盘起来后的事件
    keypress事件会在敲击按键时触发,我们可以理解为按下并抬起同一个按键 
-->
    </form>
</body>
</html>





   
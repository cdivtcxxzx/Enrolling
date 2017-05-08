<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassMan.aspx.cs" Inherits="nradmingl_ClassMan" %>

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
<body style="background:#fff">
    <form id="form1" runat="server">
    
    <!--页面开始-->
     	<div class="admin-main">

			<blockquote class="layui-elem-quote">
          <a href="" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#x1002;</i>
				</a>
            <%--<div class="layui-input-inline hidden-xs"><asp:TextBox ID="TextBox1" CssClass="layui-input" runat="server"></asp:TextBox></div>
            <a href="javascript:;" class="layui-btn layui-btn-small hidden-xs" id="search">
					<i class="layui-icon">&#xe615;</i> 搜索
				</a>--%>
                <div class="layui-input-inline hidden-xs"></div>
                <span style="float:right">
				<%--<a href="javascript:" id="test1" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe608;</i> 新增
				</a>
                <a href="#" class="layui-btn layui-btn-small  hidden-xs">
					<i class="layui-icon">&#xe60a;</i> 修改
				</a>
                <!--调用C#原生按钮设置样式举例(含批量操作)-->
                <asp:LinkButton CssClass="layui-btn layui-btn-small" name="btnDelete" onclick="Button3_Click"  txttop="txttop" ToolTip="先选择后，再批量删除！" ID="btnDelete" runat="server"   OnClientClick="return batchAudit('btnDelete');"   Text='' ><i class="layui-icon">&#xe640;</i> 删除</asp:LinkButton>
                <!--调用C#原生按钮设置样式举例OVER-->
                <a href="#" class="layui-btn layui-btn-small hidden-xs">
					<i class="layui-icon">&#xe630;</i> 审核
				</a>
                <a href="javascript:" onclick="parent.layer.open({  type: 2,  title: '项目任务导入',  shadeClose: true,  shade: 0.8,  area: ['98%', '98%'],  content: 'kfrwdr.aspx?setp=1&mb=kfgl',btn:'完成'});" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe62f;</i> 导入
				</a>--%>
                <asp:LinkButton CssClass="layui-btn layui-btn-small" name="syncclass"   txttop="txttop" ToolTip="从教务系统同步班级信息" ID="bt_syncclass" runat="server"    Text='' OnClick="bt_syncclass_Click" ><i class="layui-icon">&#xe61e;</i> 班级同步</asp:LinkButton>
                <asp:LinkButton CssClass="layui-btn layui-btn-small" name="exportexcel1" onclick="exportexcel"  txttop="txttop" ToolTip="数据导出" ID="exportexcel1" runat="server"    Text='' ><i class="layui-icon">&#xe61e;</i> 导出</asp:LinkButton>
                
				<%--<a href="#" class="layui-btn layui-btn-small hidden-xs">
					<i class="layui-icon">&#xe62a;</i> 详情
				</a>
                 --%>
                <a href="javascript:" onclick="parent.layer.open({  id:1,type: 2,  title: '当前页二维码',  shadeClose: true,  shade: 0.8,  area: ['285px', '318px'],time: 60000,  content: '2weima.aspx?url='+window.location.href.replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|').replace('&','|')});"><img src="images/2weima.png" titop="images/2weima.png" alt="images/2weima.png" style="height:28px;width:28px" /></a>
				
				</span>
			</blockquote>
			
				



<asp:HiddenField ID="hdfWPBH" runat="server" />
      
    <asp:GridView  OnRowCommand="GridView1_RowCommand"  ID="GridView1"  OnDataBound="GridView1_DataBound"  runat="server" AutoGenerateColumns="False" 
            DataSourceID="ObjectDataSource1" CssClass="site-table table-hover"
            EmptyDataText="未获取到数据!" 
            AllowPaging="True" AllowSorting="True">
    <Columns>
    <asp:TemplateField>
                <HeaderTemplate>
                      <input type="checkbox"  id="selected-all" name="selected-all" onclick="onclicksel();" />  
                </HeaderTemplate>
                <ItemTemplate>
                     <input id="BoxId" name="BoxId"  class="icheck" value='<%#(Convert.ToString(Eval("PK_Class_NO")))%>' type="checkbox" /> 
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="2%"  HorizontalAlign="Center" />
            </asp:TemplateField>
    <asp:BoundField DataField="PK_Class_NO" HeaderText="班号" SortExpression="PK_Class_NO"/>
    <asp:BoundField DataField="name" HeaderText="班级名称"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs" SortExpression="name"/>
    <asp:BoundField DataField="yhid" HeaderText="辅导员帐号"   SortExpression="yhid" />
    <asp:BoundField DataField="xm" HeaderText="姓名"   SortExpression="xm" />
        <asp:BoundField DataField="phone" HeaderText="电话"   SortExpression="phone" />
        <asp:BoundField DataField="qq" HeaderText="QQ"   SortExpression="qq" />
        <asp:TemplateField HeaderText="" >
                 
                <HeaderTemplate>管理操作

                <%--<a onclick="return batchAudit(this.id);" id="btnDelete" href="javascript:__doPostBack('btnDelete','')"><span id="plcz" runat="server">点此批量发放毕业证</span></a>--%>
                </HeaderTemplate>
                <ItemTemplate>
             <a href="javascript: " onclick="parent.layer.open({  type: 2,  title: '辅导员设置－<%# Eval("name").ToString() %>',  shadeClose: true,  shade: 0.8,  area: ['100%', '90%'],  content: 'Class_Counseller.aspx?id=<%# Eval("PK_Class_NO").ToString() %>'});"  txttop="txttop" class="layui-btn layui-btn-mini"  title="辅导员设置">辅导员设置</a>
              <%-- &nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CssClass="layui-btn layui-btn-danger layui-btn-mini" CommandName="删除"  CommandArgument='<%#Eval("id")%>'    OnClientClick="" CausesValidation="False"  Text='删除' >      
              </asp:LinkButton>--%>
                    <%--<a href="javascript: " onclick="parent.layer.open({ type: 1,  title: '辅导员设置－<%# Eval("name").ToString() %>',  shadeClose: true,  shade: 0.8,  area: ['100%', '90%'],  content:$('#div_layer')});"  txttop="txttop" class="layui-btn layui-btn-mini"  title="辅导员设置">辅导员设置测试</a>
             --%>
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

            <asp:LinkButton ID="LinkButtonGo" runat="server" class="layui-btn layui-btn-mini" Text="跳转" OnClick="LinkButtonGo_Click" /></span><span class="hidden-xs" style="float:right;padding-bottom: 8px;padding-top: 8px;">&nbsp;&nbsp;&nbsp;每页显示<asp:TextBox ID="PageSize_Set" runat="server" Height="16px" Width="32px" CssClass=" borderSolid1CCC"></asp:TextBox>条<asp:LinkButton ID="buttion2" runat="server"  class="layui-btn layui-btn-mini" Text="设置"   OnClick="PageSize_Go" /></span><span style="float:right;padding-bottom: 8px;padding-top: 8px;">&nbsp;</b></font></span>
        </PagerTemplate>
    </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetFilterClassDT" TypeName="GZJW"></asp:ObjectDataSource>
        <asp:SqlDataSource onselected="SqlDataSource1_Selected"   ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
            SelectCommand="SELECT row_number() over (order by  xw_neirong.fabutime desc)  AS 序号,xw_lanm.lmmc, xw_neirong.title, xw_neirong.author, xw_neirong.fabutime, xw_neirong.images,xw_neirong.isyn,xw_neirong.id,xw_lanm.glqx FROM xw_neirong INNER JOIN xw_lanm ON xw_neirong.LMID = xw_lanm.lmid">
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
   
        <div id="div_layer">xxx</div>


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
    </form>
</body>
</html>

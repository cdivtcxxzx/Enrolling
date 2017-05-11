<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"  CodeFile="ssgl.aspx.cs" Inherits="nradmingl_Default2" %>

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
        select
        {
            display:inline-block;
            height: 37px;
        }

    </style>
    <form id="form1"  runat="server">
    <div class="admin-main">
      <blockquote class="layui-elem-quote">
          <i class="layui-icon">&#xe602;</i>学生寝室预分配<i class="layui-icon">&#xe602;</i>宿舍信息
           <span style="float:right">

            <!--调用C#原生按钮设置样式举例OVER-->
 <%--               <a href="#" class="layui-btn layui-btn-small hidden-xs">
					<i class="layui-icon">&#xe630;</i> 一卡通更新
				</a>
             --%>
                 <asp:LinkButton CssClass="layui-btn layui-btn-small" name="exportexcel1" onclick="exportexcel"  txttop="txttop" ToolTip="清空预分配数据" ID="LinkButton12" runat="server"    Text='' ><i class="layui-icon">&#xe630;</i>清空预分配数据</asp:LinkButton>
                  <a href="mb/ssfypdr.xls" class="layui-btn layui-btn-small hidden-xs">
					<i class="layui-icon">&#xe62a;</i> 模板下载
				</a>
                 <a href="javascript:" onclick="parent.layer.open({  type: 2,  title: '寝室预分配数据导入',  shadeClose: true,  shade: 0.8,  area: ['98%', '98%'],  content: 'ssgl_dr.aspx?setp=1&mb=mb/ssyfpdr.xls',btn:'完成'});" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe62f;</i>导入预分配数据
				</a>
               
                <asp:LinkButton CssClass="layui-btn layui-btn-small" name="exportexcel1" onclick="exportexcel"  txttop="txttop" ToolTip="数据导出" ID="LinkButton13" runat="server"    Text='' ><i class="layui-icon">&#xe61e;</i>导出所选数据</asp:LinkButton>

		  </span>       
      </blockquote>

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
        
        <div>
            <div class="layui-form-item">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                  <ContentTemplate>
                
                    <asp:DropDownList ID="xq" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource2" DataTextField="Campus_Name" 
                        DataValueField="Campus_NO" 
                        onselectedindexchanged="xq_SelectedIndexChanged" Font-Size="Medium">
                        <asp:ListItem Selected="True" Value=" ">全部校区</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                        SelectCommand="select ' ' Campus_NO,'全部校区' Campus_Name union( SELECT [Campus_NO], [Campus_Name] FROM [Base_Campus] WHERE ([Enabled] = @Enabled))">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="true" Name="Enabled" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:DropDownList ID="dorm" runat="server" AutoPostBack="True" 
                        DataSourceID="SqlDataSource3" DataTextField="Name" 
                        DataValueField="PK_Dorm_NO" 
                          onselectedindexchanged="dorm_SelectedIndexChanged" Font-Size="Medium">
                        <asp:ListItem Value=" ">全部公寓</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                        SelectCommand="select ' ' PK_Dorm_NO , '' Dorm_NO,'全部公寓' Name union(SELECT DISTINCT PK_Dorm_NO,[Dorm_NO], [Name] FROM [Fresh_Dorm] WHERE ([Campus_NO] = @Campus_NO) ) ORDER BY [Dorm_NO]">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="xq" Name="Campus_NO" 
                                PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:DropDownList ID="floor" runat="server" DataSourceID="SqlDataSource4" 
                        DataTextField="Floor" DataValueField="Floor" AutoPostBack="True" 
                        onselectedindexchanged="floor_SelectedIndexChanged" Font-Size="Medium">
                        <asp:ListItem Selected="True" Value=" ">全部楼层</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                        SelectCommand="select ' ' id,'全部楼层' Floor union( SELECT DISTINCT [Floor] id,[floor] FROM [Fresh_Room] WHERE ([FK_Dorm_NO] = @FK_Dorm_NO))  ORDER BY [Floor] DESC">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="dorm" Name="FK_Dorm_NO" 
                                PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:DropDownList ID="bj" runat="server" DataSourceID="SqlDataSource5" 
                        DataTextField="Name" DataValueField="PK_Class_NO" AutoPostBack="True" 
                        onselectedindexchanged="bj_SelectedIndexChanged" Font-Size="Medium">
                        <asp:ListItem Selected="True" Value=" ">全部班级</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                        SelectCommand="select ' ' PK_Class_NO,'全部班级' Name union ( SELECT DISTINCT [PK_Class_NO], [Name] FROM [Fresh_Class])  ORDER BY [PK_Class_NO]">
                    </asp:SqlDataSource>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="g_ts" runat="server" Font-Size="Larger"></asp:Label>
                    </ContentTemplate></asp:UpdatePanel>

            </div>
        </div>    
  <div>   
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>
  <asp:HiddenField ID="hdfWPBH" runat="server" />
  <asp:GridView  OnRowCommand="GridView1_RowCommand"  ID="GridView1"  
          OnDataBound="GridView1_DataBound"  runat="server" AutoGenerateColumns="false" 
            DataSourceID="ObjectDataSource1" CssClass="site-table table-hover" OnRowDataBound="GridView1_RowDataBound"
            EmptyDataText="未获取到数据!" 
            AllowPaging="True" AllowSorting="True">
    <Columns>
    <asp:TemplateField>
                <HeaderTemplate>
                      <input type="checkbox"  id="selected-all" class="noshow" name="selected-all" onclick="onclicksel();" />  
                </HeaderTemplate>
                <ItemTemplate>
                     <input id="BoxId" name="BoxId"  class="icheck noshow" value='<%#(Convert.ToString(Eval("id")))%>' type="checkbox" /> 
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="2%"  HorizontalAlign="Center" />
            </asp:TemplateField>
    <asp:BoundField DataField="序号" HeaderText="序号" SortExpression="序号"/>
    <asp:BoundField DataField="校区" HeaderText="校区"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs" SortExpression="校区"/>
    <asp:BoundField DataField="公寓楼名称" HeaderText="公寓名称"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs" SortExpression="公寓楼名称"/>
    <asp:BoundField DataField="楼层" HeaderText="楼层"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs" SortExpression="楼层"/>
    <asp:BoundField DataField="房间编号" HeaderText="房间编号" SortExpression="房间编号"/>
    <asp:BoundField DataField="房间类型" HeaderText="房间类型"   SortExpression="房间类型"/>
    <asp:BoundField DataField="性别" HeaderText="性别"   SortExpression="性别"/>
   <asp:BoundField DataField="班级名称" HeaderText="预分配班级"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"   SortExpression="班级名称"/>
    
<%--    
     <asp:TemplateField HeaderText="学生"  SortExpression="title">
        
            <ItemTemplate>
           <%# imagestu(Eval("床位主键").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>--%>
            <%-- <asp:TemplateField HeaderText="班级"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"  SortExpression="title">
        
            <ItemTemplate>
            <a href="#" class="hidden-xs"><%# fpcw(Eval("房间编号").ToString())%></a>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>--%>
   <asp:TemplateField HeaderText="已选床位"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"  SortExpression="title">
        
            <ItemTemplate>
            <a href="#" class="hidden-xs"><%# sycw(Eval("房间编号").ToString())%></a>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
  
   
        
        <asp:TemplateField HeaderText="" >
                 
                <HeaderTemplate>管理操作

                <%--<a onclick="return batchAudit(this.id);" id="btnDelete" href="javascript:__doPostBack('btnDelete','')"><span id="plcz" runat="server">点此批量发放毕业证</span></a>--%>
                </HeaderTemplate>
                <ItemTemplate>
             <a href="javascript: " onclick="parent.layer.open({  type: 2,  title: '寝室详情－<%# Eval("公寓楼名称").ToString() %><%# Eval("房间编号").ToString() %>',  shadeClose: true,  shade: 0.8,  area: ['100%', '90%'],  content: 'ssgl_qsxq.aspx?id=<%# Eval("id").ToString() %>'});"  txttop="txttop" class="layui-btn layui-btn-mini"  title="查看详情">详情</a> &nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CssClass="layui-btn layui-btn-danger layui-btn-mini" CommandName="删除"  CommandArgument='<%#Eval("id")%>'    OnClientClick="" CausesValidation="False"  Text='删除' >      
              </asp:LinkButton>
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
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
          SelectMethod="serch_yfpgl" TypeName="dormitory">
            <SelectParameters>
                <asp:ControlParameter ControlID="xq" Name="xq" PropertyName="SelectedValue" 
                    Type="String" />
                <asp:ControlParameter ControlID="dorm" Name="dorm" PropertyName="SelectedValue" 
                    Type="String" />
                <asp:ControlParameter ControlID="floor" Name="floor" 
                    PropertyName="SelectedValue" Type="String" />
                <asp:ControlParameter ControlID="bj" Name="bjbh" PropertyName="SelectedValue" 
                    Type="String" />
            </SelectParameters>
      </asp:ObjectDataSource>
       
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

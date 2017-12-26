<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation = "false"  CodeFile="bzr_xcbd.aspx.cs" Inherits="nradmingl_bzr_xcbd" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>班主任现场报到</title>
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
      <blockquote class="layui-elem-quote">&nbsp;
          <i class="layui-icon">&#xe602;</i>现场报到操作<span class=" hidden-xs"><i class="layui-icon">&#xe602;</i>辅导员</span>
           <span style="float:right">

            <!--调用C#原生按钮设置样式举例OVER-->
 <%--               <a href="#" class="layui-btn layui-btn-small hidden-xs">
					<i class="layui-icon">&#xe630;</i> 一卡通更新
				</a>
             --%>
             
             <a href="bzr_xcbd.aspx" class="layui-btn layui-btn-small">
					刷新
				</a>
                     <asp:LinkButton CssClass="layui-btn layui-btn-small" 
              name="exportexcel1" onclick="exportexcel"  txttop="txttop" ToolTip="数据导出" 
              ID="LinkButton13" runat="server"    Text='' ><span class=" hidden-xs">学生数据</span>导出</asp:LinkButton>
          
        
		  </span>       
      </blockquote>

          
        
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                            DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="PK_Class_NO" 
                            Font-Size="Medium" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SqlConnString %>" SelectCommand="SELECT     TOP (1000) Fresh_Class.PK_Class_NO, Fresh_Class.Name
FROM         Fresh_Class right OUTER JOIN
                      Fresh_Counseller ON Fresh_Class.PK_Class_NO = Fresh_Counseller.FK_Class_NO
WHERE     (Fresh_Counseller.FK_Staff_NO = @username)">
                            <SelectParameters>
                                <asp:SessionParameter Name="username" SessionField="username" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                        
                        
                        
                        
                        
                            
                        
                        
                        
                        
                        <div class="layui-input-inline" style="width:30%">
                        <asp:TextBox ID="TextBox1"  CssClass="layui-input"  placeholder="可输入学生姓名、身份证号、高考报名号"   runat="server"></asp:TextBox>
                        </div><asp:Button ID="Button1"  CssClass="layui-btn"  runat="server" Text="查询" 
                            onclick="Button1_Click1" />
                        <asp:Label ID="g_ts" runat="server" Font-Size="Larger"></asp:Label>
                   
                       
                            </ContentTemplate>
            </asp:UpdatePanel>

        
  <div>   
                     
              <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>        
  <asp:HiddenField ID="hdfWPBH" runat="server" />
  <asp:GridView  OnRowCommand="GridView1_RowCommand"  ID="GridView1"  
          OnDataBound="GridView1_DataBound"  runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" CssClass="site-table table-hover" OnRowDataBound="GridView1_RowDataBound"
            EmptyDataText="未获取到数据!" 
            AllowPaging="false" AllowSorting="True">
    <Columns>
    <asp:BoundField DataField="序号" HeaderText="序号" SortExpression="序号" 
            InsertVisible="False" ReadOnly="True"/>
            <asp:BoundField DataField="高考报名号"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"  HeaderText="高考报名号" SortExpression="高考报名号" 
            InsertVisible="False" ReadOnly="True"/>
            <asp:BoundField DataField="姓名" HeaderText="姓名"  
           SortExpression="姓名"/>
    
     <asp:TemplateField HeaderText="性别"   ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"   SortExpression="性别">
        
            <ItemTemplate>
           <%# imagestu2(Eval("性别").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
    
             <asp:BoundField DataField="身份证号" HeaderText="身份证号"  
            ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs" SortExpression="身份证号"/>
           
  
    
     <asp:TemplateField HeaderText="网上注册"   ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"   SortExpression="网上注册">
        
            <ItemTemplate>
           <%# imagestu(Eval("网上注册").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="网上缴费"   ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"   SortExpression="网上缴费">
        
            <ItemTemplate>
           <%# imagejs(Eval("网上缴费").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField><asp:TemplateField HeaderText="选寝情况" ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"  >
        
            <ItemTemplate>
            <%# cscx(Eval("xh").ToString(), Eval("报到状态").ToString())%>
          
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
                        <asp:TemplateField HeaderText="报到状态"  SortExpression="报到状态">
        
            <ItemTemplate>
           <%# imagezt(Eval("报到状态").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
        
            <%-- <asp:TemplateField HeaderText="班级"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"  SortExpression="title">
        
            <ItemTemplate>
            <a href="#" class="hidden-xs"><%# fpcw(Eval("房间编号").ToString())%></a>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>--%>

            

              <asp:TemplateField  HeaderText="报到管理"  >
            
            <ItemTemplate>
          <asp:LinkButton ID="LinkButton22" runat="server" CommandName="确认报到"  CssClass="layui-btn layui-btn-mini"  CommandArgument='<%#Eval("xh")%>'    Text='确认报到'>   </asp:LinkButton>
        <asp:LinkButton ID="LinkButton12" runat="server" CommandName="取消"  CssClass="layui-btn layui-btn-danger layui-btn-mini"  CommandArgument='<%#Eval("xh")%>'    Text='取消'>   </asp:LinkButton>
     
     
      <%--   <a href="javascript: " onclick="parent.layer.open({  type: 2,  title: '',  shadeClose: true,  shade: 0.8,  area: ['80%', '98%'],  content: 'gl_bmgladd.aspx?id=<%# Eval("报到状态").ToString() %>&view=1',cancel: function(index, layero){ location.reload(true)  }});"  txttop="txttop" class="layui-btn layui-btn-mini"  title="取消报到信息">取消</a>  
      --%>
       </ItemTemplate></asp:TemplateField>

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
                      <asp:SqlDataSource ID="SqlDataSource1"  
          onselected="SqlDataSource1_Selected"  runat="server" 
                          ConnectionString="<%$ ConnectionStrings:tjConnectionString %>" 
                          
                          
                          
          SelectCommand="SELECT TOP 200 row_number() over (order by  xm )  AS 序号,xh,[gkbmh] 高考报名号,[xm] 姓名,a.[gender_code] 性别,[sfzjh] 身份证号,[zc_zt] 网上注册,[ol_zt] 网上缴费,b.[Status_Code] 报到状态  FROM [TJ].[dbo].[Fresh_STU] a left join enrollment.yxxt_data.dbo.base_stu b on a.xh=b.pk_sno  where bjdm=@bjdm order by xm">
                          <SelectParameters>
                              <asp:ControlParameter ControlID="DropDownList1" Name="bjdm" 
                                  PropertyName="SelectedValue" Type="String" />
                                 
                          </SelectParameters>
                      </asp:SqlDataSource>

   
   <div style="display:none">
   
       <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
           DataKeyNames="学号" DataSourceID="SqlDataSource3">
           <Columns>
               <asp:BoundField DataField="序号" HeaderText="序号" ReadOnly="True" 
                   SortExpression="序号" />
               <asp:BoundField DataField="班级名称" HeaderText="班级名称" SortExpression="班级名称" />
               <asp:BoundField DataField="学号" HeaderText="学号" ReadOnly="True" 
                   SortExpression="学号" />
               <asp:BoundField DataField="高考报名号" HeaderText="高考报名号" SortExpression="高考报名号" />
               <asp:BoundField DataField="姓名" HeaderText="姓名" SortExpression="姓名" />
             <asp:TemplateField HeaderText="性别"   ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"   SortExpression="性别">
        
            <ItemTemplate>
           <%# imagestu2(Eval("性别").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
               <asp:BoundField DataField="身份证号" HeaderText="身份证号" SortExpression="身份证号" />
               <asp:TemplateField HeaderText="网上注册"   ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"   SortExpression="网上注册">
        
            <ItemTemplate>
           <%# imagestu(Eval("网上注册").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="网上缴费"   ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  
            ItemStyle-CssClass="hidden-xs"   SortExpression="网上缴费">
        
            <ItemTemplate>
           <%# imagejs(Eval("网上缴费").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
               <asp:BoundField DataField="缴费金额" HeaderText="缴费金额" SortExpression="缴费金额" />
                <asp:TemplateField HeaderText="报到状态"  SortExpression="报到状态">
        
            <ItemTemplate>
           <%# imagezt(Eval("报到状态").ToString())%>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
               <asp:BoundField DataField="QQ" HeaderText="QQ" SortExpression="QQ" />
               <asp:BoundField DataField="联系电话" HeaderText="联系电话" SortExpression="联系电话" />
               <asp:BoundField DataField="家庭地址" HeaderText="家庭地址" SortExpression="家庭地址" />
               <asp:BoundField DataField="高考时电话" HeaderText="高考时电话" SortExpression="高考时电话" />
               <asp:BoundField DataField="父亲电话" HeaderText="父亲电话" SortExpression="父亲电话" />
               <asp:BoundField DataField="母亲电话" HeaderText="母亲电话" SortExpression="母亲电话" />
               <asp:BoundField DataField="户籍地址" HeaderText="户籍地址" SortExpression="户籍地址" />
               <asp:BoundField DataField="学生性质" HeaderText="学生性质" SortExpression="学生性质" />
           </Columns>
       </asp:GridView>
       <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
           ConnectionString="<%$ ConnectionStrings:tjConnectionString %>" 
           SelectCommand="SELECT TOP 200 row_number() over (order by  xm )  AS 序号,bjmc 班级名称,xh 学号,[gkbmh] 高考报名号,[xm] 姓名,a.[gender_code] 性别,[sfzjh] 身份证号,[zc_zt] 网上注册,[ol_zt] 网上缴费,[ol_je] 缴费金额,b.[Status_Code] 报到状态,b.[QQ]      ,b.[Phone] 联系电话   ,b.[Home_add] 家庭地址 ,b.[Phone_dr] 高考时电话,b.[Phone_fa] 父亲电话      ,b.[Phone_ma] 母亲电话      ,b.[Huji_add] 户籍地址      ,b.[Note] 学生性质  FROM [TJ].[dbo].[Fresh_STU] a left join enrollment.yxxt_data.dbo.base_stu b on a.xh=b.pk_sno where bjdm=@bjdm   order by xm">
           <SelectParameters>
               <asp:ControlParameter ControlID="DropDownList1" Name="bjdm" 
                   PropertyName="SelectedValue" />
           </SelectParameters>
       </asp:SqlDataSource>
   </div>
      
             








      <!--与后台配合的提示信息隐藏域-->
            <input id="tsxx" type="text" runat="server" value="" style="display:none" />
            <input id="tsbox" type="text" runat="server" value="" style="display:none" />
			<!--与后台配合的提示信息隐藏域OVER-->
                    </ContentTemplate>
            </asp:UpdatePanel>
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

  
    <!--引发ＬＡＹＵＩ前端必须ＪＳ　ＯＶＥＲ-->
   


   	<!--引发ＬＡＹＵＩ前端必须ＪＳ-->
   
    </form>
</body>
</html>

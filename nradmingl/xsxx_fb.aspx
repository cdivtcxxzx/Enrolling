<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xsxx_fb.aspx.cs" Inherits="nradmingl_xsxx_fb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学生管理</title>
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
    <style>
        .layui-form-select dl dd.layui-this {
            background-color: #196BAB;
            color: #fff;
        }
        select
        {
            height: 37px;
        }

    </style>
    <form id="form1"  runat="server">
    <div class="admin-main">
      <blockquote class="layui-elem-quote">
          <i class="layui-icon">&#xe602;</i>后台管理<i class="layui-icon">&#xe602;</i>学生信息
           <span style="float:right">
               <a href="xsxx_fb.aspx" class="layui-btn layui-btn-small hidden-xs">
					<i class="layui-icon">&#x1002;</i> 刷新
				</a>

               <asp:LinkButton CssClass="layui-btn layui-btn-small" name="exportexcel1" txttop="txttop" ToolTip="表格中准确填写班级信息即可" ID="LinkButton13" runat="server"     Text='' OnClick="exportexcel" ><i class="layui-icon">&#xe61e;</i>导出所选数据模板</asp:LinkButton>

                 <a href="javascript:" onclick="parent.layer.open({  type: 2,  title: '学生数据导入',  shadeClose: true,  shade: 0.8,  area: ['98%', '98%'],  content: 'xsxx_fb_dr.aspx?setp=1&mb=mb/null.xls',btn:'完成'});" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe62f;</i>导入分班数据
				</a>
               
                

		  </span>       
      </blockquote>

                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
        
        <div>
            <div class="layui-form-item">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                  <ContentTemplate>
                批次：
                    <asp:DropDownList ID="batch" runat="server" AutoPostBack="True" 
                        DataSourceID="LinqDataSource1" DataTextField="Batch_Name" 
                        DataValueField="PK_Batch_NO" 
                        Font-Size="Medium" AppendDataBoundItems="True" OnSelectedIndexChanged="batch_SelectedIndexChanged">
                        <%--<asp:ListItem Selected="True" Value="0">选择所有批次</asp:ListItem>--%>
                    </asp:DropDownList>
                      &nbsp;&nbsp;学院：
                      <asp:DropDownList ID="xueyuan" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSource2" DataTextField="Name" DataValueField="College_NO" Font-Size="Medium" OnSelectedIndexChanged="xueyuan_SelectedIndexChanged" AppendDataBoundItems="True">
                          <asp:ListItem Value="-1">请选择学院</asp:ListItem>
                      </asp:DropDownList>
                      &nbsp;&nbsp;
                      <asp:Label ID="g_ts" runat="server" Font-Size="Larger"></asp:Label>
                      <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="model.organizationModelDataContext" EntityTypeName="" Select="new (PK_Batch_NO, Batch_Name, Enabled)" TableName="Fresh_Batches" OrderBy="PK_Batch_NO" Where="Enabled == @Enabled">
                          <WhereParameters>
                              <asp:Parameter DefaultValue="run" Name="Enabled" Type="String" />
                          </WhereParameters>
                      </asp:LinqDataSource>
                      <asp:LinqDataSource ID="LinqDataSource2" runat="server" ContextTypeName="model.organizationModelDataContext" EntityTypeName="" TableName="Base_Colleges" Where="Enabled == @Enabled">
                          <WhereParameters>
                              <asp:Parameter DefaultValue="true" Name="Enabled" Type="String" />
                          </WhereParameters>
                      </asp:LinqDataSource>
                      <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="getYxByYhid" TypeName="organizationService">
                          <SelectParameters>
                              <asp:SessionParameter DefaultValue="" Name="yhid" SessionField="UserName" Type="String" />
                          </SelectParameters>
                      </asp:ObjectDataSource>
                    </ContentTemplate></asp:UpdatePanel>
            </div>
        </div>    
  <div>   
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                  <ContentTemplate>
  <asp:HiddenField ID="hdfWPBH" runat="server" />
  <asp:GridView  ID="GridView1"  
          runat="server" AutoGenerateColumns="False" 
            DataSourceID="ObjectDataSource1" CssClass="site-table table-hover" 
            EmptyDataText="未查找到相关数据!" 
            AllowPaging="True" AllowSorting="True" OnDataBound="GridView1_DataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
    <Columns>
    <asp:TemplateField>
                <HeaderTemplate>
                      <input type="checkbox"  id="selected-all" class="noshow" name="selected-all" onclick="onclicksel();" />  
                </HeaderTemplate>
                <ItemTemplate>
                     <input id="BoxId" name="BoxId"  class="icheck noshow" value='<%#(Convert.ToString(Eval("PK_SNO")))%>' type="checkbox" /> 
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="2%"  HorizontalAlign="Center" />
            </asp:TemplateField>
    <asp:BoundField DataField="PK_SNO" HeaderText="学号"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs" SortExpression="PK_SNO">
        <ControlStyle CssClass="hidden-xs" />
        <HeaderStyle CssClass="hidden-xs" />
        <ItemStyle CssClass="hidden-xs" />
        </asp:BoundField>

    <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name"/>
    <asp:BoundField DataField="Gender" HeaderText="性别"   SortExpression="Gender"/>
    <asp:BoundField DataField="ID_NO" HeaderText="身份证"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"   SortExpression="ID_NO">
        
        <ControlStyle CssClass="hidden-xs" />
        <HeaderStyle CssClass="hidden-xs" />
        <ItemStyle CssClass="hidden-xs" />
        </asp:BoundField>


<%--        <asp:TemplateField HeaderText="民族"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"  SortExpression="Nation_code">
        
            <ItemTemplate>
            <%# show_mz(Eval("Nation_code").ToString()) %>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>--%>
    <asp:TemplateField HeaderText="学院"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"  SortExpression="Colleage">
        
            <ItemTemplate>
            <%# show_xy(Eval("Colleage").ToString()) %>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>
    <asp:BoundField DataField="SPE_Name" HeaderText="专业"   SortExpression="SPE_Name"/>

    <asp:BoundField DataField="Class_Name" HeaderText="班级"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs" SortExpression="Class_Name">
            <ControlStyle CssClass="hidden-xs" />
            <HeaderStyle CssClass="hidden-xs" />
            <ItemStyle CssClass="hidden-xs" />
            </asp:BoundField>

    <asp:BoundField DataField="Xz" HeaderText="学制"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"   SortExpression="Xz">
        
        <ControlStyle CssClass="hidden-xs" />
        <HeaderStyle CssClass="hidden-xs" />
        <ItemStyle CssClass="hidden-xs" />
        </asp:BoundField>
    <asp:BoundField DataField="Year" HeaderText="年级"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"   SortExpression="Year">
        
    <ControlStyle CssClass="hidden-xs" />
    <HeaderStyle CssClass="hidden-xs" />
    <ItemStyle CssClass="hidden-xs" />
    </asp:BoundField>
        <asp:TemplateField HeaderText="" >
                 
                <HeaderTemplate>管理操作

                </HeaderTemplate>
                <ItemTemplate>                    
             <a href="javascript: " onclick="parent.layer.open({  type: 2,  title: '学生分班详情－<%# Eval("Name").ToString() %>',  shadeClose: true,  shade: 0.8,  area: ['100%', '90%'],  content: 'xsxx_detail.aspx?pk_sno=<%# Eval("PK_SNO").ToString() %>',btn:'关闭'});"  txttop="txttop" class="layui-btn layui-btn-mini"  title="查看详情">详情</a>&nbsp;&nbsp; 
             <a href="javascript:" onclick="parent.layer.open({ type: 2,title:'分班设置－<%# Eval("Name").ToString() %>',  shadeClose: true,  shade: 0.8,  area: ['28%', '58%'],  content: 'xsxx_fb_manual.aspx?pk_sno=<%# Eval("PK_SNO").ToString() %>&&spe=<%# Eval("SPE_PK").ToString() %>',btn:'关闭'});"  class="layui-btn layui-btn-mini" title="手动分班">分班</a>       

                    
                    <%--&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CssClass="layui-btn layui-btn-danger layui-btn-mini" CommandName="删除"  CommandArgument='<%#Eval("id")%>'    OnClientClick="" CausesValidation="False"  Text='删除' >      
              </asp:LinkButton>--%>
            </ItemTemplate>
                
                </asp:TemplateField>


    </Columns>
    <PagerTemplate>
<span style="float:left;padding-bottom: 8px;padding-top: 8px;" class="hidden-xs" >
    &nbsp;&nbsp;总共：<%# Session["fb_rowsCount"] %>&nbsp;行&nbsp;&nbsp;&nbsp;&nbsp;

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

            <asp:LinkButton ID="LinkButtonGo" runat="server" class="layui-btn layui-btn-mini" Text="跳转" OnClick="LinkButtonGo_Click" /></span><span class="hidden-xs" style="float:right;padding-bottom: 8px;padding-top: 8px;">&nbsp;&nbsp;&nbsp;
        </PagerTemplate>
    </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getStuByBatchCol" TypeName="organizationService" OldValuesParameterFormatString="original_{0}" OnSelected="ObjectDataSource1_Selected" >
            <SelectParameters>
                <asp:ControlParameter ControlID="batch" Name="batch" PropertyName="SelectedValue" 
                    Type="String" DefaultValue="" />
                <asp:ControlParameter ControlID="xueyuan" Name="colleage_sno" PropertyName="SelectedValue" Type="String" DefaultValue="-1" />
            </SelectParameters>
      </asp:ObjectDataSource>
       
   </ContentTemplate></asp:UpdatePanel>

      <!--与后台配合的提示信息隐藏域-->
            <asp:HiddenField ID="tsxx" runat="server" Value="" />
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
            layui.use(['layer', 'form', 'jquery'], function () {
                var layer = layui.layer
      , form = layui.form();
                var $ = layui.jquery;
                if ($("#tsxx").val() != "") {
                    parent.layer.open({ content: $("#tsxx").val(), title: '提示信息(30秒后自动关闭)', btn: ['关闭'], time: 30000 });
                    $("#tsxx").value = "";
                }
            });
            layui.config({
                base: 'plugins/layui/modules/'
            });
        </script>
        </div>
    </form>
</body>
</html>

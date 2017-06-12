<%@ Page Language="C#" AutoEventWireup="true" CodeFile="classmsg.aspx.cs" Inherits="view_classmsg" %>


<!DOCTYPE html>
<html lang="zh-cn">
<head runat="server">
    <title></title>
    <meta charset="UTF-8" content="编码" />
        <meta name="renderer" content="webkit">
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
		<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
		<meta name="apple-mobile-web-app-status-bar-style" content="black">
		<meta name="apple-mobile-web-app-capable" content="yes">
		<meta name="format-detection" content="telephone=no">

    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
        <link rel="stylesheet" href="../nradmingl/plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="../nradmingl/plugins/global.css" media="all" />
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="../nradmingl/plugins/font-awesome/css/font-awesome.min.css" />
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="../nradmingl/plugins/table.css" />
    
     <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
   
   
</head>
<body> 
<!--展示响应式CSS块-->
 <style>
         
.responsive-utilities-test .col-xs-6 {
    margin-bottom: 10px;
}
.responsive-utilities-test .col-xs-6 {
    margin-bottom: 10px;
}
.col-xs-6 {
    width: 50%;
    float:left;
}
.visible-on .col-xs-6 .hidden-xs, .visible-on .col-xs-6 .hidden-sm, .visible-on .col-xs-6 .hidden-md, .visible-on .col-xs-6 .hidden-lg, .hidden-on .col-xs-6 .hidden-xs, .hidden-on .col-xs-6 .hidden-sm, .hidden-on .col-xs-6 .hidden-md, .hidden-on .col-xs-6 .hidden-lg {
    color: #999;
    border: 1px solid #ddd;
}
     .visible-on .col-xs-6 .visible-xs-block, .visible-on .col-xs-6 .visible-sm-block, .visible-on .col-xs-6 .visible-md-block, .visible-on .col-xs-6 .visible-lg-block, .hidden-on .col-xs-6 .visible-xs-block, .hidden-on .col-xs-6 .visible-sm-block, .hidden-on .col-xs-6 .visible-md-block, .hidden-on .col-xs-6 .visible-lg-block {
    color: #468847;
    background-color: #dff0d8;
    border: 1px solid #d6e9c6;
}
.responsive-utilities-test span {
    display: block;
    padding: 15px 10px;
    font-size: 14px;
    font-weight: 700;
    line-height: 1.1;
    text-align: center;
    border-radius: 4px;
}
     </style>
     <!--展示响应式CSS块over-->
     <!--页面开始全范围框架-->
     <div class="admin-main">
     <!--顶部提示及导航-->
    		<blockquote class="layui-elem-quote">
          
            <i class="layui-icon">&#xe602;</i>自助报到>>通知
            <span style="float:right;" id="btnback">            				
                 <a href="xxzz_xsindex.aspx" class="layui-btn layui-btn-small">
					<i class="layui-icon">&#xe603;</i>
				</a>
           </span>
				
			</blockquote>
 <!--顶部提示及导航OVER-->

  <!--标签框架-->
         <style>
             .xszp img{
                 width:100%;
                 max-width:220px;
                 max-height:360px;
             }
             .xsxx1{float:left;margin-right:10px;width:32%;}
             .xsxx2{float:left;margin-right:10px;width:32%;}
             .xsxx3{float:left;width:32%;}
             @media (max-width: 930px) {
                 .xsxx1 {float: none;margin-right:10px;
                         width:100%;
                 }
                 .xsxx2 {
                 width:48%;}
                 .xsxx3 {
                 width:48%;}
             }
             @media (max-width: 550px) {
                                  .xsxx2 {
                 width:100%;float: none;}
                 .xsxx3 {
                 width:100%;float: none;}
             }
         </style>
         <div style="margin-top:15px;">
	        
       
             
<form class="layui-form layui-form-pane" runat="server">
      <asp:HiddenField ID="pk_sno" Value="" runat="server" />
    <asp:HiddenField ID="server_msg" Value="" runat="server" />

     <div  id="contents" >
            <asp:GridView  ID="GridView1"  
          runat="server" AutoGenerateColumns="False" CssClass="site-table table-hover" 
            EmptyDataText="无通知消息！"  PageSize="5"
            AllowPaging="True" AllowSorting="True" OnDataBound="GridView1_DataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
    <Columns>
    <%--<asp:TemplateField>
        <HeaderTemplate>
                <input type="checkbox"  id="selected-all" class="noshow" name="selected-all" onclick="onclicksel();" />  
        </HeaderTemplate>
        <ItemTemplate>
                <input id="BoxId" name="BoxId"  class="icheck noshow" value='<%#(Convert.ToString(Eval("PK_SNO")))%>' type="checkbox" /> 
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
        <HeaderStyle Width="2%"  HorizontalAlign="Center" />
    </asp:TemplateField>--%>
    
    <asp:BoundField DataField="title" HeaderText="标题" SortExpression="title"/>
    <asp:BoundField DataField="CreateDate" HeaderText="时间"   SortExpression="CreateDate"/>
    <asp:TemplateField HeaderText="状态"  ControlStyle-CssClass="hidden-xs"   SortExpression="pk_no">
        
        <ItemTemplate>
        <%# show_disable(Eval("pk_no").ToString()) %>
        </ItemTemplate>

        <ItemStyle  />
        </asp:TemplateField>

    <%--<asp:BoundField DataField="Year" HeaderText="年级"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"   SortExpression="Year">
        
    <ControlStyle CssClass="hidden-xs" />
    <HeaderStyle CssClass="hidden-xs" />
    <ItemStyle CssClass="hidden-xs" />
    </asp:BoundField>--%>
        <asp:TemplateField HeaderText="" >
                 
                <HeaderTemplate>操作

                </HeaderTemplate>
                <ItemTemplate>                
             <a href="javascript: " msg ='<%# Eval("pk_no").ToString() %>'   class="layui-btn layui-btn-mini btn_show_msg"  title="消息查看">查看</a> <%--&nbsp;&nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CssClass="layui-btn layui-btn-danger layui-btn-mini" CommandName="删除"  CommandArgument='<%#Eval("id")%>'    OnClientClick="" CausesValidation="False"  Text='删除' >      
              </asp:LinkButton>--%>
            </ItemTemplate>
                
                </asp:TemplateField>

    </Columns>
    <PagerTemplate>
<span style="float:left;padding-bottom: 8px;padding-top: 8px;" class="hidden-xs" >
    &nbsp;&nbsp;总共：<%# Session["rowsCount"] %>&nbsp;行&nbsp;&nbsp;&nbsp;&nbsp;

            每页 <asp:Label ID="LabelPageSize" runat="server" Text="<%# ((GridView)Container.NamingContainer).PageSize %>"></asp:Label>
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
        
     </div>

    </form>
</div>        

    
  <!--标签框架over-->

        </div>
        <script type="text/javascript" src="../b_js/jquery.min2.js"></script>       
        <script type="text/javascript" src="../nradmingl/plugins/layui/layui.js"></script>
        <script type="text/javascript" src="../b_js/app/classmsg.js"></script>

</body>
</html>

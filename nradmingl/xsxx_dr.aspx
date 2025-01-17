﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xsxx_dr.aspx.cs" Inherits="nradmingl_xsxx_dr" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="UTF-8" content="编码" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

    <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
        <link rel="stylesheet" href="plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="plugins/global.css" media="all" />
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css" />
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="plugins/table.css" />
    
     <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
     <!--下面为步骤专用CSS样式-->
   <style>
   .wizard {
    -moz-user-select: none;
    -webkit-user-select: none;
    -ms-user-select: none;
    -khtml-user-select: none;
    user-select: none;
    border: 1px solid #ccc;
    border-radius: 0;
    background-clip: padding-box;
    background-color: #fff;
    position: relative;
    overflow: hidden;
}
.wizard ul {
    list-style: none outside none;
    padding: 0;
    margin: 0;
    width: 4000px;
}
ul, menu, dir {
    display: block;
    list-style-type: disc;
    -webkit-margin-before: 1em;
    -webkit-margin-after: 1em;
    -webkit-margin-start: 0px;
    -webkit-margin-end: 0px;
    -webkit-padding-start: 40px;
}
.wizard ul li:first-child {
    -webkit-border-radius: 2px 0 0 0;
    -webkit-background-clip: padding-box;
    -moz-border-radius: 2px 0 0 0;
    -moz-background-clip: padding;
    border-radius: 2px 0 0 0;
    background-clip: padding-box;
    padding-left: 20px;
}
.wizard ul li.active {
    background: #fff;
    color: #262626;
}
.wizard ul li {
    float: left;
    margin: 0;
    padding: 0 20px 0 30px;
    line-height: 46px;
    position: relative;
    background: #f5f5f5;
    color: #d0d0d0;
    font-size: 16px;
    cursor: default;
    -webkit-transition: all .218s ease;
    -moz-transition: all .218s ease;
    -o-transition: all .218s ease;
    transition: all .218s ease;
}
.wizard ul li.active:before {
    display: block;
    content: "";
    position: absolute;
    bottom: 0;
    left: 0;
    right: -1px;
    height: 2px;
    max-height: 2px;
    overflow: hidden;
    background-color: #337ab7;
    z-index: 10000;
}
.wizard ul li.active .step {
    border-color: #337ab7;
    color: #337ab7;
}
.wizard ul li .step {
    border: 2px solid #e5e5e5;
    color: #ccc;
    font-size: 13px;
    border-radius: 100%;
    position: relative;
    z-index: 2;
    display: inline-block;
    width: 22px;
    height: 22px;
    line-height: 20px;
    text-align: center;
    margin-right: 10px;
}
* {
    padding: 0;
    margin: 0;
    font-size: 9pt;
}
.wizard ul li .chevron {
    border: 20px solid transparent;
    border-left: 14px solid #d4d4d4;
    border-right: 0;
    display: block;
    position: absolute;
    right: -14px;
    top: 0;
    z-index: 1;
}
.wizard ul li.active {
    background: #fff;
    color: #262626;
}
.wizard ul li {
    float: left;
    margin: 0;
    padding: 0 0px 0 20px;
    line-height: 38px;
    position: relative;
    background: #f5f5f5;
    color: #d0d0d0;
    font-size: 16px;
    cursor: default;
    -webkit-transition: all .218s ease;
    -moz-transition: all .218s ease;
    -o-transition: all .218s ease;
    transition: all .218s ease;
}
.wizard ul li.active .chevron:before {
    border-left: 14px solid #fff;
}
.wizard ul li .chevron:before {
    border: 20px solid transparent;
    border-left: 14px solid #f5f5f5;
    border-right: 0;
    content: "";
    display: block;
    position: absolute;
    right: 1px;
    top: -20px;
    -webkit-transition: all .218s ease;
    -moz-transition: all .218s ease;
    -o-transition: all .218s ease;
    transition: all .218s ease;
}
.ts_red{
    color:red;
}
   </style>
    <!--步骤专用CSS样式OVER-->
   
</head>
<body style="font-family: 微软雅黑,宋体,Arial,Helvetica,Verdana,sans-serif;"> 
    <form id="form1" runat="server">
     <div class="admin-main">
     <blockquote class="layui-elem-quote">
      <div id="wizard" class="wizard" data-target="#wizard-steps" style="border-left: none; border-top: none; border-right: none;">
        <ul class="steps">
            <li data-target="#step-1" class="active" style="padding-left:0px;"><span class="step">1</span>数据准备<span class="chevron"></span></li>
            <li data-target="#step-2" id="setp2" runat="server"><span class="step">2</span>数据上传<span class="chevron"></span></li>
            <li data-target="#step-3" id="setp3" runat="server"><span class="step">3</span>数据显示<span class="chevron"></span></li>
           
        </ul>
        <span style=" float:right;"><a id="setpup" runat="server" class="layui-btn layui-btn-small" style="margin-right:15px;margin-top:4px;">上一步</a><a  id="setpdown" runat="server"  style="margin-right:15px;margin-top:4px;" class="layui-btn layui-btn-small">下一步</a></span>
    </div>  
				
	</blockquote>
    <asp:Label ID="ztxx" Font-Size="Medium" runat="server" Text=""></asp:Label>
     <!--步骤1-->
    <div  id="setp1cz" runat="server">
     <blockquote class="layui-elem-quote">
     <a href="mb/xsxxdr.xls" class="layui-btn layui-btn-small" id="mbfile" runat="server">
					<i class="layui-icon">&#xe61e;</i>下载模板
				</a>&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DropDownListBatch" runat="server" AutoPostBack="True" 
                        DataSourceID="LinqDataSource1" DataTextField="Batch_Name" 
                        DataValueField="PK_Batch_NO" 
                        Font-Size="Medium" AppendDataBoundItems="True" OnSelectedIndexChanged="DropDownListBatch_SelectedIndexChanged" OnDataBound="DropDownListBatch_DataBound">
                        <asp:ListItem  Value="-1">请选择批次</asp:ListItem>
                    </asp:DropDownList>
         <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="model.organizationModelDataContext" EntityTypeName="" Select="new (PK_Batch_NO, Batch_Name)" TableName="Fresh_Batches" OrderBy="PK_Batch_NO">
                      </asp:LinqDataSource>&nbsp;&nbsp;
      <asp:Label ID="setp1ts" runat="server"
         Text="请先下载EXCLE模板按模板准备导入数据,数据准备完成后选择相应批次,否则无法完成导入,选择好后请点击＂下一步＂!" 
             Font-Size="Medium"></asp:Label>
				
	</blockquote>
    </div>


     <!--步骤2-->
     <div   id="setp2cz" runat="server" CssClass="site-table table-hover">
       <blockquote class="layui-elem-quote">

       <asp:FileUpload ID="FileUpload1" runat="server" />
           <asp:Button 
                ID="batch_import" runat="server" Text="点击上传" txttop="txttop" 
                ToolTip="点此上传已经做好的新生excel表!" OnClientClick="this.value='正在上传..';" 
                CssClass="layui-btn layui-btn-small" onclick="batch_import_Click" />
    	
	</blockquote>
    <br />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView> 
    </div>


     <!--步骤3-->
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    <div   id="setp3cz" runat="server">
        
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

         
        <asp:GridView  ID="GridView2"  
          runat="server" AutoGenerateColumns="False" CssClass="site-table table-hover" 
            EmptyDataText="无错误数据!" 
            AllowPaging="True" AllowSorting="True" OnDataBound="GridView1_DataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
    <Columns>
    <asp:TemplateField>
                <HeaderTemplate>
                      <input type="checkbox"  id="selected-all" class="noshow" name="selected-all" onclick="onclicksel();" />  
                </HeaderTemplate>
                <ItemTemplate>
                     <input id="学号" name="BoxId"  class="icheck noshow" value='<%#(Convert.ToString(Eval("学号")))%>' type="checkbox" /> 
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="2%"  HorizontalAlign="Center" />
            </asp:TemplateField>
    <asp:BoundField DataField="错误提示" HeaderText="错误提示" SortExpression="错误提示" ControlStyle-CssClass="ts_red"/>
    <%--<asp:BoundField DataField="学号" HeaderText="学号"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs" SortExpression="学号">
        <ControlStyle CssClass="hidden-xs" />
        <HeaderStyle CssClass="hidden-xs" />
        <ItemStyle CssClass="hidden-xs" />
        </asp:BoundField>--%>
    <asp:BoundField DataField="高考报名号" HeaderText="报名号"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs" SortExpression="高考报名号">
        <ControlStyle CssClass="hidden-xs" />
        <HeaderStyle CssClass="hidden-xs" />
        <ItemStyle CssClass="hidden-xs" />
        </asp:BoundField>
    <asp:BoundField DataField="姓名" HeaderText="姓名" SortExpression="姓名"/>
    <%--<asp:BoundField DataField="Gender" HeaderText="性别"   SortExpression="Gender"/>--%>
    <asp:BoundField DataField="身份证号" HeaderText="身份证"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"   SortExpression="身份证号">
        
        <ControlStyle CssClass="hidden-xs" />
        <HeaderStyle CssClass="hidden-xs" />
        <ItemStyle CssClass="hidden-xs" />
        </asp:BoundField>


        <%--<asp:TemplateField HeaderText="民族"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"  SortExpression="Nation_code">
        
            <ItemTemplate>
            <%# show_mz(Eval("Nation_code").ToString()) %>
            </ItemTemplate>

            <ItemStyle  />
            </asp:TemplateField>--%>

    <asp:BoundField DataField="专业代码" HeaderText="专业代码"   SortExpression="专业代码"/>
   <%--<asp:BoundField DataField="Xz" HeaderText="学制"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"   SortExpression="Xz">
        
        <ControlStyle CssClass="hidden-xs" />
        <HeaderStyle CssClass="hidden-xs" />
        <ItemStyle CssClass="hidden-xs" />
        </asp:BoundField>--%>
    <%--<asp:BoundField DataField="Year" HeaderText="年级"  ControlStyle-CssClass="hidden-xs" HeaderStyle-CssClass="hidden-xs"  ItemStyle-CssClass="hidden-xs"   SortExpression="Year">
        
    <ControlStyle CssClass="hidden-xs" />
    <HeaderStyle CssClass="hidden-xs" />
    <ItemStyle CssClass="hidden-xs" />
    </asp:BoundField>--%>
    </Columns>
    <PagerTemplate>
<span style="float:left;padding-bottom: 8px;padding-top: 8px;" class="hidden-xs" >
    &nbsp;&nbsp;总共：<%# Session["xsDrRowsCount"] %>&nbsp;行&nbsp;&nbsp;&nbsp;&nbsp;

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
                  else if (GridView2.PageCount >= 8 && GridView2.PageCount - GridView2.PageIndex >= 5)
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
        <%--<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getStuByBatch" TypeName="organizationService" OnSelected="ObjectDataSource2_Selected">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="batch" SessionField="batch" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>--%>
        
    
    </ContentTemplate>
     </asp:UpdatePanel>
    </div>

        </div>


        <script type="text/javascript" src="plugins/layui/layui.js"></script>
    </form>
</body>
</html>

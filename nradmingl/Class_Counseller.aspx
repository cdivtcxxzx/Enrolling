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
                    <asp:Label runat="server" Visible="false" ID="LB_Class_NO"></asp:Label><asp:Label runat="server" ID="LB_Class" CssClass="layui-form-label" ></asp:Label>
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
                    </div>
                </div>
            <div class="layui-form-item">
                    <label class="layui-form-label">QQ：</label>
                    <div class="layui-input-inline">
                        <asp:TextBox runat="server" ID="TB_qq" CssClass="layui-input"></asp:TextBox>
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
               <div class="layui-input-inline">
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
        </div>
</body>
</html>

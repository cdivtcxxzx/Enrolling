<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lanmadd_xw.aspx.cs" Inherits="admin_lanmadd_xw" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>栏目管理</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>


</head>
<body>
<form id="form1" runat="server">
<div>
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=add">当前位置&gt;&gt;&nbsp;新闻栏目管理-添加或修改</a>
			<a href="lanmgl_xw.aspx">返回新闻栏目管理</a>
		</td>
		
	</tr>
    </thead>
    </table>

    <div id="PanelManage">
    <table class="table" style="margin-top:8px;">
    <thead>
    <tr style="display:none">
	    <td colspan="2">栏目数据添加或修改&nbsp;</td>
    </tr>
    </thead>
 
    <tr id="manage_ParentID">
	    <td >所属栏目&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">所属的栏目。</span>

	    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true">
        </asp:DropDownList>&nbsp;<asp:DropDownList ID="DropDownList2" Visible="false" runat="server" AutoPostBack="true">
        </asp:DropDownList>&nbsp;
        &nbsp;
        <asp:Label ID="dhcdhTiSshi" Text="" runat="server" ForeColor="Red" />
	    </td>
    </tr>
    <tr>
	    <td >展示类型&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">分组展示的类型</span>&nbsp;
        <asp:RadioButton runat="server" Text="列表" ID="RBList" GroupName="RBClass" Checked="true" />&nbsp;&nbsp;
	    <asp:RadioButton runat="server" Text="图标" ID="RBIcon" GroupName="RBClass" />&nbsp;&nbsp;
        <asp:RadioButton runat="server" Text="介绍" ID="RBIntroduce" GroupName="RBClass" />&nbsp;&nbsp;
        <asp:RadioButton runat="server" Text="视频" ID="RBBigEvent" GroupName="RBClass"  CssClass="noshow"  />&nbsp;&nbsp;
        <asp:RadioButton runat="server" Text="图文" ID="RadioButton1" GroupName="RBClass" CssClass="noshow"  />&nbsp;&nbsp;
        <asp:RadioButton runat="server" Text="时间列表" ID="RadioButton2" GroupName="RBClass" CssClass="noshow"  />&nbsp;&nbsp;
        <asp:RadioButton runat="server" Text="时间轴" ID="RadioButton3" GroupName="RBClass" CssClass="noshow"  />&nbsp;&nbsp;</td>
    </tr>
    <tr id="manage_System">
	    <td >名称&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">栏目名称，用于页面显示。</span><input name="TB_Name0" type="text" id="TB_Name0" runat="server"/>
        <asp:RequiredFieldValidator
            ID="nameRequiredFieldValidator" 
            runat="server" ErrorMessage="*"  Font-Size="Medium"
            SetFocusOnError="true" ControlToValidate="TB_Name0" ForeColor="Red">
        </asp:RequiredFieldValidator>
	    </td>
    </tr>
 
    <tr id="manage_Name">
	    <td >关键字&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">栏目关键字，用于后台权限判断。</span>
	    <input name="TB_Name" type="text" id="TB_Name"  runat="server"/>
        <asp:RequiredFieldValidator
            ID="KeyWordRequiredFieldValidator" 
            runat="server" ErrorMessage="*"  Font-Size="Medium"
            SetFocusOnError="true" ControlToValidate="TB_Name" ForeColor="Red">
        </asp:RequiredFieldValidator>
	    </td>
    </tr>
    
    <tr id="show">
	    <td >栏目控制&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">各种显示的控制设置(底部为服务导航，首页显示指首页要显示的新闻类)</span>&nbsp;
	   <asp:CheckBox runat="server" Text="开启审核" ID="sfsh" Checked="true" />&nbsp;
             <asp:CheckBox runat="server" Text="菜单显示" ID="sfcdxs" />&nbsp;
        <asp:CheckBox runat="server" Text="幻灯导航" ID="sfdhxs" />&nbsp;
        <asp:CheckBox runat="server" Text="首页显示" ID="sftop" />&nbsp;
        &nbsp;<asp:CheckBox 
                runat="server" Text="顶部导航" ID="sftop0" />&nbsp;<asp:CheckBox runat="server" Text="OA发布" ID="sfoafb" CssClass="noshow"  />&nbsp;
            
            &nbsp;<asp:CheckBox 
                runat="server" Text="底部链接" ID="sfdown"   />
            
            <asp:CheckBox runat="server" Text="记录日志"   ID="sfjlrz" Visible="false" /><br />
            幻灯导航：为首页图片旁边栏目，首页显示:首页中下部新闻 ,底部链接:友情链接之类</td>
    </tr>

    <tr id="manage_Content"  >
	    <td >说明&nbsp;;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">栏目模块的具体说明备注。</span>
	    <textarea name="TB_Content" cols="20" id="TB_Content" class="Editor" runat="server"></textarea>
        <asp:RequiredFieldValidator
            ID="SMRequiredFieldValidator" 
            runat="server" ErrorMessage="*" Font-Size="Medium"
            SetFocusOnError="true" ControlToValidate="TB_Content" ForeColor="Red">
        </asp:RequiredFieldValidator>
	    </td>
    </tr>
 
    <%--<tr id="manage_Link">
	    <td >栏目模块项&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
        <span class="note">栏目具备的属性控制。（权限代码表所有权限项）</span>&nbsp;&nbsp;
        <asp:Repeater id="rptAllEnablePower" runat="server">
            <ItemTemplate>
                <asp:CheckBox ID="powerList" onclick="AddCheckbox()" name="powerList" runat="server" Text='<%#(Convert.ToString(Eval("qxmc")))%>' />
                <asp:Label ID="CheckBoxID" runat="server" Text='<%#Eval("qxid")%>' Visible="false"></asp:Label>
            </ItemTemplate>
        </asp:Repeater>
	    </td>
    </tr>--%>    <%--<tr id="manage_Tree">
	    <td >导航编号&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">如果导航显示请设置与他上级相同的导航编号数字。</span>
	    <input name="TB_Tree" type="text" id="TB_Tree"  runat="server"/>
        <asp:RequiredFieldValidator
            ID="orderRequiredFieldValidator" 
            runat="server" ErrorMessage="*"  Font-Size="Medium"
            SetFocusOnError="true" ControlToValidate="TB_Tree" ForeColor="Red">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator
            ID="order_RegularExpressionValidator" runat="server" ErrorMessage="请输入数字" 
            SetFocusOnError="true" ControlToValidate="TB_Tree" ForeColor="Red" ValidationExpression="^[-]?\d+[.]?\d*$">
        </asp:RegularExpressionValidator>
	    导航编号:<asp:Label ID="dhbh" runat="server" Text="菜单导航的order值，请输入数字用于编号"></asp:Label>
        </td>

    </tr>--%>
 <style>
     .noshow {
     display:none;
     }
 </style>
    <tr id="manage_IsHide">
	    <td >栏目图标&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">栏目显示的图标。</span><asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Image ID="Image1" runat="server" Height="16px" 
                        ImageUrl="images/ico_word.gif" />&nbsp;<asp:TextBox ID="TextBox1" CssClass="noshow" runat="server">images/ico_word.gif</asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp; 选择：<asp:DropDownList ID="DropDownList3" runat="server" 
                AutoPostBack="True" onselectedindexchanged="DropDownList3_SelectedIndexChanged" Height="17px">
                <asp:ListItem Value="images/ico_word.gif">默认图标</asp:ListItem>
                <asp:ListItem Value="images/ico_home.gif">首页类</asp:ListItem>
                <asp:ListItem Value="images/ico_settings.gif">设置类</asp:ListItem>
                <asp:ListItem Value="images/ico_user.gif">用户类</asp:ListItem>
                <asp:ListItem Value="images/ico_usergroup.gif">用户组类</asp:ListItem>
                <asp:ListItem Value="images/ico_log.gif">表格类</asp:ListItem>
                <asp:ListItem Value="images/chaoxing.gif">编辑类</asp:ListItem>
                <asp:ListItem Value="images/ico_shortcut.gif">提示类</asp:ListItem>
                <asp:ListItem Value="images/ico_intro.gif">个人信息类</asp:ListItem>
                <asp:ListItem Value="images/ico_notice.gif">文本类</asp:ListItem>
                <asp:ListItem Value="images/ico_error.gif">关闭退出类</asp:ListItem>
            </asp:DropDownList>
            
                </ContentTemplate>
            </asp:UpdatePanel>
   </td>
    </tr>
  <tr id="manage_IsTarget" style="display:none;" >
	    <td >栏目图片&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">栏目展示时的顶部长幅图片！</span>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                   <asp:DropDownList ID="DropDownList4" runat="server" 
                AutoPostBack="True" onselectedindexchanged="DropDownList4_SelectedIndexChanged">
                <asp:ListItem Value="">默认图片（无）</asp:ListItem>
                <asp:ListItem Value="images/bgjs.jpg">学院介绍类</asp:ListItem>
                <asp:ListItem Value="images/bgxyfj.jpg">校园风景</asp:ListItem>
                <asp:ListItem Value="images/bgcc.jpg">校园操场</asp:ListItem>
                <asp:ListItem Value="images/bgxsss.jpg">学生宿舍</asp:ListItem>
                <asp:ListItem Value="images/bgtsg.jpg">图书馆</asp:ListItem>
                <asp:ListItem Value="images/bglkt.jpg">校园鸟瞰图</asp:ListItem>
                <asp:ListItem Value="images/bgzyjs.jpg">专业介绍</asp:ListItem>
                         <asp:ListItem Value="images/jsbg1.jpg">背景类1</asp:ListItem>
                         <asp:ListItem Value="images/jsbg2.jpg">背景类2</asp:ListItem>
                         <asp:ListItem Value="images/jsbg3.jpg">背景类3</asp:ListItem>
            </asp:DropDownList>
            
             <asp:Image ID="Image2" runat="server" Height="50px" 
                        ImageUrl="" />
            
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:TextBox ID="images1" CssClass="noshow" runat="server"></asp:TextBox>
 &nbsp;&nbsp;&nbsp;&nbsp; 上传：<asp:FileUpload ID="FileUpload1" runat="server" />
	    &nbsp;注意图片为：1400*200为佳！
	        </td>
    </tr>
    <tr id="manage_IsTarget">
	    <td >外部地址&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">默认不填写为空，填写后为栏目跳转地址！</span>
            <asp:TextBox ID="TextBoxURL" runat="server"></asp:TextBox>
<%--            <asp:RequiredFieldValidator
            ID="URLRequiredFieldValidator" 
            runat="server" ErrorMessage="*"  Font-Size="Medium"
            SetFocusOnError="true" ControlToValidate="TextBoxURL" ForeColor="Red">
        </asp:RequiredFieldValidator>--%>&nbsp;&nbsp;默认不填写为空，填写后为栏目跳转地址！
	        </td>
    </tr>
     <tr id="Tr1">
	    <td >打开方式&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">窗口打开URL链接的方式。</span>
            <%--<asp:TextBox ID="TextBox3" runat="server">main</asp:TextBox>--%>
            <asp:DropDownList ID="ddlDaKaiFangShi" runat="server" Width="110">
            <asp:ListItem Text="当前窗口" Value="_self" />
            <asp:ListItem Text="右侧框架" Value="main" />      
            <asp:ListItem Text="新窗口" Value="_blank" />
            <asp:ListItem Text="父框架" Value="_top" />
            </asp:DropDownList>
	    </td>
    </tr>
 
<%--    <tr id="manage_IsLogin">
	    <td >权限列表&nbsp;<a href="#" class="help">[?]</a></td>
	    <td  >
	    <span class="note">该栏目具备的权限列表。</span>&nbsp;&nbsp;
        <asp:label id="enablePower" runat="server"></asp:label>
        </td>
	
    </tr>--%>
  
    <tr id="manage_OrderNum">
	    <td >排序数字&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">请输入数字，数字越小排在越前面。</span>
	    <input name="TB_OrderNum" type="text" id="TB_OrderNum" runat="server"/>
        <asp:RequiredFieldValidator
            ID="IsNumberRequiredFieldValidator" 
            runat="server" ErrorMessage="*"  Font-Size="Medium"
            SetFocusOnError="true" ControlToValidate="TB_OrderNum" ForeColor="Red">
        </asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator
            ID="IsNumber_RegularExpressionValidator" runat="server" ErrorMessage="请输入数字" 
            SetFocusOnError="true" ControlToValidate="TB_OrderNum" ForeColor="Red" ValidationExpression="^[-]?\d+[.]?\d*$">
        </asp:RegularExpressionValidator>
	    </td>
        <tr style="display:none">
            <td style="width: 120px;" >
                管理部门：
            </td>
            <td>
                <asp:DropDownList ID="drglbm" runat="server" DataSourceID="SqlDataSource2" DataTextField="YXMC" DataValueField="YXDM">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnString %>" SelectCommand="SELECT [YXDM], [YXMC] FROM [DM_YUANXI] WHERE ([zt] = @zt) ORDER BY [px], [YXDM]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="1" Name="zt" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                
                
                
            </td>
        </tr>
    </tr>
        <tr>
            <td style="width: 120px;">
                管理对象：
            </td>
            <td>
                <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" 
                    DataTextField="ZMC" DataValueField="ZID" Width="257px" Height="61px" 
                    AppendDataBoundItems="true" SelectionMode="Multiple" 
                    ondatabound="ListBox1_DataBound">
                    <asp:ListItem Value="00">所有人</asp:ListItem>
                </asp:ListBox>多选请按住CTRL键
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="SELECT DISTINCT [ZID], [ZMC], [px] FROM [zhuqx] ORDER BY [px]">
                </asp:SqlDataSource>
                
                
                
            </td>
        </tr>
    <tr>
	    <td ></td>
	    <td >
            <asp:Button ID="tijiao" runat="server" CssClass="button" 
                onclick="Button1_Click" Text="确认添加" Font-Size="Medium" />&nbsp;
            </td>
    </tr>
    
</table>
    </div>

</div>
</form>
</body>
</html>

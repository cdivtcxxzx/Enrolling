<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lanmadd.aspx.cs" Inherits="admin_lanmadd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<title>栏目管理</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>

<link rel="stylesheet" href="../LeaRun/css/font-awesome.min.css">

    <style type="text/css">
        .style1
        {
            height: 113px;
        }
    </style>

</head>
<body>
<form id="form1" runat="server">
<div>
    <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=add">当前位置&gt;&gt;&nbsp;栏目管理-添加或修改</a>
			<a href="lanmgl.aspx">返回栏目管理</a>
		</td>
		
	</tr>
    </thead>
    </table>

    <div id="PanelManage">
    <table class="table" style="margin-top:8px;">
    <thead>
    <tr>
	    <td colspan="2">栏目数据添加或修改&nbsp;</td>
    </tr>
    </thead>
 
    <tr id="manage_ParentID">
	    <td >所属栏目&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">所属的栏目。</span>

	    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL1XiBu_OnSelectedIndesChanged">
        </asp:DropDownList>&nbsp;<asp:DropDownList ID="DropDownList2" Visible="false" runat="server" AutoPostBack="true">
        </asp:DropDownList>&nbsp;

        &nbsp;
        <asp:Label ID="dhcdhTiSshi" Text="" runat="server" ForeColor="Red" />
	    </td>
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
	    <td >显示控制&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">各种显示的控制设置</span>&nbsp;
	    <asp:CheckBox runat="server" Text="菜单显示" ID="sfcdxs" />&nbsp;
        <asp:CheckBox runat="server" Text="导航显示" ID="sfdhxs" />&nbsp;
        <asp:CheckBox runat="server" Text="顶部显示" ID="sftop" />&nbsp;
        <asp:CheckBox runat="server" Text="记录日志" ID="sfjlrz" />&nbsp;
        
	    </td>
    </tr>

    <tr id="manage_Content">
	    <td >说明&nbsp;<a href="#" class="help">[?]</a></td>
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
 
    <tr id="manage_Link">
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
    </tr>
 
    <tr id="manage_Tree">
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

    </tr>
 
    <tr id="manage_IsHide">
	    <td class="style1" >栏目图片&nbsp;<a href="#" class="help">[?]</a></td>
	    <td class="style1" >
	    <span class="note">栏目显示的图片。</span>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Image ID="Image2" runat="server" Height="16px" 
                        ImageUrl="images/ico_word.gif" />&nbsp;<asp:TextBox ID="TextBox1" CssClass="noshow" runat="server">images/ico_word.gif</asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp; 选择：<asp:DropDownList ID="DropDownList4" runat="server" 
                AutoPostBack="True" onselectedindexchanged="DropDownList4_SelectedIndexChanged" Height="17px">
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
            &nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:FileUpload ID="FileUpload1" runat="server" Visible="False" />
            </td>
    </tr>
 <tr id="Tr2">
	    <td >栏目字体样式&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">栏目显示的字体样式。</span>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                   <span id="images2" runat="server">  <i class="fa fa-desktop"></i></span>&nbsp;<asp:TextBox ID="TextBox2" CssClass="noshow" runat="server">fa fa-address-book</asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp; 选择：<asp:DropDownList ID="DropDownList3" runat="server" 
                AutoPostBack="True" onselectedindexchanged="DropDownList3_SelectedIndexChanged" Height="17px">
                <asp:ListItem Value="fa fa-desktop">默认样式</asp:ListItem>
                 <asp:ListItem Value="fa fa-navicon">栏目菜单</asp:ListItem>
                <asp:ListItem Value="fa fa-home">首页类</asp:ListItem>
                <asp:ListItem Value="fa fa-sitemap">组织结构</asp:ListItem>

                 <asp:ListItem Value="fa fa-folder-open">文件夹</asp:ListItem>
                  <asp:ListItem Value="fa fa-sellsy">资源云盘</asp:ListItem>
                   <asp:ListItem Value="fa fa-graduation-cap">学术帽</asp:ListItem>
                    <asp:ListItem Value="fa fa-server">列表类</asp:ListItem>
                     <asp:ListItem Value="fa fa-bar-chart">统计图表</asp:ListItem>
                    
                     <asp:ListItem Value="fa fa-book">字典类</asp:ListItem>
                      <asp:ListItem Value="fa fa-file-text-o">文件类</asp:ListItem>
                       <asp:ListItem Value="fa fa-volume-up">声音类</asp:ListItem>




                <asp:ListItem Value="fa fa-cog">设置类</asp:ListItem>
                <asp:ListItem Value="fa fa-user">用户类</asp:ListItem>
                <asp:ListItem Value="fa fa-users">用户组类</asp:ListItem>
                <asp:ListItem Value="fa fa-table">表格类</asp:ListItem>
                <asp:ListItem Value="fa fa-edit">编辑类</asp:ListItem>
                <asp:ListItem Value="fa fa-exclamation-circle">提示类</asp:ListItem>
                <asp:ListItem Value="fa fa-address-book">个人信息类</asp:ListItem>
                <asp:ListItem Value="fa fa-file-word-o">文档类</asp:ListItem>
                 <asp:ListItem Value="fa fa-database">数据库</asp:ListItem>
                  <asp:ListItem Value="fa fa-address-card">名片卡片</asp:ListItem>
                  <asp:ListItem Value="fa fa-comments">聊天类</asp:ListItem>
                   <asp:ListItem Value="fa fa-envelope">邮箱邮件</asp:ListItem>
                   <asp:ListItem Value="fa fa-leaf">树叶植物</asp:ListItem>
                <asp:ListItem Value="fa fa-window-close">关闭退出类</asp:ListItem>
            </asp:DropDownList>
            
                    &nbsp;
                   
                    <asp:HyperLink　 ID="HyperLink1" runat="server" 
                        NavigateUrl="../LeaRun/fontshow.html" Target="_blank" >字体样式查看</asp:HyperLink　>
                   
                </ContentTemplate>
            </asp:UpdatePanel>  
            </td>
    </tr>
    <tr id="manage_IsTarget">
	    <td >链接地址&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">栏目URL链接地址。</span>
            <asp:TextBox ID="TextBoxURL" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator
            ID="URLRequiredFieldValidator" 
            runat="server" ErrorMessage="*"  Font-Size="Medium"
            SetFocusOnError="true" ControlToValidate="TextBoxURL" ForeColor="Red">
        </asp:RequiredFieldValidator>
	    </td>
    </tr>
     <tr id="Tr1">
	    <td >打开方式&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">窗口打开URL链接的方式。</span>
            <asp:TextBox ID="TextBox3" runat="server">main</asp:TextBox>
	    </td>
    </tr>
 
    <tr id="manage_IsLogin">
	    <td >权限列表&nbsp;<a href="#" class="help">[?]</a></td>
	    <td  >
	    <span class="note">该栏目具备的权限列表。</span>&nbsp;&nbsp;
        <asp:label id="enablePower" runat="server"></asp:label>
        </td>
	
    </tr>
 
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
    </tr>
 

    <tr>
	    <td ></td>
	    <td >
            <asp:Button ID="tijiao" runat="server" CssClass="button" 
                onclick="Button1_Click" Text="确认添加" />&nbsp;
            <input type="reset" class="button" value='重置' /></td>
    </tr>
    <asp:HiddenField ID="hiddenID" runat="server" />
</table>
    </div>

</div>
</form>
<script type="text/javascript">
    function AddCheckbox() {
        $("#enablePower").html("");
        var labels = $("span[name=powerList] input:checked").next("label");
        for (var i = 0; i < labels.length; i++) {
            var checkboxString = "<input type='checkbox' class='enableCheckbox' onclick='getEnablePowerCheckbox()' value='" + labels[i].innerHTML + "' /><lable>" + labels[i].innerHTML + "</lable>";
            var checkboxtemp = $(checkboxString);
            $("#enablePower").append(checkboxtemp);
        }
    }

    function getEnablePowerCheckbox() {
        document.getElementById("<%=hiddenID.ClientID %>").value = "";
        
        var enableCheckedboxs = $(".enableCheckbox");
        for (var i = 0; i < enableCheckedboxs.length; i++) {
            if (enableCheckedboxs[i].checked) {

                document.getElementById("<%=hiddenID.ClientID %>").value = document.getElementById("<%=hiddenID.ClientID %>").value + enableCheckedboxs[i].value + ",";
            }
        }
        
    }
</script>
</body>
</html>

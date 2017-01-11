<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lanmadd_sub.aspx.cs" Inherits="admin_lanmadd_sub" %>

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
			<a href="#">当前位置&gt;&gt;&nbsp;栏目管理-添加或修改</a>
            <asp:LinkButton ID="hrefFahui" runat="server" Text="返回栏目管理" CausesValidation="false"/>
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
	    <td >栏目图片&nbsp;<a href="#" class="help">[?]</a></td>
	    <td >
	    <span class="note">栏目显示的图片。</span>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp; 上传：<asp:FileUpload ID="FileUpload1" runat="server" />
	    &nbsp;注意图片为GIF 大小：16*16</td>
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
            <%--<asp:TextBox ID="TextBox3" runat="server">main</asp:TextBox>--%>
            <asp:DropDownList ID="ddlDaKaiFangShi" runat="server" Width="110">
            <asp:ListItem Text="内部框架" Value="main" /> 
            <asp:ListItem Text="当前窗口" Value="_self" /> 
            <asp:ListItem Text="新窗口" Value="_blank" />
            <asp:ListItem Text="父框架" Value="_top" />
            </asp:DropDownList>
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

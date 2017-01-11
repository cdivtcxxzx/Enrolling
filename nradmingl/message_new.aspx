<%@ Page Language="C#" AutoEventWireup="true" CodeFile="message_new.aspx.cs" Inherits="admin_message_new" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--应用样式及JS-->
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <link runat="server" id="webcss" type="text/css" href="styleqt.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
    <!--应用样式OVER-->
    <script type="text/javascript">
    function recdialog() {
        $("#dialog").dialog({
            resizable: false,
            width: 780,
            height: 550,
            border: 1,
            modal: true,
            appendTo:"form",
            

        });
    }
    </script>
    <style type="text/css">
        #dialog
        {
            display: none;
        }
    </style>
    <style type="text/css">
        .scrollingControlContainer
        {
            overflow: scroll;
        }
        
        .scrollingCheckBoxList
        {
            border: 1px #808080 solid;
            height: 398px;
        }
        .selecttd
        {
            width: 200px;
            border: 1px solid #529dcc;
            vertical-align: top;
        }
        .notshow
        {
            display: none;
        }
        .yiping
        {
            position: relative;
            line-height: 14px;
            border: 0px;
            color: green;
        }
        .yiping label
        {
            color: green;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server" />
    <div>
        <!--头部位置信息及工具按扭-->
        <table class="table subsubmenu">
            <thead>
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="LB_top" Text="消息提醒&gt;&gt;&nbsp;新建消息" ></asp:LinkButton>
                        <a
                            href="message_show_all.aspx">返回消息列表</a>
                    </td>
                    <td align="right">
                    </td>
                </tr>
            </thead>
        </table>
        <br />
        <!--头部位置信息及工具按扭-->
    </div>
    <table class="table">
    <tr>
            <td style="width: 120px;">
                消息标题：
            </td>
            <td>
                <asp:TextBox ID="TB_title" runat="server" TextMode="MultiLine" Width="400px" Rows="1" MaxLength="50"></asp:TextBox>25字以内
                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="TB_title" ErrorMessage="*必填" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td style="width: 120px;">
                消息内容：
            </td>
            <td>
                <asp:TextBox ID="TB_detail" runat="server" TextMode="MultiLine" Width="400px" Rows="4" MaxLength="200"></asp:TextBox>100字以内
                 <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TB_detail" ErrorMessage="*必填" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td style="width: 120px;">
                接收人：
            </td>
            <td>
                <asp:UpdatePanel ID="UP_pbjr" ChildrenAsTriggers="false" UpdateMode="Conditional"
                    runat="server">
                    <ContentTemplate>
                        <asp:TextBox TextMode="MultiLine" ID="TB_rec" runat="server" ReadOnly="true" Width="200px"
                            Rows="5"></asp:TextBox><input type="button" onclick="recdialog()" value="请选择" />
                           <%--<asp:RequiredFieldValidator runat="server" ID="RFV_rec" ControlToValidate="TB_rec" ErrorMessage="*必选" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnOK" />
                        <asp:PostBackTrigger ControlID="btnCancel" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btn_Action" runat="server" CssClass="button" Text="确认发送" OnClick="btn_Action_Click"
                    Enabled="true" />
                <asp:Button ID="btn_reset" CssClass="button" Text='重新填写' OnClick="reset_OnClick"
                    runat="server" />
            </td>
        </tr>
    </table>
    <div id="dialog" style="width: 100%; min-width: 740px; border: 1px solid #529dcc;">
        <div id="divSearch" style="height: 26px; line-height: 26px; background-color: #f6f6f6;
            padding: 0px 5px;">
            关键字：<input type="text" id='idSearchString' runat="server" style="font-size: 12px;"
                size="60" name='SearchString' maxlength="255" accesskey="key" value="请输入组或用户关键字"
                class='ms-searchbox' title="请输入组或用户关键字" />
            <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" UseSubmitBehavior="false" />
        </div>
        <div id="divContent">
            <table cellpadding="0" cellspacing="5" style="width: 100%;">
                <tr style="height: 400px;">
                    <td class="selecttd">
                        <div class="scrollingControlContainer scrollingCheckBoxList">
                            <asp:TreeView ID="tvwDeps" runat="server" ExpandDepth="1" OnSelectedNodeChanged="tvwDeps_SelectedNodeChanged"
                                ImageSet="Arrows">
                                <DataBindings>
                                    <asp:TreeNodeBinding DataMember="院系" TextField="text" ValueField="value" />
                                </DataBindings>
                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" HorizontalPadding="5px"
                                    NodeSpacing="0px" VerticalPadding="0px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                                    VerticalPadding="0px" />
                            </asp:TreeView>
                        </div>
                    </td>
                    <td id="tdList" class="selecttd">
                        <div class="scrollingControlContainer scrollingCheckBoxList">
                            <asp:UpdatePanel ID="upLeftUsers" ChildrenAsTriggers="false" UpdateMode="Conditional"
                                runat="server">
                                <ContentTemplate>
                                    <asp:CheckBoxList ID="cbList" runat="server" DataTextField="Name" DataValueField="Yhid"
                                        RepeatColumns="2">
                                    </asp:CheckBoxList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                    <asp:AsyncPostBackTrigger ControlID="tvwDeps" EventName="SelectedNodeChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAllRight" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnRight" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnLeft" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAllLeft" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    <td class="btns" style="width: 20px; text-align: center; vertical-align: middle;">
                        <asp:Button ID="btnAllRight" runat="server" Width="30px" Text=">>" OnClick="btnAllRight_Click" />
                        <asp:Button ID="btnRight" runat="server" Width="30px" Text=">" OnClick="btnRight_Click" />
                        <asp:Button ID="btnLeft" runat="server" Width="30px" Text="<" OnClick="btnLeft_Click" />
                        <asp:Button ID="btnAllLeft" runat="server" Width="30px" Text="<<" OnClick="btnAllLeft_Click" />
                    </td>
                    <td id="tdSelected" class="selecttd">
                        <div class="scrollingControlContainer scrollingCheckBoxList">
                            <asp:UpdatePanel ID="upRightUsers" ChildrenAsTriggers="false" UpdateMode="Conditional"
                                runat="server">
                                <ContentTemplate>
                                    <asp:CheckBoxList ID="cbSelected" runat="server" DataTextField="Name" DataValueField="Yhid"
                                        RepeatColumns="2">
                                    </asp:CheckBoxList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAllRight" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnRight" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnLeft" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAllLeft" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divBtn" style="text-align: right; height: 24px; padding: 0px 5px; line-height: 24px;
            vertical-align: middle;">
            <asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" CssClass="ms-ButtonHeightWidth"
                Text="确定" />
            <asp:Button ID="btnCancel" runat="server" CssClass="ms-ButtonHeightWidth" Text="取消" />
        </div>
    </div>
    </form>
</body>
</html>

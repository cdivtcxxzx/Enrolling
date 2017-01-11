<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xwshcl.aspx.cs" validateRequest="false" Inherits="admin_xwshcl" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="X-UA-Compatible" content="IE=7" />
<head id="Head1" runat="server">
    <title>新闻发布</title>
    <!--应用样式及JS-->
    
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
    <!--应用样式OVER-->
    
    <style type="text/css">
        .btnMap {
	width:50px !important;
	background:transparent url(images/word.gif) no-repeat center center;
}

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
    
    <div>
        <!--头部位置信息及工具按扭-->
        <table class="table subsubmenu">
            <thead>
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="LB_top" PostBackUrl="?action=manage" Text="新闻审核&gt;&gt;&nbsp;审核意见" ></asp:LinkButton>
                        <a
                            href="xwsh.aspx">返回新闻审核</a>
                        <asp:Label ID="tsxx" runat="server"></asp:Label>
                    </td>
                    <td align="right">
                    </td>
                </tr>
            </thead>
        </table>
        
        <!--头部位置信息及工具按扭-->
    </div>
    <table class="table">
    <tr>
            <td style="width: 120px;">
                新闻标题：
            </td>
            <td>
                <asp:TextBox ID="TB_title" runat="server" Width="583px" Rows="1" MaxLength="50"></asp:TextBox>
                50字以内
                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="TB_title" ErrorMessage="*必填" ForeColor="Red"></asp:RequiredFieldValidator>--%>
               
                   
            </td>
        </tr>
        
        <tr>
            <td style="width: 120px;">
                新闻内容：
            </td>
            <td>
                <asp:TextBox ID="txtnewsContent" runat="server"  TextMode="MultiLine" 
                    Height="401px" Width="701px"></asp:TextBox>
<script src="../xheditor/xheditor-1.1.14-zh-cn.min.js" type="text/javascript"></script>
 <script type="text/javascript">
     $(function () {

//         $.extend(xheditor.settings, { skin: 'default', tools: 'simple', shortcuts: { 'ctrl+enter': submitForm} }); //修改默认设置
//         $('#txtnewsContent').xheditor({ upLinkUrl: "xheditor/upload.aspx", upImgUrl: "xheditor/upload.aspx", upFlashUrl: "xheditor/upload.aspx", upMediaUrl: "xheditor/upload.aspx" });

         $('#txtnewsContent').xheditor({ skin: 'default', tools: 'Cut,Copy,Paste,Pastetext,|,Blocktag,Fontface,FontSize,Bold,Italic,Underline,Strikethrough,FontColor,BackColor,|,Align,List,Outdent,Indent,|,Link,Img,Flash,Media,Hr,Emot,Table,Word,|,Preview,Source',
             upLinkUrl: "../xheditor/upload.aspx", upLinkExt: "zip,rar,txt",
             upImgUrl: "../xheditor/upload.aspx", upImgExt: "jpg,jpeg,gif,png",
             upFlashUrl: "../xheditor/upload.aspx", upFlashExt: "swf",
             upMediaUrl: "../xheditor/upload.aspx", upMediaExt: "avi",
             localUrlTest: /^https?:\/\/[^\/]*?(yxxx\.com)\//i, remoteImgSaveUrl: '../xheditor/saveremoteimg.aspx'
         });
     });
 </script>
 100字以内
                 <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TB_detail" ErrorMessage="*必填" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr> 
        
        <tr>
            <td style="width: 120px;">
                新闻栏目：
            </td>
            <td> <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" 
                    DataSourceID="SqlDataSource2" DataTextField="lmmc" DataValueField="lmid" 
                    Height="25px" Width="122px">
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" 
                    DataSourceID="SqlDataSource3" DataTextField="lmmc" DataValueField="lmid" 
                    Height="25px" Width="122px" ondatabound="DropDownList2_DataBound">
                </asp:DropDownList>
                (如果有二级栏目,请选择二级栏目)<asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="SELECT [lmid], [lmmc] FROM [xw_lanm] WHERE ([fid] = @fid) ORDER BY [px]">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList1" Name="fid" 
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="SELECT [lmid], [lmmc], [px] FROM [xw_lanm] WHERE (([fid] = -1) AND (url ='' or url='#' or url is null)) order by px">
                </asp:SqlDataSource>                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="width: 120px;">
                是否幻灯：
            </td>
            <td><asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                    oncheckedchanged="CheckBox1_CheckedChanged"  />
                <asp:TextBox ID="image" runat="server" Width="326px"></asp:TextBox> 
                        <asp:Image ID="Image1" runat="server" style="width:50px;height:30px;" 
                            Visible="False" />
                        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList3_SelectedIndexChanged" 
                            Visible="False">
                        </asp:DropDownList>
                        <asp:Label ID="Label1" runat="server" Text="若要取消首页幻灯显示请删除左侧文本框内容"></asp:Label>
                         </ContentTemplate>
                </asp:UpdatePanel></td></tr>
        <tr>
            <td style="width: 120px;">
                查看对象：
            </td>
            <td>
                <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" 
                    DataTextField="ZMC" DataValueField="ZID" Width="257px" Height="88px" 
                    AppendDataBoundItems="true" SelectionMode="Multiple" 
                    ondatabound="ListBox1_DataBound">
                    <asp:ListItem Value="00">所有人</asp:ListItem>
                </asp:ListBox>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="SELECT DISTINCT [ZID], [ZMC], [px] FROM [zhuqx] ORDER BY [px]">
                </asp:SqlDataSource>
                
                
                
            </td>
        </tr>
         <tr>
                   <td>审批意见：</td>
            <td colspan="1">
                
                <asp:TextBox ID="spyj" runat="server"  TextMode="MultiLine" 
                    Height="76px" Width="701px"></asp:TextBox>
                
            </td>
        </tr>
                   <tr>
                   <td></td>
            <td colspan="1">
                <asp:Button ID="btn_Action" runat="server" CssClass="abutton" Text="审核通过" OnClick="btn_Action_Click"
                    Enabled="true" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_Action0" runat="server" CssClass="error" Text="打回重写" OnClick="btn_Action_Clickno"
                    Enabled="true" />
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>


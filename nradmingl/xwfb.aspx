<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xwfb.aspx.cs" validateRequest="false" Debug="true" Inherits="admin_xwfb" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="X-UA-Compatible" content="IE=7" />
<head id="Head1" runat="server">
    <title>新闻发布</title>
    <!--应用样式及JS-->
    
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
<script src="../layer/jquery1.83.min.js" type="text/javascript"></script>

<script src="../layer/layer.min.js"  type="text/javascript"></script>


<script type="text/javascript" src="include/common.js"></script>
    <!--应用样式OVER-->
    <link href="/html5play/video-js.css" rel="stylesheet" type="text/css"><script src="/html5play/video.js"></script> 
 <script>     videojs.options.flash.swf = "/html5play/video-js.swf";</script>
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
    <script>
    function xw(nid,title){
$.layer({
            type : 2,
            title: ''+title,
            shadeClose: true,
            maxmin: true,
   fix: true,  
            area: ['995px', 515],                     
            iframe: {
                src : 'xw.aspx?xwid='+nid+''
            } ,
        });
}
    </script>
    <div>
        <!--头部位置信息及工具按扭-->
        <table class="table subsubmenu">
            <thead>
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="LB_top" PostBackUrl="?action=manage" Text="新闻管理&gt;&gt;&nbsp;发布新闻" ></asp:LinkButton>
                        <a
                            href="xwgl.aspx">返回新闻管理</a> <a
                            href="#" id="xwyl" target="_blank" runat="server">预览新闻（需先保存）</a>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                 
    <tr>
            <td style="width: 120px;">
                新闻栏目：
            </td>
            <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" 
                    DataSourceID="SqlDataSource2" DataTextField="lmmc" DataValueField="lmid" 
                    Height="25px" Width="122px" ondatabound="DropDownList1_DataBound" 
                            onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" 
                    DataSourceID="SqlDataSource3" DataTextField="lmmc" DataValueField="lmid" 
                    Height="25px" Width="122px" ondatabound="DropDownList2_DataBound" 
                            onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="select '-99' lmid,'请选择' lmmc,0 px,'' glqx union (SELECT [lmid], [lmmc],px,[glqx] FROM [xw_lanm] WHERE ([fid] = @fid)) order by px">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="DropDownList1" Name="fid" 
                            PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                        <asp:Label ID="Label1" runat="server" Text="(如果没有选择二级栏目,新闻会在一级栏目下显示)"></asp:Label>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="SELECT [lmid], [lmmc], [px],[glqx] FROM [xw_lanm] WHERE (([fid] = -1) AND (url ='' or url='#' or url is null)) order by px">
                </asp:SqlDataSource>        
                        <asp:CheckBox ID="CheckBox2" runat="server" Enabled="False" Text="发布需审核" />
                 </ContentTemplate>
                </asp:UpdatePanel>           
            </td>
        </tr>
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
             upLinkUrl: "../xheditor/upload.aspx", upLinkExt: "zip,rar,txt,pdf,doc,docx,xls,xlsx",
             upImgUrl: "../xheditor/upload.aspx", upImgExt: "jpg,jpeg,gif,png",
             upFlashUrl: "../xheditor/upload.aspx", upFlashExt: "swf",
             upMediaUrl: "../xheditor/upload.aspx", upMediaExt: "avi,wmv,mp4,flv",
             localUrlTest: /^https?:\/\/[^\/]*?(yxxx\.com)\//i, remoteImgSaveUrl: '../xheditor/saveremoteimg.aspx'
         });
     });
 </script>
 100字以内
                 <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TB_detail" ErrorMessage="*必填" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
                
            </td>
        </tr> 
        
        
        <tr style="width: 120px;display:none">
            <td style="width: 120px;display:none">
                是否幻灯：
            </td>
            <td><asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                 
                    <script type="text/javascript">

                        function imghq()
                        {
                            var re = /<img([^>]+?)>/ig;
                            var s = document.getElementById("xhe0_fixffcursor").value;
                            var str = ""
                            var tempImg = ""
                            var re1 = /alt\s*=\s*([^\s]+)/i
                            var re2 = /src\s*=\s*(["'])([^"']+)\1/i
                            var js1 = 1
                            while (re.exec(s)) {
                                tempImg = RegExp.$1
                                //str += ((re1.test(tempImg)) ? RegExp.$1 : "") + ","
                                re2.test(tempImg)
                                str = ((re2.test(tempImg)) ? RegExp.$2 : "") + ""
                                var objSelect = document.getElementById("DropDownList3")
                                var new_opt = new Option("新闻图片"+js1, str)
                                objSelect.options.add(new_opt);
                                js1 = js1 + 1
                            }
                            alert(str)


                        }
                    </script>
                  
                        <input id="Checkbox3" onclick="imghq();"  type="checkbox" style="display:none" disabled="disabled" />
                        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="获取图片" 
                            Width="52px" />
                <asp:TextBox ID="image" runat="server" Visible="False" style="width:270px;"></asp:TextBox> 
                        <asp:Image ID="Image1" runat="server" style="width:50px;height:30px;" 
                            Visible="False" />
                        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="DropDownList3_SelectedIndexChanged" Visible="False">
                        </asp:DropDownList>
                        <asp:Label ID="tsxxtp" runat="server" Text="点击两次，如不幻灯显示请删除文本框中的图片地址"></asp:Label>
                        
                    </ContentTemplate>
                </asp:UpdatePanel></td></tr>

                <tr>
            <td style="width: 120px;">
                发布时间：
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Width="198px" Rows="1" MaxLength="50"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="spyj" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
                </td>

        </tr>
         <tr style="display:none">
            <td style="width: 120px;">
                链接地址：
            </td>
            <td>
                <asp:TextBox ID="wburl" runat="server" Width="489px" Rows="1" MaxLength="50"></asp:TextBox>
                (默认不填)&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="True" 
                    Font-Size="Small" ForeColor="Red">填入网址后,新闻会直接跳转到该网址</asp:Label>
                </td>

        </tr>
        <tr style="display:none">
            <td>
                是否置顶：
            </td>&nbsp;&nbsp;
            <td>
            <asp:RadioButton ID="isZhiDing_true" runat="server" Text="是" GroupName="isZhiDing" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="isZhiDing_false" runat="server" Text="否" GroupName="isZhiDing" Checked="True" />
               </td>

        </tr>
        <tr  style="display:none">
        <td>上传展示图片:</td><td><asp:UpdatePanel ID="UpdatePanel4" runat="server"><Triggers><asp:PostBackTrigger ControlID="upload"/></Triggers><ContentTemplate><asp:Image runat="server" ID="xwimg" /><asp:FileUpload ID="FileUpload1" runat="server" /></ContentTemplate></asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <Triggers>
        <asp:AsyncPostBackTrigger ControlID="DropDownList1"/>
        <asp:AsyncPostBackTrigger ControlID="DropDownList2"/>
        </Triggers>
                    
        <ContentTemplate><asp:Button Text="确认上传" ID="upload" runat="server" OnClick="upload_Click" /><label runat="server" id="tips" style="color:Red;">该栏目必须上传展示图片</label>
        </ContentTemplate>
                </asp:UpdatePanel>    </td>
        </tr>
        <tr style="display:none">

            <td style="width: 120px;">
                查看对象：
            </td>
            <td>
                <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource1" 
                    DataTextField="ZMC" DataValueField="ZID" Width="257px" Height="88px" 
                    AppendDataBoundItems="true" SelectionMode="Multiple" 
                    ondatabound="ListBox1_DataBound">
                    <asp:ListItem Value="00" Selected="True">所有人</asp:ListItem>
                </asp:ListBox>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
                    SelectCommand="SELECT DISTINCT [ZID], [ZMC], [px] FROM [zhuqx] ORDER BY [px]">
                </asp:SqlDataSource>
                
                
                
            </td>
        </tr>
                   

            <td colspan="2">
                <asp:Button ID="btn_Action" runat="server" CssClass="abutton" Text="确认发布" OnClick="btn_Action_Click"
                    Enabled="true" Font-Size="Medium" />
                &nbsp;&nbsp;
                <asp:Button ID="btn_reset" CssClass="abutton" Text='重新填写' OnClick="reset_OnClick"
                    runat="server" Font-Size="Medium" />
            &nbsp;
                </td>
        </tr>
    </table>
    
    </form>
</body>
</html>


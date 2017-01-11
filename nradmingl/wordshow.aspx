<%@ Page ValidateRequest="false"  Language="C#" AutoEventWireup="true" CodeFile="wordshow.aspx.cs" Inherits="upload_wordshow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

 <script type="text/javascript" src="../files/common/jquery.js"></script>
 <script>
 //清除从word中粘贴自带的格式 

function cleanHtml(html) {

    //删除Class属性 
    //html = html.replace("(<P)([^>]*)(>.*?)(<\\/P>)", "<p$3</p>");   
 //删除不需要的标签   
//html = html.replace("<[/]?(font|FONT|span|SPAN|xml|XML|del|DEL|ins|INS|meta|META|[ovwxpOVWXP]:\\w+)[^>]*?>", "");  
//html = html.replace("<([^>]*)(?:lang|LANG|class|CLASS|style|STYLE|size|SIZE|face|FACE|[ovwxpOVWXP]:\\w+)=(?:'[^']*'|\"\"[^\"\"]*\"\"|[^>]+)([^>]*)>", "<$1$2>"); 
//html=html.replace(/<([a-zA-Z1-6]+)(\s*[^>]*)?>/g, "<$1>");     
    //TEXT-INDENT: 1584pt;
 // html = html.replace("TEXT-INDENT", "TEXT-IN2");
html = html.replace(/<o:p>&nbsp;<\/o:p>/g, "");
html = html.replace(/o:/g, "");
html = html.replace(/<font>/g, "");
html = html.replace(/<FONT>/g, "");
html = html.replace(/<span>/g, "");
html = html.replace(/<SPAN>/g, "");
html = html.replace(/<SPAN lang=EN-US>/g, "");
html = html.replace(/<P>/g, "");
html = html.replace(/<\/P>/g, "");
html = html.replace(/<\/SPAN>/g, "");


return html; 

} 

 </script>
    <title>上传WORD并显示演示</title>
</head>
<body>

    <form id="form1" runat="server">
       <input id="updFile" runat="server" type="file" style="border-style: solid; border-color: inherit; border-width: 1px;"
            size="50" />
        <asp:Button style="border-right: #999999 1px solid; border-top: #999999 1px solid; font-size: 13pt;
            border-left: #999999 1px solid; width: 83px; border-bottom: #999999 1px solid;
            height: 20px"  ID="Button1" runat="server" Text="上传文档" 
            onclick="Button1_Click" />
&nbsp;<asp:Button style="border-right: #999999 1px solid; border-top: #999999 1px solid; font-size: 13pt;
            border-left: #999999 1px solid; width: 83px; border-bottom: #999999 1px solid;
            height: 20px"  ID="Button2"  OnClientClick="callback(cleanHtml($('#txtIdea').val()));" runat="server" 
           Text="确定导入" 
             />
        <br />

       <asp:TextBox ID="txtIdea" runat="server" Height="59px" TextMode="MultiLine" 
           Width="620px"></asp:TextBox>
       <br /><div style="width:660px;">
       <asp:Label ID="Label1" runat="server" Text="此处显示WORD预览效果" Width="600px"></asp:Label>
       </div>

    </form>
</body>
</html>

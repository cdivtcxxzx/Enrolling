<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="_404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<style>
p {
	line-height:20px;
}
ul{ list-style-type:none;}
li{ list-style-type:none;}
</style>
</head>
<body>
    <form id="form1" runat="server">
 <div style="margin: 0 auto; width:1000px; padding-top:70px; overflow:hidden;">
  <div style="width:300px; float:left; height:200px; background:url(broswer_logo.jpg) no-repeat 100px 60px;"></div>
  
  <div style="width:600px; float:left;">
    <div style=" height:40px; line-height:40px; color:#fff; font-size:16px; overflow:hidden; background:#6bb3f6; padding-left:20px;">网站错误提示 </div>
    <div style="border:1px dashed #cdcece; border-top:none; font-size:14px; background:#fff; color:#555; line-height:24px; height:320px; padding:20px 20px 0 20px; 

overflow-y:auto;background:#f3f7f9;">
      <p style=" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;"><span style=" font-weight:600; 

color:#fc4f03;">您访问的页面出现错误!</span></p>
<span  id="errshow" runat="server"  style=" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;" ><p  >您访问的页面出错了！</p></span>
<p style=" margin-top:12px; margin-bottom:12px; margin-left:0px; margin-right:0px; -qt-block-indent:1; text-indent:0px;">如何解决：</p>
<ul style="margin-top: 0px; margin-bottom: 0px; margin-left: 0px; margin-right: 0px; -qt-list-indent: 1;"><li style=" margin-top:12px; margin-bottom:0px; margin-

left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;">1）<a href="/">点此返回网站首页</a>；</li>
<li style=" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;">2）检查提交内容，是否包含违法信息或违规信息,然后重新提交；</li>
<li style=" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;">3）如果你是正常访问,检查访问后仍未解决，请联系安全管理员；</li></ul>
    </div>
    
  </div>

  </div>

    </form>
</body>
</html>

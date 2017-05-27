<%@ Page Language="C#" AutoEventWireup="true" CodeFile="czzt_detail.aspx.cs" Inherits="view_czzt_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <!--引用ＬＡＹＵＩ前端必须ＣＳＳ-->
        <link rel="stylesheet" href="../nradmingl/plugins/layui/css/layui.css" media="all" />
		<link rel="stylesheet" href="../nradmingl/plugins/global.css" media="all" />
         <!--引用ＬＡＹＵＩ前端字体图标ＣＳＳ-->
		<link rel="stylesheet" href="../nradmingl/plugins/font-awesome/css/font-awesome.min.css" />
         <!--引用ＬＡＹＵＩ前端表格ＣＳＳ,使用表格时才引用-->
		<link rel="stylesheet" href="../nradmingl/plugins/table.css" />
    
     <!--引用ＬＡＹＵＩ前端必须ＣＳＳ OVER-->
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="cs" Value="" runat="server" />
     <asp:HiddenField ID="pk_batch_no" Value="" runat="server" />
      <asp:HiddenField ID="pk_collage_no" Value="" runat="server" />
    <div>
          <table id="detaildata"  class="site-table table-hover" cellspacing="0" rules="all" border="1" style="border-collapse: collapse;">

           </table>
    </div>
    </form>
     <script type="text/javascript" src="../b_js/jquery.min2.js"></script>
        <script type="text/javascript" src="../b_js/app/czzt_detail.js"></script>
        <script>load();</script>
</body>
</html>

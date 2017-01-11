<%@ Page Language="C#" AutoEventWireup="true" CodeFile="2weima.aspx.cs" Inherits="nradmingl_2weima" %>

<!DOCTYPE HTML>
<html>
<head runat="server">
<meta charset="utf-8">
<title></title>
<script src="http://www.jq22.com/jquery/1.7.2/jquery.min.js"></script>
<script type="text/javascript" src="plugins/jquery.qrcode.min.js"></script>
</head>
<body onload="$('#code').qrcode('<%=myurl %>');" style="margin:0 auto; text-align:center">
    <form id="form1" runat="server">
    
   		<div id="code" ></div>
        <script>
<%--
            $(function () {
                $(document).tooltip({
                    items: "img, [titop], [title]", content: function () {
                        var element = $(this);
                        if (element.is("[titop]")) {
                            var text = element.attr("alt");
                            return "<img class='map'  src='" + text + "'>";
                        }
                        if (element.is("[txttop]")) {
                            return element.attr("title");
                        }

                    }
                });
            });
        </script>--%>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logout.aspx.cs" Inherits="admin_LoginOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div><script>
    function setCookie(name, value) {
			        var Days = 30;
			        var exp = new Date();
			        exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
			        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
    }
    setCookie('xurl',"x");


         </script>
    </div>
    </form>
</body>
</html>

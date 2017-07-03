<%@ Page Language="C#" AutoEventWireup="true" CodeFile="apptj.aspx.cs" Inherits="admin_apptj" %>
<%
    
    string agent = (Request.UserAgent + "").ToLower().Trim();
   

    if (agent == "" ||
        agent.IndexOf("mobile") != -1 ||
        agent.IndexOf("mobi") != -1 ||
        agent.IndexOf("nokia") != -1 ||
        agent.IndexOf("samsung") != -1 ||
        agent.IndexOf("sonyericsson") != -1 ||
        agent.IndexOf("mot") != -1 ||
        agent.IndexOf("blackberry") != -1 ||
        agent.IndexOf("lg") != -1 ||
        agent.IndexOf("htc") != -1 ||
        agent.IndexOf("j2me") != -1 ||
        agent.IndexOf("ucweb") != -1 ||
        agent.IndexOf("opera mini") != -1 ||
        agent.IndexOf("mobi") != -1)
    {
        //终端可能是手机
       

        Response.Write("<?xml version='1.0'?><!DOCTYPE html PUBLIC '-//WAPFORUM//DTD XHTML Mobile 1.0//EN' 'http://www.wapforum.org/DTD/xhtml-mobile10.dtd'><HTML xmlns='http://www.w3.org/1999/xhtml'>");

    }
    else
    {
        Response.Write(" <!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\">");
    }
     %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
<title>报名信息统计</title>
<link runat="server" id="webcss" type="text/css" href="style.css" rel="stylesheet" rev="stylesheet" media="all" />
    <style type="text/css">
        .style1
        {
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="PanelManage">
    <table class="table" style="margin-top:8px;">
    <thead>
    <tr>
	    <td colspan="4" class="style1" style="text-align:center">
            <br />
            <asp:Label ID="cgts" runat="server" Text="2017年新生报名信息统计" Font-Size="Medium"></asp:Label>
            <br />
            <br /><td style="width:70px" ><a href="apptjtb.aspx">图表</a></td>
        </td>
    </tr>
    </thead>

    <tr style="background-color:#acd9f8;height:40px;" >
	    <td >院系名称</td>
	    <td >类型</td>
	    <td >
            &nbsp; 计划</td>
	    <td >
            已招</td>
	    <td >
            招生进度</td>
    </tr>
    <tr   style="background-color:#E9FED3;height:40px;">
	    <td rowspan="2" >全院统计</td>
	    <td >中职</td>
	    <td >
            <asp:Label ID="all" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="all0" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="all1" runat="server">0%</asp:Label>
            </td>
    </tr>
    <tr   style="background-color:#E9FED3;height:40px;">
	    <td >高职</td>
	    <td >
            <asp:Label ID="all2" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="all3" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="all4" runat="server">0%</asp:Label>
            </td>
    </tr>
    <tr style="background-color:#f1f3f5;height:35px;" bgcolor="#f1f3f5">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0101'" rowspan="2">机械制造系</td>
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0101'">中职</td>
	    <td >
            <asp:Label ID="jjx" runat="server" Text="0"></asp:Label>
            </td>
	    <td >
            <asp:Label ID="jjx0" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="jjx1" runat="server">0%</asp:Label>
            </td>
    </tr>
   
    <tr style="background-color:#f1f3f5;height:35px;" bgcolor="#f1f3f5">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0101'">高职</td>
	    <td >
            <asp:Label ID="jjx2" runat="server" Text="0"></asp:Label>
            </td>
	    <td >
            <asp:Label ID="jjx3" runat="server" Text="0"></asp:Label>
            </td>
	    <td >
            <asp:Label ID="jjx4" runat="server" Text="0%"></asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#CFDBCE;height:35px;"  bgcolor="#CFDBCE">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0102'" rowspan="2">汽车工程系</td>
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0102'">中职</td>
	    <td >
            <asp:Label ID="qcx" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="qcx0" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="qcx1" runat="server">0%</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#CFDBCE;height:35px;"  bgcolor="#CFDBCE">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0102'">高职</td>
	    <td >
            <asp:Label ID="qcx2" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="qcx3" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="qcx4" runat="server">0</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#f1f3f5;height:35px;" bgcolor="#f1f3f5">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0103'" rowspan="2">交通运输系</td>
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0103'">中职</td>
	    <td >
            <asp:Label ID="jyx" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="jyx0" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="jyx1" runat="server">0%</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#f1f3f5;height:35px;" bgcolor="#f1f3f5">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0103'">高职</td>
	    <td >
            <asp:Label ID="jyx2" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="jyx3" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="jyx4" runat="server">0</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#CFDBCE;height:35px;"  bgcolor="#CFDBCE">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0104'" rowspan="2">物流工程系</td>
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0104'">中职</td>
	    <td >
            <asp:Label ID="wlx" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="wlx0" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="wlx1" runat="server">0%</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#CFDBCE;height:35px;"  bgcolor="#CFDBCE">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0104'">高职</td>
	    <td >
            <asp:Label ID="wlx2" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="wlx3" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="wlx4" runat="server">0</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#f1f3f5;height:35px;" bgcolor="#f1f3f5">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0107'" rowspan="2">土木工程系</td>
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0107'">中职</td>
	    <td >
            <asp:Label ID="tmx" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="tmx0" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="tmx1" runat="server">0%</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#f1f3f5;height:35px;" bgcolor="#f1f3f5">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0107'">高职</td>
	    <td >
            <asp:Label ID="tmx2" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="tmx3" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="tmx4" runat="server">0</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#CFDBCE;height:35px;"  bgcolor="#CFDBCE">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0109'" rowspan="2">财经商贸系</td>
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0109'">中职</td>
	    <td >
            <asp:Label ID="cjx" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="cjx0" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="cjx1" runat="server">0%</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#CFDBCE;height:35px;"  bgcolor="#CFDBCE">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0109'">高职</td>
	    <td >
            <asp:Label ID="cjx2" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="cjx3" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="cjx4" runat="server">0</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#f1f3f5;height:35px;" bgcolor="#f1f3f5">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0110'" rowspan="2">应用科技系</td>
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0110'">中职</td>
	    <td >
            <asp:Label ID="yyx" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="yyx0" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="yyx1" runat="server">0%</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#f1f3f5;height:35px;" bgcolor="#f1f3f5">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0110'">高职</td>
	    <td >
            <asp:Label ID="yyx2" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="yyx3" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="yyx4" runat="server">0</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#CFDBCE;height:35px;"  bgcolor="#CFDBCE">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0111'" rowspan="2">设计系</td>
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0111'">中职</td>
	    <td >
            <asp:Label ID="sjx" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="sjx0" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="sjx1" runat="server">0%</asp:Label>
            </td>
    </tr>
   
    <tr  style="background-color:#CFDBCE;height:35px;"  bgcolor="#CFDBCE">
	    <td  onmousedown="location.href='apptjyx.aspx?yxdm=0111'">高职</td>
	    <td >
            <asp:Label ID="sjx2" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="sjx3" runat="server">0</asp:Label>
            </td>
	    <td >
            <asp:Label ID="sjx4" runat="server">0</asp:Label>
            </td>
    </tr>
   
    <tr>
	    <td colspan="5" style="text-align:center">
            <br />
            点击院系名称可查看详细数据<br /><br />
        </td>
    </tr>
   
</table>

    </div>
</form>
</body>

</html>

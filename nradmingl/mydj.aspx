<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mydj.aspx.cs" Inherits="admin_mydj" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <!--应用样式及JS-->
<link type="text/css" href="styleqt.css" runat="server" id="mycss" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>
<!--应用样式OVER-->
<style>
.notshow{ display:none;}
.yiping{position:relative;line-height:14px;border:0px;color:green;}
.yiping label{color:green;}
</style>
</head>
<body id="C_User">
 
    <form id="form1" runat="server">
    <div>
    
   <!--头部位置信息及工具按扭-->
 <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
			<a href="?action=manage">当前位置&gt;&gt;&nbsp;教学质量评价-我的等级</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
		
		
	</tr>
  </thead>
</table>
<br />
<!--头部位置信息及工具按扭-->

    </div>

    <asp:GridView ID="GridView1" CssClass="table" runat="server" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSource1" 
        AllowPaging="True" AllowSorting="True" EmptyDataText="还没有对你的评定！">
        <Columns>
         <asp:BoundField DataField="uid" HeaderText="序号" SortExpression="uid" />
          <asp:BoundField DataField="nd" HeaderText="年度" SortExpression="nd" />
            <asp:BoundField DataField="xueq" HeaderText="学期" SortExpression="xueq" />
             <asp:BoundField DataField="degree" HeaderText="认定等级" SortExpression="degree" />
         
           
           
            <%--<asp:HyperLinkField HeaderText="操作" DataNavigateUrlFields="btid,bpjr"  
                Text="进入查看" DataNavigateUrlFormatString="pingjmy_show.aspx?btid={0}&amp;bpjr={1}" />--%>
           
           
           
        </Columns>
    </asp:GridView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:SqlConnString %>" 
        SelectCommand="select row_number() over (ORDER BY nd DESC,xueq) as uid,nd,xueq,case when final is null then degree else final end as degree from pjhz_dj a left join pjhz_biaotou b on a.btid=b.id where yhid=@yhid and a.final_id is not null order by nd desc,xueq">
        <SelectParameters>
            <asp:SessionParameter Name="yhid" SessionField="UserName" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

    </form>
</body>
</html>

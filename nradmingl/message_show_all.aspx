<%@ Page Language="C#" AutoEventWireup="true" CodeFile="message_show_all.aspx.cs" Inherits="admin_message_show_all" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link runat="server" id="webcss" type="text/css" href="styleqt.css" rel="stylesheet" rev="stylesheet" media="all" />
<script type="text/javascript" src="../files/common/jquery.js"></script>
<script type="text/javascript" src="include/common.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table class="table subsubmenu">
	<thead>
	<tr>
		<td>
        <asp:LinkButton runat="server" ID="LB_top" Text="消息提醒&gt;&gt;&nbsp;全部消息"></asp:LinkButton>
           <a href="message_show_all.aspx">全部消息</a>
		   <a href="message_show_new.aspx">未读消息</a>
           <a href="message_new.aspx">新建消息</a>
		</td>
		
		<td style="text-align:right;width:180px;">

        
        <asp:TextBox ID="searchtext" 
                runat="server" name="1" txttop="txttop" title="根据发送人或标题搜索消息" style=" margin-right:0px; height:18px;width:100px; border:1px solid #DBDDDE;float:left; "></asp:TextBox>
           
           <asp:ImageButton ID="ImageButton1" runat="server" 
                ImageUrl="images/sous.gif" onclick="Search_Onclick" ToolTip="搜索查找消息" style=" margin-left:0px; border:0px;float:left;" />
&nbsp;</td>
        
	</tr>
  </thead>
</table>
    <div>
    <asp:GridView ID="GV_new" runat="server" AutoGenerateColumns="false" DataSourceID="Sql_all" CssClass="table" OnRowCommand="GridView1_RowCommand" PageSize="10" EmptyDataText="你没有消息及提醒!">
    <Columns>
    <asp:BoundField DataField="uid" HeaderText="序号" SortExpression="uid"/>
    <asp:BoundField DataField="id" HeaderText="信息ID" SortExpression="id" />
    <asp:BoundField DataField="sender" HeaderText="发信人" SortExpression="sender"/>
    <asp:BoundField DataField="sendtime" HeaderText="时间" SortExpression="sendtime" />
    <%--<asp:BoundField DataField="titleurl" HeaderText="标题"  HtmlEncode="false" HtmlEncodeFormatString="false" />--%>
    <asp:HyperLinkField DataNavigateUrlFields="url,id" DataNavigateUrlFormatString="{0}&id={1}" DataTextField="title" HeaderText="标题" SortExpression="title" />
    <asp:ButtonField  Text="删除"  CommandName="shanchu" ButtonType="Button" HeaderStyle-Width="15" Visible="true" HeaderText="管理"/>
    </Columns>
    </asp:GridView>
      <asp:ObjectDataSource ID="Sql_all" runat="server" 
        SelectMethod="GetAll" TypeName="Message">
        <SelectParameters>
            <asp:SessionParameter Name="recid" SessionField="UserName" />
        </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource
         ID="messageSearch"
          runat="server" SelectMethod="GetByKey" TypeName="Message"
    >
        <SelectParameters>
            <asp:ControlParameter ControlID="searchtext" Name="key" PropertyName="Text" 
                Type="String" />
            <asp:Parameter DefaultValue="读" Name="mode" Type="String" />
            <asp:SessionParameter Name="recid" SessionField="UserName" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>

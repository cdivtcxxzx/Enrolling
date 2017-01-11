<%@ Page Language="C#" AutoEventWireup="true" CodeFile="message_show_detail.aspx.cs"
    Inherits="admin_message_show_detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link runat="server" id="webcss" type="text/css" href="styleqt.css" rel="stylesheet"
        rev="stylesheet" media="all" />
    <script type="text/javascript" src="../files/common/jquery.js"></script>
    <script type="text/javascript" src="include/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="table subsubmenu">
            <thead>
                <tr>
                    <td>
                        <asp:LinkButton runat="server" ID="LB_top" Text="消息提醒&gt;&gt;&nbsp;消息详细"></asp:LinkButton>
                        <a href="message_show_all.aspx">全部消息</a> <a href="message_show_new.aspx">未读消息</a>
                        <a href="message_new.aspx">新建消息</a>
                    </td>
                </tr>
            </thead>
        </table>
        <asp:FormView ID="FV_detail" runat="server" DataSourceID="SqlDataSource1" CssClass="table"
            EmptyDataText="该信息不存在！">
            <ItemTemplate>
                <div class="content">
                    <h2>
                        标题:
                        <%#Eval("title") %></h2>
                    <br />
                    <label>
                        发件人：<%#Eval("sender") %></label><br />
                    <label>
                        时间：<%#Eval("sendtime") %></label><br />
                </div>
                <div class="content" style="height: 300px; font-family: 'Microsoft Yahei',verdana;
                    font-size: 14px">
                    <%#Eval("detail") %></div>
            </ItemTemplate>
        </asp:FormView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnString %>"
            SelectCommand="select title,detail,xm as sender,sendtime from message left join yonghqx on message.senderid=yonghqx.yhid where id=@id">
            <SelectParameters>
                <asp:QueryStringParameter Name="id" QueryStringField="id" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssfp-yfp.aspx.cs" Inherits="view_ssfp_yfp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <title>宿舍分配-预分配</title>
    <link href="../bootstrap/3.3.4/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../b_css/app.css" rel="stylesheet" />
</head>
<body>
    <form id="form1"  runat="server">
        <div class="container">
            <h4 class="text-center">预分配宿舍</h4>
            <div class="col-xs-12 col-sm-4">
                <p><span>校区：</span><span id="xiaoqu" runat="server">天府新区</span></p>
                <p><span>类型：</span><span id="shuse" runat="server">男宿舍</span></p>
                <p>
                    <img src="#" alt="宿舍照片" style="margin-top:18px;width:100px;height:140px" id="shuseImg" runat="server" /></p>
            </div>
            <div class="col-xs-12 col-sm-8">
                <div class="form-group">
                    <label for="room_type">房间类型</label>
                    <asp:DropDownList ID="room_type" runat="server" CssClass="form-control">
                        <asp:ListItem Value="-1">请选择房间类型</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="bed_numb">床位位置编号</label>
                    <asp:DropDownList ID="bed_numb" runat="server" CssClass="form-control">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <p>价格：<span>400元/学年</span></p>
                    <p>可选床位数量：<span id="bedCount" runat="server">10个</span></p>
                </div>
                <div class="form-group">
                    <label for="dorm_numb">宿舍号</label>
                    <asp:DropDownList ID="dorm_numb" runat="server" CssClass="form-control">
                        <asp:ListItem>第1公寓</asp:ListItem>
                        <asp:ListItem>第1公寓</asp:ListItem>
                        <asp:ListItem>第1公寓</asp:ListItem>
                        <asp:ListItem>第1公寓</asp:ListItem>
                        <asp:ListItem>第1公寓</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="flo_numb">楼层</label>
                    <asp:DropDownList ID="flo_numb" runat="server" CssClass="form-control">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="room_numb">房间号</label>
                    <asp:DropDownList ID="room_numb" runat="server" CssClass="form-control">
                        <asp:ListItem>101</asp:ListItem>
                        <asp:ListItem>102</asp:ListItem>
                        <asp:ListItem>103</asp:ListItem>
                        <asp:ListItem>104</asp:ListItem>
                        <asp:ListItem>105</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <input type="button" class="btn btn-default" value="保存" id="btnSave" runat="server" />
                    <input type="button" class="btn btn-default" value="刷新" onclick="window.history.go(0)"/>
                    <input type="button" class="btn btn-default" value="返回" />
                </div>
            </div>
            
        </div>
    </form>
</body>
</html>

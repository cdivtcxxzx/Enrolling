<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ssfp-xsyfp.aspx.cs" Inherits="view_ssfp_xsyfp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <title>宿舍分配-学生已分配</title>
    <link href="../bootstrap/3.3.4/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../b_css/app.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h4 class="text-center">学生已分配宿舍</h4>
            <div class="col-xs-12 col-sm-4">
                <p><span>校区：</span><span>天府新区</span></p>
                <p><span>类型：</span><span>男宿舍</span></p>
                <p>
                    <img src="#" alt="宿舍照片" style="margin-top: 18px; width: 100px; height: 140px" />
                </p>
            </div>
            <div class="col-xs-12 col-sm-8">
                <div class="form-group">
                    <label for="room-type">房间类型</label>
                    <select class="form-control" name="room-type">
                        <option>普通公寓</option>
                        <option>高级公寓</option>
                    </select>
                </div>
                <div class="form-group">
                    <label for="bed-numb">座位位置编号</label>
                    <select class="form-control" name="bed-numb">
                        <option>1</option>
                        <option>2</option>
                        <option>3</option>
                        <option>4</option>
                        <option>5</option>
                        <option>6</option>
                    </select>
                </div>
                <div class="form-group">
                    <p>价格：<span>400元/学年</span></p>

                </div>
                <div class="form-group">
                    <label for="dorm-numb">宿舍号</label>
                    <select class="form-control" name="dorm-numb">
                        <option>第1公寓</option>
                        <option>第2公寓</option>
                        <option>第3公寓</option>
                        <option>第4公寓</option>
                        <option>第5公寓</option>
                        <option>第6公寓</option>
                    </select>
                </div>
                <div class="form-group">
                    <label for="flo-numb">楼层</label>
                    <select class="form-control" name="flo-numb">
                        <option>1</option>
                        <option>2</option>
                        <option>3</option>
                        <option>4</option>
                        <option>5</option>
                        <option>6</option>
                    </select>
                </div>
                <div class="form-group">
                    <label for="room-numb">楼层</label>
                    <select class="form-control" name="room-numb">
                        <option>101</option>
                        <option>102</option>
                        <option>103</option>
                        <option>104</option>
                        <option>105</option>
                        <option>106</option>
                        <option>107</option>
                        <option>108</option>
                        <option>109</option>
                        <option>110</option>
                    </select>
                </div>
                <div class="form-group">
                    <input type="button" class="btn btn-default" value="返回" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>

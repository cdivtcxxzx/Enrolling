<%@ Page Language="C#" AutoEventWireup="true" CodeFile="leftbqys.aspx.cs" Inherits="nradmingl_leftbqys" %>
<!DOCTYPE html>

<html><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="viewport" content="width=device-width">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>日志管理</title>
    <!--框架必需start-->
    <script src="./leftbqys_files/jquery-1.10.2.min.js"></script>
    <!--bootstrap组件start-->
    <link href="./leftbqys_files/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
<link href="./leftbqys_files/learun.css" rel="stylesheet">

    <script src="./leftbqys_files/learun.js"></script>



    <style>
        html, body {
            height: 100%;
            width: 100%;
            overflow: hidden;
             background:#ecf0f5;
        }
    </style>
</head>
<body>
    <div id="ajaxLoader" style="cursor: progress; position: fixed; top: -50%; left: -50%; width: 200%; height: 200%; background: rgb(255, 255, 255); z-index: 10000; overflow: hidden; display: none;">
    </div>
    
<script>
    $(function () {
        InitialPage();
        GetGrid();
    });
    //初始化页面
    function InitialPage() {
        //layout布局
        $('#layout').layout({
            applyDemoStyles: true,
            onresize: function () {
                $(window).resize()
            }
        });
        $('.profile-nav').height($(window).height() - 20);
        $('.profile-content').height($(window).height() - 20);
        //resize重设(表格、树形)宽高
        $(window).resize(function (e) {
            window.setTimeout(function () {
                $('#gridTable').setGridWidth(($('.gridPanel').width()));
                $("#gridTable").setGridHeight($(window).height() - 137);
                $('.profile-nav').height($(window).height() - 20);
                $('.profile-content').height($(window).height() - 20);
            }, 200);
            e.stopPropagation();
        });
    }
    //加载表格
    function GetGrid() {
        var queryJson = $("#filter-form").getWebControls();
        queryJson["CategoryId"] = 1;
        var $gridTable = $("#gridTable");
        $gridTable.jqGrid({
            url: "../../SystemManage/Log/GetPageListJson",
            postData: { queryJson: JSON.stringify(queryJson) },
            datatype: "json",
            height: $(window).height() - 137,
            autowidth: true,
            colModel: [
                { label: "主键", name: "F_LogId", hidden: true },
                {
                    label: "操作时间", name: "F_OperateTime", index: "F_OperateTime", width: 150, align: "left",
                    formatter: function (cellvalue, options, rowObject) {
                        return $.learunUIBase.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                    }
                },
                { label: "操作用户", name: "F_OperateAccount", index: "F_OperateAccount", width: 150, align: "left" },
                { label: "IP地址", name: "F_IPAddress", index: "F_IPAddress", width: 150, align: "left" },
                { label: "系统功能", name: "F_Module", index: "F_Module", width: 150, align: "left" },
                { label: "操作类型", name: "F_OperateType", index: "F_OperateType", width: 70, align: "center" },
                {
                    label: "执行结果", name: "F_ExecuteResult", index: "F_ExecuteResult", width: 70, align: "center",
                    formatter: function (cellvalue, options, rowObject) {
                        if (cellvalue == '1') {
                            return "<span class=\"label label-success\">成功</span>";
                        } else {
                            return "<span class=\"label label-danger\">失败</span>";
                        }
                    }
                },
                { label: "执行结果描述", name: "F_ExecuteResultJson", index: "F_ExecuteResultJson", width: 100, align: "left" }
            ],
            viewrecords: true,
            rowNum: 30,
            rowList: [30, 50, 100, 500, 1000],
            pager: "#gridPager",
            sortname: 'F_OperateTime desc',
            rownumbers: true,
            shrinkToFit: false,
            gridview: true
        });
        //点击时间范围（今天、近7天、近一个月、近三个月）
        $("#time_horizon a.btn-default").click(function () {
            $("#time_horizon a.btn-default").removeClass("active");
            $(this).addClass("active");
            switch ($(this).attr('data-value')) {
                case "1": //今天
                    $("#StartTime").val("2016-12-01");
                    $("#EndTime").val("2016-12-01");
                    break;
                case "2": //近7天
                    $("#StartTime").val("2016-11-24");
                    $("#EndTime").val("2016-12-01");
                    break;
                case "3": //近一个月
                    $("#StartTime").val("2016-11-01");
                    $("#EndTime").val("2016-12-01");
                    break;
                case "4": //近三个月
                    $("#StartTime").val("2016-09-01");
                    $("#EndTime").val("2016-12-01");
                    break;
                default:
                    break;
            }
            $("#SelectedStartTime").html($("#StartTime").val());
            $("#SelectedEndTime").html($("#EndTime").val());
            SearchEvent();
        });
        //查询点击事件
        $("#btn_Search").click(function () {
            SearchEvent();
            $(".ui-filter-text").trigger("click");
            $("#SelectedStartTime").html($("#StartTime").val());
            $("#SelectedEndTime").html($("#EndTime").val());
        });
        //重置
        $("#btn_Reset").click(function () {
            $("#filter-form").find("input[type='text']").val("");
        });
        //左边分类点击事件
        $(".profile-nav li").click(function () {
            SearchEvent();
        })
    }
    //查询表格函数
    function SearchEvent() {
        var queryJson = $("#filter-form").getWebControls();
        queryJson["CategoryId"] = $(".profile-nav").find('li.active').attr('data-value');
        $("#gridTable").jqGrid('setGridParam', {
            url: "../../SystemManage/Log/GetPageListJson",
            postData: { queryJson: JSON.stringify(queryJson) },
            page: 1
        }).trigger('reloadGrid');
    }
    //清空日志
    function btn_RemoveLog() {
        var categoryId = $(".profile-nav").find('li.active').attr('data-value');
        var categoryName = $(".profile-nav").find('li.active').html();
        $.learunUIBase.dialogOpen({
            id: "RemoveLog",
            title: '清空' + categoryName,
            url: '/SystemManage/Log/RemoveLog?categoryId=' + categoryId,
            width: "400px",
            height: "200px",
            callBack: function (iframeId) {
                top.frames[iframeId].AcceptClick();
            }
        });
    }
    //导出
    function btn_export() {
        $.learunUIBase.dialogOpen({
            id: "ExcelIExportDialog",
            title: '导出Excel数据',
            url: '/Utility/ExcelExportForm?gridId=gridTable&filename=log',
            width: "500px",
            height: "380px",
            callBack: function (iframeId) {
                top.frames[iframeId].AcceptClick();
            }, btn: ['导出Excel', '关闭']
        });
    }
</script>
<div class="ui-layout ui-layout-container" id="layout" style="height: 100%; width: 100%; overflow: hidden; position: relative;">
    <div class="ui-layout-west ui-layout-pane ui-layout-pane-west ui-layout-pane-hover ui-layout-pane-west-hover ui-layout-pane-open-hover ui-layout-pane-west-open-hover" style="position: absolute; margin: 0px; left: 0px; right: auto; top: 0px; bottom: 0px; height: 612px; z-index: 0; padding: 0px; background: transparent; border: 0px solid rgb(187, 187, 187); overflow: auto; width: 200px; display: block; visibility: visible;">
        <div class="west-Panel">
            <div class="profile-nav" style="height: 592px;">
                <ul style="padding-top: 20px;">
                    <li class="active" data-value="1">登录日志</li>
                    <li data-value="2" class="">访问日志</li>
                    <li data-value="3" class="">操作日志</li>
                    <div class="divide"></div>
                    <li data-value="4" class="">异常日志</li>
                    <li data-value="5" class="">授权日志</li>
                </ul>
            </div>
        </div>
    </div>
    <div class="ui-layout-center ui-layout-pane ui-layout-pane-center" style="position: absolute; margin: 0px; left: 205px; right: 0px; top: 0px; bottom: 0px; height: 612px; width: 1161px; z-index: 0; padding: 0px; background: transparent; border: 0px solid rgb(187, 187, 187); overflow: auto; display: block; visibility: visible;">
        <div class="center-Panel">
            <div class="titlePanel">
                <div class="title-search">
                    <table>
                        <tbody><tr>
                            <td>查询条件</td>
                            <td style="padding-left: 10px;">
                                <div class="ui-filter" style="width: 200px;">
                                    <div class="ui-filter-text" style="border-bottom-color: rgb(204, 204, 204);">
                                        <strong id="SelectedStartTime">2016-09-01</strong> 至 <strong id="SelectedEndTime">2016-12-01</strong>
                                    </div>
                                    <div class="ui-filter-list" style="width: 350px; display: none;">
                                        <table class="form" id="filter-form">
                                            <tbody><tr>
                                                <th class="formTitle">操作时间：</th>
                                                <td class="formValue">
                                                    <input id="Category" type="hidden" value="1">
                                                    <div style="float: left; width: 45%;">
                                                        <input id="StartTime" readonly="" type="text" value="2016-11-24" class="form-control input-wdatepicker" onFocus="WdatePicker({maxDate:&#39;%y-%M-%d&#39;})">
                                                    </div>
                                                    <div style="float: left; width: 10%; text-align: center;">至</div>
                                                    <div style="float: left; width: 45%;">
                                                        <input id="EndTime" readonly="" type="text" value="2016-12-01" class="form-control input-wdatepicker" onFocus="WdatePicker({maxDate:&#39;%y-%M-%d&#39;})">
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formTitle">操作用户：</td>
                                                <td class="formValue">
                                                    <input id="OperateAccount" type="text" class="form-control">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formTitle">IP地址：</td>
                                                <td class="formValue">
                                                    <input id="IPAddress" type="text" class="form-control">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formTitle">操作类型：</td>
                                                <td class="formValue">
                                                    <input id="OperateType" type="text" class="form-control">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="formTitle">功能模块：</td>
                                                <td class="formValue">
                                                    <input id="Module" type="text" class="form-control">
                                                </td>
                                            </tr>
                                        </tbody></table>
                                        <div class="ui-filter-list-bottom">
                                            <a id="btn_Reset" class="btn btn-default">&nbsp;重&nbsp;&nbsp;置</a>
                                            <a id="btn_Search" class="btn btn-primary">&nbsp;查&nbsp;&nbsp;询</a>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td style="padding-left: 10px;">
                                <div id="time_horizon" class="btn-group">
                                    <a class="btn btn-default" data-value="1">今天</a>
                                    <a class="btn btn-default" data-value="2">近7天</a>
                                    <a class="btn btn-default" data-value="3">近1个月</a>
                                    <a class="btn btn-default active" data-value="4">近3个月</a>
                                </div>
                            </td>
                        </tr>
                    </tbody></table>
                </div>
                <div class="toolbar">
                    <div class="btn-group">
                        <a id="lr-replace" class="btn btn-default" onClick="$.learunUIBase.reload();" authorize="no"><i class="fa fa-refresh"></i>&nbsp;刷新</a>
                    </div>
                    <div class="btn-group">
                        <a id="lr-removelog" class="btn btn-default" onClick="btn_RemoveLog()" authorize="no"><i class="fa fa-eraser"></i>&nbsp;清空</a>
                        <a id="lr-export" class="btn btn-default" onClick="btn_export()" authorize="no"><i class="fa fa-sign-out"></i>&nbsp;导出</a>
                    </div>
                    <script>                        $('.toolbar').authorizeButton()</script>
                </div>
            </div>
            <div class="gridPanel" style="height:500px;">
                
            </div>
        </div>
    </div>
<div id="" class="ui-layout-resizer ui-layout-resizer-west ui-layout-resizer-open ui-layout-resizer-west-open" title="Resize" aria-disabled="false" style="position: absolute; padding: 0px; margin: 0px; font-size: 1px; text-align: left; overflow: hidden; z-index: 2; background: transparent; border: none; cursor: col-resize; left: 200px; height: 612px; width: 5px; top: 0px;"></div></div>

    <input name="__RequestVerificationToken" type="hidden" value="Fb56GoO_SBceor8rxg4vYdIvGk1BYHo3hU5molSi0PYD-2IIZYCszQhZ2GrYD9tvdYJO7_QN0zu9jAdlxzW9hX8DxzohM-JXxvmcdc-4Nmc1">


<script>
    $(function () {
        $.learunUIBase.ajaxLoading(false);
    })
</script>
<div lang="zh-cn" style="position: absolute; z-index: 100002; display: none; top: 105px; left: 498.141px;"><iframe hidefocus="true" width="97" height="9" frameborder="0" border="0" scrolling="no" style="width: 202px; height: 220px;" src="./leftbqys_files/saved_resource.html"></iframe></div></body></html>
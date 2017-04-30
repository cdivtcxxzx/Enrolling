function load(){
    var pk_sno = $("#pk_sno").val();//初始值由服务器回传网页时生成

    if (pk_sno == null || $.trim(pk_sno).length == 0 ) {
        alert("无效的参数");
        return;
    }
    $('.container div').remove();

    //NO:42 获取学生事务操作列表
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_freshstudent_affair_status_oper_list", "pk_sno": pk_sno},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                if (json_data.data != null && json_data.data.length > 0) {
                    for (var i = 0; i < json_data.data.length; i++) {
                        var PK_Affair_NO = json_data.data[i].PK_Affair_NO;
                        var Affair_Name = json_data.data[i].Affair_Name;
                        var Affair_Type = json_data.data[i].Affair_Type;
                        var Affair_Char = json_data.data[i].Affair_Char;
                        var Affair_Status = json_data.data[i].Affair_Status;
                        var StatusDisplay = json_data.data[i].StatusDisplay;
                        var Oper_Url = json_data.data[i].Oper_Url;
                        Affair_Char=Affair_Char.toUpperCase();
                        StatusDisplay=StatusDisplay.toUpperCase();
                        var content = "";
                        if (Affair_Char== 'STATUS' || StatusDisplay=='NO') {
                            content = "<div class=\"layui-form-item\" pane=\"\" style=\"min-height: 56px\">"
                            +" <label class=\"layui-form-label\" style=\"width: 150px;\">"
                            +"<a class=\"layui-btn\" style=\"width:100%;\" href=\"javascript:void(0)\" onclick='action(\""+PK_Affair_NO+"\",\""+pk_sno+"\")'>"+Affair_Name + "</a></label>"
                            +"<div class=\"layui-input-block\" style=\"margin-left: 150px;\">"
                            +"<div class=\"layui-form-mid layui-word-aux-ts\" style=\"margin-left: 40px; padding: 18px 0;\">"
                            +"<span id=\"xsxx_xh\"><font color=\"red\"><b></b></font></span>"
                            + "</div></div></div>";
                        } else {
                            content = "<div class=\"layui-form-item\" pane=\"\" style=\"min-height: 56px\">"
                                +" <label class=\"layui-form-label\" style=\"width: 150px;\">"
                                +"<a class=\"layui-btn\" style=\"width:100%;\" href=\"javascript:void(0)\" onclick='action(\""+PK_Affair_NO+"\",\""+pk_sno+"\")'>"+Affair_Name + "</a></label>"
                                +"<div class=\"layui-input-block\" style=\"margin-left: 150px;\">"
                                +"<div class=\"layui-form-mid layui-word-aux-ts\" style=\"margin-left: 40px; padding: 18px 0;\">"
                                +"<span id=\"xsxx_xh\"><font color=\"red\"><b>" + Affair_Status + "</b></font></span>"
                                + "</div></div></div>";
                        }
                        $('.xsxx2').append(content);
                    }
                } else {
                    alert('未获取到有效的事务操作列表');
                }
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });

}

function action(pk_affair_no,pk_sno){
    //10、迎新事务定义 获取某迎新事务
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_affair","pk_affair_no": pk_affair_no },
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                var name=json_data.data.Affair_Name;
                var precondition1message=json_data.data.precondition1Message;//使能条件1信息提示
                var precondition2message=json_data.data.precondition2Message;//使能条件2信息提示

                //NO:13 校验学生事务操作条件
                $.ajax({
                    url: "/nradmingl/appserver/manager.aspx",
                    type: "get",
                    dataType: "text",
                    data: { "cs": "check_student_affair_condition", "pk_affair_no": pk_affair_no,"pk_sno": pk_sno},
                    success: function (data) {
                        var json_data = JSON.parse(data);
                        if (json_data.code == 'success') {
                            if(json_data.data==true){
                                //NO:18 获取某迎新事务操作
                                $.ajax({
                                    url: "/nradmingl/appserver/manager.aspx",
                                    type: "get",
                                    dataType: "text",
                                    data: { "cs": "get_oper","pk_affair_no": pk_affair_no },
                                    success: function (data) {
                                        var json_data = JSON.parse(data);
                                        if (json_data.code == 'success') {
                                            var url=json_data.data.OPER_URL+'?pk_affair_no='+pk_affair_no+'&pk_sno='+pk_sno;
                                            window.location.href=url;
                                        } else {
                                            alert(json_data.message);
                                        }
                                    },
                                    error: function (data) {
                                        alert("错误");
                                    }
                                });

                            }else{
                                alert(precondition1message+','+precondition2message);
                                //alert('目前不具备操作当前事务的条件，请检查当前事务的前置条件是否具备')
                            }
                        } else {
                            alert(json_data.message);
                        }
                    },
                    error: function (data) {
                        alert("错误");
                    }
                });
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });
}
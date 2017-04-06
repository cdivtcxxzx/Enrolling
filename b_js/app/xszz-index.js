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
                        var Oper_Url = json_data.data[i].Oper_Url;
                        Affair_Char=Affair_Char.toUpperCase();
                        var content = "";
                        if (Affair_Char== 'STATUS') {
                            content = "<div class=\"col-xs-12 col-sm-6\">"
                                + "<div class=\"col-xs-8\"><i class=\"glyphicon glyphicon-tags\"></i><a href=\"javascript:void(0)\" onclick='action(\""+PK_Affair_NO+"\",\""+pk_sno+"\")'>" + Affair_Name + "</a></div>"
                                + "<div class=\"col-xs-4\"><span></span></div>"
                                + "</div>";
                        } else {
                            content = "<div class=\"col-xs-12 col-sm-6\">"
                                + "<div class=\"col-xs-8\"><i class=\"glyphicon glyphicon-tags\"></i><a href=\"javascript:void(0)\" onclick='action(\""+PK_Affair_NO+"\",\""+pk_sno+"\")'>"+Affair_Name + "</a></div>"
                                + "<div class=\"col-xs-4\"><span>" + Affair_Status + "</span></div>"
                                + "</div>";
                        }
                        $('.container').append(content);
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
                                var url=json_data.data.OPER_URL;
                                console.log(url);
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
                    alert('目前不具备操作当前事务的条件，请检查当前事务的前置条件是否具备')
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
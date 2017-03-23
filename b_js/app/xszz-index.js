

function xxload(){
    var pk_sno = $("#pk_sno").val();//初始值由服务器回传网页时生成

    if (pk_sno == null || $.trim(pk_sno).length == 0 ) {
        alert("无效的参数");
        return;
    }
    $('.container div').remove();

    //NO:42 获取学生事务操作列表
    $.ajax({
        url: "appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_freshstudent_affair_list", "pk_sno": pk_sno},
        success: function (data) {
            console.log(data);
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                if (json_data.data != null && json_data.data.length > 0) {
                    for (var i = 0; i < json_data.data.length; i++) {
                        var name = json_data.data[i].Affair_Name;//事务名称
                        var pk_affair_no=json_data.data[i].PK_Affair_NO;//事务主键
                        var content="<div class=\"col-xs-12 col-sm-6\">"
                            +"<div class=\"col-xs-8\"><i class=\"glyphicon glyphicon-tags\"></i><a href=\"#\">学生操作1</a></div>"
                            +"<div class=\"col-xs-4\"><span>已完成</span></div>"
                            +"</div>";
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
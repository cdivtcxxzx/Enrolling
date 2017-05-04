function load(){
    var server_msg=$("#server_msg").val();
    if ($.trim(server_msg).length > 0 ) {
        alert(server_msg);
        return;
    }
    var pk_sno = $("#pk_sno").val();//初始值由服务器回传网页时生成
    var pk_batch_no= $("#pk_batch_no").val();
    var pk_affair_no= $("#pk_affair_no").val();
    if (pk_sno == null || $.trim(pk_sno).length == 0 || pk_batch_no == null || $.trim(pk_batch_no).length == 0
        || pk_affair_no == null || $.trim(pk_affair_no).length == 0) {
        alert("无效的参数");
        return;
    }

    var pk_staff_no= $("#pk_staff_no");
    if (!pk_staff_no)
    {
        //alert("null or undefined or NaN");
    }else{
        pk_staff_no= $("#pk_staff_no").val();
    }


    //NO:14&15&16 获取学生数据
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_student","pk_sno": pk_sno},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                var i=0;
                if(json_data.data!=null && json_data.data.length>0){
                    for(i=0;i<json_data.data.length;i++){
                        if(json_data.data[i].name=='student'){
                            $('#xsxx_xm').html(json_data.data[i].data.Name);
                            $('#xsxx_sfzh').html(json_data.data[i].data.ID_NO);
                        }
                        if(json_data.data[i].name=='spe'){
                            $('#xsxx_zymc').html(json_data.data[i].data.SPE_Name);
                        }
                    }
                }else{
                    alert('无法获取学号为'+pk_sno+' 的同学详细信息')
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

function sure(){
    var pk_sno = $("#pk_sno").val();//初始值由服务器回传网页时生成
    var pk_affair_no= $("#pk_affair_no").val();

    if (pk_sno == null || $.trim(pk_sno).length == 0  || pk_affair_no == null || $.trim(pk_affair_no).length == 0) {
        alert("无效的参数");
        return;
    }
    var pk_staff_no= $("#pk_staff_no").val();

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx?cs=set_freshstudent_register",
        type: "post",
        dataType: "text",
        data: { "pk_sno": pk_sno,"pk_affair_no":pk_affair_no,"pk_staff_no":pk_staff_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                alert('成功');
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });

}

function load() {
    var pk_staff_no=$('#pk_staff_no').val();

    if (pk_staff_no == null || $.trim(pk_staff_no).length == 0 ) {
        alert("无效的参数");
        return;
    }

    $('#batchlist option').remove();
    $('#classlist option').remove();

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: {"cs": 'get_batch_counseller', "pk_staff_no": pk_staff_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                for (i = 0; json_data.data != null && i < json_data.data.length; i++) {
                    var item = json_data.data[i];
                    var str = '';
                    if (i == 0) {
                        str = '<option value="' + item.PK_Batch_NO + '" selected="">' + item.Batch_Name + '</option>';
                        $('#batchlist').append(str);
                    }else{
                        str = '<option value="' + item.PK_Batch_NO + '" >' + item.Batch_Name + '</option>';
                        $('#batchlist').append(str);
                    }
                }
                $('#batchlist').change(function () {
                    batchchange();
                });
                batchchange();//装入班级数据
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });
}


function batchchange() {
    var pk_batch_no=$('#batchlist').children('option:selected').val();
    var pk_staff_no=$('#pk_staff_no').val();

    $('#classlist option').remove();

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: {"cs": 'get_batch_ClassByCounseller', "pk_batch_no": pk_batch_no,"pk_staff_no":pk_staff_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                for (i = 0; json_data.data != null && i < json_data.data.length; i++) {
                    var item = json_data.data[i];
                    var str = '';
                    if (i == 0) {
                        str = '<option value="' + item.PK_Class_NO + '" selected="">' + item.Name + '</option>';
                        $('#classlist').append(str);
                    }else{
                        str = '<option value="' + item.PK_Class_NO + '" >' + item.Name + '</option>';
                        $('#classlist').append(str);
                    }
                }
                $('#classlist').change(function () {
                    getstudentstatus();
                });
                getstudentstatus();
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });

}

function getstudentstatus() {
    var pk_class_no=$('#classlist').children('option:selected').val();
    var pk_batch_no=$('#batchlist').children('option:selected').val();

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_classstudentaffairlog","pk_batch_no":pk_batch_no,"pk_class_no":pk_class_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                $('#studentlist tr').remove();
                if(json_data.data!=null && json_data.data.length>0){
                    var item=json_data.data[0];
                    var str='<tr>';
                    for(var key in item){
                        str=str+'<td>'+key+'</td>';
                    }
                    str=str+'</tr>';
                    $('#studentlist').append(str);
                }
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    //console.log(item);
                    var str='<tr>';
                    for(var key in item){
                        str=str+'<td>'+item[key]+'</td>';
                    }
                    str=str+'</tr>';
                    $('#studentlist').append(str);
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


function studentdetail(){
    parent.layer.open({  type: 2,  title: '详细信息',  shadeClose: true,  shade: 0.8,  area: ['98%', '98%'],  content: '/view/czzt_detail.aspx',btn:'关闭'})

}




function load() {
    var cs=$('#cs').val();
    var pk_batch_no=$('#pk_batch_no').val();
    var pk_staff_no=$('#pk_staff_no').val();

    if (pk_batch_no == null || $.trim(pk_batch_no).length == 0 ) {
        alert("无效的参数");
        return;
    }
    if (pk_staff_no == null || $.trim(pk_staff_no).length == 0 ) {
        alert("无效的参数");
        return;
    }
    $('#classlist option').remove();
    $('#affairlist option').remove();

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
                        //classchange(item.PK_Class_NO);
                    }else{
                        str = '<option value="' + item.PK_Class_NO + '" >' + item.Name + '</option>';
                        $('#classlist').append(str);
                    }
                }
                $('#classlist').change(function () {
                    //classchange($(this).children('option:selected').val());
                    classoraffairchange();
                });
                //装入事务列表
                $.ajax({
                    url: "/nradmingl/appserver/manager.aspx",
                    type: "get",
                    dataType: "text",
                    data: {"cs": 'get_batch_affairlist', "pk_batch_no": pk_batch_no},
                    success: function (data) {
                        var json_data = JSON.parse(data);
                        if (json_data.code == 'success') {
                            for (i = 0; json_data.data != null && i < json_data.data.length; i++) {
                                var item = json_data.data[i];
                                var str = '';
                                if (i == 0) {
                                    str = '<option value="' + item.PK_Affair_NO + '" selected="">' + item.affair_name + '</option>';
                                    $('#affairlist').append(str);
                                    //affairchange(item.PK_Affair_NO);
                                }else{
                                    str = '<option value="' + item.PK_Affair_NO + '" >' + item.affair_name + '</option>';
                                    $('#affairlist').append(str);
                                }
                            }
                            $('#affairlist').change(function () {
                                //affairchange($(this).children('option:selected').val());
                                classoraffairchange();
                            });
                            classoraffairchange();//装入学生数据
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


function classchange(pk_class_no) {
    console.log('hi,class');
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_classstudent","pk_class_no":pk_class_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                $('#studentlist tbody tr').remove();
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    var str='<tr>';
                    str=str+'<td>'+(i+1)+'</td>';
                    str=str+'<td>'+item.year+'</td>';
                    str=str+'<td>'+item.collage+'</td>';
                    str=str+'<td>'+item.spe_name+'</td>';
                    str=str+'<td>'+item.name+'</td>';
                    str=str+'<td>'+item.gender+'</td>';
                    str=str+'<td>'+item.pk_sno+'</td>';
                    str=str+'<td>'+item.test_no+'</td>';
                    str=str+'<td>'+item.id_no+'</td>';
                    str=str+'<td>'+item.Status_Code+'</td>';
                    str=str+'<td>'+item.TuitionType+'</td>';
                    str=str+'<td>';
                    str=str+'<a href="#" onclick="studentdetail()" class="layui-btn layui-btn-mini" title="学生信息">学生详情</a>';
                    str=str+'</td>';
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

function affairchange(pk_affair_no) {
    console.log('hi,affair');
}


function classoraffairchange() {
    console.log('hi,class');
    var pk_class_no=$('#classlist').children('option:selected').val();
    var pk_affair_no=$('#affairlist').children('option:selected').val()

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_classstudentandaffairstatus","pk_class_no":pk_class_no,"pk_affair_no":pk_affair_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                $('#studentlist tbody tr').remove();
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    var str='<tr>';
                    str=str+'<td>'+(i+1)+'</td>';
                    str=str+'<td>'+item.year+'</td>';
                    str=str+'<td>'+item.collage+'</td>';
                    str=str+'<td>'+item.spe_name+'</td>';
                    str=str+'<td>'+item.name+'</td>';
                    str=str+'<td>'+item.gender+'</td>';
                    str=str+'<td>'+item.pk_sno+'</td>';
                    str=str+'<td>'+item.test_no+'</td>';
                    str=str+'<td>'+item.id_no+'</td>';
                    str=str+'<td>'+item.Status_Code+'</td>';
                    str=str+'<td>'+item.TuitionType+'</td>';
                    str=str+'<td>'+item.affairstatus+'</td>';
                    str=str+'<td>';
                    str=str+'<a href="#" onclick="studentdetail()" class="layui-btn layui-btn-mini" title="学生信息">学生详情</a>';
                    str=str+'</td>';
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




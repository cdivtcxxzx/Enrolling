function load() {
    var pk_staff_no = $('#pk_staff_no').val();

    if (pk_staff_no == null || $.trim(pk_staff_no).length == 0) {
        alert("无效的参数");
        return;
    }

    $('#batchlist option').remove();
    $('#classlist option').remove();

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": 'get_batch_counseller', "pk_staff_no": pk_staff_no },
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                for (i = 0; json_data.data != null && i < json_data.data.length; i++) {
                    var item = json_data.data[i];
                    var str = '';
                    if (i == 0) {
                        str = '<option value="' + item.PK_Batch_NO + '" selected="">' + item.Batch_Name + '</option>';
                        $('#batchlist').append(str);
                    } else {
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
    var pk_batch_no = $('#batchlist').children('option:selected').val();
    var pk_staff_no = $('#pk_staff_no').val();

    $('#classlist option').remove();

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": 'get_batch_ClassByCounseller', "pk_batch_no": pk_batch_no, "pk_staff_no": pk_staff_no },
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                for (i = 0; json_data.data != null && i < json_data.data.length; i++) {
                    var item = json_data.data[i];
                    var str = '';
                    if (i == 0) {
                        str = '<option value="' + item.PK_Class_NO + '" selected="">' + item.Name + '</option>';
                        $('#classlist').append(str);
                    } else {
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
    var pk_class_no = $('#classlist').children('option:selected').val();
    var pk_batch_no = $('#batchlist').children('option:selected').val();
    
    var index = parent.layer.open({
        type: 1,
        title: '信息提示',
        content: '查询学生状态时间较长，请耐心等待...', //这里content是一个普通的String
        area: ['300px', '150px'],
        resize: false,
        cancel: function (index, layero) {
            return false;
        }
    });

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_classstudentaffairlog", "pk_batch_no": pk_batch_no, "pk_class_no": pk_class_no },
        success: function (data) {
            var json_data = JSON.parse(data);
            var yes_count = 0;
            var no_count = 0;
            if (json_data.code == 'success') {
                $('#studentlist thead').remove();
                $('#studentlist tbody').remove();
                if (json_data.data != null && json_data.data.length > 0) {
                    var item = json_data.data[0];
                    var str = '<thead>';
                    str = str + '<tr><th  scope="col" >序号</th>';
                    for (var key in item) {
                        if (key == '姓名' || key == '性别' || key == '选择宿舍') {
                            str = str + '<th scope="col">' + key + '</th>';
                            //str = str + '<th  scope="col" class="hidden-xs">' + key + '</th>';
                        }
                        else if(key != 'pk_sno' ) {
                            str = str + '<th scope="col" class="hidden-xs">' + key + '</th>';
                        }
                    }
                    str = str + '<th></th>';
                    str = str + '</tr></thead>';
                    $('#studentlist').append(str);
                }
                $('#studentlist').append('<tbody>');
                for (i = 0; json_data.data != null && i < json_data.data.length; i++) {
                    var item = json_data.data[i];
                    //console.log(item);

                    var str = '<tr>';
                    str = str + '<td>' + (i + 1) + '</td>';
                    for (var key in item) {
                        //if (key != 'pk_sno') {
                        //    str = str + '<td>' + item[key] + '</td>';
                        //}
                        if (key == '姓名' || key == '性别' || key == '选择宿舍') {
                            str = str + '<td>' + item[key] + '</td>';
                            //if (item[key] == '未报到') {
                            //    str = str + '<td id="' + item.pk_sno + '" style="color:red">未报到</td>';
                            //} else if (item[key] == '已完成') {
                            //    str = str + '<td id="' + item.pk_sno + '" style="color:blue">已完成</td>';
                            //} else {
                            //    str = str + '<td>' + item[key] + '</td>';
                            //}
                            
                        }
                        else if (key != 'pk_sno') {
                            if (item[key] == '未报到') {
                                str = str + '<td class="hidden-xs" id="' + item.pk_sno + '" style="color:red">未报到</td>';
                            } else if (item[key] == '已完成') {
                                str = str + '<td class="hidden-xs" id="' + item.pk_sno + '" style="color:blue">已完成</td>';
                            } else {
                                str = str + '<td class="hidden-xs">' + item[key] + '</td>';
                            }
                            //str = str + '<td class="hidden-xs">' + item[key] + '</td>';
                        }
                    }
                    str = str + '<td>';
                    if (item["现场报到确认"] == "已完成") {
                        str = str + '<a href="#"  class="layui-btn layui-btn-mini  layui-btn-warm">已 报 到</a>  <a href="#" onclick="studentdetail(' + item.pk_sno + ')" class="layui-btn layui-btn-mini" title="学生信息">详情</a>';
                    } else {
                        str = str + '<a href="#" id="btn_' + item.pk_sno + '" onclick="studentConfrim(' + item.pk_sno + ')" class="layui-btn layui-btn-mini">确认报到</a> <a href="#" onclick="studentdetail(' + item.pk_sno + ')"  class="layui-btn layui-btn-mini" title="学生信息">详情</a>';
                    }
                    
                    str = str + '</td>';
                    str = str + '</tr>';
                    $('#studentlist').append(str);
                    if ($.trim(item['现场报到确认']) == '已完成') {
                        yes_count = yes_count + 1;
                    }
                    if ($.trim(item['现场报到确认']) == '未报到') {
                        no_count = no_count + 1;
                    }
                }
                $('#count').html('总计：' + (yes_count + no_count) + '人');
                $('#confirm_count').html('已报到：' + yes_count + '人');
                $('#noConfirm_count').html('未报到：' + no_count + '人');

                $('#studentlist').append('</tbody>');

            } else {
                alert(json_data.message);
            }
            parent.layer.close(index);
        },
        error: function (data) {
            parent.layer.close(index);
            alert("错误");
        }
    });
}



function studentdetail(pk_sno) {
    parent.layer.open({ type: 2, title: '详细信息', shadeClose: true, shade: 0.8, area: ['98%', '98%'], content: '/view/bzr_xsjbxx.aspx?pk_sno=' + pk_sno, btn: '关闭' })
}
function studentConfrim(pk_sno) {
    var pk_staff_no = $('#pk_staff_no').val();
    var this_btn = $('#btn_'+pk_sno);
    parent.layer.confirm('确认报到后将不能取消，是否继续？', {
        btn: ['确定', '取消']
    }, function () {
        $.ajax({
            url: "/nradmingl/appserver/manager.aspx",
            type: "get",
            dataType: "text",
            data: { "cs": "set_freshstudent_register_for_Counseller", "pk_sno": pk_sno, "pk_staff_no": pk_staff_no },
            success: function (data) {
                var json_data = JSON.parse(data);
                if (json_data.code == 'success') {
                    parent.layer.alert('确认成功！');
                    $('#' + pk_sno).attr("style", "color:blue").html("已完成");
                    this_btn.html("已 报 到").attr("class", "layui-btn layui-btn-mini  layui-btn-warm");
                } else {
                    parent.layer.alert('确认失败：' + json_data.message);
                }
            }
        });
    }, function () { });

   
}







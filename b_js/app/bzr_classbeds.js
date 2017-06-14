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
                    getclassbeds();
                });
                getclassbeds();
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });

}



function getclassbeds() {
    var pk_class_no=$('#classlist').children('option:selected').val();
    $('#count').html('学生总数：0人');
    $('#boy_count').html('男生数：0人');
    $('#girl_count').html('女生数：0人');

    $('#bed_count').html('床位总数：0张');
    $('#bed_boy_count').html('男床位：0张');
    $('#bed_girl_count').html('女床位：0张');

    $('#stu_bed_count').html('剩余床位总数：0张');
    $('#stu_bed_boy_count').html('剩余男床位：0张');
    $('#stu_bed_girl_count').html('剩余女床位：0张');

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_classstudent","pk_class_no":pk_class_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                var boy_count=0;
                var girl_count=0;
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    if($.trim(item.gender)=='男'){
                        boy_count=boy_count+1;
                    }
                    if($.trim(item.gender)=='女'){
                        girl_count=girl_count+1;
                    }
                }
                $('#count').html('学生总数：'+(boy_count+girl_count)+'人');
                $('#boy_count').html('男生数：'+boy_count+'人');
                $('#girl_count').html('女生数：'+girl_count+'人');
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });


    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_classbedstudent","pk_class_no":pk_class_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                $('#studentlist tbody tr').remove();
                var bed_boy_count=0;
                var bed_girl_count=0;
                var stu_bed_boy_count=0;
                var stu_bed_girl_count=0;
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    var str='<tr>';
                    str=str+'<td>'+(i+1)+'</td>';
                    str=str+'<td>'+item.Campus_Name+'</td>';
                    str=str+'<td>'+item.DormName+'</td>';
                    str=str+'<td>'+item.Floor+'</td>';
                    str=str+'<td>'+item.Type_Name+'</td>';
                    str=str+'<td>'+item.Room_NO+'</td>';
                    str=str+'<td>'+item.Gender+'</td>';
                    str=str+'<td>'+item.Bed_NO+'</td>';
                    str=str+'<td>'+item.Bed_Name+'</td>';
                    if($.trim(item.Gender)=='男'){
                        bed_boy_count=bed_boy_count+1;
                    }
                    if($.trim(item.Gender)=='女'){
                        bed_girl_count=bed_girl_count+1;
                    }

                    if(item.studentname==null){
                        str=str+'<td></td>';
                        str=str+'<td></td>';
                    }else{
                        if($.trim(item.Gender)=='男'){
                            stu_bed_boy_count=stu_bed_boy_count+1;
                        }
                        if($.trim(item.Gender)=='女'){
                            stu_bed_girl_count=stu_bed_girl_count+1;
                        }
                        str=str+'<td>'+item.studentname+'</td>';
                        str=str+'<td>';
                        str=str+'<a href="#" onclick="studentdetail('+item.FK_SNO+')" class="layui-btn layui-btn-mini" title="学生信息">学生详情</a>';
                        str=str+'</td>';
                    }
                    str=str+'</tr>';
                    $('#studentlist').append(str);
                }
                $('#bed_count').html('床位总数：'+(bed_boy_count+bed_girl_count)+'张');
                $('#bed_boy_count').html('男床位：'+bed_boy_count+'张');
                $('#bed_girl_count').html('女床位：'+bed_girl_count+'张');

                $('#stu_bed_count').html('剩余床位总数：'+(bed_boy_count+bed_girl_count-stu_bed_boy_count-stu_bed_girl_count)+'张');
                $('#stu_bed_boy_count').html('剩余男床位：'+(bed_boy_count-stu_bed_boy_count)+'张');
                $('#stu_bed_girl_count').html('剩余女床位：'+(bed_girl_count-stu_bed_girl_count)+'张');
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });

}


function studentdetail(pk_sno){
    parent.layer.open({  type: 2,  title: '详细信息',  shadeClose: true,  shade: 0.8,  area: ['98%', '98%'],  content: '/view/bzr_xsjbxx.aspx?pk_sno='+pk_sno,btn:'关闭'})

}




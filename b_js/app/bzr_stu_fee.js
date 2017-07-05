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
                    getstudent();
                });
                getstudent();
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });

}



function getstudent() {
    var pk_batch_no=$('#batchlist').children('option:selected').val();
    var pk_class_no=$('#classlist').children('option:selected').val();

    var index = parent.layer.open({
        type: 1,
        title: '信息提示',
        content:'查询学生状态时间较长，请耐心等待...', //这里content是一个普通的String
        area: ['300px', '150px'],
        resize:false,
        cancel: function(index, layero){
            return false;
        }
    });

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_classstudent_fee","pk_class_no":pk_class_no,"pk_batch_no":pk_batch_no},
        success: function (data) {
            //console.log(data);
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                $('#studentlist thead').remove();
                $('#studentlist tbody').remove();
                if(json_data.data!=null && json_data.data.length>0){
                    $('#count').html('总计：'+json_data.data.length+'人');
                    var item=json_data.data[0];
                    var str='<thead>';
                    str=str+'<tr><th  scope="col">序号</th>';
                    for(var key in item){
                        if(key!='pk_sno'){
                            str=str+'<th  scope="col">'+key+'</th>';
                        }
                    }
                    str=str+'<th></th>';
                    str=str+'</tr></thead>';
                    $('#studentlist').append(str);
                }

                $('#studentlist').append('<tbody>');
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    //console.log(item);
                    var str='<tr>';
                    str=str+'<td>'+(i+1)+'</td>';
                    for(var key in item){
                        if(key!='pk_sno'){
                            if(item[key]=='已缴'){
                                str=str+'<td style="color:green">'+item[key]+'</td>';
                            }else{
                                if(item[key]=='欠费'){
                                    str=str+'<td style="color:red">'+item[key]+'</td>';
                                }else{
                                    str=str+'<td>'+item[key]+'</td>';
                                }
                            }
                        }
                    }
                    str=str+'<td>';
                    str=str+'<a href="#" onclick="studentdetail('+item.pk_sno+')" class="layui-btn layui-btn-mini" title="学生信息">学生详情</a>';
                    str=str+'</td>';
                    str=str+'</tr>';
                    $('#studentlist').append(str);
                }
                $('#studentlist').append('</tbody>');
                parent.layer.close(index);
            } else {
                parent.layer.close(index);
                alert(json_data.message);
            }
        },
        error: function (data) {
            parent.layer.close(index);
            alert("错误");
        }
    });
}


function studentdetail(pk_sno){
    parent.layer.open({  type: 2,  title: '详细信息',  shadeClose: true,  shade: 0.8,  area: ['98%', '98%'],  content: '/view/bzr_xsjbxx.aspx?pk_sno='+pk_sno,btn:'关闭'})

}




function load() {
    var cs=$('#cs').val();
    //var pk_batch_no=$('#pk_batch_no').val();
    var pk_staff_no=$('#pk_staff_no').val();

/*    if (pk_batch_no == null || $.trim(pk_batch_no).length == 0 ) {
        alert("无效的参数");
        return;
    }*/
    if (pk_staff_no == null || $.trim(pk_staff_no).length == 0 ) {
        alert("无效的参数");
        return;
    }
    $('#batchlist option').remove();
    $('#classlist option').remove();
    $('#affairlist option').remove();

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

function batchchange(){
    var pk_batch_no=$('#batchlist').children('option:selected').val();
    var pk_staff_no=$('#pk_staff_no').val();

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
/*                    str=str+'<td>'+item.year+'</td>';
                    str=str+'<td>'+item.collage+'</td>';
                    str=str+'<td>'+item.spe_name+'</td>';*/
                    str=str+'<td>'+item.name+'</td>';
                    str=str+'<td>'+item.gender+'</td>';
                    str=str+'<td>'+item.pk_sno+'</td>';
                    str=str+'<td>'+item.test_no+'</td>';
                    str=str+'<td>'+item.id_no+'</td>';
                    if($.trim(item.phone)===','){
                        str=str+'<td></td>';
                    }else{
                        str=str+'<td>'+item.phone+'</td>';
                    }
                    str=str+'<td>'+item.register+'</td>';
                    str=str+'<td>'+item.TuitionType+'</td>';
                    str=str+'<td></td>';
                    str=str+'<td>';
                    str=str+'<a href="#" onclick="studentdetail('+item.pk_sno+')" class="layui-btn layui-btn-mini" title="学生信息">学生详情</a>';
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
}


function classoraffairchange() {
    var pk_class_no=$('#classlist').children('option:selected').val();
    var pk_affair_no=$('#affairlist').children('option:selected').val();
    $('#itemname').html($('#affairlist').children('option:selected').html());

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

    //console.log(pk_class_no);
    //console.log(pk_affair_no);

    $('#zj').html('总计：0人');
    $('#zc').html('网上注册：0人');
    //$('#lstd').html('绿色通道：0人');
    $('#zxdk').html('助学贷款：0人');
    $('#xm').html('项目：0人');

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_classstudentandaffairstatus","pk_class_no":pk_class_no,"pk_affair_no":pk_affair_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                $('#studentlist tbody tr').remove();
                var zj=0;
                var zc=0;
                var zcyw=0;
                var lstd=0;
                var zxdk=0;
                var xm=0;
                var xmzt=new Array();
                var xmzt_count=new Array();
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    var str='<tr>';
                    str=str+'<td>'+(i+1)+'</td>';
/*                    str=str+'<td>'+item.year+'</td>';
                    str=str+'<td>'+item.collage+'</td>';
                    str=str+'<td>'+item.spe_name+'</td>';*/
                    str=str+'<td>'+item.name+'</td>';
                    str=str+'<td>'+item.gender+'</td>';
                    str=str+'<td>'+item.pk_sno+'</td>';
                    str=str+'<td>'+item.test_no+'</td>';
                    str=str+'<td>'+item.id_no+'</td>';
                    if($.trim(item.phone)===','){
                        str=str+'<td></td>';
                    }else{
                        str=str+'<td>'+item.phone+'</td>';
                    }
                    str=str+'<td>'+item.register+'</td>';
                    str=str+'<td>'+item.TuitionType+'</td>';
                    str=str+'<td>'+item.affairstatus+'</td>';

                    str=str+'<td>';
                    str=str+'<a href="#" onclick="studentdetail('+item.pk_sno+')" class="layui-btn layui-btn-mini" title="学生信息">学生详情</a>';
                    str=str+'</td>';
                    str=str+'</tr>';
                    $('#studentlist').append(str);
                    zj=zj+1;
                    if($.trim(item.register)=='注册'){
                        zc=zc+1;
                    }else{
                        if($.trim(item.register).indexOf('有误')>0){
                            zcyw=zcyw+1;
                        }
                    }
/*                    if($.trim(item.TuitionType)=='绿色通道'){
                        lstd=lstd+1;
                    }*/
                    if($.trim(item.TuitionType)=='助学贷款'){
                        zxdk=zxdk+1;
                    }
                    var find=false;
                    for(var j=0;j<xmzt.length;j++){
                        if(xmzt[j]==item.affairstatus){
                            xmzt_count[j]=xmzt_count[j]+1;
                            find=true;
                            break;
                        }
                    }
                    if(!find){
                        xmzt[xmzt.length]=item.affairstatus;
                        xmzt_count[xmzt_count.length]=1;
                    }
                }
                parent.layer.close(index);
                $('#zj').html('总计：'+zj+'人');
                if(zcyw==0){
                    $('#zc').html('网上注册：'+zc+'人('+parseInt(zc/zj*100)+'%)');
                }else{
                    $('#zc').html('网上注册：'+zc+'人('+parseInt(zc/zj*100)+'%),信息有误'+zcyw+'人');
                }
                //$('#lstd').html('绿色通道：'+lstd+'人('+parseInt(lstd/zj*100)+'%)');
                $('#zxdk').html('助学贷款：'+zxdk+'人('+parseInt(zxdk/zj*100)+'%)');

                var xmmc=$('#affairlist').children('option:selected').html();
                var str=' ';
                for(var i=0;i<xmzt.length;i++){
                    str=str+', 【'+xmzt[i]+'】'+xmzt_count[i]+'人('+parseInt(xmzt_count[i]/zj*100)+'%)';
                }
                $('#xm').html(xmmc+'：'+str.substring(1));
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




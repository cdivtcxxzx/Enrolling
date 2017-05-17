function fillstr(str){
    if ($.trim(str).length >=18 ) {
        return str;
    }
    var jg=str;
    for (var i=$.trim(str).length;i<=18;i++){
        jg=jg+'&nbsp';
    }
    return jg;
}

function load(){
    //迎新批次数据
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_freshbatch_welcome_list"},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    var str='';
                    if(i==0){
                        str=str+'<option value="'+item.PK_Batch_NO+'" selected="">'+item.Batch_Name+'</option>';
                        batchchange(item.PK_Batch_NO);
                    }else{
                        str=str+'<option value="'+item.PK_Batch_NO+'" >'+item.Batch_Name+'</option>';
                    }
                    $('#freshbatch').append(str);
                }

                layui.use(['form'], function () {
                    var $ = layui.jquery;
                    var form = layui.form();

                    //selectd的onchange事件
                    form.on('select', function (obj) {
                        var id=obj.elem.id;
                        if(id!=null && id=='freshbatch'){
                            batchchange(obj.value);
                        }
                    });
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

function batchchange(value){
    var pk_batch_no=value;
    //批次中的事务数据
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_affair_list","pk_batch_no":pk_batch_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                $('#affairlist tbody tr').remove();
                $('#stafflist tbody tr').remove();
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    var str='';
                    str=str+'<tr onclick="detail('+item.PK_Affair_NO+')"><td>'+item.Affair_Name+'</td><td>'+item.Affair_Type+'</td><td><a href="javascript:void(0);" onclick="add('+item.PK_Affair_NO+',\''+item.Affair_Name+'\');" class="layui-btn layui-btn-small">添加操作员</a></td></tr>';
                    $('#affairlist').append(str);
                    if(i==0){
                        detail(item.PK_Affair_NO);
                    }
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

//显示事务下的操作员
function detail(value){
    var pk_affair_no=value;
    //获取某事务下所有授权员工及操作范围数据
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "staff_affair_auth_scope","pk_affair_no":pk_affair_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                $('#stafflist tbody tr').remove();
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    var str='';
                    str=str+'<tr onclick="detail('+item.PK_Affair_NO+')">';
                    str=str+'<td>'+item.Affair_Name+'</td><td>'+item.welcome_type+'</td><td>'+item.PK_Staff_NO+'</td><td>';
                    str=str+item.Name+'</td><td>'+item.Year+'</td><td>'+item.FK_College_NAME_STR+'</td>';
                    str=str+'<td>'
                    +'<a href="#" class="layui-btn layui-btn-small" onclick=\'modify("'+item.PK_Staff_NO+'","'+item.PK_Batch_NO+'","'+item.Name+'","'+item.FK_College_NO_STR+'","'+item.FK_College_NAME_STR+'","'+item.Affair_Name+'","'+pk_affair_no+'")\'>修改</a>'
                    +'<a href="#" class="layui-btn layui-btn-small" onclick=\'deletestaff("'+item.PK_Staff_NO+'","'+item.PK_Batch_NO+'","'+pk_affair_no+'")\'>删除</a>'
                    +'</td>';
                    str=str+'</tr>';
                    $('#stafflist').append(str);
                }
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });}

//在某事务中增加操作员
function add(value,valuename){
    var pk_batch_no= $('#freshbatch').val();
    var pk_affair_no=value;
    var affairname=valuename;

    layui.use(['form'], function () {
        var $ = layui.jquery;
        layer.open({
            title: '事务名称：'+affairname,
            type: 1,
            //area: ['800px', '600px'],
            content: $('#addstaff') //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
            , btn: ['确定', '放弃']
            , btn1: function (index, layero) {
                //按钮【按钮一】的回调
                var yhid=$('#namelist').val();
                if(yhid){
                    var xm=$('#namelist').find("option:selected").text();
                    $('#username').html(xm);

                    layer.close(index);
                    $('#namelist option').remove();

                    $('#collegelist option').remove();
                    $('#selectedcollegelist option').remove();
                    $.ajax({
                        url: "/nradmingl/appserver/manager.aspx",
                        type: "get",
                        dataType: "text",
                        data: { "cs": "get_collegelist"},
                        success: function (data) {
                            var json_data = JSON.parse(data);
                            if (json_data.code == 'success') {
                                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                                    var item=json_data.data[i];
                                    var str='';
                                    str=str+'<option value="'+item.PK_College+'">'+item.Name+'</option>';
                                    $('#collegelist').append(str);
                                }
                            } else {
                                alert(json_data.message);
                            }
                        },
                        error: function (data) {
                            alert("错误");
                        }
                    });

                    layer.open({
                        title: '事务名称：'+affairname,
                        type: 1,
                        //area: ['800px', '600px'],
                        content: $('#addstaff_content') //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
                        , btn: ['确定', '放弃']
                        , btn1: function (index, layero) {
                            //按钮【按钮一】的回调
                            layer.close(index);

                            var data1=new Array();
                            var count=0;
                            $("#selectedcollegelist option").each(function(){
                                data1[count]=$(this).val();
                                count=count+1;
                            });
                            var colleges=data1.join(',');

                            $.ajax({
                                url: "/nradmingl/appserver/manager.aspx?cs=modify_operator_auth",
                                type: "post",
                                dataType: "text",
                                data: { "pk_batch_no":pk_batch_no,"pk_affair_no":pk_affair_no,"pk_staff_no": yhid,"colleges":colleges},
                                success: function (data) {
                                    var json_data = JSON.parse(data);
                                    if (json_data.code == 'success') {
                                        layer.close(index);
                                        detail(pk_affair_no);
                                    } else {
                                        alert(json_data.message);
                                    }
                                },
                                error: function (data) {
                                    alert("错误");
                                }
                            });
                        }
                        , btn2: function (index, layero) {
                            //按钮【按钮二】的回调
                            layer.close(index);

                            //return false 开启该代码可禁止点击该按钮关闭
                        }
                        , cancel: function () {
                            //右上角关闭回调
                            //return false 开启该代码可禁止点击该按钮关闭
                        }
                    });
                }else{
                    alert('请选择人员');
                }
            }
            , btn2: function (index, layero) {
                //按钮【按钮二】的回调
                layer.close(index);
                $('#namelist option').remove();
                //return false 开启该代码可禁止点击该按钮关闭
            }
            , cancel: function () {
                //右上角关闭回调
                $('#namelist option').remove();
                //return false 开启该代码可禁止点击该按钮关闭
            }
        });


    });
}

//操作用户
function finduser(){
    var username=$('#name').val();
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_yonghqx","username":username},
        success: function (data) {
            $('#namelist option').remove();
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    var str='';
                    str=str+'<option value="'+item.yhid+'">'+fillstr($.trim(item.yhid))+ $.trim(item.xm)+'</option>';
                    $('#namelist').append(str);
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

function additem(){
    var PK_College=$('#collegelist').val();
    if(PK_College){
    }else{
        alert('请选择"可选学院"');
        return;
    }
    var name=$('#collegelist').find("option:selected").text();
    var str='';
    str=str+'<option value="'+PK_College+'">'+name+'</option>';
    $('#selectedcollegelist').append(str);
    $('#collegelist').find("option:selected").remove();
}

function removeitem(){
    var PK_College=$('#selectedcollegelist').val();
    if(PK_College){
    }else{
        alert('请选择"已选学院"');
        return;
    }
    var name=$('#selectedcollegelist').find("option:selected").text();
    var str='';
    str=str+'<option value="'+PK_College+'">'+name+'</option>';
    $('#collegelist').append(str);
    $('#selectedcollegelist').find("option:selected").remove();
}

//修改操作员权限
function modify(staffno,pk_batch_no,staffname,collegenostr,collegenamestr,affairname,affairno){
    $('#username').html(staffname);

    $('#collegelist option').remove();
    $('#selectedcollegelist option').remove();

    var collegenolist_selected=collegenostr.split(',');
    var collegenamelist_selected=collegenamestr.split(',');
    var i=0;
    for(i=collegenolist_selected.length-1;i>=0;i--){
        if($.trim(collegenolist_selected[i]).length==0){
            collegenolist_selected.splice(i,1);
            collegenamelist_selected.splice(i,1);
        }
    }

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_collegelist"},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    var str='';
                    var j=0;
                    var k=0;
                    var selected=false;
                    for(j=0;j<collegenolist_selected.length;j++){
                        if($.trim(collegenolist_selected[j])=== $.trim(item.PK_College)){
                            selected=true;
                            break;
                        }
                    }
                    if(!selected){
                        str=str+'<option value="'+item.PK_College+'">'+item.Name+'</option>';
                        $('#collegelist').append(str);
                    }else{
                        str=str+'<option value="'+item.PK_College+'">'+item.Name+'</option>';
                        $('#selectedcollegelist').append(str);
                    }
                }
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });

    layui.use(['form'], function () {
        var $ = layui.jquery;
        layer.open({
            title: '事务名称：'+affairname,
            type: 1,
            //area: ['800px', '600px'],
            content: $('#addstaff_content') //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
            , btn: ['确定', '放弃']
            , btn1: function (index, layero) {
                //按钮【按钮一】的回调
                layer.close(index);

                var data1=new Array();
                var count=0;
                $("#selectedcollegelist option").each(function(){
                    data1[count]=$(this).val();
                    count=count+1;
                });
                var colleges=data1.join(',');

                $.ajax({
                    url: "/nradmingl/appserver/manager.aspx?cs=modify_operator_auth",
                    type: "post",
                    dataType: "text",
                    data: { "pk_batch_no":pk_batch_no,"pk_affair_no":affairno,"pk_staff_no": staffno,"colleges":colleges},
                    success: function (data) {
                        var json_data = JSON.parse(data);
                        if (json_data.code == 'success') {
                            layer.close(index);
                            detail(affairno);
                        } else {
                            alert(json_data.message);
                        }
                    },
                    error: function (data) {
                        alert("错误");
                    }
                });
            }
            , btn2: function (index, layero) {
                //按钮【按钮二】的回调
                layer.close(index);

                //return false 开启该代码可禁止点击该按钮关闭
            }
            , cancel: function () {
                //右上角关闭回调
                //return false 开启该代码可禁止点击该按钮关闭
            }
        });


    });

}

//删除操作员权限
function deletestaff(staffno,pk_batch_no,affairno){
    layui.use(['form'], function () {
        var $ = layui.jquery;
        layer.open({
            title: '提示',
            type: 1,
            //area: ['800px', '600px'],
            content: '确定删除吗？' //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
            , btn: ['确定', '放弃']
            , btn1: function (index, layero) {
                //按钮【按钮一】的回调
                layer.close(index);
               $.ajax({
                    url: "/nradmingl/appserver/manager.aspx?cs=modify_operator_auth",
                    type: "post",
                    dataType: "text",
                    data: { "pk_batch_no":pk_batch_no,"pk_affair_no":affairno,"pk_staff_no": staffno,"colleges":''},
                    success: function (data) {
                        var json_data = JSON.parse(data);
                        if (json_data.code == 'success') {
                            detail(affairno);
                        } else {
                            alert(json_data.message);
                        }
                    },
                    error: function (data) {
                        alert("错误");
                    }
                });
            }
            , btn2: function (index, layero) {
                //按钮【按钮二】的回调
                layer.close(index);

                //return false 开启该代码可禁止点击该按钮关闭
            }
            , cancel: function () {
                //右上角关闭回调
                //return false 开启该代码可禁止点击该按钮关闭
            }
        });


    });

}
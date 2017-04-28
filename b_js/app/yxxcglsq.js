function load(){
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

function detail(value){
    var pk_affair_no=value;
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
                    str=str+item.Name+'</td><td>'+item.Year+'</td><td>'+item.Enabled+'</td><td>'+item.FK_College_NAME_STR+'</td>';
                    str=str+'<td><a href="#" class="layui-btn layui-btn-small">修改</a><a href="#" class="layui-btn layui-btn-small">删除</a></td>';
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

function add(value,valuename){
    $('#addstaff').attr('pk_affair_no',value);
    $('#addstaff').attr('affairname',valuename);

    layui.use(['form'], function () {
        var $ = layui.jquery;
        layer.open({
            title: '选择人员',
            type: 1,
            //area: ['800px', '600px'],
            content: $('#addstaff') //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
            , btn: ['确定', '放弃']
            , btn1: function (index, layero) {
                //按钮【按钮一】的回调
                var yhid=$('#namelist').val();
                layer.close(index);
            }
            , btn2: function (index, layero) {
                //按钮【按钮二】的回调

                //return false 开启该代码可禁止点击该按钮关闭
            }
            , cancel: function () {
                //右上角关闭回调

                //return false 开启该代码可禁止点击该按钮关闭
            }
        });


    });
}

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
                    str=str+'<option value="'+item.yhid+'">'+item.xm+'</option>';
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


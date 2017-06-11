function load() {
    var pk_staff_no=$('#pk_staff_no').val();
    var staff_name=$('#staff_name').val();

    if (pk_staff_no == null || $.trim(pk_staff_no).length == 0 || staff_name == null || $.trim(staff_name).length == 0) {
        alert("无效的参数");
        return;
    }

    $('#batchlist option').remove();

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
                        batchchange();
                    }else{
                        str = '<option value="' + item.PK_Batch_NO + '" >' + item.Batch_Name + '</option>';
                        $('#batchlist').append(str);
                    }
                }
                layui.use(['form'], function () {
                    var $ = layui.jquery;
                    var form = layui.form();

                    //selectd的onchange事件
                    form.on('select', function (obj) {
                        var id=obj.elem.id;
                        if(id!=null && id=='batchlist'){
                            batchchange();
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


function batchchange() {
    var pk_batch_no=$('#batchlist').children('option:selected').val();
    var pk_staff_no=$('#pk_staff_no').val();

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_classmsgbystaff","pk_batch_no":pk_batch_no,"pk_staff_no":pk_staff_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                $('#msglist tbody tr').remove();
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    var str='<tr id=msg'+item.PK_NO+' msg='+item.Content+' title='+item.Title+'>';
                    str=str+'<td>'+item.Title+'</td>';
                    str=str+'<td>'+item.CreateDate+'</td>';
                    str=str+'<td>'+item.Author+'</td>';
                    str=str+'<td>'+item.ClassName_STR+'</td>';
                    str=str+'<td>';
                    str=str+'<a href="#" onclick="msgdetail(\'msg'+item.PK_NO+'\')" class="layui-btn layui-btn-mini" title="查看内容">查看内容</a>';
                    str=str+'</td>';
                    str=str+'</tr>';
                    $('#msglist').append(str);
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

function msgdetail(msgid){
    var content=$('#'+msgid).attr('msg');
    var title=$('#'+msgid).attr('title');
    layui.use(['form'], function () {
        layer.open({
            title: title
            ,content: content
        });
    });

}

function addmsg(){
    var pk_batch_no=$('#batchlist').children('option:selected').val();
    var pk_staff_no=$('#pk_staff_no').val();
    var staff_name=$('#staff_name').val();

    layui.use(['form'], function () {
        var $ = layui.jquery;
        layer.open({
            title: '新建通知',
            type: 1,
            area: ['550px', '380px'],
            content: $('#newmsg') //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
            , btn: ['确定', '放弃']
            , btn1: function (index, layero) {
                //按钮【按钮一】的回调
                var title=$('#new_title').val();
                var content=$('#new_content').val();

                if($.trim(title).length==0 || $.trim(content).length==0){
                    layui.use(['form'], function () {
                        layer.open({
                            title: '提示'
                            ,content: '请填写通知内容'
                        });
                    });
                    return;
                }

                $('#classlist div').remove();
                $.ajax({
                    url: "/nradmingl/appserver/manager.aspx",
                    type: "get",
                    dataType: "text",
                    data: {"cs": 'get_batch_ClassByCounseller', "pk_batch_no": pk_batch_no,"pk_staff_no":pk_staff_no},
                    success: function (data) {
                        var json_data = JSON.parse(data);
                        if (json_data.code == 'success') {
                            var str = '<div>';
                            for (i = 0; json_data.data != null && i < json_data.data.length; i++) {
                                var item = json_data.data[i];
                                str=str+'<input type="checkbox" name="'+item.PK_Class_NO+'" title="'+item.Name+'" ref-data="'+item.PK_Class_NO+'" style="margin-top:6px;">'+item.Name+' <br />';
                            }
                            str=str+'</div>'
                            $('#classlist').append(str);
                        } else {
                            alert(json_data.message);
                            return;
                        }
                    },
                    error: function (data) {
                        alert("错误");
                        return;
                    }
                });

                var parentindex=index;
                layer.open({
                    title: '选择班级',
                    type: 1,
                    //area: ['550px', '380px'],
                    content: $('#classlist_win') //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
                    , btn: ['确定', '放弃']
                    , btn1: function (index, layero) {
                        //按钮【按钮一】的回调
                        var classlist=new Array();
                        var count=0;
                        $("#classlist div input").each(function(){
                            if($(this).is(':checked')){
                                classlist[count]=$(this).attr('ref-data');
                                count=count+1;
                            }
                        });

                        if(classlist.length>0){
                            var classliststr=classlist.join(',');
                            $.ajax({
                                url: "/nradmingl/appserver/manager.aspx?cs=createclassmsg",
                                type: "post",
                                dataType: "text",
                                data: { "title":title,"content":content,"author": pk_staff_no+':'+staff_name,"classliststr":classliststr},
                                success: function (data) {
                                    var json_data = JSON.parse(data);
                                    if (json_data.code == 'success') {
                                        layer.close(index);
                                        layer.close(parentindex);
                                        batchchange();
                                    } else {
                                        alert(json_data.message);
                                    }
                                },
                                error: function (data) {
                                    alert("错误");
                                }
                            });
                        }else{
                            layui.use(['form'], function () {
                                layer.open({
                                    title: '提示'
                                    ,content: '请选择班级'
                                });
                            });
                            return;
                        }
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





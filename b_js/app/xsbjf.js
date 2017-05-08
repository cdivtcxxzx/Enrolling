function fillstr(str){
    if ($.trim(str).length >=8 ) {
        return str;
    }
    var jg=str;
    for (var i=$.trim(str).length;i<=8;i++){
        jg='&nbsp'+jg;
    }
    return jg;
}

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
        if ($.trim(pk_staff_no).length > 0 ) {
            $('#btnback').hide();
            $('#cancel').hide();
        }
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
                    //获取交费信息
                    $.ajax({
                        url: "/nradmingl/appserver/manager.aspx",
                        type: "get",
                        dataType: "text",
                        data: { "cs": "get_fee_no_order","pk_batch_no":pk_batch_no,"pk_sno": pk_sno},
                        success: function (data) {
                            var json_data = JSON.parse(data);
                            if (json_data.code == 'success') {
                                var fee_id_list=new Array();
                                var count=0;

                                var single_must=json_data.data.single_must;//必交单项
                                var multiple_must=json_data.data.multiple_must;//必交多项
                                var single_nomust=json_data.data.single_nomust;//选交单项
                                var multiple_nomust=json_data.data.multiple_nomust;//选交多项
                                var showed=false;

                                $('#contents').append('<table>');
                                for(i=0;single_must!=null && i<single_must.length;i++){
                                    itemlist=single_must[i];
                                    $('#contents').append('<tr><td>');
                                    var str='<div class="layui-inline" style="" id=_sm'+itemlist[0].Fee_Code+' ref-data='+itemlist[0].PK_Fee_Item+'>';
                                    str=str+'<label class="layui-form-label">'+itemlist[0].Fee_Code_Name+'*</label>';
                                    str=str+'<div class="layui-input-inline">';
                                    str=str+'<div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><label>'+fillstr(itemlist[0].Fee_Amount)+'元</label></div>';
                                    str=str+'</div>';
                                    str=str+'</div>';
                                    $('#contents').append(str);
                                    $('#contents').append('</tr></td>');
                                    fee_id_list[count]='_sm'+itemlist[0].Fee_Code;
                                    count=count+1;
                                    $('#sure').show();
                                }

                                for(i=0;multiple_must!=null && i<multiple_must.length;i++){
                                    var itemlist=multiple_must[i];
                                    $('#contents').append('<tr><td>');
                                    var str='<div class="layui-inline" style="" id=_mm'+itemlist[0].Fee_Code+' ref-data="'+itemlist[0].PK_Fee_Item+'">';
                                    str=str+'<label class="layui-form-label" >'+itemlist[0].Fee_Code_Name+'*</label>';
                                    str=str+'<div class="layui-input-inline">';
                                    str=str+'<select id="'+itemlist[0].Fee_Code+'" lay-filter="aihao">';
                                    for(var j=0;j<itemlist.length;j++){
                                        if(j==0){
                                            str=str+'<option value="'+itemlist[j].PK_Fee_Item+'" selected="">'+fillstr(itemlist[j].Fee_Amount)+'元&nbsp&nbsp'+itemlist[j].Fee_Name+'</option>';
                                        }else{
                                            str=str+'<option value="'+itemlist[j].PK_Fee_Item+'" >'+fillstr(itemlist[j].Fee_Amount)+'元&nbsp&nbsp'+itemlist[j].Fee_Name+'</option>';
                                        }
                                    }
                                    str=str+'</select>';;
                                    str=str+'</div>';
                                    str=str+'</div>';
                                    $('#contents').append(str);
                                    $('#contents').append('</tr></td>');
                                    fee_id_list[count]='_mm'+itemlist[0].Fee_Code;
                                    count=count+1;
                                    $('#sure').show();
                                }

                                for(i=0;single_nomust!=null && i<single_nomust.length;i++){
                                    itemlist=single_nomust[i];
                                    $('#contents').append('<tr><td>');
                                    var str='<div class="layui-inline" style="" id=snm'+itemlist[0].Fee_Code+' ref-data="none">';
                                    str=str+'<label class="layui-form-label" >'+itemlist[0].Fee_Code_Name+'：</label>';
                                    str=str+'<div class="layui-input-inline">';
                                    str=str+'<select id="'+itemlist[0].Fee_Code+'" lay-filter="aihao">';
                                    str=str+'<option value="none" >暂不选择</option>';
                                    for(var j=0;j<itemlist.length;j++){
                                        str=str+'<option value="'+itemlist[j].PK_Fee_Item+'" >'+fillstr(itemlist[j].Fee_Amount)+'元&nbsp&nbsp'+itemlist[j].Fee_Name+'</option>';
                                    }
                                    str=str+'</select>';
                                    str=str+'</div>';
                                    str=str+'</div>';
                                    $('#contents').append(str);
                                    $('#contents').append('</tr></td>');
                                    fee_id_list[count]='snm'+itemlist[0].Fee_Code;
                                    count=count+1;
                                    $('#sure').show();
                                }

                                for(i=0;multiple_nomust!=null && i<multiple_nomust.length;i++){
                                    var itemlist=multiple_nomust[i];
                                    $('#contents').append('<tr><td>');
                                    var str='<div class="layui-inline" style="" id=mnm'+itemlist[0].Fee_Code+' ref-data="none">';
                                    str=str+'<label class="layui-form-label" >'+itemlist[0].Fee_Code_Name+'：</label>';
                                    str=str+'<div class="layui-input-inline">';
                                    str=str+'<select id="'+itemlist[0].Fee_Code+'" lay-filter="aihao">';
                                    str=str+'<option value="none" >暂不选择</option>';
                                    for(var j=0;j<itemlist.length;j++){
                                        str=str+'<option value="'+itemlist[j].PK_Fee_Item+'" >'+fillstr(itemlist[j].Fee_Amount)+'元&nbsp&nbsp'+itemlist[j].Fee_Name+'</option>';
                                    }
                                    str=str+'</select>';
                                    str=str+'</div>';
                                    str=str+'</div>';
                                    $('#contents').append(str);
                                    $('#contents').append('</tr></td>');
                                    fee_id_list[count]='mnm'+itemlist[0].Fee_Code;
                                    count=count+1;
                                    $('#sure').show();
                                }
                                $('#contents').append('</table>');

                                $('#contents').attr('fee_id_list',fee_id_list.join(","));

                                layui.use(['form', 'layedit', 'laydate', 'element'], function () {
                                    var $ = layui.jquery;

                                    var element = layui.element(); //Tab的切换功能，切换事件监听等，需要依赖element模块

                                    //触发事件
                                    var active = {
                                        tabAdd: function () {
                                            //新增一个Tab项
                                            element.tabAdd('demo', {
                                                title: '新选项' + (Math.random() * 1000 | 0) //用于演示
                                                ,
                                                content: '内容' + (Math.random() * 1000 | 0)
                                            })
                                        },
                                        tabDelete: function () {
                                            //删除指定Tab项
                                            element.tabDelete('demo', 2); //删除第3项（注意序号是从0开始计算）
                                        },
                                        tabChange: function () {
                                            //切换到指定Tab项
                                            element.tabChange('demo', 1); //切换到第2项（注意序号是从0开始计算）
                                        }
                                    };

                                    $('.site-demo-active').on('click', function () {
                                        var type = $(this).data('type');
                                        active[type] ? active[type].call(this) : '';
                                    });

                                    var form = layui.form(),
                                        layer = layui.layer,
                                        layedit = layui.layedit,
                                        laydate = layui.laydate;

                                    //创建一个编辑器
                                    var editIndex = layedit.build('LAY_demo_editor');
                                    //自定义验证规则
                                    form.verify({
                                        title: function (value) {
                                            if (value.length < 5) {
                                                return '标题至少得5个字符啊';
                                            }
                                        },
                                        pass: [/(.+){6,12}$/, '密码必须6到12位'],
                                        content: function (value) {
                                            layedit.sync(editIndex);
                                        }
                                    });

                                    //监听提交
                                    form.on('submit(demo1)', function (data) {
                                        layer.alert(JSON.stringify(data.field), {
                                            title: '最终的提交信息'
                                        })
                                        return false;
                                    });
                                    //手机设备的简单适配
                                    var treeMobile = $('.site-tree-mobile');
                                    var	shadeMobile = $('.site-mobile-shade');
                                    treeMobile.on('click', function () {
                                        $('body').addClass('site-mobile');
                                    });
                                    shadeMobile.on('click', function () {
                                        $('body').removeClass('site-mobile');
                                    });
                                    //selectd的onchange事件
                                    form.on('select', function (obj) {
                                        $(obj.elem).parent().parent().removeAttr('ref-data');
                                        $(obj.elem).parent().parent().attr('ref-data',obj.value);
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
    var pk_batch_no= $("#pk_batch_no").val();
    var pk_affair_no= $("#pk_affair_no").val();

    if (pk_sno == null || $.trim(pk_sno).length == 0 || pk_batch_no == null || $.trim(pk_batch_no).length == 0
        || pk_affair_no == null || $.trim(pk_affair_no).length == 0) {
        alert("无效的参数");
        return;
    }

    var fee_id_list=null;
    var i=0;
    var feelist=null;

    var data=$('#contents').attr('fee_id_list');
    if(data!=null && $.trim(data).length>0){
        fee_id_list=data.split(',');
    }else{
        alert("无效的参数");
    }
    if(fee_id_list!=null && fee_id_list.length>0){
        feelist=new Array();
        var count=0;
        for(i=0;i<fee_id_list.length;i++){
            var code=fee_id_list[i].substring(3);
            var val=$('#'+fee_id_list[i]).attr('ref-data');
            if(val!='none'){
                feelist[count]={"code":code,"value":val};
                count=count+1;
            }
        }
    }else{
        alert("无效的参数");
    }
    if(feelist!=null && feelist.length>0){
        layer.confirm('将提交您的预交费订单，订单生成后其内容仅允许到校后修改。您确认继续吗？', {
            btn: ['继续', '终止']
        }, function(index){
            layer.close(index);
             console.log(JSON.stringify(feelist));
            //生成订单
            var pk_staff_no= $("#pk_staff_no").val();
            var returnurl=window.location.href;

            var data=null;
            if ($.trim(pk_staff_no).length > 0 ) {
                data={ "feelist": JSON.stringify(feelist),"pk_batch_no":pk_batch_no,"pk_sno": pk_sno,"pk_affair_no":pk_affair_no,'pk_staff_no':pk_staff_no,'returnurl':returnurl};
            }else{
                data={ "feelist": JSON.stringify(feelist),"pk_batch_no":pk_batch_no,"pk_sno": pk_sno,"pk_affair_no":pk_affair_no,'returnurl':returnurl};
            }
            //提交必交费信息
            $.ajax({
                url: "/nradmingl/appserver/manager.aspx?cs=make_fee_order",
                type: "post",
                dataType: "text",
                data: data,
                success: function (data) {
                    console.log(data);
                    var json_data = JSON.parse(data);
                    if (json_data.code == 'success') {
                        if ($.trim(pk_staff_no).length > 0 ) {
                            $('#sure').hide();
                            alert('已帮助学生生成交费订单，请通知学生及时交费');
                        }else{
                            window.location.href='xsbjf_order.aspx?pk_sno='+pk_sno;
                        }
                    } else {
                        alert(json_data.message);
                    }
                },
                error: function (data) {
                    alert("错误");
                }
            });
        }, function(index){
            layer.close(index);
        });
    }
}

function cancel(){
    window.location.href="xszz-index.aspx";
}
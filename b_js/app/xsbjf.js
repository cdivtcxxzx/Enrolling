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
    var pk_staff_no= $("#pk_staff_no").val();
    if ($.trim(pk_staff_no).length > 0 ) {
        $('#btnback').hide();
        $('#cancel').hide();
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
                    //获取必交费信息
                    $.ajax({
                        url: "/nradmingl/appserver/manager.aspx",
                        type: "get",
                        dataType: "text",
                        data: { "cs": "get_ismust_fee_full","pk_batch_no":pk_batch_no,"pk_sno": pk_sno},
                        success: function (data) {
                            var json_data = JSON.parse(data);
                            if (json_data.code == 'success') {
                                var finishpay=true;//交清费用标志
                                var single=json_data.data.single_selection;//单项
                                var multiple=json_data.data.multiple_selection;//多项

                                var feelist=new Array();
                                var count=0;
                                for(i=0;single!=null && i<single.length;i++){
                                    itemlist=single[i];
                                    if(itemlist[0].Fee_Amount>itemlist[0].Fee_Payed){
                                        finishpay=false;
                                    }
                                    var str='<div class="layui-form-item" pane="" style="margin-bottom:0px;">';
                                    str=str+'<label class="layui-form-label">'+itemlist[0].Fee_Code_Name+'：</label>';
                                    str=str+'<div class="layui-input-block">';
                                    str=str+'<div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><label>'+itemlist[0].Fee_Amount+'</label></div>';
                                    str=str+'</div>';
                                    str=str+'</div>';
                                    $('#contents').append(str);
                                    feelist[count]=itemlist[0].Fee_Code+':'+itemlist[0].Fee_Amount+':'+itemlist[0].Fee_Payed;
                                    count=count+1;
                                }
                                $('#contents').attr('ref-data-single',feelist.join(","));
                                feelist=new Array();
                                count=0;

                                for(i=0;multiple!=null && i<multiple.length;i++){
                                    var itemlist=multiple[i];
                                    var str='<div class="layui-form-item">';
                                    str=str+'<label class="layui-form-label" >'+itemlist[0].Fee_Code_Name+'：</label>';
                                    str=str+'<div class="layui-input-block">';
                                    str=str+'<select id="'+itemlist[0].Fee_Code+'" lay-filter="aihao">';
                                    str=str+'<option value=""></option>';
                                    for(var j=0;j<itemlist.length;j++){
                                        if(itemlist[j].Fee_Amount>itemlist[j].Fee_Payed){
                                            finishpay=false;
                                        }
                                        if(j==0){
                                            str=str+'<option value="'+itemlist[j].Fee_Amount+'" selected="">'+itemlist[j].Fee_Amount+'</option>';
                                        }else{
                                            str=str+'<option value="'+itemlist[j].Fee_Amount+'">'+itemlist[j].Fee_Amount+'</option>';
                                        }
                                    }
                                    str=str+'</select>';
                                    str=str+'<div class="layui-unselect layui-form-select layui-form-selected">';
                                    str=str+'<div class="layui-select-title">';
                                    str=str+'<input type="text" placeholder="请选择" value="" readonly="" class="layui-input layui-unselect">';
                                    str=str+'<i class="layui-edge"></i>';
                                    str=str+'</div>';
                                    str=str+'<dl class="layui-anim layui-anim-upbit">';
                                    for(var j=0;j<itemlist.length;j++){
                                        if(j==0){
                                            str=str+'<dd lay-value="'+itemlist[j].Fee_Amount+'" class="layui-this">'+itemlist[j].Fee_Amount+'</dd>';
                                        }else{
                                            str=str+'<dd lay-value="'+itemlist[j].Fee_Amount+'" class="">'+itemlist[j].Fee_Amount+'</dd>';
                                        }
                                    }
                                    str=str+'</dl>';
                                    str=str+'</div>';
                                    str=str+'</div>';
                                    str=str+'</div>';
                                    $('#contents').append(str);
                                    feelist[count]=itemlist[0].Fee_Code+':'+itemlist[0].Fee_Amount+':'+itemlist[0].Fee_Payed;
                                    count=count+1;
                                }

                                $('#contents').attr('ref-data-multiple',feelist.join(","));

                                if(!finishpay && ((multiple!=null && multiple.length>0) || (single!=null && single.length>0))){
                                    $('#sure').show();
                                }

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

    var feelist=new Array();
    var count=0;

    var data=$('#contents').attr('ref-data-single');
    if(data!=null && $.trim(data).length>0){
        var single=data.split(',');
        var i=0;
        if(single!=null && single.length>0){
            for(i=0;i<single.length;i++){
                var con=single[i].split(':');
                feelist[count]=con[0]+':'+con[1];//code:value
                count=count+1;
            }
        }else{
            alert('参数错误');
            return;
        }
    }


    data=$('#contents').attr('ref-data-multiple');
    if(data!=null && $.trim(data).length>0){
        var multiple=data.split(',');
        var i=0;
        if(multiple!=null && multiple.length>0){
            for(i=0;i<multiple.length;i++){
                var con=multiple[i].split(':');
                feelist[count]=con[0]+':'+$('#'+con[0]).val();//code:value
                count=count+1;
            }
        }else{
            alert('参数错误');
            return;
        }
    }

    if(feelist.length>0){
        var pk_staff_no= $("#pk_staff_no").val();
        var returnurl=window.location.href;
        var data=null;
        if ($.trim(pk_staff_no).length > 0 ) {
            data={ "feelist": feelist.join(","),"pk_batch_no":pk_batch_no,"pk_sno": pk_sno,"pk_affair_no":pk_affair_no,'pk_staff_no':pk_staff_no,'returnurl':returnurl};
        }else{
            data={ "feelist": feelist.join(","),"pk_batch_no":pk_batch_no,"pk_sno": pk_sno,"pk_affair_no":pk_affair_no,'returnurl':returnurl};
        }
        //提交必交费信息
        $.ajax({
            url: "/nradmingl/appserver/manager.aspx?cs=set_ismust_fee_full",
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
                        window.location.href=json_data.data;
                    }
                } else {
                    alert(json_data.message);
                }
            },
            error: function (data) {
                alert("错误");
            }
        });
    }else{
        alert('没有可提交的数据');
    }
}

function cancel(){
    window.location.href="xszz-index.aspx";
}
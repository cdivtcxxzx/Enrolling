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
    if (pk_sno == null || $.trim(pk_sno).length == 0 || pk_batch_no == null || $.trim(pk_batch_no).length == 0) {
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
                    //获取交费订单信息
                    $.ajax({
                        url: "/nradmingl/appserver/manager.aspx",
                        type: "get",
                        dataType: "text",
                        data: { "cs": "get_fee_order","pk_sno": pk_sno},
                        success: function (data) {
                            console.log(data);
                            var json_data = JSON.parse(data);
                            if (json_data.code == 'success') {
                                if(json_data.data && json_data.data.length>0){
                                    var returnurl=window.location.href;
                                    var payed=new Array();
                                    var count_payed=0;
                                    var nopayed=new Array();
                                    var count_nopayed=0;

                                    for(var i=0;i<json_data.data.length;i++)
                                    {
                                        var items=json_data.data[i].items;
                                        var equ=true;
                                        for(var j=0;j<items.length;j++)
                                        {
                                            if(items[j].Fee_Amount>items[j].Fee_Payed && items[j].Is_Online_Order=='1'){
                                                equ=false;
                                                break;
                                            }
                                        }
                                        if(equ){
                                            payed[count_payed]=json_data.data[i];
                                            count_payed=count_payed+1;
                                        }else{
                                            nopayed[count_nopayed]=json_data.data[i];
                                            count_nopayed=count_nopayed+1
                                        }
                                    }


                                    for(var i=0;i<nopayed.length;i++)
                                    {
                                        var order_id=nopayed[i].order_id;
                                        var order_url=nopayed[i].order_url;
                                        var items=nopayed[i].items;

                                        var index=0;
                                        var new_returnurl=returnurl.replace('?','@');
                                        new_returnurl=new_returnurl.replace('&','||');
                                        index=returnurl.indexOf('/view/');
                                        returnurl=returnurl.substring(0,index)+'/view/jump.aspx?para='+new_returnurl;
                                        //console.log(returnurl);

                                        index=order_url.indexOf('returnURL');
                                        if(index>0)
                                            order_url=order_url.substring(0,index)+'returnURL='+returnurl;
                                        else
                                            order_url=order_url.substring(0,index)+'&returnURL='+returnurl;

                                        var str='';
                                        for(var j=0;j<items.length;j++){
                                            str=str+'<tr>';
                                            str=str+'<td>'
                                            if(items[j].Fee_Code_Name==items[j].Fee_Name){
                                                str=str+items[j].Fee_Code_Name;
                                            }else{
                                                str=str+items[j].Fee_Code_Name+'('+items[j].Fee_Name+')';
                                            }
                                            str=str+'</td>';
                                            str=str+'<td>'
                                            str=str+items[j].Fee_Amount;
                                            str=str+'</td>';
                                            if(j==0){
                                                str=str+'<td rowspan="'+items.length+'">';
                                                str=str+'<a class="layui-btn layui-btn-mini" href="'+order_url+'">去缴费</a>';
                                                str=str+'</td>';
                                            }
                                            str=str+'</tr>';
                                        }
                                        $('#feetable').append(str);
                                    }

                                    for(var i=0;i<payed.length;i++)
                                    {
                                        var items=payed[i].items;
                                        var str='';
                                        for(var j=0;j<items.length;j++){
                                            str=str+'<tr>';
                                            str=str+'<td>'
                                            if(items[j].Fee_Code_Name==items[j].Fee_Name){
                                                str=str+items[j].Fee_Code_Name;
                                            }else{
                                                str=str+items[j].Fee_Code_Name+'('+items[j].Fee_Name+')';
                                            }
                                            str=str+'</td>';
                                            str=str+'<td>'
                                            str=str+items[j].Fee_Amount;
                                            str=str+'</td>';
                                            if(items[j].Fee_Amount>items[j].Fee_Payed){
                                                str=str+'<td >未交</td>';
                                            }else{
                                                str=str+'<td >已交</td>';
                                            }
                                            str=str+'</tr>';
                                        }
                                        $('#feetable').append(str);
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
                                }else{

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
        layer.confirm('将提交您的预交费订单，订单生成后其内容仅允许到校后修改。', {
            btn: ['继续', '重新选择']
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
        }, function(index){
            layer.close(index);
        });
    }
}

function cancel(){
    window.location.href="xswsjf.aspx";
}
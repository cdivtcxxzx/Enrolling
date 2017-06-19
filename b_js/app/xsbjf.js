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
                                $('#tmpdata').val(data);
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

                                var find_index=-1;
                                for(i=0;multiple_must!=null && i<multiple_must.length;i++){
                                    var itemlist=multiple_must[i];
                                    for(var j=0;j<itemlist.length;j++){
                                        if($.trim(itemlist[j].Fee_Name)=='绿色通道' || $.trim(itemlist[j].Fee_Name)=='助学贷款'){
                                            find_index=i;
                                            break;
                                        }
                                    }
                                    if(find_index!=-1){
                                        $('#contents').append('<tr><td>');
                                        var str='<div class="layui-inline" style="">';
                                        str=str+'<label class="layui-form-label">'+itemlist[0].Fee_Code_Name+'*</label>';
                                        str=str+'<div class="layui-input-inline">';
                                        str=str+'<div class="layui-form-mid layui-word-aux-ts" style="margin-left:10px;"><label>'+fillstr(itemlist[0].Fee_Amount)+'元</label></div>';
                                        str=str+'</div>';
                                        str=str+'</div>';
                                        $('#contents').append(str);
                                        $('#contents').append('</tr></td>');

                                        $('#TuitionClass').attr("ref-data",itemlist[0].Fee_Code);
                                        for(var j=0;j<itemlist.length;j++){
                                            if($.trim(itemlist[j].Fee_Name)=='绿色通道'){
                                                $('#xf_green').val(itemlist[j].PK_Fee_Item);
                                                $('#xf_green').attr('fee',itemlist[j].Fee_Amount);
                                            }else{
                                                if($.trim(itemlist[j].Fee_Name)=='助学贷款'){
                                                    $('#xf_loan').val(itemlist[j].PK_Fee_Item);
                                                    $('#xf_loan').attr('fee',itemlist[j].Fee_Amount);
                                                } else{
                                                    $('#xf_normal').val(itemlist[j].PK_Fee_Item);
                                                    $('#xf_normal').attr('fee',itemlist[j].Fee_Amount);
                                                    $('#xf_normal').attr('checked', 'checked');
                                                }
                                            }
                                        }
                                        break;
                                    }
                                }

                                for(i=0;multiple_must!=null && i<multiple_must.length;i++){
                                    if(i==find_index){
                                        continue;
                                    }
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
                                    var str='<div class="layui-inline" style="" id=snm'+itemlist[0].Fee_Code+' ref-data="'+itemlist[0].PK_Fee_Item+'">';
                                    str=str+'<label class="layui-form-label" >'+itemlist[0].Fee_Code_Name+'：</label>';
                                    str=str+'<div class="layui-input-inline">';
                                    str=str+'<select id="'+itemlist[0].Fee_Code+'" lay-filter="aihao">';
                                    for(var j=0;j<itemlist.length;j++){
                                        str=str+'<option value="'+itemlist[j].PK_Fee_Item+'" >'+fillstr(itemlist[j].Fee_Amount)+'元&nbsp&nbsp'+itemlist[j].Fee_Name+'</option>';
                                    }
                                    str=str+'<option value="none" >不需要</option>';
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
                                    var str='<div class="layui-inline" style="" id=mnm'+itemlist[0].Fee_Code+' ref-data="'+itemlist[0].PK_Fee_Item+'">';
                                    str=str+'<label class="layui-form-label" >'+itemlist[0].Fee_Code_Name+'：</label>';
                                    str=str+'<div class="layui-input-inline">';
                                    str=str+'<select id="'+itemlist[0].Fee_Code+'" lay-filter="aihao">';
                                    for(var j=0;j<itemlist.length;j++){
                                        str=str+'<option value="'+itemlist[j].PK_Fee_Item+'" >'+fillstr(itemlist[j].Fee_Amount)+'元&nbsp&nbsp'+itemlist[j].Fee_Name+'</option>';
                                    }
                                    str=str+'<option value="none" >不需要</option>';
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
    }

    feelist=new Array();
    var count=0;
    var code=null;
    if(fee_id_list!=null && fee_id_list.length>0){
        for(i=0;i<fee_id_list.length;i++){
            code=fee_id_list[i].substring(3);
            var val=$('#'+fee_id_list[i]).attr('ref-data');
            if(val!='none'){
                feelist[count]={"code":code,"value":val};
                count=count+1;
            }
        }
    }
    code=$('#TuitionClass').attr('ref-data');
    if(code!='none'){
        var data=$('#tmpdata').val();
        var json_data = JSON.parse(data);
        var single_must=json_data.data.single_must;//必交单项
        var multiple_must=json_data.data.multiple_must;//必交多项
        var single_nomust=json_data.data.single_nomust;//选交单项
        var multiple_nomust=json_data.data.multiple_nomust;//选交多项
        var sum=0;
        for(var i=0;i<feelist.length;i++){
            var key_code=feelist[i].code;
            var key_value=feelist[i].value;
            var find=false;
            for(var j=0;!find && single_must!=null && j<single_must.length;j++){
                var itemlist=single_must[j];
                if(itemlist[0].Fee_Code==key_code){
                    for(var k=0;k<itemlist.length;k++){
                        if(itemlist[k].PK_Fee_Item==key_value){
                            //var str=itemlist[k].Fee_Name+'_'+itemlist[k].Fee_Amount;
                            //console.log(str);
                            sum=sum+parseFloat(itemlist[k].Fee_Amount);
                            find=true;
                            break;
                        }
                    }
                }
            }
            for(var j=0;!find && multiple_must!=null && j<multiple_must.length;j++){
                var itemlist=multiple_must[j];
                if(itemlist[0].Fee_Code==key_code){
                    for(var k=0;k<itemlist.length;k++){
                        if(itemlist[k].PK_Fee_Item==key_value){
                            //var str=itemlist[k].Fee_Name+'_'+itemlist[k].Fee_Amount;
                            //console.log(str);
                            sum=sum+parseFloat(itemlist[k].Fee_Amount);
                            find=true;
                            break;
                        }
                    }
                }
            }
            for(var j=0;!find && single_nomust!=null && j<single_nomust.length;j++){
                var itemlist=single_nomust[j];
                if(itemlist[0].Fee_Code==key_code){
                    for(var k=0;k<itemlist.length;k++){
                        if(itemlist[k].PK_Fee_Item==key_value){
                            //var str=itemlist[k].Fee_Name+'_'+itemlist[k].Fee_Amount;
                            //console.log(str);
                            sum=sum+parseFloat(itemlist[k].Fee_Amount);
                            find=true;
                            break;
                        }
                    }
                }
            }
            for(var j=0;!find && multiple_nomust!=null && j<multiple_nomust.length;j++){
                var itemlist=multiple_nomust[j];
                if(itemlist[0].Fee_Code==key_code){
                    for(var k=0;k<itemlist.length;k++){
                        if(itemlist[k].PK_Fee_Item==key_value){
                            //var str=itemlist[k].Fee_Name+'_'+itemlist[k].Fee_Amount;
                            //console.log(str);
                            sum=sum+parseFloat(itemlist[k].Fee_Amount);
                            find=true;
                            break;
                        }
                    }
                }
            }
        }

        $('#TuitionClass').attr('feesum',sum);
        changesum();
        layer.open({
            title: '学费缴纳方式',
            type: 1,
            //area: ['800px', '600px'],
            content: $('#TuitionClass') //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
            , btn: ['确定', '放弃']
            , btn1: function (index, layero) {
                //按钮【按钮一】的回调
                var val=$("#TuitionClass input[name='TuitionClass']:checked").val();
                var code=$('#TuitionClass').attr('ref-data');
                feelist[count]={"code":code,"value":val};
                layer.close(index);
                submitfee(feelist,pk_batch_no,pk_sno,pk_affair_no);
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
        submitfee(feelist,pk_batch_no,pk_sno,pk_affair_no);
    }
}

function submitfee(feelist,pk_batch_no,pk_sno,pk_affair_no){
    //console.log(feelist);
    //return;
    if(feelist!=null && feelist.length>0){
        layer.confirm('将提交您选择的缴费项目并生成缴费订单，缴费订单生成后其内容仅允许到校后修改。', {
            btn: ['继续', '重新选择']
        }, function(index){
            layer.close(index);
            //console.log(JSON.stringify(feelist));
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
                    //console.log(data);
                    var json_data = JSON.parse(data);
                    if (json_data.code == 'success') {
                        if ($.trim(pk_staff_no).length > 0 ) {
                            $('#sure').hide();
                            alert('已帮助学生生成缴费订单，请通知学生及时缴费');
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
    window.location.href="xswsjf.aspx";
}

function msg(title_t,msg_t){
    layer.open({
        title: title_t
        ,content: msg_t
    });
}
function changesum(){
    var sum=$('#TuitionClass').attr('feesum');
    var id_name=$("#TuitionClass input[name='TuitionClass']:checked").attr('id');
    if(id_name=='xf_normal'){
        sum=parseFloat(sum)+parseFloat($('#'+id_name).attr('fee'));
        sum = Math.round(sum*100)/100;
    }
    console.log(sum);
    $('#fee_sum').html(sum);
}
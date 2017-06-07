(function () {
    layui.use(['form', 'layer', 'jquery'], function () {
        var $ = layui.jquery;

        //var pk_staff_no= $("#pk_staff_no");
        //if (!pk_staff_no)
        //{
        //    //alert("null or undefined or NaN");
        //}else{
        //    pk_staff_no= $("#pk_staff_no").val();
        //    if ($.trim(pk_staff_no).length > 0 ) {
        //        $('#btnback').hide();
        //    }
        //}

        var server_msg=$("#server_msg").val();
        if ($.trim(server_msg).length > 0 ) {
            alert(server_msg);
            //if(server_msg=='您的信息已经确认,无需再次确认'){
            //    if(pk_staff_no && $.trim(pk_staff_no).length > 0){

            //    }else{
            //        //history.go(-1);
            //        $('#xx_confirm_div').hide();
            //        $('#btn_submit').hide();

            //    }
            //}
            //return;
        }else{
            $('#xx_confirm_div').show();
            $('#btn_submit').show();
        }


        var form = layui.form();
        layer = layui.layer;
        var pk_sno = $("#hidden_pk_sno").val();//初始值由服务器回传网页时生成
        //var pk_batch_no = $("#pk_batch_no").val();
        //var pk_affair_no = $("#pk_affair_no").val();

        //if (pk_sno == null || $.trim(pk_sno).length == 0 || pk_batch_no == null || $.trim(pk_batch_no).length == 0
        //    || pk_affair_no == null || $.trim(pk_affair_no).length == 0) {
        //    alert("无效的参数");
        //    return;
        //}

        if(pk_sno == null || $.trim(pk_sno).length == 0){
            layer.alert("学生学号错误!");
            return;
        }


        //清空学生信息
        $('#xszpxx').attr('src','');
        $('#xsxx_xm').text('');
        $('#xsxx_xb').text('');
        $('#xsxx_sfzh').text('');
        $('#xsxx_lxdh').text('');
        $('#xsxx_test').text('');

        $('#lqxx_xh').text('');
        $('#lqxx_zy').text('');
        $('#lqxx_bj').text('');
        $('#lqxx_bzr').text('');
        

        $('#btn_submit').on('click', function () {
            var confirmState = $('input:radio[name="xx_confirm"]:checked').val();
            $.ajax({
                url: "../../nradmingl/appserver/stu_server.aspx",
                type: "get",
                dataType: "text",
                data: { "type": "xsxx_confirm", "pk_sno": pk_sno, "confirmState": confirmState },
                success: function (data) {
                    var json_data = JSON.parse(data);
                    if (json_data.code == 'success') {
                        if (confirmState == "0") {
                            layer.msg('确认成功！3秒后跳转至主界面~');
                            $('#btn_submit').hide();
                            $('#xx_confirm_div').hide();
                            setTimeout(function () {
                                window.location.href = "../../nradmingl/defaultxs.aspx";
                            }, 3000);
                        } else {
                            layer.msg("确认成功！信息有误，将不能继续报到注册！");
                        }
                    } else if(json_data.code='failure'){
                        layer.msg(json_data.message);
                    }   
                }
            });

            return false;
        });


        //获取学生信息NO:14&15&16 获取学生数据(学号)
        $.ajax({
            url: "../../nradmingl/appserver/stu_server.aspx",
            type: "get",
            dataType: "text",
            data: { "type": "get_student", "pk_sno": pk_sno },
            success: function (data) {
                var json_data = JSON.parse(data);
                if (json_data.code == 'success') {
                    if (json_data.data != null && json_data.data.length > 0) {
                        for (var i = 0; i < json_data.data.length; i++) {
                            if (json_data.data[i].name == 'student') {
                                $('#xszpxx').attr('src', '../' + json_data.data[i].data.Photo);
                                $('#lqxx_xh').text(json_data.data[i].data.PK_SNO);
                                $('#xsxx_xm').text(json_data.data[i].data.Name);
                                $('#xsxx_xb').text(json_data.data[i].data.Gender_Code);
                                $('#xsxx_sfzh').text(json_data.data[i].data.ID_NO);
                                $('#xsxx_lxdh').text(json_data.data[i].data.Phone_dr);
                                $('#xsxx_test').text(json_data.data[i].data.Test_NO);
                            }
                            if (json_data.data[i].name == 'spe') {
                                $('#lqxx_zy').text(json_data.data[i].data.SPE_Name);
                            }
                            if (json_data.data[i].name == 'class') {
                                $('#lqxx_bj').text(json_data.data[i].data.Name);
                            }
                            if (json_data.data[i].name == 'counseller') {
                                $('#lqxx_bzr').text(json_data.data[i].data.name);
                                $('#lqxx_bzrdh').text(json_data.data[i].data.phone);
                            }
                        }//end for
                    }//end if length > 0
                }//end code=success
            }//end success
        });//end ajax

    });//end layui.use
})();


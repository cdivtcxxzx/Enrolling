(function () {
    layui.use(['form', 'layer', 'jquery'], function () {
        var $ = layui.jquery;
        var form = layui.form();
        layer = layui.layer;
        var pk_sno = $("#hidden_pk_sno").val();//初始值由服务器回传网页时生成
        
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
        
        if (pk_sno == null || pk_sno == '') { layer.alert('学号不正确！'); return;}
        //检验学生批次NO:11 校验学生迎新批次(批次编号,学号)
        //$.ajax({
        //    url: "../../nradmingl/appserver/manager.aspx",
        //    type: "get",
        //    dataType: "text",
        //    data: { "cs": "check_student_in_freshbatch"}
        //});
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
                        layer.msg('确认成功！');
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
            datType: "text",
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
                                $('#xsxx_lxdh').text(json_data.data[i].data.Phone);
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
                            }
                        }//end for
                    }//end if length > 0
                }//end code=success
            }//end success
        });//end ajax     

        //后端信息弹出信息
        var msg = $("#hidden_alert_msg").val();
        if (msg.length != 0) {
            layer.alert(msg);
            $("#hidden_alert_msg").val('');
        }




    });//end layui.use
})();


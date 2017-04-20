(function () {
    layui.use(['form', 'layer', 'jquery'], function () {
        var $ = layui.jquery;

        var server_msg=$("#server_msg").val();
        if ($.trim(server_msg).length > 0 ) {
            alert(server_msg);
            return;
        }

        var form = layui.form();
        layer = layui.layer;
        var pk_sno = $("#hidden_pk_sno").val();//初始值由服务器回传网页时生成


        //清空学生信息
        //$('#xszpxx').attr('src','');
        $('#xsxx_xm').text('');
        $('#xsxx_xb').text('');
        $('#xsxx_sfzh').text('');
        $('#xsxx_lxdh').text('');
        $('#xsxx_test').text('');

        $('#lqxx_xh').text('');
        $('#lqxx_zy').text('');
        $('#lqxx_bj').text('');
        $('#lqxx_bzr').text('');
        
        if (pk_sno == null || pk_sno == '') layer.alert('学号不正确！');
        //检验学生批次NO:11 校验学生迎新批次(批次编号,学号)
        //$.ajax({
        //    url: "../../nradmingl/appserver/manager.aspx",
        //    type: "get",
        //    dataType: "text",
        //    data: { "cs": "check_student_in_freshbatch"}
        //});


        //获取学生信息NO:14&15&16 获取学生数据(学号)
        $.ajax({
            url: "../../nradmingl/appserver/manager.aspx",
            type: "get",
            datType: "text",
            data: { "cs": "get_student", "pk_sno": pk_sno },
            success: function (data) {
                var json_data = JSON.parse(data);
                console.log(json_data);
                if (json_data.code == 'success') {
                    if (json_data.data != null && json_data.data.length > 0) {
                        if (json_data.data != null && json_data.data.length > 0) {
                            for (var i = 0; i < json_data.data.length; i++) {
                                if (json_data.data[i].name == 'student') {
                                    $('#lqxx_xh').text(json_data.data[i].data.PK_SNO);
                                    $('#xsxx_xm').text(json_data.data[i].data.Name);
                                    $('#xsxx_xb').text(json_data.data[i].data.Gender_Code);
                                    $('#xsxx_sfzh').text(json_data.data[i].data.ID_NO);
                                }
                                if (json_data.data[i].name == 'spe') {
                                    $('#xs_xl').html(json_data.data[i].data.EDU_Level_Code);
                                    $('#xs_xy').html(json_data.data[i].data.FK_College_Code);
                                    $('#xs_zy').html(json_data.data[i].data.SPE_Name);
                                    $('#xs_nj').html(json_data.data[i].data.Year);
                                    $('#xs_bj').html('');
                                    $('#xs_bzr').html('');
                                }
                                if (json_data.data[i].name == 'class') {
                                    $('#lqxx_bj').text(json_data.data[i].data.Name);
                                }
                                if (json_data.data[i].name == 'counseller') {
                                    $('#xs_bzr').text(json_data.data[i].data.name);
                                    $('#xs_bzrdhhm').html(json_data.data[i].data.phone);
                                }
                            }
                        }//end if length > 0
                    }//end if success
                }//end code=success
            }//end success
        });
    });
})();


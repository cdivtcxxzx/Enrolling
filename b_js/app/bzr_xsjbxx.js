(function () {    
    layui.use(['form', 'layer', 'jquery'], function () {
        var $ = layui.jquery;
        var form = layui.form();
        layer = layui.layer;


        var pk_sno = $("#hidden_pk_sno").val();//初始值由服务器回传网页时生成

        if (pk_sno == null || pk_sno == '') { layer.alert('学号不正确！'); return; }



        $.ajax({
            url: "../../nradmingl/appserver/manager.aspx",
            type: "get",
            datType: "text",
            data: { "cs": "get_student_detail", "pk_sno": pk_sno },
            success: function (data) {
                //console.log(data);
                var json_data = JSON.parse(data);
                if (json_data.code == 'success') {
                    if (json_data.data != null && json_data.data.length > 0) {
                        for (var i = 0; i < json_data.data.length; i++) {
                            $('#xszpxx').attr('src', '../' + json_data.data[i].Photo);
                            $('#xsxx_xh').text(json_data.data[i].pk_sno);
                            $('#xsxx_xm').text(json_data.data[i].name);
                            $('#xsxx_xb').text(json_data.data[i].gender);
                            $('#xsxx_sfzh').text(json_data.data[i].id_no);
                            $('#xsxx_nj').text(json_data.data[i].year);
                            $('#xsxx_zy').text(json_data.data[i].spe_name);
                            $('#xsxx_xlcc').text(json_data.data[i].EDU_Level);
                            $('#xsxx_xy').text(json_data.data[i].collage);
                            $('#xsxx_bjmc').text(json_data.data[i].class_name);
                            $('#xsxx_bmh').text(json_data.data[i].test_no);

                            $('#xsxx_dhhm').text(json_data.data[i].phone==null?'':json_data.data[i].phone);
                            $('#xsxx_qq').text(json_data.data[i].QQ==null?'':json_data.data[i].QQ);
                            $('#xsxx_jtzz').text(json_data.data[i].Home_add==null?'':json_data.data[i].Home_add);
                            $('#xsxx_zzmm').text(json_data.data[i].Politics==null?'':json_data.data[i].Politics);
                            $('#xsxx_mz').text(json_data.data[i].Nation==null?'':json_data.data[i].Nation);
                            $('#xsxx_sg').text(json_data.data[i].Height==null?'':json_data.data[i].Height);
                            $('#xsxx_tz').text(json_data.data[i].Weight==null?'':json_data.data[i].Weight);
                            $('#xsxx_jg').text(json_data.data[i].census==null?'':json_data.data[i].census);
                            $('#xsxx_dorm').text(json_data.data[i].dorm==null?'':json_data.data[i].dorm);

                        }//end for
                    }//end if length > 0
                }//end code=success
                $('#xszpxx').on('error', function () {
                    $('#xszpxx').attr('src', '../images/xstp/test.jpg');
                });
            }//end success
        });//end ajax

        
    });
    

})();
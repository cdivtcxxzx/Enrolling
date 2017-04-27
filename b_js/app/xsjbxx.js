(function () {    
    layui.use(['form', 'layer', 'jquery'], function () {
        var $ = layui.jquery;
        var form = layui.form();
        layer = layui.layer;
        
        
        var pk_sno = $("#hidden_pk_sno").val();//初始值由服务器回传网页时生成

        if (pk_sno == null || pk_sno == '') { layer.alert('学号不正确！'); return; }

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
                                $('#xsxx_xh').text(json_data.data[i].data.PK_SNO);
                                $('#xsxx_xm').text(json_data.data[i].data.Name);
                                $('#xsxx_xb').text(json_data.data[i].data.Gender_Code);
                                $('#xsxx_sfzh').text(json_data.data[i].data.ID_NO);
                                $('#xsxx_nj').text(json_data.data[i].data.Year);
                            }
                            if (json_data.data[i].name == 'spe') {
                                $('#xsxx_zy').text(json_data.data[i].data.SPE_Name);
                                $('#xsxx_xlcc').text(json_data.data[i].data.EDU_Level_Code);
                                $('#xsxx_xy').text(json_data.data[i].data.FK_College_Code);
                            }
                            if (json_data.data[i].name == 'class') {
                                $('#xsxx_bjmc').text(json_data.data[i].data.Name);
                            }
                            if (json_data.data[i].name == 'counseller') {
                                $('#xsxx_bzr').text(json_data.data[i].data.name);
                                $('#xsxx_bzrdh').text(json_data.data[i].data.phone);
                            }
                        }//end for
                    }//end if length > 0
                }//end code=success
            }//end success
        });//end ajax

        
    });
    

})();
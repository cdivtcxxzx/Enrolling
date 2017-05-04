(function () {
    layui.use(['jquery', 'layer', 'form'], function () {
        var $ = layui.jquery;
        layer = layui.layer;

        var pk_staff_no= $("#pk_staff_no");
        if (!pk_staff_no)
        {
            //alert("null or undefined or NaN");
        }else{
            pk_staff_no= $("#pk_staff_no").val();
            if ($.trim(pk_staff_no).length > 0 ) {
                $('#btnback').hide();
            }
        }

        //后端信息弹出信息
        var msg = $("#server_msg").val();
        if (msg!=null && msg.length != 0) {
            layer.open({
                title: '信息',
                type: 1,
                //area: ['800px', '600px'],
                content:msg //这里content是一个DOM，注意：最好该元素要存放在body最外层，否则可能被其它的相对元素所影响
                , btn: ['确定']
                , btn1: function (index, layero) {
                    //按钮【按钮一】的回调
                    layer.close(index);
                    $("#server_msg").val('');
                    history.go(-1);
                    return;
                }
                , cancel: function () {
                    //右上角关闭回调
                    //return false 开启该代码可禁止点击该按钮关闭
                    $("#server_msg").val('');
                    history.go(-1);
                    return;
                }
            });
        }else{
            var pk_sno = $("#hidden_pk_sno").val();//初始值由服务器回传网页时生成
            var pk_batch_no = $("#pk_batch_no").val();
            var pk_affair_no = $("#pk_affair_no").val();

            //if (pk_sno == null || $.trim(pk_sno).length == 0 || pk_batch_no == null || $.trim(pk_batch_no).length == 0
            //    || pk_affair_no == null || $.trim(pk_affair_no).length == 0) {
            //    alert("无效的参数");
            //    return;
            //}

            if (pk_sno == null || $.trim(pk_sno).length == 0) { layer.msg("无效的参数"); return; }

            //-----省市县
            var html = "<option value='-1'>请选择</option>"; $("#input_city").html("").append(html); $("#input_area").html("").append(html);
            $.each(pdata, function (idx, item) {
                if (parseInt(item.level) == 0) {
                    html += "<option value='" + item.names + "' exid='" + item.code + "'>" + item.names + "</option>";
                }
            });
            $("#input_province").html("").append(html);

            $("#input_province").change(function () {
                if ($(this).val() == "") return;
                $("#input_city option").remove(); $("#input_area option").remove();
                var code = $(this).find("option:selected").attr("exid"); code = code.substring(0, 2);
                var html = "<option value='-1'>请选择 </option>"; $("#input_area").append(html);
                $.each(pdata, function (idx, item) {
                    if (parseInt(item.level) == 1 && code == item.code.substring(0, 2)) {
                        html += "<option value='" + item.names + "' exid='" + item.code + "'>" + item.names + "</option>";
                    }
                });
                $("#input_city").append(html);
            });

            $("#input_city").change(function () {
                if ($(this).val() == "") return;
                $("#input_area option").remove();
                var code = $(this).find("option:selected").attr("exid"); code = code.substring(0, 4);
                var html = "<option value='-1'>请选择</option>";
                $.each(pdata, function (idx, item) {
                    if (parseInt(item.level) == 2 && code == item.code.substring(0, 4)) {
                        html += "<option value='" + item.names + "' exid='" + item.code + "'>" + item.names + "</option>";
                    }
                });
                $("#input_area").append(html);
            });
            //-------end 省市县

            //获取学生信息NO:14&15&16 获取学生数据(学号)
            $.ajax({
                url: "../../nradmingl/appserver/stu_server.aspx",
                type: "get",
                dataType: "text",
                data: { "type": "get_student", "pk_sno": pk_sno },
                success: function (data) {
                    var json_data = JSON.parse(data);
                    if (json_data.code == 'success') {
                        if (json_data.data != null && json_data.data.length > 0 && json_data.data[0].name == "student") {
                            var stu = json_data.data[0].data;
                            console.log(stu);
                            //姓名、身份证号、性别 家庭 手机 QQ
                            $('#xsxx_xm').val(stu.Name);
                            $('#xsxx_xb').val(stu.Gender_Code);
                            $('#xsxx_sfz').val(stu.ID_NO);

                            $('#xsxx_addr').val(stu.Home_add);
                            $('#phone').val(stu.Phone);
                            $('#qqnum').val(stu.QQ);
                            //政治面貌 民族 身高 体重
                            $('#xsxx_zzmm').val(stu.Politics_Code);
                            $('#xsxx_mz').val(stu.Nation_Code);
                            $('#xsxx_sg').val(stu.Height);
                            $('#xsxx_tz').val(stu.Weight);
                            //户籍
                            var cencus = '';
                            if(stu.Census!=null && stu.Census.length>0)
                            {
                                cencus=stu.Census.split('#');
                                for (var i = 0; i < cencus.length; i++) {
                                    if (i == 0) {
                                        $('#input_province').val(cencus[0]);
                                        $('#input_province').trigger("change");
                                    };
                                    if (i == 1 && cencus[1]!='-1') {
                                        $('#input_city').val(cencus[1]);
                                        $('#input_city').trigger("change");
                                    };
                                    if (i == 2 && cencus[2]!='-1') {
                                        $('#input_area').val(cencus[2]);
                                    };
                                }
                            }
                        }
                    }//end json_data.code == 'success'


                }//end success
            });//end ajax

            var form = layui.form()
                , layer = layui.layer;

            form.verify({
                address: function (value) {
                    if (value.length > 200) {
                        return '地址信息超过字符限制';
                    }
                }
                , qqnum: [/^\+?[1-9][0-9]{2,14}$/, '请输入正确的QQ号']

            });

            //监听提交
            form.on('submit(demo1)', function (data) {
                //console.log(data.field);

                var cloneObj = function (obj) {
                    var str, newobj = obj.constructor === Array ? [] : {};
                    if (typeof obj !== 'object') {
                        return;
                    } else if (window.JSON) {
                        str = JSON.stringify(obj), //序列化对象
                            newobj = JSON.parse(str); //还原
                    } else {
                        for (var i in obj) {
                            newobj[i] = typeof obj[i] === 'object' ? cloneObj(obj[i]) : obj[i];
                        }
                    }
                    return newobj;
                };
                var newdata = cloneObj(data.field);
                delete newdata.__VIEWSTATE;
                delete newdata.__EVENTVALIDATION;
                //console.log(newdata);

                $.ajax({
                    url: "../../nradmingl/appserver/stu_server.aspx?type=xsxx_update",
                    type: "post",
                    dataType: "text",
                    data: newdata,
                    success: function (data) {
                        var json_data = JSON.parse(data);
                        if (json_data.code == 'success') {
                            //layer.msg('修改成功！');
                            layer.alert("你的信息已修改！",{ title: '成功~', icon: 1 });
                        } else if (json_data.code = 'failure') {
                            layer.alert(json_data.message, { title: '错误！', icon: 1 });
                        }
                    }

                });

                //layer.alert(JSON.stringify(data.field), {
                //    title: '最终的提交信息'
                //})
                return false;
            });

        }
    });//end layui.use
})();


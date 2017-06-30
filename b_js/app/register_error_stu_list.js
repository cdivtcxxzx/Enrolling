function load() {
    //迎新批次数据
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: {"cs": "get_freshbatch_welcome_list"},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                for (i = 0; json_data.data != null && i < json_data.data.length; i++) {
                    var item = json_data.data[i];
                    var str = '';
                    if (i == 0) {
                        str = str + '<option value="' + item.PK_Batch_NO + '" selected="">' + item.Batch_Name + '</option>';
                    } else {
                        str = str + '<option value="' + item.PK_Batch_NO + '" >' + item.Batch_Name + '</option>';
                    }
                    $('#batchlist').append(str);
                }

                $('#batchlist').change(function () {
                    getstudent();
                });
                getstudent();
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });
}

function getstudent() {
    var pk_batch_no=$('#batchlist').children('option:selected').val();
    console.log(pk_batch_no);

    $('#count').html('总计：0人');
    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: { "cs": "get_infoerror_stu","pk_batch_no":pk_batch_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                $('#studentlist tbody tr').remove();
                for(i=0;json_data.data!=null && i<json_data.data.length;i++){
                    var item=json_data.data[i];
                    var str='<tr>';
                    str=str+'<td>'+(i+1)+'</td>';
                    str=str+'<td>'+item.year+'</td>';
                    str=str+'<td>'+item.Collage+'</td>';
                    str=str+'<td>'+item.SPE_Name+'</td>';
                    str=str+'<td>'+item.classname+'</td>';
                    str=str+'<td>'+item.Name+'</td>';
                    str=str+'<td>'+item.Gender+'</td>';
                    str=str+'<td>'+item.Test_NO+'</td>';
                    str=str+'<td>'+item.ID_NO+'</td>';
                    if($.trim(item.Phone_dr)===','){
                        str=str+'<td></td>';
                    }else{
                        str=str+'<td>'+item.Phone_dr+'</td>';
                    }
                    str=str+'<td>'+item.counseller_name+'</td>';
                    str=str+'<td>'+item.counseller_phone+'</td>';
                    str=str+'<td>'+item.counseller_qq+'</td>';
                    str=str+'<td>';
                    str=str+'<a href="#" onclick="studentdetail('+item.PK_SNO+')" class="layui-btn layui-btn-mini" title="学生信息">学生详情</a>';
                    str=str+'</td>';
                    str=str+'</tr>';
                    $('#studentlist').append(str);
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


function studentdetail(pk_sno){
    parent.layer.open({  type: 2,  title: '详细信息',  shadeClose: true,  shade: 0.8,  area: ['98%', '98%'],  content: '/view/bzr_xsjbxx.aspx?pk_sno='+pk_sno,btn:'关闭'})

}




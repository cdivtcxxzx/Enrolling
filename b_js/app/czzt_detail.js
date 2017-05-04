function load() {
    var cs=$('#cs').val();
    var pk_batch_no=$('#pk_batch_no').val();
    var pk_collage_no=$('#pk_collage_no').val();
    if (pk_collage_no === 'all') {
        pk_collage_no = '';
    }
    $('#detaildata tr').remove();

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx",
        type: "get",
        dataType: "text",
        data: {"cs": cs, "pk_batch_no": pk_batch_no,"pk_collage_no":pk_collage_no},
        success: function (data) {
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                if(json_data.data != null && json_data.data.length>0){
                    var item = json_data.data[0];
                    var str='';
                    for (var key in item)
                    {
                        str=str+'<td>'+key+'</td>';
                        //console.log('key='+key+' data='+item[key]);
                    }
                    str='<tr>'+str+'</tr>';
                    $('#detaildata').append(str);
                }
                for (i = 0; json_data.data != null && i < json_data.data.length; i++) {
                    var item = json_data.data[i];
                    var str='';
                    for (var key in item)
                    {
                        str=str+'<td>'+item[key]+'</td>';
                        //console.log('key='+key+' data='+item[key]);
                    }
                    str='<tr>'+str+'</tr>';
                    $('#detaildata').append(str);
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





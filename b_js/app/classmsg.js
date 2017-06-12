layui.use(['form', 'layer', 'jquery'], function () {
    var $ = layui.jquery;

    var server_msg = $("#server_msg").val();
    if ($.trim(server_msg).length > 0) {
        alert(server_msg);
    } 
    layer = layui.layer;
    $('a.btn_show_msg').on('click', function () {
        var btn_show_msg = $(this);
        var msg_pk = btn_show_msg.attr('msg')!='undefine' ? btn_show_msg.attr('msg') : '';
        layer.open({
            type: 2
            , title: false
            , closeBtn: false
            , btn: ['关闭']
            , shade: 0.7
            , id: 'msg' + msg_pk
            , content: 'classmsg_detail.aspx?msg='+msg_pk
            , end: function () {
                window.location.reload();              
            }
        });

    });
});//end layui.use


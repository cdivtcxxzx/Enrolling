(function () {
    layui.use(['form', 'layer', 'jquery'], function () {
        var $ = layui.jquery;
        var form = layui.form();
        layer = layui.layer;
        var pk_sno = $("#hidden_pk_sno").val();//初始值由服务器回传网页时生成

        


            

        //后端信息弹出信息
        var msg = $("#hidden_alert_msg").val();
        if (msg.length != 0) {
            layer.alert(msg);
            $("#hidden_alert_msg").val('');
        }




    });//end layui.use
})();


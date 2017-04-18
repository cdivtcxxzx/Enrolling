(function () {    
    layui.use(['jquery', 'layer'], function () {
        var $ = layui.jquery;
        layer = layui.layer;
        
        
        var pk_sno = $("#hidden_pk_sno").val();//初始值由服务器回传网页时生成

        if (pk_sno == null || $.trim(pk_sno).length == 0) {
            testAlert();
            return;
        }



        function msgAlert(msg) {

            layer.alert("参数2错误！");
        }
    });
    

})();
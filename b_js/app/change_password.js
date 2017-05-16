
function pwd() {
    var oldpwd=$('#oldpwd').val();
    if(oldpwd==null || $.trim(oldpwd).length==0){
        alert('请输入旧密码');
        return;
    }
    var pwd1=$('#pwd1').val();
    if(pwd1==null || $.trim(pwd1).length==0){
        alert('请输入新密码');
        return;
    }
    var pwd2=$('#pwd2').val();
    if(pwd2==null || $.trim(pwd2).length==0){
        alert('请重复输入新密码');
        return;
    }
    if(pwd1!=pwd2){
        alert('新密码和重复输入密码不一致');
        return;
    }

    $.ajax({
        url: "/nradmingl/appserver/manager.aspx?cs=modifystupwd",
        type: "post",
        dataType: "text",
        data: { "old_pwd": oldpwd,"new_pwd":pwd1},
        success: function (data) {
            console.log(data);
            var json_data = JSON.parse(data);
            if (json_data.code == 'success') {
                alert(json_data.data);
            } else {
                alert(json_data.message);
            }
        },
        error: function (data) {
            alert("错误");
        }
    });

}







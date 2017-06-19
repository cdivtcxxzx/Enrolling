
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

    if(pwd1.length<8){
        alert('密码长度必须大于8位');
        return;
    }

//定义验证规则，由于字符串数字和字母的顺序可能不同。
//也有可能字母和数字中间还包含了其他字符。故将验证规则分开定义。
    var regNumber = /\d+/; //验证0-9的任意数字最少出现1次。
    var regString = /[a-zA-Z]+/; //验证大小写26个字母任意字母最少出现1次。
    var regSpecial = /[(\ )(\~)(\!)(\@)(\#)(\$)(\%)(\^)(\&)(\*)(\()(\))(\-)(\_)(\+)(\=)(\[)(\])(\{)(\})(\|)(\\)(\;)(\:)(\')(\")(\,)(\.)(\/)(\<)(\>)(\?)(\)]+/;//特殊字符
//验证第一个字符串
    if ((regNumber.test(pwd1) && regString.test(pwd1)) || (regNumber.test(pwd1) && regSpecial.test(pwd1)) || (regString.test(pwd1) && regSpecial.test(pwd1))) {
    }else{
        alert('密码必须包含字母、数字、特殊字符中的两种');
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







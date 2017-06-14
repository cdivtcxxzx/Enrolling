layui.use(['layer', 'jquery'], function () {
    var $ = layui.jquery;
    var layer = layui.layer;
    var checkBox = $('#checkCofirm');
    var btnCofirm = $('#btnConfirm');
    var count = 14;
    
    var server_msg = $("#server_msg").val();
    if (server_msg.length > 0) {
        layer.alert(server_msg);
        return;
    }
    //控制时间显示
    var changeCheck = setInterval(function () {
        //console.log(count);
        btnCofirm.val(count + ' 秒');
        if (count < 1)
        {
            clearInterval(changeCheck);
            btnCofirm.val('确 定').attr('class', 'btnConfirm');
            btnCofirm.removeAttr('disabled');
        }
        count--;
    }, 1000);

    //gototop
    var offsetY = $(window).height() - $('#go_top_bottom').height() + $(document).scrollTop();
    var offsetX = $('.w960').width() + $('.w960').offset().left + $(document).scrollLeft();
    $('#go_top_bottom').css({ 'left': offsetX, 'top': offsetY });
    $(window).scroll(function () { scroll_position('go_top_bottom'); console.log("window.scroll"); });
    $(window).resize(function () { scroll_position('go_top_bottom'); console.log("window.resize"); });
    $(window).load(function () { scroll_position('go_top_bottom'); console.log("window.load"); });
    $('#go_top_bottom #go_top').click(function () {
        scroll_to_pos('top');
        return false;
    });

    $('#go_top_bottom #go_bottom').click(function () {
        scroll_to_pos('bottom');
        return false;
    });

    if (!isScroll()) {
        $('#go_top_bottom').hide();
    }
    else {
        $('#go_top_bottom').show();
    }

    //common
    function menu_drop(menuId, display) {
        $id(menuId).style.display = display;
    }

    function $id(id) {
        return document.getElementById(id);
    }

    function isScroll() {
        return $(window).height() == $(document).height() ? false : true;
    }

    function scroll_position(id) {
        var o = $('#' + id);
        var offsetY = $(window).height() - o.height() + $(document).scrollTop();
        o.stop(true, false).animate({ top: offsetY }, { duration: 700, queue: false });
        if ($(document).scrollTop() == 0) {
            $('#go_top').hide();
            $('#go_bottom').show();
        }
        else if ($(document).scrollTop() == $('body').height() - $(window).height()) {
            $('#go_top').show();
            $('#go_bottom').hide();
        }
        else {
            o.find('a').show();
        }
        if (!isScroll()) {
            $('#go_top_bottom').hide();
        }
        else {
            $('#go_top_bottom').show();
        }
    }
    function scroll_to_pos(pos) {
        var scrollY = 0;
        if (pos == 'top') {
            scrollY = 0;
        }
        else if (pos == 'bottom') {
            scrollY = $('body').height() - $(window).height();
        }
        $('html,body').animate({ scrollTop: scrollY }, 800);
    }


});
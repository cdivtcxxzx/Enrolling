function is_weixin() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == "micromessenger") {
        return true;
    } else {
        return false;
    }
}
var isWeixin = is_weixin();
var winHeight = typeof window.innerHeight != 'undefined' ? window.innerHeight : document.documentElement.clientHeight;
var weixinTip = $('<div id="weixinTip"><p><img id="imgWeixin" src="../images/common/live_weixin.png" alt="微信打开"/></p></div>');
if (isWeixin) {
    $("body").append(weixinTip);
}
$("#weixinTip").css({
    "position": "fixed",
    "left": "0",
    "top": "0",
    "height": winHeight,
    "width": "100%",
    "z-index": "1000",
    "background-color": "rgba(0,0,0,0.8)",
    "filter": "alpha(opacity=80)",
});
$("#weixinTip p").css({
    "text-align": "center",
    "margin-top": "10%",
    "padding-left": "5%",
    "padding-right": "5%"
});
$("#imgWeixin").css({
    "max-width": "100%",
    "height": "auto"
});
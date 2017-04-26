function load(){
    var url=window.location.href;
    var gourl='';
    var index=url.indexOf('?para');
    if(index>0){
        gourl=url.substring(index+6);
        gourl=gourl.replace('@','?');
        gourl=gourl.replace('||','&');
        //console.log(gourl);
        window.location.href=gourl;
    }

}

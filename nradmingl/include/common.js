document.write("<script language='javascript' src='/admin/sqtime/jquery-ui.js'></script>");
document.write("<link rel='stylesheet' href='/admin/sqtime/jquery-ui.css' />");
<!--
//'-=====屏蔽js错误
function killErrors() {
return true;
}
window.onerror = killErrors;
// -->
//jquery 获取url参数插件

eval(function(p,a,c,k,e,d){e=function(c){return c.toString(36)};if(!''.replace(/^/,String)){while(c--){d[c.toString(a)]=k[c]||c.toString(a)}k=[function(e){return d[e]}];e=function(){return'\\w+'};c=1};while(c--){if(k[c]){p=p.replace(new RegExp('\\b'+e(c)+'\\b','g'),k[c])}}return p}('c.f.i=d(o){2 6=j.h.g();2 5;3(6&&6.9("?")){2 b=6.a("?");2 4=b[1];3(4&&4.9("&")){2 e=4.a("&");c.p(e,d(l,7){3(7&&7.9("=")){2 8=7.a("=");3(o){3(m(o)=="n"&&o==8[0]){5=8[1]==r?\'\':8[1]}}q{5=4}}})}}k 5}',28,28,'||var|if|parms|tmp|url|val|parmarr|indexOf|split|arr|jQuery|function|parmList|fn|toString|location|urlget|window|return|key|typeof|string||each|else|null'.split('|'),0,{}))

//jquery 点击单选框影藏其他表单
//--------------------------
//var ispic = ["#manage_Pic"]
//$("#CB_IsPic").myCheck({optName:ispic})
//--------------------------
;(function($){
	$.fn.myCheck = function(options){
        var defaults = {
            optName : []
        }
        options = $.extend(defaults,options);
        
        var name = options.optName;
        for(var i in name)
            $(name[i]).hide();   

        this.click(function()
		{  
            if($(this).is(":checked")){                
                for(var i in name)
                    $(name[i]).show();              
            }else{
                for(var i in name)
                    $(name[i]).hide();   
            }
        })
		if($(this).is(":checked")){                
			for(var i in name)
				$(name[i]).show();               
		}
    }
})(jQuery);

//jquery 检查是否存在数据
//--------------------------
//$("#TB_Url").myIsData({table:"c_word",field:"url",info:"Url地址已经重复，请输入其他地址！"});
//--------------------------
(function($){
	$.fn.myIsData = function(options){
        var defaults = {
            table : "", field : "", info : ""
        }
        options = $.extend(defaults,options);

		var dom = this;
		dom.after("<span class='error' style='display:none;'></span><span class='isdata' style='display:none;'></span>");
		$("#ButtonManage").click(function()
		{
			if(dom.next().next(".isdata").html() == "true")
			{
				dom.next(".error").show();
				dom.next(".error").html(options.info);
				$("#ButtonManage1").val($(this).val());
				$("#ButtonManage1").get(0).disabled = false;
				return false;
			}
		})

		var action = $().urlget("action");
		var val = ""
		if(action == "update")
			val = dom.val();

		dom.blur(function()
		{
			if(val == dom.val())
				return;

			$.ajax
			({
				url:"dataexists.aspx?random=" + Math.random(),
				data:"table="+ options.table +"&field="+ options.field +"&keyword=" + dom.val(),
				dataType:"json",
				beforeSend:function(XMLHttpRequest)
				{
					dom.next(".error").html("<img src='images/loading.gif' />");
				},
				success: function(msg) 
				{
					dom.next().next(".isdata").html(msg[0].isdata);
					if(msg[0].isdata == "true")
					{
						dom.next(".error").show();
						dom.next(".error").html(options.info);
					}
					else
						dom.next(".error").hide();
				}
			});
			return false;
		})
    }
})(jQuery);

//jquery 必须填写的项
//--------------------------
//$("#DDL_System").myValidator();
//$("#TB_Num").myValidator({type:"int"});
//$("#TB_OrderNum").myValidator({ismust:false,type:"int"});
//--------------------------
(function($){
	$.fn.myValidator = function(options){
        var defaults = {
			ismust : true,
			type : ""
        }
        options = $.extend(defaults,options);

		var dom = this;
		var val = "";

		if(dom.length != 0)
		{
			if(dom.attr("id").indexOf("DDL_") != -1)
                val = "0";

			dom.after("<span class='error' style='display:none;'></span>");
			var ismust = false;
			$("#ButtonManage").click(function()
			{
				if(options.ismust && dom.val() == val)
					ismust = true;
				else
					ismust = false;

				if(options.type != "")
				{
					var Regx;
					if(options.type == "letter")
						Regx = /^[A-Za-z]*$/;
					else if(options.type == "email")
						Regx = /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/;
					else if(options.type == "int")
						Regx = /^[0-9]*$/;

					if(!options.ismust && dom.val() == val)
					{
						ismust = false;
					}
					else
					{
						if(!Regx.test(dom.val())) 
							ismust = true;
					}
				}

				if(ismust)
				{
					dom.next(".error").show();
					dom.next(".error").html(dom.parent().find(".note").html());
					$("#ButtonManage1").val($(this).val());
					$("#ButtonManage1").get(0).disabled = false;
					return false;
				}
				else
					dom.next(".error").hide();
			})


			dom.blur(function()
			{
				if(options.ismust && dom.val() == val)
					ismust = true;
				else
					ismust = false;

				if(options.type != "")
				{
					var Regx;
					if(options.type == "letter")
						Regx = /^[A-Za-z]*$/;
					else if(options.type == "email")
						Regx = /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/;
					else if(options.type == "int")
						Regx = /^[0-9]*$/;

					if(!options.ismust && dom.val() == val)
					{
						ismust = false;
					}
					else
					{
						if(!Regx.test(dom.val())) 
							ismust = true;
					}
				}

				if(ismust)
				{
					dom.next(".error").show();
					dom.next(".error").html(dom.parent().find(".note").html());
					return false;
				}
				else
					dom.next(".error").hide();
			})
		}
    }
})(jQuery);

// Tab
//--------------------------
//$(".tabtitle").myTab(".tabcontent > div");
//--------------------------
(function($){
	$.fn.myTab = function(tagcontent)
	{        
		var dom = this;
		var content = $(tagcontent);
        if(dom.length != 0)
		{
			var tab = $().urlget("tab");
			if(tab == undefined)
			{
				content.hide();
				content.eq(0).show();
				dom.children().eq(0).addClass("click");
			}
			else
			{
				content.hide();
				content.eq(tab-1).show();
				dom.children().eq(tab-1).addClass("click");
			}
			

			dom.children().click(function()
			{
				dom.children().removeClass("click");
				$(this).addClass("click");
				
				content.hide();
				content.eq(dom.children().index($(this))).show();
			})
		}
    }
})(jQuery);

// Color
//--------------------------
//$(".color a").myColor({colorback:function(d,v){
//		d.find("span").css("background-color",v);
//	}});
//--------------------------
(function($){
	$.fn.myColor = function(options)
	{
        var defaults = {
            colorcallback : "ColorCallBack",
			colordefault : "#000000"
        }
        var config = $.extend(defaults,options);

		$("body").append('<div id="colorpicker" style="width:251px; height:264px; position:absolute; top:0; left:0; z-index:999; display:none;"></div>');
		
		var showColorPicker = function(clickWhere)
		{
			//clickWhere 回调ID
			var colorFlash = "<embed width=\"251\" height=\"264\" type=\"application/x-shockwave-flash\" src=\"../files/common/colorpicker.swf\" id=\"color_picker\" allowFullscreen=\"true\" allowScriptAccess=\"always\" quality=\"high\" flashvars=\"{vars}\"/>";
			//callback 为选择颜色后触发的函数
			//color表示抓取器开始的颜色
			//who 表示传给callback的参数,这里我传的是点击的元素(一般可以是DOM元素的ID或其他可以被选择的属性)
			$("#colorpicker").html(colorFlash.replace("{vars}", "callback="+ config.colorcallback +"&color=" + clickWhere.find("span").css("background-color") + "&who=" + clickWhere.attr("class")));
		};

		this.click(function()
		{
			$("#colorpicker").css({left:$(this).offset().left + "px", top:$(this).offset().top + $(this).height() + "px"}).show();
			showColorPicker($(this));
			return false;
		})

		$("body").click(function()
		{
			$("#colorpicker").hide();
		})

		$(window.frames["iframe"].document).click(function()
		{
			$("#colorpicker").hide();
		})
	}
})(jQuery);


// 时间对象的格式化;
//--------------------------
//new Date().format("yyyy-MM-dd hh:mm:ss")
//--------------------------
Date.prototype.format = function(format) {
    /*
     * eg:format="YYYY-MM-dd hh:mm:ss";
     */
    var o = {
        "M+" :this.getMonth() + 1, // month
        "d+" :this.getDate(), // day
        "h+" :this.getHours(), // hour
        "m+" :this.getMinutes(), // minute
        "s+" :this.getSeconds(), // second
        "q+" :Math.floor((this.getMonth() + 3) / 3), // quarter
        "S" :this.getMilliseconds()
    // millisecond
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "")
                .substr(4 - RegExp.$1.length));
    }

    for ( var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k]
                    : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}

$(function () {
    //获取url参数动作
    var id = $().urlget("id");
    var action = $().urlget("action");

    $(".ispring").click(function () { alert("xxxxxxx") });

    //删除一级导航左边的线
    $("#pagemain .menu a").eq(0).attr("class", "noline");

    //默认打开后台的第一个页面
    $("#pagemain #left").attr("src", $("#pagemain .menu a").eq(0).attr("title"));

    //导航地址的双转向
    $(".menu a").click(function () {
        $(this).attr("target", "main");
        $("#left").attr("src", $(this).attr("title"));
    })

    //隐藏和显示左边的菜单
    $("#toolhide").toggle(
		function () {
		    $(this).find("img").attr("src", "images/ico_open.gif");
		    $(".side").hide();
		    $(".tool").attr("class", "tool tool1");
		    $(".main").attr("class", "main main1");
		},
		function () {
		    $(this).find("img").attr("src", "images/ico_close.gif");
		    $(".side").show();
		    $(".tool").attr("class", "tool");
		    $(".main").attr("class", "main");
		}
	)

    //鼠标滑过表格的效果
//    $(".table tr").mouseover(function () {


//            if ($(this).hasClass("noeffect"))
//                return false;
//            $(this).addClass("hover");
//            return false;

//    })

		//滑过时,图片显示大图,文字TITLE显示内容

      
//		$(function () {
//		    $(document).tooltip({
//		        items: "img, [titop]", content: function () {
//		            var element = $(this);
//		            if (element.is("[titop]")) {
//		                var text = element.attr("alt");
//		                return "<img class='map'  src='"+text+"'>";
//		            }
//		            if (element.is("[txttop]")) {
//		                return element.attr("title");
//		            }
//		           
//		        }
//		    });
//		});
//    //鼠标离开表格的效果
    $(".table tr").mouseout(function () {
        if ($(this).hasClass("noeffect"))
            return false;
        $(this).removeClass("hover");
        return false;
    })

    //鼠标点击表格的效果
    $(".table tr").mouseup(function () {
        if ($(this).hasClass("noeffect"))
            return false;
        $(".table tr").removeClass("click");
        $(this).addClass("click");
        return false;
    })

//    //表格各行换色的效果奇数行换色，even为偶数行
    $(".table tr:odd").addClass("even");

    //显示文本框里的图片
    $(".showpic").click(function () {
        var width = 500;
        var height = 400;
        var s = $(this).prev().prev("input").val();
        if (s == "")
            s = "images/logo.gif";
        else
            s = "getImg.html?../" + s;
        window.open(s, "newWin", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,top=" + (window.screen.height - height) / 2 + ",left=" + (window.screen.width - width) / 2 + ",width=" + width + ",height=" + height);
        return false;
    })

    //显示文本框里的图片
    $(".showpicall").click(function () {
        var width = 500;
        var height = 400;
        var s = $(this).prev().prev("input").val();
        if (s == "")
            s = "images/logo.gif";
        else
            s = "getImg.html?" + s;
        window.open(s, "newWin", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,top=" + (window.screen.height - height) / 2 + ",left=" + (window.screen.width - width) / 2 + ",width=" + width + ",height=" + height);
        return false;
    })

    //添加按钮
    $("#ButtonManage,#ButtonConfig,#ButtonImport").hide();
    $("#ButtonManage,#ButtonConfig,#ButtonImport").after("<input type='button' id='ButtonManage1' value='" + $("#ButtonManage,#ButtonConfig,#ButtonImport").val() + "' class='button'>");

    //点击按钮后为不可用
    $("#ButtonManage1").click(function () {
        $(this).get(0).disabled = true;
        $(this).val("Loading...");
        $("#ButtonManage,#ButtonConfig,#ButtonImport").get(0).click();
    })

    //获取分辨率
    $("#resolution").text(window.screen.width + " x " + window.screen.height);

    //搜索数据的转向
    $(".search").click(function () {
        var issearch = "true";
        if ($("#searchtext").attr("name") == "0") {
            issearch = "false";
        }
        location.href = '?search=' + issearch + '&field=' + $("#searchtext").attr("title") + '&keyword=' + $("#searchtext").val();
    })

    //获取当前时间
    $(".datetime").click(function () {
        $(this).prev().val(new Date().format("yyyy-MM-dd hh:mm:ss"));
        return false;
    })

    //加载时间
    setInterval("$('span[class=time]').html(new Date().format('yyyy-MM-dd hh:mm:ss'))", 500);

    //删除数据的提示
    $("a[class=delete]").click(function () {
        return confirm($(this).attr("title"));
    })

    //多选删除数据的提示
    $("a[class=deleteall]").click(function () {
        var a = "";
        if ($(".checkdelete:checked").length == 0) {
            return false;
        }
        else {
            if (confirm($(this).attr("title"))) {
                $(".checkdelete:checked").each(function () {
                    a += $(this).val() + ",";
                });
                location.href = "?action=deleteall&id=" + a.substring(0, a.lastIndexOf(","));
            }
            return false;
        }
    })

    //赋予全选
    $("input[class=checkall]").click(function () {
        $(".checkdelete").attr("checked", $("input[class=checkall]").prop("checked"));
    });

    //恢复数据的提示
    $("a[name=undo]").click(function () {
        return confirm($(this).attr("title"));
    })

    //隐藏字段说明
    $(".table td .note").hide();

    //帮助字段提示
    $("a[class=help]").click(function () {
        $(this).parent().next().find(".note").toggle();
        return false;
    })

    //帮助系统提示,全部显示帮助
    $("a[class=helpall]").click(function () {
        $(".table .note").toggle();
        return false;
    })

    //上传按钮的打开小窗口
    $("a[class=upload]").click(function () {
        var width = 320;
        var height = 100;
        if ($(this).attr("href").indexOf("action=original") > 0)
            height = 400;
        window.open($(this).attr("href"), "newWin", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,top=" + (window.screen.height - height) / 2 + ",left=" + (window.screen.width - width) / 2 + ",width=" + width + ",height=" + height);
        return false;
    })

    $(".open").live("click", function () {
        var width = $(this).attr("width");
        var height = $(this).attr("height"); ;
        window.open($(this).attr("href"), "newWin", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,top=" + (window.screen.height - height) / 2 + ",left=" + (window.screen.width - width) / 2 + ",width=" + width + ",height=" + height);
        return false;
    })

    $(".html").click(function () {
        window.open($(this).attr("href"), "newWin", "toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=no,top=100,left=100,width=680,height=520");
        return false;
    })

    //下载文本框里的文件
    $(".showfile").click(function () {
        $(this).attr("href", "../" + $(this).prev().prev("input").val()).attr("target", "_blank");
    })

    //赋予添加数据默认选中
    if (action == "insert" || action == "import" || action == "export" || action == "manage") {
        $(".subsubmenu a[href='?action=" + action + "']").attr("class", "click");
    }
    else {
        $(".subsubmenu a[href='?action=manage']").attr("class", "click");
    }

    //传入值的tag变化
    if ($("#config").length != 0 || $("#database").length != 0 || $("#templates").length != 0) {

        $(".tabtitle").myTab(".tabcontent > .table");

        /*
        var tag = $().urlget("tag");
        if(tag != undefined)
        {
        $(".tag").removeClass("click");
        $(".tag").eq(eval(tag) - 1).addClass("click");
        $(".table").hide();
        $(".table").eq(0).show();
        $(".table").eq(eval(tag)).show();
        }
        else
        {
        $(".tag").eq(0).attr("class","tag click");
        $(".table").hide();
        $(".table").eq(0).show();
        $(".table").eq(1).show();
        }

        //Tag
        $(".tag").click(function()
        {
        $(".tag").removeClass("click");
        $(this).addClass("click");
        $(".table").hide();
        $(".table").eq(0).show();
        $(".table").eq($(".tag").index($(this))+1).show();
        return false;
        })
        */
    }

    //导出搜索数据赋值
    $("#ButtonSearch").click(function () {
        var f = "";
        var c = "";
        $('input[name="CB_Fields"]').each(function (i) {
            if (this.checked) {
                c += $(this).attr("id") + ",";
                f += "[" + $(this).attr("id") + "] as " + $(this).next("label").html() + ",";
            }
        });
        $("#checked_Fields").val(c);
        $("#TB_Fields").val(f);

        $("#TB_Field").val($("#DDL_Fields").val());
    })

    //获取导出默认项
    if ($("#checked_Fields").length != 0 && $('input[name="CB_Fields"]').length != 0) {
        $('input[name="CB_Fields"]').prop("checked", false);
        var s = $("#checked_Fields").val().split(",");
        for (i = 0; i < s.length; i++) {
            if (s[i] != "")
                $("#" + s[i]).prop("checked", true);
        }
    }

    //导出范本数据，获取默认项
    $("#DownExcel").click(function () {
        $("#TB_ImportFields").val($("#ImportFields").text());
    })

    //导出范本数据，获取默认项
    $(".uploadall").click(function () {
        $('.Editor').xheditor().exec('uploadAll');
        return false;
    })

    //获取默认项
    if ($("#TB_Field").length != 0 && $("#DDL_Fields").length != 0) {
        $("#DDL_Fields option[value='" + $("#TB_Field").val() + "']").attr("selected", "selected");
    }

    //提交课程管理刷新左框架
    if ($("#C_Word").length != 0) {
        $("#ButtonManage").click(function () {
            parent.left.location.reload();
        })

        if (action == "up" || action == "down") {
            parent.left.location.reload();
        }

    }

    //根据地址栏隐藏指定字段
    var hide = $().urlget("hide");
    if (hide != undefined) {
        var item = hide.split(",");
        for (i = 0; i < item.length; i++) {
            $("#manage_" + item[i]).hide();
        }
    }

    //内置系统赋值
    var islike = $().urlget("islike");
    var classid = $().urlget("classid");
    var system = $().urlget("system");
    if (islike != undefined || system != undefined) {
        //锁定classid默认项
        if (islike == "false")
            $("#DDL_ClassID").val(classid);

        if (islike != undefined)
            islike = "&islike=" + islike + "&classid=" + classid;
        else
            islike = "";

        if (system != undefined)
            system = "&system=" + system;
        else
            system = "";

        if (hide != undefined)
            hide = "&hide=" + hide;
        else
            hide = "";

        $("a[href='?action=import'],a[href='?action=export']").hide();
        $("a[href='?action=manage'],a[href='?action=insert'],a[class='up'],a[class='down'],a[class='show'],a[class='update'],a[class='copy'],a[class='delete']").attr("href", function () { return this.href + islike + system + hide });
    }

    //给控件赋值
    var controlid = $().urlget("controlid");
    if (controlid != undefined) {
        $("#" + controlid).val($().urlget("value"));
    }

    //输入页码功能
    if ($(".page").length != 0) {
        if ($(".page a:last").length != 0) {
            var lastpage = $(".page a:last").attr("href");
            var allnum = lastpage.substring(lastpage.lastIndexOf("=") + 1, lastpage.length);

            if (parseFloat(allnum) > 10) {
                $(".page").append("&nbsp;&nbsp;<input type='text' id='jumpnum' style='width:15px;'><input class='search' id='pagejump' type='button' value='跳转'>")
            }

            if ($("#pagejump").length != 0) {
                $("#pagejump").click(function () {
                    if (parseFloat($("#jumpnum").val()) > 0 && parseFloat($("#jumpnum").val()) <= parseFloat(allnum)) {
                        location.href = "?page=" + $("#jumpnum").val();
                    }
                    else {
                        alert("请输入正确的分页数字!");
                    }
                    return false;
                })
            }
        }
    }
})
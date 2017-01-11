$(function(){
    $.cookie.raw = true;
    $.cookie.json = true;
    /* $(".scroll").sudyScroll({
 		width: 140,		// 单元格宽度
		height: 100,		// 单元格高度
		display: 4,		// 显示几个单元
		step: 2,			// 每次交替增加几个单元，值不能大于display
		dir:"x",		// 交替方向，纵向为"y"，水平为"x"，默认为"y"纵向交替
		auto:true,		// 是否自动交替,默认为自动
		speed:500,		// 交替速度
		hoverPause:5000,		// 交替暂留时间
		navigation:false,		// 是否显示导航按钮
		navTrigger:"click", 	// 导航按钮事件
		pagination:false,		// 是否显示索引按钮
		pagTrigger:"mouseenter"  //索引按钮事件
	}); */
	
	$(".search-submit").click(function(event) {
		$(this).removeAttr("name");
		/* Act on the event */
		event.preventDefault();
		var val = $.trim($(".search-title").val());
		if(val!==""){
			$(".search-window").find("form").submit();
			
		}else{
			alert("请输入关键词");
		}
		return false;
	});
	
	
	function filterDataImg(data, filter, num){
		var _data = data.slice(0);
		var _dataImg = $.map(data, function(val, i){
			if(val.img&&$.inArray(i, filter)===-1)
				return i;
		});
		var id = 0;
		$.each(filter, function(index, j){
			if(!data[j].img){
				var temp = $.extend(true, {}, data[j]);
				if(_dataImg[id]){
					data[j] = _data[_dataImg[id]];
					data[_dataImg[id]] = temp;
					id++;
				}
			}
		});
		return data.slice(0,num);
	}
	
	function filterData(data,num,m){
		var items = new Array(num);
		var after = data.slice(m);
			$.each(items, function(index, val){	
				items[index] = index > 3 ? after[index-3] : data[index];
				
				if((index===0||index===3)&&!items[index].img){
						$.each(after, function(i, v){
							if(after[i].img){
								var temp = $.extend(true,{}, data[index]);
								items[index] = $.extend(true,{},after[i]);
								after[i] = temp;
								return false;
							}
						});
				}
				
				
				
				
			});

		//console.log(items);
		return items;
		
		
	}
	
	function showxnxw(data, cookie){
		if(!data||!data.length)
			return;
		$(".xnxw").empty();
		var html = "<ul class=\"clearfix\">";
		$.each(data, function(index, val){
			var i = index%4;
			if(i===0){
				html += '<li class="news-box clearfix"><div class="qbox">';
			}
			if(val.img !== ""){
				html +="<div class='news n"+index+" clearfix'><div class='slt' ><a href='"+val.link+"' target='_blank' title='"+val.title+"' ><div class='imgs' style='background-image:url("+val.img+")'></div></a></div><div class='bt'><a href='"+val.link+"' target='_blank' title='"+val.title+"'>"+val.title+"</a></div><div class='time'>"+val.date+"</div><div class='nr'><a href='"+val.link+"' target='_blank' title='"+val.abstract+"' >"+val.abstract+"</a></div></div>";  
			}else{
				html +="<div class='news n"+index+" clearfix'><div class='bt'><a href='"+val.link+"' target='_blank' title='"+val.title+"'>"+val.title+"</a></div><div class='time'>"+val.date+"</div><div class='nr'><a href='"+val.link+"' target='_blank' title='"+val.abstract+"' >"+val.abstract+"</a></div></div>";  
			}
			if(index&&i===0){ 
				if(index===data.length-1){
					html += '</div></li>';
				}
				//else{
				//	html += '</div></li><li class="news-box clearfix"><div class="qbox">';
				//}
			}else{
				if(index===data.length-1){
					html += '</div></li>';
				}
			}
			//$.cookie("xnxw"+index, val);
		});
		
		html +="</ul>";
		
		$(".xnxw").html(html);
		
//		$(".zdy-2>ul").sudyScroll({
//			width: 766,		// 单元格宽度
//			height: 475,	// 单元格高度
//			display: 1,		// 显示几个单元
//			step: 1,		// 每次交替增加几个单元，值不能大于display
//			dir:"x",		// 交替方向，纵向为"y"，水平为"x"，默认为"y"纵向交替
//			auto:false,		// 是否自动交替,默认为自动
//			speed:500,		// 交替速度
//			hoverPause:5000,		// 交替暂留时间
//			navigation:false,		// 是否显示导航按钮
//			navTrigger:"click", 	// 导航按钮事件
//			pagination:true,		// 是否显示索引按钮
//			pagTrigger:"mouseenter"  //索引按钮事件
//		});
		
	}
	
	//////////
	function filterData1(data,num,m){
		var items = new Array(num);
			var after = data.slice(m);
			$.each(items, function(index, val){
				items[index] = data[index];
		});
		//console.log(items);
		return items;
	}
	function showxysh1(data, cookie){
		if(!data||!data.length)
			return;
		//console.log("xysh1 newsdata frorm", cookie?"cookie":"ajax");
		$(".xysh1").empty();
		$.each(data,function(index, val){
				$(".xysh1").append("<li class='newsn n"+index+" clearfix'><p class='news_title'><a href='"+val.link+"' target='_blank' title='"+val.title+"' >"+val.title+"</a></p><p class='news_meta'><a href='"+val.link+"' target='_blank' title='"+val.abstract+"' >"+val.abstract+"</a></p></li>"); 
				//$.cookie("xysh1"+index, val);
		});
		
	}
	
	//////////
	
/* 	function getCookieNews(name, num){
		var cookie = true;
		var index = 0;
		var items = [];
		while(cookie){
			var news = $.cookie(name+index);
			if(!news||index>num-1){
				break;
			}
			//console.log(name, news);
			var item = $.parseJSON(news);
			items.push(item);
			index++;
		}
		return items;
	} */
	

//	function showkxyj(data, cookie){
//		if(!data||!data.length)
//			return;
//		//console.log("kxyj newsdata frorm", cookie?"cookie":"ajax");
//		$(".kxyj").empty();
//		$.each(data,function(index, val){
//			if(val.img !== ""){
//				$(".kxyj").append("<li class='news n"+index+" clearfix'><div class='slt pr'><a href='"+val.link+"' target='_blank' title='"+val.title+"'><div class='imgs' style='background-image:url("+val.img+")'></div></a><div class='bt'><a href='"+val.link+"' target='_blank' title='"+val.title+"'>"+val.title+"</a></div><div class='titlebg'></div></div><div class='time'>"+val.date+"</div><div class='nr'><a href='"+val.link+"' target='_blank' title='"+val.abstract+"' >"+val.abstract+"</a></div></li>");
//			}else{
//				$(".kxyj").append("<li class='news n"+index+" clearfix'><div class='bt'><a href='"+val.link+"' target='_blank' title='"+val.title+"'>"+val.title+"</a></div><div class='time'>"+val.date+"</div><div class='nr'><a href='"+val.link+"' target='_blank' title='"+val.abstract+"' >"+val.abstract+"</a></div></li>");  
//			}
//			//$.cookie("kxyj"+index, val);
//		})
//	}
	
	//showxnxw(getCookieNews("xnxw",8), true);
	//showkxyj(getCookieNews("kxyj",4), true);
	//showxysh1(getCookieNews("xysh1",2), true);
	showxnxw(filterDataImg(xnxw,[0,3],8));
   // showkxyj(filterDataImg(kxyj,[0,3],4));
	//showxysh1(filterData1(xysh1,2,2));
	/* $.ajax({
        url: "http://news.nju.edu.cn/njuwebnewsjson.php",
        type: "POST",                                          
        dataType: "jsonp",
        data: {
            token1:"njuweb",
            name:"xnxw"
        },
        success: function(data,textStatus){
			showxnxw(filterDataImg(data,[0,3],8));
        }
    }); */
	
	/* $.ajax({
        url: "http://news.nju.edu.cn/njuwebnewsjson.php",
        type: "POST",                                          
        dataType: "jsonp",
        data: {
            token1:"njuweb",
            name:"kxyj"
        },
        success: function(data,textStatus){
			showkxyj(filterDataImg(data,[0,3],4));
        }
    }); */
	
    /* $.ajax({
        url: "http://news.nju.edu.cn/njuwebnewsjson.php",
        type: "POST",                                          
        dataType: "jsonp",
        data: {
            token1:"njuweb",
            name:"xysh"
        },
        success: function(data,textStatus){
			showxysh1(filterData1(data,2,2));
			
        }
    }); */
	
	
	/* $.ajax({
        url: "http://news.nju.edu.cn/njuwebnewsjson.php",
        type: "POST",                                          
        dataType: "jsonp",
        data: {
            token1:"njuweb",
            name:"xysh"
        },
        success: function(data,textStatus){
			var items = new Array(1);
			var after = data.slice(1);
			$.each(items,function(index, val){
				    items[index] = data[index];
					if(data[index].img){
						val = data[index];
					}else{
						$.each(after, function(i, v){
							if(after[i].img){
								items[index] = after[i];
								after[i] = {};
								return false;
							}
						});
					}
			});
			$.each(items,function(index, val){
					$(".xysh2").append("<li class='newsw n"+index+" clearfix'><a href='"+val.link+"' target='_blank' title='"+val.title+"' ><div class='imgs' style='background-image:url("+val.img+")'></div></a><p class='news_title'><a href='"+val.link+"' target='_blank' title='"+val.title+"' >"+val.title+"</a></p><p class='title_bg'></p></li>"); 
			});
        }
    }); */
	
	/* 	$(function(data,textStatus){
			var items = new Array(1);
			var after = data.slice(1);
			$.each(items,function(index, val){
				    items[index] = data[index];
					if(data[index].img){
						val = data[index];
					}else{
						$.each(after, function(i, v){
							if(after[i].img){
								items[index] = after[i];
								after[i] = {};
								return false;
							}
						});
					}
			});
			$.each(items,function(index, val){
					$(".xysh2").append("<li class='newsw n"+index+" clearfix'><a href='"+val.link+"' target='_blank' title='"+val.title+"' ><div class='imgs' style='background-image:url("+val.img+")'></div></a><p class='news_title'><a href='"+val.link+"' target='_blank' title='"+val.title+"' >"+val.title+"</a></p><p class='title_bg'></p></li>"); 
			});
    }); */
	
	
	/*复制下拉*/
	$(".navbox .wp_nav > .nav-item").each(function(index,val){
		$(this).find("a").removeAttr("title");
		var html = $(this).find(".sub-nav").html();
		$(".navlist").append("<div class='navlist-li navlist-li-"+index+"'></div>");
		$(".navlist").find(".navlist-li").eq(index).html(html);
		$(".navlist").children(".navlist-li").find("ul").remove();
	});
	$(".navlist").append("<div class='clear'></div>");
	
	
	
	$(".navbar .nav-item").click(function(){
	  $(".navlist").slideToggle("slow");
      return false;  
	});
	
	/*媒体链接*/
	$(".shares .share").each(function(){
		$(this).children("a").hover(function(){
			$(this).parent().find(".con").fadeIn();			
		},function(){
			$(this).parent().find(".con").fadeOut();
		});
	});
	
	$("body,html").click(function(){
			$(".search-window").animate({"width":"45px"});
			$(".searchbtn").stop(true,true).fadeIn();
			$(".navlist").slideUp();
				
	});
	
	$(".searchbtn").click(function(){
		$(this).stop(true,true).fadeOut();
		$(".search-window").stop(true,true).animate({"width":"200px"});
		return false;
	});	
	$(".search-input").click(function(){
		return false;
	});
	



	$(".kxbtn").hover(function(){
		$(".btncon").fadeIn();
	},function(){
		$(".btncon").fadeOut();
	});

       $(".site-logo").parent("a").attr("href","/main.htm");

});

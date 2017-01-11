/*
*构建SDAPP
*/
SDAPP = (function(){
	function App(options){
		this.O = options;
		this.J = jQuery;
		this.init();
	}
	return App;
})();

SDAPP.prototype.init = function(){
	this.client();
	this.plugin();
	this.renew();
}

/**
 * getClient
 */
SDAPP.prototype.client = function(){
	return {
		"width":this.J(window).width(),
		"height":this.J(window).height()
	};
}

/**
 * resize
 */
SDAPP.prototype.renew = function(){
	var _this = this;
	_this.J(window).resize(function(event) {
		_this.client();
		window.console&&console.log(_this.client().width,_this.client().height);
	});
}

/**
 * plugin
 */
SDAPP.prototype.plugin = function(){
	var _this = this;
	// focus
	this.O['focus']&&this.J("[data-focus]").sudyfocus(this.O['focus']);

	// menu
	
	this.J(".main-nav-window").find("li").on("mouseenter",function(){
		_this.J(this).children('.sub-menu').show();
	}).on("mouseleave",function(){
		_this.J(this).children('.sub-menu').hide();
	});

	var $aside = this.J("#wp-navi-aside");
	if(this.O['menu']){
		if(/slide/.test(this.O.menu['type'])){
			this.J(".wp-navi").addClass('wp-navi-slide');
			this.J(".navi-slide-head").on("click",function(){
				_this.J(this).siblings().slideToggle(150);
			});
		}
		if(/aside/.test(this.O.menu['type'])){
			var $menus = [];
			this.J("[data-nav-aside]").clone().each(function(index, el) {
				var opt = _this.J.parseJSON(_this.J(el).attr("data-nav-aside"));
				$menus[opt.index] = _this.J('<div class="navi-aside-head"><h3 class="navi-aside-title">'+opt.title+'</h3></div>').add(el);
			});;

			$.each($menus, function() {
				_this.J(this).appendTo(".navi-aside-wrap");
			});

			this.J(".navi-aside-toggle").addClass('navi-aside-toggle-show').on("click",function(){
				$aside.addClass('wp-navi-aside-active');
				_this.J("html").css({marginTop:0});
				_this.J("body").addClass('navi-aside-page').css({width:_this.client().width,height:_this.client().height})
				.stop().animate({marginLeft:216}, 250);
				_this.J(".aside-inner", $aside).addClass('aside-inner-show').stop().animate({left:0}, 250);
			});
			this.J(".navi-aside-mask").on("click",function(event){
				event.preventDefault();
				_this.J("body").removeClass('navi-aside-page').stop().animate({marginLeft:0}, 250,function(){
					_this.J("body").removeAttr('style');
					$aside.removeClass('wp-navi-aside-active');
					_this.J("html").removeAttr('style');
				});
				_this.J(".aside-inner", $aside).removeClass('aside-inner-show').stop().animate({left:-216}, 250);
			});

			this.J(".menu-switch-arrow").prev().on("click",function(){
				_this.J(this).toggleClass('menu-open-arrow').siblings(".sub-menu").slideToggle(250);
			});
                        this.J(".menu-switch-arrow").on("click",function(){
				_this.J(this).toggleClass('menu-open-arrow').siblings(".sub-menu").slideToggle(250);
			});
		}
	}

	this.J(".column-head").on("click",function(event){
		event.preventDefault();
		_this.J(".column-body").slideToggle(150);
	});

	if(this.J(".column-item").length<1){
		this.J(".column-switch").hide();
		this.J(".column-head").unbind("click");
	}

	// small screen
	if(this.client().width<768){
		if(this.O['view']){
			this.J(window).load(function() {

				/* Act on the event */
				var $images = _this.J(_this.O['view']['target']);
				var imgs = [],uWidth , index, total = 0, current, wScroll;
				_this.J.each($images, function(index, val) {
					if(_this.J(val).width()>_this.O['view']['minSize']&&_this.J(val).height()>_this.O['view']['minSize']){
						total++;
						var url = _this.J(val).attr("src"),title = _this.J(val).attr("title")||"";
						imgs.push({"url":url,"title":title});
						_this.J(val).addClass('image-view').wrap('<em class="view-box"><i class="view-box-inner"></i></em>')
						.parent().append('<a href="javascript:;" class="open-view" data-view-index="'+total+'"><i class="view-icon"></i></a>');
						_this.J('#view-image-items').append('<li style="background-image:url('+url+')"></li>');
					}
				});
				uWidth = total*_this.client().width;
				_this.J('#view-image-items').width(uWidth).children().css({"width":_this.client().width,"height":_this.client().height});

				function aniView(){
					_this.J('#view-image-items').stop(true,false).animate({"left":-index*_this.client().width},300, function(){
						current = index + 1;
						_this.J("#view-current").html(current);
						_this.J("#view-original-image").attr("href",imgs[index].url);
					});
				}

				function viewImageInfo(){
					_this.J("#view-current").html(current);
					_this.J("#view-title").html(imgs[index].title);
					_this.J("#view-original-image").attr("href",imgs[index].url);
				}

				_this.J(".open-view").on("click",function(event){
					event.preventDefault();
					_this.J("body").css({"height":_this.client().height,"overflow":"hidden"});
					wScroll = _this.J(window).scrollTop();
					current = parseInt(_this.J(this).attr("data-view-index"));
					index = current - 1;
					_this.J("#wp-view-page").show();
					_this.J("#view-total").html(total);
					_this.J('#view-image-items').css({"left":-index*_this.client().width});
					viewImageInfo();

				});

				_this.J(window).resize(function(){
					_this.J("body").css({"height":_this.client().height});
					uWidth = total*_this.client().width;
					_this.J('#view-image-items').css({"width":uWidth,"left":-index*_this.client().width}).children().css({"width":_this.client().width,"height":_this.client().height});
				});

				_this.J("#view-body").on("click",function(event){
					event.preventDefault();
					_this.J(this).toggleClass('hide-view-bar');
					if(_this.J(this).hasClass('hide-view-bar')){
						_this.J("#view-head").stop().animate({"top":-42}, 200);
						_this.J("#view-foot").stop().animate({"bottom":-42}, 200);
					}else{
						_this.J("#view-head").stop().animate({"top":0}, 200);
						_this.J("#view-foot").stop().animate({"bottom":0}, 200);
					}
				});

				function aniView(){
					_this.J('#view-image-items').stop(true,false).animate({"left":-index*_this.client().width},300, function(){
						current = index + 1;
						viewImageInfo();
					});
				}

				_this.J("#view-body").sudyTouch({
					"swipeLeft": function(){
						index++;
						if((total-1)<index)index = total-1;
					},
					"swipeRight":function(){
						index--;
						if(index<0)index=0;
					},
					"swipeEnd":function(){
						aniView();
					}
				});
				_this.J("#back-read-page").on("click",function(event){
					event.preventDefault();
					_this.J("body").css({"height":"","overflow":""});
					_this.J("#wp-view-page").hide();
					_this.J(window).scrollTop(wScroll);
				});
			});
		}

		// jumphandle
		
		this.J('<div id="jumphandle"><a id="gotop"></a><a id="gobot"></a></div>').appendTo('body');
		this.J(window).scroll(function(event) {
			/* Act on the event */
			var scrolltop = _this.J(window).scrollTop();
			if(scrolltop>200){
				_this.J("#jumphandle").show();
			}else{
				_this.J("#jumphandle").hide();
			}
		});
		this.J("#gotop").on("click",function(){
			_this.J("body,html").stop().animate({scrollTop:0}, 500);
		});
		this.J("#gobot").on("click",function(){
			_this.J("body,html").stop().animate({scrollTop:_this.J(document).height()}, 500);
		});


		//read model
		
		this.J('#read-options').on("click",function(e){
			e.preventDefault();
			_this.J("#read-setting").slideToggle(100);
		});

		this.J("#read-model").on("click",function(){
			_this.J("html").toggleClass('night-model');
			if(_this.J("html").hasClass('night-model')){
				_this.J(this).html('\u767d\u5929\u6a21\u5f0f');
			}else{
				_this.J(this).html('\u591c\u665a\u6a21\u5f0f');
			}
		});

		var fontSize = 16; // 初始字体大小
		function setFontSize(){
			_this.J(".read").removeClass('set-fz12 set-fz14 set-fz16 set-fz18 set-fz20 set-fz22').addClass('set-fz'+fontSize);
		}
		this.J("#larger-font").on("click",function(){
			fontSize += 2;
			if(fontSize>22)fontSize=22;
			setFontSize();	
		});
		this.J("#smaller-font").on("click",function(){
			fontSize -= 2;
			if(fontSize<12)fontSize=12;
			setFontSize();
		});
	}

}
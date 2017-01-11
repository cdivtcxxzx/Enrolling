$(pageInit);

function media(url, width, height, autoPlay)
{
	var myJsVal = "";

	if(url == "http://" || url == "")
	{
		myJsVal = "";
	}
	else if(url.lastIndexOf(".flv") > 0 || url.lastIndexOf(".mp4") > 0 || url.lastIndexOf(".mov") > 0)
	{
		myJsVal+="<div>";
		myJsVal+="<embed width=\""+ width +"\" height=\""+ (parseFloat(height)+24) +"\" type=\"application/x-shockwave-flash\" src=\"files/common/flvplayer.swf\" allowfullscreen=\"true\" loop=\"true\" play=\"true\" menu=\"false\" quality=\"high\" wmode=\"opaque\" flashvars=\"file="+ url +"&autostart="+ autoPlay +"&provider=video\" classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-4445535400000\" />";
		myJsVal+="</div>";
	}
	else if(url.lastIndexOf(".rm") > 0)
	{
		myJsVal+="<div>";
		myJsVal+="<object mediatype=\"0\" width=\""+ width +"\" height=\""+ height +"\" classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\">";
		myJsVal+="<param name=\"src\" value=\""+ url +"\" \/>";
		myJsVal+="<param name=\"autostart\" value=\""+ autoPlay +"\" \/>";
		myJsVal+="<param name=\"loop\" value=\"true\" \/>";
		myJsVal+="<param name=\"menu\" value=\"false\" \/>";
		myJsVal+="<\/object>";
		myJsVal+="</div>";
	}
	else
	{
		myJsVal+="<div>";
		myJsVal+="<object mediatype=\"0\" width=\""+ width +"\" height=\""+ height +"\" codebase=\"http:\/\/microsoft.com\/windows\/mediaplayer\/en\/download\/\" classid=\"clsid:6BF52A52-394A-11D3-B153-00C04F79FAA6\">";
		myJsVal+="<param name=\"url\" value=\""+ url +"\" \/>";
		myJsVal+="<param name=\"autostart\" value=\""+ autoPlay +"\" \/>";
		myJsVal+="<param name=\"loop\" value=\"true\" \/>";
		myJsVal+="<param name=\"menu\" value=\"false\" \/>";
		myJsVal+="<\/object>";
		myJsVal+="</div>";
	}
	return myJsVal;
}

function iframe(url, width, height)
{
	var myJsVal = "";

	if(url == "http://" || url == "")
	{
		myJsVal = "";
	}
	else
	{
		myJsVal = "<iframe border=\"0\" frameborder=\"0\" framespacing=\"0\" style=\"width:"+ width +"px; height:"+ height +"px\" marginheight=\"0\" marginwidth=\"0\" scrolling=\"auto\" src=\""+ url +"\" vspale=\"0\" name=\"iframe\" id=\"iframe\"><\/iframe>";
	}
	return myJsVal;
}

function Flash(url, width, height)
{
	var myJsVal = "";
	if(url == "http://" || url == "")
	{
		myJsVal = "";
	}
	else
	{
		myJsVal = "<embed width=\""+ width +"\" height=\""+ height +"\" type=\"application/x-shockwave-flash\" src=\""+ url +"\" allowfullscreen=\"true\" loop=\"true\" play=\"true\" menu=\"false\" quality=\"high\" wmode=\"opaque\" classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-4445535400000\" />";
	}
	return myJsVal;
}

function pageInit()
{
	//编辑器
	var allPlugin={
		Video:{c:'xheBtnMedia',t:'视频',s:'ctrl+m',h:1,e:function(){
			var _this=this;
			var jTest=$('<div>视频文件： <input type="text" id="xheMediaSrc" value="http://" class="xheText" /></div><div>宽度高度： <input type="text" id="xheMediawidth" style="width:40px;" value="480" /> x <input type="text" id="xheMediaheight" style="width:40px;" value="400" /></div><div>自动播放： <input type="checkbox" id="xheMediaplay"></div><div style="text-align:right;"><input type="button" id="xheSave" value="确定" /></div>');
			if(_this.settings.upMediaUrl)
				_this.uploadInit($('#xheMediaSrc',jTest),_this.settings.upMediaUrl,_this.settings.upMediaExt);
			var jSave=$('#xheSave',jTest);
			jSave.click(function(){
				_this.loadBookmark();
				_this.pasteHTML(media($('#xheMediaSrc',jTest).val(),$('#xheMediawidth',jTest).val(),$('#xheMediaheight',jTest).val(),$('#xheMediaplay',jTest).prop("checked")));
				_this.hidePanel();
				return false;	
			});
			_this.showDialog(jTest);
		}},

		Iframe:{c:'xheBtnIframe',t:'网页',s:'ctrl+i',h:1,e:function(){
			var _this=this;
			var jTest=$('<div>网页文件： <input type="text" id="xheIframeSrc" value="http://" class="xheText" /></div><div>宽度高度： <input type="text" id="xheIframewidth" style="width:40px;" value="480" /> x <input type="text" id="xheIframeheight" style="width:40px;" value="400" /></div><div style="text-align:right;"><input type="button" id="xheSave" value="确定" /></div>');
			var jSave=$('#xheSave',jTest);
			jSave.click(function(){
				_this.loadBookmark();
				_this.pasteHTML(iframe($('#xheIframeSrc',jTest).val(),$('#xheIframewidth',jTest).val(),$('#xheIframeheight',jTest).val()));
				_this.hidePanel();
				return false;	
			});
			_this.showDialog(jTest);
		}},

		//$(".ispring").click(function(){alert("xxxxxxx")});
		Flash1:{c:'xheBtnFlash1',t:'Flash动画',s:'ctrl+i',h:1,e:function(){
			
			var _this=this;
			var jTest=$('<div>动画文件： <input type="text" id="xheFlash1Src" value="http://" class="xheText" /></div><div>宽度高度： <input type="text" id="xheFlash1width" style="width:40px;" value="480" /> x <input type="text" id="xheFlash1height" style="width:40px;" value="400" /></div><div><a href="#" class="flashpaper">FlashPaper 尺寸</a> <a href="#" class="ispring">Ispring 尺寸</a></div><div style="text-align:right;"><input type="button" id="xheSave" value="确定" /></div>');
			if(_this.settings.upFlashUrl)
				_this.uploadInit($('#xheFlash1Src',jTest),_this.settings.upFlashUrl,_this.settings.upFlashExt);
			var jSave=$('#xheSave',jTest);
			$(".ispring",jTest).click(function(){$('#xheFlash1width',jTest).val("600");$('#xheFlash1height',jTest).val("450");return false;});
			$(".flashpaper",jTest).click(function(){$('#xheFlash1width',jTest).val("100%");$('#xheFlash1height',jTest).val("700");return false;});
			jSave.click(function(){
				_this.loadBookmark();
				_this.pasteHTML(Flash($('#xheFlash1Src',jTest).val(),$('#xheFlash1width',jTest).val(),$('#xheFlash1height',jTest).val()));
				_this.hidePanel();
				return false;	
			});
			_this.showDialog(jTest);
		}},
		
		
		Word:{c:'xheBtnWord',t:'导入word',s:'ctrl+w',e:function(){
			var _this=this;
			_this.saveBookmark();
			_this.showIframeModal('导入word',"{editorRoot}xheditor_plugins/word/word.aspx",function(v){
				_this.loadBookmark();
				_this.pasteHTML(v);
				},320,200);
		}},
		
		UploadPic:{c:'xheuploadPic',t:'上传图片',s:'ctrl+p',e:function(){
			var _this=this;
			_this.showIframeModal('上传图片', "{editorRoot}xheditor_plugins/multiupload/multiupload.aspx?type=image&tag=hide&uploadurl={editorRoot}xheditor_plugins/multiupload/upload.aspx%3Ftype%3Dimage&ext=图片文件(*.jpg;*.jpeg;*.gif;*.png;*.bmp;)",function(v){
				$("." + _this.settings.upMediaUrl).parent().prevAll("input").val(v[0].src);
				},320,200);
		}},

		UploadFile:{c:'xheuploadFile',t:'上传文件',s:'ctrl+f',e:function(){
			var _this=this;
			_this.showIframeModal('上传文件', "{editorRoot}xheditor_plugins/multiupload/multiupload.aspx?type=file&tag=hide&uploadurl={editorRoot}xheditor_plugins/multiupload/upload.aspx%3Ftype%3Dimage&ext=附件文件(*.gif;*.jpg;*.bmp;*.png;*.zip;*.rar;*.txt;*.doc;*.docx;*.xlsx;*.pdf;*.xls;*.ppt;*.swf;*.htm;*.html)",function(v){
				$("." + _this.settings.upMediaUrl).parent().prevAll("input").val(v[0].src);
				},320,200);
		}},
		
		UploadAll:{c:'xheuploadAll',t:'批量上传图片',s:'ctrl+7',e:function(){
			var _this=this;
			_this.showIframeModal('批量上传图片', "{editorRoot}xheditor_plugins/multiupload/multiupload.aspx?type=image&tag=hide&count=20&uploadurl={editorRoot}xheditor_plugins/multiupload/upload.aspx%3Ftype%3Dimage&ext=图片文件(*.jpg;*.jpeg;*.gif;*.png;*.bmp;)",function(v){
				var urlall = "";
				for(i=0; i<v.length; i++)
				{
					urlall += "|" + v[i].src;
				};
				$("." + _this.settings.upMediaUrl).parent().prevAll("input").val(urlall.substring(1));
				},320,200);
		}}
	};

	if($(".Editor").length != 0)
		$('.Editor').xheditor({plugins:allPlugin,width:'80%', height:'240',internalScript:true,tools:'Source,Preview,Fullscreen,|,Cut,Copy,Paste,Pastetext,|,Blocktag,Fontface,FontSize,|,FontColor,BackColor,|,Bold,Italic,Underline,Strikethrough,/,SelectAll,Removeformat,|,Align,List,Outdent,Indent,|,Link,Unlink,Anchor,|,Img,Flash1,Video,Word,Iframe,Table,|,About',upLinkUrl:"!{editorRoot}xheditor_plugins/multiupload/multiupload.aspx?type=file&uploadurl={editorRoot}xheditor_plugins/multiupload/upload.aspx%3Ftype%3Dfile&ext=附件文件(*.gif;*.jpg;*.bmp;*.png;*.zip;*.rar;*.txt;*.doc;*.pdf;*.xls;*.ppt;*.swf;*.htm;*.html)",upImgUrl:'!{editorRoot}xheditor_plugins/multiupload/multiupload.aspx?type=image&uploadurl={editorRoot}xheditor_plugins/multiupload/upload.aspx%3Ftype%3Dimage&ext=图片文件(*.jpg;*.jpeg;*.gif;*.png;*.bmp;)',upFlashUrl:'!{editorRoot}xheditor_plugins/multiupload/multiupload.aspx?type=flash&uploadurl={editorRoot}xheditor_plugins/multiupload/upload.aspx%3Ftype%3Dflash&ext=Flash动画(*.swf)',upMediaUrl:'!{editorRoot}xheditor_plugins/multiupload/multiupload.aspx?type=media&uploadurl={editorRoot}xheditor_plugins/multiupload/upload.aspx%3Ftype%3Dmedia&ext=多媒体文件(*.wmv;*.flv;*.rm;*.avi;*.mp4;*.mov;*.wma;*.mp3;*.mid)'});
	if($(".EditorBasic").length != 0)
		$('.EditorBasic').xheditor({skin:'vista',width:'80%', height:'120',tools:'Cut,Copy,Paste,Pastetext,|,Fontface,FontSize,|,FontColor,BackColor,|,Bold,Italic,Underline,Strikethrough,|,Align,List,Outdent,Indent,About'});

	if($(".xheditorupload").length != 0)
	{
		$(".xheditorupload").each(function(i)
		{
			$(this).parent().append('<div style="display:none;"><textarea class="EditorTest_'+ i +'"></textarea></div>');
			$(this).parent().find("textarea").xheditor({plugins:allPlugin,tools:'UploadPic,UploadFile,UploadAll,|,About',upMediaUrl:$(this).parent().find("textarea").attr("class")});
		})

		$(".uploadpic").click(function()
		{
			$(this).parent().find("textarea").xheditor().exec('UploadPic');
			return false;
		});

		$(".uploadfile").click(function()
		{
			$(this).parent().find("textarea").xheditor().exec('UploadFile');
			return false;
		});

		$(".uploadall").click(function()
		{
			$(this).parent().find("textarea").xheditor().exec('UploadAll');
			return false;
		});
	}

	$(".loadeditor").live("click", function()
	{
		var iseditor = $(this).attr("title");
		if(iseditor == "false")
		{
			$(this).attr("title","true");
			$(this).text("[卸载编辑器]");
			$(this).parents("div").prevAll(".EditorStandard").xheditor({plugins:allPlugin,width:'100%', height:'120',internalScript:true,tools:'Source,|,Paste,Pastetext,|,Fontface,FontSize,|,FontColor,BackColor,|,Bold,Italic,|,SelectAll,Removeformat,|,Align,List,|,Link,Unlink,|,Img,Flash1,Video,Word,Table,|,About',upLinkUrl:"!{editorRoot}xheditor_plugins/multiupload/multiupload.aspx?type=file&uploadurl={editorRoot}xheditor_plugins/multiupload/upload.aspx%3Ftype%3Dfile&ext=附件文件(*.gif;*.jpg;*.bmp;*.png;*.zip;*.rar;*.txt;*.doc;*.pdf;*.xls;*.ppt;*.swf;*.htm;*.html)",upImgUrl:'!{editorRoot}xheditor_plugins/multiupload/multiupload.aspx?type=image&uploadurl={editorRoot}xheditor_plugins/multiupload/upload.aspx%3Ftype%3Dimage&ext=图片文件(*.jpg;*.jpeg;*.gif;*.png;*.bmp;)',upFlashUrl:'!{editorRoot}xheditor_plugins/multiupload/multiupload.aspx?type=flash&uploadurl={editorRoot}xheditor_plugins/multiupload/upload.aspx%3Ftype%3Dflash&ext=Flash动画(*.swf)',upMediaUrl:'!{editorRoot}xheditor_plugins/multiupload/multiupload.aspx?type=media&uploadurl={editorRoot}xheditor_plugins/multiupload/upload.aspx%3Ftype%3Dmedia&ext=多媒体文件(*.wmv;*.flv;*.rm;*.avi;*.mp4;*.mov;*.wma;*.mp3;*.mid)'});
		}
		else
		{
			$(this).attr("title","false");
			$(this).text("[加载编辑器]");
			$(this).parents("div").prevAll(".EditorStandard").xheditor(false);
		}

		return false;
	})
}
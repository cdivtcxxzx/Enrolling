/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    //定义语言
    config.language = "zh-cn";
    // config.uiColor = '#AADC6E';
    //字体
    config.font_names = '宋体;楷体;新宋体;黑体;隶书;幼圆;微软雅黑;Arial;Comic Sans MS;Courier New;Tahoma;Times New Roman;Verdana';
    config.toolbar = 'Basic';
    config.toolbar_Basic =
    [
        { name: 'styles', items: ['Font', 'FontSize'] }, //样式栏：字体、大小
        {name: 'paragraph', items: ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] }, //对齐栏：左对齐、中心对齐、右对齐、两端对齐
        {name: 'colors', items: ['TextColor', 'BGColor'] }, //颜色栏：文本颜色、背景颜色
        {name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] }, //基本样式栏：加粗、倾斜、下划线、删除线、下标、上标、移除样式        
        {name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule'] }, //插入栏：图像、flash、表格、水平线        
        {name: 'links', items: ['Link', 'Unlink'] }, //超链接栏：增加超链接、取消超链接        
        {name: 'document', items: ['Source']}//源代码栏：查看源代码
    ];
    //工具栏是否可以被收缩（即：右上角的三角符号是否显示）    
    config.toolbarCanCollapse = true;    //工具栏默认是否展开    
    config.toolbarStartupExpanded = true;    // 是否允许“拖拽改变尺寸”功能（即：右下角的三角符号是否显示）    
    config.resize_enabled = false;    //当输入：shift+Enter时插入的标签    
    config.shiftEnterMode = CKEDITOR.ENTER_P; //可选：CKEDITOR.ENTER_BR或CKEDITOR.ENTER_DIV    //回车（Enter）时产生的标签    
    config.enterMode = CKEDITOR.ENTER_BR; //可選：CKEDITOR.ENTER_BR或CKEDITOR.ENTER_DIV    //宽度    
    //config.width = "600px";    //高度    
    //config.height = "500px";    //默认样式    
    //config.skin ：'kama'（默认）、'office2003'、'v2'    
    //config.skin = "kama";    
    //工具栏的位置    //config.toolbarLocation = 'top'; 
    //可选：bottom    //改变大小的最大高度    
    //config.resize_maxHeight = 3000;    //改变大小的最大宽度    
    //config.resize_maxWidth = 3000;    //改变大小的最小高度    
    //config.resize_minHeight = 250;    //改变大小的最小宽度    
    //config.resize_minWidth = 750;    //当提交包含有此编辑器的表单时，是否自动更新元素內的资料    
    //config.autoUpdateElement = true;    //绝对目录还是相对目录，为空为相对目录    
    //config.baseHref = ''    //编辑器的z-index值    
    //config.baseFloatZIndex = 10000;    
    //以下为上传附件的相关配置，需配合ckfinder控件使用    
    //    var ckfinderPath = "/Scripts";    
    //    config.filebrowserUrl = ckfinderPath + '/ckfinder/ckfinder.html';//上传文件时浏览服务文件夹    
    //    config.filebrowserImageBrowseUrl = ckfinderPath + '/ckfinder/ckfinder.html?Type=Images';
    //上传图片时浏览服务文件夹    
    //    config.filebrowserFlashBrowserUrl = ckfinderPath + '/ckfinder/ckfinder.html?Type=Flash'; 
    //上传Flash时浏览服务文件夹    
    //    config.filebrowserUploadUrl = ckfinderPath + '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files'; //上传文件按钮(标签)    
    //    config.filebrowserImageUploadUrl = ckfinderPath + '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';//上传图片按钮(标签)    //    config.filebrowserFlashUploadUrl = ckfinderPath + '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';//上传Flash按钮(标签)
};

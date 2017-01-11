/**
 * 主要为了实现文章评价功能
 * add by lcfeng
 */
;
(function($) {
    $.fn.WPVisitCount = function(options) {
        var defaults = {};
        var options = $.extend(defaults, options);
        $(this).each(function() {
            var url = $(this).attr("url");
            if (url) {
                initVisitCount(url, $(this));
            }
        });

        //初始化访问次数
        function initVisitCount(url, obj) {
            $.ajax({
                type: "post",
                dataType: "text",
                url: url,
                success: function(result) {
                    //alert(result);
                    if (!isNaN(result) && result != 0) {
                        obj.html(result);
                    }
                },
                error: function(error) {
                }
            });
        }
    };
})(jQuery);

$(document).ready(function() {
    $('.WP_VisitCount').WPVisitCount();
}); 
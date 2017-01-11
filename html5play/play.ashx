<%@ WebHandler Language="C#" Class="play" %>

using System;
using System.Web;

public class play : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";

        if (context.Request["vodurl"] != null)
        {
            string width = "640px";
            string height = "400px";
            string playjpg = "/html5play/play.jpg";
            string url = context.Request["vodurl"].ToString().Trim();
            string vodid = "video_zm_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            if (context.Request["playjpg"] != null) playjpg = context.Request["playjpg"].ToString().Trim();
            if (context.Request["width"] != null) width = context.Request["width"].ToString().Replace("PX", "").Replace("px", "").Trim() + "px";
            if (context.Request["height"] != null) height = context.Request["height"].ToString().Replace("PX", "").Replace("px", "").Trim() + "px";
            //页面中输出视频参数
            //context.Response.Write("<html><head>  <title>视频播放演示-支持移动端播放</title><head>");
            context.Response.Write("document.write('正在读取视频');");

           // context.Response.Write("document.write('<link href=\"/html5play/video-js.css\" rel=\"stylesheet\" type=\"text/css\"><script src=\"/html5play/video.js\"></script><script>videojs.options.flash.swf = \"/html5play/video-js.swf\";</script>');");
            //context.Response.Write(" document.write('<video id=\"" + vodid + "\" class=\"video-js vjs-default-skin  vjs-big-play-centered\" controls preload=\"none\" width=\"" + width + "\" height=\"" + height + "\"  poster=\"" + playjpg + "\"   data-setup=\"{}\"> <source src=\"" + url + "\" type=\"video/mp4\" />    <source src=\"" + url + "\" type=\"video/webm\" />    <source src=\"" + url + "\" type=\"video/ogg\" /> </video>');");
            ////自动播放
            if (context.Request["autoplay"] != null)
            {
                context.Response.Write("document.write('<script type=\"text/javascript\"> var myPlayer = videojs('" + vodid + "');videojs(\"" + vodid + "\").ready(function(){var myPlayer = this;myPlayer.play(); });</script>')");
            }
        }
        else
        {
            context.Response.Write("document.write('调用视频失败，参数传递错误');");
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
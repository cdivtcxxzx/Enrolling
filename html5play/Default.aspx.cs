using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class html5play_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["vodurl"] != null)
        {
            string width = "640px";
            string height = "400px";
            string playjpg = "/html5play/play.jpg";
            string url = Request["vodurl"].ToString().Trim();
            string vodid = "video_zm_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            if (Request["playjpg"] != null) playjpg = Request["playjpg"].ToString().Trim();
            if (Request["width"] != null) width = Request["width"].ToString().Replace("PX", "").Replace("px", "").Trim() + "px";
            if (Request["height"] != null) height = Request["height"].ToString().Replace("PX", "").Replace("px", "").Trim() + "px";
            //页面中输出视频参数
            //Response.Write("<html><head>  <title>视频播放演示-支持移动端播放</title><head>");
            //Response.Write("document.write('正在读取视频');");

            Response.Write("document.write('<link href=\"/html5play/video-js.css\" rel=\"stylesheet\" type=\"text/css\" >');");
            
            Response.Write("document.write('<script src=\"/html5play/video.js\"></script><script>videojs.options.flash.swf = \"/html5play/video-js.swf\";</script>');");
            //Response.Write(" document.write(\"<video id='" + vodid + "' class='video-js vjs-default-skin  vjs-big-play-centered' controls preload='none' width='" + width + "' height='" + height + "'  poster='" + playjpg + "' \");");
            //Response.Write(" document.write(\"data-setup='{}' > <source src='" + url + "' type='video/mp4'  \\/>    <source src='" + url + "' type='video/webm'  \\/>    <source src='" + url + "' type='video/ogg'   \\/><\\/\");");
            //Response.Write(" document.write(\"video> \");");

            ////自动播放
            if (Request["autoplay"] != null)
            {
                Response.Write("document.write('<script type=\"text/javascript\"> var myPlayer = videojs('" + vodid + "');videojs(\"" + vodid + "\").ready(function(){var myPlayer = this;myPlayer.play(); });</script>')");
            }
        }
        else
        {
            Response.Write("document.write('调用视频失败，参数传递错误');");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 网站标题标识
        DataTable wangzxx = Sqlhelper.Serach("SELECT TOP 100 *  FROM [wangzxx] order by xxid asc");
        if (wangzxx.Rows.Count > 0)
        {
            for (int i = 0; i < wangzxx.Rows.Count; i++)
            {
                //网站开关
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "isopen")
                {
                    if (wangzxx.Rows[i]["xxnr"].ToString() == "0")
                    {
                        this.Title = "网站正在维护，请稍后再访问！";
                        Response.Write("<font color=red>网站正在维护，请稍后再访问</font>");
                        Response.End();
                    }
                }
                //网站标题及META
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "title") this.Title = wangzxx.Rows[i]["xxnr"].ToString();
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "MetaKeywords") this.MetaKeywords = wangzxx.Rows[i]["xxnr"].ToString();
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "description") this.MetaDescription = wangzxx.Rows[i]["xxnr"].ToString();
                //logo图片
                //if (wangzxx.Rows[i]["xxgjz"].ToString() == "logoimg") this.logoimg.Style.Add("background-image", wangzxx.Rows[i]["xxnr"].ToString());
                //底部版权
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "copyright") this.copyrights.InnerHtml = wangzxx.Rows[i]["xxnr"].ToString();
                ////BANNER
                //if (wangzxx.Rows[i]["xxgjz"].ToString() == "banner")
                //{
                //    if (wangzxx.Rows[i]["xxnr"].ToString().Length > 4)
                //    {
                        
                //        this.bannershow.Src = wangzxx.Rows[i]["xxnr"].ToString();
                //    }
                //    else
                //    {
                //        bannershow.Style.Add("display", "none");
                //    }
                //}


            }
        }
        #endregion
        #region 菜单显示
        //菜单
        DataTable menus = Sqlhelper.Serach("SELECT TOP 9 *  FROM [xw_lanm] where sfcdxs='1'  order by px");
        if (menus.Rows.Count > 0)
        {
            this.menushow.InnerHtml = "";
            for (int i = 0; i < menus.Rows.Count; i++)
            {
                string lmmc = basic.ReplaceHtmlTag(menus.Rows[i]["lmmc"].ToString(), 20);
                string urlok = "list.aspx?id=" + menus.Rows[i]["lmid"].ToString();
                if (menus.Rows[i]["url"].ToString().Length > 4) urlok = menus.Rows[i]["url"].ToString();
                string width1 = "";
                //设置菜单宽度
                switch (menus.Rows.Count.ToString())
                {
                    case "8":
                        width1 = "width:12%;";
                        break;
                    case "9":
                        width1 = "width:10.6%;";
                        break;
                    case "10":
                        width1 = "width:9.5%;";
                        break;
                    case "11":
                        width1 = "width:8.6%;";
                        break;
                    case "12":
                        width1 = "width:8%;";
                        break;
                    case "13":
                        width1 = "width:7.2%;";
                        break;
                    case "14":
                        width1 = "width:6.4%;";
                        break;
                    case "15":
                        width1 = "width:6.2%;";
                        break;
                    default:
                        width1 = "";
                        break;


                }
                if (i == 0)
                {
                    this.menushow.InnerHtml += "  <li class=\"nav-current\"  role=\"presentation\"><a href=\"" + urlok + "\"  target=\"" + menus.Rows[i]["dkfs"].ToString() + "\" >" + lmmc + "</a></li>";

                }
                else
                {
                    this.menushow.InnerHtml += "  <li  role=\"presentation\" ><a href=\"" + urlok + "\" target=\"" + menus.Rows[i]["dkfs"].ToString() + "\" >" + lmmc + "</a></li>";

                }
            }
        }
        #endregion

        #region 图片新闻
        DataTable tpxw = Sqlhelper.Serach("SELECT TOP 4 *  FROM [xw_neirong] where isyn='1' and len(images)>4 and readpower like '%00|%' order by fabutime desc");
        if (tpxw.Rows.Count > 0)
        {
            tpshow.InnerHtml = "";
            for (int i = 0; i < tpxw.Rows.Count; i++)
            {
              
                    // <li  style="width: 690px; height: 370px;"><a href="go/to/your/url.html" title="这里是测试标题一"><img  style="width: 714px; height: 370px;" src="img/1.jpg"></a></li>
       tpshow.InnerHtml += " <li  style=\"width: 690px; height: 370px;\"><a href=\"/xw.aspx?xwid="+tpxw.Rows[i]["id"].ToString()+"\" title=\""+tpxw.Rows[i]["title"].ToString()+"\"><img  style=\"width: 714px; height: 370px;\" src=\""+tpxw.Rows[i]["images"].ToString()+"\"></a></li>";
               
            }
           
        }
        #endregion
        #region 热点新闻
        //<b>【热点新闻】</b>
        DataTable rdxw1 = Sqlhelper.Serach("SELECT TOP 1 *  FROM [xw_neirong] where isyn='1' and iszhiding='1' and readpower like '%00|%' order by fabutime desc");
        if (rdxw1.Rows.Count > 0)
        {
            string nr = basic.ReplaceHtmlTag(rdxw1.Rows[0]["content"].ToString(),169);
            
                rdxw.InnerHtml = "<b>【热点新闻】" + rdxw1.Rows[0]["title"].ToString() + "</b><br>&nbsp;&nbsp;&nbsp;&nbsp;" + nr + "...";
                this.read1.InnerHtml = "<a href=\"xw.aspx?xwid=" + rdxw1.Rows[0]["id"].ToString() + "\" class=\"btn btn-default\">阅读全文</a> ";
            
            

        }
        #endregion
        #region 通知公告
        //<b>【通知】</b>
        try
        {
             DataTable toplm = Sqlhelper.Serach("SELECT top 1 *  FROM [xw_lanm] where sfdhxs='1' order by px asc");
             string lmid2="12";
            if (toplm.Rows.Count > 0)
             {
                 lmid2 = toplm.Rows[0]["lmid"].ToString();
                 tztitle.InnerHtml = "<span style=\"float:right;font-size:15px\" ><p><a href=\"/list.aspx?id=" + lmid2 + "\">更多</a></p></span>" + toplm.Rows[0]["lmmc"].ToString();
             }
            
            DataTable indexxw = Sqlhelper.Serach("SELECT TOP 8 *  FROM [xw_neirong] where isyn='1' and ( lmid="+lmid2+") and readpower like '%00|%' order by fabutime desc");
            if (indexxw.Rows.Count > 0)
            {

                tzlist.InnerHtml = "";

                for (int i = 0; i < indexxw.Rows.Count; i++)
                {
                    string time1 = Convert.ToDateTime(indexxw.Rows[i]["fabutime"].ToString()).Month + "月" + Convert.ToDateTime(indexxw.Rows[i]["fabutime"].ToString()).Day + "日";
                    string nr = basic.ReplaceHtmlTag(indexxw.Rows[i]["title"].ToString(), 14);

                    tzlist.InnerHtml += "<span style=\"float:right\">" + time1 + "</span><p><a href=/xw.aspx?xwid=" + indexxw.Rows[i]["id"].ToString() + ">" + nr + "</a></p>";
                }


            }
            else
            {
                tzlist.InnerHtml = "<p>暂无内容!</p>";
            }
        }
        catch { }
        #endregion
        #region 会员单位
        //<b>【通知】</b>
        DataTable hydw = Sqlhelper.Serach("select top 1 * from[wangzxx] where xxgjz='幻灯旁ID'");
        if (hydw.Rows.Count > 0)
        {
            mydivtitle.InnerHtml = "<span style=\"float:right;font-size:15px\"><a href=\"/login.aspx?url=/nradmingl/defaultxs.aspx&sf=xs\">>></a></span>新生网上报到登陆";

        }
        
        #endregion

        //#region 首页新闻
        ////<b>【首页新闻】</b>
        //try
        //{
        //    //首页显示栏目
        //    DataTable toplm = Sqlhelper.Serach("SELECT top 15 *  FROM [xw_lanm] where sftop='1' order by px asc");
        //    if (toplm.Rows.Count > 0)
        //    {
        //        sftopshow.InnerHtml = "";
        //        for (int i = 0; i < toplm.Rows.Count; i++)
        //        {
        //            //框架
                   
        //           // sftopshow.InnerHtml += "<div class=\"Bbox_e fl\">  <div class=\"newItembox\">  <div class=\"newtitle\"> <span><a href=\"list.aspx?id=" + toplm.Rows[i]["lmid"].ToString() + "\">更多</a></span> <em><a href=\"list.aspx?id=" + toplm.Rows[i]["lmid"].ToString() + "\">" + toplm.Rows[i]["lmmc"].ToString() + "</a></em> </div><div class=\"clear\"></div>   <div class=\"cont\">   <ul class=\"arrowllist\"> ";
        //            sftopshow.InnerHtml += " <div class=\"col-sm-4\"><div class=\"widget\"><h4 class=\"title\" ><span style=\"float:right;font-size:15px\"><a href=\"/list.aspx?id=" + toplm.Rows[i]["lmid"].ToString() + "\">更多</a></span> " + toplm.Rows[i]["lmmc"].ToString() + "</h4>  <div class=\"content recent-post\"> ";

        //            DataTable zdxw = Sqlhelper.Serach("SELECT     TOP 8 xw_lanm.lmmc, xw_neirong.title, xw_neirong.id, xw_neirong.LMID, xw_neirong.fabutime, xw_neirong.readpower FROM         xw_neirong LEFT OUTER JOIN       xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where xw_neirong.readpower like '%00|%' and xw_neirong.lmid='" + toplm.Rows[i]["lmid"].ToString() + "' and xw_neirong.isyn='1' order by fabutime desc");
        //            if (zdxw.Rows.Count > 0)
        //            {
        //                //新闻列表      
        //                //新闻列表读取
        //                for (int c = 0; c < zdxw.Rows.Count; c++)
        //                {
        //                    string datatimeok = zdxw.Rows[c]["fabutime"].ToString();
        //                    try
        //                    {
        //                        datatimeok = DateTime.Parse(zdxw.Rows[c]["fabutime"].ToString()).ToString("MM月dd日");

        //                    }
        //                    catch { }
        //                    string title = basic.ReplaceHtmlTag(zdxw.Rows[c]["title"].ToString(), 14);
        //                    string title1 = basic.ReplaceHtmlTag(zdxw.Rows[c]["title"].ToString(), 108);
        //                    // sftopshow.InnerHtml += " <li> <a class=\"linkh\" href=\"xw.aspx?xwid=" + zdxw.Rows[c]["id"].ToString() + "\" title=\"" + title1 + "\" >" + title + " </a> </li>";
        //                    sftopshow.InnerHtml += "<span style=\"float:right;clear:both;\">" + datatimeok + "</span><p><a  href=\"xw.aspx?xwid=" + zdxw.Rows[c]["id"].ToString() + "\" title=\"" + title1 + "\" >" + title + " </a> </p>";
        //                }
        //            }
        //            else
        //            {
        //                sftopshow.InnerHtml += "<p>暂无新闻</p>";
        //            }
        //            sftopshow.InnerHtml += " </div> </div>   </div>";
        //        }
        //    }
        //}
        //catch { }
        //#endregion

        #region 底部链接
        //<b>【首页新闻】</b>
        try
        {
            //首页显示栏目
            DataTable toplm = Sqlhelper.Serach("SELECT top 2 *  FROM [xw_lanm] where sfdown='1' order by px asc");
            if (toplm.Rows.Count > 0)
            {
                footshow.InnerHtml = "";
             
                for (int i = 0; i < toplm.Rows.Count; i++)
                {
                    //框架
                  //  Response.Write(toplm.Rows[i]["lx"].ToString().Trim()+toplm.Rows.Count.ToString()+i.ToString()+"$");

                    if (toplm.Rows[i]["lx"].ToString().Trim() == "图标")
                    {
                       // Response.Write("tb");
                        // sftopshow.InnerHtml += "<div class=\"Bbox_e fl\">  <div class=\"newItembox\">  <div class=\"newtitle\"> <span><a href=\"list.aspx?id=" + toplm.Rows[i]["lmid"].ToString() + "\">更多</a></span> <em><a href=\"list.aspx?id=" + toplm.Rows[i]["lmid"].ToString() + "\">" + toplm.Rows[i]["lmmc"].ToString() + "</a></em> </div><div class=\"clear\"></div>   <div class=\"cont\">   <ul class=\"arrowllist\"> ";
                        footshow.InnerHtml += " <div class=\"zyin_link\"><strong style=\"font-weight: normal;margin-right: 10px;display: inline;float: left;    font-size: 14px;    color:#256ccb; padding-top: 4px;\"><a href=\"list.aspx?id=" + toplm.Rows[i]["lmid"].ToString() + "\">" + toplm.Rows[i]["lmmc"].ToString() + "</a>：</strong>";

                        DataTable zdxw = Sqlhelper.Serach("SELECT     TOP 12 xw_lanm.lmmc, xw_neirong.title, xw_neirong.id, xw_neirong.LMID, xw_neirong.fabutime, xw_neirong.readpower,xw_neirong.xw_img FROM         xw_neirong LEFT OUTER JOIN       xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where xw_neirong.readpower like '%00|%' and xw_neirong.lmid='" + toplm.Rows[i]["lmid"].ToString() + "' and xw_neirong.isyn='1' order by fabutime desc");
                        if (zdxw.Rows.Count > 0)
                        {
                            //新闻列表      
                            //新闻列表读取
                            footshow.InnerHtml += "<p style=\"    display: block;float: left;width: 90%;line-height: 30px; word-break: break-all;text-align:left;\">";
                            for (int c = 0; c < zdxw.Rows.Count; c++)
                            {
                                string datatimeok = zdxw.Rows[c]["fabutime"].ToString();
                                try
                                {
                                    datatimeok = DateTime.Parse(zdxw.Rows[c]["fabutime"].ToString()).ToString("MM月dd日");

                                }
                                catch { }
                                string title = basic.ReplaceHtmlTag(zdxw.Rows[c]["title"].ToString(), 14);
                                string title1 = basic.ReplaceHtmlTag(zdxw.Rows[c]["title"].ToString(), 108);
                                // sftopshow.InnerHtml += " <li> <a class=\"linkh\" href=\"xw.aspx?xwid=" + zdxw.Rows[c]["id"].ToString() + "\" title=\"" + title1 + "\" >" + title + " </a> </li>";
                               // footshow.InnerHtml += "<span style=\"float:right;clear:both;\">" + datatimeok + "</span><p><a  href=\"xw.aspx?xwid=" + zdxw.Rows[c]["id"].ToString() + "\" title=\"" + title1 + "\" >" + title + " </a> </p>";
                                footshow.InnerHtml += " <a href=\"xw.aspx?xwid=" + zdxw.Rows[c]["id"].ToString() + "\"  title=\"" + title1 + "\" target=\"_blank\"><img style=\"width:115px;height:45px;\" src=\"" + zdxw.Rows[c]["xw_img"].ToString() + "\"></a>";
                            }
                            footshow.InnerHtml += "</p><div class=\"clear\"></div>";

                        }
                        else
                        {
                            footshow.InnerHtml += "<p style=\"    display: block;float: left;width: 90%;line-height: 30px; word-break: break-all;text-align:left;\">暂无图片链接</p><div class=\"clear\"></div>";
                        }
                    }
                    else
                    {
                        //Response.Write("!notubiao");
                        footshow.InnerHtml += " <div class=\"zyin_link\"><strong style=\"font-weight: normal;margin-right: 10px;display: inline;float: left;    font-size: 14px;    color:#256ccb; padding-top: 4px;\"><a href=\"list.aspx?id=" + toplm.Rows[i]["lmid"].ToString() + "\">" + toplm.Rows[i]["lmmc"].ToString() + "</a>：</strong>";

                        DataTable zdxw = Sqlhelper.Serach("SELECT     TOP 14 xw_lanm.lmmc, xw_neirong.title, xw_neirong.id, xw_neirong.LMID, xw_neirong.fabutime, xw_neirong.readpower FROM         xw_neirong LEFT OUTER JOIN       xw_lanm ON xw_neirong.LMID = xw_lanm.lmid where xw_neirong.readpower like '%00|%' and xw_neirong.lmid='" + toplm.Rows[i]["lmid"].ToString() + "' and xw_neirong.isyn='1' order by fabutime desc");
                        if (zdxw.Rows.Count > 0)
                        {
                            //新闻列表      
                            //新闻列表读取
                            footshow.InnerHtml += "<p style=\"    display: block;float: left;width: 90%;line-height: 30px; word-break: break-all;text-align:left;\">";
                            for (int c = 0; c < zdxw.Rows.Count; c++)
                            {
                                string datatimeok = zdxw.Rows[c]["fabutime"].ToString();
                                try
                                {
                                    datatimeok = DateTime.Parse(zdxw.Rows[c]["fabutime"].ToString()).ToString("MM月dd日");

                                }
                                catch { }
                                string title = basic.ReplaceHtmlTag(zdxw.Rows[c]["title"].ToString(), 14);
                                string title1 = basic.ReplaceHtmlTag(zdxw.Rows[c]["title"].ToString(), 108);
                                // sftopshow.InnerHtml += " <li> <a class=\"linkh\" href=\"xw.aspx?xwid=" + zdxw.Rows[c]["id"].ToString() + "\" title=\"" + title1 + "\" >" + title + " </a> </li>";
                                // footshow.InnerHtml += "<span style=\"float:right;clear:both;\">" + datatimeok + "</span><p><a  href=\"xw.aspx?xwid=" + zdxw.Rows[c]["id"].ToString() + "\" title=\"" + title1 + "\" >" + title + " </a> </p>";
                                //footshow.InnerHtml += " <a href=\"xw.aspx?xwid=" + zdxw.Rows[c]["id"].ToString() + "\"  title=\"" + title1 + "\" target=\"_blank\"><img src=\"" + zdxw.Rows[c]["images"].ToString() + "\"></a>";
                                footshow.InnerHtml += " <span><a href=\"xw.aspx?xwid=" + zdxw.Rows[c]["id"].ToString() + "\" title=\"" + title1 + "\" target=\"_blank\">" + title + "</a></span>";
                            }
                            footshow.InnerHtml += "</p><div class=\"clear\"></div>";

                        }
                        else
                        {
                            footshow.InnerHtml += "<p style=\"    display: block;float: left;width: 90%;line-height: 30px; word-break: break-all;text-align:left;\">暂无链接</p><div class=\"clear\"></div>";
                        }
                    }
                    footshow.InnerHtml += " </div> ";
                }
            }
            else
            {
                footshow.InnerHtml = "";
            }
        }
        catch (Exception xx) { Response.Write(xx.Message); }
        #endregion

    }
}
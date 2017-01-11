using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
///c_index 的摘要说明
/// </summary>
public class c_index
{
    public string title { get; set; }//网站标题   
    public string metaKeyworkds { get; set; }//网站关键字
    public string metaDescription { get; set; }//网站描述
    public string webcss_Href { get; set; }//风格文件
    public string log_scr { get; set; }//logo图片
    public string leftdh_InnerHtml { get; set; }//左框架默认页
    public string nrdh_InnerHtml { get; set; }//内容框架默认页
    public string topdh_InnerHtml { get; set; }//
    public string menu_InnerHtml { get; set; }//
    public string copyright_InnerHtml { get; set; }//版权信息
    public string icp { get; set; }//备案号
    public bool isopen { get; set; } //网站开关
    public int pagenum { get; set; } //数据库列表记录数
    public c_index(string yhid)
	{
        string sqlStringWangzxx = "SELECT * FROM wangzxx order by xxid";
        string sqlStringLanm = "SELECT * FROM lanm WHERE sfcdxs='1' ORDER BY px";
        string sqlStringTopdh = "SELECT * FROM lanm WHERE sftop='1' ORDER BY px";
        DataTable dtWangzxx = Sqlhelper.Serach(sqlStringWangzxx);
        DataTable dtLanm = Sqlhelper.Serach(sqlStringLanm);
        DataTable dtTopdh = Sqlhelper.Serach(sqlStringTopdh);
        if (dtWangzxx.Rows.Count > 0)
        {
            title = dtWangzxx.Rows[0]["xxnr"].ToString();
            metaKeyworkds = dtWangzxx.Rows[1]["xxnr"].ToString();
            metaDescription = dtWangzxx.Rows[2]["xxnr"].ToString();
            log_scr = dtWangzxx.Rows[3]["xxnr"].ToString();

            leftdh_InnerHtml = "<iframe frameborder=\"0\" ID=\"left\" name=\"left\" src=\"menu.aspx\"></iframe>";
            nrdh_InnerHtml = "<iframe frameborder=\"0\" id=\"main\" name=\"main\"  src=\"" + dtWangzxx.Rows[4]["xxnr"].ToString() + "\"></iframe>";

            copyright_InnerHtml = dtWangzxx.Rows[5]["xxnr"].ToString();
            webcss_Href = dtWangzxx.Rows[6]["xxnr"].ToString();
            icp = dtWangzxx.Rows[7]["xxnr"].ToString();
            isopen = dtWangzxx.Rows[8]["xxnr"].ToString() == "1" ? true : false;
            pagenum = Int32.Parse(dtWangzxx.Rows[8]["xxnr"].ToString());
        }
        else
        {
            title = " ";
            metaKeyworkds = " ";
            metaDescription = " ";
            log_scr = " ";
           
            leftdh_InnerHtml = " ";
            nrdh_InnerHtml = " ";

            copyright_InnerHtml = " ";
            webcss_Href = " ";
            icp = " ";
            isopen = false;
            pagenum = 0;
        }
        if (dtLanm.Rows.Count > 0)
        { 
            for(int i=0;i<dtLanm.Rows.Count;i++)
            {
                //判断是否有权限
                if (new c_login().powerYanzheng(yhid,  dtLanm.Rows[i]["gjz"].ToString(), "浏览", "2")||yhid=="")
                {
                    string xjurl = "";
                    if (dtLanm.Rows[i]["url"].ToString().Length >= 4)
                    {
                        if (dtLanm.Rows[i]["url"].ToString().Substring(0, 4) == "下级目录")
                        {
                            xjurl = dtLanm.Rows[i]["url"].ToString().Substring(4, dtLanm.Rows[i]["url"].ToString().Length - 4);
                        }
                        else
                        {
                            xjurl = dtLanm.Rows[i]["url"].ToString();
                        }
                    }
                    //System.Random r = ;

//int yourandom =  //0到100之间的整型随机数

                    menu_InnerHtml += "<a href=\"" +
                        xjurl + "\" title=\"menu.aspx?maths=" + new Random().Next(10000, 500000) + "&order=" +
                        dtLanm.Rows[i]["dhcdh"].ToString() + "\">" +
                        dtLanm.Rows[i]["lmmc"].ToString() + "</a>";
               
                }
               
            }

        }
        else
        {
            menu_InnerHtml="";
        }
        if(dtTopdh.Rows.Count>0)
        {
            for(int i=0;i<dtTopdh.Rows.Count-1;i++)
            {

                

                    topdh_InnerHtml += "<a href=\"" +
                        dtTopdh.Rows[i]["url"].ToString() + "\" target=\"" +
                        dtTopdh.Rows[i]["dkfs"].ToString() + "\"><img src=\"" +
                        dtTopdh.Rows[i]["lmtp"].ToString() + "\" />" +
                        dtTopdh.Rows[i]["lmmc"].ToString() + "</a>";

            }
            topdh_InnerHtml += "<a href=\"" +
dtTopdh.Rows[dtTopdh.Rows.Count - 1]["url"].ToString() + "\" target=\"" +
dtTopdh.Rows[dtTopdh.Rows.Count - 1]["dkfs"].ToString() + "\" class=\"loginout\"><img src=\"" +
dtTopdh.Rows[dtTopdh.Rows.Count - 1]["lmtp"].ToString() + "\" />" +
dtTopdh.Rows[dtTopdh.Rows.Count - 1]["lmmc"].ToString() + "</a>";
        }
	}
    public c_index(string title,
        string metaKeyworkds,
        string metaDescription,
        string webcss_Href,
        string log_scr,
        string leftdh_InnerHtml,
        string nrdh_InnerHtml,
        string topdh_InnerHtml,
        string menu_InnerHtml,
        string copyright_InnerHtml,
        string icp,
        bool isopen,
        int pagenum)
    {
        this.title = title;
        this.metaKeyworkds = metaKeyworkds;
        this.metaDescription = metaDescription;
        this.webcss_Href = webcss_Href;
        this.log_scr = log_scr;
        this.leftdh_InnerHtml = leftdh_InnerHtml;
        this.nrdh_InnerHtml = nrdh_InnerHtml;
        this.topdh_InnerHtml = topdh_InnerHtml;
        this.menu_InnerHtml = menu_InnerHtml;
        this.copyright_InnerHtml = copyright_InnerHtml;
        this.icp = icp;
        this.isopen = isopen;
        this.pagenum = pagenum;
    }


}
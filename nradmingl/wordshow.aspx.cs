using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net;
using System.IO;

public partial class upload_wordshow : System.Web.UI.Page
{
    protected string serverport = Sqlhelper.serverport;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    //网络路径返回文件名
    private static string GetShorterFileName(string filename)
    {
        int index = filename.LastIndexOf('/');
        return filename.Substring(index + 1, filename.Length - index - 1);

    }
    //本地路径返回文件名
    private static string GetShorterFileName2(string filename)
    {
        int index = filename.LastIndexOf('\\');
        return filename.Substring(index + 1, filename.Length - index - 1);

    }
    //过滤文件名  返回文件夹路劲

    public String ReturnDirectryPath(string directryPath)
    {

        int index = directryPath.LastIndexOf('\\');

        String theresult = directryPath.Substring(0, index);

        return theresult;

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Label1.Text = "";
        this.txtIdea.Text = "";
        string wordname = "";
        try
        {
            //Response.Write("开始上传:");
           wordname = new wordtohtml().wordToHtml(updFile);
           //Response.Write("<br>上传成功："+wordname);
           if (System.IO.File.Exists(wordname.Replace("\\\\", "\\") ))
           {
               //判断是否已经成功转为HTML
              // Response.Write("()成功转换：" + doc + "<br>");
               string wordname1 = wordname.Replace(System.Web.HttpContext.Current.Server.MapPath("~/"), "").Replace("\\", "/");
               //wordname1 = wordname1.Replace("//", "/").Replace("http:/", "http://");
             // Response.Write("<br>"+wordname1);
               //string wordnamex1 = Request.Url.GetLeftPart(UriPartial.Query).ToString() + Request.ApplicationPath.ToString() + wordname1;
               //Response.Write("<br>生成访问地址：" + wordnamex1);
               //上面是访问本地发布端口
              wordname1 = Request.Url.GetLeftPart(UriPartial.Scheme).ToString()+Request.Url.Host.ToString()+":" + serverport.ToString() + Request.ApplicationPath.ToString() + wordname1;
               
               //Response.Write("<br>生成访问地址：" + wordname1);
               #region 处理HTML格式
               if (wordname1.Substring(wordname1.Length - 5, 5) == ".html")
               {
                  // Response.Write("<br>开始访问地址：" + wordname1);
                   string nr = GetPageCode(wordname1, "gb2312");
                   //if (tongc.Rows[i]["tongc"].ToString() == "2")
                   //{
                   //    nr = GetPageCode(wordname1, "UTF-8");
                   //}
                   string wordnameok = GetShorterFileName(wordname1).Replace(".html", "");
                   string wordnameok2 = wordname1.Replace(".html", "").Replace("http://localhost/", "");
                   int startint = 0;
                   startint = nr.IndexOf("<body");
                   string widthstr = "600";
                   if (nr.IndexOf("{size:") > 0)
                   {
                       widthstr = nr.Substring(nr.IndexOf("{size:") + 6, nr.IndexOf(".", nr.IndexOf("{size:")) - nr.IndexOf("{size:") - 6).ToString();
                       // Response.Write("宽度:" + Convert.ToInt32(widthstr).ToString());
                       // this.Label1.Width = Convert.ToInt32(widthstr);
                   }


                   if (startint < 0) startint = 0;
                   //int endint=nr.IndexOf("</body>");
                   //Response.Write("开始位置" + startint.ToString() + "总长度" + nr.Length.ToString()+"网址："+wordname);
                   string neirong = nr.Substring(startint, nr.Length - startint).Replace("<body", "<div><div  ").Replace("</body>", "</div></div>").Replace("</html>", "");
                   // Response.Write("全名<Br>" + wordname1 + "<br>");
                  // Response.Write("文件名<Br>" + wordnameok + "<br>");
                  // Response.Write(wordnameok2 + "<br>");
                   neirong = neirong.Replace(wordnameok, wordnameok2);
                   //改变缩进的显示方式
                   neirong = neirong.Replace("WIDTH: 457.65pt", "WIDTH:100%").Replace("??", "").Replace("TEXT-INDENT", "TEXT-INDENT:30px;te2").Replace("text-indent", "TEXT-INDENT:30px;te2");
                  


                   //附件fjurl
                  // Response.Write(neirong);
                   string fj = "";
                  
                   neirong += fj;
                   //判断是否有图片
                   int flash = 0;
                   string images ="";
                   
                       //如果该新闻有图片
                   string doc2 = wordname.Replace(".html", "");
                   
                       try
                       {

                           if (System.IO.File.Exists(doc2.Replace("\\\\", "\\") + ".001.png"))
                           {
                               //Response.Write("有图");
                              // Response.Write("<br>有图：" + doc2.Replace("\\\\", "\\") + ".001.png" + "<br>");
                               FileInfo file = new FileInfo(doc2.Replace("\\\\", "\\") + ".001.png");
                               long size = file.Length;//文件大小。byte
                              // Response.Write(size.ToString());
                               if (size >= 44000)
                               {
                                   images = wordnameok2 + ".001.png";
                               }
                               else
                               {
                                   FileInfo file2 = new FileInfo(doc2.Replace("\\\\", "\\") + ".002.png");
                                   long size2 = file2.Length;//文件大小。byte
                                   if (size2 >= 44000)
                                   {
                                       images = wordnameok2 + ".002.png";
                                   }
                               }
                           }
                       }
                       catch (Exception ex)
                       {
                          // Response.Write("获取PNG出错:" + ex.Message);
                       }

                       try
                       {
                           if (System.IO.File.Exists(doc2.Replace("\\\\", "\\") + ".001.jpeg"))
                           {
                               //Response.Write("有图");
                              // Response.Write("<br>有图：" + doc2.Replace("\\\\", "\\") + ".001.jpeg" + "<br>");
                               FileInfo file = new FileInfo(doc2.Replace("\\\\", "\\") + ".001.jpeg");
                               long size = file.Length;//文件大小。byte
                              // Response.Write(size.ToString());
                               if (size >= 30000)
                               {
                                   images = wordnameok2 + ".001.jpeg";
                               }

                           }
                           else
                           {
                               FileInfo file2 = new FileInfo(doc2.Replace("\\\\", "\\") + ".002.jpeg");
                               long size2 = file2.Length;//文件大小。byte
                               //Response.Write(size2.ToString());
                               if (size2 >= 30000)
                               {
                                   images = wordnameok2 + ".002.jpeg";
                               }
                           }
                       }
                       catch (Exception ex)
                       {
                           //Response.Write("获取JPG出错:" + ex.Message);
                       }

                   
                   if (images.Length > 4)
                   {
                       flash = 1;
                       Session["image"] = images;
                   }
                   //固定格式
                  // Response.Write("<br>图片：" + images + "<br>");
                   // Response.End();
                   //neirong = " <div style=\"width:" + widthstr + "px;\"><span id=\"Label1\" style=\"display:inline-block;width:" + widthstr + "px;\">" + neirong + "</span></div>";
                  
                   //转换完成
                   txtIdea.Text = neirong;
                       this.Label1.Text = txtIdea.Text;

               }
               #endregion
              // this.Button2.Attributes.Add("onclick", "callback('" + this.txtIdea.Text.Replace("此处显示源代码", "") + "');");
               //this.Button2.Attributes.Add("onclick", "callback('导入成功');");
           }
          


            //D:\jxpjxt\wordTmp\20131214535.docx
           //
            //域名+文件名
        
            //if (wordname.Substring(wordname.Length - 5, 5) == ".html")
            //{
            //    string nr = GetPageCode(wordname, "gb2312");

            //    int startint = 0;
            //    startint = nr.IndexOf("<body");
            //    string widthstr = "600";
            //    if (nr.IndexOf("{size:") > 0)
            //    {
            //        widthstr = nr.Substring(nr.IndexOf("{size:") + 6, nr.IndexOf(".", nr.IndexOf("{size:")) - nr.IndexOf("{size:")-6).ToString();
            //        Response.Write("宽度:" + Convert.ToInt32(widthstr).ToString());
            //        this.Label1.Width = Convert.ToInt32(widthstr); 
            //    }
               
                
            //    if (startint < 0) startint = 0;
            //    //int endint=nr.IndexOf("</body>");
            //    // Response.Write("开始位置" + startint.ToString() + "总长度" + nr.Length.ToString()+"网址："+wordname);
            //    txtIdea.Text = nr.Substring(startint, nr.Length - startint).Replace("<body", "<div style='width:" + widthstr + ";line-height:150%;'><div  ").Replace("</body>", "</div></div>").Replace("</html>", "");
            //    this.Label1.Text = txtIdea.Text;
            //}
        }
        catch (Exception ex)
        {
            wordname = ex.Message;
        }
        //txtIdea.InnerText =GetHTMLCode("http://"+Request.ServerVariables["SERVER_NAME"].ToString()+"/"+wordname);
    }

    // 在说一个小技巧，主要是用来获取网站域名(如http://www.wnweixiu.com)，如果本机测试那就是http://localhost+端口号的形式(如http://localhost:409).

    //之前还总傻到使用Request.Url然后在进行复杂的字符串拼接截取操作，居然都没发现该属性下有个很靠谱的GetLeftPart方法,Request.Url.GetLeftPart(UriPartial.Authority)这样就可以 获取到域名了。

    //Request.ApplicationPath:获取服务器上 ASP.NET 应用程序的虚拟应用程序根路径。



    /// 通过URI读取指向地址的HTML代码
    /// </summary>
    /// <param name="url">URI地址(例如:http://www.wnweixiu.com)</param>
    /// <returns></returns>
    protected string GetHTMLCode(string url)
    {
        try
        {

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            //使用Cookie设置AllowAutoRedirect属性为false,是解决“尝试自动重定向的次数太多。”的核心
            request.CookieContainer = new CookieContainer();
            request.AllowAutoRedirect = false;
            WebResponse response = (WebResponse)request.GetResponse();
            Stream sm = response.GetResponseStream();

            //Stream responseStream = response.GetResponseStream();
            //StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.UTF8);
            //string retext = readStream.ReadToEnd().ToString();


            System.IO.StreamReader streamReader = new System.IO.StreamReader(sm, System.Text.Encoding.Default);
            //将流转换为字符串
            string html = streamReader.ReadToEnd();
            streamReader.Close();
            return html;
        }
        catch (Exception ex)
        {
            return "获取:" + url + "出错：" + ex.Message;
        }
    }
    //获取指定页面的源代码//可解决500的错误，上面的有500的错误
    public String GetPageCode(String PageURL, String Charset)
    {
        String strHtml = "";
        try
        {
            //连接到目标网页
            HttpWebRequest wreq = (HttpWebRequest)WebRequest.Create(PageURL);
            wreq.Method = "GET";
            wreq.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.8) Gecko/20100722 Firefox/3.6.8";
            HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse();
            //采用流读取，并确定编码方式
            Stream s = wresp.GetResponseStream();
            StreamReader objReader = new StreamReader(s, System.Text.Encoding.GetEncoding(Charset));
            strHtml = objReader.ReadToEnd();
            objReader.Close();
            return strHtml;
        }
        catch (WebException ex)
        {
            HttpWebResponse res = ex.Response as HttpWebResponse;

            if (res.StatusCode == HttpStatusCode.InternalServerError)
            {
                Stream s = res.GetResponseStream();
                StreamReader objReader = new StreamReader(s, System.Text.Encoding.GetEncoding(Charset));
                strHtml = objReader.ReadToEnd();
                objReader.Close();
            }
            else
            {
                strHtml = "获取:" + PageURL + "出错：" + ex.Message;
            }
            return strHtml;
        }
    }


    protected void dr(object sender, EventArgs e)
    {
        //Response.Write("<script type=\"text/javascript\" src=\"../files/common/jquery.js\"></script><script>callback('" + this.txtIdea.Text.Replace("此处显示源代码","") + "');</script>");
    }
}

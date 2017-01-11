using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;    

public partial class nradmingl_webapiys : System.Web.UI.Page
{
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

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string base64 = "";
            this.tsxx.Text = "";
            if (TextBox1.Text.Length > 0)
            {
                if (CheckBox1.Checked)
                {
                    base64 = basic.systembase64(this.TextBox2.Text.Trim() + "|" + this.TextBox3.Text.Trim());
                    this.TextBox4.Text = base64;
                }


                
               // byte[] bs = Encoding.ASCII.GetBytes(param);
                string URL = this.TextBox1.Text.Trim();
                string server = "http://www.cdivtc.com/";
                string cookie = "";

                //处理参数
                Encoding encoding = Encoding.GetEncoding(this.TextBox5.Text.Trim());
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                if (cs1.Text.Trim().Length > 0)
                {
                    parameters.Add(cs1.Text.Trim(), zhi1.Text.Trim());
                }
                if (cs2.Text.Trim().Length > 0)
                {
                    parameters.Add(cs2.Text.Trim(), zhi2.Text.Trim());
                }
                if (cs3.Text.Trim().Length > 0)
                {
                    parameters.Add(cs3.Text.Trim(), zhi3.Text.Trim());
                }
                if (cs4.Text.Trim().Length > 0)
                {
                    parameters.Add(cs4.Text.Trim(), zhi4.Text.Trim());
                }
                if (cs5.Text.Trim().Length > 0)
                {
                    parameters.Add(cs5.Text.Trim(), zhi5.Text.Trim());
                }
                if (cs6.Text.Trim().Length > 0)
                {
                    parameters.Add(cs6.Text.Trim(), zhi6.Text.Trim());
                }
                if (cs7.Text.Trim().Length > 0)
                {
                    parameters.Add(cs7.Text.Trim(), zhi7.Text.Trim());
                }
                if (cs8.Text.Trim().Length > 0)
                {
                    parameters.Add(cs8.Text.Trim(), zhi8.Text.Trim());
                }
                if (cs9.Text.Trim().Length > 0)
                {
                    parameters.Add(cs9.Text.Trim(), zhi9.Text.Trim());
                }
                if (cs10.Text.Trim().Length > 0)
                {
                    parameters.Add(cs10.Text.Trim(), zhi10.Text.Trim());
                }
                if (cs11.Text.Trim().Length > 0)
                {
                    parameters.Add(cs11.Text.Trim(), zhi11.Text.Trim());
                }
                if (cs12.Text.Trim().Length > 0)
                {
                    parameters.Add(cs12.Text.Trim(), zhi12.Text.Trim());
                }
                if (cs13.Text.Trim().Length > 0)
                {
                    parameters.Add(cs13.Text.Trim(), zhi13.Text.Trim());
                }
                if (cs14.Text.Trim().Length > 0)
                {
                    parameters.Add(cs14.Text.Trim(), zhi14.Text.Trim());
                }
                if (cs15.Text.Trim().Length > 0)
                {
                    parameters.Add(cs15.Text.Trim(), zhi15.Text.Trim());
                }

                StringBuilder buffer = new StringBuilder();
                int i = 0;
                byte[] bs = encoding.GetBytes("");
                if (TextBox6.Text.Trim() == "json")
                {
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat(",\"{0}\":\"{1}\"", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("\"{0}\":\"{1}\"", key, parameters[key]);
                        }
                        i++;
                    }
                    bs = encoding.GetBytes("{"+buffer.ToString()+"}");
                    //bs = encoding.GetBytes("{\"BH\":\"16070202\"}");
                    this.tsxx.Text+=System.Text.Encoding.GetEncoding(this.TextBox5.Text.Trim()).GetString(bs);
                }
                else
                {
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        }
                        i++;
                    }
                    bs = encoding.GetBytes(buffer.ToString());
                    this.tsxx.Text +="<br>传递参数:"+ System.Text.Encoding.GetEncoding(this.TextBox5.Text.Trim()).GetString(bs);
                }
                long contentLength;
                HttpWebRequest httpWebRequest;
                HttpWebResponse webResponse;
                Stream getStream;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(URL);
                CookieContainer co = new CookieContainer();
                //co.SetCookies(new Uri(server), cookie);
                httpWebRequest.CookieContainer = co;
                if (TextBox6.Text.Trim() == "json")
                {
                    httpWebRequest.ContentType = "application/json";
                }
                else
                {
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                }
               
               
                //httpWebRequest.ContentType = "text/xml";
                httpWebRequest.Headers.Set("Pragma", "no-cache");  
               // httpWebRequest.Accept ="image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
              
                //httpWebRequest.Referer = server;
                httpWebRequest.UserAgent ="Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; Maxthon; .NET CLR 1.1.4322)";
                httpWebRequest.Method = "POST";
                
                if(CheckBox1.Checked)httpWebRequest.Headers.Add("Authorization", base64);

                httpWebRequest.ContentLength = bs.Length;
                Stream stream;
                stream = httpWebRequest.GetRequestStream();
                stream.Write(bs, 0, bs.Length);
                stream.Close();
                webResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                webResponse.Cookies = httpWebRequest.CookieContainer.GetCookies(httpWebRequest.RequestUri);

                CookieCollection cook = webResponse.Cookies;
                //Cookie字符串格式
                string strcrook = httpWebRequest.CookieContainer.GetCookieHeader(httpWebRequest.RequestUri);
                string header = webResponse.Headers.ToString();
                getStream = webResponse.GetResponseStream();
                
                //二进值返回
                contentLength = webResponse.ContentLength;
                byte[] outBytes = new byte[contentLength];
                outBytes = ReadFully(getStream);
               // this.tsxx.Text += header + "@" + outBytes;
                string str = System.Text.Encoding.GetEncoding(this.TextBox5.Text.Trim()).GetString(outBytes);
                this.tsxx.Text +="<br>得到值:<br>"+  str;
                //文本值返回

                //StreamReader streamReader = new StreamReader(getStream, Encoding.GetEncoding(this.TextBox5.Text.Trim()));
                //string getString = streamReader.ReadToEnd();
                //this.tsxx.Text += header + "@" + getString;
                //streamReader.Close();
                //getStream.Close();











               
                //HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(this.TextBox1.Text.Trim());
                //req.Method = "POST";
                //req.Headers.Add("Authorization", this.TextBox4.Text.Trim());
                //req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.8) Gecko/20100722 Firefox/3.6.8";
                //req.ContentType = "application/x-www-form-urlencoded";
                //req.ContentLength = bs.Length;
                //using (Stream reqStream = req.GetRequestStream())
                //{
                //    reqStream.Write(bs, 0, bs.Length);
                //}
                //using (WebResponse wr = req.GetResponse())
                //{
                //    //在这里对接收到的页面内容进行处理
                //    //this.tsxx.Text = wr.ToString();
                //    string header = wr.Headers.ToString();
                //    Stream getStream = wr.GetResponseStream();
                //    StreamReader streamReader = new StreamReader(getStream, Encoding.GetEncoding(this.TextBox5.Text.Trim()));
                //    string getString = streamReader.ReadToEnd();
                //    this.tsxx.Text = header+"@"+getString;
                //    streamReader.Close();
                //    getStream.Close();
                //}
            }
            else
            {
                this.tsxx.Text += "<font color=red>接口地址为空!</font>";
            }
        }
        catch (Exception e2) { this.tsxx.Text += "<br><font color=red>接口出错:" + e2.Message +"</font>"; }
    }
    public static byte[] ReadFully(Stream stream)
    {
        byte[] buffer = new byte[128];
        using (MemoryStream ms = new MemoryStream())
        {
            while (true)
            {
                int read = stream.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                    return ms.ToArray();
                ms.Write(buffer, 0, read);
            }
        }
    }
}
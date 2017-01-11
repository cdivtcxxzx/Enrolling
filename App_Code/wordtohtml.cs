using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
///wordtohtml 的摘要说明
/// </summary>
public class wordtohtml
{
	public wordtohtml()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    

    //上传文件并转换为html wordToHtml(wordFilePath)
    #region 上传文件并转换为html wordToHtml(wordFilePath)
    /**/
    /// 
    /// 上传文件并转存为html
    /// 
    /// word文档在客户机的位置
    /// 上传的html文件的地址
    public string wordToHtml(System.Web.UI.HtmlControls.HtmlInputFile wordFilePath)
    {
         //应当先把文件上传至服务器然后再解析文件为html
        string filePath = uploadWord(wordFilePath);
        //return filePath;
        //判断是否上传文件成功
        if (filePath.Substring(0,1) == "0")
            return "文件上传不成功！" + filePath;
        //判断是否为word文件
        if (filePath == "1")
            return "文件类型不正确";
        //判断WORD是DOC还是DOCX
        if (!System.IO.File.Exists(filePath))
        {
            //如果不存在
            return "文件不存在";
        }
        try
        {
            Aspose.Words.Document awd = new Aspose.Words.Document(filePath);
            string doc2 = filePath.Replace(".docx", "").Replace(".doc", "").Replace(".DOCX", "").Replace(".DOC", "");
            
            awd.Save(doc2 + ".html", Aspose.Words.SaveFormat.Html);
            //if (System.IO.File.Exists(doc2.Replace("\\\\", "\\") + ".html"))
            //{
            //    //string wordname = "http://localhost/"; //注意这个代表网站的端口是80，非80端口要改一下；
            //    // Response.Write("(" + (i + 1).ToString() + ")成功转换：" + doc + "<br>");
            //    string wordname1 = wordname + doc2.Replace("D:\\\\soft\\\\web", "").Replace("\\", "/") + ".html";
            //    wordname1 = wordname1.Replace("//", "/").Replace("http:/", "http://");
            //}
            return (doc2 + ".html");
        }
        catch (Exception ex)
        {
            return "转HTML出错x:" + ex.Message;
        }
    }
    #endregion

    public string uploadWord(System.Web.UI.HtmlControls.HtmlInputFile uploadFiles)
    {
        if (uploadFiles.PostedFile != null)
        {
            string fileName = uploadFiles.PostedFile.FileName;
            int extendNameIndex = fileName.LastIndexOf(".");
            string extendName = fileName.Substring(extendNameIndex);
            string newName = "";
            try
            {
                //验证是否为word格式
                if (extendName == ".doc" || extendName == ".docx"||extendName == ".DOC" || extendName == ".DOCX")
                {

                    DateTime now = DateTime.Now;
                    newName = System.DateTime.Now.Year.ToString()+System.DateTime.Now.Month.ToString() + uploadFiles.PostedFile.ContentLength.ToString();
                    //上传路径 指当前上传页面的同一级的目录下面的wordTmp路径
                    
                   uploadFiles.PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/wordTmp/" + newName + extendName));
                }
                else
                {
                    return "1";
                }
            }
            catch(Exception ex)
            {
                return "0"+ex.Message;
            }
            //return "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/wordTmp/" + newName + extendName;
            return System.Web.HttpContext.Current.Server.MapPath("~/wordTmp/" + newName + extendName);
        }

        else
        {
            return "0";
        }
    }
}
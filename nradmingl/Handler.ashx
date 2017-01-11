<%@ WebHandler Language="C#" Class="UpLoadFile" %>

/*
*  @Author: 	沈爱民
*  @address:  wenzhou
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Text;
using System.Drawing;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;

//[WebService(Namespace = "http://www.cdivtc.com/")]
//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]    


public class UpLoadFile : IHttpHandler, IRequiresSessionState  
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            //string path = @"G:\\Avatar\\Avatar\\Avatar\\images\\";
            //string path = System.Web.HttpContext.Current.Server.MapPath("./") + "/images/";
            //string fileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".jpg";
           // string sfzh = context.Session["sfzname"].ToString();   //传入文件名
            string fileName = "身份证错误.jpg";
            DataTable xues = new DataTable();
            string xm = "";
            if (context.Session["sfzjh"] != null)
            {
                fileName = context.Session["sfzjh"].ToString() + ".jpg";
                //获取院系及班级,姓名
                xues = Sqlhelper.Serach("select yxdm,bjdm,xm from xuesjbsj where sfzjh='" + context.Session["sfzjh"].ToString() + "'");
                if (xues.Rows.Count > 0)
                {
                    xm = xues.Rows[0]["xm"].ToString();
                }
                
            }
            else
            {
                context.Session["sfzjh"]="11111111111111111";
            }
 //           string yxdmmc = context.Session["yxdm"].ToString();
            //文件名
            Stream sin = context.Request.InputStream;
            System.Drawing.Image img = System.Drawing.Bitmap.FromStream(sin);
            Bitmap bmp = new Bitmap(img);
            MemoryStream bmpStream = new MemoryStream();

            //其它调用
            string filename2 = System.Guid.NewGuid().ToString() + ".jpg";

            //Server.MapPath("~/");//返回与Web服务器上的指定的虚拟路径相对的物理文件路径
            

            string bigdir = System.Web.HttpContext.Current.Server.MapPath("~/") + "/admin/xszp/";
            //改变文件夹位置

            if (!System.IO.Directory.Exists(bigdir))
            {
                //Scripting.FileSystemObjectClass fso = new Scripting.FileSystemObjectClass();
                //fso.CreateFolder(bigdir);
                System.IO.Directory.CreateDirectory(bigdir);
            }
            //bmp.Save(bigdir + "/" + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            //其它调用结束

            bmp.Save(bmpStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            FileStream fs = new FileStream(bigdir + "/" + filename2, FileMode.Create);
            bmpStream.WriteTo(fs);
            bmpStream.Close();
            fs.Close();
            bmpStream.Dispose();
            fs.Dispose();
            if (Sqlhelper.ExcuteNonQuery("update xuesjbsj set zp='xszp/" + filename2 + "' where sfzjh='" + context.Session["sfzjh"].ToString() + "'") > 0)
            {
                if (System.IO.File.Exists(bigdir + "/" + filename2))
                {
                    context.Response.Write("true#" + xm + "照像成功!");
                }
                else
                {
                    context.Response.Write("true#" + xm + "照像失败!服务器上保存照片失败!");
                }
            }
            else
            {
                context.Response.Write("true#" + xm + "出错了:照像失败!写数据库不成功!请重试!!!!");
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("照相出错了：" + ex.ToString());
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
    
 }


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Security.Cryptography;


namespace Web
{
    /// <summary> 
    /// 单文件上传类 (暂时不支持多文件上传) 
    /// yangyunzhou@foxmail.com 
    /// </summary> 
    public class UploadFile
    {
        /// <summary> 
        /// 上传文件信息 (动态数组) 
        /// </summary> 
        public Dictionary<string, dynamic> FileInfo = new Dictionary<string, dynamic>();

        /// <summary> 
        /// 最大文件大小 
        /// </summary> 
        public int FileSize = 10240;

        /// <summary> 
        /// 文件保存路径 
        /// </summary> 
        public string FilePath = "upload";

        /// <summary> 
        /// 允许上传的文件类型, 逗号分割,必须全部小写! 
        ///  
        /// 格式: ".gif,.exe" 或更多 
        /// </summary> 
        public string FileType = ".jpg,.gif,.png,.xls";

        /// <summary> 
        /// 上传错误 
        /// </summary> 
        public bool Error;

        /// <summary> 
        /// 消息 
        /// </summary> 
        public string Message;

        /// <summary> 
        /// 保存文件 
        /// </summary> 
        /// <param name="FormField">表单文件域名称</param> 
        ///  <param name="filename1">文件类别</param> 
        ///  <param name="username">谁传的</param> 
        public void Save(string FormField,string filename1,string username)
        {
            var Response = HttpContext.Current.Response;
            var Request = HttpContext.Current.Request;

            // 获取上传的文件 
            HttpFileCollection File = Request.Files;
            HttpPostedFile PostFile = File[FormField];

            // 验证格式 
            if (this.CheckingType(PostFile.FileName,username))
            {

                // 获取存储目录 
                var Path = this.GetPath(filename1, username);
                var dir = Path + this.FileInfo["Name"];

                // 注册文件信息 
                this.FileInfo.Add("path", Path + this.FileInfo["Name"]);
                this.FileInfo.Add("filepath", this.FileInfo["dir"] + this.FileInfo["Name"]);

                // 保存文件 
                PostFile.SaveAs(dir);
            }
        }

        /// <summary> 
        /// 获取目录 
        /// </summary> 
        /// <returns></returns> 
        private string GetPath(string file1,string user1)
        {
            // 存储目录 
            string Path = this.FilePath;

            // 目录格式 
            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            string dir = HttpContext.Current.Server.MapPath(Path + "/" + file1);

            // 注册文件信息 
           this.FileInfo.Add("dir", Path + "/"+file1+"/");

            // 创建目录 
            if (Directory.Exists(dir) == false)
                Directory.CreateDirectory(dir);
            return dir + '/';
        }

        /// <summary> 
        /// 验证文件类型 
        /// </summary> 
        /// <param name="FileName"></param> 
        private bool CheckingType(string FileName,string user1)
        {
            // 获取允许允许上传类型列表 
            string[] TypeList = this.FileType.Split(',');
            string Date = DateTime.Now.ToString("yyyy-MM-dd");
            // 获取上传文件类型(小写) 
            string Type = Path.GetExtension(FileName).ToLowerInvariant();
            string Name = Path.GetFileNameWithoutExtension(FileName);
            string NameHash = Name.GetHashCode().ToString();

            // 注册文件信息 
            this.FileInfo.Add("name", Name);
            this.FileInfo.Add("Name", Date+user1 + Type);
            this.FileInfo.Add("type", Type);

            // 验证类型 
            if (TypeList.Contains(Type) == false)
            {
                this.TryError("文件类型非法!");
                return false;
            }
            return true;
        }

        /// <summary> 
        /// 抛出错误 
        /// </summary> 
        /// <param name="Msg"></param> 
        public void TryError(string Msg)
        {
            this.Error = true;
            this.Message = Msg;
        }
    }
}

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;
using System.Data;

public partial class admin_doctoswf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private string sywj = "";
    
    /// <summary> 
    /// 执行进程 
    /// </summary> 
    /// <param name="sourceFileName">输入.doc文件路径</param> 
    /// <param name="outPutFileName">输出.swf文件路径</param> 
    public void ProcessExec(string sourceFileName, string outPutFileName,int ddsj)
    {
        //验证文件是否存在

        if (this.TextBox12.Text == "1"&&File.Exists(outPutFileName))
        {
            this.Label1.Text = this.Label1.Text + ii.ToString() + ")文件存在:" + outPutFileName + "<br><br>";
        }
        else
        {


            ii = ii + 1;
            //FlashPaper文件安装路径 可自行设置 
            string flashPrinter = "E:\\xtje\\FlashPrinter.exe";
            Process pss = new Process();
            pss.StartInfo.CreateNoWindow = false;
            pss.StartInfo.FileName = flashPrinter;
            pss.StartInfo.Arguments = string.Format("{0} {1} -o {2}", flashPrinter, sourceFileName, outPutFileName);

            pss.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardInput = true;
            pss.StartInfo.UseShellExecute = false; //不使用系统外壳程序启动  
            //startInfo.RedirectStandardInput = false; //不重定向输入  
            //startInfo.RedirectStandardOutput = false; //重定向输出  

            pss.StartInfo.RedirectStandardInput = false;
            pss.StartInfo.RedirectStandardOutput = false;
            pss.StartInfo.RedirectStandardError = true;

            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardError = true;
            pss.StartInfo.CreateNoWindow = true;
            // 创建一个进程。 
            //System.Diagnostics.Process p = new System.Diagnostics.Process(); 
            //p.StartInfo.FileName = appPath;  // 赋值进程要执行的应用程序。 
            //p.StartInfo.Arguments = param;  // 赋值应用程序可用参数。 
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardInput = false; 
            //p.StartInfo.RedirectStandardOutput = false;
            //p.StartInfo.RedirectStandardError = true; 
            //p.StartInfo.CreateNoWindow = true;  // 是否创建显示窗口。 
            try
            {
                bool IsStart = pss.Start();  // 开始执行程序，如果执行成功返回True，否则False。 
                //pss.WaitForExit();  // 等待关联进程退出。  
                //pss.Close();  // 关闭进程。 
                while (!pss.HasExited)
                {
                    continue;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //Response.Write("完成。");  
            if (this.TextBox12.Text == "1")
            {
                System.Threading.Thread.Sleep(ddsj+5000);
            }
            else
            {
                System.Threading.Thread.Sleep(ddsj);
            }

            try
            {
                //bool IsStart = pss.Start();
                //// pss.WaitForExit();  // 等待关联进程退出。  
                ////pss.Close();  // 关闭进程。 
                //while (!pss.HasExited)
                //{
                //    continue;
                //}

                //
                if (sywj == "")
                {
                }
                else
                {
                    if (File.Exists(sywj))
                    {
                        this.Label1.Text = this.Label1.Text + ii.ToString() + ")转换成功:" + sourceFileName + "<br><br>";
                        this.TextBox2.Text = this.TextBox2.Text + "," + outPutFileName;
                    }
                    else
                    {
                        this.Label1.Text = this.Label1.Text + ii.ToString() + ")<font color=red>转换失败</font>:" + sourceFileName + "<br><br>";
                        this.TextBox2.Text = this.TextBox2.Text + "," + outPutFileName;
                    }
                }
                sywj = outPutFileName;
            }
            catch (Exception ex)
            {
                //this.Label1.Text = this.Label1.Text+ii.ToString() + ")正在转换:" + sourceFileName + "   转换出错!" + ex.Message + "<br>"+outPutFileName+"<br>"  ;
                //throw ex;
            }
        }
    }
    /// <summary> 
    /// 执行进程 
    /// </summary> 
    /// <param name="sourceFileName">输入.doc文件路径</param> 
    /// <param name="outPutFileName">输出.swf文件路径</param> 
    public void ProcessExec2(string sourceFileName, string outPutFileName, int ddsj)
    {
        //验证文件是否存在

        if (File.Exists(outPutFileName))
        {
            this.Label1.Text = this.Label1.Text + ii.ToString() + ")文件存在:" + outPutFileName + "<br><br>";
        }
        else
        {
            this.Label1.Text = this.Label1.Text + ii.ToString() + ")<font color=red>文件不存在</font>:" + outPutFileName + "<br><br>";
            this.TextBox2.Text = this.TextBox2.Text + sourceFileName + "\r\n\r\n";
        }
        
        
    }
    private int ii=0;
    protected void Button1_Click(object sender, EventArgs e)
    {

        this.Label1.Text = "<br>";
        getdoc(Server.MapPath("~/"+this.TextBox1.Text.Trim()));
    }

    /// <summary>  

    /// 循环遍历获得某一目录下的所有文件信息


    /// </summary>  

    /// <param name="path">目录名</param> 

    /// <param name="tn">树节点</param>  

    private void getdoc(string path)
    {

        string[] fileNames = Directory.GetFiles(path);

        string[] directories = Directory.GetDirectories(path);



        //先遍历这个目录下的文件夹


        foreach (string dir in directories)
        {


            getdoc(dir);

           // tn.ChildNodes.Add(subtn);

        }


        //再遍历这个目录下的文件

 
        foreach (string file in fileNames)
        {

       

            //如果是符合要求的文件则垒加集合,因为我只要求显示图片文件，在checkFileType方法里定义要显示文件的扩展名
            //if (checkFileType(FSI.Extension))
            //{
            //    //由于是物理路径，如e:/luobing_web/uploadfiles/picture/test.jpg这种形式，需要提取虚拟路径，如：../uploadfiles/picture/test.jpg
            //    string FilePath = ""; //一步写来看起混乱，就分开写了
            //    FilePath = FSI.FullName.ToLower();
            //    FilePath = FilePath.Substring(FilePath.LastIndexOf("sfxys\\"));
            //    FilePath = "~/sfxjs/" + FilePath.Replace("//", "/");//这里在路径前加了../，因为我的项目里页面文件和上传文件夹不是同级文件夹
            //    File_List += FilePath + ",";
            //}

            //{
            if (TextBox3.Text.Length > 1 || TextBox4.Text.Length > 1)
            {
                if (GetShorterFileName(file).LastIndexOf(TextBox3.Text) > 0 || GetShorterFileName(file).LastIndexOf(TextBox4.Text) > 0)
                {
                    string FilePath = file;//一步写来看起混乱，就分开写了
                    string OutPath = file.Substring(0, file.Length - 4) + ".swf";

                    //开始转换
                    ProcessExec(FilePath, OutPath,100);
                }

            }
            if (TextBox5.Text.Length > 1 || TextBox5.Text.Length > 1)
            {
                if (GetShorterFileName(file).LastIndexOf(TextBox5.Text) > 0 || GetShorterFileName(file).LastIndexOf(TextBox6.Text) > 0)
                {
                    string FilePath = file;//一步写来看起混乱，就分开写了
                    string OutPath = file.Substring(0, file.Length - 4) + ".swf";

                    //开始转换
                    ProcessExec(FilePath, OutPath,100);
                }

            }
            if (TextBox7.Text.Length > 1 || TextBox8.Text.Length > 1)
            {
                if (GetShorterFileName(file).LastIndexOf(TextBox7.Text) > 0 || GetShorterFileName(file).LastIndexOf(TextBox8.Text) > 0)
                {
                    string FilePath = file;//一步写来看起混乱，就分开写了
                    string OutPath = file.Substring(0, file.Length - 4) + ".swf";

                    //开始转换
                    ProcessExec(FilePath, OutPath,100);
                }

            }
            if (TextBox9.Text.Length > 1 || TextBox10.Text.Length > 1)
            {
                if (GetShorterFileName(file).LastIndexOf(TextBox9.Text) > 0 || GetShorterFileName(file).LastIndexOf(TextBox10.Text) > 0)
                {
                    string FilePath = file;//一步写来看起混乱，就分开写了
                    string OutPath = file.Substring(0, file.Length - 4) + ".swf";

                    //开始转换
                    ProcessExec(FilePath, OutPath,10000);
                }

            }
            //if (GetShorterFileName(file).LastIndexOf(".pdf") > 0 || GetShorterFileName(file).LastIndexOf(".PDF") > 0||GetShorterFileName(file).LastIndexOf(".ppt") > 0 || GetShorterFileName(file).LastIndexOf(".PPT") > 0||GetShorterFileName(file).LastIndexOf(".doc") > 0 || GetShorterFileName(file).LastIndexOf(".DOC") > 0 || GetShorterFileName(file).LastIndexOf(".xls") > 0 || GetShorterFileName(file).LastIndexOf(".XLS") > 0)
            //{


            //    string FilePath = file;//一步写来看起混乱，就分开写了
            //    string OutPath = file.Substring(0, file.Length - 4)+".swf";
                
            //   //开始转换
            //    ProcessExec(FilePath,OutPath);

            //}

        }

    }

    /// 循环遍历获得某一目录下的所有文件信息


    /// </summary>  

    /// <param name="path">目录名</param> 

    /// <param name="tn">树节点</param>  

    private void getdoc2(string path)
    {

        string[] fileNames = Directory.GetFiles(path);

        string[] directories = Directory.GetDirectories(path);



        //先遍历这个目录下的文件夹


        foreach (string dir in directories)
        {


            getdoc2(dir);

            // tn.ChildNodes.Add(subtn);

        }


        //再遍历这个目录下的文件


        foreach (string file in fileNames)
        {



            //如果是符合要求的文件则垒加集合,因为我只要求显示图片文件，在checkFileType方法里定义要显示文件的扩展名
            //if (checkFileType(FSI.Extension))
            //{
            //    //由于是物理路径，如e:/luobing_web/uploadfiles/picture/test.jpg这种形式，需要提取虚拟路径，如：../uploadfiles/picture/test.jpg
            //    string FilePath = ""; //一步写来看起混乱，就分开写了
            //    FilePath = FSI.FullName.ToLower();
            //    FilePath = FilePath.Substring(FilePath.LastIndexOf("sfxys\\"));
            //    FilePath = "~/sfxjs/" + FilePath.Replace("//", "/");//这里在路径前加了../，因为我的项目里页面文件和上传文件夹不是同级文件夹
            //    File_List += FilePath + ",";
            //}

            //{
            if (TextBox3.Text.Length > 1 || TextBox4.Text.Length > 1)
            {
                if (GetShorterFileName(file).LastIndexOf(TextBox3.Text) > 0 || GetShorterFileName(file).LastIndexOf(TextBox4.Text) > 0)
                {
                    string FilePath = file;//一步写来看起混乱，就分开写了
                    string OutPath = file.Substring(0, file.Length - 4) + ".swf";

                    //开始转换
                    ProcessExec2(FilePath, OutPath, 100);
                }

            }
            if (TextBox5.Text.Length > 1 || TextBox5.Text.Length > 1)
            {
                if (GetShorterFileName(file).LastIndexOf(TextBox5.Text) > 0 || GetShorterFileName(file).LastIndexOf(TextBox6.Text) > 0)
                {
                    string FilePath = file;//一步写来看起混乱，就分开写了
                    string OutPath = file.Substring(0, file.Length - 4) + ".swf";

                    //开始转换
                    ProcessExec2(FilePath, OutPath, 100);
                }

            }
            if (TextBox7.Text.Length > 1 || TextBox8.Text.Length > 1)
            {
                if (GetShorterFileName(file).LastIndexOf(TextBox7.Text) > 0 || GetShorterFileName(file).LastIndexOf(TextBox8.Text) > 0)
                {
                    string FilePath = file;//一步写来看起混乱，就分开写了
                    string OutPath = file.Substring(0, file.Length - 4) + ".swf";

                    //开始转换
                    ProcessExec2(FilePath, OutPath, 100);
                }

            }
            if (TextBox9.Text.Length > 1 || TextBox10.Text.Length > 1)
            {
                if (GetShorterFileName(file).LastIndexOf(TextBox9.Text) > 0 || GetShorterFileName(file).LastIndexOf(TextBox10.Text) > 0)
                {
                    string FilePath = file;//一步写来看起混乱，就分开写了
                    string OutPath = file.Substring(0, file.Length - 4) + ".swf";

                    //开始转换
                    ProcessExec2(FilePath, OutPath, 10000);
                }

            }
            //if (GetShorterFileName(file).LastIndexOf(".pdf") > 0 || GetShorterFileName(file).LastIndexOf(".PDF") > 0||GetShorterFileName(file).LastIndexOf(".ppt") > 0 || GetShorterFileName(file).LastIndexOf(".PPT") > 0||GetShorterFileName(file).LastIndexOf(".doc") > 0 || GetShorterFileName(file).LastIndexOf(".DOC") > 0 || GetShorterFileName(file).LastIndexOf(".xls") > 0 || GetShorterFileName(file).LastIndexOf(".XLS") > 0)
            //{


            //    string FilePath = file;//一步写来看起混乱，就分开写了
            //    string OutPath = file.Substring(0, file.Length - 4)+".swf";

            //   //开始转换
            //    ProcessExec(FilePath,OutPath);

            //}

        }

    }
    /// <summary>  

    /// 滤去文件名前面的路径


    /// </summary>  

    /// <param name="filename"></param> 

    /// <returns></returns>  

    private static string GetShorterFileName(string filename)
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        this.Label1.Text = "";
        string[] xtje = this.TextBox2.Text.ToString().Split(',');
        this.TextBox2.Text = "";
        foreach (string strLsz in xtje)
        {
            if (File.Exists(strLsz))
            {
                this.Label1.Text = this.Label1.Text + ii.ToString() + ")转换成功:" + strLsz + "<br><br>";
                //this.TextBox2.Text = this.TextBox2.Text + "," + outPutFileName;
            }
            else
            {
                this.Label1.Text = this.Label1.Text + ii.ToString() + ")<font color=red>转换失败</font>:" + strLsz + "<br><br>";
                this.TextBox2.Text = this.TextBox2.Text + "," + strLsz+"\r\n";
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        this.Label1.Text = "<br>";
        getdoc2(Server.MapPath("~/" + this.TextBox1.Text.Trim()));
    }
}
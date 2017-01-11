using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.IO;


using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;

//using Common;
//using Data; 


/// <summary>
///basic 的摘要说明
///此类用于存方一些页面基础处理类,一些简单的类
/// </summary>
public class basic
{
    /// <summary>
    /// 64位BASE加密
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string base64_encode(string str)
    {
        byte[] temp1 = Encoding.UTF8.GetBytes(str);
        string temp2 = Convert.ToBase64String(temp1);
        return temp2;
    }
    /// <summary>
    /// 64位BASE解密
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string base64_dep(string str)
    {
        // byte[] temp1 = System.Text.Encoding.UTF8.GetBytes(str);
        //string temp2 =  Convert.ToBase64CharArray(temp1);
        //return temp2;

        try
        {
            string strPath = str;
            byte[] bpath = Convert.FromBase64String(strPath);
            strPath = System.Text.ASCIIEncoding.UTF8.GetString(bpath);
            return strPath;
        }
        catch (Exception e1)
        {
            return "解码失败："+e1.Message;
        }

    }
    /// <summary>
    /// 32位MD5加密
    /// </summary>
    /// <param name="input">需加密字符串</param>
    /// <returns>加密后字符串</returns>
    private static string Md5Hash(string input)
    {
        //与Response.Write(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("需加密串","MD5").ToLower());//相同
        System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] data = md5Hasher.ComputeHash(System.Text.Encoding.Default.GetBytes(input));
        System.Text.StringBuilder sBuilder = new System.Text.StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();
    }
    /// <summary>
    /// Base64标准加密
    /// </summary>
    /// <param name="input">需加密字符串</param>
    /// <returns>加密后字符串</returns>
    public static string systembase64(string username)
    {
      
        return base64_encode(username);//返回加密后的字符串 
    }

    /// <summary>
    /// 32位MD5加密
    /// </summary>
    /// <param name="input">需加密字符串</param>
    /// <returns>加密后字符串</returns>
    public static string systemmd5(string username)
    {
        //自己编写的MD5加密
        return Md5Hash(username);//返回加密后的字符串 
    }

    private static string time14()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss");//返回14位的日期时间标准字符 
    }
    /// <summary>
    /// 授权验证加密
    /// </summary>
    /// <param name="input">需加密字符串</param>
    /// <returns>加密后字符串</returns>
    public static string mymd5(string username)
    {
        //自己编写的MD5加密,用于授权
        return Md5Hash(username + "zhangming") + Md5Hash(username.Length.ToString() + "4615914");//返回加密后的字符串 
    }
    /// <summary>
    /// 返回14位日期时间字符串,单点登录时间
    /// </summary>
    /// <returns>返回14位日期时间字符串</returns>
public static string WebTime()
    {
        
        return DateTime.Now.ToString("yyyyMMddHHmmss");//返回14位的日期时间标准字符
    }
/// <summary>
/// 单点登录密钥
/// </summary>
/// <param name="username">用户登录名</param>
/// <param name="username">14位日期时间字符串</param>
/// <returns>加密后的密钥</returns>
    public static string WebPwd(string username,string time14)
    {
        //单点登录密钥
        return Md5Hash(username + "cdivtc" +time14);//返回加密后的字符串 
    }

    //该方法已经放弃使用//
    public static string mymd5m(string username)
    {
        //自己编写的MD5加密1月试用验证
        string d1 = DateTime.Now.Year.ToString() +"@"+ DateTime.Now.Month.ToString();
        string d2 = DateTime.Now.Year.ToString() + "@" + DateTime.Now.Month;
        if (DateTime.Now.Day >= 15)
        {
           d2 = DateTime.Now.Year.ToString() + (DateTime.Now.Month + 1).ToString();
        }
        return Md5Hash(d1 + "zhangming") + Md5Hash(d1.Length.ToString() + "4615914") + "@" + Md5Hash(d2 + "zhangming") + Md5Hash(d2.Length.ToString() + "4615914");//返回加密后的字符串 
    }
    /// <summary>
    /// 系统是否授权验证
    /// </summary>
    /// <returns>yes:验证通过 sy:验证试用通过 no:未授权</returns>
    public static string sqyz()
    {
        //系统授权验证
        string syssq = "";
        string sq = HttpContext.Current.Request.Url.Host.ToString();
        DataTable sysd = Sqlhelper.Serach("SELECT TOP 1 *  FROM [wangzxx] where xxgjz='系统授权' order by xxid asc");
        if (sysd.Rows.Count > 0)
        {
            syssq = sysd.Rows[0]["xxnr"].ToString().Trim();
            
            sq = Md5Hash(sq + "zhangming") + Md5Hash(sq.Trim().Length.ToString() + "4615914");

        }
        if (syssq.Contains(sq))
        {
            return "yes";
            //验证通过  
        }
        else
        {
            string d1 = DateTime.Now.Year.ToString() + "@" + DateTime.Now.Month;
            sq = Md5Hash(d1 + "zhangming") + Md5Hash(d1.Trim().Length.ToString() + "4615914");
            if (syssq.Contains(sq))
            {
                return "sy";
                //验证试用通过  
            }
            else
            {
                return "no";
            }
        }
        
    }

    /// <summary>
    /// SQL注入检测
    /// </summary>
    /// <returns>检测结果</returns>
    public static string clearsqljc(string bt)
    {
        string jgfh = "结果";
        string zrsql="";
          try
        {
            if (bt.Length>0)
            {
                DataTable cx = Sqlhelper.zsglSerach("SELECT *  FROM [sqlzrjc] where lmmc<>'SQL注入检测' or gjz<>'SQL注入检测'");
                if (cx.Rows.Count > 0)
                {
                    string jcjg = "";
                    int t = 0;
                    for (int i = 0; i < cx.Rows.Count; i++)
                    {

                        if (t <= 100)
                        {
                            if (cx.Rows[i]["lmmc"].ToString().Length > 50 || cx.Rows[i]["gjz"].ToString().Length > 50)
                            {
                                string zh = "";
                                if (cx.Rows[i]["lmmc"].ToString().Length > cx.Rows[i]["gjz"].ToString().Length)
                                {
                                    try
                                    {
                                        if (cx.Rows[i]["lmmc"].ToString().Substring(0, 4) == cx.Rows[i]["gjz"].ToString().Substring(0, 4))
                                        {
                                            // zh = cx.Rows[i]["lmmc"].ToString();
                                            if (cx.Rows[i]["gjz"].ToString().Length > 50)
                                            { }
                                            else
                                            {
                                                zh = cx.Rows[i]["lmmc"].ToString().Replace(cx.Rows[i]["gjz"].ToString(), "");
                                            }
                                        }
                                    }
                                    catch { }
                                }
                                else
                                {
                                    try
                                    {
                                        if (cx.Rows[i]["lmmc"].ToString().Substring(0, 4) == cx.Rows[i]["gjz"].ToString().Substring(0, 4))
                                        {
                                            //zh = cx.Rows[i]["gjz"].ToString();
                                            if (cx.Rows[i]["lmmc"].ToString().Length > 50)
                                            { }
                                            else
                                            {
                                                zh = cx.Rows[i]["gjz"].ToString().Replace(cx.Rows[i]["lmmc"].ToString(), "");
                                            }
                                        }
                                    }
                                    catch { }
                                }
                                if (zh.Length > 0)
                                {
                                    jcjg += zh;
                                    jgfh += zh;
                                    zrsql+=zh+"|||";

                                   //清除驻入
#region clear
         try
        {
            int sl = 0;
            int sb=0;
            string sbjl = "";
            DataTable x = Sqlhelper.zsglSerach("SELECT Name FROM " + bt + "..SysObjects where  XType='U'  ORDER BY Name");
            if (x.Rows.Count > 0)
            {
                //jgfh += "SELECT Name FROM " + bt + "..SysObjects where  XType='U'  ORDER BY Name";
               // show.InnerHtml = "获取到数据库" + bt + "的表结构：<br>";
                for (int ix = 0; ix < x.Rows.Count; ix++)
                {
                }


                for (int ip1 = 0; ip1 < x.Rows.Count; ip1++)
                {
                   // show.InnerHtml += "处理表（" + i.ToString() + "）：" + x.Rows[i][0].ToString() + "<br>";
                   // Response.Write("处理表（" + i.ToString() + "）：" + x.Rows[i][0].ToString() + "<br>");

                    //jgfh += "处理表（" + i.ToString() + "）：" + x.Rows[i][0].ToString() + x.Rows.Count.ToString() + "<br>";
                    //获取该表所有字段
                    //select name from syscolumns where id=object_id('xw_neirong') 
                    DataTable y = Sqlhelper.zsglSerach("select name from syscolumns where id=object_id('"+bt+".[dbo]." + x.Rows[ip1][0].ToString() + "')");
                    if (y.Rows.Count > 0)
                    {
                          for (int i1 = 0; i1 < y.Rows.Count; i1++)
                        {
                            //jgfh += "select name from syscolumns where id=object_id('" + bt + ".[dbo]." + x.Rows[ip1][0].ToString() + "')";

                      
                            string setok = " set ";
                            
                            // Response.Write(y.Rows[i1][0].ToString()+"<br>");
                            if (y.Rows[i1][0].ToString() != "id")
                            {

                                setok += "" + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + zh + "','')";
                               
                                if (x.Rows[ip1][0].ToString() != "sqllog")
                                {
                                   // Response.Write("替换" + x.Rows[i][0].ToString() + "的“" + y.Rows[i1][0].ToString() + "”字段<br>");
                                    //从数据库中读取替换
                                    


                                    //替换开始
                                    //替换
                                    if (bt.Length>0)
                                    {
                                       // Response.Write("UPDATE [" + x.Rows[i][0].ToString() + "]  " + setok + "<br>");
                                        try
                                        {
                                            if (Sqlhelper.zsglExcuteNonQuery("UPDATE " + bt + ".[dbo].[" + x.Rows[ip1][0].ToString() + "]  " + setok) > 0)
                                            {
                                                //jgfh+="UPDATE " + bt + ".[dbo].[" + x.Rows[ip1][0].ToString() + "]  " + setok;
                                                sl = sl + 1;
                                            }
                                            else
                                            {
                                                sb = sb + 1;
                                                sbjl += "<br>" + "UPDATE " + bt + ".[dbo].[" + x.Rows[ip1][0].ToString() + "]  " + setok;
                                            }
                                           
                                        }
                                        catch (Exception ex1)
                                        {
                                           //return "清除SQL注入时出错"+ex1.Message;
                                        }
                                    }
                                }
                            }
                            //替换结束

                        }


                       jgfh+= "清除注入" + sl.ToString() + "条数据，失败" + sb.ToString() + "条！<br>";
                    }
                    else
                    {
                       //Response.Write("<br>no find:" + "select name from syscolumns where id=object_id('" + bt + ".[dbo]." + x.Rows[ip1][0].ToString() + "')<br>");
                    }
                }

            }
        }
        catch (Exception ex)
        {
            return "清除时出错："+ex.Message;
        }
#endregion
                                    zh = "";
                                    t = t + 1;

                                }
                            }
                        }
                    }

                    if (jcjg.Length > 0)
                    {
                        //写入数据库
                        try
                        {


                            //获得当前IP
                            string ipok = HttpContext.Current.Request.UserHostAddress;
                            //HttpContext.Current.Request.Url.Authority +"|"+ HttpContext.Current.Request.Url;
                            //获得当前用户
                            string username = "匿名";
                            try
                            {
                                username = HttpContext.Current.Session["username"].ToString();

                            }
                            catch { }
                            string khd = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();
                            //写入数据库
                            try
                            {
                                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection("Data Source=10.35.10.53;Initial Catalog=gzyuan; User ID=zhangming1;Password=FY$UV#57t7dgs"))
                                {
                                    conn.Open();
                                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan],[khd]) VALUES('发现有SQL注入行为，注入语句："+zrsql+"','" + DateTime.Now.ToString() + "','" + ipok + "','首页清除','" + username + "','SQL注入清除记录" + "','新闻网站','" + khd + "')", conn))
                                    {

                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                //查询数据库中是否有该IP写入恶意访问记录


                            }
                            catch (Exception e1)
                            {

                            }
                        }
                        catch (Exception e2)
                        {
                        }
                        return "有注入："+t.ToString()+jgfh;
                    }
                    else
                    {
                        return jgfh+":无注入情况";
                    }
                }
                else
                {
                    return "未能检测到数据，请确认有检测的数据库！";
                }
            }
            else
            {
                return "未传入数据";
            }
        }
        catch { 
        return "出错了";
        }
    }

    

    /// <summary>
    /// 通过传入的值，清除HTML标签
    /// </summary>
    /// <param name="HTML">HTML内容</param>
    /// <param name="length">返回长度</param>
    ///　<returns>返回字符</returns>
    public static string ReplaceHtmlTag(string html, int length = 0)
    {
        string strText = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
        strText = System.Text.RegularExpressions.Regex.Replace(strText, "&[^;]+;", "");

        if (length > 0 && strText.Length > length)
            return strText.Substring(0, length);

        return strText;
    }


    /// <summary>
    /// sha1 加密，与php加密结果一样
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string Sha1Sign(string data)
    {
        byte[] temp1 = System.Text.Encoding.UTF8.GetBytes(data);
        SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
        byte[] temp2 = sha.ComputeHash(temp1);
        sha.Clear();            // 注意， 不能用这个           
        // string output = Convert.ToBase64String(temp2);// 不能直接转换成 base64string            
        var output = BitConverter.ToString(temp2);
        output = output.Replace("-", "");
        output = output.ToLower();
        return output;
    }



    /// <summary>
    /// 把DataTable中数据导入到Excel中(该方法及下面的导出处理已经弃用,新方法在toexcel.cs类中)
    /// </summary>

    /// <param name="dt">需导出的DATATABLE</param>
    /// <param name="strFileName">默认的导出Excel的文件名</param>
    ///  <param name="biaoti">表格标题,不需要请留空</param>
    /// <param name="strbt">表头自己以TABLE的TR方式构造一行数据,不需要请留空</param>
    /// <param name="autotr">自动生头表头，字符以逗号隔开，flase自定义TR(strbt自己构造tr)</param>
    /// <param name="tdlie">需多加的TD列，不需要设置为0</param>
    /// <param name="sql">表中无数据时,输出的提示信息</param>
    /// <param name="pdjtdz">需判断家庭地址的列名以逗号分隔，不需要请留空</param>
    //日期2013.9.20 作者：张明
    public static void DataTableToExcel(DataTable dt, string strFileName, string biaoti, string strbt, bool autotr, int tdlie, string sql,string pdjtdz)
    {
        //本方法以表格的形式输出DATATABLE到EXCEL，自定义项很灵活，完美解决科学计数法的问题

        //构造输出页面信息
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.Buffer = true;

        string strFileName1 = strFileName;
        strFileName += ".xls";
        HttpContext.Current.Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        //HttpContext.Current.Response.AppendHeader("Content-Length",



        //设置表格的样式
        string formstr = "<head><style>Table,td{border:1px solid #000;Text-align:center;}</style></head><body ><table style=\"border:1px solid #000;\">";
        //表格标题
        if (biaoti.Length > 0)
        {
            formstr = formstr + " <tr><td align=left colspan=" + (dt.Columns.Count + tdlie).ToString() + "><font size='16px'>" + biaoti + "</font></td></tr>";
        }
        //构造表头
        if (strbt.Length > 0)
        {
            //如果用户传递了表头信息
            if (autotr)
            {
                //自动加表头,处理由传入的以逗号排列列名称生成表头
                formstr = formstr + "<tr>";
                string[] bts = strbt.ToString().Split(',');
                foreach (string strLsz in bts)
                {
                    //用于标识特殊列
                    if (strLsz == "班级" || strLsz == "姓名" || strLsz == "性别" || strLsz == "身份证号" || strLsz == "民族" || strLsz == "户籍详细地址" || strLsz == "户籍性质" || strLsz == "户籍所在省" || strLsz == "是否低保" || strLsz == "学生电话" || strLsz == "家长姓名" || strLsz == "家长电话")
                    {
                        formstr = formstr + "<td bgcolor='#949694'><font color=red><b>" + strLsz + "</b></font></td>";
                    }
                    else
                    {
                        formstr = formstr + "<td>" + strLsz + "</td>";
                    }

                }
                formstr = formstr + "</tr>";
            }
            else
            {
                //自定义的表头,处理由用户传入的以<tr>开始，</tr>结束的自定义表头
                if (strbt.Length > 8)
                {

                    if (strbt.Substring(0, 3) != "<tr")
                    {
                        formstr = formstr + "<tr><td>" + strbt;
                    }
                    else
                    {
                        formstr = formstr + strbt;
                    }
                    if (strbt.Substring(strbt.Length - 4, 4) != "/tr>")
                    {
                        formstr = formstr + "</td></tr>";
                    }



                }
            }
        }
        else
        {
            //如果表头参数为空，自动用DATATABLE的列属性做表头
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                formstr = formstr + "<td>" + dt.Columns[j].Caption + "</td>";
            }
        }
        //表头处理结束

        //datatable数据显示处理

        if (dt.Rows.Count > 0)
        {
            //数据行大于0时，有数据时 //循环遍历DATATABLE中的每一行,并输出
            for (int i = 0; i < dt.Rows.Count; i++) //设置每个单元格
            {
                formstr = formstr + "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    //解决数字在EXCEL中显示为科学计数法的问题
                    float a = 0;
                    DateTime b;
                    if (float.TryParse(dt.Rows[i][j].ToString(), out a) && dt.Rows[i][j].ToString().Length > 0) //判断是否可以转换为整型            
                    {
                        if (DateTime.TryParse(dt.Rows[i][j].ToString(), out b))
                        {
                            formstr = formstr + "<td>" + dt.Rows[i][j].ToString() + "</td>";
                        }
                        else
                        {
                            //使用style=\"vnd.ms-excel.numberformat:@\ 说明这是数字
                            formstr = formstr + "<td style=\"vnd.ms-excel.numberformat:@\">" + dt.Rows[i][j].ToString() + "</td>";

                        }
                    }
                    else
                    {
                        //判断家庭地址是否填写完整  以地址中是否有镇,村,号,组,栋判断
                        //用此例子，还可以判断户籍所在省市县，身份证规范，性别等
                        if (pdjtdz.Length > 0)
                        {
                            //读取需判断的列
                             string[] jtdzs = pdjtdz.ToString().Split(',');
                             string showlie = "";
                             foreach (string jtdz in jtdzs)
                             {
                                 if (dt.Columns[j].Caption == jtdz)
                                 {
                                     //如果该列等于需判断的列
                                     if (dt.Rows[i][j].ToString().Contains("镇") || dt.Rows[i][j].ToString().Contains("村") || dt.Rows[i][j].ToString().Contains("号") || dt.Rows[i][j].ToString().Contains("组") || dt.Rows[i][j].ToString().Contains("栋"))
                                     {
                                         showlie ="<td>" + dt.Rows[i][j].ToString() + "</td>";
                                     }
                                     else
                                     {
                                         //不完整标识
                                         showlie = "<td bgcolor='#D1D2D3' style=\"text-align:left\"><font size=2 color=red>" + dt.Rows[i][j].ToString() + "</font></td>";
                                     }
                                 }
                                 else
                                 {
                                     showlie = "<td>" + dt.Rows[i][j].ToString() + "</td>";
                                 }


                             }
                             //显示需要判断的列
                             formstr += showlie;

                        }
                        else
                        {
                            //显示不需要判断的列
                            formstr = formstr + "<td>" + dt.Rows[i][j].ToString() + "</td>";
                        }
                    }

                }
                //构建用户添加的空列
                if (tdlie > 0)
                {
                    for (int ii = 0; ii < tdlie; ii++)
                    {
                        formstr = formstr + "<td></td>";
                    }
                }
                //结束一行输出
                formstr = formstr + "</tr>";

            }
            //内容输出结束
        }
        else
        {
            //数据为空时

            if (sql.Length > 0)
            {
                //输出用户自定义的错误信息
                formstr = formstr + "<tr><td align=left colspan=" + (dt.Columns.Count + tdlie).ToString() + "><font color=red>" + sql + "</font></td></tr>";
            }
        }

        
        //结束输出
        formstr = formstr + "</table>";
       
        //下载到客户端EXCEL
        HttpContext.Current.Response.Write(formstr);
        HttpContext.Current.Response.End();
    }
   







    /// <summary>
    /// 把Gridview中数据导入到Excel中
    /// </summary>
    /// <param name="gv">需要导出数据的Gridview</param>
    /// <param name="dt">Gridview的数据源</param>
    /// <param name="strFileName">默认的导出Excel的文件名</param>
    /// <param name="bolPart">全部还是部分导出到Excel.部分：true. 全部：false</param>
    public static void ToExcel(GridView gv, DataTable dt, string strFileName, bool bolPart)
    {
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.Buffer = true;
        gv.AllowPaging = bolPart;//设置导出数据是全部还是部分
        gv.DataSource = dt;
        
        gv.AllowPaging = false;
        //gv.PageSize = 100;
        //取消排序，取掉超链接
        gv.AllowSorting = false;
        gv.DataBind();

        //循环遍历GridView中的每一列
        for (int i = 0; i < gv.Columns.Count; i++) //设置每个单元格
        {
            gv.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            
         //gv.Columns[sfz]) .DataFormatString = "'{0}";
            for (int j = 0; j < gv.Rows.Count; j++)
            {
                gv.Rows[j].Cells[i].Attributes.Add("style", "vnd.ms-excel.numberformat:@;");
            }
        }
        //身份证前加"'"，在excel里才显示正确
      //  ((BoundField)gv.Columns[sfz]).DataFormatString = "'{0}";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Charset = "GB2312";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;

        strFileName += ".xls";
        
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(strFileName));//设置默认文件名
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //预防出现控件必须放在具有 runat=server 的窗体标记内的错误
        Page page = new Page();
        HtmlForm form = new HtmlForm();
        gv.EnableViewState = false;
        page.EnableEventValidation = false;
        page.DesignerInitialize();
        page.Controls.Add(form);
        form.InnerHtml = "<table><tr><td>" + strFileName + "</td></tr></table>";
        form.Controls.Add(gv);
        page.RenderControl(htw);

        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    }
    /// <summary>
    /// 把DataTable中数据导入到Excel中
    /// </summary>
    
    /// <param name="dt">需导出的DATATABLE</param>
    /// <param name="strFileName">默认的导出Excel的文件名</param>
    /// <param name="strbt">表头自己以TABLE的TR方式构造一行数据,不需要请留空</param>
    /// <param name="autotr">自动生头表头，字符以逗号隔开，flase自定义TR(strbt自己构造tr)</param>
    /// <param name="tdlie">需多加的TD列，不需要设置为0</param>
   /// <param name="sql">表中无数据时,输出的提示信息</param>
    public static void DToExcel(DataTable dt, string strFileName,string strbt,bool autotr,int tdlie,string sql)
    {
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.Buffer = true;
        
        
        
       
        ////  ((BoundField)gv.Columns[sfz]).DataFormatString = "'{0}";
        
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.Charset = "GB2312";
        //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        string strFileName1 = strFileName;
        strFileName += ".xls";
        //HttpContext.Current.Response.Buffer = true;

        //HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(strFileName));//设置默认文件名
        //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);


        HttpContext.Current.Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8)));
       HttpContext.Current.Response.ContentType = "application/ms-excel";


        //Response.ClearContent();
        
        //Response.AppendHeader("content-disposition", string.Format("attachment; filename={0}", HttpUtility.UrlEncode("招生统计数据_按班级.xls", System.Text.Encoding.UTF8)));
        //Response.ContentType = "application/ms-excel";






        //预防出现控件必须放在具有 runat=server 的窗体标记内的错误
        //Page page = new Page();
        //HtmlForm form = new HtmlForm();
        ////gv.EnableViewState = false;
        //page.EnableEventValidation = false;
        //page.DesignerInitialize();
        //page.Controls.Add(form);

        string formstr = "<head><style>Table,td{border:1px solid #000;Text-align:center;}</style></head><body ><table style=\"border:1px solid #000;\">";
        if (strbt.Substring(strbt.Length - 8, 8) == "专业所拥有的班级" || strFileName1.Substring(0, 4) == "学生学籍")
            {
                // formstr= formstr+" <tr><td align=left colspan=" +  ( dt.Columns.Count+tdlie).ToString()+ "><font size='16px'>" +strbt.Substring(strbt.Length-8,8)+ "</font></td></tr>";
      
            }
            else
            {
        formstr= formstr+" <tr><td align=left colspan=" +  ( dt.Columns.Count+tdlie).ToString()+ "><font size='16px'>" + strFileName1 + "</font></td></tr>";
       
            }
            //表头
        if (autotr)
        {
            //自动加表头
            formstr = formstr + "<tr>";
            string[] bts = strbt.ToString().Split(',');
           foreach (string strLsz in bts)
            {
                if (strLsz == "班级" || strLsz == "姓名" || strLsz == "性别" || strLsz == "身份证号" || strLsz == "民族" || strLsz == "户籍详细地址" || strLsz == "户籍性质" || strLsz == "户籍所在省" || strLsz == "是否低保" || strLsz == "学生电话" || strLsz == "家长姓名" || strLsz == "家长电话")
                {
                    formstr = formstr + "<td bgcolor='#949694'><font color=red><b>" + strLsz + "</b></font></td>";
                }
                else
                {
                    formstr = formstr + "<td>" + strLsz + "</td>";
                }





               if (strLsz == "专业所拥有的班级"&& System.Web.HttpContext.Current.Session["xingsfbqk_zydm"]!=null) { 
               //循环输出班级
                   string sqlbj = "SELECT bjmc,zyid  FROM banjxx WHERE njdm='2013' and zyid='" + System.Web.HttpContext.Current.Session["xingsfbqk_zydm"].ToString() + "'";
                   DataTable x = Sqlhelper.Serach(sqlbj);
                   if (x.Rows.Count > 0)
                   {
                       for (int i = 0; i < x.Rows.Count; i++)
                       {
                           formstr = formstr + "<td><font color=red>" + x.Rows[i]["bjmc"].ToString() + "</font></td>";
                       }
                       formstr = formstr + "<td>" + " (如果需要上传,请不要改变单元格的位置,班级只能用提供的班级,如果还没有班级,请待教务分班后,再下载一次!)</td>";
                   }
                   else
                   {
                       formstr = formstr + "<td><font color=red>" + "未从教务系统获取到班级,请联系教务部门在教务系统中录入班级信息" + "</font></td>";
         
                   }

               
               }
            }
            formstr = formstr + "</tr>";
        }
        else
        {
            //自定义的表头
            if (strbt.Length > 8)
            {

                if (strbt.Substring(0, 3) != "<tr")
                {
                    formstr = formstr + "<tr><td>" + strbt;
                }
                else
                {
                    formstr = formstr + strbt;
                }
                if (strbt.Substring(strbt.Length - 4, 4) != "/tr>")
                {
                    formstr = formstr + "</td></tr>";
                }



            }
        }
        //formstr = formstr + "<table style=\"border:1px solid #000;\">";
        

        //循环遍历DATATABLE中的每一行
        for (int i = 0; i < dt.Rows.Count; i++) //设置每个单元格
        {
            //gv.Columns[i].ItemStyle.HorizontalAlign = HorizontalAlign.Left;
            formstr = formstr + "<tr>";
            //gv.Columns[sfz]) .DataFormatString = "'{0}";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                //镇,村,号,组,路
                

                if (dt.Columns[j].Caption == "现家庭详细地址")
                {
                    if (dt.Rows[i][j].ToString().Length < 2) 
                    {
                        dt.Rows[i][j] = dt.Rows[i]["户口所在地"].ToString(); 
                    }
                }
                if (dt.Columns[j].Caption == "现家庭所在省")
                {
                    if (dt.Rows[i][j].ToString().Length < 2) { 
                        dt.Rows[i][j] = dt.Rows[i]["户籍所在省"].ToString(); 
                    }
                }
                if (dt.Columns[j].Caption == "现家庭所在市")
                {
                    if (dt.Rows[i][j].ToString().Length < 2) 
                    { 
                        dt.Rows[i][j] = dt.Rows[i]["户籍所在市"].ToString();
                    }
                }
                if (dt.Columns[j].Caption == "现家庭所在区县")
                {
                    if (dt.Rows[i][j].ToString().Length < 2) { 
                        dt.Rows[i][j] = dt.Rows[i]["户籍所在县"].ToString(); 
                    }
                }
                //xuesjbsj.xjtszs,xuesjbsj.xjtszshi,xjtszx,xuesjbsj.xjtdz
                //现家庭所在市	现家庭所在区县	现家庭详细地址




                float a = 0;
                DateTime b;
                if (float.TryParse(dt.Rows[i][j].ToString(), out a) && dt.Rows[i][j].ToString().Length > 0) //判断是否可以转换为整型            
                {


                    //身份证前加"'"，在excel里才显示正确
                    if (DateTime.TryParse(dt.Rows[i][j].ToString(), out b))
                    {
                        formstr = formstr + "<td>" + dt.Rows[i][j].ToString() + "</td>";
                    }
                    else
                    {
                       // formstr = formstr + "<td>'" + dt.Rows[i][j].ToString() + "</td>";
                        //使用style=\"vnd.ms-excel.numberformat:@\ 说明这是数字
                        formstr = formstr + "<td style=\"vnd.ms-excel.numberformat:@\">" + dt.Rows[i][j].ToString() + "</td>";

                    }
                }
                else
                {
                    if (dt.Columns[j].Caption == "户口所在地")
                    {
                        if (dt.Rows[i][j].ToString().Contains("镇") || dt.Rows[i][j].ToString().Contains("村") || dt.Rows[i][j].ToString().Contains("号") || dt.Rows[i][j].ToString().Contains("组") || dt.Rows[i][j].ToString().Contains("栋"))
                        {
                            formstr = formstr + "<td>" + dt.Rows[i][j].ToString() + "</td>";
                        }
                        else
                        {
                            formstr = formstr + "<td bgcolor='#D1D2D3' style=\"text-align:left\"><font size=2 color=red>" + dt.Rows[i][j].ToString() + "</font></td>";
                        }
                    }
                    else
                    {
                        //如果为空 填充为灰色
                        //if (dt.Rows[i][j].ToString() == "")
                        //{// bgcolor='#F0F4F0'
                        //    formstr = formstr + "<td  bgcolor='#E0E1E2'>" + dt.Rows[i][j].ToString() + "</td>";
                        //}
                        //else
                        //{

                            formstr = formstr + "<td>" + dt.Rows[i][j].ToString() + "</td>";
                        //}
                    }
                }

            }
            if (tdlie > 0)
            {
                for (int ii = 0; ii < tdlie; ii++)
                {
                    formstr = formstr + "<td></td>";
                }
            }
             formstr = formstr + "</tr>";

        }

        if (sql.Length > 0&&dt.Rows.Count<1)
        {
            formstr = formstr + "<tr><td align=left colspan=" + (dt.Columns.Count + tdlie).ToString() + "><font color=red>" + sql + "</font></td></tr>";
        }
        formstr = formstr + "</table>";
        //form.Controls.Add(dt);
        //System.IO.StringWriter sw = new System.IO.StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //formstr.RenderControl(htw);

        HttpContext.Current.Response.Write(formstr);
        HttpContext.Current.Response.End();
    }
   



	public basic()
	{

		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>    
    /// 获取客户端IP地址    
    /// </summary>    
    /// <returns></returns>   
    public static string GetIPAddress()    {
  string user_IP = string.Empty;       
        if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)     
        {            
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)      
            {               
                user_IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();        
            }           
            else        
            {              
                user_IP = System.Web.HttpContext.Current.Request.UserHostAddress;    
            }       
        }        
        else       
        {           
            user_IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();       
        }        
        return user_IP;   
    }

    /// <summary>
    /// 弹出对话框
    /// </summary>
    /// <param name="page">要弹出对话框的页面，本页用this.Page</param>
    /// 示例: basic.MsgBox(this.Page,"内容","-1");
    /// <param name="values">弹出的内容</param>
    /// <param name="URL">操作代码,及跳转的页面URL</param>
    public static void MsgBox(System.Web.UI.Page page, string values,string URL)
    {
        switch (URL)
        {
            case "":
                page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script language=javascript>alert('" + values + "')</script>");
                //显示信息
                break;
            case "-1":
                page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script language=javascript>alert('" + values + "');history.go(-1)</script>");
                //显示信息并跳到上一页
                break;
            case "close":
                page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script language=javascript>alert('" + values + "');window.opener=null;window.close();</script>");
                //显示信息并关闭页
                break;
            default:
                page.ClientScript.RegisterStartupScript(page.GetType(), "", "<script language=javascript>alert('" + values + "');location='" + URL + "'</script>");
                //显示信息并跳到至指定URL
                break;
        }
     }
    /// <summary>
    /// POST,GET安全验证
    /// </summary>
    /// 示例: basic.saferequest("GET或POST名");
    /// <param name="requestname">需安全处理的GET，POST值名</param>
    /// <param name="type">加参数为字符串，不加参数为ET，POST值名</param>
    public static void saferequest(string requestname)
    {
        string safestring = HttpContext.Current.Request[requestname];
        //处理过滤（待写）
    }
    /// <summary>
    /// 字符安全验证
    /// </summary>   
    /// 示例: basic.saferequest("值名或字符","1");
    /// <param name="requestname">需安全处理的字符串</param>
    /// <param name="type">加参数为字符串</param>
    public static void saferequest(string requestname,string type)
    {
        string safestring = requestname;
        //处理过滤（待写）
    }
}
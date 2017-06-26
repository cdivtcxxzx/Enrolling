using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml;
public partial class admin_Default : System.Web.UI.Page
{
    #region 功能模块说明及页面基本信息说明
    //所属模块：开发演示
    //任务名称：layui前端标签/响应式/表单端端功能演示及后台编写标准
    //完成功能描述：演示前端后台功能编写规范参考
    //编写人：张明
    //创建日期：2016年11月26日
    //更新日期：2016年11月28日
    //版本记录：第一版,编写后台页面编写规范
    #endregion
    #region 页面初始化参数
    private string xwdith = "1366";//屏宽
    private string xheight = "768";//屏高
    private string pagelm1 = "系统管理";//请与系统栏目管理中栏目关键字设置为一致便于权限管理

    private string pageqx1 = "登陆";//权限名称，根据页面的权限控制命名，与栏目管理中权限一致，最大设置为５个
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "";//当前页面值，在加载时会自动获取
    private string btitle = "";//附属标题
    public string url1 = "";
    #endregion



    /// <summary>
    /// 64位BASE加密
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string base64_encode(string str)
    {
        byte[] temp1 = System.Text.Encoding.UTF8.GetBytes(str);
        string temp2 = Convert.ToBase64String(temp1);
        return temp2;
    }
    public static string base64_dep(string str)
    {
        // byte[] temp1 = System.Text.Encoding.UTF8.GetBytes(str);
        //string temp2 =  Convert.ToBase64CharArray(temp1);
        //return temp2;

        string strPath = str;
        byte[] bpath = Convert.FromBase64String(strPath);
        strPath = System.Text.ASCIIEncoding.UTF8.GetString(bpath);
        return strPath;
    }
    /// <summary>
    /// 32位MD5加密
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
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


    private static string time14()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss");//返回14位的日期时间标准字符 
    }
    private static string cdivtcmd5(string username, string time14)
    {
        return Md5Hash(username + "cdivtc" + time14);//返回加密后的字符串 
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 页面基本配置及标题标识
        btitle = pagelm1;
        try
        {
            //读取cookies中的当前网址信息,如果没有使用服务器获取
            if (Request.Cookies["xurl"] != null)
            {
                HttpCookie cookiesurl = Request.Cookies["xurl"];
                webpage = cookiesurl.Value.ToString().Replace("%3A", ":").Replace("%3F", "?").Replace("%3D", "=").Replace("%26", "&");
            }
            else
            {
                webpage = Request.Url.GetLeftPart(UriPartial.Query).ToString().Replace(Request.Url.Port.ToString(), Sqlhelper.serverport);
            }

        }
        catch (Exception e1) { Response.Write(e1.Message); }

        System.Data.DataTable wangzxx = Sqlhelper.Serach("SELECT TOP 100 *  FROM [wangzxx] order by xxid asc");
        if (wangzxx.Rows.Count > 0)
        {
            for (int i = 0; i < wangzxx.Rows.Count; i++)
            {
                //网站开关
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "isopen")
                {
                    if (wangzxx.Rows[i]["xxnr"].ToString() == "0")
                    {
                        btitle = "网站正在维护，请稍后再访问！" + btitle;

                        // Response.End();前端用户启用,后台根据情况启用
                    }

                }
                //网站标题及META关键字设置
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "title") this.Title = wangzxx.Rows[i]["xxnr"].ToString() + btitle;
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "MetaKeywords") this.MetaKeywords = wangzxx.Rows[i]["xxnr"].ToString() + btitle;
                if (wangzxx.Rows[i]["xxgjz"].ToString() == "description") this.MetaDescription = wangzxx.Rows[i]["xxnr"].ToString() + btitle;

            }
        }

        #endregion
        account_displayname.Style.Add("display", "none");
#region 学生登陆
        string sf = "";
        if (Request["sf"] != null)
        {
            sf = Request["sf"].ToString();
        }
        if (sf == "xs")
        {
            this.login_title.InnerHtml = "学生网上自助报到登陆";
            this.txt_name.Attributes.Add("placeholder", "请输入高考报名号");
            this.txt_pwd.Attributes.Add("placeholder", "默认密码为身份证后八位");
            custom_display_64.Style.Add("display", "");
        }
        if (sf == "czy")
        {
            pc.Visible = true;
            account_displayname.Style.Add("display", "");
            this.login_title.InnerHtml = "迎新操作员登陆";
            this.txt_name.Attributes.Add("placeholder", "请输入用户名");
            this.txt_pwd.Attributes.Add("placeholder", "请输入密码");

        }

#endregion


        if (Request["token"] != null)
        {
            string webtime = "";
            string webpwd = "";
            string webusername = "";
            string appname = "";
            string webToUrl = "";
            //单点登陆
            string okstr = base64_dep(Request["token"].ToString()).Replace("'", "").Replace("{", "").Replace("}", "");
            //Response.Write(okstr);
            //Response.End();
            string[] strok = okstr.Split(',');
            foreach (string str1 in strok)
            {
                string[] cs = str1.Split(':');
                if (cs[0].ToString().ToLower() == "webusername")
                {
                    webusername = cs[1].ToString();
                }
                if (cs[0].ToString().ToLower() == "webpwd")
                {
                    webpwd = cs[1].ToString();
                }
                if (cs[0].ToString().ToLower() == "webtime")
                {
                    webtime = cs[1].ToString();
                }

            }

            string webpwd1 = cdivtcmd5(webusername, webtime);
            if (webpwd1 == webpwd)
            {
                //认证通过,允许登陆

                string strSqlBenDiLogin = "SELECT * FROM yonghqx WHERE yhid=@yhid";
                try
                {
                    DataTable dtBenDiLogin = Sqlhelper.Serach(strSqlBenDiLogin, new SqlParameter("yhid", webusername));
                    if (dtBenDiLogin.Rows.Count == 1)
                    {
                        string LoginName = dtBenDiLogin.Rows[0]["xm"].ToString();
                        string LoginUerLsz = dtBenDiLogin.Rows[0]["lsz"] != null ? dtBenDiLogin.Rows[0]["lsz"].ToString() : "";

                        HttpCookie mycookie = new HttpCookie("LoginUser");

                        mycookie.Values.Add("UserName", webusername);
                        mycookie.Values.Add("Name", LoginName);
                        mycookie.Values.Add("Pwd", dtBenDiLogin.Rows[0]["mm"].ToString());

                        mycookie.Expires = DateTime.Now.AddDays(7);
                        HttpContext.Current.Response.AppendCookie(mycookie);


                        Session["UserName"] = webusername;
                        Session["Name"] = LoginName;
                        Session["Yhqx"] = new c_login().getPowerFromYhqx(webusername) != null ? new c_login().getPowerFromYhqx(webusername).ToString() : "";
                        Session["Lsz"] = "";


                        string[] strLszs = LoginUerLsz.Split(',');
                        if (Session["Lsz"].ToString() == "")
                        {
                            foreach (string strLsz in strLszs)
                            {
                                System.Xml.Linq.XDocument zQx = new c_login().getPowerFromZhuqx(strLsz);
                                if (zQx == null) continue; //有可能getPowerFromZhuqx得到空
                                foreach (var temp in zQx.Elements("Root").Elements())
                                {
                                    string lanmStr = temp.Name.ToString();
                                    System.Xml.Linq.XDocument sessionXML = System.Xml.Linq.XDocument.Parse(Session["Yhqx"].ToString());
                                    //如果直接没有该栏目的权限，直接添加该栏目结点
                                    if (sessionXML.Element("Root").Element(lanmStr) == null)
                                    {
                                        sessionXML.Element("Root").Add(temp);
                                        Session["Yhqx"] = sessionXML.ToString();
                                    }
                                    else
                                    {
                                        foreach (var oper in temp.Elements())
                                        {
                                            string operStr = oper.Name.ToString();
                                            if (sessionXML.Element("Root").Element(lanmStr).Element(operStr) == null)
                                            {
                                                sessionXML.Element("Root").Element(lanmStr).Add(oper);
                                                Session["Yhqx"] = sessionXML.ToString();
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        //Response.Redirect("/"+Sqlhelper.gldir+"/");
                    }

                }
                catch (Exception e3)
                {
                    Response.Write(e3.Message);
                }
                if (Session["name"] != null)
                {
                    Response.Write(Session["name"].ToString() + Session["username"].ToString());
                    // Response.Write("自动登陆成功!");
                    //Response.End();
                    Response.Write(" <script>top.location.href = '/"+Sqlhelper.gldir+"/';</script>");
                    Response.End();
                    //Response.Redirect("/"+Sqlhelper.gldir+"/");
                }

                // return;
            }

            //string token = base64_encode("{'WebUserName':'" + webusername + "','WebPwd':'" + webpwd + "','WebTime':'" + webtime + "','WebToUrl':'','AppName':'" + appname + "'}");

            //Response.Redirect("http://www.cdivtc.com.cn/logincdivtczm.aspx?token=" + token);

        }



        if (Request["url"] != null)
        {
            //=号|,&号@
            url1 = "?url=" + Request["url"].ToString();
            Response.Write(" <script>if (top.location != self.location) {top.location.href = '/login.aspx" + url1 + "';    }</script>");

        }

        c_index indexLr = new c_index("");
        this.Title = indexLr.title;
        this.MetaKeywords = indexLr.metaKeyworkds;
        this.MetaDescription = indexLr.metaDescription;
        // this.webcss.Href = "admin/User_index.css"; //网站css文件
        /*
        //网站配置表中读取
        this.Title = "成都工业职业技术学院 - 用户登陆";
        this.MetaKeywords = "网站关键字";
        this.MetaDescription = "网站meta2与关键字类似";
        this.webcss.Href = "admin/User_index.css"; //网站登陆页css文件
         */
        //读取cookie,是否有用户名,如果有,返回给用户名文本框


    }

    protected void Unnamed1_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    { //登陆按钮事件
        string b = "1";

      




        if (this.txt_name.Value != "" && this.txt_pwd.Value != "")
        {
            string x2 = "2";
            string yanzhengma = Session["Yanzhengma"] != null ? Session["Yanzhengma"].ToString() : "";

            if (yanzhengma != this.txt_validate.Value.Trim())
            {
                Label1.Text = "<font color=red>验证码错误！</font>";
                return;
            }
            if (login_title.InnerText == "学生网上自助报到登陆")
            {
                //验证时间
                DataTable timeok = Sqlhelper.Serach("SELECT     TOP (1) Fresh_Batch.Batch_Name FROM         Fresh_Batch LEFT OUTER JOIN                      vw_fresh_student_base ON Fresh_Batch.PK_Batch_NO = vw_fresh_student_base.FK_Fresh_Batch WHERE     (Fresh_Batch.Service_Begin <= '" + DateTime.Now.ToString() + "') AND (Fresh_Batch.Service_End >= '" + DateTime.Now.ToString() + "') and (PK_SNO=@name or Test_NO=@name)", new SqlParameter("name", this.txt_name.Value));
                if (timeok.Rows.Count > 0)
                {

                }
                else
                {
                    Label1.Text = "<font color=red>对不起，迎新服务还未开始，请稍侯再试!</font>";
                    return;
                }


                DataTable userxs = Sqlhelper.Serach("SELECT TOP 1 [Name],[Password],right(ID_NO,6) depassword ,PK_SNO FROM [Base_STU] where Test_NO=@name or PK_SNO=@name", new SqlParameter("name", this.txt_name.Value));
                if (userxs.Rows.Count > 0)
                {
                    if (txt_pwd.Value == userxs.Rows[0]["Password"].ToString())
                    {
                        //登陆成功
                        Session["username"] = userxs.Rows[0]["PK_SNO"].ToString();
                        Session["pk_sno"] = userxs.Rows[0]["PK_SNO"].ToString();
                        Session["pk_sno_name"] = userxs.Rows[0]["Name"].ToString();
                        Session["name"] = userxs.Rows[0]["Name"].ToString();
                        try
                        {
                            Session["Lsz"] = "20";
                            string[] strLszs = Session["Lsz"].ToString().TrimEnd(',').Split(',');

                            foreach (string strLsz in strLszs)
                            {
                                c_login x = new c_login();
                                XDocument zQx = x.getPowerFromZhuqx(strLsz);
                                if (zQx == null) continue; //有可能getPowerFromZhuqx得到空
                                foreach (var temp in zQx.Elements("Root").Elements())
                                {
                                    string lanmStr = temp.Name.ToString();
                                    XDocument sessionXML = XDocument.Parse(Session["Yhqx"].ToString());
                                    //如果直接没有该栏目的权限，直接添加该栏目结点
                                    if (sessionXML.Element("Root").Element(lanmStr) == null)
                                    {
                                        sessionXML.Element("Root").Add(temp);
                                        Session["Yhqx"] = sessionXML.ToString();
                                    }
                                    else
                                    {
                                        foreach (var oper in temp.Elements())
                                        {
                                            string operStr = oper.Name.ToString();
                                            if (sessionXML.Element("Root").Element(lanmStr).Element(operStr) == null)
                                            {
                                                sessionXML.Element("Root").Element(lanmStr).Add(oper);
                                                Session["Yhqx"] = sessionXML.ToString();
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        catch { }
                        //Response.Redirect("/" + Sqlhelper.gldir + "/defaultxs.aspx");
                        Response.Redirect("/view/stu-baodao.aspx");
                    }
                    else
                    {
                        if (userxs.Rows[0]["Password"].ToString() == userxs.Rows[0]["depassword"].ToString())
                        {
                            Label1.Text = "<font color=red>登陆失败,密码输入错误,初始密码为身份证最后六位!</font>";
                        }
                        else
                        {
                            Label1.Text = "<font color=red>登陆失败,密码输入错误,你已经更改过密码!密码第一位是" + userxs.Rows[0]["Password"].ToString().Substring(0,1)+ "</font>";
                        }
                    }
                }
                else
                {

                    Label1.Text = "<font color=red>登陆失败,请确认高考报名号是否正确!</font>";
                }
            }
            else
            {
                c_login myLogin = new c_login();
                if (myLogin.login(this.txt_name.Value, this.txt_pwd.Value, true, false))
                {
                    //获取模块权限
                    string x1 = "1";
                    if (!new c_login().powerYanzheng(Session["UserName"].ToString(), "用户管理", "登陆", "2"))
                    {
                        Response.Write("<script>alert('对不起,你无权使用本系统！');top.location.href=/';</script>");
                    }
                    if (login_title.InnerText == "迎新操作员登陆")
                    {
                        
                        Session["pk_batch_no"] = pc.SelectedValue; ;
                        Session["pk_staff_no"] = Session["UserName"].ToString();
                        Response.Redirect("/" + Sqlhelper.gldir + "/defaultczy.aspx");
                        Response.End();
                    }
                    Response.Write("<script>alert('登陆成功！');</script>");
                    //Server.Transfer("~/"+Sqlhelper.gldir+"/default.aspx");
                    if (Request.QueryString["url"] != null)
                    {
                        //=号|,&号@

                        string x = Request["url"].ToString().Replace("|", "=").Replace("@", "&");
                        Response.Redirect(Request["url"].ToString());
                    }
                    else
                    {
                        Response.Redirect("/" + Sqlhelper.gldir + "/defaultgl.aspx");
                    }
                }
                else
                {
                    Label1.Text = "<font color=red>登陆失败,请确认自己的用户名或密码是否正确!</font>";
                }
            }
        }
        else
        {
            Label1.Text = "<font color=red>用户名或密码不能为空!</font>";
        }

    }
}
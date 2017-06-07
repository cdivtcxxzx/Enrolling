using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using ServiceReference1;
using System.Xml.Linq;

/// <summary>
///c_login 的摘要说明
/// </summary>
public class c_login:System.Web.UI.Page
{
    protected string strGetFromUum { get; set; }

    protected struct UUM
    {
        public string guid;
        public string yhqx;
        public string lsz;
        public string yhid;
        public string xm ;
        public string mm ;
        public string uumzw;
        public string lxdh ;
        public DateTime dltime;
        public int fwcs;
        public UUM(string yhid)
        {
            this.guid = "";
            this.yhqx = "";
            this.lsz = "";
            this.yhid = yhid;
            this.xm = "";
            this.mm = "";
            this.uumzw = "";
            this.lxdh = "";
            this.dltime = DateTime.Now;
            this.fwcs = 1;
        }

    }

    UumDataServiceClient client = new UumDataServiceClient();

    public c_login()
    {}
    /// <summary>
    /// 页面内统一功能登陆验证及权限验证
    /// </summary>
    /// <param name="pagelm">栏目关键字</param>
     /// <param name="qx">需判断的权限</param>
/// <param name="xiangqing">验证详情介绍</param>
///  <param name="isone">是否为只是第一次回调时验证</param>
    /// <param name="qx1">权限一</param>
     /// <param name="qx2">权限二</param>
    /// <param name="qx3">权限三</param>
   /// <param name="qx4">权限四</param>
     /// <param name="qx5">权限五</param>

    
    /// <returns></returns>
    public void tongyiyz(string pagelm, string qx, string xiangqiang, bool isone, string qx1, string qx2, string qx3, string qx4, string qx5)
    {
        #region 登陆验证及权限
        try
        {

            //将标题设置为栏目+权限
            this.Title = pagelm + "--" + qx1 + "-" + qx2 + "-" + qx3 + "-" + qx4 + "-" + qx5;
            //获取登陆状态
            if (!new c_login().Loginyanzheng())
            {
                //登陆验证为Flase
                //basic.MsgBox(this.Page, "登陆已超时，请重新登陆！", "~/Default.aspx");
                System.Web.HttpContext.Current.Response.Write("<script>top.location.href='/login.aspx';</script>");
            }
            if (isone)
            {
                if (!IsPostBack)
                {

                    //第一次访问这个页面时
                    //获取模块权限

                    if (!new c_login().powerYanzheng(Session["UserName"].ToString(), pagelm, qx, "2"))
                    {
                        System.Web.HttpContext.Current.Response.Write("<script>alert('" + System.Web.HttpContext.Current.Session["Name"].ToString() + ":对不起,您无权" + qx + "\"" + this.Title.ToString() + "\"的内容！如果你确定要操作该内容,请联系您的上级部门!');history.go(-1)</script>");
                        //Response.End();
                    }
                    else
                    {
                        //日志
                        new c_log().logAdd(pagelm, qx, xiangqiang);


                    }
                }
            }
            else
            {
                if (!new c_login().powerYanzheng(System.Web.HttpContext.Current.Session["UserName"].ToString(), pagelm, qx, "2"))
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('" + System.Web.HttpContext.Current.Session["Name"].ToString() + ":对不起,您无权" + qx + "\"" + this.Title.ToString() + "\"的内容！如果你确定要操作该内容,请联系您的上级部门!');history.go(-1)</script>");
                   // Response.End();
                }
                else
                {
                    //日志
                    new c_log().logAdd(pagelm, qx, xiangqiang);


                }
            }


        }
        catch (Exception ex)
        {
            //
            System.Web.HttpContext.Current.Response.Write("<script>alert('访问此页出错!"+ex.Message+"');history.go(-1)</script>");
             
            new c_log().logAdd(pagelm, qx, "在PAGE_LOAD登陆验证、权限验证时出错，" + ex.StackTrace.ToString() + ",详情：" + ex.InnerException + ex.Message);
        }
        #endregion

    }
    public void tongyiyz(string webpage,string pagelm, string qx, string xiangqiang, bool isone, string qx1, string qx2, string qx3, string qx4, string qx5)
    {
        #region 登陆验证及权限
        try
        {
           
            //将标题设置为栏目+权限
            this.Title = pagelm + "--" + qx1 + "-" + qx2 + "-" + qx3 + "-" + qx4 + "-" + qx5;
            //获取登陆状态
            if (!new c_login().Loginyanzhengurl(webpage))
            {
                //登陆验证为Flase
                //basic.MsgBox(this.Page, "登陆已超时，请重新登陆！", "~/Default.aspx");
                if (webpage.Length > 0)
                { System.Web.HttpContext.Current.Response.Write("<script>top.location.href='/login.aspx?url="+webpage+"';</script>"); }
                else
                {
                    System.Web.HttpContext.Current.Response.Write("<script>top.location.href='/login.aspx';</script>");
                }
            }
            if (isone)
            {
                if (!IsPostBack)
                {

                    //第一次访问这个页面时
                    //获取模块权限

                    if (!new c_login().powerYanzheng(Session["UserName"].ToString(), pagelm, qx, "2"))
                    {
                        System.Web.HttpContext.Current.Response.Write("<script>alert('" + System.Web.HttpContext.Current.Session["Name"].ToString() + ":对不起,您无权" + qx + "\"" + this.Title.ToString() + "\"的内容！如果你确定要操作该内容,请联系您的上级部门!');history.go(-1)</script>");
                        //Response.End();
                    }
                    else
                    {
                        //日志
                        new c_log().logAdd(pagelm, qx, xiangqiang);


                    }
                }
            }
            else
            {
                if (!new c_login().powerYanzheng(System.Web.HttpContext.Current.Session["UserName"].ToString(), pagelm, qx, "2"))
                {
                    System.Web.HttpContext.Current.Response.Write("<script>alert('" + System.Web.HttpContext.Current.Session["Name"].ToString() + ":对不起,您无权" + qx + "\"" + this.Title.ToString() + "\"的内容！如果你确定要操作该内容,请联系您的上级部门!');history.go(-1)</script>");
                    // Response.End();
                }
                else
                {
                    //日志
                    new c_log().logAdd(pagelm, qx, xiangqiang);


                }
            }


        }
        catch (Exception ex)
        {
            //
            System.Web.HttpContext.Current.Response.Write("<script>alert('访问此页出错!" + ex.Message + "');history.go(-1)</script>");

            new c_log().logAdd(pagelm, qx, "在PAGE_LOAD登陆验证、权限验证时出错，" + ex.StackTrace.ToString() + ",详情：" + ex.InnerException + ex.Message);
        }
        #endregion

    }
   
    /// <summary>
    /// 统一登录验证
    /// </summary>
    /// <param name="strUsername">用户名</param>
    /// <param name="Cache">是否设置自动登陆</param>
    /// <param name="IsCookie">是否以cookie登陆</param>
    /// <returns></returns>
    public bool login(string strUsername, bool Cache, bool IsCookie)
    {
        try
        {           
            strUsername = strUsername.Trim();           
            //如果是Cookie登陆，需要对strPwd时行解密
            //再次确定输入非空
            if (strUsername != "" )
            {

                string strUserNameGetFromUum = strUsername;
                UUM getUum = new UUM("");
                    if (Cache)
                    {
                        HttpCookie mycookie = new HttpCookie("LoginUser");

                        mycookie.Values.Add("UserName", strUsername);
                        //mycookie.Values.Add("Name", strNameGetFromUum);
                       // mycookie.Values.Add("Pwd", md5.MD5Encrypt(strPwd, md5.GetKey()));//加密存于cookie

                        mycookie.Expires = DateTime.Now.AddDays(7);
                        HttpContext.Current.Response.AppendCookie(mycookie);
                    }
                    Session["UserName"] = strUsername;
                    
                    Session["Yhqx"] = this.getPowerFromYhqx(strUsername);
                    Session["Lsz"] = "";



                    //登陆信息写入数据库
                    string sqlSerachByYhid = "SELECT * FROM [yonghqx] WHERE yhid=@yhid";
                    DataTable dt = Sqlhelper.Serach(sqlSerachByYhid, new SqlParameter("yhid", strUsername));
                    try
                    {
                        if (dt.Rows.Count > 0)
                        {//如果已经存在于数据库，则只修改登陆次数 ,将用户组写入session
                            Session["Name"] = dt.Rows[0]["xm"].ToString();
                            Session["Lsz"] = dt.Rows[0]["lsz"].ToString().TrimEnd(',');
                            //如果用户组为空,则赋予普通用户给用户
                            if (Session["Lsz"] == "") Session["Lsz"] = new c_log().getlsz("普通用户");
                            string[] strLszs = Session["Lsz"].ToString().Split(',');
                            if (Session["Lsz"].ToString() != "")
                            {
                                foreach (string strLsz in strLszs)
                                {
                                    XDocument zQx = this.getPowerFromZhuqx(strLsz);
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
                            //else
                            //{
                            //    Session["Yhqx"] = "";
                            //}
                            //权限验证
                            if (!powerYanzheng(Session["UserName"].ToString(), "用户管理", "登陆", "2"))
                            {
                                Response.Write("<script>alert('" + Session["Name"].ToString() + ":对不起,您无权登陆,请联系管理员或您的上级部门!!');history.go(-1)</script>");
                                //Response.End();
                                return false;

                            }
                            //string sqlUpdate = "UPDATE yonghqx SET fwcs=@fwcs,dltime=@dltime,mm=@md5mm WHERE yhid=@yhid";// md5.MD5Encrypt(strPwd, md5.GetKey())
                            string sqlUpdate = "UPDATE yonghqx SET fwcs=@fwcs,dltime=@dltime WHERE yhid=@yhid";
                            int fwcs = int.Parse(dt.Rows[0]["fwcs"].ToString()) + 1;
                            //写入数据库
                            Sqlhelper.ExcuteNonQuery(sqlUpdate, new SqlParameter("fwcs", fwcs.ToString()),
                                new SqlParameter("dltime", DateTime.Now.ToString()),
                               // new SqlParameter("md5mm", md5.MD5Encrypt(strPwd, md5.GetKey())),
                                new SqlParameter("yhid", strUsername));
                        }
                        else
                        {//不存在,则新建一条记录 
                            string sqlInsert = "INSERT INTO  [yonghqx](guid,yhqx,lsz,yhid,xm,uumzw,lxdh,dltime,fwcs) VALUES(@guid,@yhqx,@lsz,@yhid,@xm,@uumzw,@lxdh,@dltime,@fwcs)";
                            //密码来自用户输入
                            if (Sqlhelper.ExcuteNonQuery(sqlInsert,
                                new SqlParameter("guid", getUum.guid),
                                new SqlParameter("yhqx", "0"),
                                new SqlParameter("lsz", getUum.lsz),
                                new SqlParameter("yhid", getUum.yhid),
                                new SqlParameter("xm", getUum.xm),
                               // new SqlParameter("mm", md5.MD5Encrypt(strPwd, md5.GetKey())),
                                new SqlParameter("uumzw", getUum.uumzw),
                                new SqlParameter("lxdh", getUum.lxdh),
                                new SqlParameter("dltime", DateTime.Now),
                                new SqlParameter("fwcs", 1)) > 0)
                                return true;
                            //权限验证
                            if (!powerYanzheng(Session["UserName"].ToString(), "用户管理", "登陆", "2"))
                            {
                                Response.Write("<script>alert('" + Session["Name"].ToString() + ":对不起,您是第一次登陆,请重新登陆一次!!');history.go(-1)</script>");
                                //Response.End();
                                return false;

                            }
                        }
                    }
                    catch
                    {
                        basic.MsgBox(this.Page, "登陆异常！", "-1");
                        return false;
                    }
                    //写入日志
                    try
                    {
                        if (IsCookie)
                        {
                            new c_log().logAdd("用户管理", "登陆", "使用COOKIE,UUM系统登陆");
                            // +",浏览器："+Request.Browser.Browser.ToString()+Request.Browser.Version.ToString()
                        }
                        else
                        {
                            new c_log().logAdd("用户管理", "登陆", "手动UUM系统登陆");
                        }
                    }
                    catch
                    {
                        basic.MsgBox(this.Page, "登陆写入日志异常!", "");
                        return false;
                    }
                    return true;
  

            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    ///(登录) 用户登陆、用户所权限获取 登录页面方法
    /// </summary>
    /// <param name="strUsername">用户名</param>
    /// <param name="strPwd">密码</param>
    /// <param name="Cache">是否设置自动登陆</param>
    /// <param name="IsCookie">是否以cookie登陆</param>
    /// <returns></returns>
    public bool login(string strUsername, string strPwd, bool Cache, bool IsCookie)
    {
        //if (strUsername == "" || strPwd == "")
        //{
        //    return false;
        //}
        //try
        //{            
        //    if (IsCookie)
        //    {
        //        strPwd = md5.MD5Decrypt(strPwd, md5.GetKey());
        //    }
        //    strUsername = strUsername.Trim();
        //    strPwd = strPwd.Trim();
        //    //再次确定输入非空
        //    if (strUsername != "" && strPwd != "")
        //    {
        //        string strSqlBenDiLogin = "SELECT * FROM yonghqx WHERE yhid=@yhid AND mm=@mm";
        //        string strPwdEncrypt = md5.MD5Encrypt(strPwd, md5.GetKey());
        //        DataTable dtBenDiLogin = Sqlhelper.Serach(strSqlBenDiLogin, new SqlParameter("yhid", strUsername), new SqlParameter("mm", strPwdEncrypt));
        //        if (dtBenDiLogin.Rows.Count == 1)
        //        {//登陆成功
        //            string LoginName = dtBenDiLogin.Rows[0]["xm"].ToString();
        //            string LoginUerLsz = dtBenDiLogin.Rows[0]["lsz"] != null ? dtBenDiLogin.Rows[0]["lsz"].ToString() : "";
        //            //资源结构JSON
        //            string FileGroup = dtBenDiLogin.Rows[0]["file_json"] != null ? dtBenDiLogin.Rows[0]["file_json"].ToString() : "";
        //            if (Cache)
        //            {
        //                HttpCookie mycookie = new HttpCookie("LoginUser");

        //                mycookie.Values.Add("UserName", strUsername);
        //                mycookie.Values.Add("Name", LoginName);
        //                mycookie.Values.Add("Pwd", strPwdEncrypt);//加密的密码存于cookie
        //                mycookie.Values.Add("file_json", FileGroup);
        //                mycookie.Expires = DateTime.Now.AddDays(7);
        //                HttpContext.Current.Response.AppendCookie(mycookie);
        //            }
        //            //存SESSION
        //            Session["UserName"] = strUsername;
        //            Session["Name"] = LoginName;
        //            Session["Yhqx"] = this.getPowerFromYhqx(strUsername) != null ? this.getPowerFromYhqx(strUsername).ToString() : "";
        //            Session["file_json"] = FileGroup;
        //            Session["Lsz"] = LoginUerLsz;


        //            string[] strLszs = LoginUerLsz.TrimEnd(',').Split(',');
        //            foreach (string strLsz in strLszs)
        //            {
        //                XDocument zQx = this.getPowerFromZhuqx(strLsz);
        //                if (zQx == null) continue; //有可能getPowerFromZhuqx得到空
        //                foreach (var temp in zQx.Elements("Root").Elements())
        //                {
        //                    string lanmStr = temp.Name.ToString();
        //                    XDocument sessionXML = XDocument.Parse(Session["Yhqx"].ToString());
        //                    //如果直接没有该栏目的权限，直接添加该栏目结点
        //                    if (sessionXML.Element("Root").Element(lanmStr) == null)
        //                    {
        //                        sessionXML.Element("Root").Add(temp);
        //                        Session["Yhqx"] = sessionXML.ToString();
        //                    }
        //                    else
        //                    {
        //                        foreach (var oper in temp.Elements())
        //                        {
        //                            string operStr = oper.Name.ToString();
        //                            if (sessionXML.Element("Root").Element(lanmStr).Element(operStr) == null)
        //                            {
        //                                sessionXML.Element("Root").Element(lanmStr).Add(oper);
        //                                Session["Yhqx"] = sessionXML.ToString();
        //                            }

        //                        }
        //                    }
        //                }
        //            }
        //            //权限验证
        //            if (!powerYanzheng(Session["UserName"].ToString(), "用户管理", "登陆", "2"))
        //            {
        //                Response.Write("<script>alert('" + Session["Name"].ToString() + ":对不起,您无权登陆,请联系管理员或您的上级部门!!');history.go(-1)</script>");
        //                //Response.End();
        //                return false;

        //            }


        //            string sqlUpdate = "UPDATE yonghqx SET fwcs=@fwcs,dltime=@dltime,mm=@md5mm WHERE yhid=@yhid";// md5.MD5Encrypt(strPwd, md5.GetKey())

        //            int fwcs = 0;
        //            try
        //            {
        //                fwcs = int.Parse(dtBenDiLogin.Rows[0]["fwcs"].ToString()) + 1;

        //                //写入数据库
        //                Sqlhelper.ExcuteNonQuery(sqlUpdate, new SqlParameter("fwcs", fwcs.ToString()),
        //                    new SqlParameter("dltime", DateTime.Now.ToString()),
        //                    new SqlParameter("md5mm", strPwdEncrypt),
        //                    new SqlParameter("yhid", strUsername));

        //            }
        //            catch { }
        //            //写入日志
        //            try
        //            {
        //                if (IsCookie)
        //                {
        //                    new c_log().logAdd("用户管理", "登陆", "使用COOKIE,本地系统帐户登陆");
        //                    // +",浏览器："+Request.Browser.Browser.ToString()+Request.Browser.Version.ToString()
        //                }
        //                else
        //                {
        //                    new c_log().logAdd("用户管理", "登陆", "手动本地系统帐户系统登陆");
        //                }
        //            }
        //            catch
        //            {
        //                basic.MsgBox(this.Page, "登陆写入日志异常!", "");
        //            }
        //            return true;
        //        }
        //        else 
        //        {
        //            //本地数据库没有
        //            return false;
        //        }
        //        //登陆成功完
        //    }//异常情况，用户名密码为空
        //    else
        //    {
        //        return false;
        //    }
        //}
        //catch(Exception e)

        //{
        //    string x = e.Message;
        //    return false;
        //}


        //-------------------------------------
        try
        {
            string strPwdByBenDiLogin = strPwd.Trim();//用于本地数据库登陆的密码
            if (IsCookie)
            {
                strPwd = md5.MD5Decrypt(strPwd, md5.GetKey());

            }

            strUsername = strUsername.Trim();
            strPwd = strPwd.Trim();
            //如果是Cookie登陆，需要对strPwd时行解密

            //再次确定输入非空
            if (strUsername != "" && strPwd != "")
            {
                string strNameGetFromUum = "";
                string strUserNameGetFromUum = "";
                UUM getUum = new UUM("");
                try
                {
                    strGetFromUum = client.GetUumData(new Dictionary<string, string>()
                                 {
                                    {"AuthCode","E97084F7-F7C3-467C-B548-E89E39B2007D"},
                                    {"Flag","AuthUser2"},
                                    {"UserName",strUsername},
                                    {"Password",strPwd},
			      	    {"AppName","学生迎新系统"},
                                }
                                       );
                    XDocument xmlGetFromUum = XDocument.Parse(strGetFromUum);


                    foreach (var temp in xmlGetFromUum.Elements("Root").Elements("User"))
                    {
                        strNameGetFromUum = temp.Element("Name").Value;
                        strUserNameGetFromUum = temp.Element("UserName").Value;

                        getUum.guid = temp.Element("Id").Value;
                        getUum.yhid = strUsername;
                        getUum.xm = strNameGetFromUum;
                        getUum.uumzw = temp.Element("Title").Value;
                        getUum.lxdh = temp.Element("MobilePhone").Value;

                    }
                }
                catch (Exception ex)
                {

                    basic.MsgBox(this.Page, "登陆错误！", "/Default.aspx");
                    return false;
                }


                if (strUserNameGetFromUum != "" && strUserNameGetFromUum == strUsername)
                {//UUM通过验证



                    if (Cache)
                    {
                        HttpCookie mycookie = new HttpCookie("LoginUser");

                        mycookie.Values.Add("UserName", strUsername);
                        mycookie.Values.Add("Name", strNameGetFromUum);
                        mycookie.Values.Add("Pwd", md5.MD5Encrypt(strPwd, md5.GetKey()));//加密存于cookie

                        mycookie.Expires = DateTime.Now.AddDays(7);
                        HttpContext.Current.Response.AppendCookie(mycookie);
                    }
                    Session["UserName"] = strUsername;
                    Session["Name"] = strNameGetFromUum;
                    Session["Yhqx"] = this.getPowerFromYhqx(strUsername);
                    Session["Lsz"] = "";

                    //登陆信息写入数据库
                    string sqlSerachByYhid = "SELECT * FROM [yonghqx] WHERE yhid=@yhid";
                    DataTable dt = Sqlhelper.Serach(sqlSerachByYhid, new SqlParameter("yhid", strUsername));
                    try
                    {
                        if (dt.Rows.Count > 0)
                        {//如果已经存在于数据库，则只修改登陆次数 ,将用户组写入session
                            Session["Lsz"] = dt.Rows[0]["lsz"].ToString().TrimEnd(',');
                            //如果用户组为空,则赋予普通用户给用户
                            if (Session["Lsz"] == "") Session["Lsz"] = new c_log().getlsz("普通用户");
                            string[] strLszs = Session["Lsz"].ToString().Split(',');
                            if (Session["Lsz"].ToString() != "")
                            {
                                foreach (string strLsz in strLszs)
                                {
                                    XDocument zQx = this.getPowerFromZhuqx(strLsz);
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
                            //else
                            //{
                            //    Session["Yhqx"] = "";
                            //}
                            //权限验证
                            if (!powerYanzheng(Session["UserName"].ToString(), "用户管理", "登陆", "2"))
                            {
                                Response.Write("<script>alert('" + Session["Name"].ToString() + ":对不起,您无权登陆,请联系管理员或您的上级部门!!');history.go(-1)</script>");
                                //Response.End();
                                return false;

                            }
                            string sqlUpdate = "UPDATE yonghqx SET fwcs=@fwcs,dltime=@dltime,mm=@md5mm WHERE yhid=@yhid";// md5.MD5Encrypt(strPwd, md5.GetKey())
                            int fwcs = int.Parse(dt.Rows[0]["fwcs"].ToString()) + 1;
                            //写入数据库
                            Sqlhelper.ExcuteNonQuery(sqlUpdate, new SqlParameter("fwcs", fwcs.ToString()),
                                new SqlParameter("dltime", DateTime.Now.ToString()),
                                new SqlParameter("md5mm", md5.MD5Encrypt(strPwd, md5.GetKey())),
                                new SqlParameter("yhid", strUsername));
                        }
                        else
                        {//不存在,则新建一条记录 
                            string sqlInsert = "INSERT INTO  [yonghqx](guid,yhqx,lsz,yhid,xm,mm,uumzw,lxdh,dltime,fwcs) VALUES(@guid,@yhqx,@lsz,@yhid,@xm,@mm,@uumzw,@lxdh,@dltime,@fwcs)";
                            //密码来自用户输入
                            if (Sqlhelper.ExcuteNonQuery(sqlInsert,
                                new SqlParameter("guid", getUum.guid),
                                new SqlParameter("yhqx", "0"),
                                new SqlParameter("lsz", getUum.lsz),
                                new SqlParameter("yhid", getUum.yhid),
                                new SqlParameter("xm", getUum.xm),
                                new SqlParameter("mm", md5.MD5Encrypt(strPwd, md5.GetKey())),
                                new SqlParameter("uumzw", getUum.uumzw),
                                new SqlParameter("lxdh", getUum.lxdh),
                                new SqlParameter("dltime", DateTime.Now),
                                new SqlParameter("fwcs", 1)) > 0)
                                return true;
                            //权限验证
                            if (!powerYanzheng(Session["UserName"].ToString(), "用户管理", "登陆", "2"))
                            {
                                Response.Write("<script>alert('" + Session["Name"].ToString() + ":对不起,您是第一次登陆,请重新登陆一次!!');history.go(-1)</script>");
                                //Response.End();
                                return false;

                            }
                        }
                    }
                    catch
                    {
                        basic.MsgBox(this.Page, "登陆异常！", "-1");
                        return false;
                    }
                    //写入日志
                    try
                    {
                        if (IsCookie)
                        {
                            new c_log().logAdd("用户管理", "登陆", "使用COOKIE,UUM系统登陆");
                            // +",浏览器："+Request.Browser.Browser.ToString()+Request.Browser.Version.ToString()
                        }
                        else
                        {
                            new c_log().logAdd("用户管理", "登陆", "手动UUM系统登陆");
                        }
                    }
                    catch
                    {
                        basic.MsgBox(this.Page, "登陆写入日志异常!", "");
                        return false;
                    }
                    return true;
                }
                else
                {//未通过UUM，从本地库中尝试登陆
                    if (!IsCookie) strPwdByBenDiLogin = md5.MD5Encrypt(strPwd, md5.GetKey()); //如果不是Cookie登陆则需要加密
                    string strSqlBenDiLogin = "SELECT * FROM yonghqx WHERE yhid=@yhid AND mm=@mm";
                    try
                    {
                        DataTable dtBenDiLogin = Sqlhelper.Serach(strSqlBenDiLogin, new SqlParameter("yhid", strUsername), new SqlParameter("mm", strPwdByBenDiLogin));
                        if (dtBenDiLogin.Rows.Count == 1)
                        {
                            string LoginName = dtBenDiLogin.Rows[0]["xm"].ToString();
                            string LoginUerLsz = dtBenDiLogin.Rows[0]["lsz"] != null ? dtBenDiLogin.Rows[0]["lsz"].ToString() : "";
                            if (Cache)
                            {
                                HttpCookie mycookie = new HttpCookie("LoginUser");

                                mycookie.Values.Add("UserName", strUsername);
                                mycookie.Values.Add("Name", LoginName);
                                mycookie.Values.Add("Pwd", strPwdByBenDiLogin);

                                mycookie.Expires = DateTime.Now.AddDays(7);
                                HttpContext.Current.Response.AppendCookie(mycookie);
                            }

                            Session["UserName"] = strUsername;
                            Session["Name"] = LoginName;
                            Session["Yhqx"] = this.getPowerFromYhqx(strUsername) != null ? this.getPowerFromYhqx(strUsername).ToString() : "";
                            Session["Lsz"] = "";


                            string[] strLszs = LoginUerLsz.Split(',');
                            if (Session["Lsz"].ToString() == "")
                            {
                                foreach (string strLsz in strLszs)
                                {
                                    XDocument zQx = this.getPowerFromZhuqx(strLsz);
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
                            //else
                            //{
                            //    Session["Yhqx"] = "";
                            //}

                            //权限验证
                            if (!powerYanzheng(Session["UserName"].ToString(), "用户管理", "登陆", "2"))
                            {
                                Response.Write("<script>alert('" + Session["Name"].ToString() + ":对不起,您无权登陆,请联系管理员或您的上级部门!!');history.go(-1)</script>");
                                //Response.End();
                                return false;

                            }


                            string sqlUpdate = "UPDATE yonghqx SET fwcs=@fwcs,dltime=@dltime,mm=@md5mm WHERE yhid=@yhid";// md5.MD5Encrypt(strPwd, md5.GetKey())
                            int fwcs = int.Parse(dtBenDiLogin.Rows[0]["fwcs"].ToString()) + 1;
                            //写入数据库
                            Sqlhelper.ExcuteNonQuery(sqlUpdate, new SqlParameter("fwcs", fwcs.ToString()),
                                new SqlParameter("dltime", DateTime.Now.ToString()),
                                new SqlParameter("md5mm", strPwdByBenDiLogin),
                                new SqlParameter("yhid", strUsername));



                            //写入日志
                            try
                            {
                                if (IsCookie)
                                {
                                    new c_log().logAdd("用户管理", "登陆", "使用COOKIE,本地系统帐户登陆");
                                    // +",浏览器："+Request.Browser.Browser.ToString()+Request.Browser.Version.ToString()
                                }
                                else
                                {
                                    new c_log().logAdd("用户管理", "登陆", "手动本地系统帐户系统登陆");
                                }
                            }
                            catch
                            {
                                basic.MsgBox(this.Page, "登陆写入日志异常!", "");
                            }
                            return true;

                        }
                        else
                        {//本地数据中查不到用户信息
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
        //-----------------------------------------------------
    }

    /// <summary>
    /// 登陆验证
    /// </summary>
    /// <returns></returns>
    public bool Loginyanzheng()
    {
        //HttpCookie mycookie = Request.Cookies["LoginUser"];
        HttpCookie mycookie = HttpContext.Current.Request.Cookies["LoginUser"];
        if (Session["UserName"] != null && Session["Name"] != null)
            return true;
        else if (mycookie != null)
        {
            string UsernameCookie = mycookie.Values["UserName"];
            string PwdCookie = mycookie.Values["Pwd"];
            if (UsernameCookie != null && UsernameCookie != "" && PwdCookie != null && PwdCookie != "")
            {
                if (this.login(UsernameCookie, PwdCookie, false, true))
                {
                    return true;
                }
                else
                {//若修改密码后，cookie仍然存在，但还是无法正确登陆
                    //HttpContext.Current.Response.Write("<script language='javascript' type='text/javascript'>top.location.href='/Default.aspx';</script>");
                    HttpContext.Current.Response.RedirectLocation = "_top";
                    HttpContext.Current.Response.Redirect("/login.aspx");
                   
                    return false;
                }
            }
            else
            {
                HttpContext.Current.Response.RedirectLocation = "_top";
                HttpContext.Current.Response.Redirect("/login.aspx"); 
                return false;
            }            
        }
        else
        {
            HttpContext.Current.Response.RedirectLocation = "_top";
            HttpContext.Current.Response.Redirect("/login.aspx"); 
            return false;
        }
    }
    public bool Loginyanzhengurl(string webpage1)
    {
        //HttpCookie mycookie = Request.Cookies["LoginUser"];
        HttpCookie mycookie = HttpContext.Current.Request.Cookies["LoginUser"];
        if (Session["UserName"] != null && Session["Name"] != null)
            return true;
        else if (mycookie != null)
        {
            string UsernameCookie = mycookie.Values["UserName"];
            string PwdCookie = mycookie.Values["Pwd"];
            if (UsernameCookie != null && UsernameCookie != "" && PwdCookie != null && PwdCookie != "")
            {
                if (this.login(UsernameCookie, PwdCookie, false, true))
                {
                    return true;
                }
                else
                {//若修改密码后，cookie仍然存在，但还是无法正确登陆
                    //HttpContext.Current.Response.Write("<script language='javascript' type='text/javascript'>top.location.href='/Default.aspx';</script>");
                    HttpContext.Current.Response.RedirectLocation = "_top";
                    if (webpage1.Length > 0) { HttpContext.Current.Response.Redirect("/login.aspx?url="+webpage1); }
                    else
                    {
                        HttpContext.Current.Response.Redirect("/login.aspx");
                    }

                    return false;
                }
            }
            else
            {
                HttpContext.Current.Response.RedirectLocation = "_top";
                if (webpage1.Length > 0) { HttpContext.Current.Response.Redirect("/login.aspx?url=" + webpage1); }
                else
                {
                    HttpContext.Current.Response.Redirect("/login.aspx");
                }
                return false;
            }
        }
        else
        {
            HttpContext.Current.Response.RedirectLocation = "_top";
            if (webpage1.Length > 0) { HttpContext.Current.Response.Redirect("/login.aspx?url=" + webpage1); }
            else
            {
                HttpContext.Current.Response.Redirect("/login.aspx");
            }
            return false;
        }
    }
    /// <summary>
    /// 登陆验证
    /// </summary>
    /// <returns></returns>
    public bool Loginyanzheng(string url2)
    {
        //HttpCookie mycookie = Request.Cookies["LoginUser"];
        HttpCookie mycookie = HttpContext.Current.Request.Cookies["LoginUser"];
        if (Session["UserName"] != null && Session["Name"] != null)
            return true;
        else if (mycookie != null)
        {
            string UsernameCookie = mycookie.Values["UserName"];
            string PwdCookie = mycookie.Values["Pwd"];
            if (UsernameCookie != null && UsernameCookie != "" && PwdCookie != null && PwdCookie != "")
            {
                if (this.login(UsernameCookie, PwdCookie, false, true))
                {
                    return true;
                }
                else
                {//若修改密码后，cookie仍然存在，但还是无法正确登陆
                    //HttpContext.Current.Response.Write("<script language='javascript' type='text/javascript'>top.location.href='/Default.aspx';</script>");
                    HttpContext.Current.Response.RedirectLocation = "_top";
                    HttpContext.Current.Response.Redirect("/login.aspx?url="+url2);

                    return false;
                }
            }
            else
            {
                HttpContext.Current.Response.RedirectLocation = "_top";
                HttpContext.Current.Response.Redirect("/login.aspx?url=" + url2);
                return false;
            }
        }
        else
        {
            HttpContext.Current.Response.RedirectLocation = "_top";
            HttpContext.Current.Response.Redirect("/login.aspx?url=" + url2);
            return false;
        }
    }

    ///// <summary>
    ///// 获取用户权限
    ///// </summary>
    ///// <param name="Username">用户名</param>
    ///// <param name="LmKeyWord">关键字</param>
    ///// <returns>权限集合</returns>
    //public List<XElement> getPower(string Username,string LmKeyWord)
    //{
    //    List<XElement> listXElementGroup = new List<XElement>();//获取用户权限集合
    //    List<XElement> listXElementYhqx = new List<XElement>();//获取组权限集合
    //    List<XElement> listXElementTemp = null;
    //    string[] strLsz={};
    //    string SqlString = "SELECT * FROM yonghqx WHERE yhid=@yhid";
    //    try
    //    {
    //        DataTable dtQx = Sqlhelper.Serach(SqlString, new SqlParameter("yhid", Username));
    //        if (dtQx.Rows.Count == 1)
    //        {
    //            DataRow row = dtQx.Rows[0];
    //            string strYhqx = row["yhqx"].ToString();
    //            strLsz = row["lsz"].ToString().Split(',');
    //            XDocument xmlPowerYhqx = XDocument.Parse(strYhqx);
    //            if(xmlPowerYhqx.Elements("Root").Elements(LmKeyWord).Elements() != null)
    //                listXElementYhqx = xmlPowerYhqx.Elements("Root").Elements(LmKeyWord).Elements().ToList();
    //        }
    //        //获取用户组权限
    //        if (this.getPowerFromZhuqx(strLsz[0]) != null)
    //        {
    //            //foreach (var tempListXElementGroup in getPowerFromZhuqx(strLsz[0]))
    //            {
    //                //if (tempListXElementGroup.Name == LmKeyWord)
    //                    //listXElementGroup = tempListXElementGroup.Elements().ToList();
    //            }
    //        }
    //        if (listXElementGroup != null && listXElementYhqx != null)
    //        {
    //            listXElementTemp = new List<XElement>(listXElementYhqx);
    //            string s1 = "", s2 = "";
    //            for (int i = 0; i < listXElementGroup.Count; i++)
    //            {
    //                s1 = listXElementGroup[i].ToString();
    //                for (int j = 0; j < listXElementYhqx.Count; j++)
    //                {
    //                    s2 = listXElementYhqx[j].ToString();
    //                    if (s1 == s2)
    //                        break;
    //                    if (j == listXElementYhqx.Count - 1)
    //                        listXElementTemp.Add(listXElementGroup[i]);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            listXElementTemp = (listXElementGroup != null ? listXElementGroup : listXElementYhqx);
    //        }
    //        return listXElementTemp;
    //    }
    //    catch
    //    {
    //        return null;
    //    }

    //}



    /// <summary>
    /// 获取用户所有权限集合
    /// </summary>
    /// <param name="Username">用户名</param>
    /// <returns></returns>
    public XDocument getPowerFromYhqx(string Username)
    {
        string SqlString = "SELECT * FROM yonghqx WHERE yhid=@yhid";
        try
        {
            DataTable dtQx = Sqlhelper.Serach(SqlString, new SqlParameter("yhid", Username));
            if (dtQx.Rows.Count ==1)
            {
                DataRow row = dtQx.Rows[0];
                string strYhqx = row["yhqx"].ToString();
                XDocument xmlPowerYhqx = XDocument.Parse(strYhqx);
                if (xmlPowerYhqx.Elements("Root").Elements() != null)
                    //return xmlPowerYhqx.Elements("Root").Elements().ToList();
                    return xmlPowerYhqx;
                else
                    return XDocument.Parse("<Root></Root>");
            }
            else
            {
                return XDocument.Parse("<Root></Root>");
            }
        }
        catch
        {
            return  XDocument.Parse("<Root></Root>");
        }
    }

    /// <summary>
    /// 获取组权限
    /// </summary>
    /// <param name="zid">组ID</param>
    /// <returns></returns>
    public XDocument getPowerFromZhuqx(string zid)
    {
        string SqlString = "SELECT * FROM zhuqx WHERE zid=@zid";
        try
        {
            DataTable dtQx = Sqlhelper.Serach(SqlString, new SqlParameter("zid", zid));
            if (dtQx.Rows.Count > 0)
            {
                DataRow row = dtQx.Rows[0];
                string strZqx = row["zqx"].ToString();
                if (strZqx == "" || strZqx == null) return null;
                XDocument xmlPowerZqx = XDocument.Parse(strZqx);
                if (xmlPowerZqx.Elements("Root").Elements() != null)
                    //return xmlPowerZqx.Elements("Root").Elements().ToList();
                    return xmlPowerZqx;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }
    public bool powerYanzheng(string operation, string type)
    {
        return true;
    }

    /// <summary>
    /// 权限验证
    /// </summary>
    /// <param name="yhid">用户名</param>
    /// <param name="KeyWord">关键字</param>
    /// <param name="operation">待验证权限</param>
    /// <param name="type">1:只查询用户权限表,2:全部查询</param>
    /// <returns></returns>
    public bool powerYanzheng(string yhid, string KeyWord, string operation,string type)
    {
        try
        {
            //查询权限id
            string strSqlQxidSerach = "SELECT qxid FROM quanxdm WHERE qxmc=@qxmc";
            string strQxid = "";
            DataTable dtQxid = Sqlhelper.Serach(strSqlQxidSerach, new SqlParameter("qxmc", operation));
            if (dtQxid.Rows.Count > 0)
            {
                strQxid = dtQxid.Rows[0][0].ToString();
            }
            //查询是否验证
            string strSqlLmidSerach = "SELECT sfqxyz FROM lanm WHERE gjz=@gjz";
            string strSfqxyz = "";
            DataTable dtLmid = Sqlhelper.Serach(strSqlLmidSerach, new SqlParameter("gjz", KeyWord.Trim()));
            if (dtLmid.Rows.Count > 0)
            {
                foreach (DataRow row in dtLmid.Rows)
                {
                    strSfqxyz = row["sfqxyz"].ToString();
                }
            }
            //如果不需要验证，则直接通过验证
            if (!strSfqxyz.Contains(strQxid))
            {
                return true;
            }
            //验证是否为超级管理员
            //  string[] strLszs = dt.Rows[0]["lsz"].ToString().Split(',');
                            //if (Session["Lsz"].ToString() != "" || strLszs[0] != "" || Session["Yhqx"] != null)
                           // {
                              //  foreach (string strLsz in strLszs)
            try
            {
                if (Session["Lsz"].ToString() != "")
                {
                    string[] strLszs = Session["Lsz"].ToString().Split(',');
                    foreach (string strLsz in strLszs)
                    {
                        string sqlSerachByYhid = "SELECT zid,zmc FROM zhuqx WHERE zid=@zid";
                        DataTable dt1 = Sqlhelper.Serach(sqlSerachByYhid, new SqlParameter("zid", strLsz));

                        if (dt1.Rows.Count > 0)
                        {
                           
                                if (dt1.Rows[0]["zmc"].ToString() == "超级管理员")
                                {
                                    return true;
                                }
                         
                        }
                    }
                    
                }
            }
            catch
            {
                
            }

            switch (type)
            {
                //用户权限表中验证
                case "1":
                    //string strLsz = "";
                    string SqlString = "SELECT * FROM yonghqx WHERE yhid=@yhid";
                    DataTable dtQx = Sqlhelper.Serach(SqlString, new SqlParameter("yhid", yhid));
                    if (dtQx.Rows.Count > 0)
                    {
                        DataRow row = dtQx.Rows[0];
                        string strYhqx = row["yhqx"].ToString();
                        //strLsz = row["lsz"].ToString();
                        XDocument xml = XDocument.Parse(strYhqx);
                        if (xml.Element("Root").Element(KeyWord).Element(operation) != null)
                            return true;
                        else
                        {
                            //string sqlStrZqx = "SELECT * FROM zhuqx WHERE zid=@zid";
                            //DataTable dtZqx = Sqlhelper.Serach(sqlStrZqx, new SqlParameter("zid", strLsz));
                            //if (dtZqx.Rows.Count > 0)
                            //{
                            //    bool tap = false;
                            //    DataRow rowZqx = dtZqx.Rows[0];
                            //    string[] zqx = rowZqx["zqx"].ToString().Split(',');
                            //    foreach (string temp in zqx)
                            //    {
                            //        XDocument xmlTemp = XDocument.Parse(temp);
                            //        if (xmlTemp.Elements("Root").Elements(KeyWord).Elements(operation) != null)
                            //            tap = false;
                            //    }
                            //    return tap;
                            //}
                            //else
                            return false;
                        }
                    }
                    else
                        return false;

                //List<XElement> power = this.getPower(yhid, KeyWord);
                //bool tab = false;
                //if (power != null)
                //{
                //    for (int i = 0; i < power.Count; i++)
                //    {
                //        if (power[i].Name.ToString() == operation)
                //        {
                //            tab = true;
                //        }
                //    }
                //    return tab;
                //}
                //else
                //{
                //    return false;
                //}

                //session所有权限中验证
                case "2":
                    try
                    {
                        if (Session["Yhqx"] != null)
                        {
                            //string x = Session["Yhqx"].ToString();
                            XDocument xml = XDocument.Parse(Session["Yhqx"].ToString());
                            //运行到这里时,自动跳出....?
                            if (xml.Element("Root").Element(KeyWord).Element(operation) != null)
                                return true;
                        }
                        return false;

                    }
                    catch
                    {
                        return false;
                    }
                    

                default: return false;
            }
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// 权限验证（重载）
    /// </summary>
    /// <param name="zid">组权限ID</param>
    /// <param name="keyword">栏目模块名称</param>
    /// <param name="operation">操作（权限）名称</param>
    /// <returns></returns>
    public bool powerYanzheng(string zid, string keyword, string operation)
    {
        try
        {
            string sqlStrZqx = "SELECT * FROM zhuqx WHERE zid=@zid";
            DataTable dtZqx = Sqlhelper.Serach(sqlStrZqx, new SqlParameter("zid", zid));
            if (dtZqx.Rows.Count > 0)
            {
                DataRow rowZqx = dtZqx.Rows[0];
                string zqx = rowZqx["zqx"].ToString();
                XDocument xmlTemp = XDocument.Parse(zqx);
                if (xmlTemp.Element("Root").Element(keyword).Element(operation) != null)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 隶属组权限验证
    /// </summary>
    /// <param name="lsz">用户权限表中隶属组数组</param>
    /// <param name="keyword">栏目模块名称</param>
    /// <param name="operation">操作（权限）名称</param>
    /// <returns></returns>
    public bool powerYanzheng(string[] lsz, string keyword, string operation)
    {
        foreach (string zid in lsz)
        {
            try
            {
                string sqlStrZqx = "SELECT * FROM zhuqx WHERE zid=@zid";
                DataTable dtZqx = Sqlhelper.Serach(sqlStrZqx, new SqlParameter("zid", zid));
                if (dtZqx.Rows.Count > 0)
                {
                    DataRow rowZqx = dtZqx.Rows[0];
                    string zqx = rowZqx["zqx"].ToString();
                    XDocument xmlTemp = XDocument.Parse(zqx);
                    if (xmlTemp.Element("Root").Element(keyword).Element(operation) != null)
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }
        return false;
    }
    //public List<XElement> getPowerFromZhuqx(string[] zid)
    //{
    //    foreach (string temp in zid)
    //    { 
    //        string sqlStrZqx = "SELECT * FROM zhuqx WHERE zid=@zid";
    //        DataTable dtZqx = Sqlhelper.Serach(sqlStrZqx, new SqlParameter("zid", zid));
    //        if (dtZqx.Rows.Count > 0)
    //        {
    //            DataRow rowZqx = dtZqx.Rows[0];
    //            string zqx = rowZqx["zqx"].ToString();
    //            XDocument xmlTemp = XDocument.Parse(zqx);
    //        }
    //    }
    //    return null;
    //}
}   

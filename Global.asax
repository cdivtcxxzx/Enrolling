<%@ Application Language="C#" %>

<script runat="server">

    public static string zrlaiy = "新生报到系统";//记录来源 
    //读取webconfig中SQL连接字符串                
    public static string zrsql =ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString;//用来记录注入的SQL连接 读取配置文件
    public static string fwjlop = "0";//是否记录所有页面访问记录，1为开，0为不开启
    //需过滤的注入关键字
    public static string reurl = "http://www.cdivtc.com.cn/";//禁止访问后，能访问的站点
    public static string rename = "学院门户网站";//禁止访问后，能访问的站点的名称
    public static string word = "win.ini|boot.ini|concat|exec|insert|select|delete|update|master|truncate|declare|join|script|sysadmin|create|char|union|extractvalue|updatexml|xmlelement|<div style=\"display:none\">|substr|group by|unhex|(0x";
    public static string wzkey = "新生报到系统";
    public static string noset = "login.aspx|xw.aspx|/404/";//不进行验证的页面或地址login.aspx|xw.aspx
    public static string noserver = "/loginsf.aspx||/login.aspx|/nradmin";//网站关闭情况下能访问的地址/login.aspx|/admin/
    public static string noczip = "10.35.|118.114.252.|222.209.216.75|171.221.255.5|118.122.122.237";//白名单，除白名单外，还可以在库中noip表中对IP对应的no设置为“0”，tsxx设置为“不限制”
    //private void myTime(object sender, System.Timers.ElapsedEventArgs e)
    //{
    //    Application.Lock();            
    //    try {
    //        batch logic = new batch();
    //        logic.freshlog();//更新迎新学生事务日志
    //    }
    //    catch (Exception ex)
    //    {
            
    //    }
    //    Application.UnLock();
    //}
   

   
    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码
       // System.Timers.Timer timer = new System.Timers.Timer(1800*1000);//3600*1000一小时执行一次  
        //timer.Elapsed += new System.Timers.ElapsedEventHandler(myTime);
        //timer.AutoReset = true;
        //timer.Enabled = true;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }

   
    void Application_Error(object sender, EventArgs e) 
    {
        //在出现未处理的错误时运行的代码
        #region 在出现未处理的错误时运行的代码
    string errxx="未获取到错误！";
    string urlok ="未获取到地址";
    string username = HttpContext.Current.Request.UserHostAddress + DateTime.Now.ToString("yyMMddhhmmssfff");
    try{

        Exception ex = Server.GetLastError().GetBaseException(); 
        StringBuilder str = new StringBuilder(); 

      urlok = HttpContext.Current.Request.Url.Authority +HttpContext.Current.Request.Url.PathAndQuery;
  
   //获得当前页面
    try
    {
                //获得当前IP11
                    string ipok = HttpContext.Current.Request.UserHostAddress;
                    //HttpContext.Current.Request.Url.Authority +"|"+ HttpContext.Current.Request.Url;
                    //获得当前用户
                    
                    try
                    {
                        username = HttpContext.Current.Session["username"].ToString();

                    }
                    catch { }
                    string khd = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();
                    //写入数据库
                    try
                    {

                        zrupdate("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan],[khd]) VALUES(@sql,'" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff") + "','" + ipok + "',@urlok,'" + username + "','页面出错记录err" + "','" + zrlaiy + "','" + khd + "')", new System.Data.SqlClient.SqlParameter("sql", ex.Message), new System.Data.SqlClient.SqlParameter("urlok", urlok));
                       // System.Web.HttpContext.Current.Response.Write("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan],[khd]) VALUES('" + ex.Message + "','" + DateTime.Now.ToString() + "','" + ipok + "','@urlok','" + username + "','页面出错记录err" + "','新闻网站','" + khd + "')");
                       // System.Web.HttpContext.Current.Response.End();
                        //查询数据库中是否有该IP写入恶意访问记录
                        
                        
                    }
                    catch (Exception e1)
                    {
                    
                    }
    }
    catch(Exception e2)
    {
    }

    
    try
    {
        errxx = HttpUtility.HtmlEncode("|错误时间：" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "||问题提示：" + ex.Message + "||错误地址：" + HttpContext.Current.Request.Path + "||").Replace("&", "@@").Replace("#", "￥￥");
    }
    catch(Exception e44)
    {
    errxx=e44.Message;
    }
    }
    catch(Exception e55){}
    errxx = errxx.Replace("@@", "&").Replace("￥￥", "#");
        
        

    string tsxxw = "<!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta name=\"renderer\" content=\"webkit\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1, maximum-scale=1\"><meta name=\"apple-mobile-web-app-status-bar-style\" content=\"black\"><meta name=\"apple-mobile-web-app-capable\" content=\"yes\"><meta name=\"format-detection\" content=\"telephone=no\"><title>系统错误提示</title><META name=\"keywords\" content=\"" + wzkey + "\"> <META name=\"description\" content=\"" + wzkey + "\"></head><style>p {line-height:20px;}ul{ list-style-type:none;}li{ list-style-type:none;}a:link{text-decoration:none;}</style><body style=\" padding:0; margin:0; font:14px/1.5 Microsoft Yahei, 宋体,sans-serif; color:#555;\"><div style=\"margin: 0 auto; width:90%; padding-top:60px; overflow:hidden;\">  <div style=\"width:100%; float:left;\"> <div style=\" height:40px; line-height:40px; color:#fff; font-size:16px; overflow:hidden; background:#6bb3f6; padding-left:20px;\">网站错误提示信息 </div> <div style=\"border:1px dashed #cdcece; border-top:none; font-size:14px; background:#fff; color:#555; line-height:24px; height:220px; padding:20px 20px 0 20px; overflow-y:auto;background:#f3f7f9;\">";
    tsxxw += " <p style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\"><span style=\" font-weight:600; color:#fc4f03;\">";

    tsxxw += "您访问的页面出现错误!</span></p><br><span  id=\"errshow\"   style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\" ><p>" + errxx.Replace("|", "<br>").Replace("|", "<br>").Replace("|", "<br>").Replace("|", "<br>").Replace("|", "<br>").Replace("|", "<br>").Replace("d:\\soft\\gzy\\", "网站中:").Replace("e:\\", "网站中:").Replace("c:\\", "网站中:").Replace("d:\\", "网站中:").Replace("e:\\", "网站中:").Replace("f:\\", "网站中:") + "</p></span>";
    tsxxw += " </div>   </div>  </div></body></html>";
    System.Web.HttpContext.Current.Response.Write(tsxxw);
    System.Web.HttpContext.Current.Response.End();
        //Response.Redirect("/404/?username="+username+"&id="+errxx);
    #endregion
    }

    void Session_Start(object sender, EventArgs e) 
    {
        //在新会话启动时运行的代码
    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
    
    /// <summary>
    /// 通过传入的IP判断是否非法,非法后,会禁止用户访问，并记录访问数量
    /// 会判断网站是否开关维护
    /// </summary>
    /// <param name="ipjl">IP地址</param>
    ///　<returns>不返回数据</returns>
    public void ipjl(string ipjl)
    {
        try
        {
            //网站开关
            string urlok = HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.PathAndQuery;
            System.Data.DataTable isopen = zrSerach("SELECT TOP 1 *  FROM [wangzxx] where xxgjz='isopen' ");
            if (isopen.Rows.Count > 0)
            {
                if (isopen.Rows[0]["xxnr"].ToString() == "0")
                {
                    string jcnoserver = "noserver";
                    if(noserver.Split('|').Length>0)
                    {//读取关闭网站时仍需服务的地址
                        
                        for (int xt = 0; xt < noserver.Split('|').Length; xt++)
                        {
                            if (urlok.Contains(noserver.Split('|')[xt]))
                            {
                                jcnoserver = "ok";
                            }
                        }
                        
                    }
                    if (jcnoserver == "noserver")
                    {
                        string tsxxw = " <!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta name=\"renderer\" content=\"webkit\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1, maximum-scale=1\"><meta name=\"apple-mobile-web-app-status-bar-style\" content=\"black\"><meta name=\"apple-mobile-web-app-capable\" content=\"yes\"><meta name=\"format-detection\" content=\"telephone=no\"><title>系统提示</title><META name=\"keywords\" content=\"" + wzkey + "\"> <META name=\"description\" content=\"" + wzkey + "\"></head><style>p {line-height:20px;}ul{ list-style-type:none;}li{ list-style-type:none;}a:link{text-decoration:none;}</style><body style=\" padding:0; margin:0; font:14px/1.5 Microsoft Yahei, 宋体,sans-serif; color:#555;\"><div style=\"margin: 0 auto; width:90%; padding-top:60px; overflow:hidden;\">  <div style=\"width:100%; float:left;\"> <div style=\" height:40px; line-height:40px; color:#fff; font-size:16px; overflow:hidden; background:#6bb3f6; padding-left:20px;\">网站提示信息 </div> <div style=\"border:1px dashed #cdcece; border-top:none; font-size:14px; background:#fff; color:#555; line-height:24px; height:220px; padding:20px 20px 0 20px; overflow-y:auto;background:#f3f7f9;\">";
                        tsxxw += " <p style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\"><span style=\" font-weight:600; color:#fc4f03;\">";
                        tsxxw += "对不起,网站正在维护中,请稍侯再访问!!</span></p><br><p style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">";
                        tsxxw += "可能原因：系统管理员正在维护网站，暂时关闭了网站。</p><p style=\" margin-top:12px; margin-bottom:12px; margin-left:0px; margin-right:0px; -qt-block-indent:1; text-indent:0px;\">";
                        tsxxw += "如何解决：</p><ul style=\"margin-top: 0px; margin-bottom: 0px; margin-left: 0px; margin-right: 0px; -qt-list-indent: 1;\"><li style=\" margin-top:12px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">1）稍后再来访问本网站；</li><li style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">2）如果你是正常访问,此提示超过1小时，请及时联系网站管理员；</li></ul>";
                        tsxxw += " </div>   </div>  </div></body></html>";
                        System.Web.HttpContext.Current.Response.Write(tsxxw);
                        System.Web.HttpContext.Current.Response.End();
                    }
                    
                }
            }
            
            
            //非法访问检测
            
            string eyjl = "0";//默认为非法
            int iscz = 0;//记录是否有该IP在系统中了
            System.Data.DataTable ipcx = zrSerach("SELECT TOP 2 *  FROM [noip] where ip='" + ipjl + "' order by id");
            if (ipcx.Rows.Count > 0)
            {
                iscz = ipcx.Rows.Count;
                string errbh= ipcx.Rows[0]["id"].ToString();
                
                
                if (ipcx.Rows[0]["no"].ToString() == "0" && ipcx.Rows[0]["ts"].ToString()!="不限制")
                {
                    //非法访问次数>=10
                    System.Data.DataTable ipcxey = zrSerach("SELECT count(id)  FROM [sqllog] where len(username)>20 and bz like'%直接拦截%' and ip='" + ipjl + "'");
                    if (ipcxey.Rows.Count > 0)
                    {
                        if (ipcxey.Rows[0][0].ToString().Trim().Length > 1)
                        {
                            //System.Web.HttpContext.Current.Response.Write(ipcxey.Rows[0][0].ToString().Trim().Length.ToString());
                            eyjl = "1";//被拦截数据大于10
                        }
                    }
                    //访问不存在的页面或出错>=100
                    System.Data.DataTable ipcxcc = zrSerach("SELECT count(id)  FROM [sqllog] where len(username)>20 and bz like'%页面出错%' and ip='" + ipjl + "'");
                    if (ipcxcc.Rows.Count > 0)
                    {
                        if (ipcxcc.Rows[0][0].ToString().Trim().Length > 2)
                        {
                            //System.Web.HttpContext.Current.Response.Write(ipcxey.Rows[0][0].ToString().Trim().Length.ToString());
                            eyjl = "2";//错误操作100次
                        }
                    }
                }
                if (ipcx.Rows[0]["no"].ToString() == "1" && ipcx.Rows[0]["ts"].ToString() != "不限制")
                {
                    eyjl = "3";
                }
               
                //禁止访问
                if (eyjl == "3" || eyjl == "1" || eyjl == "2")
                {
                    //插入记录
                    string tsxx = ipcx.Rows[0]["ts"].ToString();
                    try
                    {
                        string khd = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();//获取浏览器信息
                        string username = HttpContext.Current.Request.UserHostAddress + DateTime.Now.ToString("yyMMddhhmmssfff"); //IP+时间
                        try
                        {
                            username = HttpContext.Current.Session["username"].ToString();

                        }
                        catch { }
                      
                        if (eyjl == "1")
                        {
                            tsxx = "由于您的非法访问超过10次,系统自动开启了禁止访问功能！";
                            zrupdate("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan],[khd]) VALUES(@sql,'" + DateTime.Now.ToString() + "','" + ipjl + "',@urlok,'"+username+"','开启禁止访问','" + zrlaiy + "','" + khd + "')", new System.Data.SqlClient.SqlParameter("sql", "系统操作：由于超过10条违规记录,开启禁止访问"), new System.Data.SqlClient.SqlParameter("urlok",urlok));
                        }
                        if (eyjl == "2")
                        {
                            // System.Web.HttpContext.Current.Response.Write("noip");
                            // System.Web.HttpContext.Current.Response.End();
                            tsxx = "由于您的访问操作超过100条出错记录,系统自动开启了禁止访问功能！";
                            zrupdate("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan],[khd]) VALUES(@sql,'" + DateTime.Now.ToString() + "','" + ipjl + "',@urlok,'" + username + "','开启禁止访问','" + zrlaiy + "','" + khd + "')", new System.Data.SqlClient.SqlParameter("sql", "系统操作：由于超过100条访问出错记录,开启禁止访问"), new System.Data.SqlClient.SqlParameter("urlok", urlok));
                        }
                        if (eyjl == "3")
                        {
                            tsxx = "系统已经禁止了您的访问！";
                        }
                        else
                        {
                            zrupdate("UPDATE [noip]   SET [sl] =[sl]+1,noipsl=noipsl+1,no='1',overtime='" + DateTime.Now.ToString() + "',laiy='" + zrlaiy + "',ts='" + tsxx + "' WHERE ip='" + ipjl + "'");
                        }
                    }
                    catch { }
                    
                    string tsxxw = "<!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta name=\"renderer\" content=\"webkit\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1, maximum-scale=1\"><meta name=\"apple-mobile-web-app-status-bar-style\" content=\"black\"><meta name=\"apple-mobile-web-app-capable\" content=\"yes\"><meta name=\"format-detection\" content=\"telephone=no\"><title>系统提示</title><META name=\"keywords\" content=\"" + wzkey + "\"> <META name=\"description\" content=\"" + wzkey + "\"></head><style>p {line-height:20px;}ul{ list-style-type:none;}li{ list-style-type:none;}a:link{text-decoration:none;}</style><body style=\" padding:0; margin:0; font:14px/1.5 Microsoft Yahei, 宋体,sans-serif; color:#555;\"><div style=\"margin: 0 auto; width:90%; padding-top:60px; overflow:hidden;\">  <div style=\"width:100%; float:left;\"> <div style=\" height:40px; line-height:40px; color:#fff; font-size:16px; overflow:hidden; background:#6bb3f6; padding-left:20px;\">网站提示信息 </div> <div style=\"border:1px dashed #cdcece; border-top:none; font-size:14px; background:#fff; color:#555; line-height:24px; height:220px; padding:20px 20px 0 20px; overflow-y:auto;background:#f3f7f9;\">";
                    tsxxw += " <p style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\"><span style=\" font-weight:600; color:#fc4f03;\">";
                   
                    tsxxw += "对不起,网站已经禁止了你[" + ipjl + "]的访问!编号[" + errbh + "]!</span></p><br><p style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">可能原因：" + tsxx + "</p><p style=\" margin-top:12px; margin-bottom:12px; margin-left:0px; margin-right:0px; -qt-block-indent:1; text-indent:0px;\">如何解决：</p><ul style=\"margin-top: 0px; margin-bottom: 0px; margin-left: 0px; margin-right: 0px; -qt-list-indent: 1;\"><li style=\" margin-top:12px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">1）你的IP地址已进入访问黑名单!请记下提示的IP地址；</li><li style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">2）若你是正常访问,请及时联系安全管理人员!</li><li style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">3）仍可以通过<a href='"+reurl+"'>点击此处访问"+rename+"</a>!</li></ul>";
                    tsxxw += " </div>   </div>  </div></body></html>";
                    System.Web.HttpContext.Current.Response.Write(tsxxw);
                    System.Web.HttpContext.Current.Response.End();
                }
                else
                {
                    zrupdate("UPDATE [noip]   SET [sl] =sl+1,overtime='" + DateTime.Now.ToString() + "',laiy='" + zrlaiy + "' WHERE ip='" + ipjl + "'");
                }
            }
            else
            {
                //插入记录
                zrupdate("INSERT INTO [noip]([ip],[laiy]) VALUES ('" + ipjl + "','"+zrlaiy+"')");
                
            }
        }
        catch(Exception e1)
        {
           
        }
    }
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
  
        try
        {
            //非法IP检测及拒绝访问
            string ipok1 = GetHostAddress();
            string noipxz = "noip";
            if (noczip.Split('|').Length > 0)
            {//读取白名单IP地址

                for (int xt = 0; xt < noczip.Split('|').Length; xt++)
                {
                    if (ipok1.Contains(noserver.Split('|')[xt]))
                    {
                        noipxz = "ok";
                    }
                }

            }
            if(noipxz=="noip")ipjl(ipok1); //当该IP不在白名单中时，进行验证
  
            //非法IP检测结束
        }catch{}
        
    //禁止爬虫
        //获得当前页面
        try
        {
            string pcstop = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();
            if (pcstop.Contains("yisouspider") || pcstop.Length<12) {
                Response.Write("<html><head><META charset=utf-8><title>"+wzkey+"</title><META name=\"keywords\" content=\"" + wzkey + "\"> <META name=\"description\" content=\"" + wzkey + "\"> </head><body><br> <font color=red>" + wzkey + "</font></body></html>");
             
                Response.End(); }
        }
        catch { }
        
             
        //wangzfwjl写入数据库
        #region 访问记录
        
        try
        {
            if (fwjlop == "1")
            {
                //获得当前IP
                string ipok = GetHostAddress();
                //获得当前页面
                string khd = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();

                string urlok = HttpContext.Current.Request.Url.Authority + ":" + HttpContext.Current.Request.Url.Port + HttpContext.Current.Request.Url.PathAndQuery;

                zrupdate("INSERT INTO [wangzfwjl] ([ur],[time],[ip],hkd,laiy) VALUES(@urlok,'" + DateTime.Now.ToString() + "','" + ipok + "','" + khd + "','" + zrlaiy + "')", new System.Data.SqlClient.SqlParameter("urlok", urlok));
                   
                //查询数据库中是否有该IP写入恶意访问记录

            }

        }
        catch
        {


        }
        #endregion




        string urlokjc = "未获取到地址";
        urlokjc = HttpContext.Current.Request.Url.PathAndQuery;
        
        //遍历Post参数，隐藏域除外 判断注入情况
        string postgetbl = "0";

        if (noset.Split('|').Length > 0)
            {//读取白名单IP地址

                for (int xt = 0; xt < noset.Split('|').Length; xt++)
                {
                    if (urlokjc.Contains(noset.Split('|')[xt]))
                    {
                        postgetbl = "ok";
                    }
                }

            }
        if (postgetbl == "0") //当该页在限制之列时，进行验证
        {
            foreach (string i in this.Request.Form)
            {
                if (i == "__VIEWSTATE") continue;
                if (i == "__EVENTVALIDATION") continue;
                this.goErr(this.Request.Form[i].ToString(), "form");
                //Response.Write("<br>form:" + this.Request.Form[i].ToString());

            }
            //遍历Get参数。
            foreach (string i in this.Request.QueryString)
            {

                    this.goErr(this.Request.QueryString[i].ToString(), "query");
                    //Response.Write("<br>get:" + this.Request.QueryString[i].ToString());

            }
        }
        
    }


    private void goErr(string tm,string rque)
    {
        string username = SqlFilter2(tm, rque);
        if ( username!= "false")
        {
           // Response.Write(username);


            string tsxxw = "<!DOCTYPE html><html><head><meta charset=\"utf-8\"><meta name=\"renderer\" content=\"webkit\"><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1, maximum-scale=1\"><meta name=\"apple-mobile-web-app-status-bar-style\" content=\"black\"><meta name=\"apple-mobile-web-app-capable\" content=\"yes\"><meta name=\"format-detection\" content=\"telephone=no\"><title>系统提示</title><META name=\"keywords\" content=\"" + wzkey + "\"> <META name=\"description\" content=\"" + wzkey + "\"></head><style>p {line-height:20px;}ul{ list-style-type:none;}li{ list-style-type:none;}a:link{text-decoration:none;}</style><body style=\" padding:0; margin:0; font:14px/1.5 Microsoft Yahei, 宋体,sans-serif; color:#555;\"><div style=\"margin: 0 auto; width:90%; padding-top:60px; overflow:hidden;\">  <div style=\"width:100%; float:left;\"> <div style=\" height:40px; line-height:40px; color:#fff; font-size:16px; overflow:hidden; background:#6bb3f6; padding-left:20px;\">网站提示信息 </div> <div style=\"border:1px dashed #cdcece; border-top:none; font-size:14px; background:#fff; color:#555; line-height:24px; height:220px; padding:20px 20px 0 20px; overflow-y:auto;background:#f3f7f9;\">";
            tsxxw += " <p style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\"><span style=\" font-weight:600; color:#fc4f03;\">";

            tsxxw += "对不起,网站已经禁止了你的非法操作!!<br><br>" + username + "</span></p><br><p style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">问题提示：[" + GetHostAddress() + "]请勿非法操作网站,如果10次非法访问,将禁止访问!||若您是正常操作出现此提示请与联系!</p><p>提示时间：" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "</p>";
            tsxxw += " </div>   </div>  </div></body></html>";
            System.Web.HttpContext.Current.Response.Write(tsxxw);
            System.Web.HttpContext.Current.Response.End();
            
            //Response.Redirect("/404/?id=" + HttpUtility.HtmlEncode("|错误时间：" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "||问题提示：[" + GetHostAddress() + "]请勿非法操作网站,如果10次非法访问,将禁止访问!||若您是正常操作出现此提示请与联系!") + "&username=" + username);
            //如果有恶意参数，跳转
            //Response.End();
            
        }
    }



    public static string SqlFilter2(string InText,string rque)
    {
        
        if (InText == null)
            return "false";
        try
        {
            foreach (string i in word.Split('|'))
            {
                //string.ToLower()是小写的意思  

                if (InText.ToString().Length > 200 || InText.ToLower().Contains(i) || (InText.ToLower().Contains("and") && InText.ToLower().Contains("or")))
                {
                    //写入日志
                    string fitmc = i.ToString() + "<br>";
                    if ((InText.ToLower().Contains("and") && InText.ToLower().Contains("or"))) fitmc += "andor";
                    if (InText.ToString().Length > 200) fitmc += "大于200字符";
                    fitmc += "<br>非法信息：" + InText.ToString() + "<br>";
                    //获得当前IP
                    string ipok = GetHostAddress();
                    //获得当前页面

                    string urlok = "未获取到地址";
                    urlok = HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.PathAndQuery;
                    string khd = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim() + "|" + HttpContext.Current.Request.Url.Port;
                    //HttpContext.Current.Request.Url.Authority +"|"+ HttpContext.Current.Request.Url;
                    //获得当前用户
                    string username = ipok + DateTime.Now.ToString("yyMMddhhmmssfff");


                    //写入数据库
                    try
                    {
                        zrupdate("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan],[khd]) VALUES(@sql,'" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff") + "','" + ipok + "',@urlok,'" + username + "','直接拦截" + rque + ":" + fitmc + "','" + zrlaiy + "','" + khd + "')", new System.Data.SqlClient.SqlParameter("sql", InText), new System.Data.SqlClient.SqlParameter("urlok", urlok));
                        return username + "<br>"+fitmc;
                    }
                    catch (Exception e)
                    {
                        throw e;

                    }
                    //Sqlhelper.ExcuteNonQuery("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan]) VALUES('" +InText + "','" + DateTime.Now.ToString() + "','" + ipok + "','" + urlok + "','" + username + "','request直接拦截','学院网站')");
                    //return "true";
                }
                else
                {
                    return "false";
                }

            }
        }
        catch (Exception eeee) { return "err:" + eeee.Message; }
        return "false";
    }
    public static System.Data.DataTable zrSerach(string sql, params System.Data.SqlClient.SqlParameter[] parameters)
    {
        //建立连接
        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(zrsql))
        {
            //打开连接
            try
            {
                conn.Open();
                using (System.Data.SqlClient.SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (System.Data.SqlClient.SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    System.Data.DataSet st = new System.Data.DataSet();
                    System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    adapter.Fill(st);
                    return st.Tables[0];
                }

            }
            catch (Exception e)
            {
                return new System.Data.DataTable();
            }
        }
    }
    public static void zrupdate(string sql, params System.Data.SqlClient.SqlParameter[] parameters)
    {
        try
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(zrsql))
            {
                conn.Open();
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn))
                {
                    foreach (System.Data.SqlClient.SqlParameter parameter in parameters)
                    {
                        object o = parameter.Value;
                        cmd.Parameters.Add(parameter);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch { }
    }
    /// <summary>
    /// 获取客户端IP地址（无视代理）
    /// </summary>
    /// <returns>若失败则返回回送地址</returns>
    public static string GetHostAddress()
    {
        string userHostAddress = HttpContext.Current.Request.UserHostAddress;

        if (string.IsNullOrEmpty(userHostAddress))
        {
            userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
        if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
        {
            return userHostAddress;
        }
        return "127.0.0.1";
    }

    /// <summary>
    /// 检查IP地址格式
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool IsIP(string ip)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
    }
</script>

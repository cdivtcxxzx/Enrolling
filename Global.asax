<%@ Application Language="C#" %>

<script runat="server">

    public static string zrlaiy = "新生报到系统";//记录来源
    public static string zrsql = "Data Source=118.114.252.173,20933;Initial Catalog=yxxt_data; User ID=sa;Password=123!@#qwe";//用来记录注入的SQL连接 
    public static string fwjlop = "0";//是否记录所有页面访问记录，1为开，0为不开启
    //需过滤的注入关键字
    //public static string word = "win.ini|boot.ini|concat|exec|insert|select|delete|update|master|truncate|declare|join|script|sysadmin|create|char|union|extractvalue|updatexml|xmlelement|<div style=\"display:none\">|substr|group by|unhex|(0x";
    public static string word = "";
    public static string wzkey = "新生报到系统";
   // public static string usernamea = "匿名a";

    private void myTime(object sender, System.Timers.ElapsedEventArgs e)
    {
        Application.Lock();            
        try {
            batch logic = new batch();
            logic.freshlog();//更新迎新学生事务日志
        }
        catch (Exception ex)
        {
            
        }
        Application.UnLock();
    }
   
   
    void Application_Start(object sender, EventArgs e) 
    {
        //在应用程序启动时运行的代码
        System.Timers.Timer timer = new System.Timers.Timer(1800*1000);//3600*1000一小时执行一次  
        timer.Elapsed += new System.Timers.ElapsedEventHandler(myTime);
        timer.AutoReset = true;
        timer.Enabled = true;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }

   
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码
    string errxx="未获取到错误！";
    string urlok ="未获取到";
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

        Response.Redirect("/404/?username="+username+"&id="+errxx);

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
                    
                    if (urlok.Contains("cdivtcadmingl") || urlok.Contains("cdivtczm"))
                    {

                    }
                    else
                    {

                       

     string tsxxw = " <html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>系统维护提示</title><META name=\"keywords\" content=\"" + wzkey + "\"> <META name=\"description\" content=\"" + wzkey + "\"></head><style>p {line-height:20px;}ul{ list-style-type:none;}li{ list-style-type:none;}a:link{text-decoration:none;}</style><body style=\" padding:0; margin:0; font:14px/1.5 Microsoft Yahei, 宋体,sans-serif; color:#555;\"><div style=\"margin: 0 auto; width:1000px; padding-top:70px; overflow:hidden;\">  <div style=\"width:300px; float:left; height:200px; background:url(/broswer_logo.jpg) no-repeat 100px 60px;\"></div> <div style=\"width:600px; float:left;\"> <div style=\" height:40px; line-height:40px; color:#fff; font-size:16px; overflow:hidden; background:#6bb3f6; padding-left:20px;\">网站安全提示信息 </div> <div style=\"border:1px dashed #cdcece; border-top:none; font-size:14px; background:#fff; color:#555; line-height:24px; height:220px; padding:20px 20px 0 20px; overflow-y:auto;background:#f3f7f9;\">";

     tsxxw += " <p style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\"><span style=\" font-weight:600; color:#fc4f03;\">对不起,网站正在维护中,请稍侯再访问!!</span></p><br><p style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">可能原因：系统管理员正在维护网站，暂时关闭了网站。</p><p style=\" margin-top:12px; margin-bottom:12px; margin-left:0px; margin-right:0px; -qt-block-indent:1; text-indent:0px;\">如何解决：</p><ul style=\"margin-top: 0px; margin-bottom: 0px; margin-left: 0px; margin-right: 0px; -qt-list-indent: 1;\"><li style=\" margin-top:12px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">1）稍后再来访问本网站；</li><li style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">2）如果你是正常访问,此提示超过1小时，请及时联系网站管理员；</li></ul>";

     tsxxw += " </div>   </div>  </div></body></html>";
                    
                        System.Web.HttpContext.Current.Response.Write(tsxxw);
                        System.Web.HttpContext.Current.Response.End();
                    }
                }
            }

            string eyjl = "0";

            System.Data.DataTable ipcx = zrSerach("SELECT TOP 1 *  FROM [noip] where ip='" + ipjl + "'");
            if (ipcx.Rows.Count > 0)
            {
                string errbh= ipcx.Rows[0]["id"].ToString();
                if (ipcx.Rows[0]["no"].ToString() == "0")
                {
                    //非法访问次数>=10
                    System.Data.DataTable ipcxey = zrSerach("SELECT count(id)  FROM [sqllog] where len(username)>20 and bz like'%直接拦截%' and ip='" + ipjl + "' and (ip not like '118.114.252%') and (ip not like '10.35.%') and (ip not like '222.209.216.75') and (ip not like '171.221.255.5') and (ip not like '118.122.122.237')");
                    if (ipcxey.Rows.Count > 0)
                    {
                        if (ipcxey.Rows[0][0].ToString().Trim().Length > 1)
                        {
                            //System.Web.HttpContext.Current.Response.Write(ipcxey.Rows[0][0].ToString().Trim().Length.ToString());
                            eyjl = "1";
                        }
                    }
                    //访问不存在的页面或出错>=100
                    System.Data.DataTable ipcxcc = zrSerach("SELECT count(id)  FROM [sqllog] where len(username)>20 and bz like'%页面出错%' and ip='" + ipjl + "' and (ip not like '118.114.252%') and (ip not like '10.35.%') and (ip not like '222.209.216.75') and (ip not like '171.221.255.5') and (ip not like '118.122.122.237')");
                    if (ipcxcc.Rows.Count > 0)
                    {
                        if (ipcxcc.Rows[0][0].ToString().Trim().Length > 2)
                        {
                            //System.Web.HttpContext.Current.Response.Write(ipcxey.Rows[0][0].ToString().Trim().Length.ToString());
                            eyjl = "2";
                        }
                    }
                }
                //
               // Response.Write("SELECT TOP 10 *  FROM [noip] where ip='" + ipjl + "'" + "id:" + ipcx.Rows[0]["id"].ToString() + "@" + "ipno:" + ipcx.Rows[0]["no"].ToString() + "IP:" + ipcx.Rows[0]["ip"].ToString() + "@" + ipcx.Rows[0]["sl"].ToString() + "@TS:" + ipcx.Rows[0]["ts"].ToString() + ipcx.Rows[0]["laiy"].ToString() + "@ip:" + ipjl + "@eyjl:" + eyjl + "<br>");
               
                //禁止访问
                if (ipcx.Rows[0]["no"].ToString() == "1" || eyjl == "1" || eyjl == "2")
                {
                   // Response.Write("ipno:" + ipcx.Rows[0]["no"].ToString() + "ip:" + ipjl + "eyjlx:" + eyjl + "<br>" + "SELECT TOP 1 *  FROM [noip] where ip='" + ipjl + "'");
                    //插入记录
                    string tsxx = ipcx.Rows[0]["ts"].ToString();
                    try
                    {
                        string khd = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();
                        string username = HttpContext.Current.Request.UserHostAddress + DateTime.Now.ToString("yyMMddhhmmssfff");
                        try
                        {
                            username = HttpContext.Current.Session["username"].ToString();

                        }
                        catch { }
                      
                        if (eyjl == "1")
                        {
                           // System.Web.HttpContext.Current.Response.Write("noip");
                           // System.Web.HttpContext.Current.Response.End();
                            
                            //System.Web.HttpContext.Current.Response.Write("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan],[khd]) VALUES(@sql,'" + DateTime.Now.ToString() + "','" + ipjl + "',@urlok,'" + username + "','开启禁止访问','" + zrlaiy + "','" + khd + "')");
                            tsxx = "由于您的非法访问超过10次,系统自动开启了禁止访问！";
                            zrupdate("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan],[khd]) VALUES(@sql,'" + DateTime.Now.ToString() + "','" + ipjl + "',@urlok,'"+username+"','开启禁止访问','" + zrlaiy + "','" + khd + "')", new System.Data.SqlClient.SqlParameter("sql", "系统操作：由于超过10条违规记录,开启禁止访问"), new System.Data.SqlClient.SqlParameter("urlok",urlok));
                        }
                        if (eyjl == "2")
                        {
                            // System.Web.HttpContext.Current.Response.Write("noip");
                            // System.Web.HttpContext.Current.Response.End();
                            tsxx = "由于您的访问超过100条访问出错记录,系统自动开启了禁止访问！";

                            //System.Web.HttpContext.Current.Response.Write("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan],[khd]) VALUES(@sql,'" + DateTime.Now.ToString() + "','" + ipjl + "',@urlok,'" + username + "','开启禁止访问','" + zrlaiy + "','" + khd + "')");

                            zrupdate("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan],[khd]) VALUES(@sql,'" + DateTime.Now.ToString() + "','" + ipjl + "',@urlok,'" + username + "','开启禁止访问','" + zrlaiy + "','" + khd + "')", new System.Data.SqlClient.SqlParameter("sql", "系统操作：由于超过100条访问出错记录,开启禁止访问"), new System.Data.SqlClient.SqlParameter("urlok", urlok));
                        }
                        zrupdate("UPDATE [noip]   SET [sl] =[sl]+1,noipsl=noipsl+1,no='1',overtime='" + DateTime.Now.ToString() + "',laiy='" + zrlaiy + "',ts='"+tsxx+"' WHERE ip='" + ipjl + "'");
                    }
                    catch { }
                   // System.Web.HttpContext.Current.Response.Write(ipcx.Rows[0]["ts"].ToString());
                    string tsxxw = " <html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><title>网站安全提示</title><META name=\"keywords\" content=\"" + wzkey + "\"> <META name=\"description\" content=\"" + wzkey + "\"></head><style>p {line-height:20px;}ul{ list-style-type:none;}li{ list-style-type:none;}a:link{text-decoration:none;}</style><body style=\" padding:0; margin:0; font:14px/1.5 Microsoft Yahei, 宋体,sans-serif; color:#555;\"><div style=\"margin: 0 auto; width:1000px; padding-top:70px; overflow:hidden;\">  <div style=\"width:300px; float:left; height:200px; background:url(/broswer_logo.jpg) no-repeat 100px 60px;\"></div> <div style=\"width:600px; float:left;\"> <div style=\" height:40px; line-height:40px; color:#fff; font-size:16px; overflow:hidden; background:#6bb3f6; padding-left:20px;\">网站安全提示信息 </div> <div style=\"border:1px dashed #cdcece; border-top:none; font-size:14px; background:#fff; color:#555; line-height:24px; height:320px; padding:20px 20px 0 20px; overflow-y:auto;background:#f3f7f9;\">";

                    tsxxw += " <p style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\"><span style=\" font-weight:600; color:#fc4f03;\">对不起,网站已经禁止了你[" + ipjl + "]的访问!编号["+errbh+"]!</span></p><br><p style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">可能原因：" + tsxx + "</p><p style=\" margin-top:12px; margin-bottom:12px; margin-left:0px; margin-right:0px; -qt-block-indent:1; text-indent:0px;\">如何解决：</p><ul style=\"margin-top: 0px; margin-bottom: 0px; margin-left: 0px; margin-right: 0px; -qt-list-indent: 1;\"><li style=\" margin-top:12px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">1）你的IP地址已进入访问黑名单!请记下提示的IP地址；</li><li style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">2）若你是正常访问,请及时联系安全管理人员!</li><li style=\" margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\">3）仍可以通过<a href='http://oa.cdivtc.com/login/login.aspx'>点击此处访问学院OA协同办公系统</a>!</li></ul>";

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
                //Response.Write("INSERT INTO [noip]([ip],[laiy]) VALUES ('" + ipjl + "','" + zrlaiy + "')");
                //Response.End();
            }
        }
        catch
        {
            //Response.Write("你访问的IP地址导致网站出错！");

        }
    }
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        //获得当前用户
       
        //Response.Write(usernamea);
        
        try
        {
            //非法IP检测及拒绝访问
            
            //Response.Write(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            string ipok1 = HttpContext.Current.Request.UserHostAddress;
            //Response.Write(ipok1);
            ipjl(ipok1);
            
           // ipjl("61.232.3.73");
  
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
                string ipok = HttpContext.Current.Request.UserHostAddress;
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
        if (urlokjc.ToLower().Contains("/oa.aspx") || urlokjc.ToLower().Contains("/oa.htmc") || urlokjc.ToLower().Contains("admingl"))
        {//排除oa文档
        }
        else
        {

            foreach (string i in this.Request.Form)
            {


                if (i == "__VIEWSTATE") continue;
                if (i == "__EVENTVALIDATION") continue;
                this.goErr(this.Request.Form[i].ToString(), "form");
                Response.Write("<br>form:" + this.Request.Form[i].ToString());

            }
            //遍历Get参数。
            foreach (string i in this.Request.QueryString)
            {
                if (urlokjc.ToLower().Contains("/404/"))
                {//排除404文档
                }
                else
                {
                    this.goErr(this.Request.QueryString[i].ToString(), "query");
                    Response.Write("<br>get:" + this.Request.QueryString[i].ToString());
                }
            }
        }
        
    }


    private void goErr(string tm,string rque)
    {
        string username = SqlFilter2(tm, rque);
        if ( username!= "false")
        {
            Response.Write(username);
            //Response.Redirect("/404/?id=" + HttpUtility.HtmlEncode("|错误时间：" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss") + "||问题提示：[" + HttpContext.Current.Request.UserHostAddress + "]请勿非法操作网站,如果10次非法访问,将禁止访问!||若您是正常操作出现此提示请与联系!") + "&username=" + username);
            //如果有恶意参数，跳转
            Response.End();
            
        }
    }



    public static string SqlFilter2(string InText,string rque)
    {
        
        if (InText == null)
            return "false";
        foreach (string i in word.Split('|'))
        {
            //string.ToLower()是小写的意思  

            if (InText.ToString().Length > 200 || InText.ToLower().Contains(i) || (InText.ToLower().Contains("and") && InText.ToLower().Contains("or")))
                {
                    //写入日志
                    string fitmc = i.ToString();
                    if((InText.ToLower().Contains("and")&& InText.ToLower().Contains("or")))fitmc="andor";
                    if (InText.ToString().Length > 200) fitmc = "大于200字符";
                    //获得当前IP
                    string ipok = HttpContext.Current.Request.UserHostAddress;
                    //获得当前页面

                    string urlok = "未获取到地址";
                    urlok = HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url.PathAndQuery;
                    string khd = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim()+"|" + HttpContext.Current.Request.Url.Port;
                    //HttpContext.Current.Request.Url.Authority +"|"+ HttpContext.Current.Request.Url;
                    //获得当前用户
                    string username = ipok+DateTime.Now.ToString("yyMMddhhmmssfff");
                    
                    
                    //写入数据库
                    try
                    {
                        zrupdate("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan],[khd]) VALUES(@sql,'" + DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff") + "','" + ipok + "',@urlok,'" + username + "','直接拦截" + rque + ":" + fitmc + "','" + zrlaiy + "','" + khd + "')", new System.Data.SqlClient.SqlParameter("sql", InText), new System.Data.SqlClient.SqlParameter("urlok", urlok));
                        return username;              
                    }
                    catch (Exception e)
                    {
                        throw e;

                    }
                    //Sqlhelper.ExcuteNonQuery("INSERT INTO [sqllog] ([sql],[time],[ip],[url],[username],[bz],[laiyuan]) VALUES('" +InText + "','" + DateTime.Now.ToString() + "','" + ipok + "','" + urlok + "','" + username + "','request直接拦截','学院网站')");
                    //return "true";
                }
            
        }
        return "false";
    }
</script>

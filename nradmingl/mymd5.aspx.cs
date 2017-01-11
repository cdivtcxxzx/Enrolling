using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Data.Common;

public partial class nradmingl_mymd5 : System.Web.UI.Page
{
    #region  建立MySql数据库连接
    /// <summary>
    /// 建立数据库连接.
    /// </summary>
    /// <returns>返回MySqlConnection对象</returns>
    public MySqlConnection getmysqlcon()
    {
        //http://sosoft.cnblogs.com/
        string M_str_sqlcon = String.Format("server={0};user id={1}; password={2}; port={3}; database=cms; pooling=false; charset=utf8", "10.35.10.182", "root", "123456", 3306); ; //根据自己的设置
        MySqlConnection myCon = new MySqlConnection(M_str_sqlcon);
        return myCon;
    }
    #endregion

    #region  执行MySqlCommand命令
    /// <summary>
    /// 执行MySqlCommand
    /// </summary>
    /// <param name="M_str_sqlstr">SQL语句</param>
    public void getmysqlcom(string M_str_sqlstr)
    {
        MySqlConnection mysqlcon = this.getmysqlcon();
        mysqlcon.Open();
        MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
        mysqlcom.ExecuteNonQuery();
        mysqlcom.Dispose();
        mysqlcon.Close();
        mysqlcon.Dispose();
    }
    #endregion

    #region  创建MySqlDataReader对象
    /// <summary>
    /// 创建一个MySqlDataReader对象
    /// </summary>
    /// <param name="M_str_sqlstr">SQL语句</param>
    /// <returns>返回MySqlDataReader对象</returns>
    public MySqlDataReader getmysqlread(string M_str_sqlstr)
    {
        MySqlConnection mysqlcon = this.getmysqlcon();
        MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
        mysqlcon.Open();
        MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
        return mysqlread;
    }
    #endregion
    private MySqlConnection conn;

    private DataTable data;

    private MySqlDataAdapter da;

    private MySqlCommandBuilder cb;

    private DataGrid dataGrid;


    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(HttpContext.Current.Request.Url.Host.ToString());
       string  webpage = Request.Url.GetLeftPart(UriPartial.Query).ToString().Replace(Request.Url.Port.ToString(), Sqlhelper.serverport);
       Response.Write("<br>" + webpage);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.TextBox2.Text == "4615914")
        {
            if (TextBox1.Text.Length > 0)
            {
                Label1.Text = basic.mymd5(TextBox1.Text.Trim());
            }
            else
            {
                Label1.Text = "值数字太少了!";
            }
        }
        else
        {
            Label1.Text = "密钥不正确!";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (basic.mymd5(TextBox3.Text.Trim()) == TextBox4.Text.Trim())
        {
            Label2.Text = "验证通过!";
        }
        else
        {
            Label2.Text = "验证失败!";
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (this.TextBox2.Text == "4615914")
        {
            if (TextBox1.Text.Length > 0)
            {
                Label1.Text = basic.mymd5m(TextBox1.Text.Trim());
            }
            else
            {
                Label1.Text = "值数字太少了!";
            }
        }
        else
        {
            Label1.Text = "密钥不正确!";
        }
    }
    protected void Button41_Click(object sender, EventArgs e)
    {
        if (Sqlhelper.ExcuteNonQuery("update [wangzxx] set xxnr='" + this.TextBox5.Text.Trim() + "' where xxgjz='系统授权'") > 0)
        {
            Label2.Text = "写入授权成功!";
        }
        else
        {
            Label2.Text="写入授权失败!";
        }
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        this.TextBox5.Text =basic.systemmd5(TextBox6.Text);
        //this.TextBox5.Text = basic.mymd5key(TextBox6.Text, TextBox7.Text);
    }
    protected void Button133_Click(object sender, EventArgs e)
    {
        //Response.Write( jm("x", "y", 5));
        //string jmok = System.Security.Cryptography.SHA1(TextBox6.Text);
        //建立SHA1对象
        //<5"vtJ?sOGLK.]38H@*D!;=~VZ-^q$Pn&0CY4(kQ

      //  this.Label1.Text = Sha1Sign(TextBox6.Text.Trim());
      // string hash= basic.systemmd5(SHA1(TextBox6.Text)+"ThinkUCenter");

        //Hash运算
        
      // this.Label1.Text=hash;
        string hash2 = basic.systemmd5(Sha1Sign(TextBox6.Text.Trim()) + this.TextBox7.Text.Trim());
        this.TextBox5.Text = hash2;

    }
    /// <summary>
    /// sha1 加密，与php加密结果一样
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string Sha1Sign(string data)
    {
        byte[] temp1 =System.Text.Encoding.UTF8.GetBytes(data);
        SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
        byte[] temp2 = sha.ComputeHash(temp1);
        sha.Clear();            // 注意， 不能用这个           
        // string output = Convert.ToBase64String(temp2);// 不能直接转换成 base64string            
        var output = BitConverter.ToString(temp2);
        output = output.Replace("-", "");
        output = output.ToLower();
        return output;
    }
    protected string jm(string data, string key, int expire)
    {
        //         * 系统加密方法
// * @param string $data 要加密的字符串
// * @param string $key  加密密钥
// * @param int $expire  过期时间 (单位:秒)
// * @return string 
// */
//function think_ucenter_encrypt($data, $key, $expire = 0) {
//    $key  = md5($key);

        key = basic.systemmd5("<5\"vtJ?sOGLK.]38H@*D!;=~VZ-^q$Pn&0CY4(kQ");

//    $data = base64_encode($data);
        data = basic.systembase64(TextBox6.Text);
//    $x    = 0;
        int x = 0;
//    $len  = strlen($data);
       int len = data.Length;
//    $l    = strlen($key);
       int l = key.Length;
//    $char =  '';
        string char1="";
//    for ($i = 0; $i < $len; $i++) {
//        if ($x == $l) $x=0;
//        $char  .= substr($key, $x, 1);
//        $x++;
//    }
        for (int i = 0; i < len; i++)
        {
            if(x==l)x=0;
            char1=key.Substring(x,1);
            x++;
        }
        //    $str = sprintf('%010d', $expire ? $expire + time() : 0);        $expire = 0)
        long str1 = 0;
         if (expire != 0)
         {
             //str1 = expire + DateTime.Now.Ticks;
             DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳  
             str1=expire+( (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000);  //注意这里有时区问题，用now就要减掉8个小时  
             
         }
       
//    for ($i = 0; $i < $len; $i++) {
//        $str .= chr(   ord(substr($data,$i,1)) + (ord(substr($char,$i,1)))   %256   );
//    }
         for (int i = 0; i < len; i++)
         {
             string str2=data.Substring(i,1);
             byte[] array = System.Text.Encoding.ASCII.GetBytes(str2);
            int asciicode = (int)(array[0]); //将str2转为ASCII
            int asciicode2 = 0;
            try
            {
                string str3 = char1.Substring(i, 1);
                byte[] array2 = System.Text.Encoding.ASCII.GetBytes(str3);
                 asciicode2 = (int)(array2[0]); //将str3转为ASCII
                
            }
            catch {
                byte[] array2 = System.Text.Encoding.ASCII.GetBytes(" ");
                asciicode2 = (int)(array2[0]); //将str3转为ASCII
            }
            str1 += asciicode + asciicode2 % 256;

                
         }
         return basic.systembase64(str1.ToString()).Replace("=","");
//    return str_replace('=', '', base64_encode($str));
//}

    }
    protected void Button41t_Click(object sender, EventArgs e)
    {
        string username = "zhangming1";
        string password1 = "admin888";
        string name = "";
        string mobile = "";
        string xueyuan = "";
        this.Label3.Text = "";
        this.Label4.Text = "";
        int countuser = 0;
        int countcs = 0;
        int countcrsb = 0;
        int countupdatesb = 0;
        int countupdate = 0;
        //获取UUM帐户
        DataTable uum = Sqlhelper.uumSerach("SELECT  [Name],[UserName],[Idcard], convert(nchar, [Password]) password,[MobilePhone],DepartmentName  FROM [v_user] where username<>'admin'");
        if (uum.Rows.Count > 0)
        {
            for (int x = 0; x < uum.Rows.Count; x++)
            {
                //清空上一条数据
                username = ""; password1 = ""; name = ""; mobile = ""; xueyuan = "";
                //写入当前数据
                username = uum.Rows[x]["UserName"].ToString().Trim();
                //password1 = uum.Rows[x]["Password"].ToString().Trim();
                password1 = basic.systemmd5(Sha1Sign(uum.Rows[x]["Password"].ToString().Trim()) + this.TextBox7.Text.Trim());
               // if (username == "weixi") this.TextBox5.Text = uum.Rows[x]["Password"].ToString().Trim() + "@" + password1;
                name = uum.Rows[x]["Name"].ToString().Trim();
                mobile = uum.Rows[x]["MobilePhone"].ToString().Trim();
                xueyuan = uum.Rows[x]["DepartmentName"].ToString().Trim();
                #region 向影画工职更新数据


                try
                {
                    //设置MYSQL库
                    if (conn != null) conn.Close();
                    string M_str_sqlcon = String.Format("server={0};user id={1}; password={2}; port={3}; database=cms; pooling=false; charset=utf8", "10.35.10.182", "root", "123456", 3306); ; //根据自己的设置
                    try
                    {
                        conn = new MySqlConnection(M_str_sqlcon);
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        Label4.Text += "连接MY SQL出错:" + ex.Message;
                    }
                   

                    string sqlupdate = "";

                   // string passwordok = basic.systemmd5(Sha1Sign(password1) + this.TextBox7.Text.Trim());
                    string sql = "SELECT * FROM cms_ucenter_member  where username='" + username + "'";
                    //Response.Write(sql);
                    DataTable data = new DataTable();
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;

                        DataSet st = new DataSet();
                        MySql.Data.MySqlClient.MySqlDataAdapter adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
                        //adapter.SelectCommand = cmd;
                        adapter.Fill(data);

                    }
                    string iscr = "1";
                    string sqlfzok = "";
                    if (data.Rows.Count > 0)
                    {
                        //获得更新用户分组

                        DataTable datafz = new DataTable();
                        string sqlfz = "select * from cms_auth_group_access where uid='" + data.Rows[0]["id"].ToString() + "'";
                        using (MySql.Data.MySqlClient.MySqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = sqlfz;

                            DataSet st = new DataSet();
                            MySql.Data.MySqlClient.MySqlDataAdapter adapter = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd);
                            //adapter.SelectCommand = cmd;
                            adapter.Fill(datafz);

                        }
                        if (datafz.Rows.Count > 0)
                        {
                        }
                        else
                        {
                            //创建分组
                            sqlfzok = "INSERT INTO cms_auth_group_access(uid,group_id)VALUES('" + data.Rows[0]["id"].ToString() + "','1')";
                        }
                        //更新
                        iscr = "0";
                        sqlupdate = "update cms_ucenter_member set password='" + password1 + "',mobile='" + mobile + "',status='1' where username='" + username + "';"+sqlfzok+"";
                        this.Label3.Text += "<br><font color=green>"+name+"[" + username + "]已存在,更新资料";
                        countupdate++;
                        
                        //"["+password1+"]"+sqlupdate+"
                    }
                    else
                    {
                        countcs++;
                        sqlupdate = "INSERT INTO cms_ucenter_member(username,password,mobile,status,isregister)VALUES('" + username + "','" + password1 + "','" + mobile + "','1','1');INSERT INTO cms_member(nickname,school,status,isregister)VALUES('" + name + "','" + xueyuan + "','1','1');";
                        this.Label3.Text += "<br><font color=blue>插入新用户"+name+"[" + username + "]";
                    }
                    int cgsl = 0;
                    using (MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlupdate, conn))
                    {
                        int affectedRows = cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        cmd.Connection.Dispose();
                        cgsl = affectedRows;
                    }
                    if (cgsl > 0)
                    {
                        countuser++;

                        this.Label3.Text += "</font><font color=green>成功!</font>";
                    }
                    else
                    {
                        if (iscr == "0")
                        {
                            countupdatesb++;
                        }
                        else
                        {
                            countcrsb++;
                        }
                        this.Label3.Text += "</font><font color=red>失败!</font>";
                    }

                    //getmysqlcom("INSERT INTO cms_ucenter_member(username,password)VALUES('zhangming4','111111')");
                }

                catch (MySqlException ex1)
                {

                    this.Label3.Text += "<br>数据操作错误: " + ex1.Message;

                }
                #endregion

            }
            Label4.Text+="共操作数据:"+uum.Rows.Count.ToString()+"条数据,写入:"+countcs.ToString()+"(失败:"+countcrsb+"),更新:"+countupdate+"(失败:"+countcrsb+")";
        }
        else
        {
            Label4.Text += "<br>UUM中暂无数据需更新或连接失败!";
        
        }

        
    }

    protected void Button41_Clickurl(object sender, EventArgs e)
    {
        string webtime = basic.WebTime();   //时间参数
        string webpwd =basic.WebPwd("zhangming1",webtime); //密钥
        string webusername = "zhangming1"; //登陆帐号
        string appname = "接口测试平台";//平台名称
        string webToUrl = "";  //跳转地址
        string Operate = "1";  //要执行的操作  默认为空或等于1为登陆，2为注销退出系统
        string token = basic.systembase64("{'WebUserName':'" + webusername + "','WebPwd':'" + webpwd + "','WebTime':'" + webtime + "','WebToUrl':'" + this.TextBox9.Text.Trim() + "','AppName':'" + appname + "','Operate':'" + this.TextBox10.Text+"'}");
        this.TextBox8.Text = this.TextBox8.Text.Trim().Split('?')[0] + "?token=" + HttpUtility.UrlEncode(token);
        this.Label3.Text = "<font color=green>WebUserName:" + webusername;
        this.Label3.Text += "<br>WebPwd:" + webpwd;
        this.Label3.Text += "<br>WebTime:" + webtime;
        this.Label3.Text += "<br>WebToUrl:"+this.TextBox9.Text;
        this.Label3.Text += "<br>AppName:" + appname;
        this.Label3.Text += "<br>Operate:" + this.TextBox10.Text;
        
        //this.Label3.Text += "<br>BASE64转码前:" + "{'WebUserName':'" + webusername + "','WebPwd':'" + webpwd + "','WebTime':'" + webtime + "','WebToUrl':'','AppName':'" + appname + "'}";
        this.Label3.Text += "<br>BASE64转码前:" + "{'WebUserName':'" + webusername + "','WebPwd':'" + webpwd + "','WebTime':'" + webtime + "','WebToUrl':'" + this.TextBox9.Text.Trim() + "','AppName':'" + appname + "','Operate':'" + this.TextBox10.Text+"'}";

        this.Label3.Text += "<br>最终传递token:<br>" +token + "";

        this.Label3.Text += "<br>解晰token:<br>" + basic.base64_dep(token) + "</font>";
        if (Request["token"] != null)
        {
            string jwebtime = "";
            string jwebpwd = "";
            string jwebusername = "";
            string jappname = "";
            string jwebtourl = "";
            string joperate = "";
            //单点登陆
            string okstr = basic.base64_dep(Request["token"].ToString()).Replace("'", "").Replace("{", "").Replace("}", "");
            //Response.Write(okstr);
            //Response.End();
            string[] strok = okstr.Split(',');
            foreach (string str1 in strok)
            {
                string[] cs = str1.Split(':');
                switch (cs[0].ToString().ToLower())
                {
                    
                    case "webusername":
                        jwebusername = cs[1].ToString();
                        break;
                    case "webpwd":
                        jwebpwd = cs[1].ToString();
                        break;
                    case "webtime":
                        jwebtime = cs[1].ToString();
                        break;
                    case "webtourl":
                        jwebtourl = cs[1].ToString();
                        break;
                    case "operate":
                        joperate = cs[1].ToString();
                        break;
                   default:
                        break;
                }
               
            }

            string webpwd1 = basic.WebPwd(webusername, webtime);
            if (webpwd1 == jwebpwd)
            {
                //认证通过,允许登陆
                this.Label3.Text += "<br>单点登陆认证通过！";
                switch (joperate)
                {

                    case "2":
                        this.Label3.Text += "此次为注销退出事件！";
                        break;
                   
                    default:
                        this.Label3.Text += "此次为登陆事件！"+jwebusername;
                        break;
                }
                if (jwebtourl!="")
                {
                    //跳转地址
                    //Response.Redirect(jwebtourl);
                }
               
            }
        }

//        传递的参数
//Token=加密串
//加入参数：
//WebUserName: 用户登陆ID(必传)
//WebPwd: 单点登陆加密后的动态统一密钥（必传）
//WebTime:加密时的时间（必传）
//WebToUrl: 需转到的页面，如果为空为登陆后默认页（可不传）
//AppName:应用来源，名称
//使用上述参数生成Token
//Token加密算法：
//用户动态密钥：用户ID(WebUserName)  以zhangming1举例
//固定动态密钥：cdivtc    时间动态密钥：年月日+当前小时分钟秒数
//例2016年05月07日 09:49:10，密钥为：20160507094910
//WebPwd加密方式：字符串zhangming1+cdivtc+20160507094910进行32位MD5加密传递,最终WebPwd值bdda0c238d18466963a52bd8a20a049d
//Token=Base64(“{‘WebUserName’:’zhangming1’,’WebPwd’:’ bdda0c238d18466963a52bd8a20a049d’,’WebTime’:’20160507094910’,’WebToUrl’:’’,’AppName’,’OA门户’}”);

    }
}
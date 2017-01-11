using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///c_log 的摘要说明
/// </summary>
public class c_log:System.Web.UI.Page
{
	public c_log()
	{
	}
    /// <summary>
    /// 日志写入
    /// </summary>
    /// <param name="lmName">页面或栏目名称</param>
    /// <param name="qxName">权限名称或操作项</param>
    /// <param name="neirong">日志详细内容或说明</param>
    /// <returns></returns>
    public int logAdd(string lmName,string qxName,string neirong)
    {
       


        string lmID="-1", qxID="-1";
        string lmSqlString = "SELECT lmid,sfjlrz,gjz FROM lanm WHERE gjz=@gjz";
        string qxSqlString = "SELECT qxid FROM quanxdm WHERE qxmc=@qxmc";
        string lsz = "0";
        string userName = "";
        if (Session["Lsz"] != null && Session["UserName"] != null)
        {
            lsz = Session["Lsz"].ToString().Split(',')[0];
            userName = Session["UserName"].ToString();
        }
      
        try
        {
            DataTable lmDt = Sqlhelper.Serach(lmSqlString, new SqlParameter("gjz", lmName));
            DataTable qxDt = Sqlhelper.Serach(qxSqlString, new SqlParameter("qxmc", qxName));
            if (lmDt.Rows.Count > 0 && qxDt.Rows.Count > 0)
            {
                if (lmDt.Rows[0]["sfjlrz"].ToString() == "1")
                {
                    lmID = lmDt.Rows[0][0].ToString();//获取栏目ID 
                    //判断是否需要写日志
                    if (lmDt.Rows[0][1].ToString() == "0")
                    {
                        return 0;
                    }

                    if (lsz == "") lsz = getlsz("普通用户");
                    qxID = qxDt.Rows[0][0].ToString();//获取权限ID
                    string time = DateTime.Now.ToString();
                    try
                    {
                        //如果为登陆,则记录浏览器信息!
                        if (qxName == "登陆")
                        {
                            neirong = neirong + ",浏览器:" + System.Web.HttpContext.Current.Request.Browser.Browser.ToString() + System.Web.HttpContext.Current.Request.Browser.Version.ToString();
                        }
                    }
                    catch(Exception ex)
                    {
                        neirong = neirong + ex.Message;
                    }
                    string cznr = "【" + this.getNameByUserName(userName) + "】在【" + lmName + "】执行【" + qxName + "】操作：" + neirong;

                    string loginIP = HttpContext.Current.Request.UserHostAddress;
                    string logInsertSqlString = "INSERT INTO rizlog(time,lx,lm,userid,usergroup,ip,cznr) VALUES(@time,@lx,@lm,@userid,@usergroup,@ip,@cznr)";
                    return Sqlhelper.ExcuteNonQuery(logInsertSqlString,
                        new SqlParameter("time", time),
                        new SqlParameter("lx", qxID),
                        new SqlParameter("lm", lmID),
                        new SqlParameter("userid", userName),
                        new SqlParameter("usergroup", lsz),
                        new SqlParameter("ip", loginIP),
                        new SqlParameter("cznr", cznr));
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        catch
        { 
            return 0;
        }
    }

    /// <summary>
    /// 日志分类记录
    /// </summary>
    /// <param name="lmName">页面或栏目名称</param>
    /// <param name="qxName">权限名称或操作项</param>
    /// <param name="neirong">日志详细内容或说明</param>
    /// <param name="lx">日志类型,1:普通日志,2:错误日志</param>
    /// <param name="username">操作人员ID </param>
    /// <returns>成功标志</returns>
    public int logAdd(string lmName, string qxName, string neirong,string lx,string username)
    {
        //功能描述:分类记录系统操作日志
        //作者:张明
        //更新日期:2016年11月27日
        try
        {
        string lmID="-1", qxID="-1";
        string lmSqlString = "SELECT lmid,sfjlrz,gjz FROM lanm WHERE gjz=@gjz";
        string qxSqlString = "SELECT qxid FROM quanxdm WHERE qxmc=@qxmc";
        string cznr = "【" + username + "】在【" + lmName + "】执行【" + qxName + "】操作：" + neirong;
        DataTable lmDt = Sqlhelper.Serach(lmSqlString, new SqlParameter("gjz", lmName));
        DataTable qxDt = Sqlhelper.Serach(qxSqlString, new SqlParameter("qxmc", qxName));
            if (lmDt.Rows.Count > 0 && qxDt.Rows.Count > 0)
            {
                 lmID = lmDt.Rows[0][0].ToString();//获取栏目ID 
                 qxID = qxDt.Rows[0][0].ToString();//获取权限ID
            }
                    string loginIP = HttpContext.Current.Request.UserHostAddress;
                    string logInsertSqlString = "INSERT INTO rizlog(time,lx,lm,userid,usergroup,ip,cznr) VALUES(@time,@lx,@lm,@userid,@usergroup,@ip,@cznr)";
                    return Sqlhelper.ExcuteNonQuery(logInsertSqlString,
                        new SqlParameter("time", DateTime.Now.ToString()),
                        new SqlParameter("lx", qxID),
                        new SqlParameter("lm", lmID),
                        new SqlParameter("userid", username),
                        new SqlParameter("usergroup", "0"),
                        new SqlParameter("ip", loginIP),
                        new SqlParameter("cznr", cznr));
                        
        }
        catch
        {
            return 0;
        }
    }
    protected string getNameByUserName(string UserName)
    {
        string sqlString = "SELECT xm FROM yonghqx WHERE yhid=@yhid";
        try
        {
            DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("yhid", UserName));
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "未知用户";
            }
        }
        catch
        {
            return "未知用户";
        }
    }
    public string getlsz(string zhuname)
    {
        string sqlString = "SELECT zid FROM zhuqx WHERE zmc=@zmc";
        try
        {
            DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("zmc", zhuname));
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "13";
            }
        }
        catch
        {
            return "13";
        }
    }
}
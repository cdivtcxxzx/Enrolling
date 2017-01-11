using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
///Message 的摘要说明
/// </summary>
public class Message
{
	public Message()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public DataTable GetAll(string recid)
    {
        return Sqlhelper.Serach("select row_number() over (order by readed,id desc) as uid,id,title,url,sender,sendtime from (select sendtime,readed,id,case when readed='未读' then '<b>'+title+'</b><font color=red>New</font>' else title end as title,url,case when type='system' then '系统消息' else xm end as sender from message left join yonghqx on message.senderid=yonghqx.yhid where recid=@recid ) t order by readed,id desc", new SqlParameter("recid", recid));
    }
    public DataTable GetNew(string recid)
    {
        return Sqlhelper.Serach("select row_number() over (order by readed,id desc) as uid,id,title,url,sender,sendtime from (select sendtime,readed,id,'<b>'+title+'</b><font color=red>New</font>' title,url,case when type='system' then '系统消息' else xm end as sender from message left join yonghqx on message.senderid=yonghqx.yhid where recid=@recid and readed='未读') t", new SqlParameter("recid", recid));
    }
    /// <summary>
    /// 获取系统消息空表
    /// </summary>
    /// <returns></returns>
    public DataTable GetSystemMessageScheme()
    {
        DataTable dt = new DataTable("message");
        dt.Columns.Add("recid", typeof(string));
        dt.Columns.Add("title", typeof(string));
        dt.Columns.Add("detail", typeof(string));
        dt.Columns.Add("url", typeof(string));
        dt.Columns.Add("sendtime", typeof(DateTime));
        dt.Columns.Add("type", typeof(string));
        dt.Columns.Add("readed", typeof(string));
        return dt;
    }
    /// <summary>
    /// 获取普通消息空表
    /// </summary>
    /// <returns></returns>
    public DataTable GetNormalMessageScheme()
    {
        DataTable dt = new DataTable("message");
        dt.Columns.Add("recid", typeof(string));
        dt.Columns.Add("senderid", typeof(string));
        dt.Columns.Add("title", typeof(string));
        dt.Columns.Add("detail", typeof(string));
        dt.Columns.Add("sendtime", typeof(DateTime));
        dt.Columns.Add("type", typeof(string));
        dt.Columns.Add("url", typeof(string));
        dt.Columns.Add("readed", typeof(string));
        return dt;
    }
    /// <summary>
    /// 发送系统消息
    /// </summary>
    /// <param name="recid">收件人id集合</param>
    /// <param name="title">消息标题</param>
    /// <param name="detail">消息内容</param>
    /// <param name="url">消息链接</param>
    /// <param name="sendtime">发送时间</param>
    /// <returns>true：成功</returns>
    public bool SystemMessage(string[] recid,string title,string detail,string url,DateTime sendtime)
    {
        url = HttpUtility.UrlEncode(url);
        DataTable dt = new Message().GetSystemMessageScheme();
        foreach (string x in recid)
        {
            
                DataRow dr = dt.NewRow();
                dr["recid"] = x;
                dr["title"] = title;
                dr["detail"] = detail;
                dr["url"] = "message.aspx?mode=system&url="+url;
                dr["sendtime"] = sendtime;
                dr["type"] = "system";
                dr["readed"] = "未读";
                dt.Rows.Add(dr);
        }
        SqlBulkCopyColumnMapping[] mapping = new SqlBulkCopyColumnMapping[7];
        mapping[0] = new SqlBulkCopyColumnMapping("recid", "recid");
        mapping[1] = new SqlBulkCopyColumnMapping("title", "title");
        mapping[2] = new SqlBulkCopyColumnMapping("detail", "detail");
        mapping[3] = new SqlBulkCopyColumnMapping("url", "url");
        mapping[4] = new SqlBulkCopyColumnMapping("sendtime", "sendtime");
        mapping[5] = new SqlBulkCopyColumnMapping("type", "type");
        mapping[6] = new SqlBulkCopyColumnMapping("readed", "readed");
        try
        {
            Sqlhelper.BulkInsert(dt, "message", mapping);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        //int x=Sqlhelper.ExcuteNonQuery("insert into message (recid,title,detail,url,sendtime,type) values (@recid,@title,@detail,@url,@sendtime,'system')", new SqlParameter("recid", recid), new SqlParameter("title", title), new SqlParameter("detail", detail), new SqlParameter("url", url), new SqlParameter("sendtime", sendtime));
        //if (x == 0) { return false; }
        //else { return true; }
    }
    public bool SystemMessageToAll(string title, string detail, string url, DateTime sendtime)
    {
        url = HttpUtility.UrlEncode(url);
        DataTable recid = Sqlhelper.Serach("select yhid from yonghqx");
        DataTable dt = new Message().GetSystemMessageScheme();
        foreach (DataRow x in recid.Rows)
        {

            DataRow dr = dt.NewRow();
            dr["recid"] = x["yhid"].ToString();
            dr["title"] = title;
            dr["detail"] = detail;
            dr["url"] = "message.aspx?mode=system&url=" + url; ;
            dr["sendtime"] = sendtime;
            dr["type"] = "system";
            dr["readed"] = "未读";
            dt.Rows.Add(dr);
        }
        SqlBulkCopyColumnMapping[] mapping = new SqlBulkCopyColumnMapping[7];
        mapping[0] = new SqlBulkCopyColumnMapping("recid", "recid");
        mapping[1] = new SqlBulkCopyColumnMapping("title", "title");
        mapping[2] = new SqlBulkCopyColumnMapping("detail", "detail");
        mapping[3] = new SqlBulkCopyColumnMapping("url", "url");
        mapping[4] = new SqlBulkCopyColumnMapping("sendtime", "sendtime");
        mapping[5] = new SqlBulkCopyColumnMapping("type", "type");
        mapping[6] = new SqlBulkCopyColumnMapping("readed", "readed");
        try
        {
            Sqlhelper.BulkInsert(dt, "message", mapping);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        //int x=Sqlhelper.ExcuteNonQuery("insert into message (recid,title,detail,url,sendtime,type) values (@recid,@title,@detail,@url,@sendtime,'system')", new SqlParameter("recid", recid), new SqlParameter("title", title), new SqlParameter("detail", detail), new SqlParameter("url", url), new SqlParameter("sendtime", sendtime));
        //if (x == 0) { return false; }
        //else { return true; }
    }
    /// <summary>
    /// 发送普通消息
    /// </summary>
    /// <param name="recid">收件人id集合</param>
    /// <param name="senderid">发件人id</param>
    /// <param name="title">消息标题</param>
    /// <param name="detail">消息内容</param>
    /// <param name="sendtime">发送时间</param>
    /// <returns>true：成功</returns>
    public bool NormalMessage(string[] recid, string senderid, string title, string detail, DateTime sendtime)
    {
        DataTable dt = new Message().GetNormalMessageScheme();
        foreach (string x in recid)
        {

            DataRow dr = dt.NewRow();
            dr["recid"] = x;
            dr["senderid"] = senderid;
            dr["title"] = title;
            dr["detail"] = detail;
            dr["sendtime"] = sendtime;
            dr["type"] = "normal";
            dr["url"] = "message.aspx?url=message_show_detail.aspx&mode=normal";
            dr["readed"] = "未读";
            dt.Rows.Add(dr);
        }
        SqlBulkCopyColumnMapping[] mapping = new SqlBulkCopyColumnMapping[8];
        mapping[0] = new SqlBulkCopyColumnMapping("recid", "recid");
        mapping[1] = new SqlBulkCopyColumnMapping("senderid", "senderid");
        mapping[2] = new SqlBulkCopyColumnMapping("title", "title");
        mapping[3] = new SqlBulkCopyColumnMapping("detail", "detail");
        mapping[4] = new SqlBulkCopyColumnMapping("sendtime", "sendtime");
        mapping[5] = new SqlBulkCopyColumnMapping("type", "type");
        mapping[6] = new SqlBulkCopyColumnMapping("url", "url");
        mapping[7] = new SqlBulkCopyColumnMapping("readed", "readed");
        try
        {
            Sqlhelper.BulkInsert(dt, "message", mapping);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public bool NormalMessageToAll(string senderid, string title, string detail, DateTime sendtime)
    {
        DataTable recid = Sqlhelper.Serach("select yhid from yonghqx");
        DataTable dt = new Message().GetNormalMessageScheme();
        foreach (DataRow x in recid.Rows)
        {

            DataRow dr = dt.NewRow();
            dr["recid"] = x["yhid"].ToString();
            dr["senderid"] = senderid;
            dr["title"] = title;
            dr["detail"] = detail;
            dr["sendtime"] = sendtime;
            dr["type"] = "normal";
            dr["url"] = "message.aspx?url=message_show_detail.aspx&mode=normal";
            dr["readed"] = "未读";
            dt.Rows.Add(dr);
        }
        SqlBulkCopyColumnMapping[] mapping = new SqlBulkCopyColumnMapping[8];
        mapping[0] = new SqlBulkCopyColumnMapping("recid", "recid");
        mapping[1] = new SqlBulkCopyColumnMapping("senderid", "senderid");
        mapping[2] = new SqlBulkCopyColumnMapping("title", "title");
        mapping[3] = new SqlBulkCopyColumnMapping("detail", "detail");
        mapping[4] = new SqlBulkCopyColumnMapping("sendtime", "sendtime");
        mapping[5] = new SqlBulkCopyColumnMapping("type", "type");
        mapping[6] = new SqlBulkCopyColumnMapping("url", "url");
        mapping[7] = new SqlBulkCopyColumnMapping("readed", "readed");
        try
        {
            Sqlhelper.BulkInsert(dt, "message", mapping);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    /// <summary>
    /// 删除消息
    /// </summary>
    /// <param name="id">消息id集合</param>
    /// <returns></returns>
    public int DeleteMessage(string[] id)
    {
        if (id.Length == 0) { return 0; }
        string temp="";
        foreach(string x in id)
        {
            temp=temp+","+x;
        }
        temp=temp.TrimStart(',');
        return Sqlhelper.ExcuteNonQuery("delete from message where id in (@id)",new SqlParameter("id",temp));
    }
    /// <summary>
    /// 获取未读消息数目
    /// </summary>
    /// <param name="recid"></param>
    /// <returns></returns>
    public int News(string recid)
    {
       DataTable dt= Sqlhelper.Serach("select id from message where recid=@recid and readed='未读'",new SqlParameter("recid",recid));
       return dt.Rows.Count;
       
    }
    /// <summary>
    /// 处理消息状态为已读
    /// </summary>
    /// <param name="id">消息id</param>
    public void Readed(string id)
    {
        Sqlhelper.ExcuteNonQuery("update message set readed='已读',firstreadtime=getdate() where id="+id);
    }
    /// <summary>
    /// 搜索消息
    /// </summary>
    /// <param name="key">关键字</param>
    /// <param name="mode">已读/未读</param>
    /// <param name="recid">消息人id</param>
    /// <returns></returns>
    public DataTable GetByKey(string key,string mode,string recid)
    {
        key = key.Trim();
        return Sqlhelper.Serach("select id,title,url,sender from (select id,title,url,case when type='system' then '系统消息' else xm end as sender from message left join yonghqx on message.senderid=yonghqx.yhid where recid=@recid and readed like @mode) t where  sender like @key or title like @key", new SqlParameter("recid", recid), new SqlParameter("mode", "%" + mode + "%"), new SqlParameter("key", "%" + key + "%"));
    
    }

}
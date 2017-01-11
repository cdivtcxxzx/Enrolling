using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///通用方法集
/// </summary>
public class Common
{
	public Common()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 获取表头内容
    /// </summary>
    /// <param name="btid">表头ID</param>
    /// <returns></returns>
    public static DataTable getddBiaotaouById(string btid)
    {
        string sqlString = "SELECT * FROM JCB_biaotou WHERE id =@btid";
        try
        {
            DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("btid", btid));
            if (dt.Rows.Count > 0)
                return dt;
            else
                return null;
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 获取表头内容
    /// </summary>
    /// <param name="btid">表头ID</param>
    /// <returns></returns>
    public static DataTable getfkBiaotaouById(string btid)
    {
        string sqlString = "SELECT * FROM FKB_biaotou WHERE id =@btid";
        try
        {
            DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("btid", btid));
            if (dt.Rows.Count > 0)
                return dt;
            else
                return null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 获取表头内容
    /// </summary>
    /// <param name="btid">表头ID</param>
    /// <returns></returns>
    public static DataTable getBiaotaouById(string btid)
    {
        string sqlString = "SELECT * FROM PJB_biaotou WHERE id =@btid";
        try
        {
            DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("btid", btid));
            if (dt.Rows.Count > 0)
                return dt;
            else
                return null;
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 获取项目集合
    /// </summary>
    /// <param name="btid"></param>
    /// <returns></returns>
    public static DataTable getddXiangMuByBtid(string btid)
    {
        string sqlString = "SELECT * FROM JCB_PJXM WHERE btid =@btid ORDER BY px";
        try
        {
            DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("btid", btid));
            if (dt.Rows.Count > 0)
                return dt;
            else
                return null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 获取项目集合
    /// </summary>
    /// <param name="btid"></param>
    /// <returns></returns>
    public static DataTable getfkXiangMuByBtid(string btid)
    {
        string sqlString = "SELECT * FROM FKB_PJXM WHERE btid =@btid ORDER BY px";
        try
        {
            DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("btid", btid));
            if (dt.Rows.Count > 0)
                return dt;
            else
                return null;
        }
        catch
        {
            return null;
        }
    }


    /// <summary>
    /// 获取项目集合
    /// </summary>
    /// <param name="btid"></param>
    /// <returns></returns>
    public static DataTable getXiangMuByBtid(string btid)
    {
        string sqlString = "SELECT * FROM PJB_PJXM WHERE btid =@btid ORDER BY px";
        try
        {
            DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("btid", btid));
            if (dt.Rows.Count > 0)
                return dt;
            else
                return null;
        }
        catch
        {
            return null;
        }
    }

    
    /// <summary>
    /// 获取系部数据
    /// </summary>
    /// <returns></returns>
    public static DataTable getDtXibt()
    {
        string sqlString = "SELECT * FROM DM_YUANXI order by px";
        try
        {
            return Sqlhelper.Serach(sqlString).Rows.Count > 0 ? Sqlhelper.Serach(sqlString) : null;
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 获取系部名称
    /// </summary>
    /// <param name="xbdm"></param>
    /// <returns></returns>
    public static string getXiBuName(string xbdm)
    {
        if (xbdm == "")
            return "";
        else
        {
            string sqlString = "SELECT YXMC FROM DM_YUANXI WHERE YXDM=@yxdm";
            try
            {
                DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("yxdm", xbdm));
                if (dt.Rows.Count > 0)
                    return dt.Rows[0][0].ToString();
                else
                    return "";
            }
            catch
            {
                return "";
            }

        }
    }
    /// <summary>
    /// 获取各子项目总分//未做
    /// </summary>
    /// <param name="xbdm"></param>
    /// <returns></returns>
    public static string getzf(string lx)
    {
        if (lx == "")
            return "";
        else
        {
            string sqlString = "SELECT lxmc,pjzq FROM DM_PJBLX WHERE id=@lx";
            try
            {
                DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("lx", lx));
                if (dt.Rows.Count > 0)
                    return "<a href='#' txttop='txttop' title='类型统计周期:" + dt.Rows[0][1].ToString() + "' >[" + dt.Rows[0][1].ToString() + "]" + dt.Rows[0][0].ToString() + "</a>";
                else
                    return "";
            }
            catch
            {
                return "";
            }

        }
    }
    /// <summary>
    /// 获取评价表类型名称
    /// </summary>
    /// <param name="xbdm"></param>
    /// <returns></returns>
    public static string getlxName(string lx)
    {
        if (lx == "")
            return "";
        else
        {
            string sqlString = "SELECT lxmc,pjzq FROM DM_PJBLX WHERE id=@lx";
            try
            {
                DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("lx", lx));
                if (dt.Rows.Count > 0)
                    return "<a href='#' txttop='txttop' title='类型统计周期:" + dt.Rows[0][1].ToString() + "' >[" + dt.Rows[0][1].ToString() + "]" + dt.Rows[0][0].ToString() + "</a>";
                else
                    return "";
            }
            catch
            {
                return "";
            }

        }
    }
    /// <summary>
    /// 获取反馈表类型名称
    /// </summary>
    /// <param name="xbdm"></param>
    /// <returns></returns>
    public static string getfklxName(string lx)
    {
        if (lx == "")
            return "";
        else
        {
            string sqlString = "SELECT lxmc FROM DM_FKBLX WHERE id=@lx";
            try
            {
                DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("lx", lx));
                if (dt.Rows.Count > 0)
                    return "<a href='#'  >"  + dt.Rows[0][0].ToString() + "</a>";
                else
                    return "";
            }
            catch
            {
                return "";
            }

        }
    }
    /// <summary>
    /// 获取督导表类型名称
    /// </summary>
    /// <param name="xbdm"></param>
    /// <returns></returns>
    public static string getddlxName(string lx)
    {
        if (lx == "")
            return "";
        else
        {
            string sqlString = "SELECT lxmc FROM DM_JCBLX WHERE id=@lx";
            try
            {
                DataTable dt = Sqlhelper.Serach(sqlString, new SqlParameter("lx", lx));
                if (dt.Rows.Count > 0)
                    return "<a href='#'  >" + dt.Rows[0][0].ToString() + "</a>";
                else
                    return "";
            }
            catch
            {
                return "";
            }

        }
    }
    /// <summary>
    /// 修改项目表中的值
    /// </summary>
    /// <param name="id">项目id</param>
    /// <param name="updateItems">修改选项</param>
    /// <param name="updateData">选项的值</param>
    /// <returns>影响条数，一般情况下为1，修改失败为0</returns>
    public static int updateddXiangMu(string id, string updateItems, string updateData)
    {
        string sqlString = "UPDATE JCB_PJXM SET " + updateItems + "=" + updateData + " WHERE id = @id";
        try
        {
            return Sqlhelper.ExcuteNonQuery(sqlString, new SqlParameter("id", id));
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 修改项目表中的值
    /// </summary>
    /// <param name="id">项目id</param>
    /// <param name="updateItems">修改选项</param>
    /// <param name="updateData">选项的值</param>
    /// <returns>影响条数，一般情况下为1，修改失败为0</returns>
    public static int updatefkXiangMu(string id, string updateItems, string updateData)
    {
        string sqlString = "UPDATE FKB_PJXM SET " + updateItems + "=" + updateData + " WHERE id = @id";
        try
        {
            return Sqlhelper.ExcuteNonQuery(sqlString, new SqlParameter("id", id));
        }
        catch
        {
            return 0;
        }
    }
    /// <summary>
    /// 修改项目表中的值
    /// </summary>
    /// <param name="id">项目id</param>
    /// <param name="updateItems">修改选项</param>
    /// <param name="updateData">选项的值</param>
    /// <returns>影响条数，一般情况下为1，修改失败为0</returns>
    public static int updateXiangMu(string id,string updateItems, string updateData)
    {
        string sqlString = "UPDATE PJB_PJXM SET " + updateItems + "=" + updateData + " WHERE id = @id";
        try
        {
            return Sqlhelper.ExcuteNonQuery(sqlString,new SqlParameter("id",id));
        }
        catch
        {
            return 0;
        }
    }
    /// <summary>
    /// 通过zhuid获取评价组
    /// </summary>
    /// <param name="zhuid"></param>
    /// <returns></returns>
    public static DataTable getPingJiaZhu(string zhuid)
    {
        string sqlString = "SELECT * FROM zhuqx WHERE ZID = @id";
        try
        {
            return Sqlhelper.Serach(sqlString, new SqlParameter("id", zhuid)).Rows.Count > 0 ? Sqlhelper.Serach(sqlString, new SqlParameter("id", zhuid)) : null;
        }
        catch
        {
            return null;
        }
    }
}
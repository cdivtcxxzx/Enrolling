using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Banj 的摘要说明
/// </summary>
public class Banj
{
	public Banj()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public string getSchool(string UserName)
    {
         DataTable dt = Sqlhelper.Serach("select school_id from zy_school s left join yonghqx y on s.userguid=y.guid where y.yhid="+UserName);
         if (dt.Rows.Count > 0)
         {
             return dt.Rows[0][0].ToString();
         }
         else return "";
    }
    public DataTable getAllByUser(string UserName)
    {

        return getAll(getSchool(UserName));
       
    }
    public DataTable getAll(string school_id)
    {
       
        return Sqlhelper.Serach("select * from ZY_BANJ where school_id="+school_id);
    }
    public DataTable getByKeyByUser(string key, string UserName)
    {

        return getByKey(key, getSchool(UserName));
        
    }
    public DataTable getByKey(string key,string school_id)
    {
        if (key == null) { key = ""; }
        DataTable dt = getAll(school_id);
        DataTable searchDt = new DataTable();
        searchDt = dt.Clone();
        foreach (DataRow dr in dt.Rows)
        {
            string xx = "";
            foreach (var m in dr.ItemArray)
            {
                xx = xx + m.ToString();

            }
            if (System.Text.RegularExpressions.Regex.IsMatch(xx, key, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {

                searchDt.ImportRow(dr);

            }
            else
            {
                //searchDt.Rows.Remove(dr);
            }

        }
        return searchDt;
    }
}
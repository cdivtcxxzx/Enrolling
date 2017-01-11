using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// School 的摘要说明
/// </summary>
public class School
{
	public School()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public DataTable getAll()
    {
        return Sqlhelper.Serach("select * from ZY_SCHOOL");
    }
    public DataTable getByKey(string key)
    {
        if (key == null) { key = ""; }
        DataTable dt = getAll();
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
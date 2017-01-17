using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
///dormitory 宿舍服务类
/// </summary>
public class dormitory
{
	public dormitory()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 功能描述：学生是否已分配宿舍(学号):根据“学号”判断学生是否已经分配了宿舍，已分配返回true，否则返回false。
    /// 编写人：张明
    /// 创建时间：2017.1.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="xh">学号</param>
    /// <returns>真假</returns>
    public static bool isbillet(string xh)
    {
        try
        {
            //到宿舍分配表中使用学号查询，如果记录大于0，即已分配；
            DataTable billet = Sqlhelper.Serach("SELECT TOP 1 [FK_Bed_NO]  FROM [Fresh_Bed_Log] where [FK_SNO]=@xh", new SqlParameter("xh", xh));
            if (billet.Rows.Count > 0) return true;
        }
        catch(Exception err)
        {
            try{new c_log().logAdd("dormitory.cs","isbillet", err.Message, "2", "zhangming1");//记录错误日志
            }catch{}
            return false;
        }
        return false;
    }
    /// <summary>
    /// 功能描述：床位分配 学生已分配床位(学号):根据“学号”返回学生已分配的“床位分配”数据，否则返回null。
    /// 编写人：张明
    /// 创建时间：2017.1.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="xh">学号</param>
    /// <returns>datatable</returns>
    public static DataTable billetdata(string xh)
    {
        DataTable billet = new DataTable();
        try
        {
            //到宿舍分配表中使用学号查询,并联查出相关床位分配信息；
            billet = Sqlhelper.Serach("SELECT TOP 1 [PK_Bed_Log],[FK_Bed_NO],[FK_SNO],[Updater],[Update_DT]  FROM [Fresh_Bed_Log] where [FK_SNO]=@xh", new SqlParameter("xh", xh));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "billetdata", err.Message, "2", "zhangming1");//记录错误日志
            }
            catch { }
           
        }
        return billet;
    }
   



}
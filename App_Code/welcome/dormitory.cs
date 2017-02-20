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
    /// 功能描述：20学生是否已分配宿舍(学号):根据“学号”判断学生是否已经分配了宿舍，已分配返回true，否则返回false。
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
            throw;
            //return false;
        }
        return false;
    }
    /// <summary>
    /// 功能描述：21床位分配 学生已分配床位(学号):根据“学号”返回学生已分配的“床位分配”数据，否则返回null。
    /// 编写人：张明
    /// 创建时间：2017.1.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="xh">学号</param>
    /// <returns>datatable【主键、床位主键、学号、操作人、操作时间】</returns>
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
                throw;
            }
            catch { }
           
        }
        return billet;
    }

    /// <summary>
    /// 功能描述：22床位 获取某床位数据(床位主键):根据“床位主键”返回“床位”数据table，否则返回空table。
    /// 编写人：张明
    /// 创建时间：2017.1.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">主键</param>
    /// <returns>datatable【主键、床位编号、床位位置主键、房间主键】</returns>
    public static DataTable getbed(string mkey)
    {
        DataTable bed = new DataTable();
        try
        {
            //到宿舍分配表中使用学号查询,并联查出相关床位分配信息；
            bed = Sqlhelper.Serach("SELECT TOP 1 [PK_Bed_NO],[Bed_NO],[FK_Bed_Type],[FK_Room_NO]  FROM [Fresh_Bed] where [PK_Bed_NO]=@mkey", new SqlParameter("mkey", mkey));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "beddata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return bed;
    }
    /// <summary>
    /// 功能描述：23、房间 获取某房间数据(房间主键):根据“房间主键”返回“房间”datatable数据，否则返回空TABLE。
    /// 编写人：张明
    /// 创建时间：2017.2.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">房间主键</param>
    /// <returns>datatable【主键、宿舍主键、房间编号、楼层、入住性别编码、房间类型编码】</returns>
    public static DataTable getroom(string mkey)
    {
        DataTable room = new DataTable();
        try
        {
            //到房间表中使用房间主键查询房间相关信息；
            room = Sqlhelper.Serach("SELECT top 1 [PK_Room_NO],[Room_NO],[FK_Dorm_NO],[Floor],[Gender],[FK_Room_Type]  FROM [Fresh_Room] where PK_Room_NO=@bh", new SqlParameter("bh", mkey));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "roomdata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return room;
    }

    /// <summary>
    /// 功能描述：24、宿舍 获取某宿舍数据(宿舍主键):根据“宿舍主键”返回“宿舍”datatable数据，否则返回空datatable数据。
    /// 编写人：张明
    /// 创建时间：2017.2.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">主键</param>
    /// <returns>datatable【主键、宿舍号、学年、宿舍名称、校区编号】</returns>
    public static DataTable getdorm(string mkey)
    {
        DataTable dorm = new DataTable();
        try
        {
            //到宿舍表中使用主键查询宿舍相关信息；
            dorm = Sqlhelper.Serach("SELECT TOP 1 [PK_Dorm_NO],[Dorm_NO],[Year],[Name],[Campus_NO]  FROM [Fresh_Dorm] where PK_Dorm_NO=@bh", new SqlParameter("bh", mkey));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "dormdata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return dorm;
    }




    /// <summary>
    /// 功能描述：25、床位位置 获取某床位位置数据(床位位置主键):根据“床位位置主键”返回“床位位置”datatable数据，否则返回nulldatatable。
    /// 编写人：张明
    /// 创建时间：2017.2.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">床位位置主键</param>
    /// <returns>datatable【主键、床位类型、房间类型主键、床位位置序号、床位位置编号】</returns>
    public static DataTable getbedtype(string mkey)
    {
        DataTable bedtype = new DataTable();
        try
        {
            //到床位位置表中使用主键查询床位位置相关信息；
            bedtype = Sqlhelper.Serach("SELECT TOP 1 [PK_Bed_Type],[Type_Name],[FK_Room_Type],[Bed_Index],[Bed_NO] FROM [Fresh_Bed_Type] where PK_Bed_Type=@bh", new SqlParameter("bh", mkey));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "bedtypedata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return bedtype;
    }



    /// <summary>
    /// 功能描述：26、房间类型 获取某房间类型数据(房间类型主键):根据“房间类型主键”返回“房间类型”datatable数据，否则返回nulldatatable。
    /// 编写人：张明
    /// 创建时间：2017.2.17
    /// 更新记录：无
    /// 版本记录：v0.0.1
    /// </summary>
    /// <param name="mkey">房间类型主键</param>
    /// <returns>datatable【主键、房间类型编码、收费项目代码、学年、房间布局简图、床位布局简图、房间类型名称】</returns>
    public static DataTable getroomtype(string mkey)
    {
        DataTable roomtype = new DataTable();
        try
        {
            //到房间类型表中使用类型主键查询房间类型相关信息；
            roomtype = Sqlhelper.Serach("SELECT TOP 1 [PK_Room_Type],[Type_NO],[FK_Fee_Item],[Year],[Room_Layout],[Bed_Layout],[Type_Name]  FROM [Fresh_Room_Type] where PK_Room_Type=@mkey", new SqlParameter("mkey", mkey));
        }
        catch (Exception err)
        {
            try
            {
                new c_log().logAdd("dormitory.cs", "roomtypedata", err.Message, "2", "zhangming1");//记录错误日志
                throw;
            }
            catch { }

        }
        return roomtype;
    }
}
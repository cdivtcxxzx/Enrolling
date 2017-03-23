using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model;
/// <summary>
/// 宿舍逻辑操作
/// </summary>
public class dormitoryControl
{
    protected static dormitoryModelDataContext DDC = new dormitoryModelDataContext();
	public dormitoryControl()
	{
		
	}
    /// <summary>
    /// 实时添加床位分配日志记录信息
    /// </summary>
    /// <param name="FK_Bed_NO">床位主键</param>
    /// <param name="FK_SNO">学号</param>
    /// <param name="Updater">操作人员</param>
    /// <returns>添加成功返回"1",否则返回错误信息</returns>
    public static string Add_Fresh_Bed_Log(string FK_Bed_NO,string FK_SNO,string Updater )
    {
        Fresh_Bed_Log fresh_bed = new Fresh_Bed_Log
        {
            PK_Bed_Log = System.Guid.NewGuid().ToString(),
            FK_Bed_NO = FK_Bed_NO,
            FK_SNO = FK_SNO,
            Updater = Updater,
            Update_DT = DateTime.Now
        };
        DDC.Fresh_Bed_Logs.InsertOnSubmit(fresh_bed);
        try
        {
            DDC.SubmitChanges();
            return "1";
        }
        catch (Exception e)
        {

            return e.Message.ToString();
        }
        
    }
}
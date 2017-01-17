using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///名称：迎新批次
///作者：胡元
/// </summary>
public class fresh_batch{
    string PK_Batch_NO;//迎新批次编号
    string Batch_Name;//迎新批次名称
    string Year;//迎新年
    string STU_Type;//迎新学生类型
    DateTime Welcome_Begin;//迎新办理开始时间
    DateTime Welcome_End;//迎新办理结束时间
    DateTime Service_Begin;//迎新服务开始时间
    DateTime Service_End;//迎新服务结束时间
    string Enabled;//迎新服务启停标志
}


/// <summary>
/// 名称：迎新事务
/// 作者：胡元
/// </summary>
public class fresh_affair
{
    string PK_Affair_NO;//迎新事务编号
    string FK_Batch_NO;//迎新批次编号
    int Affair_Index;//自编序号 
    string Affair_Name;//事务名称
    string Affair_Type;//事务类型
    string Precondition1;//使能条件1
    string Precondition2;//使能条件2
    string Call_Function;//返回迎新事务状态调用函数
    string Affair_CHAR;//事务性质
    string FK_OPER_NO;//与事务绑定的操作编号
    string Parameters;//其他操作参数
}

/// <summary>
/// 名称：迎新操作
/// 作者：胡元
/// </summary>
public class fresh_oper
{
    string PK_OPER_NO;//操作编号
    string OPER_Name;//操作名称
    string OPER_URL;//操作URL地址
    string OPER_Type;//操作类型
}

/// <summary>
/// 名称：学生迎新事务
/// 作者：胡元
/// </summary>
public class fresh_affair_log
{
    string PK_Affair_Log;//学生迎新事务主键
    string FK_SNO;//学号
    string FK_Affair_NO;//迎新事务编号
    string Log_Status;//事务状态
    string Creater;//创建者
    DateTime Create_DT;//创建时间
    string Updater;//更新者
    DateTime Update_DT;//更新时间
}

/// <summary>
///迎新批次服务类说明
/// </summary>
public class batch
{
   

	public batch()
	{
		//
        //类方法列表
		//
	}

    /// <summary>
    /// 迎新批次[] 获取当前有效迎新批次目录()
    ///返回当前时间介于“迎新服务开始时间”和“迎新服务结束时间”之间，并且“启停服务标志”为“启动”的数据集合。否则返回null。
    /// </summary>
    /// <returns></returns>
    public List<fresh_batch> get_list_fresh_batch()
    {
        List<fresh_batch> result = null;
        try {
            string sqlstr = "select * from fresh_batch";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr);
            
        }
        catch (Exception ex)
        {
            return null;
        }
        return result;
    }
}
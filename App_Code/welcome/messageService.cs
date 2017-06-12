using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using model;

/// <summary>
/// 信息相关服务
/// </summary>
public static class messageService
{
    #region 根据班级编码获取相关通知消息对象 getMsgsByClassNO
    /// <summary>
    /// 根据班级编码获取相关通知消息对象
    /// </summary>
    /// <param name="fk_class_no">班级编码</param>
    /// <returns>List<ClassMsgObj></returns>
    public static List<ClassMsgObj> getMsgsByClassNO(string fk_class_no)
    {
        messageModelDataContext mDC = new messageModelDataContext();
        return mDC.ClassMsgObjs.Where(c => c.FK_Class_NO == fk_class_no).ToList();
    }
    #endregion
    #region 根据班级编码获取相关通知消息 getDtMsgsByClassNO
    /// <summary>
    /// 根据班级编码获取相关通知消息DataTable
    /// </summary>
    /// <param name="fk_class_no"></param>
    /// <returns></returns>
    public static DataTable getDtMsgsByClassNO(string fk_class_no)
    {
        messageModelDataContext mDC = new messageModelDataContext();
        var msgs = from m in mDC.ClassMsgs
                   join mObj in mDC.ClassMsgObjs on m.PK_NO equals mObj.FK_NO_ClassMsg
                   where mObj.FK_Class_NO == fk_class_no && m.Disabled == "on"
                   orderby m.CreateDate descending
                   select new
                   {
                       pk_no = m.PK_NO,
                       title = m.Title.Length > 18 ? m.Title.Substring(0,18)+"..." : m.Title,
                       CreateDate = m.CreateDate
                   };
        return organizationService.ParseToDataTable(msgs);
    }
    #endregion 
    #region 根据班级编码获取相关通知消息 getListMsgsByClassNO
    /// <summary>
    /// 根据班级编码获取相关通知消息List<Object>
    /// </summary>
    /// <param name="fk_class_no"></param>
    /// <returns></returns>
    public static List<Object> getListMsgsByClassNO(string fk_class_no)
    {
        messageModelDataContext mDC = new messageModelDataContext();
        var msgs = from m in mDC.ClassMsgs
                   join mObj in mDC.ClassMsgObjs on m.PK_NO equals mObj.FK_NO_ClassMsg
                   where mObj.FK_Class_NO == fk_class_no && m.Disabled == "on"
                   orderby m.CreateDate  descending
                   select new
                   {
                       pk_no = m.PK_NO,
                       title = m.Title.Length > 18 ? m.Title.Substring(0, 18) + "..." : m.Title,
                       CreateDate = m.CreateDate
                   };
        return msgs.ToList<Object>();
    }
    #endregion
    #region 根据通知消息主键获取通知消息 getMsgByPK_NO
    /// <summary>
    /// 根据通知消息主键获取通知消息
    /// </summary>
    /// <param name="PK_NO">消息主键</param>
    /// <returns>ClassMsg</returns>
    public static ClassMsg getMsgByPK_NO(string PK_NO)
    {
        messageModelDataContext mDC = new messageModelDataContext();
        return mDC.ClassMsgs.Where(m => m.PK_NO == long.Parse(PK_NO)).SingleOrDefault();
    }
    #endregion

    #region 查询学生是否已查看通知消息 isStuReadMsg
    /// <summary>
    /// 查询学生是否已查看通知消息
    /// </summary>
    /// <param name="PK_NO_msg">通知消息主键</param>
    /// <param name="PK_SNO">学生学号主键</param>
    /// <returns>true：已读 false：未读</returns>
    public static bool isStuReadMsg(string PK_NO_msg, string PK_SNO)
    {
        messageModelDataContext mDC = new messageModelDataContext();
        return mDC.ClassMsgStus.Where(m => m.FK_NO_ClassMsg == long.Parse(PK_NO_msg) && m.FK_SNO == PK_SNO).SingleOrDefault() != null ? true : false;
    }
    #endregion

    #region 插入学生查看通知消息记录 addStuReadMsg
    /// <summary>
    /// 插入学生查看通知消息记录
    /// </summary>
    /// <param name="PK_NO_msg">通知消息主键</param>
    /// <param name="PK_SNO">学生学号主键</param>
    /// <returns>"true":插入成功 错误信息:插入失败</returns>
    public static string addStuReadMsg(string PK_NO_msg, string PK_SNO)
    {
        messageModelDataContext mDC = new messageModelDataContext();
        ClassMsgStu msgStu = new ClassMsgStu
        {
            FK_SNO = PK_SNO,
            FK_NO_ClassMsg = long.Parse(PK_NO_msg),
            ReadDate = DateTime.Now
        };
        try
        {
            mDC.ClassMsgStus.InsertOnSubmit(msgStu);
            mDC.ClassMsgStus.InsertOnSubmit(msgStu);
            mDC.SubmitChanges();
            return "true";
        }
        catch(Exception e) 
        {

            return e.Message;
        }
    }
    #endregion
}
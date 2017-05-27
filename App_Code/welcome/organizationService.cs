using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using model;

/// <summary>
///组织机构服务(学生数据处理)
/// </summary>
public static class organizationService
{
    #region linq 查询转DataTable
    /// <summary>
    /// linq 查询转DataTable
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="array">枚举</param>
    /// <returns></returns>
    public static DataTable ParseToDataTable<T>(this IEnumerable<T> array)
    {
        var ret = new DataTable();
        foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
            ret.Columns.Add(dp.Name, dp.PropertyType);
        foreach (T item in array)
        {
            var Row = ret.NewRow();
            foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
                Row[dp.Name] = dp.GetValue(item);
            ret.Rows.Add(Row);
        }
        return ret;
    }
    #endregion

    #region 操作员身份是有效 staffVerify
    /// <summary>
    /// 功能描述：根据“员工编号”查询获取“密码明文”，如果“密文”==校验函数(“密码明文”,“验证码”)返回true。否则返回false。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="staffNo">员工编号</param>
    /// <param name="password">密码</param>
    /// <param name="verifyCode">验证码</param>
    /// <param name="pwdEncode">密文</param>
    /// <returns></returns>
    public static bool staffVerify(string staffNo,string password,string verifyCode,string pwdEncode)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        Base_Staff staff = oDC.Base_Staffs.Where(s => s.PK_Staff_NO == staffNo && s.Password == pwdEncode).SingleOrDefault();
        if (staff == null) return false;
        if (pwdEncode == md5.MD5Encrypt(password, verifyCode))
        {
            return true;
        }
        return false;
    }
    #endregion
    #region 获取某操作员数据 getOperator
    /// <summary>
    /// 根据“员工编号”返回员工基本数据。否则返回null。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="staffNo">员工编号</param>
    /// <returns>返回员工类</returns>
    public static Base_Staff getOperator(string staffNo)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Base_Staffs.Where(s => s.PK_Staff_NO == staffNo).SingleOrDefault();        
    }
    #endregion
    #region 获取某学生数据 getStu
    /// <summary>
    /// 根据“学号”返回学生基本数据，否则返回null。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="sno">学号</param>
    /// <returns>返回学生</returns>
    public static Base_STU getStu(string sno)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Base_STUs.Where(s => s.PK_SNO == sno).SingleOrDefault();
    }
    #endregion
    #region 通过身份证查询是否有学生数据 getStu
    /// <summary>
    /// 根据“身份证号”返回是否有学生数据。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="sfz">身份证号</param>
    /// <returns>真假</returns>
    public static bool getStuBySFZ(string sfz)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Base_STUs.Where(s => s.ID_NO == sfz).SingleOrDefault() != null;
        
    }
    #endregion
    #region 通过年级和专业查询学生数量 getStuCount
    /// <summary>
    /// 通过年级和专业查询学生数量
    /// </summary>
    /// <param name="FK_SPE">专业代码主键</param>
    /// <param name="Year">年级</param>
    /// <returns>学生数量</returns>
    public static int getStuCount(string FK_SPE,string Year)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Base_STUs.Where(stu => stu.FK_SPE_Code == FK_SPE && stu.Year == Year).ToList<Base_STU>().Count;
    }
    #endregion
    #region 根据学年获取某专业数据 getSpe
    /// <summary>
    /// 根据“学年”查找“专业编号”并返回对应专业数据，否则返回null。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="year">学年</param>
    /// <param name="sepCode">专业编号</param>
    /// <returns>返回专业类</returns>
    public static Fresh_SPE getSpe(string year,string sepCode)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Fresh_SPEs.Where(s => s.Year == year && s.SPE_Code == sepCode).SingleOrDefault();
    }
    #endregion
    #region  获取某班级数据 getClass
    /// <summary>
    /// 根据“班级编号”查找并返回对应班级数据，否则返回null。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="classNo">班级编号</param>
    /// <returns>返回班级类</returns>
    public static Fresh_Class getClass(string classNo)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Fresh_Classes.Where(b => b.PK_Class_NO == classNo).SingleOrDefault();
    }
    #endregion
    #region  获取某班级数据 getAllClass
    /// <summary>
    /// 查找数据库中班级表中的所有数据
    /// 编写人：陈智秋
    /// 创建：2017.5.16
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <returns>返回所有班级</returns>
    public static List<Fresh_Class> getAllClass()
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Fresh_Classes.ToList();
    }
    #endregion
    #region 获取某专业下的班级信息 getClasses
    /// <summary>
    /// 根据“专业主键”返回对应专业下的所有班级。
    /// 编写人：陈智秋
    /// 创建：2017.5.16
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="sepCode">专业主键</param>
    /// <returns>返回班级集合</returns>
    public static List<Fresh_Class> getClasses(string PK_SPE)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Fresh_Classes.Where(cls => cls.FK_SPE_NO == PK_SPE).ToList();
    }
    #endregion
    #region 学生身份是否有效 stuVerify
    /// <summary>
    /// 功能描述：根据“学号”查询所在批次中“禁止操作标志”为false，并且被授权“迎新事务”的“事务性质”为“交互性”，“事务列席”为“学生自治”或“两者”的数据。否则返回null。
    /// 编写人：陈智秋
    /// 创建：2017.3.1
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="sno">学号</param>
    /// <param name="password">密码</param>
    /// <param name="verifyCode">验证码</param>
    /// <param name="pwdEncode">密文</param>
    /// <returns></returns>
    public static bool stuVerify(string sno, string password, string verifyCode, string pwdEncode)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        Base_STU stu = oDC.Base_STUs.Where(s => s.PK_SNO == sno && s.Password == pwdEncode).SingleOrDefault();
        if (stu == null) return false;
        if (pwdEncode == md5.MD5Encrypt(password, verifyCode))
            return true;
        else
            return false;
    }
    #endregion
    #region 获取学院数据 getColleage
    /// <summary>
    /// 根据学院主键返回学院数据
    /// 编写人：陈智秋
    /// 创建：2017.3.20
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="colleageNo">学院主键编号</param>
    /// <returns>学院实体</returns>
    public static Base_College getColleage(string colleagePk)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Base_Colleges.Where(c => c.PK_College == colleagePk).SingleOrDefault();
    }
    #endregion
    #region 获取学院数据 getColleageByCode
    /// <summary>
    /// 根据学院代码返回学院数据
    /// 编写人：陈智秋
    /// 创建：2017.5.9
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="colleageNo">学院主键编号</param>
    /// <returns>学院实体</returns>
    public static Base_College getColleageByCode(string colleage)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Base_Colleges.Where(c => c.College_NO == colleage).SingleOrDefault();
    }
    #endregion
    #region 获取校区数据 getCampus
    /// <summary>
    /// 根据校区主键返回校区数据
    /// 编写人：陈智秋
    /// 创建：2017.3.20
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="campusPk">校区主键编号</param>
    /// <returns>校区实体</returns>
    public static Base_Campus getCampus(string campusPk)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Base_Campus.Where(c => c.PK_Campus == campusPk).SingleOrDefault();
    }
    #endregion
    #region 获取辅导员信息 getCounseller
    /// <summary>
    /// 根据辅导员主键返回辅导员信息
    /// 编写人：陈智秋
    /// 创建：2017.3.20
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="counsellerPk">辅导员主键编号</param>
    /// <returns>辅导员实体</returns>
    public static Fresh_Counseller getCounseller(string counsellerPk)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Fresh_Counsellers.Where(c => c.PK_Counseller_NO == counsellerPk).SingleOrDefault();
    }
    #endregion getCounsellerForClassPK
    #region 根据班级主键返回辅导员信息 getCounsellerForClassPK
    /// <summary>
    /// 根据班级主键返回辅导员信息
    /// 编写人：胡元
    /// 创建：2017.3.20
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="counsellerPk">班级主键</param>
    /// <returns>辅导员实体</returns>
    public static Fresh_Counseller getCounsellerForClassPK(string classPK)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Fresh_Counsellers.Where(c => c.FK_Class_NO == classPK).SingleOrDefault();
    }
    #endregion
    #region 获取专业信息 getSpe(重载1)
    /// <summary>
    /// 根据“专业主键”返回对应专业数据，否则返回null。
    /// 编写人：陈智秋
    /// 创建：2017.3.21
    /// 更新：无
    /// 版本：v0.0.1
    /// </summary>
    /// <param name="sepCode">专业主键</param>
    /// <returns>返回专业实体</returns>
    public static Fresh_SPE getSpe(string PK_SPE)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Fresh_SPEs.Where(s => s.PK_SPE == PK_SPE).SingleOrDefault();
    }
    #endregion    
    #region 验证学生基本信息是否确认 isStuConfrim
    /// <summary>
    /// 对比学生学号验证学生是否进行基本信息的确认(有数据则已确认)
    /// </summary>
    /// <param name="FK_SNO">学号</param>
    /// <returns>是否确认</returns>
    public static bool isStuConfrim(string FK_SNO)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        Fresh_Confirm confirm = oDC.Fresh_Confirms.Where(s => s.FK_SNO == FK_SNO).SingleOrDefault();
        return confirm != null ? true : false;
    }
    #endregion
    #region 判断添加学生基本信息确认记录 addStuConfirm
    /// <summary>
    /// 判断添加学生基本信息确认记录，有记录则只修改
    /// </summary>
    /// <param name="FK_SNO">学生学号</param>
    /// <param name="state">状态：true为信息无误，false为信息有误</param>
    /// <returns></returns>
    public static bool addStuConfirm(string FK_SNO, bool state)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        if (isStuConfrim(FK_SNO))
        {
            //判断存在，只修改状态
            Fresh_Confirm confirm = oDC.Fresh_Confirms.SingleOrDefault(s => s.FK_SNO == FK_SNO);
            if (confirm != null)
            {
                confirm.Confirm_state = state;
                confirm.Confirm_Date = DateTime.Now;
            }
            
            try
            {
                oDC.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                string s = e.Message;
                return false;
            }
            
        }
        else
        { 
            //不存在，新生成
            Fresh_Confirm confirm = new Fresh_Confirm
            {
                PK_Confirm_ID = Guid.NewGuid().ToString(),
                FK_SNO = FK_SNO,
                Confirm_state = state,
                Confirm_Date = DateTime.Now
            };
            try
            {
                oDC.Fresh_Confirms.InsertOnSubmit(confirm);
                oDC.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;                
            }
        }
    }
    #endregion
    #region 修改学生信息 stuUpdate
    /// <summary>
    /// 修改学生相关信息
    /// </summary>
    /// <param name="FK_SNO">学号</param>
    /// <param name="stu">学生实体</param>
    /// <returns>修改是否成功</returns>
    public static bool stuUpdate(string FK_SNO, Base_STU stu)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        Base_STU stu_info = oDC.Base_STUs.Where(s => s.PK_SNO == FK_SNO).SingleOrDefault();
        if (stu_info == null && stu.PK_SNO != FK_SNO) return false;
        PropertyInfo[] propertys = stu_info.GetType().GetProperties();
        foreach (PropertyInfo property in propertys)
        {
            var value = property.GetValue(stu, null);
            property.SetValue(stu_info, value, null);                
        }
        try
        {
            oDC.SubmitChanges();
            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }
    #endregion
    #region 通过代码类型名称获取代码类型数据 getCodeByCodeName
    /// <summary>
    /// 通过代码类型名称获取代码类型数据
    /// </summary>
    /// <param name="CodeName">代码类型名称</param>
    /// <returns>代码类型数据</returns>
    public static Base_Code getCodeByCodeName(string CodeName)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Base_Codes.Where(c => c.Code_Name == CodeName).SingleOrDefault();
    }
    #endregion
    #region 获取代码项数据 getCodeItem
    /// <summary>
    /// 获取代码项中某一小项数据
    /// </summary>
    /// <param name="FK_Code">大项类别代码</param>
    /// <param name="item_no">大项类的小项编码</param>
    /// <returns>代码项</returns>
    public static Base_Code_Item getCodeItem(string FK_Code, string item_no)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Base_Code_Items.Where(i => i.FK_Code == FK_Code && i.Item_NO == item_no).SingleOrDefault();
    }
    #endregion
    #region 获取指定大类型中所有代码信息 getCodesItem
    /// <summary>
    /// 获取指定大类型中所有代码信息,比如民族类下的所有民族信息
    /// </summary>
    /// <param name="PK_item">类型编号</param>
    /// <returns></returns>
    public static List<Base_Code_Item> getCodesItem(string Code)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        return oDC.Base_Code_Items.Where(i => i.FK_Code == Code).ToList();
    }
    #endregion
    #region 根据批次返回学生信息 getStuByBatch
    /// <summary>
    /// 根据批次返回学生信息（学号(PK_SNO)|高考报名号(Test_NO)|姓名(Name)|性别(Gender)|身份证号(ID_NO)|民族代码(Nation_code)|专业名称(SPE_Name)|学制(Xz)|年度(Year)|批次（Fresh_bath））
    /// </summary>
    /// <param name="batch">批次代码,"0"显示所有批次</param>
    /// <returns></returns>
    public static DataTable getStuByBatch(string batch)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        var stu = from s in oDC.Base_STUs
                  join zy in oDC.Fresh_SPEs on s.FK_SPE_Code equals zy.PK_SPE
                  join f in oDC.Fresh_STUs on s.PK_SNO equals f.PK_SNO
                  orderby s.PK_SNO
                  select new
                  {
                      PK_SNO = s.PK_SNO,
                      Test_NO = s.Test_NO,
                      Name = s.Name,
                      Gender = s.Gender_Code == "" ? "" : s.Gender_Code == "01" ? "男" : "女",
                      ID_NO = s.ID_NO,
                      Nation_code = s.Nation_Code,
                      SPE_Name = zy.SPE_Name,
                      Xz = zy.Xznx,
                      Year = s.Year,
                      Fresh_bath = f.FK_Fresh_Batch
                  } ;
        if (batch != "0")
        {
            //return stu.Where(s => s.Fresh_bath == batch).ToList<object>();
            //return stu.Where(s => s.Fresh_bath == batch).ToArray();
            return ParseToDataTable(stu.Where(s => s.Fresh_bath == batch));
        }
        else
        {
            //return stu.ToList<object>();
            //return stu.ToArray();
            return ParseToDataTable(stu);
        }

    }
    #endregion
    #region 根据批次和学院返回学生信息 getStuByBatchCol
    /// <summary>
    /// 根据批次和学院返回学生信息（学号(PK_SNO)|高考报名号(Test_NO)|姓名(Name)|性别(Gender)|身份证号(ID_NO)|专业主键（SPE_PK）|民族代码(Nation_code)|专业名称(SPE_Name)|学制(Xz)|年度(Year)|批次（Fresh_bath|学院代码(Colleage)））
    /// </summary>
    /// <param name="batch">批次代码,"0"返回所有批次</param>
    /// <param name="colleage_sno">学院代码，"0"返回所有学院</param>
    /// <returns>学生信息表</returns>
    public static DataTable getStuByBatchCol(string batch, string colleage_sno)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        var stu = from s in oDC.Base_STUs
                  join zy in oDC.Fresh_SPEs on s.FK_SPE_Code equals zy.PK_SPE
                  join f in oDC.Fresh_STUs on s.PK_SNO equals f.PK_SNO
                  join banji in oDC.Fresh_Classes on s.FK_Class_NO equals banji.PK_Class_NO into gc
                  orderby zy.PK_SPE
                  from banji in gc.DefaultIfEmpty()
                  select new
                  {
                      PK_SNO = s.PK_SNO,
                      Name = s.Name,
                      Gender = s.Gender_Code == "" ? "" : s.Gender_Code == "01" ? "男" : "女",
                      ID_NO = s.ID_NO,
                      SPE_PK = zy.PK_SPE,
                      SPE_Name = zy.SPE_Name,
                      Xz = zy.Xznx,
                      Year = s.Year,
                      Fresh_bath = f.FK_Fresh_Batch,
                      Colleage = zy.FK_College_Code,
                      Class_Name = banji.Name
                  };
        if (batch != "0")
        {
            if (colleage_sno != "0")
            {
                return ParseToDataTable(stu.Where(s => s.Fresh_bath == batch && s.Colleage == colleage_sno));
            }
            else
            {
                return ParseToDataTable(stu.Where(s => s.Fresh_bath == batch));
            }
        }
        else
        {
            if (colleage_sno != "0")
            {
                return ParseToDataTable(stu.Where(s => s.Colleage == colleage_sno));
            }
            else
            {
                return ParseToDataTable(stu);
            }
                 
        }
    
    }
    #endregion

    #region 根据用户ID返回能管理的学院信息 getYxByYhid
    public static List<Base_College> getYxByYhid(string yhid)
    {        
        List<Base_College> result = new List<Base_College>();
        if (yhid == "" || yhid == null) return result;
        ArrayList yxmcList = new Power().GetYxmcsByYhid(yhid);
        organizationModelDataContext oDC = new organizationModelDataContext();
        foreach (var item in yxmcList)
        {
            Base_College colle = oDC.Base_Colleges.Where(s => s.Name == item && s.Enabled == "true").SingleOrDefault();

            if (colle != null) result.Add(colle);
        }
        return result;
    }
    #endregion

    #region 生成学号 createNum
    /// <summary>
    /// 
    /// </summary>
    /// <param name="year">年份</param>
    /// <param name="FK_SPE">专业代码主键</param>
    /// <param name="xz">学制</param>
    /// <returns>学号</returns>
    public static string  createNum(string year,string FK_SPE,string xz)
    {
        Fresh_SPE s = getSpe(FK_SPE);
        if (s == null || year.Length != 4 || xz.Length != 2) return "";
        int num = getStuCount(FK_SPE, year) + 1;
        string str_num = num.ToString("000");
        return year + s.SPE_Code + xz + str_num;
    }
    #endregion

    #region 添加一条相应批次的学生数据, addStu
    /// <summary>
    /// 添加一条学生数据
    /// </summary>
    /// <param name="stu">学生实体</param>
    /// <param name="batch">批次代码</param>
    /// <returns>真假</returns>
    public static bool addStu(Base_STU stu,string batch)
    {
        organizationModelDataContext oDC = new organizationModelDataContext();
        Fresh_STU stu_batch = new Fresh_STU
        {
            PK_SNO = stu.PK_SNO,
            FK_Fresh_Batch = batch
        };
        try
        {
            oDC.Base_STUs.InsertOnSubmit(stu);
            oDC.Fresh_STUs.InsertOnSubmit(stu_batch);
            oDC.SubmitChanges();
            return true;
        }
        catch
        {
            
            return false;
        }
    }
    #endregion

}
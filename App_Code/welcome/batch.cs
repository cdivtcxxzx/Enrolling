using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Services.Description;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Data.SqlClient;

/// 动态调用web服务 
public class WSHelper
{
    /// < summary>           
    /// 动态调用web服务         
    /// < /summary>          
    /// < param name="url">WSDL服务地址< /param> 
    /// < param name="methodname">方法名< /param>           
    /// < param name="args">参数< /param>           
    /// < returns>< /returns>          
    public static object InvokeWebService(string url, string methodname, object[] args)
    {
        return WSHelper.InvokeWebService(url, null, methodname, args);
    }
    /// < summary>          
    /// 动态调用web服务 
    /// < /summary>          
    /// < param name="url">WSDL服务地址< /param>
    /// < param name="classname">类名< /param>  
    /// < param name="methodname">方法名< /param>  
    /// < param name="args">参数< /param> 
    /// < returns>< /returns>
    public static object InvokeWebService(string url, string classname, string methodname, object[] args)
    {
        string @namespace = "EnterpriseServerBase.WebService.DynamicWebCalling";
        if ((classname == null) || (classname == ""))
        {
            classname = WSHelper.GetWsClassName(url);
        }
        try
        {                   //获取WSDL   
            WebClient wc = new WebClient();
            Stream stream = wc.OpenRead(url + "?WSDL");
            ServiceDescription sd = ServiceDescription.Read(stream);
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);
            //生成客户端代理类代码          
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            CSharpCodeProvider icc = new CSharpCodeProvider();
            //设定编译参数                 
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");
            //编译代理类                 
            CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }
            //生成代理实例，并调用方法   
            System.Reflection.Assembly assembly = cr.CompiledAssembly;
            Type t = assembly.GetType(@namespace + "." + classname, true, true);
            object obj = Activator.CreateInstance(t);
            System.Reflection.MethodInfo mi = t.GetMethod(methodname);
            return mi.Invoke(obj, args);
            // PropertyInfo propertyInfo = type.GetProperty(propertyname);     
            //return propertyInfo.GetValue(obj, null); 
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
        }
    }
    private static string GetWsClassName(string wsUrl)
    {
        string[] parts = wsUrl.Split('/');
        string[] pps = parts[parts.Length - 1].Split('.');
        return pps[0];
    }
}

public class affair_operate_auth_msg
{
    public bool isauth;
    public string msg;
}

/// <summary>
///名称：迎新批次
///作者：胡元
/// </summary>
public class fresh_batch{
    public string PK_Batch_NO;//迎新批次编号
    public string Batch_Name;//迎新批次名称
    public string Year;//迎新年
    public string STU_Type;//迎新学生类型
    public DateTime Welcome_Begin;//迎新办理开始时间
    public DateTime Welcome_End;//迎新办理结束时间
    public DateTime Service_Begin;//迎新服务开始时间
    public DateTime Service_End;//迎新服务结束时间
    public string Enabled;//迎新服务启停标志
    public string Financial_PK_Fee;//迎新批次对应财务收费主键
    public string Financial_XH_Prefix;////迎新批次对应财务学号前缀字符串
}

/// <summary>
/// 名称：迎新事务
/// 作者：胡元
/// </summary>
public class fresh_affair
{
    public string PK_Affair_NO;//迎新事务编号
    public string FK_Batch_NO;//迎新批次编号
    public int Affair_Index;//自编序号 
    public string Affair_Name;//事务名称
    public string Affair_Type;//事务类型
    public string Precondition1;//使能条件1
    public string Precondition2;//使能条件2
    public string Call_Function;//返回迎新事务状态调用函数
    public string Affair_CHAR;//事务性质
    public string FK_OPER_NO;//与事务绑定的操作编号
    public string Parameters;//其他操作参数
    public string precondition1Message;//使能条件1提示信息
    public string precondition2Message;//使能条件2提示信息
    public int Affair_Order;//显示序列
    public string StatusDisplay;//是否显示状态
}

/// <summary>
/// 名称：迎新操作
/// 作者：胡元
/// </summary>
public class fresh_oper
{
    public string PK_OPER_NO;//操作编号
    public string OPER_Name;//操作名称
    public string OPER_URL;//操作URL地址
    public string OPER_Type;//操作类型
}

/// <summary>
/// 名称：学生迎新事务
/// 作者：胡元
/// </summary>
public class fresh_affair_log
{
    public string PK_Affair_Log;//学生迎新事务主键
    public string FK_SNO;//学号
    public string FK_Affair_NO;//迎新事务编号
    public string Log_Status;//事务状态
    public string Creater;//创建者
    public DateTime Create_DT;//创建时间
    public string Updater;//更新者
    public DateTime Update_DT;//更新时间
}

/// <summary>
/// 名称：学生基本信息
/// 作者：胡元
/// </summary>
public class base_stu
{
    public string PK_SNO;//学号
    public string FK_SPE_Code;//专业主键
    public string Year;//学年
    public string Test_NO;//考生号
    public string ID_NO;//身份证号
    public string Name;//姓名
    public string Gender_Code;//性别码
    public string Photo;//照片地址
    public string Status_Code;//迎新状态码
    public DateTime DT_Initial;//从招办导入时的时间
    public string FK_Class_NO;//班级编码
    public string Password;//口令
}

/// <summary>
/// 名称：专业基本信息
/// 作者：胡元
/// </summary>
public class fresh_spe
{
    public string SPE_Code;//专业编号
    public string Year;//学年
    public string SPE_Name;//专业名称
    public string EDU_Level_Code;//学历层次码
    public string FK_College_Code;//学院主键
    public string PK_SPE;//专业主键
}

/// <summary>
/// 名称：班级基本信息
/// 作者：胡元
/// </summary>
public class fresh_class
{
    public string PK_Class_NO;//班级编号
    public string FK_Campus_NO;//校区主键
    public string FK_SPE_NO;//专业主键
    public string Name;//班级名称
}

/// <summary>
/// 名称：班主任基本信息
/// 作者：胡元
/// </summary>
public class fresh_counseller
{
    public string PK_Counseller_NO;//班主任主键
    public string FK_Class_NO;//班级编号
    public string FK_Staff_NO;//员工编号
    public string Year;//任职年
}

/// <summary>
/// 名称：学院基本信息
/// 作者：胡元
/// </summary>
public class base_college
{
    public string PK_College;//学院主键
    public string Name;//学院名称
    public string Enabled;//是否有效标志
    public string College_NO;//学院编号
}

/// <summary>
/// 名称：校区基本信息
/// 作者：胡元
/// </summary>
public class base_campus
{
    public string PK_Campus;//校区主键
    public string Campus_NO;//校区编码
    public string Enabled;//是否有效标志
    public string Campus_Name;//校区名称
}

/// <summary>
/// 名称：编码目录
/// 作者：胡元
/// </summary>
public class base_code
{
    public string PK_Code;//主键
    public string Code_Name;//目录名称
    public string Remark;//备注
    public string Code_NO;//目录编码
}

/// <summary>
/// 名称：编码项目信息
/// 作者：胡元
/// </summary>
public class base_code_item
{
    public string PK_Item;//主键
    public string FK_Code;//编码目录主键
    public string Item_Name;//项目名称
    public string Remark;//备注
    public string Item_NO;//项目编码
}






/// <summary>
///迎新批次服务类说明
/// </summary>
public class batch
{

    /// <summary>
    ///功能名称：获取当前所有编码目录
    ///功能描述：
    ///编写人：胡元
    ///创建时间：2017-3-22
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public List<base_code> get_base_code_list(string PK_Code)
    {
        List<base_code> result = null;
        try
        {
            string sqlstr = null;
            System.Data.DataTable dt = null;

            if (PK_Code != null)
            {
                sqlstr = "select * from base_code where PK_Code=@pa";
                dt = Sqlhelper.Serach(sqlstr, new SqlParameter("pa", PK_Code.Trim()));
            }
            else
            {
                sqlstr="select * from base_code";
                dt = Sqlhelper.Serach(sqlstr);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<base_code>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    base_code row = new base_code();
                    row.PK_Code = dt.Rows[i]["PK_Code"].ToString().Trim();
                    row.Code_Name = dt.Rows[i]["Code_Name"].ToString().Trim();
                    row.Remark = dt.Rows[i]["Remark"].ToString().Trim();
                    row.Code_NO = dt.Rows[i]["Code_NO"].ToString().Trim();
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_base_code_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    /// <summary>
    ///功能名称：获取指定编码目录的编码项目信息
    ///功能描述：
    ///编写人：胡元
    ///创建时间：2017-3-22
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public List<base_code_item> get_base_code_item(string FK_Code)
    {
        List<base_code_item> result = null;
        try
        {
            string sqlstr = "select * from base_code_item where FK_Code=@pa";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("pa", FK_Code.Trim()));
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<base_code_item>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    base_code_item row = new base_code_item();
                    row.PK_Item = dt.Rows[i]["PK_Item"].ToString().Trim();
                    row.FK_Code = dt.Rows[i]["FK_Code"].ToString().Trim();
                    row.Item_Name = dt.Rows[i]["Item_Name"].ToString().Trim();
                    row.Remark = dt.Rows[i]["Remark"].ToString().Trim();
                    row.Item_NO = dt.Rows[i]["Item_NO"].ToString().Trim();
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_base_code_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    /// <summary>
    ///功能名称：获取当前有效迎新批次目录
    ///功能描述：
    ///返回当前时间介于“迎新服务开始时间”和“迎新服务结束时间”之间，并且“启停服务标志”为“run”的迎新批次数据集合。否则返回null。
    ///编写人：胡元
    ///创建时间：2017-1-17
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public List<fresh_batch> get_freshbatch_list()
    {
        List<fresh_batch> result = null;
        try {
            string sqlstr = "select * from fresh_batch where Service_Begin<getdate() and Service_End>getdate() and UPPER(Enabled)='RUN'";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<fresh_batch>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fresh_batch row = new fresh_batch();
                    row.Batch_Name = dt.Rows[i]["Batch_Name"].ToString().Trim();
                    row.Enabled = dt.Rows[i]["Enabled"].ToString().Trim();
                    row.PK_Batch_NO = dt.Rows[i]["PK_Batch_NO"].ToString().Trim();
                    row.Service_Begin =DateTime.Parse(dt.Rows[i]["Service_Begin"].ToString());
                    row.Service_End = DateTime.Parse(dt.Rows[i]["Service_End"].ToString());
                    row.STU_Type = dt.Rows[i]["STU_Type"].ToString().Trim();
                    row.Welcome_Begin = DateTime.Parse(dt.Rows[i]["Welcome_Begin"].ToString());
                    row.Welcome_End = DateTime.Parse(dt.Rows[i]["Welcome_End"].ToString());
                    row.Year = dt.Rows[i]["Year"].ToString().Trim();
                    row.Financial_PK_Fee = dt.Rows[i]["Financial_PK_Fee"].ToString().Trim();
                    row.Financial_XH_Prefix = dt.Rows[i]["Financial_XH_Prefix"].ToString().Trim();
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try{
                new c_log().logAdd("batch.cs", "get_freshbatch_list", ex.Message, "2", "huyuan");//记录错误日志
            }catch { }
            throw ex;
        }
        return result;
    }

    /// <summary>
    ///功能名称：获取某迎新批次
    ///功能描述：
    ///根据“迎新批次编号”返回迎新批次数据。否则返回null。
    ///参数：
    ///PK_Batch_NO：迎新批次编号
    ///编写人：胡元
    ///创建时间：2017-1-17
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public fresh_batch get_freshbatch(string PK_Batch_NO)
    {
        fresh_batch result = null;
        try
        {
            if (PK_Batch_NO == null || PK_Batch_NO.Trim().Length == 0)
            {
                return result;
            }
            PK_Batch_NO = PK_Batch_NO.Trim();

            string sqlstr = "select * from fresh_batch where PK_Batch_NO=@pa" ;
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr,new SqlParameter("pa", PK_Batch_NO.Trim()));
            if (dt != null && dt.Rows.Count ==1)
            {
                int i = 0;
                result = new fresh_batch();
                result.Batch_Name = dt.Rows[i]["Batch_Name"].ToString().Trim();
                result.Enabled = dt.Rows[i]["Enabled"].ToString().Trim();
                result.PK_Batch_NO = dt.Rows[i]["PK_Batch_NO"].ToString().Trim();
                result.Service_Begin = DateTime.Parse(dt.Rows[i]["Service_Begin"].ToString());
                result.Service_End = DateTime.Parse(dt.Rows[i]["Service_End"].ToString());
                result.STU_Type = dt.Rows[i]["STU_Type"].ToString().Trim();
                result.Welcome_Begin = DateTime.Parse(dt.Rows[i]["Welcome_Begin"].ToString());
                result.Welcome_End = DateTime.Parse(dt.Rows[i]["Welcome_End"].ToString());
                result.Year = dt.Rows[i]["Year"].ToString().Trim();
                result.Financial_PK_Fee = dt.Rows[i]["Financial_PK_Fee"].ToString().Trim();
                result.Financial_XH_Prefix = dt.Rows[i]["Financial_XH_Prefix"].ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            try{
                new c_log().logAdd("batch.cs", "get_freshbatch", ex.Message, "2", "huyuan");//记录错误日志
            }catch { }
            throw ex;
        }
        return result;
    }

    /// <summary>
    ///功能名称：获取某迎新批次是否有效
    ///功能描述：
    ///根据“迎新批次编号”查询“启停服务标志”为“启动”并且当前时间在“迎新服务开始时间”和“迎新服务结束时间”之间的记录，找到返回true。否则返回false。    
    ///参数：
    ///PK_Batch_NO：迎新批次编号
    /// 编写人：胡元
    ///创建时间：2017-1-17
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public bool get_freshbatch_isrun(string PK_Batch_NO)
    {
        bool result = false;
        try
        {
            if (PK_Batch_NO == null || PK_Batch_NO.Trim().Length == 0)
            {
                return result;
            }
            PK_Batch_NO = PK_Batch_NO.Trim();

            string sqlstr = "select * from fresh_batch where  PK_Batch_NO=@pa and Service_Begin<getdate() and Service_End>getdate() and UPPER(Enabled)='RUN'";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("pa", PK_Batch_NO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                result = true;
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_freshbatch_isrun", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称：获取操作员在某批次是否有效
    ///功能描述：
    ///查找“员工编号”在“批次编号”的迎新批次中是否被分配了权限，没有则返回false。
    ///否则检查该用户在该批次的“禁止操作标志”，等于true则返回false。
    ///否则继续检查是否被分配了相应的“迎新事务操作”项目，存在被分配的“迎新事务操作”返回true。否则返回false。    
    ///编写人：胡元
    ///参数：
    ///PK_Batch_NO：迎新批次编号；PK_Staff_NO：员工编号   
    ///创建时间：2017-1-18
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public bool get_freshoperator_isauth(string PK_Batch_NO, string PK_Staff_NO)
    {
        bool result = false;
        try
        {
            if (PK_Batch_NO == null || PK_Batch_NO.Trim().Length == 0 || PK_Staff_NO == null || PK_Staff_NO.Trim().Length == 0)
            {
                return result;
            }
            string sqlstr = "select * from Fresh_Operator_AUTH where  FK_Fresh_Batch=@cs1 and FK_Staff_NO=@cs2 and UPPER(Enabled)='ENABLED'";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()), new SqlParameter("cs2", PK_Staff_NO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                sqlstr = "select count(*) as sl from Fresh_Operator_AUTH a,Fresh_Affair_AUTH b,Fresh_Affair c " +
                " where a.PK_Operator_AUTH=b.FK_Operator_AUTH and b.FK_Affair_NO=c.PK_Affair_NO and UPPER(a.Enabled)='ENABLED' "+
                "and a.FK_Staff_NO=@cs2 and a.FK_Fresh_Batch=@cs1";
                dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()), new SqlParameter("cs2", PK_Staff_NO.Trim()));
                if (int.Parse(dt.Rows[0]["sl"].ToString())>0)
                {
                    result = true;
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_freshoperator_isauth", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称：获取操作员事务操作列表
    ///功能描述：
    ///根据“批次编号”查询“员工编号”在该批次中“禁止操作标志”为false，并且被授权“迎新事务”的“事务性质”为“交互性”，
    ///“事务类型”为“现场”或“两者”的数据。否则返回null。  
    ///编写人：胡元
    ///参数：
    ///PK_Batch_NO：迎新批次编号；PK_Staff_NO：员工编号   
    ///创建时间：2017-1-30
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public List<fresh_affair> get_freshoperator_auth_affair_list(string PK_Batch_NO, string PK_Staff_NO)
    {
        List<fresh_affair> result = null;
        try
        {
            if (PK_Batch_NO == null || PK_Batch_NO.Trim().Length == 0 || PK_Staff_NO == null || PK_Staff_NO.Trim().Length == 0)
            {
                return null;
            }
            string sqlstr = "select * from Fresh_Operator_AUTH where  FK_Fresh_Batch=@cs1 and FK_Staff_NO=@cs2 and UPPER(Enabled)='ENABLED'";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()), new SqlParameter("cs2", PK_Staff_NO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                sqlstr = "select c.* from Fresh_Operator_AUTH a,Fresh_Affair_AUTH b,Fresh_Affair c " +
                " where a.PK_Operator_AUTH=b.FK_Operator_AUTH and b.FK_Affair_NO=c.PK_Affair_NO and UPPER(a.Enabled)='ENABLED' " +
                "and a.FK_Staff_NO=@cs2 and a.FK_Fresh_Batch=@cs1 "+
                " and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='SCHOOL' OR upper(c.Affair_Type)='BOTH')";
                dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()), new SqlParameter("cs2", PK_Staff_NO.Trim()));
                if (dt != null && dt.Rows.Count>0)
                {
                    result = new List<fresh_affair>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        
                        fresh_affair row = new fresh_affair();
                        row.PK_Affair_NO = dt.Rows[i]["PK_Affair_NO"].ToString().Trim();//迎新事务编号
                        row.FK_Batch_NO = dt.Rows[i]["FK_Batch_NO"].ToString().Trim();//迎新批次编号
                        row.Affair_Index = int.Parse(dt.Rows[i]["Affair_Index"] is DBNull?"0":dt.Rows[i]["Affair_Index"].ToString());//自编序号 
                        row.Affair_Name = dt.Rows[i]["Affair_Name"].ToString().Trim();//事务名称
                        row.Affair_Type = dt.Rows[i]["Affair_Type"].ToString().Trim();//事务类型
                        row.Precondition1 = dt.Rows[i]["Precondition1"].ToString().Trim();//使能条件1
                        row.Precondition2 = dt.Rows[i]["Precondition2"].ToString().Trim();//使能条件2
                        row.Call_Function = dt.Rows[i]["Call_Function"].ToString().Trim();//返回迎新事务状态调用函数
                        row.Affair_CHAR = dt.Rows[i]["Affair_CHAR"].ToString().Trim();//事务性质
                        row.FK_OPER_NO = dt.Rows[i]["FK_OPER_NO"].ToString().Trim();//与事务绑定的操作编号
                        row.Parameters = dt.Rows[i]["Parameters"].ToString().Trim();//其他操作参数
                        row.precondition1Message = dt.Rows[i]["precondition1Message"].ToString().Trim();//使能条件1信息提示
                        row.precondition2Message = dt.Rows[i]["precondition2Message"].ToString().Trim();//使能条件2信息提示
                        row.Affair_Order = int.Parse(dt.Rows[i]["Affair_Order"] is DBNull ? "0" : dt.Rows[i]["Affair_Order"].ToString());//显示序列
                        row.StatusDisplay = dt.Rows[i]["StatusDisplay"].ToString().Trim();//是否显示状态
                        result.Add(row);
                    }                
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_freshoperator_auth_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称：获取预操作总人数
    ///功能描述：
    ///返回 “员工编号”在被授权的批次中“事务编号”中可操作的总的学生数量减去该员工已成功完成的学生数量。
    ///编写人：胡元
    ///参数：
    ///PK_Batch_NO：迎新批次编号；PK_Staff_NO：员工编号；PK_Affair_NO：迎新事务编号   
    ///创建时间：2017-1-30
    ///更新记录：无
    ///版本记录：v0.0.1
    public int get_freshoperator_will_affair_students_count(string PK_Batch_NO, string PK_Staff_NO, string PK_Affair_NO)
    {
        int result = -1;
        try
        {
            if (PK_Batch_NO == null || PK_Batch_NO.Trim().Length == 0 || PK_Staff_NO == null || PK_Staff_NO.Trim().Length == 0 || PK_Affair_NO == null || PK_Affair_NO.Trim().Length == 0)
            {
                return -1;
            }
            string sqlstr = "select * from Fresh_Operator_AUTH where  FK_Fresh_Batch=@cs1 and FK_Staff_NO=@cs2 and UPPER(Enabled)='ENABLED'";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()), new SqlParameter("cs2", PK_Staff_NO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                sqlstr = "select count(*) as sl from vw_fresh_student_base where FK_Fresh_Batch+'_'+SPE_Code+'_'+[year] in ( "+
                            "select distinct(FK_Batch_NO+'_'+SPE_Code+'_'+[year]) from vw_operator_scope "+
                            "where FK_Batch_NO=@cs1 and FK_Staff_NO=@cs2 and PK_Affair_NO=@cs3)" +
                            "and PK_SNO not in (select distinct(FK_SNO) from Fresh_Affair_Log where FK_Affair_NO=@cs3)";
                dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()), new SqlParameter("cs2", PK_Staff_NO.Trim()), new SqlParameter("cs3", PK_Affair_NO.Trim()));
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = int.Parse(dt.Rows[0]["sl"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_freshoperator_will_students_count", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    /// <summary>
    ///功能名称：获取已完成操作总人数
    ///功能描述：
    ///返回 “员工编号”在被授权的批次中“事务编号”中已成功完成的学生数量。
    ///编写人：胡元
    ///参数：
    ///PK_Batch_NO：迎新批次编号；PK_Staff_NO：员工编号；PK_Affair_NO：迎新事务编号   
    ///创建时间：2017-1-30
    ///更新记录：无
    ///版本记录：v0.0.1
    public int get_freshoperator_finish_affair_students_count(string PK_Batch_NO, string PK_Staff_NO, string PK_Affair_NO)
    {
        int result = -1;
        try
        {
            if (PK_Batch_NO == null || PK_Batch_NO.Trim().Length == 0 || PK_Staff_NO == null || PK_Staff_NO.Trim().Length == 0 || PK_Affair_NO == null || PK_Affair_NO.Trim().Length == 0)
            {
                return -1;
            }
            string sqlstr = "select * from Fresh_Operator_AUTH where  FK_Fresh_Batch=@cs1 and FK_Staff_NO=@cs2 and UPPER(Enabled)='ENABLED'";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()), new SqlParameter("cs2", PK_Staff_NO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                sqlstr = "select count(*) as sl from vw_fresh_student_base where FK_Fresh_Batch+'_'+SPE_Code+'_'+[year] in ( " +
                            "select distinct(FK_Batch_NO+'_'+SPE_Code+'_'+[year]) from vw_operator_scope " +
                            "where FK_Batch_NO=@cs1 and FK_Staff_NO=@cs2 and PK_Affair_NO=@cs3)" +
                            "and PK_SNO in (select distinct(FK_SNO) from Fresh_Affair_Log where FK_Affair_NO=@cs3)";
                dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()), new SqlParameter("cs2", PK_Staff_NO.Trim()), new SqlParameter("cs3", PK_Affair_NO.Trim()));
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = int.Parse(dt.Rows[0]["sl"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_freshoperator_finish_students_count", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    /// <summary>
    ///功能名称： 获取某迎新事务
    ///功能描述：
    ///返回“事务编号”对应的事务数据。否则返回null。
    ///编写人：胡元
    ///参数：
    ///PK_Affair_NO：迎新事务编号   
    ///创建时间：2017-1-31
    ///更新记录：无
    ///版本记录：v0.0.1
    public fresh_affair get_affair(string PK_Affair_NO)
    {
        fresh_affair result = null;
        try
        {
            if (PK_Affair_NO == null || PK_Affair_NO.Trim().Length == 0)
            {
                return null;
            }
            string sqlstr = "select * from Fresh_Affair where  PK_Affair_NO=@cs1 ";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Affair_NO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                int i = 0;
                result = new fresh_affair();
                result.PK_Affair_NO = dt.Rows[i]["PK_Affair_NO"].ToString().Trim();//迎新事务编号
                result.FK_Batch_NO = dt.Rows[i]["FK_Batch_NO"].ToString().Trim();//迎新批次编号
                result.Affair_Index = int.Parse(dt.Rows[i]["Affair_Index"] is DBNull ? "0" : dt.Rows[i]["Affair_Index"].ToString());//自编序号 
                result.Affair_Name = dt.Rows[i]["Affair_Name"].ToString().Trim();//事务名称
                result.Affair_Type = dt.Rows[i]["Affair_Type"].ToString().Trim();//事务类型
                result.Precondition1 = dt.Rows[i]["Precondition1"].ToString().Trim();//使能条件1
                result.Precondition2 = dt.Rows[i]["Precondition2"].ToString().Trim();//使能条件2
                result.Call_Function = dt.Rows[i]["Call_Function"].ToString().Trim();//返回迎新事务状态调用函数
                result.Affair_CHAR = dt.Rows[i]["Affair_CHAR"].ToString().Trim();//事务性质
                result.FK_OPER_NO = dt.Rows[i]["FK_OPER_NO"].ToString().Trim();//与事务绑定的操作编号
                result.Parameters = dt.Rows[i]["Parameters"].ToString().Trim();//其他操作参数
                result.precondition1Message = dt.Rows[i]["precondition1Message"].ToString().Trim();//使能条件1信息提示
                result.precondition2Message = dt.Rows[i]["precondition2Message"].ToString().Trim();//使能条件2信息提示
                result.Affair_Order = int.Parse(dt.Rows[i]["Affair_Order"] is DBNull ? "0" : dt.Rows[i]["Affair_Order"].ToString());//显示序列
                result.StatusDisplay = dt.Rows[i]["StatusDisplay"].ToString().Trim();//是否显示状态
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_affair", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    /// <summary>
    ///功能名称： 校验学生迎新批次
    ///功能描述：
    ///如果“学号”对应学生的“批次编号”等于参数“批次编号”返回true，否则返回false。
    ///编写人：胡元
    ///参数：
    ///PK_Batch_NO：迎新批次编号；PK_SNO：学号   
    ///创建时间：2017-1-31
    ///更新记录：无
    ///版本记录：v0.0.1
    public bool check_student_in_freshbatch(string PK_Batch_NO, string PK_SNO)
    {
        bool result =false;
        try
        {
            if (PK_Batch_NO == null || PK_Batch_NO.Trim().Length == 0 || PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                return result;
            }
            string sqlstr = "select * from vw_fresh_student_base where FK_Fresh_Batch=@cs1 and PK_SNO=@cs2";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()), new SqlParameter("cs2", PK_SNO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                result = true;
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "check_student_in_freshbatch", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称： 校验操作员操作对象
    ///功能描述：
    ///如果：操作员在某批次是否有效(批次编号,员工编号)==false，则返回false，否则根据“员工编号”检查被授权的“事务编号”，
    ///根据该操作员在该事务编号上被授权的“可操作院系”检查“学号”对应学生是否被包含其中，包含则返回true，否则返回false。
    ///编写人：胡元
    ///参数：
    ///PK_Staff_NO：迎新操作员编号；PK_Affair_NO：迎新事务编号；PK_SNO：学号   
    ///创建时间：2017-1-31
    ///更新记录：无
    ///版本记录：v0.0.1
    public bool check_operator_object(string PK_Staff_NO,string PK_Affair_NO, string PK_SNO)
    {
        bool result = false;
        try
        {
            if (PK_Staff_NO == null || PK_Staff_NO.Trim().Length == 0 || PK_Affair_NO == null || PK_Affair_NO.Trim().Length == 0 || PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                return result;
            }
            string sqlstr = "select * from vw_fresh_student_base where FK_Fresh_Batch+'_'+SPE_Code+'_'+[year] in ( "+
                            " select distinct(FK_Batch_NO+'_'+SPE_Code+'_'+[year]) from vw_operator_scope "+
                            " where FK_Staff_NO=@cs1 and PK_Affair_NO=@cs2)and PK_SNO=@cs3";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Staff_NO.Trim()), new SqlParameter("cs2", PK_Affair_NO.Trim()), new SqlParameter("cs3", PK_SNO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                result = true;
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "check_operator_object", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    ///功能名称： 获取某迎新事务操作
    ///功能描述：
    ///根据“事务编号”查找返回绑定的“操作定义”，否则返回null。
    ///编写人：胡元
    ///参数：
    ///PK_Affair_NO：迎新事务编号  
    ///创建时间：2017-1-31
    ///更新记录：无
    ///版本记录：v0.0.1
    public fresh_oper get_oper(string PK_Affair_NO)
    {
        fresh_oper result = null;
        try
        {
            if (PK_Affair_NO == null || PK_Affair_NO.Trim().Length == 0)
            {
                return null;
            }
            string sqlstr = "select b.* from Fresh_Affair a,Fresh_OPER b where a.FK_OPER_NO=b.PK_OPER_NO and PK_Affair_NO=@cs1";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Affair_NO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                int i = 0;
                result = new fresh_oper();
                result.PK_OPER_NO = dt.Rows[i]["PK_OPER_NO"].ToString().Trim();//操作编号
                result.OPER_Name = dt.Rows[i]["OPER_Name"].ToString().Trim();//操作名称
                result.OPER_URL = dt.Rows[i]["OPER_URL"].ToString().Trim();//地址 
                result.OPER_Type = dt.Rows[i]["OPER_Type"].ToString().Trim();//操作类型
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_oper", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    ///功能名称： 获取学生事务操作列表
    ///功能描述：
    ///根据“学号”查询所在批次中被授权“迎新事务”的“事务性质”为“交互性”和"状态性"，
    ///“事务类型”为“学生”或“两者”的数据。否则返回null。
    ///编写人：胡元
    ///参数：
    ///PK_SNO：学号  
    ///创建时间：2017-1-31
    ///更新记录：无
    ///版本记录：v0.0.1
    public List<fresh_affair> get_freshstudent_affair_list(string PK_SNO)
    {
        List<fresh_affair> result = null;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                return null;
            }

            string sqlstr = "select c.* from vw_fresh_student_base a,Fresh_Batch b,Fresh_Affair c " +
                            " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO " +
                            " and  a.PK_SNO=@cs1 and (upper(c.Affair_CHAR)='INTERACTIVE' or upper(c.Affair_CHAR)='STATUS') and (upper(c.Affair_Type)='STUDENT' or upper(c.Affair_Type)='BOTH')" +
                            " order by c.Affair_Order";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()));
            if (dt != null && dt.Rows.Count>0)
            {
                result = new List<fresh_affair>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    fresh_affair row = new fresh_affair();
                    row.PK_Affair_NO = dt.Rows[i]["PK_Affair_NO"].ToString().Trim();//迎新事务编号
                    row.FK_Batch_NO = dt.Rows[i]["FK_Batch_NO"].ToString().Trim();//迎新批次编号
                    row.Affair_Index = int.Parse(dt.Rows[i]["Affair_Index"] is DBNull ? "0" : dt.Rows[i]["Affair_Index"].ToString());//自编序号 
                    row.Affair_Name = dt.Rows[i]["Affair_Name"].ToString().Trim();//事务名称
                    row.Affair_Type = dt.Rows[i]["Affair_Type"].ToString().Trim();//事务类型
                    row.Precondition1 = dt.Rows[i]["Precondition1"].ToString().Trim();//使能条件1
                    row.Precondition2 = dt.Rows[i]["Precondition2"].ToString().Trim();//使能条件2
                    row.Call_Function = dt.Rows[i]["Call_Function"].ToString().Trim();//返回迎新事务状态调用函数
                    row.Affair_CHAR = dt.Rows[i]["Affair_CHAR"].ToString().Trim();//事务性质
                    row.FK_OPER_NO = dt.Rows[i]["FK_OPER_NO"].ToString().Trim();//与事务绑定的操作编号
                    row.Parameters = dt.Rows[i]["Parameters"].ToString().Trim();//其他操作参数
                    row.precondition1Message = dt.Rows[i]["precondition1Message"].ToString().Trim();//使能条件1信息提示
                    row.precondition2Message = dt.Rows[i]["precondition2Message"].ToString().Trim();//使能条件2信息提示
                    row.Affair_Order = int.Parse(dt.Rows[i]["Affair_Order"] is DBNull ? "0" : dt.Rows[i]["Affair_Order"].ToString());//显示序列
                    row.StatusDisplay = dt.Rows[i]["StatusDisplay"].ToString().Trim();//是否显示状态
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_freshstudent_affair_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    ///功能名称： 学生现场报到
    ///功能描述：
    ///根据“学号”修改“学生基本状态”为“报到”。
    ///编写人：胡元
    ///参数：
    ///PK_SNO：学号  
    ///创建时间：2017-1-31
    ///更新记录：无
    ///版本记录：v0.0.1
    public bool set_freshstudent_register(string PK_SNO)
    {
        bool result = false;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                return false;
            }

            string sqlstr = "select * from vw_fresh_student_base where PK_SNO=@cs1";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()));
            if (dt != null && dt.Rows.Count ==1)
            {
                sqlstr = "update Base_STU set Status_Code='已报到' where PK_SNO=@cs1";
                int jg=Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()));
                if (jg == 1){
                    result = true;
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "set_freshstudent_register", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    ///功能名称： 获取某学生现场迎新事务列表
    ///功能描述：
    ///1、	根据“学号”查找该学生所有“事务类型”为“现场”或“两者”的“学生迎新事务”列表，列表为空返回null。否则第2步。
    ///2、	扫描“学生迎新事务”列表中的每一项。
    ///3、	如果扫描项中的“迎新事务状态”等于“完成”，则扫描下一项。否则检查该项对应的“迎新事务定义”中的“返回迎新事务状态调用函数”（该函数返回“未完成”等）是否为空，如果为空则扫描下一项，否则调用“返回迎新事务状态调用函数”并将调用值赋值给“迎新事务状态”并保持到数据库中，然后扫描下一项。
    ///4、	返回“学生迎新事务”列表。
    ///编写人：胡元
    ///参数：
    ///PK_SNO：学号  
    ///创建时间：2017-1-31
    ///更新记录：无
    ///版本记录：v0.0.1
    public List<fresh_affair_log> get_schoolaffairlog_list(string PK_SNO)
    {
        List<fresh_affair_log> result = null;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                return null;
            }

            //string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
            //                "(case when d.Log_Status='已完成' or d.Log_Status=NULL then '已完成' else '未完成' end) as Log_Status," +
            //                "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
            //                " from vw_fresh_student_base a,Fresh_Batch b," +
            //                "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
            //                " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO" +
            //                " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='SCHOOL' or upper(c.Affair_Type)='BOTH')";
            string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
                            "(case when d.Log_Status is null then c.InitStatus else d.Log_Status end) as Log_Status," +
                            "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
                            " from vw_fresh_student_base a,Fresh_Batch b," +
                            "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
                            " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO" +
                            " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='SCHOOL' or upper(c.Affair_Type)='BOTH')";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()));
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<fresh_affair_log>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fresh_affair_log row = new fresh_affair_log();
                    row.PK_Affair_Log = dt.Rows[i]["PK_Affair_Log"].ToString().Trim();//学生迎新事务主键
                    row.FK_SNO = dt.Rows[i]["FK_SNO"].ToString().Trim();//学号
                    row.FK_Affair_NO = dt.Rows[i]["FK_Affair_NO"].ToString().Trim();//迎新事务编号 
                    row.Log_Status = dt.Rows[i]["Log_Status"].ToString().Trim();//事务状态
                    row.Creater = dt.Rows[i]["Creater"].ToString().Trim();//创建者
                    row.Create_DT = dt.Rows[i]["Create_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Create_DT"].ToString());//创建时间
                    row.Updater = dt.Rows[i]["Updater"].ToString().Trim();//更新者
                    row.Update_DT = dt.Rows[i]["Update_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Update_DT"].ToString());//更新时间

                    //if (dt.Rows[i]["Log_Status"].ToString().Trim() == "未完成" && dt.Rows[i]["Call_Function"] != null && dt.Rows[i]["Call_Function"].ToString().Trim().Length != 0)
                    if (dt.Rows[i]["Log_Status"].ToString().Trim() != "已完成" && dt.Rows[i]["Call_Function"] != null && dt.Rows[i]["Call_Function"].ToString().Trim().Length != 0)             
                    {
                        /*Call_Function格式，web服务器url地址?方法名称，例如：http://localhost:3893/test/WebService.asmx?test_Log_Status*/
                        try
                        {
                            string url = dt.Rows[i]["Call_Function"].ToString().Trim();
                            string[] parts = url.Split('?');
                            if (parts.Length != 2)
                            {
                                throw new Exception("函数格式错误");
                            }

                            url = parts[0];
                            string method = parts[1];
                            string[] args = new string[1];
                            args[0] = row.FK_SNO;
                            object data = WSHelper.InvokeWebService(url, method, args);
                            string jg = data.ToString().Trim();
                            row.Log_Status = jg.Trim();

                            if (row.PK_Affair_Log != null && row.PK_Affair_Log.Trim().Length != 0)
                            {
                                sqlstr = "update Fresh_Affair_Log set Log_Status=@cs3,Updater='system',Update_DT=getdate() where FK_SNO=@cs1 and FK_Affair_NO=@cs2";
                                Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()), new SqlParameter("cs3", row.Log_Status.Trim()));
                            }
                            else
                                if ((row.PK_Affair_Log == null || row.PK_Affair_Log.Trim().Length == 0))
                                {
                                    sqlstr = "insert into Fresh_Affair_Log (PK_Affair_Log,FK_SNO,FK_Affair_NO,Log_Status,Creater,Create_DT,Updater,Update_DT) values (" +
                                            "  newid(),@cs1,@cs2,@cs3,'system',getdate(),'system',getdate())";
                                    Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()), new SqlParameter("cs3", row.Log_Status.Trim()));
                                }
                        }
                        catch (Exception ex1)
                        {
                            row.Log_Status = "状态回调函数错误：" + ex1.Message;
                        }
                    }
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_schoolaffairlog_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    public List<fresh_affair_log> get_school_affair_affairlog_list(string PK_SNO, string PK_Affair_NO)
    {
        List<fresh_affair_log> result = null;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0 || PK_Affair_NO == null || PK_Affair_NO.Trim().Length == 0)
            {
                return null;
            }

            //string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
            //                "(case when d.Log_Status='已完成' or d.Log_Status=NULL then '已完成' else '未完成' end) as Log_Status," +
            //                "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
            //                " from vw_fresh_student_base a,Fresh_Batch b," +
            //                "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
            //                " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO" +
            //                " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='SCHOOL' or upper(c.Affair_Type)='BOTH')";
            string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
                            "(case when d.Log_Status is null then c.InitStatus else d.Log_Status end) as Log_Status," +
                            "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
                            " from vw_fresh_student_base a,Fresh_Batch b," +
                            "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
                            " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO  and c.PK_Affair_NO=@cs2" +
                            " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='SCHOOL' or upper(c.Affair_Type)='BOTH')";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", PK_Affair_NO.Trim()));
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<fresh_affair_log>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fresh_affair_log row = new fresh_affair_log();
                    row.PK_Affair_Log = dt.Rows[i]["PK_Affair_Log"].ToString().Trim();//学生迎新事务主键
                    row.FK_SNO = dt.Rows[i]["FK_SNO"].ToString().Trim();//学号
                    row.FK_Affair_NO = dt.Rows[i]["FK_Affair_NO"].ToString().Trim();//迎新事务编号 
                    row.Log_Status = dt.Rows[i]["Log_Status"].ToString().Trim();//事务状态
                    row.Creater = dt.Rows[i]["Creater"].ToString().Trim();//创建者
                    row.Create_DT = dt.Rows[i]["Create_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Create_DT"].ToString());//创建时间
                    row.Updater = dt.Rows[i]["Updater"].ToString().Trim();//更新者
                    row.Update_DT = dt.Rows[i]["Update_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Update_DT"].ToString());//更新时间

                    //if (dt.Rows[i]["Log_Status"].ToString().Trim() == "未完成" && dt.Rows[i]["Call_Function"] != null && dt.Rows[i]["Call_Function"].ToString().Trim().Length != 0)
                    if (dt.Rows[i]["Log_Status"].ToString().Trim() != "已完成" && dt.Rows[i]["Call_Function"] != null && dt.Rows[i]["Call_Function"].ToString().Trim().Length != 0)
                    {
                        /*Call_Function格式，web服务器url地址?方法名称，例如：http://localhost:3893/test/WebService.asmx?test_Log_Status*/
                        try
                        {
                            string url = dt.Rows[i]["Call_Function"].ToString().Trim();
                            string[] parts = url.Split('?');
                            if (parts.Length != 2)
                            {
                                throw new Exception("函数格式错误");
                            }

                            url = parts[0];
                            string method = parts[1];
                            string[] args = new string[1];
                            args[0] = row.FK_SNO;
                            object data = WSHelper.InvokeWebService(url, method, args);
                            string jg = data.ToString().Trim();
                            row.Log_Status = jg.Trim();

                            if (row.PK_Affair_Log != null && row.PK_Affair_Log.Trim().Length != 0)
                            {
                                sqlstr = "update Fresh_Affair_Log set Log_Status=@cs3,Updater='system',Update_DT=getdate() where FK_SNO=@cs1 and FK_Affair_NO=@cs2";
                                Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()), new SqlParameter("cs3", row.Log_Status.Trim()));
                            }
                            else
                                if ((row.PK_Affair_Log == null || row.PK_Affair_Log.Trim().Length == 0))
                                {
                                    sqlstr = "insert into Fresh_Affair_Log (PK_Affair_Log,FK_SNO,FK_Affair_NO,Log_Status,Creater,Create_DT,Updater,Update_DT) values (" +
                                            "  newid(),@cs1,@cs2,@cs3,'system',getdate(),'system',getdate())";
                                    Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()), new SqlParameter("cs3", row.Log_Status.Trim()));
                                }
                        }
                        catch (Exception ex1)
                        {
                            row.Log_Status = "状态回调函数错误：" + ex1.Message;
                        }
                    }
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_schoolaffairlog_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    public List<fresh_affair_log> get_schoolaffairlog_list_old(string PK_SNO)
    {
        List<fresh_affair_log> result = null;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                return null;
            }

            //string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
            //                "(case when d.Log_Status='已完成' or d.Log_Status=NULL then '已完成' else '未完成' end) as Log_Status," +
            //                "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
            //                " from vw_fresh_student_base a,Fresh_Batch b," +
            //                "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
            //                " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO" +
            //                " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='SCHOOL' or upper(c.Affair_Type)='BOTH')";
            string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
                            "(case when d.Log_Status='已完成' then '已完成' else '未完成' end) as Log_Status," +
                            "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
                            " from vw_fresh_student_base a,Fresh_Batch b," +
                            "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
                            " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO" +
                            " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='SCHOOL' or upper(c.Affair_Type)='BOTH')";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()));
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<fresh_affair_log>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fresh_affair_log row = new fresh_affair_log();
                    row.PK_Affair_Log = dt.Rows[i]["PK_Affair_Log"].ToString().Trim();//学生迎新事务主键
                    row.FK_SNO = dt.Rows[i]["FK_SNO"].ToString().Trim();//学号
                    row.FK_Affair_NO = dt.Rows[i]["FK_Affair_NO"].ToString().Trim();//迎新事务编号 
                    row.Log_Status = dt.Rows[i]["Log_Status"].ToString().Trim();//事务状态
                    row.Creater = dt.Rows[i]["Creater"].ToString().Trim();//创建者
                    row.Create_DT = dt.Rows[i]["Create_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Create_DT"].ToString());//创建时间
                    row.Updater = dt.Rows[i]["Updater"].ToString().Trim();//更新者
                    row.Update_DT = dt.Rows[i]["Update_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Update_DT"].ToString());//更新时间

                    if (dt.Rows[i]["Log_Status"].ToString().Trim() == "未完成" && dt.Rows[i]["Call_Function"] != null && dt.Rows[i]["Call_Function"].ToString().Trim().Length != 0)
                    {
                        /*Call_Function格式，web服务器url地址?方法名称，例如：http://localhost:3893/test/WebService.asmx?test_Log_Status*/
                        try
                        {
                            string url = dt.Rows[i]["Call_Function"].ToString().Trim();
                            string[] parts = url.Split('?');
                            if (parts.Length != 2)
                            {
                                throw new Exception("函数格式错误");
                            }

                            url = parts[0];
                            string method = parts[1];
                            string[] args = new string[1];
                            args[0] = row.FK_SNO;
                            object data = WSHelper.InvokeWebService(url, method, args);
                            string jg = data.ToString().Trim();
                            row.Log_Status = jg.Trim();

                            if (row.PK_Affair_Log != null && row.PK_Affair_Log.Trim().Length != 0 && jg.Trim() == "已完成")
                            {
                                sqlstr = "update Fresh_Affair_Log set Log_Status='已完成',Updater='system',Update_DT=getdate() where FK_SNO=@cs1 and FK_Affair_NO=@cs2";
                                Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()));
                            }
                            else
                                if ((row.PK_Affair_Log == null || row.PK_Affair_Log.Trim().Length == 0) && jg.Trim() == "已完成")
                                {
                                    sqlstr = "insert into Fresh_Affair_Log (PK_Affair_Log,FK_SNO,FK_Affair_NO,Log_Status,Creater,Create_DT,Updater,Update_DT) values (" +
                                            "  newid(),@cs1,@cs2,'已完成','system',getdate(),'system',getdate())";
                                    Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()));
                                }
                        }
                        catch (Exception ex1)
                        {
                            row.Log_Status = "状态回调函数错误：" + ex1.Message;
                        }
                    }
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_schoolaffairlog_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    ///功能名称： 获取某学生自助迎新事务列表
    ///功能描述：
    ///1、	根据“学号”查找该学生所有“事务类型”为“学生”或“两者”的“学生迎新事务”列表，列表为空返回null。否则第2步。
    ///2、	扫描“学生迎新事务”列表中的每一项。
    ///3、	如果扫描项中的“迎新事务状态”等于“完成”，则扫描下一项。否则检查该项对应的“迎新事务定义”中的“返回迎新事务状态调用函数”（该函数返回“未完成”等）是否为空，如果为空则扫描下一项，否则调用“返回迎新事务状态调用函数”并将调用值赋值给“迎新事务状态”并保持到数据库中，然后扫描下一项。
    ///4、	返回“学生迎新事务”列表。
    ///编写人：胡元
    ///参数：
    ///PK_SNO：学号  
    ///创建时间：2017-1-31
    ///更新记录：无
    ///版本记录：v0.0.1
    public List<fresh_affair_log> get_studentaffairlog_list(string PK_SNO)
    {
        List<fresh_affair_log> result = null;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                return null;
            }

            //string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
            //                "(case when d.Log_Status='已完成' or d.Log_Status=NULL then '已完成' else '未完成' end) as Log_Status," +
            //                "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
            //                " from vw_fresh_student_base a,Fresh_Batch b," +
            //                "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
            //                " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO" +
            //                " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='STUDENT' or upper(c.Affair_Type)='BOTH')";
            string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
                "(case when d.Log_Status is null then c.InitStatus else d.Log_Status end) as Log_Status," +
                "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
                " from vw_fresh_student_base a,Fresh_Batch b," +
                "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
                " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO" +
                " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='STUDENT' or upper(c.Affair_Type)='BOTH')";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()));
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<fresh_affair_log>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fresh_affair_log row = new fresh_affair_log();
                    row.PK_Affair_Log = dt.Rows[i]["PK_Affair_Log"].ToString().Trim();//学生迎新事务主键
                    row.FK_SNO = dt.Rows[i]["FK_SNO"].ToString().Trim();//学号
                    row.FK_Affair_NO = dt.Rows[i]["FK_Affair_NO"].ToString().Trim();//迎新事务编号 
                    row.Log_Status = dt.Rows[i]["Log_Status"].ToString().Trim();//事务状态
                    row.Creater = dt.Rows[i]["Creater"].ToString().Trim();//创建者
                    row.Create_DT = dt.Rows[i]["Create_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Create_DT"].ToString());//创建时间
                    row.Updater = dt.Rows[i]["Updater"].ToString().Trim();//更新者
                    row.Update_DT = dt.Rows[i]["Update_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Update_DT"].ToString());//更新时间

                    //if (dt.Rows[i]["Log_Status"].ToString().Trim() == "未完成" && dt.Rows[i]["Call_Function"] != null && dt.Rows[i]["Call_Function"].ToString().Trim().Length != 0)
                    if (dt.Rows[i]["Log_Status"].ToString().Trim() != "已完成" && dt.Rows[i]["Call_Function"] != null && dt.Rows[i]["Call_Function"].ToString().Trim().Length != 0)
                    {
                        try
                        {
                            string url = dt.Rows[i]["Call_Function"].ToString().Trim();
                            string[] parts = url.Split('?');
                            if (parts.Length != 2)
                            {
                                throw new Exception("函数格式错误");
                            }
                            url = parts[0];
                            string method = parts[1];
                            string[] args = new string[1];
                            args[0] = row.FK_SNO;
                            object data = WSHelper.InvokeWebService(url, method, args);//动态调用webservice格式的回调函数
                            string jg = data.ToString().Trim();
                            row.Log_Status = jg.Trim();

                            if (row.PK_Affair_Log != null && row.PK_Affair_Log.Trim().Length != 0)
                            {
                                sqlstr = "update Fresh_Affair_Log set Log_Status=@cs3,Updater='system',Update_DT=getdate() where FK_SNO=@cs1 and FK_Affair_NO=@cs2";
                                Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()), new SqlParameter("cs3", row.Log_Status.Trim()));
                            }
                            else
                                if ((row.PK_Affair_Log == null || row.PK_Affair_Log.Trim().Length == 0))
                                {
                                    sqlstr = "insert into Fresh_Affair_Log (PK_Affair_Log,FK_SNO,FK_Affair_NO,Log_Status,Creater,Create_DT,Updater,Update_DT) values (" +
                                            "  newid(),@cs1,@cs2,@cs3,'system',getdate(),'system',getdate())";
                                    Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()), new SqlParameter("cs3", row.Log_Status.Trim()));
                                }
                        }
                        catch (Exception ex1)
                        {
                            row.Log_Status = "状态回调函数错误："+ex1.Message;
                        } 
                    }
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_studentaffairlog_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    public List<fresh_affair_log> get_student_affair_affairlog_list(string PK_SNO,string AFFAIR_NO)
    {
        List<fresh_affair_log> result = null;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0 || AFFAIR_NO == null || AFFAIR_NO.Trim().Length == 0)
            {
                return null;
            }

            //string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
            //                "(case when d.Log_Status='已完成' or d.Log_Status=NULL then '已完成' else '未完成' end) as Log_Status," +
            //                "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
            //                " from vw_fresh_student_base a,Fresh_Batch b," +
            //                "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
            //                " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO" +
            //                " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='STUDENT' or upper(c.Affair_Type)='BOTH')";
            string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
                "(case when d.Log_Status is null then c.InitStatus else d.Log_Status end) as Log_Status," +
                "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
                " from vw_fresh_student_base a,Fresh_Batch b," +
                "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
                " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO and c.PK_Affair_NO=@cs2 " +
                " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='STUDENT' or upper(c.Affair_Type)='BOTH')";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", AFFAIR_NO.Trim()));
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<fresh_affair_log>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fresh_affair_log row = new fresh_affair_log();
                    row.PK_Affair_Log = dt.Rows[i]["PK_Affair_Log"].ToString().Trim();//学生迎新事务主键
                    row.FK_SNO = dt.Rows[i]["FK_SNO"].ToString().Trim();//学号
                    row.FK_Affair_NO = dt.Rows[i]["FK_Affair_NO"].ToString().Trim();//迎新事务编号 
                    row.Log_Status = dt.Rows[i]["Log_Status"].ToString().Trim();//事务状态
                    row.Creater = dt.Rows[i]["Creater"].ToString().Trim();//创建者
                    row.Create_DT = dt.Rows[i]["Create_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Create_DT"].ToString());//创建时间
                    row.Updater = dt.Rows[i]["Updater"].ToString().Trim();//更新者
                    row.Update_DT = dt.Rows[i]["Update_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Update_DT"].ToString());//更新时间

                    //if (dt.Rows[i]["Log_Status"].ToString().Trim() == "未完成" && dt.Rows[i]["Call_Function"] != null && dt.Rows[i]["Call_Function"].ToString().Trim().Length != 0)
                    if (dt.Rows[i]["Log_Status"].ToString().Trim() != "已完成" && dt.Rows[i]["Call_Function"] != null && dt.Rows[i]["Call_Function"].ToString().Trim().Length != 0)
                    {
                        try
                        {
                            string url = dt.Rows[i]["Call_Function"].ToString().Trim();
                            string[] parts = url.Split('?');
                            if (parts.Length != 2)
                            {
                                throw new Exception("函数格式错误");
                            }
                            url = parts[0];
                            string method = parts[1];
                            string[] args = new string[1];
                            args[0] = row.FK_SNO;
                            object data = WSHelper.InvokeWebService(url, method, args);//动态调用webservice格式的回调函数
                            string jg = data.ToString().Trim();
                            row.Log_Status = jg.Trim();

                            if (row.PK_Affair_Log != null && row.PK_Affair_Log.Trim().Length != 0)
                            {
                                sqlstr = "update Fresh_Affair_Log set Log_Status=@cs3,Updater='system',Update_DT=getdate() where FK_SNO=@cs1 and FK_Affair_NO=@cs2";
                                Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()), new SqlParameter("cs3", row.Log_Status.Trim()));
                            }
                            else
                                if ((row.PK_Affair_Log == null || row.PK_Affair_Log.Trim().Length == 0))
                                {
                                    sqlstr = "insert into Fresh_Affair_Log (PK_Affair_Log,FK_SNO,FK_Affair_NO,Log_Status,Creater,Create_DT,Updater,Update_DT) values (" +
                                            "  newid(),@cs1,@cs2,@cs3,'system',getdate(),'system',getdate())";
                                    Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()), new SqlParameter("cs3", row.Log_Status.Trim()));
                                }
                        }
                        catch (Exception ex1)
                        {
                            row.Log_Status = "状态回调函数错误：" + ex1.Message;
                        }
                    }
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_studentaffairlog_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }
    public List<fresh_affair_log> get_studentaffairlog_list_old(string PK_SNO)
    {
        List<fresh_affair_log> result = null;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                return null;
            }

            //string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
            //                "(case when d.Log_Status='已完成' or d.Log_Status=NULL then '已完成' else '未完成' end) as Log_Status," +
            //                "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
            //                " from vw_fresh_student_base a,Fresh_Batch b," +
            //                "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
            //                " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO" +
            //                " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='STUDENT' or upper(c.Affair_Type)='BOTH')";
            string sqlstr = "select d.PK_Affair_Log,a.PK_SNO as FK_SNO,c.PK_Affair_NO as FK_Affair_NO," +
                "(case when d.Log_Status='已完成' then '已完成' else '未完成' end) as Log_Status," +
                "d.Creater,d.Create_DT,d.Updater,d.Update_DT,c.Call_Function" +
                " from vw_fresh_student_base a,Fresh_Batch b," +
                "Fresh_Affair c LEFT JOIN (select * from Fresh_Affair_Log where FK_SNO=@cs1) d on c.PK_Affair_NO=d.FK_Affair_NO " +
                " where a.FK_Fresh_Batch=b.PK_Batch_NO and c.FK_Batch_NO=b.PK_Batch_NO" +
                " and  a.PK_SNO=@cs1 and upper(c.Affair_CHAR)='INTERACTIVE' and (upper(c.Affair_Type)='STUDENT' or upper(c.Affair_Type)='BOTH')";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()));
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<fresh_affair_log>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fresh_affair_log row = new fresh_affair_log();
                    row.PK_Affair_Log = dt.Rows[i]["PK_Affair_Log"].ToString().Trim();//学生迎新事务主键
                    row.FK_SNO = dt.Rows[i]["FK_SNO"].ToString().Trim();//学号
                    row.FK_Affair_NO = dt.Rows[i]["FK_Affair_NO"].ToString().Trim();//迎新事务编号 
                    row.Log_Status = dt.Rows[i]["Log_Status"].ToString().Trim();//事务状态
                    row.Creater = dt.Rows[i]["Creater"].ToString().Trim();//创建者
                    row.Create_DT = dt.Rows[i]["Create_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Create_DT"].ToString());//创建时间
                    row.Updater = dt.Rows[i]["Updater"].ToString().Trim();//更新者
                    row.Update_DT = dt.Rows[i]["Update_DT"] is DBNull ? DateTime.Now : DateTime.Parse(dt.Rows[i]["Update_DT"].ToString());//更新时间

                    if (dt.Rows[i]["Log_Status"].ToString().Trim() == "未完成" && dt.Rows[i]["Call_Function"] != null && dt.Rows[i]["Call_Function"].ToString().Trim().Length != 0)
                    {
                        try
                        {
                            string url = dt.Rows[i]["Call_Function"].ToString().Trim();
                            string[] parts = url.Split('?');
                            if (parts.Length != 2)
                            {
                                throw new Exception("函数格式错误");
                            }
                            url = parts[0];
                            string method = parts[1];
                            string[] args = new string[1];
                            args[0] = row.FK_SNO;
                            object data = WSHelper.InvokeWebService(url, method, args);//动态调用webservice格式的回调函数
                            string jg = data.ToString().Trim();
                            row.Log_Status = jg.Trim();

                            if (row.PK_Affair_Log != null && row.PK_Affair_Log.Trim().Length != 0 && jg.Trim() == "已完成")
                            {
                                sqlstr = "update Fresh_Affair_Log set Log_Status='已完成',Updater='system',Update_DT=getdate() where FK_SNO=@cs1 and FK_Affair_NO=@cs2";
                                Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()));
                            }
                            else
                                if ((row.PK_Affair_Log == null || row.PK_Affair_Log.Trim().Length == 0) && jg.Trim() == "已完成")
                                {
                                    sqlstr = "insert into Fresh_Affair_Log (PK_Affair_Log,FK_SNO,FK_Affair_NO,Log_Status,Creater,Create_DT,Updater,Update_DT) values (" +
                                            "  newid(),@cs1,@cs2,'已完成','system',getdate(),'system',getdate())";
                                    Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", row.FK_Affair_NO.Trim()));
                                }
                        }
                        catch (Exception ex1)
                        {
                            row.Log_Status = "状态回调函数错误：" + ex1.Message;
                        }
                    }
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_studentaffairlog_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    ///功能名称： 更新学生迎新事务状态
    ///功能描述：
    ///如果该“学号”所在迎新批次的“事务编号”没有对应的“学生迎新事务”记录，则创建记录，
    ///将对应记录的“迎新事务状态”标记为“事务状态”。。
    ///编写人：胡元
    ///参数：
    ///PK_SNO：学号；PK_Affair_NO：事务编号；Log_Status：事务状态；Creater：操作者姓名
    ///创建时间：2017-1-31
    ///更新记录：无
    ///版本记录：v0.0.1
    public bool set_affairlog(string PK_SNO, string PK_Affair_NO, string Log_Status, string Creater)
    {
        bool result = false;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0 || PK_Affair_NO == null || PK_Affair_NO.Trim().Length == 0
                || Log_Status == null || Log_Status.Trim().Length == 0 || Creater == null || Creater.Trim().Length == 0)
            {
                return result;
            }

            //if (Log_Status.Trim() != "未完成" && Log_Status.Trim() != "已完成")
            //{
            //    return result;
            //}

            string sqlstr = "select * from Fresh_Affair_Log where FK_SNO=@cs1 and FK_Affair_NO=@cs2";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", PK_Affair_NO.Trim()));
            if (dt != null && dt.Rows.Count ==1)
            {
                sqlstr = "update Fresh_Affair_Log set Log_Status=@cs3,Updater=@cs4,Update_DT=getdate() where FK_SNO=@cs1 and FK_Affair_NO=@cs2 ";
                int jg=Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", PK_Affair_NO.Trim()),
                    new SqlParameter("cs3", Log_Status.Trim()), new SqlParameter("cs4", Creater.Trim()));
                if (jg == 1)
                {
                    result = true;
                }
            }else
                if (dt == null || dt.Rows.Count == 0)
                {
                    sqlstr = "insert into Fresh_Affair_Log (PK_Affair_Log,FK_SNO,FK_Affair_NO,Log_Status,Creater,Create_DT,Updater,Update_DT) values (" +
                            "  newid(),@cs1,@cs2,@cs3,@cs4,getdate(),@cs4,getdate())";
                    int jg = Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", PK_Affair_NO.Trim()),
                        new SqlParameter("cs3", Log_Status.Trim()), new SqlParameter("cs4", Creater.Trim()));
                    if (jg == 1)
                    {
                        result = true;
                    }
                }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "set_affairlog", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    public bool set_affairlog_old(string PK_SNO, string PK_Affair_NO, string Log_Status, string Creater)
    {
        bool result = false;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0 || PK_Affair_NO == null || PK_Affair_NO.Trim().Length == 0
                || Log_Status == null || Log_Status.Trim().Length == 0 || Creater == null || Creater.Trim().Length == 0)
            {
                return result;
            }

            if (Log_Status.Trim() != "未完成" && Log_Status.Trim() != "已完成")
            {
                return result;
            }

            string sqlstr = "select * from Fresh_Affair_Log where FK_SNO=@cs1 and FK_Affair_NO=@cs2";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", PK_Affair_NO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                sqlstr = "update Fresh_Affair_Log set Log_Status=@cs3,Updater=@cs4,Update_DT=getdate() where FK_SNO=@cs1 and FK_Affair_NO=@cs2 ";
                int jg = Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", PK_Affair_NO.Trim()),
                    new SqlParameter("cs3", Log_Status.Trim()), new SqlParameter("cs4", Creater.Trim()));
                if (jg == 1)
                {
                    result = true;
                }
            }
            else
                if (dt == null || dt.Rows.Count == 0)
                {
                    sqlstr = "insert into Fresh_Affair_Log (PK_Affair_Log,FK_SNO,FK_Affair_NO,Log_Status,Creater,Create_DT,Updater,Update_DT) values (" +
                            "  newid(),@cs1,@cs2,@cs3,@cs4,getdate(),@cs4,getdate())";
                    int jg = Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", PK_Affair_NO.Trim()),
                        new SqlParameter("cs3", Log_Status.Trim()), new SqlParameter("cs4", Creater.Trim()));
                    if (jg == 1)
                    {
                        result = true;
                    }
                }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "set_affairlog", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    ///功能名称： 校验学生事务操作条件
    ///功能描述：
    ///根据“事务编号”获取“使能条件1”（前置迎新事务状态条件表达式）和“使能条件2”（前置迎新事务应收款的付款状态条件表达式），
    ///并将“学号”对应学生的状态应用到“使能条件”中，都满足返回true，否则返回false。
    ///编写人：胡元
    ///参数：
    ///PK_SNO：学号；PK_Affair_NO：事务编号；PK_SNO：学号
    ///创建时间：2017-2-1
    ///更新记录：无
    ///版本记录：v0.0.1
    public bool check_student_affair_condition(string PK_SNO, string PK_Affair_NO)
    {
        bool result = false;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0 || PK_Affair_NO == null || PK_Affair_NO.Trim().Length == 0)
            {
                return result;
            }

            string sqlstr = "select * from Fresh_Affair where PK_Affair_NO=@cs1";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Affair_NO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                string PK_Batch_NO=dt.Rows[0]["FK_Batch_NO"].ToString().Trim();
                if (dt.Rows[0]["Precondition1"] != null && dt.Rows[0]["Precondition1"] != System.DBNull.Value && dt.Rows[0]["Precondition1"].ToString().Trim().Length > 0)
                {
                    string status_condition = dt.Rows[0]["Precondition1"].ToString().Trim();//事务状态条件
                    result=analysis_status_condition(status_condition, PK_Batch_NO, PK_SNO);//前置条件解析
                }
                else
                {
                    result = true;//没有前置条件，返回真
                }

                //前置收费条件解析
                if (dt.Rows[0]["Precondition2"] != null && dt.Rows[0]["Precondition2"] != System.DBNull.Value && dt.Rows[0]["Precondition2"].ToString().Trim().Length > 0)
                {
                    string status_condition = dt.Rows[0]["Precondition2"].ToString().Trim();//收费状态条件
                    result = result && analysis_fee_condition(status_condition, PK_Batch_NO, PK_SNO);//前置收费条件解析
                }
                else
                {
                    result = result && true;//没有前置收费条件，返回真
                }
            }
            else
                throw new Exception("invalid PK_Affair_NO");
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "check_student_affair_condition", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //前置事务状态条件解析
    private bool analysis_status_condition(string status_condition, string PK_Batch_NO, string PK_SNO)
    {
        //status_condition的格式是：{@a1=已完成 and @a2=未完成}:{@a1,@a2}
        //a1中的a是必须的关键字符，a1中的1是迎新事务的索引号(一个迎新批次中的事务索引号唯一)。常量只能是"已完成"和"未完成"两种。
        string sqlstr = "";
        bool result = false;
        string[] arr = status_condition.Split(':');

        if (arr.Length != 2)
            throw new Exception("status_condition format error");

        arr[0] = arr[0].Substring(1, arr[0].Length - 2);
        string express = arr[0];

        arr[1] = arr[1].Substring(1, arr[1].Length - 2);
        if (arr[1].Trim().Length > 0)
        {
            List<fresh_affair_log> data1 = get_studentaffairlog_list(PK_SNO);
            List<fresh_affair_log> data2 = get_schoolaffairlog_list(PK_SNO);
            List<fresh_affair_log> affairlog = new List<fresh_affair_log>();//学生事务日志
            if (data1 != null && data1.Count > 0)
            {
                for (int i = 0; i < data1.Count; i++)
                {
                    affairlog.Add(data1[i]);
                }
            }
            if (data2 != null && data2.Count > 0)
            {
                if (affairlog.Count == 0)
                {
                    for (int i = 0; i < data2.Count; i++)
                    {
                        affairlog.Add(data2[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < data2.Count; i++)
                    {
                        bool havesame = false;
                        for (int j = 0; j < affairlog.Count; j++)
                        {
                            if (affairlog[j].FK_Affair_NO.Trim() == data2[i].FK_Affair_NO.Trim())
                            {
                                havesame = true;
                                break;
                            }
                        }
                        if (!havesame)
                        {
                            affairlog.Add(data2[i]);
                        }
                    }
                }
            }

            string[] parameters = arr[1].Split(',');

            for (int i = 0; i < parameters.Length; i++)
            {
                string index = parameters[i].Substring(2);
                sqlstr = "select PK_Affair_NO from Fresh_Affair where FK_Batch_NO=@cs1 and Affair_Index=@cs2";
                System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()), new SqlParameter("cs2", index.Trim()));
                if (dt != null && dt.Rows.Count == 1)
                {
                    string PK_Affair_NO = dt.Rows[0]["PK_Affair_NO"].ToString().Trim();
                    for (int j = 0; j < affairlog.Count; j++)
                    {
                        if (affairlog[j].FK_Affair_NO.Trim() == PK_Affair_NO.Trim())
                        {
                            express = express.Replace(parameters[i], "\'" + affairlog[j].Log_Status.Trim() + "\'");
                            break;
                        }
                    }
                }
            }
            express = express.Replace("\"","\'");
        }

        sqlstr = "select isnull((select 1  where " + express.Trim() + " ),0) as flag";
        System.Data.DataTable jg = Sqlhelper.Serach(sqlstr);
        if (jg.Rows[0]["flag"].ToString().Trim() == "1")
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

    private bool analysis_status_condition_old(string status_condition, string PK_Batch_NO, string PK_SNO)
    {
        //status_condition的格式是：{@a1=已完成 and @a2=未完成}:{@a1,@a2}
        //a1中的a是必须的关键字符，a1中的1是迎新事务的索引号(一个迎新批次中的事务索引号唯一)。常量只能是"已完成"和"未完成"两种。
        string sqlstr = "";
        bool result = false;
        string[] arr = status_condition.Split(':');

        if (arr.Length != 2)
            throw new Exception("status_condition format error");

        arr[0] = arr[0].Substring(1, arr[0].Length - 2);
        string express = arr[0];

        arr[1] = arr[1].Substring(1, arr[1].Length - 2);
        if (arr[1].Trim().Length > 0)
        {
            List<fresh_affair_log> data1 = get_studentaffairlog_list(PK_SNO);
            List<fresh_affair_log> data2 = get_schoolaffairlog_list(PK_SNO);
            List<fresh_affair_log> affairlog = new List<fresh_affair_log>();//学生事务日志
            if (data1 != null && data1.Count > 0)
            {
                for (int i = 0; i < data1.Count; i++)
                {
                    affairlog.Add(data1[i]);
                }
            }
            if (data2 != null && data2.Count > 0)
            {
                if (affairlog.Count == 0)
                {
                    for (int i = 0; i < data2.Count; i++)
                    {
                        affairlog.Add(data2[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < data2.Count; i++)
                    {
                        bool havesame = false;
                        for (int j = 0; j < affairlog.Count; j++)
                        {
                            if (affairlog[j].FK_Affair_NO.Trim() == data2[i].FK_Affair_NO.Trim())
                            {
                                havesame = true;
                                break;
                            }
                        }
                        if (!havesame)
                        {
                            affairlog.Add(data2[i]);
                        }
                    }
                }
            }

            string[] parameters = arr[1].Split(',');

            for (int i = 0; i < parameters.Length; i++)
            {
                string index = parameters[i].Substring(2);
                sqlstr = "select PK_Affair_NO from Fresh_Affair where FK_Batch_NO=@cs1 and Affair_Index=@cs2";
                System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()), new SqlParameter("cs2", index.Trim()));
                if (dt != null && dt.Rows.Count == 1)
                {
                    string PK_Affair_NO = dt.Rows[0]["PK_Affair_NO"].ToString().Trim();
                    for (int j = 0; j < affairlog.Count; j++)
                    {
                        if (affairlog[j].FK_Affair_NO.Trim() == PK_Affair_NO.Trim())
                        {
                            express = express.Replace(parameters[i], affairlog[j].Log_Status.Trim());
                            break;
                        }
                    }
                }
            }
            express = express.Replace("已完成", "\'已完成\'");
            express = express.Replace("未完成", "\'未完成\'");
        }

        sqlstr = "select isnull((select 1  where " + express.Trim() + " ),0) as flag";
        System.Data.DataTable jg = Sqlhelper.Serach(sqlstr);
        if (jg.Rows[0]["flag"].ToString().Trim() == "1")
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

    //前置收费条件解析
    public bool analysis_fee_condition(string fee_condition, string PK_Batch_NO, string PK_SNO)
    {
        ///fee_condition的格式是：{@f1=@fs1 and @f2>0}:{@f1=001,@f2=003}
        ///f1中的f是必须的关键字符，f1中的1是任意整数，@f1=001，f1代表'收费款项编号'(Fee_No)为'001'的学生
        ///实际缴费数,fs1代表'收费款项编号'(Fee_No)为'001'的收费标准。
        ///
        string sqlstr = "";
        bool result = false;
        string[] arr = fee_condition.Split(':');

        if (arr.Length != 2)
            throw new Exception("fee_condition format error");

        arr[0] = arr[0].Substring(1, arr[0].Length - 2);
        string express = arr[0];//表达式

        arr[1] = arr[1].Substring(1, arr[1].Length - 2);
        if (arr[1].Trim().Length > 0)
        {
            string[] arr1 = arr[1].Split(',');//参数变量数组
            List<string> parameter_list= new List<string>();//参数变量列表
            List<string> fee_no_list = new List<string>();//参数对应的财务收费编号列表

            for (int i = 0; i < arr1.Length; i++)
            {
                string[] arr2 = arr1[i].Trim().Split('=');
                parameter_list.Add(arr2[0]);//参数变量
                fee_no_list.Add(arr2[1]);//参数变量对应的财务收费编号
            }
            financial fsrv = new financial();
            for (int i = 0; i < parameter_list.Count; i++)
            {
                //获取学生收费条目实收金额 ssje= getssje(xh,fee_no_list[i])
                //获取学生收费条目收费标准金额 bzje= getbzje(xh,fee_no_list[i])
                //替换实收金额 express = express.Replace(parameter_list[i], ssje);
                //替换收费标准金额
                //string[] arr3 = parameter_list[i].Split(',')
                //string fs=arr3[0]+"s"+arr3[1];
                //express = express.Replace(fs, bzje);
                student_fee data=fsrv.get_student_fee(PK_Batch_NO, fee_no_list[i], PK_SNO);
                if (data == null)
                {
                    return false;
                }
                express = express.Replace(parameter_list[i], data.Fee_Payed.ToString().Trim());
                string[] arr3 = parameter_list[i].Split('f');
                string fs=arr3[0]+"fs"+arr3[1];
                express = express.Replace(fs, data.Fee_Amount.ToString().Trim());
            }

        }

        sqlstr = "select isnull((select 1  where " + express.Trim() + " ),0) as flag";
        System.Data.DataTable jg = Sqlhelper.Serach(sqlstr);
        if (jg.Rows[0]["flag"].ToString().Trim() == "1")
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }


    ///功能名称： 校验某事务对应操作的应用密码是否与提供的应用密码一致
    ///功能描述：
    ///根据“事务编号”获取“使能条件1”（前置迎新事务状态条件表达式）和“使能条件2”（前置迎新事务应收款的付款状态条件表达式），
    ///并将“学号”对应学生的状态应用到“使能条件”中，都满足返回true，否则返回false。
    ///编写人：胡元
    ///参数：
    ///PK_AFFAIR_NO：事务编号；APPKEY：提供的应用密码；
    ///创建时间：2017-4-18
    ///更新记录：无
    ///版本记录：v0.0.1
    public bool is_affair_operate_appKey(string PK_AFFAIR_NO, string APPKEY)
    {
        bool result = false;
        try
        {
            if (PK_AFFAIR_NO == null || PK_AFFAIR_NO.Trim().Length == 0 || APPKEY == null || APPKEY.Trim().Length == 0)
            {
                return result;
            }
            string sqlstr = "select * from Fresh_Affair a,Fresh_OPER b where  a.PK_Affair_NO=@cs1 and b.APP_Key=@cs2 and a.FK_OPER_NO=b.PK_OPER_NO";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_AFFAIR_NO.Trim()), new SqlParameter("cs2", APPKEY.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                result = true;
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "is_affair_operate_appKey", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    //判断学生或管理员对指定事务和事务中的操作是否满足其授权条件，满足返回字符串true,否则返回提示信息

    public affair_operate_auth_msg affair_operate_auth(string PK_AFFAIR_NO, string PK_SNO,string SESSION_PK_SNO, string PK_STAFF_NO,string SESSION_PK_STAFF_NO, string APPKEY)
    {
        affair_operate_auth_msg result = new affair_operate_auth_msg();
        result.isauth = false;
        result.msg = "";
        try
        {
            if (PK_AFFAIR_NO == null || PK_AFFAIR_NO.Trim().Length == 0 || APPKEY == null || APPKEY.Trim().Length == 0 || PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                result.msg = "参数错误";
                return result;
            }
            batch batch_logic = new batch();
            bool flag = batch_logic.is_affair_operate_appKey(PK_AFFAIR_NO, APPKEY);//本应用验证码是否正确
            if (!flag)
            {
                result.msg = "非授权访问";
                return result;
            }

            if (PK_STAFF_NO == null || PK_STAFF_NO.Trim().Length == 0)
            {
                PK_STAFF_NO = "";
                //是学生自主登陆，验证pk_sno是是否与当前学生登陆帐户一致
                if (SESSION_PK_SNO == null || !SESSION_PK_SNO.Trim().Equals(PK_SNO.Trim()))
                {
                    result.msg = "非授权访问";
                    return result;
                }
            }
            else
            {
                //验证现场迎新操作员是否登陆
                if (SESSION_PK_STAFF_NO == null || !SESSION_PK_STAFF_NO.Trim().Equals(PK_STAFF_NO.Trim()))
                {
                    result.msg = "非授权访问";
                    return result;
                }
                //验证迎新管理元是否具备操作权限
                flag = batch_logic.check_operator_object(PK_STAFF_NO, PK_AFFAIR_NO, PK_SNO);
                if (!flag)
                {
                    result.msg = "非授权访问";
                    return result;
                }
            }

            //验证学生是否具备该事务操作权限
            flag = true;
            if (PK_STAFF_NO == null || PK_STAFF_NO.Trim().Length == 0)
            {
                flag = batch_logic.check_student_affair_condition(PK_SNO, PK_AFFAIR_NO);//验证该生本事务操作前置条件是否满足
            }
            if (!flag)
            {
                //前置条件不满足
                fresh_affair data1 = batch_logic.get_affair(PK_AFFAIR_NO);
                if (data1 != null)
                {
                    result.msg = data1.precondition1Message + "," + data1.precondition2Message;
                    return result;
                }
                else
                {
                    result.msg = "获取事务前置条件时错误";
                    return result;
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("xsbjf.aspx.cs", "affair_operate_auth", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        result.isauth = true;
        return result;
    }


    ///功能名称： 插入学生助学贷款或绿色通道标志
    ///功能描述：
    ///插入学生助学贷款或绿色通道标志。
    ///编写人：胡元
    ///参数：
    ///PK_SNO：学号；Tuition：绿色通道或助学贷款
    ///创建时间：2017-4-26
    ///更新记录：无
    ///版本记录：v0.0.1
    public bool set_TuitionFee(string PK_SNO, string Tuition)
    {
        bool result = false;
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0 || Tuition == null || Tuition.Trim().Length == 0)
            {
                return result;
            }

            if (Tuition.Trim() != "绿色通道" && Tuition.Trim() != "助学贷款")
            {
                return result;
            }

            string sqlstr = "insert into Fresh_TuitionFee (PK_SNO,Tuition) values (@cs1,@cs2)";
            int jg = Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", Tuition.Trim()));
            if (jg == 1)
            {
                result = true;
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "set_TuitionFee", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }



    /// <summary>
    ///功能名称：获取当前有效的迎新批次目录
    ///功能描述：
    ///返回当前时间介于“迎新工作开始时间”和“迎新工作结束时间”之间迎新批次数据集合。否则返回null。
    ///编写人：胡元
    ///创建时间：2017-4-27
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public List<fresh_batch> get_freshbatch_welcome_list()
    {
        List<fresh_batch> result = null;
        try
        {
            string sqlstr = "select * from fresh_batch where Welcome_Begin<getdate() and Welcome_End>getdate() order by Welcome_Begin desc";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<fresh_batch>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fresh_batch row = new fresh_batch();
                    row.Batch_Name = dt.Rows[i]["Batch_Name"].ToString().Trim();
                    row.Enabled = dt.Rows[i]["Enabled"].ToString().Trim();
                    row.PK_Batch_NO = dt.Rows[i]["PK_Batch_NO"].ToString().Trim();
                    row.Service_Begin = DateTime.Parse(dt.Rows[i]["Service_Begin"].ToString());
                    row.Service_End = DateTime.Parse(dt.Rows[i]["Service_End"].ToString());
                    row.STU_Type = dt.Rows[i]["STU_Type"].ToString().Trim();
                    row.Welcome_Begin = DateTime.Parse(dt.Rows[i]["Welcome_Begin"].ToString());
                    row.Welcome_End = DateTime.Parse(dt.Rows[i]["Welcome_End"].ToString());
                    row.Year = dt.Rows[i]["Year"].ToString().Trim();
                    row.Financial_PK_Fee = dt.Rows[i]["Financial_PK_Fee"].ToString().Trim();
                    row.Financial_XH_Prefix = dt.Rows[i]["Financial_XH_Prefix"].ToString().Trim();
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_freshbatch_welcome_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称：获取可分配给迎新操作员的事务操作列表
    ///功能描述：
    ///根据“批次编号”查询Affair_Type为both和school的事务目录，
    ///编写人：胡元
    ///参数：
    ///PK_Batch_NO：迎新批次编号   
    ///创建时间：2017-4-27
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public List<fresh_affair> get_affair_list(string PK_Batch_NO)
    {
        List<fresh_affair> result = null;
        try
        {
            if (PK_Batch_NO == null || PK_Batch_NO.Trim().Length == 0)
            {
                return null;
            }
            string sqlstr = "select * from Fresh_Affair" +
             " where FK_Batch_NO=@cs1 and (upper(Affair_Type)='SCHOOL' OR upper(Affair_Type)='BOTH')";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Batch_NO.Trim()));
            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<fresh_affair>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    fresh_affair row = new fresh_affair();
                    row.PK_Affair_NO = dt.Rows[i]["PK_Affair_NO"].ToString().Trim();//迎新事务编号
                    row.FK_Batch_NO = dt.Rows[i]["FK_Batch_NO"].ToString().Trim();//迎新批次编号
                    row.Affair_Index = int.Parse(dt.Rows[i]["Affair_Index"] is DBNull ? "0" : dt.Rows[i]["Affair_Index"].ToString());//自编序号 
                    row.Affair_Name = dt.Rows[i]["Affair_Name"].ToString().Trim();//事务名称
                    row.Affair_Type = dt.Rows[i]["Affair_Type"].ToString().Trim();//事务类型
                    row.Precondition1 = dt.Rows[i]["Precondition1"].ToString().Trim();//使能条件1
                    row.Precondition2 = dt.Rows[i]["Precondition2"].ToString().Trim();//使能条件2
                    row.Call_Function = dt.Rows[i]["Call_Function"].ToString().Trim();//返回迎新事务状态调用函数
                    row.Affair_CHAR = dt.Rows[i]["Affair_CHAR"].ToString().Trim();//事务性质
                    row.FK_OPER_NO = dt.Rows[i]["FK_OPER_NO"].ToString().Trim();//与事务绑定的操作编号
                    row.Parameters = dt.Rows[i]["Parameters"].ToString().Trim();//其他操作参数
                    row.precondition1Message = dt.Rows[i]["precondition1Message"].ToString().Trim();//使能条件1信息提示
                    row.precondition2Message = dt.Rows[i]["precondition2Message"].ToString().Trim();//使能条件2信息提示
                    row.Affair_Order = int.Parse(dt.Rows[i]["Affair_Order"] is DBNull ? "0" : dt.Rows[i]["Affair_Order"].ToString());//显示序列
                    row.StatusDisplay = dt.Rows[i]["StatusDisplay"].ToString().Trim();//是否显示状态
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_affair_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    /// <summary>
    ///功能名称：获取某事务下所有授权员工及操作范围数据
    ///功能描述：
    ///获取某事务下所有授权员工及操作范围数据
    ///编写人：胡元
    ///参数：
    ///PK_Affair_NO：事务编号   
    ///创建时间：2017-4-27
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public System.Data.DataTable staff_affair_auth_scope(string PK_Affair_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            if (PK_Affair_NO == null || PK_Affair_NO.Trim().Length == 0)
            {
                return null;
            }
            string sqlstr = "select * from vw_staff_affair_auth_scope" +
             " where PK_Affair_NO=@cs1 order by name";
            result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Affair_NO.Trim()));
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_affair_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    /// <summary>
    ///功能名称：获取用户列表
    ///功能描述：
    ///获取用户列表
    ///编写人：胡元
    ///参数：
    ///PK_Affair_NO：事务编号   
    ///创建时间：2017-4-27
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public System.Data.DataTable get_yonghqx(string username)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr=null;
            if (username == null || username.Trim().Length == 0)
            {
                sqlstr = "select * from yonghqx order by xm";
            }
            else
            {
                username = username.Trim() + "%";
                sqlstr = "select * from yonghqx" +
                " where xm like @cs1 order by xm";
            }

            result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", username.Trim()));
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_affair_list", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称：获取有效的学院列表
    ///功能描述：
    ///获取用户列表
    ///编写人：胡元
    ///参数：
    ///创建时间：2017-5-2
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public System.Data.DataTable get_collegelist()
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            sqlstr = "select * from Base_College where enabled='true' order by name";

            result = Sqlhelper.Serach(sqlstr);
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_college", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称：修改迎新现场操作员权限
    ///功能描述：
    ///获取用户列表
    ///编写人：胡元
    ///参数：
    ///创建时间：2017-5-2
    ///更新记录：无
    ///版本记录：v0.0.1
    /// </summary>
    /// <returns></returns>
    public bool modify_operator_auth(string PK_BATCH_NO, string PK_STAFF_NO, string PK_Affair_NO, string[] COLLEGELIST)
    {
        bool result = false;
        try
        {
            if (PK_BATCH_NO == null || PK_BATCH_NO.Trim().Length == 0 || PK_Affair_NO == null || PK_Affair_NO.Trim().Length == 0
                || PK_STAFF_NO == null || PK_STAFF_NO.Trim().Length == 0)
            {
                return result;
            }
            int jg = 0;
            string sqlstr = "select * from Base_Staff"
                            + " where PK_Staff_NO=@cs1";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_STAFF_NO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
            }
            else
            {
                sqlstr = "select * from yonghqx"
                            + " where yhid=@cs1";
                dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_STAFF_NO.Trim()));
                if (dt == null || dt.Rows.Count == 0)
                {
                    throw new Exception("无效的用户编号");
                }

                string xm = "";
                if (dt.Rows[0]["xm"] != null && !Convert.IsDBNull(dt.Rows[0]["xm"]))
                {
                    xm = dt.Rows[0]["xm"].ToString().Trim();
                }

                sqlstr = "insert into Base_Staff (PK_Staff_NO,Phone,FK_College_NO,Name,Gender,Password) values ("
                    + "@cs1,'','',@cs2,'','')";
                jg = Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_STAFF_NO.Trim()), new SqlParameter("cs2", xm.Trim()));
                if (jg == 0)
                {
                    throw new Exception("insert错误");
                }
            }


            //根据PK_STAFF_NO和PK_BATCH_NO查看Fresh_Operator_AUTH是否有记录，没有即添加，否则取出PK_STAFF_NO对应的PK_Operator_AUTH
            sqlstr = "select * from Fresh_Operator_AUTH"
                            + " where FK_Fresh_Batch=@cs1 and FK_Staff_NO=@cs2";
            dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", PK_STAFF_NO.Trim()));

            string PK_Operator_AUTH=null;
            if (dt!=null && dt.Rows.Count==1)
            {
                PK_Operator_AUTH=dt.Rows[0]["PK_Operator_AUTH"].ToString().Trim();
            }else{
                PK_Operator_AUTH = Guid.NewGuid().ToString().Trim();
                sqlstr = "insert into Fresh_Operator_AUTH (PK_Operator_AUTH,FK_Fresh_Batch,FK_Staff_NO,Enabled) values (" 
                    +"  @cs1,@cs2,@cs3,'enabled')";
                jg = Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_Operator_AUTH.Trim()), new SqlParameter("cs2", PK_BATCH_NO.Trim()),
                new SqlParameter("cs3", PK_STAFF_NO.Trim()));
                if(jg==0){
                    throw new Exception("insert错误");
                }
            }
            //根据PK_Operator_AUTH和PK_Affair_NO查看Fresh_Affair_AUTH，没有即添加，否则取出PK_Affair_AUTH
            string PK_Affair_AUTH = null;
            sqlstr = "select * from Fresh_Affair_AUTH"
                   + " where FK_Operator_AUTH=@cs1 and FK_Affair_NO=@cs2";
            dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_Operator_AUTH.Trim()), new SqlParameter("cs2", PK_Affair_NO.Trim()));
            if (dt != null && dt.Rows.Count == 1)
            {
                PK_Affair_AUTH = dt.Rows[0]["PK_Affair_AUTH"].ToString().Trim();
            }
            else
            {
                PK_Affair_AUTH = Guid.NewGuid().ToString().Trim();
                sqlstr = "insert into Fresh_Affair_AUTH (PK_Affair_AUTH,FK_Operator_AUTH,FK_Affair_NO) values ("
                    + "  @cs1,@cs2,@cs3)";
                jg = Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_Affair_AUTH.Trim()), new SqlParameter("cs2", PK_Operator_AUTH.Trim()),
                new SqlParameter("cs3", PK_Affair_NO.Trim()));
                if (jg == 0)
                {
                    throw new Exception("insert错误");
                }
            }
            //根据PK_Affair_AUTH删除Fresh_OPER_Scope中的记录，并根据COLLEGELIST添加记录
            sqlstr = "delete from Fresh_OPER_Scope where FK_Affair_AUTH =@cs1";
            Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_Affair_AUTH.Trim()));
            bool isnull = true;
            for (int i = 0; COLLEGELIST != null && COLLEGELIST.Length > i; i++)
            {
                if (COLLEGELIST[i] != null && COLLEGELIST[i].Trim().Length > 0)
                {
                    isnull = false;
                    string PK_OPER_Scope = Guid.NewGuid().ToString().Trim();
                    sqlstr = "insert into Fresh_OPER_Scope (PK_OPER_Scope,FK_Affair_AUTH,FK_College_NO) values ("
                                        + "  @cs1,@cs2,@cs3)";
                    jg = Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_OPER_Scope.Trim()), new SqlParameter("cs2", PK_Affair_AUTH.Trim()),
                    new SqlParameter("cs3", COLLEGELIST[i].Trim()));
                    if (jg == 0)
                    {
                        throw new Exception("insert错误");
                    }
                }
            }

            if (isnull)
            {
                sqlstr = "delete from Fresh_Affair_AUTH"
                       + " where FK_Operator_AUTH=@cs1 and FK_Affair_NO=@cs2";
                Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_Operator_AUTH.Trim()), new SqlParameter("cs2", PK_Affair_NO.Trim()));
            }

        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "modify_operator_auth", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }




    //某迎新批次全部或某学院数据
    public System.Data.DataTable get_batch_collage(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0) 
            {
                sqlstr = "select distinct collage,College_NO from vw_fresh_student_base where FK_Fresh_Batch=@cs1 order by collage";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select distinct collage,College_NO from vw_fresh_student_base where FK_Fresh_Batch=@cs1 and College_NO=@cs2 order by collage";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_collage", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校或某学院专业数据
    public System.Data.DataTable get_batch_spe(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;

            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select college_no,collage,spe_code,spe_name,count(*) as stotal  from vw_fresh_student_base"
                        + " where FK_Fresh_Batch=@cs1"
                        + " GROUP BY college_no,collage,spe_code,spe_name order by SPE_Name";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select college_no,collage,spe_code,spe_name,count(*) as stotal  from vw_fresh_student_base "
                        + " where FK_Fresh_Batch=@cs1  and College_NO=@cs2"
                        + " GROUP BY college_no,collage,spe_code,spe_name order by SPE_Name";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }

        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_spe", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校或某学院学生男、女性别数据
    public System.Data.DataTable get_batch_student_gender(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select b.Item_Name as gender,count(*) as gendercount from vw_fresh_student_base a,Base_Code_Item b"
                        + " where a.Gender_Code=b.Item_NO and b.FK_Code='002' and a.FK_Fresh_Batch=@cs1 group by b.Item_Name";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select b.Item_Name as gender,count(*) as gendercount from vw_fresh_student_base a,Base_Code_Item b"
                        + " where a.Gender_Code=b.Item_NO and b.FK_Code='002' and a.FK_Fresh_Batch=@cs1 and a.College_NO=@cs2 group by b.Item_Name";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_student_gender", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }
    //某迎新批次全校或某学院已设置班级数据
    public System.Data.DataTable get_batch_spe_hasclass(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select ClassName,PK_Class_NO,FK_Fresh_Batch,year,Campus_Name,Campus_NO,Collage,College_NO,SPE_Name,SPE_Code from "
                        +" (select a.FK_Fresh_Batch,a.[year], d.Campus_Name,d.Campus_NO,a.Collage,a.College_NO ,a.SPE_Name,a.SPE_Code,"
                        +"c.Name as ClassName ,c.PK_Class_NO"
                        +" from vw_fresh_student_base a,Fresh_spe b,Fresh_Class c,Base_Campus d"
                        +" where a.SPE_Code=b.SPE_Code and a.[year]=b.[Year] and c.FK_SPE_NO=b.PK_SPE and c.FK_Campus_NO=d.PK_Campus"
                        +" and FK_Fresh_Batch=@cs1"
                        +" GROUP BY a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name,d.Campus_NO,d.Campus_Name,"
                        +" c.PK_Class_NO,c.Name ) as tb";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select ClassName,PK_Class_NO,FK_Fresh_Batch,year,Campus_Name,Campus_NO,Collage,College_NO,SPE_Name,SPE_Code from "
                        + " (select a.FK_Fresh_Batch,a.[year], d.Campus_Name,d.Campus_NO,a.Collage,a.College_NO ,a.SPE_Name,a.SPE_Code,"
                        + "c.Name as ClassName ,c.PK_Class_NO"
                        + " from vw_fresh_student_base a,Fresh_spe b,Fresh_Class c,Base_Campus d"
                        + " where a.SPE_Code=b.SPE_Code and a.[year]=b.[Year] and c.FK_SPE_NO=b.PK_SPE and c.FK_Campus_NO=d.PK_Campus"
                        + " and FK_Fresh_Batch=@cs1 and College_NO=@cs2"
                        + " GROUP BY a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name,d.Campus_NO,d.Campus_Name,"
                        + " c.PK_Class_NO,c.Name) as tb";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_spe_hasclass", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校或某学院未设置班级的专业
    public System.Data.DataTable get_batch_spe_nohasclass(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select fk_fresh_batch,year,collage,college_no,spe_name,spe_code from "
                        +" (select a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name"
                        +" from vw_fresh_student_base a,Fresh_spe b"
                        +" where a.SPE_Code=b.SPE_Code and a.[year]=b.[Year] "
                        +" and FK_Fresh_Batch=@cs1"
                        +" GROUP BY a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name) as tb1"
                        +" where tb1.SPE_Code not in "
                        +" (select tb2.SPE_Code from "
                        +"(select a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name,d.Campus_NO,"
                        +" d.Campus_Name,c.PK_Class_NO,c.Name as ClassName"
                        +" from vw_fresh_student_base a,Fresh_spe b,Fresh_Class c,Base_Campus d"
                        +" where a.SPE_Code=b.SPE_Code and a.[year]=b.[Year] and c.FK_SPE_NO=b.PK_SPE and c.FK_Campus_NO=d.PK_Campus"
                        + " and FK_Fresh_Batch=@cs1"
                        +" GROUP BY a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name,d.Campus_NO,d.Campus_Name,"
                        +" c.PK_Class_NO,c.Name) as tb2)";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select fk_fresh_batch,year,collage,college_no,spe_name,spe_code from "
                        + " (select a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name"
                        + " from vw_fresh_student_base a,Fresh_spe b"
                        + " where a.SPE_Code=b.SPE_Code and a.[year]=b.[Year] "
                        + " and FK_Fresh_Batch=@cs1 and College_NO=@cs2"
                        + " GROUP BY a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name) as tb1"
                        + " where tb1.SPE_Code not in "
                        + " (select tb2.SPE_Code from "
                        + "(select a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name,d.Campus_NO,"
                        + " d.Campus_Name,c.PK_Class_NO,c.Name as ClassName"
                        + " from vw_fresh_student_base a,Fresh_spe b,Fresh_Class c,Base_Campus d"
                        + " where a.SPE_Code=b.SPE_Code and a.[year]=b.[Year] and c.FK_SPE_NO=b.PK_SPE and c.FK_Campus_NO=d.PK_Campus"
                        + " and FK_Fresh_Batch=@cs1 and College_NO=@cs2"
                        + " GROUP BY a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name,d.Campus_NO,d.Campus_Name,"
                        + " c.PK_Class_NO,c.Name) as tb2)";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_spe_nohasclass", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校或某学院班级中有学生的班级数据
    public System.Data.DataTable get_batch_class_hasstudent(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select classname,PK_Class_NO,studentcount,year,Collage,SPE_Name from ("
                        +" select a.[year],a.Collage,a.SPE_Name,b.Name as classname,b.PK_Class_NO,count(*) as studentcount"
                        +" from vw_fresh_student_base a,Fresh_Class b"
                        +" where (a.FK_Class_NO is not null or len(rtrim(ltrim(a.FK_Class_NO)))>0) and a.FK_Class_NO=b.PK_Class_NO and a.FK_Fresh_Batch=@cs1"
                        +" GROUP BY a.[year],a.Collage,a.SPE_Name,b.Name,b.PK_Class_NO) as tb";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select classname,PK_Class_NO,studentcount,year,Collage,SPE_Name from ("
                        +" select a.[year],a.Collage,a.SPE_Name,b.Name as classname,b.PK_Class_NO,count(*) as studentcount"
                        + " from vw_fresh_student_base a,Fresh_Class b"
                        + " where (a.FK_Class_NO is not null or len(rtrim(ltrim(a.FK_Class_NO)))>0) and a.FK_Class_NO=b.PK_Class_NO and a.FK_Fresh_Batch=@cs1 and a.College_NO=@cs2"
                        + " GROUP BY a.[year],a.Collage,a.SPE_Name,b.Name,b.PK_Class_NO) as tb";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_class_hasstudent", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校或某学院存在未分班学生的专业数据
    public System.Data.DataTable get_batch_spe_nohasclassstudent(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select a.[year],a.Collage,a.SPE_Name,count(*) as studentcount"
                        +" from vw_fresh_student_base a"
                        +" where (a.FK_Class_NO is null or len(rtrim(ltrim(a.FK_Class_NO)))=0) and a.FK_Fresh_Batch=@cs1"
                        +" group by a.[year],a.Collage,a.SPE_Name";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select a.[year],a.Collage,a.SPE_Name,count(*) as studentcount"
                        + " from vw_fresh_student_base a"
                        + " where (a.FK_Class_NO is null or len(rtrim(ltrim(a.FK_Class_NO)))=0) and a.FK_Fresh_Batch=@cs1 and a.College_NO=@cs2"
                        + " group by a.[year],a.Collage,a.SPE_Name";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_spe_nohasclassstudent", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校或某学院分错专业的学生数据
    public System.Data.DataTable get_batch_class_hasstudent_buterror(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select a.[year],b.Name as ClassName,a.FK_Class_NO,a.Collage,a.SPE_Code,a.SPE_Name,a.Name,a.PK_SNO,a.Test_NO,"
                        +" d.Name as Class_Collage,c.SPE_Code as Class_SPE_Code,c.SPE_Name as Class_SPE_Name"
                        +" from vw_fresh_student_base a,Fresh_Class b,Fresh_SPE c,Base_College d"
                        +" where (a.FK_Class_NO is not null or len(rtrim(ltrim(a.FK_Class_NO)))>0) and a.FK_Fresh_Batch=@cs1"
                        +" and a.FK_Class_NO=b.PK_Class_NO and b.FK_SPE_NO=c.PK_SPE and c.FK_College_Code=d.PK_College and a.SPE_Code<>c.SPE_Code"
                        +" order by classname";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select a.[year],b.Name as ClassName,a.FK_Class_NO,a.Collage,a.SPE_Code,a.SPE_Name,a.Name,a.PK_SNO,a.Test_NO,"
                        + " d.Name as Class_Collage,c.SPE_Code as Class_SPE_Code,c.SPE_Name as Class_SPE_Name"
                        + " from vw_fresh_student_base a,Fresh_Class b,Fresh_SPE c,Base_College d"
                        + " where (a.FK_Class_NO is not null or len(rtrim(ltrim(a.FK_Class_NO)))>0) and a.FK_Fresh_Batch=@cs1 and a.College_NO=@cs2"
                        + " and a.FK_Class_NO=b.PK_Class_NO and b.FK_SPE_NO=c.PK_SPE and c.FK_College_Code=d.PK_College and a.SPE_Code<>c.SPE_Code"
                        + " order by classname";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_class_hasstudent_buterror", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校或某学院学生已预分床位数据
    public System.Data.DataTable get_batch_hasbed(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select class_campus_name,collegename,spe_name,classname,pk_class_no,gender,dormname,count(*) as bedcount"
                        + " from ("
                        + " select class_campus_name,collegename,spe_name,classname,pk_class_no,gender,dormname,floor,type_name,room_no,"
                        + " bed_no,bed_name,FK_Fresh_Batch"
                        + " from vw_class_beds a, "
                        + " (select FK_Fresh_Batch,FK_Class_NO from vw_fresh_student_base group by FK_Fresh_Batch,FK_Class_NO) as b "
                        + " where a.PK_Class_NO=b.FK_Class_NO and b.FK_Fresh_Batch=@cs1) as tb"
                        + " group by class_campus_name,collegename,spe_name,classname,pk_class_no,gender,dormname";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select class_campus_name,collegename,spe_name,classname,pk_class_no,gender,dormname,count(*) as bedcount" 
                        +" from ("
                        +" select class_campus_name,collegename,spe_name,classname,pk_class_no,gender,dormname,floor,type_name,room_no,"
                        +" bed_no,bed_name,FK_Fresh_Batch"
                        +" from vw_class_beds a, "
                        +" (select FK_Fresh_Batch,FK_Class_NO from vw_fresh_student_base group by FK_Fresh_Batch,FK_Class_NO) as b "
                        +" where a.PK_Class_NO=b.FK_Class_NO and b.FK_Fresh_Batch=@cs1 and a.college_no=@cs2 ) as tb"
                        +" group by class_campus_name,collegename,spe_name,classname,pk_class_no,gender,dormname";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_hasbed", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校或某学院学生未预分床位人数数据
    public System.Data.DataTable get_batch_nohasbedclass(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select tm1.Campus_Name,tm1.Name,tm1.SPE_Name,tm1.ClassName,tm2.gender,tm2.studentcount,tm2.hasbedcount,tm2.nohasbedcount,"
                        +" tm2.requirebedcount from vw_class tm1,("
                        +" select tt3.*,case when tt3.nohasbedcount<0 then 0 else tt3.nohasbedcount end as requirebedcount"
                        +" from ("
                        +" select  tt1.*,tt2.hasbedcount,"
                        +" tt1.studentcount-(case when tt2.hasbedcount is null then 0 else tt2.hasbedcount end) as nohasbedcount"
                        +" from ("
                        +" select fk_class_no as pk_class_no,item_name as gender,count(*) as studentcount"
                        +" from ("
                        +" select b.Item_Name,a.* from vw_fresh_student_base a,Base_Code_Item b"
                        +" where a.Gender_Code=b.Item_NO and b.FK_Code='002' and a.FK_Fresh_Batch=@cs1) as tb1"
                        +" group by fk_class_no,item_name) as tt1"
                        +" LEFT JOIN ("
                        +" select pk_class_no,gender,count(*) as hasbedcount"
                        +" from ("
                        +" select a.*,b.FK_Fresh_Batch "
                        +" from vw_class_beds a,"
                        +" (select FK_Fresh_Batch,FK_Class_NO from vw_fresh_student_base"
                        +" group by FK_Fresh_Batch,FK_Class_NO) as b"
                        +" where a.PK_Class_NO=b.FK_Class_NO and b.FK_Fresh_Batch=@cs1) as tb2"
                        +" group by pk_class_no,gender) as tt2"
                        +" on ltrim(rtrim(tt1.pk_class_no))+'_'+ltrim(rtrim(tt1.gender))=ltrim(rtrim(tt2.pk_class_no))+'_'+ltrim(rtrim(tt2.gender))"
                        +" ) as tt3) as tm2"
                        +" where tm1.PK_Class_NO=tm2.pk_class_no and tm2.requirebedcount>0"
                        +" order by tm1.Campus_Name,tm1.Name,tm1.SPE_Name,tm1.ClassName,tm2.gender";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select tm1.Campus_Name,tm1.Name,tm1.SPE_Name,tm1.ClassName,tm2.gender,tm2.studentcount,tm2.hasbedcount,tm2.nohasbedcount,"
                        + " tm2.requirebedcount from vw_class tm1,("
                        + " select tt3.*,case when tt3.nohasbedcount<0 then 0 else tt3.nohasbedcount end as requirebedcount"
                        + " from ("
                        + " select  tt1.*,tt2.hasbedcount,"
                        + " tt1.studentcount-(case when tt2.hasbedcount is null then 0 else tt2.hasbedcount end) as nohasbedcount"
                        + " from ("
                        + " select fk_class_no as pk_class_no,item_name as gender,count(*) as studentcount"
                        + " from ("
                        + " select b.Item_Name,a.* from vw_fresh_student_base a,Base_Code_Item b"
                        + " where a.Gender_Code=b.Item_NO and b.FK_Code='002' and a.FK_Fresh_Batch=@cs1 and a.college_no=@cs2) as tb1"
                        + " group by fk_class_no,item_name) as tt1"
                        + " LEFT JOIN ("
                        + " select pk_class_no,gender,count(*) as hasbedcount"
                        + " from ("
                        + " select a.*,b.FK_Fresh_Batch "
                        + " from vw_class_beds a,"
                        + " (select FK_Fresh_Batch,FK_Class_NO from vw_fresh_student_base"
                        + " group by FK_Fresh_Batch,FK_Class_NO) as b"
                        + " where a.PK_Class_NO=b.FK_Class_NO and b.FK_Fresh_Batch=@cs1 and a.college_no=@cs2 ) as tb2"
                        + " group by pk_class_no,gender) as tt2"
                        + " on ltrim(rtrim(tt1.pk_class_no))+'_'+ltrim(rtrim(tt1.gender))=ltrim(rtrim(tt2.pk_class_no))+'_'+ltrim(rtrim(tt2.gender))"
                        + " ) as tt3) as tm2"
                        + " where tm1.PK_Class_NO=tm2.pk_class_no and tm2.requirebedcount>0"
                        + " order by tm1.Campus_Name,tm1.Name,tm1.SPE_Name,tm1.ClassName,tm2.gender";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_nohasbedclass", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    //某迎新批次全校或某学院学生已预分床位，但年或校区错误的数据
    public System.Data.DataTable get_batch_hasbed_buterror(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select dorm_year,dorm_campus_name,dorm_name,dorm_no,gender,collegename,spe_name,class_campus_name,class_year,classname,"
                        + "pk_class_no,count(*) as bedcount "
                        + " from ("
                        + "select a.year as dorm_year,a.campus_name as dorm_campus_name,a.dormname as dorm_name,a.dorm_no,"
                        + " a.gender,collegename,spe_name,class_campus_name,"
                        + " (select [year] from Fresh_Batch t where t.PK_Batch_NO=b.FK_Fresh_Batch) as class_year,classname,pk_class_no"
                        + " from vw_class_beds a,"
                        + " (select FK_Fresh_Batch,FK_Class_NO from vw_fresh_student_base group by FK_Fresh_Batch,FK_Class_NO) as b "
                        + " where a.PK_Class_NO=b.FK_Class_NO and b.FK_Fresh_Batch=@cs1"
                        + " and (ltrim(rtrim(campus_name))<>ltrim(rtrim(class_campus_name))  "
                        + " or [year]<>(select [year] from Fresh_Batch t where t.PK_Batch_NO=b.FK_Fresh_Batch)"
                        + ")) as tb"
                        + " group by dorm_year,dorm_campus_name,dorm_name,dorm_no,gender,collegename,spe_name,class_campus_name,"
                        + " class_year,classname,pk_class_no"
                        + " order by classname";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr ="select dorm_year,dorm_campus_name,dorm_name,dorm_no,gender,collegename,spe_name,class_campus_name,class_year,classname,"
                        +"pk_class_no,count(*) as bedcount "
                        +" from (" 
                        + "select a.year as dorm_year,a.campus_name as dorm_campus_name,a.dormname as dorm_name,a.dorm_no,"
                        + " a.gender,collegename,spe_name,class_campus_name,"
                        + " (select [year] from Fresh_Batch t where t.PK_Batch_NO=b.FK_Fresh_Batch) as class_year,classname,pk_class_no"
                        + " from vw_class_beds a,"
                        + " (select FK_Fresh_Batch,FK_Class_NO from vw_fresh_student_base group by FK_Fresh_Batch,FK_Class_NO) as b "
                        + " where a.PK_Class_NO=b.FK_Class_NO and b.FK_Fresh_Batch=@cs1 and a.college_no=@cs2"
                        + " and (ltrim(rtrim(campus_name))<>ltrim(rtrim(class_campus_name))  "
                        + " or [year]<>(select [year] from Fresh_Batch t where t.PK_Batch_NO=b.FK_Fresh_Batch)"
                        + ")) as tb"
                        +" group by dorm_year,dorm_campus_name,dorm_name,dorm_no,gender,collegename,spe_name,class_campus_name,"
                        +" class_year,classname,pk_class_no"
                        +" order by classname";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_hasbed_buterror", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校或某学院已设置班主任数据
    public System.Data.DataTable get_batch_hascounseller(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select a.Campus_Name,a.ClassName,a.PK_Class_NO,d.name,c.phone,c.qq,a.name as collagename,a.SPE_Name"
                        +" from vw_class a,"
                        +" (select distinct(fk_class_no)"
                        +" from vw_fresh_student_base"
                        +" where FK_Fresh_Batch=@cs1) as b,"
                        +" Fresh_Counseller c,Base_Staff d"
                        +" where a.PK_Class_NO=b.fk_class_no and a.PK_Class_NO=c.FK_Class_NO and c.FK_Staff_NO=d.pk_staff_no"
                        +" order by collagename,spe_name,ClassName,name";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select a.Campus_Name,a.ClassName,a.PK_Class_NO,d.name,c.phone,c.qq,a.name as collagename,a.SPE_Name"
                        + " from vw_class a,"
                        + " (select distinct(fk_class_no)"
                        + " from vw_fresh_student_base"
                        + " where FK_Fresh_Batch=@cs1 and College_NO=@cs2) as b,"
                        + " Fresh_Counseller c,Base_Staff d"
                        + " where a.PK_Class_NO=b.fk_class_no and a.PK_Class_NO=c.FK_Class_NO and c.FK_Staff_NO=d.pk_staff_no"
                        + " order by collagename,spe_name,ClassName,name";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_hascounseller", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校或某学院未设置班主任数据
    public System.Data.DataTable get_batch_nohascounseller(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select a.Campus_Name,a.ClassName,a.PK_Class_NO,a.name as collagename,a.SPE_Name"
                        + " from vw_class a,"
                        + " (select distinct(fk_class_no)"
                        + " from vw_fresh_student_base"
                        + " where FK_Fresh_Batch=@cs1"
                        + " and FK_Class_NO not in (select FK_Class_NO from Fresh_Counseller)) as b"
                        + " where a.PK_Class_NO=b.fk_class_no "
                        + " order by collagename,SPE_Name,classname,campus_name";

                //sqlstr = "select a.name as collagename,a.SPE_Name,a.ClassName,a.PK_Class_NO,a.Campus_Name"
                //        + " from vw_class a left join "
                //        + " (select distinct(fk_class_no)"
                //        + " from vw_fresh_student_base"
                //        + " where FK_Fresh_Batch=@cs1"
                //        + " and FK_Class_NO not in (select FK_Class_NO from Fresh_Counseller)) as b"
                //        + " on a.PK_Class_NO=b.fk_class_no "
                //        + " order by collagename,SPE_Name,classname,campus_name";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select a.Campus_Name,a.ClassName,a.PK_Class_NO,a.name as collagename,a.SPE_Name"
                        + " from vw_class a,"
                        + " (select distinct(fk_class_no)"
                        + " from vw_fresh_student_base"
                        + " where FK_Fresh_Batch=@cs1 and College_NO=@cs2 "
                        + " and FK_Class_NO not in (select FK_Class_NO from Fresh_Counseller)) as b"
                        + " where a.PK_Class_NO=b.fk_class_no "
                        + " order by collagename,SPE_Name,classname,campus_name";
                //sqlstr = "select a.name as collagename,a.SPE_Name,a.ClassName,a.PK_Class_NO,a.Campus_Name"
                //        + " from vw_class a left join "
                //        + " (select distinct(fk_class_no)"
                //        + " from vw_fresh_student_base"
                //        + " where FK_Fresh_Batch=@cs1 and College_NO=@cs2 "
                //        + " and FK_Class_NO not in (select FK_Class_NO from Fresh_Counseller)) as b"
                //        + " on a.PK_Class_NO=b.fk_class_no "
                //        + " order by collagename,SPE_Name,classname,campus_name";
                result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_nohascounseller", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校已设置现场迎新事务的事务数据
    public System.Data.DataTable get_batch_hascollageaffair(string PK_BATCH_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            sqlstr = "select kk2.Affair_Name,kk2.Affair_Type,kk1.pk_affair_no,kk1.college_no as college_no,"
                    + " kk3.Name as collegename"
                    + " FROM"
                    + " (select tb1.pk_affair_no,tb1.college_no,tb2.has_college_no "
                    + " from (select * "
                    + " from ("
                    + " select distinct(pk_affair_no) from vw_affair"
                    + " where (Affair_Type='school' or Affair_Type='both') and PK_Batch_NO=@cs1) as s1"
                    + " ,(select distinct(College_NO) from vw_fresh_student_base"
                    + " where fk_fresh_batch=@cs1) as s2"
                    + " ) as tb1"
                    + " LEFT JOIN"
                    + " (select t1.pk_affair_no,t2.FK_College_NO as has_college_no"
                    + " from ("
                    + " select distinct(pk_affair_no) from vw_affair"
                    + " where (Affair_Type='school' or Affair_Type='both') and PK_Batch_NO=@cs1) as t1 "
                    + " LEFT JOIN"
                    + " (select PK_Affair_NO,FK_College_NO from vw_operator_scope"
                    + " where FK_Batch_NO=@cs1"
                    + " group by PK_Affair_NO,FK_College_NO) as t2 "
                    + " on t1.pk_affair_no=t2.PK_Affair_NO) as tb2"
                    + " on ltrim(rtrim(tb1.pk_affair_no))+ltrim(rtrim(tb1.college_no))=ltrim(rtrim(tb2.pk_affair_no))+ltrim(rtrim(tb2.has_college_no))"
                    + " ) as kk1,vw_affair as kk2,Base_College as kk3"
                    + " where kk1.pk_affair_no=kk2.PK_Affair_NO and kk1.college_no=kk3.PK_College and not kk1.has_college_no is null"
                    + " order by kk1.pk_affair_no,kk1.college_no";
            result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_hascollageaffair", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某迎新批次全校未设置现场迎新事务的事务数据
    public System.Data.DataTable get_batch_nohascollageaffair(string PK_BATCH_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            sqlstr = "select kk2.Affair_Name,kk2.Affair_Type,kk1.pk_affair_no,kk1.college_no as require_college_no,"
                    + " kk3.Name as require_collegename"
                    + " FROM"
                    + " (select tb1.pk_affair_no,tb1.college_no,tb2.has_college_no "
                    + " from (select * "
                    + " from ("
                    + " select distinct(pk_affair_no) from vw_affair"
                    + " where (Affair_Type='school' or Affair_Type='both') and PK_Batch_NO=@cs1) as s1"
                    + " ,(select distinct(College_NO) from vw_fresh_student_base"
                    + " where fk_fresh_batch=@cs1) as s2"
                    + " ) as tb1"
                    + " LEFT JOIN"
                    + " (select t1.pk_affair_no,t2.FK_College_NO as has_college_no"
                    + " from ("
                    + " select distinct(pk_affair_no) from vw_affair"
                    + " where (Affair_Type='school' or Affair_Type='both') and PK_Batch_NO=@cs1) as t1 "
                    + " LEFT JOIN"
                    + " (select PK_Affair_NO,FK_College_NO from vw_operator_scope"
                    + " where FK_Batch_NO=@cs1"
                    + " group by PK_Affair_NO,FK_College_NO) as t2 "
                    + " on t1.pk_affair_no=t2.PK_Affair_NO) as tb2"
                    + " on ltrim(rtrim(tb1.pk_affair_no))+ltrim(rtrim(tb1.college_no))=ltrim(rtrim(tb2.pk_affair_no))+ltrim(rtrim(tb2.has_college_no))"
                    + " ) as kk1,vw_affair as kk2,Base_College as kk3"
                    + " where kk1.pk_affair_no=kk2.PK_Affair_NO and kk1.college_no=kk3.PK_College and kk1.has_college_no is null"
                    + " order by kk1.pk_affair_no,kk1.college_no";
            result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_nohascollageaffair", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    //某迎新批次全校或某学院的专业财务交费项目数据
    public System.Data.DataTable get_batch_college_financial(string PK_BATCH_NO, string College_NO)
    {
        System.Data.DataTable result = null;
        System.Data.DataTable data = null;
        try
        {
            string sqlstr = null;
            if (College_NO == null || College_NO.Trim().Length == 0)
            {
                sqlstr = "select  Collage,College_NO,SPE_Name,SPE_Code from vw_fresh_student_base"
                        + " where FK_Fresh_Batch=@cs1"
                        +" group by Collage,College_NO,SPE_Name,SPE_Code";
                data = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
            }
            else
            {
                sqlstr = "select  Collage,College_NO,SPE_Name,SPE_Code from vw_fresh_student_base"
                        + " where FK_Fresh_Batch=@cs1 and College_NO=@cs2"
                        + " group by Collage,College_NO,SPE_Name,SPE_Code";
                data = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", College_NO.Trim()));
            }
            sqlstr = "select '' as Collage,'' as College_NO,'' as SPE_Name,'' as SPE_Code,'' as Fee_Code_Name,'' as Fee_Name, "
                     + " '' as Fee_Amount,'' as Type_Name,'' as Is_Must,'' as Is_Online_Order,'' as Fee_Code" ;
            result = Sqlhelper.Serach(sqlstr);
            if (data != null && data.Rows.Count > 0)
            {
                financial financial_logic = new financial();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    System.Data.DataRow newrow = null;
                    string spe_code=data.Rows[i]["SPE_Code"].ToString().Trim();//招办专业码
                    //System.Data.DataTable dt1=financial_logic.get_base_spe_zydm(spe_code);//教务处专业码
                    //if (dt1 == null || dt1.Rows.Count == 0)
                    //{
                    //    throw new Exception("无效的教务专业码");
                    //}
                    //spe_code = dt1.Rows[0]["ZYDM"].ToString().Trim();//教务处专业码

                    List<Financial.Fee_Item> fin_data = financial_logic.get_feeitem(PK_BATCH_NO, spe_code);
                    if (fin_data != null)
                    {
                        for (int j = 0; j < fin_data.Count; j++)
                        {
                            newrow = result.NewRow();
                            newrow["Collage"] = data.Rows[i]["Collage"].ToString().Trim();
                            newrow["College_NO"] = data.Rows[i]["College_NO"].ToString().Trim();
                            newrow["SPE_Name"] = data.Rows[i]["SPE_Name"].ToString().Trim();
                            newrow["SPE_Code"] = data.Rows[i]["SPE_Code"].ToString().Trim();
                            newrow["Fee_Code_Name"] = fin_data[j].Fee_Code_Name.ToString().Trim();
                            newrow["Fee_Name"] = fin_data[j].Fee_Name.ToString().Trim();
                            newrow["Fee_Amount"] = fin_data[j].Fee_Amount.ToString().Trim();
                            newrow["Type_Name"] = fin_data[j].Type_Name.ToString().Trim();
                            newrow["Is_Must"] = fin_data[j].Is_Must.ToString().Trim().Equals("1")?"是":"否";
                            newrow["Is_Online_Order"] = fin_data[j].Is_Online_Order.ToString().Trim().Equals("1") ? "是" : "否";
                            newrow["Fee_Code"] = fin_data[j].Fee_Code.ToString().Trim();
                            result.Rows.Add(newrow);
                        }
                    }
                    newrow = result.NewRow();
                    newrow["Collage"] = "";
                    newrow["College_NO"] = "";
                    newrow["SPE_Name"] = "";
                    newrow["SPE_Code"] = "";
                    newrow["Fee_Code_Name"] = "";
                    newrow["Fee_Name"] = "";
                    newrow["Fee_Amount"] = "";
                    newrow["Type_Name"] = "";
                    newrow["Is_Must"] = "";
                    newrow["Is_Online_Order"] = "";
                    newrow["Fee_Code"] = "";
                    result.Rows.Add(newrow);
                }
                //result.DefaultView.Sort = "Collage,SPE_Name,Fee_Code_Name ASC";
                //result = result.DefaultView.ToTable();
                result.AcceptChanges();
            }

        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_college_financial", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某批次、某班主任的班级数据(班级管理模块)
    public System.Data.DataTable get_batch_ClassByCounseller(string PK_BATCH_NO,string PK_STAFF_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            sqlstr = "select b.* from Fresh_Counseller a,Fresh_Class b"
                    +" where a.fk_staff_no=@cs2 and a.FK_Class_NO=b.PK_Class_NO and a.FK_Class_NO in ("
                    +" select DISTINCT(fk_class_no) from vw_fresh_student_base"
                    +" where FK_Fresh_Batch=@cs1)";
            result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()), new SqlParameter("cs2", PK_STAFF_NO.Trim()));
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_ClassByCounseller", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某批次事务列表(班级管理模块)
    public System.Data.DataTable get_batch_affairlist(string PK_BATCH_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            sqlstr = "select affair_name,PK_Affair_NO from Fresh_Affair"
                    +" where FK_Batch_NO=@cs1 and affair_char='interactive' and StatusDisplay='yes'"
                    + " order by affair_order,affair_name";
            result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_BATCH_NO.Trim()));
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_affairlist", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某班级学生列表(班级管理模块)
    public System.Data.DataTable get_classstudent(string PK_CLASS_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            //sqlstr = "select [year],collage,spe_name,a.name,c.Item_Name as gender,a.pk_sno,test_no,id_no,Status_Code,"
            //        +" case when d.Tuition is null then '' else d.Tuition end as TuitionType, "
            //        +" case when a.Phone is null and a.Phone_dr is null then '' else "
            //        +" ( case when a.Phone is not null and a.Phone_dr is null then a.Phone else "
            //        +" ( case when a.Phone is null and a.Phone_dr is not null then a.Phone_dr else a.Phone+','+a.Phone_dr  end )"
            //        +" end )"
            //        +" end as phone"
            //        +" from vw_fresh_student_base a LEFT JOIN Fresh_TuitionFee d on a.PK_SNO=d.PK_SNO"
            //        +" ,Fresh_Class b,Base_Code_Item c"
            //        +" where a.FK_Class_NO=b.PK_Class_NO and a.Gender_Code=c.Item_NO and c.FK_Code='002'"
            //        + " and a.FK_Class_NO=@cs1 order by name ";
            sqlstr = "select [year],collage,spe_name,a.name,c.Item_Name as gender,a.pk_sno,test_no,id_no,Status_Code,"
                    + " case when d.Tuition is null then '' else d.Tuition end as TuitionType, "
                    + " case when a.Phone is null and a.Phone_dr is null then '' else "
                    + " ( case when a.Phone is not null and a.Phone_dr is null then a.Phone else "
                    + " ( case when a.Phone is null and a.Phone_dr is not null then a.Phone_dr else a.Phone+','+a.Phone_dr  end )"
                    + " end )"
                    + " end as phone,"
                    + " case when e.Confirm_state is null then '未注册' else (case when e.Confirm_state='0' then '注册【有误】' else '注册' end ) END as register"
                    + " from vw_fresh_student_base a LEFT JOIN Fresh_TuitionFee d on a.PK_SNO=d.PK_SNO"
                    + " left join Fresh_Confirm e on a.PK_SNO=e.FK_SNO"
                    + " ,Fresh_Class b,Base_Code_Item c"
                    + " where a.FK_Class_NO=b.PK_Class_NO and a.Gender_Code=c.Item_NO and c.FK_Code='002'"
                    + " and a.FK_Class_NO=@cs1 order by name ";
            result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_CLASS_NO.Trim()));
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_classstudent", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //某班级学生的某事务状态列表(班级管理模块)
    public System.Data.DataTable get_classstudentandaffairstatus(string PK_CLASS_NO,string PK_AFFAIR_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            sqlstr = "select [year],collage,spe_name,a.name,c.Item_Name as gender,a.pk_sno,test_no,id_no,Status_Code,"
                    + " case when d.Tuition is null then '' else d.Tuition end as TuitionType,'' as affairstatus "
                    + " from vw_fresh_student_base a LEFT JOIN Fresh_TuitionFee d on a.PK_SNO=d.PK_SNO"
                    + " ,Fresh_Class b,Base_Code_Item c"
                    + " where a.FK_Class_NO=b.PK_Class_NO and a.Gender_Code=c.Item_NO and c.FK_Code='002'"
                    + " and a.FK_Class_NO=@cs1 order by name ";
            result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_CLASS_NO.Trim()));
            if (result != null)
            {
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    string pk_sno = result.Rows[i]["pk_sno"].ToString().Trim();
                    //List<fresh_affair_log> stu_loglist=this.get_studentaffairlog_list(pk_sno);
                    List<fresh_affair_log> stu_loglist = this.get_student_affair_affairlog_list(pk_sno, PK_AFFAIR_NO);
                    if (stu_loglist != null && stu_loglist.Count > 0)
                    {
                        result.Rows[i]["affairstatus"] = stu_loglist[0].Log_Status.Trim();
                    }
                    //List<fresh_affair_log> school_loglist=this.get_schoolaffairlog_list(pk_sno);
                    List<fresh_affair_log> school_loglist = this.get_school_affair_affairlog_list(pk_sno, PK_AFFAIR_NO);
                    if (school_loglist != null && school_loglist.Count > 0)
                    {
                        result.Rows[i]["affairstatus"] = school_loglist[0].Log_Status.Trim();
                    }

                    //for (int j = 0; stu_loglist != null && j < stu_loglist.Count; j++)
                    //{
                    //    if (stu_loglist[j].FK_Affair_NO.Trim().Equals(PK_AFFAIR_NO.Trim()))
                    //    {
                    //        result.Rows[i]["affairstatus"] = stu_loglist[j].Log_Status.Trim();
                    //    }
                    //}
                    //for (int j = 0; school_loglist != null && j < school_loglist.Count; j++)
                    //{
                    //    if (school_loglist[j].FK_Affair_NO.Trim().Equals(PK_AFFAIR_NO.Trim()))
                    //    {
                    //        result.Rows[i]["affairstatus"] = school_loglist[j].Log_Status.Trim();
                    //    }
                    //}

                }
                result.AcceptChanges();
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_classstudentandaffairstatus", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //根据班主任编号获取他班级对应的当前有效迎新批次
    public System.Data.DataTable get_batch_counseller(string PK_STAFF_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            sqlstr = "select distinct d.PK_Batch_NO,d.Batch_Name"
                    +" from Fresh_Counseller a,Fresh_Class b,Fresh_SPE c,Fresh_Batch d"
                    +" where a.FK_Class_NO=b.PK_Class_NO and b.FK_SPE_NO=c.PK_SPE and c.[Year]=d.[Year] and" 
                    +" d.Welcome_Begin<=getdate() and d.Welcome_End>=getdate() and d.Enabled='run' and a.fk_staff_no=@cs1"
                    +" order by d.Batch_Name DESC";
            result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_STAFF_NO.Trim()));
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_batch_counseller", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //获取某批次某班级所有学生事务状态
    public System.Data.DataTable get_classstudentaffairlog(string PK_Batch_NO,string PK_CLASS_NO)
    {
        System.Data.DataTable result = null;
        try
        {
            System.Data.DataTable dt_affair = get_batch_affairlist(PK_Batch_NO);//某批次事务列表
            System.Data.DataTable dt_student = get_classstudent(PK_CLASS_NO);//某班级学生列表
            if (dt_student != null)
            {
                dt_student.Columns.Remove("year");
                dt_student.Columns.Remove("collage");
                dt_student.Columns.Remove("spe_name");
                dt_student.Columns.Remove("Status_Code");
                dt_student.Columns.Remove("TuitionType");
                dt_student.Columns.Remove("test_no");

                dt_student.AcceptChanges();
                if (dt_affair != null && dt_affair.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_affair.Rows.Count; i++)
                    {
                        string PK_Affair_NO = dt_affair.Rows[i]["PK_Affair_NO"].ToString().Trim();
                        string affair_name = dt_affair.Rows[i]["affair_name"].ToString().Trim();
                        System.Data.DataTable dt_log = get_classstudentandaffairstatus(PK_CLASS_NO, PK_Affair_NO);//某班级学生的某事务状态列表
                        if (dt_log != null && dt_log.Rows.Count > 0)
                        {
                            dt_student.Columns.Add(affair_name, typeof(string));
                            for (int j = 0; j < dt_student.Rows.Count; j++)
                            {
                                string pk_sno = dt_student.Rows[j]["pk_sno"].ToString().Trim();
                                for (int k = 0; k < dt_log.Rows.Count; k++)
                                {
                                    if (dt_log.Rows[k]["pk_sno"].ToString().Trim().Equals(pk_sno.Trim()))
                                    {
                                        dt_student.Rows[j][affair_name] = dt_log.Rows[k]["affairstatus"].ToString().Trim();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                dt_student.Columns["name"].ColumnName = "姓名";
                dt_student.Columns["gender"].ColumnName = "性别";
                dt_student.Columns["pk_sno"].ColumnName = "学号";
                //dt_student.Columns["test_no"].ColumnName = "高考报名号";
                dt_student.Columns["id_no"].ColumnName = "身份证号";
                dt_student.Columns["phone"].ColumnName = "联系电话";
                dt_student.Columns["register"].ColumnName = "注册状况";
                dt_student.AcceptChanges();
                result = dt_student;
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_classstudentaffairlog", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    //某学生信息(班级管理模块)
    public System.Data.DataTable get_student_detail(string PK_SNO)
    {
        System.Data.DataTable result = null;
        try
        {
            string sqlstr = null;
            sqlstr = "select a.[year],collage,spe_name,g.Item_Name as EDU_Level,b.Name as class_name,a.name,c.Item_Name as gender,"
                     + "a.Photo,a.pk_sno,test_no,id_no,Status_Code,QQ,Height,Weight,"
			         +" Nation_Code,e.item_name as Nation,census,Politics_Code,f.item_name as Politics,Home_add,"
                     +" case when d.Tuition is null then '' else d.Tuition end as TuitionType, "
                     +" case when a.Phone is null and a.Phone_dr is null then '' else "
                     +" ( case when a.Phone is not null and a.Phone_dr is null then a.Phone else "
                     +" ( case when a.Phone is null and a.Phone_dr is not null then a.Phone_dr else a.Phone+','+a.Phone_dr  end )"
                     +"end )"
                     +" end as phone,"
                     + " j.Campus_Name+','+j.Room_NO as dorm"
                     +" from vw_fresh_student_base a LEFT JOIN Fresh_TuitionFee d on a.PK_SNO=d.PK_SNO "
				     +" LEFT JOIN (select * from Base_Code_Item where fk_code='003') as e on a.Nation_Code=e.Item_NO"
					 +" LEFT JOIN (select * from Base_Code_Item where fk_code='004') as f on a.Politics_Code=f.Item_NO"
                     + " LEFT JOIN Fresh_Class as b on a.FK_Class_NO=b.PK_Class_NO"
                     +" LEFT JOIN ( select * from Fresh_Bed_Log h,vw_beds i where h.FK_Bed_NO=i.PK_Bed_NO) as j on a.PK_SNO=j.FK_SNO"
                     + ",Base_Code_Item c,Base_Code_Item g"
                     + " where a.Gender_Code=c.Item_NO and c.FK_Code='002' and a.EDU_Level_Code=g.Item_NO and g.FK_Code='001'"
					 +" AND a.PK_SNO=@cs1";
            result = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()));
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_student", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    //修改学生口令
    public string modifystupwd(string PK_SNO,string Old_PWD,string New_PWD)
    {
        string result = "修改密码失败";
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0 || Old_PWD == null || Old_PWD.Trim().Length == 0 || New_PWD == null || New_PWD.Trim().Length == 0)
            {
                return result;
            }

            string sqlstr = "select * from Base_STU where PK_SNO=@cs1 and Password=@cs2";
            System.Data.DataTable dt = Sqlhelper.Serach(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", Old_PWD));
            if (dt != null && dt.Rows.Count == 1)
            {
                sqlstr = "update Base_STU set Password=@cs3 where PK_SNO=@cs1 and Password=@cs2";
                int jg = Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", Old_PWD), new SqlParameter("cs3", New_PWD));
                if (jg == 1)
                {
                    result ="成功";
                }
            }
            else
            {
                result = "输入的是错误的旧密码";
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "modifystupwd", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

}
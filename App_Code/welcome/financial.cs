using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;


//学生财务收费款项
public class student_fee
{
    public string PK_SNO;//学号
    public string Fee_Code;//财务系统费用编号
    public string Fee_Code_Name;//财务系统费用名称
    public float Fee_Amount;//应收金额
    public float Fee_Payed;//已缴金额
}

public class fresh_fee
{
    public string SYSNAME;//生成订单的应用名次，例如“必交费”，“床上用品”等
    public string PK_SNO;//学号
    public string FEE_ORDERID;//订单编号
    public string FEE_ORDERID_URL;//订单URL地址
    public string XH;//订单中使用的学号
    public string UPDATER;//更新人
    public DateTime UPDATETIME;//更新时间
}

//收费系统的用户结构
public class user_fee
{
    public string xh;//	学号
    public string xm;//	姓名
    public string xbm;//	性别
    public string csrq;//	出生日期
    public string jtdz;//	家庭地址称
    public string gjdq;//	国籍地区
    public string sfz;//	身份证号
    public string rxnd;//	入学年度
    public string lxnd;//	离校年度
    public string xz;//	学制
    public string yxdm;//	院系代码
    public string yxmc;//	院系名称
    public string zydm;//	专业代码
    public string zymc;//	专业名称
    public string bjdm;//	班级代码
    public string bjmc;//	班级名称
    public string xsxzdm;//	学生性质代码
    public string xsxzmc;//	学生性质名称
    public string xslydm;//	学生来源代码
    public string xslymc;//	学生来源名称
    public string xsztdm;//	学生状态代码
    public string xsztmc;//	学生状态名称
    public string xqdm;//	校区代码
    public string xqmc;//	校区名称
    public string yhzh;//	银行帐号
    public string phone;//	联系电话
    public string bz;//	更新方式
    public string gxms;//	更新描述
}


/// <summary>
///financial 财务收费服务类
/// </summary>
public class financial
{
	public financial()
	{
	}

    /// <summary> 
    /// 32位MD5加密 
    /// </summary> 
    /// <param name="password"></param> /// <returns></returns> 
   public string MD5Encrypt32(string password) 
   {   string cl = password;  
       string pwd = "";   
       MD5 md5 = MD5.Create(); //实例化一个md5对像   // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　   
       byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));   // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得   
       for (int i = 0; i < s.Length; i++)   
       {     
           // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符     
           //pwd = pwd + s[i].ToString("X");
           pwd = pwd + s[i].ToString("x");   
       }  
       return pwd;
   } 

    //根据学号创建一个收费用户信息
   private user_fee make_user_fee(string pk_sno)
   {
        user_fee result = null;

        batch logic = new batch();
        model.Base_STU stu_data = organizationService.getStu(pk_sno);
        if (stu_data != null)
        {
            result = new user_fee();
            result.xh = stu_data.PK_SNO;//学号
            result.rxnd = stu_data.Year;//学年
            result.sfz = stu_data.ID_NO;//身份证号
            result.xm = stu_data.Name;//姓名            

            List<base_code_item> itemlist = logic.get_base_code_item("002");
            if (itemlist != null)
            {
                for (int i = 0; i < itemlist.Count; i++)
                {
                    if (itemlist[i].Item_NO.Trim().Equals(stu_data.Gender_Code.Trim()))
                    {
                        result.xbm = itemlist[i].Item_Name.Trim();//性别
                        break;
                    }
                }
            }
            List<fresh_affair> afflist = logic.get_freshstudent_affair_list(stu_data.PK_SNO);
            if (afflist != null && afflist.Count > 0)
            {
                fresh_batch freshbatch = logic.get_freshbatch(afflist[0].FK_Batch_NO);
                string prefix = freshbatch.Financial_XH_Prefix;
                if (prefix != null)
                {
                    result.xh = prefix.Trim() + result.xh.Trim();
                }
            }

            if (stu_data.FK_SPE_Code != null)
            {
                model.Fresh_SPE spe_data = organizationService.getSpe(stu_data.FK_SPE_Code);
                if (spe_data != null)
                {
                    result.zydm = spe_data.SPE_Code;//专业编号
                    result.zymc = spe_data.SPE_Name;//专业名称
                    if (spe_data.FK_College_Code != null)
                    {
                        model.Base_College college = organizationService.getColleage(spe_data.FK_College_Code.Trim());
                        if (college != null)
                        {
                            result.yxmc = college.Name;//学院名称
                            result.yxdm = college.College_NO;//学院代码
                        }
                    }

                    itemlist = logic.get_base_code_item("001");
                    if (itemlist != null)
                    {
                        for (int i = 0; i < itemlist.Count; i++)
                        {
                            if (itemlist[i].Item_NO.Trim().Equals(spe_data.EDU_Level_Code.Trim()))
                            {
                                result.xsxzmc = itemlist[i].Item_Name.Trim();//学生性质名称
                                result.xsxzdm = itemlist[i].Item_NO.Trim();//学生性质代码
                                break;
                            }
                        }
                    }
                }
            }

            if (stu_data.FK_Class_NO != null)
            {
                model.Fresh_Class class_data = organizationService.getClass(stu_data.FK_Class_NO);
                if (class_data != null)
                {
                    result.bjdm = class_data.PK_Class_NO;//班级编号
                    result.bjmc = class_data.Name;//班级名称
                }
            }
        }
        result.xz = "3";
        if (result.rxnd != null && result.rxnd.Trim().Length > 0)
        {
            result.lxnd = (int.Parse(result.rxnd) + 3).ToString();
        }
        result.bz = "I";//立即同步
        return result;
    }


    /// <summary>
    ///功能名称： 获取本系统保存的学生订单
    ///功能描述：
    ///根据“学号”该生已生成的收费订单，否则返回null。
    ///编写人：胡元
    ///参数：
    ///PK_SNO：学号   
    ///创建时间：2017-4-15
    ///更新记录：无
    public List<fresh_fee> get_fresh_fee(string PK_SNO)
    {
        List<fresh_fee> result = null;
        try
        {
            string sqlstr = null;
            System.Data.DataTable dt = null;

            sqlstr = "select * from fresh_fee where PK_SNO=@pa order by UPDATETIME";
            dt = Sqlhelper.Serach(sqlstr, new SqlParameter("pa", PK_SNO.Trim()));

            if (dt != null && dt.Rows.Count > 0)
            {
                result = new List<fresh_fee>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    fresh_fee row = new fresh_fee();
                    row.SYSNAME = dt.Rows[i]["SYSNAME"].ToString().Trim();
                    row.PK_SNO = dt.Rows[i]["PK_SNO"].ToString().Trim();
                    row.XH = dt.Rows[i]["FEE_XH"].ToString().Trim();
                    row.FEE_ORDERID = dt.Rows[i]["FEE_ORDERID"].ToString().Trim();
                    row.FEE_ORDERID_URL = dt.Rows[i]["FEE_ORDERID_URL"].ToString().Trim();
                    row.UPDATER = dt.Rows[i]["UPDATER"].ToString().Trim();
                    row.UPDATETIME =DateTime.Parse(dt.Rows[i]["UPDATETIME"].ToString());
                    result.Add(row);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("batch.cs", "get_fresh_fee", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    /// <summary>
    ///功能名称： 获取专业应收费列表
    ///功能描述：
    ///根据“专业代码”和“收费批次号”返回该专业应收款项条目（如学费）和专业不相关条目（通用费用项，如住宿费），否则返回null。
    ///编写人：胡元
    ///参数：
    ///PK_Fee：收费批次号；SPE_Code:专业代码   
    ///创建时间：2017-4-12
    ///更新记录：无
    public List<Financial.Fee_Item> get_feeitem(string PK_Fee, string SPE_Code)
    {
        List<Financial.Fee_Item> result = null;
        try {
            if (PK_Fee != null && PK_Fee.Trim().Length > 0 && SPE_Code != null && SPE_Code.Trim().Length > 0)
            {
                Financial.FinancialWSSoapClient ws = new Financial.FinancialWSSoapClient();
                Financial.Fee_Item[] data = ws.GetFeeItem(PK_Fee, SPE_Code);
                if (data != null && data.Length > 0)
                {
                    result = data.ToList();
                }
            }      
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("financial.cs", "get_feeitem", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称： 获取学生已生成订单缴费列表
    ///功能描述：
    ///根据“学号”和“收费款项编号”返回该学生已生成订单的应收款项条目, 多条订单记录合并金额后返回。
    ///编写人：胡元
    ///参数：
    ///PK_Fee：收费批次号；Spe_Code:专业代码   
    ///创建时间：2017-4-12
    ///更新记录：无
    public List<Financial.Fee_Item> get_ordered_feeitem(string PK_Fee, string PK_SNO)
    {
        List<Financial.Fee_Item> result = null;
        try
        {
            if (PK_Fee != null && PK_Fee.Trim().Length > 0 && PK_SNO != null && PK_SNO.Trim().Length > 0)
            {
                string FEE_XH = PK_SNO;
                user_fee user = make_user_fee(PK_SNO);
                if (user != null)
                {
                    FEE_XH = user.xh;
                }

                Financial.FinancialWSSoapClient ws = new Financial.FinancialWSSoapClient();
                Financial.Fee_Item[] data = ws.GetOrderedFeeItem(PK_Fee, FEE_XH);
                if (data != null && data.Length > 0)
                {
                    result = data.ToList();
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("financial.cs", "get_ordered_feeitem", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称： 根据订单获取学生收费款项列表
    ///功能描述：
    ///根据“订单号”该订单中的款项。
    ///编写人：胡元
    ///参数：
    ///ORDERID：订单号   
    ///创建时间：2017-4-15
    ///更新记录：无
    public List<Financial.Fee_Item> get_feeitem_byorder(string ORDERID)
    {
        List<Financial.Fee_Item> result = null;
        try
        {
            if (ORDERID != null && ORDERID.Trim().Length > 0 )
            {
                Financial.FinancialWSSoapClient ws = new Financial.FinancialWSSoapClient();
                Financial.Fee_Item[] data = ws.GetFeeItemByOrder(ORDERID);
                if (data != null && data.Length > 0)
                {
                    result = data.ToList();
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("financial.cs", "get_feeitem_byorder", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }

    /// <summary>
    ///功能名称： 获取学生某财务款项收费数据
    ///功能描述：
    ///根据“迎新批次编号”、“学号”和“财务系统费用编号”返回该学生在该款项上的收费情况。
    ///如果该生在应收目录中有该款项但未生成订单则返回null
    ///如果该生在应收目录中没有该款项则返回null。
    ///编写人：胡元
    ///参数：
    ///PK_Batch_NO:迎新批次编号；Fee_Code：财务系统费用编号；PK_SNO:学号   
    ///创建时间：2017-4-12
    ///更新记录：无
    public student_fee get_student_fee(string PK_Batch_NO,string Fee_Code, string PK_SNO)
    {
        student_fee result = null;
        try
        {
            if (PK_Batch_NO != null && PK_Batch_NO.Trim().Length > 0 && Fee_Code != null && Fee_Code.Trim().Length > 0 && PK_SNO != null && PK_SNO.Trim().Length > 0)
            {
                batch logic = new batch();
                fresh_batch data = logic.get_freshbatch(PK_Batch_NO);
                if (data != null && data.Financial_PK_Fee!=null && data.Financial_PK_Fee.Trim().Length>0)
                {
                    List<Financial.Fee_Item> data1 = get_ordered_feeitem(data.Financial_PK_Fee, PK_SNO);//获取学生已生成订单缴费列表
                    if (data1 != null && data1.Count > 0)
                    {
                        float sfbz = 0;//收费标准
                        float ysf = 0;//已收费
                        //获取已生成订单的数据
                        for (int i = 0; i < data1.Count; i++)
                        {
                            if (data1[i].Fee_Code.Trim().Equals(Fee_Code.Trim()))
                            {
                                sfbz = data1[i].Fee_Amount + sfbz;
                                ysf = data1[i].Fee_Payed + ysf;
                            }
                        }
                        result = new student_fee();
                        result.Fee_Amount = sfbz;
                        result.Fee_Payed = ysf;
                        result.Fee_Code = Fee_Code;
                        result.Fee_Code_Name = data1[0].Fee_Code_Name;
                    }
                }
                else
                {
                    throw new Exception("无法获取迎新批次或迎新批次中的Financial_PK_Fee！");
                }
            }
            else
            {
                throw new Exception("参数错误！");
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("financial.cs", "get_student_fee", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称： 获取学生必交费用款项
    ///功能描述：
    ///根据“迎新批次编号”、“学号”返回该学生在必交款项。

    ///编写人：胡元
    ///参数：
    ///PK_Batch_NO:迎新批次编号；PK_SNO:学号   
    ///创建时间：2017-4-12
    ///更新记录：无
    public List<Financial.Fee_Item> get_isMust_Fee(string PK_Batch_NO, string PK_SNO)
    {
        List<Financial.Fee_Item> result = null;
        try
        {
            if (PK_Batch_NO != null && PK_Batch_NO.Trim().Length > 0  && PK_SNO != null && PK_SNO.Trim().Length > 0)
            {
                batch logic = new batch();
                fresh_batch data = logic.get_freshbatch(PK_Batch_NO);
                if (data != null && data.Financial_PK_Fee != null && data.Financial_PK_Fee.Trim().Length > 0)
                {
                    model.Base_STU stu = organizationService.getStu(PK_SNO);
                    if (stu == null || stu.FK_SPE_Code == null || stu.FK_SPE_Code.Trim().Length == 0)
                    {
                        throw new Exception("无法获取考生或专业编码！");
                    }
                    model.Fresh_SPE spe = organizationService.getSpe(stu.FK_SPE_Code);
                    if (spe == null || spe.SPE_Code == null || spe.SPE_Code.Trim().Length == 0)
                    {
                        throw new Exception("无法获取专业编码！");
                    }

                    List<Financial.Fee_Item> data1 = get_feeitem(data.Financial_PK_Fee, spe.SPE_Code);
                    if (data1 != null && data1.Count > 0)
                    {
                        result = new List<Financial.Fee_Item>();
                        for (int i = 0; i < data1.Count; i++)
                        {
                            if (data1[i].Is_Must.Trim().Equals("1"))//"1"表示必交费用
                            {
                                result.Add(data1[i]);
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("无法获取迎新批次或迎新批次中的Financial_PK_Fee！");
                }
            }
            else
            {
                throw new Exception("参数错误！");
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("financial.cs", "get_isMust_Fee", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称： 写入交费系统学生信息
    ///功能描述：
    ///根据“学号”，将学生信息写入交费系统。
    ///编写人：胡元
    ///参数：
    ///PK_SNO:学号；FEE_XH_Prefix：订单xh的前缀字符串   
    ///创建时间：2017-4-15
    public bool InitStuInfo(string PK_SNO)
    {
        bool result = false;
        try
        {
            if (PK_SNO != null && PK_SNO.Trim().Length > 0 )
            {
                Financial.FinancialWSSoapClient ws = new Financial.FinancialWSSoapClient();
                string app = "yxxt";
                //string md5 = MD5Encrypt32("yxxtgf$RET54s");

                user_fee user = make_user_fee(PK_SNO);
                //调试信息
                user.bjdm = "lsdz201701";
                user.bjmc = "2017单招临时班";
                user.lxnd = "2017";
                user.rxnd = "2017";
                user.xz = "1";
                user.yxdm = "98";
                user.yxmc = "招办";
                user.zydm = "ls001";
                user.zymc = "单招专业";

                string md5 ="9017dc9c2d3f2532ed6834c207ea6c86";
                string user_json = null;
                if (user != null)
                {
                    user_json = JsonConvert.SerializeObject(user);
                    result = ws.InitStuInfo(app, md5, user_json);//等待确认
                }                 
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("financial.cs", "InitStuInfo", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return result;
    }


    /// <summary>
    ///功能名称： 写入交费系统订单数据
    ///功能描述：
    ///将“学号”、“收费项目代码”、“实收金额”写入交费系统准备网上交费，并返回“订单url”，否则返回null。
    ///编写人：胡元
    ///参数：
    ///PK_SNO:学号；FEE_XH:订单学号；pk_fee_items：订单内容；payUrl：订单url地址   
    ///创建时间：2017-4-15
    public string InitPayOrder(string PK_AFFAIR_NO,string PK_SNO, Financial.ArrayOfString pk_fee_items, string UPDATER)
    {
        string FEE_XH = null;
        string orderid = null;
        string orderid_url = null;
        try
        {
            if (PK_SNO != null && PK_SNO.Trim().Length > 0 && PK_AFFAIR_NO != null && PK_AFFAIR_NO.Trim().Length > 0
                && pk_fee_items != null && pk_fee_items.Count > 0 && UPDATER != null && UPDATER.Trim().Length > 0)
            {
                FEE_XH = PK_SNO;
                user_fee user = make_user_fee(PK_SNO);
                if (user != null)
                {
                    FEE_XH = user.xh;
                }
                
                Financial.FinancialWSSoapClient ws = new Financial.FinancialWSSoapClient();
                string app = "yxxt";
                //string md5 = MD5Encrypt32("yxxtgf$RET54s");
                string md5 = "9017dc9c2d3f2532ed6834c207ea6c86";

                orderid=ws.InitPayOrder(app, md5, FEE_XH, pk_fee_items, out orderid_url);//生成订单并返回其url地址
                if (orderid!=null)
                {
                    string sqlstr = "insert into Fresh_Fee (PK_SNO,FEE_XH,FEE_ORDERID,FEE_ORDERID_URL,SYSNAME,UPDATER,UPDATETIME) values (" +
                                    "@cs1,@cs2,@cs3,@cs4,@cs5,@cs6,getdate())";
                    int jg = Sqlhelper.ExcuteNonQuery(sqlstr, new SqlParameter("cs1", PK_SNO.Trim()), new SqlParameter("cs2", FEE_XH.Trim()),
                        new SqlParameter("cs3", orderid.Trim()), new SqlParameter("cs4", orderid_url.Trim()), new SqlParameter("cs5", "必交费"),
                        new SqlParameter("cs6", UPDATER.Trim()));
                    batch logic = new batch();
                    logic.set_affairlog(PK_SNO, PK_AFFAIR_NO, "已完成", UPDATER);
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("financial.cs", "InitPayOrder", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
            throw ex;
        }
        return orderid_url;
    }


}
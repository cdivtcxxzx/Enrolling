using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;

/// <summary>
/// WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public List<fresh_batch> get_freshbatch_list()
    //{
    //    batch bt = new batch();
    //    return bt.get_freshbatch_list();
    //}

    //[WebMethod]
    //public fresh_batch get_freshbatch(string PK_Batch_NO)
    //{
    //    batch bt = new batch();
    //    return bt.get_freshbatch(PK_Batch_NO);
    //}

    //[WebMethod]
    //public bool get_freshbatch_isrun(string PK_Batch_NO)
    //{
    //    batch bt = new batch();
    //    return bt.get_freshbatch_isrun(PK_Batch_NO);
    //}

    //[WebMethod]
    //public bool get_freshoperator_isauth(string PK_Batch_NO, string PK_Staff_NO)
    //{
    //    batch bt = new batch();
    //    return bt.get_freshoperator_isauth(PK_Batch_NO, PK_Staff_NO);
    //}

    //[WebMethod]
    //public List<fresh_affair> get_freshoperator_auth_affair_list(string PK_Batch_NO, string PK_Staff_NO)
    //{
    //    batch bt = new batch();
    //    return bt.get_freshoperator_auth_affair_list(PK_Batch_NO, PK_Staff_NO);
    //}

    //[WebMethod]
    //public int get_freshoperator_will_affair_students_count(string PK_Batch_NO, string PK_Staff_NO, string PK_Affair_NO)
    //{
    //    batch bt = new batch();
    //    return bt.get_freshoperator_will_affair_students_count(PK_Batch_NO, PK_Staff_NO, PK_Affair_NO);
    //}

    //[WebMethod]
    //public int get_freshoperator_finish_affair_students_count(string PK_Batch_NO, string PK_Staff_NO, string PK_Affair_NO)
    //{
    //    batch bt = new batch();
    //    return bt.get_freshoperator_finish_affair_students_count(PK_Batch_NO, PK_Staff_NO, PK_Affair_NO);
    //}

    //[WebMethod]
    //public fresh_affair get_affair(string PK_Affair_NO)
    //{
    //    batch bt = new batch();
    //    return bt.get_affair(PK_Affair_NO);
    //}

    //[WebMethod]
    //public bool check_student_in_freshbatch(string PK_Batch_NO, string PK_SNO)
    //{
    //    batch bt = new batch();
    //    return bt.check_student_in_freshbatch( PK_Batch_NO, PK_SNO);
    //}

    //[WebMethod]
    //public bool check_operator_object(string PK_Staff_NO, string PK_Affair_NO, string PK_SNO)
    //{
    //    batch bt = new batch();
    //    return bt.check_operator_object(PK_Staff_NO, PK_Affair_NO, PK_SNO);
    //}

    //[WebMethod]
    //public fresh_oper get_oper(string PK_Affair_NO)
    //{
    //    batch bt = new batch();
    //    return bt.get_oper(PK_Affair_NO);
    //}
    
    //[WebMethod]
    //public List<fresh_affair> get_freshstudent_affair_list(string PK_SNO)
    //{
    //    batch bt = new batch();
    //    return bt.get_freshstudent_affair_list(PK_SNO);
    //}

    //[WebMethod]
    //public bool set_freshstudent_register(string PK_SNO)
    //{
    //    batch bt = new batch();
    //    return bt.set_freshstudent_register(PK_SNO);
    //}

    //[WebMethod]
    //public List<fresh_affair_log> get_schoolaffairlog_list(string PK_SNO)
    //{
    //    batch bt = new batch();
    //    return bt.get_schoolaffairlog_list(PK_SNO);
    //}

    //[WebMethod]
    //public List<fresh_affair_log> get_studentaffairlog_list(string PK_SNO)
    //{
    //    batch bt = new batch();
    //    return bt.get_studentaffairlog_list(PK_SNO);
    //}

    //[WebMethod]
    //public bool set_affairlog(string PK_SNO, string PK_Affair_NO, string Log_Status, string Creater)
    //{
    //    batch bt = new batch();
    //    return bt.set_affairlog(PK_SNO, PK_Affair_NO, Log_Status, Creater);
    //}

    //[WebMethod]
    //public bool check_student_affair_condition(string PK_SNO, string PK_Affair_NO)
    //{
    //    batch bt = new batch();
    //    return bt.check_student_affair_condition(PK_SNO, PK_Affair_NO);
    //}

    //public bool analysis_fee_condition(string fee_condition, string PK_Batch_NO, string PK_SNO)
    //{
    //    batch bt = new batch();
    //    return bt.analysis_fee_condition(fee_condition, PK_Batch_NO, PK_SNO);
    //}


    //[WebMethod]
    //public string test(string input)
    //{
    //    //input的格式是：{@a1=已完成 and @a2=未完成}:{@a1,@a2}
    //    string jg = "";
    //    string[] arr = input.Split(':');
    //    if (arr.Length != 2)
    //        return arr.Length.ToString();

    //    arr[0] = arr[0].Substring(1, arr[0].Length - 2);
    //    string express = arr[0];

    //    arr[1] = arr[1].Substring(1, arr[1].Length - 2);
    //    if (arr[1].Trim().Length > 0)
    //    {
    //        string[] parameters = arr[1].Split(',');

    //        for (int i = 0; i < parameters.Length; i++)
    //        {
    //            express = express.Replace(parameters[i], "para" + i.ToString().Trim());
    //        }
    //    }
    //    return express;
    //}


    //[WebMethod]
    //public string test_get_feeitem_byorder(string ORDERID)
    //{
    //    financial svr = new financial();
    //    List<Financial.Fee_Item> data = svr.get_feeitem_byorder(ORDERID);
    //    return JsonConvert.SerializeObject(data);
    //}

    //[WebMethod]
    //public string testmd5(string pwd)
    //{
    //    financial svr = new financial();
    //    string data = svr.MD5Encrypt32(pwd);
    //    return data;
    //}

    //返回事务状态的样式用例，格式：webservice地址?方法名称
    //http://localhost:3893/test/WebService.asmx?test_Log_Status
    [WebMethod]
    public string test_Log_Status(string PK_SNO)
    {
        return "已完成";
    }

    //判断学生必交费是否交清
    [WebMethod]
    public string get_fee_ismust(string PK_SNO)
    {
        string result = "未完成";
        try {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                throw new Exception("参数错误");
            }

            batch batch_logic = new batch();
            List<fresh_affair> data = batch_logic.get_freshstudent_affair_list(PK_SNO);
            if (data == null || data.Count == 0)
            {
                throw new Exception("获取学生迎新事务数据错误");
            }
            string pk_batch_no = data[0].FK_Batch_NO;
            if (pk_batch_no == null || pk_batch_no.Trim().Length == 0)
            {
                throw new Exception("获取学生迎新事务中的迎新批次数据错误");
            }
            financial logic_fee = new financial();
            fee_ismust data1 = logic_fee.get_fee_ismust(pk_batch_no, PK_SNO);
            if (data1 == null || data1.orderid==null || data1.orderid.Trim().Length==0) {
                result = "未完成";
            }
            else
            {
                bool finishpay = true;
                if (data1.single != null && data1.single.Count > 0)
                {
                    for (int i = 0; i < data1.single.Count; i++)
                    {
                        if (data1.single[i][0].Fee_Amount > data1.single[i][0].Fee_Payed)
                        {
                            finishpay = false;
                        }
                    }
                }
                if (data1.multiple != null && data1.multiple.Count > 0)
                {
                    for (int i = 0; i < data1.multiple.Count; i++)
                    {
                        for (int j = 0; j < data1.multiple[i].Count; j++)
                        {
                            if (data1.multiple[i][j].Fee_Amount > data1.multiple[i][j].Fee_Payed)
                            {
                                finishpay = false;
                            }
                        }
                    }
                }
                if (finishpay)
                {
                    result = "已完成";
                }
                else
                {
                    result = "未完成";
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("webService.cs", "get_fee_ismust", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
        }
        return result;
    }

}

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

    [WebMethod]
    public List<fresh_batch> get_freshbatch_list()
    {
        batch bt = new batch();
        return bt.get_freshbatch_list();
    }

    [WebMethod]
    public fresh_batch get_freshbatch(string PK_Batch_NO)
    {
        batch bt = new batch();
        return bt.get_freshbatch(PK_Batch_NO);
    }

    [WebMethod]
    public bool get_freshbatch_isrun(string PK_Batch_NO)
    {
        batch bt = new batch();
        return bt.get_freshbatch_isrun(PK_Batch_NO);
    }

    [WebMethod]
    public bool get_freshoperator_isauth(string PK_Batch_NO, string PK_Staff_NO)
    {
        batch bt = new batch();
        return bt.get_freshoperator_isauth(PK_Batch_NO, PK_Staff_NO);
    }

    [WebMethod]
    public List<fresh_affair> get_freshoperator_auth_affair_list(string PK_Batch_NO, string PK_Staff_NO)
    {
        batch bt = new batch();
        return bt.get_freshoperator_auth_affair_list(PK_Batch_NO, PK_Staff_NO);
    }

    [WebMethod]
    public int get_freshoperator_will_affair_students_count(string PK_Batch_NO, string PK_Staff_NO, string PK_Affair_NO)
    {
        batch bt = new batch();
        return bt.get_freshoperator_will_affair_students_count(PK_Batch_NO, PK_Staff_NO, PK_Affair_NO);
    }

    [WebMethod]
    public int get_freshoperator_finish_affair_students_count(string PK_Batch_NO, string PK_Staff_NO, string PK_Affair_NO)
    {
        batch bt = new batch();
        return bt.get_freshoperator_finish_affair_students_count(PK_Batch_NO, PK_Staff_NO, PK_Affair_NO);
    }

    [WebMethod]
    public fresh_affair get_affair(string PK_Affair_NO)
    {
        batch bt = new batch();
        return bt.get_affair(PK_Affair_NO);
    }

    [WebMethod]
    public bool check_student_in_freshbatch(string PK_Batch_NO, string PK_SNO)
    {
        batch bt = new batch();
        return bt.check_student_in_freshbatch( PK_Batch_NO, PK_SNO);
    }

    [WebMethod]
    public bool check_operator_object(string PK_Staff_NO, string PK_Affair_NO, string PK_SNO)
    {
        batch bt = new batch();
        return bt.check_operator_object(PK_Staff_NO, PK_Affair_NO, PK_SNO);
    }

    [WebMethod]
    public fresh_oper get_oper(string PK_Affair_NO)
    {
        batch bt = new batch();
        return bt.get_oper(PK_Affair_NO);
    }
    
    [WebMethod]
    public List<fresh_affair> get_freshstudent_affair_list(string PK_SNO)
    {
        batch bt = new batch();
        return bt.get_freshstudent_affair_list(PK_SNO);
    }

    [WebMethod]
    public bool set_freshstudent_register(string PK_SNO)
    {
        batch bt = new batch();
        return bt.set_freshstudent_register(PK_SNO);
    }

    [WebMethod]
    public List<fresh_affair_log> get_schoolaffairlog_list(string PK_SNO)
    {
        batch bt = new batch();
        return bt.get_schoolaffairlog_list(PK_SNO);
    }

    [WebMethod]
    public List<fresh_affair_log> get_studentaffairlog_list(string PK_SNO)
    {
        batch bt = new batch();
        return bt.get_studentaffairlog_list(PK_SNO);
    }

    [WebMethod]
    public bool set_affairlog(string PK_SNO, string PK_Affair_NO, string Log_Status, string Creater)
    {
        batch bt = new batch();
        return bt.set_affairlog(PK_SNO, PK_Affair_NO, Log_Status, Creater);
    }

    [WebMethod]
    public bool check_student_affair_condition(string PK_SNO, string PK_Affair_NO)
    {
        batch bt = new batch();
        return bt.check_student_affair_condition(PK_SNO, PK_Affair_NO);
    }

    //返回事务状态的样式用例，格式：webservice地址?方法名称
    //http://localhost:3893/test/WebService.asmx?test_Log_Status
    [WebMethod]
    public string test_Log_Status(string PK_SNO)
    {
        return "已完成";
    }

    [WebMethod]
    public string test(string input)
    {
        //input的格式是：{@a1=已完成 and @a2=未完成}:{@a1,@a2}
        string jg = "";
        string[] arr = input.Split(':');
        if (arr.Length != 2)
            return arr.Length.ToString();

        arr[0] = arr[0].Substring(1, arr[0].Length - 2);
        string express = arr[0];

        arr[1] = arr[1].Substring(1, arr[1].Length - 2);
        if (arr[1].Trim().Length > 0)
        {
            string[] parameters = arr[1].Split(',');

            for (int i = 0; i < parameters.Length; i++)
            {
                express = express.Replace(parameters[i], "para" + i.ToString().Trim());
            }
        }
        return express;
    }


    [WebMethod]
    public string run_remote_webservice()
    {
        Financial.FinancialWSSoapClient s = new Financial.FinancialWSSoapClient();
        Financial.Fee[] data=s.GetFee("yxxt");
        return JsonConvert.SerializeObject(data);
    }
}

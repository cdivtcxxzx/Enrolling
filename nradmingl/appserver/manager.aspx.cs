using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public partial class nradmingl_appserver_manger : System.Web.UI.Page
{
    //返回给js客户端的数据格式
    public class ResultData
    {
        public string code { get; set; }
        public string message { get; set; }
        public Object data { get; set; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ResultData result = new ResultData();
        result.code = "failure";
        result.message = "无效参数";
        result.data = null;
        try {
            string cs = Request.QueryString["cs"];//获取get的参数
            if (cs != null && cs.Trim().Length!=0)
            {
                #region NO:2 迎新批次 获取某迎新批次数据(批次编号)
                if (cs.Trim().Equals("get_freshbatch"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        fresh_batch jg = batch_logic.get_freshbatch(pk_batch_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region NO:3 校验某迎新批次当前是否有效(迎新编号)
                if (cs.Trim().Equals("get_freshbatch_isrun"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        bool jg=batch_logic.get_freshbatch_isrun(pk_batch_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;                        
                    }
                }
                #endregion

                #region NO:5 操作员在某批次是否有效(批次编号,员工编号)
                if (cs.Trim().Equals("get_freshoperator_isauth"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    string pk_staff_no = Request.QueryString["pk_staff_no"];
                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0 && pk_staff_no != null && pk_staff_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        bool jg = batch_logic.get_freshoperator_isauth(pk_batch_no, pk_staff_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region NO:6 获取某操作员数据(员工编号)
                if (cs.Trim().Equals("getOperator"))
                {
                    string pk_staff_no = Request.QueryString["pk_staff_no"];
                    if (pk_staff_no != null && pk_staff_no.Trim().Length != 0)
                    {
                        model.Base_Staff data=organizationService.getOperator(pk_staff_no);                        
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:7 获取操作员事务操作列表(员工编号,批次编号)
                if (cs.Trim().Equals("get_freshoperator_auth_affair_list"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    string pk_staff_no = Request.QueryString["pk_staff_no"];
                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0 && pk_staff_no != null && pk_staff_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        List<fresh_affair> data=batch_logic.get_freshoperator_auth_affair_list(pk_batch_no, pk_staff_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:8 获取预操作总人数(员工编号,批次编号，事务编号)
                if (cs.Trim().Equals("get_freshoperator_will_affair_students_count"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    string pk_staff_no = Request.QueryString["pk_staff_no"];
                    string pk_affair_no = Request.QueryString["pk_affair_no"];

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0 && 
                        pk_staff_no != null && pk_staff_no.Trim().Length != 0 &&
                        pk_affair_no != null && pk_affair_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        int data = batch_logic.get_freshoperator_will_affair_students_count(pk_batch_no, pk_staff_no, pk_affair_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:9 获取已操作总人数(员工编号,批次编号，事务编号)
                if (cs.Trim().Equals("get_freshoperator_finish_affair_students_count"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    string pk_staff_no = Request.QueryString["pk_staff_no"];
                    string pk_affair_no = Request.QueryString["pk_affair_no"];

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0 && 
                        pk_staff_no != null && pk_staff_no.Trim().Length != 0 &&
                        pk_affair_no != null && pk_affair_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        int data = batch_logic.get_freshoperator_finish_affair_students_count(pk_batch_no, pk_staff_no, pk_affair_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:8&9 获取预操作总人数和已操作总人数(员工编号,批次编号，事务编号)
                if (cs.Trim().Equals("get_freshoperator_affair_students_count"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    string pk_staff_no = Request.QueryString["pk_staff_no"];
                    string pk_affair_no = Request.QueryString["pk_affair_no"];

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0 &&
                        pk_staff_no != null && pk_staff_no.Trim().Length != 0 &&
                        pk_affair_no != null && pk_affair_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        int will_count = batch_logic.get_freshoperator_will_affair_students_count(pk_batch_no, pk_staff_no, pk_affair_no);//预操作人数
                        int finish_count = batch_logic.get_freshoperator_finish_affair_students_count(pk_batch_no, pk_staff_no, pk_affair_no);//已操作人数
                        result.code = "success";
                        result.message = "成功";
                        var obj = new { will_count = will_count, finish_count = finish_count };
                        result.data = obj;
                    }
                }
                #endregion

                #region NO:10 迎新事务定义 获取某迎新事务(事务编号)
                if (cs.Trim().Equals("get_affair"))
                {
                    string pk_affair_no = Request.QueryString["pk_affair_no"];

                    if (pk_affair_no != null && pk_affair_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        fresh_affair data = batch_logic.get_affair(pk_affair_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:11 校验学生迎新批次(批次编号,学号)
                if (cs.Trim().Equals("check_student_in_freshbatch"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    string pk_sno = Request.QueryString["pk_sno"];

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0 && pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        bool data = batch_logic.check_student_in_freshbatch(pk_batch_no, pk_sno);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:12 校校验操作员操作对象(事务编号,员工编号,学号)
                if (cs.Trim().Equals("check_operator_object"))
                {
                    string pk_affair_no = Request.QueryString["pk_affair_no"];
                    string pk_staff_no = Request.QueryString["pk_staff_no"];
                    string pk_sno = Request.QueryString["pk_sno"];

                    if (pk_affair_no != null && pk_affair_no.Trim().Length != 0 && pk_staff_no != null && pk_staff_no.Trim().Length != 0 && pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        bool data = batch_logic.check_operator_object(pk_staff_no,pk_affair_no, pk_sno);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:13 校验学生事务操作条件(事务编号,学号)
                if (cs.Trim().Equals("check_student_affair_condition"))
                {
                    string pk_affair_no = Request.QueryString["pk_affair_no"];
                    string pk_sno = Request.QueryString["pk_sno"];

                    if (pk_affair_no != null && pk_affair_no.Trim().Length != 0 && pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        bool data = batch_logic.check_student_affair_condition(pk_sno, pk_affair_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:14 获取某学生数据(学号)
                if (cs.Trim().Equals("getStu"))
                {
                    string pk_sno = Request.QueryString["pk_sno"];

                    if (pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        model.Base_STU data = organizationService.getStu(pk_sno);    
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:15 获取某专业数据(学年,专业编号)
                if (cs.Trim().Equals("getSpe"))
                {
                    string Year = Request.QueryString["Year"];
                    string SPE_Code = Request.QueryString["SPE_Code"];

                    if (Year != null && Year.Trim().Length != 0 && SPE_Code != null && SPE_Code.Trim().Length != 0)
                    {
                        model.Fresh_SPE data = organizationService.getSpe(Year, SPE_Code);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:16 获取某班级数据(班级编号)
                if (cs.Trim().Equals("getClass"))
                {
                    string PK_Class_NO = Request.QueryString["PK_Class_NO"];

                    if (PK_Class_NO != null && PK_Class_NO.Trim().Length != 0)
                    {
                        model.Fresh_Class data = organizationService.getClass(PK_Class_NO);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:17 获取某学生现场迎新事务列表(学号)
                if (cs.Trim().Equals("get_schoolaffairlog_list"))
                {
                    string pk_sno = Request.QueryString["pk_sno"];

                    if (pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        List<fresh_affair_log> data = batch_logic.get_schoolaffairlog_list(pk_sno);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:18 获取某迎新事务操作(事务编号)
                if (cs.Trim().Equals("get_oper"))
                {
                    string pk_affair_no = Request.QueryString["pk_affair_no"];

                    if (pk_affair_no != null && pk_affair_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        fresh_oper data = batch_logic.get_oper(pk_affair_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

            }
        }
        catch (Exception ex)
        {
            result.code = "error";
            result.message = ex.Message;
            result.data = null;
        }
        string result_str = JsonConvert.SerializeObject(result);
        Response.Write(result_str);
    }
}
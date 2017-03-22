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
            //会话检测，检测用户是否登陆


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

                #region NO:12 校验操作员操作对象(事务编号,员工编号,学号)
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
                        if (data != null)
                        {
                            base_stu newdata = new base_stu();
                            newdata.PK_SNO = data.PK_SNO;//学号
                            newdata.FK_SPE_Code = data.FK_SPE_Code;//专业主键
                            newdata.Year = data.Year;//学年
                            newdata.Test_NO = data.Test_NO;//考生号
                            newdata.ID_NO = data.ID_NO;//身份证号
                            newdata.Name = data.Name;//姓名
                            newdata.Gender_Code = data.Gender_Code;//性别码
                            newdata.Photo = data.Photo;//照片地址
                            newdata.Status_Code = data.Status_Code;//迎新状态码
                            newdata.DT_Initial = DateTime.Parse(data.DT_Initial.ToString());//从招办导入时的时间
                            newdata.FK_Class_NO = data.FK_Class_NO;//班级编码
                            newdata.Password = data.Password;//口令
                            result.code = "success";
                            result.message = "成功";
                            result.data = newdata;
                        }
                    }
                }
                #endregion

                #region NO:15 获取某专业数据(学年,专业编号)
                if (cs.Trim().Equals("getSpe"))
                {
                    string year = Request.QueryString["year"];
                    string spe_code = Request.QueryString["spe_code"];

                    if (year != null && year.Trim().Length != 0 && spe_code != null && spe_code.Trim().Length != 0)
                    {
                        model.Fresh_SPE data = organizationService.getSpe(year, spe_code);
                        if (data != null)
                        {
                            fresh_spe newdata = new fresh_spe();
                            newdata.SPE_Code=data.SPE_Code;//专业编号
                            newdata.Year=data.Year;//学年
                            newdata.SPE_Name=data.SPE_Name;//专业名称
                            newdata.EDU_Level_Code=data.EDU_Level_Code;//学历层次码
                            newdata.FK_College_Code=data.FK_College_Code;//学院主键
                            newdata.PK_SPE=data.PK_SPE;//专业主键
                            result.code = "success";
                            result.message = "成功";
                            result.data = newdata;
                        }
                    }
                }
                #endregion

                #region NO:15 获取某专业数据(专业主键)
                if (cs.Trim().Equals("getSpeForPK"))
                {
                    string pk_spe = Request.QueryString["pk_spe"];

                    if (pk_spe != null && pk_spe.Trim().Length != 0 )
                    {
                        model.Fresh_SPE data = organizationService.getSpe(pk_spe);
                        if (data != null)
                        {
                            fresh_spe newdata = new fresh_spe();
                            newdata.SPE_Code = data.SPE_Code;//专业编号
                            newdata.Year = data.Year;//学年
                            newdata.SPE_Name = data.SPE_Name;//专业名称
                            newdata.EDU_Level_Code = data.EDU_Level_Code;//学历层次码
                            newdata.FK_College_Code = data.FK_College_Code;//学院主键
                            newdata.PK_SPE = data.PK_SPE;//专业主键
                            result.code = "success";
                            result.message = "成功";
                            result.data = newdata;
                        }
                    }
                }
                #endregion

                #region NO:16 获取某班级数据(班级编号)
                if (cs.Trim().Equals("getClass"))
                {
                    string pk_class_no = Request.QueryString["pk_class_no"];

                    if (pk_class_no != null && pk_class_no.Trim().Length != 0)
                    {
                        model.Fresh_Class data = organizationService.getClass(pk_class_no);
                        if (data != null)
                        {
                            fresh_class newdata = new fresh_class();
                            newdata.PK_Class_NO = data.PK_Class_NO;//班级编号
                            newdata.FK_Campus_NO = data.FK_Campus_NO;//校区主键
                            newdata.FK_SPE_NO = data.FK_SPE_NO;//专业主键
                            newdata.Name = data.Name;//班级名称
                            result.code = "success";
                            result.message = "成功";
                            result.data = newdata;
                        }
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

                #region NO:14&15&16 获取学生数据(学号)
                if (cs.Trim().Equals("get_student"))
                {
                    string pk_sno = Request.QueryString["pk_sno"];
                    if (pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        batch logic = new batch();
                        List<Object> newdata = null;
                        model.Base_STU stu_data=organizationService.getStu(pk_sno);
                        if (stu_data != null)
                        {
                            newdata = new List<Object>();

                            base_stu stu_newdata = new base_stu();
                            stu_newdata.PK_SNO = stu_data.PK_SNO;//学号
                            stu_newdata.FK_SPE_Code = stu_data.FK_SPE_Code;//专业主键
                            stu_newdata.Year = stu_data.Year;//学年
                            stu_newdata.Test_NO = stu_data.Test_NO;//考生号
                            stu_newdata.ID_NO = stu_data.ID_NO;//身份证号
                            stu_newdata.Name = stu_data.Name;//姓名
                            stu_newdata.Gender_Code = stu_data.Gender_Code;//性别码
                            stu_newdata.Photo = stu_data.Photo;//照片地址
                            stu_newdata.Status_Code = stu_data.Status_Code;//迎新状态码
                            stu_newdata.DT_Initial = DateTime.Parse(stu_data.DT_Initial.ToString());//从招办导入时的时间
                            stu_newdata.FK_Class_NO = stu_data.FK_Class_NO;//班级编码
                            stu_newdata.Password = null;//口令

                            List<base_code_item> itemlist = logic.get_base_code_item("002");
                            if (itemlist != null)
                            {
                                for (int i = 0; i < itemlist.Count; i++)
                                {
                                    if (itemlist[i].Item_NO.Trim().Equals(stu_newdata.Gender_Code.Trim()))
                                    {
                                        stu_newdata.Gender_Code = itemlist[i].Item_Name.Trim();
                                        break;
                                    }
                                }
                            }

                            newdata.Add(new {name="student",data=stu_newdata });

                            if (stu_data.FK_SPE_Code != null)
                            {
                                model.Fresh_SPE spe_data = organizationService.getSpe(stu_data.FK_SPE_Code);
                                if (spe_data != null)
                                {
                                    fresh_spe spe_newdata = new fresh_spe();
                                    spe_newdata.SPE_Code = spe_data.SPE_Code;//专业编号
                                    spe_newdata.Year = spe_data.Year;//学年
                                    spe_newdata.SPE_Name = spe_data.SPE_Name;//专业名称
                                    spe_newdata.EDU_Level_Code = spe_data.EDU_Level_Code;//学历层次码
                                    spe_newdata.FK_College_Code = spe_data.Base_College.Name;//学院名称
                                    spe_newdata.PK_SPE = spe_data.PK_SPE;//专业主键

                                    itemlist = logic.get_base_code_item("001");
                                    if (itemlist != null)
                                    {
                                        for (int i = 0; i < itemlist.Count; i++)
                                        {
                                            if (itemlist[i].Item_NO.Trim().Equals(spe_newdata.EDU_Level_Code.Trim()))
                                            {
                                                spe_newdata.EDU_Level_Code = itemlist[i].Item_Name.Trim();
                                                break;
                                            }
                                        }
                                    }

                                    newdata.Add(new { name = "spe", data = spe_newdata });
                                }
                            }

                            if (stu_data.FK_Class_NO != null)
                            {
                                model.Fresh_Class class_data = organizationService.getClass(stu_data.FK_Class_NO);
                                if (class_data != null)
                                {
                                    fresh_class class_newdata = new fresh_class();
                                    class_newdata.PK_Class_NO = class_data.PK_Class_NO;//班级编号
                                    class_newdata.FK_Campus_NO = class_data.FK_Campus_NO;//校区主键
                                    class_newdata.FK_SPE_NO = class_data.FK_SPE_NO;//专业主键
                                    class_newdata.Name = class_data.Name;//班级名称
                                    newdata.Add(new { name = "class", data = class_newdata });

                                    if (class_newdata.PK_Class_NO != null)
                                    {
                                        model.Fresh_Counseller counseller = organizationService.getCounsellerForClassPK(class_newdata.PK_Class_NO);
                                        if (counseller != null)
                                        {
                                            string FK_Staff_NO = counseller.FK_Staff_NO;
                                            if (FK_Staff_NO != null)
                                            {
                                                model.Base_Staff staff = organizationService.getOperator(FK_Staff_NO.Trim());
                                                if (staff != null)
                                                {
                                                    newdata.Add(new { name = "counseller", data = new { name = staff.Name ,phone=staff.Phone}});
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = newdata;
                    }
                }
                #endregion

                #region NO:17-1 获取某学生现场迎新事务详细列表(学号)
                if (cs.Trim().Equals("get_schoolaffairlog_detail_list"))
                {
                    string pk_sno = Request.QueryString["pk_sno"];
                    List<object> data = null;
                    if (pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        List<fresh_affair_log> data_log = batch_logic.get_schoolaffairlog_list(pk_sno);
                        if (data_log != null)
                        {
                            data = new List<object>();
                            List<fresh_affair> data_affair = new List<fresh_affair>();
                            for (int i = 0; i < data_log.Count; i++)
                            {
                                fresh_affair data2 = batch_logic.get_affair(data_log[i].FK_Affair_NO);
                                data_affair.Add(data2);
                            }
                            data.Add(data_log);
                            data.Add(data_affair);
                        }
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
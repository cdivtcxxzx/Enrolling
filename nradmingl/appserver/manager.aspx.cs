using System;
using System.Collections;
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
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

public static class selftool
{
    public static List<T> JSONStringToList<T>(this string JsonStr)
    {
        JavaScriptSerializer Serializer = new JavaScriptSerializer();
        List<T> objs = Serializer.Deserialize<List<T>>(JsonStr);
        return objs;
    }

    public static T Deserialize<T>(string json)
    {
        T obj = Activator.CreateInstance<T>();
        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            return (T)serializer.ReadObject(ms);
        }
    }
}

public partial class nradmingl_appserver_manger : System.Web.UI.Page
{
    //返回给js客户端的数据格式
    public class ResultData
    {
        public string code { get; set; }
        public string message { get; set; }
        public Object data { get; set; }

    }

    public class fee
    {
        public string code { get; set; }
        public string value { get; set; }
    } 

    protected void Page_Load(object sender, EventArgs e)
    {
        ResultData result = new ResultData();
        result.code = "failure";
        result.message = "无效参数";
        result.data = null;
        try {

            #region 检测用户是否登陆
            //Object se_pk_sno = Session["pk_sno"];//获取学号
            //Object se_pk_staff_no = Session["pk_staff_no"];//获取员工编号

            //if ((se_pk_sno == null || se_pk_sno.ToString().Trim().Length == 0) && (se_pk_staff_no == null || se_pk_staff_no.ToString().Trim().Length == 0))
            //{
            //    result.message = "非授权访问";
            //}
            #endregion

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
                                    spe_newdata.FK_College_Code = spe_data.FK_College_Code;//学院主键
                                    if (spe_data.FK_College_Code != null)
                                    {
                                        model.Base_College college=organizationService.getColleage(spe_data.FK_College_Code.Trim());
                                        if (college != null)
                                        {
                                            spe_newdata.FK_College_Code = college.Name;//学院名称
                                        }
                                    }
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

                #region NO:42 获取学生事务操作列表(学号)
                if (cs.Trim().Equals("get_freshstudent_affair_list"))
                {
                    string pk_sno = Request.QueryString["pk_sno"];

                    if (pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        List<fresh_affair> data = batch_logic.get_freshstudent_affair_list(pk_sno);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:43 获取某学生自助迎新事务列表(学号)
                if (cs.Trim().Equals("get_studentaffairlog_list"))
                {
                    string pk_sno = Request.QueryString["pk_sno"];

                    if (pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        List<fresh_affair_log> data = batch_logic.get_studentaffairlog_list(pk_sno);
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region NO:42&43 获取某学生自助迎新事务级、状态、操作列表(学号)
                if (cs.Trim().Equals("get_freshstudent_affair_status_oper_list"))
                {
                    string pk_sno = Request.QueryString["pk_sno"];

                    if (pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        List<fresh_affair> affair_list = batch_logic.get_freshstudent_affair_list(pk_sno);//事务列表
                        List<fresh_affair_log> affairlog_list = batch_logic.get_studentaffairlog_list(pk_sno);//事务日志列表

                        List<object> data = null;
                        if (affair_list != null && affair_list.Count > 0)
                        {
                            data=new List<object>(); 
                            for (int i = 0; i < affair_list.Count; i++)
                            {
                                fresh_oper oper = batch_logic.get_oper(affair_list[i].PK_Affair_NO);//获取操作
                                string oper_url = null;
                                if (oper != null && oper.OPER_URL!=null && oper.OPER_URL.Trim().Length>0)
                                {
                                    oper_url = oper.OPER_URL.Trim();
                                }
                                if (oper_url!=null && affair_list[i].Parameters != null && affair_list[i].Parameters.Trim().Length > 0)
                                {
                                    oper_url = oper_url+"?" + affair_list[i].Parameters.Trim();//添加操作参数
                                }
                                if (oper_url == null)
                                {
                                    oper_url = "#";
                                }
                                string affair_status = "未完成";
                                for (int j = 0; affairlog_list != null && j < affairlog_list.Count; j++)
                                {
                                    if (affair_list[i].PK_Affair_NO.Trim().Equals(affairlog_list[j].FK_Affair_NO.Trim()))
                                    {
                                        affair_status = affairlog_list[j].Log_Status.Trim();
                                    }
                                }
                                //if (!affair_status.Trim().Equals("已完成") && !affair_status.Trim().Equals("未完成"))
                                //{
                                //    affair_status = "错误状态";
                                //}
                                var row = new
                                {
                                    PK_Affair_NO = affair_list[i].PK_Affair_NO.Trim(),
                                    Affair_Name = affair_list[i].Affair_Name.Trim(),
                                    Affair_Type = affair_list[i].Affair_Type.Trim(),
                                    Affair_Char = affair_list[i].Affair_CHAR,
                                    Affair_Status = affair_status,
                                    StatusDisplay=affair_list[i].StatusDisplay.Trim(),
                                    Affair_Order=affair_list[i].Affair_Order.ToString().Trim(),
                                    Oper_Url = oper_url
                                };
                                data.Add(row);
                            }
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = data;
                    }
                }
                #endregion

                #region  获取学生必交费用款项（迎新批次号，学号）
                if (cs.Trim().Equals("get_ismust_fee_full"))
                {
                    string pk_sno = Request.QueryString["pk_sno"];
                    string pk_batch_no = Request.QueryString["pk_batch_no"];

                    if (pk_sno != null && pk_sno.Trim().Length != 0 && pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        financial logic_fee = new financial();
                        fee_list data = logic_fee.get_fee_ismust(pk_batch_no, pk_sno);
                        result.code = "success";
                        result.message = "成功";
                        result.data = new { single_selection = data.single, multiple_selection = data.multiple };
                    }
                }
                #endregion

                #region  提交学生必交费用款项（迎新批次号，学号）
                if (cs.Trim().Equals("set_ismust_fee_full"))
                {
                    string pk_sno = Request.Form.Get("pk_sno");
                    string pk_batch_no = Request.Form.Get("pk_batch_no");
                    string pk_affair_no = Request.Form.Get("pk_affair_no");
                    string pk_staff_no = Request.Form.Get("pk_staff_no");
                    string returnUrl = Request.Form.Get("returnurl");

                    if (pk_sno != null && pk_sno.Trim().Length != 0 && pk_batch_no != null && pk_batch_no.Trim().Length != 0
                        && pk_affair_no != null && pk_affair_no.Trim().Length != 0)
                    {
                        financial logic_fee = new financial();
                        //获取学生是否已生成订单，如果已生成，则返回订单的url地址；否则生成并保存订单，返回订单的url地址
                        fee_list data = logic_fee.get_fee_ismust(pk_batch_no, pk_sno);
                        if (data.orderid_url != null && data.orderid_url.Trim().Length > 0)
                        {
                            //已生成过订单，直接返回其订单url地址
                            result.code = "success";
                            result.message = "成功";
                            result.data = data.orderid_url;
                        }
                        else
                        {
                            if ((data.single == null || data.single.Count == 0) && (data.multiple == null || data.multiple.Count == 0))
                            {
                                //没有必交费用，返回空的url地址
                                result.code = "success";
                                result.message = "成功";
                                result.data = null;
                            }
                            else
                            {
                                string post_feelist = Request.Form.Get("feelist");
                                if (post_feelist == null || post_feelist.Trim().Length == 0)
                                {
                                    throw new Exception("参数错误");
                                }
                                else
                                {
                                    string[] list1 = post_feelist.Split(',');
                                    if (list1 == null || list1.Length == 0)
                                    {
                                        throw new Exception("参数错误");
                                    }
                                    else
                                    {
                                        int feelist_count = 0;//交费项目数量
                                        if (data.single != null && data.single.Count > 0)
                                        {
                                            feelist_count = data.single.Count;
                                        }
                                        if (data.multiple != null && data.multiple.Count > 0)
                                        {
                                            feelist_count = feelist_count+data.multiple.Count;
                                        }
                                        if (feelist_count!=list1.Length)
                                        {
                                            throw new Exception("参数错误");
                                        }

                                        List<string[]> feelist = new List<string[]>();//交费目录
                                        List<string> feelist_pk = new List<string>();//交费项目主键
                                        for (int i = 0; i < list1.Length; i++)
                                        {
                                            string[] list2 = list1[i].Split(':');
                                            if (list2.Length != 2)
                                            {
                                                throw new Exception("参数错误");
                                            }
                                            string fee_code = list2[0];
                                            float fee_amount = float.Parse(list2[1]);
                                            bool has = false;

                                            feelist.Add(list2);

                                            if (data.single != null && data.single.Count > 0)
                                            {
                                                for (int j = 0; j < data.single.Count; j++)
                                                {
                                                    List<Financial.Fee_Item> item = data.single[j];
                                                    if (item[0].Fee_Code.Trim().ToUpper().Equals(fee_code.Trim().ToUpper()) && item[0].Fee_Amount==fee_amount)
                                                    {
                                                        has = true;
                                                        feelist_pk.Add(item[0].PK_Fee_Item);
                                                        break;
                                                    }
                                                }
                                            }
                                            if (data.multiple != null && data.multiple.Count > 0)
                                            {
                                                for (int j = 0; j < data.multiple.Count; j++)
                                                {
                                                    List<Financial.Fee_Item> item = data.multiple[j];
                                                    for (int k = 0; k < item.Count; k++)
                                                    {
                                                        if (item[k].Fee_Code.Trim().ToUpper().Equals(fee_code.Trim().ToUpper()) && item[k].Fee_Amount == fee_amount)
                                                        {
                                                            has = true;
                                                            feelist_pk.Add(item[k].PK_Fee_Item);
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                            if (!has)
                                            {
                                                throw new Exception("参数错误");
                                            }
                                        }

                                        #region 检查操作权限
                                        string session_pk_sno = null;
                                        string session_pk_staff_no = null;
                                        if (Session["pk_sno"] != null)
                                        {
                                            session_pk_sno = Session["pk_sno"].ToString();
                                        }
                                        if (Session["pk_staff_no"] != null)
                                        {
                                            session_pk_staff_no = Session["pk_staff_no"].ToString();
                                        }

                                        batch batch_logic = new batch();
                                        affair_operate_auth_msg jg = batch_logic.affair_operate_auth(pk_affair_no, pk_sno, session_pk_sno, pk_staff_no, session_pk_staff_no, "cdivtc_jf_ab087");
                                        if (!jg.isauth)
                                        {
                                            throw new Exception(jg.msg);
                                        }
                                        #endregion

                                        //生成订单用户，生成订单，存储订单号，返回订单url地址
                                        financial fee_logic = new financial();
                                        bool flag = fee_logic.InitStuInfo(pk_sno);//生成订单用户

                                        if (flag)
                                        {
                                            //根据订单目录创建订单列表
                                            string orderid_url = null;
                                            Financial.ArrayOfString wdata = new Financial.ArrayOfString();
                                            for (int i = 0; i < feelist_pk.Count; i++)
                                            {
                                                wdata.Add(feelist_pk[i]);
                                            }

                                            orderid_url = fee_logic.InitPayOrder(pk_affair_no, pk_sno, wdata, "system", returnUrl);//生成订单并返回订单url地址
                                            result.code = "success";
                                            result.message = "成功";
                                            result.data = orderid_url;
                                        }
                                        else
                                        {
                                            throw new Exception("创建缴费帐户错误");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region  获取学生交费列表（迎新批次号，学号）
                if (cs.Trim().Equals("get_fee"))
                {
                    string pk_sno = Request.QueryString["pk_sno"];
                    string pk_batch_no = Request.QueryString["pk_batch_no"];

                    if (pk_sno != null && pk_sno.Trim().Length != 0 && pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        financial logic_fee = new financial();
                        List<fee_list> data = logic_fee.get_fee(pk_batch_no, pk_sno);
                        result.code = "success";
                        result.message = "成功";
                        result.data = new { single_must = data[0].single, multiple_must = data[0].multiple, single_nomust = data[1].single, multiple_nomust = data[1].multiple };
                    }
                }
                #endregion

                #region  获取学生未生成订单的交费列表（迎新批次号，学号）
                if (cs.Trim().Equals("get_fee_no_order"))
                {
                    string pk_sno = Request.QueryString["pk_sno"];
                    string pk_batch_no = Request.QueryString["pk_batch_no"];

                    if (pk_sno != null && pk_sno.Trim().Length != 0 && pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        financial logic_fee = new financial();
                        List<fee_list> data = logic_fee.get_fee_no_order(pk_batch_no, pk_sno);
                        result.code = "success";
                        result.message = "成功";
                        result.data = new { single_must = data[0].single, multiple_must = data[0].multiple, single_nomust = data[1].single, multiple_nomust = data[1].multiple };
                    }
                }
                #endregion

                #region  生成学生交费订单（迎新批次号，学号）
                if (cs.Trim().Equals("make_fee_order"))
                {
                    string pk_sno = Request.Form.Get("pk_sno");
                    string pk_batch_no = Request.Form.Get("pk_batch_no");
                    string pk_affair_no = Request.Form.Get("pk_affair_no");
                    string pk_staff_no = Request.Form.Get("pk_staff_no");
                    string returnUrl = Request.Form.Get("returnurl");
                    string post_feelist = Request.Form.Get("feelist");

                    if (pk_sno != null && pk_sno.Trim().Length != 0 && pk_batch_no != null && pk_batch_no.Trim().Length != 0
                        && pk_affair_no != null && pk_affair_no.Trim().Length != 0 && post_feelist != null && post_feelist.Trim().Length != 0)
                    {

                        List<fee> feelist = new List<fee>();
                        List<string> feelist_pk = new List<string>();//交费项目主键
                        feelist = selftool.JSONStringToList<fee>(post_feelist);
                        if (feelist.Count > 0)
                        {
                            string TuitionClass = null;//绿色通道、助学贷款标志
                            financial logic_fee = new financial();
                            List<fee_list> data = logic_fee.get_fee_no_order(pk_batch_no, pk_sno);//获取未生成订单的学生费用款项,包括必交费、选交费
                            if (data != null && data.Count == 2)
                            {
                                #region 验证提交的款项是否是还没有生成订单的款项
                                for (int k = 0; k < feelist.Count; k++)
                                {
                                    fee row = feelist[k];
                                    bool find = false;
                                    for (int i = 0; data[0].single != null && data[0].single.Count > i; i++)
                                    {
                                        if (row.code.Trim().Equals(data[0].single[i][0].Fee_Code.Trim()) && row.value.Trim().Equals(data[0].single[i][0].PK_Fee_Item.Trim()))
                                        {
                                            find = true;
                                            feelist_pk.Add(data[0].single[i][0].PK_Fee_Item);
                                            if (data[0].single[i][0].Fee_Name.Trim().Equals("绿色通道"))
                                            {
                                                TuitionClass = "绿色通道";
                                            }
                                            if (data[0].single[i][0].Fee_Name.Trim().Equals("助学贷款"))
                                            {
                                                TuitionClass = "助学贷款";
                                            }
                                        }
                                    }
                                    for (int i = 0; data[0].multiple != null && data[0].multiple.Count > i; i++)
                                    {
                                        for (int j = 0; data[0].multiple[i] != null && data[0].multiple[i].Count > j; j++)
                                        {
                                            if (row.code.Trim().Equals(data[0].multiple[i][j].Fee_Code.Trim()) && row.value.Trim().Equals(data[0].multiple[i][j].PK_Fee_Item.Trim()))
                                            {
                                                find = true;
                                                feelist_pk.Add(data[0].multiple[i][j].PK_Fee_Item);
                                                if (data[0].multiple[i][j].Fee_Name.Trim().Equals("绿色通道"))
                                                {
                                                    TuitionClass = "绿色通道";
                                                }
                                                if (data[0].multiple[i][j].Fee_Name.Trim().Equals("助学贷款"))
                                                {
                                                    TuitionClass = "助学贷款";
                                                }
                                            }
                                        }
                                    }
                                    for (int i = 0; data[1].single != null && data[1].single.Count > i; i++)
                                    {
                                        if (row.code.Trim().Equals(data[1].single[i][0].Fee_Code.Trim()) && row.value.Trim().Equals(data[1].single[i][0].PK_Fee_Item.Trim()))
                                        {
                                            find = true;
                                            feelist_pk.Add(data[1].single[i][0].PK_Fee_Item);
                                            if (data[1].single[i][0].Fee_Name.Trim().Equals("绿色通道"))
                                            {
                                                TuitionClass = "绿色通道";
                                            }
                                            if (data[1].single[i][0].Fee_Name.Trim().Equals("助学贷款"))
                                            {
                                                TuitionClass = "助学贷款";
                                            }
                                        }
                                    }
                                    for (int i = 0; data[1].multiple != null && data[1].multiple.Count > i; i++)
                                    {
                                        for (int j = 0; data[1].multiple[i] != null && data[1].multiple[i].Count > j; j++)
                                        {
                                            if (row.code.Trim().Equals(data[1].multiple[i][j].Fee_Code.Trim()) && row.value.Trim().Equals(data[1].multiple[i][j].PK_Fee_Item.Trim()))
                                            {
                                                find = true;
                                                feelist_pk.Add(data[1].multiple[i][j].PK_Fee_Item);
                                                if (data[1].multiple[i][j].Fee_Name.Trim().Equals("绿色通道"))
                                                {
                                                    TuitionClass = "绿色通道";
                                                }
                                                if (data[1].multiple[i][j].Fee_Name.Trim().Equals("助学贷款"))
                                                {
                                                    TuitionClass = "助学贷款";
                                                }
                                            }
                                        }
                                    }
                                    if (!find)
                                    {
                                        result.code = "failure";
                                        result.message = "错误参数";
                                        result.data = null;
                                        break;
                                    }
                                }
                                #endregion

                                #region 检查操作权限
                                string session_pk_sno = null;
                                string session_pk_staff_no = null;
                                if (Session["pk_sno"] != null)
                                {
                                    session_pk_sno = Session["pk_sno"].ToString();
                                }
                                if (Session["pk_staff_no"] != null)
                                {
                                    session_pk_staff_no = Session["pk_staff_no"].ToString();
                                }

                                batch batch_logic = new batch();
                                affair_operate_auth_msg jg = batch_logic.affair_operate_auth(pk_affair_no, pk_sno, session_pk_sno, pk_staff_no, session_pk_staff_no, "cdivtc_jf_ab087");
                                if (!jg.isauth)
                                {
                                    throw new Exception(jg.msg);
                                }
                                #endregion

                                #region 生成学生订单
                                bool flag = true;
                                List<fresh_fee> data2=logic_fee.get_fresh_fee(pk_sno);
                                if (data2 == null || data2.Count == 0)
                                {
                                    flag = logic_fee.InitStuInfo(pk_sno);//生成订单用户
                                }
                                if (flag)
                                {
                                    //根据订单目录创建订单列表
                                    string orderid_url = null;
                                    Financial.ArrayOfString wdata = new Financial.ArrayOfString();
                                    for (int i = 0; i < feelist_pk.Count; i++)
                                    {
                                        wdata.Add(feelist_pk[i]);
                                    }

                                    orderid_url = logic_fee.InitPayOrder(pk_affair_no, pk_sno, wdata, "system", returnUrl);//生成订单并返回订单url地址
                                    if (TuitionClass != null)
                                    {
                                        batch_logic.set_TuitionFee(pk_sno, TuitionClass);
                                    }
                                    result.code = "success";
                                    result.message = "成功";
                                    result.data = orderid_url;
                                }
                                else
                                {
                                    throw new Exception("创建缴费帐户错误");
                                }
                                #endregion
                            }
                        }
                    }
                }
                #endregion

                #region  获取学生已生成订单的交费列表（学号）
                if (cs.Trim().Equals("get_fee_order"))
                {
                    string pk_sno = Request.QueryString["pk_sno"];

                    if (pk_sno != null && pk_sno.Trim().Length != 0 )
                    {
                        financial logic_fee = new financial();
                        List<fresh_fee> data = logic_fee.get_fresh_fee(pk_sno);//获取本系统保存的学生订单
                        if (data != null && data.Count > 0)
                        {
                            List<object> jg = new List<object>();
                            for (int i = 0; i < data.Count; i++)
                            {
                                string orderid = data[i].FEE_ORDERID;
                                string orderurl = data[i].FEE_ORDERID_URL;
                                List<Financial.Fee_Item> data1 = logic_fee.get_feeitem_byorder(orderid);//根据订单获取学生收费款项列表
                                jg.Add(new {order_id=orderid,order_url=orderurl,items=data1 });
                            }
                            result.code = "success";
                            result.message = "成功";
                            result.data = jg;
                        }
                    }
                }
                #endregion

                #region 获取迎新批次目录
                if (cs.Trim().Equals("get_freshbatch_welcome_list"))
                {
                    batch batch_logic = new batch();
                    List<fresh_batch> jg = batch_logic.get_freshbatch_welcome_list();
                    result.code = "success";
                    result.message = "成功";
                    result.data = jg;
                }
                #endregion

                #region 获取可分配给迎新操作员的事务操作列表
                if (cs.Trim().Equals("get_affair_list"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        List<fresh_affair> jg = batch_logic.get_affair_list(pk_batch_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion


                #region 获取某事务下所有授权员工及操作范围数据
                if (cs.Trim().Equals("staff_affair_auth_scope"))
                {
                    string pk_affair_no = Request.QueryString["pk_affair_no"];
                    if (pk_affair_no != null && pk_affair_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.staff_affair_auth_scope(pk_affair_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 获取用户列表
                if (cs.Trim().Equals("get_yonghqx"))
                {
                    string username = Request.QueryString["username"];
                    //if (username != null && username.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_yonghqx(username);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
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
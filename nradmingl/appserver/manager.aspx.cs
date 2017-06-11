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
                                if (data1 != null && data.Count > 0)
                                {
                                    for (int j = 0; j < data1.Count; j++)
                                    {
                                        if (data1[j].Fee_Name.Trim().Equals("绿色通道") || data1[j].Fee_Name.Trim().Equals("助学贷款"))
                                        {
                                            data1.RemoveAt(j);
                                            break;
                                        }
                                    }
                                }
                                jg.Add(new {order_id=orderid,order_url=orderurl,items=data1 });
                            }
                            result.code = "success";
                            result.message = "成功";
                            result.data = jg;
                        }
                        else
                        {
                            result.code = "success";
                            result.message = "成功";
                            result.data = null;
                        }
                    }
                }
                #endregion

                #region 获取当前有效的迎新批次目录
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
                    batch batch_logic = new batch();
                    System.Data.DataTable jg = batch_logic.get_yonghqx(username);
                    result.code = "success";
                    result.message = "成功";
                    result.data = jg;
                }
                #endregion

                #region  学生现场报到（学号）
                if (cs.Trim().Equals("set_freshstudent_register"))
                {
                    string pk_sno = Request.Form.Get("pk_sno");
                    string pk_affair_no = Request.Form.Get("pk_affair_no");
                    string pk_staff_no = Request.Form.Get("pk_staff_no");

                    if (pk_sno != null && pk_sno.Trim().Length != 0  && pk_affair_no != null && pk_affair_no.Trim().Length != 0 )
                    {
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
                        affair_operate_auth_msg jg = batch_logic.affair_operate_auth(pk_affair_no, pk_sno, session_pk_sno, pk_staff_no, session_pk_staff_no, "cdivtc_xsjbxx_9086");
                        if (!jg.isauth)
                        {
                            throw new Exception(jg.msg);
                        }
                        #endregion

                        batch_logic.set_freshstudent_register(pk_sno);

                        string create_name = null;
                        if (Session["pk_sno"] != null)
                        {
                            create_name = session_pk_sno.Trim() + ":" + Session["Name"].ToString().Trim();
                        }
                        if (Session["pk_staff_no"] != null)
                        {
                            create_name = session_pk_staff_no.Trim() + ":" + Session["Name"].ToString().Trim();
                        }

                        batch_logic.set_affairlog(pk_sno, pk_affair_no, "已完成", create_name);
                        //batch_logic.set_affairlog(pk_sno, pk_affair_no, "已完成", "system");
                        result.code = "success";
                        result.message = "成功";
                        result.data = null;
                    }
                }
                #endregion

                #region 获取有效的学院列表
                if (cs.Trim().Equals("get_collegelist"))
                {
                    batch batch_logic = new batch();
                    System.Data.DataTable jg = batch_logic.get_collegelist();
                    result.code = "success";
                    result.message = "成功";
                    result.data = jg;
                }
                #endregion

                #region  修改迎新现场操作员权限
                if (cs.Trim().Equals("modify_operator_auth"))
                {
                    string pk_batch_no = Request.Form.Get("pk_batch_no");
                    string pk_affair_no = Request.Form.Get("pk_affair_no");
                    string pk_staff_no = Request.Form.Get("pk_staff_no");
                    string colleges = Request.Form.Get("colleges");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0 && pk_affair_no != null && pk_affair_no.Trim().Length != 0)
                    {
                        string[] colleges_list = null;
                        colleges_list=colleges.Split(',');
                        batch batch_logic = new batch();
                        batch_logic.modify_operator_auth(pk_batch_no, pk_staff_no, pk_affair_no, colleges_list);
                        result.code = "success";
                        result.message = "成功";
                        result.data = null;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院数据
                if (cs.Trim().Equals("get_batch_collage"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_collage(pk_batch_no, pk_collage_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院迎新数据准备概要状态数据
                if (cs.Trim().Equals("get_batch_collage_outline_status"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable collage_data = batch_logic.get_batch_collage(pk_batch_no, pk_collage_no);//某迎新批次全部或某学院数据
                        System.Data.DataTable spe_data = batch_logic.get_batch_spe(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院专业数据
                        System.Data.DataTable studentgender_data = batch_logic.get_batch_student_gender(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院学生男、女性别数据
                        System.Data.DataTable spehasclass_data = batch_logic.get_batch_spe_hasclass(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院已设置班级数据
                        System.Data.DataTable spenohasclass_data = batch_logic.get_batch_spe_nohasclass(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院未设置班级的专业
                        System.Data.DataTable classhasstudent_data = batch_logic.get_batch_class_hasstudent(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院班级中有学生的班级数据
                        System.Data.DataTable spenohasclassstudent_data = batch_logic.get_batch_spe_nohasclassstudent(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院存在未分班学生的专业数据
                        System.Data.DataTable classhasstudent_buterror_data = batch_logic.get_batch_class_hasstudent_buterror(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院分错专业的学生数据
                        System.Data.DataTable hasbed_data = batch_logic.get_batch_hasbed(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院学生已预分床位数据
                        System.Data.DataTable nohasbedclass_data = batch_logic.get_batch_nohasbedclass(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院学生未预分床位人数数据
                        System.Data.DataTable hasbed_buterror_data = batch_logic.get_batch_hasbed_buterror(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院学生已预分床位，但年或校区错误的数据
                        System.Data.DataTable hascounseller_data = batch_logic.get_batch_hascounseller(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院已设置班主任数据，但年或校区错误的数据
                        System.Data.DataTable nohascounseller_data = batch_logic.get_batch_nohascounseller(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院未设置班主任数据，但年或校区错误的数据
                        System.Data.DataTable hascollageaffair_data = batch_logic.get_batch_hascollageaffair(pk_batch_no);//某迎新批次全校已设置现场迎新事务的事务数据，但年或校区错误的数据
                        System.Data.DataTable nohascollageaffair_data = batch_logic.get_batch_nohascollageaffair(pk_batch_no);//某迎新批次全校未设置现场迎新事务的事务数据，但年或校区错误的数据
                        System.Data.DataTable collegefinancial_data = batch_logic.get_batch_college_financial(pk_batch_no, pk_collage_no);//某迎新批次全校或某学院的专业财务交费项目数据，但年或校区错误的数据

                        System.Data.DataTable nohasbedclass_boy_data = null;
                        System.Data.DataTable nohasbedclass_girl_data = null;
                        if (nohasbedclass_data != null && nohasbedclass_data.Rows.Count > 0)
                        {
                            nohasbedclass_boy_data = nohasbedclass_data.Copy();
                            nohasbedclass_girl_data = nohasbedclass_data.Copy();
                            for (int i = nohasbedclass_data.Rows.Count-1; i >= 0; i--)
                            {
                                if (nohasbedclass_boy_data.Rows[i]["gender"].ToString().Trim().Equals("女"))
                                {
                                    nohasbedclass_boy_data.Rows.RemoveAt(i);
                                }
                                if (nohasbedclass_girl_data.Rows[i]["gender"].ToString().Trim().Equals("男"))
                                {
                                    nohasbedclass_girl_data.Rows.RemoveAt(i);
                                }
                            }
                            nohasbedclass_boy_data.AcceptChanges();
                            nohasbedclass_girl_data.AcceptChanges();
                        }

                        result.code = "success";
                        result.message = "成功";
                        result.data = new { student_gender = studentgender_data, collage = collage_data, spe = spe_data,
                                            spehasclass = spehasclass_data,
                                            spenohasclass = spenohasclass_data,
                                            classhasstudent = classhasstudent_data,
                                            spenohasclassstudent = spenohasclassstudent_data,
                                            classhasstudent_buterror = classhasstudent_buterror_data,
                                            hasbed= hasbed_data,
                                            nohasbedclass = nohasbedclass_data,
                                            hasbed_buterror = hasbed_buterror_data,
                                            hascounseller = hascounseller_data,
                                            nohascounseller = nohascounseller_data,
                                            hascollageaffair = hascollageaffair_data,
                                            nohascollageaffair = nohascollageaffair_data,
                                            collegefinancial = collegefinancial_data,
                                            nohasbedclass_boy = nohasbedclass_boy_data,
                                            nohasbedclass_girl = nohasbedclass_girl_data
                        };
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院数据(汉字列头)
                if (cs.Trim().Equals("get_detail_batch_collage"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_collage(pk_batch_no, pk_collage_no);
                        for (int i = 0; jg!=null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("collage"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("College_NO"))
                            {
                                jg.Columns[i].ColumnName = "学院编码";
                            }
                        }
                        if (jg != null)
                        {
                            jg.Columns.Remove("学院编码");
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院专业数据(汉字列头)
                if (cs.Trim().Equals("get_detail_batch_spe"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_spe(pk_batch_no, pk_collage_no);
                        //college_no,collage,spe_code,spe_name
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("collage"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("college_no"))
                            {
                                jg.Columns[i].ColumnName = "学院编码";
                            }
                            if (colname.Trim().Equals("spe_code"))
                            {
                                jg.Columns[i].ColumnName = "专业编码";
                            }
                            if (colname.Trim().Equals("spe_name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                            if (colname.Trim().Equals("stotal"))
                            {
                                jg.Columns[i].ColumnName = "学生人数";
                            }
                        }
                        if (jg != null)
                        {
                            jg.Columns.Remove("学院编码");
                            jg.Columns.Remove("专业编码");

                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院已设置班级数据(汉字列头)
                if (cs.Trim().Equals("get_batch_spe_hasclass"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_spe_hasclass(pk_batch_no, pk_collage_no);
                        //a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name,d.Campus_NO,d.Campus_Name,c.PK_Class_NO,c.Name as ClassName 
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("FK_Fresh_Batch"))
                            {
                                jg.Columns[i].ColumnName = "批次编码";
                            }
                            if (colname.Trim().Equals("year"))
                            {
                                jg.Columns[i].ColumnName = "年级";
                            }
                            if (colname.Trim().Equals("Campus_NO"))
                            {
                                jg.Columns[i].ColumnName = "校区编码";
                            }
                            if (colname.Trim().Equals("Campus_Name"))
                            {
                                jg.Columns[i].ColumnName = "校区名称";
                            }
                            if (colname.Trim().Equals("PK_Class_NO"))
                            {
                                jg.Columns[i].ColumnName = "班级编码";
                            }
                            if (colname.Trim().Equals("ClassName"))
                            {
                                jg.Columns[i].ColumnName = "班级名称";
                            }
                            if (colname.Trim().Equals("Collage"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("College_NO"))
                            {
                                jg.Columns[i].ColumnName = "学院编码";
                            }
                            if (colname.Trim().Equals("SPE_Code"))
                            {
                                jg.Columns[i].ColumnName = "专业编码";
                            }
                            if (colname.Trim().Equals("SPE_Name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                        }
                        if (jg != null)
                        {
                            jg.Columns.Remove("学院编码");
                            jg.Columns.Remove("专业编码");
                            jg.Columns.Remove("校区编码");
                            jg.Columns.Remove("批次编码");

                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院未设置班级的专业(汉字列头)
                if (cs.Trim().Equals("get_batch_spe_nohasclass"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_spe_nohasclass(pk_batch_no, pk_collage_no);
                        //a.FK_Fresh_Batch, a.Collage,a.College_NO,a.[year] ,a.SPE_Code,a.SPE_Name 
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("fk_fresh_batch"))
                            {
                                jg.Columns[i].ColumnName = "批次编码";
                            }
                            if (colname.Trim().Equals("year"))
                            {
                                jg.Columns[i].ColumnName = "年级";
                            }
                            if (colname.Trim().Equals("collage"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("college_no"))
                            {
                                jg.Columns[i].ColumnName = "学院编码";
                            }
                            if (colname.Trim().Equals("spe_code"))
                            {
                                jg.Columns[i].ColumnName = "专业编码";
                            }
                            if (colname.Trim().Equals("spe_name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                        }
                        if (jg != null)
                        {
                            jg.Columns.Remove("批次编码");
                            jg.Columns.Remove("专业编码");
                            jg.Columns.Remove("学院编码");

                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院班级中有学生的班级数据(汉字列头)
                if (cs.Trim().Equals("get_batch_class_hasstudent"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_class_hasstudent(pk_batch_no, pk_collage_no);
                        //a.[year],a.Collage,a.SPE_Name,b.Name as classname,b.PK_Class_NO,studentcount
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("studentcount"))
                            {
                                jg.Columns[i].ColumnName = "人数";
                            }
                            if (colname.Trim().Equals("year"))
                            {
                                jg.Columns[i].ColumnName = "年级";
                            }
                            if (colname.Trim().Equals("Collage"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("SPE_Name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                            if (colname.Trim().Equals("classname"))
                            {
                                jg.Columns[i].ColumnName = "班级名称";
                            }
                            if (colname.Trim().Equals("PK_Class_NO"))
                            {
                                jg.Columns[i].ColumnName = "班级编码";
                            }
                        }
                        if (jg != null)
                        {
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院存在未分班学生的专业数据(汉字列头)
                if (cs.Trim().Equals("get_batch_spe_nohasclassstudent"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_spe_nohasclassstudent(pk_batch_no, pk_collage_no);
                        //a.[year],a.Collage,a.SPE_Name,studentcount
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("studentcount"))
                            {
                                jg.Columns[i].ColumnName = "人数";
                            }
                            if (colname.Trim().Equals("year"))
                            {
                                jg.Columns[i].ColumnName = "年级";
                            }
                            if (colname.Trim().Equals("Collage"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("SPE_Name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                        }
                        if (jg != null)
                        {
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院分错专业的学生数据(汉字列头)
                if (cs.Trim().Equals("get_batch_class_hasstudent_buterror"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_class_hasstudent_buterror(pk_batch_no, pk_collage_no);
                        //a.[year],a.Collage,a.SPE_Code,a.SPE_Name,a.Name,a.PK_SNO,a.Test_NO,b.Name as ClassName,a.FK_Class_NO,
                        //d.Name as Class_Collage,c.SPE_Code as Class_SPE_Code,c.SPE_Name as Class_SPE_Name
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("year"))
                            {
                                jg.Columns[i].ColumnName = "年级";
                            }
                            if (colname.Trim().Equals("Collage"))
                            {
                                jg.Columns[i].ColumnName = "学生所属学院名称";
                            }
                            if (colname.Trim().Equals("SPE_Code"))
                            {
                                jg.Columns[i].ColumnName = "学生所属专业编码";
                            }
                            if (colname.Trim().Equals("SPE_Name"))
                            {
                                jg.Columns[i].ColumnName = "学生所属专业名称";
                            }
                            if (colname.Trim().Equals("Name"))
                            {
                                jg.Columns[i].ColumnName = "姓名";
                            }
                            if (colname.Trim().Equals("PK_SNO"))
                            {
                                jg.Columns[i].ColumnName = "学号";
                            }
                            if (colname.Trim().Equals("Test_NO"))
                            {
                                jg.Columns[i].ColumnName = "高考报名号";
                            }
                            if (colname.Trim().Equals("ClassName"))
                            {
                                jg.Columns[i].ColumnName = "班级名称";
                            }
                            if (colname.Trim().Equals("FK_Class_NO"))
                            {
                                jg.Columns[i].ColumnName = "班级编码";
                            }
                            if (colname.Trim().Equals("Class_Collage"))
                            {
                                jg.Columns[i].ColumnName = "班级所属学院";
                            }
                            if (colname.Trim().Equals("Class_SPE_Code"))
                            {
                                jg.Columns[i].ColumnName = "班级所属专业编码";
                            }
                            if (colname.Trim().Equals("Class_SPE_Name"))
                            {
                                jg.Columns[i].ColumnName = "班级所属专业名称";
                            }
                        }
                        if (jg != null)
                        {
                            jg.Columns.Remove("学生所属专业编码");
                            jg.Columns.Remove("班级所属专业编码");

                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院学生已预分床位数据(汉字列头)
                if (cs.Trim().Equals("get_batch_hasbed"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_hasbed(pk_batch_no, pk_collage_no);
                        //class_campus_name,collegename,spe_name,classname,pk_class_no,gender,dormname,bedcount,
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("class_campus_name"))
                            {
                                jg.Columns[i].ColumnName = "校区";
                            }
                            if (colname.Trim().Equals("collegename"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("classname"))
                            {
                                jg.Columns[i].ColumnName = "班级名称";
                            }
                            if (colname.Trim().Equals("pk_class_no"))
                            {
                                jg.Columns[i].ColumnName = "班级编码";
                            }
                            if (colname.Trim().Equals("spe_name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                            if (colname.Trim().Equals("gender"))
                            {
                                jg.Columns[i].ColumnName = "性别";
                            }
                            if (colname.Trim().Equals("dormname"))
                            {
                                jg.Columns[i].ColumnName = "宿舍名称";
                            }
                            if (colname.Trim().Equals("bedcount"))
                            {
                                jg.Columns[i].ColumnName = "床位数量";
                            }
                        }
                        if (jg != null)
                        {
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院学生未预分床位人数数据(汉字列头)
                if (cs.Trim().Equals("get_batch_nohasbedclass"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_nohasbedclass(pk_batch_no, pk_collage_no);
                        //tm1.Campus_Name,tm1.Name,tm1.SPE_Name,tm1.ClassName,tm2.gender,tm2.studentcount,tm2.hasbedcount,tm2.nohasbedcount,tm2.requirebedcount
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("Campus_Name"))
                            {
                                jg.Columns[i].ColumnName = "校区";
                            }
                            if (colname.Trim().Equals("Name"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("ClassName"))
                            {
                                jg.Columns[i].ColumnName = "班级名称";
                            }
                            if (colname.Trim().Equals("SPE_Name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                            if (colname.Trim().Equals("gender"))
                            {
                                jg.Columns[i].ColumnName = "性别";
                            }
                            if (colname.Trim().Equals("studentcount"))
                            {
                                jg.Columns[i].ColumnName = "学生人数";
                            }
                            if (colname.Trim().Equals("hasbedcount"))
                            {
                                jg.Columns[i].ColumnName = "已预分床位数";
                            }
                            if (colname.Trim().Equals("nohasbedcount"))
                            {
                                jg.Columns[i].ColumnName = "学生床位差";
                            }
                            if (colname.Trim().Equals("requirebedcount"))
                            {
                                jg.Columns[i].ColumnName = "实际未预分人数";
                            }
                        }
                        if (jg != null)
                        {
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院男学生未预分床位人数数据(汉字列头)
                if (cs.Trim().Equals("get_batch_nohasbedclass_boy"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_nohasbedclass(pk_batch_no, pk_collage_no);
                        int total = 0;
                        for (int i = jg.Rows.Count - 1; i >= 0; i--)
                        {
                            if (jg.Rows[i]["gender"].ToString().Trim().Equals("女"))
                            {
                                jg.Rows.RemoveAt(i);
                            }
                            else
                            {
                                total = total + int.Parse(jg.Rows[i]["requirebedcount"].ToString());
                            }
                        }
                        jg.AcceptChanges();

                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("Campus_Name"))
                            {
                                jg.Columns[i].ColumnName = "校区";
                            }
                            if (colname.Trim().Equals("Name"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("ClassName"))
                            {
                                jg.Columns[i].ColumnName = "班级名称";
                            }
                            if (colname.Trim().Equals("SPE_Name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                            if (colname.Trim().Equals("gender"))
                            {
                                jg.Columns[i].ColumnName = "性别";
                            }
                            if (colname.Trim().Equals("studentcount"))
                            {
                                jg.Columns[i].ColumnName = "学生人数";
                            }
                            if (colname.Trim().Equals("hasbedcount"))
                            {
                                jg.Columns[i].ColumnName = "已预分床位数";
                            }
                            if (colname.Trim().Equals("nohasbedcount"))
                            {
                                jg.Columns[i].ColumnName = "学生床位差";
                            }
                            if (colname.Trim().Equals("requirebedcount"))
                            {
                                jg.Columns[i].ColumnName = "缺少床位数";
                            }
                        }

                        if (jg != null)
                        {
                            DataRow row = jg.NewRow();
                            row["校区"] = "";
                            row["学院名称"] = "";
                            row["专业名称"] = "";
                            row["班级名称"] = "";
                            row["性别"] = "合计";
                            row["缺少床位数"] = total;
                            jg.Rows.InsertAt(row, 0);

                            jg.Columns.Remove("学生人数");
                            jg.Columns.Remove("已预分床位数");
                            jg.Columns.Remove("学生床位差");
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院女学生未预分床位人数数据(汉字列头)
                if (cs.Trim().Equals("get_batch_nohasbedclass_girl"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_nohasbedclass(pk_batch_no, pk_collage_no);
                        int total = 0;
                        for (int i = jg.Rows.Count - 1; i >= 0; i--)
                        {
                            if (jg.Rows[i]["gender"].ToString().Trim().Equals("男"))
                            {
                                jg.Rows.RemoveAt(i);
                            }
                            else
                            {
                                total = total + int.Parse(jg.Rows[i]["requirebedcount"].ToString());
                            }
                        }
                        jg.AcceptChanges();

                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("Campus_Name"))
                            {
                                jg.Columns[i].ColumnName = "校区";
                            }
                            if (colname.Trim().Equals("Name"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("ClassName"))
                            {
                                jg.Columns[i].ColumnName = "班级名称";
                            }
                            if (colname.Trim().Equals("SPE_Name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                            if (colname.Trim().Equals("gender"))
                            {
                                jg.Columns[i].ColumnName = "性别";
                            }
                            if (colname.Trim().Equals("studentcount"))
                            {
                                jg.Columns[i].ColumnName = "学生人数";
                            }
                            if (colname.Trim().Equals("hasbedcount"))
                            {
                                jg.Columns[i].ColumnName = "已预分床位数";
                            }
                            if (colname.Trim().Equals("nohasbedcount"))
                            {
                                jg.Columns[i].ColumnName = "学生床位差";
                            }
                            if (colname.Trim().Equals("requirebedcount"))
                            {
                                jg.Columns[i].ColumnName = "缺少床位数";
                            }
                        }
                        if (jg != null)
                        {
                            DataRow row = jg.NewRow();
                            row["校区"] = "";
                            row["学院名称"] = "";
                            row["专业名称"] = "";
                            row["班级名称"] = "";
                            row["性别"] = "合计";
                            row["缺少床位数"] = total;
                            jg.Rows.InsertAt(row, 0);
                            jg.Columns.Remove("学生人数");
                            jg.Columns.Remove("已预分床位数");
                            jg.Columns.Remove("学生床位差");
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院学生已预分床位，但年或校区错误的数据(汉字列头)
                if (cs.Trim().Equals("get_batch_hasbed_buterror"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_hasbed_buterror(pk_batch_no, pk_collage_no);
                        //a.year as dorm_year,a.campus_name as dorm_campus_name,a.dormname as dorm_name,a.dorm_no
                        //a.gender,collegename,spe_name,class_campus_name,class_year,classname,pk_class_no,bedcount
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("dorm_campus_name"))
                            {
                                jg.Columns[i].ColumnName = "宿舍所在校区";
                            }
                            if (colname.Trim().Equals("collegename"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("classname"))
                            {
                                jg.Columns[i].ColumnName = "班级名称";
                            }
                            if (colname.Trim().Equals("spe_name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                            if (colname.Trim().Equals("gender"))
                            {
                                jg.Columns[i].ColumnName = "性别";
                            }
                            if (colname.Trim().Equals("dorm_year"))
                            {
                                jg.Columns[i].ColumnName = "宿舍年";
                            }
                            if (colname.Trim().Equals("dorm_name"))
                            {
                                jg.Columns[i].ColumnName = "宿舍名称";
                            }
                            if (colname.Trim().Equals("dorm_no"))
                            {
                                jg.Columns[i].ColumnName = "宿舍编号";
                            }
                            if (colname.Trim().Equals("class_campus_name"))
                            {
                                jg.Columns[i].ColumnName = "班级所在校区";
                            }
                            if (colname.Trim().Equals("class_year"))
                            {
                                jg.Columns[i].ColumnName = "班级所在年级";
                            }
                            if (colname.Trim().Equals("pk_class_no"))
                            {
                                jg.Columns[i].ColumnName = "班级编号";
                            }
                            if (colname.Trim().Equals("bedcount"))
                            {
                                jg.Columns[i].ColumnName = "床位数量";
                            }
                        }
                        if (jg != null)
                        {
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院已设置班主任数据(汉字列头)
                if (cs.Trim().Equals("get_batch_hascounseller"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_hascounseller(pk_batch_no, pk_collage_no);
                        //a.name as collagename,a.SPE_Name,a.ClassName,a.PK_Class_NO,a.Campus_Name,d.name,c.phone,c.qq
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("collagename"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("ClassName"))
                            {
                                jg.Columns[i].ColumnName = "班级名称";
                            }
                            if (colname.Trim().Equals("SPE_Name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                            if (colname.Trim().Equals("PK_Class_NO"))
                            {
                                jg.Columns[i].ColumnName = "班级编码";
                            }
                            if (colname.Trim().Equals("Campus_Name"))
                            {
                                jg.Columns[i].ColumnName = "班级所在校区";
                            }
                            if (colname.Trim().Equals("name"))
                            {
                                jg.Columns[i].ColumnName = "姓名";
                            }
                            if (colname.Trim().Equals("phone"))
                            {
                                jg.Columns[i].ColumnName = "联系电话";
                            }
                            if (colname.Trim().Equals("qq"))
                            {
                                jg.Columns[i].ColumnName = "qq号码";
                            }
                        }
                        if (jg != null)
                        {
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院未设置班主任数据(汉字列头)
                if (cs.Trim().Equals("get_batch_nohascounseller"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_nohascounseller(pk_batch_no, pk_collage_no);
                        //a.name as collagename,a.SPE_Name,a.ClassName,a.PK_Class_NO,a.Campus_Name
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("collagename"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                            if (colname.Trim().Equals("ClassName"))
                            {
                                jg.Columns[i].ColumnName = "班级名称";
                            }
                            if (colname.Trim().Equals("SPE_Name"))
                            {
                                jg.Columns[i].ColumnName = "专业名称";
                            }
                            if (colname.Trim().Equals("PK_Class_NO"))
                            {
                                jg.Columns[i].ColumnName = "班级编码";
                            }
                            if (colname.Trim().Equals("Campus_Name"))
                            {
                                jg.Columns[i].ColumnName = "班级所在校区";
                            }
                        }
                        if (jg != null)
                        {
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校已设置现场迎新事务的事务数据(汉字列头)
                if (cs.Trim().Equals("get_batch_hascollageaffair"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_hascollageaffair(pk_batch_no);
                        //kk2.Affair_Name,kk2.Affair_Type,kk1.pk_affair_no,kk1.college_no as college_no,kk3.Name as collegename
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("Affair_Name"))
                            {
                                jg.Columns[i].ColumnName = "事务名称";
                            }
                            if (colname.Trim().Equals("Affair_Type"))
                            {
                                jg.Columns[i].ColumnName = "类型";
                            }
                            if (colname.Trim().Equals("pk_affair_no"))
                            {
                                jg.Columns[i].ColumnName = "事务编号";
                            }
                            if (colname.Trim().Equals("college_no"))
                            {
                                jg.Columns[i].ColumnName = "学院编号";
                            }
                            if (colname.Trim().Equals("collegename"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                        }
                        if (jg != null)
                        {
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校未设置现场迎新事务的事务数据(汉字列头)
                if (cs.Trim().Equals("get_batch_nohascollageaffair"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_nohascollageaffair(pk_batch_no);
                        //kk2.Affair_Name,kk2.Affair_Type,kk1.pk_affair_no,kk1.college_no as require_college_no,kk3.Name as require_collegename
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("Affair_Name"))
                            {
                                jg.Columns[i].ColumnName = "事务名称";
                            }
                            if (colname.Trim().Equals("Affair_Type"))
                            {
                                jg.Columns[i].ColumnName = "类型";
                            }
                            if (colname.Trim().Equals("pk_affair_no"))
                            {
                                jg.Columns[i].ColumnName = "事务编号";
                            }
                            if (colname.Trim().Equals("require_college_no"))
                            {
                                jg.Columns[i].ColumnName = "学院编号";
                            }
                            if (colname.Trim().Equals("require_collegename"))
                            {
                                jg.Columns[i].ColumnName = "学院名称";
                            }
                        }
                        if (jg != null)
                        {
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某迎新批次全校或某学院的专业财务交费项目数据(汉字列头)
                if (cs.Trim().Equals("get_batch_college_financial"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_collage_no = Request.QueryString.Get("pk_collage_no");

                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_college_financial(pk_batch_no, pk_collage_no);
                        for (int i = 0; jg != null && i < jg.Columns.Count; i++)
                        {
                            // Collage, College_NO,SPE_Name,SPE_Code,Fee_Code_Name,Fee_Name, Fee_Amount,Type_Name,Is_Must,Is_Online_Order,Fee_Code"
                            string colname = jg.Columns[i].ColumnName;
                            if (colname.Trim().Equals("Collage"))
                            {
                                jg.Columns[i].ColumnName = "学院";
                            }
                            if (colname.Trim().Equals("College_NO"))
                            {
                                jg.Columns[i].ColumnName = "学院编码";
                            }
                            if (colname.Trim().Equals("SPE_Name"))
                            {
                                jg.Columns[i].ColumnName = "专业";
                            }
                            if (colname.Trim().Equals("SPE_Code"))
                            {
                                jg.Columns[i].ColumnName = "专业编码";
                            }
                            if (colname.Trim().Equals("Fee_Code_Name"))
                            {
                                jg.Columns[i].ColumnName = "项目名称";
                            }
                            if (colname.Trim().Equals("Fee_Name"))
                            {
                                jg.Columns[i].ColumnName = "项目条目";
                            }
                            if (colname.Trim().Equals("Fee_Amount"))
                            {
                                jg.Columns[i].ColumnName = "收费标准";
                            }
                            if (colname.Trim().Equals("Type_Name"))
                            {
                                jg.Columns[i].ColumnName = "类型";
                            }
                            if (colname.Trim().Equals("Is_Must"))
                            {
                                jg.Columns[i].ColumnName = "必交";
                            } 
                            if (colname.Trim().Equals("Is_Online_Order"))
                            {
                                jg.Columns[i].ColumnName = "允许网上交费";
                            }
                            if (colname.Trim().Equals("Fee_Code"))
                            {
                                jg.Columns[i].ColumnName = "财务收费编号";
                            }
                        }
                        if (jg != null)
                        {
                            jg.Columns.Remove("学院编码");
                            jg.AcceptChanges();
                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某批次、某班主任的班级数据
                if (cs.Trim().Equals("get_batch_ClassByCounseller"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_staff_no = Request.QueryString.Get("pk_staff_no");
                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0 && pk_staff_no != null && pk_staff_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_ClassByCounseller(pk_batch_no, pk_staff_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region    某批次事务列表(班级管理模块)
                if (cs.Trim().Equals("get_batch_affairlist"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0 )
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_affairlist(pk_batch_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region  某班级学生列表(班级管理模块)
                if (cs.Trim().Equals("get_classstudent"))
                {
                    string pk_class_no = Request.QueryString.Get("pk_class_no");
                    if (pk_class_no != null && pk_class_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_classstudent(pk_class_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region  某班级学生的某事务状态列表(班级管理模块)
                if (cs.Trim().Equals("get_classstudentandaffairstatus"))
                {
                    string pk_class_no = Request.QueryString.Get("pk_class_no");
                    string pk_affair_no = Request.QueryString.Get("pk_affair_no");
                    if (pk_class_no != null && pk_class_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_classstudentandaffairstatus(pk_class_no, pk_affair_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 根据班主任编号获取他班级对应的当前有效迎新批次
                if (cs.Trim().Equals("get_batch_counseller"))
                {
                    string pk_staff_no = Request.QueryString.Get("pk_staff_no");
                    if (pk_staff_no != null && pk_staff_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_batch_counseller( pk_staff_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 获取某批次某班级所有学生事务状态
                if (cs.Trim().Equals("get_classstudentaffairlog"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_class_no = Request.QueryString.Get("pk_class_no");
                    if (pk_class_no != null && pk_class_no.Trim().Length != 0 && pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_classstudentaffairlog(pk_batch_no, pk_class_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 某学生信息(班级管理模块)
                if (cs.Trim().Equals("get_student_detail"))
                {
                    string pk_sno = Request.QueryString.Get("pk_sno");
                    if (pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_student_detail(pk_sno);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region 修改学生密码
                if (cs.Trim().Equals("modifystupwd"))
                {
                    string pk_sno = null;
                    if (Session["pk_sno"] != null)
                    {
                        pk_sno = Session["pk_sno"].ToString();
                    }

                    if (pk_sno != null && pk_sno.Trim().Length != 0)
                    {
                        string old_pwd = Request.Form.Get("old_pwd");
                        string new_pwd = Request.Form.Get("new_pwd");
                        batch batch_logic = new batch();
                        string jg = batch_logic.modifystupwd(pk_sno,old_pwd,new_pwd);
                        result.code = "success";
                        result.message = jg;
                        result.data = jg;
                    }
                    else
                    {
                        result.message = "未登陆";
                    }
                }
                #endregion

                #region 获取学生数据(模糊查询学号、高考报名号、身份证号)
                if (cs.Trim().Equals("getStuBy_type"))
                {
                    string type = Request.QueryString["type"];
                    string key = Request.QueryString["key"];
                    if (key != null && key.Trim().Length != 0 && type != null && type.Trim().Length != 0)
                    {
                        batch logic = new batch();
                        List<Object> newdata = null;
                        List<model.Base_STU> stu_data = null;
                        if (type.Trim() == "xh")
                        {
                            stu_data = logic.getStuBy_pk_sno(key);
                        }
                        if (type.Trim() == "gkbmh")
                        {
                            stu_data = logic.getStuBy_test_no(key);
                        }
                        if (type.Trim() == "sfzh")
                        {
                            stu_data = logic.getStuBy_id_no(key);
                        }

                        if (stu_data != null)
                        {
                            newdata = new List<Object>();
                            for (int k = 0; k < stu_data.Count; k++)
                            {
                                base_stu stu_newdata = new base_stu();
                                stu_newdata.PK_SNO = stu_data[k].PK_SNO;//学号
                                stu_newdata.FK_SPE_Code = stu_data[k].FK_SPE_Code;//专业主键
                                stu_newdata.Year = stu_data[k].Year;//学年
                                stu_newdata.Test_NO = stu_data[k].Test_NO;//考生号
                                stu_newdata.ID_NO = stu_data[k].ID_NO;//身份证号
                                stu_newdata.Name = stu_data[k].Name;//姓名
                                stu_newdata.Gender_Code = stu_data[k].Gender_Code;//性别码
                                stu_newdata.Photo = stu_data[k].Photo;//照片地址
                                stu_newdata.Status_Code = stu_data[k].Status_Code;//迎新状态码
                                stu_newdata.DT_Initial = DateTime.Parse(stu_data[k].DT_Initial.ToString());//从招办导入时的时间
                                stu_newdata.FK_Class_NO = stu_data[k].FK_Class_NO;//班级编码
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
                                newdata.Add(stu_newdata);
                            }

                        }
                        result.code = "success";
                        result.message = "成功";
                        result.data = newdata;
                    }
                }
                #endregion

                #region 获取某批次某班主任所管班级的通知列表
                if (cs.Trim().Equals("get_classmsgbystaff"))
                {
                    string pk_batch_no = Request.QueryString.Get("pk_batch_no");
                    string pk_staff_no = Request.QueryString.Get("pk_staff_no");
                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0 && pk_staff_no != null && pk_staff_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        System.Data.DataTable jg = batch_logic.get_classmsgbystaff(pk_batch_no, pk_staff_no);
                        result.code = "success";
                        result.message = "成功";
                        result.data = jg;
                    }
                }
                #endregion

                #region  创建班级通知
                if (cs.Trim().Equals("createclassmsg"))
                {
                    string title = Request.Form.Get("title");
                    string content = Request.Form.Get("content");
                    string author = Request.Form.Get("author");
                    string classliststr = Request.Form.Get("classliststr");

                    if (title != null && title.Trim().Length != 0 && content != null && content.Trim().Length != 0
                        && author != null && author.Trim().Length != 0 && classliststr != null && classliststr.Trim().Length != 0)
                    {
                        string[] classlist = null;
                        classlist = classliststr.Split(',');
                        batch batch_logic = new batch();
                        batch_logic.createclassmsg(title, content, author, classlist);
                        result.code = "success";
                        result.message = "成功";
                        result.data = null;
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
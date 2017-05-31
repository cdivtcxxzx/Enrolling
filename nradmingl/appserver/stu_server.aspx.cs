using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using model;
using Newtonsoft.Json;

public partial class nradmingl_appserver_stu_server : System.Web.UI.Page
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

        string type = Request.QueryString["type"];//获取类别
        string pk_sno = Request.QueryString["pk_sno"];//获取学号
        //异常情况处理
        if (type == null || type.Trim().Length == 0)
        {
            Response.Write(JsonConvert.SerializeObject(result));
            return;
        }
        try
        {
            #region 获取学生信息 get_student
            if (type.Trim().Equals("get_student"))
            {
                if (pk_sno != null && pk_sno.Trim().Length != 0)
                {
                    batch logic = new batch();
                    List<Object> newdata = null;
                    model.Base_STU stu_data = organizationService.getStu(pk_sno);
                    if (stu_data != null)
                    {
                        newdata = new List<Object>();
                        //修改性别
                        List<base_code_item> itemlist = logic.get_base_code_item("002");
                        if (itemlist != null)
                        {
                            for (int i = 0; i < itemlist.Count; i++)
                            {
                                if (itemlist[i].Item_NO.Trim().Equals(stu_data.Gender_Code.Trim()))
                                {
                                    stu_data.Gender_Code = itemlist[i].Item_Name.Trim();
                                    break;
                                }
                            }
                        }

                        newdata.Add(new { name = "student", data = stu_data });

                        //专业信息表
                        if (stu_data.FK_SPE_Code != null)
                        {
                            model.Fresh_SPE spe_data = organizationService.getSpe(stu_data.FK_SPE_Code);
                            if (spe_data != null)
                            {
                                //替换学院名称
                                if (spe_data.FK_College_Code != null)
                                {
                                    model.Base_College college = organizationService.getColleage(spe_data.FK_College_Code.Trim());
                                    if (college != null)
                                    {
                                        spe_data.FK_College_Code = college.Name;//学院名称
                                    }
                                }

                                //替换学历代码
                                if (spe_data.EDU_Level_Code != null)
                                {
                                    Base_Code_Item item = organizationService.getCodeItem("001", spe_data.EDU_Level_Code);
                                    if (item != null)
                                    {
                                        spe_data.EDU_Level_Code = item.Item_Name;
                                    }
                                }

                                newdata.Add(new { name = "spe", data = spe_data });
                            }
                        }

                        //班级信息表
                        if (stu_data.FK_Class_NO != null)
                        {
                            model.Fresh_Class class_data = organizationService.getClass(stu_data.FK_Class_NO);
                            if (class_data != null)
                            {

                                newdata.Add(new { name = "class", data = class_data });

                                //班主任信息表
                                if (class_data.PK_Class_NO != null)
                                {
                                    model.Fresh_Counseller counseller = organizationService.getCounsellerForClassPK(class_data.PK_Class_NO);
                                    if (counseller != null)
                                    {
                                        string FK_Staff_NO = counseller.FK_Staff_NO;
                                        if (FK_Staff_NO != null)
                                        {
                                            model.Base_Staff staff = organizationService.getOperator(FK_Staff_NO.Trim());
                                            if (staff != null)
                                            {
                                                //newdata.Add(new { name = "counseller", data = new { name = staff.Name, phone = staff.Phone, qq = counseller.QQ} }); 使用Base_Staff中的phone
                                                newdata.Add(new { name = "counseller", data = new { name = staff.Name, phone = counseller.Phone, qq = counseller.QQ } });
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

            #region 学生信息确认 xsxx_confirm
            if (type.Trim().Equals("xsxx_confirm"))
            {
                string confirmState = Request.QueryString["confirmState"];//获取确认状态
                //string pk_batch_no = Request["pk_batch_no"];
                //string pk_affair_no = Request["pk_affair_no"];
                //string pk_staff_no = Request["pk_staff_no"];

                //#region 检查操作权限
                //string session_pk_sno = null;
                //string session_pk_staff_no = null;
                //if (Session["pk_sno"] != null)
                //{
                //    session_pk_sno = Session["pk_sno"].ToString();
                //}
                //if (Session["pk_staff_no"] != null)
                //{
                //    session_pk_staff_no = Session["pk_staff_no"].ToString();
                //}
                ////权限验证

                //batch batch_logic = new batch();
                //affair_operate_auth_msg jg = batch_logic.affair_operate_auth(pk_affair_no, pk_sno, session_pk_sno, pk_staff_no, session_pk_staff_no, "cdivtc_xsxxqr_xabh");
                //if (!jg.isauth)
                //{
                //    throw new Exception(jg.msg);
                //}
                //#endregion

                if (pk_sno != null && pk_sno.Trim().Length != 0 && confirmState != null)
                {
                    //1代表无误 非1代表信息有错误
                    bool boolState = confirmState == "1" ? true : false;
                    //todo:操作事务修改
                    //string create_name = null;
                    //if (Session["pk_sno"] != null)
                    //{
                    //    create_name = session_pk_sno.Trim() + ":" + Session["Name"].ToString().Trim();
                    //}
                    //if (Session["pk_staff_no"] != null)
                    //{
                    //    create_name = session_pk_staff_no.Trim() + ":" + Session["Name"].ToString().Trim();
                    //}
                    //bool isWrite = batch_logic.set_affairlog(pk_sno, pk_affair_no, "已完成", create_name);

                    //bool isWrite = batch_logic.set_affairlog(pk_sno, pk_affair_no, "已完成", "system:" + DateTime.Now.ToShortDateString());
                    //if (!isWrite)
                    //{
                    //    throw new Exception("事务修改失败");
                    //}
                    if (organizationService.addStuConfirm(pk_sno, boolState))
                    {
                        result.code = "success";
                        result.message = "操作成功！";
                    }
                    else
                    {
                        result.message = "操作失败！";
                    }
                }
            }
            #endregion

            #region 学生信息修改 xsxx_update
            if (type.Trim().Equals("xsxx_update"))
            {
                pk_sno = Request.Form["hidden_pk_sno"];//获取隐藏学号
                string pk_affair_no = Request.Form["pk_affair_no"];
                string pk_batch_no = Request.Form["pk_batch_no"];
                string pk_staff_no = Request.Form["pk_staff_no"];
                string input_area = Request.Form["input_area"];
                string input_city = Request.Form["input_city"];
                string input_province = Request.Form["input_province"];
                string phone = Request.Form["phone"];
                string qqnum = Request.Form["qqnum"];
                string xsxx_addr = Request.Form["xsxx_addr"];
                string xsxx_mz = Request.Form["xsxx_mz"];
                //string xsxx_sfz = Request.Form["xsxx_sfz"];
                //string xsxx_xm = Request.Form["xsxx_xm"];
                //string xsxx_xb = Request.Form["xsxx_xb"];
                string xsxx_sg = Request.Form["xsxx_sg"];
                string xsxx_tz = Request.Form["xsxx_tz"];

                string xsxx_zzmm = Request.Form["xsxx_zzmm"];

                string Huji_add = Request.Form["xsxx_huji_add"];
                string Phone_fa = Request.Form["phone_fa"];
                string Phone_ma = Request.Form["phone_ma"];
                
                //简单论证，todo权限认证
                if (pk_sno != null && pk_sno.Trim().Length != 0)
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
                    //权限验证

                    batch batch_logic = new batch();
                    affair_operate_auth_msg jg = batch_logic.affair_operate_auth(pk_affair_no, pk_sno, session_pk_sno, pk_staff_no, session_pk_staff_no, "cdivtc_xsextend_113");
                    if (!jg.isauth)
                    {
                        throw new Exception(jg.msg);
                    }
                    #endregion

                    Base_STU stu = organizationService.getStu(pk_sno);
                    if (stu != null)
                    {
                        stu.QQ = qqnum.Trim();
                        stu.Phone = phone.Trim();
                        stu.Height = xsxx_sg;
                        stu.Weight = xsxx_tz;
                        stu.Nation_Code = xsxx_mz;
                        stu.Census = input_province + "#" + input_city + "#" + input_area;
                        stu.Politics_Code = xsxx_zzmm;
                        stu.Home_add = xsxx_addr.Trim();

                        stu.Phone_fa = Phone_fa.Trim();
                        stu.Phone_ma = Phone_ma.Trim();
                        stu.Huji_add = Huji_add.Trim();
                    }
                    //todo:操作事务修改
                    string create_name = null;
                    if (Session["pk_sno"] != null)
                    {
                        create_name = session_pk_sno.Trim() + ":" + Session["Name"].ToString().Trim();
                    }
                    if (Session["pk_staff_no"] != null)
                    {
                        create_name = session_pk_staff_no.Trim() + ":" + Session["Name"].ToString().Trim();
                    }
                    bool isWrite = batch_logic.set_affairlog(pk_sno, pk_affair_no, "已完成", create_name);
                    //bool isWrite = batch_logic.set_affairlog(pk_sno, pk_affair_no, "已完成", "system:" + DateTime.Now.ToShortDateString());
                    if (!isWrite)
                    {
                        throw new Exception("事务修改失败");
                    }
                    if (organizationService.stuUpdate(pk_sno, stu))
                    {
                        result.code = "success";
                        result.message = "操作成功！";
                    }
                    else
                    {
                        result.message = "操作失败！";
                    }
                }
            }
            #endregion

            #region 学生信息添加 add_student
            #endregion

            #region 学生信息删除 del_student
            #endregion

        }
        catch (Exception ex)
        {
            result.code = "error";
            result.message = ex.Message;
            result.data = null;
        }//end try-catch

        string result_str = JsonConvert.SerializeObject(result);
        Response.Write(result_str);

    }//end page load
}
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
                                            newdata.Add(new { name = "counseller", data = new { name = staff.Name, phone = staff.Phone } });
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
            if (pk_sno != null && pk_sno.Trim().Length != 0 && confirmState != null)
            {
                //1代表无误 非1代表信息有错误
                bool boolState = confirmState == "1" ? true : false;
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
        string result_str = JsonConvert.SerializeObject(result);
        Response.Write(result_str);
    }
}
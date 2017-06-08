using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using model;

public partial class view_xsxx_pwd_reset : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (IsPostBack)
        {
            string ID_NO = Request["xsxx_ID_NO"].ToString().Trim();
            string Test_NO = Request["xsxx_Test_NO"].ToString().Trim();
            Base_STU stu = organizationService.getStuDataBySFZ(ID_NO);
            if (stu != null && stu.Test_NO == Test_NO)
            {
                stu.Password = stu.ID_NO.Substring(12, 6);
                if (organizationService.stuUpdate(stu.PK_SNO, stu))
                {
                    tsxx.Value = "重置成功，请重新登录！";
                }
                else
                {
                    tsxx.Value = "重置失败，请重试！";
                }
            }
            else
            {
                tsxx.Value = "未找到该学生，请重试！";                
            }
        }
    }
}
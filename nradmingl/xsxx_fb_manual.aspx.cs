using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using model;

public partial class nradmingl_xsxx_fb_manual : System.Web.UI.Page
{
    Base_STU stu = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["pk_sno"] != null && Request["spe"] != null)
        {
            //专业信息
            Fresh_SPE get_spe = organizationService.getSpe(Request["spe"].ToString());
            //个人信息
            stu = organizationService.getStu(Request["pk_sno"].ToString());
            if (get_spe != null && stu != null)
            {
                
                SPE.Text = get_spe.SPE_Name;
                //学院信息
                Base_College coll = organizationService.getColleage(get_spe.FK_College_Code);
                
                if (coll != null )
                {
                    Colleage_txt.Text = coll.Name;
                }
                else
                {
                    Colleage_txt.Text = "-无学院信息-";
                    return;
                }
                //班级信息
                List<Fresh_Class> clsBySPE = organizationService.getClasses(get_spe.PK_SPE);
                if (clsBySPE.Count > 0)
                {
                    ddlClass.DataSource = clsBySPE;
                    ddlClass.DataTextField = "Name";
                    ddlClass.DataValueField = "PK_Class_NO";
                    ddlClass.DataBind();
                }
                //班级信息是否已设置
                if (stu.FK_Class_NO != null && stu.FK_Class_NO.Length > 0)
                {
                    Fresh_Class cls_bySNO = clsBySPE.Find(cls => cls.PK_Class_NO == stu.FK_Class_NO);
                    if (cls_bySNO != null)
                    {
                        ts.Text = "已设置班级：" + cls_bySNO.Name;
                    }
                    else
                    {
                        ts.Text = "已设班级和专业不匹配，请联系管理员！";
                        ddlClass.Items.Clear();
                        ddlClass.Items.Add(new ListItem("选择班级", "-1"));
                    }
                }
                else
                {
                    ts.Text = "";
                }
            }
            else
            {
                SPE.Text = "-无专业信息-";
                Colleage_txt.Text = "-无学院信息-";            
            }
        }
        else
        {
            ts.Text = "参数错误，请重试！";
        }
    }
    //设置
    protected void btn_confirm_Click(object sender, EventArgs e)
    {
        string sel_cls = ddlClass.SelectedValue;
        if (sel_cls != "-1")
        { 
            //设置班级
            if (stu != null && Request["pk_sno"] != null)
            {
                stu.FK_Class_NO = sel_cls;
                if (organizationService.stuUpdate(Request["pk_sno"].ToString(), stu))
                {
                    ts.Text = "设置成功！";
                }
                else
                {
                    ts.Text = "设置失败！";
                }
                
            }
        }
    }
}
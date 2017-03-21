using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using model;

public partial class view_ssfp_yfp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //判断两个参数：oCode  oSNO 操作员还是学生自助  
            if (Request["oCode"] != null && Request["oCode"].ToString() != "" && Request["oSNO"] != null && Request["oSNO"].ToString() != "")
            {
                string operateCode = Request["oCode"].ToString();
                //学号
                string SNO = Request["oSNO"].ToString();

                //14获取学生数据
                Base_STU baseStu = organizationService.getStu(SNO);
                //获取迎新学生数据
                Fresh_STU freshStu = organizationService.getFreshStu(SNO);
                //16获取班级数据
                Fresh_Class freshClass = organizationService.getClass(freshStu.FK_Class_NO);

                if (baseStu == null || freshStu == null || freshClass == null)
                {
                    return;
                }

                //20学生是否已分配床位
                if (!dormitory.isbillet(SNO))
                {//未分配
                   
                    //28获取班级可用房间类型列表
                    DataTable enableRoomByClass = dormitory.classgetroomtype(freshStu.FK_Class_NO, baseStu.Gender_Code);
                    //29获取班级某房间类型可用床位位置列表

                    //30获取班级某房间类型某床位位置的可用床位列表
                    //31获取某班级某房间类型某床位位置的可用宿舍列表
                    //32获取某班级某房间类型某床位位置某宿舍可用楼层列表
                    //33获取某班级某房间类型某床位位置某宿舍某楼层可用房间列表
                }
                else
                {
                    //已分配

                }



            }
        }
        else
        {
            //提交表单信息
            Response.Write("IsPostBack");
        }
    }
}
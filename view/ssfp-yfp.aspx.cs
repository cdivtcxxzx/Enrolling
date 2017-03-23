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
                    room_type.DataSource = enableRoomByClass;
                    room_type.DataBind();
                    //29获取班级某房间类型可用床位位置列表
                    DataTable enableBedByClass = dormitory.classgetbedtype(freshStu.FK_Class_NO);


                    //30获取班级某房间类型某床位位置的可用床位列表
                    DataTable enableBed = dormitory.classgetbed(freshStu.FK_Class_NO);
                    //床位数量
                    bedCount.InnerText = enableBed.Columns.ToString() + "个";

                    //31获取某班级某房间类型某床位位置的可用宿舍列表
                    DataTable enableDorm = dormitory.classgetdorm(freshStu.FK_Class_NO);

                    //32获取某班级某房间类型某床位位置某宿舍可用楼层列表
                    

                    //33获取某班级某房间类型某床位位置某宿舍某楼层可用房间列表

                }
                else
                {
                    //已分配(不能修改)
                    
                }


            }
        }
        else
        {
            //提交表单信息
            
            //Fresh_Bed_Log
            //床位主键FK_Bed_Log|学号Fk_SNO|操作人Updater|操作时间Update_DT
            string result_add = dormitoryControl.Add_Fresh_Bed_Log("2", "1", "chenzhiqiu");
            if (result_add == "1")
            {
                Response.Write("添加成功！");
            }
            else
            {
                Response.Write(result_add);
            }
        }
    }
}
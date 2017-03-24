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
        //btnSave.Enabled = false;
        if (!IsPostBack)
        {
            //判断两个参数：oCode  oSNO 操作员还是学生自助  
            if (Request["oCode"] != null && Request["oCode"].ToString() != "" && Request["oSNO"] != null && Request["oSNO"].ToString() != "")
            {
                string operateCode = Request["oCode"].ToString();
                string SNO = Request["oSNO"].ToString();

                //14获取学生数据
                Base_STU baseStu = organizationService.getStu(SNO);
                //16获取班级数据
                Fresh_Class freshClass = organizationService.getClass(baseStu.FK_Class_NO);
                
                if (baseStu == null || freshClass == null)
                {
                    Response.Write("error");
                    return;
                }
                //缓存于页面 班级编码、性别
                hidenClassNo.Value = baseStu.FK_Class_NO;
                hidenGender.Value = baseStu.Gender_Code;
                //20学生是否已分配床位
                if (!dormitory.isbillet(SNO))
                {//未分配
                    
                    //28获取班级可用房间类型列表

                    List<Fresh_Room_Type> listEnableRoomByClass = dormitory.listclassgetroomtype(baseStu.FK_Class_NO, baseStu.Gender_Code);
                    room_type.DataSource = listEnableRoomByClass;
                    room_type.DataTextField = "Type_Name";
                    room_type.DataValueField = "Type_NO";
                    room_type.DataBind();

                }
                else
                {
                    //已分配(不能修改)
                    
                }


            }
        }
    }

    
    protected void DropDownlistChange(object sender, EventArgs e)
    {
        //房间类型下拉菜单更改事件
        //获取房间类型编码、班级号、性别
        string roomTypeNo = room_type.SelectedValue.ToString();
        string classNo = hidenClassNo.Value;
        string gender = hidenGender.Value;
        if (roomTypeNo != "-1")
        {
            //29获取班级某房间类型可用床位位置列表
            List<Fresh_Bed_Type> listBedType = dormitory.listclassgetbedtype(classNo, gender, roomTypeNo);
            bed_numb.DataSource = listBedType;
            bed_numb.DataTextField = "Bed_Index";
            bed_numb.DataValueField = "Bed_Index";
            bed_numb.DataBind();
        }    
    }

    protected void DropDownBedNumbChange(object sender, EventArgs e)
    {
        //床位位置编号下拉菜单更改事件
        //获取房间类型编号、床位位置编号、班级号、性别
        string roomTypeNo = room_type.SelectedValue.ToString(); 
        string bed_index = bed_numb.SelectedValue.ToString();
        string classNo = hidenClassNo.Value;
        string gender = hidenGender.Value;
        if (bed_index != "-1" && roomTypeNo != "-1")
        {
            //30获取班级某房间类型某床位位置的可用床位列表
            List<Fresh_Bed> listBed = dormitory.listclassgetbed(classNo, gender,roomTypeNo,bed_index);
            //31获取某班级某房间类型某床位位置的可用宿舍列表
            List<Fresh_Dorm> listDorm = dormitory.listclassgetdorm(classNo, gender, roomTypeNo, bed_index);
            bedCount.InnerText = listBed.Count + "个";
            dorm_numb.DataSource = listDorm;
            dorm_numb.DataTextField = "Name";
            dorm_numb.DataValueField = "Dorm_NO";
            dorm_numb.DataBind();

            //todo 校区、类型、宿舍照片

        }        
    }

    protected void DropDownDormChange(object sender, EventArgs e)
    {
        //宿舍位置编号下拉菜单更改事件
        //获取房间类型编号、床位位置编号、宿舍号、班级号、性别
        string roomTypeNo = room_type.SelectedValue.ToString();
        string bed_index = bed_numb.SelectedValue.ToString();
        string dorm_no = dorm_numb.SelectedValue.ToString();
        string classNo = hidenClassNo.Value;
        string gender = hidenGender.Value;
        if (bed_index != "-1" && roomTypeNo != "-1" && dorm_no !="-1")
        {
            //32获取某班级某房间类型某床位位置某宿舍可用楼层列表
            List<string> listFloor = dormitory.listFloor(classNo, gender, roomTypeNo, bed_index, dorm_no);
            flo_numb.DataSource = listFloor;
            flo_numb.DataTextField = "Floor";
            flo_numb.DataValueField = "Floor";
            flo_numb.DataBind();
        }
        

    }

    protected void DropDownFloorChange(object sender,EventArgs e)
    {
        //楼层编号下拉菜单更改事件
        //获取房间类型编号、床位位置编号、宿舍号、楼层、班级号、性别
        string roomTypeNo = room_type.SelectedValue.ToString();
        string bed_index = bed_numb.SelectedValue.ToString();
        string dorm_no = dorm_numb.SelectedValue.ToString();
        string floor = flo_numb.SelectedValue.ToString();
        string classNo = hidenClassNo.Value;
        string gender = hidenGender.Value;
        //33、获取某班级某房间类型某床位位置某宿舍某楼层可用房间列表
        List<string> listRoom = dormitory.listroom(classNo, gender, roomTypeNo, bed_index, dorm_no, floor);
        room_numb.DataSource = listRoom;
        room_numb.DataTextField = "Room_NO";
        room_numb.DataValueField = "Room_NO";
        room_numb.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //提交表单信息
        
    }
}
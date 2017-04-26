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


            //获取传递参数
            string pk_sno = "";
            if (Request["pk_sno"] != null)
            {
                pk_sno = Request["pk_sno"].Trim();//获取学号
                xsxx_xh.Text = pk_sno;
            }
            string pk_affair_no = Request.QueryString["pk_affair_no"];//获取事务主键
            string pk_staff_no = Request.QueryString["pk_staff_no"];//获取员工编号

            #region 检查操作权限
            #endregion

            //根据学号设置绑定各控件值
           
            if (xsxx_xh.Text.Length == 0)
            {
                //Response.Write("学号为空！");
                return;
            }
            if (!dormitory.isbillet(xsxx_xh.Text))
            {//未分配
               // Response.Write("未分配寝室！");
            }
            else
            {
                //已分配，屏掉分配内容
               // Response.Write("已分配寝室！");
                sc_cwxc.Style.Add("display", "none");
                sc_fjxc.Style.Add("display", "none");
                sc_lc.Style.Add("display", "none");
                sc_ld.Style.Add("display", "none");
                sc_lx.Style.Add("display", "none");
                sc_qsxz.Style.Add("display", "none");
                R_room.Visible = false;
                R_bed.Visible = false;
                sc_qsxz.Visible = false;
                //获取已选择寝室床位信息
                DataTable yfp = dormitory.serch_yfpbed(xsxx_xh.Text);
                if (yfp.Rows.Count > 0)
                {
                    this.xzts.InnerHtml = "您已选择:<font color=green><b>" + yfp.Rows[0][1].ToString() +","+ yfp.Rows[0][2].ToString() + "</b></font>寝室<font color=green><b>" + yfp.Rows[0][3].ToString() + "</b></font>床!";
                }
                else
                {
                    this.xzts.InnerHtml = "您已选择寝室，但未找到你的选寝信息，请联系您的班主任！"; 
                }
            }


















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
                //缓存于页面 班级编码、性别、学号
                hidenClassNo.Value = baseStu.FK_Class_NO;
                hidenGender.Value = baseStu.Gender_Code;
                hiddenSno.Value = SNO;
                //20学生是否已分配床位
                if (!dormitory.isbillet(SNO))
                {//未分配
                    
                    //28获取班级可用房间类型列表

                    List<Fresh_Room_Type> listEnableRoomByClass = dormitory.listclassgetroomtype(baseStu.FK_Class_NO, baseStu.Gender_Code);
                    //room_type.DataSource = listEnableRoomByClass;
                    //room_type.DataTextField = "Type_Name";
                    //room_type.DataValueField = "Type_NO";
                    //room_type.DataBind();
                    //room_type.Items.Insert(0, new ListItem("请选择房间类型","-1"));

                    //房间类型照片
                    //shuseImg.Src = listEnableRoomByClass[0].Bed_Layout;
                }
                else
                {
                    //已分配(不能修改)
                    //21学生已分配床位
                    List<Fresh_Bed_Log> listbilletdata = dormitory.listbilletdata(SNO);
                    //22获取床位数据
                    List<Fresh_Bed> freshBed = dormitory.listgetbed(listbilletdata[0].FK_Bed_No);
                    //23获取某房间数据
                    List<Fresh_Room> freshRoom = dormitory.listgetroom(freshBed[0].FK_Room_NO);
                    //24获取某宿舍数据
                    List<Fresh_Dorm> freshDorm = dormitory.listgetdorm(freshRoom[0].FK_Dorm_NO);
                    //25获取某床位位置数据
                    List<Fresh_Bed_Type> listgetbedtype = dormitory.listgetbedtype(freshBed[0].FK_Bed_Type);
                    //26获取某房间类型数据
                    List<Fresh_Room_Type> listgetroomtype = dormitory.listgetroomtype(freshRoom[0].FK_Room_Type);
                    //27获取某收费款项条目数据


                }


            }
        }
    }

    
    protected void DropDownlistChange(object sender, EventArgs e)
    {
        //房间类型下拉菜单更改事件
        //获取房间类型编码、班级号、性别
        //string roomTypeNo = room_type.SelectedValue.ToString();
        //string classNo = hidenClassNo.Value;
        //string gender = hidenGender.Value;
        //if (roomTypeNo != "-1")
        //{
        //    //29获取班级某房间类型可用床位位置列表
        //    List<Fresh_Bed_Type> listBedType = dormitory.listclassgetbedtype(classNo, gender, roomTypeNo);
        //    bed_numb.DataSource = listBedType;
        //    bed_numb.DataTextField = "Bed_Index";
        //    bed_numb.DataValueField = "Bed_Index";
        //    bed_numb.DataBind();
        //    bed_numb.Items.Insert(0, new ListItem("请选择床位位置","-1"));
            
        //}    
    }

    protected void DropDownBedNumbChange(object sender, EventArgs e)
    {
        //床位位置编号下拉菜单更改事件
        //获取房间类型编号、床位位置编号、班级号、性别
        //string roomTypeNo = room_type.SelectedValue.ToString(); 
        //string bed_index = bed_numb.SelectedValue.ToString();
        //string classNo = hidenClassNo.Value;
        //string gender = hidenGender.Value;
        //if (bed_index != "-1" && roomTypeNo != "-1")
        //{
        //    //30获取班级某房间类型某床位位置的可用床位列表
        //    List<Fresh_Bed> listBed = dormitory.listclassgetbed(classNo, gender,roomTypeNo,bed_index);
        //    //31获取某班级某房间类型某床位位置的可用宿舍列表
        //    List<Fresh_Dorm> listDorm = dormitory.listclassgetdorm(classNo, gender, roomTypeNo, bed_index);
        //    bedCount.InnerText = listBed.Count + "个";
        //    dorm_numb.DataSource = listDorm;
        //    dorm_numb.DataTextField = "Name";
        //    dorm_numb.DataValueField = "Dorm_NO";
        //    dorm_numb.DataBind();
        //    dorm_numb.Items.Insert(0, new ListItem("请选择宿舍号", "-1"));
        //    //todo 校区、类型、宿舍照片
        //    xiaoqu.InnerText = listDorm[0].Campus_NO;
        //    shuse.InnerText = listDorm[0].Name;
        //}        
    }

    protected void DropDownDormChange(object sender, EventArgs e)
    {
        //宿舍位置编号下拉菜单更改事件
        //获取房间类型编号、床位位置编号、宿舍号、班级号、性别
        //string roomTypeNo = room_type.SelectedValue.ToString();
        //string bed_index = bed_numb.SelectedValue.ToString();
        //string dorm_no = dorm_numb.SelectedValue.ToString();
        //string classNo = hidenClassNo.Value;
        //string gender = hidenGender.Value;
        //if (bed_index != "-1" && roomTypeNo != "-1" && dorm_no !="-1")
        //{
        //    //32获取某班级某房间类型某床位位置某宿舍可用楼层列表
        //    List<string> listFloor = dormitory.listFloor(classNo, gender, roomTypeNo, bed_index, dorm_no);
        //    flo_numb.DataSource = listFloor;
        //    flo_numb.DataTextField = "Floor";
        //    flo_numb.DataValueField = "Floor";
        //    flo_numb.DataBind();
        //    flo_numb.Items.Insert(0, new ListItem("请选择楼层", "-1"));
        //}
        

    }

    protected void DropDownFloorChange(object sender,EventArgs e)
    {
        //楼层编号下拉菜单更改事件
        //获取房间类型编号、床位位置编号、宿舍号、楼层、班级号、性别
        //string roomTypeNo = room_type.SelectedValue.ToString();
        //string bed_index = bed_numb.SelectedValue.ToString();
        //string dorm_no = dorm_numb.SelectedValue.ToString();
        //string floor = flo_numb.SelectedValue.ToString();
        //string classNo = hidenClassNo.Value;
        //string gender = hidenGender.Value;
        ////33、获取某班级某房间类型某床位位置某宿舍某楼层可用房间列表
        //List<string> listRoom = dormitory.listroom(classNo, gender, roomTypeNo, bed_index, dorm_no, floor);
        //room_numb.DataSource = listRoom;
        //room_numb.DataTextField = "Room_NO";
        //room_numb.DataValueField = "Room_NO";
        //room_numb.DataBind();
        //room_numb.Items.Insert(0, new ListItem("请选择房间号", "-1"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //提交表单信息
        //获取房间类型编号、床位位置编号、宿舍号、班级号、性别、学号、房间编号
        //string roomTypeNo = room_type.SelectedValue.ToString();
        //string bed_index = bed_numb.SelectedValue.ToString();
        //string dorm_no = dorm_numb.SelectedValue.ToString();
        //string classNo = hidenClassNo.Value;
        //string gender = hidenGender.Value;
        //string xh = hiddenSno.Value;
        //string room_no = room_numb.SelectedValue.ToString();
        ////List<Fresh_Bed_Class_Log> updateFresh_Bed_Class_Log
        //dormitory.updateFresh_Bed_Class_Log(xh, dorm_no, room_no, bed_index, "none");
        //Response.Write("选择成功！");
    }


    protected void xq_roomtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        zt();
    }
    protected void xq_dorm_SelectedIndexChanged(object sender, EventArgs e)
    {
        //已选择一号学生公寓3楼306寝室，该寝室已有3人选择，剩于3个床位';
        R_room.Items.Clear();
        R_bed.Items.Clear();
        zt();
        //+R_room.SelectedItem.Text + "寝室，该寝室剩于3个床位！";
    }
    protected void xq_floor_SelectedIndexChanged(object sender, EventArgs e)
    {
        R_room.Items.Clear();
        R_bed.Items.Clear();
        zt();
    }
    protected void R_room_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        R_bed.Items.Clear();
        zt();
    }
    protected void R_bed_SelectedIndexChanged(object sender, EventArgs e)
    {
        zt();
        //获取床位
        DataTable cw = dormitory.serch_bedbz(this.R_bed.SelectedValue);
        if (cw.Rows.Count > 0)
        {
            this.xzts.InnerHtml += "，您选择了<font color=green><b>" + this.R_bed.SelectedItem.Text + "</b></font>床！";
            this.cwts.Text = "" + cw.Rows[0][2].ToString();
        }
        else
        {
            this.cwts.Text= "请选择床位";
        }
    }
    protected void zt()
    {
        this.xzts.InnerHtml = "已选择：" + xq_dorm.SelectedItem.Text;
        if (R_room.SelectedIndex != -1)
        {
            this.xzts.InnerHtml += "<font color=green><b>"+R_room.SelectedItem.Text + "</b></font>寝室！";
        }

        //获取床位
        DataTable cw = dormitory.serch_bed(this.R_room.SelectedValue);
        if (cw.Rows.Count > 0)
        {
            this.xzts.InnerHtml += "该寝室还有" + cw.Rows.Count.ToString() + "个床位";
        }
        //else
        //{
        //    this.xzts.InnerHtml += "，该寝室已没有床位，请重新选择";
        //}
        //获取床位说明

    }
    protected void qsxz_Click(object sender, EventArgs e)
    {
        //获取床位ID和学号，存储到数据库 //获取员工编号
        try
        {
            string czy = xsxx_xh.Text;
            string bedid = "";
            if (Request["pk_staff_no"] != null)
            {
                czy = Request["pk_staff_no"].ToString();
            }
            if (R_bed.SelectedIndex != -1)
            {
                bedid = R_bed.SelectedValue;

            }
            string tsxx = dormitory.update_yfpbed(xsxx_xh.Text, bedid, czy);
            if (tsxx.Split(',')[0] == "1")
            {
                Response.Redirect("");
            }
            else
            {
                xzts.InnerHtml = "<font color=red>" + tsxx.Split(',')[1] + "</font>";
            }
        }
        catch (Exception e1)
        {
            xzts.InnerHtml = "<font color=red>确认选择寝室时出错：" + e1.Message + "</font>";
        }


        //Response.Write(tsxx);
           

    }
}
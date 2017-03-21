using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                string SNO = Request["oSNO"].ToString();
                //20学生是否已分配床位
                //未分配
                //14获取学生数据
                //16获取班级数据
                //28获取班级可用房间类型列表
                //29获取班级某房间类型可用床位位置列表
                //30获取班级某房间类型某床位位置的可用床位列表
                //31获取某班级某房间类型某床位位置的可用宿舍列表
                //32获取某班级某房间类型某床位位置某宿舍可用楼层列表
                //33获取某班级某房间类型某床位位置某宿舍某楼层可用房间列表

                //已分配


            }
        }
        else
        {
            //提交表单信息
            Response.Write("IsPostBack");
        }
    }
}
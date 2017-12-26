using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class nradmingl_ssgl_qstz : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["roomno"] != null&&Request["cwid"]!=null)
        {
            //读取信息
            DataTable xscx = Sqlhelper.Serach("SELECT     TOP (20) Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号,Fresh_Bed.Bed_Name AS 床位位置描述,  Base_College.Name AS 已分配院系,                       Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名, Base_STU.Phone AS 联系电话,Fresh_Bed.PK_Bed_NO 房间id FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Bed_Log LEFT OUTER JOIN                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN                      Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON                       Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO WHERE   Fresh_Bed.PK_Bed_NO='" + Request["cwid"].ToString().Split(',')[0].ToString() + "' ORDER BY 床位编号");
            // g_ts.Text += "SELECT     TOP (20) Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号,Fresh_Bed.Bed_Name AS 床位位置描述,  Base_College.Name AS 已分配院系,                       Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名, Base_STU.Phone AS 联系电话,[Fresh_Bed_Class_Log].FK_Bed_NO 房间id FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Bed_Log LEFT OUTER JOIN                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN                      Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON                       Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO WHERE   FK_Bed_NO='" + x.Split(',')[i].ToString() + "' ORDER BY 床位编号";


            if (xscx.Rows.Count > 0)
            {
                yx.DataBind();
                yx.SelectedIndex = yx.Items.IndexOf(yx.Items.FindByText(xscx.Rows[0]["已分配院系"].ToString()));
            }


            hdfWPBH.Value = Request["cwid"];
            SqlDataSource2.SelectCommand = "SELECT     TOP (20) Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号,Fresh_Bed.Bed_Name AS 床位位置描述,  Base_College.Name AS 已分配院系,                       Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名, Base_STU.Phone AS 联系电话,Fresh_Bed.PK_Bed_NO 房间id FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Bed_Log LEFT OUTER JOIN                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN                      Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON                       Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO WHERE     (Fresh_Room.Room_NO = '" + Request["roomno"] + "') ORDER BY 床位编号";
            GridView3.DataBind();
        }
        else
        {
            if (Request["cwid"] != null)
            {

                //读取信息
                DataTable xscx = Sqlhelper.Serach("SELECT     TOP (20) Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号,Fresh_Bed.Bed_Name AS 床位位置描述,  Base_College.Name AS 已分配院系,                       Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名, Base_STU.Phone AS 联系电话,Fresh_Bed.PK_Bed_NO 房间id FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Bed_Log LEFT OUTER JOIN                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN                      Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON                       Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO WHERE   Fresh_Bed.PK_Bed_NO='" + Request["cwid"].ToString().Split(',')[0].ToString() + "' ORDER BY 床位编号");
                   // g_ts.Text += "SELECT     TOP (20) Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号,Fresh_Bed.Bed_Name AS 床位位置描述,  Base_College.Name AS 已分配院系,                       Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名, Base_STU.Phone AS 联系电话,[Fresh_Bed_Class_Log].FK_Bed_NO 房间id FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Bed_Log LEFT OUTER JOIN                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN                      Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON                       Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO WHERE   FK_Bed_NO='" + x.Split(',')[i].ToString() + "' ORDER BY 床位编号";


                if (xscx.Rows.Count > 0)
                {
                    yx.DataBind();
                    yx.SelectedIndex = yx.Items.IndexOf(yx.Items.FindByText(xscx.Rows[0]["已分配院系"].ToString()));
                }




                hdfWPBH.Value = Request["cwid"];
                string x = Request["cwid"];
                string y = "";
                for (int i = 0; i < x.Split(',').Length; i++)
                {
                    if (i == 0)
                    {
                        y += "Fresh_Bed.PK_Bed_NO='" + x.Split(',')[i].ToString() + "' ";
                    }
                    else
                    {
                        y += " or Fresh_Bed.PK_Bed_NO='" + x.Split(',')[i].ToString() + "' ";
                    }
                }
                SqlDataSource2.SelectCommand = "SELECT     TOP (20) Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号,Fresh_Bed.Bed_Name AS 床位位置描述,  Base_College.Name AS 已分配院系,                       Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名, Base_STU.Phone AS 联系电话,Fresh_Bed.PK_Bed_NO 房间id FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Bed_Log LEFT OUTER JOIN                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN                      Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON                       Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO WHERE   " + y + " ORDER BY 床位编号";
                //Response.Write(SqlDataSource2.SelectCommand);


                GridView3.DataBind();
            }
            else
            {
                g_ts.Text = "<font color=red>参数传递不正确！</font>";
            }
        }

        #region 管理数据筛选
        string qx = "";
        string sx = "";
        #region 获取该操作员能操作的系数据
        Power qxhq = new Power();
        qx = qxhq.Getonebmdm("Fresh_SPE.FK_College_Code");
        try
        {
            qx = qx.Substring(0, qx.Length - 1);
        }
        catch { }
        //Response.Write(qx);

        #endregion
        if (qx.Split(',').Length > 0)
        {

            for (int i = 0; i < qx.Split(',').Length; i++)
            {
                #region 清除本年度预分配数据
                string sqlcx = "SELECT TOP 1 [YXMC] FROM  [DM_YUANXI] where yxdm='" + qx.Split(',')[i].ToString() + "'";
                DataTable qxd = Sqlhelper.Serach(sqlcx);
                //this.ztts.Text +=sqlcx;
                if (qxd.Rows.Count > 0)
                {
                    //查询有多少条预分配数据
                    // SELECT     count(Fresh_Bed_Class_Log.PK_Bed_Class_Log) FROM         Fresh_SPE RIGHT OUTER JOIN                      Fresh_Class ON Fresh_SPE.PK_SPE = Fresh_Class.FK_SPE_NO RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO
                    sx += " or yxdm='" + qx.Split(',')[i].ToString() + "'";


                }

                #endregion
            }
            if (sx.Length > 0)
            {
                sx = " yxdm='0' " + sx + "";
            }
        }
        #endregion
        //Response.Write(sx);
        if (!IsPostBack)
        {
            this.SqlDataSource6.FilterExpression = sx;//院系筛选
        }
    }
    protected void yx_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        g_ts.Text = "";

        string x = hdfWPBH.Value;
        string y = "";
        string cccw = "";
        for (int i = 0; i < x.Split(',').Length; i++)
        {
            //判断班级是否分配学生
            DataTable xscx = Sqlhelper.Serach("SELECT     TOP (20) Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号,Fresh_Bed.Bed_Name AS 床位位置描述,  Base_College.Name AS 已分配院系,                       Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名, Base_STU.Phone AS 联系电话,[Fresh_Bed_Class_Log].FK_Bed_NO 房间id FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Bed_Log LEFT OUTER JOIN                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN                      Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON                       Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO WHERE   [Fresh_Bed_Class_Log].FK_Bed_NO='" + x.Split(',')[i].ToString() + "' ORDER BY 床位编号");
            if (xscx.Rows.Count > 0)
            {
                if (xscx.Rows[0]["学生学号"].ToString().Length > 1)
                {
                    g_ts.Text += "<font color=red>" + xscx.Rows[0]["床位编号"].ToString() + "号床位不能调整，已有学生选择该床位！</font>"; ;
                    //return;
                }
                else
                {
                    cccw += xscx.Rows[0]["床位编号"].ToString() + ",";
                    if (y.Length == 0)
                    {
                        y += "FK_Bed_NO='" + x.Split(',')[i].ToString() + "' ";
                    }
                    else
                    {
                        y += " or FK_Bed_NO='" + x.Split(',')[i].ToString() + "' ";
                    }
                }
            }


        }
        if (y.Length > 0)
        {
            y = " where " + y; 
            if (Sqlhelper.ExcuteNonQuery(" update [Fresh_Bed_Class_Log] set FK_Class_NO='" + this.DropDownList1.SelectedValue + "' " + y) > 0)
            {
                g_ts.Text += "<font color=blue>床位调整成功！</font>";

            }
            else
            {
                g_ts.Text += "<font color=red>床位调整失败！请重试!</font>";
            }
        }
        GridView3.DataBind();
    }
    protected void Button1_Click2(object sender, EventArgs e)
    {
        g_ts.Text = "";
           
              string x = hdfWPBH.Value;
              //
                string y = "";
                string cccw = "";
                for (int i = 0; i < x.Split(',').Length; i++)
                {
                    //判断班级是否分配学生
                    DataTable xscx = Sqlhelper.Serach("SELECT     TOP (20) Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号,Fresh_Bed.Bed_Name AS 床位位置描述,  Base_College.Name AS 已分配院系,                       Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名, Base_STU.Phone AS 联系电话,[Fresh_Bed_Class_Log].FK_Bed_NO 房间id FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Bed_Log LEFT OUTER JOIN                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN                      Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON                       Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO WHERE   [Fresh_Bed_Class_Log].FK_Bed_NO='" + x.Split(',')[i].ToString() + "' ORDER BY 床位编号");
                   // g_ts.Text += "SELECT     TOP (20) Fresh_Room.Room_NO AS 房间编号, Fresh_Bed.Bed_NO AS 床位编号,Fresh_Bed.Bed_Name AS 床位位置描述,  Base_College.Name AS 已分配院系,                       Fresh_Class.Name AS 已分配班级, Fresh_Bed_Log.FK_SNO AS 学生学号, Base_STU.Name AS 学生姓名, Base_STU.Phone AS 联系电话,[Fresh_Bed_Class_Log].FK_Bed_NO 房间id FROM         Fresh_Bed LEFT OUTER JOIN                      Fresh_Bed_Log LEFT OUTER JOIN                      Base_STU ON Fresh_Bed_Log.FK_SNO = Base_STU.PK_SNO ON Fresh_Bed.PK_Bed_NO = Fresh_Bed_Log.FK_Bed_NO LEFT OUTER JOIN                      Base_College ON Fresh_Bed.College_NO = Base_College.College_NO LEFT OUTER JOIN                      Fresh_Class RIGHT OUTER JOIN                      Fresh_Bed_Class_Log ON Fresh_Class.PK_Class_NO = Fresh_Bed_Class_Log.FK_Class_NO ON                       Fresh_Bed.PK_Bed_NO = Fresh_Bed_Class_Log.FK_Bed_NO LEFT OUTER JOIN                      Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO WHERE   FK_Bed_NO='" + x.Split(',')[i].ToString() + "' ORDER BY 床位编号";
                   
                    
                    if (xscx.Rows.Count > 0)
                    {
                        if (xscx.Rows[0]["学生学号"].ToString().Length > 1)
                        {
                            g_ts.Text += "<font color=red>" + xscx.Rows[0]["床位编号"].ToString() + "号床位未清除，已有学生选择该床位！</font>"; ;
                            //return;
                        }
                        else
                        {
                            cccw += xscx.Rows[0]["床位编号"].ToString()+",";
                            if (y.Length == 0)
                            {
                                y += "FK_Bed_NO='" + x.Split(',')[i].ToString() + "' ";
                            }
                            else
                            {
                                y += " or FK_Bed_NO='" + x.Split(',')[i].ToString() + "' ";
                            }
                        }
                    }

                    
                }
                //g_ts.Text += "update [Fresh_Bed_Class_Log] set FK_Class_NO='' where  " + y;
               // return;

                if (y.Length > 0)
                {
                    y = " where " + y; 
                    if (Sqlhelper.ExcuteNonQuery(" update [Fresh_Bed_Class_Log] set FK_Class_NO=null " + y) > 0)
                    {
                        g_ts.Text += "<font color=blue>床位清除班级成功！</font>";

                    }
                    else
                    {
                        g_ts.Text += "<font color=red>床位清除班级失败！请重试!</font>";
                    }
                    GridView3.DataBind();
                }
                else
                {
                    g_ts.Text += "<font color=red>无床位需清除！</font>";
                }
    }
}
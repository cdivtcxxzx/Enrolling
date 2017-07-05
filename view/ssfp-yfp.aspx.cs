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
        cwts.Text = "";
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

            if (pk_sno == null || pk_sno.Trim().Length == 0)
            {
                Response.Write("<script>alert('参数错误');location.href='xxzz_xsindex.aspx';</script>");
                Response.End();
                return;
            }


            if (pk_affair_no == null || pk_affair_no.Trim().Length == 0)
            {
                Response.Write("<script>alert('参数错误');location.href='xxzz_xsindex.aspx';</script>");
                Response.End();
                return;
            }


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

            batch batch_logic = new batch();
            affair_operate_auth_msg jg = batch_logic.affair_operate_auth(pk_affair_no, pk_sno, session_pk_sno, pk_staff_no, session_pk_staff_no, "cdivtc_xzss_01a");
            if (!jg.isauth)
            {
                Response.Write("<script>alert('" + jg.msg + "');location.href='xxzz_xsindex.aspx';</script>");
                Response.End();
                return;

            }






            #endregion

            this.pk_staff_no.Value = pk_staff_no;
            this.pk_affair_no.Value = pk_affair_no;

            //根据学号设置绑定各控件值

            if (xsxx_xh.Text.Length == 0)
            {
                //设置学生信息
                
                return;
            }
            else
            {
                //设置学生信息
                xsxx();
            }
            
            if (!dormitory.isbillet(xsxx_xh.Text))
            {//未分配
                // 设置默认照片信息
                zp();
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
                    this.xzts.InnerHtml = "您已选择:<font color=green><b>" + yfp.Rows[0][1].ToString() + "," + yfp.Rows[0][2].ToString() + "</b></font>寝室<font color=green><b>" + yfp.Rows[0][3].ToString() + "</b></font>床!";
                }
                else
                {
                    this.xzts.InnerHtml = "您已选择寝室，但未找到你的选寝信息，请联系您的辅导员！";
                }

                //设置已选寝室照片信息
                DataTable qszp = dormitory.serch_dormyfp(xsxx_xh.Text);
                if (qszp.Rows.Count > 0)
                {
                    this.shuseImg.Src = qszp.Rows[0]["小图"].ToString();
                    this.shuseImg.Attributes.Add("onclick", "location.href='ssfp_zp.aspx?img=" + qszp.Rows[0]["大图"].ToString() + "'");
                }
                else
                {
                    zp();
                }

















            //判断两个参数：oCode  oSNO 操作员还是学生自助  
   
        }
    }




    protected void xq_roomtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        zt();
    }
    protected void xq_dorm_SelectedIndexChanged(object sender, EventArgs e)
    {
        //照片设置
        zp();
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
        this.shuseImg.Src = "images/room_small.jpg";
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
            this.cwts.Text = "请选择床位";
        }
    }
    protected void zt()
    {
        this.xzts.InnerHtml = "已选择：" + xq_dorm.SelectedItem.Text;
        if (R_room.SelectedIndex != -1)
        {
            this.xzts.InnerHtml += "<font color=green><b>" + R_room.SelectedItem.Text + "</b></font>寝室！";
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
    protected void zp()
    {
        //获取寝室照片信息
        string zp = this.shuseImg.Src;
        string zpbig = this.shuseImg.Src;
        DataTable zpok = dormitory.serch_dorm(xsxx_xh.Text, xq_dorm.SelectedValue);
        if (zpok.Rows.Count > 0)
        {
            if (zpok.Rows[0][4].ToString().Length>4) zp = zpok.Rows[0][4].ToString();
            if (zpok.Rows[0][3].ToString().Length > 4) zpbig = zpok.Rows[0][3].ToString();

        }
        this.shuseImg.Src = zp;
        this.shuseImg.Attributes.Add("onclick", "location.href='ssfp_zp.aspx?img=" + zpbig + "'");
    }
    protected void xsxx()
    {
        //根据学号设置学生个人基本信息
        DataTable xsxxok = dormitory.serch_xsxx(xsxx_xh.Text);
        if (xsxxok.Rows.Count > 0)
        {
            this.xsxx_xm.Text = xsxxok.Rows[0]["姓名"].ToString();
            this.xsxx_bj.Text = xsxxok.Rows[0]["班级名称"].ToString();
            //查询该班是否有足够的寝室
            string bjxx = xsxxok.Rows[0]["班级代码"].ToString();
            string xb = xsxxok.Rows[0]["性别"].ToString();

            DataTable yzbj = Sqlhelper.Serach("SELECT     TOP (10) Fresh_Bed_Class_Log.PK_Bed_Class_Log, Fresh_Bed_Class_Log.FK_Bed_NO, Fresh_Bed_Class_Log.FK_Class_NO,                       Fresh_Bed_Class_Log.College_NO, Fresh_Bed_Class_Log.remark, Fresh_Room.Gender FROM         Fresh_Bed_Class_Log LEFT OUTER JOIN                      Fresh_Bed ON Fresh_Bed_Class_Log.FK_Bed_NO = Fresh_Bed.PK_Bed_NO LEFT OUTER JOIN                     Fresh_Room ON Fresh_Bed.FK_Room_NO = Fresh_Room.PK_Room_NO where Fresh_Room.Gender='"+xb+"' and FK_Class_NO='"+bjxx+"'");
            if(yzbj.Rows.Count>0)
            {

            }
            else
            {
                xzts.InnerHtml = "<font color=red>"+this.xsxx_bj.Text+"的"+xb+"生寝室已不足，暂时不能网上选择寝室，请联系您的辅导员！</font>";
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
                return;
            }
        }

    }
    protected void qsxz_Click(object sender, EventArgs e)
    {
        //获取床位ID和学号，存储到数据库 //获取员工编号
        try
        {
            string czy = xsxx_xh.Text;
            string pk_affair_no = this.pk_affair_no.Value;
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
                batch x = new batch();

                string create_name = null;
                if (Session["pk_sno"] != null)
                {
                    string session_pk_sno = Session["pk_sno"].ToString();
                    create_name = session_pk_sno.Trim() + ":" + Session["Name"].ToString().Trim();
                }
                if (Session["pk_staff_no"] != null)
                {
                    string session_pk_staff_no = Session["pk_staff_no"].ToString();
                    create_name = session_pk_staff_no.Trim() + ":" + Session["Name"].ToString().Trim();
                }
                x.set_affairlog(xsxx_xh.Text, pk_affair_no, tsxx.Split(',')[1], create_name);
                //x.set_affairlog(xsxx_xh.Text, pk_affair_no, tsxx.Split(',')[1], "system");

                //写入操作记录
                string urlok = HttpContext.Current.Request.Url.PathAndQuery;
                //Response.Write(urlok);
                Response.Redirect(urlok);
            }
            else
            {
                xzts.InnerHtml = "<font color=red>" + tsxx.Split(',')[1] + ",请重新选择！</font>";
            }
        }
        catch (Exception e1)
        {
            xzts.InnerHtml = "<font color=red>确认选择寝室时出错：" + e1.Message + "</font>";
        }


        //Response.Write(tsxx);


    }
    protected void qsxz_Click2(object sender, EventArgs e)
    {
        //Response.Redirect("/view/xszz-index.aspx?pk_sno="+xsxx_xh.Text);
        Response.Redirect("/view/xxzz_xsindex.aspx");

    }
}
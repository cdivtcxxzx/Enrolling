using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_xsjbxx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PK_Affair_NO  事务编号
        //PK_SNO        学生编号
        //PK_Staff_NO   操作员编号
        string PK_SNO = "2"; //post get 获取
        hidden_pk_sno.Value = PK_SNO;
        if (!IsPostBack)
        {
            #region 信息清空    
            //照片地址
            xszpxx.ImageUrl = "";
            //学号
            xsxx_xh.Text = PK_SNO;
            //姓名
            xsxx_xm.Text = "";
            //性别
            xsxx_xb.Text = "";
            //身份证
            xsxx_sfzh.Text = "";
            //学历层次
            xsxx_xlcc.Text = "";
            //学院
            xsxx_xy.Text = "";
            //专业 
            xsxx_zy.Text = "";
            //年级
            xsxx_nj.Text = "";
            //班级名称
            xsxx_bjmc.Text = "";
            //班主任
            xsxx_bzr.Text = "";
            //班主任电话
            xsxx_bzrdh.Text = "";
            #endregion
            
            

        }

    }
}
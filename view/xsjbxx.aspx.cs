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
        string PK_SNO = "2";
        string PK_Staff_NO = "chenzhiqiu";

        hidden_pk_sno.Value = PK_SNO;


    }
}
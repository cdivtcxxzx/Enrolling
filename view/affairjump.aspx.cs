using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nradmingl_affairjump : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string pk_sno = null;
        if (Session["username"] != null)
        {
            pk_sno = Session["username"].ToString().Trim();
        }
        else
        {
            Response.Write("<script>top.location.href='/login.aspx?sf=xs'</script>");
            return;
        }


        string pk_affair_index = Request.QueryString["pk_affair_index"];//获取事务索引
        if (pk_affair_index == null || pk_affair_index.Trim().Length == 0)
        {
            Response.Write("<script>window.location.href='xxzz_xsindex.aspx';</script>");
            return;
        }
        batch batch_logic = new batch();
        List<fresh_affair> affairlst_stu = batch_logic.get_freshstudent_affair_list(pk_sno);//学生事务 

        if (affairlst_stu != null)
        {
            for (int j = 0; affairlst_stu != null && j < affairlst_stu.Count; j++)
            {
                if (affairlst_stu[j].Affair_Index.ToString().Trim().Equals(pk_affair_index))
                {
                    fresh_oper oper = batch_logic.get_oper(affairlst_stu[j].PK_Affair_NO.Trim());
                    if (oper != null)
                    {
                        string url = oper.OPER_URL.Trim() + "?pk_affair_no=" + affairlst_stu[j].PK_Affair_NO.Trim() + "&pk_sno=" + pk_sno.Trim();
                        Response.Write("<script>window.location.href='"+url+"';</script>");
                        return;
                    }
                    else
                    {
                        Response.Write("<script>window.location.href='xxzz_xsindex.aspx';</script>");
                        return;
                    }
                }
            }
        }
        else
        {
            Response.Write("<script>window.location.href='xxzz_xsindex.aspx';</script>");
            return;
        }
    }
}
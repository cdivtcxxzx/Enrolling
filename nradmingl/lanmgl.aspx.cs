using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_lanmgl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            
        }
    }
    protected void lvLanm_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            int lmid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "lmid").ToString());
            SqlDataSource srcSencondLm = (SqlDataSource)e.Item.FindControl("srcLanm2");
            srcSencondLm.SelectParameters["getfid"].DefaultValue = lmid.ToString();
        }
    }
    protected void lvLanm2_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            int lmid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "lmid").ToString());
            SqlDataSource srcThirdLm = (SqlDataSource)e.Item.FindControl("srcLanm3");
            srcThirdLm.SelectParameters["secondfid"].DefaultValue = lmid.ToString();
        }
    }
    int NumLanm1 = 1;
    protected string GetNumLanm1()
    {
        return "1."+(NumLanm1++).ToString();
    }

    int NumLanm2 = 1;
    protected string GetNumLanm2()
    {
        return "2."+(NumLanm2++).ToString();
    }

    int NumLanm3 = 1;
    protected string GetNumLanm3()
    {
        return "3." + (NumLanm3++).ToString();
    }
    protected string GetPower(string lmyyqxs)
    {
        string groupPower ="";
        int countMax = 5;
        foreach (string lmyyqx in lmyyqxs.Split(','))
        {
            try
            {
                string SqlStrQuanx = "SELECT qxmc FROM quanxdm WHERE qxid=@qxid";
                DataTable dt = Sqlhelper.Serach(SqlStrQuanx, new SqlParameter("qxid", lmyyqx));
                if (dt.Rows.Count > 0 && countMax>=0)
                {
                    groupPower += dt.Rows[0]["qxmc"].ToString()+"|";
                    countMax--;
                }
            }
            catch
            {
                return groupPower;
            }
        }
        return groupPower;
    }
    protected string displayLanmStatus(string codeSFXS)
    {
        if (codeSFXS.Trim() == "0")
            return "ㄨ";
        else
            return "√";
    }

    protected string getColorLanmStatus(string codeSFXS)
    {
        if (codeSFXS.Trim() == "0")
            return "black";
        else
            return "red";
    }
}   
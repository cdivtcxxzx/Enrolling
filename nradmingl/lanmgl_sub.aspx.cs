using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_lanmgl_sub : System.Web.UI.Page
{
    public string fid = "";
    public string FID { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Request["fid"] != null)
            {
                fid = Request["fid"].ToString();
                this.FID = fid;
            }
            switch (fid)
            {
                case "163":
                    labDangQianWeiZhi.Text = "教学质量评价管理-栏目设置";
                    hrefAdd.PostBackUrl = "lanmadd_sub.aspx?action=add&fid=163";
                    break;
                case "164":
                    labDangQianWeiZhi.Text = "督导监测管理-栏目设置";
                    hrefAdd.PostBackUrl = "lanmadd_sub.aspx?action=add&fid=164";
                    break;
                case "165":
                    labDangQianWeiZhi.Text = "教学质量反馈-栏目设置";
                    hrefAdd.PostBackUrl = "lanmadd_sub.aspx?action=add&fid=165";
                    break;
                default: break;
            }
            srcLanm1.SelectParameters["fid"].DefaultValue = fid;
            srcLanm1.DataBind();
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
        //if (e.Item.ItemType == ListViewItemType.DataItem)
        //{
        //    int lmid = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "lmid").ToString());
        //    SqlDataSource srcThirdLm = (SqlDataSource)e.Item.FindControl("srcLanm3");
        //    srcThirdLm.SelectParameters["secondfid"].DefaultValue = lmid.ToString();
        //}
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
    protected void hrefAdd_Click(object sender, EventArgs e)
    {

    }
}   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class admin_lanmgl_xw : System.Web.UI.Page
{
    #region 页面初始化参数

    private string pagelm1 = "信息栏目管理";

    private string pageqx1 = "浏览";
    private string pageqx2 = "";
    private string pageqx3 = "";
    private string pageqx4 = "";
    private string pageqx5 = "";
    private string webpage = "lanmgl_xw.aspx";//页面值
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        new c_login().tongyiyz(pagelm1, pageqx1, "进入新闻栏目管理页", true, pageqx1, pageqx2, pageqx3, pageqx4, pageqx5);
        if (!IsPostBack)
        {
            
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        ListView _gridView = (ListView)sender;
        string id, sql;

        id = e.CommandArgument.ToString();




        if (e.CommandName == "删除")
        {
            //string xx = textbox.Text;

            if (Sqlhelper.ExcuteNonQuery("DELETE FROM xw_lanm  where lmid=" + id + " ") > 0)
            {
                //this.Label1.Text = "<font color=green> &nbsp;&nbsp;&nbsp;&nbsp;删除成功!</font>";
            }
            else
            {
                //this.Label1.Text = "<font color=red> &nbsp;&nbsp;&nbsp;&nbsp;删除失败,请重试!!</font>";
            }
        }
       // ViewState["gridsql"] = SqlDataSource1.SelectCommand;
       // SqlDataSource1.SelectCommand = ViewState["gridsql"].ToString();
        _gridView.DataBind();
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
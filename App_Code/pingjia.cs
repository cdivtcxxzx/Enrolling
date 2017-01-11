using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
///pingjia 的摘要说明
/// </summary>
public class pingjia
{
	public pingjia()
	{
		//
		//TODO: 关于评价表操作的一些基础类
		//
	}
    /// <summary>
    /// 通过评价历史表头和评价人得到所有的评价状态并分析后显示
    /// </summary>
    /// <param name="pjr">评价人</param>
    /// <param name="btid">历史表头ID</param>
    /// <returns>整体评价状态</returns>
    public static string getfkzt(string pjr, string btid)
    {
        if (pjr != "" && btid != "")
        {
            DataTable pjztb = Sqlhelper.Serach("SELECT pjzt FROM FKB_biao where btid='" + btid + "' and pjr='" + pjr + "'");
            string pjzt = "";
            string pjztretu = "<font color=red>未反馈</fong>";
            if (pjztb.Rows.Count > 0)
            {
                for (int i = 0; i < pjztb.Rows.Count; i++)
                {
                    pjzt += "|" + pjztb.Rows[i]["pjzt"].ToString();
                }

                if (pjzt.Contains("已反馈"))
                {
                    if (pjzt.Contains("|||"))
                    {
                        pjztretu = "<font color=red>部分反馈</font>";

                    }
                    else
                    {
                        pjztretu = "<font color=green>完成反馈</font>";
                    }
                }
                return pjztretu;
            }
            else
            {
                return "<font color=red>无反馈记录</font>";
            }
        }
        else
        {
            return "<font color=red>参数错误</font>";
        }
    }
    /// <summary>
    /// 通过评价历史表头和评价人得到所有的评价状态并分析后显示
    /// </summary>
    /// <param name="pjr">评价人</param>
    /// <param name="btid">历史表头ID</param>
    /// <returns>整体评价状态</returns>
    public static string getpjzt(string pjr, string btid)
    {
        if (pjr != "" && btid != "")
        {
            DataTable pjztb = Sqlhelper.Serach("SELECT pjzt FROM PJB_biao where btid='" + btid + "' and pjr='" + pjr + "'");
            string pjzt = "";
            string pjztretu = "<font color=red>未评价</fong>";
            if (pjztb.Rows.Count > 0)
            {
                for (int i = 0; i < pjztb.Rows.Count; i++)
                {
                    pjzt += "|"+pjztb.Rows[i]["pjzt"].ToString();
                }
                
                if (pjzt.Contains("已评价"))
                {
                    if (pjzt.Contains("|||"))
                    {
                        pjztretu = "<font color=red>部分评价</font>";
                
                    }
                    else
                    {
                        pjztretu = "<font color=green>完成评价</font>";
                    }
                    }
                return pjztretu;
            }
            else
            {
                return "<font color=red>无评价记录</font>";
            }
        }
        else
        {
            return "<font color=red>参数错误</font>";
        }
    }
    /// <summary>
    /// 通过评价历史表头和评价人得到所有的被评价人及历史流水ID
    /// </summary>
    /// <param name="pjr">评价人</param>
    /// <param name="btid">历史表头ID</param>
    /// <returns>被评价人及历史流水ID DATATABLE</returns>
    public static DataTable getpjr(string pjr, string btid)
    {
        DataTable pjrb = new DataTable();

        if (pjr != "" && btid != "")
        {
            pjrb = Sqlhelper.Serach("SELECT PJB_biao.time_start AS 开始时间, PJB_biao.time_end AS 结束时间, PJB_lsbiaotou.title AS 评价标题, PJB_lsbiaotou.zt AS 评价状态, DM_xueqi.name AS 学期, DM_zhouci.name AS 周次, PJB_biao.btid, PJB_biao.bpjr AS 教师,PJB_pjbls as 教师评价表ID, DM_YUANXI.YXMC AS 院系 FROM PJB_biao INNER JOIN PJB_lsbiaotou ON PJB_biao.btid = PJB_lsbiaotou.id INNER JOIN DM_xueqi ON PJB_lsbiaotou.xueq = DM_xueqi.id INNER JOIN DM_zhouci ON PJB_lsbiaotou.zhouc = DM_zhouci.id INNER JOIN DM_YUANXI ON PJB_biao.yxid = DM_YUANXI.YXDM WHERE (PJB_biao.pjr = '" + pjr + "' and  PJB_biao.btid=" + btid + " ) ORDER BY PJB_biao.pjbls ");
            return pjrb;
        }
        else
        {
            return pjrb;
        }
    }
    //pingjia.getpjxm(Eval("pjbls").ToString(), Eval("xmid").ToString(),Eval("评教等级或评分").ToString(),Eval("xmlx").ToString())
    /// <summary>
    /// 通过评价历史流水ID,及项目ID自动自成相对应的项目选项
    /// </summary>
    /// <param name="pjbls">评价表流水</param>
    /// <param name="xmid">项目表ID</param>
 /// <param name="pjxm">项目选项</param>
        /// <param name="xmlx">项目类型</param>
    /// <returns>相对应的项目选项</returns>
    public static Panel getpjxm(string pjbls, string xmid,string pjxm,string xmlx)
    {
       Panel panel1 = new Panel();
       string stringdata = pjxm;
        panel1.ID = "p" + pjbls + "_" + xmid;
        switch (xmlx)
        {
            case "单项选择项":
                RadioButtonList rekeys = new RadioButtonList();

                //rekeys.ID = "cb_keys";
                rekeys.ID = "radio111";
                rekeys.RepeatDirection = RepeatDirection.Horizontal;
                //rekeys.TextAlign = TextAlign.Left;
                ;
                rekeys.AppendDataBoundItems = true;
                if (stringdata.Length > 0)
                {

                    for (int i = 0; i < stringdata.Split('|').Length; i++)
                    {
                        if (stringdata.Split('|')[i].ToString() != "")
                        {
                            rekeys.Items.Add(new ListItem(stringdata.Split('|')[i].ToString() + "分", stringdata.Split('|')[i].ToString()));
                        }
                    }
                }


                panel1.Controls.Add(rekeys);
                break;
            case "多项选择项":
                CheckBoxList cbkeys = new CheckBoxList();
                cbkeys.ID = "checkbox111";
                cbkeys.RepeatDirection = RepeatDirection.Horizontal;
                //rekeys.TextAlign = TextAlign.Left;
                ;
                cbkeys.AppendDataBoundItems = true;
                if (stringdata.Length > 0)
                {

                    for (int i = 0; i < stringdata.Split('|').Length; i++)
                    {
                        if (stringdata.Split('|')[i].ToString() != "")
                        {
                            cbkeys.Items.Add(new ListItem(stringdata.Split('|')[i].ToString() + "分", stringdata.Split('|')[i].ToString()));
                        }
                    }
                }

                panel1.Controls.Add(cbkeys);
                break;

            case "固定下拉项":


                DropDownList drr = new DropDownList();
                drr.ID = "dropdown111";
                drr.AppendDataBoundItems = true;
                //System.Web.HttpContext.Current.Response.Write("stringdata");
                if (stringdata.Length > 0)
                {

                    for (int i = 0; i < stringdata.Split('|').Length; i++)
                    {
                        if (stringdata.Split('|')[i].ToString() != "")
                        {
                            drr.Items.Add(new ListItem(stringdata.Split('|')[i].ToString() + "分", stringdata.Split('|')[i].ToString()));
                        }
                    }
                }
                
                panel1.Controls.Add(drr);
                break;
            default:
                TextBox myTextBox = new TextBox();
                myTextBox.ID = "textbox111";
                //myTextBox.DataBinding += new EventHandler(this.TextBoxDataBinding);
                myTextBox.Width = Convert.ToInt32("120");
                //myTextBox.AutoPostBack = true;
                panel1.Controls.Add(myTextBox);
                break;

        }
                
        
        return panel1;
       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
public partial class apptjyx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.PanelManage.InnerHtml = "<table class=\"table\" style=\"margin-top:8px;\">    <thead>    <tr>	    <td colspan=\"4\" class=\"style1\"><br /><span id=\"cgts\" style=\"font-size:Medium;\">2016年新生报名信息统计</span> <br />  <br /> </td></tr> </thead>";
        if (Request["yxdm"] != null)
        {
            //显示数据
            DataTable jjxdata = Sqlhelper.Serach("SELECT     zsgl_zsjh.院系名称, zsgl_zsjh.专业名称, zsgl_zsjh.招生计划, zsgl_zsjh.已招人数, DM_ZY.zycc FROM         zsgl_zsjh LEFT OUTER JOIN                      DM_ZY ON zsgl_zsjh.学制 = DM_ZY.xz AND zsgl_zsjh.专业代码 = DM_ZY.zydm WHERE     (zsgl_zsjh.年度 = '" + basic.dqnd() + "') AND (zsgl_zsjh.院系代码 = '" + Request["yxdm"].ToString() + "') ORDER BY zsgl_zsjh.院系名称");
             if (jjxdata.Rows.Count > 0)
             {
                 this.PanelManage.InnerHtml = "<table class=\"table\" style=\"margin-top:8px;\">    <thead>    <tr>	    <td colspan=\"4\" class=\"style1\"><br /><span id=\"cgts\" style=\"font-size:Medium;text-align:center;\">" + basic.dqnd() + "年" + jjxdata.Rows[0][0].ToString() + "报名信息统计</span> <br />  <br /> </td></tr> </thead>";
                 this.PanelManage.InnerHtml += "<tr style=\"background-color:#E9FED3;height:40px;\" >   <td >专业名称</td>   <td >计划</td>  <td >已招</td><td >进度</td> </tr>";

                 for (int i = 0; i < jjxdata.Rows.Count; i++)
                 {

                     if ((i % 2) == 0)
                     {
                         this.PanelManage.InnerHtml += "<tr style=\"background-color:#f1f3f5;height:35px;\" bgcolor=\"#f1f3f5\">";

                     }
                     else
                     {
                         this.PanelManage.InnerHtml += "<tr style=\"background-color:#CFDBCE;height:35px;\" bgcolor=\"#CFDBCE\">";

                     }
                     string zym =  ""+jjxdata.Rows[i][1].ToString();
                     if (zym.Length > 10)
                     {
                         zym = zym.Substring(0, 10);
                     }
                     this.PanelManage.InnerHtml += "<td title='" + jjxdata.Rows[i][1].ToString() + "'>" + zym + "</td><td>" + jjxdata.Rows[i][2].ToString() + "</td><td>" + jjxdata.Rows[i][3].ToString() + "</td>";
                     
                      int x1 = 0;
                         int x2 = 0;
                         try
                         {
                             x1 =Convert.ToInt32( jjxdata.Rows[i][3].ToString());
                             x2 = Convert.ToInt32(jjxdata.Rows[i][2].ToString());
                         }
                         catch{}

                     if (x2 ==0)
                     {
                         this.PanelManage.InnerHtml += "<td>" +"0.00%"+"</td>";
                     }
                     else
                     {
                        
                       
                         this.PanelManage.InnerHtml += "<td>"+((float)x1 / x2 * 100).ToString("0.00") + "%"+"</td>";
                     }

                     this.PanelManage.InnerHtml += "</tr>";
                     
                 }
                 
                 this.PanelManage.InnerHtml += "<tr > <td style=\"text-align:center\" onmousedown=\"location.href='javascript:history.go(-1);'\" colspan=\"4\" >  <br /><a href='javascript:history.go(-1);'>返回</a><br /><br /> </td>  </tr></table>";
             }
        }


    }
}
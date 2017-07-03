
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebReference;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

public partial class admin_apptjtb : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //统计数据
        DataTable tjtime = Sqlhelper.Serach("SELECT 统计时间,专业代码 FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' order by 院系名称");
        if (tjtime.Rows.Count > 0)
        {
            try
            {
                string tjtime1 = tjtime.Rows[0][0].ToString();
                DateTime time2 = DateTime.Parse(tjtime1);
                if (DateTime.Now.Minute - time2.Minute >= 5 || time2.Minute - DateTime.Now.Minute >= 5)
                {
                    //更新学生的报名状态，查询缴费情况
                    DataTable xssj = Sqlhelper.Serach("SELECT [ID],xszt from  [XSJCSJ] where xszt='2' and nj='"+basic.dqnd()+"'");
                    if (xssj.Rows.Count > 0)
                    {
                        for (int y = 0; y < xssj.Rows.Count; y++)
                        {
                            string bmzt1 = bmzt(xssj.Rows[y][0].ToString());
                            string id = xssj.Rows[y][0].ToString();
                            //Response.Write(bmzt1 + "<br>");
                            DataTable fyqk = new WebReference.WebService().GetFeeDetail(basic.dqnd(), id);
                            if (fyqk.Rows.Count > 0)
                            {

                                //将学生状态置为已报名

                                Student stu = new Student();
                                stu.id = id;
                                stu.xszt = "3";
                                string temp = JsonConvert.SerializeObject(stu);
                                if (new WebReference.WebService().UpdateStu("zsgl", "189287EE-01F8-42AF-A084-9FD2697006BE", temp, "系统自动更新状态为报名"))
                                {
                                    //return "<a href=\"printtzsok.aspx?bh=" + bmzt1 + "\" target=\"_blank\" >打印通知书</a>";
                                }



                                //string cs = new WebReference.WebService()..CreateStu("zsgl", "189287EE-01F8-42AF-A084-9FD2697006BE", "", "");
                            }

                        }

                    }


                    for (int i = 0; i < tjtime.Rows.Count; i++)
                    {
                        //已招总数
                        string yzzs = "";
                        DataTable tjok = Sqlhelper.Serach("SELECT count(id) as zszs,count(case when xb='男'   then xb else null end) as xblan,count(case when xb='女'   then xb else null end) as xbnv,count(case when jdfs='住校'   then jdfs else null end) as zhux,count(case when jdfs='走读'   then jdfs else null end) as zhoud,count(case when xszt='2'   then xszt else null end) as wsh,count(case when xszt='3'   then xszt else null end) as ysh from xsjcsj where nj='"+basic.dqnd()+"' and  ZYDM='" + tjtime.Rows[i][1].ToString() + "' and (xszt='2' or xszt='3') group by yxdm");
                        if (tjok.Rows.Count > 0)
                        {
                            //更新统计时间,及人数
                            yzzs = tjok.Rows[0][0].ToString();
                            Sqlhelper.ExcuteNonQuery("UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "',男生人数='" + tjok.Rows[0][1].ToString() + "',女生人数='" + tjok.Rows[0][2].ToString() + "',住校='" + tjok.Rows[0][3].ToString() + "',走读='" + tjok.Rows[0][4].ToString() + "',未收费='" + tjok.Rows[0][5].ToString() + "',已收费='" + tjok.Rows[0][6].ToString() + "',已招人数='" + tjok.Rows[0][0].ToString() + "'  WHERE  年度='"+basic.dqnd()+"' and 专业代码='" + tjtime.Rows[i][1].ToString() + "' ");
                        }
                        else
                        {
                            Sqlhelper.ExcuteNonQuery("UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "',男生人数='0',女生人数='0',住校='0',走读='0',未收费='0',已收费='0',已招人数='0'  WHERE  年度='"+basic.dqnd()+"' and 专业代码='" + tjtime.Rows[i][1].ToString() + "' ");

                        }


                    }


                }
            }
            catch
            {
                //第一次，更新
                Sqlhelper.ExcuteNonQuery("UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "'  WHERE  年度='"+basic.dqnd()+"' ");
            }
        }
        else
        {
            Sqlhelper.ExcuteNonQuery("UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "'  WHERE  年度='"+basic.dqnd()+"' ");
        }


        //输出
        this.PanelManage.InnerHtml = "<table id=\"datatj\" class=\"table\" style=\"margin-top:8px;\">    <thead>    <tr>	    <td colspan=\"10\" style=\"text-align:center;\" class=\"style1\"><br /><span id=\"cgts\" style=\"font-size:Medium;text-align:center;\">" + basic.dqnd() + "年新生报名信息按系统计</span> <br />  <br /> </td></tr> </thead>";
        this.PanelManage.InnerHtml += "<tr style=\"background-color:#acd9f8;height:40px;\" >  <td >院系名称</td>  <td >计划人数</td>	    <td >            已招总数</td>	    <td >            招生进度</td>	    <td >            男生</td>	    <td >            女生</td>	    <td >住校</td><td >走读</td> <td >未收费</td> <td > 已收费</td> </tr>";
        this.Div1.InnerHtml = "<table id=\"datatj2\" class=\"table\" style=\"margin-top:8px;\"> ";
        this.Div1.InnerHtml += "<tr style=\"background-color:#acd9f8;height:40px;\" >  <td >院系名称</td>   <td >            已招总数</td>	  </tr>";



        int tjz1 = 0;
        int tjz2 = 0;
        int tjz3 = 0;
        int tjz4 = 0;
        int tjz5 = 0;
        int tjz6 = 0;
        int tjz7 = 0;
        int tjz8 = 0;
        string bfbz = "0.00%";
                DataTable dt = new DataTable();
        dt.Columns.Add("院系", typeof(String));
        dt.Columns.Add("招生总数", typeof(String));
        //获取系部
        DataTable xibu = Sqlhelper.Serach("select distinct 院系名称 FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' order by 院系名称");
        if (xibu.Rows.Count > 0)
        {

            for (int y = 0; y < xibu.Rows.Count; y++)
            {
                try
                {
                    DataTable jjxdata = Sqlhelper.Serach("SELECT [院系名称],[招生计划],[已招人数],[男生人数],[女生人数],[住校],[走读],[未收费],[已收费],[院系代码],[统计时间] FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系名称='" + xibu.Rows[y][0].ToString() + "'  order by 院系名称");
                    if (jjxdata.Rows.Count > 0)
                    {
                        this.tjtime.Text = "最后统计时间：" + jjxdata.Rows[0][10].ToString();
                        if ((y % 2) == 0)
                        {
                            this.PanelManage.InnerHtml += "<tr style=\"background-color:#f1f3f5;height:35px;\" bgcolor=\"#f1f3f5\">";

                        }
                        else
                        {
                            this.PanelManage.InnerHtml += "<tr style=\"background-color:#CFDBCE;height:35px;\" bgcolor=\"#CFDBCE\">";

                        }
                        this.Div1.InnerHtml += "<tr style=\"background-color:#CFDBCE;height:35px;\" bgcolor=\"#CFDBCE\">";
                        int tj1 = 0;
                        int tj2 = 0;
                        int tj3 = 0;
                        int tj4 = 0;
                        int tj5 = 0;
                        int tj6 = 0;
                        int tj7 = 0;
                        int tj8 = 0;
                        string bfb = "0.00%";

                        for (int i = 0; i < jjxdata.Rows.Count; i++)
                        {
                            try
                            {
                                tj1 = (tj1 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                                tj2 = (tj2 + Convert.ToInt32(jjxdata.Rows[i][2].ToString()));
                                tj3 = (tj3 + Convert.ToInt32(jjxdata.Rows[i][3].ToString()));
                                tj4 = (tj4 + Convert.ToInt32(jjxdata.Rows[i][4].ToString()));
                                tj5 = (tj5 + Convert.ToInt32(jjxdata.Rows[i][5].ToString()));//住校
                                tj6 = (tj6 + Convert.ToInt32(jjxdata.Rows[i][6].ToString()));//走读
                                tj7 = (tj7 + Convert.ToInt32(jjxdata.Rows[i][7].ToString()));//未收
                                tj8 = (tj8 + Convert.ToInt32(jjxdata.Rows[i][8].ToString()));//已收
                                tjz1 = (tjz1 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                                tjz2 = (tjz2 + Convert.ToInt32(jjxdata.Rows[i][2].ToString()));
                                tjz3 = (tjz3 + Convert.ToInt32(jjxdata.Rows[i][3].ToString()));
                                tjz4 = (tjz4 + Convert.ToInt32(jjxdata.Rows[i][4].ToString()));
                                tjz5 = (tjz5 + Convert.ToInt32(jjxdata.Rows[i][5].ToString()));//住校
                                tjz6 = (tjz6 + Convert.ToInt32(jjxdata.Rows[i][6].ToString()));//走读
                                tjz7 = (tjz7 + Convert.ToInt32(jjxdata.Rows[i][7].ToString()));//未收
                                tjz8 = (tjz8 + Convert.ToInt32(jjxdata.Rows[i][8].ToString()));//已收

                            }
                            catch
                            {
                            }

                        }

                        int x1 = Convert.ToInt32(tj1.ToString());
                        int x2 = Convert.ToInt32(tj2.ToString());
                        if (x2 == 0)
                        {
                            //输出百分比
                            bfb = "0.00%";
                        }
                        else
                        {
                            //输出百分比
                            bfb = ((float)x2 / x1 * 100).ToString("0.00") + "%";
                        }



                        this.PanelManage.InnerHtml += "<td style=\"cursor:pointer\"  onmousedown=\"location.href='tongjyx1.aspx?yxdm=" + jjxdata.Rows[0][9].ToString() + "'\">" + jjxdata.Rows[0][0].ToString() + "</td><td>" + tj1 + "</td><td>" + tj2 + "</td> <td>" + bfb + "</td><td>" + tj3 + "</td><td>" + tj4 + "</td><td>" + tj5 + "</td><td>" + tj6 + "</td><td>" + tj7 + "</td><td>" + tj8 + "</td></tr>";

                        this.Div1.InnerHtml += "<td>" + jjxdata.Rows[0][0].ToString() + "</td><td>" + tj2 + "</td></tr>";
                         DataRow dr = dt.NewRow();
                        dr[0] = jjxdata.Rows[0][0].ToString();

                        dr[1] = tj2;
                        dt.Rows.Add(dr);
                    }
                }
                catch
                { }
            }

        }
        //获取计划数
        int xz1 = Convert.ToInt32(tjz1.ToString());
        int xz2 = Convert.ToInt32(tjz2.ToString());
        if (xz2 == 0)
        {
            //输出百分比
            bfbz = "0.00%";
        }
        else
        {
            //输出百分比
            bfbz = ((float)xz2 / xz1 * 100).ToString("0.00") + "%";
        }
        this.PanelManage.InnerHtml += "<tr  style=\"background-color:#acd9f8;height:40px;\"><td >全校总计</td><td>" + tjz1 + "</td><td>" + tjz2 + "</td> <td>" + bfbz + "</td><td>" + tjz3 + "</td><td>" + tjz4 + "</td><td>" + tjz5 + "</td><td>" + tjz6 + "</td><td>" + tjz7 + "</td><td>" + tjz8 + "</td></tr>";

        this.PanelManage.InnerHtml += "<tr > <td style=\"text-align:center\"  colspan=\"10\" >  <br />点击院系名称可查看详细数据,滚动条往下拉可看到详细统计图表<br /><br /> </td>  </tr></table>";

        this.Div1.InnerHtml += "</table>";


        //显示图表
 if(dt.Rows.Count>0)
               {

        this.jsshow.InnerHtml = " <script>";
        this.jsshow.InnerHtml += "$(document).ready(function () { ";
   this.jsshow.InnerHtml += "$(function () {    $('#container').highcharts({        chart: {            type: 'column',            margin: [ 50, 50, 100, 80]        },";
   this.jsshow.InnerHtml += "  title: {           text: '" + basic.dqnd() + "年全校各系招生情况统计表'        },        xAxis: {            categories: [";

              for(int y1=0;y1<dt.Rows.Count;y1++)
              {
                  if(y1==0)
                  {
                  this.jsshow.InnerHtml +="'"+dt.Rows[y1][0].ToString()+"'";
                  }
                  else
                  {
                      this.jsshow.InnerHtml += ",'" + dt.Rows[y1][0].ToString() + "'";
                  }
              }
               
                       this.jsshow.InnerHtml += "            ],            labels: {                rotation: -45,                align: 'right',                style: {                    fontSize: '13px',          fontFamily: 'Verdana, sans-serif'                }            }        },";
           this.jsshow.InnerHtml += "        yAxis: {            min: 0,            title: {                text: '招生人数'            }        },        legend: {            enabled: false        },        tooltip: {            pointFormat: '招生人数: <b>{point.y:.1f} 人</b>',        },";
           this.jsshow.InnerHtml += "        series: [{            name: 'Population',            data: [";
           for (int y1 = 0; y1 < dt.Rows.Count; y1++)
           {
               if (y1 == 0)
               {
                   this.jsshow.InnerHtml += dt.Rows[y1][1].ToString();
               }
               else
               {
                   this.jsshow.InnerHtml += "," + dt.Rows[y1][1].ToString();
               }
           }
               
           this.jsshow.InnerHtml += "],            dataLabels: {                enabled: true,                rotation: -90,                color: '#FFFFFF',           align: 'right',    x: 4, y: 10,   style: {  fontSize: '13px',";
           this.jsshow.InnerHtml += "fontFamily: 'Verdana, sans-serif',   textShadow: '0 0 3px black'     }    }    }]    });});";
   this.jsshow.InnerHtml +=" });</script>		<script type='text/javascript' src='js2/data.js'></script>";

 }



        //显示饼图
        //this.jsshow.InnerHtml += " <script>  $(document).ready(function () { $('#container1').highcharts({chart: {plotBackgroundColor: null,plotBorderWidth: null,plotShadow: false },                title: {                    text: ''                },";
        //this.jsshow.InnerHtml += "tooltip: {                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b><br>招生人数:<b>{point.y}人</b>'                },                plotOptions: {                    pie: {                        allowPointSelect: true,                        cursor: 'pointer',                        dataLabels: {                            enabled: true,";
        //this.jsshow.InnerHtml += "color: '#000000',                          connectorColor: '#000000',                            formatter: function () {                                return '<a href=javascript:showurl(\"swtjxq.aspx?area='+this.point.name+'\")><b>' + this.point.name + '</b></a>: ' + Highcharts.numberFormat(this.percentage, 1) + ' %';";
        //this.jsshow.InnerHtml += "}                        }                    }                },                series: [{                    type: 'pie',                    name: '所占比例',               data: [       ";
        //DataTable dt = Sqlhelper.Serach("select b.yxmc 院系,COUNT(*) 人数 from XSJCSJ a left join DM_YUANXI b on a.YXDM=b.yxdm where NJ=2016 and XSZT!=1 and xszt!=0 and a.ZYDM!='' group by b.yxmc");

        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    if (i == 0)
        //    {
        //        //{                    name: '二圈层',y: 28.7,                    sliced: true,                    selected: true                } 
        //        this.jsshow.InnerHtml += "{name: '" + dt.Rows[i][0].ToString() + "',y:" + dt.Rows[i][1].ToString() + ",sliced: true,selected: true} ";
        //    }
        //    else
        //    {
        //        this.jsshow.InnerHtml += ",['" + dt.Rows[i][0].ToString() + "', " + dt.Rows[i][1].ToString() + "]";
        //    }
        //}
        //// this.jsshow.InnerHtml +="              ['三圈层', 45.0],                ['一圈层', 26.3],                {                    name: '二圈层',y: 28.7,                    sliced: true,                    selected: true                }  ";
        //this.jsshow.InnerHtml += " ] }]            });";
        //this.jsshow.InnerHtml += "  });</script>	";



    }
    protected string bmzt(string bh)
    {
        //查询是否缴费
        //返回结果

        //string cs = new WebReference.WebService().CreateStu("zsgl", "189287EE-01F8-42AF-A084-9FD2697006BE", "", "");
        DataTable fyqk = new WebReference.WebService().GetFeeDetail(basic.dqnd(), bh);
        if (fyqk.Rows.Count > 0)
        {

            //将学生状态置为已报名
            Student stu = new Student();
            stu.id = bh;
            stu.xszt = "3";
            string temp = JsonConvert.SerializeObject(stu);
            if (new WebReference.WebService().UpdateStu("zsgl", "189287EE-01F8-42AF-A084-9FD2697006BE", temp, ""))
            {
                return "OK";
            }
            else
            {
                return "no";
            }



            //string cs = new WebReference.WebService()..CreateStu("zsgl", "189287EE-01F8-42AF-A084-9FD2697006BE", "", "");
        }
        else
        {
            return "no";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //更新学生的报名状态，查询缴费情况
        DataTable xssj = Sqlhelper.Serach("SELECT [ID],xszt from  [XSJCSJ] where xszt='2' and nj='"+basic.dqnd()+"'");
        if (xssj.Rows.Count > 0)
        {
            for (int y = 0; y < xssj.Rows.Count; y++)
            {
                string bmzt1 = bmzt(xssj.Rows[y][0].ToString());
                string id = xssj.Rows[y][0].ToString();
                //Response.Write(bmzt1 + "<br>");
                DataTable fyqk = new WebReference.WebService().GetFeeDetail(basic.dqnd(), id);
                if (fyqk.Rows.Count > 0)
                {

                    //将学生状态置为已报名

                    Student stu = new Student();
                    stu.id = id;
                    stu.xszt = "3";
                    string temp = JsonConvert.SerializeObject(stu);
                    if (new WebReference.WebService().UpdateStu("zsgl", "189287EE-01F8-42AF-A084-9FD2697006BE", temp, "系统自动更新状态为报名"))
                    {
                        //return "<a href=\"printtzsok.aspx?bh=" + bmzt1 + "\" target=\"_blank\" >打印通知书</a>";
                    }



                    //string cs = new WebReference.WebService()..CreateStu("zsgl", "189287EE-01F8-42AF-A084-9FD2697006BE", "", "");
                }

            }

        }



        //统计数据
        DataTable tjtime = Sqlhelper.Serach("SELECT 统计时间,专业代码 FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' order by 院系名称");
        if (tjtime.Rows.Count > 0)
        {
            try
            {

                this.tjtime.Text = "最后统计时间：" + DateTime.Now.ToString();

                for (int i = 0; i < tjtime.Rows.Count; i++)
                {
                    //已招总数
                    string yzzs = "";
                    DataTable tjok = Sqlhelper.Serach("SELECT count(id) as zszs,count(case when xb='男'   then xb else null end) as xblan,count(case when xb='女'   then xb else null end) as xbnv,count(case when jdfs='住校'   then jdfs else null end) as zhux,count(case when jdfs='走读'   then jdfs else null end) as zhoud,count(case when xszt='2'   then xszt else null end) as wsh,count(case when xszt='3'   then xszt else null end) as ysh from xsjcsj where nj='"+basic.dqnd()+"' and  ZYDM='" + tjtime.Rows[i][1].ToString() + "' and (xszt='2' or xszt='3') group by yxdm");
                    if (tjok.Rows.Count > 0)
                    {
                        //更新统计时间,及人数
                        //yzzs = tjok.Rows[0][0].ToString();
                        Sqlhelper.ExcuteNonQuery("UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "',男生人数='" + tjok.Rows[0][1].ToString() + "',女生人数='" + tjok.Rows[0][2].ToString() + "',住校='" + tjok.Rows[0][3].ToString() + "',走读='" + tjok.Rows[0][4].ToString() + "',未收费='" + tjok.Rows[0][5].ToString() + "',已收费='" + tjok.Rows[0][6].ToString() + "',已招人数='" + tjok.Rows[0][0].ToString() + "'  WHERE  年度='"+basic.dqnd()+"' and 专业代码='" + tjtime.Rows[i][1].ToString() + "' ");
                    }
                    else
                    {
                        Sqlhelper.ExcuteNonQuery("UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "'  WHERE  年度='"+basic.dqnd()+"' and 专业代码='" + tjtime.Rows[i][1].ToString() + "' ");

                    }






                }
            }
            catch
            {
                //第一次，更新
                Sqlhelper.ExcuteNonQuery("UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "'  WHERE  年度='"+basic.dqnd()+"' ");
            }
        }
        else
        {
            Sqlhelper.ExcuteNonQuery("UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "'  WHERE  年度='"+basic.dqnd()+"' ");
        }
    }
}
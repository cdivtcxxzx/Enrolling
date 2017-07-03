using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using WebReference;
using Newtonsoft.Json;

public partial class admin_apptj : System.Web.UI.Page
{
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //统计数据
        DataTable tjtime = Sqlhelper.Serach("SELECT 统计时间,专业代码 FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' order by 院系名称");
        //if (tjtime.Rows.Count > 0)
        //{
        //    try
        //    {
        //        string tjtime1 = tjtime.Rows[0][0].ToString();
        //        DateTime time2 = DateTime.Parse(tjtime1);
        //        if (DateTime.Now.Minute - time2.Minute >= 15 || time2.Minute-DateTime.Now.Minute>=15 )
        //        {
        //            //更新学生的报名状态，查询缴费情况
        //            DataTable xssj = Sqlhelper.Serach("SELECT [ID],xszt from  [XSJCSJ] where xszt='2' and nj='"+basic.dqnd()+"'");
        //            if (xssj.Rows.Count > 0)
        //            {
        //                for (int y = 0; y < xssj.Rows.Count; y++)
        //                {
        //                    string bmzt1 = bmzt(xssj.Rows[y][0].ToString());
        //                    string id = xssj.Rows[y][0].ToString();
        //                    //Response.Write(bmzt1 + "<br>");
        //                    DataTable fyqk = new WebReference.WebService().GetFeeDetail(basic.dqnd(), id);
        //                    if (fyqk.Rows.Count > 0)
        //                    {

        //                        //将学生状态置为已报名

        //                        Student stu = new Student();
        //                        stu.id = id;
        //                        stu.xszt = "3";
        //                        string temp = JsonConvert.SerializeObject(stu);
        //                        if (new WebReference.WebService().UpdateStu("zsgl", "189287EE-01F8-42AF-A084-9FD2697006BE", temp, "系统自动更新状态为报名"))
        //                        {
        //                            //return "<a href=\"printtzsok.aspx?bh=" + bmzt1 + "\" target=\"_blank\" >打印通知书</a>";
        //                        }



        //                        //string cs = new WebReference.WebService()..CreateStu("zsgl", "189287EE-01F8-42AF-A084-9FD2697006BE", "", "");
        //                    }

        //                }

        //            }

        //            //for (int i = 0; i < tjtime.Rows.Count; i++)
        //            //{
        //            //    DataTable tjok = Sqlhelper.Serach(" SELECT count([ID]) as tjjg  FROM [XSJCSJ] where nj='"+basic.dqnd()+"' and  ZYDM='" + tjtime.Rows[i][1].ToString() + "' and (xszt='2' or xszt='3') and zgz='无'");
        //            //    if (tjok.Rows.Count > 0)
        //            //    {
        //            //        string x = tjtime.Rows[i][1].ToString();

        //            //       // if (tjtime.Rows[i][1].ToString() == "010208280003") Response.Write("SELECT count([ID]) as tjjg  FROM [XSJCSJ] where nj='"+basic.dqnd()+"' and  ZYDM='" + tjtime.Rows[i][1].ToString() + "' and (xszt='2' or xszt='3')"+"UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "',已招人数='" + tjok.Rows[0][0].ToString() + "'  WHERE  年度='"+basic.dqnd()+"' and 专业代码='" + tjtime.Rows[i][1].ToString() + "' ");
        //            //        //更新统计时间,及人数
        //            //        string xx = tjok.Rows[0][0].ToString();
        //            //        //Sqlhelper.ExcuteNonQuery("UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "',已招人数='" + tjok.Rows[0][0].ToString() + "'  WHERE  年度='"+basic.dqnd()+"' and 专业代码='" + tjtime.Rows[i][1].ToString() + "' ");
        //            //    }
        //            //}
                 
                    
        //        }
        //    }
        //    catch
        //    {
        //    //第一次，更新
        //       // Sqlhelper.ExcuteNonQuery("UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "'  WHERE  年度='"+basic.dqnd()+"' ");
        //    }
        //}
        //else
        //{
        //    //Sqlhelper.ExcuteNonQuery("UPDATE [zsgl_zsjh]   SET [统计时间] ='" + DateTime.Now.ToString() + "'  WHERE  年度='"+basic.dqnd()+"' ");
        //}




        //获取计划数
        try
        {
            DataTable jjxdata = Sqlhelper.Serach("SELECT [院系名称],[招生计划],[已招人数],[院系代码],[专业代码] FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系名称='机械制造系' order by 院系名称");
            if (jjxdata.Rows.Count > 0)
            {
                int tj1 = 0;
                int tj2 = 0;
                int tj3 = 0;
                int tj4 = 0;
                for (int i = 0; i < jjxdata.Rows.Count; i++)
                {
                    if (jjxdata.Rows[i]["专业代码"].ToString().Length < 8)
                    {
                        tj3 = (tj3 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }
                    else
                    {
                        tj1 = (tj1 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }

                }
                DataTable gzzssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='高职' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (gzzssl.Rows.Count > 0) tj4 = Convert.ToInt32(gzzssl.Rows[0][0].ToString());
                int x3 = Convert.ToInt32(tj3);
                int x4 = Convert.ToInt32(tj4);
                string bfb = "0.00%";
                if (tj3.ToString() != "0")
                {
                    bfb = ((float)x4 / x3 * 100).ToString("0.00") + "%";
                }
                jjx2.Text = tj3.ToString();
                jjx3.Text = tj4.ToString();
                jjx4.Text = bfb;


                DataTable zssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='中职' and zgz='无' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (zssl.Rows.Count > 0) tj2 = Convert.ToInt32(zssl.Rows[0][0].ToString());
                jjx.Text = tj1.ToString();
                jjx0.Text = tj2.ToString();
                int x1 = Convert.ToInt32(jjx0.Text);
                int x2 = Convert.ToInt32(jjx.Text);
                if (jjx.Text == "0")
                {
                    jjx1.Text = "0.00%";
                }
                else
                {
                    jjx1.Text = ((float)x1 / x2 * 100).ToString("0.00") + "%";
                }
            }
        }
        catch
        { }
        try
        {
            DataTable jjxdata = Sqlhelper.Serach("SELECT [院系名称],[招生计划],[已招人数],[院系代码],[专业代码] FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系名称='汽车工程系' order by 院系名称");
            if (jjxdata.Rows.Count > 0)
            {
                int tj1 = 0;
                int tj2 = 0;
                int tj3 = 0;
                int tj4 = 0;
                for (int i = 0; i < jjxdata.Rows.Count; i++)
                {
                    if (jjxdata.Rows[i]["专业代码"].ToString().Length < 8)
                    {
                        tj3 = (tj3 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }
                    else
                    {
                        tj1 = (tj1 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }

                }
                DataTable gzzssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='高职' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (gzzssl.Rows.Count > 0) tj4 = Convert.ToInt32(gzzssl.Rows[0][0].ToString());
                int x3 = Convert.ToInt32(tj3);
                int x4 = Convert.ToInt32(tj4);
                string bfb = "0.00%";
                if (tj3.ToString() != "0")
                {
                    bfb = ((float)x4/x3 * 100).ToString("0.00") + "%";
                }
                qcx2.Text = tj3.ToString();
                qcx3.Text = tj4.ToString();
                qcx4.Text = bfb;
                DataTable zssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='中职' and zgz='无' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (zssl.Rows.Count > 0) tj2 = Convert.ToInt32(zssl.Rows[0][0].ToString());
                qcx.Text = tj1.ToString();
                qcx0.Text = tj2.ToString();
                
                int x1 = Convert.ToInt32(qcx0.Text);
                int x2 = Convert.ToInt32(qcx.Text);
                if (qcx.Text == "0")
                {
                    qcx1.Text = "0.00%";
                }
                else
                {
                    qcx1.Text = ((float)x1 / x2 * 100).ToString("0.00") + "%";
                }
            }
        }
        catch
        { }

        try
        {
            DataTable jjxdata = Sqlhelper.Serach("SELECT [院系名称],[招生计划],[已招人数],[院系代码],[专业代码] FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系名称='交通运输系' order by 院系名称");
            if (jjxdata.Rows.Count > 0)
            {
                int tj1 = 0;
                int tj2 = 0;
                int tj3 = 0;
                int tj4 = 0;
                for (int i = 0; i < jjxdata.Rows.Count; i++)
                {
                    if (jjxdata.Rows[i]["专业代码"].ToString().Length < 8)
                    {
                        tj3 = (tj3 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }
                    else
                    {
                        tj1 = (tj1 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }

                }
                DataTable gzzssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='高职' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (gzzssl.Rows.Count > 0) tj4 = Convert.ToInt32(gzzssl.Rows[0][0].ToString());
                int x3 = Convert.ToInt32(tj3);
                int x4 = Convert.ToInt32(tj4);
                string bfb = "0.00%";
                if (tj3.ToString() != "0")
                {
                    bfb = ((float)x4/x3 * 100).ToString("0.00") + "%";
                }
                jyx2.Text = tj3.ToString();
                jyx3.Text = tj4.ToString();
                jyx4.Text = bfb;
                DataTable zssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='中职' and zgz='无' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (zssl.Rows.Count > 0) tj2 = Convert.ToInt32(zssl.Rows[0][0].ToString());
                jyx.Text = tj1.ToString();
                jyx0.Text = tj2.ToString();

                
                //jyx0.Text = jjxdata.Rows[0][2].ToString();
                int x1 = Convert.ToInt32(jyx0.Text);
                int x2 = Convert.ToInt32(jyx.Text);
                if (jyx.Text == "0")
                {
                    jyx1.Text = "0.00%";
                }
                else
                {
                    jyx1.Text = ((float)x1 / x2 * 100).ToString("0.00") + "%";
                }
            }
        }
        catch
        { }
        try
        {
            DataTable jjxdata = Sqlhelper.Serach("SELECT [院系名称],[招生计划],[已招人数],[院系代码],[专业代码] FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系名称='物流工程系' order by 院系名称");
            if (jjxdata.Rows.Count > 0)
            {
                int tj1 = 0;
                int tj2 = 0;
                int tj3 = 0;
                int tj4 = 0;
                for (int i = 0; i < jjxdata.Rows.Count; i++)
                {
                    if (jjxdata.Rows[i]["专业代码"].ToString().Length < 8)
                    {
                        tj3 = (tj3 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }
                    else
                    {
                        tj1 = (tj1 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }

                }
                DataTable gzzssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='高职' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (gzzssl.Rows.Count > 0) tj4 = Convert.ToInt32(gzzssl.Rows[0][0].ToString());
                int x3 = Convert.ToInt32(tj3);
                int x4 = Convert.ToInt32(tj4);
                string bfb = "0.00%";
                if (tj3.ToString() != "0")
                {
                    bfb = ((float)x4/x3 * 100).ToString("0.00") + "%";
                }
                wlx2.Text = tj3.ToString();
                wlx3.Text = tj4.ToString();
                wlx4.Text = bfb;
                DataTable zssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='中职' and zgz='无' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (zssl.Rows.Count > 0) tj2 = Convert.ToInt32(zssl.Rows[0][0].ToString());
                wlx.Text = tj1.ToString();
                wlx0.Text = tj2.ToString();
                
                int x1 = Convert.ToInt32(wlx0.Text);
                int x2 = Convert.ToInt32(wlx.Text);
                if (wlx.Text == "0")
                {
                    wlx1.Text = "0.00%";
                }
                else
                {
                    wlx1.Text = ((float)x1 / x2 * 100).ToString("0.00") + "%";
                }
            }
        }
        catch
        { }

        try
        {
            DataTable jjxdata = Sqlhelper.Serach("SELECT [院系名称],[招生计划],[已招人数],[院系代码],[专业代码] FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系名称='土木工程系' order by 院系名称");
            if (jjxdata.Rows.Count > 0)
            {
                int tj1 = 0;
                int tj2 = 0;
                int tj3 = 0;
                int tj4 = 0;
                for (int i = 0; i < jjxdata.Rows.Count; i++)
                {
                    if (jjxdata.Rows[i]["专业代码"].ToString().Length < 8)
                    {
                        tj3 = (tj3 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }
                    else
                    {
                        tj1 = (tj1 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }

                }
                DataTable gzzssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='高职' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (gzzssl.Rows.Count > 0) tj4 = Convert.ToInt32(gzzssl.Rows[0][0].ToString());
                int x3 = Convert.ToInt32(tj3);
                int x4 = Convert.ToInt32(tj4);
                string bfb = "0.00%";
                if (tj3.ToString() != "0")
                {
                    bfb = ((float)x4/x3 * 100).ToString("0.00") + "%";
                }
                tmx2.Text = tj3.ToString();
                tmx3.Text = tj4.ToString();
                tmx4.Text = bfb;
                DataTable zssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='中职' and zgz='无' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (zssl.Rows.Count > 0) tj2 = Convert.ToInt32(zssl.Rows[0][0].ToString());
                tmx.Text = tj1.ToString();
                tmx0.Text = tj2.ToString();
  
               
                int x1 = Convert.ToInt32(tmx0.Text);
                int x2 = Convert.ToInt32(tmx.Text);
                if (tmx.Text == "0")
                {
                    tmx1.Text = "0.00%";
                }
                else
                {
                    tmx1.Text = ((float)x1 / x2 * 100).ToString("0.00") + "%";
                }
            }
        }
        catch
        { }


        try
        {
            DataTable jjxdata = Sqlhelper.Serach("SELECT [院系名称],[招生计划],[已招人数],[院系代码],[专业代码] FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系名称='财经商贸系' order by 院系名称");
            if (jjxdata.Rows.Count > 0)
            {
                int tj1 = 0;
                int tj2 = 0;
                int tj3 = 0;
                int tj4 = 0;
                for (int i = 0; i < jjxdata.Rows.Count; i++)
                {
                    if (jjxdata.Rows[i]["专业代码"].ToString().Length < 8)
                    {
                        tj3 = (tj3 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }
                    else
                    {
                        tj1 = (tj1 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }

                }
                DataTable gzzssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='高职' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (gzzssl.Rows.Count > 0) tj4 = Convert.ToInt32(gzzssl.Rows[0][0].ToString());
                int x3 = Convert.ToInt32(tj3);
                int x4 = Convert.ToInt32(tj4);
                string bfb = "0.00%";
                if (tj3.ToString() != "0")
                {
                    bfb = ((float)x4/x3 * 100).ToString("0.00") + "%";
                }
                cjx2.Text = tj3.ToString();
                cjx3.Text = tj4.ToString();
                cjx4.Text = bfb;
                DataTable zssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='中职' and zgz='无' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (zssl.Rows.Count > 0) tj2 = Convert.ToInt32(zssl.Rows[0][0].ToString());
                cjx.Text = tj1.ToString();
                cjx0.Text = tj2.ToString();
                int x1 = Convert.ToInt32(cjx0.Text);
                int x2 = Convert.ToInt32(cjx.Text);
                if (cjx.Text == "0")
                {
                    cjx1.Text = "0.00%";
                }
                else
                {
                    cjx1.Text = ((float)x1 / x2 * 100).ToString("0.00") + "%";
                }
            }
        }
        catch
        { }
        try
        {
            DataTable jjxdata = Sqlhelper.Serach("SELECT [院系名称],[招生计划],[已招人数],[院系代码],[专业代码] FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系名称='应用科技系' order by 院系名称");
            if (jjxdata.Rows.Count > 0)
            {
                int tj1 = 0;
                int tj2 = 0;
                int tj3 = 0;
                int tj4 = 0;
                for (int i = 0; i < jjxdata.Rows.Count; i++)
                {
                    if (jjxdata.Rows[i]["专业代码"].ToString().Length < 8)
                    {
                        tj3 = (tj3 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }
                    else
                    {
                        tj1 = (tj1 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }

                }
                DataTable gzzssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='高职' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (gzzssl.Rows.Count > 0) tj4 = Convert.ToInt32(gzzssl.Rows[0][0].ToString());
                int x3 = Convert.ToInt32(tj3);
                int x4 = Convert.ToInt32(tj4);
                string bfb = "0.00%";
                if (tj3.ToString() != "0")
                {
                    bfb = ((float)x4/x3 * 100).ToString("0.00") + "%";
                }
                yyx2.Text = tj3.ToString();
                yyx3.Text = tj4.ToString();
                yyx4.Text = bfb;
                DataTable zssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2  and lb='中职' and nj='"+basic.dqnd()+"' and zgz='无' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (zssl.Rows.Count > 0) tj2 = Convert.ToInt32(zssl.Rows[0][0].ToString());
                yyx.Text = tj1.ToString();
                yyx0.Text = tj2.ToString();
                int x1 = Convert.ToInt32(yyx0.Text);
                int x2 = Convert.ToInt32(yyx.Text);
                if (yyx.Text == "0")
                {
                    yyx1.Text = "0.00%";
                }
                else
                {
                    yyx1.Text = ((float)x1 / x2 * 100).ToString("0.00") + "%";
                }
            }
        }
        catch
        { }
        try
        {
            DataTable jjxdata = Sqlhelper.Serach("SELECT [院系名称],[招生计划],[已招人数],[院系代码],[专业代码] FROM [zsgl_zsjh] where 年度='"+basic.dqnd()+"' and 院系名称='设计系' order by 院系名称");
            if (jjxdata.Rows.Count > 0)
            {
                int tj1 = 0;
                int tj2 = 0;
                int tj3 = 0;
                int tj4 = 0;
                for (int i = 0; i < jjxdata.Rows.Count; i++)
                {
                    if (jjxdata.Rows[i]["专业代码"].ToString().Length < 8)
                    {
                        tj3 = (tj3 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }
                    else
                    {
                        tj1 = (tj1 + Convert.ToInt32(jjxdata.Rows[i][1].ToString()));
                    }

                }
                DataTable gzzssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and nj='"+basic.dqnd()+"'  and lb='高职' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (gzzssl.Rows.Count > 0) tj4 = Convert.ToInt32(gzzssl.Rows[0][0].ToString());
                int x3 = Convert.ToInt32(tj3);
                int x4 = Convert.ToInt32(tj4);
                string bfb = "0.00%";
                if (tj3.ToString() != "0")
                {
                    bfb = ((float)x4/x3 * 100).ToString("0.00") + "%";
                }
                sjx2.Text = tj3.ToString();
                sjx3.Text = tj4.ToString();
                sjx4.Text = bfb;
                DataTable zssl = Sqlhelper.Serach("select count(id) from xsjcsj where xszt>=2 and lb='中职' and nj='"+basic.dqnd()+"' and zgz='无' and yxdm='" + jjxdata.Rows[0]["院系代码"].ToString() + "'");
                if (zssl.Rows.Count > 0) tj2 = Convert.ToInt32(zssl.Rows[0][0].ToString());
                sjx.Text = tj1.ToString();
                sjx0.Text = tj2.ToString();
                int x1 = Convert.ToInt32(sjx0.Text);
                int x2 = Convert.ToInt32(sjx.Text);
                if (sjx.Text == "0")
                {
                    sjx1.Text = "0.00%";
                }
                else
                {
                    sjx1.Text = ((float)x1 / x2 * 100).ToString("0.00") + "%";
                }
            }
        }
        catch
        { }
        try
        {
            all.Text = (Convert.ToInt32(jjx.Text) + Convert.ToInt32(qcx.Text) + Convert.ToInt32(jyx.Text) + Convert.ToInt32(wlx.Text) + Convert.ToInt32(tmx.Text) + Convert.ToInt32(cjx.Text) + Convert.ToInt32(yyx.Text) + Convert.ToInt32(sjx.Text) ).ToString();

            all0.Text = (Convert.ToInt32(jjx0.Text) + Convert.ToInt32(qcx0.Text) + Convert.ToInt32(jyx0.Text) + Convert.ToInt32(wlx0.Text) + Convert.ToInt32(tmx0.Text) + Convert.ToInt32(cjx0.Text) + Convert.ToInt32(yyx0.Text) + Convert.ToInt32(sjx0.Text)).ToString();
           
            all2.Text = (Convert.ToInt32(jjx2.Text) + Convert.ToInt32(qcx2.Text) + Convert.ToInt32(jyx2.Text) + Convert.ToInt32(wlx2.Text) + Convert.ToInt32(tmx2.Text) + Convert.ToInt32(cjx2.Text) + Convert.ToInt32(yyx2.Text) + Convert.ToInt32(sjx2.Text)).ToString();

            all3.Text = (Convert.ToInt32(jjx3.Text) + Convert.ToInt32(qcx3.Text) + Convert.ToInt32(jyx3.Text) + Convert.ToInt32(wlx3.Text) + Convert.ToInt32(tmx3.Text) + Convert.ToInt32(cjx3.Text) + Convert.ToInt32(yyx3.Text) + Convert.ToInt32(sjx3.Text)).ToString();
           
            
            int x1 = Convert.ToInt32(all0.Text);
                int x2 = Convert.ToInt32(all.Text);
                if (all.Text == "0")
                {
                    all1.Text = "0.00%";
                }
                else
                {
                    all1.Text = ((float)x1 / x2 * 100).ToString("0.00") + "%";
                }


                int x3 = Convert.ToInt32(all2.Text);
                int x4 = Convert.ToInt32(all3.Text);
                if (x3 == 0)
                {
                    all4.Text = "0.00%";
                }
                else
                {
                    all4.Text = ((float)x4 / x3 * 100).ToString("0.00") + "%";
                }

        }
        catch
        { }

    }
}
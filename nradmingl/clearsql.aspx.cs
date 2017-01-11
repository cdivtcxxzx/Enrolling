using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class clearsql : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //获取所有数据库名
        DataTable x = Sqlhelper.Serach("SELECT Name FROM Master..SysDatabases ORDER BY Name");
//3.获取所有表名
//SELECT Name FROM DatabaseName..SysObjects Where XType='U'ORDER BY Name
//XType='U':表示所有用户表;
//XType='S':表示所有系统表;
//4.获取所有字段名:
//SELECT Name FROM SysColumns WHERE id=Object_Id('TableName')
//5.获取数据库所有类型
//select name from systypes
//6.获取主键字段
//SELECT   name FROM SysColumns WHERE id=Object_Id('表名') and colidin(selectkeyno from sysindexkeyswhereid=Object_Id('表名'))


        if(x.Rows.Count>0)
        {
            show.InnerHtml = "获取到数据库名：<br>";
            for (int i = 0; i < x.Rows.Count; i++)
            {
                show.InnerHtml += x.Rows[i][0].ToString() + "<br>";
            }

        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        //SELECT Name FROM gzyuan..SysObjects  ORDER BY Name
        DataTable x = Sqlhelper.Serach("SELECT Name FROM " + this.dataname.Text + "..SysObjects where  XType='U' ORDER BY Name");
        if (x.Rows.Count > 0)
        {
            show.InnerHtml = "获取到数据库" + this.dataname.Text + "的表结构：<br>";
            for (int i = 0; i < x.Rows.Count; i++)
            {
                show.InnerHtml += x.Rows[i][0].ToString() + "<br>";
            }

        }
        else
        {
            show.InnerHtml = "<font color=red>未获取到数据库" + this.dataname.Text + "的数据！</font><br>" + "SELECT Name FROM " + this.dataname.Text + "..SysObjects  ORDER BY Name";
        }
    }
    protected void Button1_Click2(object sender, EventArgs e)
    {
        //获取表结构
        //Response.Write(HttpUtility.HtmlDecode(this.TextBox1.Text));
        //.End();
       // string text1="<div style=\"display:none\">android free spy app <a href=\"http://www.turbofish.com/template/page/free-cell-phone-spyware-for-android.aspx\">phone spyware free</a> android phone spying</div>";
        //Response.Write("text:"+text1);
        try
        {
            int sl = 0;
            int sb=0;
            string sbjl = "";
            DataTable x = Sqlhelper.Serach("SELECT Name FROM " + this.dataname.Text + "..SysObjects where  XType='U'  ORDER BY Name");
            if (x.Rows.Count > 0)
            {
                show.InnerHtml = "获取到数据库" + this.dataname.Text + "的表结构：<br>";
                for (int ix = 0; ix < x.Rows.Count; ix++)
                {
                }


                for (int i = 0; i < x.Rows.Count; i++)
                {
                    show.InnerHtml += "处理表（" + i.ToString() + "）：" + x.Rows[i][0].ToString() + "<br>";
                    Response.Write("处理表（" + i.ToString() + "）：" + x.Rows[i][0].ToString() + "<br>");
                    //获取该表所有字段
                    //select name from syscolumns where id=object_id('xw_neirong') 
                    DataTable y = Sqlhelper.Serach("select name from syscolumns where id=object_id('"+this.dataname.Text+".[dbo]." + x.Rows[i][0].ToString() + "')");
                    if (y.Rows.Count > 0)
                    {

                        for (int i1 = 0; i1 < y.Rows.Count; i1++)
                        {
                            string setok = " set ";
                            string setok2 = " set ";
                            string setok3 = " set ";
                            string setok4 = " set ";
                            string setok5 = " set ";
                            string setok6 = " set ";
                            string setok7 = " set ";
                            string setok8 = " set ";
                            string setok9 = " set ";
                            string setok10 = " set ";
                            // Response.Write(y.Rows[i1][0].ToString()+"<br>");
                            if (y.Rows[i1][0].ToString() != "id")
                            {

                                setok += "" + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + this.TextBox1.Text + "','')";
                                setok2 += "" + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + this.TextBox2.Text + "','')";
                                setok3 += "" + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + this.TextBox3.Text + "','')";
                                setok4 += "" + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + this.TextBox4.Text + "','')";
                                setok5 += "" + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + this.TextBox5.Text + "','')";
                                setok6 += "" + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + this.TextBox6.Text + "','')";
                                setok7 += "" + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + this.TextBox7.Text + "','')";
                                setok8 += "" + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + this.TextBox8.Text + "','')";
                                setok9 += "" + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + this.TextBox9.Text + "','')";
                                setok10 += "" + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + this.TextBox10.Text + "','')";

                                if (x.Rows[i][0].ToString() != "sqlzr")
                                {
                                    Response.Write("替换" + x.Rows[i][0].ToString() + "的“" + y.Rows[i1][0].ToString() + "”字段<br>");
                                    //从数据库中读取替换
                                    if (CheckBox1.Checked)
                                    {
                                        DataTable zz = Sqlhelper.Serach("SELECT sql  FROM [sqlzr]");
                                        if (zz.Rows.Count > 0)
                                        {
                                            for (int i2 = 0; i2 < zz.Rows.Count; i2++)
                                            {
                                                string setok11 = " set " + y.Rows[i1][0].ToString() + "=replace(" + y.Rows[i1][0].ToString() + ",'" + zz.Rows[i2][0].ToString() + "','')";
                                                try
                                                {
                                                    if (Sqlhelper.ExcuteNonQuery("UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok11) > 0)
                                                    {
                                                        sl = sl + 1;
                                                    }
                                                    else
                                                    {
                                                        sb = sb + 1;
                                                        sbjl += "<br>" + "UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok11;
                                                    }
                                                }
                                                catch (Exception m1)
                                                {
                                                    Response.Write(m1.Message);
                                                }
                                            }
                                        }
                                        Response.Write(sbjl);
                                        sbjl = "";
                                    }
                                    //over


                                    //替换开始
                                    //替换
                                    if (CheckBox2.Checked)
                                    {
                                        Response.Write("UPDATE [" + x.Rows[i][0].ToString() + "]  " + setok + "<br>");
                                        try
                                        {
                                            if (Sqlhelper.ExcuteNonQuery("UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok) > 0)
                                            {
                                                sl = sl + 1;
                                            }
                                            else
                                            {
                                                sb = sb + 1;
                                                sbjl += "<br>" + "UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok;
                                            }
                                            if (Sqlhelper.ExcuteNonQuery("UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok2) > 0)
                                            {
                                                sl = sl + 1;
                                            }
                                            else
                                            {
                                                sb = sb + 1;
                                                sbjl += "<br>" + "UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok2;
                                            }
                                            if (Sqlhelper.ExcuteNonQuery("UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok3) > 0)
                                            {
                                                sl = sl + 1;
                                            }
                                            else
                                            {
                                                sb = sb + 1;
                                                sbjl += "<br>" + "UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok3;
                                            }
                                            if (Sqlhelper.ExcuteNonQuery("UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok4) > 0)
                                            {
                                                sl = sl + 1;
                                            }
                                            else
                                            {
                                                sb = sb + 1;
                                                sbjl += "<br>" + "UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok4;
                                            }
                                            if (Sqlhelper.ExcuteNonQuery("UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok5) > 0)
                                            {
                                                sl = sl + 1;
                                            }
                                            else
                                            {
                                                sb = sb + 1;
                                                sbjl += "<br>" + "UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok5;
                                            }
                                            if (Sqlhelper.ExcuteNonQuery("UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok6) > 0)
                                            {
                                                sl = sl + 1;
                                            }
                                            else
                                            {
                                                sb = sb + 1;
                                                sbjl += "<br>" + "UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok6;
                                            }
                                            if (Sqlhelper.ExcuteNonQuery("UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok7) > 0)
                                            {
                                                sl = sl + 1;
                                            }
                                            else
                                            {
                                                sb = sb + 1;
                                                sbjl += "<br>" + "UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok7;
                                            }
                                            if (Sqlhelper.ExcuteNonQuery("UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok8) > 0)
                                            {
                                                sl = sl + 1;
                                            }
                                            else
                                            {
                                                sb = sb + 1;
                                                sbjl += "<br>" + "UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok8;
                                            }
                                            if (Sqlhelper.ExcuteNonQuery("UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok9) > 0)
                                            {
                                                sl = sl + 1;
                                            }
                                            else
                                            {
                                                sb = sb + 1;
                                                sbjl += "<br>" + "UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok9;
                                            }
                                            if (Sqlhelper.ExcuteNonQuery("UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok10) > 0)
                                            {
                                                sl = sl + 1;
                                            }
                                            else
                                            {
                                                sb = sb + 1;
                                                sbjl += "<br>" + "UPDATE " + this.dataname.Text + ".[dbo].[" + x.Rows[i][0].ToString() + "]  " + setok10;
                                            }
                                        }
                                        catch (Exception ex1)
                                        {
                                            Response.Write(ex1.Message);
                                        }
                                    }
                                }
                            }
                            //替换结束

                        }


                        this.show.InnerHtml = "<font color=green>更新" + sl.ToString() + "条数据，失败" + sb.ToString() + "条！</font>";
                    }
                    else
                    {
                        Response.Write("<br>no find:" + "select name from syscolumns where id=object_id('" + this.dataname.Text + ".[dbo]." + x.Rows[i][0].ToString() + "')<br>");
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
    protected void Button1_Click23(object sender, EventArgs e)
    {
         DataTable y = Sqlhelper.Serach("select name from syscolumns where id=object_id('"+this.dataname.Text+".[dbo]." +this.TextBox12.Text + "')");
         if (y.Rows.Count > 0)
         {
             this.show.InnerHtml = "";
             for (int i1 = 0; i1 < y.Rows.Count; i1++)
             {
                 this.show.InnerHtml += "<font color=green>"+y.Rows[i1][0].ToString()+"<br></font>";
             }

         }
         else
         {
             show.InnerHtml = "<font color=red>未获取到数据 </font><br>" + "select name from syscolumns where id=object_id('" + this.dataname.Text + ".[dbo]." + this.TextBox12.Text + "')";
        
         }
                          
    }
}
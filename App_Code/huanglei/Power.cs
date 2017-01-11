using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;

/// <summary>
///Power 的摘要说明
/// </summary>
public class Power : System.Web.UI.Page
{
    protected string sqlString;
    //lanmqxID 存栏目id_栏目名称
    //public static ArrayList lanmqxID;
    //lanmCheckBox 存权限名称集合，用“，”隔开
    //public static ArrayList lanmCheckBox;
    //lanmCheckBoxID 存栏目id_栏目名称_权限名称
    protected ArrayList lanmCheckBoxID;
	public Power()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public DataTable GetLanmStruct(string lmid)
    {
        DataTable dt = Sqlhelper.Serach("select lmid,lmmc,gjz,sfqxyz,fid from lanm  where fid=@lmid order by fid,lmid", new SqlParameter("lmid", lmid));
        DataTable result=new DataTable();
        result.Columns.Add("lmid", typeof(string));
        result.Columns.Add("lmmc", typeof(string));
        result.Columns.Add("gjz", typeof(string));
        result.Columns.Add("sfqxyz", typeof(string));
        result.Columns.Add("fid", typeof(string));
        foreach(DataRow dr in dt.Rows)
        {
            DataRow ndr = result.NewRow();
            ndr["lmid"] = dr["lmid"].ToString();
            ndr["lmmc"] = dr["lmmc"].ToString();
            ndr["gjz"] = dr["gjz"].ToString();
            ndr["sfqxyz"] = dr["sfqxyz"].ToString();
            ndr["fid"] = dr["fid"].ToString();
            result.Rows.Add(ndr);
            result = GetLanmChild(result, dr["lmid"].ToString());
        }
        return result;
    }

    public DataTable GetLanmChild(DataTable dt,string lmid)
    {
        DataTable temp = Sqlhelper.Serach("select lmid,lmmc,gjz,sfqxyz,fid from lanm  where fid=@lmid order by fid,lmid", new SqlParameter("lmid", lmid));
        foreach (DataRow dr in temp.Rows)
        {
            DataRow ndr = dt.NewRow();
            ndr["lmid"] = dr["lmid"].ToString();
            ndr["lmmc"] = dr["lmmc"].ToString();
            ndr["gjz"] = dr["gjz"].ToString();
            ndr["sfqxyz"] = dr["sfqxyz"].ToString();
            ndr["fid"] = dr["fid"].ToString();
            dt.Rows.Add(ndr);
            dt=GetLanmChild(dt,dr["lmid"].ToString());
        }
        return dt;
    }
    //public XDocument MergeQx(string yhid)
    //{}
    /// <summary>
    ///教务班级直接读取Session里的yhid和传入的列字段名参数返回筛选条件
    /// </summary>
    /// <param name="colName">院系名称在相应表里的字段名</param>
    /// <returns>返回能操作系部的筛选条件</returns>
    public string GetFilterjwgl(string colName)
    {
        string yhid = "";
        try
        {
            yhid = Session["UserName"].ToString();
        }

        catch
        {
            HttpContext.Current.Response.Redirect("~/Default.aspx");
        }
        string ex = "1=2";
        DataTable dt = Sqlhelper.Serach("select yhqx from yonghqx where yhid='" + yhid + "'");
        string str = dt.Rows[0][0].ToString();
        if (str == "0" || str == "") { return ex; }
        XDocument xml = null;
        try
        {
            xml = XDocument.Parse(str);
        }
        catch (Exception exception)
        {
            xml = XDocument.Parse("<Root><能操作数据的系部></能操作数据的系部></Root>");
            new c_log().logAdd("Power.cs", "GetYxdmsByYhid", exception.Message);
        }
        ArrayList al = new ArrayList();
        DataTable yxdm = Sqlhelper.Serach("select * from dm_yuanxi");
        string x = string.Empty;
        if (xml.Element("Root").Element("能操作数据的系部") == null) { return ex; }
        foreach (var temp in xml.Element("Root").Element("能操作数据的系部").Elements())
        {
            try
            {
                x = yxdm.Select("yxmc='" + temp.Name.ToString() + "'")[0][0].ToString();
                ex += " or " + "right(left(" + colName + ",6),2)" + "='" + x.ToString().Substring(2, 2) + "'";
            }
            catch
            {
            }
        }
        return ex;
    }
    /// <summary>
    /// 直接读取Session里的yhid和传入的列字段名参数返回筛选条件
    /// </summary>
    /// <param name="colName">院系名称在相应表里的字段名</param>
    /// <returns>返回能操作系部的筛选条件</returns>
    public string GetFilteryxmc(string colName)
    {
        string yhid = "";
        try
        {
            yhid = Session["UserName"].ToString();
        }

        catch
        {
            HttpContext.Current.Response.Redirect("~/Default.aspx");
        }
        string ex = " 1=2";
        //DataTable dt = Sqlhelper.Serach("select yhqx from yonghqx where yhid='" + yhid + "'");
        //string str = dt.Rows[0][0].ToString();
        string str = Session["yhqx"].ToString();
       
        if (str == "0" || str == "") { return ex; }
        XDocument xml = null;
        try
        {
            xml = XDocument.Parse(str);
        }
        catch (Exception exception)
        {
            xml = XDocument.Parse("<Root><能操作数据的系部></能操作数据的系部></Root>");
            new c_log().logAdd("Power.cs", "GetYxdmsByYhid", exception.Message);
        }
        ArrayList al = new ArrayList();
        DataTable yxdm = Sqlhelper.Serach("select * from dm_yuanxi");
        string x = string.Empty;
        if (xml.Element("Root").Element("能操作数据的系部") == null) { return ex; }
        foreach (var temp in xml.Element("Root").Element("能操作数据的系部").Elements())
        {
            try
            {
                x = yxdm.Select("yxmc='" + temp.Name.ToString() + "'")[0][2].ToString();
                ex += " or " + colName + " like '%" + x.ToString() + "%' ";
            }
            catch
            {
            }
        }
        return ex;
    }
    /// <summary>
    /// 直接读取Session里的yhid和传入的列字段名参数返回筛选条件
    /// </summary>
    /// <param name="colName">院系代码在相应表里的字段名</param>
    /// <returns>返回能操作系部的所有系部，以逗号隔开</returns>
    public string Getonebmdm(string colName)
    {
        string yhid = "";
        try
        {
            yhid = Session["UserName"].ToString();
        }

        catch
        {
            HttpContext.Current.Response.Redirect("~/Default.aspx");
        }
        string ex = "1=2";
        //DataTable dt = Sqlhelper.Serach("select yhqx from yonghqx where yhid='" + yhid + "'");
        string str = Session["yhqx"].ToString();
        //return str;
        if (str == "0" || str == "") { return ex; }
        XDocument xml = null;
        try
        {
            xml = XDocument.Parse(str);
        }
        catch (Exception exception)
        {
            xml = XDocument.Parse("<Root><能操作数据的系部></能操作数据的系部></Root>");
            new c_log().logAdd("Power.cs", "GetYxdmsByYhid", exception.Message);
        }
        ArrayList al = new ArrayList();
        DataTable yxdm = Sqlhelper.Serach("select * from dm_yuanxi");
        string x = string.Empty;
        if (xml.Element("Root").Element("能操作数据的系部") == null) { return ex; }
        foreach (var temp in xml.Element("Root").Element("能操作数据的系部").Elements())
        {
            try
            {
                x += yxdm.Select("yxmc='" + temp.Name.ToString() + "'")[0][1].ToString() + ",";
            }
            catch
            {
            }
        }
        return x;
    }


    /// <summary>
    /// 直接读取Session里的yhid和传入的列字段名参数返回筛选条件
    /// </summary>
    /// <param name="colName">权限在相应表里的字段名</param>
    /// <returns>返回能操作的栏目列表的筛选条件</returns>
    public string Getlanm(string colName)
    {
        string yhid = "";
        try
        {
            yhid = Session["UserName"].ToString();
        }

        catch
        {
            HttpContext.Current.Response.Redirect("~/Default.aspx");
        }
        //根据用户获取自己所属的组
        string ex = "1=2  or "+colName +"='' ";
        //DataTable dt = Sqlhelper.Serach("select yhqx from yonghqx where yhid='" + yhid + "'");
        string str = "";
        //return str;

        DataTable lsz = Sqlhelper.Serach("select lsz from yonghqx where yhid='"+yhid+"'");
        if (lsz.Rows.Count > 0)
        {
            str = lsz.Rows[0][0].ToString();
        }
        string[] s = str.Split(',');
        foreach (var temp in s)
        {
            try
            {
              if(temp.Length>0)ex += " or " + colName + " like '%" + temp + "|%' ";
            }
            catch { }
        }
        return ex;
    }






    /// <summary>
    /// 直接读取Session里的yhid和传入的列字段名参数返回筛选条件
    /// </summary>
    /// <param name="colName">院系代码在相应表里的字段名</param>
    /// <returns>返回能操作系部的筛选条件</returns>
    public string GetFilterExpression(string colName)
    {
        string yhid = "";
        try
        {
           yhid = Session["UserName"].ToString();
        }
               
        catch
        {
            HttpContext.Current.Response.Redirect("~/Default.aspx");
        }
        string ex = "1=2 or " + colName + "='' ";
        //DataTable dt = Sqlhelper.Serach("select yhqx from yonghqx where yhid='" + yhid + "'");
        string str = Session["yhqx"].ToString();
        //return str;
        if (str == "0" || str == "") { return ex; }
        XDocument xml = null;
        try
        {
            xml = XDocument.Parse(str);
        }
        catch (Exception exception)
        {
            xml = XDocument.Parse("<Root><能操作数据的系部></能操作数据的系部></Root>");
            new c_log().logAdd("Power.cs", "GetYxdmsByYhid", exception.Message);
        }
        ArrayList al = new ArrayList();
        DataTable yxdm = Sqlhelper.Serach("select * from dm_yuanxi");
        string x = string.Empty;
        if (xml.Element("Root").Element("能操作数据的系部") == null) { return ex; }
        foreach (var temp in xml.Element("Root").Element("能操作数据的系部").Elements())
        {
            try
            {
                x = yxdm.Select("yxmc='" + temp.Name.ToString() + "'")[0][1].ToString();
                //ex += " or "+colName+" like '%" + x.ToString() + "%' ";
                ex += " or " + colName + " like '%" + x.ToString() + "%' ";
            }
            catch { }
        }
        return ex;
    }
    /// <summary>
    /// 通过yhid返回该用户能管理的系部代码集合
    /// </summary>
    /// <returns></returns>
    public ArrayList GetYxdmsByYhid(string yhid)
    {
       DataTable dt=Sqlhelper.Serach("select yhqx from yonghqx where yhid='"+yhid+"'");
       string str = dt.Rows[0][0].ToString();
        if (str == "0" || str == "") { return new ArrayList(); }
        XDocument xml = null;
        try
        {
            xml = XDocument.Parse(str);
        }
        catch (Exception ex)
        {
            xml = XDocument.Parse("<Root><能操作数据的系部></能操作数据的系部></Root>");
            new c_log().logAdd("Power.cs","GetYxdmsByYhid",ex.Message);
        }
        ArrayList al= new ArrayList();
        DataTable yxdm = Sqlhelper.Serach("select * from dm_yuanxi");
        string x = string.Empty;
        foreach (var temp in xml.Element("Root").Element("能操作数据的系部").Elements())
        {
            x = yxdm.Select("yxmc='" + temp.Name.ToString() + "'")[0][0].ToString();
            al.Add(x);
        }
        return al;
    }
    /// <summary>
    /// 通过yhid返回该用户能管理的系部名称集合
    /// </summary>
    /// <returns></returns>
    public ArrayList GetYxmcsByYhid(string yhid)
    {
        DataTable dt = Sqlhelper.Serach("select yhqx from yonghqx where yhid='" + yhid + "'");
        string str = dt.Rows[0][0].ToString();
        if (str == "0" || str == "") { return new ArrayList(); }
        XDocument xml = null;
        try
        {
            xml = XDocument.Parse(str);
        }
        catch (Exception ex)
        {
            xml = XDocument.Parse("<Root><能操作数据的系部></能操作数据的系部></Root>");
            new c_log().logAdd("Power.cs", "GetYxdmsByYhid", ex.Message);
        }
        ArrayList al = new ArrayList();
        string x = string.Empty;
        if (xml.Element("Root").Element("能操作数据的系部") == null) { xml.Element("Root").Add(new XElement("能操作数据的系部")); }
        foreach (var temp in xml.Element("Root").Element("能操作数据的系部").Elements())
        {
            x = temp.Name.ToString();
            al.Add(x);
        }
        return al;
    }
    /// <summary>
    /// 通过组id返回该用户能管理的系部名称集合
    /// </summary>
    /// <returns></returns>
    public ArrayList GetYxmcsByZid(string zid)
    {
        DataTable dt = Sqlhelper.Serach("select zqx from zhuqx where zid='" + zid + "'");
        string str = dt.Rows[0][0].ToString();
        if (str == "0" || str == "") { return new ArrayList(); }
        XDocument xml = null;
        try
        {
            xml = XDocument.Parse(str);
        }
        catch (Exception ex)
        {
            xml = XDocument.Parse("<Root><能操作数据的系部></能操作数据的系部></Root>");
            new c_log().logAdd("Power.cs", "GetYxmcsByZid", ex.Message);
        }
        ArrayList al = new ArrayList();
        string x = string.Empty;
        if (xml.Element("Root").Element("能操作数据的系部") == null) { xml.Element("Root").Add(new XElement("能操作数据的系部")); }
        foreach (var temp in xml.Element("Root").Element("能操作数据的系部").Elements())
        {
            x = temp.Name.ToString();
            al.Add(x);
        }
        return al;
    }
    public DataTable ZhuPower()
    {
        sqlString = "select zid,zmc from zhuqx";
        return Sqlhelper.Serach(sqlString);
    }

    /// <summary>
    /// 通过教学部门id串得到名称串
    /// </summary>
    /// <param name="zids">组id串</param>
    /// <returns>组名称串</returns>
    public string getjxMCs(string zids)
    {
        if (zids == "") { return ""; }
        string zhuMCs = "";
        string[] zid = getFromQx(zids);
        string sqlString = "select yxmc from dm_yuanxi where (1=2)";
        foreach (string x in zid)
        {
            sqlString = sqlString + " or  yxdm=" + "'" + x + "'";
        }
        DataTable dt = Sqlhelper.Serach(sqlString);
        foreach (DataRow y in dt.Rows)
        {
            zhuMCs = zhuMCs + y[0] + ",";
        }
        return zhuMCs;
    }
    /// <summary>
    /// 通过教学部门id串得到名称串
    /// </summary>
    /// <param name="zids">组id串</param>
    /// <returns>组名称串</returns>
    public string getjxids(string zids)
    {
        if (zids == "") { return ""; }
        string zhuMCs = "";
        string[] zid = getFromQx(zids);
        string sqlString = "select yxdm from dm_yuanxi where (1=2)";
        foreach (string x in zid)
        {
            sqlString = sqlString + " or  yxmc=" + "'" + x + "'";
        }
        DataTable dt = Sqlhelper.Serach(sqlString);
        foreach (DataRow y in dt.Rows)
        {
            zhuMCs = zhuMCs + y[0] + ",";
        }
        return zhuMCs;
    }

    /// <summary>
    /// 通过组id串得到组名称串
    /// </summary>
    /// <param name="zids">组id串</param>
    /// <returns>组名称串</returns>
    public string getZhuMCs(string zids)
    {
        if (zids == "") { return ""; }
        string zhuMCs = "";
        string[] zid = getFromQx(zids);
        string sqlString = "select zmc from zhuqx where (1=2)";
        foreach (string x in zid)
        {
            sqlString = sqlString + " or  zid=" + "'" + x + "'";
        }
        DataTable dt = Sqlhelper.Serach(sqlString);
        foreach (DataRow y in dt.Rows)
        {
            zhuMCs = zhuMCs + y[0] + ",";
        }
        return zhuMCs;
    }
    /// <summary>
    /// 通过组名称串得到组id串
    /// </summary>
    /// <param name="zids">组名称串</param>
    /// <returns>组id串</returns>
    public string getZhuIDs(string zmcs)
    {
        if (zmcs == "") { return ""; }
        string zhuIDs = "";
        string[] zmc = getFromQx(zmcs);
        string sqlString="select zid from zhuqx where (1=2)";
        foreach(string x in zmc)
        {
            sqlString=sqlString+" or  zmc="+"'"+x+"'";
        }
        DataTable dt = Sqlhelper.Serach(sqlString);
        foreach (DataRow y in dt.Rows)
        {
            zhuIDs = zhuIDs + y[0] + ",";
        }
        return zhuIDs;
    }
    //public bool YhqxUpdate(string _yhid,DataTable dt)
    //{

    //}

    /// <summary>
    /// 获取需要分配权限的栏目及相应权限列表
    /// </summary>
    /// <returns>"栏目id","栏目名称","权限列表"三列的DataTable,其中“权限列表”里是html代码输出页面,并且用户已经拥有的权限checkbox打上勾</returns>
    public DataTable LanmPower(string yhid)
    {
        sqlString = "select lmid,lmmc,sfqxyz from lanm";
        DataTable lanm = Sqlhelper.Serach(sqlString);
        DataTable tempTable = new DataTable("lanmqx");
        tempTable.Columns.Add("lmid", typeof(int));
        tempTable.Columns.Add("lmmc", typeof(string));
        tempTable.Columns.Add("qxlb", typeof(string));
        DataColumn[] columns = new DataColumn[1];
        columns[0] = tempTable.Columns["lmid"];
        tempTable.PrimaryKey = columns;
        foreach (DataRow x in lanm.Rows)
        {
            if (x.ItemArray[2].ToString() != "0")
            {
                DataRow dr = tempTable.NewRow();

                dr[0] = x.ItemArray[0];
                dr[1] = x.ItemArray[1];
                string[] qxIds = getFromQx(x.ItemArray[2].ToString());
                string htmlString = string.Empty;
                foreach (string id in qxIds)
                {
                    if (id != "0")
                    { sqlString = "select qxmc from quanxdm where qxid=" + id; }
                    else continue;
                    string qxmc = Sqlhelper.Serach(sqlString).Rows[0].ItemArray[0].ToString();
                    string checkedString = "";
                    if (new c_login().powerYanzheng(yhid,dr[1].ToString(),qxmc)) { checkedString = "checked=\"checked\""; }
                    htmlString += "<input ID=\"qxid" + id + "\" type=\"checkbox\" "+checkedString+"/><label for=\"qxid" + id +"\">" + qxmc + "</label>&nbsp;&nbsp;";
                }
                dr[2] = htmlString;
                tempTable.Rows.Add(dr);
            }
         }
        return tempTable;
      }
    /// <summary>
    /// 获取栏目权限的checkbox表
    /// </summary>
    /// <returns></returns>
    public Table LanmPowerTable()
    {
        sqlString = "select lmid,lmmc,sfqxyz from lanm";
        DataTable lanm = Sqlhelper.Serach(sqlString);
        Table tempTable = new Table();
        tempTable.ID = "lanmqx";
        tempTable.CssClass = "table";
        TableRow tr = new TableRow();
        tr.Cells.Add(new TableCell());
        tr.Cells[0].Width = 70;
        tr.Cells[0].Text = "栏目模块ID";
        tr.Cells.Add(new TableCell());
        tr.Cells[1].Width = 120;
        tr.Cells[1].Text = "栏目模块名称";
        tr.Cells.Add(new TableCell());
        tr.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        tr.Cells[2].Text = "权限列表<a href=\"#\" class=\"help\">[?]</a><span class=\"note\">选择该用户对模块栏目所具备的权限。</span>&nbsp;&nbsp;<input id=\"seleteall\" type=\"checkbox\" name=\"seleteall\" /><label for=\"seleteall\">全选</label>";
        tempTable.Rows.Add(tr);
        lanmCheckBoxID = new ArrayList();
        foreach (DataRow z in lanm.Rows)
        {
            if (z.ItemArray[2].ToString() != "0")
            {
                TableRow row = new TableRow();
                int i = 1;
                string[] qxIds = getFromQx(z.ItemArray[2].ToString());
                foreach (var x in z.ItemArray)
                {
                    TableCell tc = new TableCell();
                    tc.Text = x.ToString();
                    tc.HorizontalAlign = HorizontalAlign.Left;
                    if (i == 3)
                    {
                        string qxmcs = string.Empty;
                        foreach (string id in qxIds)
                        {
                            if (id != "0")
                            { sqlString = "select qxmc from quanxdm where qxid=" + id; }
                            else continue;
                            string qxmc = Sqlhelper.Serach(sqlString).Rows[0].ItemArray[0].ToString();
                            CheckBox tempCB = new CheckBox();
                            tempCB.ID = z.ItemArray[0].ToString() + "_" + z.ItemArray[1].ToString() + "_" + qxmc;
                            lanmCheckBoxID.Add(tempCB.ID);
                            tempCB.Text = qxmc;
                            tc.Controls.Add(tempCB);
                        }
                    }
                    row.Cells.Add(tc);
                    i++;
                }
                tempTable.Controls.Add(row);
            }
        }
        Session["lanmCheckBoxID"]=lanmCheckBoxID;
        return tempTable;
    }
    /// <summary>
    /// 获取栏目权限的checkbox表，并对应用户应有栏目权限打上勾
    /// </summary>
    /// <param name="yhid">用户id</param>
    /// <param name="yhORzhu">true返回用户所对应权限，false返回组对应权限</param>
    /// <returns></returns>
    public Table LanmPowerTable(string id,bool yhORzhu)
    {
        sqlString = "select lmid,lmmc,sfqxyz from lanm";
        DataTable lanm = Sqlhelper.Serach(sqlString);
        Table tempTable = new Table();
        tempTable.ID = "lanmqx";
        tempTable.CssClass = "table";
        TableRow tr = new TableRow();
        tr.Cells.Add(new TableCell());
        tr.Cells[0].Width = 70;
        tr.Cells[0].Text = "栏目模块ID";
        tr.Cells.Add(new TableCell());
        tr.Cells[1].Width = 120;
        tr.Cells[1].Text = "栏目模块名称";
        tr.Cells.Add(new TableCell());
        tr.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        tr.Cells[2].Text = "权限列表<a href=\"#\" class=\"help\">[?]</a><span class=\"note\">选择该用户对模块栏目所具备的权限。</span>&nbsp;&nbsp;<input id=\"seleteall\" type=\"checkbox\" name=\"seleteall\" /><label for=\"seleteall\">全选</label>";
        tempTable.Rows.Add(tr);
        //lanmCheckBox = new ArrayList();
        //lanmqxID=new ArrayList();
        lanmCheckBoxID = new ArrayList();
        //int j = 0;
        //int k = 0;
        foreach (DataRow z in lanm.Rows)
        {
            //TableHeaderCell headerCell = new TableHeaderCell();
            //headerCell.Text = dt.Columns[i].ColumnName;
            //row.Cells.Add(headerCell);
            if (z.ItemArray[2].ToString() != "0")
            {
                //lanmqxID.Add(z.ItemArray[0].ToString()+"_"+z.ItemArray[1].ToString());
                //k++;
                TableRow row = new TableRow();
                int i = 1;
                string[] qxIds = getFromQx(z.ItemArray[2].ToString());
                foreach (var x in z.ItemArray)
                {
                    TableCell tc = new TableCell();
                    tc.Text = x.ToString();
                    tc.HorizontalAlign = HorizontalAlign.Left;
                    if (i == 3)
                    {
                        string qxmcs = string.Empty;
                        foreach (string qxid in qxIds)
                        {
                            if (qxid != "0")
                            { sqlString = "select qxmc from quanxdm where qxid=" + qxid; }
                            else continue;
                            string qxmc = Sqlhelper.Serach(sqlString).Rows[0].ItemArray[0].ToString();
                            CheckBox tempCB = new CheckBox();
                            tempCB.ID = z.ItemArray[0].ToString()+"_"+z.ItemArray[1].ToString() + "_" + qxmc;
                            lanmCheckBoxID.Add(tempCB.ID);
                            //qxmcs =qxmcs+"_"+qxmc;
                            //j++;
                            tempCB.Text = qxmc;
                            if (yhORzhu) { if (powerYanzheng(id, z.ItemArray[1].ToString(), qxmc, "1")) { tempCB.Checked = true; } }
                            else if (powerYanzheng(id, z.ItemArray[1].ToString(), qxmc)) { tempCB.Checked = true; }
                            tc.Controls.Add(tempCB);
                        }
                        //lanmCheckBox.Insert((int)z.ItemArray[0],qxmcs);
                        //lanmCheckBox.Add(qxmcs);
                    }
                    row.Cells.Add(tc);
                    i++;
                }
                tempTable.Controls.Add(row);
            }
            
 
        }
        Session["lanmCheckBoxID"] = lanmCheckBoxID;
        return tempTable;
    }
    /// <summary>
    /// 权限验证
    /// </summary>
    /// <param name="yhid">用户名</param>
    /// <param name="KeyWord">关键字</param>
    /// <param name="operation">待验证权限</param>
    /// <param name="type">1:只查询用户权限表,2:全部查询</param>
    /// <returns></returns>
    public bool powerYanzheng(string yhid, string KeyWord, string operation, string type)
    {
        try
        {
            //查询权限id
            string strSqlQxidSerach = "SELECT qxid FROM quanxdm WHERE qxmc=@qxmc";
            string strQxid = "";
            DataTable dtQxid = Sqlhelper.Serach(strSqlQxidSerach, new SqlParameter("qxmc", operation));
            if (dtQxid.Rows.Count > 0)
            {
                strQxid = dtQxid.Rows[0][0].ToString();
            }
            //查询是否验证
            string strSqlLmidSerach = "SELECT sfqxyz FROM lanm WHERE gjz=@gjz";
            string strSfqxyz = "";
            DataTable dtLmid = Sqlhelper.Serach(strSqlLmidSerach, new SqlParameter("gjz", KeyWord.Trim()));
            if (dtLmid.Rows.Count > 0)
            {
                foreach (DataRow row in dtLmid.Rows)
                {
                    strSfqxyz = row["sfqxyz"].ToString();
                }
            }
            //如果不需要验证，则直接通过验证
            if (!strSfqxyz.Contains(strQxid))
            {
                return true;
            }
           

            switch (type)
            {
                //用户权限表中验证
                case "1":
                    //string strLsz = "";
                    string SqlString = "SELECT * FROM yonghqx WHERE yhid=@yhid";
                    DataTable dtQx = Sqlhelper.Serach(SqlString, new SqlParameter("yhid", yhid));
                    if (dtQx.Rows.Count > 0)
                    {
                        DataRow row = dtQx.Rows[0];
                        string strYhqx = row["yhqx"].ToString();
                        //strLsz = row["lsz"].ToString();
                        XDocument xml = XDocument.Parse(strYhqx);
                        if (xml.Element("Root").Element(KeyWord).Element(operation) != null)
                            return true;
                        else
                        {
                           
                            return false;
                        }
                    }
                    else
                        return false;


                case "2":
                    try
                    {
                        if (Session["Yhqx"] != null)
                        {
                            //string x = Session["Yhqx"].ToString();
                            XDocument xml = XDocument.Parse(Session["Yhqx"].ToString());
                            //运行到这里时,自动跳出....?
                            if (xml.Element("Root").Element(KeyWord).Element(operation) != null)
                                return true;
                        }
                        return false;

                    }
                    catch
                    {
                        return false;
                    }


                default: return false;
            }
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// 权限验证
    /// </summary>
    /// <param name="yhid">用户名</param>
    /// <param name="KeyWord">关键字</param>
    /// <param name="operation">待验证权限</param>
    /// <param name="type">1:只查询用户权限表,2:全部查询</param>
    /// <returns></returns>
    public bool powerYanzheng(string operation, string type)
    {
        return true;

        string x1 = HttpContext.Current.Request.PhysicalPath;

        string url = Path.GetFileName(x1);
        try
        {
            //查询权限id
            string strSqlQxidSerach = "SELECT qxid FROM quanxdm WHERE qxmc=@qxmc";
            string strQxid = "";
            DataTable dtQxid = Sqlhelper.Serach(strSqlQxidSerach, new SqlParameter("qxmc", operation));
            if (dtQxid.Rows.Count > 0)
            {
                strQxid = dtQxid.Rows[0][0].ToString();
            }
            //查询是否验证
            string strSqlLmidSerach = "SELECT sfqxyz,lmmc FROM lanm WHERE url=@url";
            string strSfqxyz = "";
            DataTable dtLmid = Sqlhelper.Serach(strSqlLmidSerach, new SqlParameter("url", url));
            if (dtLmid.Rows.Count > 0)
            {
                foreach (DataRow row in dtLmid.Rows)
                {
                    strSfqxyz = row["sfqxyz"].ToString();
                }
            }
            //如果不需要验证，则直接通过验证
            if (!strSfqxyz.Contains(strQxid))
            {
                return true;
            }
            string lmmc = dtLmid.Rows[0]["lmmc"].ToString();
            switch (type)
            {
                //用户权限表中验证
                case "1":
                    //string strLsz = "";
                    string SqlString = "SELECT * FROM yonghqx WHERE yhid=@yhid";
                    DataTable dtQx = Sqlhelper.Serach(SqlString, new SqlParameter("yhid", Session["UserName"].ToString()));
                    if (dtQx.Rows.Count > 0)
                    {
                        DataRow row = dtQx.Rows[0];
                        string strYhqx = row["yhqx"].ToString();
                        XDocument xml = XDocument.Parse(strYhqx);
                        if (xml.Element("Root").Element(lmmc).Element(operation) != null)
                            return true;
                        else
                        {
                           HttpContext.Current.Response.Write("<script>alert('没有权限');history.go(-1)</script>");
                            return false;
                        }
                    }
                    else
                        HttpContext.Current.Response.Write("<script>alert('没有权限');history.go(-1)</script>");
                    return false;
                case "2":
                    if (Session["Yhqx"] != null)
                    {
                        string xx = Session["Yhqx"].ToString();
                        XDocument xml = XDocument.Parse(Session["Yhqx"].ToString());
                        if (xml.Element("Root").Element(lmmc).Element(operation) != null)
                            return true;
                    }
                    HttpContext.Current.Response.Write("<script>alert('没有权限');history.go(-1)</script>");
                    return false;

                default: HttpContext.Current.Response.Write("<script>alert('没有权限');history.go(-1)</script>"); return false;
            }
        }
        catch (Exception ex)
        {

            HttpContext.Current.Response.Write("<script>alert('没有权限');history.go(-1)</script>");
            return false;
        }
    }
    /// <summary>
    /// 验证组权限
    /// </summary>
    /// <param name="zid">组id</param>
    /// <param name="keyword">栏目名称</param>
    /// <param name="operation">权限</param>
    /// <returns></returns>
    public bool powerYanzheng(string zid, string keyword, string operation)
    {
        string sqlStrZqx = "SELECT * FROM zhuqx WHERE zid=@zid";
        DataTable dtZqx = Sqlhelper.Serach(sqlStrZqx, new SqlParameter("zid", zid));
        if (dtZqx.Rows.Count > 0)
        {
            try
            {
            DataRow rowZqx = dtZqx.Rows[0];
            string zqx = rowZqx["zqx"].ToString();
            XDocument xmlTemp = XDocument.Parse(zqx);
           
                if (xmlTemp.Element("Root").Element(keyword).Element(operation) != null)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                
                return false;
            }
        }
        else
            return false;
    }

   /// <summary>
   /// 把一串“，”隔开的字符串取掉最后的多余“，”后转换成string[]
   /// </summary>
   /// <param name="qx">需要转换的字符串</param>
   /// <returns></returns>
    public string[] getFromQx(string qx)
    {
        string[] chkIds = {};
        string batchRegroup = qx.TrimEnd(',');//通过这种方式来获得前台隐藏域的内容  1,2,3
        if (batchRegroup.Length != 0)
        {
            chkIds = batchRegroup.Split(',');
        }
        return chkIds;
    }

    public void AddLsz(string yhid, string lsz)
    {
        DataTable dt = null;
        try
        {
            dt = Sqlhelper.Serach("select lsz from yonghqx where yhid='" + yhid + "'");
        }
        catch (Exception ex)
        {

            new c_log().logAdd("问题建议", "删除", "在使用Power().AddLsz方法时，查询sql出问题，和guid:" + yhid + "有关,异常为:" + ex.Message);
        }
        string lsz_old = dt != null ? dt.Rows[0][0].ToString() : "";
        string lsz_new = "";
        string[] lszs = lsz_old.Trim(',').Split(',');
        foreach (string x in lszs)
        {
            if (x != lsz)
            { lsz_new += x + ","; }
        }
        lsz_new += lsz + ",";
        try
        {
            Sqlhelper.ExcuteNonQuery("update yonghqx set lsz=@lsz where yhid=@yhid", new SqlParameter("lsz", lsz_new), new SqlParameter("yhid", yhid));
        }
        catch (Exception ex)
        {

            new c_log().logAdd("问题建议", "删除", "在使用Power().RemoveLsz方法时，更新sql出问题，和guid:" + yhid + "有关,异常为:" + ex.Message);
        }
    }
    public void RemoveLsz(string yhid, string lsz)
    {
        DataTable dt = null;
        try
        {
            dt = Sqlhelper.Serach("select lsz from yonghqx where yhid='" + yhid + "'");
        }
        catch (Exception ex)
        {
            
            new c_log().logAdd("问题建议","删除","在使用Power().RemoveLsz方法时，查询sql出问题，和guid:"+yhid+"有关,异常为:"+ex.Message);
        }
       string lsz_old = dt != null ? dt.Rows[0][0].ToString() : "";
        string lsz_new="";
       string[] lszs = lsz_old.Trim(',').Split(',');
       foreach (string x in lszs)
       { if (x != lsz) 
       { lsz_new += x + ","; } }
       try
       {
           Sqlhelper.ExcuteNonQuery("update yonghqx set lsz=@lsz where yhid=@yhid", new SqlParameter("lsz", lsz_new), new SqlParameter("yhid", yhid));
       }
       catch (Exception ex)
       {
           
           new c_log().logAdd("问题建议","删除","在使用Power().RemoveLsz方法时，更新sql出问题，和guid:"+yhid+"有关,异常为:"+ex.Message);
       }
    }
    /// <summary>
    /// FindControl递归
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Controls">controls的集合，比如this.Controls</param>
    /// <returns></returns>
    public static T FindControl<T>(System.Web.UI.ControlCollection Controls) where T : class
    {
        T found = default(T);

        if (Controls != null && Controls.Count > 0)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i] is T)
                {
                    found = Controls[i] as T;
                    break;
                }
                else
                    found = FindControl<T>(Controls[i].Controls);
            }
        }

        return found;
    }
    /// <summary>
    /// 通过id递归查找
    /// </summary>
    /// <param name="root"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Control FindControlIterative(Control root, string id)
    {
        if (root == null) return null;
        Control ctrl = root.FindControl(id);
        if (ctrl == null)
        {
            foreach (Control child in root.Controls)
            {
                ctrl = FindControlIterative(child, id);
                if (ctrl != null) break;
            }
        }
        return ctrl;

    }
}
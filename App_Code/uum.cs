using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceReference1;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;
/// <summary>
///uum 的摘要说明
/// </summary>
public class uum : System.Web.UI.Page
{
    public DataTable uumDt{get;set;}
    public uum()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
        uumDt = new DataTable();
        uumDt.Columns.Add("guid", typeof(string));
        uumDt.Columns.Add("xm", typeof(string));
        uumDt.Columns.Add("yhid", typeof(string));
        uumDt.Columns.Add("sfz", typeof(string));
        uumDt.Columns.Add("bmmc", typeof(string));
        uumDt.Columns.Add("gh", typeof(string));
        uumDt.Columns.Add("zw", typeof(string));
        uumDt.Columns.Add("yhlx", typeof(string));
        uumDt.Columns.Add("dh", typeof(string));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DataTable yonghqx()
    {   DataTable yhdt = new DataTable(); 
        DataTable gt = getAll();
        yhdt.Columns.Add("guid", typeof(string));
        //yhdt.Columns.Add("yhqx", typeof(string));
        //yhdt.Columns.Add("lsz", typeof(string));
        yhdt.Columns.Add("yhid", typeof(string));
        yhdt.Columns.Add("xm", typeof(string));
        //yhdt.Columns.Add("mm", typeof(string));
        yhdt.Columns.Add("uumzw", typeof(string));
        yhdt.Columns.Add("lxdh", typeof(string));
        //yhdt.Columns.Add("dltime", typeof(DateTime));
        //yhdt.Columns.Add("fwcs", typeof(int));
        DataColumn[] columns=new DataColumn[1];
        columns[0]=yhdt.Columns["yhid"];
        yhdt.PrimaryKey = columns;
        foreach (DataRow m in gt.Rows)
        {
            DataRow dr = yhdt.NewRow();
            dr[0] = m.ItemArray[0];
            dr[1] = m.ItemArray[2];
            dr[2] = m.ItemArray[1]; 
            dr[3] = m.ItemArray[4]; 
            dr[4] = m.ItemArray[8]; 
            yhdt.Rows.Add(dr);
        }
        return yhdt;
    }
    /// <summary>
    /// 把uum得到的数据表转换成uum需要写入数据库的表
    /// </summary>
    /// <param name="gt">uum得到的数据表</param>
    /// <param name="mode">true为返回uum导入需要的table,false为班主任导入需要的table</param>
    /// <returns></returns>
    public DataTable yonghqx(DataTable gt,bool mode)
    {
        DataTable yhdt = new DataTable();
        yhdt.TableName = "yonghqx";
        yhdt.Columns.Add("guid", typeof(string));
        //yhdt.Columns.Add("lsz", typeof(string));
        yhdt.Columns.Add("yhid", typeof(string));
        yhdt.Columns.Add("xm", typeof(string));
        yhdt.Columns.Add("uumzw", typeof(string));
        yhdt.Columns.Add("lxdh", typeof(string));
        yhdt.Columns.Add("yxdm", typeof(string));
        if (mode)
        {
        DataColumn[] columns = new DataColumn[1];
        columns[0] = yhdt.Columns["yhid"];
        yhdt.PrimaryKey = columns;

            foreach (DataRow m in gt.Rows)
            {
                DataRow dr = yhdt.NewRow();
                dr[0] = m.ItemArray[0];
                dr[1] = m.ItemArray[2];
                dr[2] = m.ItemArray[1];
                dr[3] = m.ItemArray[4];
                dr[4] = m.ItemArray[8];
                dr[5] = "";
                yhdt.Rows.Add(dr);
            }
        }
        else
        {
            yhdt.Columns.Add("lsz", typeof(string));
            DataColumn[] columns = new DataColumn[1];
            columns[0] = yhdt.Columns["yhid"];
            yhdt.PrimaryKey = columns;
            DataTable yx = Sqlhelper.Serach("select yxdm,yxmc from dm_yuanxi");
            DataTable lsz = Sqlhelper.Serach("select lsz from yonghqx where yhid='" + gt.Rows[0]["yhid"] + "'");
            string str_lsz = lsz != null ? lsz.Rows[0]["lsz"].ToString() : "";
            foreach (DataRow m in gt.Rows)
            {
                DataRow dr = yhdt.NewRow();
                dr[0] = m.ItemArray[0];
                dr[1] = m.ItemArray[2];
                dr[2] = m.ItemArray[1];
                dr[3] = m.ItemArray[4];
                dr[4] = m.ItemArray[8];
                DataRow[] rs = yx.Select("yxmc='" + m.ItemArray[4] + "'");
                if (rs.Count() > 0)
                {
                    dr[5] = rs[0][0];
                }
                else { dr[5] = "9999"; }
                dr[6] = "6,"+str_lsz;
                yhdt.Rows.Add(dr);
            }
        }
        return yhdt;
    }
    
    #region 写入本地数据库
    public int ToSql()
    {
        DataTable dt;
        try
        {
            dt = getAll();
        }
        catch (Exception)
        {
            return 0;

        }
        string sqlString = "INSERT INTO  yonghqx VALUES(@guid,@yhqx,@lsz,@yhid,@xm,@mm,@uumzw,@lxdh,@dltime,@fwcs)";

        return Sqlhelper.ExcuteNonQuery(sqlString,
                           new SqlParameter("guid", dt.Rows[0][0]),
                           new SqlParameter("yhqx", ""),
                           new SqlParameter("lsz", ""),
                           new SqlParameter("yhid", dt.Rows[0][2]),
                           new SqlParameter("xm", dt.Rows[0][1]),
                           new SqlParameter("mm", ""),
                           new SqlParameter("uumzw", ""),
                           new SqlParameter("lxdh", dt.Rows[0][8]),
                           new SqlParameter("dltime", ""),
                           new SqlParameter("fwcs", ""));
     

    }

    public int UumToAccount()
    {
        DataTable dt;
        try
        {
            dt = getAll();
        }
        catch (Exception)
        {
            return 0;
            
        }
        string sqlString = "INSERT INTO yonghqx VALUES (";
        
        SqlParameter[] parameters=new SqlParameter[9];
        for(int i=0;i<dt.Columns.Count;i++)
        {
        sqlString = "'"+sqlString+"@"+dt.Columns[i].ToString()+"',";
        parameters[i] = new SqlParameter(dt.Columns[i].ToString(), dt.Rows[0][i]);
            //while(i<dt.Columns.Count-1){sqlString = sqlString+",";}
         }
        sqlString = sqlString.Substring(0,sqlString.Length-1)+")";
        try
        {
            return Sqlhelper.ExcuteNonQuery(sqlString, parameters);
        }
        catch (Exception)
        {
            return 0;
           
        }
        
    }
    #endregion


    //根据用户名,查询后返回数据 返回的数据格式如下
    //<Root>
    //  <State>1</State> 
    //  <Users>
    //  <User>
    //  <Id>f3b3d618-a3ad-4dfe-9624-003dcf8281d5</Id> 
    //  <Name>徐丽</Name> 
    //  <UserName>xuli</UserName> 
    //  <IdNumber>512901196807080449</IdNumber> 
    //  <DepartmentName>教学人员</DepartmentName> 
    //  <WorkCode /> 
    //  <Title /> 
    //  <UserType>在职教职工</UserType> 
    //  <MobilePhone /> 
    //  <RootDep>化工电气系</RootDep> 
    //  </User>

    public DataTable getAll()
    {
        UumDataServiceClient client = new UumDataServiceClient();
        string str = client.GetUumData(new Dictionary<string, string> 
                       {
                       {"AuthCode","E97084F7-F7C3-467C-B548-E89E39B2007D"},
                       {"Flag","GetAllHrUser"}
                        });
        //string str1 = client.GetUumData(new Dictionary<string, string> 
        //               {
        //               {"AuthCode","BA4A8F15-2812-4338-A206-45BEE4BEDB43"},
        //               {"Flag","GetUserPath"},
        //               {"UserName","sunyu"}
        //                });
        
        XDocument xml = XDocument.Parse(str);
        foreach (var temp in xml.Elements("Root").Elements("Users").Elements("User"))
        {
            DataRow dr = uumDt.NewRow();
            dr[0] = temp.Element("Id").Value;
            dr[1] = temp.Element("Name").Value;
            dr[2] = temp.Element("UserName").Value;
            dr[3] = temp.Element("IdNumber").Value;
            dr[4] = temp.Element("DepartmentName").Value;
            //dr[4] = temp.Element("RootDep").Value;
            dr[5] = temp.Element("WorkCode").Value;
            dr[6] = temp.Element("Title").Value;
            dr[7] = temp.Element("UserType").Value;
            dr[8] = temp.Element("MobilePhone").Value;

            
            uumDt.Rows.Add(dr);
        }


        Session["UumTotalRows"] = uumDt.Rows.Count;//统计记录数
        return uumDt;
    }
    /// <summary>
    /// 通过用户名得到uum用户数据
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public DataTable getByName(string name)
    {
        UumDataServiceClient client = new UumDataServiceClient();
        string str = client.GetUumData(new Dictionary<string, string> 
                       {
                       {"AuthCode","E97084F7-F7C3-467C-B548-E89E39B2007D"},
                       {"Flag","SearchHrUserByName"},
                       {"Key",name}
                        });


        XDocument xml = XDocument.Parse(str);
        foreach (var temp in xml.Elements("Root").Elements("Users").Elements("User"))
        {
            DataRow dr = uumDt.NewRow();
            dr[0] = temp.Element("Id").Value;
            dr[1] = temp.Element("Name").Value;
            dr[2] = temp.Element("UserName").Value;
            dr[3] = temp.Element("IdNumber").Value;
            dr[4] = temp.Element("RootDep").Value;
            dr[5] = temp.Element("WorkCode").Value;
            dr[6] = temp.Element("Title").Value;
            dr[7] = temp.Element("UserType").Value;
            dr[8] = temp.Element("MobilePhone").Value;


            uumDt.Rows.Add(dr);
        }

        return uumDt;
    }
    /// <summary>
    /// 通过用户名得到uum用户数据
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public DataTable getByName(string name,string yhid)
    {
        UumDataServiceClient client = new UumDataServiceClient();
        string str = client.GetUumData(new Dictionary<string, string> 
                      {
                       {"AuthCode","E97084F7-F7C3-467C-B548-E89E39B2007D"},
                       {"Flag","SearchHrUserByName"},
                       {"Key",name}
                        });
        //取得部门信息
        string dep = client.GetUumData(new Dictionary<string, string> 
                       {
                       {"AuthCode","BA4A8F15-2812-4338-A206-45BEE4BEDB43"},
                       {"Flag","GetUserPath"},
                       {"UserName",yhid}
                        });
        XDocument depXml = XDocument.Parse(dep);
        //取得部门全路径
        string allDep = depXml.Element("Root").Element("s").Element("Data").Element("FullPath").Value;
        //取得部门名称
        string yxDep = allDep.Split('/')[1];
        XDocument xml = XDocument.Parse(str);
        foreach (var temp in xml.Elements("Root").Elements("Users").Elements("User"))
        {
            DataRow dr = uumDt.NewRow();
            dr[0] = temp.Element("Id").Value;
            dr[1] = temp.Element("Name").Value;
            dr[2] = temp.Element("UserName").Value;
            dr[3] = temp.Element("IdNumber").Value;
            dr[4] = yxDep;
            dr[5] = temp.Element("WorkCode").Value;
            dr[6] = temp.Element("Title").Value;
            dr[7] = temp.Element("UserType").Value;
            dr[8] = temp.Element("MobilePhone").Value;


            uumDt.Rows.Add(dr);
        }
        return uumDt.Select("yhid='"+yhid+"'").CopyToDataTable();
        //return uumDt;
    }
    //通过key查询uum数据
    public DataTable getBySearch(String key)
    {

        getAll();
        if (key == null)
        { return uumDt; }
        DataTable searchDt = new DataTable();
        searchDt = uumDt.Clone();
        foreach(DataRow dr in uumDt.Rows)
        {
            string xx = "";
            foreach (var m in dr.ItemArray)
            {
                xx = xx + m.ToString();
                
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(xx, key, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                
                searchDt.ImportRow(dr);
               
            }
            else
            {
                //searchDt.Rows.Remove(dr);
            }
            
        }
        Session["UumTotalRows"] = searchDt.Rows.Count;//统计记录数
        return searchDt;
    }
    /// <summary>
    /// 通过查找uum数据，返回没有设置为班主任的数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public DataTable banzrBySearch(String key)
    {
        DataTable banzr = Sqlhelper.Serach("select guid from banzr");
        getAll();
        //如果班主任表是空的，直接返回所有uum数据
        if (banzr.Rows.Count == 0)
        {
            return uumDt;
        }
        //如果查找框是空的，用班主任表的数据和uum表比较，uum表去掉已是班主任的数据
        if (key == null) 
        {
            foreach (DataRow x in banzr.Rows)
            {
                DataRow[] drs = uumDt.Select("guid='" + x[0].ToString() + "'");
                if (drs.Count() == 0) { continue; }//如果有uum里面没有的数据，如临时班主任，则继续下一循环
               DataRow temp = drs[0];
               uumDt.Rows.Remove(temp);
            }
            return uumDt; }
        //使用查找框的内容，筛选uum数据，同时去掉已存在的班主任数据
        DataTable searchDt = new DataTable();
        searchDt = uumDt.Clone();
        foreach (DataRow dr in uumDt.Rows)
        {
            DataRow[] temp=banzr.Select("guid='" + dr[0].ToString()+"'");
            if (temp.Length == 0)
            {
                string xx = "";
                foreach (var m in dr.ItemArray)
                {
                    xx = xx + m.ToString();

                }
                if (System.Text.RegularExpressions.Regex.IsMatch(xx, key, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {

                    searchDt.ImportRow(dr);

                }
                else
                {
                    //searchDt.Rows.Remove(dr);
                }
            }
        }
        Session["UumTotalRows"] = searchDt.Rows.Count;//统计记录数
        return searchDt;
    }
    /// <summary>
    /// 通过查找uum数据，返回没有添加进学生处管理的人员
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public DataTable UserBySearch(String key)
    {
        DataTable user = Sqlhelper.Serach("select guid from yonghqx where yxdm<>''");
        getAll();
        //如果用户表里yxdm是空的，直接返回所有uum数据
        if (user.Rows.Count == 0)
        {
            return uumDt;
        }
        //如果查找框是空的，用user表的数据和uum表比较，uum表去掉已添加的数据
        if (key == null)
        {
            foreach (DataRow x in user.Rows)
            {
                DataRow[] drs = uumDt.Select("guid='" + x[0].ToString() + "'");
                if (drs.Count() == 0) { continue; }//如果有uum里面没有的数据，如临时班主任，则继续下一循环
                DataRow temp = drs[0];
                uumDt.Rows.Remove(temp);
            }
            return uumDt;
        }
        //使用查找框的内容，筛选uum数据，同时去掉已存在的班主任数据
        DataTable searchDt = new DataTable();
        searchDt = uumDt.Clone();
        foreach (DataRow dr in uumDt.Rows)
        {
            DataRow[] temp = user.Select("guid='" + dr[0].ToString() + "'");
            if (temp.Length == 0)
            {
                string xx = "";
                foreach (var m in dr.ItemArray)
                {
                    xx = xx + m.ToString();

                }
                if (System.Text.RegularExpressions.Regex.IsMatch(xx, key, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {

                    searchDt.ImportRow(dr);

                }
                else
                {
                    //searchDt.Rows.Remove(dr);
                }
            }
        }
        Session["UumTotalRows"] = searchDt.Rows.Count;//统计记录数
        return searchDt;
    }
    //<Root>
    //  <State>1</State>
    //  <Result>5</Result>
    //</Root>
    //返回结果:
    //Result大于0表示成功
    public bool setUumPwd(string userName,string newPwd)
    {
        UumDataServiceClient client = new UumDataServiceClient();
        string _value=string.Empty;
        string str =client.SetUumData(new Dictionary<string, string>()
            {
                {"AuthCode","BA4A8F15-2812-4338-A206-45BEE4BEDB43"},
                {"Flag","SetPassword"},
                {"UserName",userName},
                {"NewPassword",newPwd}
            });
        XDocument xml = XDocument.Parse(str);
        foreach (var temp in xml.Elements("Root"))
        {
            _value = temp.Element("Result").Value;

        }
        int reply=0;
        Int32.TryParse(_value, out reply);
        if(reply>=0)
        {return true;}
        return false;
    }
}
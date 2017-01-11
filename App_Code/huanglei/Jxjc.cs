using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///Jxjc 的摘要说明
/// </summary>
public class Jxjc
{
    
	public Jxjc()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
       
	}

    public DataTable GetWeekHz(string btid)
    {
        DataTable allxm = Sqlhelper.Serach("select id,pjxm from jcb_pjlsbxm where lsbtid='" + btid + "' order by px");
        //Dictionary<string, string> xms = new Dictionary<string, string> { };
        string sqlstr = "select pjbls 表ID";
        string mainstr = " from (SELECT  jcb_biao.pjbls,jcb_biao.btid,jcb_biao.yxid,jcb_biao.pjjl,jcb_biao.pjzt FROM  jcb_biao  where btid='" + btid + "' ) t";
        string addstr = "";
        int i = 1;
        foreach (DataRow x in allxm.Rows)
        {
            sqlstr += ",t" + i.ToString() + ".xmpj '" + x[1].ToString() + "'";
            addstr += " left join jcb_pjjg t" + i + " on t.pjbls=t" + i + ".pjbid and t" + i + ".xmid=" + x[0].ToString();
            i++;
        }
        sqlstr = sqlstr + mainstr + addstr;

        DataTable temp= Sqlhelper.Serach(sqlstr);
         DataTable result = new DataTable();
         result.Columns.Add("系部",typeof(string));
         result.Columns.Add("到课率不足85%的班级（次）", typeof(string));
         result.Columns.Add("课程所属系部入课率不足85%的班级（次）", typeof(string));
         result.Columns.Add("其他情况", typeof(string));
         result.Columns.Add("教学中存在问题的次数合计", typeof(string));
         result.Columns.Add("周次", typeof(string));
        Dictionary<string,int> col1=new Dictionary<string,int>();
        Dictionary<string,int> col2=new Dictionary<string,int>();
        Dictionary<string,int> col3=new Dictionary<string,int>();
        Dictionary<string,int> col4=new Dictionary<string,int>();
        DataTable yx=Sqlhelper.Serach("select distinct xmpj from [jcb_PJJG] a left join jcb_pjlsbxm b on a.xmid=b.id  where b.pjxm='系部' and b.lsbtid='"+btid+"'");
        foreach(DataRow x in yx.Rows)
        {
            col1.Add(x["xmpj"].ToString(),0);
            col2.Add(x["xmpj"].ToString(),0);
            col3.Add(x["xmpj"].ToString(),0);
            col4.Add(x["xmpj"].ToString(),0);
        }
         foreach (DataRow x in temp.Rows)
         {
             float dkl=1;
             float rkl=1;
             int cs=0;
             string xb= x["系部"].ToString();
             try
             {
                 dkl = Convert.ToSingle(x["到课人数"]) / Convert.ToSingle(x["应到人数"]);
                 rkl = Convert.ToSingle(x["入课人数"]) / Convert.ToSingle(x["到课人数"]);
                 
             }
             catch (Exception)
             {

             }
             if (dkl < 0.85) { col1[xb]++; cs++; }
             if (rkl < 0.85) { col2[xb]++; cs++; }
             if(!string.IsNullOrEmpty(x["教师教学情况"].ToString())){col3[xb]++;cs++;}
             col4[xb]+=cs;
         }
         DataTable zhouc = Sqlhelper.Serach("select zhouc from jcb_lsbiaotou where id='"+btid+"'");
        string zhouc_str="";
        if (zhouc.Rows.Count > 0) { zhouc_str = zhouc.Rows[0][0].ToString(); }
         foreach (DataRow x in yx.Rows)
        {
            string xb = x["xmpj"].ToString();
            DataRow dr = result.NewRow();
             dr[0] = xb;
             dr[1] = col1[xb].ToString();
             dr[2] = col2[xb].ToString();
             dr[3] = col3[xb].ToString();
             dr[4] = col4[xb].ToString();
             dr[5] = zhouc_str;
             result.Rows.Add(dr);
        }
         return result;
    }
    
    public DataTable GetXueqHz(string xueq)
    {
        DataTable result = new DataTable();
        result.Columns.Add("系部", typeof(string));
        result.Columns.Add("到课率不足85%的班级（次）", typeof(string));
        result.Columns.Add("课程所属系部入课率不足85%的班级（次）", typeof(string));
        result.Columns.Add("其他情况", typeof(string));
        result.Columns.Add("教学中存在问题的次数合计", typeof(string));
        result.Columns.Add("周次", typeof(string));
        DataTable allxm = Sqlhelper.Serach("select  id from [JCB_lsbiaotou] where title='教学情况检查表' and xueq='" + xueq + "'");
        foreach (DataRow x in allxm.Rows)
        {
            result.Merge(GetWeekHz(x["id"].ToString()));
        }
        return result;
    }
}
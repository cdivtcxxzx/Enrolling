using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public class Degree_obj{
    public int id { get; set; }
   public string yhid{get;set;}
   public string btid { get; set; }
   public string degree { get; set; }
   public float jg { get; set; }
   public string updater_id { get; set; }
   public System.DateTime update_dt { get; set; }
   public string final { get; set; }
   public string final_tip { get; set; }
   public string final_id { get; set; }
   public System.DateTime final_dt { get; set; }
public Degree_obj(){}

}
/// <summary>
///Degree 的摘要说明
/// </summary>
public class Degree
{
	public Degree()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 获取学期汇总结果sql语句
    /// </summary>
    /// <param name="guid">汇总表头guid</param>
    /// <param name="nd">年度：2013</param>
    /// <param name="lx">评价表类型名称:教学工作考核</param>
    /// <param name="xueq">学期:2013-2014年第一学期</param>
    /// <param name="yxdm">评价部门代码</param>
    /// <param name="zf">项目总分</param>
    /// <param name="qz">项目权重</param>
    /// <returns></returns>
    public string XueqCollectionStr(string guid,string nd,string lx,string xueq,string yxdm,float qz )
    {
        float zf = GetZf( nd, lx, yxdm);
        List<string> btid = GetBtidByLxmc(lx, nd, yxdm);
        string temp = "''";
        foreach (string x in btid)
        {
            temp+=",'"+x+"'";
        }
        string sqlstr = "select '" + guid + "' guid,yhid,xm,a.yxdm,yxmc,'system' pjr,isnull(avg(汇总),0) jg," + zf + " zf," + qz + " qz,'" + lx + "' xmmc from (select yhid,xm,z.yxdm,yxmc,btid,SUM(xmpj) 汇总 from (select yhid,xm,yxdm,btid,pjxm,round(avg(cast(case when xmlx='手动文本项' then null else xmpj end as real)),2) xmpj from pjb_pjjg left join pjb_biao on PJB_PJJG.pjbid=PJB_biao.pjbls left join PJB_PJLSBXM on PJB_PJJG.xmid=PJB_PJLSBXM.id left join yonghqx on pjb_biao.bpjr=yonghqx.yhid where xmpj<>'' and yonghqx.pjbmdm='" + yxdm + "' and pjbid in (select pjbls from PJB_biao where  btid in  (" + temp + ")) group by pjxm,btid,bpjr,xm,yhid,yxdm) z left join DM_YUANXI yx on z.yxdm=yx.YXDM  group by yhid,xm,btid,z.yxdm,yxmc) a left join pjb_lsbiaotou b on a.btid=b.id where b.xueq='" + xueq + "' and  b.pjzq='学期'  group by yhid,xm,a.yxdm,yxmc";
        return sqlstr;
    }
    /// <summary>
    /// 获取项目总分
    /// </summary>
    /// <param name="nd">年度</param>
    /// <param name="lx">评价表类型名称:教学工作考核</param>
    /// <param name="yxdm">院系代码</param>
    /// <returns></returns>
    public float GetZf(string nd, string lx, string yxdm)
    {
        try
        {
            DataTable pjbls = Sqlhelper.Serach("select zf from pjb_lsbiaotou where lx='" + lx + "' and nd='" + nd + "' and yxdm='" + yxdm + "'");
            return Convert.ToSingle(pjbls.Rows[0]["zf"]);
        }
        catch (Exception)
        {
            return 0;
        }
    }
    /// <summary>
    /// 获取年度汇总结果sql语句
    /// </summary>
    /// <param name="guid">汇总表头guid</param>
    /// <param name="nd">年度：2013</param>
    /// <param name="lx">评价表类型名称:教学工作考核</param>
    /// <param name="yxdm">评价部门代码</param>
    /// <param name="zf">项目总分</param>
    /// <param name="qz">项目权重</param>
    /// <returns></returns>
    public string YearCollectionStr(string guid, string nd, string lx, string yxdm,  float qz)
    {
        List<string> btid = GetBtidByLxmc(lx,nd,yxdm);
        float zf = GetZf(nd, lx, yxdm);
        string temp = "''";
        foreach (string x in btid)
        {
            temp += ",'" + x + "'";
        }
        string sqlstr = "select '" + guid + "' guid,yhid,xm,a.yxdm,yxmc,'system' pjr,isnull(avg(汇总),0) jg," + zf + " zf," + qz + " qz,'" + lx + "' xmmc from (select yhid,xm,z.yxdm,yxmc,btid,sum(xmpj) 汇总 from (select yhid,xm,yxdm,btid,pjxm,round(avg(cast(xmpj as real)),2) xmpj from pjb_pjjg left join pjb_biao on PJB_PJJG.pjbid=PJB_biao.pjbls left join PJB_PJLSBXM on PJB_PJJG.xmid=PJB_PJLSBXM.id left join yonghqx on pjb_biao.bpjr=yonghqx.yhid where xmpj<>'' and yonghqx.pjbmdm='" + yxdm + "' and pjbid in (select pjbls from PJB_biao where  btid in  (" + temp + ")) group by pjxm,btid,bpjr,xm,yhid,yxdm) z left join DM_YUANXI yx on z.yxdm=yx.YXDM  group by yhid,xm,btid,z.yxdm,yxmc) a left join pjb_lsbiaotou b on a.btid=b.id where b.nd='" + nd + "' and b.pjzq='年度' and b.lx='" + lx + "' group by yhid,xm,a.yxdm,yxmc";
        return sqlstr;
    }
    /// <summary>
    /// 更新等级认定
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool DegreeUpdate( Degree_obj obj)
    {
        try
        {
            if (Sqlhelper.Serach("select btid from pjhz_dj where btid='" + obj.btid + "' and yhid='" + obj.yhid + "'").Rows.Count > 0)
            {
                Sqlhelper.ExcuteNonQuery("update pjhz_dj set jg=" + obj.jg + ",degree='" + obj.degree + "',updater_id='" + obj.updater_id + "',update_dt=@dt where btid='" + obj.btid + "' and yhid='" + obj.yhid + "'", new SqlParameter("dt", System.DateTime.Now));
                return true;
            }
            else
            {
                Sqlhelper.ExcuteNonQuery("insert into pjhz_dj (btid,yhid,jg,degree,updater_id,update_dt) values ('" + obj.btid + "','" + obj.yhid + "'," + obj.jg + ",'" + obj.degree + "','" + obj.updater_id + "',@dt)", new SqlParameter("dt", System.DateTime.Now));
                return true;
            }
        }
        catch (Exception)
        {

            return false;
        }
    }
    /// <summary>
    /// 更新最终等级认定
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool FinalDegreeUpdate(Degree_obj obj)
    {
        try
        {
            if (Sqlhelper.ExcuteNonQuery("update pjhz_dj set final='" + obj.final + "',final_tip='" + obj.final_tip + "',final_id='" + obj.final_id + "',final_dt='" + obj.final_dt + "' where id=" + obj.id) > 0)
                return true;
            else return false;
        }
        catch (Exception)
        {

            return false;
        }
    }
    /// <summary>
    /// 最终认定时，自动通过全部评价
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool FinalDegreeAllPass(string nd,string xueq,string yxmc,string final_id,System.DateTime final_dt)
    {
        
        try
        {
            string btid="''";
            DataTable dt;
            if (xueq != "")
            {
                dt = Sqlhelper.Serach("select id from pjhz_biaotou where nd=@nd and xueq=@xueq and yxmc=@yxmc", new SqlParameter("nd", nd), new SqlParameter("xueq", xueq), new SqlParameter("yxmc", yxmc));
            }
            else
            {
                dt = Sqlhelper.Serach("select id from pjhz_biaotou where nd=@nd and yxmc=@yxmc", new SqlParameter("nd", nd), new SqlParameter("yxmc", yxmc));
            }
            foreach (DataRow x in dt.Rows)
            {
                btid += ",'"+x["id"].ToString()+"'";
            }
            if (Sqlhelper.ExcuteNonQuery("update pjhz_dj set final=a.degree,final_id='" + final_id + "',final_dt='" + final_dt + "' from (select id,degree from [pjhz_dj]) a where pjhz_dj.id=a.id and btid in (" + btid + ")") > 0)
                return true;
            else return false;
        }
        catch (Exception)
        {

            return false;
        }
    }
    /// <summary>
    /// 等级评定汇总sql语句
    /// </summary>
    /// <param name="btid">汇总的表头id</param>
    /// <returns></returns>
    public string DegreeStr(string btid)
    {
        DataTable allxm = Sqlhelper.Serach("select distinct xmmc from [pjhz_jg] where btid='" + btid + "'");
        string sqlstr = "select yhid 用户ID,xm 被评价人";
        string pivotstr = "";
        string addstr = "";
        int i = 1;
        foreach (DataRow x in allxm.Rows)
        {
            addstr += ",[" + x["xmmc"].ToString() + "]";
            i++;
        }

        string mainstr = " from (select yhid,xm,btid,xmmc,round(avg(cast(jg as real)),2) jg from pjhz_jg  where jg is not null and  btid='" + btid + "' group by xmmc,btid,yhid,xm) t ";
        if (i > 1) { addstr += ",[汇总]"; pivotstr = "pivot (avg(jg) for xmmc in (" + addstr.TrimStart(',') + ") ) p"; mainstr = " from ((select yhid,xm,btid,xmmc,round(avg(cast(jg as real)),2) jg from pjhz_jg  where jg is not null and  btid='" + btid + "' group by xmmc,btid,yhid,xm)  union (select yhid,xm,btid,'汇总',round(SUM((jg*100*qz)/zf),2) from pjhz_jg  where jg is not null and  btid='" + btid + "' group by yhid,xm,btid)) t "; }
        sqlstr = sqlstr + addstr+",'' 等级评定" + mainstr + pivotstr;
        return sqlstr;
    }
    /// <summary>
    /// 等级评定汇总datatable
    /// </summary>
    /// <param name="btid">表头id</param>
    /// <returns></returns>
    public DataTable DegreeTable(string btid)
    {
        return Sqlhelper.Serach(DegreeStr(btid));
    }

    /// <summary>
    /// 通过关键字查询最终等级评定
    /// </summary>
    /// <param name="nd">年度</param>
    /// <param name="xueq">学期</param>
    /// <param name="key">用户id、姓名或评价部门名称关键字</param>
    /// <returns></returns>
    public DataTable FinalDegreeTable(string nd, string xueq,string yxmc,string key)
    {
        if (xueq != null)
        {
            return Sqlhelper.Serach("select row_number() over (order by a.yhid) as id,a.id lsid,a.yhid,xm,jg,degree,final,final_tip,updater_id,update_dt from pjhz_dj a left join yonghqx c on a.yhid=c.yhid left join pjhz_biaotou b on a.btid=b.id left join dm_yuanxi d on c.pjbmdm=d.yxdm where nd=@nd and xueq=@xueq and b.yxmc=@yxmc and pjzq='学期' and (a.yhid like '%" + key + "%' or xm like '%" + key + "%' ) order by a.yhid", new SqlParameter("nd", nd), new SqlParameter("xueq", xueq), new SqlParameter("yxmc", yxmc));
        }
        else
        {
            return Sqlhelper.Serach("select row_number() over (order by a.yhid) as id,a.id lsid,a.yhid,xm,jg,degree,final,final_tip,updater_id,update_dt from pjhz_dj a left join yonghqx c on a.yhid=c.yhid left join pjhz_biaotou b on a.btid=b.id left join dm_yuanxi d on c.pjbmdm=d.yxdm where nd=@nd and b.yxmc=@yxmc and pjzq='年度' and (a.yhid like '%" + key + "%' or xm like '%" + key + "%'  ) order by a.yhid", new SqlParameter("nd", nd), new SqlParameter("yxmc", yxmc));
        }
    }
    /// <summary>
    /// 最终等级评定查询
    /// </summary>
    /// <param name="nd">年度</param>
    /// <param name="xueq">学期</param>
    /// <returns></returns>
    public DataTable FinalDegreeTable(string nd,string xueq,string yxmc)
    {
        if (xueq != null)
        {
            return Sqlhelper.Serach("select row_number() over (order by a.yhid) as id,a.id lsid,a.yhid,xm,jg,degree,final,final_tip,updater_id,update_dt from pjhz_dj a left join yonghqx c on a.yhid=c.yhid left join pjhz_biaotou b on a.btid=b.id where nd=@nd and xueq=@xueq and b.yxmc=@yxmc and pjzq='学期'  order by a.yhid", new SqlParameter("nd", nd), new SqlParameter("xueq", xueq), new SqlParameter("yxmc", yxmc));
        }
        else
        {
            return Sqlhelper.Serach("select row_number() over (order by a.yhid) as id,a.id lsid,a.yhid,xm,jg,degree,final,final_tip,updater_id,update_dt from pjhz_dj a left join yonghqx c on a.yhid=c.yhid left join pjhz_biaotou b on a.btid=b.id where nd=@nd and b.yxmc=@yxmc and pjzq='年度'  order by a.yhid", new SqlParameter("nd", nd), new SqlParameter("yxmc", yxmc));
        }
    }
    /// <summary>
    /// 获取相应评价表的汇总结果sql语句
    /// </summary>
    /// <param name="btid">流水表头ID：lsbiaotouid</param>
    /// <returns></returns>
    public string TongjStr(string btid)
    {
        DataTable allxm = Sqlhelper.Serach("select id,pjxm from pjb_pjlsbxm where lsbtid='" + btid + "' order by px");
        string sqlstr = "select bpjr 用户ID,xm 被评价人";
        string pivotstr = "";
        string addstr = "";
        int i = 1;
        foreach (DataRow x in allxm.Rows)
        {
            addstr += ",[" + x[1].ToString() + "]";
            i++;
        }

        string mainstr = " from (select bpjr,xm,btid,pjxm,round(avg(cast(xmpj as real)),2) xmpj from pjb_pjjg left join pjb_biao on PJB_PJJG.pjbid=PJB_biao.pjbls left join PJB_PJLSBXM on PJB_PJJG.xmid=PJB_PJLSBXM.id left join yonghqx on pjb_biao.bpjr=yonghqx.yhid where xmpj<>'' and pjbid in (select pjbls from PJB_biao where btid='" + btid + "') group by pjxm,btid,bpjr,xm) t ";
        if (i > 1) { addstr += ",[汇总]"; pivotstr = "pivot (avg(xmpj) for pjxm in (" + addstr.TrimStart(',') + ") ) p"; mainstr = " from ((select bpjr,xm,btid,pjxm,round(avg(cast(xmpj as real)),2) xmpj from pjb_pjjg left join pjb_biao on PJB_PJJG.pjbid=PJB_biao.pjbls left join PJB_PJLSBXM on PJB_PJJG.xmid=PJB_PJLSBXM.id left join yonghqx on pjb_biao.bpjr=yonghqx.yhid where xmpj<>'' and pjbid in (select pjbls from PJB_biao where btid='" + btid + "') group by pjxm,btid,bpjr,xm) union (select bpjr,xm,btid,'汇总',SUM(xmpj) from (select bpjr,xm,btid,pjxm,round(avg(cast(xmpj as real)),2) xmpj from pjb_pjjg left join pjb_biao on PJB_PJJG.pjbid=PJB_biao.pjbls left join PJB_PJLSBXM on PJB_PJJG.xmid=PJB_PJLSBXM.id left join yonghqx on pjb_biao.bpjr=yonghqx.yhid where xmpj<>'' and pjbid in (select pjbls from PJB_biao where btid='" + btid + "') group by pjxm,btid,bpjr,xm) z group by bpjr,xm,btid)) t "; }
        sqlstr = sqlstr + addstr + mainstr + pivotstr;
        return sqlstr;
    }
    /// <summary>
    /// 获取相应评价表的汇总结果，并对被评价人进行平均,sql语句
    /// </summary>
    /// <param name="btid">流水表头ID：lsbiaotouid</param>
    /// <returns></returns>
    public string AvgTongjStr(string btid)
    {
        DataTable allxm = Sqlhelper.Serach("select id,pjxm from pjb_pjlsbxm where lsbtid='" + btid + "' order by px");
        string sqlstr = "";
        string pivotstr = "";
        string addstr = "";
        string avgstr = "select bpjr ";
        int i = 1;
        foreach (DataRow x in allxm.Rows)
        {
            addstr += ",[" + x[1].ToString() + "]";
            avgstr += ",avg([" + x[1].ToString() + "]) as [" + x[1].ToString() + "]";
            i++;
        }

        string mainstr = " from (select xm,btid,pjxm,round(avg(cast(xmpj as real)),2) xmpj from pjb_pjjg left join pjb_biao on PJB_PJJG.pjbid=PJB_biao.pjbls left join PJB_PJLSBXM on PJB_PJJG.xmid=PJB_PJLSBXM.id left join yonghqx on pjb_biao.bpjr=yonghqx.yhid where xmpj<>'' and pjbid in (select pjbls from PJB_biao where btid='" + btid + "') group by pjxm,btid,bpjr,xm) t ";
        if (i > 1)
        {
            
            addstr += ",[汇总]";
            sqlstr = "select a.xm 被评价人 " + addstr+" from (";
            pivotstr = "pivot (avg(xmpj) for pjxm in (" + addstr.TrimStart(',') + ") ) p  group by bpjr) z left join yonghqx a on a.yhid=z.bpjr";
            mainstr = " from ((select bpjr,btid,pjxm,round(avg(cast(xmpj as real)),2) xmpj from pjb_pjjg left join pjb_biao on PJB_PJJG.pjbid=PJB_biao.pjbls left join PJB_PJLSBXM on PJB_PJJG.xmid=PJB_PJLSBXM.id  where xmpj<>'' and pjbid in (select pjbls from PJB_biao where btid='" + btid + "') group by pjxm,btid,bpjr) union (select bpjr,btid,'汇总',SUM(xmpj) from (select bpjr,btid,pjxm,round(avg(cast(xmpj as real)),2) xmpj from pjb_pjjg left join pjb_biao on PJB_PJJG.pjbid=PJB_biao.pjbls left join PJB_PJLSBXM on PJB_PJJG.xmid=PJB_PJLSBXM.id where xmpj<>'' and pjbid in (select pjbls from PJB_biao where btid='" + btid + "') group by pjxm,btid,bpjr) z group by bpjr,btid)) t ";
            sqlstr = sqlstr+avgstr + ",avg([汇总]) as [汇总]" + mainstr + pivotstr;
        }
        else { sqlstr = "select xm 被评价人"; sqlstr = sqlstr + addstr + mainstr + pivotstr; }
        return sqlstr;
    }
        /// <summary>
    /// 通过表类型名称获取相应流水表id的集合
    /// </summary>
    /// <param name="lxmc">类型名称</param>
    /// <param name="nd">年度</param>
    /// <param name="yxdm">院系代码</param>
    /// <returns></returns>
    public List<string> GetBtidByLxmc(string lxmc,string nd,string yxdm)
    {
        DataTable pjbls = Sqlhelper.Serach("select id from pjb_lsbiaotou where lx='" + lxmc + "' and nd='"+nd+"' and yxdm='"+yxdm+"'");
        List<string> btid = new List<string> { };
        foreach (DataRow x in pjbls.Rows)
        {
            btid.Add(x["id"].ToString());
        }
        return btid;
    }
    /// <summary>
    /// 通过评价表的标题获取相应流水表id的集合
    /// </summary>
    /// <param name="title">标题</param>
    /// <returns></returns>
    public List<string> GetBtidByTitle(string title)
    {
        DataTable pjbls = Sqlhelper.Serach("select id from pjb_lsbiaotou where title='"+title+"'");
        List<string> btid = new List<string> { };
        foreach (DataRow x in pjbls.Rows)
        {
            btid.Add(x["id"].ToString());
        }
        return btid;
    }
    /// <summary>
    /// 生成相应学期字符串
    /// </summary>
    /// <returns></returns>
    public string getXueQi()
    {
        DateTime dt = System.DateTime.Now;
        if (dt.Month <= 7)
        { return (dt.Year - 1).ToString() + "-" + dt.Year.ToString() + "年第二学期"; }
        else
        {return dt.Year.ToString() + "-" + (dt.Year + 1).ToString() + "年第一学期";}
    }
    /// <summary>
    /// 加权平均
    /// </summary>
    /// <param name="num">项目个数</param>
    /// <param name="w">权重数组</param>
    /// <param name="a">分值数组</param>
    /// <returns></returns>
    public float WeightAverage(int num,float[] w,float[] a)
    {
        if (w.Length < 1 || a.Length < 1) { return 0; }
        float ans=0;
        float scores=0;
        float weights=0;
        for (int i=0;i<num;i++)
        {
            weights += w[i];
            scores += w[i]*a[i];
        }
        ans = scores / weights;
        return ans;
    }
    /// <summary>
    /// 通过评价历史表头和被评价人得到所有的评价状态并分析后显示
    /// </summary>
    /// <param name="bpjr">被评价人</param>
    /// <param name="btid">历史表头ID</param>
    /// <returns>整体评价状态</returns>
    public string getpjzt(string bpjr, string btid)
    {
        if (bpjr != "" && btid != "")
        {
            DataTable pjztb = Sqlhelper.Serach("SELECT pjzt FROM PJB_biao where btid=" + btid + " and bpjr='" + bpjr + "'");
            string pjzt = "";
            string pjztretu = "未评价";
            if (pjztb.Rows.Count > 0)
            {
                for (int i = 0; i < pjztb.Rows.Count; i++)
                {
                    pjzt += pjztb.Rows[i]["pjzt"].ToString();
                }
                if (pjzt.Contains("未评价") && pjzt.Contains("已评价"))
                {
                    pjztretu = "部分评价";
                }
                if (pjzt.Contains("未评价") && !pjzt.Contains("已评价"))
                {
                    pjztretu = "<font color=red>未评价</font>";
                }
                if (!pjzt.Contains("未评价") && pjzt.Contains("已评价"))
                {
                    pjztretu = "<font color=green>完成评价</font>";
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
    /// 通过评价历史表头得到所有的评价状态并分析后显示
    /// </summary>
    /// <param name="btid">历史表头ID</param>
    /// <returns>整体评价状态</returns>
  

    /// <summary>
    /// 通过评价历史表头得到所有的评价状态并分析后显示
    /// </summary>
    /// <param name="btid">历史表头ID</param>
    /// <returns>整体评价状态</returns>
    public string getddbzt(string btid)
    {
        if (btid != "")
        {
            DataTable pjztb = Sqlhelper.Serach("SELECT pjzt FROM JCB_biao where btid='" + btid + "'");
            string pjzt = "";
            string pjztretu = "未填写";
            if (pjztb.Rows.Count > 0)
            {
                for (int i = 0; i < pjztb.Rows.Count; i++)
                {
                    pjzt += pjztb.Rows[i]["pjzt"].ToString();
                }
                if (pjzt.Contains("未填写") && pjzt.Contains("已填写"))
                {
                    pjztretu = "部分填写";
                }
                if (pjzt.Contains("未填写") && !pjzt.Contains("已填写"))
                {
                    pjztretu = "<font color=red>未填写</font>";
                }
                if (!pjzt.Contains("未填写") && pjzt.Contains("已填写"))
                {
                    pjztretu = "<font color=green>完成填写</font>";
                }
                return pjztretu;
            }
            else
            {
                return "<font color=red>无填写记录</font>";
            }
        }
        else
        {
            return "<font color=red>参数错误</font>";
        }
    }
    /// <summary>
    /// 通过评价历史表头得到所有的评价状态并分析后显示
    /// </summary>
    /// <param name="btid">历史表头ID</param>
    /// <returns>整体评价状态</returns>
    public string getfkbzt(string btid)
    {
        if (btid != "")
        {
            DataTable pjztb = Sqlhelper.Serach("SELECT pjzt FROM FKB_biao where btid='" + btid + "'");
            string pjzt = "";
            string pjztretu = "未填写";
            if (pjztb.Rows.Count > 0)
            {
                for (int i = 0; i < pjztb.Rows.Count; i++)
                {
                    pjzt += pjztb.Rows[i]["pjzt"].ToString();
                }
                if (pjzt.Contains("未填写") && pjzt.Contains("已填写"))
                {
                    pjztretu = "部分填写";
                }
                if (pjzt.Contains("未填写") && !pjzt.Contains("已填写"))
                {
                    pjztretu = "<font color=red>未填写</font>";
                }
                if (!pjzt.Contains("未填写") && pjzt.Contains("已填写"))
                {
                    pjztretu = "<font color=green>完成填写</font>";
                }
                return pjztretu;
            }
            else
            {
                return "<font color=red>无填写记录</font>";
            }
        }
        else
        {
            return "<font color=red>参数错误</font>";
        }
    }

    /// <summary>
    /// 通过评价历史表头得到所有的评价状态并分析后显示
    /// </summary>
    /// <param name="btid">历史表头ID</param>
    /// <returns>整体评价状态</returns>
    public string getpjbzt(string btid)
    {
        if (btid != "")
        {
            DataTable pjztb = Sqlhelper.Serach("SELECT pjzt FROM PJB_biao where btid='" + btid+"'");
            string pjzt = "";
            string pjztretu = "未评价";
            if (pjztb.Rows.Count > 0)
            {
                for (int i = 0; i < pjztb.Rows.Count; i++)
                {
                    pjzt += pjztb.Rows[i]["pjzt"].ToString();
                }
                if (pjzt.Contains("未评价") && pjzt.Contains("已评价"))
                {
                    pjztretu = "部分评价";
                }
                if (pjzt.Contains("未评价") && !pjzt.Contains("已评价"))
                {
                    pjztretu = "<font color=red>未评价</font>";
                }
                if (!pjzt.Contains("未评价") && pjzt.Contains("已评价"))
                {
                    pjztretu = "<font color=green>完成评价</font>";
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
}
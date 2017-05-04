using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// GZJW 的摘要说明
/// </summary>
public class GZJW
{
	public GZJW()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public bool SyncClass()
    {
        string year = DateTime.Today.Year.ToString();
        year = "2016";
        DataTable dt = Sqlhelper.ConSerach(Sqlhelper.conStrzsgl, "select cl.bh PK_Class_NO,cl.xqdm FK_Campus_NO,spe.pk_spe FK_SPE_NO,zy.SPE_Code,cl.bjmc Name from gzjw.[PantoSchoolGJ].[dbo].[JX_Class] cl left join enrollment.yxxt_data.dbo.Base_SPE_ZYDM zy on cl.zydm=zy.zydm left join enrollment.yxxt_data.dbo.Fresh_SPE spe on zy.spe_code=spe.spe_code left join enrollment.yxxt_data.dbo.Fresh_Counseller coun on cl.bh=coun.fk_class_no  where cl.njdm=@njdm and spe.[year]=@njdm", new SqlParameter("njdm", year));
        DataSet tempDs = new DataSet();
        DataTable temp = dt.Clone();
        temp.TableName = "Fresh_Class";
        DataColumn[] columns = new DataColumn[1];
        columns[0] = temp.Columns["PK_Class_NO"];
        temp.PrimaryKey = columns;
        foreach (DataRow dr in dt.Rows)
        {
            DataRow x = temp.NewRow();
            x["PK_Class_NO"] = dr["PK_Class_NO"].ToString();
            x["FK_Campus_NO"] = dr["FK_Campus_NO"].ToString();
            x["FK_SPE_NO"] = dr["FK_SPE_NO"].ToString();
            x["Name"] = dr["Name"].ToString();
            temp.Rows.Add(x);
        }
        tempDs.Tables.Add(temp);
        Sqlhelper.GetUpdateDataSet("select PK_Class_NO,FK_Campus_NO,FK_SPE_NO,Name from Fresh_Class", tempDs, "Fresh_Class");
        //Sqlhelper.ExcuteNonQuery("delete FROM [dbo].[Fresh_Class] where PK_Class_NO in ( select PK_Class_NO from Fresh_Class left join Fresh_SPE on Fresh_Class.FK_SPE_NO=Fresh_SPE.PK_SPE where Fresh_SPE.[Year]=@njdm)", new SqlParameter("njdm", year));
        //DataTable Fresh_Class = new DataTable("Fresh_Class");
        //Fresh_Class.Columns.Add("PK_Class_NO", typeof(string));
        //Fresh_Class.Columns.Add("FK_Campus_NO", typeof(string));
        //Fresh_Class.Columns.Add("FK_SPE_NO", typeof(string));
        //Fresh_Class.Columns.Add("Name", typeof(string));
        //foreach (DataRow dr in dt.Rows)
        //{
        //    DataRow x = Fresh_Class.NewRow();
        //    x["PK_Class_NO"] = dr["PK_Class_NO"].ToString();
        //    x["FK_Campus_NO"] = dr["FK_Campus_NO"].ToString();
        //    x["FK_SPE_NO"] = dr["FK_SPE_NO"].ToString();
        //    x["Name"] = dr["Name"].ToString();
        //    Fresh_Class.Rows.Add(x);
        //}
        //SqlBulkCopyColumnMapping[] mapping = new SqlBulkCopyColumnMapping[4];
        //mapping[0] = new SqlBulkCopyColumnMapping("PK_Class_NO", "PK_Class_NO");
        //mapping[1] = new SqlBulkCopyColumnMapping("FK_Campus_NO", "FK_Campus_NO");
        //mapping[2] = new SqlBulkCopyColumnMapping("FK_SPE_NO", "FK_SPE_NO");
        //mapping[3] = new SqlBulkCopyColumnMapping("Name", "Name");
        //Sqlhelper.BulkInsert(Fresh_Class, "Fresh_Class", mapping);
        return false;
    }
    public DataTable GetFilterClassDT()
    {
        DataTable dt = GetClassDT();
        string filter = new Power().GetFilterExpressionYX("College_Name");
        if (dt.Rows.Count > 0)
        {
            DataRow[] drs = dt.Select(filter);
            if (drs.Count() > 0)
            { return drs.CopyToDataTable(); }
            return null;
        }
        return null;
    }

    /// <summary>
    /// 获取班级信息，包括班主任信息
    /// </summary>
    /// <returns></returns>
    public DataTable GetClassDT()
    {
        string year = DateTime.Today.Year.ToString();
        year = "2016";
        return Sqlhelper.Serach("select cl.PK_Class_NO,spe.pk_spe,col.College_NO,col.Name College_Name,cl.name,yh.yhid,yh.xm,coun.Phone,coun.QQ from [Fresh_Class] cl left join Fresh_SPE spe on cl.FK_SPE_NO=spe.PK_SPE left join Base_College col on spe.FK_College_Code=col.PK_College left join Fresh_Counseller coun on cl.PK_Class_NO=coun.fk_class_no left join yonghqx yh on coun.fk_staff_no=yh.yhid where spe.[year]=@njdm", new SqlParameter("njdm", year));
    }
    public DataTable GetClassOutDT()
    {
        string year = DateTime.Today.Year.ToString();
        year = "2016";
        return Sqlhelper.Serach("select cl.PK_Class_NO 班号,cl.name 班级名称,yh.yhid 辅导员帐号,yh.xm 辅导员姓名,coun.Phone 辅导员电话,coun.QQ 辅导员QQ号 from [Fresh_Class] cl left join Fresh_SPE spe on cl.FK_SPE_NO=spe.PK_SPE left join Base_College col on spe.FK_College_Code=col.PK_College left join Fresh_Counseller coun on cl.PK_Class_NO=coun.fk_class_no left join yonghqx yh on coun.fk_staff_no=yh.yhid where spe.[year]=@njdm", new SqlParameter("njdm", year));
    }
    public bool SetCounseller(string PK_Class_NO,string yhid,string phone,string qq)
    {
        DataTable dt_class = Sqlhelper.Serach("select PK_Class_NO from Fresh_Class where PK_Class_NO=@PK_Class_NO", new SqlParameter("PK_Class_NO", PK_Class_NO));
        if(dt_class.Rows.Count!=1)
        {
            return false;
        }
        DataTable dt = Sqlhelper.Serach("select FK_Class_NO from Fresh_Counseller where FK_Class_NO=@FK_Class_NO", new SqlParameter("FK_Class_NO", PK_Class_NO));
        if(dt.Rows.Count>0)
        {
            if (Sqlhelper.ExcuteNonQuery("update Fresh_Counseller set FK_Staff_NO=@FK_Staff_NO,Phone=@Phone,QQ=@QQ where FK_Class_NO=@FK_Class_NO", new SqlParameter("FK_Class_NO", PK_Class_NO), new SqlParameter("FK_Staff_NO", yhid), new SqlParameter("Phone", phone), new SqlParameter("QQ", qq)) == 1)
            { return true; }
            return false;
        }
        else 
        {
            string guid = Guid.NewGuid().ToString();
            if (Sqlhelper.ExcuteNonQuery("insert into Fresh_Counseller (PK_Counseller_NO,FK_Class_NO,FK_Staff_NO,Phone,QQ) values (@PK_Counseller_NO,@FK_Class_NO,@FK_Staff_NO,@Phone,@QQ)", new SqlParameter("PK_Counseller_NO", guid), new SqlParameter("FK_Class_NO", PK_Class_NO), new SqlParameter("FK_Staff_NO", yhid), new SqlParameter("Phone", phone), new SqlParameter("QQ", qq)) == 1)
            { return true; }
            return false;
        }
        return false;
    }
    public DataTable GetClass(string PK_Class_NO)
    {
        return Sqlhelper.Serach("select PK_Counseller_NO,FK_Class_NO,FK_Staff_NO,yonghqx.xm,Fresh_Counseller.Phone,Fresh_Counseller.QQ from Fresh_Counseller left join yonghqx on Fresh_Counseller.FK_Staff_NO=yonghqx.yhid where FK_Class_NO=@FK_Class_NO", new SqlParameter("FK_Class_NO", PK_Class_NO));
    }
    public string GetClassName(string PK_Class_NO)
    {
        DataTable dt= Sqlhelper.Serach("select name from Fresh_Class where PK_Class_NO=@PK_Class_NO", new SqlParameter("PK_Class_NO", PK_Class_NO));
        if(dt.Rows.Count==1)
        {
            return dt.Rows[0][0].ToString();
        }
        return "";
    }
}
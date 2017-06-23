using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// CostMan 的摘要说明
/// </summary>
public class CostMan
{
	public CostMan()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public DataTable GetCostSpe(string pk_fee)
    {
        DataTable dt = Sqlhelper.ConSerach(Sqlhelper.conStr_cost, "SELECT distinct SPE_Code,SPE_Name FROM [dbo].[Fee_Item]  where Is_Online_Order='1' and FK_Fee=@pk_fee and SPE_Code!='all'", new SqlParameter("pk_fee", pk_fee));
        if (dt.Rows.Count > 0)
        {
            return dt;
        }
        return null;
    }
    public DataTable GetCostStandard(string pk_fee)
    {
        DataTable dt = Sqlhelper.ConSerach(Sqlhelper.conStr_cost, "SELECT PK_Fee_Item,FK_Fee,Fee_Code,Fee_Code_Name,Fee_Name,Fee_Amount,FK_Fee_Type,Type_Name,case when Is_Must=0 then '否' else '是' end Is_Must,SPE_Code,case when Is_Online_Order=0 then '否' else '是' end Is_Online_Order FROM [dbo].[Fee_Item] left join Fee_Type on Fee_Item.FK_Fee_Type=Fee_Type.PK_Fee_Type where FK_Fee=@pk_fee", new SqlParameter("pk_fee", pk_fee));
        if(dt.Rows.Count>0)
        {
            return dt;
        }
        return null;
    }
    public DataTable GetCostStandard(string pk_fee,string spe_code)
    {
        DataTable dt = Sqlhelper.ConSerach(Sqlhelper.conStr_cost, "SELECT PK_Fee_Item,FK_Fee,Fee_Code,Fee_Code_Name,Fee_Name,Fee_Amount,FK_Fee_Type,Type_Name,case when Is_Must=0 then '否' else '是' end Is_Must,SPE_Code,case when Is_Online_Order=0 then '否' else '是' end Is_Online_Order FROM [dbo].[Fee_Item] left join Fee_Type on Fee_Item.FK_Fee_Type=Fee_Type.PK_Fee_Type where FK_Fee=@pk_fee and (SPE_Code=@spe_code or SPE_Code='all') and Is_Online_Order='1' order by Fee_Code", new SqlParameter("pk_fee", pk_fee), new SqlParameter("spe_code", spe_code));
        if (dt.Rows.Count > 0)
        {
            return dt;
        }
        return null;
    }
    public DataTable GetFee()
    {
        DataTable dt = Sqlhelper.ConSerach(Sqlhelper.conStr_cost, "SELECT PK_Fee,Fee_NO,[Year],Name,FK_App,xtdm FROM [dbo].[Fee] where FK_App='yxxt' and [Year]=year(GetDate())");
        if (dt.Rows.Count > 0)
        {
            return dt;
        }
        return null;
    }
}
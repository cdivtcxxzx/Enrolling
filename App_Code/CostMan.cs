﻿using System;
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
    public DataTable GetCostStandard(string pk_fee)
    {
        DataTable dt = Sqlhelper.ConSerach(Sqlhelper.conStr_cost, "SELECT PK_Fee_Item,FK_Fee,Fee_Code,Fee_Code_Name,Fee_Name,Fee_Amount,FK_Fee_Type,Is_Must,SPE_Code,Is_Online_Order FROM [dbo].[Fee_Item] where FK_Fee=@pk_fee", new SqlParameter("pk_fee", pk_fee));
        if(dt.Rows.Count>0)
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
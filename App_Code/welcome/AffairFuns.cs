using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// AffairFuns 的摘要说明
/// </summary>
public class AffairFuns
{
	public AffairFuns()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static string RunAffairFun(string FunctionName,string PK_SNO){
        string result = null;
        if (FunctionName.Trim().Equals("get_fee_ismust"))
        {
            result=get_fee_ismust(PK_SNO);
        }
        if (FunctionName.Trim().Equals("get_hasnopayorder"))
        {
            result = get_hasnopayorder(PK_SNO);
        }
        return result;
    }

    //判断学生必交费是否交清
    private static string get_fee_ismust(string PK_SNO)
    {
        string result = "未选必交费";
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                throw new Exception("参数错误");
            }

            batch batch_logic = new batch();
            List<fresh_affair> data = batch_logic.get_freshstudent_affair_list(PK_SNO);
            if (data == null || data.Count == 0)
            {
                throw new Exception("获取学生迎新事务数据错误");
            }
            string pk_batch_no = data[0].FK_Batch_NO;
            if (pk_batch_no == null || pk_batch_no.Trim().Length == 0)
            {
                throw new Exception("获取学生迎新事务中的迎新批次数据错误");
            }
            financial logic_fee = new financial();
            fee_list data1 = logic_fee.get_fee_ismust(pk_batch_no, PK_SNO);
            if (data1 == null || data1.orderid == null || data1.orderid.Trim().Length == 0)
            {
                result = "未选必交费";
            }
            else
            {
                bool finishpay = true;
                if (data1.single != null && data1.single.Count > 0)
                {
                    for (int i = 0; i < data1.single.Count; i++)
                    {
                        if (data1.single[i][0].Fee_Amount > data1.single[i][0].Fee_Payed)
                        {
                            finishpay = false;
                        }
                    }
                }
                if (data1.multiple != null && data1.multiple.Count > 0)
                {
                    for (int i = 0; i < data1.multiple.Count; i++)
                    {
                        for (int j = 0; j < data1.multiple[i].Count; j++)
                        {
                            if (data1.multiple[i][j].Fee_Amount > data1.multiple[i][j].Fee_Payed)
                            {
                                finishpay = false;
                            }
                        }
                    }
                }
                if (finishpay)
                {
                    result = "已缴必交费";
                }
                else
                {
                    result = "已选必交费";
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("AffairFuns.cs", "get_fee_ismust", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
        }
        return result;
    }

    //判断学生是否还有没有完成网上交费的订单信息
    private static string get_hasnopayorder(string PK_SNO)
    {
        string result = "待缴订单数量:0";
        try
        {
            if (PK_SNO == null || PK_SNO.Trim().Length == 0)
            {
                throw new Exception("参数错误");
            }

            batch batch_logic = new batch();
            List<fresh_affair> data = batch_logic.get_freshstudent_affair_list(PK_SNO);
            if (data == null || data.Count == 0)
            {
                throw new Exception("获取学生迎新事务数据错误");
            }
            string pk_batch_no = data[0].FK_Batch_NO;
            if (pk_batch_no == null || pk_batch_no.Trim().Length == 0)
            {
                throw new Exception("获取学生迎新事务中的迎新批次数据错误");
            }
            financial logic_fee = new financial();

            List<fresh_fee> order_data = logic_fee.get_fresh_fee(PK_SNO);//获取本系统保存的学生订单
            int nopayorder_count = 0;
            if (order_data != null && order_data.Count > 0)
            {
                for (int k = 0; k < order_data.Count; k++)
                {
                    string orderid = order_data[k].FEE_ORDERID;
                    string orderurl = order_data[k].FEE_ORDERID_URL;
                    List<Financial.Fee_Item> feeitem_List = logic_fee.get_feeitem_byorder(orderid);//根据订单获取学生收费款项列表
                    bool equ = true;//订单所有款项已付费标志
                    for (int i = 0; feeitem_List != null && i < feeitem_List.Count; i++)
                    {
                        Financial.Fee_Item items = feeitem_List[i];
                        if (items.Fee_Amount > items.Fee_Payed && items.Is_Online_Order.Trim().Equals("1"))
                        {
                            equ = false;
                            break;
                        }
                    }
                    if (!equ)
                    {
                        nopayorder_count = nopayorder_count + 1;
                    }
                }
                if (nopayorder_count > 0)
                {
                    result = "待缴订单数量:" + nopayorder_count.ToString().Trim();
                }
            }
        }
        catch (Exception ex)
        {
            try
            {
                new c_log().logAdd("AffairFuns.cs", "get_fee_ismust", ex.Message, "2", "huyuan");//记录错误日志
            }
            catch { }
        }
        return result;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
///stuSql 的摘要说明
/// </summary>
public class stuSql
{
	public stuSql()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 用DataSet更新通过sql选择语句出来的DataSet，并把删除的行bdzt列填为-1
    /// </summary>
    /// <param name="selectCommand">选择语句</param>
    /// <param name="dataSet">源DataSet</param>
    /// <param name="tableName">表名</param>
    public static void UpdateDataSet(string selectCommand, DataSet dataSet, string tableName)
    {
        //if (insertCommand == null) throw new ArgumentNullException("insertCommand");
        //if (deleteCommand == null) throw new ArgumentNullException("deleteCommand");
        //if (updateCommand == null) throw new ArgumentNullException("updateCommand");
        if (tableName == null || tableName.Length == 0) throw new ArgumentNullException("tableName");

        try
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand, new SqlConnection(Sqlhelper.conStr)))
            {
                SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);
                builder.QuotePrefix = "[";
                builder.QuoteSuffix = "]";
                //string xx = builder.GetUpdateCommand().CommandText;//测试用代码
                //string yy = builder.GetInsertCommand().CommandText;//测试用代码
                //string zz = builder.GetDeleteCommand().CommandText;//测试用代码
                //dataAdapter.UpdateCommand = updateCommand;
                //dataAdapter.UpdateCommand = new SqlCommand(updateCommand, new SqlConnection(conStr));
                //dataAdapter.InsertCommand = new SqlCommand(insertCommand, new SqlConnection(conStr));
                //dataAdapter.DeleteCommand = new SqlCommand(deleteCommand, new SqlConnection(conStr));
                //dataAdapter.UpdateCommand.Parameters.Add("guid", "");
                //dataAdapter.UpdateCommand.Parameters.Add("yhqx", "");
                //dataAdapter.UpdateCommand.Parameters.Add("lsz", "");
                //dataAdapter.UpdateCommand.Parameters.Add("yhid", "xx1");
                //dataAdapter.UpdateCommand.Parameters.Add("xm", "3");
                //dataAdapter.UpdateCommand.Parameters.Add("mm", "2");
                //dataAdapter.UpdateCommand.Parameters.Add("uumzw", "");
                //dataAdapter.UpdateCommand.Parameters.Add("lxdh", "7");
                //dataAdapter.UpdateCommand.Parameters.Add("dltime", DateTime.Now.ToString());
                //dataAdapter.UpdateCommand.Parameters.Add("fwcs", 1);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                ds.Tables[0].TableName = tableName;
                //int yyy = ds.Tables[0].Rows.Count;
                //int xxx = dataSet.Tables[0].Rows.Count;
                ds.Merge(dataSet);
                 DataRow[] drs=ds.Tables[0].Select("","xsid",DataViewRowState.Unchanged);
                //ds.Tables[0].Merge(dataSet.Tables[0]);
                //dataSet.Merge(ds,true);
                foreach(DataRow dr in drs)
                {

                        Sqlhelper.ExcuteNonQuery("update xuesjbsj set bdzt='-1' where xsid='" + dr["xsid"] + "'");
                        
                    //Sqlhelper.zsglSerach
                }
                dataAdapter.Update(ds, tableName);
                dataSet.AcceptChanges();
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}
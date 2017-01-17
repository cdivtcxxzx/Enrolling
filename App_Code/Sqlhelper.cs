using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
public class Sqlhelper
{
    //读取webconfig中SQL连接字符串                
    public static string conStr =ConfigurationManager.ConnectionStrings["SqlConnString"].ConnectionString;
   //读取管理目录
    public static string gldir = ConfigurationManager.ConnectionStrings["gldir"].ConnectionString;
    //读取服务端口信息
    public static string serverport = ConfigurationManager.ConnectionStrings["serverport"].ConnectionString;
    //招生管理SQL
    public static string conStrzsgl = ConfigurationManager.ConnectionStrings["zsglConnectionString"].ConnectionString;
    //一卡通管理SQL
    public static string conStrykt = ConfigurationManager.ConnectionStrings["yktSqlConnString"].ConnectionString;
    //一卡通管理218SQL
    public static string conStrykt218 = ConfigurationManager.ConnectionStrings["ykt218SqlConnString"].ConnectionString;
    //163UUM数据库视图
    public static string conStruum = ConfigurationManager.ConnectionStrings["uumSqlConnString"].ConnectionString;
    /// <summary>
    /// 执行查询
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <param name="parameters">查询参数</param>
    /// <returns></returns>
    public static DataTable zsglSerach(string sql, params SqlParameter[] parameters)
    {
        //建立连接
        using (SqlConnection conn = new SqlConnection(conStrzsgl))
        {
            //打开连接
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataSet st = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(st);
                    return st.Tables[0];
                }
                
            }
            catch (Exception e)
            {
                return new DataTable();
            }
        }
    }
    public static DataTable yktSerach(string sql, params SqlParameter[] parameters)
    {
        //建立连接
        using (SqlConnection conn = new SqlConnection(conStrykt))
        {
            //打开连接
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataSet st = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(st);
                    return st.Tables[0];
                }

            }
            catch (Exception e)
            {
                return new DataTable();
            }
        }
    }
    public static DataTable uumSerach(string sql, params SqlParameter[] parameters)
    {
        //建立连接
        using (SqlConnection conn = new SqlConnection(conStruum))
        {
            //打开连接
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataSet st = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(st);
                    return st.Tables[0];
                }

            }
            catch (Exception e)
            {
                return new DataTable();
            }
        }
    }
    public static DataTable ykt218Serach(string sql, params SqlParameter[] parameters)
    {
        //建立连接
        using (SqlConnection conn = new SqlConnection(conStrykt218))
        {
            //打开连接
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataSet st = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(st);
                    return st.Tables[0];
                }

            }
            catch (Exception e)
            {
                return new DataTable();
            }
        }
    }
    /// <summary>
    /// 执行查询
    /// </summary>
    /// <param name="sql">查询语句</param>
    /// <param name="parameters">查询参数</param>
    /// <returns></returns>
    public static DataTable Serach(string sql, params SqlParameter[] parameters)
    {
        //建立连接
        using (SqlConnection conn = new SqlConnection(conStr))
        {
            //打开连接
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataSet st = new DataSet();
                    try
                    {
                        //记录SQL查询
                        if (sql.Contains("script") || sql.Contains("href") || sql.Contains("<div") || sql.Contains("exec") || sql.Contains("sysadmin") || sql.Contains("master") || sql.Contains("create") || sql.Length > 200)
                        {
                            //获得当前IP
                            string ipok = HttpContext.Current.Request.UserHostAddress;
                            //获得当前页面
                            string urlok = HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.Url;
                            //获得当前用户
                            string username = "匿名";
                            try
                            {
                                username = HttpContext.Current.Session["username"].ToString();

                            }
                            catch { }
                            //写入日志
                            Sqlhelper.ExcuteNonQuery("INSERT INTO [sqllog]    ([sql],[time],[ip],[url],[username],[bz],[laiyuan]) VALUES('" + sql + "','" + DateTime.Now.ToString() + "','" + ipok + "','" + urlok + "','" + username + "','SQL查询监控','迎新系统')");
                        }
                    }
                    catch
                    {
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(st);
                    return st.Tables[0];
                }
            }
            catch(Exception e)
            {   
                return new DataTable();
            }
        }
    }
    /// <summary>
    /// 执行ExcuteNonQuery操作
    /// </summary>
    /// <param name="sql">执行sql语句</param>
    /// <param name="parameters">相关参数</param>
    /// <returns>影响行数</returns>
    public static int ExcuteNonQuery(String sql, params SqlParameter[] parameters)
    {
        try
        {
            using ( System.Data.SqlClient.SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        object o =parameter.Value;
                        cmd.Parameters.Add(parameter);
                    }


                    return cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            throw e;
            return 0;
        }
    }
    /// <summary>
    /// 执行zsglExcuteNonQuery操作
    /// </summary>
    /// <param name="sql">执行sql语句</param>
    /// <param name="parameters">相关参数</param>
    /// <returns>影响行数</returns>
    public static int zsglExcuteNonQuery(String sql, params SqlParameter[] parameters)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(conStrzsgl))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    foreach (SqlParameter parameter in parameters)
                    {
                        object o = parameter.Value;
                        cmd.Parameters.Add(parameter);
                    }


                    return cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            return 0;
        }
    }
   
/// <summary>
/// 
/// </summary>
/// <param name="selectCommand"></param>
/// <param name="dataSet"></param>
/// <param name="tableName"></param>
    public static void UpdateDataSet(string selectCommand, DataSet dataSet, string tableName)
    {
        //if (insertCommand == null) throw new ArgumentNullException("insertCommand");
        //if (deleteCommand == null) throw new ArgumentNullException("deleteCommand");
        //if (updateCommand == null) throw new ArgumentNullException("updateCommand");
        if (tableName == null || tableName.Length == 0) throw new ArgumentNullException("tableName");

        try
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand, new SqlConnection(conStr)))
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
                //ds.Tables[0].Merge(dataSet.Tables[0]);
                //dataSet.Merge(ds,true);
                dataAdapter.Update(ds, tableName);
                dataSet.AcceptChanges();
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="selectCommand"></param>
    /// <param name="dataSet"></param>
    /// <param name="tableName"></param>
    public static int GetUpdateDataSet(string selectCommand, DataSet dataSet, string tableName)
    {
        //if (insertCommand == null) throw new ArgumentNullException("insertCommand");
        //if (deleteCommand == null) throw new ArgumentNullException("deleteCommand");
        //if (updateCommand == null) throw new ArgumentNullException("updateCommand");
        if (tableName == null || tableName.Length == 0) throw new ArgumentNullException("tableName");

        try
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand, new SqlConnection(conStr)))
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
                //ds.Tables[0].Merge(dataSet.Tables[0]);
                //dataSet.Merge(ds,true);
               int result= dataAdapter.Update(ds, tableName);
                dataSet.AcceptChanges();
                return result;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public static void BulkInsert(DataTable sourceDataTable, string targetTableName, SqlBulkCopyColumnMapping[] mapping)
        {
            /*  调用方法 -2012年11月16日编写
            //DataTable dt = Get_All_RoomState_ByHID();
            //SqlBulkCopyColumnMapping[] mapping = new SqlBulkCopyColumnMapping[4];
            //mapping[0] = new SqlBulkCopyColumnMapping("Xing_H_ID", "Xing_H_ID");
            //mapping[1] = new SqlBulkCopyColumnMapping("H_Name", "H_Name");
            //mapping[2] = new SqlBulkCopyColumnMapping("H_sName", "H_sName");
            //mapping[3] = new SqlBulkCopyColumnMapping("H_eName", "H_eName");
            //BulkToDB(dt, "Bak_Tts_Hotel_Name", mapping);
            */
            SqlConnection conn = new SqlConnection(conStr);
            SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);   //用其它源的数据有效批量加载sql server表中
            bulkCopy.DestinationTableName = targetTableName;    //服务器上目标表的名称
            bulkCopy.BatchSize = sourceDataTable.Rows.Count;   //每一批次中的行数
            try
            {
                conn.Open();
                if (sourceDataTable != null && sourceDataTable.Rows.Count != 0)
                {
                    for (int i = 0; i < mapping.Length; i++)
                        bulkCopy.ColumnMappings.Add(mapping[i]);

                    //将提供的数据源中的所有行复制到目标表中
                    bulkCopy.WriteToServer(sourceDataTable );   
                }
            }
            catch (Exception ex)
            {
                throw ex;
               
            }
            finally
            {
                conn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();
            }
        }
}  

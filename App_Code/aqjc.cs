using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///aqjc 的摘要说明
/// </summary>
public class aqjc
{

    /// <summary>
    /// 根据传输的信息，检测是否满足条件，并提示安全警告
    /// </summary>

    /// <param name="xx">需验证的数据/param>
    /// <param name="tsxx">需输出的提示信息</param>
    ///  <param name="type">验证类型，true:int/flase:length/</param>
    /// <param name="value">int 为0，length为最大长度</param>
    /// <returns>返回详细提示信息，可用作直接输出</returns>
    ///日期2013.9.20 作者：张明
   public static string requestjc(string xx, string tsxx, bool type, int value)
    {
        int i = 0;
        string err_tsxx = "安全警告：你提交的信息非法，已记录您的访问信息！";
        if (tsxx.Length > 0) err_tsxx += tsxx;

        if (type == true)
        {
            double temp = 0;
            if (double.TryParse(xx, out temp))
            {
                tsxx = "";
            }
            else
            {
                tsxx = "<html><head><META charset=utf-8><title>安全警告</title><META name=\"keywords\" content=\"站点安全警告\"> <META name=\"description\" content=\"站点安全警告\"> </head><body><br> <font color=red>" + err_tsxx + "</font></body></html>";
            }
        }
        else
        {
            if (xx.Length > value)
            {
                tsxx = "<html><head><META charset=utf-8><title>安全警告</title><META name=\"keywords\" content=\"站点安全警告\"> <META name=\"description\" content=\"站点安全警告\"> </head><body><br> <font color=red>" + err_tsxx + "</font></body></html>";
            }
            else
            {
                tsxx="";
            }
        }
        return tsxx;
    }
	
    
  
}
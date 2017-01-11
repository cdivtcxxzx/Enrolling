using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maths
{
    /// <summary>
    /// 统计类
    /// </summary>
    public class Statistic
    {
        /// <summary>
        /// 计算平均值
        /// consisting of the following eight values:
        /// 2,\  4,\  4,\  4,\  5,\  5,\  7,\  9
        /// These eight data points have the mean (average) of 5:
        /// {2 + 4 + 4 + 4 + 5 + 5 + 7 + 9}/{8} = 5.  
        /// </summary>
        /// <param name="flt">float数组值</param>
        /// <returns></returns>
        public double Mean(double[] flt)
        {
            double total = 0;
            foreach (double x in flt)
            {
                total += x;
            }
            return total / flt.Length;
        }
        /// <summary>
        /// 计算方差
        /// compute the difference of each data point from the mean, and square the result of each:
        /// (2-5)^2 = (-3)^2 = 9  &&  (5-5)^2 = 0^2 = 0 
        /// (4-5)^2 = (-1)^2 = 1  &&  (5-5)^2 = 0^2 = 0 
        /// (4-5)^2 = (-1)^2 = 1  &&  (7-5)^2 = 2^2 = 4 
        /// (4-5)^2 = (-1)^2 = 1  &&  (9-5)^2 = 4^2 = 16
        /// 最后计算各项和的平均值
        /// </summary>
        /// <param name="flt"></param>
        /// <returns></returns>
        public double Variance(double[] flt)
        {
            double mean = Mean(flt);
            double temp = 0;
            foreach (double x in flt)
            {
                temp += (System.Math.Pow(x - mean,2));
            }
            return temp / flt.Length;
        }
        /// <summary>
        /// 计算标准差
        /// </summary>
        /// <param name="flt"></param>
        /// <returns></returns>
        public double StandardDeviation(double[] flt)
        {
            return System.Math.Sqrt(Variance(flt));
        }
        /// <summary>
        /// 正态分布值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double[] NormalDistribution(double[] flt)
        {
            double e = System.Math.E;
            double sigma = StandardDeviation(flt);
            double u = Mean(flt);
            double[] nd=new double[flt.Length];
            for (int i = 0; i < flt.Length; i++)
            {
                double temp = System.Math.Exp(-System.Math.Pow((flt[i]-u),2)/(2*System.Math.Pow(sigma,2)));
                nd[i] = (1 / (sigma * System.Math.Sqrt(2 * System.Math.PI))) * temp;
            }
            return nd;
        }
    }
}

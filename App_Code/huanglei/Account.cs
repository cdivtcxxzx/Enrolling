using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
///Account 的摘要说明
/// </summary>
public class Account : System.Web.UI.Page
{
    protected string sqlString;
	public Account()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    /// <summary>
    /// 通过身份证生成性别、户籍所在省、户籍所在市、所在县、出生日期
    /// </summary>
    /// <param name="sfzjh">返回Dictionary,对应Key为xb,hjszs,hjszshi,hjszx,csrq</param>
    /// <returns></returns>
    public Dictionary<string, string> GetFromSfz(string sfzjh)
    {
        Dictionary<string, string> dic = new Dictionary<string, string> {{"xb",""},{"hjszs",""},{"hjszshi",""},{"hjszx",""},{"csrq",""} };
        DataTable sfz = Sqlhelper.Serach("select * from dm_xzqh");
        DataTable sfz_old = Sqlhelper.Serach("select * from dm_sfzold");
        if (sfzjh != "" && sfzjh != null && sfzjh.Length > 17)
        {
            if (Convert.ToInt32(sfzjh.Substring(16, 1)) % 2 == 1) dic["xb"] = "男"; else dic["xb"] = "女";

            dic["csrq"] = sfzjh.Substring(6, 8);
            try
            {
                //取身份证前2位
                string code = sfzjh.Substring(0, 2);
                //查询前两位加0000得到的省份
                string area = sfz.Select("code=" + code + "0000")[0].ItemArray[1].ToString();
                dic["hjszs"] = area;
                //取前4位
                code = sfzjh.Substring(0, 4);
                //在行政区划表中查询前4位加00得到的行
                DataRow[] dr0 = sfz.Select("code=" + code + "00");
                if (dr0.Length > 0)
                    area = dr0[0].ItemArray[1].ToString();
                else
                {
                    //如果没查到，继续在老表中查
                    DataRow[] dr00 = sfz_old.Select("code=" + code + "00");
                    if (dr00.Length > 0)
                    {
                        //char[] area_chars0 = area.ToCharArray();
                        string[] str = new string[1];
                        str[0] = area.Substring(area.Length - 2, 2);
                        //使用所在省的最后两位来分隔，因为直辖市最后一位是市
                        string[] temp1 = dr00[0].ItemArray[2].ToString().Split(str, System.StringSplitOptions.RemoveEmptyEntries);
                        if (temp1.Length == 2)
                            area = temp1[1];
                        else if (temp1.Length > 2)
                            area = temp1[temp1.Length - 1];
                    }
                }
                dic["hjszshi"] = area;
                char[] area_chars = area.ToCharArray();
                code = sfzjh.Substring(0, 6);
                //取前六位查询
                DataRow[] dr1 = sfz.Select("code=" + code);
                if (dr1.Length > 0)
                    area = dr1[0].ItemArray[1].ToString();
                else
                {
                    DataRow[] dr2 = sfz_old.Select("code=" + code);
                    if (dr2.Length > 0)
                    {
                        //char c = area_chars[area_chars.Length - 1];
                        string[] str = new string[1];
                        str[0] = area.Substring(area.Length - 2, 2);
                        string[] temp1 = dr2[0].ItemArray[2].ToString().Split(str, System.StringSplitOptions.RemoveEmptyEntries);
                        if (temp1.Length == 2)
                            area = temp1[1];
                        //因为表里的数据有重复的，比如"秦皇岛市秦皇岛市"，可能出现分隔出的数据不对，所以用最后一组得到
                        else if (temp1.Length > 2)
                            area = temp1[temp1.Length - 1];
                    }
                }
                dic["hjszx"] = area;
            }
            catch (Exception ex)
            {
                dic["hjszx"] = "";
                //throw;
            }
        }
        else
        {
            dic["xb"] = "";
            dic["csrq"] = "";
            dic["hjszs"] = ""; dic["hjszshi"] = ""; dic["hjszx"] = "";
        }
        return dic;
    }
    public DataTable CheckXj()
    {
        DataTable temp = new DataTable();
        temp.Columns.Add("xsid", typeof(string));
        temp.Columns.Add("姓名", typeof(string));
        temp.Columns.Add("身份证", typeof(string));
        temp.Columns.Add("院系", typeof(string));
        temp.Columns.Add("错误提示", typeof(string));
        DataTable xsbjsj = Sqlhelper.Serach("select *,bjmc,yxmc from xuesjbsj a left join banjxx b on a.bjdm=b.bjdm left join dm_yuanxi c on a.yxdm=c.yxdm where bdzt=1 and a.bjdm<>''");
        foreach (DataRow x in xsbjsj.Rows)
        {
            DataRow dr = temp.NewRow();
            int count_error = 0;
            if (x["sfzjh"] == null || x["sfzjh"].ToString() == "") { dr["错误提示"] += "身份证为空!"; count_error++; }
            if (x["sfzjh"].ToString().Trim().Length != 18) { dr["错误提示"] += "身份证长度错误!"; count_error++; }
            if (x["nd"] == null || x["nd"].ToString() == "") { dr["错误提示"] += "年度为空!"; count_error++; }
            //if (x["xqdm"] == null || x["xqdm"].ToString() == "") { dr["错误提示"] += "校区为空!";count_error++; }
            //if (x["yxdm"] == null || x["yxdm"].ToString() == "") { dr["错误提示"] += "院系为空!"; count_error++;}
            if (x["xm"] == null || x["xm"].ToString() == "") { dr["错误提示"] += "姓名为空!"; count_error++; }
            if (x["xb"].ToString() != "男" && x["xb"].ToString() != "女") { dr["错误提示"] += "性别错误!"; count_error++; }
            if (x["mz"] == null || x["mz"].ToString() == "") { dr["错误提示"] += "民族为空!"; count_error++; }
            if (x["jkzt"] == null || x["jkzt"].ToString() == "") { dr["错误提示"] += "健康状态为空!"; count_error++; }
            if (x["zymc"] == null || x["zymc"].ToString() == "") { dr["错误提示"] += "专业为空!"; count_error++; }
            if (x["xz"] == null || x["xz"].ToString() == "") { dr["错误提示"] += "学制为空!"; count_error++; }
            //if (x["bjdm"] == null || x["bjdm"].ToString() == "") { dr["错误提示"] += "班级为空!"; count_error++; }
            if (x["hjszd"] == null || x["hjszd"].ToString() == "") { dr["错误提示"] += "户籍所在地为空!"; count_error++; }
            if (x["hjszs"] == null || x["hjszs"].ToString() == "") { dr["错误提示"] += "户籍所在省为空!"; count_error++; }
            if (x["hjszshi"] == null || x["hjszx"] == null) { dr["错误提示"] += "户籍所在市或所在区县为NULL!"; count_error++; }
            else if (x["hjszshi"].ToString() == "" && x["hjszx"].ToString() == "") { dr["错误提示"] += "所在市和所在区县必须填一个!"; count_error++; }
            if (x["hjxz"] == null || x["hjxz"].ToString() == "") { dr["错误提示"] += "户籍性质为空!"; count_error++; }
            if (x["sfdb"] == null || x["sfdb"].ToString() == "") { dr["错误提示"] += "是否低保为空!"; count_error++; }
            if (x["bylx"] == null || x["bylx"].ToString() == "") { dr["错误提示"] += "毕业类型为空!"; count_error++; }
            if (x["jzxm"] == null || x["jzxm"].ToString() == "") { dr["错误提示"] += "家长姓名为空!"; count_error++; }
            if (x["jzdh"] == null || x["jzdh"].ToString() == "") { dr["错误提示"] += "家长电话为空!"; count_error++; }
            if (x["xjtdz"] == null || x["xjtdz"].ToString() == "") { dr["错误提示"] += "现家庭所在地为空!"; count_error++; }
            if (x["xjtszs"] == null || x["xjtszs"].ToString() == "") { dr["错误提示"] += "现家庭所在省为空!"; count_error++; }
            if (x["xjtszshi"] == null || x["xjtszx"] == null) { dr["错误提示"] += "现家庭所在市或所在区县为NULL!"; count_error++; }
            else if (x["xjtszshi"].ToString() == "" && x["xjtszx"].ToString() == "") { dr["错误提示"] += "现家庭所在市和所在区县必须填一个!"; count_error++; }
            //#region 可选核对数据
            //if (x["csrq"] == null || x["csrq"].ToString() == "") { dr["错误提示"] += "出生日期为空!"; count_error++; }
            //if (x["xqdm"] == null || x["xqdm"].ToString() == "") { dr["错误提示"] += "校区为空!"; count_error++; }
            //if (x["yxdm"] == null || x["yxdm"].ToString() == "") { dr["错误提示"] += "院系为空!"; count_error++; }
            //if (x["zydm"] == null || x["zydm"].ToString() == "") { dr["错误提示"] += "专业为空!"; count_error++; } 
            //#endregion
            if (count_error > 0)
            {
                dr["xsid"]=x["xsid"];
                dr["身份证"] = x["sfzjh"];
                dr["姓名"] = x["xm"];
                dr["院系"] = x["yxmc"];
                temp.Rows.Add(dr);
            }
        }
        temp = temp.Select("1=1", "姓名 desc").CopyToDataTable();
        return temp;
    }
    public int AutoUpdateXj()
    {
        DataSet tempDs= new DataSet();;
        DataTable xsbjsj = Sqlhelper.Serach("select xsid,xm,sfzjh,nd,mz,jkzt,hjszs,hjszshi,hjszx,hjszd,hjxz,sfdb,bylx,jzxm,jzdh,lxdh,xjtdz,xjtszs,xjtszshi,xjtszx from xuesjbsj a where bdzt=1 and a.bjdm<>''");
        DataTable temp = xsbjsj.Clone();
        temp.TableName = "xuesjbsj";
        DataColumn[] columns = new DataColumn[1];
        columns[0] = temp.Columns["xsid"];
        temp.PrimaryKey = columns;
        foreach (DataRow x in xsbjsj.Rows)
        {
            DataRow dr = temp.NewRow();
            int count_error = 0;
            dr["xsid"]=x["xsid"];
            dr["xm"] = x["xm"];
            dr["sfzjh"] = x["sfzjh"];
            dr["nd"] = x["nd"];
            dr["mz"] = x["mz"];
            dr["jkzt"] = x["jkzt"];
            dr["hjszs"] = x["hjszs"];
            dr["hjszshi"] = x["hjszshi"];
            dr["hjszx"] = x["hjszx"];
            dr["hjszd"] = x["hjszd"];
            dr["hjxz"] = x["hjxz"];
            dr["sfdb"] = x["sfdb"];
            dr["bylx"] = x["bylx"];
            dr["jzxm"] = x["jzxm"];
            dr["jzdh"] = x["jzdh"];
            dr["lxdh"] = x["lxdh"];
            dr["xjtdz"] = x["xjtdz"];
            dr["xjtszs"] = x["xjtszs"];
            dr["xjtszshi"] = x["xjtszshi"];
            dr["xjtszx"] = x["xjtszx"];
            //if (x["sfzjh"] == null || x["sfzjh"].ToString() == "") { dr["错误提示"] += "身份证为空!"; count_error++; }
            //if (x["sfzjh"].ToString().Length != 18) { dr["错误提示"] += "身份证长度错误!"; count_error++; }
            if (x["nd"] == null || x["nd"].ToString() == "") { dr["nd"] = "2013"; count_error++; }
            //if (x["xqdm"] == null || x["xqdm"].ToString() == "") { dr["错误提示"] += "校区为空!";count_error++; }
            //if (x["yxdm"] == null || x["yxdm"].ToString() == "") { dr["错误提示"] += "院系为空!"; count_error++;}
            //if (x["xm"] == null || x["xm"].ToString() == "") { dr["错误提示"] += "姓名为空!"; count_error++; }
            //if (x["xb"].ToString() != "男" && x["xb"].ToString() != "女") { dr["错误提示"] += "性别错误!"; count_error++; }
            if (x["mz"] == null || x["mz"].ToString() == "") { dr["mz"] = "汉族"; count_error++; }
            if (x["jkzt"] == null || x["jkzt"].ToString() == "") { dr["jkzt"] = "健康"; count_error++; }
            //if (x["zymc"] == null || x["zymc"].ToString() == "") { dr["错误提示"] += "专业为空!"; count_error++; }
            //if (x["xz"] == null || x["xz"].ToString() == "") { dr["错误提示"] += "学制为空!"; count_error++; }
            //if (x["bjdm"] == null || x["bjdm"].ToString() == "") { dr["错误提示"] += "班级为空!"; count_error++; }
            Dictionary<string, string> dic = GetFromSfz(x["sfzjh"].ToString());
            if (x["hjszs"] == null || x["hjszs"].ToString() == "") { dr["hjszs"] = dic["hjszs"]; count_error++; }
            if (x["hjszshi"] == null || x["hjszx"] == null) { dr["hjszshi"] = dic["hjszshi"]; dr["hjszx"] = dic["hjszx"]; count_error++; }
            else if (x["hjszshi"].ToString() == "" && x["hjszx"].ToString() == "") { dr["hjszshi"] = dic["hjszshi"]; dr["hjszx"] = dic["hjszx"]; count_error++; }

            if (x["hjszd"] == null || x["hjszd"].ToString() == "") { dr["hjszd"] = dr["hjszs"].ToString() + dr["hjszshi"].ToString() + dr["hjszx"].ToString(); count_error++; }
            if (x["hjxz"] == null || x["hjxz"].ToString() == "") { dr["hjxz"] = "城市"; count_error++; }
            if (x["sfdb"] == null || x["sfdb"].ToString() == "") { dr["sfdb"] = "否"; count_error++; }
            if (x["bylx"] == null || x["bylx"].ToString() == "") { dr["bylx"] = "应届初中别业"; count_error++; }
            if (x["jzxm"] == null || x["jzxm"].ToString() == "") { dr["jzxm"] = dr["xm"]; count_error++; }
            if (x["jzdh"] == null || x["jzdh"].ToString() == "") { dr["jzdh"] = dr["lxdh"]; count_error++; }
            if (x["xjtdz"] == null || x["xjtdz"].ToString() == "") { dr["xjtdz"] = dr["hjszd"]; count_error++; }
            if (x["xjtszs"] == null || x["xjtszs"].ToString() == "") { dr["xjtszs"] = dr["hjszs"]; count_error++; }
            if (x["xjtszshi"] == null || x["xjtszx"] == null) { dr["xjtszshi"] = dr["hjszshi"]; dr["xjtszx"] = dr["hjszx"]; count_error++; }
            else if (x["xjtszshi"].ToString() == "" && x["xjtszx"].ToString() == "") { dr["xjtszshi"] = dr["hjszshi"]; dr["xjtszx"] = dr["hjszx"]; count_error++; }
            //#region 可选核对数据
            //if (x["csrq"] == null || x["csrq"].ToString() == "") { dr["错误提示"] += "出生日期为空!"; count_error++; }
            //if (x["xqdm"] == null || x["xqdm"].ToString() == "") { dr["错误提示"] += "校区为空!"; count_error++; }
            //if (x["yxdm"] == null || x["yxdm"].ToString() == "") { dr["错误提示"] += "院系为空!"; count_error++; }
            //if (x["zydm"] == null || x["zydm"].ToString() == "") { dr["错误提示"] += "专业为空!"; count_error++; } 
            //#endregion
            if (count_error > 0)
            {
                
                temp.Rows.Add(dr);
            }
        }
        tempDs.Tables.Add(temp);
        return Sqlhelper.GetUpdateDataSet("select xsid,xm,sfzjh,nd,mz,jkzt,hjszs,hjszshi,hjszx,hjszd,hjxz,sfdb,bylx,jzxm,jzdh,lxdh,xjtdz,xjtszs,xjtszshi,xjtszx from xuesjbsj a where bdzt=1 and a.bjdm<>''", tempDs, "xuesjbsj");
    }
    /// <summary>
    /// 修改本系统密码
    /// </summary>
    /// <param name="yhid">用户登录名</param>
    /// <param name="oldPwd">旧密码</param>
    /// <param name="newPwd">新密码</param>
    /// <returns>-1为旧密码错误，1为修改成功，0为修改失败</returns>
    public int ChangePwd(string yhid,string oldPwd, string newPwd) {

        DataTable dt_mm= Sqlhelper.Serach("select mm from yonghqx where yhid='"+yhid+"'");
        string mm = dt_mm.Rows[0]["mm"].ToString();
        string mm_dec=md5.MD5Decrypt(mm,md5.GetKey());
        if (oldPwd != mm_dec) { return -1; }
        else { 
            string newPwd_enc=md5.MD5Encrypt(newPwd,md5.GetKey());
            try
            {
                if (Sqlhelper.ExcuteNonQuery("update yonghqx set mm='" + newPwd_enc + "' where yhid='" + yhid + "'") > 0)
                { return 1; }
                else { return 0; }
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
    /// <summary>
    /// 管理员重置本系统密码
    /// </summary>
    /// <param name="yhid">用户登录名</param>
    /// <param name="newPwd">新密码</param>
    /// <returns>1为修改成功，0为修改失败</returns>
    public int ResetPwd(string yhid,  string newPwd)
    {


            string newPwd_enc = md5.MD5Encrypt(newPwd, md5.GetKey());
            try
            {
                if (Sqlhelper.ExcuteNonQuery("update yonghqx set mm='" + newPwd_enc + "' where yhid='" + yhid + "'") > 0)
                { return 1; }
                else { return 0; }
            }
            catch (Exception)
            {

                return 0;
            }
        
    }
    public static string GetByKey(string tableName,string column,string expression){

     DataTable temp=   Sqlhelper.Serach("select "+column+" from "+tableName+" where "+expression);
     if (temp.Rows.Count > 0) { return temp.Rows[0][column].ToString(); }
     else return "";
    
    }
    /// <summary>
    /// 检查身份证合法性
    /// </summary>
    /// <param name="sfzjh">身份证号</param>
    /// <returns></returns>
    public bool IdCheck(string sfzjh)
    {
        if (sfzjh.Length != 18 ) { return false; }
        Dictionary<int, string> dic = new Dictionary<int, string> { { 0, "1" }, { 1, "0" }, { 2, "X" }, { 3, "9" }, { 4, "8" }, { 5, "7" }, { 6, "6" }, { 7, "5" }, { 8, "4" }, { 9, "3" }, { 10, "2" } };
        int[] multi_code = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        int sum = 0;
        try
        {
            for (int i = 0; i < 17; i++)
            { string x = sfzjh.Substring(i, 1); sum += Convert.ToInt32(x) * multi_code[i]; }
            int reminder = sum % 11;
            if (dic[reminder] == sfzjh.Substring(17, 1).ToUpper()) { return true; }
            else { return false; }
        }
        catch (Exception)
        {
            return false;
        }
    }
    public DataTable getAllUser()
    {
        sqlString = "select * from yonghqx";
        int xx = Sqlhelper.Serach(sqlString).Rows.Count;
        DataTable dt = Sqlhelper.Serach(sqlString);
        foreach (DataRow x in dt.Rows)
        {
            x[2] = new Power().getZhuMCs(x.ItemArray[2].ToString());

        }
        Session["YhqxTotalRows"] = dt.Rows.Count;//统计记录数
        return dt;
    }
    public DataTable getById(string yhid)
    {
        sqlString = "select * from yonghqx where yhid=@yhid";
        return Sqlhelper.Serach(sqlString, new SqlParameter("yhid", yhid));
    }
    /// <summary>
    /// 通过组id获取相应组
    /// </summary>
    /// <param name="zid">组id</param>
    /// <returns></returns>
    public DataTable getByZhuId(string zid)
    {
        sqlString = "select * from zhuqx where zid=@zid";
        return Sqlhelper.Serach(sqlString, new SqlParameter("zid", zid));
    }
    public DataTable getByName(string xm)
    {
        sqlString = "select * from yonghqx where xm=@xm";
        return Sqlhelper.Serach(sqlString, new SqlParameter("xm", xm));
    }
    public DataTable getByKey(string key)
    {
        DataTable dt = getAllUser();
        DataTable searchDt = new DataTable();
        searchDt = dt.Clone();
        foreach (DataRow dr in dt.Rows)
        {
            string xx = "";
            foreach (var m in dr.ItemArray)
            {
                xx = xx + m.ToString();

            }
            if (System.Text.RegularExpressions.Regex.IsMatch(xx, key, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {

                searchDt.ImportRow(dr);

            }
            else
            {
                //searchDt.Rows.Remove(dr);
            }

        }
        Session["YhqxTotalRows"] = searchDt.Rows.Count;//统计记录数
        return searchDt;
    }

    /// <summary>
    /// 把uum得到的数据表转换成需要写入banzr数据库的表
    /// </summary>
    /// <param name="gt">uum得到的数据表</param>
    /// <returns></returns>
    public DataTable banzr(DataTable gt)
    {
        DataTable temp = new DataTable();
        temp.TableName = "banzr";
        temp.Columns.Add("guid", typeof(string));
        temp.Columns.Add("szbmid", typeof(string));
        //DataColumn[] columns = new DataColumn[1];
        //columns[0] = temp.Columns["guid"];
        //temp.PrimaryKey = columns;
        DataTable yx=Sqlhelper.Serach("select yxdm,yxmc from dm_yuanxi");
        foreach (DataRow m in gt.Rows)
        {
            DataRow dr = temp.NewRow();
            DataRow[] rs = yx.Select("yxmc='" + m.ItemArray[4] + "'");
            dr[0] = m.ItemArray[0];
            if (rs.Count() > 0)
            {
                dr[1] = rs[0][0];
            }
            else { dr[1] ="9999"; }

            temp.Rows.Add(dr);
        }
        return temp;
    }
}
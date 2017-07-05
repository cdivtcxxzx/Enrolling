using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class nradmingl_Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //string pk_staff_no = Request.QueryString["pk_staff_no"];//获取员工编号
        //if (pk_staff_no == null || pk_staff_no.Trim().Length == 0)
        //{
        //    throw new Exception("参数错误");
        //}

        //Session["pk_staff_no"] = pk_staff_no;

        if (Session["username"] == null)
        {
            throw new Exception("没登陆");
        }
        this.pk_staff_no.Value = Session["username"].ToString().Trim();
    }
    //下载数据
    protected void btn_down_Click(object sender, EventArgs e)
    {
        if (hid_class_no.Value != null && hid_class_no.Value.Trim().Length > 0 && hid_batch_no.Value != null && hid_batch_no.Value.Trim().Length > 0)
        {
            batch batch_logic = new batch();
            System.Data.DataTable dt = batch_logic.get_classstudent(hid_class_no.Value.Trim());
            System.Data.DataTable dt_fee = new DataTable();
            List<Object> jg = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                
                DataRow row = null;
                DataColumn column = new DataColumn();

                column.DataType = System.Type.GetType("System.String");
                column.Caption = "pk_sno";
                column.ColumnName = "pk_sno";
                dt_fee.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.Caption = "姓名";
                column.ColumnName = "姓名";
                dt_fee.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.Caption = "性别";
                column.ColumnName = "性别";
                dt_fee.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.Caption = "电话";
                column.ColumnName = "电话";
                dt_fee.Columns.Add(column);
                dt_fee.AcceptChanges();

                jg = new List<object>();
                financial logic_fee = new financial();
                Hashtable fee_type_hash = new Hashtable(); //  创建哈希表
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string pk_sno = dt.Rows[i]["pk_sno"].ToString().Trim();

                    row = dt_fee.NewRow();
                    row["pk_sno"] = pk_sno.Trim();
                    row["姓名"] = dt.Rows[i]["name"].ToString().Trim();
                    row["性别"] = dt.Rows[i]["gender"].ToString().Trim();
                    row["电话"] = dt.Rows[i]["phone"].ToString().Trim();
                    dt_fee.Rows.Add(row);
                    dt_fee.AcceptChanges();

                    List<fee_list> data = logic_fee.get_fee(hid_batch_no.Value.Trim(), pk_sno);
                    if (data != null)
                    {
                        List<List<Financial.Fee_Item>> single_must = data[0].single;
                        List<List<Financial.Fee_Item>> multiple_must = data[0].multiple;
                        List<List<Financial.Fee_Item>> single_nomust = data[1].single;
                        List<List<Financial.Fee_Item>> multiple_nomust = data[1].multiple;
                        if (single_must != null)
                        {
                            for (int j = 0; j < single_must.Count; j++)
                            {
                                Financial.Fee_Item fee_item = single_must[j][0];
                                if (fee_type_hash[fee_item.FK_Fee_Type.Trim()] == null)
                                {
                                    ResultData node = new ResultData();
                                    node.code = fee_item.FK_Fee_Type.Trim();
                                    node.message = fee_item.Type_Name.Trim();
                                    if (fee_item.Fee_Amount > fee_item.Fee_Payed)
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("欠费");
                                        node.data = tmp;
                                    }
                                    else
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("已缴");
                                        node.data = tmp;
                                    }
                                    List<ResultData> node_list = new List<ResultData>();
                                    node_list.Add(node);
                                    fee_type_hash.Add(fee_item.FK_Fee_Type.Trim(), node_list);
                                }
                                else
                                {
                                    List<ResultData> node_list = (List<ResultData>)fee_type_hash[fee_item.FK_Fee_Type.Trim()];
                                    ResultData node = new ResultData();
                                    node.code = fee_item.FK_Fee_Type.Trim();
                                    node.message = fee_item.Type_Name.Trim();
                                    if (fee_item.Fee_Amount > fee_item.Fee_Payed)
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("欠费");
                                        node.data = tmp;
                                    }
                                    else
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("已缴");
                                        node.data = tmp;
                                    }
                                    node_list.Add(node);
                                }
                            }
                        }

                        if (multiple_must != null)
                        {
                            for (int j = 0; j < multiple_must.Count; j++)
                            {
                                string FK_Fee_Type = null;
                                string Type_Name = null;
                                bool find = false;//已交标志
                                for (int k = 0; k < multiple_must[j].Count; k++)
                                {
                                    Financial.Fee_Item fee_item = multiple_must[j][k];
                                    FK_Fee_Type = fee_item.FK_Fee_Type.Trim();
                                    Type_Name = fee_item.Type_Name.Trim();
                                    if (fee_item.Fee_Amount <= fee_item.Fee_Payed)
                                    {
                                        find = true;
                                        break;
                                    }
                                }

                                if (fee_type_hash[FK_Fee_Type.Trim()] == null)
                                {
                                    ResultData node = new ResultData();
                                    node.code = FK_Fee_Type.Trim();
                                    node.message = Type_Name.Trim();
                                    if (find)
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("已缴");
                                        node.data = tmp;
                                    }
                                    else
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("欠费");
                                        node.data = tmp;
                                    }
                                    List<ResultData> node_list = new List<ResultData>();
                                    node_list.Add(node);
                                    fee_type_hash.Add(FK_Fee_Type.Trim(), node_list);
                                }
                                else
                                {
                                    List<ResultData> node_list = (List<ResultData>)fee_type_hash[FK_Fee_Type.Trim()];
                                    ResultData node = new ResultData();
                                    node.code = FK_Fee_Type.Trim();
                                    node.message = Type_Name.Trim();
                                    if (find)
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("已缴");
                                        node.data = tmp;
                                    }
                                    else
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("欠费");
                                        node.data = tmp;
                                    }
                                    node_list.Add(node);
                                }
                            }
                        }

                        if (single_nomust != null)
                        {
                            for (int j = 0; j < single_nomust.Count; j++)
                            {
                                Financial.Fee_Item fee_item = single_nomust[j][0];
                                if (fee_type_hash[fee_item.FK_Fee_Type.Trim()] == null)
                                {
                                    ResultData node = new ResultData();
                                    node.code = fee_item.FK_Fee_Type.Trim();
                                    node.message = fee_item.Type_Name.Trim();
                                    if (fee_item.Fee_Amount > fee_item.Fee_Payed)
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("欠费");
                                        node.data = tmp;
                                    }
                                    else
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("已缴");
                                        node.data = tmp;
                                    }
                                    List<ResultData> node_list = new List<ResultData>();
                                    node_list.Add(node);
                                    fee_type_hash.Add(fee_item.FK_Fee_Type.Trim(), node_list);
                                }
                                else
                                {
                                    List<ResultData> node_list = (List<ResultData>)fee_type_hash[fee_item.FK_Fee_Type.Trim()];
                                    ResultData node = new ResultData();
                                    node.code = fee_item.FK_Fee_Type.Trim();
                                    node.message = fee_item.Type_Name.Trim();
                                    if (fee_item.Fee_Amount > fee_item.Fee_Payed)
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("欠费");
                                        node.data = tmp;
                                    }
                                    else
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("已缴");
                                        node.data = tmp;
                                    }
                                    node_list.Add(node);
                                }
                            }
                        }

                        if (multiple_nomust != null)
                        {
                            for (int j = 0; j < multiple_nomust.Count; j++)
                            {
                                string FK_Fee_Type = null;
                                string Type_Name = null;
                                bool find = false;//已交标志
                                for (int k = 0; k < multiple_nomust[j].Count; k++)
                                {
                                    Financial.Fee_Item fee_item = multiple_nomust[j][k];
                                    FK_Fee_Type = fee_item.FK_Fee_Type.Trim();
                                    Type_Name = fee_item.Type_Name.Trim();
                                    if (fee_item.Fee_Amount <= fee_item.Fee_Payed)
                                    {
                                        find = true;
                                        break;
                                    }
                                }

                                if (fee_type_hash[FK_Fee_Type.Trim()] == null)
                                {
                                    ResultData node = new ResultData();
                                    node.code = FK_Fee_Type.Trim();
                                    node.message = Type_Name.Trim();
                                    if (find)
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("已缴");
                                        node.data = tmp;
                                    }
                                    else
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("欠费");
                                        node.data = tmp;
                                    }
                                    List<ResultData> node_list = new List<ResultData>();
                                    node_list.Add(node);
                                    fee_type_hash.Add(FK_Fee_Type.Trim(), node_list);
                                }
                                else
                                {
                                    List<ResultData> node_list = (List<ResultData>)fee_type_hash[FK_Fee_Type.Trim()];
                                    ResultData node = new ResultData();
                                    node.code = FK_Fee_Type.Trim();
                                    node.message = Type_Name.Trim();
                                    if (find)
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("已缴");
                                        node.data = tmp;
                                    }
                                    else
                                    {
                                        List<string> tmp = new List<string>();
                                        tmp.Add(pk_sno);
                                        tmp.Add("欠费");
                                        node.data = tmp;
                                    }
                                    node_list.Add(node);
                                }
                            }
                        }
                    }
                }
                IDictionaryEnumerator en = fee_type_hash.GetEnumerator();  //  遍历哈希表所有的键,读出相应的值


                while (en.MoveNext())
                {
                    string key = en.Key.ToString().Trim();
                    List<ResultData> node_list = (List<ResultData>)en.Value;
                    ResultData node = node_list[0];

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.Caption = node.message.Trim();
                    column.ColumnName = node.message.Trim();
                    dt_fee.Columns.Add(column);
                }
                dt_fee.AcceptChanges();

                en.Reset();

                while (en.MoveNext())
                {
                    string key = en.Key.ToString().Trim();
                    List<ResultData> node_list = (List<ResultData>)en.Value;
                    for (int i = 0; i < node_list.Count; i++)
                    {
                        string columnname = node_list[i].message.Trim();
                        List<string> tmp = (List<string>)node_list[i].data;
                        string pk_sno = tmp[0];
                        string value = tmp[1];
                        bool find = false;
                        for (int j = 0; j < dt_fee.Rows.Count; j++)
                        {
                            if (dt_fee.Rows[j]["pk_sno"].ToString().Trim().Equals(pk_sno.Trim()))
                            {
                                dt_fee.Rows[j][columnname] = value;
                                find = true;
                                break;
                            }
                        }
                        if (!find)
                        {
                            row = dt_fee.NewRow();
                            row["pk_sno"] = pk_sno.Trim();
                            row[columnname] = value;
                            dt_fee.Rows.Add(row);
                        }
                        dt_fee.AcceptChanges();
                    }

                }
            }
            if (dt_fee == null || dt_fee.Rows.Count <= 0) { this.tsxx.Value = "<span style=\"font-size:Large;\"><font color=red>导出<b>失败</b>,请重试!</font></span>"; return; }
            dt_fee.Columns.Remove("pk_sno");
            #region 导出
            //引用EXCEL导出类
            toexcel xzfile = new toexcel();
            string filen = xzfile.DatatableToExcel(dt_fee, "学生缴费信息");
            

            if (filen.Length > 4)
            {
                this.tsxx.Value = "<span style=\"font-size:Large;\"> <font color=green>导出成功,请<a href=" + filen + " target=_blank >点此下载</a></font></span>";
                //this.g_ts.Text = "<font color=green>生成导入模板成功,请<a href=" + filen + " target=_blank >点此下载模板</a></font>";

            }
            else
            {
                this.tsxx.Value = "<span style=\"font-size:Large;\"><font color=red>导出<b>失败</b>,请重试!</font></span>";
            }
            #endregion
        }
    }
    
}
//返回给js客户端的数据格式
public class ResultData
{
    public string code { get; set; }
    public string message { get; set; }
    public Object data { get; set; }

}

public class fee
{
    public string code { get; set; }
    public string value { get; set; }
}
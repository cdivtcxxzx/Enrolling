using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public partial class nradmingl_appserver_manger : System.Web.UI.Page
{
    //返回给js客户端的数据格式
    public class ResultData
    {
        public string Code { get; set; }
        public string Msg { get; set; }
        public Object Value { get; set; }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ResultData result = new ResultData();
        result.Code = "failure";
        result.Msg = "无效参数";
        result.Value = null;
        try {
            string cs = Request.QueryString["cs"];//获取get的参数
            if (cs != null && cs.Trim().Length!=0)
            {
                #region NO:2 迎新批次 获取某迎新批次数据(批次编号)
                if (cs.Trim().Equals("get_freshbatch"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        fresh_batch jg = batch_logic.get_freshbatch(pk_batch_no);
                        result.Code = "success";
                        result.Msg = "成功";
                        result.Value = jg;
                    }
                }
                #endregion

                #region NO:3 校验某迎新批次当前是否有效(迎新编号)
                if (cs.Trim().Equals("get_freshbatch_isrun"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        bool jg=batch_logic.get_freshbatch_isrun(pk_batch_no);
                        result.Code = "success";
                        result.Msg = "成功";
                        result.Value = jg;                        
                    }
                }
                #endregion

                #region NO:5 操作员在某批次是否有效(批次编号,员工编号)
                if (cs.Trim().Equals("get_freshoperator_isauth"))
                {
                    string pk_batch_no = Request.QueryString["pk_batch_no"];
                    string pk_staff_no = Request.QueryString["pk_staff_no"];
                    if (pk_batch_no != null && pk_batch_no.Trim().Length != 0 && pk_staff_no != null && pk_staff_no.Trim().Length != 0)
                    {
                        batch batch_logic = new batch();
                        bool jg = batch_logic.get_freshoperator_isauth(pk_batch_no, pk_staff_no);
                        result.Code = "success";
                        result.Msg = "成功";
                        result.Value = jg;
                    }
                }
                #endregion



            }
        }
        catch (Exception ex)
        {
            result.Code = "error";
            result.Msg = ex.Message;
            result.Value = null;
        }
        string result_str = JsonConvert.SerializeObject(result);
        Response.Write(result_str);
    }
}
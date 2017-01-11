<%@ WebHandler Language="C#" Class="ajaxLanmHandler" %>

using System;
using System.Web;

public class ajaxLanmHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        if (context.Request["lmid"] != null & context.Request["sfxs"] != null & context.Request["item"] != null)
        {
            string strLmid = context.Request["lmid"].ToString();

            string strSfxs = "";
            if (context.Request["item"].ToString() == "sfqxyz")
            {

                if (context.Request["sfxs"].ToString() == "1")
                {
                    strSfxs ="1,";
                }
                else
                {
                    strSfxs = context.Request["sfxs"].ToString();
                }
            }
            else
            {
                strSfxs = context.Request["sfxs"].ToString();
            }
            string strItem = context.Request["item"].ToString();
            strSfxs = (strSfxs == "0" ? "1" : "0");
            try
            {
                string sqlStrUpdateLanm = "UPDATE xibu_lanm SET " + strItem + "=@xsIteml WHERE lmid=@lmid";
                int dt = Sqlhelper.ExcuteNonQuery(sqlStrUpdateLanm,
                    new System.Data.SqlClient.SqlParameter("xsIteml", strSfxs),
                    new System.Data.SqlClient.SqlParameter("lmid", strLmid));
                if (dt > 0)
                {

                        context.Response.Write("1");
                    
                }
                else
                {
                    context.Response.Write("0");
                }
            }
            catch
            {
                context.Response.Write("0");
            }
        }

        if (context.Request["lmid"] != null & context.Request["action"] == "Delete")
        {
            string strLmid = context.Request["lmid"].ToString();
            try
            {
                string sqlStrDeleteLanm = "DELETE  FROM xibu_lanm  WHERE lmid=@lmid";
                int dt = Sqlhelper.ExcuteNonQuery(sqlStrDeleteLanm, new System.Data.SqlClient.SqlParameter("lmid", strLmid));
                if (dt > 0)
                {
                    context.Response.Write("success");
                }
                else
                {
                    context.Response.Write("error");
                }
            }
            catch
            {
                context.Response.Write("error");
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
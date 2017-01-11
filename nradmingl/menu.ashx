<%@ WebHandler Language="C#" Class="Handler2" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
public class Handler2 : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";


        //左部菜单

        string sqlSerachByDhcdh = "SELECT * FROM lanm WHERE (sfcdxs=1 or sfdhxs=1) and fid='-1' order by px asc";
        DataTable dt = Sqlhelper.Serach(sqlSerachByDhcdh);
        string url = "";
        if (dt.Rows.Count > 0)
        {
            //{"title": "基本元素","icon": "fa-cubes","spread": true,"children": [{"title": "按钮","icon": "&#xe641;","href": "button.html"}
            context.Response.Write("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                url = dt.Rows[i]["url"].ToString();
                string sqlSerachByDhcdh2 = "SELECT * FROM lanm WHERE (sfcdxs=1 or sfdhxs=1) and fid='" + dt.Rows[i]["lmid"].ToString() + "' order by px asc";
                DataTable dt2 = Sqlhelper.Serach(sqlSerachByDhcdh2);
                
                    if(dt2.Rows.Count>0)
                    {
                        //context.Response.Write(sqlSerachByDhcdh2);
                     //展示二级栏目　
                        if (i == 0)
                        {
                            
                            context.Response.Write("{\"title\": \"" + dt.Rows[i]["lmmc"].ToString() + "\",\"icon\": \"" + dt.Rows[i]["lmfont"].ToString() + "\",\"spread\": true,\"children\": [");
                        }
                        else
                        {

                            context.Response.Write(",{\"title\": \"" + dt.Rows[i]["lmmc"].ToString() + "\",\"icon\": \"" + dt.Rows[i]["lmfont"].ToString() + "\",\"spread\": false,\"children\": [");
                        }
                        for (int c = 0; c < dt2.Rows.Count; c++)
                        {
                            url = dt2.Rows[c]["url"].ToString();
                            if (c == 0)
                            {
                                context.Response.Write("{\"title\": \"" + dt2.Rows[c]["lmmc"].ToString() + "\",\"icon\": \"" + dt2.Rows[c]["lmfont"].ToString() + "\",\"href\": \"" + url + "\" }");
                            }
                            else
                            {
                                context.Response.Write(",{\"title\": \"" + dt2.Rows[c]["lmmc"].ToString() + "\",\"icon\": \"" + dt2.Rows[c]["lmfont"].ToString() + "\",\"href\": \"" + url + "\" }");
                            }
                        }
                        context.Response.Write("]}");
                        
                    }
                    else
                    {
                         //无二级栏目
                        
                            if (i == 0)
                            {
                                context.Response.Write("{\"title\": \"" + dt.Rows[i]["lmmc"].ToString() + "\",\"icon\": \"" + dt.Rows[i]["lmfont"].ToString() + "\",\"spread\": true,\"href\": \""+url+"\" ");
                            }
                            else
                            {
                                context.Response.Write(",{\"title\": \"" + dt.Rows[i]["lmmc"].ToString() + "\",\"icon\": \"" + dt.Rows[i]["lmfont"].ToString() + "\",\"spread\":false,\"href\": \"" + url + "\" ");
                            }
                       
                       
                    }
                   // context.Response.Write("{\"title\": \"基本元素\",\"icon\": \"fa-cubes\",\"spread\": true,\"children\": ");
                    
                
            }
                
              
            context.Response.Write("]");
        }
        else
        {
            context.Response.Write("[]");
        }

        
        
        
        
        
        //context.Response.Write("alert('Hello World');");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_stdentlogin : System.Web.UI.Page
{    //获取验证码图案
    public System.Drawing.Image ReturnPhoto(byte[] streamByte)
    {
        System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
        System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
        return img;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ValidatedCode valcode = new ValidatedCode();
        string code = valcode.CreateVerifyCode();            //取随机码  
        Session["ValidateCode"] = code;
        byte[] bytes = valcode.CreateImage(code);       // 输出图片   
        Session["ValidatedCode"] = code;
        ReturnPhoto(bytes);

    }
}
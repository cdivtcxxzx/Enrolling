using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using model;

public partial class view_classmsg_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string pk_sno = null;//获取学号
        string msg = Request.QueryString["msg"].Trim();//获取msg主键
        if (Session["username"] == null)
        {
            msg_label.Text = "参数错误";
            return;
        }
       pk_sno = Session["username"].ToString().Trim();
        //pk_sno = "201756010203010";

        if(!messageService.isStuReadMsg(msg,pk_sno))
        {
            //没有阅读，插入记录
            messageService.addStuReadMsg(msg, pk_sno);
        }

        //显示信息
        ClassMsg getMsg = messageService.getMsgByPK_NO(msg);
        if (getMsg != null)
        {
            msg_label.Text = getMsg.Content;
        }
        else
        {
            msg_label.Text = "获取消息失败，请重试。";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using UserDep;
using System.Text;

public partial class admin_message_new : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            XmlDataSource XDdataSource = new XmlDataSource();
            XDdataSource.EnableCaching = false;
            XDdataSource.Data = new UserDep.Dep().getDep().ToString();
            tvwDeps.DataSource = XDdataSource;
            tvwDeps.DataBind();


        }
    }
    /// <summary>
    /// 发新消息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Action_Click(object sender, EventArgs e)
    {

        string title = TB_title.Text.ToString();
        string detail = TB_detail.Text.ToString();
        string senderid = Session["UserName"].ToString();
        if (ViewState["recids"] != null&&title!=""&&detail!="")
        {
            string[] recids = ViewState["recids"].ToString().TrimEnd(';').Split(';');
            if (new Message().NormalMessage(recids, senderid, title, detail, System.DateTime.Now)) { basic.MsgBox(this.Page, "发送成功", "message_new.aspx"); }
            else { basic.MsgBox(this.Page, "发送失败", ""); }
        }
        else { basic.MsgBox(this.Page, "题目、内容或接收人未填", ""); }
    }
    /// <summary>
    /// 获取部门（组）下的人员或下级组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvwDeps_SelectedNodeChanged(object sender, EventArgs e)
    {


        cbList.Items.Clear();
        string depId = tvwDeps.SelectedValue.ToString();
        List<User> users = new List<User>();
        //Dictionary<string, string> users = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(depId))
        {

            DataTable dt = Sqlhelper.Serach("select xm,yhid from yonghqx where yxdm='" + depId + "'");
            foreach (DataRow x in dt.Rows)
            {
                //users.Add(x["yhid"].ToString(), x["xm"].ToString());
                User user = new User();
                user.Name = x["xm"].ToString();
                user.Yhid = x["yhid"].ToString();
                users.Add(user);
            }
            cbList.DataSource = users;
            cbList.DataBind();
        }
    }
    /// <summary>
    /// 选择所有人员或组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAllRight_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in cbList.Items)
        {
            cbSelected.Items.Add(new ListItem(item.Text, item.Value));
        }
        cbList.Items.Clear();
    }

    /// <summary>
    /// 选择选中的人员或组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRight_Click(object sender, EventArgs e)
    {
        ListItemCollection items = new ListItemCollection();
        foreach (ListItem item in cbList.Items)
        {
            if (item.Selected)
            {
                if (!cbSelected.Items.Contains(new ListItem(item.Text, item.Value)))
                {
                    cbSelected.Items.Add(new ListItem(item.Text, item.Value));
                    items.Add(item);
                }
            }
        }
        if (items != null && items.Count > 0)
        {
            foreach (ListItem item in items)
            {
                cbList.Items.Remove(item);
            }
        }
    }

    /// <summary>
    /// 移除选中的人员或组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLeft_Click(object sender, EventArgs e)
    {
        ListItemCollection items = new ListItemCollection();
        foreach (ListItem item in cbSelected.Items)
        {
            if (item.Selected)
            {
                if (!cbList.Items.Contains(new ListItem(item.Text, item.Value)))
                {
                    cbList.Items.Add(new ListItem(item.Text, item.Value));
                    items.Add(item);
                }
            }
        }
        if (items != null && items.Count > 0)
        {
            foreach (ListItem item in items)
            {
                cbSelected.Items.Remove(item);
            }
        }
    }

    /// <summary>
    /// 移除所有人员或组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAllLeft_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in cbSelected.Items)
        {
            cbList.Items.Add(new ListItem(item.Text, item.Value));
        }
        cbSelected.Items.Clear();
    }
    /// <summary>
    /// 搜素人员或组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        cbList.Items.Clear();
        string key = idSearchString.Value.ToString();
        List<User> users = new List<User>();
        //Dictionary<string, string> users = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(key))
        {

            DataTable dt = Sqlhelper.Serach("select xm,yhid from yonghqx where xm like '%" + key + "%' or yhid like '%" + key + "%'");
            foreach (DataRow x in dt.Rows)
            {
                //users.Add(x["yhid"].ToString(), x["xm"].ToString());
                User user = new User();
                user.Name = x["xm"].ToString();
                user.Yhid = x["yhid"].ToString();
                users.Add(user);
            }
            cbList.DataSource = users;
            cbList.DataBind();
        }
    }


    /// <summary>
    /// 选择被评价人
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        StringBuilder recids = new StringBuilder();
        StringBuilder xms = new StringBuilder();
        if (cbSelected.Items.Count > 0)
        {
            foreach (ListItem selectedItem in cbSelected.Items)
            {

                xms.Append(selectedItem.Text + ";");
                recids.Append(selectedItem.Value + ";");

            }
        }
        TB_rec.Text = xms.ToString();
        ViewState["recids"] = recids;
        //ViewState["datepicker1"] = datepicker1.Value;
    }
    protected void reset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("message_new.aspx", true);
    }
}
using System;
using System.Collections.Generic;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/////添加包复选框的模板列的类
/// </summary>
public class MyTemplate : ITemplate
{
    private string strColumnName;
    private DataControlRowType dcrtColumnType;

    public MyTemplate()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 动态添加模版列
    /// </summary>
    /// <param name="strColumnName">列名</param>
    /// <param name="dcrtColumnType">列的类型</param>
    public MyTemplate(string strColumnName, DataControlRowType dcrtColumnType)
    {
        this.strColumnName = strColumnName;
        this.dcrtColumnType = dcrtColumnType;
    }

    public void InstantiateIn(Control ctlContainer)
    {
        switch (dcrtColumnType)
        {
            case DataControlRowType.Header: //列标题
                //添加选择列
                //<asp:TemplateField HeaderText="编号">  
                //<HeaderTemplate>  
                // <input type="checkbox" name="BoxIdAll" id="BoxIdAll" onclick="onclicksel();" />  
                // </HeaderTemplate>  
                // <ItemTemplate>  
                //   <input id="BoxId" name="BoxId" value='<%#(Convert.ToString(Eval("id")))%>' type="checkbox" />  
                //</ItemTemplate>  
                // //<ItemStyle Height="22px" HorizontalAlign="Center" />  
                // <HeaderStyle Width="3%" Height="28px" BackColor="#80B4CF" HorizontalAlign="Center" />  
                // </asp:TemplateField>  

                //如果头部使用标题则使用以下代码
                //Literal ltr = new Literal();
                //ltr.Text = strColumnName;
                //ctlContainer.Controls.Add(ltr);

                //头部也创建一个checkbox，则使用如下代码
                CheckBox cbh = new CheckBox();
                cbh.ID = "BoxIdAll";
                // textbox.Attributes.Add("onmouseover", "t.value=''");//
                cbh.Attributes.Add("onclick", "onclicksel()");
                cbh.Checked = false;
                ctlContainer.Controls.Add(cbh);
                break;
            case DataControlRowType.DataRow: //模版列内容——加载CheckBox
                CheckBox cb = new CheckBox();
                cb.ID = "BoxId";
                cb.Text = "<%#(Convert.ToString(Eval('yhid')))%>";
                cb.Checked = false;
                ctlContainer.Controls.Add(cb);
                break;
        }
    }

}

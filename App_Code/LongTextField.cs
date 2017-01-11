using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace myControls
{
    public class DeleteButtonField : ButtonField
    {
        private string _confirmText = "确认要删除吗？";
        public string ConfirmText
        {
            get { return _confirmText; }
            set { _confirmText = value; }
        }
        public DeleteButtonField()
        {
            this.CommandName = "删除";
            this.Text = "删除";
        }
        public override void InitializeCell(DataControlFieldCell cell,DataControlCellType cellType,DataControlRowState rowState,int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cellType == DataControlCellType.DataCell)
            {
                WebControl button = (WebControl)cell.Controls[0];
                button.Attributes["onclick"] = String.Format("return confirm('{0}');", _confirmText);

            }
        }
    }
    /// <summary>
    ///LongTextField 的摘要说明
    ///自定义控件
    /// </summary>
    /// 
    public class LongTextField : BoundField
    {
        private Unit _width = new Unit("300px");
        private Unit _height = new Unit("60px");
        public Unit Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public Unit Height
        {
            get { return _height; }
            set { _height = value; }

        }
        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            if ((rowState & DataControlRowState.Edit) == 0)
            {
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.Attributes["class"] = "logTextField";
                div.Style[HtmlTextWriterStyle.Width] = _width.ToString();
                div.Style[HtmlTextWriterStyle.Height] = _height.ToString();
                div.Style[HtmlTextWriterStyle.Overflow] = "auto";
                div.DataBinding += new EventHandler(div_DataBinding);
                cell.Controls.Add(div);
            }
            else
            {
                TextBox txtEdit = new TextBox();
                txtEdit.TextMode = TextBoxMode.MultiLine;
                txtEdit.Width = _width;
                txtEdit.Height = _height;
                txtEdit.DataBinding += new EventHandler(txtEdit_DataBinding);
                cell.Controls.Add(txtEdit);
            }
        }
        void div_DataBinding(object s, EventArgs e)
        {
            HtmlGenericControl div = (HtmlGenericControl)s;
            object value = this.GetValue(div.NamingContainer);
            div.InnerText = this.FormatDataValue(value, this.HtmlEncode);
        }
        void txtEdit_DataBinding(object s, EventArgs e)
        {
            TextBox txtEdit = (TextBox)s;
            object value = this.GetValue(txtEdit.NamingContainer);
            txtEdit.Text = this.FormatDataValue(value, this.HtmlEncode);
        }
    }
}
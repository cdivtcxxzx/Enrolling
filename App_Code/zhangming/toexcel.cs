using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;

using System.Text;
using Aspose.Cells;
using System.Data;

/// <summary>
///toexcel 的摘要说明
///使用aspose操作EXCEL文件
/// </summary>
public class toexcel
{
    private string outFileName = "";
    private string fullFilename = "";
    private Workbook book = null;
    private Worksheet sheet = null;
    public void AsposeExcel(string outfilename, string tempfilename) //导出构造数
    {
        outFileName = outfilename;
        book = new Workbook();
        // book.Open(tempfilename);这里我们暂时不用模板
        sheet = book.Worksheets[0];
    }
    public void AsposeExcel(string fullfilename) //导入构造数
    {
        fullFilename = fullfilename;
        // book = new Workbook();
        // book.Open(tempfilename);
        // sheet = book.Worksheets[0];
    }
    private void AddTitle(string title, int columnCount)
    {
        sheet.Cells.Merge(0, 0, 1, columnCount);
        sheet.Cells.Merge(1, 0, 1, columnCount);
        Cell cell1 = sheet.Cells[0, 0];
       
        cell1.PutValue(title);
        
        Cell cell2 = sheet.Cells[1, 0];
     }
    private void AddHeader(DataTable dt)
    {
        Cell cell = null;
        for (int col = 0; col < dt.Columns.Count; col++)
        {
            Style style = new Style();
            style.Pattern = BackgroundType.Solid;
            style.HorizontalAlignment = TextAlignmentType.Center;
            style.Font.Name = "黑体";
            style.Font.Size = 12;
            style.Font.IsBold = true;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            cell = sheet.Cells[0, col];
            cell.SetStyle(style);            
            cell.PutValue(dt.Columns[col].ColumnName);
    //cell.SetStyle.Font.IsBold = true;
        }
    }
    private void AddBody(DataTable dt)
    {
        for (int r = 0; r < dt.Rows.Count; r++)
        {
            if (r == 0)
            {
                Style style = new Style();
                style.Pattern = BackgroundType.Solid;
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.Name = "黑体";
                style.Font.Size = 10;
                style.Font.IsBold = true;
                style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                for (int c = 0; c < dt.Columns.Count; c++)
                {


                    // style.Font.IsBold = true;
                    sheet.Cells[r, c].SetStyle(style);
                    sheet.Cells[r, c].PutValue(dt.Columns[c].Caption.ToString());
                }
            }
            for (int c = 0; c < dt.Columns.Count; c++)
            {

                Style style = new Style();
                style.Pattern = BackgroundType.Solid;
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.Name = "宋体";
                style.Font.Size = 10;
                //style.Font.Color = System.Drawing.Color.Green;
                style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;


                // style.Font.IsBold = true;
                sheet.Cells[r + 1, c].SetStyle(style);
                sheet.Cells[r + 1, c].PutValue(dt.Rows[r][c].ToString());
            }
        }
    }
   
      //导出------------下一篇会用到这个方法
    /// <summary>
    ///将DATATABLE导出为EXCEL (现在使用的导出类)
    /// </summary>
    /// <param name="filename">要输出的文件名称</param>
    /// <param name="dt">DATATABLE</param>
    /// <returns>返回下载地址</returns>
    public string DatatableToExcel(DataTable dt,string filename)
    {
        string yn = "";
        try
        {

            string outfilename = System.Web.HttpContext.Current.Server.MapPath("~/upload/")+filename + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + ".xls";
            book = new Workbook();
            // book.Open(tempfilename);这里我们暂时不用模板
            sheet = book.Worksheets[0];
    
            AddBody(dt);
            sheet.AutoFitColumns();
            //sheet.AutoFitRows();
            book.Save(outfilename);
            if (File.Exists(outfilename))
            {
                yn = "/"+outfilename.Replace(System.Web.HttpContext.Current.Server.MapPath("~/"), "").Replace("\\", "/");
            }
            
            return yn;
        }
        catch (Exception e)
        {
            return yn;
            // throw e;
        }
    }
    /// <summary>
    ///通过EXCEL文件地址,将第一张表返回一个DATATABLE
    /// </summary>
    /// <param name="FileUpload1">一个上传表单按钮</param>
    /// <param name="col">是否将第一行做为列名</param>
    /// <returns>返回datatable</returns>
    public DataTable ExcelToDatatalbe(System.Web.UI.WebControls.FileUpload FileUpload1,bool col)//导入
    {
        #region 文件上传
        DataTable datab = new DataTable();
        datab.Columns.Add("序号");
        datab.Columns.Add("错误提示");
        string filename = "";
        int errjs = 0;
        Boolean fileOK = false;
        String path = System.Web.HttpContext.Current.Server.MapPath("~/upload/");
        if (FileUpload1.HasFile)
        {
            String fileExtension =
                System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
            filename = fileExtension.ToString();
            String[] allowedExtensions = { ".xls", ".xlsx" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    fileOK = true;
                }
                
            }
        }
        if (fileOK)
        {
            try
            {
                string username = "";
                try
                {
                    username = System.Web.HttpContext.Current.Session["UserName"].ToString().Trim();
                }
                catch
                {
                }
                string fileName = username+ "temp" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + filename;
                path += fileName;
                FileUpload1.SaveAs(path);
            }
            catch(Exception e2)
            {
                errjs++;
                datab.Rows.Add(errjs.ToString(), "<font color=red>上传文件出错：" + e2.Message+"</font>");
                return datab;
            }
        }
        else
        {
            errjs++;
            datab.Rows.Add(errjs.ToString(),"上传文件类型错误，只允许.xls和.xlsx！");
            return datab;
        }
        if (System.IO.File.Exists(path))
        {
            try
            {
                Workbook book = new Workbook();
                book.Open(path);
                Worksheet sheet = book.Worksheets[0];
                Cells cells = sheet.Cells;
                //获取excel中的数据保存到一个datatable中
                DataTable dt_Import = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, col);
                // dt_Import.
                return dt_Import;
            }
            catch(Exception e1)
            {
                errjs++;
                datab.Rows.Add(errjs.ToString(), "<font color=red>处理表格出错：" + e1.Message + "</font>");
                return datab;
            }    
        }
        else
        {
            errjs++;
            datab.Rows.Add(errjs.ToString(), "上传文件失败，请重新上传:未找到上传的文件！");
            return datab;
        }
        #endregion
        
    }



    /// <summary>  /// 导出到EXCEL表  /// </summary>        
    public void toExcel(string FileName, string SheetName, System.Data.DataTable dt)
    {
        
    }
    /// <summary>  /// 因为调用EXCEL后，结束时EXCEL进程并没有立即KILL，用这个方法调用可以KILL掉EXCEL  /// </summary>      
    public void drExcel(string FileName, string SheetName, System.Data.DataTable dt)
    {
        toExcel(FileName, SheetName, dt);
        GC.Collect();
    }

   
}

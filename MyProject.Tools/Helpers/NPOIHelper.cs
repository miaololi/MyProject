using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Linq;

namespace MyProject.Tools.Helpers
{
    public class NPOIMemoryStream : MemoryStream
    {
        public override void Close()
        {

        }
    }

    public class NPOIHelper
    {
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="headerText">表头文本</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public static MemoryStream DataTableToExcel(DataTable data, string fileName, string sheetName, string headerText, bool isColumnWritten = true)
        {
            try
            {
                if (data == null || data.Rows.Count == 0)
                {
                    headerText = "";
                    data = new DataTable();
                    data.Columns.Add("                               无数据                               ");
                    //data.Rows.Add("                               无数据                               ");
                }

                int maxRow = 0;
                IDataFormat format = null;
                IWorkbook workbook = null;
                
                //根据后缀名判断execl的版本
                if (fileName.ToString().ToLower().EndsWith(".xlsx")) //2007以上版本
                {
                    workbook = new XSSFWorkbook();
                    maxRow = 1048576;
                    format = workbook.CreateDataFormat() as XSSFDataFormat;
                }
                else
                {
                    workbook = new HSSFWorkbook();
                    maxRow = 65535;
                    format = workbook.CreateDataFormat() as HSSFDataFormat;
                }

                ISheet sheet = null;
                if (workbook != null)
                {
                    if (!string.IsNullOrEmpty(sheetName))
                        sheet = workbook.CreateSheet(sheetName);
                    else
                        sheet = workbook.CreateSheet();
                }
                else
                {
                    return null;
                }

                //日期样式
                ICellStyle dateStyle = workbook.CreateCellStyle();
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd HH:mm:ss");

                int rowIndex = 0;
                //标题行
                if (!string.IsNullOrEmpty(headerText))
                {
                    IRow headerRow = sheet.CreateRow(0);
                    headerRow.HeightInPoints = 25;
                    headerRow.CreateCell(0).SetCellValue(headerText);

                    ICellStyle headStyle = workbook.CreateCellStyle();
                    headStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                    //边框  
                    headStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    headStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    headStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    headStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    //边框颜色  
                    headStyle.BottomBorderColor = 8;
                    headStyle.TopBorderColor = 8;
                    IFont font = workbook.CreateFont();
                    font.FontHeightInPoints = 20;
                    font.Boldweight = 700;
                    headStyle.SetFont(font);

                    headerRow.GetCell(0).CellStyle = headStyle;

                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, data.Columns.Count - 1));
                    rowIndex++;
                }
                //写入DataTable的列名
                if (isColumnWritten) 
                {
                    var row = sheet.CreateRow(rowIndex);
                    for (int j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    rowIndex++;
                }
                //写入行内容
                foreach (DataRow row in data.Rows)
                {
                    if (rowIndex > maxRow)
                    {
                        rowIndex = 0;
                        sheet = workbook.CreateSheet();
                    }
                    if (rowIndex <= maxRow)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        foreach (DataColumn column in data.Columns)
                        {
                            ICell newCell = dataRow.CreateCell(column.Ordinal);
                            string drValue = row[column]?.ToString();
                            switch (column.DataType.ToString())
                            {
                                case "System.String": //字符串类型
                                    double result;
                                    if (double.TryParse(drValue, out result))
                                    {
                                        newCell.SetCellValue(drValue);
                                        break;
                                    }
                                    else
                                    {
                                        newCell.SetCellValue(drValue);
                                        break;
                                    }
                                case "System.DateTime": //日期类型
                                    DateTime dateV;
                                    newCell.SetCellValue(DateTime.TryParse(drValue, out dateV) ? drValue : "");
                                    newCell.CellStyle = dateStyle; //格式化显示
                                    break;
                                case "System.Boolean": //布尔型
                                    bool boolV = false;
                                    bool.TryParse(drValue, out boolV);
                                    newCell.SetCellValue(boolV);
                                    break;
                                case "System.Int16": //整型
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    int intV = 0;
                                    int.TryParse(drValue, out intV);
                                    newCell.SetCellValue(intV);
                                    break;
                                case "System.Decimal": //浮点型
                                case "System.Double":
                                    double doubV = 0;
                                    double.TryParse(drValue, out doubV);
                                    newCell.SetCellValue(doubV);
                                    break;
                                case "System.DBNull": //空值处理
                                    newCell.SetCellValue("");
                                    break;
                                default:
                                    newCell.SetCellValue("");
                                    break;
                            }
                        }
                        rowIndex++;
                    }
                }

                var dtResult = from a in data.AsEnumerable()
                               select a;

                int colLength = 0;

                //列宽自适应，只对英文和数字有效
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    //sheet.AutoSizeColumn(i);//自适应 速度慢
                    //sheet.SetColumnWidth(i, 16 * 256 + 200); // 200为常量，这样即可控制列宽为16

                    try
                    {
                        colLength = dtResult.Where(p => p[data.Columns[i].ColumnName] != DBNull.Value)
                            .Select(p => System.Text.Encoding.Default.GetBytes(p.Field<string>(data.Columns[i].ColumnName)).Length).Max();
                    }
                    catch
                    {

                    }

                    if (colLength < 12) { 
                        colLength = 12;
                    }
                    if (colLength > 100) { 
                        colLength = 100;
                    }
                    colLength += 4;
                    sheet.SetColumnWidth(i, colLength * 256 + 200); // 200为常量
                    colLength = 0;
                }

                NPOIMemoryStream ms = new NPOIMemoryStream();
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                return ms;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="fileName"文件名</param>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public static DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn)
        {
            try
            {
                var data = new DataTable();
                var startRow = 0;
                IWorkbook workbook = null;
                ISheet sheet = null;

                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.ToLower().EndsWith(".xlsx")) //2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.ToLower().EndsWith(".xls")) //2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    var firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            var cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                cell.SetCellType(CellType.String);
                                var cellValue = cell.StringCellValue;
                                if (!string.IsNullOrEmpty(cellValue))
                                {
                                    var column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        //生成列
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            var column = new DataColumn("列" + i);
                            data.Columns.Add(column);
                        }
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    var rowCount = sheet.LastRowNum;
                    for (var i = startRow; i <= rowCount; ++i)
                    {
                        var row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        var dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                return null;
            }
        }
    }
}

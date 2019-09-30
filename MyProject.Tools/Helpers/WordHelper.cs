using Aspose.Words;
using Aspose.Words.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MyProject.Tools
{
    public class WordHelper
    {
        #region 替换word模板内容
        /// <summary>
        /// 替换word模板内容
        /// </summary>
        /// <param name="filePath">模板路径</param>
        /// <param name="replaceContent">替换的内容</param>
        /// <param name="tableDictionary">表格列表数据</param>
        /// <param name="outPath">导出路径</param>
        /// <param name="type">导出类型(1word2pdf)</param>
        /// <returns></returns>
        public static bool ReplaceTempContent(string filePath, Dictionary<string, string> replaceContent, Dictionary<string, DataTable> tableDictionary, string outPath = "", int type = 1)
        {
            try
            {
                // 获取原Word文档地址
                //filePath = Path.GetFullPath(filePath);
                Document doc = new Document(filePath);

                NodeCollection allTables = doc.GetChildNodes(NodeType.Table, true);//获取所有的表格

                if (replaceContent != null && replaceContent.Count > 0)
                {
                    //循环所有字符名，替换Word书签内容
                    foreach (var key in replaceContent.Keys)
                    {
                        var repStr = "{{ " + key + " }}";
                        doc.Range.Replace(repStr, replaceContent[key].ToSafeString(), new Aspose.Words.Replacing.FindReplaceOptions { MatchCase = false, FindWholeWordsOnly = false });

                        //替换方案2，书签替换word模板中填充位置添加指定key的书签
                        //builder.MoveToBookmark(key);//将光标移入书签的位置
                        //builder.Write(replaceContent[key]);//填充内容
                    }
                }

                if (tableDictionary != null && tableDictionary.Count > 0)
                {
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    //循环动态添加table
                    foreach (var item in tableDictionary)
                    {
                        if (item.Value == null || item.Value.Columns.Count <= 0)
                        {
                            continue;
                        }

                        Table table = allTables[0] as Table;//拿到表格

                        List<double> widthList = new List<double>();//获取第一列的宽度
                        for (int i = 0; i < item.Value.Columns.Count; i++)
                        {
                            Cell cell = table.Rows[0].Cells[i];
                            double width = cell.CellFormat.Width.ToSafeInt32(0);
                            widthList.Add(width);
                        }

                        builder.MoveToBookmark(item.Key);//移到书签位置

                        for (var i = 0; i < item.Value.Rows.Count; i++)//第几行
                        {
                            List<string> rowValues = new List<string>();

                            for (var j = 0; j < item.Value.Columns.Count; j++)//第几个单元格
                            {
                                rowValues.Add(item.Value.Rows[i][j].ToSafeString());

                                //builder.InsertCell();
                                //builder.CellFormat.FitText = true;
                                //builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                                //builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                                //builder.CellFormat.Width = widthList[j];
                                //builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;//垂直居中对齐
                                //builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                //builder.Write(item.Value.Rows[i][j].ToString());
                            }

                            Row row = CreateRow(doc, item.Value.Columns.Count, rowValues, widthList);//新增列
                            table.Rows.Add(row);

                            //builder.EndRow();
                        }
                        doc.Range.Bookmarks[item.Key].Remove();
                    }
                }

                //保存word
                if (string.IsNullOrEmpty(outPath))
                {
                    doc.Save(filePath);
                }
                else
                {
                    if (type == 1)
                    {
                        doc.Save(outPath);
                    }
                    else
                    {
                        doc.Save(outPath, SaveFormat.Pdf);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 替换word模板内容 返回文件流
        /// </summary>
        /// <param name="filePath">模板路径</param>
        /// <param name="replaceContent">替换的内容</param>
        /// <param name="tableDictionary">表格列表数据</param>
        /// <returns></returns>
        public static byte[] ReplaceTempContent(string filePath, Dictionary<string, string> replaceContent, Dictionary<string, DataTable> tableDictionary)
        {
            try
            {
                // 获取原Word文档地址
                //filePath = Path.GetFullPath(filePath);
                Document doc = new Document(filePath);

                NodeCollection allTables = doc.GetChildNodes(NodeType.Table, true);//获取所有的表格

                if (replaceContent != null && replaceContent.Count > 0)
                {
                    //循环所有字符名，替换Word书签内容
                    foreach (var key in replaceContent.Keys)
                    {
                        var repStr = "{{ " + key + " }}";
                        doc.Range.Replace(repStr, replaceContent[key].ToSafeString(), new Aspose.Words.Replacing.FindReplaceOptions { MatchCase = false, FindWholeWordsOnly = false });

                        //替换方案2，书签替换word模板中填充位置添加指定key的书签
                        //builder.MoveToBookmark(key);//将光标移入书签的位置
                        //builder.Write(replaceContent[key]);//填充内容
                    }
                }

                if (tableDictionary != null && tableDictionary.Count > 0)
                {
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    //循环动态添加table
                    foreach (var item in tableDictionary)
                    {
                        if (item.Value == null || item.Value.Columns.Count <= 0)
                        {
                            continue;
                        }

                        Table table = allTables[0] as Table;//拿到表格

                        List<double> widthList = new List<double>();//获取第一列的宽度
                        for (int i = 0; i < item.Value.Columns.Count; i++)
                        {
                            Cell cell = table.Rows[0].Cells[i];
                            double width = cell.CellFormat.Width.ToSafeInt32(0);
                            widthList.Add(width);
                        }

                        builder.MoveToBookmark(item.Key);//移到书签位置

                        for (var i = 0; i < item.Value.Rows.Count; i++)//第几行
                        {
                            List<string> rowValues = new List<string>();

                            for (var j = 0; j < item.Value.Columns.Count; j++)//第几个单元格
                            {
                                rowValues.Add(item.Value.Rows[i][j].ToSafeString());

                                //builder.InsertCell();
                                //builder.CellFormat.FitText = true;
                                //builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                                //builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                                //builder.CellFormat.Width = widthList[j];
                                //builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;//垂直居中对齐
                                //builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                                //builder.Write(item.Value.Rows[i][j].ToString());
                            }

                            Row row = CreateRow(doc, item.Value.Columns.Count, rowValues, widthList);//新增列
                            table.Rows.Add(row);

                            //builder.EndRow();
                        }
                        doc.Range.Bookmarks[item.Key].Remove();
                    }
                }
                //保存byte
                Stream stream = new MemoryStream();
                doc.Save(stream, SaveFormat.Pdf);
                byte[] buffur = new byte[stream.Length];
                stream.Read(buffur, 0, buffur.Length);
                stream.Close();
                return buffur;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }
        #endregion
        #region 操作表格
        /// <summary>
        /// 添加一行
        /// </summary>
        /// <param name="doc">文档</param>
        /// <param name="columnCount">共多少列</param>
        /// <param name="columnValues">对应列的值</param>
        /// <param name="widthList">对应列的宽度</param>
        /// <returns></returns>
        private static Row CreateRow(Document doc, int columnCount, List<string> columnValues, List<double> widthList)
        {
            Row r2 = new Row(doc);
            for (int i = 0; i < columnCount; i++)
            {
                if (columnValues.Count > i)
                {
                    Cell cell = CreateCell(doc, columnValues[i], widthList[i]);
                    r2.Cells.Add(cell);
                }
                else
                {
                    Cell cell = CreateCell(doc, "", widthList[i]);
                    r2.Cells.Add(cell);
                }

            }
            return r2;
        }


        /// <summary>
        /// 添加一个单元格
        /// </summary>
        /// <param name="doc">文档</param>
        /// <param name="value">单元格内容</param>
        /// <param name="width">单元格宽度</param>
        /// <returns></returns>
        private static Cell CreateCell(Document doc, string value, double width)
        {
            Cell c1 = new Cell(doc);
            Paragraph p = new Paragraph(doc);
            p.AppendChild(new Run(doc, value));
            p.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
            p.ParagraphFormat.Style.Font.Size = 10;
            p.ParagraphFormat.Style.Font.Name = "微软雅黑";
            c1.AppendChild(p);
            //c1.CellFormat.FitText = true;
            //c1.CellFormat.Borders.LineStyle = LineStyle.Single;
            //c1.CellFormat.Borders.Color = System.Drawing.Color.Black;
            //c1.CellFormat.Width = width;
            //c1.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;//垂直居中对齐
            return c1;
        }
        #endregion

        /// <summary>
        /// Word转PDF
        /// </summary>
        /// <param name="wordPath"></param>
        /// <param name="pdfPath"></param>
        /// <returns></returns>
        public static bool WordToPdf(string wordPath, string pdfPath)
        {
            // 获取原Word文档完整地址
            //wordPath = Path.GetFullPath(wordPath);
            Document doc = new Document(wordPath);
            doc.Save(pdfPath, SaveFormat.Pdf);
            return true;
        }
    }
}

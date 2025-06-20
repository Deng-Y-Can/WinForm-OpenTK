using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.IO;
using System.Management;
using Color = DocumentFormat.OpenXml.Wordprocessing.Color;
using DataTable = System.Data.DataTable;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;

namespace WinFormsApp
{
    public partial class BookLabel : Form
    {
        private string excelFilePath = "";
        private string wordTemplatePath = "";
        private string outputFolder = "";
        // 定义默认样式（可根据需要修改）
        private static readonly TextFormat DefaultFormat = new TextFormat
        {
            FontName = "宋体",
            FontSize = 10.5, // 单位：磅（1/2点）
            Color = "00FF00" // 黑色
        };
        public BookLabel()
        {
            InitializeComponent();
        }

        private void btnSelectExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel文件|*.xlsx;*.xls";
                openFileDialog.Title = "选择Excel数据文件";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    excelFilePath = openFileDialog.FileName;
                    lblExcelPath.Text = excelFilePath;
                }
            }
        }

        private void btnSelectTemplate_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Word文档|*.docx";
                openFileDialog.Title = "选择Word模板文件";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    wordTemplatePath = openFileDialog.FileName;
                    lblTemplatePath.Text = wordTemplatePath;
                }
            }
        }

        private void btnSelectOutput_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "选择输出文件夹";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    outputFolder = folderBrowserDialog.SelectedPath;
                    lblOutputPath.Text = outputFolder;
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证文件和文件夹是否已选择
                if (string.IsNullOrEmpty(excelFilePath))
                {
                    MessageBox.Show("请选择Excel数据文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(wordTemplatePath))
                {
                    MessageBox.Show("请选择Word模板文件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(outputFolder))
                {
                    MessageBox.Show("请选择输出文件夹！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 验证文件是否存在
                if (!File.Exists(excelFilePath))
                {
                    MessageBox.Show($"Excel文件不存在：{excelFilePath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!File.Exists(wordTemplatePath))
                {
                    MessageBox.Show($"Word模板文件不存在：{wordTemplatePath}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 处理Excel数据到Word书签
                ProcessExcelToWord(excelFilePath, wordTemplatePath, outputFolder);

                Console.WriteLine("处理完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理过程中发生错误: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("按任意键退出...");
            }
        }


        static void ProcessExcelToWord(string excelFilePath, string wordTemplatePath, string outputFolder)
        {
            try
            {
                // 读取Excel数据
                DataTable excelData = ReadExcelData(excelFilePath);

                // 检查数据有效性
                if (excelData == null || excelData.Rows.Count == 0)
                {
                    Console.WriteLine("Excel文件中没有数据可处理！");
                    return;
                }

                // 处理每行数据
                for (int rowIndex = 0; rowIndex < excelData.Rows.Count; rowIndex++)
                {
                    try
                    {
                        // 输出文件路径
                        string outputFilePath = Path.Combine(outputFolder, $"output_{rowIndex + 1}.docx");

                        // 复制模板文件
                        File.Copy(wordTemplatePath, outputFilePath, true);

                        // 处理当前行数据
                        DataRow dataRow = excelData.Rows[rowIndex];
                        FillWordBookmarks(outputFilePath, dataRow);

                        Console.WriteLine($"已生成文件: {outputFilePath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"处理第 {rowIndex + 1} 行时发生错误: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"处理Excel到Word时发生错误: {ex.Message}", ex);
            }
        }

        static DataTable ReadExcelData(string excelFilePath)
        {
            try
            {
                using (FileStream fs = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read))
                {
                    // 创建工作簿（适用于.xlsx格式）
                    IWorkbook workbook = new XSSFWorkbook(fs);

                    // 获取第一个工作表
                    ISheet sheet = workbook.GetSheetAt(0);

                    // 创建DataTable
                    DataTable dataTable = new DataTable();

                    // 获取第一行作为列名
                    IRow headerRow = sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;

                    // 添加列
                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        DataColumn column = new DataColumn(headerRow.GetCell(i)?.StringCellValue ?? $"Column{i}");
                        dataTable.Columns.Add(column);
                    }

                    // 添加数据行（从第二行开始）
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;

                        DataRow dataRow = dataTable.NewRow();

                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                // 根据单元格类型获取值
                                switch (row.GetCell(j).CellType)
                                {
                                    case CellType.String:
                                        dataRow[j] = row.GetCell(j).StringCellValue;
                                        break;
                                    case CellType.Numeric:
                                        if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                        {
                                            dataRow[j] = row.GetCell(j).DateCellValue.ToString();
                                        }
                                        else
                                        {
                                            dataRow[j] = row.GetCell(j).NumericCellValue.ToString();
                                        }
                                        break;
                                    case CellType.Boolean:
                                        dataRow[j] = row.GetCell(j).BooleanCellValue.ToString();
                                        break;
                                    case CellType.Formula:
                                        dataRow[j] = row.GetCell(j).CellFormula;
                                        break;
                                    default:
                                        dataRow[j] = string.Empty;
                                        break;
                                }
                            }
                        }

                        dataTable.Rows.Add(dataRow);
                    }

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"读取Excel文件时发生错误: {ex.Message}", ex);
            }
        }

        static void FillWordBookmarks(string filePath, DataRow dataRow)
        {
            try
            {
                // 以可编辑模式打开Word文档
                using (WordprocessingDocument document = WordprocessingDocument.Open(filePath, true))
                {
                    // 获取主文档部分
                    MainDocumentPart mainPart = document.MainDocumentPart;

                    // 获取所有书签
                    Dictionary<string, BookmarkStart> bookmarks = GetBookmarks(mainPart);

                    // 遍历DataTable的列（第一行作为书签名）
                    foreach (DataColumn column in dataRow.Table.Columns)
                    {
                        string bookmarkName = column.ColumnName;
                        string cellValue = dataRow[column].ToString();

                        // 检查书签是否存在
                        if (bookmarks.ContainsKey(bookmarkName))
                        {
                            // 获取书签起始标记
                            BookmarkStart bookmarkStart = bookmarks[bookmarkName];

                            // 为不同的书签定义不同的格式（示例：根据书签名决定格式）
                            TextFormat format = GetFormatForBookmark(bookmarkName);

                            // 在书签位置插入格式化文本
                            InsertFormattedTextAfterBookmark(mainPart, bookmarkStart, cellValue, format);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"填充Word书签时发生错误: {ex.Message}", ex);
            }
        }

        static Dictionary<string, BookmarkStart> GetBookmarks(MainDocumentPart mainPart)
        {
            // 获取所有书签起始标记
            var bookmarkStarts = mainPart.Document.Body.Descendants<BookmarkStart>();

            // 构建书签名到书签起始标记的映射
            return bookmarkStarts.ToDictionary(b => b.Name.Value, b => b);
        }

        // 根据书签名获取对应的格式（可根据需求扩展）
        static TextFormat GetFormatForBookmark(string bookmarkName)
        {
            // 示例：为特定书签定义不同的格式
            if (bookmarkName.Contains("标题"))
            {
                return new TextFormat
                {
                    FontName = "黑体",
                    FontSize = 16,
                    Color = "FF0000" // 红色
                };
            }
            else if (bookmarkName.Contains("日期"))
            {
                return new TextFormat
                {
                    FontName = "楷体",
                    FontSize = 10.5,
                    Color = "0000FF" // 蓝色
                };
            }

            // 默认格式
            return DefaultFormat;
        }

        // 在书签后插入格式化文本
        static void InsertFormattedTextAfterBookmark(MainDocumentPart mainPart, BookmarkStart bookmarkStart, string text, TextFormat format)
        {
            try
            {
                // 获取书签所在的段落
                Paragraph paragraph = bookmarkStart.Ancestors<Paragraph>().FirstOrDefault();
                if (paragraph == null) return;

                // 将ChildElements转换为普通列表，以便使用IndexOf方法
                var elementsList = paragraph.ChildElements.ToList();

                // 获取书签在段落中的位置
                int index = elementsList.IndexOf(bookmarkStart);

                // 创建带有格式的运行元素
                RunProperties runProperties = new RunProperties();

                // 设置字体
                if (!string.IsNullOrEmpty(format.FontName))
                {
                    runProperties.Append(new RunFonts { Ascii = format.FontName, EastAsia = format.FontName });
                }

                // 设置字号（注意：Open XML中字号以1/2点为单位）
                if (format.FontSize > 0)
                {
                    runProperties.Append(new FontSize { Val = (format.FontSize * 2).ToString() });
                }

                // 设置颜色
                if (!string.IsNullOrEmpty(format.Color))
                {
                    runProperties.Append(new Color { Val = format.Color });
                }

                // 创建运行元素并添加文本和格式
                Run newRun = new Run(
                    runProperties,
                    new Text(text)
                );

                // 在书签后插入新的运行元素
                if (index >= 0 && index < elementsList.Count - 1)
                {
                    paragraph.InsertAfter(newRun, elementsList[index]);
                }
                else
                {
                    // 如果书签是段落的最后一个元素，则直接添加到末尾
                    paragraph.AppendChild(newRun);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"插入格式化文本时出错: {ex.Message}");
                // 可以选择记录错误但继续处理其他书签
            }
        }
    }

    // 文本格式类
    public class TextFormat
    {
        public string FontName { get; set; }
        public double FontSize { get; set; }
        public string Color { get; set; } // 十六进制颜色代码，例如"FF0000"表示红色
    }

}

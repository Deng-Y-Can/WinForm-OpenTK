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

        // 新增变量，用于存储用户选择的格式
        private TextFormat userSelectedFormat = new TextFormat
        {
            FontName = "宋体",
            FontSize = 10.5,
            Color = "000000" // 黑色
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

        // 字体选择按钮点击事件
        private void btnSelectFont_Click(object sender, EventArgs e)
        {
            // 配置字体对话框
            try
            {
                // 确保使用当前选择的字体和大小初始化对话框
                fontDialog.Font = new System.Drawing.Font(
                    userSelectedFormat.FontName,
                    (float)userSelectedFormat.FontSize,
                    System.Drawing.FontStyle.Regular,
                    System.Drawing.GraphicsUnit.Point,
                    134); // 中文字体

                fontDialog.Color = ColorTranslator.FromHtml("#" + userSelectedFormat.Color);
                fontDialog.ShowColor = true;

                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    // 更新用户选择的格式
                    userSelectedFormat.FontName = fontDialog.Font.Name;
                    userSelectedFormat.FontSize = Math.Round(fontDialog.Font.Size, 1); // 保留一位小数

                    // 处理颜色选择
                    string colorHex = ColorTranslator.ToHtml(fontDialog.Color);
                    userSelectedFormat.Color = colorHex.Replace("#", "");

                    // 更新字体大小控件值
                    numFontSize.Value = (decimal)userSelectedFormat.FontSize;

                    // 更新显示
                    UpdateFormatDisplay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"选择字体时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 颜色选择按钮点击事件
        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    colorDialog.Color = ColorTranslator.FromHtml("#" + userSelectedFormat.Color);

            //    if (colorDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        // 更新颜色
            //        string colorHex = ColorTranslator.ToHtml(colorDialog.Color);
            //        userSelectedFormat.Color = colorHex.Replace("#", "");
            //        UpdateFormatDisplay();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"选择颜色时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            try
            {
                colorDialog.Color = ColorTranslator.FromHtml("#" + userSelectedFormat.Color);

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // 获取用户选择的颜色
                    string colorHex = ColorTranslator.ToHtml(colorDialog.Color);

                    // 检查是否是颜色名称而非十六进制代码
                    if (!colorHex.StartsWith("#"))
                    {
                        // 尝试从映射表中获取对应的十六进制值
                        string hexValue;
                        if (ColorNameToHexMap.TryGetValue(colorHex.ToLower(), out hexValue))
                        {
                            colorHex = hexValue;
                        }
                        else
                        {
                            // 如果映射表中没有，使用黑色
                            colorHex = "#000000";
                        }
                    }
                    else
                    {
                        // 去除#前缀
                        colorHex = colorHex.Replace("#", "");
                    }

                    // 更新颜色设置
                    userSelectedFormat.Color = colorHex;
                    UpdateFormatDisplay();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"选择颜色时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        // 颜色名称到十六进制值的映射表
        private static readonly Dictionary<string, string> ColorNameToHexMap = new Dictionary<string, string>
{
    { "aliceblue", "F0F8FF" },
    { "antiquewhite", "FAEBD7" },
    { "aqua", "00FFFF" },
    { "aquamarine", "7FFFD4" },
    { "azure", "F0FFFF" },
    { "beige", "F5F5DC" },
    { "bisque", "FFE4C4" },
    { "black", "000000" },
    { "blanchedalmond", "FFEBCD" },
    { "blue", "0000FF" },
    { "blueviolet", "8A2BE2" },
    { "brown", "A52A2A" },
    { "burlywood", "DEB887" },
    { "cadetblue", "5F9EA0" },
    { "chartreuse", "7FFF00" },
    { "chocolate", "D2691E" },
    { "coral", "FF7F50" },
    { "cornflowerblue", "6495ED" },
    { "cornsilk", "FFF8DC" },
    { "crimson", "DC143C" },
    { "cyan", "00FFFF" },
    { "darkblue", "00008B" },
    { "darkcyan", "008B8B" },
    { "darkgoldenrod", "B8860B" },
    { "darkgray", "A9A9A9" },
    { "darkgreen", "006400" },
    { "darkkhaki", "BDB76B" },
    { "darkmagenta", "8B008B" },
    { "darkolivegreen", "556B2F" },
    { "darkorange", "FF8C00" },
    { "darkorchid", "9932CC" },
    { "darkred", "8B0000" },
    { "darksalmon", "E9967A" },
    { "darkseagreen", "8FBC8F" },
    { "darkslateblue", "483D8B" },
    { "darkslategray", "2F4F4F" },
    { "darkturquoise", "00CED1" },
    { "darkviolet", "9400D3" },
    { "deeppink", "FF1493" },
    { "deepskyblue", "00BFFF" },
    { "dimgray", "696969" },
    { "dodgerblue", "1E90FF" },
    { "firebrick", "B22222" },
    { "floralwhite", "FFFAF0" },
    { "forestgreen", "228B22" },
    { "fuchsia", "FF00FF" },
    { "gainsboro", "DCDCDC" },
    { "ghostwhite", "F8F8FF" },
    { "gold", "FFD700" },
    { "goldenrod", "DAA520" },
    { "gray", "808080" },
    { "green", "008000" },
    { "greenyellow", "ADFF2F" },
    { "honeydew", "F0FFF0" },
    { "hotpink", "FF69B4" },
    { "indianred", "CD5C5C" },
    { "indigo", "4B0082" },
    { "ivory", "FFFFF0" },
    { "khaki", "F0E68C" },
    { "lavender", "E6E6FA" },
    { "lavenderblush", "FFF0F5" },
    { "lawngreen", "7CFC00" },
    { "lemonchiffon", "FFFACD" },
    { "lightblue", "ADD8E6" },
    { "lightcoral", "F08080" },
    { "lightcyan", "E0FFFF" },
    { "lightgoldenrodyellow", "FAFAD2" },
    { "lightgray", "D3D3D3" },
    { "lightgreen", "90EE90" },
    { "lightpink", "FFB6C1" },
    { "lightsalmon", "FFA07A" },
    { "lightseagreen", "20B2AA" },
    { "lightskyblue", "87CEFA" },
    { "lightslategray", "778899" },
    { "lightsteelblue", "B0C4DE" },
    { "lightyellow", "FFFFE0" },
    { "lime", "00FF00" },
    { "limegreen", "32CD32" },
    { "linen", "FAF0E6" },
    { "magenta", "FF00FF" },
    { "maroon", "800000" },
    { "mediumaquamarine", "66CDAA" },
    { "mediumblue", "0000CD" },
    { "mediumorchid", "BA55D3" },
    { "mediumpurple", "9370DB" },
    { "mediumseagreen", "3CB371" },
    { "mediumslateblue", "7B68EE" },
    { "mediumspringgreen", "00FA9A" },
    { "mediumturquoise", "48D1CC" },
    { "mediumvioletred", "C71585" },
    { "midnightblue", "191970" },
    { "mintcream", "F5FFFA" },
    { "mistyrose", "FFE4E1" },
    { "moccasin", "FFE4B5" },
    { "navajowhite", "FFDEAD" },
    { "navy", "000080" },
    { "oldlace", "FDF5E6" },
    { "olive", "808000" },
    { "olivedrab", "6B8E23" },
    { "orange", "FFA500" },
    { "orangered", "FF4500" },
    { "orchid", "DA70D6" },
    { "palegoldenrod", "EEE8AA" },
    { "palegreen", "98FB98" },
    { "paleturquoise", "AFEEEE" },
    { "palevioletred", "DB7093" },
    { "papayawhip", "FFEFD5" },
    { "peachpuff", "FFDAB9" },
    { "peru", "CD853F" },
    { "pink", "FFC0CB" },
    { "plum", "DDA0DD" },
    { "powderblue", "B0E2FF" },
    { "purple", "800080" },
    { "red", "FF0000" },
    { "rosybrown", "BC8F8F" },
    { "royalblue", "4169E1" },
    { "saddlebrown", "8B4513" },
    { "salmon", "FA8072" },
    { "sandybrown", "F4A460" },
    { "seagreen", "2E8B57" },
    { "seashell", "FFF5EE" },
    { "sienna", "A0522D" },
    { "silver", "C0C0C0" },
    { "skyblue", "87CEEB" },
    { "slateblue", "6A5ACD" },
    { "slategray", "708090" },
    { "snow", "FFFAFA" },
    { "springgreen", "00FF7F" },
    { "steelblue", "4682B4" },
    { "tan", "D2B48C" },
    { "teal", "008080" },
    { "thistle", "D8BFD8" },
    { "tomato", "FF6347" },
    { "turquoise", "40E0D0" },
    { "violet", "EE82EE" },
    { "wheat", "F5DEB3" },
    { "white", "FFFFFF" },
    { "whitesmoke", "F5F5F5" },
    { "yellow", "FFFF00" },
    { "yellowgreen", "9ACD32" }
};

        // 字体大小更改事件
        private void numFontSize_ValueChanged(object sender, EventArgs e)
        {
            userSelectedFormat.FontSize = (double)numFontSize.Value;
            UpdateFormatDisplay();
        }

        // 更新格式显示
        private void UpdateFormatDisplay()
        {
            string fontDisplay = $"{userSelectedFormat.FontName}, {userSelectedFormat.FontSize}pt";
            lblCurrentFont.Text = "当前字体: " + fontDisplay;

            try
            {
                // 设置颜色显示文本的颜色
                lblCurrentColor.Text = "当前颜色: " + GetColorName(userSelectedFormat.Color);
                lblCurrentColor.ForeColor = ColorTranslator.FromHtml("#" + userSelectedFormat.Color);
            }
            catch (Exception ex)
            {
                // 如果颜色转换失败，显示默认文本
                lblCurrentColor.Text = "当前颜色: 自定义颜色";
                lblCurrentColor.ForeColor = System.Drawing.Color.Black;
                MessageBox.Show($"显示颜色时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 根据颜色代码获取颜色名称（增强实现）
        private string GetColorName(string colorCode)
        {
            try
            {
                // 尝试将颜色代码转换为Color对象
                System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml("#" + colorCode);

                // 检查是否是已知颜色
                if (color.IsKnownColor)
                {
                    return color.Name;
                }

                // 检查是否是系统颜色
                if (color.IsSystemColor)
                {
                    return "系统颜色: " + color.Name;
                }

                // 对于自定义颜色，提供更详细的信息
                return $"RGB({color.R}, {color.G}, {color.B})";
            }
            catch
            {
                // 如果转换失败，返回原始代码
                return $"#{colorCode}";
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

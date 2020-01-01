using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using NPOI.XSSF.Model;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace NPOI.SS.Converter
{
	public class ExcelToHtmlConverter
	{
		private POILogger logger = POILogFactory.GetLogger(typeof(ExcelToHtmlConverter));

		private DataFormatter _formatter = new DataFormatter();

		private string cssClassContainerCell;

		private string cssClassContainerDiv;

		private string cssClassTable;

		private Dictionary<short, string> excelStyleToClass = new Dictionary<short, string>();

		private HtmlDocumentFacade htmlDocumentFacade;

		private bool outputColumnHeaders = true;

		private bool outputHiddenColumns;

		private bool outputHiddenRows;

		private bool outputLeadingSpacesAsNonBreaking = true;

		private bool outputRowNumbers = true;

		private bool useDivsToSpan;

		/// <summary>
		/// 是否输出列头
		/// </summary>
		public bool OutputColumnHeaders
		{
			get
			{
				return outputColumnHeaders;
			}
			set
			{
				outputColumnHeaders = value;
			}
		}

		/// <summary>
		/// 是否输出隐藏的列
		/// </summary>
		public bool OutputHiddenColumns
		{
			get
			{
				return outputHiddenColumns;
			}
			set
			{
				outputHiddenColumns = value;
			}
		}

		/// <summary>
		/// 是否输出隐藏的行
		/// </summary>
		public bool OutputHiddenRows
		{
			get
			{
				return outputHiddenRows;
			}
			set
			{
				outputHiddenRows = value;
			}
		}

		/// <summary>
		/// 是否输出文本前的空格
		/// </summary>
		public bool OutputLeadingSpacesAsNonBreaking
		{
			get
			{
				return outputLeadingSpacesAsNonBreaking;
			}
			set
			{
				outputLeadingSpacesAsNonBreaking = value;
			}
		}

		/// <summary>
		/// 是否输出行号
		/// </summary>
		public bool OutputRowNumbers
		{
			get
			{
				return outputRowNumbers;
			}
			set
			{
				outputRowNumbers = value;
			}
		}

		/// <summary>
		/// 在跨列的单元格使用DIV标记
		/// </summary>
		public bool UseDivsToSpan
		{
			get
			{
				return useDivsToSpan;
			}
			set
			{
				useDivsToSpan = value;
			}
		}

		public XmlDocument Document
		{
			get
			{
				return htmlDocumentFacade.Document;
			}
		}

		public ExcelToHtmlConverter()
		{
			XmlDocument document = new XmlDocument();
			htmlDocumentFacade = new HtmlDocumentFacade(document);
			cssClassTable = htmlDocumentFacade.GetOrCreateCssClass("table", "t", "border-collapse:collapse;border-spacing:0;");
		}

		protected static int GetColumnWidth(ISheet sheet, int columnIndex)
		{
			return ExcelToHtmlUtils.GetColumnWidthInPx(sheet.GetColumnWidth(columnIndex));
		}

		public static XmlDocument Process(string excelFile)
		{
			IWorkbook workbook = WorkbookFactory.Create(excelFile);
			ExcelToHtmlConverter excelToHtmlConverter = new ExcelToHtmlConverter();
			excelToHtmlConverter.ProcessWorkbook(workbook);
			return excelToHtmlConverter.Document;
		}

		public void ProcessWorkbook(IWorkbook workbook)
		{
			ProcessDocumentInformation(workbook);
			if (UseDivsToSpan)
			{
				cssClassContainerCell = htmlDocumentFacade.GetOrCreateCssClass("td", "c", "padding:0;margin:0;align:left;vertical-align:top;");
				cssClassContainerDiv = htmlDocumentFacade.GetOrCreateCssClass("div", "d", "position:relative;");
			}
			for (int i = 0; i < workbook.NumberOfSheets; i++)
			{
				ISheet sheetAt = workbook.GetSheetAt(i);
				ProcessSheet(sheetAt);
			}
			htmlDocumentFacade.UpdateStylesheet();
		}

		protected void ProcessSheet(ISheet sheet)
		{
			ProcessSheetHeader(htmlDocumentFacade.Body, sheet);
			int physicalNumberOfRows = sheet.PhysicalNumberOfRows;
			if (physicalNumberOfRows > 0)
			{
				XmlElement xmlElement = htmlDocumentFacade.CreateTable();
				xmlElement.SetAttribute("class", cssClassTable);
				XmlElement xmlElement2 = htmlDocumentFacade.CreateTableBody();
				CellRangeAddress[][] mergedRanges = ExcelToHtmlUtils.BuildMergedRangesMap(sheet);
				List<XmlElement> list = new List<XmlElement>(physicalNumberOfRows);
				int num = 1;
				for (int i = 0; i < physicalNumberOfRows; i++)
				{
					IRow row = sheet.GetRow(i);
					if (row != null && (OutputHiddenRows || !row.ZeroHeight))
					{
						XmlElement xmlElement3 = htmlDocumentFacade.CreateTableRow();
						htmlDocumentFacade.AddStyleClass(xmlElement3, "r", "height:" + (float)row.Height / 20f + "pt;");
						int num2 = ProcessRow(mergedRanges, row, xmlElement3);
						if (num2 == 0)
						{
							list.Add(xmlElement3);
						}
						else
						{
							if (list.Count > 0)
							{
								foreach (XmlElement item in list)
								{
									xmlElement2.AppendChild(item);
								}
								list.Clear();
							}
							xmlElement2.AppendChild(xmlElement3);
						}
						num = Math.Max(num, num2);
					}
				}
				ProcessColumnWidths(sheet, num, xmlElement);
				if (OutputColumnHeaders)
				{
					ProcessColumnHeaders(sheet, num, xmlElement);
				}
				xmlElement.AppendChild(xmlElement2);
				htmlDocumentFacade.Body.AppendChild(xmlElement);
			}
		}

		protected void ProcessSheetHeader(XmlElement htmlBody, ISheet sheet)
		{
			XmlElement xmlElement = htmlDocumentFacade.CreateHeader2();
			xmlElement.AppendChild(htmlDocumentFacade.CreateText(sheet.SheetName));
			htmlBody.AppendChild(xmlElement);
		}

		protected void ProcessDocumentInformation(IWorkbook workbook)
		{
			if (workbook is HSSFWorkbook)
			{
				SummaryInformation summaryInformation = ((HSSFWorkbook)workbook).SummaryInformation;
				if (summaryInformation != null)
				{
					if (!string.IsNullOrEmpty(summaryInformation.Title))
					{
						htmlDocumentFacade.Title = summaryInformation.Title;
					}
					if (!string.IsNullOrEmpty(summaryInformation.Author))
					{
						htmlDocumentFacade.AddAuthor(summaryInformation.Author);
					}
					if (!string.IsNullOrEmpty(summaryInformation.Keywords))
					{
						htmlDocumentFacade.AddKeywords(summaryInformation.Keywords);
					}
					if (!string.IsNullOrEmpty(summaryInformation.Comments))
					{
						htmlDocumentFacade.AddDescription(summaryInformation.Comments);
					}
				}
			}
			else if (workbook is XSSFWorkbook)
			{
				POIXMLProperties properties = ((XSSFWorkbook)workbook).GetProperties();
				if (!string.IsNullOrEmpty(properties.CoreProperties.Title))
				{
					htmlDocumentFacade.Title = properties.CoreProperties.Title;
				}
				if (!string.IsNullOrEmpty(properties.CoreProperties.Creator))
				{
					htmlDocumentFacade.AddAuthor(properties.CoreProperties.Creator);
				}
				if (!string.IsNullOrEmpty(properties.CoreProperties.Keywords))
				{
					htmlDocumentFacade.AddKeywords(properties.CoreProperties.Keywords);
				}
				if (!string.IsNullOrEmpty(properties.CoreProperties.Description))
				{
					htmlDocumentFacade.AddDescription(properties.CoreProperties.Description);
				}
			}
		}

		/// @return maximum 1-base index of column that were rendered, zero if none
		protected int ProcessRow(CellRangeAddress[][] mergedRanges, IRow row, XmlElement tableRowElement)
		{
			ISheet sheet = row.Sheet;
			int lastCellNum = row.LastCellNum;
			if (lastCellNum <= 0)
			{
				return 0;
			}
			List<XmlElement> list = new List<XmlElement>(lastCellNum);
			if (OutputRowNumbers)
			{
				XmlElement xmlElement = htmlDocumentFacade.CreateTableHeaderCell();
				ProcessRowNumber(row, xmlElement);
				list.Add(xmlElement);
			}
			int num = 0;
			for (int i = 0; i < lastCellNum; i++)
			{
				if (OutputHiddenColumns || !sheet.IsColumnHidden(i))
				{
					CellRangeAddress mergedRange = ExcelToHtmlUtils.GetMergedRange(mergedRanges, row.RowNum, i);
					if (mergedRange == null || (mergedRange.FirstColumn == i && mergedRange.FirstRow == row.RowNum))
					{
						ICell cell = row.GetCell(i);
						int num2 = 0;
						if (UseDivsToSpan)
						{
							num2 = GetColumnWidth(sheet, i);
							bool flag = false;
							for (int j = i + 1; j < lastCellNum; j++)
							{
								if (OutputHiddenColumns || !sheet.IsColumnHidden(j))
								{
									if (row.GetCell(j) != null && !IsTextEmpty(row.GetCell(j)))
									{
										flag = true;
										break;
									}
									num2 += GetColumnWidth(sheet, j);
								}
							}
							if (!flag)
							{
								num2 = 2147483647;
							}
						}
						XmlElement xmlElement2 = htmlDocumentFacade.CreateTableCell();
						if (mergedRange != null)
						{
							if (mergedRange.FirstColumn != mergedRange.LastColumn)
							{
								xmlElement2.SetAttribute("colspan", (mergedRange.LastColumn - mergedRange.FirstColumn + 1).ToString());
							}
							if (mergedRange.FirstRow != mergedRange.LastRow)
							{
								xmlElement2.SetAttribute("rowspan", (mergedRange.LastRow - mergedRange.FirstRow + 1).ToString());
							}
						}
						if (cell == null || ProcessCell(cell, xmlElement2, GetColumnWidth(sheet, i), num2, (float)row.Height / 20f))
						{
							list.Add(xmlElement2);
						}
						else
						{
							foreach (XmlElement item in list)
							{
								tableRowElement.AppendChild(item);
							}
							list.Clear();
							tableRowElement.AppendChild(xmlElement2);
							num = i;
						}
					}
				}
			}
			return num + 1;
		}

		private string GetRowName(IRow row)
		{
			return (row.RowNum + 1).ToString();
		}

		protected void ProcessRowNumber(IRow row, XmlElement tableRowNumberCellElement)
		{
			tableRowNumberCellElement.SetAttribute("class", "rownumber");
			XmlText newChild = htmlDocumentFacade.CreateText(GetRowName(row));
			tableRowNumberCellElement.AppendChild(newChild);
		}

		/// Creates COLGROUP element with width specified for all columns. (Except
		/// first if <tt>{@link #isOutputRowNumbers()}==true</tt>)
		protected void ProcessColumnWidths(ISheet sheet, int maxSheetColumns, XmlElement table)
		{
			XmlElement xmlElement = htmlDocumentFacade.CreateTableColumnGroup();
			if (OutputRowNumbers)
			{
				xmlElement.AppendChild(htmlDocumentFacade.CreateTableColumn());
			}
			for (int i = 0; i < maxSheetColumns; i++)
			{
				if (OutputHiddenColumns || !sheet.IsColumnHidden(i))
				{
					XmlElement xmlElement2 = htmlDocumentFacade.CreateTableColumn();
					xmlElement2.SetAttribute("width", GetColumnWidth(sheet, i).ToString());
					xmlElement.AppendChild(xmlElement2);
				}
			}
			table.AppendChild(xmlElement);
		}

		protected void ProcessColumnHeaders(ISheet sheet, int maxSheetColumns, XmlElement table)
		{
			XmlElement xmlElement = htmlDocumentFacade.CreateTableHeader();
			table.AppendChild(xmlElement);
			XmlElement xmlElement2 = htmlDocumentFacade.CreateTableRow();
			if (OutputRowNumbers)
			{
				xmlElement2.AppendChild(htmlDocumentFacade.CreateTableHeaderCell());
			}
			for (int i = 0; i < maxSheetColumns; i++)
			{
				if (OutputHiddenColumns || !sheet.IsColumnHidden(i))
				{
					XmlElement xmlElement3 = htmlDocumentFacade.CreateTableHeaderCell();
					string columnName = GetColumnName(i);
					xmlElement3.AppendChild(htmlDocumentFacade.CreateText(columnName));
					xmlElement2.AppendChild(xmlElement3);
				}
			}
			xmlElement.AppendChild(xmlElement2);
		}

		protected string GetColumnName(int columnIndex)
		{
			return (columnIndex + 1).ToString();
		}

		protected bool IsTextEmpty(ICell cell)
		{
			string value;
			switch (cell.CellType)
			{
			case CellType.String:
				value = cell.RichStringCellValue.String;
				break;
			case CellType.Formula:
				switch (cell.CachedFormulaResultType)
				{
				case CellType.String:
				{
					IRichTextString richStringCellValue = cell.RichStringCellValue;
					if (richStringCellValue == null || richStringCellValue.Length <= 0)
					{
						return false;
					}
					value = richStringCellValue.ToString();
					break;
				}
				case CellType.Numeric:
				{
					ICellStyle cellStyle = cell.CellStyle;
					if (cellStyle == null)
					{
						return false;
					}
					value = _formatter.FormatRawCellContents(cell.NumericCellValue, cellStyle.DataFormat, cellStyle.GetDataFormatString());
					break;
				}
				case CellType.Boolean:
					value = cell.BooleanCellValue.ToString();
					break;
				case CellType.Error:
					value = ErrorEval.GetText(cell.ErrorCellValue);
					break;
				default:
					value = string.Empty;
					break;
				}
				break;
			case CellType.Blank:
				value = string.Empty;
				break;
			case CellType.Numeric:
				value = _formatter.FormatCellValue(cell);
				break;
			case CellType.Boolean:
				value = cell.BooleanCellValue.ToString();
				break;
			case CellType.Error:
				value = ErrorEval.GetText(cell.ErrorCellValue);
				break;
			default:
				return true;
			}
			return string.IsNullOrEmpty(value);
		}

		protected bool ProcessCell(ICell cell, XmlElement tableCellElement, int normalWidthPx, int maxSpannedWidthPx, float normalHeightPt)
		{
			ICellStyle cellStyle = cell.CellStyle;
			string text;
			switch (cell.CellType)
			{
			case CellType.String:
				text = cell.RichStringCellValue.String;
				break;
			case CellType.Formula:
				switch (cell.CachedFormulaResultType)
				{
				case CellType.String:
				{
					IRichTextString richStringCellValue = cell.RichStringCellValue;
					text = ((richStringCellValue == null || richStringCellValue.Length <= 0) ? string.Empty : richStringCellValue.String);
					break;
				}
				case CellType.Numeric:
				{
					ICellStyle cellStyle2 = cellStyle;
					text = ((cellStyle2 != null) ? _formatter.FormatRawCellContents(cell.NumericCellValue, cellStyle2.DataFormat, cellStyle2.GetDataFormatString()) : cell.NumericCellValue.ToString());
					break;
				}
				case CellType.Boolean:
					text = cell.BooleanCellValue.ToString();
					break;
				case CellType.Error:
					text = ErrorEval.GetText(cell.ErrorCellValue);
					break;
				default:
					logger.Log(5, "Unexpected cell cachedFormulaResultType (" + cell.CachedFormulaResultType.ToString() + ")");
					text = string.Empty;
					break;
				}
				break;
			case CellType.Blank:
				text = string.Empty;
				break;
			case CellType.Numeric:
				text = _formatter.FormatCellValue(cell);
				break;
			case CellType.Boolean:
				text = cell.BooleanCellValue.ToString();
				break;
			case CellType.Error:
				text = ErrorEval.GetText(cell.ErrorCellValue);
				break;
			default:
				logger.Log(5, "Unexpected cell type (" + cell.CellType.ToString() + ")");
				return true;
			}
			bool flag = string.IsNullOrEmpty(text);
			bool flag2 = !flag && UseDivsToSpan && !cellStyle.WrapText;
			short index = cellStyle.Index;
			if (index != 0)
			{
				IWorkbook workbook = cell.Row.Sheet.Workbook;
				string styleClassName = GetStyleClassName(workbook, cellStyle);
				if (flag2)
				{
					tableCellElement.SetAttribute("class", styleClassName + " " + cssClassContainerCell);
				}
				else
				{
					tableCellElement.SetAttribute("class", styleClassName);
				}
				if (flag)
				{
					text = "\u00a0";
				}
			}
			if (OutputLeadingSpacesAsNonBreaking && text.StartsWith(" "))
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < text.Length && text[i] == ' '; i++)
				{
					stringBuilder.Append('\u00a0');
				}
				if (text.Length != stringBuilder.Length)
				{
					stringBuilder.Append(text.Substring(stringBuilder.Length));
				}
				text = stringBuilder.ToString();
			}
			XmlText newChild = htmlDocumentFacade.CreateText(text);
			if (flag2)
			{
				XmlElement xmlElement = htmlDocumentFacade.CreateBlock();
				xmlElement.SetAttribute("class", cssClassContainerDiv);
				XmlElement xmlElement2 = htmlDocumentFacade.CreateBlock();
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("position:absolute;min-width:");
				stringBuilder2.Append(normalWidthPx);
				stringBuilder2.Append("px;");
				if (maxSpannedWidthPx != 2147483647)
				{
					stringBuilder2.Append("max-width:");
					stringBuilder2.Append(maxSpannedWidthPx);
					stringBuilder2.Append("px;");
				}
				stringBuilder2.Append("overflow:hidden;max-height:");
				stringBuilder2.Append(normalHeightPt);
				stringBuilder2.Append("pt;white-space:nowrap;");
				ExcelToHtmlUtils.AppendAlign(stringBuilder2, cellStyle.Alignment);
				htmlDocumentFacade.AddStyleClass(xmlElement, "d", stringBuilder2.ToString());
				xmlElement2.AppendChild(newChild);
				xmlElement.AppendChild(xmlElement2);
				tableCellElement.AppendChild(xmlElement);
			}
			else
			{
				tableCellElement.AppendChild(newChild);
			}
			if (string.IsNullOrEmpty(text))
			{
				return index == 0;
			}
			return false;
		}

		protected string GetStyleClassName(IWorkbook workbook, ICellStyle cellStyle)
		{
			short index = cellStyle.Index;
			if (excelStyleToClass.ContainsKey(index))
			{
				return excelStyleToClass[index];
			}
			string style = BuildStyle(workbook, cellStyle);
			string orCreateCssClass = htmlDocumentFacade.GetOrCreateCssClass("td", "c", style);
			excelStyleToClass.Add(index, orCreateCssClass);
			return orCreateCssClass;
		}

		protected string BuildStyle(IWorkbook workbook, ICellStyle cellStyle)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (workbook is HSSFWorkbook)
			{
				HSSFPalette customPalette = ((HSSFWorkbook)workbook).GetCustomPalette();
				stringBuilder.Append("white-space: pre-wrap; ");
				ExcelToHtmlUtils.AppendAlign(stringBuilder, cellStyle.Alignment);
				if (cellStyle.FillPattern != 0)
				{
					if (cellStyle.FillPattern == FillPattern.SolidForeground)
					{
						HSSFColor color = customPalette.GetColor(cellStyle.FillForegroundColor);
						if (color != null)
						{
							stringBuilder.AppendFormat("background-color:{0}; ", ExcelToHtmlUtils.GetColor(color));
						}
					}
					else
					{
						HSSFColor color2 = customPalette.GetColor(cellStyle.FillBackgroundColor);
						if (color2 != null)
						{
							stringBuilder.AppendFormat("background-color:{0}; ", ExcelToHtmlUtils.GetColor(color2));
						}
					}
				}
			}
			else
			{
				stringBuilder.Append("white-space: pre-wrap; ");
				ExcelToHtmlUtils.AppendAlign(stringBuilder, cellStyle.Alignment);
				StylesTable stylesSource = ((XSSFWorkbook)workbook).GetStylesSource();
				stylesSource.GetTheme();
				if (cellStyle.FillPattern != 0)
				{
					if (cellStyle.FillPattern == FillPattern.SolidForeground)
					{
						IndexedColors indexedColors = IndexedColors.ValueOf(cellStyle.FillForegroundColor);
						string text = null;
						if (indexedColors != null)
						{
							text = indexedColors.HexString;
						}
						else
						{
							XSSFColor xSSFColor = (XSSFColor)cellStyle.FillForegroundColorColor;
							if (xSSFColor != null)
							{
								text = ExcelToHtmlUtils.GetColor(xSSFColor);
							}
						}
						if (text != null)
						{
							stringBuilder.AppendFormat("background-color:{0}; ", text);
						}
					}
					else
					{
						IndexedColors indexedColors2 = IndexedColors.ValueOf(cellStyle.FillBackgroundColor);
						string text2 = null;
						if (indexedColors2 != null)
						{
							text2 = indexedColors2.HexString;
						}
						else
						{
							XSSFColor xSSFColor2 = (XSSFColor)cellStyle.FillBackgroundColorColor;
							if (xSSFColor2 != null)
							{
								text2 = ExcelToHtmlUtils.GetColor(xSSFColor2);
							}
						}
						if (text2 != null)
						{
							stringBuilder.AppendFormat("background-color:{0}; ", text2);
						}
					}
				}
			}
			BuildStyle_Border(workbook, stringBuilder, "top", cellStyle.BorderTop, cellStyle.TopBorderColor);
			BuildStyle_Border(workbook, stringBuilder, "right", cellStyle.BorderRight, cellStyle.RightBorderColor);
			BuildStyle_Border(workbook, stringBuilder, "bottom", cellStyle.BorderBottom, cellStyle.BottomBorderColor);
			BuildStyle_Border(workbook, stringBuilder, "left", cellStyle.BorderLeft, cellStyle.LeftBorderColor);
			IFont font = cellStyle.GetFont(workbook);
			BuildStyle_Font(workbook, stringBuilder, font);
			return stringBuilder.ToString();
		}

		private void BuildStyle_Border(IWorkbook workbook, StringBuilder style, string type, BorderStyle xlsBorder, short borderColor)
		{
			if (xlsBorder != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(ExcelToHtmlUtils.GetBorderWidth(xlsBorder));
				stringBuilder.Append(' ');
				stringBuilder.Append(ExcelToHtmlUtils.GetBorderStyle(xlsBorder));
				if (workbook is HSSFWorkbook)
				{
					HSSFColor color = ((HSSFWorkbook)workbook).GetCustomPalette().GetColor(borderColor);
					if (color != null)
					{
						stringBuilder.Append(' ');
						stringBuilder.Append(ExcelToHtmlUtils.GetColor(color));
					}
				}
				else
				{
					IndexedColors indexedColors = IndexedColors.ValueOf(borderColor);
					if (indexedColors != null)
					{
						stringBuilder.Append(' ');
						stringBuilder.Append(indexedColors.HexString);
					}
					else
					{
						XSSFColor themeColor = ((XSSFWorkbook)workbook).GetStylesSource().GetTheme().GetThemeColor(borderColor);
						if (themeColor != null)
						{
							stringBuilder.Append(' ');
							stringBuilder.Append(ExcelToHtmlUtils.GetColor(themeColor));
						}
					}
				}
				style.AppendFormat("border-{0}: {1}; ", type, stringBuilder);
			}
		}

		private void BuildStyle_Font(IWorkbook workbook, StringBuilder style, IFont font)
		{
			switch (font.Boldweight)
			{
			case 700:
				style.Append("font-weight: bold; ");
				break;
			}
			if (workbook is HSSFWorkbook)
			{
				HSSFColor color = ((HSSFWorkbook)workbook).GetCustomPalette().GetColor(font.Color);
				if (color != null)
				{
					style.AppendFormat("color:{0}; ", ExcelToHtmlUtils.GetColor(color));
				}
			}
			else
			{
				IndexedColors indexedColors = IndexedColors.ValueOf(font.Color);
				string text = null;
				if (indexedColors != null)
				{
					text = indexedColors.HexString;
				}
				else
				{
					StylesTable stylesSource = ((XSSFWorkbook)workbook).GetStylesSource();
					XSSFColor xSSFColor = null;
					xSSFColor = ((stylesSource == null || stylesSource.GetTheme() == null) ? ((XSSFFont)font).GetXSSFColor() : stylesSource.GetTheme().GetThemeColor(font.Color));
					if (xSSFColor != null)
					{
						text = ExcelToHtmlUtils.GetColor(xSSFColor);
					}
				}
				if (text != null)
				{
					style.AppendFormat("color:{0}; ", text);
				}
			}
			if (font.FontHeightInPoints != 0)
			{
				style.Append("font-size: " + font.FontHeightInPoints + "pt; ");
			}
			if (font.IsItalic)
			{
				style.Append("font-style: italic; ");
			}
		}
	}
}

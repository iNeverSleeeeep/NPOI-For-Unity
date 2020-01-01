using NPOI.HSSF.Extractor;
using NPOI.OpenXml4Net.OPC;
using NPOI.SS.Extractor;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections;
using System.Globalization;
using System.Text;

namespace NPOI.XSSF.Extractor
{
	/// Helper class to extract text from an OOXML Excel file
	public class XSSFExcelExtractor : POIXMLTextExtractor, IExcelExtractor
	{
		public static XSSFRelation[] SUPPORTED_TYPES = new XSSFRelation[5]
		{
			XSSFRelation.WORKBOOK,
			XSSFRelation.MACRO_TEMPLATE_WORKBOOK,
			XSSFRelation.MACRO_ADDIN_WORKBOOK,
			XSSFRelation.TEMPLATE_WORKBOOK,
			XSSFRelation.MACROS_WORKBOOK
		};

		private XSSFWorkbook workbook;

		private bool includeSheetNames = true;

		private bool formulasNotResults;

		private bool includeCellComments;

		private bool includeHeadersFooters = true;

		private CultureInfo locale;

		/// <summary>
		///  Should header and footer be included? Default is true
		/// </summary>
		public bool IncludeHeaderFooter
		{
			get
			{
				return includeHeadersFooters;
			}
			set
			{
				includeHeadersFooters = value;
			}
		}

		/// <summary>
		/// Should sheet names be included? Default is true
		/// </summary>
		/// <value>if set to <c>true</c> [include sheet names].</value>
		public bool IncludeSheetNames
		{
			get
			{
				return includeSheetNames;
			}
			set
			{
				includeSheetNames = value;
			}
		}

		/// <summary>
		/// Should we return the formula itself, and not
		/// the result it produces? Default is false
		/// </summary>
		/// <value>if set to <c>true</c> [formulas not results].</value>
		public bool FormulasNotResults
		{
			get
			{
				return formulasNotResults;
			}
			set
			{
				formulasNotResults = value;
			}
		}

		/// <summary>
		/// Should cell comments be included? Default is false
		/// </summary>
		/// <value>if set to <c>true</c> [include cell comments].</value>
		public bool IncludeCellComments
		{
			get
			{
				return includeCellComments;
			}
			set
			{
				includeCellComments = value;
			}
		}

		/// Retreives the text contents of the file
		public override string Text
		{
			get
			{
				DataFormatter formatter = (locale != null) ? new DataFormatter(locale) : new DataFormatter();
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < workbook.NumberOfSheets; i++)
				{
					XSSFSheet xSSFSheet = (XSSFSheet)workbook.GetSheetAt(i);
					if (includeSheetNames)
					{
						stringBuilder.Append(workbook.GetSheetName(i) + "\n");
					}
					if (includeHeadersFooters)
					{
						stringBuilder.Append(ExtractHeaderFooter(xSSFSheet.FirstHeader));
						stringBuilder.Append(ExtractHeaderFooter(xSSFSheet.OddHeader));
						stringBuilder.Append(ExtractHeaderFooter(xSSFSheet.EvenHeader));
					}
					foreach (object item in xSSFSheet)
					{
						IRow row = (IRow)item;
						IEnumerator enumerator2 = row.GetEnumerator();
						bool flag = true;
						while (enumerator2.MoveNext())
						{
							if (!flag)
							{
								stringBuilder.Append("\t");
							}
							else
							{
								flag = false;
							}
							ICell cell = (ICell)enumerator2.Current;
							if (cell.CellType == CellType.Formula)
							{
								if (formulasNotResults)
								{
									stringBuilder.Append(cell.CellFormula);
								}
								else if (cell.CachedFormulaResultType == CellType.String)
								{
									HandleStringCell(stringBuilder, cell);
								}
								else
								{
									HandleNonStringCell(stringBuilder, cell, formatter);
								}
							}
							else if (cell.CellType == CellType.String)
							{
								HandleStringCell(stringBuilder, cell);
							}
							else
							{
								HandleNonStringCell(stringBuilder, cell, formatter);
							}
							IComment cellComment = cell.CellComment;
							if (includeCellComments && cellComment != null)
							{
								string value = cellComment.String.String.Replace('\n', ' ');
								stringBuilder.Append(" Comment by ").Append(cellComment.Author).Append(": ")
									.Append(value);
							}
						}
						stringBuilder.Append("\n");
					}
					if (includeHeadersFooters)
					{
						stringBuilder.Append(ExtractHeaderFooter(xSSFSheet.FirstFooter));
						stringBuilder.Append(ExtractHeaderFooter(xSSFSheet.OddFooter));
						stringBuilder.Append(ExtractHeaderFooter(xSSFSheet.EvenFooter));
					}
				}
				return stringBuilder.ToString();
			}
		}

		public XSSFExcelExtractor(string path)
			: this(new XSSFWorkbook(path))
		{
		}

		public XSSFExcelExtractor(OPCPackage Container)
			: this(new XSSFWorkbook(Container))
		{
		}

		public XSSFExcelExtractor(XSSFWorkbook workbook)
			: base(workbook)
		{
			this.workbook = workbook;
		}

		/// Should sheet names be included? Default is true
		public void SetIncludeSheetNames(bool includeSheetNames)
		{
			this.includeSheetNames = includeSheetNames;
		}

		/// Should we return the formula itself, and not
		///  the result it produces? Default is false
		public void SetFormulasNotResults(bool formulasNotResults)
		{
			this.formulasNotResults = formulasNotResults;
		}

		/// Should cell comments be included? Default is true
		public void SetIncludeCellComments(bool includeCellComments)
		{
			this.includeCellComments = includeCellComments;
		}

		/// Should headers and footers be included? Default is true
		public void SetIncludeHeadersFooters(bool includeHeadersFooters)
		{
			this.includeHeadersFooters = includeHeadersFooters;
		}

		public void SetLocale(CultureInfo locale)
		{
			this.locale = locale;
		}

		private void HandleStringCell(StringBuilder text, ICell cell)
		{
			text.Append(cell.RichStringCellValue.String);
		}

		private void HandleNonStringCell(StringBuilder text, ICell cell, DataFormatter formatter)
		{
			CellType cellType = cell.CellType;
			if (cellType == CellType.Formula)
			{
				cellType = cell.CachedFormulaResultType;
			}
			if (cellType == CellType.Numeric)
			{
				ICellStyle cellStyle = cell.CellStyle;
				if (cellStyle.GetDataFormatString() != null)
				{
					text.Append(formatter.FormatRawCellContents(cell.NumericCellValue, cellStyle.DataFormat, cellStyle.GetDataFormatString()));
					return;
				}
			}
			XSSFCell xSSFCell = (XSSFCell)cell;
			text.Append(xSSFCell.GetRawValue());
		}

		private string ExtractHeaderFooter(IHeaderFooter hf)
		{
			return ExcelExtractor.ExtractHeaderFooter(hf);
		}
	}
}

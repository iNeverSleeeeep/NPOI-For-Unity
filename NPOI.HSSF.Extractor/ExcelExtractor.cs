using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.Extractor;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System;
using System.Text;

namespace NPOI.HSSF.Extractor
{
	/// <summary>
	/// A text extractor for Excel files.
	/// Returns the textual content of the file, suitable for
	/// indexing by something like Lucene, but not really
	/// intended for display to the user.
	/// </summary>
	public class ExcelExtractor : POIOLE2TextExtractor, IExcelExtractor
	{
		private HSSFWorkbook wb;

		private HSSFDataFormatter _formatter;

		private bool includeSheetNames = true;

		private bool formulasNotResults;

		private bool includeCellComments;

		private bool includeBlankCells;

		private bool includeHeaderFooter = true;

		/// <summary>
		///  Should header and footer be included? Default is true
		/// </summary>
		public bool IncludeHeaderFooter
		{
			get
			{
				return includeHeaderFooter;
			}
			set
			{
				includeHeaderFooter = value;
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

		/// <summary>
		/// Should blank cells be output? Default is to only
		/// output cells that are present in the file and are
		/// non-blank.
		/// </summary>
		/// <value>if set to <c>true</c> [include blank cells].</value>
		public bool IncludeBlankCells
		{
			get
			{
				return includeBlankCells;
			}
			set
			{
				includeBlankCells = value;
			}
		}

		/// <summary>
		/// Retreives the text contents of the file
		/// </summary>
		/// <value>All the text from the document.</value>
		public override string Text
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				wb.MissingCellPolicy = MissingCellPolicy.RETURN_BLANK_AS_NULL;
				for (int i = 0; i < wb.NumberOfSheets; i++)
				{
					HSSFSheet hSSFSheet = (HSSFSheet)wb.GetSheetAt(i);
					if (hSSFSheet != null)
					{
						if (includeSheetNames)
						{
							string sheetName = wb.GetSheetName(i);
							if (sheetName != null)
							{
								stringBuilder.Append(sheetName);
								stringBuilder.Append("\n");
							}
						}
						if (hSSFSheet.Header != null && includeHeaderFooter)
						{
							stringBuilder.Append(ExtractHeaderFooter(hSSFSheet.Header));
						}
						int firstRowNum = hSSFSheet.FirstRowNum;
						int lastRowNum = hSSFSheet.LastRowNum;
						for (int j = firstRowNum; j <= lastRowNum; j++)
						{
							IRow row = hSSFSheet.GetRow(j);
							if (row != null)
							{
								int num = row.FirstCellNum;
								int lastCellNum = row.LastCellNum;
								if (includeBlankCells)
								{
									num = 0;
								}
								for (int k = num; k < lastCellNum; k++)
								{
									ICell cell = row.GetCell(k);
									bool flag = true;
									if (cell == null)
									{
										flag = includeBlankCells;
									}
									else
									{
										switch (cell.CellType)
										{
										case CellType.String:
											stringBuilder.Append(cell.RichStringCellValue.String);
											break;
										case CellType.Numeric:
											stringBuilder.Append(_formatter.FormatCellValue(cell));
											break;
										case CellType.Boolean:
											stringBuilder.Append(cell.BooleanCellValue);
											break;
										case CellType.Error:
											stringBuilder.Append(ErrorEval.GetText(cell.ErrorCellValue));
											break;
										case CellType.Formula:
											if (formulasNotResults)
											{
												stringBuilder.Append(cell.CellFormula);
											}
											else
											{
												switch (cell.CachedFormulaResultType)
												{
												case CellType.String:
												{
													IRichTextString richStringCellValue = cell.RichStringCellValue;
													if (richStringCellValue != null && richStringCellValue.Length > 0)
													{
														stringBuilder.Append(richStringCellValue.ToString());
													}
													break;
												}
												case CellType.Numeric:
												{
													HSSFCellStyle hSSFCellStyle = (HSSFCellStyle)cell.CellStyle;
													if (hSSFCellStyle == null)
													{
														stringBuilder.Append(cell.NumericCellValue);
													}
													else
													{
														stringBuilder.Append(_formatter.FormatRawCellContents(cell.NumericCellValue, hSSFCellStyle.DataFormat, hSSFCellStyle.GetDataFormatString()));
													}
													break;
												}
												case CellType.Boolean:
													stringBuilder.Append(cell.BooleanCellValue);
													break;
												case CellType.Error:
													stringBuilder.Append(ErrorEval.GetText(cell.ErrorCellValue));
													break;
												}
											}
											break;
										default:
											throw new Exception("Unexpected cell type (" + cell.CellType + ")");
										}
										IComment cellComment = cell.CellComment;
										if (includeCellComments && cellComment != null)
										{
											string str = cellComment.String.String.Replace('\n', ' ');
											stringBuilder.Append(" Comment by " + cellComment.Author + ": " + str);
										}
									}
									if (flag && k < lastCellNum - 1)
									{
										stringBuilder.Append("\t");
									}
								}
								stringBuilder.Append("\n");
							}
						}
						if (hSSFSheet.Footer != null && includeHeaderFooter)
						{
							stringBuilder.Append(ExtractHeaderFooter(hSSFSheet.Footer));
						}
					}
				}
				return stringBuilder.ToString();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.Extractor.ExcelExtractor" /> class.
		/// </summary>
		/// <param name="wb">The wb.</param>
		public ExcelExtractor(HSSFWorkbook wb)
			: base(wb)
		{
			this.wb = wb;
			_formatter = new HSSFDataFormatter();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.Extractor.ExcelExtractor" /> class.
		/// </summary>
		/// <param name="fs">The fs.</param>
		public ExcelExtractor(POIFSFileSystem fs)
			: this(new HSSFWorkbook(fs))
		{
		}

		/// <summary>
		/// Extracts the header footer.
		/// </summary>
		/// <param name="hf">The header or footer</param>
		/// <returns></returns>
		public static string ExtractHeaderFooter(IHeaderFooter hf)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (hf.Left != null)
			{
				stringBuilder.Append(hf.Left);
			}
			if (hf.Center != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("\t");
				}
				stringBuilder.Append(hf.Center);
			}
			if (hf.Right != null)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("\t");
				}
				stringBuilder.Append(hf.Right);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}
	}
}

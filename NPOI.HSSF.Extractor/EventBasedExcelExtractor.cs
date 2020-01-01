using NPOI.HPSF;
using NPOI.HSSF.EventUserModel;
using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace NPOI.HSSF.Extractor
{
	/// <summary>
	/// A text extractor for Excel files, that is based
	/// on the hssf eventusermodel api.
	/// It will typically use less memory than
	/// ExcelExtractor, but may not provide
	/// the same richness of formatting.
	/// Returns the textual content of the file, suitable for
	/// indexing by something like Lucene, but not really
	/// intended for display to the user.
	/// </summary>
	public class EventBasedExcelExtractor : POIOLE2TextExtractor
	{
		private class TextListener : IHSSFListener
		{
			public FormatTrackingHSSFListener ft;

			private SSTRecord sstRecord;

			private IList sheetNames = new ArrayList();

			public StringBuilder text = new StringBuilder();

			private int sheetNum = -1;

			private int rowNum;

			private bool outputNextStringValue;

			private int nextRow = -1;

			private bool includeSheetNames;

			private bool formulasNotResults;

			public TextListener(bool includeSheetNames, bool formulasNotResults)
			{
				this.includeSheetNames = includeSheetNames;
				this.formulasNotResults = formulasNotResults;
			}

			/// <summary>
			/// Process an HSSF Record. Called when a record occurs in an HSSF file.
			/// </summary>
			/// <param name="record"></param>
			public void ProcessRecord(NPOI.HSSF.Record.Record record)
			{
				string text = null;
				int num = -1;
				switch (record.Sid)
				{
				case 133:
				{
					BoundSheetRecord boundSheetRecord = (BoundSheetRecord)record;
					sheetNames.Add(boundSheetRecord.Sheetname);
					break;
				}
				case 2057:
				{
					BOFRecord bOFRecord = (BOFRecord)record;
					if (bOFRecord.Type == BOFRecordType.Worksheet)
					{
						sheetNum++;
						rowNum = -1;
						if (includeSheetNames)
						{
							if (this.text.Length > 0)
							{
								this.text.Append("\n");
							}
							this.text.Append(sheetNames[sheetNum]);
						}
					}
					break;
				}
				case 252:
					sstRecord = (SSTRecord)record;
					break;
				case 6:
				{
					FormulaRecord formulaRecord = (FormulaRecord)record;
					num = formulaRecord.Row;
					if (formulasNotResults)
					{
						text = HSSFFormulaParser.ToFormulaString(null, formulaRecord.ParsedExpression);
					}
					else if (formulaRecord.HasCachedResultString)
					{
						outputNextStringValue = true;
						nextRow = formulaRecord.Row;
					}
					else
					{
						text = FormatNumberDateCell(formulaRecord, formulaRecord.Value);
					}
					break;
				}
				case 519:
					if (outputNextStringValue)
					{
						StringRecord stringRecord = (StringRecord)record;
						text = stringRecord.String;
						num = nextRow;
						outputNextStringValue = false;
					}
					break;
				case 516:
				{
					LabelRecord labelRecord = (LabelRecord)record;
					num = labelRecord.Row;
					text = labelRecord.Value;
					break;
				}
				case 253:
				{
					LabelSSTRecord labelSSTRecord = (LabelSSTRecord)record;
					num = labelSSTRecord.Row;
					if (sstRecord == null)
					{
						throw new Exception("No SST record found");
					}
					text = sstRecord.GetString(labelSSTRecord.SSTIndex).ToString();
					break;
				}
				case 28:
				{
					NoteRecord noteRecord = (NoteRecord)record;
					num = noteRecord.Row;
					break;
				}
				case 515:
				{
					NumberRecord numberRecord = (NumberRecord)record;
					num = numberRecord.Row;
					text = FormatNumberDateCell(numberRecord, numberRecord.Value);
					break;
				}
				}
				if (text != null)
				{
					if (num != rowNum)
					{
						rowNum = num;
						if (this.text.Length > 0)
						{
							this.text.Append("\n");
						}
					}
					else
					{
						this.text.Append("\t");
					}
					this.text.Append(text);
				}
			}

			/// <summary>
			/// Formats a number or date cell, be that a real number, or the
			/// answer to a formula
			/// </summary>
			/// <param name="cell">The cell.</param>
			/// <param name="value">The value.</param>
			/// <returns></returns>
			private string FormatNumberDateCell(CellValueRecordInterface cell, double value)
			{
				int formatIndex = ft.GetFormatIndex(cell);
				string formatString = ft.GetFormatString(cell);
				if (formatString == null)
				{
					return value.ToString(CultureInfo.InvariantCulture);
				}
				if (DateUtil.IsADateFormat(formatIndex, formatString) && DateUtil.IsValidExcelDate(value))
				{
					formatString = formatString.Replace('m', 'M');
					formatString = formatString.Replace("\\\\-", "-");
					DateTime javaDate = DateUtil.GetJavaDate(value, use1904windowing: false);
					SimpleDateFormat simpleDateFormat = new SimpleDateFormat(formatString);
					return simpleDateFormat.Format(javaDate, CultureInfo.CurrentCulture);
				}
				if (formatString == "General")
				{
					return value.ToString(CultureInfo.InvariantCulture);
				}
				DecimalFormat decimalFormat = new DecimalFormat(formatString);
				return decimalFormat.Format(value, CultureInfo.CurrentCulture);
			}
		}

		private POIFSFileSystem fs;

		private bool includeSheetNames = true;

		private bool formulasNotResults;

		/// <summary>
		/// Would return the document information metadata for the document,
		/// if we supported it
		/// </summary>
		/// <value>The doc summary information.</value>
		public override DocumentSummaryInformation DocSummaryInformation
		{
			get
			{
				throw new NotImplementedException("Metadata extraction not supported in streaming mode, please use ExcelExtractor");
			}
		}

		/// <summary>
		/// Would return the summary information metadata for the document,
		/// if we supported it
		/// </summary>
		/// <value>The summary information.</value>
		public override SummaryInformation SummaryInformation
		{
			get
			{
				throw new NotImplementedException("Metadata extraction not supported in streaming mode, please use ExcelExtractor");
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
		/// Retreives the text contents of the file
		/// </summary>
		/// <value>All the text from the document.</value>
		public override string Text
		{
			get
			{
				string text = null;
				try
				{
					TextListener textListener = TriggerExtraction();
					text = textListener.text.ToString();
					if (!text.EndsWith("\n", StringComparison.Ordinal))
					{
						return text + "\n";
					}
					return text;
				}
				catch (IOException)
				{
					throw;
				}
			}
		}

		public EventBasedExcelExtractor(POIFSFileSystem fs)
			: base(null)
		{
			this.fs = fs;
		}

		/// <summary>
		/// Triggers the extraction.
		/// </summary>
		/// <returns></returns>
		private TextListener TriggerExtraction()
		{
			TextListener textListener = new TextListener(includeSheetNames, formulasNotResults);
			FormatTrackingHSSFListener lsnr = textListener.ft = new FormatTrackingHSSFListener(textListener);
			HSSFEventFactory hSSFEventFactory = new HSSFEventFactory();
			HSSFRequest hSSFRequest = new HSSFRequest();
			hSSFRequest.AddListenerForAllRecords(lsnr);
			hSSFEventFactory.ProcessWorkbookEvents(hSSFRequest, fs);
			return textListener;
		}
	}
}

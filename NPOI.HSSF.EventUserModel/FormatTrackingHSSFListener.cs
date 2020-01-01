using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace NPOI.HSSF.EventUserModel
{
	/// A proxy HSSFListener that keeps track of the document
	///  formatting records, and provides an easy way to look
	///  up the format strings used by cells from their ids.
	public class FormatTrackingHSSFListener : IHSSFListener
	{
		private static POILogger logger = POILogFactory.GetLogger(typeof(FormatTrackingHSSFListener));

		private IHSSFListener childListener;

		private Dictionary<int, FormatRecord> customFormatRecords = new Dictionary<int, FormatRecord>();

		private DataFormatter formatter = new DataFormatter();

		private List<ExtendedFormatRecord> xfRecords = new List<ExtendedFormatRecord>();

		public int NumberOfCustomFormats => customFormatRecords.Count;

		public int NumberOfExtendedFormats => xfRecords.Count;

		public FormatTrackingHSSFListener(IHSSFListener childListener)
		{
			this.childListener = childListener;
		}

		/// Process this record ourselves, and then
		///  pass it on to our child listener
		public void ProcessRecord(NPOI.HSSF.Record.Record record)
		{
			ProcessRecordInternally(record);
			childListener.ProcessRecord(record);
		}

		/// Process the record ourselves, but do not
		///  pass it on to the child Listener.
		/// @param record
		public void ProcessRecordInternally(NPOI.HSSF.Record.Record record)
		{
			if (record is FormatRecord)
			{
				FormatRecord formatRecord = (FormatRecord)record;
				customFormatRecords[formatRecord.IndexCode] = formatRecord;
			}
			else if (record is ExtendedFormatRecord)
			{
				ExtendedFormatRecord item = (ExtendedFormatRecord)record;
				xfRecords.Add(item);
			}
		}

		/// Formats the given numeric of date Cell's contents
		///  as a String, in as close as we can to the way 
		///  that Excel would do so.
		/// Uses the various format records to manage this.
		///
		/// TODO - move this to a central class in such a
		///  way that hssf.usermodel can make use of it too
		public string FormatNumberDateCell(CellValueRecordInterface cell)
		{
			double value;
			if (cell is NumberRecord)
			{
				value = ((NumberRecord)cell).Value;
			}
			else
			{
				if (!(cell is FormulaRecord))
				{
					throw new ArgumentException("Unsupported CellValue Record passed in " + cell);
				}
				value = ((FormulaRecord)cell).Value;
			}
			int formatIndex = GetFormatIndex(cell);
			string formatString = GetFormatString(cell);
			if (formatString == null)
			{
				return value.ToString(CultureInfo.InvariantCulture);
			}
			return formatter.FormatRawCellContents(value, formatIndex, formatString);
		}

		/// Returns the format string, eg $##.##, for the
		///  given number format index.
		public string GetFormatString(int formatIndex)
		{
			string result = null;
			if (formatIndex >= HSSFDataFormat.NumberOfBuiltinBuiltinFormats)
			{
				FormatRecord formatRecord = customFormatRecords[formatIndex];
				if (formatRecord == null)
				{
					logger.Log(7, "Requested format at index " + formatIndex + ", but it wasn't found");
				}
				else
				{
					result = formatRecord.FormatString;
				}
			}
			else
			{
				result = HSSFDataFormat.GetBuiltinFormat((short)formatIndex);
			}
			return result;
		}

		/// Returns the format string, eg $##.##, used
		///  by your cell 
		public string GetFormatString(CellValueRecordInterface cell)
		{
			int formatIndex = GetFormatIndex(cell);
			if (formatIndex == -1)
			{
				return null;
			}
			return GetFormatString(formatIndex);
		}

		/// Returns the index of the format string, used by your cell,
		///  or -1 if none found
		public int GetFormatIndex(CellValueRecordInterface cell)
		{
			ExtendedFormatRecord extendedFormatRecord = xfRecords[cell.XFIndex];
			if (extendedFormatRecord == null)
			{
				logger.Log(7, "Cell " + cell.Row + "," + cell.Column + " uses XF with index " + cell.XFIndex + ", but we don't have that");
				return -1;
			}
			return extendedFormatRecord.FormatIndex;
		}
	}
}

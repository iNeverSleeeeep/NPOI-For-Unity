using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.UserModel
{
	[Serializable]
	public class HSSFDataFormat : IDataFormat
	{
		/// The first user-defined format starts at 164.
		public const int FIRST_USER_DEFINED_FORMAT_INDEX = 164;

		private static List<string> builtinFormats = new List<string>(BuiltinFormats.GetAll());

		private List<string> formats = new List<string>();

		private InternalWorkbook workbook;

		private bool movedBuiltins;

		/// <summary>
		/// Get the number of builtin and reserved builtinFormats
		/// </summary>
		/// <returns>number of builtin and reserved builtinFormats</returns>
		public static int NumberOfBuiltinBuiltinFormats => builtinFormats.Count;

		/// <summary>
		/// Construncts a new data formatter.  It takes a workbook to have
		/// access to the workbooks format records.
		/// </summary>
		/// <param name="workbook">the workbook the formats are tied to.</param>
		public HSSFDataFormat(InternalWorkbook workbook)
		{
			this.workbook = workbook;
			IEnumerator enumerator = workbook.Formats.GetEnumerator();
			while (enumerator.MoveNext())
			{
				FormatRecord formatRecord = (FormatRecord)enumerator.Current;
				int num = formats.Count;
				while (formats.Count <= formatRecord.IndexCode)
				{
					formats.Add(null);
					num++;
				}
				formats[formatRecord.IndexCode] = formatRecord.FormatString;
			}
		}

		public static List<string> GetBuiltinFormats()
		{
			return builtinFormats;
		}

		/// <summary>
		/// Get the format index that matches the given format string
		/// Automatically Converts "text" to excel's format string to represent text.
		/// </summary>
		/// <param name="format">The format string matching a built in format.</param>
		/// <returns>index of format or -1 if Undefined.</returns>
		public static short GetBuiltinFormat(string format)
		{
			if (format.ToUpper().Equals("TEXT"))
			{
				format = "@";
			}
			short result = -1;
			for (short num = 0; num <= 49; num = (short)(num + 1))
			{
				string text = builtinFormats[num];
				if (text != null && text.Equals(format))
				{
					result = num;
					break;
				}
			}
			return result;
		}

		/// <summary>
		/// Get the format index that matches the given format
		/// string, creating a new format entry if required.
		/// Aliases text to the proper format as required.
		/// </summary>
		/// <param name="pFormat">The format string matching a built in format.</param>
		/// <returns>index of format.</returns>
		public short GetFormat(string pFormat)
		{
			string text = (!pFormat.ToUpper().Equals("TEXT")) ? pFormat : "@";
			IEnumerator enumerator;
			int num;
			if (!movedBuiltins)
			{
				enumerator = builtinFormats.GetEnumerator();
				num = 0;
				while (enumerator.MoveNext())
				{
					int num2 = formats.Count;
					while (formats.Count < num + 1)
					{
						formats.Add(null);
						num2++;
					}
					formats[num] = (enumerator.Current as string);
					num++;
				}
				movedBuiltins = true;
			}
			enumerator = formats.GetEnumerator();
			num = 0;
			while (enumerator.MoveNext())
			{
				if (text.Equals(enumerator.Current))
				{
					return (short)num;
				}
				num++;
			}
			num = workbook.GetFormat(text, CreateIfNotFound: true);
			int num3 = formats.Count;
			while (formats.Count < num + 1)
			{
				formats.Add(null);
				num3++;
			}
			formats[num] = text;
			return (short)num;
		}

		/// <summary>
		/// Get the format string that matches the given format index
		/// </summary>
		/// <param name="index">The index of a format.</param>
		/// <returns>string represented at index of format or null if there Is not a  format at that index</returns>
		public string GetFormat(short index)
		{
			if (movedBuiltins)
			{
				return formats[index];
			}
			if (index == -1)
			{
				return null;
			}
			string text = (formats.Count > index) ? formats[index] : null;
			if (builtinFormats.Count > index && builtinFormats[index] != null)
			{
				if (text != null)
				{
					return text;
				}
				return builtinFormats[index];
			}
			return text;
		}

		/// <summary>
		/// Get the format string that matches the given format index
		/// </summary>
		/// <param name="index">The index of a built in format.</param>
		/// <returns>string represented at index of format or null if there Is not a builtin format at that index</returns>
		public static string GetBuiltinFormat(short index)
		{
			return builtinFormats[index];
		}

		/// Ensures that the formats list can hold entries
		///  up to and including the entry with this index
		private void EnsureFormatsSize(int index)
		{
			if (formats.Count <= index)
			{
				formats.Capacity = index + 1;
			}
		}
	}
}

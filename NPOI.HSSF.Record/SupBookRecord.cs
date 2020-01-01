using NPOI.Util;
using System;
using System.IO;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Sup Book (EXTERNALBOOK) 
	/// Description:  A External Workbook Description (Suplemental Book)
	///               Its only a dummy record for making new ExternSheet Record 
	/// REFERENCE:  5.38
	/// @author Libin Roman (Vista Portal LDT. Developer)
	/// @author Andrew C. Oliver (acoliver@apache.org)
	public class SupBookRecord : StandardRecord
	{
		public const short sid = 430;

		private const short SMALL_RECORD_SIZE = 4;

		private const short TAG_INTERNAL_REFERENCES = 1025;

		private const short TAG_ADD_IN_FUNCTIONS = 14849;

		public const char CH_VOLUME = '\u0001';

		public const char CH_SAME_VOLUME = '\u0002';

		public const char CH_DOWN_DIR = '\u0003';

		public const char CH_UP_DIR = '\u0004';

		public const char CH_LONG_VOLUME = '\u0005';

		public const char CH_STARTUP_DIR = '\u0006';

		public const char CH_ALT_STARTUP_DIR = '\a';

		public const char CH_LIB_DIR = '\b';

		private static POILogger logger = POILogFactory.GetLogger(typeof(SupBookRecord));

		private short field_1_number_of_sheets;

		private string field_2_encoded_url;

		private string[] field_3_sheet_names;

		private bool _isAddInFunctions;

		public static readonly char PATH_SEPERATOR = Path.DirectorySeparatorChar;

		public bool IsExternalReferences => field_3_sheet_names != null;

		public bool IsInternalReferences
		{
			get
			{
				if (field_3_sheet_names == null)
				{
					return !_isAddInFunctions;
				}
				return false;
			}
		}

		public bool IsAddInFunctions
		{
			get
			{
				if (field_3_sheet_names == null)
				{
					return _isAddInFunctions;
				}
				return false;
			}
		}

		protected override int DataSize
		{
			get
			{
				if (!IsExternalReferences)
				{
					return 4;
				}
				int num = 2;
				num += StringUtil.GetEncodedSize(field_2_encoded_url);
				for (int i = 0; i < field_3_sheet_names.Length; i++)
				{
					num += StringUtil.GetEncodedSize(field_3_sheet_names[i]);
				}
				return num;
			}
		}

		public short NumberOfSheets
		{
			get
			{
				return field_1_number_of_sheets;
			}
			set
			{
				field_1_number_of_sheets = value;
			}
		}

		public override short Sid => 430;

		public string URL
		{
			get
			{
				string text = field_2_encoded_url;
				switch (text[0])
				{
				case '\0':
					return text.Substring(1);
				case '\u0001':
					return DecodeFileName(text);
				case '\u0002':
					return text.Substring(1);
				default:
					return text;
				}
			}
			set
			{
				field_2_encoded_url = field_2_encoded_url.Substring(0, 1) + value;
			}
		}

		public string[] SheetNames => (string[])field_3_sheet_names.Clone();

		public static SupBookRecord CreateInternalReferences(short numberOfSheets)
		{
			return new SupBookRecord(IsAddInFuncs: false, numberOfSheets);
		}

		public static SupBookRecord CreateAddInFunctions()
		{
			return new SupBookRecord(IsAddInFuncs: true, 1);
		}

		public static SupBookRecord CreateExternalReferences(string url, string[] sheetNames)
		{
			return new SupBookRecord(url, sheetNames);
		}

		private SupBookRecord(bool IsAddInFuncs, short numberOfSheets)
		{
			field_1_number_of_sheets = numberOfSheets;
			field_2_encoded_url = null;
			field_3_sheet_names = null;
			_isAddInFunctions = IsAddInFuncs;
		}

		public SupBookRecord(string url, string[] sheetNames)
		{
			field_1_number_of_sheets = (short)sheetNames.Length;
			field_2_encoded_url = url;
			field_3_sheet_names = sheetNames;
			_isAddInFunctions = false;
		}

		/// Constructs a Extern Sheet record and Sets its fields appropriately.
		///
		/// @param id     id must be 0x16 or an exception will be throw upon validation
		/// @param size  the size of the data area of the record
		/// @param data  data of the record (should not contain sid/len)
		public SupBookRecord(RecordInputStream in1)
		{
			int remaining = in1.Remaining;
			field_1_number_of_sheets = in1.ReadShort();
			if (remaining > 4)
			{
				_isAddInFunctions = false;
				field_2_encoded_url = in1.ReadString();
				string[] array = new string[field_1_number_of_sheets];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = in1.ReadString();
				}
				field_3_sheet_names = array;
			}
			else
			{
				field_2_encoded_url = null;
				field_3_sheet_names = null;
				short num = in1.ReadShort();
				switch (num)
				{
				case 1025:
					_isAddInFunctions = false;
					break;
				case 14849:
					_isAddInFunctions = true;
					if (field_1_number_of_sheets != 1)
					{
						throw new Exception("Expected 0x0001 for number of sheets field in 'Add-In Functions' but got (" + field_1_number_of_sheets + ")");
					}
					break;
				default:
					throw new Exception("invalid EXTERNALBOOK code (" + StringUtil.ToHexString(num) + ")");
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name).Append(" [SUPBOOK ");
			if (IsExternalReferences)
			{
				stringBuilder.Append("External References");
				stringBuilder.Append(" nSheets=").Append(field_1_number_of_sheets);
				stringBuilder.Append(" url=").Append(field_2_encoded_url);
			}
			else if (_isAddInFunctions)
			{
				stringBuilder.Append("Add-In Functions");
			}
			else
			{
				stringBuilder.Append("Internal References ");
				stringBuilder.Append(" nSheets= ").Append(field_1_number_of_sheets);
			}
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_number_of_sheets);
			if (IsExternalReferences)
			{
				StringUtil.WriteUnicodeString(out1, field_2_encoded_url);
				for (int i = 0; i < field_3_sheet_names.Length; i++)
				{
					StringUtil.WriteUnicodeString(out1, field_3_sheet_names[i]);
				}
			}
			else
			{
				int v = _isAddInFunctions ? 14849 : 1025;
				out1.WriteShort(v);
			}
		}

		private static string DecodeFileName(string encodedUrl)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 1; i < encodedUrl.Length; i++)
			{
				char c = encodedUrl[i];
				switch (c)
				{
				case '\u0001':
				{
					char c2 = encodedUrl[++i];
					if (c2 == '@')
					{
						stringBuilder.Append("\\\\");
					}
					else
					{
						stringBuilder.Append(c2).Append(":");
					}
					break;
				}
				case '\u0002':
					stringBuilder.Append(PATH_SEPERATOR);
					break;
				case '\u0003':
					stringBuilder.Append(PATH_SEPERATOR);
					break;
				case '\u0004':
					stringBuilder.Append("..").Append(PATH_SEPERATOR);
					break;
				case '\u0005':
					logger.Log(5, "Found unexpected key: ChLongVolume - IGNORING");
					break;
				case '\u0006':
				case '\a':
				case '\b':
					logger.Log(5, "EXCEL.EXE path unkown - using this directoy instead: .");
					stringBuilder.Append(".").Append(PATH_SEPERATOR);
					break;
				default:
					stringBuilder.Append(c);
					break;
				}
			}
			return stringBuilder.ToString();
		}
	}
}

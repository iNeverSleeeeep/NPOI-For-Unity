using NPOI.Util;

namespace NPOI.HSSF.Record
{
	/// Title: Interface End Record
	/// Description: Shows where the Interface Records end (MMS)
	///  (has no fields)
	/// REFERENCE:  PG 324 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class InterfaceEndRecord : StandardRecord
	{
		public const short sid = 226;

		public static InterfaceEndRecord Instance = new InterfaceEndRecord();

		protected override int DataSize => 0;

		public override short Sid => 226;

		private InterfaceEndRecord()
		{
		}

		/// Constructs an InterfaceEnd record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public static Record Create(RecordInputStream in1)
		{
			switch (in1.Remaining)
			{
			case 0:
				return Instance;
			case 2:
				return new InterfaceHdrRecord(in1);
			default:
				throw new RecordFormatException("Invalid record data size: " + in1.Remaining);
			}
		}

		public override string ToString()
		{
			return "[INTERFACEEND/]\n";
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
		}

		/// <summary>
		/// for test TestInterfaceEndRecord.TestCreate()
		/// </summary>
		/// <returns></returns>
		public int GetDataSize()
		{
			return DataSize;
		}
	}
}

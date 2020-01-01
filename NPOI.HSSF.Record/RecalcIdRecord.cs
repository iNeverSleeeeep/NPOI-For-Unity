using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Recalc Id Record
	/// Description:  This record Contains an ID that marks when a worksheet was last
	///               recalculated. It's an optimization Excel uses to determine if it
	///               needs to  recalculate the spReadsheet when it's opened. So far, only
	///               the two values <c>0xC1 0x01 0x00 0x00 0x80 0x38 0x01 0x00</c>
	///               (do not recalculate) and <c>0xC1 0x01 0x00 0x00 0x60 0x69 0x01
	///               0x00</c> have been seen. If the field <c>isNeeded</c> Is
	///               Set to false (default), then this record Is swallowed during the
	///               serialization Process
	/// REFERENCE:  http://chicago.sourceforge.net/devel/docs/excel/biff8.html
	/// @author Luc Girardin (luc dot girardin at macrofocus dot com)
	/// @version 2.0-pre
	/// @see org.apache.poi.hssf.model.Workbook
	public class RecalcIdRecord : StandardRecord
	{
		public const short sid = 449;

		private int _reserved0;

		/// An unsigned integer that specifies the recalculation engine identifier
		/// of the recalculation engine that performed the last recalculation.
		/// If the value is less than the recalculation engine identifier associated with the application,
		/// the application will recalculate the results of all formulas on
		/// this workbook immediately after loading the file
		private int _engineId;

		public bool IsNeeded => true;

		public int EngineId
		{
			get
			{
				return _engineId;
			}
			set
			{
				_engineId = value;
			}
		}

		protected override int DataSize => 8;

		public override short Sid => 449;

		public RecalcIdRecord()
		{
			_reserved0 = 0;
			_engineId = 0;
		}

		/// Constructs a RECALCID record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public RecalcIdRecord(RecordInputStream in1)
		{
			in1.ReadUShort();
			_reserved0 = in1.ReadUShort();
			_engineId = in1.ReadInt();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[RECALCID]\n");
			stringBuilder.Append("    .reserved = ").Append(HexDump.ShortToHex(_reserved0)).Append("\n");
			stringBuilder.Append("    .engineId = ").Append(HexDump.IntToHex(_engineId)).Append("\n");
			stringBuilder.Append("[/RECALCID]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(449);
			out1.WriteShort(_reserved0);
			out1.WriteInt(_engineId);
		}
	}
}

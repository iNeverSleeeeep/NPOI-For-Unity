using NPOI.Util;
using System;

namespace NPOI.HSSF.Record
{
	/// <summary>
	/// this record only used for record that has name and not implemented.
	/// </summary>
	public abstract class RowDataRecord : StandardRecord
	{
		private byte[] _rawData;

		protected override int DataSize => _rawData.Length;

		public override short Sid
		{
			get
			{
				throw new NotImplementedException("must be implemented in sub class");
			}
		}

		public RowDataRecord(RecordInputStream in1)
		{
			_rawData = in1.ReadRemainder();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.Write(_rawData);
		}
	}
}

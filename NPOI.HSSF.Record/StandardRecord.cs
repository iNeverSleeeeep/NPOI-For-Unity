using NPOI.Util;
using System;

namespace NPOI.HSSF.Record
{
	/// Subclasses of this class (the majority of BIFF records) are non-continuable.  This allows for
	/// some simplification of serialization logic
	///
	/// @author Josh Micich
	public abstract class StandardRecord : Record
	{
		protected abstract int DataSize
		{
			get;
		}

		public override int RecordSize => 4 + DataSize;

		/// <summary>
		/// Write the data content of this BIFF record including the sid and record length.
		/// The subclass must write the exact number of bytes as reported by Record#getRecordSize()
		/// </summary>
		/// <param name="offset">offset</param>
		/// <param name="data">data</param>
		/// <returns></returns>
		public override int Serialize(int offset, byte[] data)
		{
			int dataSize = DataSize;
			int num = 4 + dataSize;
			LittleEndianByteArrayOutputStream littleEndianByteArrayOutputStream = new LittleEndianByteArrayOutputStream(data, offset, num);
			littleEndianByteArrayOutputStream.WriteShort(Sid);
			littleEndianByteArrayOutputStream.WriteShort(dataSize);
			Serialize(littleEndianByteArrayOutputStream);
			if (littleEndianByteArrayOutputStream.WriteIndex - offset != num)
			{
				throw new InvalidOperationException("Error in serialization of (" + GetType().Name + "): Incorrect number of bytes written - expected " + num + " but got " + (littleEndianByteArrayOutputStream.WriteIndex - offset));
			}
			return num;
		}

		/// Write the data content of this BIFF record.  The 'ushort sid' and 'ushort size' header fields
		/// have already been written by the superclass.<br />
		///
		/// The number of bytes written must equal the record size reported by
		/// {@link Record#getDataSize()} minus four
		/// ( record header consiting of a 'ushort sid' and 'ushort reclength' has already been written
		/// by thye superclass).
		public abstract void Serialize(ILittleEndianOutput out1);
	}
}

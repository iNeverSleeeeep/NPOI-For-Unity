using NPOI.Util;

namespace NPOI.HSSF.Record.Cont
{
	/// Common superclass of all records that can produce {@link ContinueRecord}s while being Serialized.
	///
	/// @author Josh Micich
	public abstract class ContinuableRecord : Record
	{
		/// @return the total Length of the encoded record(s) 
		/// (Note - if any {@link ContinueRecord} is required, this result includes the
		/// size of those too)
		public override int RecordSize
		{
			get
			{
				ContinuableRecordOutput continuableRecordOutput = ContinuableRecordOutput.CreateForCountingOnly();
				Serialize(continuableRecordOutput);
				continuableRecordOutput.Terminate();
				return continuableRecordOutput.TotalSize;
			}
		}

		/// Serializes this record's content to the supplied data output.<br />
		/// The standard BIFF header (ushort sid, ushort size) has been handled by the superclass, so 
		/// only BIFF data should be written by this method.  Simple data types can be written with the
		/// standard {@link LittleEndianOutput} methods.  Methods from {@link ContinuableRecordOutput} 
		/// can be used to Serialize strings (with {@link ContinueRecord}s being written as required).
		/// If necessary, implementors can explicitly start {@link ContinueRecord}s (regardless of the
		/// amount of remaining space).
		///
		/// @param out a data output stream
		protected abstract void Serialize(ContinuableRecordOutput out1);

		public override int Serialize(int offset, byte[] data)
		{
			ILittleEndianOutput @out = new LittleEndianByteArrayOutputStream(data, offset);
			ContinuableRecordOutput continuableRecordOutput = new ContinuableRecordOutput(@out, Sid);
			Serialize(continuableRecordOutput);
			continuableRecordOutput.Terminate();
			return continuableRecordOutput.TotalSize;
		}
	}
}

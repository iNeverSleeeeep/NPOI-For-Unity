using System;

namespace NPOI.Util
{
	/// A common exception thrown by our binary format Parsers
	///  (especially HSSF and DDF), when they hit invalid
	///  format or data when Processing a record.
	[Serializable]
	public class RecordFormatException : RuntimeException
	{
		public RecordFormatException(string exception)
			: base(exception)
		{
		}

		public RecordFormatException(string exception, Exception ex)
			: base(exception, ex)
		{
		}

		public RecordFormatException(Exception ex)
			: base(ex)
		{
		}
	}
}

using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	[Obsolete("Not found in poi,is it useful?")]
	public interface CustomField : ICloneable
	{
		/// @return  The size of this field in bytes.  This operation Is not valid
		///          Until after the call to <c>FillField()</c>
		int Size
		{
			get;
		}

		/// Populates this fields data from the byte array passed in1.
		/// @param in the RecordInputstream to Read the record from
		int FillField(RecordInputStream in1);

		/// Appends the string representation of this field to the supplied
		/// StringBuilder.
		///
		/// @param str   The string buffer to Append to.
		void ToString(StringBuilder str);

		/// Converts this field to it's byte array form.
		/// @param offset    The offset into the byte array to start writing to.
		/// @param data      The data array to Write to.
		/// @return  The number of bytes written.
		int SerializeField(int offset, byte[] data);
	}
}

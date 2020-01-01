using System.IO;

namespace NPOI.HPSF
{
	/// The Character Encoding is not supported.
	///
	/// @author  Asmus Freytag
	/// @since   JDK1.1
	public class UnsupportedEncodingException : IOException
	{
		/// Constructs an UnsupportedEncodingException without a detail message.
		public UnsupportedEncodingException()
		{
		}

		/// Constructs an UnsupportedEncodingException with a detail message.
		/// @param s Describes the reason for the exception.
		public UnsupportedEncodingException(string s)
			: base(s)
		{
		}
	}
}

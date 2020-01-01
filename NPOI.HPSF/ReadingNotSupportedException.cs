using System;

namespace NPOI.HPSF
{
	/// <summary>
	/// This exception is thrown when HPSF tries To Read a (yet) unsupported
	/// variant type.
	/// @see WritingNotSupportedException
	/// @see UnsupportedVariantTypeException
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2003-08-08
	/// </summary>
	[Serializable]
	public class ReadingNotSupportedException : UnsupportedVariantTypeException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.ReadingNotSupportedException" /> class.
		/// </summary>
		/// <param name="variantType">The unsupported variant type</param>
		/// <param name="value">The value who's variant type is not yet supported</param>
		public ReadingNotSupportedException(long variantType, object value)
			: base(variantType, value)
		{
		}
	}
}

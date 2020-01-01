using System;

namespace NPOI.HPSF
{
	/// <summary>
	/// This exception is thrown when trying To Write a (yet) unsupported variant
	/// type.
	/// @see ReadingNotSupportedException
	/// @see UnsupportedVariantTypeException
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2003-08-08
	/// </summary>
	[Serializable]
	public class WritingNotSupportedException : UnsupportedVariantTypeException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.WritingNotSupportedException" /> class.
		/// </summary>
		/// <param name="variantType">The unsupported variant type.</param>
		/// <param name="value">The value</param>
		public WritingNotSupportedException(long variantType, object value)
			: base(variantType, value)
		{
		}
	}
}

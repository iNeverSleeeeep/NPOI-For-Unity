using NPOI.Util;
using System;

namespace NPOI.HPSF
{
	/// <summary>
	/// This exception is thrown if HPSF encounters a variant type that isn't
	/// supported yet. Although a variant type is unsupported the value can still be
	/// retrieved using the {@link VariantTypeException#GetValue} method.
	/// Obviously this class should disappear some day.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2003-08-05
	/// </summary>
	[Serializable]
	public abstract class UnsupportedVariantTypeException : VariantTypeException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.UnsupportedVariantTypeException" /> class.
		/// </summary>
		/// <param name="variantType">The unsupported variant type</param>
		/// <param name="value">The value who's variant type is not yet supported</param>
		public UnsupportedVariantTypeException(long variantType, object value)
			: base(variantType, value, "HPSF does not yet support the variant type " + variantType + " (" + Variant.GetVariantName(variantType) + ", " + HexDump.ToHex(variantType) + "). If you want support for this variant type in one of the next POI releases please submit a request for enhancement (RFE) To <http://issues.apache.org/bugzilla/>! Thank you!")
		{
		}
	}
}

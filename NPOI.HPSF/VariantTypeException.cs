using System;

namespace NPOI.HPSF
{
	/// <summary>
	/// This exception is thrown if HPSF encounters a problem with a variant type.
	/// Concrete subclasses specifiy the problem further.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2004-06-21
	/// </summary>
	[Serializable]
	public abstract class VariantTypeException : HPSFException
	{
		private object value;

		private long variantType;

		/// <summary>
		/// Gets the offending variant type
		/// </summary>
		/// <value>the offending variant type.</value>
		public long VariantType => variantType;

		/// <summary>
		/// Returns the value who's variant type caused the problem.
		/// </summary>
		/// <value>the value who's variant type caused the problem.</value>
		public object Value => value;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.VariantTypeException" /> class.
		/// </summary>
		/// <param name="variantType">The variant type causing the problem</param>
		/// <param name="value">The value who's variant type causes the problem</param>
		/// <param name="msg">A message text describing the problem</param>
		public VariantTypeException(long variantType, object value, string msg)
			: base(msg)
		{
			this.variantType = variantType;
			this.value = value;
		}
	}
}

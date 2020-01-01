using System;

namespace NPOI.HPSF
{
	/// <summary>
	/// This exception is thrown if a format error in a property Set stream Is
	/// detected or when the input data do not constitute a property Set stream.
	/// The constructors of this class are analogous To those of its superclass
	/// and are documented there.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2002-02-09
	/// </summary>
	[Serializable]
	public class NoPropertySetStreamException : HPSFException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoPropertySetStreamException" /> class.
		/// </summary>
		public NoPropertySetStreamException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoPropertySetStreamException" /> class.
		/// </summary>
		/// <param name="msg">The exception's message string</param>
		public NoPropertySetStreamException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoPropertySetStreamException" /> class.
		/// </summary>
		/// <param name="reason">This exception's underlying reason</param>
		public NoPropertySetStreamException(Exception reason)
			: base(reason)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoPropertySetStreamException" /> class.
		/// </summary>
		/// <param name="msg">The exception's message string</param>
		/// <param name="reason">This exception's underlying reason</param>
		public NoPropertySetStreamException(string msg, Exception reason)
			: base(msg, reason)
		{
		}
	}
}

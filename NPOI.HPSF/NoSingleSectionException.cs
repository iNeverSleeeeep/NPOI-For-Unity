using System;

namespace NPOI.HPSF
{
	/// <summary>
	/// This exception is thrown if one of the {@link PropertySet}'s
	/// convenience methods that require a single {@link Section} is called
	/// and the {@link PropertySet} does not contain exactly one {@link
	/// Section}.
	/// The constructors of this class are analogous To those of its
	/// superclass and documented there.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2002-02-09
	/// </summary>
	[Serializable]
	public class NoSingleSectionException : HPSFRuntimeException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoSingleSectionException" /> class.
		/// </summary>
		public NoSingleSectionException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoSingleSectionException" /> class.
		/// </summary>
		/// <param name="msg">The exception's message string</param>
		public NoSingleSectionException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoSingleSectionException" /> class.
		/// </summary>
		/// <param name="reason">This exception's underlying reason</param>
		public NoSingleSectionException(Exception reason)
			: base(reason)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoSingleSectionException" /> class.
		/// </summary>
		/// <param name="msg">The exception's message string</param>
		/// <param name="reason">This exception's underlying reason</param>
		public NoSingleSectionException(string msg, Exception reason)
			: base(msg, reason)
		{
		}
	}
}

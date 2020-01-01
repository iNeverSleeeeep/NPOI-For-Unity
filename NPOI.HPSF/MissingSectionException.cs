using System;

namespace NPOI.HPSF
{
	/// <summary>
	/// This exception is thrown if one of the {@link PropertySet}'s
	/// convenience methods does not Find a required {@link Section}.
	/// The constructors of this class are analogous To those of its
	/// superclass and documented there.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2006-02-08
	/// </summary>
	[Serializable]
	public class MissingSectionException : HPSFRuntimeException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.MissingSectionException" /> class.
		/// </summary>
		public MissingSectionException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.MissingSectionException" /> class.
		/// </summary>
		/// <param name="msg">The exception's message string</param>
		public MissingSectionException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.MissingSectionException" /> class.
		/// </summary>
		/// <param name="reason">This exception's underlying reason.</param>
		public MissingSectionException(Exception reason)
			: base(reason)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.MissingSectionException" /> class.
		/// </summary>
		/// <param name="msg">The exception's message string</param>
		/// <param name="reason">This exception's underlying reason</param>
		public MissingSectionException(string msg, Exception reason)
			: base(msg, reason)
		{
		}
	}
}

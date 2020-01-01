using NPOI.Util;
using System;

namespace NPOI.HPSF
{
	/// <summary>
	/// This exception is the superclass of all other unchecked
	/// exceptions thrown in this package. It supports a nested "reason"
	/// throwable, i.e. an exception that caused this one To be thrown.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2002-02-09
	/// </summary>
	[Serializable]
	public class HPSFRuntimeException : RuntimeException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.HPSFRuntimeException" /> class.
		/// </summary>
		public HPSFRuntimeException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.HPSFRuntimeException" /> class.
		/// </summary>
		/// <param name="msg">The message string.</param>
		public HPSFRuntimeException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.HPSFRuntimeException" /> class.
		/// </summary>
		/// <param name="reason">The reason, i.e. a throwable that indirectly
		/// caused this exception.</param>
		public HPSFRuntimeException(Exception reason)
			: base(reason.Message, reason)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.HPSFRuntimeException" /> class.
		/// </summary>
		/// <param name="msg">The message string.</param>
		/// <param name="reason">The reason, i.e. a throwable that indirectly
		/// caused this exception.</param>
		public HPSFRuntimeException(string msg, Exception reason)
			: base(msg, reason)
		{
		}
	}
}

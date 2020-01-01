using System;

namespace NPOI.HPSF
{
	/// <summary>
	/// This exception is thrown if a certain type of property Set Is
	/// expected (e.g. a Document Summary Information) but the provided
	/// property Set is not of that type.
	/// The constructors of this class are analogous To those of its
	/// superclass and documented there.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2002-02-09 
	/// </summary>
	[Serializable]
	public class UnexpectedPropertySetTypeException : HPSFException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.UnexpectedPropertySetTypeException" /> class.
		/// </summary>
		public UnexpectedPropertySetTypeException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.UnexpectedPropertySetTypeException" /> class.
		/// </summary>
		/// <param name="msg">The message string.</param>
		public UnexpectedPropertySetTypeException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.UnexpectedPropertySetTypeException" /> class.
		/// </summary>
		/// <param name="reason">The reason, i.e. a throwable that indirectly
		/// caused this exception.</param>
		public UnexpectedPropertySetTypeException(Exception reason)
			: base(reason)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.UnexpectedPropertySetTypeException" /> class.
		/// </summary>
		/// <param name="msg">The message string.</param>
		/// <param name="reason">The reason, i.e. a throwable that indirectly
		/// caused this exception.</param>
		public UnexpectedPropertySetTypeException(string msg, Exception reason)
			: base(msg, reason)
		{
		}
	}
}

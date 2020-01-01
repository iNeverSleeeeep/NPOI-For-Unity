using System;

namespace NPOI.HPSF
{
	/// <summary>
	/// This exception is thrown if a {@link MutablePropertySet} is To be written
	/// but does not have a formatID Set (see {@link
	/// MutableSection#SetFormatID(ClassID)} or
	/// {@link org.apache.poi.hpsf.MutableSection#SetFormatID(byte[])}.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2002-09-03 
	/// </summary>
	[Serializable]
	public class NoFormatIDException : HPSFRuntimeException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoFormatIDException" /> class.
		/// </summary>
		public NoFormatIDException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoFormatIDException" /> class.
		/// </summary>
		/// <param name="msg">The exception's message string</param>
		public NoFormatIDException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoFormatIDException" /> class.
		/// </summary>
		/// <param name="reason">This exception's underlying reason</param>
		public NoFormatIDException(Exception reason)
			: base(reason)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.NoFormatIDException" /> class.
		/// </summary>
		/// <param name="msg">The exception's message string</param>
		/// <param name="reason">This exception's underlying reason</param>
		public NoFormatIDException(string msg, Exception reason)
			: base(msg, reason)
		{
		}
	}
}

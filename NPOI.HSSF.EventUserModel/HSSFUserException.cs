using System;

namespace NPOI.HSSF.EventUserModel
{
	/// <summary>
	/// This exception Is provided as a way for API users to throw
	/// exceptions from their event handling code. By doing so they
	/// abort file Processing by the HSSFEventFactory and by
	/// catching it from outside the HSSFEventFactory.ProcessEvents
	/// method they can diagnose the cause for the abort.
	/// The HSSFUserException supports a nested "reason"
	/// throwable, i.e. an exception that caused this one to be thrown.
	/// The HSSF package does not itself throw any of these
	/// exceptions.
	/// </summary>
	/// <remarks>
	/// @author Rainer Klute (klute@rainer-klute.de)
	/// @author Carey Sublette (careysub@earthling.net)
	/// </remarks>
	[Serializable]
	public class HSSFUserException : Exception
	{
		/// <summary>
		/// Creates a new HSSFUserException
		/// </summary>
		public HSSFUserException()
		{
		}

		/// <summary>
		/// Creates a new HSSFUserException with a message
		/// string.
		/// </summary>
		/// <param name="msg">The MSG.</param>
		public HSSFUserException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// Creates a new HSSFUserException with a reason.
		/// </summary>
		/// <param name="reason">The reason.</param>
		public HSSFUserException(Exception reason)
		{
		}

		/// <summary>
		/// Creates a new HSSFUserException with a message string
		/// and a reason.
		/// </summary>
		/// <param name="msg">The MSG.</param>
		/// <param name="reason">The reason.</param>
		public HSSFUserException(string msg, Exception reason)
			: base(msg, reason)
		{
		}
	}
}

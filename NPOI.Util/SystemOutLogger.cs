using System;
using System.Configuration;
using System.Globalization;

namespace NPOI.Util
{
	/// <summary>
	/// A logger class that strives to make it as easy as possible for
	/// developers to write log calls, while simultaneously making those
	/// calls as cheap as possible by performing lazy Evaluation of the log
	/// message.
	/// </summary>
	/// <remarks>
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// @author Glen Stampoultzis (glens at apache.org)
	/// @author Nicola Ken Barozzi (nicolaken at apache.org)
	/// </remarks>
	public class SystemOutLogger : POILogger
	{
		private string _cat;

		public override void Initialize(string cat)
		{
			_cat = cat;
		}

		/// <summary>
		/// Log a message
		/// </summary>
		/// <param name="level">One of DEBUG, INFO, WARN, ERROR, FATAL</param>
		/// <param name="obj1">The object to log.</param>
		public override void Log(int level, object obj1)
		{
			Log(level, obj1, null);
		}

		/// <summary>
		/// Log a message
		/// </summary>
		/// <param name="level"> One of DEBUG, INFO, WARN, ERROR, FATAL</param>
		/// <param name="obj1">The object to log.  This is Converted to a string.</param>
		/// <param name="exception">An exception to be logged</param>
		public override void Log(int level, object obj1, Exception exception)
		{
			if (Check(level))
			{
				Console.WriteLine("[" + _cat + "] " + obj1);
				if (exception != null)
				{
					Console.Write(exception.StackTrace);
				}
			}
		}

		/// <summary>
		/// Check if a logger is enabled to log at the specified level
		/// </summary>
		/// <param name="level">One of DEBUG, INFO, WARN, ERROR, FATAL</param>
		/// <returns></returns>
		public override bool Check(int level)
		{
			int num;
			try
			{
				string text = ConfigurationManager.AppSettings["poi.log.level"];
				if (string.IsNullOrEmpty(text))
				{
					text = 5.ToString(CultureInfo.InvariantCulture);
				}
				num = int.Parse(text, CultureInfo.InvariantCulture);
			}
			catch (Exception)
			{
				num = 1;
			}
			if (level >= num)
			{
				return true;
			}
			return false;
		}
	}
}

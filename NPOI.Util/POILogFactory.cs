using System;
using System.Collections;
using System.Configuration;

namespace NPOI.Util
{
	public class POILogFactory
	{
		/// Map of POILogger instances, with classes as keys
		private static Hashtable _loggers = new Hashtable();

		/// A common instance of NullLogger, as it does nothing
		///  we only need the one
		private static POILogger _nullLogger = new NullLogger();

		/// The name of the class to use. Initialised the
		///  first time we need it
		private static string _loggerClassName = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.Util.POILogFactory" /> class.
		/// </summary>
		private POILogFactory()
		{
		}

		/// <summary>
		/// Get a logger, based on a class name
		/// </summary>
		/// <param name="type">the class whose name defines the log</param>
		/// <returns>a POILogger for the specified class</returns>
		public static POILogger GetLogger(Type type)
		{
			return GetLogger(type.Name);
		}

		/// <summary>
		/// Get a logger, based on a String
		/// </summary>
		/// <param name="cat">the String that defines the log</param>
		/// <returns>a POILogger for the specified class</returns>
		public static POILogger GetLogger(string cat)
		{
			POILogger pOILogger = null;
			if (_loggerClassName == null)
			{
				try
				{
					_loggerClassName = ConfigurationManager.AppSettings["loggername"];
				}
				catch (Exception)
				{
				}
				if (_loggerClassName == null)
				{
					_loggerClassName = _nullLogger.GetType().Name;
				}
			}
			if (_loggerClassName.Equals(_nullLogger.GetType().Name))
			{
				return _nullLogger;
			}
			if (_loggers.ContainsKey(cat))
			{
				pOILogger = (POILogger)_loggers[cat];
			}
			else
			{
				try
				{
					Type type = Type.GetType(_loggerClassName);
					pOILogger = (Activator.CreateInstance(type) as POILogger);
					pOILogger.Initialize(cat);
				}
				catch (Exception)
				{
					pOILogger = _nullLogger;
				}
				_loggers[cat] = pOILogger;
			}
			return pOILogger;
		}
	}
}

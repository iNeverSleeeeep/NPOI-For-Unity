using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace NPOI.Util
{
	public abstract class POILogger
	{
		public const int DEBUG = 1;

		public const int INFO = 3;

		public const int WARN = 5;

		public const int ERROR = 7;

		public const int FATAL = 9;

		/// package scope so it cannot be instantiated outside of the util
		/// package. You need a POILogger? Go to the POILogFactory for one
		public POILogger()
		{
		}

		public abstract void Initialize(string cat);

		/// Log a message
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 The object to Log.  This is converted to a string.
		public abstract void Log(int level, object obj1);

		/// Log a message
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 The object to Log.  This is converted to a string.
		/// @param exception An exception to be Logged
		public abstract void Log(int level, object obj1, Exception exception);

		/// Check if a Logger is enabled to Log at the specified level
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		public abstract bool Check(int level);

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first object to place in the message
		/// @param obj2 second object to place in the message
		public virtual void Log(int level, object obj1, object obj2)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(32).Append(obj1).Append(obj2));
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third Object to place in the message
		public virtual void Log(int level, object obj1, object obj2, object obj3)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(48).Append(obj1).Append(obj2).Append(obj3));
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third Object to place in the message
		/// @param obj4 fourth Object to place in the message
		public virtual void Log(int level, object obj1, object obj2, object obj3, object obj4)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(64).Append(obj1).Append(obj2).Append(obj3)
					.Append(obj4));
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third Object to place in the message
		/// @param obj4 fourth Object to place in the message
		/// @param obj5 fifth Object to place in the message
		public virtual void Log(int level, object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(80).Append(obj1).Append(obj2).Append(obj3)
					.Append(obj4)
					.Append(obj5));
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third Object to place in the message
		/// @param obj4 fourth Object to place in the message
		/// @param obj5 fifth Object to place in the message
		/// @param obj6 sixth Object to place in the message
		public virtual void Log(int level, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(96).Append(obj1).Append(obj2).Append(obj3)
					.Append(obj4)
					.Append(obj5)
					.Append(obj6));
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third Object to place in the message
		/// @param obj4 fourth Object to place in the message
		/// @param obj5 fifth Object to place in the message
		/// @param obj6 sixth Object to place in the message
		/// @param obj7 seventh Object to place in the message
		public virtual void Log(int level, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(112).Append(obj1).Append(obj2).Append(obj3)
					.Append(obj4)
					.Append(obj5)
					.Append(obj6)
					.Append(obj7));
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third Object to place in the message
		/// @param obj4 fourth Object to place in the message
		/// @param obj5 fifth Object to place in the message
		/// @param obj6 sixth Object to place in the message
		/// @param obj7 seventh Object to place in the message
		/// @param obj8 eighth Object to place in the message
		public virtual void Log(int level, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(128).Append(obj1).Append(obj2).Append(obj3)
					.Append(obj4)
					.Append(obj5)
					.Append(obj6)
					.Append(obj7)
					.Append(obj8));
			}
		}

		/// Log an exception, without a message
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param exception An exception to be Logged
		public virtual void Log(int level, Exception exception)
		{
			Log(level, null, exception);
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param exception An exception to be Logged
		public virtual void Log(int level, object obj1, object obj2, Exception exception)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(32).Append(obj1).Append(obj2), exception);
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third object to place in the message
		/// @param exception An error message to be Logged
		public virtual void Log(int level, object obj1, object obj2, object obj3, Exception exception)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(48).Append(obj1).Append(obj2).Append(obj3), exception);
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third object to place in the message
		/// @param obj4 fourth object to place in the message
		/// @param exception An exception to be Logged
		public virtual void Log(int level, object obj1, object obj2, object obj3, object obj4, Exception exception)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(64).Append(obj1).Append(obj2).Append(obj3)
					.Append(obj4), exception);
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third object to place in the message
		/// @param obj4 fourth object to place in the message
		/// @param obj5 fifth object to place in the message
		/// @param exception An exception to be Logged
		public virtual void Log(int level, object obj1, object obj2, object obj3, object obj4, object obj5, Exception exception)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(80).Append(obj1).Append(obj2).Append(obj3)
					.Append(obj4)
					.Append(obj5), exception);
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third object to place in the message
		/// @param obj4 fourth object to place in the message
		/// @param obj5 fifth object to place in the message
		/// @param obj6 sixth object to place in the message
		/// @param exception An exception to be Logged
		public virtual void Log(int level, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, Exception exception)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(96).Append(obj1).Append(obj2).Append(obj3)
					.Append(obj4)
					.Append(obj5)
					.Append(obj6), exception);
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third object to place in the message
		/// @param obj4 fourth object to place in the message
		/// @param obj5 fifth object to place in the message
		/// @param obj6 sixth object to place in the message
		/// @param obj7 seventh object to place in the message
		/// @param exception An exception to be Logged
		public virtual void Log(int level, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, Exception exception)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(112).Append(obj1).Append(obj2).Append(obj3)
					.Append(obj4)
					.Append(obj5)
					.Append(obj6)
					.Append(obj7), exception);
			}
		}

		/// Log a message. Lazily appends Object parameters together.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param obj1 first Object to place in the message
		/// @param obj2 second Object to place in the message
		/// @param obj3 third object to place in the message
		/// @param obj4 fourth object to place in the message
		/// @param obj5 fifth object to place in the message
		/// @param obj6 sixth object to place in the message
		/// @param obj7 seventh object to place in the message
		/// @param obj8 eighth object to place in the message
		/// @param exception An exception to be Logged
		public virtual void Log(int level, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8, Exception exception)
		{
			if (Check(level))
			{
				Log(level, new StringBuilder(128).Append(obj1).Append(obj2).Append(obj3)
					.Append(obj4)
					.Append(obj5)
					.Append(obj6)
					.Append(obj7)
					.Append(obj8), exception);
			}
		}

		/// Logs a formated message. The message itself may contain %
		/// characters as place holders. This routine will attempt to match
		/// the placeholder by looking at the type of parameter passed to
		/// obj1.
		///
		/// If the parameter is an array, it traverses the array first and
		/// matches parameters sequentially against the array items.
		/// Otherwise the parameters after <c>message</c> are matched
		/// in order.
		///
		/// If the place holder matches against a number it is printed as a
		/// whole number. This can be overridden by specifying a precision
		/// in the form %n.m where n is the padding for the whole part and
		/// m is the number of decimal places to display. n can be excluded
		/// if desired. n and m may not be more than 9.
		///
		/// If the last parameter (after flattening) is a Exception it is
		/// Logged specially.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param message The message to Log.
		/// @param obj1 The first object to match against.
		public virtual void LogFormatted(int level, string message, object obj1)
		{
			CommonLogFormatted(level, message, new object[1]
			{
				obj1
			});
		}

		/// Logs a formated message. The message itself may contain %
		/// characters as place holders. This routine will attempt to match
		/// the placeholder by looking at the type of parameter passed to
		/// obj1.
		///
		/// If the parameter is an array, it traverses the array first and
		/// matches parameters sequentially against the array items.
		/// Otherwise the parameters after <c>message</c> are matched
		/// in order.
		///
		/// If the place holder matches against a number it is printed as a
		/// whole number. This can be overridden by specifying a precision
		/// in the form %n.m where n is the padding for the whole part and
		/// m is the number of decimal places to display. n can be excluded
		/// if desired. n and m may not be more than 9.
		///
		/// If the last parameter (after flattening) is a Exception it is
		/// Logged specially.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param message The message to Log.
		/// @param obj1 The first object to match against.
		/// @param obj2 The second object to match against.
		public virtual void LogFormatted(int level, string message, object obj1, object obj2)
		{
			CommonLogFormatted(level, message, new object[2]
			{
				obj1,
				obj2
			});
		}

		/// Logs a formated message. The message itself may contain %
		/// characters as place holders. This routine will attempt to match
		/// the placeholder by looking at the type of parameter passed to
		/// obj1.
		///
		/// If the parameter is an array, it traverses the array first and
		/// matches parameters sequentially against the array items.
		/// Otherwise the parameters after <c>message</c> are matched
		/// in order.
		///
		/// If the place holder matches against a number it is printed as a
		/// whole number. This can be overridden by specifying a precision
		/// in the form %n.m where n is the padding for the whole part and
		/// m is the number of decimal places to display. n can be excluded
		/// if desired. n and m may not be more than 9.
		///
		/// If the last parameter (after flattening) is a Exception it is
		/// Logged specially.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param message The message to Log.
		/// @param obj1 The first object to match against.
		/// @param obj2 The second object to match against.
		/// @param obj3 The third object to match against.
		public virtual void LogFormatted(int level, string message, object obj1, object obj2, object obj3)
		{
			CommonLogFormatted(level, message, new object[3]
			{
				obj1,
				obj2,
				obj3
			});
		}

		/// Logs a formated message. The message itself may contain %
		/// characters as place holders. This routine will attempt to match
		/// the placeholder by looking at the type of parameter passed to
		/// obj1.
		///
		/// If the parameter is an array, it traverses the array first and
		/// matches parameters sequentially against the array items.
		/// Otherwise the parameters after <c>message</c> are matched
		/// in order.
		///
		/// If the place holder matches against a number it is printed as a
		/// whole number. This can be overridden by specifying a precision
		/// in the form %n.m where n is the padding for the whole part and
		/// m is the number of decimal places to display. n can be excluded
		/// if desired. n and m may not be more than 9.
		///
		/// If the last parameter (after flattening) is a Exception it is
		/// Logged specially.
		///
		/// @param level One of DEBUG, INFO, WARN, ERROR, FATAL
		/// @param message The message to Log.
		/// @param obj1 The first object to match against.
		/// @param obj2 The second object to match against.
		/// @param obj3 The third object to match against.
		/// @param obj4 The forth object to match against.
		public virtual void LogFormatted(int level, string message, object obj1, object obj2, object obj3, object obj4)
		{
			CommonLogFormatted(level, message, new object[4]
			{
				obj1,
				obj2,
				obj3,
				obj4
			});
		}

		private void CommonLogFormatted(int level, string message, object[] unflatParams)
		{
			if (Check(level))
			{
				object[] array = (object[])FlattenArrays(unflatParams);
				if (array[array.Length - 1].GetType() == typeof(Exception))
				{
					Log(level, string.Format(CultureInfo.InvariantCulture, message, array), (Exception)array[array.Length - 1]);
				}
				else
				{
					Log(level, string.Format(CultureInfo.InvariantCulture, message, array));
				}
			}
		}

		/// Flattens any contained objects. Only tranverses one level deep.
		private Array FlattenArrays(object[] objects)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < objects.Length; i++)
			{
				arrayList.AddRange(ObjectToObjectArray(objects[i]));
			}
			return arrayList.ToArray();
		}

		private ArrayList ObjectToObjectArray(object obj)
		{
			ArrayList arrayList = new ArrayList();
			if (obj.GetType() == typeof(char[]))
			{
				byte[] array = (byte[])obj;
				for (int i = 0; i < array.Length; i++)
				{
					arrayList.Add(array[i]);
				}
			}
			if (obj.GetType() == typeof(char[]))
			{
				char[] array2 = (char[])obj;
				for (int j = 0; j < array2.Length; j++)
				{
					arrayList.Add(array2[j]);
				}
			}
			else if (obj.GetType() == typeof(short[]))
			{
				short[] array3 = (short[])obj;
				for (int k = 0; k < array3.Length; k++)
				{
					arrayList.Add(array3[k]);
				}
			}
			else if (obj.GetType() == typeof(int[]))
			{
				int[] array4 = (int[])obj;
				for (int l = 0; l < array4.Length; l++)
				{
					arrayList.Add(array4[l]);
				}
			}
			else if (obj.GetType() == typeof(long[]))
			{
				long[] array5 = (long[])obj;
				for (int m = 0; m < array5.Length; m++)
				{
					arrayList.Add(array5[m]);
				}
			}
			else if (obj.GetType() == typeof(float[]))
			{
				float[] array6 = (float[])obj;
				for (int n = 0; n < array6.Length; n++)
				{
					arrayList.Add(array6[n]);
				}
			}
			else if (obj.GetType() == typeof(double[]))
			{
				double[] array7 = (double[])obj;
				for (int num = 0; num < array7.Length; num++)
				{
					arrayList.Add(array7[num]);
				}
			}
			else if (obj.GetType() == typeof(object[]))
			{
				object[] array8 = (object[])obj;
				for (int num2 = 0; num2 < array8.Length; num2++)
				{
					arrayList.Add(array8[num2]);
				}
			}
			else
			{
				arrayList.Add(obj);
			}
			return arrayList;
		}
	}
}

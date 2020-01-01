using NPOI.OpenXml4Net.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.OpenXml4Net.OPC.Internal
{
	public class ContentType : IComparable
	{
		/// Type in Type/Subtype.
		private string type;

		/// Subtype
		private string subType;

		/// Parameters
		private SortedList<string, string> parameters;

		/// Media type compiled pattern for parameters.
		private static Regex patternMediaType;

		/// Get the subtype.
		///
		/// @return The subtype of this content type.
		public string SubType
		{
			get
			{
				return subType;
			}
		}

		/// Get the type.
		///
		/// @return The type of this content type.
		public string Type
		{
			get
			{
				return type;
			}
		}

		static ContentType()
		{
			string text = "[^\\(\\)<>@,;:\\\\/\"\\[\\]\\?={}\\s]";
			patternMediaType = new Regex("^(" + text + "+)/(" + text + "+)$");
		}

		/// Constructor. Check the input with the RFC 2616 grammar.
		///
		/// @param contentType
		///            The content type to store.
		/// @throws InvalidFormatException
		///             If the specified content type is not valid with RFC 2616.
		public ContentType(string contentType)
		{
			Match match = patternMediaType.Match(contentType);
			if (!match.Success)
			{
				throw new InvalidFormatException("The specified content type '" + contentType + "' is not compliant with RFC 2616: malformed content type.");
			}
			if (match.Groups.Count >= 2)
			{
				type = match.Groups[1].Value;
				subType = match.Groups[2].Value;
				parameters = new SortedList<string, string>();
				for (int i = 4; i <= match.Groups.Count && match.Groups[i] != null; i += 2)
				{
					parameters[match.Groups[i].Value] = match.Groups[i + 1].Value;
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Type);
			stringBuilder.Append("/");
			stringBuilder.Append(SubType);
			return stringBuilder.ToString();
		}

		public override bool Equals(object obj)
		{
			if (obj is ContentType)
			{
				return ToString().Equals(obj.ToString(), StringComparison.InvariantCultureIgnoreCase);
			}
			return true;
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		/// Gets the value associated to the specified key.
		///
		/// @param key
		///            The key of the key/value pair.
		/// @return The value associated to the specified key.
		public string GetParameters(string key)
		{
			return parameters[key];
		}

		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return -1;
			}
			if (Equals(obj))
			{
				return 0;
			}
			return 1;
		}
	}
}

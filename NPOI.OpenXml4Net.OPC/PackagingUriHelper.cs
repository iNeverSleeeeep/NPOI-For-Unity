using NPOI.OpenXml4Net.Exceptions;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.OpenXml4Net.OPC
{
	/// Helper for part and pack Uri.
	///
	/// @author Julien Chable, CDubet, Kim Ung
	/// @version 0.1
	public class PackagingUriHelper
	{
		/// Package root Uri.
		private static Uri packageRootUri;

		/// Extension name of a relationship part.
		public static string RELATIONSHIP_PART_EXTENSION_NAME;

		/// Segment name of a relationship part.
		public static string RELATIONSHIP_PART_SEGMENT_NAME;

		/// Segment name of the package properties folder.
		public static string PACKAGE_PROPERTIES_SEGMENT_NAME;

		/// Core package properties art name.
		public static string PACKAGE_CORE_PROPERTIES_NAME;

		/// Forward slash Uri separator.
		public static char FORWARD_SLASH_CHAR;

		/// Forward slash Uri separator.
		public static string FORWARD_SLASH_STRING;

		/// Package relationships part Uri
		public static Uri PACKAGE_RELATIONSHIPS_ROOT_URI;

		/// Package relationships part name.
		public static PackagePartName PACKAGE_RELATIONSHIPS_ROOT_PART_NAME;

		/// Core properties part Uri.
		public static Uri CORE_PROPERTIES_URI;

		/// Core properties partname.
		public static PackagePartName CORE_PROPERTIES_PART_NAME;

		/// Root package Uri.
		public static Uri PACKAGE_ROOT_URI;

		/// Root package part name.
		public static PackagePartName PACKAGE_ROOT_PART_NAME;

		private static Regex missingAuthPattern;

		private static readonly bool IsMono;

		private static char[] hexDigits;

		/// Gets the Uri for the package root.
		///
		/// @return Uri of the package root.
		public static Uri PackageRootUri
		{
			get
			{
				return packageRootUri;
			}
		}

		static PackagingUriHelper()
		{
			missingAuthPattern = new Regex("\\w+://$");
			IsMono = (Type.GetType("Mono.Runtime") != null);
			hexDigits = new char[16]
			{
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9',
				'A',
				'B',
				'C',
				'D',
				'E',
				'F'
			};
			RELATIONSHIP_PART_SEGMENT_NAME = "_rels";
			RELATIONSHIP_PART_EXTENSION_NAME = ".rels";
			FORWARD_SLASH_CHAR = '/';
			FORWARD_SLASH_STRING = "/";
			PACKAGE_PROPERTIES_SEGMENT_NAME = "docProps";
			PACKAGE_CORE_PROPERTIES_NAME = "core.xml";
			Uri uri = null;
			Uri uri2 = null;
			Uri uri3 = null;
			uri = ParseUri("/", UriKind.Relative);
			uri2 = ParseUri(FORWARD_SLASH_CHAR + RELATIONSHIP_PART_SEGMENT_NAME + FORWARD_SLASH_CHAR + RELATIONSHIP_PART_EXTENSION_NAME, UriKind.Relative);
			packageRootUri = ParseUri("/", UriKind.Relative);
			uri3 = ParseUri(FORWARD_SLASH_CHAR + PACKAGE_PROPERTIES_SEGMENT_NAME + FORWARD_SLASH_CHAR + PACKAGE_CORE_PROPERTIES_NAME, UriKind.Relative);
			PACKAGE_ROOT_URI = uri;
			PACKAGE_RELATIONSHIPS_ROOT_URI = uri2;
			CORE_PROPERTIES_URI = uri3;
			PackagePartName pACKAGE_ROOT_PART_NAME = null;
			PackagePartName pACKAGE_RELATIONSHIPS_ROOT_PART_NAME = null;
			PackagePartName cORE_PROPERTIES_PART_NAME = null;
			try
			{
				pACKAGE_RELATIONSHIPS_ROOT_PART_NAME = CreatePartName(PACKAGE_RELATIONSHIPS_ROOT_URI);
				cORE_PROPERTIES_PART_NAME = CreatePartName(CORE_PROPERTIES_URI);
				pACKAGE_ROOT_PART_NAME = new PackagePartName(PACKAGE_ROOT_URI, false);
			}
			catch (InvalidFormatException)
			{
			}
			PACKAGE_RELATIONSHIPS_ROOT_PART_NAME = pACKAGE_RELATIONSHIPS_ROOT_PART_NAME;
			CORE_PROPERTIES_PART_NAME = cORE_PROPERTIES_PART_NAME;
			PACKAGE_ROOT_PART_NAME = pACKAGE_ROOT_PART_NAME;
        }

        public static Uri ParseUri(string s)
        {
            return ParseUri(s, UriKind.Absolute);
        }

        public static Uri ParseUri(string s, UriKind kind)
		{
			if (IsMono)
			{
				switch (kind)
				{
				case UriKind.Absolute:
					throw new UriFormatException();
				case UriKind.RelativeOrAbsolute:
					if (s.StartsWith("/"))
					{
						kind = UriKind.Relative;
					}
					break;
				}
			}
			return new Uri(s, kind);
		}

		/// Know if the specified Uri is a relationship part name.
		///
		/// @param partUri
		///            Uri to check.
		/// @return <i>true</i> if the Uri <i>false</i>.
		public static bool IsRelationshipPartURI(Uri partUri)
		{
			if (partUri == null)
			{
				throw new ArgumentException("partUri");
			}
			return Regex.IsMatch(partUri.OriginalString, ".*" + RELATIONSHIP_PART_SEGMENT_NAME + ".*" + RELATIONSHIP_PART_EXTENSION_NAME + "$");
		}

		/// Get file name from the specified Uri.
		public static string GetFilename(Uri uri)
		{
			if (uri != null)
			{
				string originalString = uri.OriginalString;
				int length = originalString.Length;
				int num = length;
				while (--num >= 0)
				{
					char c = originalString[num];
					if (c == FORWARD_SLASH_CHAR)
					{
						return originalString.Substring(num + 1);
					}
				}
			}
			return "";
		}

		/// Get the file name without the trailing extension.
		public static string GetFilenameWithoutExtension(Uri uri)
		{
			string filename = GetFilename(uri);
			int num = filename.LastIndexOf(".");
			if (num == -1)
			{
				return filename;
			}
			return filename.Substring(0, num);
		}

		/// Get the directory path from the specified Uri.
		public static Uri GetPath(Uri uri)
		{
			if (uri != null)
			{
				string originalString = uri.OriginalString;
				int length = originalString.Length;
				int num = length;
				while (--num >= 0)
				{
					char c = originalString[num];
					if (c == FORWARD_SLASH_CHAR)
					{
						try
						{
							return ParseUri(originalString.Substring(0, num));
						}
						catch (UriFormatException)
						{
							return null;
						}
					}
				}
			}
			return null;
		}

		/// Combine two URIs.
		///
		/// @param prefix the prefix Uri
		/// @param suffix the suffix Uri
		///
		/// @return the Combined Uri
		public static Uri Combine(Uri prefix, Uri suffix)
		{
			Uri uri = null;
			try
			{
				return ParseUri(Combine(prefix.OriginalString, suffix.OriginalString));
			}
			catch (UriFormatException)
			{
				throw new ArgumentException("Prefix and suffix can't be Combine !");
			}
		}

		/// Combine a string Uri with a prefix and a suffix.
		public static string Combine(string prefix, string suffix)
		{
			if (!prefix.EndsWith(string.Concat(FORWARD_SLASH_CHAR)) && !suffix.StartsWith(string.Concat(FORWARD_SLASH_CHAR)))
			{
				return prefix + FORWARD_SLASH_CHAR + suffix;
			}
			if ((!prefix.EndsWith(string.Concat(FORWARD_SLASH_CHAR)) && suffix.StartsWith(string.Concat(FORWARD_SLASH_CHAR))) || (prefix.EndsWith(string.Concat(FORWARD_SLASH_CHAR)) && !suffix.StartsWith(string.Concat(FORWARD_SLASH_CHAR))))
			{
				return prefix + suffix;
			}
			return "";
		}

		/// Fully relativize the source part Uri against the target part Uri.
		///
		/// @param sourceURI
		///            The source part Uri.
		/// @param targetURI
		///            The target part Uri.
		/// @return A fully relativize part name Uri ('word/media/image1.gif',
		///         '/word/document.xml' =&gt; 'media/image1.gif') else
		///         <code>null</code>.
		public static Uri RelativizeUri(Uri sourceURI, Uri targetURI, bool msCompatible)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = sourceURI.ToString().Split('/');
			string[] array2 = targetURI.ToString().Split('/');
			if (array.Length == 0)
			{
				throw new ArgumentException("Can't relativize an empty source Uri !");
			}
			if (array2.Length == 0)
			{
				throw new ArgumentException("Can't relativize an empty target Uri !");
			}
			if (!sourceURI.ToString().Equals("/"))
			{
				int num = 0;
				for (int i = 0; i < array.Length && i < array2.Length && array[i].Equals(array2[i]); i++)
				{
					num++;
				}
				if ((num == 0 || num == 1) && array[0].Equals("") && array2[0].Equals(""))
				{
					for (int j = 0; j < array.Length - 2; j++)
					{
						stringBuilder.Append("../");
					}
					for (int k = 0; k < array2.Length; k++)
					{
						if (!array2[k].Equals(""))
						{
							stringBuilder.Append(array2[k]);
							if (k != array2.Length - 1)
							{
								stringBuilder.Append("/");
							}
						}
					}
					try
					{
						return ParseUri(stringBuilder.ToString(), UriKind.RelativeOrAbsolute);
					}
					catch (Exception)
					{
						return null;
					}
				}
				if (num == array.Length && num == array2.Length)
				{
					if (sourceURI.Equals(targetURI))
					{
						stringBuilder.Append(array[array.Length - 1]);
					}
					else
					{
						stringBuilder.Append("");
					}
				}
				else
				{
					if (num == 1)
					{
						stringBuilder.Append("/");
					}
					else
					{
						for (int l = num; l < array.Length - 1; l++)
						{
							stringBuilder.Append("../");
						}
					}
					for (int m = num; m < array2.Length; m++)
					{
						if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] != '/')
						{
							stringBuilder.Append("/");
						}
						stringBuilder.Append(array2[m]);
					}
				}
				try
				{
					return ParseUri(stringBuilder.ToString(), UriKind.RelativeOrAbsolute);
				}
				catch (Exception)
				{
					return null;
				}
			}
			string text = targetURI.ToString();
			if (msCompatible && text.Length > 0 && text[0] == '/')
			{
				try
				{
					targetURI = ParseUri(text.Substring(1), UriKind.RelativeOrAbsolute);
					return targetURI;
				}
				catch (Exception)
				{
					return null;
				}
			}
			return targetURI;
		}

		/// Fully relativize the source part URI against the target part URI.
		///
		/// @param sourceURI
		///            The source part URI.
		/// @param targetURI
		///            The target part URI.
		/// @return A fully relativize part name URI ('word/media/image1.gif',
		///         '/word/document.xml' =&gt; 'media/image1.gif') else
		///         <code>null</code>.
		public static Uri RelativizeUri(Uri sourceURI, Uri targetURI)
		{
			return RelativizeUri(sourceURI, targetURI, false);
		}

		/// Resolve a source uri against a target.
		///
		/// @param sourcePartUri
		///            The source Uri.
		/// @param targetUri
		///            The target Uri.
		/// @return The resolved Uri.
		public static Uri ResolvePartUri(Uri sourcePartUri, Uri targetUri)
		{
			if (sourcePartUri == null || sourcePartUri.IsAbsoluteUri)
			{
				throw new ArgumentException("sourcePartUri invalid - " + sourcePartUri);
			}
			if (targetUri == null || targetUri.IsAbsoluteUri)
			{
				throw new ArgumentException("targetUri invalid - " + targetUri);
			}
			string text = (!(sourcePartUri.OriginalString == "/")) ? Path.GetDirectoryName(sourcePartUri.OriginalString).Replace("\\", "/") : "/";
			string text2 = targetUri.OriginalString;
			if (text2.StartsWith("../"))
			{
				string[] array = text.Split('/');
				int num = array.Length - 1;
				while (text2.StartsWith("../"))
				{
					text2 = text2.Substring(3);
					num--;
				}
				text = "/";
				for (int i = 0; i <= num; i++)
				{
					if (array[i] != string.Empty)
					{
						text = text + array[i] + "/";
					}
				}
				text += text2;
			}
			else
			{
				text = Path.Combine(text, targetUri.OriginalString).Replace("\\", "/");
			}
			return ParseUri(text, UriKind.RelativeOrAbsolute);
		}

		/// Get Uri from a string path.
		public static Uri GetURIFromPath(string path)
		{
			Uri uri = null;
			try
			{
				return ParseUri(path, UriKind.RelativeOrAbsolute);
			}
			catch (UriFormatException)
			{
				throw new ArgumentException("path");
			}
		}

		/// Get the source part Uri from a specified relationships part.
		///
		/// @param relationshipPartUri
		///            The relationship part use to retrieve the source part.
		/// @return The source part Uri from the specified relationships part.
		public static Uri GetSourcePartUriFromRelationshipPartUri(Uri relationshipPartUri)
		{
			if (relationshipPartUri == null)
			{
				throw new ArgumentException("Must not be null");
			}
			if (!IsRelationshipPartURI(relationshipPartUri))
			{
				throw new ArgumentException("Must be a relationship part");
			}
			if (Uri.Compare(relationshipPartUri, PACKAGE_RELATIONSHIPS_ROOT_URI, UriComponents.AbsoluteUri, UriFormat.SafeUnescaped, StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return PACKAGE_ROOT_URI;
			}
			string originalString = relationshipPartUri.OriginalString;
			string filenameWithoutExtension = GetFilenameWithoutExtension(relationshipPartUri);
			originalString = originalString.Substring(0, originalString.Length - filenameWithoutExtension.Length - RELATIONSHIP_PART_EXTENSION_NAME.Length);
			originalString = originalString.Substring(0, originalString.Length - RELATIONSHIP_PART_SEGMENT_NAME.Length - 1);
			originalString = Combine(originalString, filenameWithoutExtension);
			return GetURIFromPath(originalString);
		}

		/// Create an OPC compliant part name by throwing an exception if the Uri is
		/// not valid.
		///
		/// @param partUri
		///            The part name Uri to validate.
		/// @return A valid part name object, else <code>null</code>.
		/// @throws InvalidFormatException
		///             Throws if the specified Uri is not OPC compliant.
		public static PackagePartName CreatePartName(Uri partUri)
		{
			if (partUri == null)
			{
				throw new ArgumentException("partName");
			}
			return new PackagePartName(partUri, true);
		}

		/// Create an OPC compliant part name.
		///
		/// @param partName
		///            The part name to validate.
		/// @return The correspondant part name if valid, else <code>null</code>.
		/// @throws InvalidFormatException
		///             Throws if the specified part name is not OPC compliant.
		/// @see #CreatePartName(Uri)
		public static PackagePartName CreatePartName(string partName)
		{
			Uri partUri;
			try
			{
				partName = partName.Replace("\\", "/");
				partUri = ParseUri(partName, UriKind.Relative);
			}
			catch (UriFormatException ex)
			{
				throw new InvalidFormatException(ex.Message);
			}
			return CreatePartName(partUri);
		}

		/// Create an OPC compliant part name by resolving it using a base part.
		///
		/// @param partName
		///            The part name to validate.
		/// @param relativePart
		///            The relative base part.
		/// @return The correspondant part name if valid, else <code>null</code>.
		/// @throws InvalidFormatException
		///             Throws if the specified part name is not OPC compliant.
		/// @see #CreatePartName(Uri)
		public static PackagePartName CreatePartName(string partName, PackagePart relativePart)
		{
			Uri partUri;
			try
			{
				partUri = ResolvePartUri(relativePart.PartName.URI, ParseUri(partName, UriKind.RelativeOrAbsolute));
			}
			catch (UriFormatException ex)
			{
				throw new InvalidFormatException(ex.Message);
			}
			return CreatePartName(partUri);
		}

		/// Create an OPC compliant part name by resolving it using a base part.
		///
		/// @param partName
		///            The part name Uri to validate.
		/// @param relativePart
		///            The relative base part.
		/// @return The correspondant part name if valid, else <code>null</code>.
		/// @throws InvalidFormatException
		///             Throws if the specified part name is not OPC compliant.
		/// @see #CreatePartName(Uri)
		public static PackagePartName CreatePartName(Uri partName, PackagePart relativePart)
		{
			Uri partUri = ResolvePartUri(relativePart.PartName.URI, partName);
			return CreatePartName(partUri);
		}

		/// Validate a part Uri by returning a bool.
		/// ([M1.1],[M1.3],[M1.4],[M1.5],[M1.6])
		///
		/// (OPC Specifications 8.1.1 Part names) :
		///
		/// Part Name Syntax
		///
		/// The part name grammar is defined as follows:
		///
		/// <i>part_name = 1*( "/" segment )
		///
		/// segment = 1*( pchar )</i>
		///
		///
		/// (pchar is defined in RFC 3986)
		///
		/// @param partUri
		///            The Uri to validate.
		/// @return <b>true</b> if the Uri is valid to the OPC Specifications, else
		///         <b>false</b>
		///
		/// @see #CreatePartName(Uri)
		public static bool IsValidPartName(Uri partUri)
		{
			if (!(partUri == null))
			{
				try
				{
					CreatePartName(partUri);
					return true;
				}
				catch (Exception)
				{
					return false;
				}
			}
			throw new ArgumentException("partUri");
		}

		/// Decode a Uri by converting all percent encoded character into a String
		/// character.
		///
		/// @param uri
		///            The Uri to decode.
		/// @return The specified Uri in a String with converted percent encoded
		///         characters.
		public static string DecodeURI(Uri uri)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string originalString = uri.OriginalString;
			for (int i = 0; i < originalString.Length; i++)
			{
				char c = originalString[i];
				if (c == '%')
				{
					if (originalString.Length - i < 2)
					{
						throw new ArgumentException("The uri " + originalString + " contain invalid encoded character !");
					}
					char value = (char)Convert.ToInt32(originalString.Substring(i + 1, i + 3), 16);
					stringBuilder.Append(value);
					i += 2;
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		public static Uri ToUri(string value)
		{
			if (value.IndexOf("\\") != -1)
			{
				value = value.Replace('\\', '/');
			}
			value = value.Trim();
			int num = value.IndexOf('#');
			if (num != -1)
			{
				string str = value.Substring(0, num);
				string s = value.Substring(num + 1);
				value = str + "#" + Encode(s);
			}
			if (value.Length > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num2;
				for (num2 = value.Length - 1; num2 >= 0; num2--)
				{
					char c = value[num2];
					if (!char.IsWhiteSpace(c) && c != '\u00a0')
					{
						break;
					}
					stringBuilder.Append(c);
				}
				if (stringBuilder.Length > 0)
				{
					char[] array = stringBuilder.ToString().ToCharArray();
					Array.Reverse(array);
					value = value.Substring(0, num2 + 1) + Encode(new string(array));
				}
			}
			if (missingAuthPattern.IsMatch(value))
			{
				value += "/";
			}
			return ParseUri(value, UriKind.RelativeOrAbsolute);
		}

		public static string Encode(string s)
		{
			if (s.Length == 0)
			{
				return s;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = bytes;
			foreach (byte b in array)
			{
				int num = b & 0xFF;
				if (IsUnsafe(num))
				{
					stringBuilder.Append('%');
					stringBuilder.Append(hexDigits[(num >> 4) & 0xF]);
					stringBuilder.Append(hexDigits[num & 0xF]);
				}
				else
				{
					stringBuilder.Append((char)num);
				}
			}
			return stringBuilder.ToString();
		}

		private static bool IsUnsafe(int ch)
		{
			if (ch <= 128 && !char.IsWhiteSpace((char)ch))
			{
				return ch == 160;
			}
			return true;
		}

		/// Build a part name where the relationship should be stored ((ex
		/// /word/document.xml -&gt; /word/_rels/document.xml.rels)
		///
		/// @param partName
		///            Source part Uri
		/// @return the full path (as Uri) of the relation file
		/// @throws InvalidOperationException
		///             Throws if the specified Uri is a relationshp part.
		public static PackagePartName GetRelationshipPartName(PackagePartName partName)
		{
			if (partName == null)
			{
				throw new ArgumentException("partName");
			}
			if (PACKAGE_ROOT_URI.OriginalString == partName.URI.OriginalString)
			{
				return PACKAGE_RELATIONSHIPS_ROOT_PART_NAME;
			}
			if (!partName.IsRelationshipPartURI())
			{
				string originalString = partName.URI.OriginalString;
				string filename = GetFilename(partName.URI);
				originalString = originalString.Substring(0, originalString.Length - filename.Length);
				originalString = Combine(originalString, RELATIONSHIP_PART_SEGMENT_NAME);
				originalString = Combine(originalString, filename);
				originalString += RELATIONSHIP_PART_EXTENSION_NAME;
				try
				{
					return CreatePartName(originalString);
				}
				catch (InvalidFormatException)
				{
					return null;
				}
			}
			throw new InvalidOperationException("Can't be a relationship part");
		}
	}
}

using NPOI.OpenXml4Net.Exceptions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NPOI.OpenXml4Net.OPC
{
	/// An immutable Open Packaging Convention compliant part name.
	///
	/// @author Julien Chable
	///
	/// @see <a href="http://www.ietf.org/rfc/rfc3986.txt">http://www.ietf.org/rfc/rfc3986.txt</a>
	public class PackagePartName : Comparer<PackagePartName>, IComparable
	{
		/// Part name stored as an URI.
		private Uri partNameURI;

		/// Reserved characters for sub delimitations.
		private static string[] RFC3986_PCHAR_SUB_DELIMS = new string[11]
		{
			"!",
			"$",
			"&",
			"'",
			"(",
			")",
			"*",
			"+",
			",",
			";",
			"="
		};

		private static string[] RFC3986_PCHAR_UNRESERVED_SUP = new string[4]
		{
			"-",
			".",
			"_",
			"~"
		};

		/// Authorized reserved characters for pChar.
		private static string[] RFC3986_PCHAR_AUTHORIZED_SUP = new string[2]
		{
			":",
			"@"
		};

		/// Flag to know if this part name is from a relationship part name.
		private bool isRelationship;

		/// Retrieves the extension of the part name if any. If there is no extension
		/// returns an empty String. Example : '/document/content.xml' =&gt; 'xml'
		///
		/// @return The extension of the part name.
		public string Extension
		{
			get
			{
				string originalString = partNameURI.OriginalString;
				if (originalString.Length > 0)
				{
					int num = originalString.LastIndexOf(".");
					if (num > -1)
					{
						return originalString.Substring(num + 1);
					}
				}
				return "";
			}
		}

		/// Get this part name.
		///
		/// @return The name of this part name.
		public string Name
		{
			get
			{
				return partNameURI.OriginalString;
			}
		}

		/// Part name property getter.
		///
		/// @return This part name URI.
		public Uri URI
		{
			get
			{
				return partNameURI;
			}
		}

		/// Constructor. Makes a ValidPartName object from a java.net.URI
		///
		/// @param uri
		///            The URI to validate and to transform into ValidPartName.
		/// @param checkConformance
		///            Flag to specify if the contructor have to validate the OPC
		///            conformance. Must be always <code>true</code> except for
		///            special URI like '/' which is needed for internal use by
		///            OpenXml4Net but is not valid.
		/// @throws InvalidFormatException
		///             Throw if the specified part name is not conform to Open
		///             Packaging Convention specifications.
		/// @see java.net.URI
		public PackagePartName(Uri uri, bool checkConformance)
		{
			if (checkConformance)
			{
				ThrowExceptionIfInvalidPartUri(uri);
			}
			else if (!PackagingUriHelper.PACKAGE_ROOT_URI.Equals(uri))
			{
				throw new OpenXml4NetException("OCP conformance must be check for ALL part name except special cases : ['/']");
			}
			partNameURI = uri;
			isRelationship = IsRelationshipPartURI(partNameURI);
		}

		/// Constructor. Makes a ValidPartName object from a String part name.
		///
		/// @param partName
		///            Part name to valid and to create.
		/// @param checkConformance
		///            Flag to specify if the contructor have to validate the OPC
		///            conformance. Must be always <code>true</code> except for
		///            special URI like '/' which is needed for internal use by
		///            OpenXml4Net but is not valid.
		/// @throws InvalidFormatException
		///             Throw if the specified part name is not conform to Open
		///             Packaging Convention specifications.
		internal PackagePartName(string partName, bool checkConformance)
		{
			Uri uri;
			try
			{
				uri = PackagingUriHelper.ParseUri(partName, UriKind.RelativeOrAbsolute);
			}
			catch (UriFormatException)
			{
				throw new ArgumentException("partName argmument is not a valid OPC part name !");
			}
			if (checkConformance)
			{
				ThrowExceptionIfInvalidPartUri(uri);
			}
			else if (!PackagingUriHelper.PACKAGE_ROOT_URI.Equals(uri))
			{
				throw new OpenXml4NetException("OCP conformance must be check for ALL part name except special cases : ['/']");
			}
			partNameURI = uri;
			isRelationship = IsRelationshipPartURI(partNameURI);
		}

		/// Check if the specified part name is a relationship part name.
		///
		/// @param partUri
		///            The URI to check.
		/// @return <code>true</code> if this part name respect the relationship
		///         part naming convention else <code>false</code>.
		private bool IsRelationshipPartURI(Uri partUri)
		{
			if (partUri == null)
			{
				throw new ArgumentException("partUri");
			}
			return Regex.IsMatch(partUri.OriginalString, "^.*/" + PackagingUriHelper.RELATIONSHIP_PART_SEGMENT_NAME + "/.*\\" + PackagingUriHelper.RELATIONSHIP_PART_EXTENSION_NAME + "$");
		}

		/// Know if this part name is a relationship part name.
		///
		/// @return <code>true</code> if this part name respect the relationship
		///         part naming convention else <code>false</code>.
		public bool IsRelationshipPartURI()
		{
			return isRelationship;
		}

		/// Throws an exception (of any kind) if the specified part name does not
		/// follow the Open Packaging Convention specifications naming rules.
		///
		/// @param partUri
		///            The part name to check.
		/// @throws Exception
		///             Throws if the part name is invalid.
		private static void ThrowExceptionIfInvalidPartUri(Uri partUri)
		{
			if (partUri == null)
			{
				throw new ArgumentException("partUri");
			}
			ThrowExceptionIfEmptyURI(partUri);
			ThrowExceptionIfAbsoluteUri(partUri);
			ThrowExceptionIfPartNameNotStartsWithForwardSlashChar(partUri);
			ThrowExceptionIfPartNameEndsWithForwardSlashChar(partUri);
			ThrowExceptionIfPartNameHaveInvalidSegments(partUri);
		}

		/// Throws an exception if the specified URI is empty. [M1.1]
		///
		/// @param partURI
		///            Part URI to check.
		/// @throws InvalidFormatException
		///             If the specified URI is empty.
		private static void ThrowExceptionIfEmptyURI(Uri partURI)
		{
			if (partURI == null)
			{
				throw new ArgumentException("partURI");
			}
			string originalString = partURI.OriginalString;
			if (originalString.Length == 0 || (originalString.Length == 1 && originalString[0] == PackagingUriHelper.FORWARD_SLASH_CHAR))
			{
				throw new InvalidFormatException("A part name shall not be empty [M1.1]: " + partURI.OriginalString);
			}
		}

		/// Throws an exception if the part name has empty segments. [M1.3]
		///
		/// Throws an exception if a segment any characters other than pchar
		/// characters. [M1.6]
		///
		/// Throws an exception if a segment contain percent-encoded forward slash
		/// ('/'), or backward slash ('\') characters. [M1.7]
		///
		/// Throws an exception if a segment contain percent-encoded unreserved
		/// characters. [M1.8]
		///
		/// Throws an exception if the specified part name's segments end with a dot
		/// ('.') character. [M1.9]
		///
		/// Throws an exception if a segment doesn't include at least one non-dot
		/// character. [M1.10]
		///
		/// @param partUri
		///            The part name to check.
		/// @throws InvalidFormatException
		///             if the specified URI contain an empty segments or if one the
		///             segments contained in the part name, ends with a dot ('.')
		///             character.
		private static void ThrowExceptionIfPartNameHaveInvalidSegments(Uri partUri)
		{
			if (partUri == null || "".Equals(partUri))
			{
				throw new ArgumentException("partUri");
			}
			string[] array = partUri.OriginalString.Split('/');
			if (array.Length <= 1 || !array[0].Equals(""))
			{
				throw new InvalidFormatException("A part name shall not have empty segments [M1.3]: " + partUri.OriginalString);
			}
			int num = 1;
			while (true)
			{
				if (num >= array.Length)
				{
					return;
				}
				string text = array[num];
				if (text == null || "".Equals(text))
				{
					throw new InvalidFormatException("A part name shall not have empty segments [M1.3]: " + partUri.OriginalString);
				}
				if (text.EndsWith("."))
				{
					throw new InvalidFormatException("A segment shall not end with a dot ('.') character [M1.9]: " + partUri.OriginalString);
				}
				if ("".Equals(text.Replace("\\\\.", "")))
				{
					break;
				}
				CheckPCharCompliance(text);
				num++;
			}
			throw new InvalidFormatException("A segment shall include at least one non-dot character. [M1.10]: " + partUri.OriginalString);
		}

		/// Throws an exception if a segment any characters other than pchar
		/// characters. [M1.6]
		///
		/// Throws an exception if a segment contain percent-encoded forward slash
		/// ('/'), or backward slash ('\') characters. [M1.7]
		///
		/// Throws an exception if a segment contain percent-encoded unreserved
		/// characters. [M1.8]
		///
		/// @param segment
		///            The segment to check
		private static void CheckPCharCompliance(string segment)
		{
			int num = 0;
			while (true)
			{
				if (num >= segment.Length)
				{
					return;
				}
				char c = segment[num];
				bool flag = true;
				if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
				{
					flag = false;
				}
				else
				{
					for (int i = 0; i < RFC3986_PCHAR_UNRESERVED_SUP.Length; i++)
					{
						if (c == RFC3986_PCHAR_UNRESERVED_SUP[i][0])
						{
							flag = false;
							break;
						}
					}
					int num2 = 0;
					while (flag && num2 < RFC3986_PCHAR_AUTHORIZED_SUP.Length)
					{
						if (c == RFC3986_PCHAR_AUTHORIZED_SUP[num2][0])
						{
							flag = false;
						}
						num2++;
					}
					int num3 = 0;
					while (flag && num3 < RFC3986_PCHAR_SUB_DELIMS.Length)
					{
						if (c == RFC3986_PCHAR_SUB_DELIMS[num3][0])
						{
							flag = false;
						}
						num3++;
					}
				}
				if (flag && c == '%')
				{
					if (segment.Length - num < 2)
					{
						throw new InvalidFormatException("The segment " + segment + " contain invalid encoded character !");
					}
					flag = false;
					char c2 = (char)Convert.ToInt32(segment.Substring(num + 1, 2), 16);
					num += 2;
					if (c2 == '/' || c2 == '\\')
					{
						throw new InvalidFormatException("A segment shall not contain percent-encoded forward slash ('/'), or backward slash ('') characters. [M1.7]");
					}
					if ((c2 >= 'A' && c2 <= 'Z') || (c2 >= 'a' && c2 <= 'z') || (c2 >= '0' && c2 <= '9'))
					{
						flag = true;
					}
					int num4 = 0;
					while (!flag && num4 < RFC3986_PCHAR_UNRESERVED_SUP.Length)
					{
						if (c == RFC3986_PCHAR_UNRESERVED_SUP[num4][0])
						{
							flag = true;
							break;
						}
						num4++;
					}
					if (flag)
					{
						throw new InvalidFormatException("A segment shall not contain percent-encoded unreserved characters. [M1.8]");
					}
				}
				if (flag)
				{
					break;
				}
				num++;
			}
			throw new InvalidFormatException("A segment shall not hold any characters other than pchar characters. [M1.6]");
		}

		/// Throws an exception if the specified part name doesn't start with a
		/// forward slash character '/'. [M1.4]
		///
		/// @param partUri
		///            The part name to check.
		/// @throws InvalidFormatException
		///             If the specified part name doesn't start with a forward slash
		///             character '/'.
		private static void ThrowExceptionIfPartNameNotStartsWithForwardSlashChar(Uri partUri)
		{
			string originalString = partUri.OriginalString;
			if (originalString.Length > 0 && originalString[0] != PackagingUriHelper.FORWARD_SLASH_CHAR)
			{
				throw new InvalidFormatException("A part name shall start with a forward slash ('/') character [M1.4]: " + partUri.OriginalString);
			}
		}

		/// Throws an exception if the specified part name ends with a forwar slash
		/// character '/'. [M1.5]
		///
		/// @param partUri
		///            The part name to check.
		/// @throws InvalidFormatException
		///             If the specified part name ends with a forwar slash character
		///             '/'.
		private static void ThrowExceptionIfPartNameEndsWithForwardSlashChar(Uri partUri)
		{
			string originalString = partUri.OriginalString;
			if (originalString.Length > 0 && originalString[originalString.Length - 1] == PackagingUriHelper.FORWARD_SLASH_CHAR)
			{
				throw new InvalidFormatException("A part name shall not have a forward slash as the last character [M1.5]: " + partUri.OriginalString);
			}
		}

		/// Throws an exception if the specified URI is absolute.
		///
		/// @param partUri
		///            The URI to check.
		/// @throws InvalidFormatException
		///             Throws if the specified URI is absolute.
		private static void ThrowExceptionIfAbsoluteUri(Uri partUri)
		{
			if (partUri.IsAbsoluteUri)
			{
				throw new InvalidFormatException("Absolute URI forbidden: " + partUri);
			}
		}

		/// Compare two part name following the rule M1.12 :
		///
		/// Part name equivalence is determined by comparing part names as
		/// case-insensitive ASCII strings. Packages shall not contain equivalent
		/// part names and package implementers shall neither create nor recognize
		/// packages with equivalent part names. [M1.12]
		public override int Compare(PackagePartName thisPartName, PackagePartName otherPartName)
		{
			if (otherPartName == null || thisPartName == null)
			{
				return -1;
			}
			return thisPartName.partNameURI.OriginalString.ToLower().CompareTo(otherPartName.partNameURI.OriginalString.ToLower());
		}

		public int CompareTo(object o)
		{
			if (o == null)
			{
				return -1;
			}
			PackagePartName packagePartName = (PackagePartName)o;
			return partNameURI.OriginalString.ToLower().CompareTo(packagePartName.partNameURI.OriginalString.ToLower());
		}

		/// Part name equivalence is determined by comparing part names as
		/// case-insensitive ASCII strings. Packages shall not contain equivalent
		/// part names and package implementers shall neither create nor recognize
		/// packages with equivalent part names. [M1.12]
		public override bool Equals(object otherPartName)
		{
			if (otherPartName == null || !(otherPartName is PackagePartName))
			{
				return false;
			}
			return partNameURI.OriginalString.ToLower().Equals(((PackagePartName)otherPartName).partNameURI.OriginalString.ToLower());
		}

		public override int GetHashCode()
		{
			return partNameURI.OriginalString.ToLower().GetHashCode();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}

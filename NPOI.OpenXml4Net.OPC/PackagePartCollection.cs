using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI.OpenXml4Net.OPC
{
	/// A package part collection.
	///
	/// @author Julien Chable
	/// @version 0.1
	public class PackagePartCollection : SortedList<PackagePartName, PackagePart>
	{
		private static long serialVersionUID = 2515031135957635515L;

		/// Arraylist use to store this collection part names as string for rule
		/// M1.11 optimized checking.
		private List<string> registerPartNameStr = new List<string>();

		/// Check rule [M1.11]: a package implementer shall neither create nor
		/// recognize a part with a part name derived from another part name by
		/// Appending segments to it.
		///
		/// @exception InvalidOperationException
		///                Throws if you try to add a part with a name derived from
		///                another part name.
		public PackagePart Put(PackagePartName partName, PackagePart part)
		{
			string[] array = partName.URI.OriginalString.Split(PackagingUriHelper.FORWARD_SLASH_CHAR);
			StringBuilder stringBuilder = new StringBuilder();
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (!text.Equals(""))
				{
					stringBuilder.Append(PackagingUriHelper.FORWARD_SLASH_CHAR);
				}
				stringBuilder.Append(text);
				if (registerPartNameStr.Contains(stringBuilder.ToString()))
				{
					throw new InvalidOperationException("You can't add a part with a part name derived from another part ! [M1.11]");
				}
			}
			registerPartNameStr.Add(partName.Name);
			return base[partName] = part;
		}

		public new void Remove(PackagePartName key)
		{
			registerPartNameStr.Remove(key.Name);
			base.Remove(key);
		}
	}
}

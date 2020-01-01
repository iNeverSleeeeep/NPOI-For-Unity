using NPOI.Util;
using System;

namespace NPOI.HSSF.Record
{
	/// Handles the task of deserializing a SST string.  The two main entry points are
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	/// @author Jason Height (jheight at apache.org)
	public class SSTDeserializer
	{
		private IntMapper<UnicodeString> strings;

		public SSTDeserializer(IntMapper<UnicodeString> strings)
		{
			this.strings = strings;
		}

		/// This Is the starting point where strings are constructed.  Note that
		/// strings may span across multiple continuations. Read the SST record
		/// carefully before beginning to hack.
		public void ManufactureStrings(int stringCount, RecordInputStream in1)
		{
			for (int i = 0; i < stringCount; i++)
			{
				UnicodeString str;
				if (in1.Available() == 0 && !in1.HasNextRecord)
				{
					Console.WriteLine("Ran out of data before creating all the strings! String at index " + i);
					str = new UnicodeString("");
				}
				else
				{
					str = new UnicodeString(in1);
				}
				AddToStringTable(strings, str);
			}
		}

		public static void AddToStringTable(IntMapper<UnicodeString> strings, UnicodeString str)
		{
			strings.Add(str);
		}
	}
}

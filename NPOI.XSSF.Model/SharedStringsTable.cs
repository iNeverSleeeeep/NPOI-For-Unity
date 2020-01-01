using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Spreadsheet;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XSSF.Model
{
	/// Table of strings shared across all sheets in a workbook.
	/// <p>
	/// A workbook may contain thousands of cells Containing string (non-numeric) data. Furthermore this data is very
	/// likely to be repeated across many rows or columns. The goal of implementing a single string table that is shared
	/// across the workbook is to improve performance in opening and saving the file by only Reading and writing the
	/// repetitive information once.
	/// </p>
	/// <p>
	/// Consider for example a workbook summarizing information for cities within various countries. There may be a
	/// column for the name of the country, a column for the name of each city in that country, and a column
	/// Containing the data for each city. In this case the country name is repetitive, being duplicated in many cells.
	/// In many cases the repetition is extensive, and a tremendous savings is realized by making use of a shared string
	/// table when saving the workbook. When displaying text in the spreadsheet, the cell table will just contain an
	/// index into the string table as the value of a cell, instead of the full string.
	/// </p>
	/// <p>
	/// The shared string table Contains all the necessary information for displaying the string: the text, formatting
	/// properties, and phonetic properties (for East Asian languages).
	/// </p>
	///
	/// @author Nick Birch
	/// @author Yegor Kozlov
	public class SharedStringsTable : POIXMLDocumentPart
	{
		/// Array of individual string items in the Shared String table.
		private List<CT_Rst> strings = new List<CT_Rst>();

		/// Maps strings and their indexes in the <code>strings</code> arrays
		private Dictionary<string, int> stmap = new Dictionary<string, int>();

		/// An integer representing the total count of strings in the workbook. This count does not
		/// include any numbers, it counts only the total of text strings in the workbook.
		private int count;

		/// An integer representing the total count of unique strings in the Shared String Table.
		/// A string is unique even if it is a copy of another string, but has different formatting applied
		/// at the character level.
		private int uniqueCount;

		private SstDocument _sstDoc;

		/// Return an integer representing the total count of strings in the workbook. This count does not
		/// include any numbers, it counts only the total of text strings in the workbook.
		///
		/// @return the total count of strings in the workbook
		public int Count
		{
			get
			{
				return count;
			}
		}

		/// Returns an integer representing the total count of unique strings in the Shared String Table.
		/// A string is unique even if it is a copy of another string, but has different formatting applied
		/// at the character level.
		///
		/// @return the total count of unique strings in the workbook
		public int UniqueCount
		{
			get
			{
				return uniqueCount;
			}
		}

		/// Provide low-level access to the underlying array of CT_Rst beans
		///
		/// @return array of CT_Rst beans
		public List<CT_Rst> Items
		{
			get
			{
				return strings;
			}
		}

		public SharedStringsTable()
		{
			_sstDoc = new SstDocument();
			_sstDoc.AddNewSst();
		}

		internal SharedStringsTable(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			XmlDocument xml = POIXMLDocumentPart.ConvertStreamToXml(part.GetInputStream());
			ReadFrom(xml);
		}

		public void ReadFrom(XmlDocument xml)
		{
			int num = 0;
			_sstDoc = SstDocument.Parse(xml, POIXMLDocumentPart.NamespaceManager);
			CT_Sst sst = _sstDoc.GetSst();
			count = sst.count;
			uniqueCount = sst.uniqueCount;
			foreach (CT_Rst item in sst.si)
			{
				string key = GetKey(item);
				if (key != null && !stmap.ContainsKey(key))
				{
					stmap.Add(key, num);
				}
				strings.Add(item);
				num++;
			}
		}

		private string GetKey(CT_Rst st)
		{
			return st.XmlText;
		}

		/// Return a string item by index
		///
		/// @param idx index of item to return.
		/// @return the item at the specified position in this Shared String table.
		public CT_Rst GetEntryAt(int idx)
		{
			return strings[idx];
		}

		/// Add an entry to this Shared String table (a new value is appened to the end).
		///
		/// <p>
		/// If the Shared String table already Contains this <code>CT_Rst</code> bean, its index is returned.
		/// Otherwise a new entry is aded.
		/// </p>
		///
		/// @param st the entry to add
		/// @return index the index of Added entry
		public int AddEntry(CT_Rst st)
		{
			string key = GetKey(st);
			count++;
			if (stmap.ContainsKey(key))
			{
				return stmap[key];
			}
			uniqueCount++;
			CT_Rst cT_Rst = new CT_Rst();
			_sstDoc.GetSst().si.Add(cT_Rst);
			cT_Rst.Set(st);
			int num = strings.Count;
			stmap[key] = num;
			strings.Add(cT_Rst);
			return num;
		}

		/// this table out as XML.
		///
		/// @param out The stream to write to.
		/// @throws IOException if an error occurs while writing.
		public void WriteTo(Stream out1)
		{
			CT_Sst sst = _sstDoc.GetSst();
			sst.count = count;
			sst.uniqueCount = uniqueCount;
			_sstDoc.Save(out1);
		}

		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			Stream outputStream = packagePart.GetOutputStream();
			WriteTo(outputStream);
			outputStream.Close();
		}
	}
}

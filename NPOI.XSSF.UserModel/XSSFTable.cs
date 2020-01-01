using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.Util;
using NPOI.Util;
using NPOI.XSSF.UserModel.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XSSF.UserModel
{
	/// This class : the Table Part (Open Office XML Part 4:
	/// chapter 3.5.1)
	///
	/// This implementation works under the assumption that a table Contains mappings to a subtree of an XML.
	/// The root element of this subtree an occur multiple times (one for each row of the table). The child nodes
	/// of the root element can be only attributes or element with maxOccurs=1 property set
	///
	///
	/// @author Roberto Manicardi
	public class XSSFTable : POIXMLDocumentPart
	{
		private CT_Table ctTable;

		private List<XSSFXmlColumnPr> xmlColumnPr;

		private CellReference startCellReference;

		private CellReference endCellReference;

		private string commonXPath;

		/// @return the name of the Table, if set
		public string Name
		{
			get
			{
				return ctTable.name;
			}
			set
			{
				ctTable.name = value;
			}
		}

		/// @return the display name of the Table, if set
		public string DisplayName
		{
			get
			{
				return ctTable.displayName;
			}
			set
			{
				ctTable.displayName = value;
			}
		}

		/// @return  the number of mapped table columns (see Open Office XML Part 4: chapter 3.5.1.4)
		public long NumberOfMappedColumns
		{
			get
			{
				return ctTable.tableColumns.count;
			}
		}

		/// @return the total number of rows in the selection. (Note: in this version autofiltering is ignored)
		public int RowCount
		{
			get
			{
				CellReference cellReference = GetStartCellReference();
				CellReference cellReference2 = GetEndCellReference();
				int result = -1;
				if (cellReference != null && cellReference2 != null)
				{
					result = cellReference2.Row - cellReference.Row;
				}
				return result;
			}
		}

		public XSSFTable()
		{
			ctTable = new CT_Table();
		}

		internal XSSFTable(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			XmlDocument xmlDoc = POIXMLDocumentPart.ConvertStreamToXml(part.GetInputStream());
			ReadFrom(xmlDoc);
		}

		public void ReadFrom(XmlDocument xmlDoc)
		{
			try
			{
				TableDocument tableDocument = TableDocument.Parse(xmlDoc, POIXMLDocumentPart.NamespaceManager);
				ctTable = tableDocument.GetTable();
			}
			catch (XmlException ex)
			{
				throw new IOException(ex.Message);
			}
		}

		public XSSFSheet GetXSSFSheet()
		{
			return (XSSFSheet)GetParent();
		}

		public void WriteTo(Stream out1)
		{
			UpdateHeaders();
			TableDocument tableDocument = new TableDocument();
			tableDocument.SetTable(ctTable);
			tableDocument.Save(out1);
		}

		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			Stream outputStream = packagePart.GetOutputStream();
			WriteTo(outputStream);
			outputStream.Close();
		}

		public CT_Table GetCTTable()
		{
			return ctTable;
		}

		/// Checks if this Table element Contains even a single mapping to the map identified by id
		/// @param id the XSSFMap ID
		/// @return true if the Table element contain mappings
		public bool MapsTo(long id)
		{
			bool result = false;
			List<XSSFXmlColumnPr> xmlColumnPrs = GetXmlColumnPrs();
			foreach (XSSFXmlColumnPr item in xmlColumnPrs)
			{
				if (item.GetMapId() == id)
				{
					return true;
				}
			}
			return result;
		}

		/// Calculates the xpath of the root element for the table. This will be the common part
		/// of all the mapping's xpaths
		///
		/// @return the xpath of the table's root element
		public string GetCommonXpath()
		{
			if (commonXPath == null)
			{
				Array array = null;
				foreach (CT_TableColumn item in ctTable.tableColumns.tableColumn)
				{
					if (item.xmlColumnPr != null)
					{
						string xpath = item.xmlColumnPr.xpath;
						string[] array2 = xpath.Split('/');
						if (array == null)
						{
							array = array2;
						}
						else
						{
							int num = (array.Length > array2.Length) ? array2.Length : array.Length;
							for (int i = 0; i < num; i++)
							{
								if (!array.GetValue(i).Equals(array2[i]))
								{
									ArrayList range = Arrays.AsList(array).GetRange(0, i);
									array = range.ToArray(typeof(string));
									break;
								}
							}
						}
					}
				}
				commonXPath = "";
				for (int j = 1; j < array.Length; j++)
				{
					commonXPath = commonXPath + "/" + array.GetValue(j);
				}
			}
			return commonXPath;
		}

		public List<XSSFXmlColumnPr> GetXmlColumnPrs()
		{
			if (xmlColumnPr == null)
			{
				xmlColumnPr = new List<XSSFXmlColumnPr>();
				foreach (CT_TableColumn item2 in ctTable.tableColumns.tableColumn)
				{
					if (item2.xmlColumnPr != null)
					{
						XSSFXmlColumnPr item = new XSSFXmlColumnPr(this, item2, item2.xmlColumnPr);
						xmlColumnPr.Add(item);
					}
				}
			}
			return xmlColumnPr;
		}

		/// @return The reference for the cell in the top-left part of the table
		/// (see Open Office XML Part 4: chapter 3.5.1.2, attribute ref) 
		public CellReference GetStartCellReference()
		{
			if (startCellReference == null)
			{
				string @ref = ctTable.@ref;
				if (@ref != null)
				{
					string[] array = @ref.Split(":".ToCharArray());
					string cellRef = array[0];
					startCellReference = new CellReference(cellRef);
				}
			}
			return startCellReference;
		}

		/// @return The reference for the cell in the bottom-right part of the table
		/// (see Open Office XML Part 4: chapter 3.5.1.2, attribute ref)
		public CellReference GetEndCellReference()
		{
			if (endCellReference == null)
			{
				string @ref = ctTable.@ref;
				string[] array = @ref.Split(':');
				string cellRef = array[1];
				endCellReference = new CellReference(cellRef);
			}
			return endCellReference;
		}

		/// Synchronize table headers with cell values in the parent sheet.
		/// Headers <em>must</em> be in sync, otherwise Excel will display a
		/// "Found unreadable content" message on startup.
		public void UpdateHeaders()
		{
			XSSFSheet xSSFSheet = (XSSFSheet)GetParent();
			CellReference cellReference = GetStartCellReference();
			if (cellReference != null)
			{
				int row = cellReference.Row;
				int col = cellReference.Col;
				XSSFRow xSSFRow = xSSFSheet.GetRow(row) as XSSFRow;
				if (xSSFRow != null)
				{
					foreach (CT_TableColumn item in GetCTTable().tableColumns.tableColumn)
					{
						int cellnum = (int)(item.id - 1) + col;
						XSSFCell xSSFCell = xSSFRow.GetCell(cellnum) as XSSFCell;
						if (xSSFCell != null)
						{
							item.name = xSSFCell.StringCellValue;
						}
					}
				}
			}
		}
	}
}

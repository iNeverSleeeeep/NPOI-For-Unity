using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.XSSF.Model;
using NPOI.XSSF.UserModel.Helpers;
using System.Collections.Generic;

namespace NPOI.XSSF.UserModel
{
	/// This class : the Map element (Open Office XML Part 4:
	/// chapter 3.16.2)
	///
	/// This element Contains all of the properties related to the XML map,
	/// and the behaviors expected during data refresh operations.
	///
	/// @author Roberto Manicardi
	public class XSSFMap
	{
		private CT_Map ctMap;

		private MapInfo mapInfo;

		public XSSFMap(CT_Map ctMap, MapInfo mapInfo)
		{
			this.ctMap = ctMap;
			this.mapInfo = mapInfo;
		}

		public CT_Map GetCTMap()
		{
			return ctMap;
		}

		public CT_Schema GetCTSchema()
		{
			string schemaID = ctMap.SchemaID;
			return mapInfo.GetCTSchemaById(schemaID);
		}

		public string GetSchema()
		{
			CT_Schema cTSchema = GetCTSchema();
			return cTSchema.InnerXml;
		}

		/// @return the list of Single Xml Cells that provide a map rule to this mapping.
		public List<XSSFSingleXmlCell> GetRelatedSingleXMLCell()
		{
			List<XSSFSingleXmlCell> list = new List<XSSFSingleXmlCell>();
			int numberOfSheets = mapInfo.Workbook.NumberOfSheets;
			for (int i = 0; i < numberOfSheets; i++)
			{
				XSSFSheet xSSFSheet = (XSSFSheet)mapInfo.Workbook.GetSheetAt(i);
				foreach (POIXMLDocumentPart relation in xSSFSheet.GetRelations())
				{
					if (relation is SingleXmlCells)
					{
						SingleXmlCells singleXmlCells = (SingleXmlCells)relation;
						foreach (XSSFSingleXmlCell item in singleXmlCells.GetAllSimpleXmlCell())
						{
							if (item.GetMapId() == ctMap.ID)
							{
								list.Add(item);
							}
						}
					}
				}
			}
			return list;
		}

		/// @return the list of all Tables that provide a map rule to this mapping
		public List<XSSFTable> GetRelatedTables()
		{
			List<XSSFTable> list = new List<XSSFTable>();
			int numberOfSheets = mapInfo.Workbook.NumberOfSheets;
			for (int i = 0; i < numberOfSheets; i++)
			{
				XSSFSheet xSSFSheet = (XSSFSheet)mapInfo.Workbook.GetSheetAt(i);
				foreach (POIXMLDocumentPart relation in xSSFSheet.GetRelations())
				{
					if (relation.GetPackageRelationship().RelationshipType.Equals(XSSFRelation.TABLE.Relation))
					{
						XSSFTable xSSFTable = (XSSFTable)relation;
						if (xSSFTable.MapsTo(ctMap.ID))
						{
							list.Add(xSSFTable);
						}
					}
				}
			}
			return list;
		}
	}
}

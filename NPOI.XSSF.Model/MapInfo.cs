using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XSSF.Model
{
	/// This class : the Custom XML Mapping Part (Open Office XML Part 1:
	/// chapter 12.3.6)
	///
	/// An instance of this part type Contains a schema for an XML file, and
	/// information on the behavior that is used when allowing this custom XML schema
	/// to be mapped into the spreadsheet.
	///
	/// @author Roberto Manicardi
	public class MapInfo : POIXMLDocumentPart
	{
		private CT_MapInfo mapInfo;

		private Dictionary<int, XSSFMap> maps;

		private XmlDocument xml;

		/// Returns the parent XSSFWorkbook
		///
		/// @return the parent XSSFWorkbook
		public XSSFWorkbook Workbook
		{
			get
			{
				return (XSSFWorkbook)GetParent();
			}
		}

		public MapInfo()
		{
			mapInfo = new CT_MapInfo();
		}

		internal MapInfo(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			xml = POIXMLDocumentPart.ConvertStreamToXml(part.GetInputStream());
			ReadFrom(xml);
		}

		public void ReadFrom(XmlDocument xmldoc)
		{
			try
			{
				MapInfoDocument mapInfoDocument = MapInfoDocument.Parse(xmldoc, POIXMLDocumentPart.NamespaceManager);
				mapInfo = mapInfoDocument.GetMapInfo();
				maps = new Dictionary<int, XSSFMap>();
				foreach (CT_Map item in mapInfo.Map)
				{
					maps[(int)item.ID] = new XSSFMap(item, this);
				}
			}
			catch (XmlException ex)
			{
				throw new IOException(ex.Message);
			}
		}

		/// @return the internal data object
		public CT_MapInfo GetCTMapInfo()
		{
			return mapInfo;
		}

		/// Gets the
		/// @param schemaId the schema ID
		/// @return CTSchema by it's ID
		public CT_Schema GetCTSchemaById(string schemaId)
		{
			CT_Schema result = null;
			foreach (CT_Schema item in mapInfo.Schema)
			{
				if (item.ID.Equals(schemaId))
				{
					return item;
				}
			}
			return result;
		}

		public XSSFMap GetXSSFMapById(int id)
		{
			return maps[id];
		}

		public XSSFMap GetXSSFMapByName(string name)
		{
			XSSFMap result = null;
			foreach (XSSFMap value in maps.Values)
			{
				if (value.GetCTMap().Name != null && value.GetCTMap().Name.Equals(name))
				{
					result = value;
				}
			}
			return result;
		}

		/// @return all the mappings configured in this document
		public List<XSSFMap> GetAllXSSFMaps()
		{
			List<XSSFMap> list = new List<XSSFMap>();
			foreach (XSSFMap value in maps.Values)
			{
				list.Add(value);
			}
			return list;
		}

		protected void WriteTo(Stream out1)
		{
			xml.Save(out1);
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

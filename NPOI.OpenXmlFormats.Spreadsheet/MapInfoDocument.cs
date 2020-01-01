using NPOI.OpenXml4Net.Util;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class MapInfoDocument
	{
		private CT_MapInfo map;

		public MapInfoDocument()
		{
		}

		public MapInfoDocument(CT_MapInfo map)
		{
			this.map = map;
		}

		public CT_MapInfo GetMapInfo()
		{
			return map;
		}

		public void SetMapInfo(CT_MapInfo map)
		{
			this.map = map;
		}

		public void SetComments(CT_MapInfo map)
		{
			this.map = map;
		}

		public void Save(Stream stream)
		{
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				streamWriter.Write("<MapInfo xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" SelectionNamespaces=\"xmlns:ns1='http://schemas.openxmlformats.org/spreadsheetml/2006/main'\">");
				if (map.Schema != null)
				{
					foreach (CT_Schema item in map.Schema)
					{
						streamWriter.Write(string.Format("<Schema ID=\"{0}\"", item.ID));
						if (item.Namespace != null)
						{
							streamWriter.Write(string.Format(" Namespace=\"{0}\"", item.Namespace));
						}
						if (item.SchemaRef != null)
						{
							streamWriter.Write(string.Format(" SchemaRef=\"{0}\"", item.SchemaRef));
						}
						streamWriter.Write(">");
						streamWriter.Write(item.InnerXml);
						streamWriter.Write("</Schema>");
					}
				}
				if (map.Map != null)
				{
					foreach (CT_Map item2 in map.Map)
					{
						streamWriter.Write(string.Format("<Map ID=\"{0}\"", item2.ID));
						if (item2.SchemaID != null)
						{
							streamWriter.Write(string.Format(" SchemaID=\"{0}\"", item2.SchemaID));
						}
						if (item2.RootElement != null)
						{
							streamWriter.Write(string.Format(" RootElement=\"{0}\"", item2.RootElement));
						}
						if (item2.Name != null)
						{
							streamWriter.Write(string.Format(" Name=\"{0}\"", item2.Name));
						}
						if (item2.PreserveFormat)
						{
							streamWriter.Write(" PreserveFormat=\"true\"");
						}
						if (item2.PreserveSortAFLayout)
						{
							streamWriter.Write(" PreserveSortAFLayout=\"true\"");
						}
						if (item2.ShowImportExportValidationErrors)
						{
							streamWriter.Write(" ShowImportExportValidationErrors=\"true\"");
						}
						if (item2.Append)
						{
							streamWriter.Write(" Append=\"true\"");
						}
						if (item2.AutoFit)
						{
							streamWriter.Write(" AutoFit=\"true\"");
						}
						streamWriter.Write(" />");
					}
				}
				streamWriter.Write("</MapInfo");
			}
		}

		public static MapInfoDocument Parse(XmlDocument xmldoc, XmlNamespaceManager NameSpaceManager)
		{
			MapInfoDocument mapInfoDocument = new MapInfoDocument();
			mapInfoDocument.map = new CT_MapInfo();
			mapInfoDocument.map.Map = new List<CT_Map>();
			foreach (XmlElement item in xmldoc.SelectNodes("d:MapInfo/d:Map", NameSpaceManager))
			{
				CT_Map cT_Map = new CT_Map();
				cT_Map.ID = XmlHelper.ReadUInt(item.GetAttributeNode("ID"));
				cT_Map.Name = XmlHelper.ReadString(item.GetAttributeNode("Name"));
				cT_Map.RootElement = XmlHelper.ReadString(item.GetAttributeNode("RootElement"));
				cT_Map.SchemaID = XmlHelper.ReadString(item.GetAttributeNode("SchemaID"));
				cT_Map.ShowImportExportValidationErrors = XmlHelper.ReadBool(item.GetAttributeNode("ShowImportExportValidationErrors"));
				cT_Map.PreserveFormat = XmlHelper.ReadBool(item.GetAttributeNode("PreserveFormat"));
				cT_Map.PreserveSortAFLayout = XmlHelper.ReadBool(item.GetAttributeNode("PreserveSortAFLayout"));
				cT_Map.Append = XmlHelper.ReadBool(item.GetAttributeNode("Append"));
				cT_Map.AutoFit = XmlHelper.ReadBool(item.GetAttributeNode("AutoFit"));
				mapInfoDocument.map.Map.Add(cT_Map);
			}
			mapInfoDocument.map.Schema = new List<CT_Schema>();
			foreach (XmlNode item2 in xmldoc.SelectNodes("d:MapInfo/d:Schema", NameSpaceManager))
			{
				CT_Schema cT_Schema = new CT_Schema();
				cT_Schema.ID = item2.Attributes["ID"].Value;
				if (item2.Attributes["Namespace"] != null)
				{
					cT_Schema.Namespace = item2.Attributes["Namespace"].Value;
				}
				if (item2.Attributes["SchemaRef"] != null)
				{
					cT_Schema.SchemaRef = item2.Attributes["SchemaRef"].Value;
				}
				cT_Schema.InnerXml = item2.InnerXml;
				mapInfoDocument.map.Schema.Add(cT_Schema);
			}
			return mapInfoDocument;
		}
	}
}

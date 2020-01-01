using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XSSF.UserModel.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace NPOI.XSSF.Extractor
{
	/// Maps an XLSX to an XML according to one of the mapping defined.
	///
	///
	/// The output XML Schema must respect this limitations:
	///
	/// <ul>
	/// <li> all mandatory elements and attributes must be mapped (enable validation to check this)</li>
	///
	/// <li> no &lt;any&gt; in complex type/element declaration </li>
	/// <li> no &lt;anyAttribute&gt; attributes declaration </li>
	/// <li> no recursive structures: recursive structures can't be nested more than one level </li>
	/// <li> no abstract elements: abstract complex types can be declared but must not be used in elements. </li>
	/// <li> no mixed content: an element can't contain simple text and child element(s) together </li>
	/// <li> no &lt;substitutionGroup&gt; in complex type/element declaration </li>
	/// </ul>
	public class XSSFExportToXml : IComparer<string>
	{
		private XSSFMap map;

		/// Creates a new exporter and Sets the mapping to be used when generating the XML output document
		///
		/// @param map the mapping rule to be used
		public XSSFExportToXml(XSSFMap map)
		{
			this.map = map;
		}

		/// Exports the data in an XML stream
		///
		/// @param os OutputStream in which will contain the output XML
		/// @param validate if true, validates the XML againts the XML Schema
		/// @throws SAXException
		/// @throws TransformerException  
		/// @throws ParserConfigurationException 
		public void ExportToXML(Stream os, bool validate)
		{
			ExportToXML(os, "UTF-8", validate);
		}

		private XmlDocument GetEmptyDocument()
		{
			return new XmlDocument();
		}

		/// Exports the data in an XML stream
		///
		/// @param os OutputStream in which will contain the output XML
		/// @param encoding the output charset encoding
		/// @param validate if true, validates the XML againts the XML Schema
		/// @throws SAXException
		/// @throws ParserConfigurationException 
		/// @throws TransformerException 
		/// @throws InvalidFormatException
		public void ExportToXML(Stream os, string encoding, bool validate)
		{
			List<XSSFSingleXmlCell> relatedSingleXMLCell = map.GetRelatedSingleXMLCell();
			List<XSSFTable> relatedTables = map.GetRelatedTables();
			string rootElement = map.GetCTMap().RootElement;
			XmlDocument emptyDocument = GetEmptyDocument();
			XmlElement xmlElement = null;
			xmlElement = ((!IsNamespaceDeclared()) ? emptyDocument.CreateElement(rootElement) : emptyDocument.CreateElement(rootElement, GetNamespace()));
			emptyDocument.AppendChild(xmlElement);
			List<string> list = new List<string>();
			Dictionary<string, XSSFSingleXmlCell> dictionary = new Dictionary<string, XSSFSingleXmlCell>();
			Dictionary<string, XSSFTable> dictionary2 = new Dictionary<string, XSSFTable>();
			foreach (XSSFSingleXmlCell item in relatedSingleXMLCell)
			{
				list.Add(item.GetXpath());
				dictionary[item.GetXpath()] = item;
			}
			foreach (XSSFTable item2 in relatedTables)
			{
				string commonXpath = item2.GetCommonXpath();
				list.Add(commonXpath);
				dictionary2[commonXpath] = item2;
			}
			list.Sort();
			foreach (string item3 in list)
			{
				XSSFSingleXmlCell xSSFSingleXmlCell = (!dictionary.ContainsKey(item3)) ? null : dictionary[item3];
				XSSFTable xSSFTable = (!dictionary2.ContainsKey(item3)) ? null : dictionary2[item3];
				if (!Regex.IsMatch(item3, ".*\\[.*"))
				{
					if (xSSFSingleXmlCell != null)
					{
						XSSFCell xSSFCell = (XSSFCell)xSSFSingleXmlCell.GetReferencedCell();
						if (xSSFCell != null)
						{
							XmlNode nodeByXPath = GetNodeByXPath(item3, emptyDocument.FirstChild, emptyDocument, false);
							ST_XmlDataType xmlDataType = xSSFSingleXmlCell.GetXmlDataType();
							mapCellOnNode(xSSFCell, nodeByXPath, xmlDataType);
						}
					}
					if (xSSFTable != null)
					{
						List<XSSFXmlColumnPr> xmlColumnPrs = xSSFTable.GetXmlColumnPrs();
						XSSFSheet xSSFSheet = xSSFTable.GetXSSFSheet();
						int row = xSSFTable.GetStartCellReference().Row;
						row++;
						int row2 = xSSFTable.GetEndCellReference().Row;
						for (int i = row; i <= row2; i++)
						{
							XSSFRow xSSFRow = (XSSFRow)xSSFSheet.GetRow(i);
							XmlNode nodeByXPath2 = GetNodeByXPath(xSSFTable.GetCommonXpath(), emptyDocument.FirstChild, emptyDocument, true);
							short col = xSSFTable.GetStartCellReference().Col;
							for (int j = col; j <= xSSFTable.GetEndCellReference().Col; j++)
							{
								XSSFCell xSSFCell2 = (XSSFCell)xSSFRow.GetCell(j);
								if (xSSFCell2 != null)
								{
									XSSFXmlColumnPr xSSFXmlColumnPr = xmlColumnPrs[j - col];
									string localXPath = xSSFXmlColumnPr.GetLocalXPath();
									XmlNode nodeByXPath3 = GetNodeByXPath(localXPath, nodeByXPath2, emptyDocument, false);
									ST_XmlDataType xmlDataType2 = xSSFXmlColumnPr.GetXmlDataType();
									mapCellOnNode(xSSFCell2, nodeByXPath3, xmlDataType2);
								}
							}
						}
					}
				}
			}
			bool flag = true;
			if (validate)
			{
				flag = IsValid(emptyDocument);
			}
			if (flag)
			{
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.Indent = true;
				xmlWriterSettings.Encoding = Encoding.GetEncoding(encoding);
				using (XmlWriter w = XmlWriter.Create(os, xmlWriterSettings))
				{
					emptyDocument.WriteTo(w);
				}
			}
		}

		/// Validate the generated XML against the XML Schema associated with the XSSFMap
		///
		/// @param xml the XML to validate
		/// @return
		private bool IsValid(XmlDocument xml)
		{
			return true;
		}

		private void mapCellOnNode(XSSFCell cell, XmlNode node, ST_XmlDataType outputDataType)
		{
			string text = "";
			switch (cell.CellType)
			{
			case CellType.String:
				text = cell.StringCellValue;
				break;
			case CellType.Boolean:
				text += cell.BooleanCellValue;
				break;
			case CellType.Error:
				text = cell.ErrorCellString;
				break;
			case CellType.Formula:
				text = cell.StringCellValue;
				break;
			case CellType.Numeric:
				text += cell.GetRawValue();
				break;
			}
			if (node is XmlElement)
			{
				XmlElement xmlElement = (XmlElement)node;
				xmlElement.InnerText = text;
			}
			else
			{
				node.Value = text;
			}
		}

		private string RemoveNamespace(string elementName)
		{
			if (!Regex.IsMatch(elementName, ".*:.*"))
			{
				return elementName;
			}
			return elementName.Split(':')[1];
		}

		private XmlNode GetNodeByXPath(string xpath, XmlNode rootNode, XmlDocument doc, bool CreateMultipleInstances)
		{
			string[] array = xpath.Split('/');
			XmlNode xmlNode = rootNode;
			for (int i = 2; i < array.Length; i++)
			{
				string text = RemoveNamespace(array[i]);
				if (!text.StartsWith("@"))
				{
					XmlNodeList childNodes = xmlNode.ChildNodes;
					XmlNode xmlNode2 = null;
					if (!CreateMultipleInstances || i != array.Length - 1)
					{
						xmlNode2 = selectNode(text, childNodes);
					}
					if (xmlNode2 == null)
					{
						xmlNode2 = CreateElement(doc, xmlNode, text);
					}
					xmlNode = xmlNode2;
				}
				else
				{
					XmlNode xmlNode3 = CreateAttribute(doc, xmlNode, text);
					xmlNode = xmlNode3;
				}
			}
			return xmlNode;
		}

		private XmlNode CreateAttribute(XmlDocument doc, XmlNode currentNode, string axisName)
		{
			string name = axisName.Substring(1);
			XmlAttributeCollection attributes = currentNode.Attributes;
			XmlNode xmlNode = attributes.GetNamedItem(name);
			if (xmlNode == null)
			{
				xmlNode = doc.CreateAttribute(name);
				attributes.SetNamedItem(xmlNode);
			}
			return xmlNode;
		}

		private XmlNode CreateElement(XmlDocument doc, XmlNode currentNode, string axisName)
		{
			XmlNode xmlNode = (!IsNamespaceDeclared()) ? doc.CreateElement(axisName) : doc.CreateElement(axisName, GetNamespace());
			currentNode.AppendChild(xmlNode);
			return xmlNode;
		}

		private XmlNode selectNode(string axisName, XmlNodeList list)
		{
			XmlNode result = null;
			for (int i = 0; i < list.Count; i++)
			{
				XmlNode xmlNode = list[i];
				if (xmlNode.Name.Equals(axisName))
				{
					result = xmlNode;
					break;
				}
			}
			return result;
		}

		private bool IsNamespaceDeclared()
		{
			string @namespace = GetNamespace();
			if (@namespace != null)
			{
				return !@namespace.Equals("");
			}
			return false;
		}

		private string GetNamespace()
		{
			return map.GetCTSchema().Namespace;
		}

		/// Compares two xpaths to define an ordering according to the XML Schema
		public int Compare(string leftXpath, string rightXpath)
		{
			int result = 0;
			string schema = map.GetSchema();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(schema);
			string[] array = leftXpath.Split('/');
			string[] array2 = rightXpath.Split('/');
			int num = (array.Length < array2.Length) ? array.Length : array2.Length;
			XmlNode xmlNode = xmlDocument.DocumentElement;
			for (int i = 1; i < num; i++)
			{
				string text = array[i];
				string text2 = array2[i];
				if (text.Equals(text2))
				{
					XmlNode complexTypeForElement = GetComplexTypeForElement(text, xmlDocument.DocumentElement, xmlNode);
					xmlNode = complexTypeForElement;
				}
				else
				{
					int num2 = IndexOfElementInComplexType(text, xmlNode);
					int num3 = IndexOfElementInComplexType(text2, xmlNode);
					if (num2 != -1 && num3 != -1)
					{
						if (num2 < num3)
						{
							result = -1;
						}
						if (num2 > num3)
						{
							result = 1;
						}
					}
				}
			}
			return result;
		}

		private int IndexOfElementInComplexType(string elementName, XmlNode complexType)
		{
			XmlNodeList childNodes = complexType.ChildNodes;
			int result = -1;
			for (int i = 0; i < childNodes.Count; i++)
			{
				XmlNode xmlNode = childNodes[i];
				if (xmlNode is XmlElement && xmlNode.LocalName.Equals("element"))
				{
					XmlNode namedItem = xmlNode.Attributes.GetNamedItem("name");
					if (namedItem.Value.Equals(RemoveNamespace(elementName)))
					{
						result = i;
						break;
					}
				}
			}
			return result;
		}

		private XmlNode GetComplexTypeForElement(string elementName, XmlNode xmlSchema, XmlNode localComplexTypeRootNode)
		{
			XmlNode xmlNode = null;
			string value = RemoveNamespace(elementName);
			XmlNodeList childNodes = localComplexTypeRootNode.ChildNodes;
			string value2 = "";
			for (int i = 0; i < childNodes.Count; i++)
			{
				XmlNode xmlNode2 = childNodes[i];
				if (xmlNode2 is XmlElement && xmlNode2.LocalName.Equals("element"))
				{
					XmlNode namedItem = xmlNode2.Attributes.GetNamedItem("name");
					if (namedItem.Value.Equals(value))
					{
						XmlNode namedItem2 = xmlNode2.Attributes.GetNamedItem("type");
						if (namedItem2 != null)
						{
							value2 = namedItem2.Value;
							break;
						}
					}
				}
			}
			if (!"".Equals(value2))
			{
				XmlNodeList childNodes2 = xmlSchema.ChildNodes;
				for (int j = 0; j < childNodes2.Count; j++)
				{
					XmlNode xmlNode3 = childNodes[j];
					if (xmlNode3 is XmlElement && xmlNode3.LocalName.Equals("complexType"))
					{
						XmlNode namedItem3 = xmlNode3.Attributes.GetNamedItem("name");
						if (namedItem3.Value.Equals(value2))
						{
							XmlNodeList childNodes3 = xmlNode3.ChildNodes;
							for (int k = 0; k < childNodes3.Count; k++)
							{
								XmlNode xmlNode4 = childNodes3[k];
								if (xmlNode4 is XmlElement && xmlNode4.LocalName.Equals("sequence"))
								{
									xmlNode = xmlNode4;
									break;
								}
							}
							if (xmlNode != null)
							{
								break;
							}
						}
					}
				}
			}
			return xmlNode;
		}
	}
}

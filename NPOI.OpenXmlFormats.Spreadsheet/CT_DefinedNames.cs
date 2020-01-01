using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_DefinedNames
	{
		private List<CT_DefinedName> definedNameField;

		[XmlElement]
		public List<CT_DefinedName> definedName
		{
			get
			{
				return definedNameField;
			}
			set
			{
				definedNameField = value;
			}
		}

		public static CT_DefinedNames Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DefinedNames cT_DefinedNames = new CT_DefinedNames();
			cT_DefinedNames.definedName = new List<CT_DefinedName>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "definedName")
				{
					cT_DefinedNames.definedName.Add(CT_DefinedName.Parse(childNode, namespaceManager));
				}
			}
			return cT_DefinedNames;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (definedName != null)
			{
				foreach (CT_DefinedName item in definedName)
				{
					item.Write(sw, "definedName");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_DefinedName AddNewDefinedName()
		{
			if (definedNameField == null)
			{
				definedNameField = new List<CT_DefinedName>();
			}
			CT_DefinedName cT_DefinedName = new CT_DefinedName();
			definedNameField.Add(cT_DefinedName);
			return cT_DefinedName;
		}

		public void SetDefinedNameArray(List<CT_DefinedName> array)
		{
			definedNameField = array;
		}
	}
}

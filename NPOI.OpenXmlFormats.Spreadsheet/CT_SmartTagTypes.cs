using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_SmartTagTypes
	{
		private List<CT_SmartTagType> smartTagTypeField;

		[XmlElement]
		public List<CT_SmartTagType> smartTagType
		{
			get
			{
				return smartTagTypeField;
			}
			set
			{
				smartTagTypeField = value;
			}
		}

		public static CT_SmartTagTypes Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SmartTagTypes cT_SmartTagTypes = new CT_SmartTagTypes();
			cT_SmartTagTypes.smartTagType = new List<CT_SmartTagType>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "smartTagType")
				{
					cT_SmartTagTypes.smartTagType.Add(CT_SmartTagType.Parse(childNode, namespaceManager));
				}
			}
			return cT_SmartTagTypes;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (smartTagType != null)
			{
				foreach (CT_SmartTagType item in smartTagType)
				{
					item.Write(sw, "smartTagType");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}

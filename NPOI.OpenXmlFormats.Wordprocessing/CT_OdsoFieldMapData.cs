using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_OdsoFieldMapData
	{
		private CT_MailMergeOdsoFMDFieldType typeField;

		private CT_String nameField;

		private CT_String mappedNameField;

		private CT_DecimalNumber columnField;

		private CT_Lang lidField;

		private CT_OnOff dynamicAddressField;

		[XmlElement(Order = 0)]
		public CT_MailMergeOdsoFMDFieldType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_String name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_String mappedName
		{
			get
			{
				return mappedNameField;
			}
			set
			{
				mappedNameField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_DecimalNumber column
		{
			get
			{
				return columnField;
			}
			set
			{
				columnField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_Lang lid
		{
			get
			{
				return lidField;
			}
			set
			{
				lidField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_OnOff dynamicAddress
		{
			get
			{
				return dynamicAddressField;
			}
			set
			{
				dynamicAddressField = value;
			}
		}

		public static CT_OdsoFieldMapData Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OdsoFieldMapData cT_OdsoFieldMapData = new CT_OdsoFieldMapData();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "type")
				{
					cT_OdsoFieldMapData.type = CT_MailMergeOdsoFMDFieldType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "name")
				{
					cT_OdsoFieldMapData.name = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mappedName")
				{
					cT_OdsoFieldMapData.mappedName = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "column")
				{
					cT_OdsoFieldMapData.column = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lid")
				{
					cT_OdsoFieldMapData.lid = CT_Lang.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dynamicAddress")
				{
					cT_OdsoFieldMapData.dynamicAddress = CT_OnOff.Parse(childNode, namespaceManager);
				}
			}
			return cT_OdsoFieldMapData;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (type != null)
			{
				type.Write(sw, "type");
			}
			if (name != null)
			{
				name.Write(sw, "name");
			}
			if (mappedName != null)
			{
				mappedName.Write(sw, "mappedName");
			}
			if (column != null)
			{
				column.Write(sw, "column");
			}
			if (lid != null)
			{
				lid.Write(sw, "lid");
			}
			if (dynamicAddress != null)
			{
				dynamicAddress.Write(sw, "dynamicAddress");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}

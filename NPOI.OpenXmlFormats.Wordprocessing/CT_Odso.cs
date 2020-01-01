using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Odso
	{
		private CT_String udlField;

		private CT_String tableField;

		private CT_Rel srcField;

		private CT_DecimalNumber colDelimField;

		private CT_MailMergeSourceType typeField;

		private CT_OnOff fHdrField;

		private List<CT_OdsoFieldMapData> fieldMapDataField;

		private List<CT_Rel> recipientDataField;

		[XmlElement(Order = 0)]
		public CT_String udl
		{
			get
			{
				return udlField;
			}
			set
			{
				udlField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_String table
		{
			get
			{
				return tableField;
			}
			set
			{
				tableField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Rel src
		{
			get
			{
				return srcField;
			}
			set
			{
				srcField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_DecimalNumber colDelim
		{
			get
			{
				return colDelimField;
			}
			set
			{
				colDelimField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_MailMergeSourceType type
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

		[XmlElement(Order = 5)]
		public CT_OnOff fHdr
		{
			get
			{
				return fHdrField;
			}
			set
			{
				fHdrField = value;
			}
		}

		[XmlElement("fieldMapData", Order = 6)]
		public List<CT_OdsoFieldMapData> fieldMapData
		{
			get
			{
				return fieldMapDataField;
			}
			set
			{
				fieldMapDataField = value;
			}
		}

		[XmlElement("recipientData", Order = 7)]
		public List<CT_Rel> recipientData
		{
			get
			{
				return recipientDataField;
			}
			set
			{
				recipientDataField = value;
			}
		}

		public static CT_Odso Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Odso cT_Odso = new CT_Odso();
			cT_Odso.fieldMapData = new List<CT_OdsoFieldMapData>();
			cT_Odso.recipientData = new List<CT_Rel>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "udl")
				{
					cT_Odso.udl = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "table")
				{
					cT_Odso.table = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "src")
				{
					cT_Odso.src = CT_Rel.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "colDelim")
				{
					cT_Odso.colDelim = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "type")
				{
					cT_Odso.type = CT_MailMergeSourceType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fHdr")
				{
					cT_Odso.fHdr = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fieldMapData")
				{
					cT_Odso.fieldMapData.Add(CT_OdsoFieldMapData.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "recipientData")
				{
					cT_Odso.recipientData.Add(CT_Rel.Parse(childNode, namespaceManager));
				}
			}
			return cT_Odso;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (udl != null)
			{
				udl.Write(sw, "udl");
			}
			if (table != null)
			{
				table.Write(sw, "table");
			}
			if (src != null)
			{
				src.Write(sw, "src");
			}
			if (colDelim != null)
			{
				colDelim.Write(sw, "colDelim");
			}
			if (type != null)
			{
				type.Write(sw, "type");
			}
			if (fHdr != null)
			{
				fHdr.Write(sw, "fHdr");
			}
			if (fieldMapData != null)
			{
				foreach (CT_OdsoFieldMapData fieldMapDatum in fieldMapData)
				{
					fieldMapDatum.Write(sw, "fieldMapData");
				}
			}
			if (recipientData != null)
			{
				foreach (CT_Rel recipientDatum in recipientData)
				{
					recipientDatum.Write(sw, "recipientData");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}

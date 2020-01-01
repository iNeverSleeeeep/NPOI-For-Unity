using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_SdtDate
	{
		private CT_String dateFormatField;

		private CT_Lang lidField;

		private CT_SdtDateMappingType storeMappedDataAsField;

		private CT_CalendarType calendarField;

		private string fullDateField;

		private bool fullDateFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_String dateFormat
		{
			get
			{
				return dateFormatField;
			}
			set
			{
				dateFormatField = value;
			}
		}

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
		public CT_SdtDateMappingType storeMappedDataAs
		{
			get
			{
				return storeMappedDataAsField;
			}
			set
			{
				storeMappedDataAsField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_CalendarType calendar
		{
			get
			{
				return calendarField;
			}
			set
			{
				calendarField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string fullDate
		{
			get
			{
				return fullDateField;
			}
			set
			{
				fullDateField = value;
			}
		}

		[XmlIgnore]
		public bool fullDateSpecified
		{
			get
			{
				return fullDateFieldSpecified;
			}
			set
			{
				fullDateFieldSpecified = value;
			}
		}

		public static CT_SdtDate Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtDate cT_SdtDate = new CT_SdtDate();
			cT_SdtDate.fullDateField = XmlHelper.ReadString(node.Attributes["w.fullDate"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "dateFormat")
				{
					cT_SdtDate.dateFormat = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lid")
				{
					cT_SdtDate.lid = CT_Lang.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "storeMappedDataAs")
				{
					cT_SdtDate.storeMappedDataAs = CT_SdtDateMappingType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "calendar")
				{
					cT_SdtDate.calendar = CT_CalendarType.Parse(childNode, namespaceManager);
				}
			}
			return cT_SdtDate;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:fullDate", fullDateField);
			sw.Write(">");
			if (dateFormat != null)
			{
				dateFormat.Write(sw, "dateFormat");
			}
			if (lid != null)
			{
				lid.Write(sw, "lid");
			}
			if (storeMappedDataAs != null)
			{
				storeMappedDataAs.Write(sw, "storeMappedDataAs");
			}
			if (calendar != null)
			{
				calendar.Write(sw, "calendar");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}

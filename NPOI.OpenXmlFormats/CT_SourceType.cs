using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	public class CT_SourceType
	{
		private List<object> itemsField;

		private List<ItemsChoiceType1> itemsElementNameField;

		[XmlElement("ShortTitle", typeof(string))]
		[XmlElement("StandardNumber", typeof(string))]
		[XmlElement("StateProvince", typeof(string))]
		[XmlElement("Medium", typeof(string))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("AlbumTitle", typeof(string))]
		[XmlElement("Author", typeof(CT_AuthorType))]
		[XmlElement("BookTitle", typeof(string))]
		[XmlElement("BroadcastTitle", typeof(string))]
		[XmlElement("Broadcaster", typeof(string))]
		[XmlElement("CaseNumber", typeof(string))]
		[XmlElement("ChapterNumber", typeof(string))]
		[XmlElement("City", typeof(string))]
		[XmlElement("Comments", typeof(string))]
		[XmlElement("ConferenceName", typeof(string))]
		[XmlElement("CountryRegion", typeof(string))]
		[XmlElement("Court", typeof(string))]
		[XmlElement("Day", typeof(string))]
		[XmlElement("DayAccessed", typeof(string))]
		[XmlElement("Department", typeof(string))]
		[XmlElement("Distributor", typeof(string))]
		[XmlElement("Edition", typeof(string))]
		[XmlElement("Guid", typeof(string))]
		[XmlElement("Institution", typeof(string))]
		[XmlElement("InternetSiteTitle", typeof(string))]
		[XmlElement("Issue", typeof(string))]
		[XmlElement("JournalName", typeof(string))]
		[XmlElement("LCID", typeof(string))]
		[XmlElement("AbbreviatedCaseNumber", typeof(string))]
		[XmlElement("Month", typeof(string))]
		[XmlElement("MonthAccessed", typeof(string))]
		[XmlElement("NumberVolumes", typeof(string))]
		[XmlElement("Pages", typeof(string))]
		[XmlElement("PatentNumber", typeof(string))]
		[XmlElement("PeriodicalTitle", typeof(string))]
		[XmlElement("ProductionCompany", typeof(string))]
		[XmlElement("PublicationTitle", typeof(string))]
		[XmlElement("Publisher", typeof(string))]
		[XmlElement("RecordingNumber", typeof(string))]
		[XmlElement("RefOrder", typeof(string))]
		[XmlElement("Reporter", typeof(string))]
		[XmlElement("SourceType", typeof(ST_SourceType))]
		[XmlElement("Station", typeof(string))]
		[XmlElement("Tag", typeof(string))]
		[XmlElement("Theater", typeof(string))]
		[XmlElement("ThesisType", typeof(string))]
		[XmlElement("Title", typeof(string))]
		[XmlElement("Type", typeof(string))]
		[XmlElement("URL", typeof(string))]
		[XmlElement("Version", typeof(string))]
		[XmlElement("Volume", typeof(string))]
		[XmlElement("Year", typeof(string))]
		[XmlElement("YearAccessed", typeof(string))]
		public List<object> Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		[XmlIgnore]
		public List<ItemsChoiceType1> ItemsElementName
		{
			get
			{
				return itemsElementNameField;
			}
			set
			{
				itemsElementNameField = value;
			}
		}

		public CT_SourceType()
		{
			itemsElementNameField = new List<ItemsChoiceType1>();
			itemsField = new List<object>();
		}
	}
}

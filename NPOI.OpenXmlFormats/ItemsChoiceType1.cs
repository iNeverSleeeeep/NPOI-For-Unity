using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography", IncludeInSchema = false)]
	public enum ItemsChoiceType1
	{
		AbbreviatedCaseNumber,
		AlbumTitle,
		Author,
		BookTitle,
		BroadcastTitle,
		Broadcaster,
		CaseNumber,
		ChapterNumber,
		City,
		Comments,
		ConferenceName,
		CountryRegion,
		Court,
		Day,
		DayAccessed,
		Department,
		Distributor,
		Edition,
		Guid,
		Institution,
		InternetSiteTitle,
		Issue,
		JournalName,
		LCID,
		Medium,
		Month,
		MonthAccessed,
		NumberVolumes,
		Pages,
		PatentNumber,
		PeriodicalTitle,
		ProductionCompany,
		PublicationTitle,
		Publisher,
		RecordingNumber,
		RefOrder,
		Reporter,
		ShortTitle,
		SourceType,
		StandardNumber,
		StateProvince,
		Station,
		Tag,
		Theater,
		ThesisType,
		Title,
		Type,
		URL,
		Version,
		Volume,
		Year,
		YearAccessed
	}
}

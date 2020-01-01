using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public enum ST_DocPartGallery
	{
		placeholder,
		any,
		@default,
		docParts,
		coverPg,
		eq,
		ftrs,
		hdrs,
		pgNum,
		tbls,
		watermarks,
		autoTxt,
		txtBox,
		pgNumT,
		pgNumB,
		pgNumMargins,
		tblOfContents,
		bib,
		custQuickParts,
		custCoverPg,
		custEq,
		custFtrs,
		custHdrs,
		custPgNum,
		custTbls,
		custWatermarks,
		custAutoTxt,
		custTxtBox,
		custPgNumT,
		custPgNumB,
		custPgNumMargins,
		custTblOfContents,
		custBib,
		custom1,
		custom2,
		custom3,
		custom4,
		custom5
	}
}

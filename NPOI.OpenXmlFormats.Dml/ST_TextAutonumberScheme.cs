using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public enum ST_TextAutonumberScheme
	{
		alphaLcParenBoth,
		alphaUcParenBoth,
		alphaLcParenR,
		alphaUcParenR,
		alphaLcPeriod,
		alphaUcPeriod,
		arabicParenBoth,
		arabicParenR,
		arabicPeriod,
		arabicPlain,
		romanLcParenBoth,
		romanUcParenBoth,
		romanLcParenR,
		romanUcParenR,
		romanLcPeriod,
		romanUcPeriod,
		circleNumDbPlain,
		circleNumWdBlackPlain,
		circleNumWdWhitePlain,
		arabicDbPeriod,
		arabicDbPlain,
		ea1ChsPeriod,
		ea1ChsPlain,
		ea1ChtPeriod,
		ea1ChtPlain,
		ea1JpnChsDbPeriod,
		ea1JpnKorPlain,
		ea1JpnKorPeriod,
		arabic1Minus,
		arabic2Minus,
		hebrew2Minus,
		thaiAlphaPeriod,
		thaiAlphaParenR,
		thaiAlphaParenBoth,
		thaiNumPeriod,
		thaiNumParenR,
		thaiNumParenBoth,
		hindiAlphaPeriod,
		hindiNumPeriod,
		hindiNumParenR,
		hindiAlpha1Period
	}
}

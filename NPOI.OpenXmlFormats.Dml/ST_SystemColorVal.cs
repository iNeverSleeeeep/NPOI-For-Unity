using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public enum ST_SystemColorVal
	{
		scrollBar,
		background,
		activeCaption,
		inactiveCaption,
		menu,
		window,
		windowFrame,
		menuText,
		windowText,
		captionText,
		activeBorder,
		inactiveBorder,
		appWorkspace,
		highlight,
		highlightText,
		btnFace,
		btnShadow,
		grayText,
		btnText,
		inactiveCaptionText,
		btnHighlight,
		[XmlEnum("3dDkShadow")]
		Item3dDkShadow,
		[XmlEnum("3dLight")]
		Item3dLight,
		infoText,
		infoBk,
		hotLight,
		gradientActiveCaption,
		gradientInactiveCaption,
		menuHighlight,
		menuBar
	}
}

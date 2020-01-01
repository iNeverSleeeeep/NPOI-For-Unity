using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IncludeInSchema = false)]
	public enum EG_ColorTransform
	{
		alpha,
		alphaMod,
		alphaOff,
		blue,
		blueMod,
		blueOff,
		comp,
		gamma,
		gray,
		green,
		greenMod,
		greenOff,
		hue,
		hueMod,
		hueOff,
		inv,
		invGamma,
		lum,
		lumMod,
		lumOff,
		red,
		redMod,
		redOff,
		sat,
		satMod,
		satOff,
		shade,
		tint
	}
}

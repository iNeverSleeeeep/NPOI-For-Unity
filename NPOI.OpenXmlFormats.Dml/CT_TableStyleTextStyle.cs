using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_TableStyleTextStyle
	{
		private CT_FontCollection fontField;

		private CT_FontReference fontRefField;

		private CT_ScRgbColor scrgbClrField;

		private CT_SRgbColor srgbClrField;

		private CT_HslColor hslClrField;

		private CT_SystemColor sysClrField;

		private CT_SchemeColor schemeClrField;

		private CT_PresetColor prstClrField;

		private CT_OfficeArtExtensionList extLstField;

		private ST_OnOffStyleType bField;

		private ST_OnOffStyleType iField;

		[XmlElement(Order = 0)]
		public CT_FontCollection font
		{
			get
			{
				return fontField;
			}
			set
			{
				fontField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_FontReference fontRef
		{
			get
			{
				return fontRefField;
			}
			set
			{
				fontRefField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_ScRgbColor scrgbClr
		{
			get
			{
				return scrgbClrField;
			}
			set
			{
				scrgbClrField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_SRgbColor srgbClr
		{
			get
			{
				return srgbClrField;
			}
			set
			{
				srgbClrField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_HslColor hslClr
		{
			get
			{
				return hslClrField;
			}
			set
			{
				hslClrField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_SystemColor sysClr
		{
			get
			{
				return sysClrField;
			}
			set
			{
				sysClrField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_SchemeColor schemeClr
		{
			get
			{
				return schemeClrField;
			}
			set
			{
				schemeClrField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_PresetColor prstClr
		{
			get
			{
				return prstClrField;
			}
			set
			{
				prstClrField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_OfficeArtExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[DefaultValue(ST_OnOffStyleType.def)]
		[XmlAttribute]
		public ST_OnOffStyleType b
		{
			get
			{
				return bField;
			}
			set
			{
				bField = value;
			}
		}

		[DefaultValue(ST_OnOffStyleType.def)]
		[XmlAttribute]
		public ST_OnOffStyleType i
		{
			get
			{
				return iField;
			}
			set
			{
				iField = value;
			}
		}
	}
}

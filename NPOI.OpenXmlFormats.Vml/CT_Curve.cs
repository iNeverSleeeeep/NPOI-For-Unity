using NPOI.OpenXmlFormats.Vml.Presentation;
using NPOI.OpenXmlFormats.Vml.Spreadsheet;
using NPOI.OpenXmlFormats.Vml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public class CT_Curve
	{
		private List<CT_Path> pathField;

		private List<CT_Formulas> formulasField;

		private List<CT_Handles> handlesField;

		private List<CT_Fill> fillField;

		private List<CT_Stroke> strokeField;

		private List<CT_Shadow> shadowField;

		private List<CT_Textbox> textboxField;

		private List<CT_TextPath> textpathField;

		private List<CT_ImageData> imagedataField;

		private List<CT_Wrap> wrapField;

		private List<CT_AnchorLock> anchorlockField;

		private List<CT_Border> bordertopField;

		private List<CT_Border> borderbottomField;

		private List<CT_Border> borderleftField;

		private List<CT_Border> borderrightField;

		private List<CT_ClientData> clientDataField;

		private List<CT_Rel> textdataField;

		private string fromField;

		private string control1Field;

		private string control2Field;

		private string toField;

		[XmlElement("path")]
		public List<CT_Path> path
		{
			get
			{
				return pathField;
			}
			set
			{
				pathField = value;
			}
		}

		[XmlElement("formulas")]
		public List<CT_Formulas> formulas
		{
			get
			{
				return formulasField;
			}
			set
			{
				formulasField = value;
			}
		}

		[XmlElement("handles")]
		public List<CT_Handles> handles
		{
			get
			{
				return handlesField;
			}
			set
			{
				handlesField = value;
			}
		}

		[XmlElement("fill")]
		public List<CT_Fill> fill
		{
			get
			{
				return fillField;
			}
			set
			{
				fillField = value;
			}
		}

		[XmlElement("stroke")]
		public List<CT_Stroke> stroke
		{
			get
			{
				return strokeField;
			}
			set
			{
				strokeField = value;
			}
		}

		[XmlElement("shadow")]
		public List<CT_Shadow> shadow
		{
			get
			{
				return shadowField;
			}
			set
			{
				shadowField = value;
			}
		}

		[XmlElement("textbox")]
		public List<CT_Textbox> textbox
		{
			get
			{
				return textboxField;
			}
			set
			{
				textboxField = value;
			}
		}

		[XmlElement("textpath")]
		public List<CT_TextPath> textpath
		{
			get
			{
				return textpathField;
			}
			set
			{
				textpathField = value;
			}
		}

		[XmlElement("imagedata")]
		public List<CT_ImageData> imagedata
		{
			get
			{
				return imagedataField;
			}
			set
			{
				imagedataField = value;
			}
		}

		[XmlElement("wrap", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_Wrap> wrap
		{
			get
			{
				return wrapField;
			}
			set
			{
				wrapField = value;
			}
		}

		[XmlElement("anchorlock", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_AnchorLock> anchorlock
		{
			get
			{
				return anchorlockField;
			}
			set
			{
				anchorlockField = value;
			}
		}

		[XmlElement("bordertop", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_Border> bordertop
		{
			get
			{
				return bordertopField;
			}
			set
			{
				bordertopField = value;
			}
		}

		[XmlElement("borderbottom", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_Border> borderbottom
		{
			get
			{
				return borderbottomField;
			}
			set
			{
				borderbottomField = value;
			}
		}

		[XmlElement("borderleft", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_Border> borderleft
		{
			get
			{
				return borderleftField;
			}
			set
			{
				borderleftField = value;
			}
		}

		[XmlElement("borderright", Namespace = "urn:schemas-microsoft-com:office:word")]
		public List<CT_Border> borderright
		{
			get
			{
				return borderrightField;
			}
			set
			{
				borderrightField = value;
			}
		}

		[XmlElement("ClientData", Namespace = "urn:schemas-microsoft-com:office:excel")]
		public List<CT_ClientData> ClientData
		{
			get
			{
				return clientDataField;
			}
			set
			{
				clientDataField = value;
			}
		}

		[XmlElement("textdata", Namespace = "urn:schemas-microsoft-com:office:powerpoint")]
		public List<CT_Rel> textdata
		{
			get
			{
				return textdataField;
			}
			set
			{
				textdataField = value;
			}
		}

		[XmlAttribute]
		public string from
		{
			get
			{
				return fromField;
			}
			set
			{
				fromField = value;
			}
		}

		[XmlAttribute]
		public string control1
		{
			get
			{
				return control1Field;
			}
			set
			{
				control1Field = value;
			}
		}

		[XmlAttribute]
		public string control2
		{
			get
			{
				return control2Field;
			}
			set
			{
				control2Field = value;
			}
		}

		[XmlAttribute]
		public string to
		{
			get
			{
				return toField;
			}
			set
			{
				toField = value;
			}
		}
	}
}

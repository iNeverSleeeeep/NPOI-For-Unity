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
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public class CT_Image
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

		private string srcField;

		private string cropleftField;

		private string croptopField;

		private string croprightField;

		private string cropbottomField;

		private string gainField;

		private string blacklevelField;

		private string gammaField;

		private ST_TrueFalse grayscaleField;

		private bool grayscaleFieldSpecified;

		private ST_TrueFalse bilevelField;

		private bool bilevelFieldSpecified;

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
		public string src
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

		[XmlAttribute]
		public string cropleft
		{
			get
			{
				return cropleftField;
			}
			set
			{
				cropleftField = value;
			}
		}

		[XmlAttribute]
		public string croptop
		{
			get
			{
				return croptopField;
			}
			set
			{
				croptopField = value;
			}
		}

		[XmlAttribute]
		public string cropright
		{
			get
			{
				return croprightField;
			}
			set
			{
				croprightField = value;
			}
		}

		[XmlAttribute]
		public string cropbottom
		{
			get
			{
				return cropbottomField;
			}
			set
			{
				cropbottomField = value;
			}
		}

		[XmlAttribute]
		public string gain
		{
			get
			{
				return gainField;
			}
			set
			{
				gainField = value;
			}
		}

		[XmlAttribute]
		public string blacklevel
		{
			get
			{
				return blacklevelField;
			}
			set
			{
				blacklevelField = value;
			}
		}

		[XmlAttribute]
		public string gamma
		{
			get
			{
				return gammaField;
			}
			set
			{
				gammaField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse grayscale
		{
			get
			{
				return grayscaleField;
			}
			set
			{
				grayscaleField = value;
			}
		}

		[XmlIgnore]
		public bool grayscaleSpecified
		{
			get
			{
				return grayscaleFieldSpecified;
			}
			set
			{
				grayscaleFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse bilevel
		{
			get
			{
				return bilevelField;
			}
			set
			{
				bilevelField = value;
			}
		}

		[XmlIgnore]
		public bool bilevelSpecified
		{
			get
			{
				return bilevelFieldSpecified;
			}
			set
			{
				bilevelFieldSpecified = value;
			}
		}
	}
}

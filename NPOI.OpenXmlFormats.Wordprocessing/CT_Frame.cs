using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Frame
	{
		private CT_String szField;

		private CT_String nameField;

		private CT_Rel sourceFileNameField;

		private CT_PixelsMeasure marWField;

		private CT_PixelsMeasure marHField;

		private CT_FrameScrollbar scrollbarField;

		private CT_OnOff noResizeAllowedField;

		private CT_OnOff linkedToFileField;

		[XmlElement(Order = 0)]
		public CT_String sz
		{
			get
			{
				return szField;
			}
			set
			{
				szField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_String name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Rel sourceFileName
		{
			get
			{
				return sourceFileNameField;
			}
			set
			{
				sourceFileNameField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_PixelsMeasure marW
		{
			get
			{
				return marWField;
			}
			set
			{
				marWField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_PixelsMeasure marH
		{
			get
			{
				return marHField;
			}
			set
			{
				marHField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_FrameScrollbar scrollbar
		{
			get
			{
				return scrollbarField;
			}
			set
			{
				scrollbarField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_OnOff noResizeAllowed
		{
			get
			{
				return noResizeAllowedField;
			}
			set
			{
				noResizeAllowedField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_OnOff linkedToFile
		{
			get
			{
				return linkedToFileField;
			}
			set
			{
				linkedToFileField = value;
			}
		}

		public CT_Frame()
		{
			linkedToFileField = new CT_OnOff();
			noResizeAllowedField = new CT_OnOff();
			scrollbarField = new CT_FrameScrollbar();
			marHField = new CT_PixelsMeasure();
			marWField = new CT_PixelsMeasure();
			sourceFileNameField = new CT_Rel();
			nameField = new CT_String();
			szField = new CT_String();
		}
	}
}

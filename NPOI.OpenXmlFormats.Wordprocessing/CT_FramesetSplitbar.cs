using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_FramesetSplitbar
	{
		private CT_TwipsMeasure wField;

		private CT_Color colorField;

		private CT_OnOff noBorderField;

		private CT_OnOff flatBordersField;

		[XmlElement(Order = 0)]
		public CT_TwipsMeasure w
		{
			get
			{
				return wField;
			}
			set
			{
				wField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Color color
		{
			get
			{
				return colorField;
			}
			set
			{
				colorField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_OnOff noBorder
		{
			get
			{
				return noBorderField;
			}
			set
			{
				noBorderField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff flatBorders
		{
			get
			{
				return flatBordersField;
			}
			set
			{
				flatBordersField = value;
			}
		}

		public CT_FramesetSplitbar()
		{
			flatBordersField = new CT_OnOff();
			noBorderField = new CT_OnOff();
			colorField = new CT_Color();
			wField = new CT_TwipsMeasure();
		}
	}
}

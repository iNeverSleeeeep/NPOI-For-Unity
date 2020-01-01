using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_DivBdr
	{
		private CT_Border topField;

		private CT_Border leftField;

		private CT_Border bottomField;

		private CT_Border rightField;

		[XmlElement(Order = 0)]
		public CT_Border top
		{
			get
			{
				return topField;
			}
			set
			{
				topField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Border left
		{
			get
			{
				return leftField;
			}
			set
			{
				leftField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Border bottom
		{
			get
			{
				return bottomField;
			}
			set
			{
				bottomField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Border right
		{
			get
			{
				return rightField;
			}
			set
			{
				rightField = value;
			}
		}

		public CT_DivBdr()
		{
			rightField = new CT_Border();
			bottomField = new CT_Border();
			leftField = new CT_Border();
			topField = new CT_Border();
		}
	}
}

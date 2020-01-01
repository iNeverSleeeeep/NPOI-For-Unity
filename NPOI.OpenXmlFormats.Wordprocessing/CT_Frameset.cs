using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Frameset
	{
		private CT_String szField;

		private CT_FramesetSplitbar framesetSplitbarField;

		private CT_FrameLayout frameLayoutField;

		private object[] itemsField;

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
		public CT_FramesetSplitbar framesetSplitbar
		{
			get
			{
				return framesetSplitbarField;
			}
			set
			{
				framesetSplitbarField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_FrameLayout frameLayout
		{
			get
			{
				return frameLayoutField;
			}
			set
			{
				frameLayoutField = value;
			}
		}

		[XmlElement("frameset", typeof(CT_Frameset), Order = 3)]
		[XmlElement("frame", typeof(CT_Frame), Order = 3)]
		public object[] Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		public CT_Frameset()
		{
			itemsField = new object[0];
			frameLayoutField = new CT_FrameLayout();
			framesetSplitbarField = new CT_FramesetSplitbar();
			szField = new CT_String();
		}
	}
}

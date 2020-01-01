using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_RPR
	{
		private CT_OnOff litField;

		private object[] itemsField;

		private CT_ManualBreak brkField;

		private CT_OnOff alnField;

		[XmlElement(Order = 0)]
		public CT_OnOff lit
		{
			get
			{
				return litField;
			}
			set
			{
				litField = value;
			}
		}

		[XmlElement("nor", typeof(CT_OnOff), Order = 1)]
		[XmlElement("scr", typeof(CT_Script), Order = 1)]
		[XmlElement("sty", typeof(CT_Style), Order = 1)]
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

		[XmlElement(Order = 2)]
		public CT_ManualBreak brk
		{
			get
			{
				return brkField;
			}
			set
			{
				brkField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff aln
		{
			get
			{
				return alnField;
			}
			set
			{
				alnField = value;
			}
		}

		public CT_RPR()
		{
			alnField = new CT_OnOff();
			brkField = new CT_ManualBreak();
			itemsField = new object[0];
			litField = new CT_OnOff();
		}
	}
}

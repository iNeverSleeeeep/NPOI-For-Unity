using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Div
	{
		private CT_OnOff blockQuoteField;

		private CT_OnOff bodyDivField;

		private CT_SignedTwipsMeasure marLeftField;

		private CT_SignedTwipsMeasure marRightField;

		private CT_SignedTwipsMeasure marTopField;

		private CT_SignedTwipsMeasure marBottomField;

		private CT_DivBdr divBdrField;

		private List<CT_Divs> divsChildField;

		private string idField;

		[XmlElement(Order = 0)]
		public CT_OnOff blockQuote
		{
			get
			{
				return blockQuoteField;
			}
			set
			{
				blockQuoteField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OnOff bodyDiv
		{
			get
			{
				return bodyDivField;
			}
			set
			{
				bodyDivField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_SignedTwipsMeasure marLeft
		{
			get
			{
				return marLeftField;
			}
			set
			{
				marLeftField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_SignedTwipsMeasure marRight
		{
			get
			{
				return marRightField;
			}
			set
			{
				marRightField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_SignedTwipsMeasure marTop
		{
			get
			{
				return marTopField;
			}
			set
			{
				marTopField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_SignedTwipsMeasure marBottom
		{
			get
			{
				return marBottomField;
			}
			set
			{
				marBottomField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_DivBdr divBdr
		{
			get
			{
				return divBdrField;
			}
			set
			{
				divBdrField = value;
			}
		}

		[XmlElement("divsChild", Order = 7)]
		public List<CT_Divs> divsChild
		{
			get
			{
				return divsChildField;
			}
			set
			{
				divsChildField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		public CT_Div()
		{
			divsChildField = new List<CT_Divs>();
			divBdrField = new CT_DivBdr();
			marBottomField = new CT_SignedTwipsMeasure();
			marTopField = new CT_SignedTwipsMeasure();
			marRightField = new CT_SignedTwipsMeasure();
			marLeftField = new CT_SignedTwipsMeasure();
			bodyDivField = new CT_OnOff();
			blockQuoteField = new CT_OnOff();
		}
	}
}

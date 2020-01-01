using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlInclude(typeof(CT_Bookmark))]
	[XmlInclude(typeof(CT_MoveBookmark))]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_BookmarkRange : CT_MarkupRange
	{
		private string colFirstField;

		private string colLastField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string colFirst
		{
			get
			{
				return colFirstField;
			}
			set
			{
				colFirstField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string colLast
		{
			get
			{
				return colLastField;
			}
			set
			{
				colLastField = value;
			}
		}
	}
}

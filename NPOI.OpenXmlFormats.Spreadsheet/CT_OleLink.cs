using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_OleLink
	{
		private CT_OleItem[] oleItemsField;

		private string idField;

		private string progIdField;

		[XmlArrayItem("oleItem", IsNullable = false)]
		public CT_OleItem[] oleItems
		{
			get
			{
				return oleItemsField;
			}
			set
			{
				oleItemsField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
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

		[XmlAttribute]
		public string progId
		{
			get
			{
				return progIdField;
			}
			set
			{
				progIdField = value;
			}
		}
	}
}

using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionHeader
	{
		private CT_SheetIdMap sheetIdMapField;

		private CT_ReviewedRevisions reviewedListField;

		private CT_ExtensionList extLstField;

		private string guidField;

		private DateTime dateTimeField;

		private uint maxSheetIdField;

		private string userNameField;

		private string idField;

		private uint minRIdField;

		private bool minRIdFieldSpecified;

		private uint maxRIdField;

		private bool maxRIdFieldSpecified;

		public CT_SheetIdMap sheetIdMap
		{
			get
			{
				return sheetIdMapField;
			}
			set
			{
				sheetIdMapField = value;
			}
		}

		public CT_ReviewedRevisions reviewedList
		{
			get
			{
				return reviewedListField;
			}
			set
			{
				reviewedListField = value;
			}
		}

		public CT_ExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		public string guid
		{
			get
			{
				return guidField;
			}
			set
			{
				guidField = value;
			}
		}

		public DateTime dateTime
		{
			get
			{
				return dateTimeField;
			}
			set
			{
				dateTimeField = value;
			}
		}

		public uint maxSheetId
		{
			get
			{
				return maxSheetIdField;
			}
			set
			{
				maxSheetIdField = value;
			}
		}

		public string userName
		{
			get
			{
				return userNameField;
			}
			set
			{
				userNameField = value;
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

		public uint minRId
		{
			get
			{
				return minRIdField;
			}
			set
			{
				minRIdField = value;
			}
		}

		[XmlIgnore]
		public bool minRIdSpecified
		{
			get
			{
				return minRIdFieldSpecified;
			}
			set
			{
				minRIdFieldSpecified = value;
			}
		}

		public uint maxRId
		{
			get
			{
				return maxRIdField;
			}
			set
			{
				maxRIdField = value;
			}
		}

		[XmlIgnore]
		public bool maxRIdSpecified
		{
			get
			{
				return maxRIdFieldSpecified;
			}
			set
			{
				maxRIdFieldSpecified = value;
			}
		}

		public CT_RevisionHeader()
		{
			extLstField = new CT_ExtensionList();
			reviewedListField = new CT_ReviewedRevisions();
			sheetIdMapField = new CT_SheetIdMap();
		}
	}
}

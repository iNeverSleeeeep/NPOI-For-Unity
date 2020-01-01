using System.ComponentModel;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionSheetRename
	{
		private CT_ExtensionList extLstField;

		private uint rIdField;

		private bool uaField;

		private bool raField;

		private uint sheetIdField;

		private string oldNameField;

		private string newNameField;

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

		public uint rId
		{
			get
			{
				return rIdField;
			}
			set
			{
				rIdField = value;
			}
		}

		[DefaultValue(false)]
		public bool ua
		{
			get
			{
				return uaField;
			}
			set
			{
				uaField = value;
			}
		}

		[DefaultValue(false)]
		public bool ra
		{
			get
			{
				return raField;
			}
			set
			{
				raField = value;
			}
		}

		public uint sheetId
		{
			get
			{
				return sheetIdField;
			}
			set
			{
				sheetIdField = value;
			}
		}

		public string oldName
		{
			get
			{
				return oldNameField;
			}
			set
			{
				oldNameField = value;
			}
		}

		public string newName
		{
			get
			{
				return newNameField;
			}
			set
			{
				newNameField = value;
			}
		}

		public CT_RevisionSheetRename()
		{
			extLstField = new CT_ExtensionList();
			uaField = false;
			raField = false;
		}
	}
}

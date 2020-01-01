using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionDefinedName
	{
		private string formulaField;

		private string oldFormulaField;

		private CT_ExtensionList extLstField;

		private uint rIdField;

		private bool uaField;

		private bool raField;

		private uint localSheetIdField;

		private bool localSheetIdFieldSpecified;

		private bool customViewField;

		private string nameField;

		private bool functionField;

		private bool oldFunctionField;

		private byte functionGroupIdField;

		private bool functionGroupIdFieldSpecified;

		private byte oldFunctionGroupIdField;

		private bool oldFunctionGroupIdFieldSpecified;

		private byte shortcutKeyField;

		private bool shortcutKeyFieldSpecified;

		private byte oldShortcutKeyField;

		private bool oldShortcutKeyFieldSpecified;

		private bool hiddenField;

		private bool oldHiddenField;

		private string customMenuField;

		private string oldCustomMenuField;

		private string descriptionField;

		private string oldDescriptionField;

		private string helpField;

		private string oldHelpField;

		private string statusBarField;

		private string oldStatusBarField;

		private string commentField;

		private string oldCommentField;

		public string formula
		{
			get
			{
				return formulaField;
			}
			set
			{
				formulaField = value;
			}
		}

		public string oldFormula
		{
			get
			{
				return oldFormulaField;
			}
			set
			{
				oldFormulaField = value;
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

		public uint localSheetId
		{
			get
			{
				return localSheetIdField;
			}
			set
			{
				localSheetIdField = value;
			}
		}

		[XmlIgnore]
		public bool localSheetIdSpecified
		{
			get
			{
				return localSheetIdFieldSpecified;
			}
			set
			{
				localSheetIdFieldSpecified = value;
			}
		}

		[DefaultValue(false)]
		public bool customView
		{
			get
			{
				return customViewField;
			}
			set
			{
				customViewField = value;
			}
		}

		public string name
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

		[DefaultValue(false)]
		public bool function
		{
			get
			{
				return functionField;
			}
			set
			{
				functionField = value;
			}
		}

		[DefaultValue(false)]
		public bool oldFunction
		{
			get
			{
				return oldFunctionField;
			}
			set
			{
				oldFunctionField = value;
			}
		}

		public byte functionGroupId
		{
			get
			{
				return functionGroupIdField;
			}
			set
			{
				functionGroupIdField = value;
			}
		}

		[XmlIgnore]
		public bool functionGroupIdSpecified
		{
			get
			{
				return functionGroupIdFieldSpecified;
			}
			set
			{
				functionGroupIdFieldSpecified = value;
			}
		}

		public byte oldFunctionGroupId
		{
			get
			{
				return oldFunctionGroupIdField;
			}
			set
			{
				oldFunctionGroupIdField = value;
			}
		}

		[XmlIgnore]
		public bool oldFunctionGroupIdSpecified
		{
			get
			{
				return oldFunctionGroupIdFieldSpecified;
			}
			set
			{
				oldFunctionGroupIdFieldSpecified = value;
			}
		}

		public byte shortcutKey
		{
			get
			{
				return shortcutKeyField;
			}
			set
			{
				shortcutKeyField = value;
			}
		}

		[XmlIgnore]
		public bool shortcutKeySpecified
		{
			get
			{
				return shortcutKeyFieldSpecified;
			}
			set
			{
				shortcutKeyFieldSpecified = value;
			}
		}

		public byte oldShortcutKey
		{
			get
			{
				return oldShortcutKeyField;
			}
			set
			{
				oldShortcutKeyField = value;
			}
		}

		[XmlIgnore]
		public bool oldShortcutKeySpecified
		{
			get
			{
				return oldShortcutKeyFieldSpecified;
			}
			set
			{
				oldShortcutKeyFieldSpecified = value;
			}
		}

		[DefaultValue(false)]
		public bool hidden
		{
			get
			{
				return hiddenField;
			}
			set
			{
				hiddenField = value;
			}
		}

		[DefaultValue(false)]
		public bool oldHidden
		{
			get
			{
				return oldHiddenField;
			}
			set
			{
				oldHiddenField = value;
			}
		}

		public string customMenu
		{
			get
			{
				return customMenuField;
			}
			set
			{
				customMenuField = value;
			}
		}

		public string oldCustomMenu
		{
			get
			{
				return oldCustomMenuField;
			}
			set
			{
				oldCustomMenuField = value;
			}
		}

		public string description
		{
			get
			{
				return descriptionField;
			}
			set
			{
				descriptionField = value;
			}
		}

		public string oldDescription
		{
			get
			{
				return oldDescriptionField;
			}
			set
			{
				oldDescriptionField = value;
			}
		}

		public string help
		{
			get
			{
				return helpField;
			}
			set
			{
				helpField = value;
			}
		}

		public string oldHelp
		{
			get
			{
				return oldHelpField;
			}
			set
			{
				oldHelpField = value;
			}
		}

		public string statusBar
		{
			get
			{
				return statusBarField;
			}
			set
			{
				statusBarField = value;
			}
		}

		public string oldStatusBar
		{
			get
			{
				return oldStatusBarField;
			}
			set
			{
				oldStatusBarField = value;
			}
		}

		public string comment
		{
			get
			{
				return commentField;
			}
			set
			{
				commentField = value;
			}
		}

		public string oldComment
		{
			get
			{
				return oldCommentField;
			}
			set
			{
				oldCommentField = value;
			}
		}

		public CT_RevisionDefinedName()
		{
			extLstField = new CT_ExtensionList();
			uaField = false;
			raField = false;
			customViewField = false;
			functionField = false;
			oldFunctionField = false;
			hiddenField = false;
			oldHiddenField = false;
		}
	}
}

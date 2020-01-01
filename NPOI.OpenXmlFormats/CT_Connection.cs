using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Connection
	{
		private CT_DbPr dbPrField;

		private CT_OlapPr olapPrField;

		private CT_WebPr webPrField;

		private CT_TextPr textPrField;

		private CT_Parameters parametersField;

		private CT_ExtensionList extLstField;

		private uint idField;

		private string sourceFileField;

		private string odcFileField;

		private bool keepAliveField;

		private uint intervalField;

		private string nameField;

		private string descriptionField;

		private uint typeField;

		private bool typeFieldSpecified;

		private uint reconnectionMethodField;

		private byte refreshedVersionField;

		private byte minRefreshableVersionField;

		private bool savePasswordField;

		private bool newField;

		private bool deletedField;

		private bool onlyUseConnectionFileField;

		private bool backgroundField;

		private bool refreshOnLoadField;

		private bool saveDataField;

		private ST_CredMethod credentialsField;

		private string singleSignOnIdField;

		public CT_DbPr dbPr
		{
			get
			{
				return dbPrField;
			}
			set
			{
				dbPrField = value;
			}
		}

		public CT_OlapPr olapPr
		{
			get
			{
				return olapPrField;
			}
			set
			{
				olapPrField = value;
			}
		}

		public CT_WebPr webPr
		{
			get
			{
				return webPrField;
			}
			set
			{
				webPrField = value;
			}
		}

		public CT_TextPr textPr
		{
			get
			{
				return textPrField;
			}
			set
			{
				textPrField = value;
			}
		}

		public CT_Parameters parameters
		{
			get
			{
				return parametersField;
			}
			set
			{
				parametersField = value;
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

		[XmlAttribute]
		public uint id
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
		public string sourceFile
		{
			get
			{
				return sourceFileField;
			}
			set
			{
				sourceFileField = value;
			}
		}

		[XmlAttribute]
		public string odcFile
		{
			get
			{
				return odcFileField;
			}
			set
			{
				odcFileField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool keepAlive
		{
			get
			{
				return keepAliveField;
			}
			set
			{
				keepAliveField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint interval
		{
			get
			{
				return intervalField;
			}
			set
			{
				intervalField = value;
			}
		}

		[XmlAttribute]
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

		[XmlAttribute]
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

		[XmlAttribute]
		public uint type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlIgnore]
		public bool typeSpecified
		{
			get
			{
				return typeFieldSpecified;
			}
			set
			{
				typeFieldSpecified = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "1")]
		public uint reconnectionMethod
		{
			get
			{
				return reconnectionMethodField;
			}
			set
			{
				reconnectionMethodField = value;
			}
		}

		[XmlAttribute]
		public byte refreshedVersion
		{
			get
			{
				return refreshedVersionField;
			}
			set
			{
				refreshedVersionField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(byte), "0")]
		public byte minRefreshableVersion
		{
			get
			{
				return minRefreshableVersionField;
			}
			set
			{
				minRefreshableVersionField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool savePassword
		{
			get
			{
				return savePasswordField;
			}
			set
			{
				savePasswordField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool @new
		{
			get
			{
				return newField;
			}
			set
			{
				newField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool deleted
		{
			get
			{
				return deletedField;
			}
			set
			{
				deletedField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool onlyUseConnectionFile
		{
			get
			{
				return onlyUseConnectionFileField;
			}
			set
			{
				onlyUseConnectionFileField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool background
		{
			get
			{
				return backgroundField;
			}
			set
			{
				backgroundField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool refreshOnLoad
		{
			get
			{
				return refreshOnLoadField;
			}
			set
			{
				refreshOnLoadField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool saveData
		{
			get
			{
				return saveDataField;
			}
			set
			{
				saveDataField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_CredMethod.integrated)]
		public ST_CredMethod credentials
		{
			get
			{
				return credentialsField;
			}
			set
			{
				credentialsField = value;
			}
		}

		[XmlAttribute]
		public string singleSignOnId
		{
			get
			{
				return singleSignOnIdField;
			}
			set
			{
				singleSignOnIdField = value;
			}
		}

		public CT_Connection()
		{
			keepAliveField = false;
			intervalField = 0u;
			reconnectionMethodField = 1u;
			minRefreshableVersionField = 0;
			savePasswordField = false;
			newField = false;
			deletedField = false;
			onlyUseConnectionFileField = false;
			backgroundField = false;
			refreshOnLoadField = false;
			saveDataField = false;
			credentialsField = ST_CredMethod.integrated;
		}
	}
}

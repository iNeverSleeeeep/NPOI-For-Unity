using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_TextPr
	{
		private CT_TextFields textFieldsField;

		private bool promptField;

		private ST_FileType fileTypeField;

		private uint codePageField;

		private uint firstRowField;

		private string sourceFileField;

		private bool delimitedField;

		private string decimalField;

		private string thousandsField;

		private bool tabField;

		private bool spaceField;

		private bool commaField;

		private bool semicolonField;

		private bool consecutiveField;

		private ST_Qualifier qualifierField;

		private string delimiterField;

		public CT_TextFields textFields
		{
			get
			{
				return textFieldsField;
			}
			set
			{
				textFieldsField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool prompt
		{
			get
			{
				return promptField;
			}
			set
			{
				promptField = value;
			}
		}

		[DefaultValue(ST_FileType.win)]
		[XmlAttribute]
		public ST_FileType fileType
		{
			get
			{
				return fileTypeField;
			}
			set
			{
				fileTypeField = value;
			}
		}

		[DefaultValue(typeof(uint), "1252")]
		[XmlAttribute]
		public uint codePage
		{
			get
			{
				return codePageField;
			}
			set
			{
				codePageField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "1")]
		public uint firstRow
		{
			get
			{
				return firstRowField;
			}
			set
			{
				firstRowField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
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

		[DefaultValue(true)]
		[XmlAttribute]
		public bool delimited
		{
			get
			{
				return delimitedField;
			}
			set
			{
				delimitedField = value;
			}
		}

		[DefaultValue(".")]
		[XmlAttribute]
		public string @decimal
		{
			get
			{
				return decimalField;
			}
			set
			{
				decimalField = value;
			}
		}

		[DefaultValue(",")]
		[XmlAttribute]
		public string thousands
		{
			get
			{
				return thousandsField;
			}
			set
			{
				thousandsField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool tab
		{
			get
			{
				return tabField;
			}
			set
			{
				tabField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool space
		{
			get
			{
				return spaceField;
			}
			set
			{
				spaceField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool comma
		{
			get
			{
				return commaField;
			}
			set
			{
				commaField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool semicolon
		{
			get
			{
				return semicolonField;
			}
			set
			{
				semicolonField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool consecutive
		{
			get
			{
				return consecutiveField;
			}
			set
			{
				consecutiveField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_Qualifier.doubleQuote)]
		public ST_Qualifier qualifier
		{
			get
			{
				return qualifierField;
			}
			set
			{
				qualifierField = value;
			}
		}

		[XmlAttribute]
		public string delimiter
		{
			get
			{
				return delimiterField;
			}
			set
			{
				delimiterField = value;
			}
		}

		public CT_TextPr()
		{
			promptField = true;
			fileTypeField = ST_FileType.win;
			codePageField = 1252u;
			firstRowField = 1u;
			sourceFileField = "";
			delimitedField = true;
			decimalField = ".";
			thousandsField = ",";
			tabField = true;
			spaceField = false;
			commaField = false;
			semicolonField = false;
			consecutiveField = false;
			qualifierField = ST_Qualifier.doubleQuote;
		}
	}
}

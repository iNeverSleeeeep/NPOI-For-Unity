using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_QueryTable
	{
		private CT_QueryTableRefresh queryTableRefreshField;

		private CT_ExtensionList extLstField;

		private string nameField;

		private bool headersField;

		private bool rowNumbersField;

		private bool disableRefreshField;

		private bool backgroundRefreshField;

		private bool firstBackgroundRefreshField;

		private bool refreshOnLoadField;

		private ST_GrowShrinkType growShrinkTypeField;

		private bool fillFormulasField;

		private bool removeDataOnSaveField;

		private bool disableEditField;

		private bool preserveFormattingField;

		private bool adjustColumnWidthField;

		private bool intermediateField;

		private uint connectionIdField;

		private uint autoFormatIdField;

		private bool autoFormatIdFieldSpecified;

		private bool applyNumberFormatsField;

		private bool applyNumberFormatsFieldSpecified;

		private bool applyBorderFormatsField;

		private bool applyBorderFormatsFieldSpecified;

		private bool applyFontFormatsField;

		private bool applyFontFormatsFieldSpecified;

		private bool applyPatternFormatsField;

		private bool applyPatternFormatsFieldSpecified;

		private bool applyAlignmentFormatsField;

		private bool applyAlignmentFormatsFieldSpecified;

		private bool applyWidthHeightFormatsField;

		private bool applyWidthHeightFormatsFieldSpecified;

		public CT_QueryTableRefresh queryTableRefresh
		{
			get
			{
				return queryTableRefreshField;
			}
			set
			{
				queryTableRefreshField = value;
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

		[DefaultValue(true)]
		[XmlAttribute]
		public bool headers
		{
			get
			{
				return headersField;
			}
			set
			{
				headersField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool rowNumbers
		{
			get
			{
				return rowNumbersField;
			}
			set
			{
				rowNumbersField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool disableRefresh
		{
			get
			{
				return disableRefreshField;
			}
			set
			{
				disableRefreshField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool backgroundRefresh
		{
			get
			{
				return backgroundRefreshField;
			}
			set
			{
				backgroundRefreshField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool firstBackgroundRefresh
		{
			get
			{
				return firstBackgroundRefreshField;
			}
			set
			{
				firstBackgroundRefreshField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
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

		[XmlAttribute]
		[DefaultValue(ST_GrowShrinkType.insertDelete)]
		public ST_GrowShrinkType growShrinkType
		{
			get
			{
				return growShrinkTypeField;
			}
			set
			{
				growShrinkTypeField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool fillFormulas
		{
			get
			{
				return fillFormulasField;
			}
			set
			{
				fillFormulasField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool removeDataOnSave
		{
			get
			{
				return removeDataOnSaveField;
			}
			set
			{
				removeDataOnSaveField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool disableEdit
		{
			get
			{
				return disableEditField;
			}
			set
			{
				disableEditField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool preserveFormatting
		{
			get
			{
				return preserveFormattingField;
			}
			set
			{
				preserveFormattingField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool adjustColumnWidth
		{
			get
			{
				return adjustColumnWidthField;
			}
			set
			{
				adjustColumnWidthField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool intermediate
		{
			get
			{
				return intermediateField;
			}
			set
			{
				intermediateField = value;
			}
		}

		[XmlAttribute]
		public uint connectionId
		{
			get
			{
				return connectionIdField;
			}
			set
			{
				connectionIdField = value;
			}
		}

		[XmlAttribute]
		public uint autoFormatId
		{
			get
			{
				return autoFormatIdField;
			}
			set
			{
				autoFormatIdField = value;
			}
		}

		[XmlIgnore]
		public bool autoFormatIdSpecified
		{
			get
			{
				return autoFormatIdFieldSpecified;
			}
			set
			{
				autoFormatIdFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool applyNumberFormats
		{
			get
			{
				return applyNumberFormatsField;
			}
			set
			{
				applyNumberFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyNumberFormatsSpecified
		{
			get
			{
				return applyNumberFormatsFieldSpecified;
			}
			set
			{
				applyNumberFormatsFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool applyBorderFormats
		{
			get
			{
				return applyBorderFormatsField;
			}
			set
			{
				applyBorderFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyBorderFormatsSpecified
		{
			get
			{
				return applyBorderFormatsFieldSpecified;
			}
			set
			{
				applyBorderFormatsFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool applyFontFormats
		{
			get
			{
				return applyFontFormatsField;
			}
			set
			{
				applyFontFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyFontFormatsSpecified
		{
			get
			{
				return applyFontFormatsFieldSpecified;
			}
			set
			{
				applyFontFormatsFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool applyPatternFormats
		{
			get
			{
				return applyPatternFormatsField;
			}
			set
			{
				applyPatternFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyPatternFormatsSpecified
		{
			get
			{
				return applyPatternFormatsFieldSpecified;
			}
			set
			{
				applyPatternFormatsFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool applyAlignmentFormats
		{
			get
			{
				return applyAlignmentFormatsField;
			}
			set
			{
				applyAlignmentFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyAlignmentFormatsSpecified
		{
			get
			{
				return applyAlignmentFormatsFieldSpecified;
			}
			set
			{
				applyAlignmentFormatsFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool applyWidthHeightFormats
		{
			get
			{
				return applyWidthHeightFormatsField;
			}
			set
			{
				applyWidthHeightFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyWidthHeightFormatsSpecified
		{
			get
			{
				return applyWidthHeightFormatsFieldSpecified;
			}
			set
			{
				applyWidthHeightFormatsFieldSpecified = value;
			}
		}

		public CT_QueryTable()
		{
			headersField = true;
			rowNumbersField = false;
			disableRefreshField = false;
			backgroundRefreshField = true;
			firstBackgroundRefreshField = false;
			refreshOnLoadField = false;
			growShrinkTypeField = ST_GrowShrinkType.insertDelete;
			fillFormulasField = false;
			removeDataOnSaveField = false;
			disableEditField = false;
			preserveFormattingField = true;
			adjustColumnWidthField = true;
			intermediateField = false;
		}
	}
}

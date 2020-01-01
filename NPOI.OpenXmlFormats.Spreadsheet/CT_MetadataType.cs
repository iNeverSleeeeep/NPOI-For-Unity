using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_MetadataType
	{
		private string nameField;

		private uint minSupportedVersionField;

		private bool ghostRowField;

		private bool ghostColField;

		private bool editField;

		private bool deleteField;

		private bool copyField;

		private bool pasteAllField;

		private bool pasteFormulasField;

		private bool pasteValuesField;

		private bool pasteFormatsField;

		private bool pasteCommentsField;

		private bool pasteDataValidationField;

		private bool pasteBordersField;

		private bool pasteColWidthsField;

		private bool pasteNumberFormatsField;

		private bool mergeField;

		private bool splitFirstField;

		private bool splitAllField;

		private bool rowColShiftField;

		private bool clearAllField;

		private bool clearFormatsField;

		private bool clearContentsField;

		private bool clearCommentsField;

		private bool assignField;

		private bool coerceField;

		private bool adjustField;

		private bool cellMetaField;

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

		public uint minSupportedVersion
		{
			get
			{
				return minSupportedVersionField;
			}
			set
			{
				minSupportedVersionField = value;
			}
		}

		[DefaultValue(false)]
		public bool ghostRow
		{
			get
			{
				return ghostRowField;
			}
			set
			{
				ghostRowField = value;
			}
		}

		[DefaultValue(false)]
		public bool ghostCol
		{
			get
			{
				return ghostColField;
			}
			set
			{
				ghostColField = value;
			}
		}

		[DefaultValue(false)]
		public bool edit
		{
			get
			{
				return editField;
			}
			set
			{
				editField = value;
			}
		}

		[DefaultValue(false)]
		public bool delete
		{
			get
			{
				return deleteField;
			}
			set
			{
				deleteField = value;
			}
		}

		[DefaultValue(false)]
		public bool copy
		{
			get
			{
				return copyField;
			}
			set
			{
				copyField = value;
			}
		}

		[DefaultValue(false)]
		public bool pasteAll
		{
			get
			{
				return pasteAllField;
			}
			set
			{
				pasteAllField = value;
			}
		}

		[DefaultValue(false)]
		public bool pasteFormulas
		{
			get
			{
				return pasteFormulasField;
			}
			set
			{
				pasteFormulasField = value;
			}
		}

		[DefaultValue(false)]
		public bool pasteValues
		{
			get
			{
				return pasteValuesField;
			}
			set
			{
				pasteValuesField = value;
			}
		}

		[DefaultValue(false)]
		public bool pasteFormats
		{
			get
			{
				return pasteFormatsField;
			}
			set
			{
				pasteFormatsField = value;
			}
		}

		[DefaultValue(false)]
		public bool pasteComments
		{
			get
			{
				return pasteCommentsField;
			}
			set
			{
				pasteCommentsField = value;
			}
		}

		[DefaultValue(false)]
		public bool pasteDataValidation
		{
			get
			{
				return pasteDataValidationField;
			}
			set
			{
				pasteDataValidationField = value;
			}
		}

		[DefaultValue(false)]
		public bool pasteBorders
		{
			get
			{
				return pasteBordersField;
			}
			set
			{
				pasteBordersField = value;
			}
		}

		[DefaultValue(false)]
		public bool pasteColWidths
		{
			get
			{
				return pasteColWidthsField;
			}
			set
			{
				pasteColWidthsField = value;
			}
		}

		[DefaultValue(false)]
		public bool pasteNumberFormats
		{
			get
			{
				return pasteNumberFormatsField;
			}
			set
			{
				pasteNumberFormatsField = value;
			}
		}

		[DefaultValue(false)]
		public bool merge
		{
			get
			{
				return mergeField;
			}
			set
			{
				mergeField = value;
			}
		}

		[DefaultValue(false)]
		public bool splitFirst
		{
			get
			{
				return splitFirstField;
			}
			set
			{
				splitFirstField = value;
			}
		}

		[DefaultValue(false)]
		public bool splitAll
		{
			get
			{
				return splitAllField;
			}
			set
			{
				splitAllField = value;
			}
		}

		[DefaultValue(false)]
		public bool rowColShift
		{
			get
			{
				return rowColShiftField;
			}
			set
			{
				rowColShiftField = value;
			}
		}

		[DefaultValue(false)]
		public bool clearAll
		{
			get
			{
				return clearAllField;
			}
			set
			{
				clearAllField = value;
			}
		}

		[DefaultValue(false)]
		public bool clearFormats
		{
			get
			{
				return clearFormatsField;
			}
			set
			{
				clearFormatsField = value;
			}
		}

		[DefaultValue(false)]
		public bool clearContents
		{
			get
			{
				return clearContentsField;
			}
			set
			{
				clearContentsField = value;
			}
		}

		[DefaultValue(false)]
		public bool clearComments
		{
			get
			{
				return clearCommentsField;
			}
			set
			{
				clearCommentsField = value;
			}
		}

		[DefaultValue(false)]
		public bool assign
		{
			get
			{
				return assignField;
			}
			set
			{
				assignField = value;
			}
		}

		[DefaultValue(false)]
		public bool coerce
		{
			get
			{
				return coerceField;
			}
			set
			{
				coerceField = value;
			}
		}

		[DefaultValue(false)]
		public bool adjust
		{
			get
			{
				return adjustField;
			}
			set
			{
				adjustField = value;
			}
		}

		[DefaultValue(false)]
		public bool cellMeta
		{
			get
			{
				return cellMetaField;
			}
			set
			{
				cellMetaField = value;
			}
		}

		public CT_MetadataType()
		{
			ghostRowField = false;
			ghostColField = false;
			editField = false;
			deleteField = false;
			copyField = false;
			pasteAllField = false;
			pasteFormulasField = false;
			pasteValuesField = false;
			pasteFormatsField = false;
			pasteCommentsField = false;
			pasteDataValidationField = false;
			pasteBordersField = false;
			pasteColWidthsField = false;
			pasteNumberFormatsField = false;
			mergeField = false;
			splitFirstField = false;
			splitAllField = false;
			rowColShiftField = false;
			clearAllField = false;
			clearFormatsField = false;
			clearContentsField = false;
			clearCommentsField = false;
			assignField = false;
			coerceField = false;
			adjustField = false;
			cellMetaField = false;
		}
	}
}

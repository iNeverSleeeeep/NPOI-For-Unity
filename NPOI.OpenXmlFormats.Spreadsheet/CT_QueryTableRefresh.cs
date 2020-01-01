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
	public class CT_QueryTableRefresh
	{
		private CT_QueryTableFields queryTableFieldsField;

		private CT_QueryTableDeletedFields queryTableDeletedFieldsField;

		private CT_SortState sortStateField;

		private CT_ExtensionList extLstField;

		private bool preserveSortFilterLayoutField;

		private bool fieldIdWrappedField;

		private bool headersInLastRefreshField;

		private byte minimumVersionField;

		private uint nextIdField;

		private uint unboundColumnsLeftField;

		private uint unboundColumnsRightField;

		public CT_QueryTableFields queryTableFields
		{
			get
			{
				return queryTableFieldsField;
			}
			set
			{
				queryTableFieldsField = value;
			}
		}

		public CT_QueryTableDeletedFields queryTableDeletedFields
		{
			get
			{
				return queryTableDeletedFieldsField;
			}
			set
			{
				queryTableDeletedFieldsField = value;
			}
		}

		public CT_SortState sortState
		{
			get
			{
				return sortStateField;
			}
			set
			{
				sortStateField = value;
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
		[DefaultValue(true)]
		public bool preserveSortFilterLayout
		{
			get
			{
				return preserveSortFilterLayoutField;
			}
			set
			{
				preserveSortFilterLayoutField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool fieldIdWrapped
		{
			get
			{
				return fieldIdWrappedField;
			}
			set
			{
				fieldIdWrappedField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool headersInLastRefresh
		{
			get
			{
				return headersInLastRefreshField;
			}
			set
			{
				headersInLastRefreshField = value;
			}
		}

		[DefaultValue(typeof(byte), "0")]
		[XmlAttribute]
		public byte minimumVersion
		{
			get
			{
				return minimumVersionField;
			}
			set
			{
				minimumVersionField = value;
			}
		}

		[DefaultValue(typeof(uint), "1")]
		[XmlAttribute]
		public uint nextId
		{
			get
			{
				return nextIdField;
			}
			set
			{
				nextIdField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint unboundColumnsLeft
		{
			get
			{
				return unboundColumnsLeftField;
			}
			set
			{
				unboundColumnsLeftField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint unboundColumnsRight
		{
			get
			{
				return unboundColumnsRightField;
			}
			set
			{
				unboundColumnsRightField = value;
			}
		}

		public CT_QueryTableRefresh()
		{
			preserveSortFilterLayoutField = true;
			fieldIdWrappedField = false;
			headersInLastRefreshField = true;
			minimumVersionField = 0;
			nextIdField = 1u;
			unboundColumnsLeftField = 0u;
			unboundColumnsRightField = 0u;
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Metadata
	{
		private CT_MetadataTypes metadataTypesField;

		private CT_MetadataStrings metadataStringsField;

		private CT_MdxMetadata mdxMetadataField;

		private List<CT_FutureMetadata> futureMetadataField;

		private CT_MetadataBlocks cellMetadataField;

		private CT_MetadataBlocks valueMetadataField;

		private CT_ExtensionList extLstField;

		public CT_MetadataTypes metadataTypes
		{
			get
			{
				return metadataTypesField;
			}
			set
			{
				metadataTypesField = value;
			}
		}

		public CT_MetadataStrings metadataStrings
		{
			get
			{
				return metadataStringsField;
			}
			set
			{
				metadataStringsField = value;
			}
		}

		public CT_MdxMetadata mdxMetadata
		{
			get
			{
				return mdxMetadataField;
			}
			set
			{
				mdxMetadataField = value;
			}
		}

		public List<CT_FutureMetadata> futureMetadata
		{
			get
			{
				return futureMetadataField;
			}
			set
			{
				futureMetadataField = value;
			}
		}

		public CT_MetadataBlocks cellMetadata
		{
			get
			{
				return cellMetadataField;
			}
			set
			{
				cellMetadataField = value;
			}
		}

		public CT_MetadataBlocks valueMetadata
		{
			get
			{
				return valueMetadataField;
			}
			set
			{
				valueMetadataField = value;
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

		public CT_Metadata()
		{
			extLstField = new CT_ExtensionList();
			valueMetadataField = new CT_MetadataBlocks();
			cellMetadataField = new CT_MetadataBlocks();
			futureMetadataField = new List<CT_FutureMetadata>();
			mdxMetadataField = new CT_MdxMetadata();
			metadataStringsField = new CT_MetadataStrings();
			metadataTypesField = new CT_MetadataTypes();
		}
	}
}

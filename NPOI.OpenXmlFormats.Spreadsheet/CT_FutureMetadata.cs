using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_FutureMetadata
	{
		private List<CT_FutureMetadataBlock> bkField;

		private CT_ExtensionList extLstField;

		private string nameField;

		private uint countField;

		[XmlElement("bk")]
		public List<CT_FutureMetadataBlock> bk
		{
			get
			{
				return bkField;
			}
			set
			{
				bkField = value;
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

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		public CT_FutureMetadata()
		{
			extLstField = new CT_ExtensionList();
			bkField = new List<CT_FutureMetadataBlock>();
			countField = 0u;
		}
	}
}

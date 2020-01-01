using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_Cxn
	{
		private CT_OfficeArtExtensionList extLstField;

		private string modelIdField;

		private ST_CxnType typeField;

		private string srcIdField;

		private string destIdField;

		private uint srcOrdField;

		private uint destOrdField;

		private string parTransIdField;

		private string sibTransIdField;

		private string presIdField;

		[XmlElement(Order = 0)]
		public CT_OfficeArtExtensionList extLst
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
		public string modelId
		{
			get
			{
				return modelIdField;
			}
			set
			{
				modelIdField = value;
			}
		}

		[DefaultValue(ST_CxnType.parOf)]
		[XmlAttribute]
		public ST_CxnType type
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

		[XmlAttribute]
		public string srcId
		{
			get
			{
				return srcIdField;
			}
			set
			{
				srcIdField = value;
			}
		}

		[XmlAttribute]
		public string destId
		{
			get
			{
				return destIdField;
			}
			set
			{
				destIdField = value;
			}
		}

		[XmlAttribute]
		public uint srcOrd
		{
			get
			{
				return srcOrdField;
			}
			set
			{
				srcOrdField = value;
			}
		}

		[XmlAttribute]
		public uint destOrd
		{
			get
			{
				return destOrdField;
			}
			set
			{
				destOrdField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("0")]
		public string parTransId
		{
			get
			{
				return parTransIdField;
			}
			set
			{
				parTransIdField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("0")]
		public string sibTransId
		{
			get
			{
				return sibTransIdField;
			}
			set
			{
				sibTransIdField = value;
			}
		}

		[DefaultValue("")]
		[XmlAttribute]
		public string presId
		{
			get
			{
				return presIdField;
			}
			set
			{
				presIdField = value;
			}
		}

		public CT_Cxn()
		{
			typeField = ST_CxnType.parOf;
			parTransIdField = "0";
			sibTransIdField = "0";
			presIdField = "";
		}
	}
}

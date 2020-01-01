using NPOI.OpenXmlFormats.Vml.Spreadsheet;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	public class CT_OLEObject
	{
		private ST_OLELinkType linkTypeField;

		private bool linkTypeFieldSpecified;

		private ST_TrueFalseBlank lockedFieldField;

		private bool lockedFieldFieldSpecified;

		private string fieldCodesField;

		private ST_OLEType typeField;

		private bool typeFieldSpecified;

		private string progIDField;

		private string shapeIDField;

		private ST_OLEDrawAspect drawAspectField;

		private bool drawAspectFieldSpecified;

		private string objectIDField;

		private string idField;

		private ST_OLEUpdateMode updateModeField;

		private bool updateModeFieldSpecified;

		public ST_OLELinkType LinkType
		{
			get
			{
				return linkTypeField;
			}
			set
			{
				linkTypeField = value;
			}
		}

		[XmlIgnore]
		public bool LinkTypeSpecified
		{
			get
			{
				return linkTypeFieldSpecified;
			}
			set
			{
				linkTypeFieldSpecified = value;
			}
		}

		public ST_TrueFalseBlank LockedField
		{
			get
			{
				return lockedFieldField;
			}
			set
			{
				lockedFieldField = value;
			}
		}

		[XmlIgnore]
		public bool LockedFieldSpecified
		{
			get
			{
				return lockedFieldFieldSpecified;
			}
			set
			{
				lockedFieldFieldSpecified = value;
			}
		}

		public string FieldCodes
		{
			get
			{
				return fieldCodesField;
			}
			set
			{
				fieldCodesField = value;
			}
		}

		[XmlAttribute]
		public ST_OLEType Type
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
		public bool TypeSpecified
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
		public string ProgID
		{
			get
			{
				return progIDField;
			}
			set
			{
				progIDField = value;
			}
		}

		[XmlAttribute]
		public string ShapeID
		{
			get
			{
				return shapeIDField;
			}
			set
			{
				shapeIDField = value;
			}
		}

		[XmlAttribute]
		public ST_OLEDrawAspect DrawAspect
		{
			get
			{
				return drawAspectField;
			}
			set
			{
				drawAspectField = value;
			}
		}

		[XmlIgnore]
		public bool DrawAspectSpecified
		{
			get
			{
				return drawAspectFieldSpecified;
			}
			set
			{
				drawAspectFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string ObjectID
		{
			get
			{
				return objectIDField;
			}
			set
			{
				objectIDField = value;
			}
		}

		[XmlAttribute]
		public string id
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
		public ST_OLEUpdateMode UpdateMode
		{
			get
			{
				return updateModeField;
			}
			set
			{
				updateModeField = value;
			}
		}

		[XmlIgnore]
		public bool UpdateModeSpecified
		{
			get
			{
				return updateModeFieldSpecified;
			}
			set
			{
				updateModeFieldSpecified = value;
			}
		}
	}
}

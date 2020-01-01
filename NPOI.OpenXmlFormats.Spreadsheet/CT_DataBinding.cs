using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DebuggerStepThrough]
	public class CT_DataBinding
	{
		private XmlElement anyField;

		private string dataBindingNameField;

		private bool? fileBindingField = null;

		private uint? connectionIDField = null;

		private string fileBindingNameField;

		private uint dataBindingLoadModeField;

		[XmlAnyElement]
		public XmlElement Any
		{
			get
			{
				return anyField;
			}
			set
			{
				anyField = value;
			}
		}

		[XmlAttribute]
		public string DataBindingName
		{
			get
			{
				return dataBindingNameField;
			}
			set
			{
				dataBindingNameField = value;
			}
		}

		[XmlIgnore]
		public bool outlineSpecified
		{
			get
			{
				return null != dataBindingNameField;
			}
		}

		[XmlAttribute]
		public bool FileBinding
		{
			get
			{
				if (fileBindingField.HasValue)
				{
					return fileBindingField.Value;
				}
				return false;
			}
			set
			{
				fileBindingField = value;
			}
		}

		[XmlIgnore]
		public bool FileBindingSpecified
		{
			get
			{
				return fileBindingField.HasValue;
			}
		}

		[XmlAttribute]
		public uint ConnectionID
		{
			get
			{
				if (connectionIDField.HasValue)
				{
					return connectionIDField.Value;
				}
				return 0u;
			}
			set
			{
				connectionIDField = value;
			}
		}

		[XmlIgnore]
		public bool ConnectionIDSpecified
		{
			get
			{
				return connectionIDField.HasValue;
			}
		}

		[XmlAttribute]
		public string FileBindingName
		{
			get
			{
				return fileBindingNameField;
			}
			set
			{
				fileBindingNameField = value;
			}
		}

		[XmlIgnore]
		public bool FileBindingNameSpecified
		{
			get
			{
				return null != fileBindingNameField;
			}
		}

		[XmlAttribute]
		public uint DataBindingLoadMode
		{
			get
			{
				return dataBindingLoadModeField;
			}
			set
			{
				dataBindingLoadModeField = value;
			}
		}
	}
}

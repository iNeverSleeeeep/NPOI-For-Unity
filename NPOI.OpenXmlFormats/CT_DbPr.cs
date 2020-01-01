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
	public class CT_DbPr
	{
		private string connectionField;

		private string commandField;

		private string serverCommandField;

		private uint commandTypeField;

		[XmlAttribute]
		public string connection
		{
			get
			{
				return connectionField;
			}
			set
			{
				connectionField = value;
			}
		}

		[XmlAttribute]
		public string command
		{
			get
			{
				return commandField;
			}
			set
			{
				commandField = value;
			}
		}

		[XmlAttribute]
		public string serverCommand
		{
			get
			{
				return serverCommandField;
			}
			set
			{
				serverCommandField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "2")]
		public uint commandType
		{
			get
			{
				return commandTypeField;
			}
			set
			{
				commandTypeField = value;
			}
		}

		public CT_DbPr()
		{
			commandTypeField = 2u;
		}
	}
}

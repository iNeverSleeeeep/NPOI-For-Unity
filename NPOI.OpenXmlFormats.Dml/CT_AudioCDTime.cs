using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_AudioCDTime
	{
		private byte trackField;

		private uint timeField;

		[XmlAttribute]
		public byte track
		{
			get
			{
				return trackField;
			}
			set
			{
				trackField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint time
		{
			get
			{
				return timeField;
			}
			set
			{
				timeField = value;
			}
		}

		public CT_AudioCDTime()
		{
			timeField = 0u;
		}
	}
}

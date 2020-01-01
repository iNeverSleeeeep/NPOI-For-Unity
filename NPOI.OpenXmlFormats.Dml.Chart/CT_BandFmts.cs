using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_BandFmts
	{
		private List<CT_BandFmt> bandFmtField;

		[XmlElement("bandFmt", Order = 0)]
		public List<CT_BandFmt> bandFmt
		{
			get
			{
				return bandFmtField;
			}
			set
			{
				bandFmtField = value;
			}
		}
	}
}

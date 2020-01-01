using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_DispUnits
	{
		private object itemField;

		private CT_DispUnitsLbl dispUnitsLblField;

		private List<CT_Extension> extLstField;

		private CT_BuiltInUnit builtInUnitField;

		private CT_Double custUnitField;

		public CT_BuiltInUnit builtInUnit
		{
			get
			{
				return builtInUnitField;
			}
			set
			{
				builtInUnitField = value;
			}
		}

		[XmlElement(Order = 0)]
		public CT_Double custUnit
		{
			get
			{
				return custUnitField;
			}
			set
			{
				custUnitField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_DispUnitsLbl dispUnitsLbl
		{
			get
			{
				return dispUnitsLblField;
			}
			set
			{
				dispUnitsLblField = value;
			}
		}

		[XmlElement(Order = 2)]
		public List<CT_Extension> extLst
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

		internal static CT_DispUnits Parse(XmlNode childNode, XmlNamespaceManager namespaceManager)
		{
			throw new NotImplementedException();
		}

		internal void Write(StreamWriter sw, string p)
		{
			throw new NotImplementedException();
		}
	}
}

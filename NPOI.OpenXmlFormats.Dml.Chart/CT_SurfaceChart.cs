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
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_SurfaceChart
	{
		private CT_Boolean wireframeField;

		private List<CT_SurfaceSer> serField;

		private List<CT_BandFmt> bandFmtsField;

		private List<CT_UnsignedInt> axIdField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_Boolean wireframe
		{
			get
			{
				return wireframeField;
			}
			set
			{
				wireframeField = value;
			}
		}

		[XmlElement("ser", Order = 1)]
		public List<CT_SurfaceSer> ser
		{
			get
			{
				return serField;
			}
			set
			{
				serField = value;
			}
		}

		[XmlElement(Order = 2)]
		public List<CT_BandFmt> bandFmts
		{
			get
			{
				return bandFmtsField;
			}
			set
			{
				bandFmtsField = value;
			}
		}

		[XmlElement("axId", Order = 3)]
		public List<CT_UnsignedInt> axId
		{
			get
			{
				return axIdField;
			}
			set
			{
				axIdField = value;
			}
		}

		[XmlElement(Order = 4)]
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

		public static CT_SurfaceChart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SurfaceChart cT_SurfaceChart = new CT_SurfaceChart();
			cT_SurfaceChart.ser = new List<CT_SurfaceSer>();
			cT_SurfaceChart.bandFmts = new List<CT_BandFmt>();
			cT_SurfaceChart.axId = new List<CT_UnsignedInt>();
			cT_SurfaceChart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "wireframe")
				{
					cT_SurfaceChart.wireframe = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ser")
				{
					cT_SurfaceChart.ser.Add(CT_SurfaceSer.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "bandFmts")
				{
					cT_SurfaceChart.bandFmts.Add(CT_BandFmt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "axId")
				{
					cT_SurfaceChart.axId.Add(CT_UnsignedInt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_SurfaceChart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_SurfaceChart;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (wireframe != null)
			{
				wireframe.Write(sw, "wireframe");
			}
			if (ser != null)
			{
				foreach (CT_SurfaceSer item in ser)
				{
					item.Write(sw, "ser");
				}
			}
			if (bandFmts != null)
			{
				foreach (CT_BandFmt bandFmt in bandFmts)
				{
					bandFmt.Write(sw, "bandFmts");
				}
			}
			if (axId != null)
			{
				foreach (CT_UnsignedInt item2 in axId)
				{
					item2.Write(sw, "axId");
				}
			}
			if (extLst != null)
			{
				foreach (CT_Extension item3 in extLst)
				{
					item3.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}

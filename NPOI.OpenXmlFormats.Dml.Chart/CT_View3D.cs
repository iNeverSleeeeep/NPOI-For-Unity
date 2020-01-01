using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_View3D
	{
		private CT_RotX rotXField;

		private CT_HPercent hPercentField;

		private CT_RotY rotYField;

		private CT_DepthPercent depthPercentField;

		private CT_Boolean rAngAxField;

		private CT_Perspective perspectiveField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_RotX rotX
		{
			get
			{
				return rotXField;
			}
			set
			{
				rotXField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_HPercent hPercent
		{
			get
			{
				return hPercentField;
			}
			set
			{
				hPercentField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_RotY rotY
		{
			get
			{
				return rotYField;
			}
			set
			{
				rotYField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_DepthPercent depthPercent
		{
			get
			{
				return depthPercentField;
			}
			set
			{
				depthPercentField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_Boolean rAngAx
		{
			get
			{
				return rAngAxField;
			}
			set
			{
				rAngAxField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Perspective perspective
		{
			get
			{
				return perspectiveField;
			}
			set
			{
				perspectiveField = value;
			}
		}

		[XmlElement(Order = 6)]
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

		public static CT_View3D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_View3D cT_View3D = new CT_View3D();
			cT_View3D.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rotX")
				{
					cT_View3D.rotX = CT_RotX.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hPercent")
				{
					cT_View3D.hPercent = CT_HPercent.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rotY")
				{
					cT_View3D.rotY = CT_RotY.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "depthPercent")
				{
					cT_View3D.depthPercent = CT_DepthPercent.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rAngAx")
				{
					cT_View3D.rAngAx = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "perspective")
				{
					cT_View3D.perspective = CT_Perspective.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_View3D.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_View3D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (rotX != null)
			{
				rotX.Write(sw, "rotX");
			}
			if (hPercent != null)
			{
				hPercent.Write(sw, "hPercent");
			}
			if (rotY != null)
			{
				rotY.Write(sw, "rotY");
			}
			if (depthPercent != null)
			{
				depthPercent.Write(sw, "depthPercent");
			}
			if (rAngAx != null)
			{
				rAngAx.Write(sw, "rAngAx");
			}
			if (perspective != null)
			{
				perspective.Write(sw, "perspective");
			}
			if (extLst != null)
			{
				foreach (CT_Extension item in extLst)
				{
					item.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}

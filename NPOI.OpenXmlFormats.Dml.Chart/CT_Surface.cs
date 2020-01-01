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
	public class CT_Surface
	{
		private CT_UnsignedInt thicknessField;

		private CT_ShapeProperties spPrField;

		private CT_PictureOptions pictureOptionsField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_UnsignedInt thickness
		{
			get
			{
				return thicknessField;
			}
			set
			{
				thicknessField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_ShapeProperties spPr
		{
			get
			{
				return spPrField;
			}
			set
			{
				spPrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_PictureOptions pictureOptions
		{
			get
			{
				return pictureOptionsField;
			}
			set
			{
				pictureOptionsField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		public static CT_Surface Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Surface cT_Surface = new CT_Surface();
			cT_Surface.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "thickness")
				{
					cT_Surface.thickness = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_Surface.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pictureOptions")
				{
					cT_Surface.pictureOptions = CT_PictureOptions.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Surface.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_Surface;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (thickness != null)
			{
				thickness.Write(sw, "thickness");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (pictureOptions != null)
			{
				pictureOptions.Write(sw, "pictureOptions");
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

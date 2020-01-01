using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_GradientFillProperties
	{
		private CT_GradientStopList gsLstField;

		private CT_LinearShadeProperties linField;

		private CT_PathShadeProperties pathField;

		private CT_RelativeRect tileRectField;

		private ST_TileFlipMode flipField;

		private bool flipFieldSpecified;

		private bool rotWithShapeField;

		private bool rotWithShapeFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_GradientStopList gsLst
		{
			get
			{
				return gsLstField;
			}
			set
			{
				gsLstField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_LinearShadeProperties lin
		{
			get
			{
				return linField;
			}
			set
			{
				linField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_PathShadeProperties path
		{
			get
			{
				return pathField;
			}
			set
			{
				pathField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_RelativeRect tileRect
		{
			get
			{
				return tileRectField;
			}
			set
			{
				tileRectField = value;
			}
		}

		[XmlAttribute]
		public ST_TileFlipMode flip
		{
			get
			{
				return flipField;
			}
			set
			{
				flipField = value;
			}
		}

		[XmlIgnore]
		public bool flipSpecified
		{
			get
			{
				return flipFieldSpecified;
			}
			set
			{
				flipFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool rotWithShape
		{
			get
			{
				return rotWithShapeField;
			}
			set
			{
				rotWithShapeField = value;
			}
		}

		[XmlIgnore]
		public bool rotWithShapeSpecified
		{
			get
			{
				return rotWithShapeFieldSpecified;
			}
			set
			{
				rotWithShapeFieldSpecified = value;
			}
		}

		public static CT_GradientFillProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GradientFillProperties cT_GradientFillProperties = new CT_GradientFillProperties();
			if (node.Attributes["flip"] != null)
			{
				cT_GradientFillProperties.flip = (ST_TileFlipMode)Enum.Parse(typeof(ST_TileFlipMode), node.Attributes["flip"].Value);
			}
			cT_GradientFillProperties.rotWithShape = XmlHelper.ReadBool(node.Attributes["rotWithShape"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "gsLst")
				{
					cT_GradientFillProperties.gsLst = CT_GradientStopList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lin")
				{
					cT_GradientFillProperties.lin = CT_LinearShadeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "path")
				{
					cT_GradientFillProperties.path = CT_PathShadeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tileRect")
				{
					cT_GradientFillProperties.tileRect = CT_RelativeRect.Parse(childNode, namespaceManager);
				}
			}
			return cT_GradientFillProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "flip", flip.ToString());
			XmlHelper.WriteAttribute(sw, "rotWithShape", rotWithShape);
			sw.Write(">");
			if (gsLst != null)
			{
				gsLst.Write(sw, "gsLst");
			}
			if (lin != null)
			{
				lin.Write(sw, "lin");
			}
			if (path != null)
			{
				path.Write(sw, "path");
			}
			if (tileRect != null)
			{
				tileRect.Write(sw, "tileRect");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}

using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing", IsNullable = true)]
	public class CT_BlipFillProperties
	{
		private CT_Blip blipField;

		private CT_RelativeRect srcRectField;

		private CT_TileInfoProperties tileField;

		private CT_StretchInfoProperties stretchField;

		private uint dpiField;

		private bool dpiFieldSpecified;

		private bool rotWithShapeField;

		private bool rotWithShapeFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_Blip blip
		{
			get
			{
				return blipField;
			}
			set
			{
				blipField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_RelativeRect srcRect
		{
			get
			{
				return srcRectField;
			}
			set
			{
				srcRectField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TileInfoProperties tile
		{
			get
			{
				return tileField;
			}
			set
			{
				tileField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_StretchInfoProperties stretch
		{
			get
			{
				return stretchField;
			}
			set
			{
				stretchField = value;
			}
		}

		[XmlAttribute]
		public uint dpi
		{
			get
			{
				return dpiField;
			}
			set
			{
				dpiField = value;
			}
		}

		[XmlIgnore]
		public bool dpiSpecified
		{
			get
			{
				return dpiFieldSpecified;
			}
			set
			{
				dpiFieldSpecified = value;
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

		public CT_Blip AddNewBlip()
		{
			blipField = new CT_Blip();
			return blipField;
		}

		public CT_StretchInfoProperties AddNewStretch()
		{
			stretchField = new CT_StretchInfoProperties();
			return stretchField;
		}

		public static CT_BlipFillProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BlipFillProperties cT_BlipFillProperties = new CT_BlipFillProperties();
			cT_BlipFillProperties.dpi = XmlHelper.ReadUInt(node.Attributes["dpi"]);
			cT_BlipFillProperties.rotWithShape = XmlHelper.ReadBool(node.Attributes["rotWithShape"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "blip")
				{
					cT_BlipFillProperties.blip = CT_Blip.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "srcRect")
				{
					cT_BlipFillProperties.srcRect = CT_RelativeRect.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tile")
				{
					cT_BlipFillProperties.tile = CT_TileInfoProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "stretch")
				{
					cT_BlipFillProperties.stretch = CT_StretchInfoProperties.Parse(childNode, namespaceManager);
				}
			}
			return cT_BlipFillProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "dpi", dpi);
			if (rotWithShape)
			{
				XmlHelper.WriteAttribute(sw, "rotWithShape", rotWithShape);
			}
			sw.Write(">");
			if (blip != null)
			{
				blip.Write(sw, "blip");
			}
			if (srcRect != null)
			{
				srcRect.Write(sw, "srcRect");
			}
			if (tile != null)
			{
				tile.Write(sw, "tile");
			}
			if (stretch != null)
			{
				stretch.Write(sw, "stretch");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}

		public bool IsSetBlip()
		{
			return blipField != null;
		}
	}
}

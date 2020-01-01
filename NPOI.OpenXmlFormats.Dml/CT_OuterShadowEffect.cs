using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_OuterShadowEffect
	{
		private CT_ScRgbColor scrgbClrField;

		private CT_SRgbColor srgbClrField;

		private CT_HslColor hslClrField;

		private CT_SystemColor sysClrField;

		private CT_SchemeColor schemeClrField;

		private CT_PresetColor prstClrField;

		private long blurRadField;

		private long distField;

		private int dirField;

		private int sxField;

		private int syField;

		private int kxField;

		private int kyField;

		private ST_RectAlignment algnField;

		private bool rotWithShapeField;

		[XmlElement(Order = 0)]
		public CT_ScRgbColor scrgbClr
		{
			get
			{
				return scrgbClrField;
			}
			set
			{
				scrgbClrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_SRgbColor srgbClr
		{
			get
			{
				return srgbClrField;
			}
			set
			{
				srgbClrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_HslColor hslClr
		{
			get
			{
				return hslClrField;
			}
			set
			{
				hslClrField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_SystemColor sysClr
		{
			get
			{
				return sysClrField;
			}
			set
			{
				sysClrField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_SchemeColor schemeClr
		{
			get
			{
				return schemeClrField;
			}
			set
			{
				schemeClrField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_PresetColor prstClr
		{
			get
			{
				return prstClrField;
			}
			set
			{
				prstClrField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(long), "0")]
		public long blurRad
		{
			get
			{
				return blurRadField;
			}
			set
			{
				blurRadField = value;
			}
		}

		[DefaultValue(typeof(long), "0")]
		[XmlAttribute]
		public long dist
		{
			get
			{
				return distField;
			}
			set
			{
				distField = value;
			}
		}

		[DefaultValue(0)]
		[XmlAttribute]
		public int dir
		{
			get
			{
				return dirField;
			}
			set
			{
				dirField = value;
			}
		}

		[DefaultValue(100000)]
		[XmlAttribute]
		public int sx
		{
			get
			{
				return sxField;
			}
			set
			{
				sxField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(100000)]
		public int sy
		{
			get
			{
				return syField;
			}
			set
			{
				syField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0)]
		public int kx
		{
			get
			{
				return kxField;
			}
			set
			{
				kxField = value;
			}
		}

		[DefaultValue(0)]
		[XmlAttribute]
		public int ky
		{
			get
			{
				return kyField;
			}
			set
			{
				kyField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_RectAlignment.b)]
		public ST_RectAlignment algn
		{
			get
			{
				return algnField;
			}
			set
			{
				algnField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
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

		public static CT_OuterShadowEffect Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OuterShadowEffect cT_OuterShadowEffect = new CT_OuterShadowEffect();
			cT_OuterShadowEffect.blurRad = XmlHelper.ReadLong(node.Attributes["blurRad"]);
			cT_OuterShadowEffect.dist = XmlHelper.ReadLong(node.Attributes["dist"]);
			cT_OuterShadowEffect.dir = XmlHelper.ReadInt(node.Attributes["dir"]);
			cT_OuterShadowEffect.sx = XmlHelper.ReadInt(node.Attributes["sx"]);
			cT_OuterShadowEffect.sy = XmlHelper.ReadInt(node.Attributes["sy"]);
			cT_OuterShadowEffect.kx = XmlHelper.ReadInt(node.Attributes["kx"]);
			cT_OuterShadowEffect.ky = XmlHelper.ReadInt(node.Attributes["ky"]);
			if (node.Attributes["algn"] != null)
			{
				cT_OuterShadowEffect.algn = (ST_RectAlignment)Enum.Parse(typeof(ST_RectAlignment), node.Attributes["algn"].Value);
			}
			cT_OuterShadowEffect.rotWithShape = XmlHelper.ReadBool(node.Attributes["rotWithShape"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "scrgbClr")
				{
					cT_OuterShadowEffect.scrgbClr = CT_ScRgbColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "srgbClr")
				{
					cT_OuterShadowEffect.srgbClr = CT_SRgbColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hslClr")
				{
					cT_OuterShadowEffect.hslClr = CT_HslColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sysClr")
				{
					cT_OuterShadowEffect.sysClr = CT_SystemColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "schemeClr")
				{
					cT_OuterShadowEffect.schemeClr = CT_SchemeColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "prstClr")
				{
					cT_OuterShadowEffect.prstClr = CT_PresetColor.Parse(childNode, namespaceManager);
				}
			}
			return cT_OuterShadowEffect;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "blurRad", (double)blurRad);
			XmlHelper.WriteAttribute(sw, "dist", (double)dist);
			XmlHelper.WriteAttribute(sw, "dir", dir);
			XmlHelper.WriteAttribute(sw, "sx", sx);
			XmlHelper.WriteAttribute(sw, "sy", sy);
			XmlHelper.WriteAttribute(sw, "kx", kx);
			XmlHelper.WriteAttribute(sw, "ky", ky);
			XmlHelper.WriteAttribute(sw, "algn", algn.ToString());
			XmlHelper.WriteAttribute(sw, "rotWithShape", rotWithShape);
			sw.Write(">");
			if (scrgbClr != null)
			{
				scrgbClr.Write(sw, "scrgbClr");
			}
			if (srgbClr != null)
			{
				srgbClr.Write(sw, "srgbClr");
			}
			if (hslClr != null)
			{
				hslClr.Write(sw, "hslClr");
			}
			if (sysClr != null)
			{
				sysClr.Write(sw, "sysClr");
			}
			if (schemeClr != null)
			{
				schemeClr.Write(sw, "schemeClr");
			}
			if (prstClr != null)
			{
				prstClr.Write(sw, "prstClr");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}

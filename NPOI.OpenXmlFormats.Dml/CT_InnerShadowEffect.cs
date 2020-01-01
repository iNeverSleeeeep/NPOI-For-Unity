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
	public class CT_InnerShadowEffect
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

		[XmlAttribute]
		[DefaultValue(typeof(long), "0")]
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

		public static CT_InnerShadowEffect Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_InnerShadowEffect cT_InnerShadowEffect = new CT_InnerShadowEffect();
			cT_InnerShadowEffect.blurRad = XmlHelper.ReadLong(node.Attributes["blurRad"]);
			cT_InnerShadowEffect.dist = XmlHelper.ReadLong(node.Attributes["dist"]);
			cT_InnerShadowEffect.dir = XmlHelper.ReadInt(node.Attributes["dir"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "scrgbClr")
				{
					cT_InnerShadowEffect.scrgbClr = CT_ScRgbColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "srgbClr")
				{
					cT_InnerShadowEffect.srgbClr = CT_SRgbColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hslClr")
				{
					cT_InnerShadowEffect.hslClr = CT_HslColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sysClr")
				{
					cT_InnerShadowEffect.sysClr = CT_SystemColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "schemeClr")
				{
					cT_InnerShadowEffect.schemeClr = CT_SchemeColor.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "prstClr")
				{
					cT_InnerShadowEffect.prstClr = CT_PresetColor.Parse(childNode, namespaceManager);
				}
			}
			return cT_InnerShadowEffect;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "blurRad", (double)blurRad);
			XmlHelper.WriteAttribute(sw, "dist", (double)dist);
			XmlHelper.WriteAttribute(sw, "dir", dir);
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

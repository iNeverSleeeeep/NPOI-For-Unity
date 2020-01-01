using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_TileInfoProperties
	{
		private long txField;

		private bool txFieldSpecified;

		private long tyField;

		private bool tyFieldSpecified;

		private int sxField;

		private bool sxFieldSpecified;

		private int syField;

		private bool syFieldSpecified;

		private ST_TileFlipMode flipField;

		private bool flipFieldSpecified;

		private ST_RectAlignment algnField;

		private bool algnFieldSpecified;

		[XmlAttribute]
		public long tx
		{
			get
			{
				return txField;
			}
			set
			{
				txField = value;
			}
		}

		[XmlIgnore]
		public bool txSpecified
		{
			get
			{
				return txFieldSpecified;
			}
			set
			{
				txFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public long ty
		{
			get
			{
				return tyField;
			}
			set
			{
				tyField = value;
			}
		}

		[XmlIgnore]
		public bool tySpecified
		{
			get
			{
				return tyFieldSpecified;
			}
			set
			{
				tyFieldSpecified = value;
			}
		}

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

		[XmlIgnore]
		public bool sxSpecified
		{
			get
			{
				return sxFieldSpecified;
			}
			set
			{
				sxFieldSpecified = value;
			}
		}

		[XmlAttribute]
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

		[XmlIgnore]
		public bool sySpecified
		{
			get
			{
				return syFieldSpecified;
			}
			set
			{
				syFieldSpecified = value;
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

		[XmlIgnore]
		public bool algnSpecified
		{
			get
			{
				return algnFieldSpecified;
			}
			set
			{
				algnFieldSpecified = value;
			}
		}

		public static CT_TileInfoProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TileInfoProperties cT_TileInfoProperties = new CT_TileInfoProperties();
			cT_TileInfoProperties.tx = XmlHelper.ReadLong(node.Attributes["tx"]);
			cT_TileInfoProperties.ty = XmlHelper.ReadLong(node.Attributes["ty"]);
			cT_TileInfoProperties.sx = XmlHelper.ReadInt(node.Attributes["sx"]);
			cT_TileInfoProperties.sy = XmlHelper.ReadInt(node.Attributes["sy"]);
			if (node.Attributes["flip"] != null)
			{
				cT_TileInfoProperties.flip = (ST_TileFlipMode)Enum.Parse(typeof(ST_TileFlipMode), node.Attributes["flip"].Value);
			}
			if (node.Attributes["algn"] != null)
			{
				cT_TileInfoProperties.algn = (ST_RectAlignment)Enum.Parse(typeof(ST_RectAlignment), node.Attributes["algn"].Value);
			}
			return cT_TileInfoProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "tx", (double)tx);
			XmlHelper.WriteAttribute(sw, "ty", (double)ty);
			XmlHelper.WriteAttribute(sw, "sx", sx);
			XmlHelper.WriteAttribute(sw, "sy", sy);
			XmlHelper.WriteAttribute(sw, "flip", flip.ToString());
			XmlHelper.WriteAttribute(sw, "algn", algn.ToString());
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}

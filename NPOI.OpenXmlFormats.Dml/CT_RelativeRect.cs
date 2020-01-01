using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_RelativeRect
	{
		private int? lField;

		private int? tField;

		private int? rField;

		private int? bField;

		[XmlAttribute]
		[DefaultValue(0)]
		public int l
		{
			get
			{
				if (!lField.HasValue)
				{
					return 0;
				}
				return lField.Value;
			}
			set
			{
				lField = value;
			}
		}

		[XmlIgnore]
		public bool lSpecified
		{
			get
			{
				return lField.HasValue;
			}
		}

		[XmlAttribute]
		[DefaultValue(0)]
		public int t
		{
			get
			{
				if (!tField.HasValue)
				{
					return 0;
				}
				return tField.Value;
			}
			set
			{
				tField = value;
			}
		}

		[XmlIgnore]
		public bool tSpecified
		{
			get
			{
				return tField.HasValue;
			}
		}

		[DefaultValue(0)]
		[XmlAttribute]
		public int r
		{
			get
			{
				if (!rField.HasValue)
				{
					return 0;
				}
				return rField.Value;
			}
			set
			{
				rField = value;
			}
		}

		[XmlIgnore]
		public bool rSpecified
		{
			get
			{
				return rField.HasValue;
			}
		}

		[XmlAttribute]
		[DefaultValue(0)]
		public int b
		{
			get
			{
				if (!bField.HasValue)
				{
					return 0;
				}
				return bField.Value;
			}
			set
			{
				bField = value;
			}
		}

		[XmlIgnore]
		public bool bSpecified
		{
			get
			{
				return bField.HasValue;
			}
		}

		public static CT_RelativeRect Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RelativeRect cT_RelativeRect = new CT_RelativeRect();
			cT_RelativeRect.l = XmlHelper.ReadInt(node.Attributes["l"]);
			cT_RelativeRect.t = XmlHelper.ReadInt(node.Attributes["t"]);
			cT_RelativeRect.r = XmlHelper.ReadInt(node.Attributes["r"]);
			cT_RelativeRect.b = XmlHelper.ReadInt(node.Attributes["b"]);
			return cT_RelativeRect;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "l", l);
			XmlHelper.WriteAttribute(sw, "t", t);
			XmlHelper.WriteAttribute(sw, "r", r);
			XmlHelper.WriteAttribute(sw, "b", b);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}

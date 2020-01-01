using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Wordprocessing
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:word")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:word", IsNullable = true)]
	public class CT_Wrap
	{
		private ST_WrapType typeField;

		private bool typeFieldSpecified;

		private ST_WrapSide sideField;

		private bool sideFieldSpecified;

		private ST_HorizontalAnchor anchorxField;

		private bool anchorxFieldSpecified;

		private ST_VerticalAnchor anchoryField;

		private bool anchoryFieldSpecified;

		[XmlAttribute]
		public ST_WrapType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlIgnore]
		public bool typeSpecified
		{
			get
			{
				return typeFieldSpecified;
			}
			set
			{
				typeFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_WrapSide side
		{
			get
			{
				return sideField;
			}
			set
			{
				sideField = value;
			}
		}

		[XmlIgnore]
		public bool sideSpecified
		{
			get
			{
				return sideFieldSpecified;
			}
			set
			{
				sideFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_HorizontalAnchor anchorx
		{
			get
			{
				return anchorxField;
			}
			set
			{
				anchorxField = value;
			}
		}

		[XmlIgnore]
		public bool anchorxSpecified
		{
			get
			{
				return anchorxFieldSpecified;
			}
			set
			{
				anchorxFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_VerticalAnchor anchory
		{
			get
			{
				return anchoryField;
			}
			set
			{
				anchoryField = value;
			}
		}

		[XmlIgnore]
		public bool anchorySpecified
		{
			get
			{
				return anchoryFieldSpecified;
			}
			set
			{
				anchoryFieldSpecified = value;
			}
		}

		public static CT_Wrap Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Wrap cT_Wrap = new CT_Wrap();
			if (node.Attributes["type"] != null)
			{
				cT_Wrap.type = (ST_WrapType)Enum.Parse(typeof(ST_WrapType), node.Attributes["type"].Value);
			}
			if (node.Attributes["side"] != null)
			{
				cT_Wrap.side = (ST_WrapSide)Enum.Parse(typeof(ST_WrapSide), node.Attributes["side"].Value);
			}
			if (node.Attributes["anchorx"] != null)
			{
				cT_Wrap.anchorx = (ST_HorizontalAnchor)Enum.Parse(typeof(ST_HorizontalAnchor), node.Attributes["anchorx"].Value);
			}
			if (node.Attributes["anchory"] != null)
			{
				cT_Wrap.anchory = (ST_VerticalAnchor)Enum.Parse(typeof(ST_VerticalAnchor), node.Attributes["anchory"].Value);
			}
			return cT_Wrap;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			if (type != ST_WrapType.none)
			{
				XmlHelper.WriteAttribute(sw, "type", type.ToString());
			}
			XmlHelper.WriteAttribute(sw, "side", side.ToString());
			XmlHelper.WriteAttribute(sw, "anchorx", anchorx.ToString());
			XmlHelper.WriteAttribute(sw, "anchory", anchory.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}

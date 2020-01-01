using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_EffectContainer
	{
		private List<object> itemsField;

		private ST_EffectContainerType typeField;

		private string nameField;

		[XmlElement("outerShdw", typeof(CT_OuterShadowEffect), Order = 0)]
		[XmlElement("fill", typeof(CT_FillEffect), Order = 0)]
		[XmlElement("lum", typeof(CT_LuminanceEffect), Order = 0)]
		[XmlElement("effect", typeof(CT_EffectReference), Order = 0)]
		[XmlElement("innerShdw", typeof(CT_InnerShadowEffect), Order = 0)]
		[XmlElement("fillOverlay", typeof(CT_FillOverlayEffect), Order = 0)]
		[XmlElement("glow", typeof(CT_GlowEffect), Order = 0)]
		[XmlElement("blur", typeof(CT_BlurEffect), Order = 0)]
		[XmlElement("alphaInv", typeof(CT_AlphaInverseEffect), Order = 0)]
		[XmlElement("alphaModFix", typeof(CT_AlphaModulateFixedEffect), Order = 0)]
		[XmlElement("reflection", typeof(CT_ReflectionEffect), Order = 0)]
		[XmlElement("xfrm", typeof(CT_TransformEffect), Order = 0)]
		[XmlElement("duotone", typeof(CT_DuotoneEffect), Order = 0)]
		[XmlElement("alphaOutset", typeof(CT_AlphaOutsetEffect), Order = 0)]
		[XmlElement("softEdge", typeof(CT_SoftEdgesEffect), Order = 0)]
		[XmlElement("relOff", typeof(CT_RelativeOffsetEffect), Order = 0)]
		[XmlElement("hsl", typeof(CT_HSLEffect), Order = 0)]
		[XmlElement("alphaBiLevel", typeof(CT_AlphaBiLevelEffect), Order = 0)]
		[XmlElement("alphaCeiling", typeof(CT_AlphaCeilingEffect), Order = 0)]
		[XmlElement("alphaFloor", typeof(CT_AlphaFloorEffect), Order = 0)]
		[XmlElement("clrRepl", typeof(CT_ColorReplaceEffect), Order = 0)]
		[XmlElement("cont", typeof(CT_EffectContainer), Order = 0)]
		[XmlElement("alphaRepl", typeof(CT_AlphaReplaceEffect), Order = 0)]
		[XmlElement("clrChange", typeof(CT_ColorChangeEffect), Order = 0)]
		[XmlElement("biLevel", typeof(CT_BiLevelEffect), Order = 0)]
		[XmlElement("grayscl", typeof(CT_GrayscaleEffect), Order = 0)]
		[XmlElement("tint", typeof(CT_TintEffect), Order = 0)]
		[XmlElement("alphaMod", typeof(CT_AlphaModulateEffect), Order = 0)]
		[XmlElement("blend", typeof(CT_BlendEffect), Order = 0)]
		[XmlElement("prstShdw", typeof(CT_PresetShadowEffect), Order = 0)]
		public List<object> Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		[DefaultValue(ST_EffectContainerType.sib)]
		[XmlAttribute]
		public ST_EffectContainerType type
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

		[XmlAttribute(DataType = "token")]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public CT_EffectContainer()
		{
			itemsField = new List<object>();
			typeField = ST_EffectContainerType.sib;
		}

		public static CT_EffectContainer Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_EffectContainer cT_EffectContainer = new CT_EffectContainer();
			if (node.Attributes["type"] != null)
			{
				cT_EffectContainer.type = (ST_EffectContainerType)Enum.Parse(typeof(ST_EffectContainerType), node.Attributes["type"].Value);
			}
			cT_EffectContainer.name = XmlHelper.ReadString(node.Attributes["name"]);
			return cT_EffectContainer;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "type", type.ToString());
			XmlHelper.WriteAttribute(sw, "name", name);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}

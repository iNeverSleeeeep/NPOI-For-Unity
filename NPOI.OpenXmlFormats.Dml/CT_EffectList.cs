using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_EffectList
	{
		private CT_BlurEffect blurField;

		private CT_FillOverlayEffect fillOverlayField;

		private CT_GlowEffect glowField;

		private CT_InnerShadowEffect innerShdwField;

		private CT_OuterShadowEffect outerShdwField;

		private CT_PresetShadowEffect prstShdwField;

		private CT_ReflectionEffect reflectionField;

		private CT_SoftEdgesEffect softEdgeField;

		[XmlElement(Order = 0)]
		public CT_BlurEffect blur
		{
			get
			{
				return blurField;
			}
			set
			{
				blurField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_FillOverlayEffect fillOverlay
		{
			get
			{
				return fillOverlayField;
			}
			set
			{
				fillOverlayField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_GlowEffect glow
		{
			get
			{
				return glowField;
			}
			set
			{
				glowField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_InnerShadowEffect innerShdw
		{
			get
			{
				return innerShdwField;
			}
			set
			{
				innerShdwField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_OuterShadowEffect outerShdw
		{
			get
			{
				return outerShdwField;
			}
			set
			{
				outerShdwField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_PresetShadowEffect prstShdw
		{
			get
			{
				return prstShdwField;
			}
			set
			{
				prstShdwField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_ReflectionEffect reflection
		{
			get
			{
				return reflectionField;
			}
			set
			{
				reflectionField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_SoftEdgesEffect softEdge
		{
			get
			{
				return softEdgeField;
			}
			set
			{
				softEdgeField = value;
			}
		}

		public static CT_EffectList Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_EffectList cT_EffectList = new CT_EffectList();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "blur")
				{
					cT_EffectList.blur = CT_BlurEffect.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fillOverlay")
				{
					cT_EffectList.fillOverlay = CT_FillOverlayEffect.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "glow")
				{
					cT_EffectList.glow = CT_GlowEffect.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "innerShdw")
				{
					cT_EffectList.innerShdw = CT_InnerShadowEffect.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "outerShdw")
				{
					cT_EffectList.outerShdw = CT_OuterShadowEffect.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "prstShdw")
				{
					cT_EffectList.prstShdw = CT_PresetShadowEffect.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "reflection")
				{
					cT_EffectList.reflection = CT_ReflectionEffect.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "softEdge")
				{
					cT_EffectList.softEdge = CT_SoftEdgesEffect.Parse(childNode, namespaceManager);
				}
			}
			return cT_EffectList;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (blur != null)
			{
				blur.Write(sw, "blur");
			}
			if (fillOverlay != null)
			{
				fillOverlay.Write(sw, "fillOverlay");
			}
			if (glow != null)
			{
				glow.Write(sw, "glow");
			}
			if (innerShdw != null)
			{
				innerShdw.Write(sw, "innerShdw");
			}
			if (outerShdw != null)
			{
				outerShdw.Write(sw, "outerShdw");
			}
			if (prstShdw != null)
			{
				prstShdw.Write(sw, "prstShdw");
			}
			if (reflection != null)
			{
				reflection.Write(sw, "reflection");
			}
			if (softEdge != null)
			{
				softEdge.Write(sw, "softEdge");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}

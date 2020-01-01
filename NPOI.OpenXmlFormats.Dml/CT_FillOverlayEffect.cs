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
	public class CT_FillOverlayEffect
	{
		private CT_NoFillProperties noFillField;

		private CT_SolidColorFillProperties solidFillField;

		private CT_GradientFillProperties gradFillField;

		private CT_BlipFillProperties blipFillField;

		private CT_PatternFillProperties pattFillField;

		private CT_GroupFillProperties grpFillField;

		private ST_BlendMode blendField;

		[XmlElement(Order = 0)]
		public CT_NoFillProperties noFill
		{
			get
			{
				return noFillField;
			}
			set
			{
				noFillField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_SolidColorFillProperties solidFill
		{
			get
			{
				return solidFillField;
			}
			set
			{
				solidFillField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_GradientFillProperties gradFill
		{
			get
			{
				return gradFillField;
			}
			set
			{
				gradFillField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_BlipFillProperties blipFill
		{
			get
			{
				return blipFillField;
			}
			set
			{
				blipFillField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_PatternFillProperties pattFill
		{
			get
			{
				return pattFillField;
			}
			set
			{
				pattFillField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_GroupFillProperties grpFill
		{
			get
			{
				return grpFillField;
			}
			set
			{
				grpFillField = value;
			}
		}

		[XmlAttribute]
		public ST_BlendMode blend
		{
			get
			{
				return blendField;
			}
			set
			{
				blendField = value;
			}
		}

		public static CT_FillOverlayEffect Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FillOverlayEffect cT_FillOverlayEffect = new CT_FillOverlayEffect();
			if (node.Attributes["blend"] != null)
			{
				cT_FillOverlayEffect.blend = (ST_BlendMode)Enum.Parse(typeof(ST_BlendMode), node.Attributes["blend"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "noFill")
				{
					cT_FillOverlayEffect.noFill = new CT_NoFillProperties();
				}
				else if (childNode.LocalName == "solidFill")
				{
					cT_FillOverlayEffect.solidFill = CT_SolidColorFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gradFill")
				{
					cT_FillOverlayEffect.gradFill = CT_GradientFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "blipFill")
				{
					cT_FillOverlayEffect.blipFill = CT_BlipFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pattFill")
				{
					cT_FillOverlayEffect.pattFill = CT_PatternFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grpFill")
				{
					cT_FillOverlayEffect.grpFill = new CT_GroupFillProperties();
				}
			}
			return cT_FillOverlayEffect;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "blend", blend.ToString());
			sw.Write(">");
			if (noFill != null)
			{
				sw.Write("<a:noFill/>");
			}
			if (solidFill != null)
			{
				solidFill.Write(sw, "solidFill");
			}
			if (gradFill != null)
			{
				gradFill.Write(sw, "gradFill");
			}
			if (blipFill != null)
			{
				blipFill.Write(sw, "a:blipFill");
			}
			if (pattFill != null)
			{
				pattFill.Write(sw, "pattFill");
			}
			if (grpFill != null)
			{
				sw.Write("<a:grpFill/>");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}

using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_GroupShapeProperties
	{
		private CT_GroupTransform2D xfrmField;

		private CT_NoFillProperties noFillField;

		private CT_SolidColorFillProperties solidFillField;

		private CT_GradientFillProperties gradFillField;

		private CT_BlipFillProperties blipFillField;

		private CT_PatternFillProperties pattFillField;

		private CT_GroupFillProperties grpFillField;

		private CT_EffectList effectLstField;

		private CT_EffectContainer effectDagField;

		private CT_Scene3D scene3dField;

		private CT_OfficeArtExtensionList extLstField;

		private ST_BlackWhiteMode bwModeField;

		private bool bwModeFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_GroupTransform2D xfrm
		{
			get
			{
				return xfrmField;
			}
			set
			{
				xfrmField = value;
			}
		}

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
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

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 5)]
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

		[XmlElement(Order = 6)]
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

		[XmlElement(Order = 7)]
		public CT_EffectList effectLst
		{
			get
			{
				return effectLstField;
			}
			set
			{
				effectLstField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_EffectContainer effectDag
		{
			get
			{
				return effectDagField;
			}
			set
			{
				effectDagField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_Scene3D scene3d
		{
			get
			{
				return scene3dField;
			}
			set
			{
				scene3dField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_OfficeArtExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[XmlAttribute]
		public ST_BlackWhiteMode bwMode
		{
			get
			{
				return bwModeField;
			}
			set
			{
				bwModeField = value;
			}
		}

		[XmlIgnore]
		public bool bwModeSpecified
		{
			get
			{
				return bwModeFieldSpecified;
			}
			set
			{
				bwModeFieldSpecified = value;
			}
		}

		public static CT_GroupShapeProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GroupShapeProperties cT_GroupShapeProperties = new CT_GroupShapeProperties();
			if (node.Attributes["bwMode"] != null)
			{
				cT_GroupShapeProperties.bwMode = (ST_BlackWhiteMode)Enum.Parse(typeof(ST_BlackWhiteMode), node.Attributes["bwMode"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "xfrm")
				{
					cT_GroupShapeProperties.xfrm = CT_GroupTransform2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noFill")
				{
					cT_GroupShapeProperties.noFill = new CT_NoFillProperties();
				}
				else if (childNode.LocalName == "solidFill")
				{
					cT_GroupShapeProperties.solidFill = CT_SolidColorFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gradFill")
				{
					cT_GroupShapeProperties.gradFill = CT_GradientFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "blipFill")
				{
					cT_GroupShapeProperties.blipFill = CT_BlipFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pattFill")
				{
					cT_GroupShapeProperties.pattFill = CT_PatternFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grpFill")
				{
					cT_GroupShapeProperties.grpFill = new CT_GroupFillProperties();
				}
				else if (childNode.LocalName == "effectLst")
				{
					cT_GroupShapeProperties.effectLst = CT_EffectList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "effectDag")
				{
					cT_GroupShapeProperties.effectDag = CT_EffectContainer.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "scene3d")
				{
					cT_GroupShapeProperties.scene3d = CT_Scene3D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_GroupShapeProperties.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_GroupShapeProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			if (bwMode != 0)
			{
				XmlHelper.WriteAttribute(sw, "bwMode", bwMode.ToString());
			}
			sw.Write(">");
			if (xfrm != null)
			{
				xfrm.Write(sw, "xfrm");
			}
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
			if (effectLst != null)
			{
				effectLst.Write(sw, "effectLst");
			}
			if (effectDag != null)
			{
				effectDag.Write(sw, "effectDag");
			}
			if (scene3d != null)
			{
				scene3d.Write(sw, "scene3d");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_GroupTransform2D AddNewXfrm()
		{
			xfrmField = new CT_GroupTransform2D();
			return xfrmField;
		}
	}
}

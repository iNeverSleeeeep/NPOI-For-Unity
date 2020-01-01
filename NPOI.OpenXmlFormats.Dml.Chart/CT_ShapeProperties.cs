using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_ShapeProperties
	{
		private CT_Transform2D xfrmField;

		private CT_CustomGeometry2D custGeomField;

		private CT_PresetGeometry2D prstGeomField;

		private CT_NoFillProperties noFillField;

		private CT_SolidColorFillProperties solidFillField;

		private CT_GradientFillProperties gradFillField;

		private CT_BlipFillProperties blipFillField;

		private CT_PatternFillProperties pattFillField;

		private CT_GroupFillProperties grpFillField;

		private CT_LineProperties lnField;

		private CT_EffectList effectLstField;

		private CT_EffectContainer effectDagField;

		private CT_Scene3D scene3dField;

		private CT_Shape3D sp3dField;

		private CT_OfficeArtExtensionList extLstField;

		private ST_BlackWhiteMode bwModeField;

		[XmlElement(Order = 0)]
		public CT_Transform2D xfrm
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
		public CT_CustomGeometry2D custGeom
		{
			get
			{
				return custGeomField;
			}
			set
			{
				custGeomField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_PresetGeometry2D prstGeom
		{
			get
			{
				return prstGeomField;
			}
			set
			{
				prstGeomField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 5)]
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

		[XmlElement(Order = 6)]
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

		[XmlElement(Order = 7)]
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

		[XmlElement(Order = 8)]
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

		[XmlElement(Order = 9)]
		public CT_LineProperties ln
		{
			get
			{
				return lnField;
			}
			set
			{
				lnField = value;
			}
		}

		[XmlElement(Order = 10)]
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

		[XmlElement(Order = 11)]
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

		[XmlElement(Order = 12)]
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

		[XmlElement(Order = 13)]
		public CT_Shape3D sp3d
		{
			get
			{
				return sp3dField;
			}
			set
			{
				sp3dField = value;
			}
		}

		[XmlElement(Order = 14)]
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
				return ST_BlackWhiteMode.none != bwModeField;
			}
		}

		public CT_PresetGeometry2D AddNewPrstGeom()
		{
			prstGeomField = new CT_PresetGeometry2D();
			return prstGeomField;
		}

		public CT_Transform2D AddNewXfrm()
		{
			xfrmField = new CT_Transform2D();
			return xfrmField;
		}

		public CT_SolidColorFillProperties AddNewSolidFill()
		{
			solidFillField = new CT_SolidColorFillProperties();
			return solidFillField;
		}

		public bool IsSetPattFill()
		{
			return pattFillField != null;
		}

		public bool IsSetSolidFill()
		{
			return solidFillField != null;
		}

		public bool IsSetLn()
		{
			return lnField != null;
		}

		public CT_LineProperties AddNewLn()
		{
			lnField = new CT_LineProperties();
			return lnField;
		}

		public void unsetPattFill()
		{
			pattFill = null;
		}

		public void unsetSolidFill()
		{
			solidFill = null;
		}

		public static CT_ShapeProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ShapeProperties cT_ShapeProperties = new CT_ShapeProperties();
			if (node.Attributes["bwMode"] != null)
			{
				cT_ShapeProperties.bwMode = (ST_BlackWhiteMode)Enum.Parse(typeof(ST_BlackWhiteMode), node.Attributes["bwMode"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "xfrm")
				{
					cT_ShapeProperties.xfrm = CT_Transform2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "custGeom")
				{
					cT_ShapeProperties.custGeom = CT_CustomGeometry2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "prstGeom")
				{
					cT_ShapeProperties.prstGeom = CT_PresetGeometry2D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noFill")
				{
					cT_ShapeProperties.noFill = new CT_NoFillProperties();
				}
				else if (childNode.LocalName == "solidFill")
				{
					cT_ShapeProperties.solidFill = CT_SolidColorFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gradFill")
				{
					cT_ShapeProperties.gradFill = CT_GradientFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "blipFill")
				{
					cT_ShapeProperties.blipFill = CT_BlipFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pattFill")
				{
					cT_ShapeProperties.pattFill = CT_PatternFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grpFill")
				{
					cT_ShapeProperties.grpFill = new CT_GroupFillProperties();
				}
				else if (childNode.LocalName == "ln")
				{
					cT_ShapeProperties.ln = CT_LineProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "effectLst")
				{
					cT_ShapeProperties.effectLst = CT_EffectList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "effectDag")
				{
					cT_ShapeProperties.effectDag = CT_EffectContainer.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "scene3d")
				{
					cT_ShapeProperties.scene3d = CT_Scene3D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sp3d")
				{
					cT_ShapeProperties.sp3d = CT_Shape3D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_ShapeProperties.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_ShapeProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			if (bwMode != 0)
			{
				XmlHelper.WriteAttribute(sw, "bwMode", bwMode.ToString());
			}
			sw.Write(">");
			if (xfrm != null)
			{
				xfrm.Write(sw, "xfrm");
			}
			if (custGeom != null)
			{
				custGeom.Write(sw, "custGeom");
			}
			if (prstGeom != null)
			{
				prstGeom.Write(sw, "prstGeom");
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
			if (ln != null)
			{
				ln.Write(sw, "ln");
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
			if (sp3d != null)
			{
				sp3d.Write(sw, "sp3d");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}

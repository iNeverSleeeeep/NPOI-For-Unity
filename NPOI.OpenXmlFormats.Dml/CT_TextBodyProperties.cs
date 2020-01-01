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
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_TextBodyProperties
	{
		private CT_PresetTextShape prstTxWarpField;

		private CT_TextNoAutofit noAutofitField;

		private CT_TextNormalAutofit normAutofitField;

		private CT_TextShapeAutofit spAutoFitField;

		private CT_Scene3D scene3dField;

		private CT_Shape3D sp3dField;

		private CT_FlatText flatTxField;

		private CT_OfficeArtExtensionList extLstField;

		private int rotField;

		private bool rotFieldSpecified;

		private bool spcFirstLastParaField;

		private bool spcFirstLastParaFieldSpecified;

		private ST_TextVertOverflowType vertOverflowField;

		private bool vertOverflowFieldSpecified;

		private ST_TextHorzOverflowType horzOverflowField;

		private bool horzOverflowFieldSpecified;

		private ST_TextVerticalType vertField;

		private bool vertFieldSpecified;

		private ST_TextWrappingType wrapField;

		private bool wrapFieldSpecified;

		private int lInsField;

		private bool lInsFieldSpecified;

		private int tInsField;

		private bool tInsFieldSpecified;

		private int rInsField;

		private bool rInsFieldSpecified;

		private int bInsField;

		private bool bInsFieldSpecified;

		private int numColField;

		private bool numColFieldSpecified;

		private int spcColField;

		private bool spcColFieldSpecified;

		private bool rtlColField;

		private bool rtlColFieldSpecified;

		private bool fromWordArtField;

		private bool fromWordArtFieldSpecified;

		private ST_TextAnchoringType anchorField;

		private bool anchorFieldSpecified;

		private bool anchorCtrField;

		private bool anchorCtrFieldSpecified;

		private bool forceAAField;

		private bool forceAAFieldSpecified;

		private bool uprightField;

		private bool compatLnSpcField;

		private bool compatLnSpcFieldSpecified;

		public CT_PresetTextShape prstTxWarp
		{
			get
			{
				return prstTxWarpField;
			}
			set
			{
				prstTxWarpField = value;
			}
		}

		public CT_TextNoAutofit noAutofit
		{
			get
			{
				return noAutofitField;
			}
			set
			{
				noAutofitField = value;
			}
		}

		public CT_TextNormalAutofit normAutofit
		{
			get
			{
				return normAutofitField;
			}
			set
			{
				normAutofitField = value;
			}
		}

		public CT_TextShapeAutofit spAutoFit
		{
			get
			{
				return spAutoFitField;
			}
			set
			{
				spAutoFitField = value;
			}
		}

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

		public CT_FlatText flatTx
		{
			get
			{
				return flatTxField;
			}
			set
			{
				flatTxField = value;
			}
		}

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
		public int rot
		{
			get
			{
				return rotField;
			}
			set
			{
				rotField = value;
			}
		}

		[XmlIgnore]
		public bool rotSpecified
		{
			get
			{
				return rotFieldSpecified;
			}
			set
			{
				rotFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool spcFirstLastPara
		{
			get
			{
				return spcFirstLastParaField;
			}
			set
			{
				spcFirstLastParaField = value;
			}
		}

		[XmlIgnore]
		public bool spcFirstLastParaSpecified
		{
			get
			{
				return spcFirstLastParaFieldSpecified;
			}
			set
			{
				spcFirstLastParaFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TextVertOverflowType vertOverflow
		{
			get
			{
				return vertOverflowField;
			}
			set
			{
				vertOverflowField = value;
			}
		}

		[XmlIgnore]
		public bool vertOverflowSpecified
		{
			get
			{
				return vertOverflowFieldSpecified;
			}
			set
			{
				vertOverflowFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TextHorzOverflowType horzOverflow
		{
			get
			{
				return horzOverflowField;
			}
			set
			{
				horzOverflowField = value;
			}
		}

		[XmlIgnore]
		public bool horzOverflowSpecified
		{
			get
			{
				return horzOverflowFieldSpecified;
			}
			set
			{
				horzOverflowFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TextVerticalType vert
		{
			get
			{
				return vertField;
			}
			set
			{
				vertField = value;
			}
		}

		[XmlIgnore]
		public bool vertSpecified
		{
			get
			{
				return vertFieldSpecified;
			}
			set
			{
				vertFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TextWrappingType wrap
		{
			get
			{
				return wrapField;
			}
			set
			{
				wrapField = value;
			}
		}

		[XmlIgnore]
		public bool wrapSpecified
		{
			get
			{
				return wrapFieldSpecified;
			}
			set
			{
				wrapFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int lIns
		{
			get
			{
				return lInsField;
			}
			set
			{
				lInsField = value;
			}
		}

		[XmlIgnore]
		public bool lInsSpecified
		{
			get
			{
				return lInsFieldSpecified;
			}
			set
			{
				lInsFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int tIns
		{
			get
			{
				return tInsField;
			}
			set
			{
				tInsField = value;
			}
		}

		[XmlIgnore]
		public bool tInsSpecified
		{
			get
			{
				return tInsFieldSpecified;
			}
			set
			{
				tInsFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int rIns
		{
			get
			{
				return rInsField;
			}
			set
			{
				rInsField = value;
			}
		}

		[XmlIgnore]
		public bool rInsSpecified
		{
			get
			{
				return rInsFieldSpecified;
			}
			set
			{
				rInsFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int bIns
		{
			get
			{
				return bInsField;
			}
			set
			{
				bInsField = value;
			}
		}

		[XmlIgnore]
		public bool bInsSpecified
		{
			get
			{
				return bInsFieldSpecified;
			}
			set
			{
				bInsFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int numCol
		{
			get
			{
				return numColField;
			}
			set
			{
				numColField = value;
			}
		}

		[XmlIgnore]
		public bool numColSpecified
		{
			get
			{
				return numColFieldSpecified;
			}
			set
			{
				numColFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int spcCol
		{
			get
			{
				return spcColField;
			}
			set
			{
				spcColField = value;
			}
		}

		[XmlIgnore]
		public bool spcColSpecified
		{
			get
			{
				return spcColFieldSpecified;
			}
			set
			{
				spcColFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool rtlCol
		{
			get
			{
				return rtlColField;
			}
			set
			{
				rtlColField = value;
			}
		}

		[XmlIgnore]
		public bool rtlColSpecified
		{
			get
			{
				return rtlColFieldSpecified;
			}
			set
			{
				rtlColFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool fromWordArt
		{
			get
			{
				return fromWordArtField;
			}
			set
			{
				fromWordArtField = value;
			}
		}

		[XmlIgnore]
		public bool fromWordArtSpecified
		{
			get
			{
				return fromWordArtFieldSpecified;
			}
			set
			{
				fromWordArtFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public ST_TextAnchoringType anchor
		{
			get
			{
				return anchorField;
			}
			set
			{
				anchorField = value;
			}
		}

		[XmlIgnore]
		public bool anchorSpecified
		{
			get
			{
				return anchorFieldSpecified;
			}
			set
			{
				anchorFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool anchorCtr
		{
			get
			{
				return anchorCtrField;
			}
			set
			{
				anchorCtrField = value;
			}
		}

		[XmlIgnore]
		public bool anchorCtrSpecified
		{
			get
			{
				return anchorCtrFieldSpecified;
			}
			set
			{
				anchorCtrFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public bool forceAA
		{
			get
			{
				return forceAAField;
			}
			set
			{
				forceAAField = value;
			}
		}

		[XmlIgnore]
		public bool forceAASpecified
		{
			get
			{
				return forceAAFieldSpecified;
			}
			set
			{
				forceAAFieldSpecified = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool upright
		{
			get
			{
				return uprightField;
			}
			set
			{
				uprightField = value;
			}
		}

		[XmlAttribute]
		public bool compatLnSpc
		{
			get
			{
				return compatLnSpcField;
			}
			set
			{
				compatLnSpcField = value;
			}
		}

		[XmlIgnore]
		public bool compatLnSpcSpecified
		{
			get
			{
				return compatLnSpcFieldSpecified;
			}
			set
			{
				compatLnSpcFieldSpecified = value;
			}
		}

		public CT_TextBodyProperties()
		{
			uprightField = false;
			vert = ST_TextVerticalType.horz;
			wrap = ST_TextWrappingType.none;
			spcFirstLastPara = false;
		}

		public static CT_TextBodyProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextBodyProperties cT_TextBodyProperties = new CT_TextBodyProperties();
			cT_TextBodyProperties.rot = XmlHelper.ReadInt(node.Attributes["rot"]);
			cT_TextBodyProperties.spcFirstLastPara = XmlHelper.ReadBool(node.Attributes["spcFirstLastPara"]);
			if (node.Attributes["vertOverflow"] != null)
			{
				cT_TextBodyProperties.vertOverflow = (ST_TextVertOverflowType)Enum.Parse(typeof(ST_TextVertOverflowType), node.Attributes["vertOverflow"].Value);
			}
			if (node.Attributes["horzOverflow"] != null)
			{
				cT_TextBodyProperties.horzOverflow = (ST_TextHorzOverflowType)Enum.Parse(typeof(ST_TextHorzOverflowType), node.Attributes["horzOverflow"].Value);
			}
			if (node.Attributes["vert"] != null)
			{
				cT_TextBodyProperties.vert = (ST_TextVerticalType)Enum.Parse(typeof(ST_TextVerticalType), node.Attributes["vert"].Value);
			}
			if (node.Attributes["wrap"] != null)
			{
				cT_TextBodyProperties.wrap = (ST_TextWrappingType)Enum.Parse(typeof(ST_TextWrappingType), node.Attributes["wrap"].Value);
			}
			cT_TextBodyProperties.lIns = XmlHelper.ReadInt(node.Attributes["lIns"]);
			cT_TextBodyProperties.tIns = XmlHelper.ReadInt(node.Attributes["tIns"]);
			cT_TextBodyProperties.rIns = XmlHelper.ReadInt(node.Attributes["rIns"]);
			cT_TextBodyProperties.bIns = XmlHelper.ReadInt(node.Attributes["bIns"]);
			cT_TextBodyProperties.numCol = XmlHelper.ReadInt(node.Attributes["numCol"]);
			cT_TextBodyProperties.spcCol = XmlHelper.ReadInt(node.Attributes["spcCol"]);
			cT_TextBodyProperties.rtlCol = XmlHelper.ReadBool(node.Attributes["rtlCol"]);
			cT_TextBodyProperties.fromWordArt = XmlHelper.ReadBool(node.Attributes["fromWordArt"]);
			if (node.Attributes["anchor"] != null)
			{
				cT_TextBodyProperties.anchor = (ST_TextAnchoringType)Enum.Parse(typeof(ST_TextAnchoringType), node.Attributes["anchor"].Value);
			}
			cT_TextBodyProperties.anchorCtr = XmlHelper.ReadBool(node.Attributes["anchorCtr"]);
			cT_TextBodyProperties.forceAA = XmlHelper.ReadBool(node.Attributes["forceAA"]);
			cT_TextBodyProperties.upright = XmlHelper.ReadBool(node.Attributes["upright"]);
			cT_TextBodyProperties.compatLnSpc = XmlHelper.ReadBool(node.Attributes["compatLnSpc"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "prstTxWarp")
				{
					cT_TextBodyProperties.prstTxWarp = CT_PresetTextShape.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noAutofit")
				{
					cT_TextBodyProperties.noAutofit = new CT_TextNoAutofit();
				}
				else if (childNode.LocalName == "normAutofit")
				{
					cT_TextBodyProperties.normAutofit = CT_TextNormalAutofit.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spAutoFit")
				{
					cT_TextBodyProperties.spAutoFit = new CT_TextShapeAutofit();
				}
				else if (childNode.LocalName == "scene3d")
				{
					cT_TextBodyProperties.scene3d = CT_Scene3D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sp3d")
				{
					cT_TextBodyProperties.sp3d = CT_Shape3D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "flatTx")
				{
					cT_TextBodyProperties.flatTx = CT_FlatText.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_TextBodyProperties.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_TextBodyProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "rot", rot);
			if (spcFirstLastPara)
			{
				XmlHelper.WriteAttribute(sw, "spcFirstLastPara", spcFirstLastPara);
			}
			XmlHelper.WriteAttribute(sw, "vertOverflow", vertOverflow.ToString());
			XmlHelper.WriteAttribute(sw, "horzOverflow", horzOverflow.ToString());
			if (vert != 0)
			{
				XmlHelper.WriteAttribute(sw, "vert", vert.ToString());
			}
			if (wrap != 0)
			{
				XmlHelper.WriteAttribute(sw, "wrap", wrap.ToString());
			}
			XmlHelper.WriteAttribute(sw, "lIns", lIns, true);
			XmlHelper.WriteAttribute(sw, "tIns", tIns, true);
			XmlHelper.WriteAttribute(sw, "rIns", rIns, true);
			XmlHelper.WriteAttribute(sw, "bIns", bIns, true);
			XmlHelper.WriteAttribute(sw, "numCol", numCol);
			XmlHelper.WriteAttribute(sw, "spcCol", spcCol);
			XmlHelper.WriteAttribute(sw, "rtlCol", rtlCol);
			XmlHelper.WriteAttribute(sw, "fromWordArt", fromWordArt, false);
			XmlHelper.WriteAttribute(sw, "anchor", anchor.ToString());
			XmlHelper.WriteAttribute(sw, "anchorCtr", anchorCtr, false);
			XmlHelper.WriteAttribute(sw, "forceAA", forceAA, false);
			if (upright)
			{
				XmlHelper.WriteAttribute(sw, "upright", upright);
			}
			if (compatLnSpc)
			{
				XmlHelper.WriteAttribute(sw, "compatLnSpc", compatLnSpc);
			}
			sw.Write(">");
			if (prstTxWarp != null)
			{
				prstTxWarp.Write(sw, "prstTxWarp");
			}
			if (noAutofit != null)
			{
				sw.Write("<a:noAutofit/>");
			}
			if (normAutofit != null)
			{
				normAutofit.Write(sw, "normAutofit");
			}
			if (spAutoFit != null)
			{
				sw.Write("<a:spAutoFit/>");
			}
			if (scene3d != null)
			{
				scene3d.Write(sw, "scene3d");
			}
			if (sp3d != null)
			{
				sp3d.Write(sw, "sp3d");
			}
			if (flatTx != null)
			{
				flatTx.Write(sw, "flatTx");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}

using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_Shape3D
	{
		private CT_Bevel bevelTField;

		private CT_Bevel bevelBField;

		private CT_Color extrusionClrField;

		private CT_Color contourClrField;

		private CT_OfficeArtExtensionList extLstField;

		private long zField;

		private long extrusionHField;

		private long contourWField;

		private ST_PresetMaterialType prstMaterialField;

		[XmlElement(Order = 0)]
		public CT_Bevel bevelT
		{
			get
			{
				return bevelTField;
			}
			set
			{
				bevelTField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Bevel bevelB
		{
			get
			{
				return bevelBField;
			}
			set
			{
				bevelBField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Color extrusionClr
		{
			get
			{
				return extrusionClrField;
			}
			set
			{
				extrusionClrField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Color contourClr
		{
			get
			{
				return contourClrField;
			}
			set
			{
				contourClrField = value;
			}
		}

		[XmlElement(Order = 4)]
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

		[DefaultValue(typeof(long), "0")]
		[XmlAttribute]
		public long z
		{
			get
			{
				return zField;
			}
			set
			{
				zField = value;
			}
		}

		[DefaultValue(typeof(long), "0")]
		[XmlAttribute]
		public long extrusionH
		{
			get
			{
				return extrusionHField;
			}
			set
			{
				extrusionHField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(long), "0")]
		public long contourW
		{
			get
			{
				return contourWField;
			}
			set
			{
				contourWField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_PresetMaterialType.warmMatte)]
		public ST_PresetMaterialType prstMaterial
		{
			get
			{
				return prstMaterialField;
			}
			set
			{
				prstMaterialField = value;
			}
		}

		public static CT_Shape3D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Shape3D cT_Shape3D = new CT_Shape3D();
			cT_Shape3D.z = XmlHelper.ReadLong(node.Attributes["z"]);
			cT_Shape3D.extrusionH = XmlHelper.ReadLong(node.Attributes["extrusionH"]);
			cT_Shape3D.contourW = XmlHelper.ReadLong(node.Attributes["contourW"]);
			if (node.Attributes["prstMaterial"] != null)
			{
				cT_Shape3D.prstMaterial = (ST_PresetMaterialType)Enum.Parse(typeof(ST_PresetMaterialType), node.Attributes["prstMaterial"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "bevelT")
				{
					cT_Shape3D.bevelT = CT_Bevel.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bevelB")
				{
					cT_Shape3D.bevelB = CT_Bevel.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extrusionClr")
				{
					cT_Shape3D.extrusionClr = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "contourClr")
				{
					cT_Shape3D.contourClr = CT_Color.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Shape3D.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Shape3D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "z", (double)z);
			XmlHelper.WriteAttribute(sw, "extrusionH", (double)extrusionH);
			XmlHelper.WriteAttribute(sw, "contourW", (double)contourW);
			XmlHelper.WriteAttribute(sw, "prstMaterial", prstMaterial.ToString());
			sw.Write(">");
			if (bevelT != null)
			{
				bevelT.Write(sw, "bevelT");
			}
			if (bevelB != null)
			{
				bevelB.Write(sw, "bevelB");
			}
			if (extrusionClr != null)
			{
				extrusionClr.Write(sw, "extrusionClr");
			}
			if (contourClr != null)
			{
				contourClr.Write(sw, "contourClr");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_Shape3D()
		{
			zField = 0L;
			extrusionHField = 0L;
			contourWField = 0L;
			prstMaterialField = ST_PresetMaterialType.warmMatte;
		}
	}
}

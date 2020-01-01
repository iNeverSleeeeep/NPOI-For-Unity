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
	public class CT_ObjectStyleDefaults
	{
		private CT_DefaultShapeDefinition spDefField;

		private CT_DefaultShapeDefinition lnDefField;

		private CT_DefaultShapeDefinition txDefField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_DefaultShapeDefinition spDef
		{
			get
			{
				return spDefField;
			}
			set
			{
				spDefField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_DefaultShapeDefinition lnDef
		{
			get
			{
				return lnDefField;
			}
			set
			{
				lnDefField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_DefaultShapeDefinition txDef
		{
			get
			{
				return txDefField;
			}
			set
			{
				txDefField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		public static CT_ObjectStyleDefaults Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ObjectStyleDefaults cT_ObjectStyleDefaults = new CT_ObjectStyleDefaults();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "spDef")
				{
					cT_ObjectStyleDefaults.spDef = CT_DefaultShapeDefinition.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lnDef")
				{
					cT_ObjectStyleDefaults.lnDef = CT_DefaultShapeDefinition.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txDef")
				{
					cT_ObjectStyleDefaults.txDef = CT_DefaultShapeDefinition.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_ObjectStyleDefaults.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_ObjectStyleDefaults;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (spDef != null)
			{
				spDef.Write(sw, "spDef");
			}
			if (lnDef != null)
			{
				lnDef.Write(sw, "lnDef");
			}
			if (txDef != null)
			{
				txDef.Write(sw, "txDef");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}

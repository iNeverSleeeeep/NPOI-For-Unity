using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_EffectStyleItem
	{
		private CT_EffectList effectLstField;

		private CT_EffectContainer effectDagField;

		private CT_Scene3D scene3dField;

		private CT_Shape3D sp3dField;

		[XmlElement(Order = 0)]
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

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
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

		[XmlElement(Order = 3)]
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

		public static CT_EffectStyleItem Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_EffectStyleItem cT_EffectStyleItem = new CT_EffectStyleItem();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "effectLst")
				{
					cT_EffectStyleItem.effectLst = CT_EffectList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "effectDag")
				{
					cT_EffectStyleItem.effectDag = CT_EffectContainer.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "scene3d")
				{
					cT_EffectStyleItem.scene3d = CT_Scene3D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sp3d")
				{
					cT_EffectStyleItem.sp3d = CT_Shape3D.Parse(childNode, namespaceManager);
				}
			}
			return cT_EffectStyleItem;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
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
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}

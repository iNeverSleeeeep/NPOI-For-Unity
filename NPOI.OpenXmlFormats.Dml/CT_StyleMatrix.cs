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
	[DesignerCategory("code")]
	public class CT_StyleMatrix
	{
		private CT_FillStyleList fillStyleLstField;

		private List<CT_LineProperties> lnStyleLstField;

		private List<CT_EffectStyleItem> effectStyleLstField;

		private CT_BackgroundFillStyleList bgFillStyleLstField;

		private string nameField;

		[XmlElement(Order = 0)]
		public CT_FillStyleList fillStyleLst
		{
			get
			{
				return fillStyleLstField;
			}
			set
			{
				fillStyleLstField = value;
			}
		}

		[XmlArray(Order = 1)]
		[XmlArrayItem("ln", IsNullable = false)]
		public List<CT_LineProperties> lnStyleLst
		{
			get
			{
				return lnStyleLstField;
			}
			set
			{
				lnStyleLstField = value;
			}
		}

		[XmlArray(Order = 2)]
		[XmlArrayItem("effectStyle", IsNullable = false)]
		public List<CT_EffectStyleItem> effectStyleLst
		{
			get
			{
				return effectStyleLstField;
			}
			set
			{
				effectStyleLstField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_BackgroundFillStyleList bgFillStyleLst
		{
			get
			{
				return bgFillStyleLstField;
			}
			set
			{
				bgFillStyleLstField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
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

		public static CT_StyleMatrix Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_StyleMatrix cT_StyleMatrix = new CT_StyleMatrix();
			cT_StyleMatrix.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_StyleMatrix.lnStyleLst = new List<CT_LineProperties>();
			cT_StyleMatrix.effectStyleLst = new List<CT_EffectStyleItem>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "fillStyleLst")
				{
					cT_StyleMatrix.fillStyleLst = CT_FillStyleList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bgFillStyleLst")
				{
					cT_StyleMatrix.bgFillStyleLst = CT_BackgroundFillStyleList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lnStyleLst")
				{
					cT_StyleMatrix.lnStyleLst.Add(CT_LineProperties.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "effectStyleLst")
				{
					cT_StyleMatrix.effectStyleLst.Add(CT_EffectStyleItem.Parse(childNode, namespaceManager));
				}
			}
			return cT_StyleMatrix;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			sw.Write(">");
			if (fillStyleLst != null)
			{
				fillStyleLst.Write(sw, "fillStyleLst");
			}
			if (bgFillStyleLst != null)
			{
				bgFillStyleLst.Write(sw, "bgFillStyleLst");
			}
			if (lnStyleLst != null)
			{
				foreach (CT_LineProperties item in lnStyleLst)
				{
					item.Write(sw, "lnStyleLst");
				}
			}
			if (effectStyleLst != null)
			{
				foreach (CT_EffectStyleItem item2 in effectStyleLst)
				{
					item2.Write(sw, "effectStyleLst");
				}
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_StyleMatrix()
		{
			nameField = "";
		}
	}
}

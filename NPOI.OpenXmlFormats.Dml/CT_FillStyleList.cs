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
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_FillStyleList
	{
		private List<CT_BlipFillProperties> blipFillField;

		private List<CT_GradientFillProperties> gradFillField;

		private List<CT_GroupFillProperties> grpFillField;

		private List<CT_NoFillProperties> noFillField;

		private List<CT_PatternFillProperties> pattFillField;

		private List<CT_SolidColorFillProperties> solidFillField;

		public List<CT_BlipFillProperties> blipFill
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

		public List<CT_GradientFillProperties> gradFill
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

		public List<CT_GroupFillProperties> grpFill
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

		public List<CT_NoFillProperties> noFill
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

		public List<CT_PatternFillProperties> pattFill
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

		public List<CT_SolidColorFillProperties> solidFill
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

		public static CT_FillStyleList Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FillStyleList cT_FillStyleList = new CT_FillStyleList();
			cT_FillStyleList.blipFill = new List<CT_BlipFillProperties>();
			cT_FillStyleList.gradFill = new List<CT_GradientFillProperties>();
			cT_FillStyleList.grpFill = new List<CT_GroupFillProperties>();
			cT_FillStyleList.noFill = new List<CT_NoFillProperties>();
			cT_FillStyleList.pattFill = new List<CT_PatternFillProperties>();
			cT_FillStyleList.solidFill = new List<CT_SolidColorFillProperties>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "blipFill")
				{
					cT_FillStyleList.blipFill.Add(CT_BlipFillProperties.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "gradFill")
				{
					cT_FillStyleList.gradFill.Add(CT_GradientFillProperties.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "grpFill")
				{
					cT_FillStyleList.grpFill.Add(new CT_GroupFillProperties());
				}
				else if (childNode.LocalName == "noFill")
				{
					cT_FillStyleList.noFill.Add(new CT_NoFillProperties());
				}
				else if (childNode.LocalName == "pattFill")
				{
					cT_FillStyleList.pattFill.Add(CT_PatternFillProperties.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "solidFill")
				{
					cT_FillStyleList.solidFill.Add(CT_SolidColorFillProperties.Parse(childNode, namespaceManager));
				}
			}
			return cT_FillStyleList;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (blipFill != null)
			{
				foreach (CT_BlipFillProperties item in blipFill)
				{
					item.Write(sw, "a:blipFill");
				}
			}
			if (gradFill != null)
			{
				foreach (CT_GradientFillProperties item2 in gradFill)
				{
					item2.Write(sw, "gradFill");
				}
			}
			if (grpFill != null)
			{
				foreach (CT_GroupFillProperties item3 in grpFill)
				{
					CT_GroupFillProperties cT_GroupFillProperty = item3;
					sw.Write("<grpFill/>");
				}
			}
			if (noFill != null)
			{
				foreach (CT_NoFillProperties item4 in noFill)
				{
					CT_NoFillProperties cT_NoFillProperty = item4;
					sw.Write("<noFill/>");
				}
			}
			if (pattFill != null)
			{
				foreach (CT_PatternFillProperties item5 in pattFill)
				{
					item5.Write(sw, "pattFill");
				}
			}
			if (solidFill != null)
			{
				foreach (CT_SolidColorFillProperties item6 in solidFill)
				{
					item6.Write(sw, "solidFill");
				}
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}

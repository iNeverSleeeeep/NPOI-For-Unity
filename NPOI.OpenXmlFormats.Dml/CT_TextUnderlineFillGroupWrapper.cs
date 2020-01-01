using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_TextUnderlineFillGroupWrapper
	{
		private CT_BlipFillProperties blipFillField;

		private CT_GroupFillProperties grpFillField;

		private CT_NoFillProperties noFillField;

		private CT_SolidColorFillProperties solidFillField;

		private CT_GradientFillProperties gradFillField;

		private CT_PatternFillProperties pattFillField;

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

		public static CT_TextUnderlineFillGroupWrapper Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextUnderlineFillGroupWrapper cT_TextUnderlineFillGroupWrapper = new CT_TextUnderlineFillGroupWrapper();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "noFill")
				{
					cT_TextUnderlineFillGroupWrapper.noFill = new CT_NoFillProperties();
				}
				else if (childNode.LocalName == "solidFill")
				{
					cT_TextUnderlineFillGroupWrapper.solidFill = CT_SolidColorFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gradFill")
				{
					cT_TextUnderlineFillGroupWrapper.gradFill = CT_GradientFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "blipFill")
				{
					cT_TextUnderlineFillGroupWrapper.blipFill = CT_BlipFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pattFill")
				{
					cT_TextUnderlineFillGroupWrapper.pattFill = CT_PatternFillProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grpFill")
				{
					cT_TextUnderlineFillGroupWrapper.grpFill = new CT_GroupFillProperties();
				}
			}
			return cT_TextUnderlineFillGroupWrapper;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
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

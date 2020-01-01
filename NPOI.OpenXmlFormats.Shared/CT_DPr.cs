using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_DPr
	{
		private CT_Char begChrField;

		private CT_Char sepChrField;

		private CT_Char endChrField;

		private CT_OnOff growField;

		private CT_Shp shpField;

		private CT_CtrlPr ctrlPrField;

		[XmlElement(Order = 0)]
		public CT_Char begChr
		{
			get
			{
				return begChrField;
			}
			set
			{
				begChrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Char sepChr
		{
			get
			{
				return sepChrField;
			}
			set
			{
				sepChrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Char endChr
		{
			get
			{
				return endChrField;
			}
			set
			{
				endChrField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff grow
		{
			get
			{
				return growField;
			}
			set
			{
				growField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_Shp shp
		{
			get
			{
				return shpField;
			}
			set
			{
				shpField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_CtrlPr ctrlPr
		{
			get
			{
				return ctrlPrField;
			}
			set
			{
				ctrlPrField = value;
			}
		}

		public static CT_DPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DPr cT_DPr = new CT_DPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "begChr")
				{
					cT_DPr.begChr = CT_Char.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sepChr")
				{
					cT_DPr.sepChr = CT_Char.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "endChr")
				{
					cT_DPr.endChr = CT_Char.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grow")
				{
					cT_DPr.grow = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shp")
				{
					cT_DPr.shp = CT_Shp.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_DPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_DPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (begChr != null)
			{
				begChr.Write(sw, "begChr");
			}
			if (sepChr != null)
			{
				sepChr.Write(sw, "sepChr");
			}
			if (endChr != null)
			{
				endChr.Write(sw, "endChr");
			}
			if (grow != null)
			{
				grow.Write(sw, "grow");
			}
			if (shp != null)
			{
				shp.Write(sw, "shp");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}

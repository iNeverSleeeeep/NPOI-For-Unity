using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_PhantPr
	{
		private CT_OnOff showField;

		private CT_OnOff zeroWidField;

		private CT_OnOff zeroAscField;

		private CT_OnOff zeroDescField;

		private CT_OnOff transpField;

		private CT_CtrlPr ctrlPrField;

		[XmlElement(Order = 0)]
		public CT_OnOff show
		{
			get
			{
				return showField;
			}
			set
			{
				showField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OnOff zeroWid
		{
			get
			{
				return zeroWidField;
			}
			set
			{
				zeroWidField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_OnOff zeroAsc
		{
			get
			{
				return zeroAscField;
			}
			set
			{
				zeroAscField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff zeroDesc
		{
			get
			{
				return zeroDescField;
			}
			set
			{
				zeroDescField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_OnOff transp
		{
			get
			{
				return transpField;
			}
			set
			{
				transpField = value;
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

		public static CT_PhantPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PhantPr cT_PhantPr = new CT_PhantPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "show")
				{
					cT_PhantPr.show = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "zeroWid")
				{
					cT_PhantPr.zeroWid = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "zeroAsc")
				{
					cT_PhantPr.zeroAsc = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "zeroDesc")
				{
					cT_PhantPr.zeroDesc = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "transp")
				{
					cT_PhantPr.transp = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ctrlPr")
				{
					cT_PhantPr.ctrlPr = CT_CtrlPr.Parse(childNode, namespaceManager);
				}
			}
			return cT_PhantPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			sw.Write(">");
			if (show != null)
			{
				show.Write(sw, "show");
			}
			if (zeroWid != null)
			{
				zeroWid.Write(sw, "zeroWid");
			}
			if (zeroAsc != null)
			{
				zeroAsc.Write(sw, "zeroAsc");
			}
			if (zeroDesc != null)
			{
				zeroDesc.Write(sw, "zeroDesc");
			}
			if (transp != null)
			{
				transp.Write(sw, "transp");
			}
			if (ctrlPr != null)
			{
				ctrlPr.Write(sw, "ctrlPr");
			}
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}

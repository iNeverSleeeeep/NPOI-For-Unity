using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_EdnDocProps : CT_EdnProps
	{
		private List<CT_FtnEdnSepRef> endnoteField;

		[XmlElement("endnote", Order = 0)]
		public List<CT_FtnEdnSepRef> endnote
		{
			get
			{
				return endnoteField;
			}
			set
			{
				endnoteField = value;
			}
		}

		public new static CT_EdnDocProps Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_EdnDocProps cT_EdnDocProps = new CT_EdnDocProps();
			cT_EdnDocProps.endnote = new List<CT_FtnEdnSepRef>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pos")
				{
					cT_EdnDocProps.pos = CT_EdnPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numFmt")
				{
					cT_EdnDocProps.numFmt = CT_NumFmt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numStart")
				{
					cT_EdnDocProps.numStart = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numRestart")
				{
					cT_EdnDocProps.numRestart = CT_NumRestart.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "endnote")
				{
					cT_EdnDocProps.endnote.Add(CT_FtnEdnSepRef.Parse(childNode, namespaceManager));
				}
			}
			return cT_EdnDocProps;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (base.pos != null)
			{
				base.pos.Write(sw, "pos");
			}
			if (base.numFmt != null)
			{
				base.numFmt.Write(sw, "numFmt");
			}
			if (base.numStart != null)
			{
				base.numStart.Write(sw, "numStart");
			}
			if (base.numRestart != null)
			{
				base.numRestart.Write(sw, "numRestart");
			}
			if (endnote != null)
			{
				foreach (CT_FtnEdnSepRef item in endnote)
				{
					item.Write(sw, "endnote");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}

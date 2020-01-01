using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_FtnDocProps : CT_FtnProps
	{
		private List<CT_FtnEdnSepRef> footnoteField;

		[XmlElement("footnote", Order = 0)]
		public List<CT_FtnEdnSepRef> footnote
		{
			get
			{
				return footnoteField;
			}
			set
			{
				footnoteField = value;
			}
		}

		public new static CT_FtnDocProps Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FtnDocProps cT_FtnDocProps = new CT_FtnDocProps();
			cT_FtnDocProps.footnote = new List<CT_FtnEdnSepRef>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pos")
				{
					cT_FtnDocProps.pos = CT_FtnPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numFmt")
				{
					cT_FtnDocProps.numFmt = CT_NumFmt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numStart")
				{
					cT_FtnDocProps.numStart = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numRestart")
				{
					cT_FtnDocProps.numRestart = CT_NumRestart.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "footnote")
				{
					cT_FtnDocProps.footnote.Add(CT_FtnEdnSepRef.Parse(childNode, namespaceManager));
				}
			}
			return cT_FtnDocProps;
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
			if (footnote != null)
			{
				foreach (CT_FtnEdnSepRef item in footnote)
				{
					item.Write(sw, "footnote");
				}
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}

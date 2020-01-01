using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_NumPr
	{
		private CT_DecimalNumber ilvlField;

		private CT_DecimalNumber numIdField;

		private CT_TrackChangeNumbering numberingChangeField;

		private CT_TrackChange insField;

		[XmlElement(Order = 0)]
		public CT_DecimalNumber ilvl
		{
			get
			{
				return ilvlField;
			}
			set
			{
				ilvlField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_DecimalNumber numId
		{
			get
			{
				return numIdField;
			}
			set
			{
				numIdField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TrackChangeNumbering numberingChange
		{
			get
			{
				return numberingChangeField;
			}
			set
			{
				numberingChangeField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_TrackChange ins
		{
			get
			{
				return insField;
			}
			set
			{
				insField = value;
			}
		}

		public static CT_NumPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NumPr cT_NumPr = new CT_NumPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "ilvl")
				{
					cT_NumPr.ilvl = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numId")
				{
					cT_NumPr.numId = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numberingChange")
				{
					cT_NumPr.numberingChange = CT_TrackChangeNumbering.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ins")
				{
					cT_NumPr.ins = CT_TrackChange.Parse(childNode, namespaceManager);
				}
			}
			return cT_NumPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (ilvl != null)
			{
				ilvl.Write(sw, "ilvl");
			}
			if (numId != null)
			{
				numId.Write(sw, "numId");
			}
			if (numberingChange != null)
			{
				numberingChange.Write(sw, "numberingChange");
			}
			if (ins != null)
			{
				ins.Write(sw, "ins");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_DecimalNumber AddNewNumId()
		{
			numId = new CT_DecimalNumber();
			return numId;
		}
	}
}

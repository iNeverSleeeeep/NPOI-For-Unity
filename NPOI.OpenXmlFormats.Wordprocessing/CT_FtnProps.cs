using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlInclude(typeof(CT_FtnDocProps))]
	public class CT_FtnProps
	{
		private CT_FtnPos posField;

		private CT_NumFmt numFmtField;

		private CT_DecimalNumber numStartField;

		private CT_NumRestart numRestartField;

		[XmlElement(Order = 0)]
		public CT_FtnPos pos
		{
			get
			{
				return posField;
			}
			set
			{
				posField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_NumFmt numFmt
		{
			get
			{
				return numFmtField;
			}
			set
			{
				numFmtField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_DecimalNumber numStart
		{
			get
			{
				return numStartField;
			}
			set
			{
				numStartField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_NumRestart numRestart
		{
			get
			{
				return numRestartField;
			}
			set
			{
				numRestartField = value;
			}
		}

		public static CT_FtnProps Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FtnProps cT_FtnProps = new CT_FtnProps();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pos")
				{
					cT_FtnProps.pos = CT_FtnPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numFmt")
				{
					cT_FtnProps.numFmt = CT_NumFmt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numStart")
				{
					cT_FtnProps.numStart = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numRestart")
				{
					cT_FtnProps.numRestart = CT_NumRestart.Parse(childNode, namespaceManager);
				}
			}
			return cT_FtnProps;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (pos != null)
			{
				pos.Write(sw, "w:pos");
			}
			if (numFmt != null)
			{
				numFmt.Write(sw, "w:numFmt");
			}
			if (numStart != null)
			{
				numStart.Write(sw, "w:numStart");
			}
			if (numRestart != null)
			{
				numRestart.Write(sw, "w:numRestart");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}

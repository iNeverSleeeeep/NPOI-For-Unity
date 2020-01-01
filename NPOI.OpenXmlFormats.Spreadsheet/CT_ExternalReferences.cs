using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_ExternalReferences
	{
		private List<CT_ExternalReference> externalReferenceField;

		[XmlElement]
		public List<CT_ExternalReference> externalReference
		{
			get
			{
				return externalReferenceField;
			}
			set
			{
				externalReferenceField = value;
			}
		}

		public static CT_ExternalReferences Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ExternalReferences cT_ExternalReferences = new CT_ExternalReferences();
			cT_ExternalReferences.externalReference = new List<CT_ExternalReference>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "externalReference")
				{
					cT_ExternalReferences.externalReference.Add(CT_ExternalReference.Parse(childNode, namespaceManager));
				}
			}
			return cT_ExternalReferences;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (externalReference != null)
			{
				foreach (CT_ExternalReference item in externalReference)
				{
					item.Write(sw, "externalReference");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}

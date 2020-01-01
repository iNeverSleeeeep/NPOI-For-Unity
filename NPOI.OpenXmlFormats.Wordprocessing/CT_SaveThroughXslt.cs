using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_SaveThroughXslt
	{
		private string idField;

		private string solutionIDField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string solutionID
		{
			get
			{
				return solutionIDField;
			}
			set
			{
				solutionIDField = value;
			}
		}

		public static CT_SaveThroughXslt Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SaveThroughXslt cT_SaveThroughXslt = new CT_SaveThroughXslt();
			cT_SaveThroughXslt.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			cT_SaveThroughXslt.solutionID = XmlHelper.ReadString(node.Attributes["w:solutionID"]);
			return cT_SaveThroughXslt;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r:id", id);
			XmlHelper.WriteAttribute(sw, "w:solutionID", solutionID);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}

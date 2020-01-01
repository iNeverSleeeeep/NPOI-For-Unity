using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_ExternalData
	{
		private CT_Boolean autoUpdateField;

		private string idField;

		[XmlElement(Order = 0)]
		public CT_Boolean autoUpdate
		{
			get
			{
				return autoUpdateField;
			}
			set
			{
				autoUpdateField = value;
			}
		}

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

		public static CT_ExternalData Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ExternalData cT_ExternalData = new CT_ExternalData();
			cT_ExternalData.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "autoUpdate")
				{
					cT_ExternalData.autoUpdate = CT_Boolean.Parse(childNode, namespaceManager);
				}
			}
			return cT_ExternalData;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write(">");
			if (autoUpdate != null)
			{
				autoUpdate.Write(sw, "autoUpdate");
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}

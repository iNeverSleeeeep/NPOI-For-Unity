using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_ProtectedRange
	{
		private byte[] passwordField;

		private List<string> sqrefField;

		private string nameField;

		private string securityDescriptorField;

		public byte[] password
		{
			get
			{
				return passwordField;
			}
			set
			{
				passwordField = value;
			}
		}

		public List<string> sqref
		{
			get
			{
				return sqrefField;
			}
			set
			{
				sqrefField = value;
			}
		}

		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public string securityDescriptor
		{
			get
			{
				return securityDescriptorField;
			}
			set
			{
				securityDescriptorField = value;
			}
		}

		public static CT_ProtectedRange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ProtectedRange cT_ProtectedRange = new CT_ProtectedRange();
			cT_ProtectedRange.password = XmlHelper.ReadBytes(node.Attributes["password"]);
			cT_ProtectedRange.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_ProtectedRange.securityDescriptor = XmlHelper.ReadString(node.Attributes["securityDescriptor"]);
			cT_ProtectedRange.sqref = new List<string>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sqref")
				{
					cT_ProtectedRange.sqref.Add(childNode.InnerText);
				}
			}
			return cT_ProtectedRange;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "password", password);
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "securityDescriptor", securityDescriptor);
			sw.Write(">");
			if (sqref != null)
			{
				foreach (string item in sqref)
				{
					sw.Write(string.Format("<sqref><![CDATA[{0}]]></sqref>", item));
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}

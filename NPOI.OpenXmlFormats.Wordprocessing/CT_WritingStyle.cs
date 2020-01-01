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
	public class CT_WritingStyle
	{
		private string langField;

		private string vendorIDField;

		private string dllVersionField;

		private ST_OnOff nlCheckField;

		private bool nlCheckFieldSpecified;

		private ST_OnOff checkStyleField;

		private string appNameField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string lang
		{
			get
			{
				return langField;
			}
			set
			{
				langField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string vendorID
		{
			get
			{
				return vendorIDField;
			}
			set
			{
				vendorIDField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string dllVersion
		{
			get
			{
				return dllVersionField;
			}
			set
			{
				dllVersionField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff nlCheck
		{
			get
			{
				return nlCheckField;
			}
			set
			{
				nlCheckField = value;
			}
		}

		[XmlIgnore]
		public bool nlCheckSpecified
		{
			get
			{
				return nlCheckFieldSpecified;
			}
			set
			{
				nlCheckFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff checkStyle
		{
			get
			{
				return checkStyleField;
			}
			set
			{
				checkStyleField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string appName
		{
			get
			{
				return appNameField;
			}
			set
			{
				appNameField = value;
			}
		}

		public static CT_WritingStyle Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_WritingStyle cT_WritingStyle = new CT_WritingStyle();
			cT_WritingStyle.lang = XmlHelper.ReadString(node.Attributes["w:lang"]);
			cT_WritingStyle.vendorID = XmlHelper.ReadString(node.Attributes["w:vendorID"]);
			cT_WritingStyle.dllVersion = XmlHelper.ReadString(node.Attributes["w:dllVersion"]);
			if (node.Attributes["w:nlCheck"] != null)
			{
				cT_WritingStyle.nlCheck = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:nlCheck"].Value);
			}
			if (node.Attributes["w:checkStyle"] != null)
			{
				cT_WritingStyle.checkStyle = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:checkStyle"].Value);
			}
			cT_WritingStyle.appName = XmlHelper.ReadString(node.Attributes["w:appName"]);
			return cT_WritingStyle;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:lang", lang);
			XmlHelper.WriteAttribute(sw, "w:vendorID", vendorID);
			XmlHelper.WriteAttribute(sw, "w:dllVersion", dllVersion);
			XmlHelper.WriteAttribute(sw, "w:nlCheck", nlCheck.ToString());
			XmlHelper.WriteAttribute(sw, "w:checkStyle", checkStyle.ToString());
			XmlHelper.WriteAttribute(sw, "w:appName", appName);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}

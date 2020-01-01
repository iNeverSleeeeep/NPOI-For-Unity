using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_PageMar
	{
		private string topField;

		private ulong rightField;

		private string bottomField;

		private ulong leftField;

		private ulong headerField;

		private ulong footerField;

		private ulong gutterField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string top
		{
			get
			{
				return topField;
			}
			set
			{
				topField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong right
		{
			get
			{
				return rightField;
			}
			set
			{
				rightField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string bottom
		{
			get
			{
				return bottomField;
			}
			set
			{
				bottomField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong left
		{
			get
			{
				return leftField;
			}
			set
			{
				leftField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong header
		{
			get
			{
				return headerField;
			}
			set
			{
				headerField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong footer
		{
			get
			{
				return footerField;
			}
			set
			{
				footerField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong gutter
		{
			get
			{
				return gutterField;
			}
			set
			{
				gutterField = value;
			}
		}

		public static CT_PageMar Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PageMar cT_PageMar = new CT_PageMar();
			cT_PageMar.top = XmlHelper.ReadString(node.Attributes["w:top"]);
			cT_PageMar.right = XmlHelper.ReadULong(node.Attributes["w:right"]);
			cT_PageMar.bottom = XmlHelper.ReadString(node.Attributes["w:bottom"]);
			cT_PageMar.left = XmlHelper.ReadULong(node.Attributes["w:left"]);
			cT_PageMar.header = XmlHelper.ReadULong(node.Attributes["w:header"]);
			cT_PageMar.footer = XmlHelper.ReadULong(node.Attributes["w:footer"]);
			cT_PageMar.gutter = XmlHelper.ReadULong(node.Attributes["w:gutter"]);
			return cT_PageMar;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:top", top);
			XmlHelper.WriteAttribute(sw, "w:right", (double)right);
			XmlHelper.WriteAttribute(sw, "w:bottom", bottom);
			XmlHelper.WriteAttribute(sw, "w:left", (double)left);
			XmlHelper.WriteAttribute(sw, "w:header", (double)header);
			XmlHelper.WriteAttribute(sw, "w:footer", (double)footer);
			XmlHelper.WriteAttribute(sw, "w:gutter", (double)gutter);
			sw.Write("/>");
		}
	}
}

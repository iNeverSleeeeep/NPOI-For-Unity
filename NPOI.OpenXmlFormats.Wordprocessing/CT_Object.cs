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
	public class CT_Object : CT_PictureBase
	{
		private CT_Control controlField;

		private ulong dxaOrigField;

		private bool dxaOrigFieldSpecified;

		private ulong dyaOrigField;

		private bool dyaOrigFieldSpecified;

		[XmlElement(Order = 0)]
		public CT_Control control
		{
			get
			{
				return controlField;
			}
			set
			{
				controlField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong dxaOrig
		{
			get
			{
				return dxaOrigField;
			}
			set
			{
				dxaOrigField = value;
			}
		}

		[XmlIgnore]
		public bool dxaOrigSpecified
		{
			get
			{
				return dxaOrigFieldSpecified;
			}
			set
			{
				dxaOrigFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong dyaOrig
		{
			get
			{
				return dyaOrigField;
			}
			set
			{
				dyaOrigField = value;
			}
		}

		[XmlIgnore]
		public bool dyaOrigSpecified
		{
			get
			{
				return dyaOrigFieldSpecified;
			}
			set
			{
				dyaOrigFieldSpecified = value;
			}
		}

		public static CT_Object Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Object cT_Object = new CT_Object();
			cT_Object.dxaOrig = XmlHelper.ReadULong(node.Attributes["w:dxaOrig"]);
			cT_Object.dyaOrig = XmlHelper.ReadULong(node.Attributes["w:dyaOrig"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "control")
				{
					cT_Object.control = CT_Control.Parse(childNode, namespaceManager);
				}
			}
			return cT_Object;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:dxaOrig", (double)dxaOrig);
			XmlHelper.WriteAttribute(sw, "w:dyaOrig", (double)dyaOrig);
			sw.Write(">");
			if (control != null)
			{
				control.Write(sw, "control");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}

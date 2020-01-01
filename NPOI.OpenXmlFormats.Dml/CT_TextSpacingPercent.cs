using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_TextSpacingPercent
	{
		private int valField;

		[XmlAttribute]
		public int val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		public static CT_TextSpacingPercent Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextSpacingPercent cT_TextSpacingPercent = new CT_TextSpacingPercent();
			cT_TextSpacingPercent.val = XmlHelper.ReadInt(node.Attributes["val"]);
			return cT_TextSpacingPercent;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val);
			sw.Write("/>");
		}
	}
}

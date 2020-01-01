using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_OutlinePr
	{
		private bool applyStylesField;

		private bool summaryBelowField;

		private bool summaryRightField;

		private bool showOutlineSymbolsField;

		[DefaultValue(false)]
		public bool applyStyles
		{
			get
			{
				return applyStylesField;
			}
			set
			{
				applyStylesField = value;
			}
		}

		[DefaultValue(true)]
		public bool summaryBelow
		{
			get
			{
				return summaryBelowField;
			}
			set
			{
				summaryBelowField = value;
			}
		}

		[DefaultValue(true)]
		public bool summaryRight
		{
			get
			{
				return summaryRightField;
			}
			set
			{
				summaryRightField = value;
			}
		}

		[DefaultValue(true)]
		public bool showOutlineSymbols
		{
			get
			{
				return showOutlineSymbolsField;
			}
			set
			{
				showOutlineSymbolsField = value;
			}
		}

		public static CT_OutlinePr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OutlinePr cT_OutlinePr = new CT_OutlinePr();
			cT_OutlinePr.applyStyles = XmlHelper.ReadBool(node.Attributes["applyStyles"]);
			cT_OutlinePr.summaryBelow = XmlHelper.ReadBool(node.Attributes["summaryBelow"]);
			cT_OutlinePr.summaryRight = XmlHelper.ReadBool(node.Attributes["summaryRight"]);
			cT_OutlinePr.showOutlineSymbols = XmlHelper.ReadBool(node.Attributes["showOutlineSymbols"]);
			return cT_OutlinePr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "applyStyles", applyStyles);
			XmlHelper.WriteAttribute(sw, "summaryBelow", summaryBelow);
			XmlHelper.WriteAttribute(sw, "summaryRight", summaryRight);
			XmlHelper.WriteAttribute(sw, "showOutlineSymbols", showOutlineSymbols);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_OutlinePr()
		{
			applyStylesField = false;
			summaryBelowField = true;
			summaryRightField = true;
			showOutlineSymbolsField = true;
		}
	}
}

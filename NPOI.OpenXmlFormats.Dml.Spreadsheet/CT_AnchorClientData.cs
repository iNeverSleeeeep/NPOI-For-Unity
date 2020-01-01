using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_AnchorClientData
	{
		private bool _fLocksWithSheet;

		private bool _fPrintsWithSheet;

		[XmlAttribute]
		public bool fLocksWithSheet
		{
			get
			{
				return _fLocksWithSheet;
			}
			set
			{
				_fLocksWithSheet = value;
			}
		}

		[XmlAttribute]
		public bool fPrintsWithSheet
		{
			get
			{
				return _fPrintsWithSheet;
			}
			set
			{
				_fPrintsWithSheet = value;
			}
		}

		public static CT_AnchorClientData Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_AnchorClientData cT_AnchorClientData = new CT_AnchorClientData();
			cT_AnchorClientData.fLocksWithSheet = XmlHelper.ReadBool(node.Attributes["fLocksWithSheet"]);
			cT_AnchorClientData.fPrintsWithSheet = XmlHelper.ReadBool(node.Attributes["fPrintsWithSheet"]);
			return cT_AnchorClientData;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "fLocksWithSheet", fLocksWithSheet, false);
			XmlHelper.WriteAttribute(sw, "fPrintsWithSheet", fPrintsWithSheet, false);
			sw.Write(">");
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}
	}
}

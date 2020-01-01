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
	public class CT_Fills
	{
		private List<CT_Fill> fillField;

		private uint countField;

		private bool countFieldSpecified;

		[XmlElement]
		public List<CT_Fill> fill
		{
			get
			{
				return fillField;
			}
			set
			{
				fillField = value;
			}
		}

		[XmlAttribute]
		public uint count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		[XmlIgnore]
		public bool countSpecified
		{
			get
			{
				return countFieldSpecified;
			}
			set
			{
				countFieldSpecified = value;
			}
		}

		public static CT_Fills Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Fills cT_Fills = new CT_Fills();
			cT_Fills.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_Fills.fill = new List<CT_Fill>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "fill")
				{
					cT_Fills.fill.Add(CT_Fill.Parse(childNode, namespaceManager));
				}
			}
			return cT_Fills;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", count);
			sw.Write(">");
			if (fill != null)
			{
				foreach (CT_Fill item in fill)
				{
					item.Write(sw, "fill");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public void SetFillArray(List<CT_Fill> array)
		{
			fillField = array;
		}
	}
}

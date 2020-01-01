using NPOI.OpenXml4Net.Util;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_Dxfs
	{
		private List<CT_Dxf> dxfField;

		private uint countField;

		private bool countFieldSpecified;

		[XmlElement]
		public List<CT_Dxf> dxf
		{
			get
			{
				return dxfField;
			}
			set
			{
				dxfField = value;
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

		public static CT_Dxfs Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Dxfs cT_Dxfs = new CT_Dxfs();
			cT_Dxfs.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_Dxfs.dxf = new List<CT_Dxf>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "dxf")
				{
					cT_Dxfs.dxf.Add(CT_Dxf.Parse(childNode, namespaceManager));
				}
			}
			return cT_Dxfs;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", (double)count, true);
			sw.Write(">");
			if (dxf != null)
			{
				foreach (CT_Dxf item in dxf)
				{
					item.Write(sw, "dxf");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}

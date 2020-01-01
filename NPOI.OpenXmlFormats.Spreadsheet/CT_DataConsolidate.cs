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
	public class CT_DataConsolidate
	{
		private CT_DataRefs dataRefsField;

		private ST_DataConsolidateFunction functionField;

		private bool leftLabelsField;

		private bool topLabelsField;

		private bool linkField;

		public CT_DataRefs dataRefs
		{
			get
			{
				return dataRefsField;
			}
			set
			{
				dataRefsField = value;
			}
		}

		[DefaultValue(ST_DataConsolidateFunction.sum)]
		public ST_DataConsolidateFunction function
		{
			get
			{
				return functionField;
			}
			set
			{
				functionField = value;
			}
		}

		[DefaultValue(false)]
		public bool leftLabels
		{
			get
			{
				return leftLabelsField;
			}
			set
			{
				leftLabelsField = value;
			}
		}

		[DefaultValue(false)]
		public bool topLabels
		{
			get
			{
				return topLabelsField;
			}
			set
			{
				topLabelsField = value;
			}
		}

		[DefaultValue(false)]
		public bool link
		{
			get
			{
				return linkField;
			}
			set
			{
				linkField = value;
			}
		}

		public CT_DataConsolidate()
		{
			dataRefsField = new CT_DataRefs();
			functionField = ST_DataConsolidateFunction.sum;
			leftLabelsField = false;
			topLabelsField = false;
			linkField = false;
		}

		public static CT_DataConsolidate Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DataConsolidate cT_DataConsolidate = new CT_DataConsolidate();
			if (node.Attributes["function"] != null)
			{
				cT_DataConsolidate.function = (ST_DataConsolidateFunction)Enum.Parse(typeof(ST_DataConsolidateFunction), node.Attributes["function"].Value);
			}
			cT_DataConsolidate.leftLabels = XmlHelper.ReadBool(node.Attributes["leftLabels"]);
			cT_DataConsolidate.topLabels = XmlHelper.ReadBool(node.Attributes["topLabels"]);
			cT_DataConsolidate.link = XmlHelper.ReadBool(node.Attributes["link"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "dataRefs")
				{
					cT_DataConsolidate.dataRefs = CT_DataRefs.Parse(childNode, namespaceManager);
				}
			}
			return cT_DataConsolidate;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "function", function.ToString());
			XmlHelper.WriteAttribute(sw, "leftLabels", leftLabels);
			XmlHelper.WriteAttribute(sw, "topLabels", topLabels);
			XmlHelper.WriteAttribute(sw, "link", link);
			sw.Write(">");
			if (dataRefs != null)
			{
				dataRefs.Write(sw, "dataRefs");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}

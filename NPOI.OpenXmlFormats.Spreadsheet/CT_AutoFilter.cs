using NPOI.OpenXml4Net.Util;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_AutoFilter
	{
		private List<CT_FilterColumn> filterColumnField;

		private CT_SortState sortStateField;

		private CT_ExtensionList extLstField;

		private string refField;

		[XmlElement]
		public List<CT_FilterColumn> filterColumn
		{
			get
			{
				return filterColumnField;
			}
			set
			{
				filterColumnField = value;
			}
		}

		[XmlElement]
		public CT_SortState sortState
		{
			get
			{
				return sortStateField;
			}
			set
			{
				sortStateField = value;
			}
		}

		[XmlElement]
		public CT_ExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[XmlAttribute]
		public string @ref
		{
			get
			{
				return refField;
			}
			set
			{
				refField = value;
			}
		}

		public static CT_AutoFilter Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_AutoFilter cT_AutoFilter = new CT_AutoFilter();
			cT_AutoFilter.@ref = XmlHelper.ReadString(node.Attributes["ref"]);
			cT_AutoFilter.filterColumn = new List<CT_FilterColumn>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sortState")
				{
					cT_AutoFilter.sortState = CT_SortState.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_AutoFilter.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "filterColumn")
				{
					cT_AutoFilter.filterColumn.Add(CT_FilterColumn.Parse(childNode, namespaceManager));
				}
			}
			return cT_AutoFilter;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "ref", @ref);
			sw.Write(">");
			if (sortState != null)
			{
				sortState.Write(sw, "sortState");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			if (filterColumn != null)
			{
				foreach (CT_FilterColumn item in filterColumn)
				{
					item.Write(sw, "filterColumn");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}

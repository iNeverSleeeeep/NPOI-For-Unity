using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_SortState
	{
		private List<CT_SortCondition> sortConditionField;

		private CT_ExtensionList extLstField;

		private bool columnSortField;

		private bool caseSensitiveField;

		private ST_SortMethod sortMethodField;

		private string refField;

		[XmlElement]
		public List<CT_SortCondition> sortCondition
		{
			get
			{
				return sortConditionField;
			}
			set
			{
				sortConditionField = value;
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

		[DefaultValue(false)]
		[XmlAttribute]
		public bool columnSort
		{
			get
			{
				return columnSortField;
			}
			set
			{
				columnSortField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool caseSensitive
		{
			get
			{
				return caseSensitiveField;
			}
			set
			{
				caseSensitiveField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_SortMethod.none)]
		public ST_SortMethod sortMethod
		{
			get
			{
				return sortMethodField;
			}
			set
			{
				sortMethodField = value;
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

		public CT_SortState()
		{
			columnSortField = false;
			caseSensitiveField = false;
			sortMethodField = ST_SortMethod.none;
		}

		public static CT_SortState Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SortState cT_SortState = new CT_SortState();
			if (node.Attributes["columnSort"] != null)
			{
				cT_SortState.columnSort = XmlHelper.ReadBool(node.Attributes["columnSort"]);
			}
			if (node.Attributes["caseSensitive"] != null)
			{
				cT_SortState.caseSensitive = XmlHelper.ReadBool(node.Attributes["caseSensitive"]);
			}
			if (node.Attributes["sortMethod"] != null)
			{
				cT_SortState.sortMethod = (ST_SortMethod)Enum.Parse(typeof(ST_SortMethod), node.Attributes["sortMethod"].Value);
			}
			cT_SortState.@ref = XmlHelper.ReadString(node.Attributes["ref"]);
			cT_SortState.sortCondition = new List<CT_SortCondition>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_SortState.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sortCondition")
				{
					cT_SortState.sortCondition.Add(CT_SortCondition.Parse(childNode, namespaceManager));
				}
			}
			return cT_SortState;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "columnSort", columnSort);
			XmlHelper.WriteAttribute(sw, "caseSensitive", caseSensitive);
			XmlHelper.WriteAttribute(sw, "sortMethod", sortMethod.ToString());
			XmlHelper.WriteAttribute(sw, "ref", @ref);
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			if (sortCondition != null)
			{
				foreach (CT_SortCondition item in sortCondition)
				{
					item.Write(sw, "sortCondition");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}

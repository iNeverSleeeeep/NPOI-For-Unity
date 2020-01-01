using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_SortCondition
	{
		private bool descendingField;

		private ST_SortBy sortByField;

		private string refField;

		private string customListField;

		private uint dxfIdField;

		private bool dxfIdFieldSpecified;

		private ST_IconSetType iconSetField;

		private uint iconIdField;

		private bool iconIdFieldSpecified;

		[DefaultValue(false)]
		public bool descending
		{
			get
			{
				return descendingField;
			}
			set
			{
				descendingField = value;
			}
		}

		[DefaultValue(ST_SortBy.value)]
		public ST_SortBy sortBy
		{
			get
			{
				return sortByField;
			}
			set
			{
				sortByField = value;
			}
		}

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

		public string customList
		{
			get
			{
				return customListField;
			}
			set
			{
				customListField = value;
			}
		}

		public uint dxfId
		{
			get
			{
				return dxfIdField;
			}
			set
			{
				dxfIdField = value;
			}
		}

		[XmlIgnore]
		public bool dxfIdSpecified
		{
			get
			{
				return dxfIdFieldSpecified;
			}
			set
			{
				dxfIdFieldSpecified = value;
			}
		}

		[DefaultValue(ST_IconSetType.Item3Arrows)]
		public ST_IconSetType iconSet
		{
			get
			{
				return iconSetField;
			}
			set
			{
				iconSetField = value;
			}
		}

		public uint iconId
		{
			get
			{
				return iconIdField;
			}
			set
			{
				iconIdField = value;
			}
		}

		[XmlIgnore]
		public bool iconIdSpecified
		{
			get
			{
				return iconIdFieldSpecified;
			}
			set
			{
				iconIdFieldSpecified = value;
			}
		}

		public CT_SortCondition()
		{
			descendingField = false;
			sortByField = ST_SortBy.value;
			iconSetField = ST_IconSetType.Item3Arrows;
		}

		public static CT_SortCondition Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SortCondition cT_SortCondition = new CT_SortCondition();
			if (node.Attributes["descending"] != null)
			{
				cT_SortCondition.descending = XmlHelper.ReadBool(node.Attributes["descending"]);
			}
			if (node.Attributes["sortBy"] != null)
			{
				cT_SortCondition.sortBy = (ST_SortBy)Enum.Parse(typeof(ST_SortBy), node.Attributes["sortBy"].Value);
			}
			cT_SortCondition.@ref = XmlHelper.ReadString(node.Attributes["ref"]);
			cT_SortCondition.customList = XmlHelper.ReadString(node.Attributes["customList"]);
			if (node.Attributes["dxfId"] != null)
			{
				cT_SortCondition.dxfId = XmlHelper.ReadUInt(node.Attributes["dxfId"]);
			}
			if (node.Attributes["iconSet"] != null)
			{
				cT_SortCondition.iconSet = (ST_IconSetType)Enum.Parse(typeof(ST_IconSetType), node.Attributes["iconSet"].Value);
			}
			if (node.Attributes["iconId"] != null)
			{
				cT_SortCondition.iconId = XmlHelper.ReadUInt(node.Attributes["iconId"]);
			}
			return cT_SortCondition;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "descending", descending);
			XmlHelper.WriteAttribute(sw, "sortBy", sortBy.ToString());
			XmlHelper.WriteAttribute(sw, "ref", @ref);
			XmlHelper.WriteAttribute(sw, "customList", customList);
			XmlHelper.WriteAttribute(sw, "dxfId", dxfId);
			XmlHelper.WriteAttribute(sw, "iconSet", iconSet.ToString());
			XmlHelper.WriteAttribute(sw, "iconId", iconId);
			sw.Write("/>");
		}
	}
}

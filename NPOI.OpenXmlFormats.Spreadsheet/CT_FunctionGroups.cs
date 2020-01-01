using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_FunctionGroups
	{
		private List<CT_FunctionGroup> functionGroupField;

		private uint builtInGroupCountField;

		public List<CT_FunctionGroup> functionGroup
		{
			get
			{
				return functionGroupField;
			}
			set
			{
				functionGroupField = value;
			}
		}

		[DefaultValue(typeof(uint), "16")]
		public uint builtInGroupCount
		{
			get
			{
				return builtInGroupCountField;
			}
			set
			{
				builtInGroupCountField = value;
			}
		}

		public CT_FunctionGroups()
		{
			builtInGroupCountField = 16u;
		}

		public static CT_FunctionGroups Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FunctionGroups cT_FunctionGroups = new CT_FunctionGroups();
			cT_FunctionGroups.builtInGroupCount = XmlHelper.ReadUInt(node.Attributes["builtInGroupCount"]);
			cT_FunctionGroups.functionGroup = new List<CT_FunctionGroup>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "functionGroup")
				{
					cT_FunctionGroups.functionGroup.Add(CT_FunctionGroup.Parse(childNode, namespaceManager));
				}
			}
			return cT_FunctionGroups;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "builtInGroupCount", builtInGroupCount);
			sw.Write(">");
			if (functionGroup != null)
			{
				foreach (CT_FunctionGroup item in functionGroup)
				{
					item.Write(sw, "functionGroup");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}

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
	public class CT_Scenario
	{
		private List<CT_InputCells> inputCellsField;

		private string nameField;

		private bool lockedField;

		private bool hiddenField;

		private uint countField;

		private bool countFieldSpecified;

		private string userField;

		private string commentField;

		public List<CT_InputCells> inputCells
		{
			get
			{
				return inputCellsField;
			}
			set
			{
				inputCellsField = value;
			}
		}

		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[DefaultValue(false)]
		public bool locked
		{
			get
			{
				return lockedField;
			}
			set
			{
				lockedField = value;
			}
		}

		[DefaultValue(false)]
		public bool hidden
		{
			get
			{
				return hiddenField;
			}
			set
			{
				hiddenField = value;
			}
		}

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

		public string user
		{
			get
			{
				return userField;
			}
			set
			{
				userField = value;
			}
		}

		public string comment
		{
			get
			{
				return commentField;
			}
			set
			{
				commentField = value;
			}
		}

		public CT_Scenario()
		{
			lockedField = false;
			hiddenField = false;
		}

		public static CT_Scenario Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Scenario cT_Scenario = new CT_Scenario();
			cT_Scenario.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_Scenario.locked = XmlHelper.ReadBool(node.Attributes["locked"]);
			cT_Scenario.hidden = XmlHelper.ReadBool(node.Attributes["hidden"]);
			cT_Scenario.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_Scenario.user = XmlHelper.ReadString(node.Attributes["user"]);
			cT_Scenario.comment = XmlHelper.ReadString(node.Attributes["comment"]);
			cT_Scenario.inputCells = new List<CT_InputCells>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "inputCells")
				{
					cT_Scenario.inputCells.Add(CT_InputCells.Parse(childNode, namespaceManager));
				}
			}
			return cT_Scenario;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "locked", locked);
			XmlHelper.WriteAttribute(sw, "hidden", hidden);
			XmlHelper.WriteAttribute(sw, "count", count);
			XmlHelper.WriteAttribute(sw, "user", user);
			XmlHelper.WriteAttribute(sw, "comment", comment);
			if (inputCells != null)
			{
				sw.Write(">");
				foreach (CT_InputCells inputCell in inputCells)
				{
					inputCell.Write(sw, "inputCells");
				}
				sw.Write(string.Format("</{0}>", nodeName));
			}
			else
			{
				sw.Write("/>");
			}
		}
	}
}

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
	public class CT_Row
	{
		private List<CT_Cell> cField;

		private CT_ExtensionList extLstField;

		private uint rField;

		private string spansField;

		private uint sField;

		private bool customFormatField;

		private double htField = -1.0;

		private bool hiddenField;

		private bool customHeightField;

		private byte outlineLevelField;

		private bool collapsedField;

		private bool thickTopField;

		private bool thickBotField;

		private bool phField;

		[XmlElement("c")]
		public List<CT_Cell> c
		{
			get
			{
				return cField;
			}
			set
			{
				cField = value;
			}
		}

		[XmlElement("extLst")]
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

		[XmlAttribute("r")]
		public uint r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}

		[XmlAttribute]
		public string spans
		{
			get
			{
				return spansField;
			}
			set
			{
				spansField = value;
			}
		}

		[XmlAttribute]
		public uint s
		{
			get
			{
				return sField;
			}
			set
			{
				sField = value;
			}
		}

		[XmlAttribute]
		public bool customFormat
		{
			get
			{
				return customFormatField;
			}
			set
			{
				customFormatField = value;
			}
		}

		[XmlAttribute]
		public double ht
		{
			get
			{
				return htField;
			}
			set
			{
				htField = value;
			}
		}

		[XmlAttribute]
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

		[XmlAttribute]
		public bool customHeight
		{
			get
			{
				return customHeightField;
			}
			set
			{
				customHeightField = value;
			}
		}

		[DefaultValue(typeof(byte), "0")]
		[XmlAttribute]
		public byte outlineLevel
		{
			get
			{
				return outlineLevelField;
			}
			set
			{
				outlineLevelField = value;
			}
		}

		[XmlAttribute]
		public bool collapsed
		{
			get
			{
				return collapsedField;
			}
			set
			{
				collapsedField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool thickTop
		{
			get
			{
				return thickTopField;
			}
			set
			{
				thickTopField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool thickBot
		{
			get
			{
				return thickBotField;
			}
			set
			{
				thickBotField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool ph
		{
			get
			{
				return phField;
			}
			set
			{
				phField = value;
			}
		}

		public static CT_Row Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Row cT_Row = new CT_Row();
			cT_Row.r = XmlHelper.ReadUInt(node.Attributes["r"]);
			cT_Row.spans = XmlHelper.ReadString(node.Attributes["spans"]);
			cT_Row.s = XmlHelper.ReadUInt(node.Attributes["s"]);
			cT_Row.customFormat = XmlHelper.ReadBool(node.Attributes["customFormat"]);
			if (node.Attributes["ht"] != null)
			{
				cT_Row.ht = XmlHelper.ReadDouble(node.Attributes["ht"]);
			}
			cT_Row.hidden = XmlHelper.ReadBool(node.Attributes["hidden"]);
			cT_Row.outlineLevel = XmlHelper.ReadByte(node.Attributes["outlineLevel"]);
			cT_Row.customHeight = XmlHelper.ReadBool(node.Attributes["customHeight"]);
			cT_Row.collapsed = XmlHelper.ReadBool(node.Attributes["collapsed"]);
			cT_Row.thickTop = XmlHelper.ReadBool(node.Attributes["thickTop"]);
			cT_Row.thickBot = XmlHelper.ReadBool(node.Attributes["thickBot"]);
			cT_Row.ph = XmlHelper.ReadBool(node.Attributes["ph"]);
			cT_Row.c = new List<CT_Cell>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_Row.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "c")
				{
					cT_Row.c.Add(CT_Cell.Parse(childNode, namespaceManager));
				}
			}
			return cT_Row;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r", r);
			XmlHelper.WriteAttribute(sw, "spans", spans);
			XmlHelper.WriteAttribute(sw, "s", s);
			XmlHelper.WriteAttribute(sw, "customFormat", customFormat, false);
			if (ht >= 0.0)
			{
				XmlHelper.WriteAttribute(sw, "ht", ht);
			}
			XmlHelper.WriteAttribute(sw, "hidden", hidden, false);
			XmlHelper.WriteAttribute(sw, "customHeight", customHeight, false);
			XmlHelper.WriteAttribute(sw, "outlineLevel", outlineLevel);
			XmlHelper.WriteAttribute(sw, "collapsed", collapsed, false);
			XmlHelper.WriteAttribute(sw, "thickTop", thickTop, false);
			XmlHelper.WriteAttribute(sw, "thickBot", thickBot, false);
			XmlHelper.WriteAttribute(sw, "ph", ph, false);
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			if (c != null)
			{
				foreach (CT_Cell item in c)
				{
					item.Write(sw, "c");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public void Set(CT_Row row)
		{
			cField = row.cField;
			extLstField = row.extLstField;
			rField = row.rField;
			spansField = row.spansField;
			sField = row.sField;
			customFormatField = row.customFormatField;
			htField = row.htField;
			hiddenField = row.hiddenField;
			customHeightField = row.customHeightField;
			outlineLevelField = row.outlineLevelField;
			collapsedField = row.collapsedField;
			thickTopField = row.thickTopField;
			thickBotField = row.thickBotField;
			phField = row.phField;
		}

		public CT_Cell AddNewC()
		{
			if (cField == null)
			{
				cField = new List<CT_Cell>();
			}
			CT_Cell cT_Cell = new CT_Cell();
			cField.Add(cT_Cell);
			return cT_Cell;
		}

		public void UnsetCollapsed()
		{
			collapsedField = false;
		}

		public void UnsetS()
		{
			sField = 0u;
		}

		public void UnsetCustomFormat()
		{
			customFormatField = false;
		}

		public bool IsSetHidden()
		{
			return hiddenField;
		}

		public bool IsSetCollapsed()
		{
			return collapsedField;
		}

		public bool IsSetHt()
		{
			return htField >= 0.0;
		}

		public void unSetHt()
		{
			htField = -1.0;
		}

		public bool IsSetCustomHeight()
		{
			return customHeightField;
		}

		public void unSetCustomHeight()
		{
			customHeightField = false;
		}

		public bool IsSetS()
		{
			return sField != 0;
		}

		public void unsetHidden()
		{
			hiddenField = false;
		}

		public int SizeOfCArray()
		{
			if (cField != null)
			{
				return cField.Count;
			}
			return 0;
		}

		public CT_Cell GetCArray(int index)
		{
			if (cField != null)
			{
				return cField[index];
			}
			return null;
		}

		public void SetCArray(CT_Cell[] array)
		{
			cField = new List<CT_Cell>(array);
		}
	}
}

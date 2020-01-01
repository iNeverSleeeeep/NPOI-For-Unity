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
	public class CT_Col
	{
		private uint minField;

		private uint maxField;

		private double widthField;

		private bool widthSpecifiedField;

		private uint styleField;

		private bool styleSpecifiedField;

		private bool hiddenField;

		private bool bestFitField;

		private bool customWidthField;

		private bool phoneticField;

		private byte outlineLevelField;

		private bool collapsedField = true;

		private bool collapsedSpecifiedField = true;

		[XmlAttribute]
		public uint min
		{
			get
			{
				return minField;
			}
			set
			{
				minField = value;
			}
		}

		[XmlAttribute]
		public uint max
		{
			get
			{
				return maxField;
			}
			set
			{
				maxField = value;
			}
		}

		[XmlAttribute]
		public double width
		{
			get
			{
				return widthField;
			}
			set
			{
				widthField = value;
				widthSpecified = true;
			}
		}

		[XmlIgnore]
		public bool widthSpecified
		{
			get
			{
				return widthSpecifiedField;
			}
			set
			{
				widthSpecifiedField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		[XmlIgnore]
		public bool styleSpecified
		{
			get
			{
				return styleSpecifiedField;
			}
			set
			{
				styleSpecifiedField = value;
			}
		}

		[XmlAttribute]
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

		[DefaultValue(false)]
		[XmlAttribute]
		public bool bestFit
		{
			get
			{
				return bestFitField;
			}
			set
			{
				bestFitField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool customWidth
		{
			get
			{
				return customWidthField;
			}
			set
			{
				customWidthField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool phonetic
		{
			get
			{
				return phoneticField;
			}
			set
			{
				phoneticField = value;
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

		[DefaultValue(true)]
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
				collapsedSpecifiedField = true;
			}
		}

		[XmlIgnore]
		public bool collapsedSpecified
		{
			get
			{
				return collapsedSpecifiedField;
			}
			set
			{
				collapsedSpecifiedField = value;
			}
		}

		public bool IsSetBestFit()
		{
			return bestFitField;
		}

		public bool IsSetCustomWidth()
		{
			return customWidthField;
		}

		public bool IsSetHidden()
		{
			return hiddenField;
		}

		public bool IsSetStyle()
		{
			return true;
		}

		public bool IsSetWidth()
		{
			return widthField > 0.0;
		}

		public bool IsSetCollapsed()
		{
			return collapsedSpecifiedField;
		}

		public bool IsSetPhonetic()
		{
			return phoneticField;
		}

		public bool IsSetOutlineLevel()
		{
			return outlineLevelField != 0;
		}

		public void UnsetHidden()
		{
			hiddenField = false;
		}

		public void UnsetCollapsed()
		{
			collapsedField = true;
			collapsedSpecified = false;
		}

		public static CT_Col Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Col cT_Col = new CT_Col();
			cT_Col.min = XmlHelper.ReadUInt(node.Attributes["min"]);
			cT_Col.max = XmlHelper.ReadUInt(node.Attributes["max"]);
			cT_Col.width = XmlHelper.ReadDouble(node.Attributes["width"]);
			cT_Col.style = XmlHelper.ReadUInt(node.Attributes["style"]);
			cT_Col.hidden = XmlHelper.ReadBool(node.Attributes["hidden"]);
			cT_Col.bestFit = XmlHelper.ReadBool(node.Attributes["bestFit"]);
			cT_Col.outlineLevel = XmlHelper.ReadByte(node.Attributes["outlineLevel"]);
			cT_Col.customWidth = XmlHelper.ReadBool(node.Attributes["customWidth"]);
			cT_Col.phonetic = XmlHelper.ReadBool(node.Attributes["phonetic"]);
			cT_Col.collapsed = XmlHelper.ReadBool(node.Attributes["collapsed"]);
			return cT_Col;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "min", min);
			XmlHelper.WriteAttribute(sw, "max", max);
			XmlHelper.WriteAttribute(sw, "width", width);
			XmlHelper.WriteAttribute(sw, "style", (double)style, true);
			XmlHelper.WriteAttribute(sw, "hidden", hidden, false);
			XmlHelper.WriteAttribute(sw, "bestFit", bestFit, false);
			XmlHelper.WriteAttribute(sw, "customWidth", customWidth, false);
			XmlHelper.WriteAttribute(sw, "phonetic", phonetic, false);
			XmlHelper.WriteAttribute(sw, "outlineLevel", outlineLevel);
			XmlHelper.WriteAttribute(sw, "collapsed", collapsed, false);
			sw.Write("/>");
		}
	}
}
